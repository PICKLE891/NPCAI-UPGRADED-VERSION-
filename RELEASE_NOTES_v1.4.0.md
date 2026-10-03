# NPC AI Talk v1.4.0 — ACTION JACKSON

**Status: Public Beta**

NPC AI Talk v1.4.0 has been compiled successfully with GitHub Actions. Real in-game GTA V/LSPDFR testing is still recommended before treating this build as fully stable.

## Hands-Free Traffic-Stop Hotfix

- No **N** key press is required during an active traffic stop or stopped-ped contact.
- When you are close to the current stop target, the plugin automatically listens for normal officer speech.
- Local no-key NPC replies use available LSPDFR scene/persona facts such as name, date of birth, citations and wanted status.
- NPCs do not invent unknown stop facts, evidence, weapons, impairment, warrants or admissions. If the game does not provide a fact, the NPC says it does not know.
- Traffic-stop/stopped-ped conversations are cooperative. This plugin will not tell the NPC to flee or resist during those contacts.
- Manual **N** talk remains available as a fallback.

## No API Key Required

NPC AI Talk works without an OpenAI API key using local case-aware responses, Windows Speech Recognition and Windows TTS.

Adding an OpenAI API key is **optional** and enables the advanced OpenAI conversation, transcription, multilingual reply and voice path.

## Features

- Hands-free traffic-stop conversations
- Case-aware civilian/suspect replies
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
3. Copy `NpcAiTalk.dll` and `NpcAiTalk.ini` into your GTA V `Plugins\LSPDFR` folder, replacing the older copies.
4. Load LSPDFR.
5. Start a traffic stop, approach the target, and speak normally. No talk key is required.
6. Optional: to enable advanced OpenAI features, set your API key in Windows:

```bat
setx OPENAI_API_KEY "YOUR_OPENAI_API_KEY"
```

7. Restart RAGE Plugin Hook / GTA V after setting or changing the key.

**Keep your OpenAI API key private. Never post it in Discord, GitHub Issues, screenshots, logs, or the INI file.**

## Controls

- **Hands-free** — speak normally at an active traffic stop/stopped-ped contact
- **L** — Lock/unlock the exact NPC or responder you are targeting
- **N** — Manual talk fallback for civilian/suspect
- **J** — Radio-talk to LEO / Fire / EMS / Air
- **F9** — Manual update check
- **F10** — Reload configuration

## Automatic Updates

Updater asset: `NpcAiTalk_Update.zip`

## Copyright and License

**Copyright © 2026 ACTION JACKSON. All Rights Reserved.**

NPC AI Talk is proprietary software. Personal, non-commercial gameplay use is permitted. Redistribution, resale, re-uploading, repackaging, publishing modified builds, or claiming ownership is not permitted without prior written permission from ACTION JACKSON.

The full proprietary license is included with the download. Third-party components and dependencies remain subject to their own licenses and rights.

## Support / Bug Reports

If something does not work, include:

- NPC AI Talk version
- RAGE Plugin Hook version
- LSPDFR version
- Whether hands-free listening appeared during the stop
- Whether you are using local mode or an OpenAI API key
- What you said to the NPC
- What response you received
- Relevant `RAGEPluginHook.log` or error logs

Do **not** include your OpenAI API key.

## Discord

**Join here:** https://discord.gg/qpP2EXsNgD

## Creator

**ACTION JACKSON**

GitHub repository owner: `PICKLE891`
