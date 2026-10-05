# Miner Search

[Русский](README.ru.md) | English | [Chinese](README.cn.md)

This program is designed to find and destroy hidden miners.
It is an auxiliary tool for searching suspicious files, directories, processes, etc. and is NOT an antivirus.

> [!CAUTION]
> ### Antivirus may mistakenly treat this application as malware. Please do not create issues about this.

## Update news is now on Telegram!
## https://t.me/MinerSearch_blog
## ⬇ ![Download latest version](https://github.com/BlendLog/MinerSearch/releases/latest)
### NET Framework 4.8 is required

![GitHub Downloads (all assets, latest release)](https://img.shields.io/github/downloads/BlendLog/MinerSearch/latest/total?logoColor=AA00F0&color=Navy)

> [!CAUTION]
> ### Windows 7 is outdated. MinerSearch development for this OS has been discontinued.

Version v1.4.9.5

- Windows version detection mechanism updated
- Improved accuracy of digital signature verification of files
- Fixed display of file paths in the log
- Improved detection of suspicious scheduled tasks
- Expanded heuristics for scheduled tasks
- Fixed the link to the download page when a new version is available
- Eliminated false access errors during file analysis
- Fixed background mode (--silent)
- Eliminated endless attempts to remove unwanted Windows Defender exclusions
- Restored WMI integrity check
- Added handling of read errors for corrupted files
- Optimized language resources (deduplication)
- Expanded the list of detectable malicious services
- Added scanning of COM autostart components
- SHA1 hashes of unsigned files are now recorded at all scanning stages
- Fixed analysis error for files locked by other processes
- Eliminated false positives on a legitimate system process (conhost)

--------------------------------------------

## How to use

Completely unzip the archive with the program into a separate folder and launch the application. Wait for the scan to complete. When using the program for the first time, you are offered to report the scan results to the author at your discretion. After completion, a form will be shown with a brief report on the threats that have been eliminated. You can view the detailed log by clicking the "Show Log" button. Clicking the "Quarantine" button will open the Quarantine Manager, in which you can completely delete a file or restore it.

If unknown threats are found, the "Threat Review" window opens. For each item you can select an action (Cure, Quarantine, Delete, Terminate, Disable, Skip) or set a single action for all items ("Set to all"). The "Set recommended" button restores the actions recommended by the analyzers. If the window is closed without clicking "Apply", the recommended actions are applied after confirmation. Automatic acceptance of decisions for unknown threats can be enabled in "Open settings" or with the -norev parameter.

Clicking on the "Open quarantine" button will open the Quarantine Manager, in which you can completely delete or restore quarantined files, scheduled tasks, services and registry entries.

----------------
How to switch language in the app?

1) Create language.cfg file if it doesn't exist yet
2) Open it with any text editor
3) Set your preferred language: RU or EN

----------------

The application also supports additional launch parameters (listed below). To use them, you should:
1) Run the command line (cmd) as administrator
2) Hold Shift and right-click on the application - select "Copy as path"
3) Paste the path into the command line and add the necessary parameters* after a space

Additional launch parameters (usually not required):

| Short Option | Long Option | Description |
|:---|:---|:---|
| `-h` | `--help` | Show this help message |
| `-a` | `--accept-eula` | Accept the End-User License Agreement (EULA) |
| `-nl` | `--no-logs` | Do not write logs to a file |
| `-nwmi` | `--no-scan-wmi` | Do not check WMI integrity and/or event subscriptions |
| `-nr` | `--no-runtime` | Do not scan processes (only directories, files, registry keys, etc.) |
| `-nse` | `--no-services` | Skip scanning services |
| `-nst` | `--no-scan-tasks` | Skip scanning scheduler tasks |
| `-nsu` | `--no-scan-users` | Skip scanning user profiles |
| `-nss` | `--no-signature-scan` | Skip signature scanning of files |
| `-nsr` | `--no-scan-registry` | Skip scanning system registry |
| `-nrc` | `--no-rootkit-check` | Do not check for rootkit presence |
| `-nch` | `--no-check-hosts` | Skip checking the hosts file |
| `-nfw` | `--no-firewall` | Skip scanning firewall rules |
| `-cm` | `--console-mode` | Activate console mode without dialog boxes |
| `-p` | `--pause` | Pause before cleanup |
| `-ret` | `--remove-empty-tasks` | Remove task from Task Scheduler if its application file does not exist |
| `-so` | `--scan-only` | Display malicious or suspicious objects, but do not perform treatment |
| `-fs` | `--full-scan` | Add all other local drives for signature scanning |
| `-f` | `--force` | Used to suppress confirmation prompts for potentially dangerous functions |
| `-s` | `--select` | Scan only the selected directory, including subdirectories |
| `-s=` | `--select= <path>` | Same as `--select (-s)`. Where `<path>` specifies the directory path to scan |
| `-si` | `--silent` | Enables silent (background) mode without dialog boxes. The application switches to background mode, messages are not displayed, but are still written to the log. Incompatible with `--select` or `--winpemode` parameters. |
| `-d=` | `--depth=<num>` | Where `<num>` is the maximum search depth level. Example usage: `-d=5` (default is 8, maximum is 16) |
| `-v` | `--verbose` | Outputs detailed information about processes to the console, and also disables the filter for lines with files not recognized as malicious. May increase log file size. |
| `-w` | `--winpemode` | Starts scanning in WinPE mode (without scanning processes, registry, firewall rules, services, scheduler tasks) |
| `-q` | `--open-quarantine` | Open the quarantine manager |
| `-res=` | `--restore= <list>` | Restore items (files, tasks, services, registry entries) from quarantine in console mode (e.g., `-res= 1,2,3`). Requires `-f`. Use `-q -cm` to view the list. |
| `-del=` | `--delete= <list>` | Delete items from quarantine in console mode (e.g., `-del= 1,2,3`). Use `-q -cm` to view the list. |
| `-norev` | `--no-review-interact` | Skip the Threat Review window; recommended actions are applied automatically to unknown threats |

* Not necessarily in strict order
----------------------------

Symbols in logs

| Hint | Description |
|-----------|----------|
|    [!] | Minor warning |
|   [!!] | Warning worth paying attention to |
|  [!!!] | Threat detected |
| [!!!!] | Rootkit detected |
|  [Reg] | Scanning registry key(s) |
|    [+] | Successful completion of action (treatment, removal, etc.) |
|    [x] | Error |
|  [xxx] | Critical error: for example, when running in a sandbox |
|    [#] | Status |
|    [.] | Description |
|    [_] | Unblocking directory and deleting if empty |
|    [i] | Info |
|    [$] | Scan elapsed time |

----------------------------

## Screenshots
Stop and remove malicious processes and their support components, which make malware deletion harder
![first](https://github.com/user-attachments/assets/29828484-6d57-4e71-ad5c-641913ce34f7)

General information form
![second](https://github.com/user-attachments/assets/309e7625-bc57-4b80-9052-4805c33f9486)
