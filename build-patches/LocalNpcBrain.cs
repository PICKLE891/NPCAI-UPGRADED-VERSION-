using System;

namespace NpcAiTalk
{
    internal static class LocalNpcBrain
    {
        public static NpcDecision Generate(
            string playerSpeech,
            NpcRole role,
            bool radioMode)
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
                    if (HasAny(lower,
                        "hands up", "show me your hands", "put your hands up",
                        "raise your hands"))
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
                        "hold still", "stop moving", "hold position"))
                    {
                        action = "HOLD";
                        reply = "Okay, officer. I'll stay right here.";
                    }
                    else if (HasAny(lower, "get out of here", "go away", "run away", "leave now"))
                    {
                        action = "FLEE";
                        reply = "Okay, I'm leaving.";
                    }
                    else
                    {
                        reply = "Okay, officer.";
                    }
                    break;
            }

            return new NpcDecision
            {
                Reply = reply,
                Action = ActionExecutor.NormalizeForRole(role, action),
                Tone = "natural",
                MemoryNote = "Local no-API-key mode handled: " + speech
            };
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
