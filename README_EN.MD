# Miner Search

A program designed to find and destroy silent crypto miners. 
It is an auxiliary tool for searching suspicious files, 
directories, processes, etc. and is NOT an antivirus. 

Based on Miner Killer.

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
- Improved process scanning process;
- Added static directory scanning;
- Added the function of automatic cleaning from malicious directories;
- Destruction of malicious processes (including those marked as critical)
- Scanning malicious registry keys, etc...

NET Framework 4.7.2 is required to run.

Version 1.3

- Added digital signature verification
- More additional registry scanning keys (autorun key, appinit_dlls)
- Changed the logging method. Scan statistics are recorded in parallel to a file in the format MinerSearch_yyyy_mm_dd_hh_ss.log
- Fixed some minor bugs

# Demo

Detects and suspends malicious processes. Starts deleting blocked folders through the access control list on the security tab. 
If any suspicious or malicious objects are found, confirmation is required to start automatic cleaning.

![image](https://user-images.githubusercontent.com/56220293/215475650-25d31515-d52a-485b-b194-7db63e0e9962.png)

![image2](https://user-images.githubusercontent.com/56220293/215356942-8080b05a-f324-4006-9864-6843923ff2be.png)
