# Miner Search

Настоящая программа разработанная для поиска и уничтожения скрытых майнеров.
Является вспомогательным инструментом для поиска подозрительных файлов, каталогов, процессов и тд. и НЕ является антивирусом. 

### ВНИМАНИЕ! Некоторые антивирусы помечают этот проект как троян. Это ложное срабатывание, просто добавьте исполняемый файл в исключение антивируса

Версия 1.4.7.1

- Добавлено обнаружение вредоносной версии утилиты печати (print.exe)
- Добавлена проверка Установщика Windows (msiexec.exe)
- добавлена проверка установленного пакета NET Framework 4.5.X
- Исправлено зависание утилиты при сканировании процессов, если систем несколько
- Исправление ошибок локализации
- Корректная проверка powershell
- Нативная проверка имени пользователя
- добавлен параметр --no-scan-tasks для пропуска сканирования задач планировщика
- обновление сигнатур

Версия 1.4.7.0

- Добавлен перевод на русский
- Исправлена обработка путей с неразрывным пробелом
- Добавлено предотвращение внедрения неподписаных Microsoft dll
- Добавлено предотвращение блокировки по хэшу значка в заголовке приложения
- Исправление ошибки 0xc0000005 при проверке процессов в безопасном режиме без поддержки сети
- Добавлена поддержка малых разрешений экрана
- Добавлено запись в лог версии приложения
- Добавлена запись в лог, что ранее найденные процессы были приостановлены, в случае аварийного закрытия программы
- Добавлен параметр --debug для отладки. Используется при запуске приложения через cmd или powershell

Версия 1.4.6.1

- Запись режима загрузки в лог (нормальный или безопасный)
- Обнаружение работы другой копии программы
- Обнаружение внедрения в процесс проводника
- Удаление нежелательных правил блокировки приложений политикой AppLocker
- Добавлен русский язык справки
- Исправлена ошибка, когда окно консоли задано больше, чем разрешается
- Убрана возможность выделения в консоли, предотвращая случайную приостановку работы приложения
- Зараженные файлы будут зашифрованы, перед добавлением в карантин
- Добавлена команда --restore=<путь> для восстановления указанного файла из карантина
  
Версия 1.4.6.0

- Добавлено обнаружение запуска приложения из под архива
- Добавлено корректное определение версии Windows
- Исключена попытка случайно удалить легальный .bat скрипт
- Исключена потеря списка вредоносных процессов из памяти, когда приложение аварийно закрывалось
- Добавлено отключение установленных майнером сервисов
- Добавлена проверка подписи файла из аргументов rundll32.exe и pcalua.exe, а не сами файлы
- Исправлена ошибка "Доступ к этому файлу из системы отсутствует"
- Исправлена недоработка, когда вредоносный файл удален, но выводится сообщение "No threats found"
- Определение sfx архива в автозапуске
- Восстановление службы удаленных рабочих столов и удаление RDP Wrapper
- Опеределение исполняемого файла по сигнатуре, а не по расширению
- Добавлен параметр --no-services для пропуска сканирования служб
- Добавлен парамтре --full-scan для добавления всех дополнительных локальных дисков для сигнатурного сканирования
- Добавлен параметр --scan-only. Отображать подозрительные или вредоносные объекты, но не выполнять лечение.


Версия 1.4.5.2

- Переработан алгоритм получения удаленного порта процесса (без netstat)
- Лог файл записывается в каталог MinerSearch_Logs сканируемого диска, а не напрямую в корень
- Добавление новых сигнатур

Версия v1.4.5.1

- Добавлено окно лицензионного соглашения
- Исправление ошибки неверного закрытия процесса rk_test
- Добавлен отсутствующий параметр --no-rootkit-check. Отключает проверку наличия руткита майнера
- Уменьшение ложных срабатываний сторонних антивирусных решений

Версия v1.4.5

- Вывод параметров запуска задачи из планировщика
- Стороняя библиотека Microsoft.Win32.TaskScheduler больше не требуется. 
- Добавлено обнаружение и удаление руткита майнера
- Исправлен баг, когда сканируемый процесс уже завершил работу
- Исправлен баг, когда запрошенный доступ к реестру запрещен
- Лог файл записывается в корень сканируемого диска
- Требуемая версия NET Framework понижена до 4.5.2
- Исправление мелких недоработок
- Пополнение базы вредоносных записей в файле hosts

Версия v1.4.4

- Исправлен баг с невозможностью удалить найденные файлы до закрытия программы
- Улучшен алгоритм поиска майнера при статическом анализе
- Сокращено ложное срабатывание Антивирусных решений. Некоторые антивирусы всё ещё могут помечать файл как троян, просто добавьте в исключение. 
- Добавлен параметр --depth. Устанавливает максимальное значение глубины поиска при сигнатурном сканировании. Пример использования: --depth=3 (по умолчанию 8). 
- Добавлена сигнатурная проверка файлов из планировщика задач
- Добавлен счетчик запуска MinerSearch
- Добавлен параметр --winpemode. В это режиме запрашивается буква диска для сканирования. Предполагается, что это диск с зараженной системой. Сканирование процессов, реестра, задач планировщика, каталогов в профиле пользователя не производится. Далее, необходимо снова загрузится в зараженную систему и выполнить повторное сканирование. 

Версия v1.4.3

- Добавлен сигнатурное обнаружение подозрительных файлов
- Добавлена краткая информация о системе в лог
- Удаление вредоносных записей в hosts файле, а не самого файла. Подтверждение не требуется
- Исправлен баг, при котором не удается получать аргументы командной строки в связи с отключением службы WMI.
- Добавлен карантин. Кроме файлов, обнаруженных при статическом анализе, также создается txt файл с прежними путями этих файлов
- Исправить баг при неверном определении инжекта в процесс Dwm на Win 8 (не 8.1)
- Удаление пользователя John, если это не текущий пользователь
- Добавлена создание защищенных скрытых файлов от повторного заражение
- Обработка исключения TCP подключений для процессов
- Исправлен баг с ошибкой "Отказано в доступе" для некоторых файлов
- Исправлен баг с невозможностью разблокировать каталог при установленом Антивирусе
- Добавлено автоматическое удаление недействительных задач из планировщика на основе результата последнего запуска. Для лучшего результата следует перезагрузить ПК после сканирования. Параметр --remove-empty-tasks также работает.
- Добавлен параметр запуска --no-signature-scan для пропуска скнирования по сигнатурам

Версия v1.4.2

- Добавлено определение загрузки ОС
- Добавлено удаление вредоносных путей из исключения Windows Defender
- Добавлено удаление вредоносных правил из брандмауэра Windows
- Удаление каталогов и восстановление прав на них выполняется отдельно
- Сканирование файла hosts теперь выполняется в конце
- Исправлен баг с пропуском дубликатов задач в планировщике задач

---------------------------------------------------

This program is designed to find and destroy hidden miners.
It is an auxiliary tool for searching suspicious files, directories, processes, etc. and is NOT an antivirus.

### ATTENTION! Some antiviruses mark this project as a Trojan. This is a false positive, just add the executable file to the antivirus exception.

Version 1.4.7.1

- Added detection of a malicious version of the printing utility (print.exe )
- Added Windows Installer check (msiexec.exe )
- added verification of the installed NET Framework 4.5.X package
- Fixed the utility hanging when scanning processes if there are several systems
- Fixed localization typos
- Correct powershell check
- user name check now use native functions 
- added the --no-scan-tasks parameter to skip scanning of scheduler tasks
- updating signatures

Version 1.4.7.0

- Added Russian translation
- Fixed the processing of paths with non-breaking space
- Added inject prevention of unsigned Microsoft dlls
- Added prevention of blocking by hash of the icon in the title bar
- Fixed error 0xc0000005 when checking processes in safe mode without networking
- Added support for small screen resolutions
- Added logging version of application to the file
- Added a log entry that previously found processes were suspended while program crash
- Added the --debug parameter for debugging. It is used when launching the application via cmd or powershell

Version 1.4.6.1

- Recording the boot mode in the log (normal or safe)
- Detecting the operation of another copy of the program
- Detecting the injection to the explorer's process
- Removing unwanted app Blocking rules by AppLocker Policy
- Added the Russian help language
- Fixed a bug where the console window is set to be larger than allowed
- Removed text selection option in the console, preventing accidental suspension of the application
- Infected files will be encrypted before being added to quarantine
- Added the --restore=<path> command to restore the specified file from quarantine

Version 1.4.6.0

- Added detection if application launched from the archive
- Added the correct detection of the Windows version
- Fixed attempt to accidentally delete a legal one .bat-script
- Fixed the loss of the list of malicious processes from memory when application crashes
- Added option for disabling of the services installed by miner-trojan
- Added file signature verification from arguments rundll32.exe and pcalua.exe , not the files themselves
- Fixed the error "There is no access to this file from system"
- Fixed a bug when a malicious file is deleted, but the message "No threats found" is displayed
- Added sfx archive detection in autorun
- Restoring the Remote Desktop Service and removing illegal the RDP Wrapper
- Determining an executable file by signature, not by extension
- Added the --no-services parameter to skip scanning services
- Added a parameter --full-scan to add all additional local disks for signature scanning
- Added the --scan-only parameter. Display suspicious or malicious objects, but do not perform treatment.

Version 1.4.5.2

- Redesigned the algorithm for obtaining the remote port of the process (without netstat.exe)
- The log file is written to the _MinerSearch_Logs directory of the scanned disk (usualy C:\_MinerSearch_Logs), and not directly to the root
- Update / adding new signatures

Version v1.4.5.1

- Added a license agreement window
- Correction of the error of incorrect closing of the rk_test process
- Added missing parameter --no-rootkit-check. Disables checking for the presence of the miner's rootkit
- Reduction of false positives detection of third-party antivirus solutions

Version v1.4.5

- Output of task launch parameters from the scheduler
- Microsoft's third-party library.Win32.TaskScheduler is no longer required.
- Added detection and removal of the miner's rootkit
- Fixed a bug when the scanned process has already completed its work
- Fixed a bug when requested access to the registry is denied
- The log file is written to the root of the scanned disk
- The required version of the NET Framework has been downgraded to 4.5.2
- Correction of minor flaws
- Replenishment of the database of malicious entries in the hosts file

Version v1.4.4

- Fixed a bug with the inability to delete found files before closing the program
- Improved miner search algorithm for static analysis
- Reduced false triggering of Antivirus solutions. Some antiviruses can still mark a file as a Trojan, just add it to the exception.
- Added the --depth parameter. Sets the maximum value of the search depth during signature scanning. Usage example: --depth=3 (default is 8).
- Added signature verification of files from the task scheduler
- Added MinerSearch launch counter
- Added the --winpemode parameter. In this mode, the drive letter is requested for scanning. It is assumed that this is a disk with an infected system. Scanning of processes, registry, scheduler tasks, directories in the user profile is not performed. Next, you need to boot into the infected system again and perform a second scan.

Version v1.4.3

- Added signature detection of suspicious files
- Added brief information about the system to the log
- Removal of malicious entries in the hosts file, not the file itself. Confirmation is not required
- Fixed a bug where it is not possible to receive command line arguments due to the shutdown of the WMI service.
- Added quarantine. In addition to the files found during static analysis, a txt file is also created with the previous paths of these files
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
- Added removal of malicious paths from Windows Defender exception
- Added removal of malicious rules from Windows Firewall
- Deleting directories and restoring rights to them is performed separately
- The hosts file is now scanned at the end
- Fixed a bug with missing duplicate tasks in the task scheduler

--------------------------------------------------------------

# Demo

Обнаруживает и приостанавливает вредоносные процессы, а также вспомогательне компоненты майнера, которые затруденяют его удаление.
## Screenshots

![image](https://user-images.githubusercontent.com/56220293/215475650-25d31515-d52a-485b-b194-7db63e0e9962.png)

![image2](https://user-images.githubusercontent.com/56220293/215356942-8080b05a-f324-4006-9864-6843923ff2be.png)
