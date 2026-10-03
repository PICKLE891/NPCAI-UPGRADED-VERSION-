# NPC AI Talk — ACTION JACKSON

AI-powered NPC interaction plugin project for **GTA V / LSPDFR / RAGE Plugin Hook**.

## Highlights

- Works **without an OpenAI API key** using local command/response logic, Windows Speech Recognition, and Windows TTS
- Optional OpenAI-powered conversations, transcription, multilingual replies, and voice when a key is configured
- Talk to civilians and suspects
- LEO / backup officer conversations
- Fire / EMS conversations
- Air-unit conversations
- Exact target lock
- Soft-spoken officer support
- Autonomous backup behavior
- Backup officers assess a scene, take useful positions, then ask the primary officer what to do next
- Safer Code 3 pursuit-driving logic
- Short per-NPC conversation memory
- GitHub Releases auto-updater
- **GitHub Actions builds the DLL automatically — no developer PC required for compilation**

## Automatic build

This repository contains `.github/workflows/build.yml`.

Every push to `main`/`master` builds the plugin on a Windows GitHub runner.

Go to:

**Actions → Build NPC AI Talk → latest successful run → Artifacts**

Download:

`NPC-AI-TALK-ACTION-JACKSON`

That artifact contains the compiled plugin ZIP.

## Automatic release

The release workflow builds `NpcAiTalk.dll`, creates the public install ZIP and updater ZIP, and publishes them to the GitHub Release.

The in-game updater is preconfigured for:

- Owner: `PICKLE891`
- Repo: `NPCAI-UPGRADED-VERSION-`
- Update asset: `NpcAiTalk_Update.zip`

## RAGE Plugin Hook

The build downloads the **developer SDK** from NuGet for compilation. The RAGE Plugin Hook DLL is **not included in this repository or releases**.

Players install RAGE Plugin Hook separately.

## LSPDFR integration

This build does **not** require `LSPD First Response.dll` at compile time.

LSPDFR APIs are discovered at runtime using reflection. If LSPDFR is loaded, the plugin can use available traffic-stop, stopped-ped, pursuit and persona context. If an API call is unavailable, the plugin falls back to direct RAGE entity targeting instead of failing to load.

## OpenAI API key — optional

**An OpenAI API key is not required to run NPC AI Talk.**

Without a key, the plugin uses local command/response logic plus Windows Speech Recognition and Windows TTS. This supports common civilian, suspect, LEO, Fire, EMS, and air-unit commands without making OpenAI API calls.

A key is optional and enables the advanced OpenAI conversation/transcription/voice path.

If you choose to use those advanced features, set the key in Windows:

```bat
setx OPENAI_API_KEY "YOUR_OPENAI_API_KEY"
```

Restart RAGE Plugin Hook / GTA V after setting it.

**Keep your API key private. Never put it in the INI, Discord, screenshots, GitHub Issues, or logs.**

## Controls

- **L** — lock/unlock exact NPC or responder
- **N** — talk to civilian/suspect
- **J** — radio-talk to LEO / Fire / EMS / Air
- **F9** — manual update check
- **F10** — reload config

## Discord

**Join here:** https://discord.gg/qpP2EXsNgD

## Copyright and License

**Copyright © 2026 ACTION JACKSON. All Rights Reserved.**

NPC AI Talk is proprietary software. Personal, non-commercial gameplay use is permitted. Redistribution, resale, re-uploading, repackaging, publishing modified builds, or claiming ownership is not permitted without prior written permission from ACTION JACKSON.

See the `LICENSE` file for the full terms. Third-party components and dependencies remain subject to their own licenses and rights.

## Creator

**ACTION JACKSON**
