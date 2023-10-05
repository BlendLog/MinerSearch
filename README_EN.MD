# Miner Search

A program designed to find and destroy silent crypto miners. 
It is an auxiliary tool for searching suspicious files, 
directories, processes, etc. and is NOT an antivirus. 

NET Framework 4.5.2 is required to run.

### WARNING! Some antiviruses mark this project as trojan. It's false positive detection, just add executable file to the AV white list

Version v1.4.5

- Output of task launch parameters from the scheduler
- A third-party Task Scheduler library is no longer required. 
- Added detection and removal of the miner's rootkit
- Fixed a bug when the scanned process has already completed its work
- Fixed a bug when the requested access to the registry is denied
- The log file is written to the root of the scanned disk
- The required version of the NET Framework has been downgraded to 4.5.2
- Correction of minor flaws
- Replenishment of the database of malicious entries in the hosts file

Version v1.4.4
- Fixed a bug with the inability to delete found files before closing the program
- Improved miner search algorithm for static analysis
- Reduced false positive detection of Antivirus solutions. Some antiviruses can still delete program as a Trojan, just add it to AV exclusions. 
- Added the --depth parameter. Sets the maximum value of the search depth during signature scanning. Usage example: --depth=3 (default is 8).
- Added signature verification of files from the task scheduler
- Added Mineresearch launch counter
- Added the --winpemode parameter. In this mode, the drive letter is requested for scanning. It is assumed that this is a disk with an infected system. Scanning of processes, registry, scheduler tasks, directories in the user profile is not performed. Next, you need to boot into the infected system again and perform a second scan.

Version v1.4.3

- Added signature detection of suspicious files
- Added brief information about the system to the log
- Removal of malicious entries in the hosts file, not the file itself. Confirmation is not required
- Fixed a bug where it is not possible to receive command line arguments due to the shutdown of the WMI service.
- Added quarantine. In addition to the files detected during static analysis, a txt file is also created with the previous paths of these files
- Fix a bug when incorrectly defining the injection into the Dwm process on Win 8 (not 8.1)
- Deleting the John user if it is not the current user
- Added creation of protected hidden files from re-infection
- Handling TCP connection exclusion for processes
- Fixed a bug with the error "Access denied" for some files
- Fixed a bug with the inability to unlock the directory when the Antivirus is installed
- Added automatic deletion of invalid tasks from the scheduler based on the result of the last run. For the best result, you should restart your PC after scanning. The --remove-empty-tasks parameter also works.
- Added the --no-signature-scan startup parameter to skip scanning by signatures

Version v1.4.2

- Added OS boot definition
- Added removal of malicious paths from Windows Defender exclusions
- Added removal of malicious rules from Windows Firewall
- Deleting directories and restoring rights to them is performed separately
- The hosts file is now scanned at the end
- Fixed a bug with missing duplicate tasks in the task scheduler

Version v1.4

- Redesigned algorithm for checking and removing malicious directories/files
- Added task scheduler check
- Rework the algorithm for parsing the application path in autorun from the registry
- Added a check whether the process is really suspended
- Added the function of renaming malicious process files
- Added a call to help --help
- Redesigned digital signature verification
- The text in the log is no longer duplicated
- Fixed a bug when the input string had an incorrect format
- Added parent process verification

Version 1.3

- Added digital signature verification
- More additional registry scanning keys (autorun key, appinit_dlls)
- Changed the logging method. Scan statistics are recorded in parallel to a file in the format MinerSearch_yyyy_mm_dd_hh_ss.log
- Fixed some minor bugs

# Demo

Detects, suspends and delete malicious processes, as well as auxiliary components of the trojan-miner, which make it difficult to remove it.

![image](https://user-images.githubusercontent.com/56220293/215475650-25d31515-d52a-485b-b194-7db63e0e9962.png)

![image2](https://user-images.githubusercontent.com/56220293/215356942-8080b05a-f324-4006-9864-6843923ff2be.png)
