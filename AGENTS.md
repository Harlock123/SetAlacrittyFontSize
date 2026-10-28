# AGENTS.md

Notes for anyone, human or AI agent, changing this code. `README.md` says what the tool is for; this file covers
how it works and what to watch when changing it.

## What this is

A tiny Linux command-line tool in C#: `SetAlacrittyFontSize 14` rewrites the font `size:` entry in Alacritty's
YAML config. It targets `net5.0`, which needs the .NET 5 SDK (out of support); to build with a newer SDK, change
`TargetFramework` in `SetAlacrittyFontSize.csproj`.

## Commands

```bash
dotnet build SetAlacrittyFontSize.csproj
dotnet run -- 14                 # sets the font size to 14 (allowed range 6 to 40)
./MakeExecutable.sh              # publishes a trimmed single-file linux-x64 build into Releases/
```

There are no tests yet.

## Layout

- `Program.cs`: the whole program. It checks the argument, reads the config line by line, replaces the first
  `size:` line found after a `font:` line, and writes the file back.
- `SetAlacrittyFontSize.csproj`: the project; there is no solution file.
- `MakeExecutable.sh`: builds, deletes and recreates `Releases/`, then copies the published binary there.
- `Releases/SetAlacrittyFontSize`: the committed linux-x64 binary.
- `.vscode/`: build, publish and watch tasks and a launch profile for VS Code.

## Rules that keep it working

- **The config path.** The file is `SpecialFolder.ApplicationData` + `/alacritty/alacritty.yml`, which on Linux is
  `~/.config/alacritty/alacritty.yml`. Only that YAML file is handled; Alacritty's newer `alacritty.toml` is not.
- **Bad input prints usage.** A missing, non-numeric or out-of-range argument prints the usage text and changes
  nothing. A missing config file throws.
- **Rewrite in place.** Every other line is written back unchanged, with `\n` line endings.
- **Line endings.** `Program.cs` is CRLF with a BOM; keep it that way so diffs show only real changes.

## Making a change

- Test against a copy of a config file before pointing it at a real one, since the tool overwrites the file.
- If you rebuild the release binary with `MakeExecutable.sh`, note that the script expects the `net5.0` output path.
- Update `README.md` if the usage changes.
