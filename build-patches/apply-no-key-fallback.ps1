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
        throw "No-key fallback patch point not found: $Label"
    }
    return $Text.Replace($Old, $New)
}

$src = Join-Path $WorkRoot "src"
Copy-Item "build-patches/LocalNpcBrain.cs" (Join-Path $src "LocalNpcBrain.cs") -Force

# OpenAI client: use local NPC brain when no key exists, and skip OpenAI TTS without a key.
$clientPath = Join-Path $src "OpenAiNpcClient.cs"
$text = Read-Normalized $clientPath
$text = Replace-Required $text @'
        {
            string systemPrompt = BuildSystemPrompt(role, radioMode);
'@ @'
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                return LocalNpcBrain.Generate(playerSpeech, role, radioMode);

            string systemPrompt = BuildSystemPrompt(role, radioMode);
'@ "OpenAiNpcClient.Generate fallback"
$text = Replace-Required $text @'
            if (_cfg.UseOpenAITts)
            {
'@ @'
            if (_cfg.UseOpenAITts && !string.IsNullOrWhiteSpace(apiKey))
            {
'@ "OpenAiNpcClient.Speak key guard"
Set-Content $clientPath $text -Encoding UTF8

# Main conversation flow: no key is no longer fatal; Windows Speech is used for input.
$mainPath = Join-Path $src "PluginMain.cs"
$text = Read-Normalized $mainPath
$text = Replace-Required $text @'
            if (_cfg.UseOpenAITts)
            {
                Game.DisplayNotification(
                    "~b~NPC AI Talk:~s~ NPC voice audio is AI-generated.");
            }
'@ @'
            string startupApiKey =
                Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            if (_cfg.UseOpenAITts && !string.IsNullOrWhiteSpace(startupApiKey))
            {
                Game.DisplayNotification(
                    "~b~NPC AI Talk:~s~ NPC voice audio is AI-generated.");
            }
            else if (string.IsNullOrWhiteSpace(startupApiKey))
            {
                Game.DisplayNotification(
                    "~g~NPC AI Talk:~s~ local mode active - no API key required.");
            }
'@ "PluginMain startup mode notification"
$text = Replace-Required $text @'
                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    Game.DisplayNotification(
                        "~r~NPC AI Talk:~s~ OPENAI_API_KEY is not set.");
                    return;
                }

'@ '' "PluginMain remove API-key hard stop"
$text = Replace-Required $text @'
                Game.DisplayNotification(
                    "~b~NPC AI Talk:~s~ thinking...");
'@ @'
                Game.DisplayNotification(
                    string.IsNullOrWhiteSpace(apiKey)
                        ? "~g~NPC AI Talk:~s~ local response..."
                        : "~b~NPC AI Talk:~s~ thinking...");
'@ "PluginMain local response notification"
$text = Replace-Required $text @'
            if (!_cfg.UseOpenAITranscription)
'@ @'
            if (!_cfg.UseOpenAITranscription || string.IsNullOrWhiteSpace(apiKey))
'@ "PluginMain Windows transcription fallback"
Set-Content $mainPath $text -Encoding UTF8

# Backup officer voice should also fall back to Windows TTS without a key.
$backupPath = Join-Path $src "BackupAutonomy.cs"
$text = Read-Normalized $backupPath
$text = Replace-Required $text @'
            if (client != null &&
                !string.IsNullOrWhiteSpace(apiKey))
'@ @'
            if (client != null)
'@ "BackupAutonomy Windows TTS fallback"
Set-Content $backupPath $text -Encoding UTF8

# Add the local brain to the project.
$projPath = Join-Path $WorkRoot "NpcAiTalk.csproj"
$text = Read-Normalized $projPath
if (-not $text.Contains("LocalNpcBrain.cs")) {
    $text = Replace-Required $text @'
    <Compile Include="src\ActionExecutor.cs" />
'@ @'
    <Compile Include="src\ActionExecutor.cs" />
    <Compile Include="src\LocalNpcBrain.cs" />
'@ "NpcAiTalk.csproj LocalNpcBrain include"
}
Set-Content $projPath $text -Encoding UTF8

# Make the included configuration explicitly say the key is optional.
$iniPath = Join-Path $WorkRoot "NpcAiTalk.ini"
$text = Read-Normalized $iniPath
$text = Replace-Required $text @'
; Keep your OpenAI API key OUT of this file.
; Set OPENAI_API_KEY as a Windows environment variable.
'@ @'
; OpenAI API key is OPTIONAL. The plugin works without one in local mode.
; To enable advanced OpenAI conversation/voice features, set OPENAI_API_KEY as a Windows environment variable.
; Keep your API key OUT of this file.
'@ "NpcAiTalk.ini optional API key note"
Set-Content $iniPath $text -Encoding UTF8

# Keep the generated default INI consistent.
$configPath = Join-Path $src "PluginConfig.cs"
$text = Read-Normalized $configPath
$text = Replace-Required $text @'
; Do NOT put your OpenAI API key in this file.
; Use the Windows environment variable OPENAI_API_KEY.
'@ @'
; OpenAI API key is OPTIONAL. Without one, local command/response mode and Windows speech/TTS are used.
; To enable advanced OpenAI features, use the Windows environment variable OPENAI_API_KEY.
; Do NOT put your API key in this file.
'@ "PluginConfig default INI optional API key note"
Set-Content $configPath $text -Encoding UTF8

Write-Host "Applied no-API-key fallback patch successfully."
