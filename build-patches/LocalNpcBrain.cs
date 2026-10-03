using System;

namespace NpcAiTalk
{
    internal static class LocalNpcBrain
    {
        public static NpcDecision Generate(
            string playerSpeech,
            NpcRole role,
            bool radioMode,
            string context,
            string memory)
        {
            string speech = (playerSpeech ?? "").Trim();
            string lower = speech.ToLowerInvariant();
            string action = "STAY";
            string reply;

            switch (role)
            {
                case NpcRole.LawEnforcement:
                    if (HasAny(lower,
                        "cover the suspect", "cover suspect", "cover him", "cover her",
                        "watch the suspect", "watch him", "watch her"))
                    {
                        action = "COVER_SUSPECT";
                        reply = "Copy. I've got the suspect covered.";
                    }
                    else if (HasAny(lower, "follow me", "come with me", "stay with me"))
                    {
                        action = "FOLLOW";
                        reply = "Copy. I'm with you.";
                    }
                    else if (HasAny(lower, "come here", "move closer", "move up", "step up"))
                    {
                        action = "MOVE_CLOSER";
                        reply = "Copy. Moving up.";
                    }
                    else if (HasAny(lower, "hold", "stay there", "stay here", "don't move", "do not move"))
                    {
                        action = "HOLD";
                        reply = "Copy. Holding here.";
                    }
                    else
                    {
                        reply = radioMode
                            ? "Copy. Standing by for your next instruction."
                            : "Copy. What do you need?";
                    }
                    break;

                case NpcRole.Firefighter:
                    if (HasAny(lower, "follow me", "come with me"))
                    {
                        action = "FOLLOW";
                        reply = "Copy. Following you.";
                    }
                    else if (HasAny(lower, "come here", "move closer", "move up"))
                    {
                        action = "MOVE_CLOSER";
                        reply = "Copy. Moving closer.";
                    }
                    else if (HasAny(lower, "hold", "stay there", "stay here", "stand by"))
                    {
                        action = "HOLD";
                        reply = "Copy. Fire is standing by.";
                    }
                    else
                    {
                        reply = "Fire copies. Standing by.";
                    }
                    break;

                case NpcRole.Paramedic:
                    if (HasAny(lower, "follow me", "come with me"))
                    {
                        action = "FOLLOW";
                        reply = "Copy. Following you.";
                    }
                    else if (HasAny(lower, "come here", "move closer", "move up"))
                    {
                        action = "MOVE_CLOSER";
                        reply = "Copy. Moving closer.";
                    }
                    else if (HasAny(lower, "hold", "stay there", "stay here", "stand by"))
                    {
                        action = "HOLD";
                        reply = "Copy. EMS is standing by.";
                    }
                    else
                    {
                        reply = "EMS copies. Standing by.";
                    }
                    break;

                case NpcRole.AirUnit:
                    if (HasAny(lower,
                        "follow the suspect", "track the suspect", "stay on the suspect",
                        "stay on him", "stay on her", "keep eyes on", "keep visual"))
                    {
                        action = "AIR_FOLLOW_SUSPECT";
                        reply = "Air unit copies. We'll stay on the suspect.";
                    }
                    else
                    {
                        reply = "Air unit copies. Standing by.";
                    }
                    break;

                default:
                    return GenerateCivilianDecision(speech, lower, context, memory);
            }

            return Make(reply, role, action, speech);
        }

        private static NpcDecision GenerateCivilianDecision(
            string speech,
            string lower,
            string context,
            string memory)
        {
            string action = "STAY";
            string reply;

            string fullName = PersonaValue(context, "FullName");
            string forename = PersonaValue(context, "Forename");
            string surname = PersonaValue(context, "Surname");
            string birthday = PersonaValue(context, "Birthday");
            string gender = PersonaValue(context, "Gender");
            string citations = PersonaValue(context, "Citations");
            string wanted = PersonaValue(context, "Wanted");
            bool trafficStop = Contains(context, "Scene: traffic stop");

            if (HasAny(lower,
                "what's your name", "what is your name", "your name", "identify yourself",
                "who are you"))
            {
                string name = !string.IsNullOrWhiteSpace(fullName)
                    ? fullName
                    : JoinName(forename, surname);

                reply = !string.IsNullOrWhiteSpace(name)
                    ? "My name is " + name + ", officer."
                    : "I don't have a name available in my record here, officer.";
            }
            else if (HasAny(lower,
                "date of birth", "birthday", "when were you born", "dob"))
            {
                reply = !string.IsNullOrWhiteSpace(birthday)
                    ? "My date of birth is " + birthday + ", officer."
                    : "I don't have that information available, officer.";
            }
            else if (HasAny(lower,
                "are you wanted", "any warrants", "have a warrant", "got a warrant",
                "warrant for you", "warrants"))
            {
                bool parsed;
                bool isWanted;
                parsed = TryParseBoolish(wanted, out isWanted);

                reply = parsed
                    ? (isWanted
                        ? "Yes, officer. My record shows that I'm wanted."
                        : "No, officer. My record does not show me as wanted.")
                    : "I don't have verified warrant information to give you, officer.";
            }
            else if (HasAny(lower,
                "any tickets", "any citations", "how many tickets", "how many citations",
                "citation history", "ticket history"))
            {
                reply = !string.IsNullOrWhiteSpace(citations)
                    ? "My record shows citations: " + citations + "."
                    : "I don't have verified citation information to give you, officer.";
            }
            else if (HasAny(lower, "gender", "male or female"))
            {
                reply = !string.IsNullOrWhiteSpace(gender)
                    ? "My record lists my gender as " + gender + "."
                    : "I don't have that information available, officer.";
            }
            else if (trafficStop && HasAny(lower,
                "know why i stopped you", "know why you're being stopped",
                "know why you are being stopped", "why did i stop you",
                "why i stopped you"))
            {
                reply = "No, officer. I don't know why you stopped me.";
            }
            else if (trafficStop && HasAny(lower,
                "how fast", "know your speed", "speed were you doing",
                "speed you were doing"))
            {
                reply = "I'm not sure how fast I was going, officer.";
            }
            else if (HasAny(lower,
                "hands up", "show me your hands", "put your hands up", "raise your hands"))
            {
                action = "HANDSUP";
                reply = "Okay, officer. My hands are up.";
            }
            else if (HasAny(lower,
                "get out of the car", "get out of the vehicle", "step out",
                "exit the vehicle", "exit the car"))
            {
                action = "EXIT_VEHICLE";
                reply = "Okay, officer. I'm getting out.";
            }
            else if (HasAny(lower, "follow me", "come with me"))
            {
                action = "FOLLOW";
                reply = "Okay, officer. I'll follow you.";
            }
            else if (HasAny(lower, "come here", "move closer", "step over here"))
            {
                action = "MOVE_CLOSER";
                reply = "Okay, officer. I'm coming over.";
            }
            else if (HasAny(lower,
                "don't move", "do not move", "stay there", "stay here",
                "hold still", "stop moving", "hold position", "stop right there"))
            {
                action = "HOLD";
                reply = "Okay, officer. I'll stay right here.";
            }
            else if (HasAny(lower, "run away", "take off", "flee", "get out of here"))
            {
                action = "HOLD";
                reply = "No, officer. I'll stay here and cooperate.";
            }
            else if (HasAny(lower,
                "have you been drinking", "been drinking", "any alcohol", "drugs",
                "any weapons", "have a weapon", "have any weapons"))
            {
                reply = "I don't have verified information for that, officer, so I won't make something up.";
            }
            else if (HasAny(lower, "hello", "hi", "how are you"))
            {
                reply = "Hello, officer.";
            }
            else
            {
                string lastOfficerFact = LastOfficerStatement(memory);
                reply = !string.IsNullOrWhiteSpace(lastOfficerFact)
                    ? "Okay, officer. I understand."
                    : "Okay, officer. What do you need to know?";
            }

            return Make(reply, NpcRole.Civilian, action, speech);
        }

        private static NpcDecision Make(string reply, NpcRole role, string action, string speech)
        {
            return new NpcDecision
            {
                Reply = reply,
                Action = ActionExecutor.NormalizeForRole(role, action),
                Tone = "calm",
                MemoryNote = "Local case-aware response: " + (speech ?? "")
            };
        }

        private static string PersonaValue(string context, string key)
        {
            if (string.IsNullOrWhiteSpace(context) || string.IsNullOrWhiteSpace(key))
                return "";

            const string prefix = "LSPDFR persona: ";
            int lineStart = context.IndexOf(prefix, StringComparison.OrdinalIgnoreCase);
            if (lineStart < 0)
                return "";

            lineStart += prefix.Length;
            int lineEnd = context.IndexOf('\n', lineStart);
            string line = lineEnd >= 0
                ? context.Substring(lineStart, lineEnd - lineStart)
                : context.Substring(lineStart);

            string marker = key + "=";
            int valueStart = line.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
            if (valueStart < 0)
                return "";

            valueStart += marker.Length;
            int valueEnd = line.IndexOf(", ", valueStart, StringComparison.Ordinal);
            string value = valueEnd >= 0
                ? line.Substring(valueStart, valueEnd - valueStart)
                : line.Substring(valueStart);

            return value.Trim();
        }

        private static string JoinName(string first, string last)
        {
            first = (first ?? "").Trim();
            last = (last ?? "").Trim();

            if (first.Length == 0)
                return last;
            if (last.Length == 0)
                return first;

            return first + " " + last;
        }

        private static bool TryParseBoolish(string value, out bool result)
        {
            result = false;
            if (string.IsNullOrWhiteSpace(value))
                return false;

            string lower = value.Trim().ToLowerInvariant();
            if (lower == "true" || lower == "yes" || lower == "1" || lower == "wanted")
            {
                result = true;
                return true;
            }

            if (lower == "false" || lower == "no" || lower == "0" || lower == "not wanted")
            {
                result = false;
                return true;
            }

            return false;
        }

        private static string LastOfficerStatement(string memory)
        {
            if (string.IsNullOrWhiteSpace(memory) || memory == "(none)")
                return "";

            int index = memory.LastIndexOf("Player: ", StringComparison.OrdinalIgnoreCase);
            if (index < 0)
                return "";

            index += "Player: ".Length;
            int end = memory.IndexOf(" | NPC:", index, StringComparison.OrdinalIgnoreCase);
            if (end < 0)
                end = memory.Length;

            return memory.Substring(index, end - index).Trim();
        }

        private static bool Contains(string text, string value)
        {
            return !string.IsNullOrWhiteSpace(text) &&
                   !string.IsNullOrWhiteSpace(value) &&
                   text.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static bool HasAny(string text, params string[] phrases)
        {
            if (string.IsNullOrWhiteSpace(text) || phrases == null)
                return false;

            foreach (string phrase in phrases)
            {
                if (!string.IsNullOrWhiteSpace(phrase) &&
                    text.IndexOf(phrase, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
