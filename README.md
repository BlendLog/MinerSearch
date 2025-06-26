# Miner Search

[Русский](README.ru.md) | English | [Chinese](README.cn.md)

This program is designed to find and destroy hidden miners.
It is an auxiliary tool for searching suspicious files, directories, processes, etc. and is NOT an antivirus.

## News about updates now in the Telegram!
https://t.me/MinerSearch_blog
## ⬇ ![Download latest version](https://github.com/BlendLog/MinerSearch/releases/latest)
### NET Framework 4.7.1 is required

![GitHub Downloads (all assets, latest release)](https://img.shields.io/github/downloads/BlendLog/MinerSearch/latest/total?logoColor=AA00F0&color=Navy)

Version 1.4.8.0

- Optimized heuristic analysis algorithms
- Improved detection and removal of new miner malware
- The `--verbose` option now includes all warnings for unsigned files
- Fixed a bug with the threat counter when using the `--scan-only` option
- Added scanning and validation of shortcuts in autostart directories
- Common autostart registry keys now display detailed file information
- Fixed an issue with deleting malicious files when they are in use/being accessed
- Added the ability to batch delete and restore files from quarantine
- Refactored the task scheduler handler. Added task verification in Safe Mode
- Extended the `--select (-s)` option. You can now specify `--select= <path>` (or `-s= <path>`) to explicitly provide a path. P.S.: A space after the equals sign is required
- Added `--console-mode (-cm)` option - launches the application in console mode (without dialog boxes)
- Added `--no-firewall (-nfw)` option - skips firewall rule scanning
- Added `--accept-eula (-a)` option - accepts the End User License Agreement (EULA)
- Added `--force (-f)` option - confirms potentially dangerous operations
- Added `--restore= <list>` (-res=) option - restores files from quarantine in console mode (e.g., 1,2,3). Enter `-q -cm` to view the list
- Added `--delete= <list>` (-del=) option - deletes files from quarantine in console mode (e.g., 1,2,3). Enter `-q -cm` to view the list
- Added a "Note" column to the summary report, detailing why a threat could not be remediated if an error occurred
- Objects blocked by Windows Defender are now displayed in the summary report
- Added a warning specifying which components failed to load if the application fails to start
- The design of the form for handling suspicious hosts file entries has been unified with the overall application style
- Corrected some technical phrasings and translation errors


Version 1.4.7.92 \[patch\]

- Improvement of process handling rules
- Detection and removal of new miners
- Fixed incorrect classification of the copilot address in the hosts file
- Fixed a short-term hang when generating reports on the threat summary form
- More information about scheduler tasks with the --verbose option
- Other minor fixes

Version v1.4.7.91

- Fixed application crashes when reading files and directories.
- Fixed a visual bug with an incorrect number of threats found
- Improved algorithm for detecting suspicious files
- Improved stability processing of third-party network services
- Added language switching functionality. Create a language.cfg (the content should be EN or RU)
- Removed restrictions of Windows Defender are no longer duplicated in the log
- Added the --no-check-hosts argument - skips checking the hosts file
- Added an identifier key (Devide ID) to the short report form for quick report processing (more details here)
- Implemented a mechanism for unblocking the task manager and registry editor
- The normal color of the Collapse button

-----------------------------------------

## How to use

Completely unzip the archive with the program into a separate folder and launch the application. Wait for the scan to complete. When using the program for the first time, you are offered to report the results of the scan to the author at your wish. After completion, a form will be shown with a brief report on the threats that have been eliminated. You can view the detailed log by clicking on the "Show log" button. Clicking on the "Quarantine" button will open the Quarantine Manager, in which you can completely delete the file or restore it.

----------------
How to switch language in the app?

1) Create language.cfg file if not exist
2) Open it with any text editor
3) Choose your preferred language: EN or RU

----------------

The application also supports additional launch parameters (listed below). To use them, you should:
1) Run the command line (cmd) as administrator
2) Hold down shift and right-click on the application - select "Copy as path"
3) Paste the path into the command line and add the necessary parameters* after a space

Additional command line args (usually is not required):

Конечно, вот эти опции командной строки, оформленные в виде Markdown-таблицы для GitHub:

Generated markdown
| Short Option | Long Option         | Description                                                                                                                                                                                                                                                                                                                         |
| :----------- | :------------------ | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `-h`         | `--help`            | Display this help message                                                                                                                                                                                                                                                                                                           |
| `-a`         | `--accept-eula`     | Accept the End-User License Agreement (EULA)                                                                                                                                                                                                                                                                                        |
| `-nl`        | `--no-logs`         | Do not write logs to a file                                                                                                                                                                                                                                                                                                         |
| `-nstm`      | `--no-scantime`     | Scan only running processes                                                                                                                                                                                                                                                                                                         |
| `-nr`        | `--no-runtime`      | Do not scan running processes (only directories, files, registry keys, etc.)                                                                                                                                                                                                                                                        |
| `-nse`       | `--no-services`     | Skip scanning services                                                                                                                                                                                                                                                                                                              |
| `-nss`       | `--no-signature-scan` | Skip file signature scanning                                                                                                                                                                                                                                                                                                        |
| `-nrc`       | `--no-rootkit-check` | Disable the rootkit check                                                                                                                                                                                                                                                                                                           |
| `-nch`       | `--no-check-hosts` | Skip checking the hosts file                                                                                                                                                                                                                                                                                                        |
| `-nfw`       | `--no-firewall`     | Skip scanning firewall rules                                                                                                                                                                                                                                                                                                        |
| `-cm`        | `--console-mode`    | Enable console mode (no dialog boxes)                                                                                                                                                                                                                                                                                               |
| `-p`         | `--pause`           | Pause before cleanup                                                                                                                                                                                                                                                                                                                |
| `-ret`       | `--remove-empty-tasks` | Remove a task from Task Scheduler if its executable file does not exist                                                                                                                                                                                                                                                           |
| `-so`        | `--scan-only`       | Report malicious or suspicious objects but do not remove or quarantine them                                                                                                                                                                                                                                                         |
| `-fs`        | `--full-scan`       | Include all local drives in the signature scan                                                                                                                                                                                                                                                                                      |
| `-ras`       | `--run-as-system`   | Run the scan as the SYSTEM account (for advanced users only)                                                                                                                                                                                                                                                                        |
| `-f`         | `--force`           | Suppress confirmation prompts for potentially dangerous actions                                                                                                                                                                                                                                                                     |
| `-s`         | `--select`          | Scan only the selected directory and its subdirectories (recursively)                                                                                                                                                                                                                                                               |
| `-s=`        | `--select= <path>`  | Same as `--select (-s)`. Specifies the directory path to scan.                                                                                                                                                                                                                                                                      |
| `-si`        | `--silent`          | Enable silent mode. The application runs in the background without dialog boxes. Messages are suppressed but still written to the log file. Incompatible with `--select` and `--winpemode`.                                                                                                                                             |
| `-d=`        | `--depth=<num>`     | Sets the maximum scan depth. Example: `-d=5` (default is 8).                                                                                                                                                                                                                                                                        |
| `-v`         | `--verbose`         | Enable verbose output. Displays detailed process information and includes non-malicious files in the output. This may significantly increase the log file size.                                                                                                                                                                   |
| `-w`         | `--winpemode`       | Run the scan in WinPE mode (skips scanning processes, registry, firewall rules, services, and scheduled tasks).                                                                                                                                                                                                                   |
| `-q`         | `--open-quarantine` | Open the Quarantine Manager                                                                                                                                                                                                                                                                                                         |
| `-res`       | `--restore= <list>` | Restore files from quarantine in console mode (e.g., `1,2,3`). Use `-q -cm` to see the list of quarantined items.                                                                                                                                                                                                                    |
| `-del`       | `--delete= <list>` | Delete files from quarantine in console mode (e.g., `1,2,3`). Use `-q -cm` to see the list of quarantined items.                                                                                                                                                                                                                      |


* Not necessarily in strict order
--------------------------------------------------------------

Symbols in logs

| Hint | Description |
|-----------|----------|
|    [!] | Minor warning |
| [!!] | A warning worth paying attention to |
|  [!!!] | Threat detected |
| [!!!!] | A rootkit has been detected |
| [Reg] | Scan the registry key(s) |
|    [+] | Successful completion of the action (treatment, removal, etc.) |
|    [x] | Error |
| [xxx] | Critical error: for example, when running in the sandbox |
|    [#] | Status |
| [.] | Description |
|    [_] | Unblocking the directory and deleting if empty |
| [i] | Info |
|    [$] | Scan elapsed time |

----------------------------

## Screenshots
Stop and remove malicious processes and his support components, that makes deletion malware harder
![first](https://github.com/user-attachments/assets/29828484-6d57-4e71-ad5c-641913ce34f7)

Final report form
![second](https://github.com/user-attachments/assets/309e7625-bc57-4b80-9052-4805c33f9486)


