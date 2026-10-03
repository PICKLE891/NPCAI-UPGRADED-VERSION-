# NPC AI Talk — ACTION JACKSON

AI-powered NPC interaction plugin project for **GTA V / LSPDFR / RAGE Plugin Hook**.

## Highlights

- Talk to civilians and suspects
- LEO / backup officer conversations
- Fire / EMS conversations
- Air-unit conversations
- Exact target lock
- Soft-spoken officer support
- Multilingual transcription and same-language NPC replies
- Autonomous backup behavior
- Backup officers assess a scene, take useful positions, then ask the primary officer what to do next
- Safer Code 3 pursuit-driving logic
- Short per-NPC conversation memory
- OpenAI voice output with Windows TTS fallback
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

To publish a release, create/push a version tag such as:

`v1.4.0`

The **Publish NPC AI Talk Release** workflow will:

1. Restore the public RAGE Plugin Hook SDK from NuGet
2. Build `NpcAiTalk.dll`
3. Create the public install ZIP
4. Create `NpcAiTalk_Update.zip`
5. Publish both files to the GitHub Release

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

## OpenAI API key

Players should use their own OpenAI API key. The key is not stored in this repository.

Windows:

```bat
setx OPENAI_API_KEY "YOUR_OPENAI_API_KEY"
```

Restart RAGE Plugin Hook / GTA V after setting it.

## Controls

- **L** — lock/unlock exact NPC or responder
- **N** — talk to civilian/suspect
- **J** — radio-talk to LEO / Fire / EMS / Air
- **F9** — manual update check
- **F10** — reload config

## Discord

**Join here:** https://discord.gg/qpP2EXsNgD

## Creator

**ACTION JACKSON**
