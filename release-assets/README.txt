NPC AI Talk v1.4.0 — ACTION JACKSON
====================================

STATUS
Public Beta. This build compiled successfully with GitHub Actions. Real GTA V/LSPDFR in-game testing is still recommended before treating it as fully stable.

HANDS-FREE TRAFFIC STOPS
- No N key press is required during an active traffic stop or stopped-ped contact.
- Walk up near the current stop target and speak normally.
- The plugin automatically listens for officer speech.
- Manual N remains available as a fallback.

TRUTHFUL / CASE-AWARE NPC RESPONSES
- Local mode uses available LSPDFR scene/persona facts such as name, date of birth, citations and wanted status.
- NPCs do not invent unknown stop reasons, evidence, weapons, impairment, warrants or admissions.
- If a fact is not available from the game, the NPC says it does not know.
- During traffic stops/stopped-ped contacts the plugin keeps the NPC cooperative and does not trigger fleeing or resistance.

NO API KEY REQUIRED
NPC AI Talk works without an OpenAI API key using local case-aware logic, Windows Speech Recognition and Windows TTS.
An OpenAI API key is OPTIONAL and enables advanced OpenAI conversation, transcription, multilingual reply and voice features.

REQUIREMENTS
- GTA V
- RAGE Plugin Hook
- LSPDFR
- Windows with .NET Framework 4.8
- OpenAI API key: OPTIONAL

INSTALLATION
1. Download NPC_AI_TALK_ACTION_JACKSON_v1.4.0.zip from the GitHub release.
2. Extract the ZIP.
3. Copy NpcAiTalk.dll and NpcAiTalk.ini into your GTA V\Plugins\LSPDFR folder and replace the older copies.
4. Load LSPDFR.
5. Start a traffic stop, approach the target and speak normally.
6. Optional: to enable advanced OpenAI features, set your API key in Windows:
   setx OPENAI_API_KEY "YOUR_OPENAI_API_KEY"
7. Restart RAGE Plugin Hook / GTA V after setting or changing the key.

CONTROLS
Hands-free = Speak normally during an active traffic stop/stopped-ped contact
L   = Lock/unlock exact NPC or responder
N   = Manual talk fallback for civilian/suspect
J   = Radio-talk to LEO / Fire / EMS / Air
F9  = Manual update check
F10 = Reload configuration

FEATURES
- Hands-free traffic-stop conversations
- Case-aware no-key NPC responses
- Optional OpenAI conversations with civilians and suspects
- LEO / backup officer conversations
- Fire / EMS interaction
- Air-unit / helicopter interaction
- Exact NPC target locking
- Soft-spoken voice support
- Optional multilingual OpenAI transcription and replies
- Autonomous backup behavior
- Safer Code 3 pursuit-driving logic
- Per-NPC conversation memory
- Windows TTS with optional OpenAI voice output
- GitHub Releases auto-updater

COPYRIGHT AND LICENSE
Copyright © 2026 ACTION JACKSON. All Rights Reserved.
NPC AI Talk is proprietary software. Personal, non-commercial gameplay use is permitted.
Redistribution, resale, re-uploading, repackaging, publishing modified builds, or claiming ownership is not permitted without prior written permission from ACTION JACKSON.
See LICENSE included with the download for full terms. Third-party components and dependencies remain subject to their own licenses and rights.

AUTO UPDATES
Updater asset: NpcAiTalk_Update.zip

DISCORD
Join here: https://discord.gg/qpP2EXsNgD

IMPORTANT
Keep your OpenAI API key private if you choose to use one. Do not post it in Discord, GitHub Issues, screenshots, logs, or the INI file.

CREATOR
ACTION JACKSON
GitHub repository owner: PICKLE891
