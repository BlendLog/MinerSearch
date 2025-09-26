# Miner Search

[Русский](README.ru.md) | English | [Chinese](README.cn.md)

This program is designed to find and destroy hidden miners.
It is an auxiliary tool for searching suspicious files, directories, processes, etc. and is NOT an antivirus.

## News about updates now in the Telegram!
https://t.me/MinerSearch_blog
## ⬇ ![Download latest version](https://github.com/BlendLog/MinerSearch/releases/latest)
### NET Framework 4.7.2 is required

> [!CAUTION]
> ### Windows 7 is outdated. MinerSearch support and testing for this OS will be discontinued coming soon.

![GitHub Downloads (all assets, latest release)](https://img.shields.io/github/downloads/BlendLog/MinerSearch/latest/total?logoColor=AA00F0&color=Navy)

Version v1.4.8.1

- New miners detection
- Fixed a localization string in the Note column.
- Fixed an error when creating the log file for the first time.
- Checking for a new application version is now faster.
- Fixed a false positive in RobloxPlayer.
- Fixed a bug when deleting a malicious task due to returning a target object.
- Fixed an error when parsing the protected insecure parameter AppInit_DLLs.
- Minor translation and typo fixes.

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


