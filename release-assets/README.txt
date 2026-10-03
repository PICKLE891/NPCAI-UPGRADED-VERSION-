NPC AI Talk v1.4.0 — ACTION JACKSON
====================================

STATUS
Public Beta. This build compiled successfully with GitHub Actions. Real GTA V/LSPDFR in-game testing is still recommended before treating it as fully stable.

NO API KEY REQUIRED
NPC AI Talk works without an OpenAI API key.

Without a key it uses:
- Local command/response logic
- Windows Speech Recognition for officer voice input
- Windows TTS for NPC/responder voice output
- Built-in civilian, suspect, LEO, Fire, EMS and air-unit responses

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
3. Copy NpcAiTalk.dll and NpcAiTalk.ini into your GTA V\Plugins\LSPDFR folder.
4. Load LSPDFR. Local mode works without an API key.
5. Optional: to enable advanced OpenAI features, set your API key in Windows:
   setx OPENAI_API_KEY "YOUR_OPENAI_API_KEY"
6. Restart RAGE Plugin Hook / GTA V after setting or changing the key.

CONTROLS
L   = Lock/unlock exact NPC or responder
N   = Talk to civilian/suspect
J   = Radio-talk to LEO / Fire / EMS / Air
F9  = Manual update check
F10 = Reload configuration

FEATURES
- Local no-key NPC command/response mode
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

AUTO UPDATES
Updater asset: NpcAiTalk_Update.zip

DISCORD
Join here: https://discord.gg/qpP2EXsNgD

IMPORTANT
Keep your OpenAI API key private if you choose to use one. Do not post it in Discord, GitHub Issues, screenshots, logs, or the INI file.

CREATOR
ACTION JACKSON
GitHub repository owner: PICKLE891
