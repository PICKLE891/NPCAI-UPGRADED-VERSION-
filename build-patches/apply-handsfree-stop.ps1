param(
    [string]$WorkRoot = "work"
)

$ErrorActionPreference = "Stop"

function Read-Normalized([string]$Path) {
    return (Get-Content $Path -Raw).Replace("`r`n", "`n")
}

function Replace-Required([string]$Text, [string]$Old, [string]$New, [string]$Label) {
    $Old = $Old.Replace("`r`n", "`n").TrimStart([char]10)
    $New = $New.Replace("`r`n", "`n").TrimStart([char]10)
    if (-not $Text.Contains($Old)) {
        throw "Hands-free stop patch point not found: $Label"
    }
    return $Text.Replace($Old, $New)
}

$src = Join-Path $WorkRoot "src"

# PluginMain: automatically listen during traffic stops and stopped-ped contacts.
$mainPath = Join-Path $src "PluginMain.cs"
$text = Read-Normalized $mainPath
$text = Replace-Required $text @'
        private static bool _busy;
        private static DateTime _lastKeyUtc = DateTime.MinValue;
'@ @'
        private static bool _busy;
        private static DateTime _lastKeyUtc = DateTime.MinValue;
        private static DateTime _nextHandsFreeUtc = DateTime.MinValue;
'@ "PluginMain hands-free state"

$text = Replace-Required $text @'
                if (!CanReadKey())
                    continue;
'@ @'
                if (TryStartHandsFreeStopConversation())
                    continue;

                if (!CanReadKey())
                    continue;
'@ "PluginMain hands-free loop trigger"

$text = Replace-Required $text @'
        private static void RunConversation(bool radioMode)
'@ @'
        private static void RunConversation(bool radioMode, bool handsFree = false)
'@ "PluginMain conversation signature"

$text = Replace-Required $text @'
                Game.DisplayHelp(
                    (radioMode ? "Radio " : "Talk ") +
                    RoleDetector.Label(role) +
                    sourceText +
                    ": speak now...");
'@ @'
                Game.DisplayHelp(
                    handsFree
                        ? "NPC AI Talk hands-free: speak normally..."
                        : (radioMode ? "Radio " : "Talk ") +
                          RoleDetector.Label(role) +
                          sourceText +
                          ": speak now...");
'@ "PluginMain hands-free prompt"

$text = Replace-Required $text @'
                if (string.IsNullOrWhiteSpace(playerSpeech))
                {
                    Game.DisplayNotification(
                        "~o~NPC AI Talk:~s~ no speech detected.");
                    return;
                }
'@ @'
                if (string.IsNullOrWhiteSpace(playerSpeech))
                {
                    if (!handsFree)
                    {
                        Game.DisplayNotification(
                            "~o~NPC AI Talk:~s~ no speech detected.");
                    }
                    return;
                }
'@ "PluginMain quiet hands-free silence"

$text = Replace-Required $text @'
        private static bool CanReadKey()
'@ @'
        private static bool TryStartHandsFreeStopConversation()
        {
            if (_cfg == null || !_cfg.HandsFreeTalk || _busy)
                return false;

            if (DateTime.UtcNow < _nextHandsFreeUtc)
                return false;

            _nextHandsFreeUtc =
                DateTime.UtcNow.AddMilliseconds(
                    Math.Max(350, _cfg.HandsFreeRetryMilliseconds));

            Ped player = Game.LocalPlayer.Character;
            TargetResult result = TargetResolver.ResolveTalkTarget(player, _cfg);

            if (result == null || !TargetResolver.IsValid(result.Ped, player))
                return false;

            bool isStop = false;

            try
            {
                isStop =
                    string.Equals(result.Source, "TRAFFIC STOP", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(result.Source, "STOPPED PED", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(TargetResolver.SceneTag(player), "traffic stop", StringComparison.OrdinalIgnoreCase) ||
                    LspdfrBridge.IsPedStoppedByPlayer(result.Ped);
            }
            catch { }

            if (_cfg.HandsFreeStopsOnly && !isStop)
                return false;

            try
            {
                if (result.Ped.DistanceTo(player) > Math.Max(6f, _cfg.TalkDistance))
                    return false;
            }
            catch
            {
                return false;
            }

            _busy = true;
            GameFiber.StartNew(
                () => RunConversation(false, true),
                "NPC AI Talk Hands-Free Stop");
            return true;
        }

        private static bool CanReadKey()
'@ "PluginMain hands-free starter method"

Set-Content $mainPath $text -Encoding UTF8

# Config: hands-free mode is ON by default, including for users with an older INI.
$configPath = Join-Path $src "PluginConfig.cs"
$text = Read-Normalized $configPath
$text = Replace-Required $text @'
        public bool ReplyInDetectedLanguage = true;

        public bool ShowSubtitles = true;
'@ @'
        public bool ReplyInDetectedLanguage = true;
        public bool HandsFreeTalk = true;
        public bool HandsFreeStopsOnly = true;
        public int HandsFreeRetryMilliseconds = 700;

        public bool ShowSubtitles = true;
'@ "PluginConfig hands-free fields"

$text = Replace-Required $text @'
            cfg.ReplyInDetectedLanguage = ParseBool(Get(map, "Voice.ReplyInDetectedLanguage", "true"), cfg.ReplyInDetectedLanguage);

            cfg.ShowSubtitles = ParseBool(Get(map, "Display.ShowSubtitles", "true"), cfg.ShowSubtitles);
'@ @'
            cfg.ReplyInDetectedLanguage = ParseBool(Get(map, "Voice.ReplyInDetectedLanguage", "true"), cfg.ReplyInDetectedLanguage);
            cfg.HandsFreeTalk = ParseBool(Get(map, "Voice.HandsFreeTalk", "true"), cfg.HandsFreeTalk);
            cfg.HandsFreeStopsOnly = ParseBool(Get(map, "Voice.HandsFreeStopsOnly", "true"), cfg.HandsFreeStopsOnly);
            cfg.HandsFreeRetryMilliseconds = ParseInt(Get(map, "Voice.HandsFreeRetryMilliseconds", "700"), cfg.HandsFreeRetryMilliseconds);

            cfg.ShowSubtitles = ParseBool(Get(map, "Display.ShowSubtitles", "true"), cfg.ShowSubtitles);
'@ "PluginConfig hands-free parsing"

$text = Replace-Required $text @'
FallbackWindowsTTS=true

[Backup]
'@ @'
FallbackWindowsTTS=true
HandsFreeTalk=true
HandsFreeStopsOnly=true
HandsFreeRetryMilliseconds=700

[Backup]
'@ "PluginConfig hands-free default INI"
Set-Content $configPath $text -Encoding UTF8

# Included INI: expose hands-free settings for testers.
$iniPath = Join-Path $WorkRoot "NpcAiTalk.ini"
$text = Read-Normalized $iniPath
$text = Replace-Required $text @'
FallbackWindowsTTS=true

[Backup]
'@ @'
FallbackWindowsTTS=true
HandsFreeTalk=true
HandsFreeStopsOnly=true
HandsFreeRetryMilliseconds=700

[Backup]
'@ "NpcAiTalk.ini hands-free settings"
Set-Content $iniPath $text -Encoding UTF8

# OpenAI/local path: pass real scene context/memory into the local brain, then tighten truth/compliance rules.
$clientPath = Join-Path $src "OpenAiNpcClient.cs"
$text = Read-Normalized $clientPath
$text = Replace-Required $text @'
                return LocalNpcBrain.Generate(playerSpeech, role, radioMode);
'@ @'
                return LocalNpcBrain.Generate(playerSpeech, role, radioMode, sceneContext, memory);
'@ "OpenAiNpcClient local context/memory"

$text = Replace-Required $text @'
                    sb.AppendLine(
                        "React naturally. Compliance is common but not guaranteed. FLEE should be uncommon unless the player's words and scene support it.");
                    sb.AppendLine(
                        "Allowed actions: STAY, HOLD, HANDSUP, EXIT_VEHICLE, FOLLOW, MOVE_CLOSER, FLEE.");
'@ @'
                    sb.AppendLine(
                        "Be truthful and grounded in the supplied scene/persona data. Never invent a reason for the stop, identity detail, warrant, citation, contraband, impairment, weapon, evidence, or admission that is not supplied.");
                    sb.AppendLine(
                        "During traffic stops and stopped-ped contacts, be calm and cooperative. Do not resist or flee. If a fact is unknown, say you do not know rather than making it up.");
                    sb.AppendLine(
                        "Allowed actions: STAY, HOLD, HANDSUP, EXIT_VEHICLE, FOLLOW, MOVE_CLOSER.");
'@ "OpenAI traffic-stop truth/compliance prompt"
Set-Content $clientPath $text -Encoding UTF8

Write-Host "Applied hands-free, case-aware traffic-stop patch successfully."
