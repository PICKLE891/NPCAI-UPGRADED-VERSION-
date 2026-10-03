# NPC AI Talk v1.4.0 — ACTION JACKSON

**Status: Public Beta**

NPC AI Talk v1.4.0 has been compiled successfully with GitHub Actions and is available for download. Real in-game GTA V/LSPDFR testing is still recommended before treating this build as fully stable.

## No API Key Required

NPC AI Talk now works without an OpenAI API key.

Without a key, it uses:

- Local command/response logic
- Windows Speech Recognition for officer voice input
- Windows TTS for NPC/responder voice output
- Built-in civilian/suspect commands
- Built-in LEO, Fire, EMS, and air-unit responses

Adding an OpenAI API key is **optional** and enables the advanced OpenAI conversation, transcription, multilingual reply, and voice path.

## Features

- AI/local conversations with civilians and suspects
- LEO and backup officer conversations
- Fire and EMS interaction
- Air-unit / helicopter interaction
- Exact NPC target locking
- Soft-spoken voice support
- Optional OpenAI multilingual speech transcription and replies
- Autonomous backup behavior
- Backup officers can assess a scene, take useful positions, and ask the primary officer what to handle next
- Safer Code 3 pursuit-driving logic
- Per-NPC conversation memory
- Windows TTS with optional OpenAI voice output
- GitHub Releases auto-updater

## Requirements

- GTA V
- RAGE Plugin Hook
- LSPDFR
- Windows with .NET Framework 4.8
- **OpenAI API key is optional**

## Installation

1. Download `NPC_AI_TALK_ACTION_JACKSON_v1.4.0.zip` from the Assets section below.
2. Extract the ZIP.
3. Copy `NpcAiTalk.dll` and `NpcAiTalk.ini` into your GTA V `Plugins\LSPDFR` folder.
4. Load LSPDFR and use the plugin in local mode with no API key.
5. Optional: to enable advanced OpenAI features, set your API key in Windows:

```bat
setx OPENAI_API_KEY "YOUR_OPENAI_API_KEY"
```

6. Restart RAGE Plugin Hook / GTA V after setting or changing the key.

**Keep your OpenAI API key private. Never post it in Discord, GitHub Issues, screenshots, logs, or the INI file.**

## Controls

- **L** — Lock/unlock the exact NPC or responder you are targeting
- **N** — Talk to civilian/suspect
- **J** — Radio-talk to LEO / Fire / EMS / Air
- **F9** — Manual update check
- **F10** — Reload configuration

## Automatic Updates

The plugin is configured to check this GitHub repository for updates.

Updater asset name:

`NpcAiTalk_Update.zip`

That update package is included in the Assets section below.

## Support / Bug Reports

If something does not work, include:

- NPC AI Talk version
- RAGE Plugin Hook version
- LSPDFR version
- Whether you are using local mode or an OpenAI API key
- What you were doing when the issue happened
- What you expected to happen
- Relevant `RAGEPluginHook.log` or error logs

Do **not** include your OpenAI API key.

## Discord

**Join here:** https://discord.gg/qpP2EXsNgD

## Creator

**ACTION JACKSON**

GitHub repository owner: `PICKLE891`
