# Miner Search

[Русский](README.ru.md) | English | [Chinese](README.cn.md)

This program is designed to find and destroy hidden miners.
It is an auxiliary tool for searching suspicious files, directories, processes, etc. and is NOT an antivirus.

> [!CAUTION]
> ### Antivirus may mistakenly treat this application as malware. Please do not create issues about this.

## Update news is now on Telegram!
## https://t.me/MinerSearch_blog
## ⬇ ![Download latest version](https://github.com/BlendLog/MinerSearch/releases/latest)
### NET Framework 4.7.2 is required

![GitHub Downloads (all assets, latest release)](https://img.shields.io/github/downloads/BlendLog/MinerSearch/latest/total?logoColor=AA00F0&color=Navy)

> [!CAUTION]
> ### Windows 7 is outdated. MinerSearch development for this OS has been discontinued.

Version v1.4.9.0

- New architecture: Scanner -> Analyzer -> Handler
- Updated codebase with full revision
- Added removal of new miners
- Fixed erroneous deletion of all WMI subscriptions
- Fixed erroneous threat type detection
- Added "Class" column to the report form
- Added option --no-scan-wmi (-nwmi)
- Added option --no-scan-users (-nsu)
- Added option --no-scan-registry (-nsr)
- Redesigned signature analyzer
--------------------------------------------

## How to use

Completely unzip the archive with the program into a separate folder and launch the application. Wait for the scan to complete. When using the program for the first time, you are offered to report the scan results to the author at your discretion. After completion, a form will be shown with a brief report on the threats that have been eliminated. You can view the detailed report by clicking the "Open Report" button. Clicking the "Quarantine" button will open the Quarantine Manager, in which you can completely delete a file or restore it.

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

-h     --help                 Show this help message
-a     --accept-eula          Accept the End-User License Agreement (EULA)
-nl    --no-logs              Do not write logs to a file
-nstm  --no-scantime          Scan only processes
-nwmi  --no-scan-wmi          Do not check WMI integrity and/or event subscriptions
-nr    --no-runtime           Do not scan processes (only directories, files, registry keys, etc.)
-nse   --no-services           Skip scanning services
-nst   --no-scan-tasks        Skip scanning scheduler tasks
-nsu   --no-scan-users        Skip scanning user profiles
-nss   --no-signature-scan    Skip signature scanning of files
-nsr   --no-scan-registry     Skip scanning system registry
-nrc   --no-rootkit-check     Do not check for rootkit presence
-nch   --no-check-hosts       Skip checking the hosts file
-nfw   --no-firewall          Skip scanning firewall rules
-cm    --console-mode         Activate console mode without dialog boxes
-p     --pause                Pause before cleanup
-ret   --remove-empty-tasks   Remove task from Task Scheduler if its application file does not exist
-so    --scan-only            Display malicious or suspicious objects, but do not perform treatment
-fs    --full-scan            Add all other local drives for signature scanning
-f     --force                Used to suppress confirmation prompts for potentially dangerous functions
-s     --select               Scan only the selected directory, including subdirectories
-s=    --select= <path>       Same as --select (-s). Where <path> specifies the directory path to scan
-si    --silent               Enables silent (background) mode without dialog boxes. The application switches to background mode, messages are not displayed, but are still written to the log. Incompatible with --select or --winpemode parameters.
-d=    --depth=<num>          Where <num> is the maximum search depth level. Example usage: -d=5 (default is 8)
-v     --verbose              Outputs detailed information about processes to the console, and also
                              disables the filter for lines with files not recognized as malicious. May increase log file size.
-w     --winpemode            Starts scanning in WinPE mode
                              (without scanning processes, registry, firewall rules, services, scheduler tasks)
-q     --open-quarantine      Open the quarantine manager
-res   --restore= <list>      Restore files from quarantine in console mode (e.g., 1,2,3). Enter -q -cm to view the list.
-del   --delete= <list>       Delete files from quarantine in console mode (e.g., 1,2,3). Enter -q -cm to view the list.


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
