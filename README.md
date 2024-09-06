# Miner Search

Настоящая программа разработанная для поиска и уничтожения скрытых майнеров.
Является вспомогательным инструментом для поиска подозрительных файлов, каталогов, процессов и тд. и НЕ является антивирусом. 

## Новости об обновлениях теперь в телеграм! 
## https://t.me/MinerSearch_blog

Версия 1.4.7.4

- Сбор статистики удаления угроз (на усмотрение пользователя)
- Восстановление прав доступа существующих приложений из списка заблокированных каталогов
- Добавлена тщательная проверка процессов по времени использования CPU
- Добавлена легенда условных обозначений в логе
- Добавлена кнопка "Подробно" для открытия папки с логами
- Более точное обнаружение руткита майнера
- Нормальный формат даты в имени лог файла
- Улучшена проверка служб, включая Службы удаленных рабочих столов (TermService)
- Восстанавление базы данных WMI
- Удаление новых вредоносных версий системных программ

[HOTFIX]
- Исправление некорректного чтения файла hosts
- Корректное создание индентификатора устройства в логе
- Улучшена обработка планировщика задач
- Исправлено ложное срабатываение на легальный скрипт powershell (UnusedSmb1.ps1)
- Добавлено удаление задачи руткита из планировщика задач 
- Улучшено обнаружение угроз, которые запускаются через RunDLL
- Предположительно вредоносные файлы перемещаются в карантин
- Легенда обозначений перемещена в начало лог-файла
- Исправлено исчезнавение крестика "закрыть" в окне с кратким отчётом
- Проверка установленной версии NET Framework 4.5.2 (для windows 7)

Версия 1.4.7.3

- Удаление новой версии майнера с цифровой подписью;
- Исправлено автоматическое закрытие приложения в случае сбоя;
- Переработан алгоритм отключения вредоносных служб;
- Исправлено добавление удалённых файлов в карантин;
- Добавлена проверку удаления файла из аргументов msiexec;
- Добавлен счетчик всех найденных угроз;
- Добавлено игнорирование новых функций удаления с опцией --scan-only;
- Добавлено отображение строк из файла hosts, которые были обезврежены;
- Исправлена обработка каталогов в виде символической ссылки;
- Теперь статистика отображатеся в диалоговом окне;
- Вывод уведомления по завершении сканирования;

--------------------------------------------

## Как пользоваться

Полностью распакуйте архив с программой в отдельную папку и запустите приложение. Дождитесь окончания сканирования. После окончания будет указано итоговое время проверки.
Важно примечание: на данный момент количество обнаруженных угроз указано для каждого этапа. Ознакомиться с полным отчётом (логом) сканирования можно в файле MinerSearch_<датавремя>.log,
генерируемый по-умолчанию в каталоге C:\\_MinerSearchLogs\\.

Дополнительные параметры запуска (обычно не требуется):

| Параметр | Описание |
| -------- | -------- |
|--help | Вызов справки |
| --no-logs	| Не вести журнал сканирования |
| --no-scantime | Сканировать только процессы |
| --no-runtime	| Не сканировать процессы (только каталоги, файлы, ключи реестра, и т.д.) |
| --no-services | Пропустить сканирование служб |
| --no-signature-scan | Пропустить сигнатурное сканирование файлов |
| --no-rootkit-check | Не проверять присутствие руткита |
| --depth=[число] | Где [число] — уровень максимальной глубины поиска. Пример использования --depth=5 (по-умолчанию 8) |
| --pause | Пауза перед очисткой |
| --remove-empty-tasks | Удалять задачу из Планировщика задач, если исполняемый файл не существует
| --winpemode | Запускает сканирование в режиме WinPE (без сканирования процессов, реестра, правил фаерволла, служб, задач планировщика) |
| --scan-only	| Отображать вредоносный или подозрительный объект, но не выполнять лечение |
| --full-scan | Целиком добавляет другие локальные диски для сигнатурного сканирования |
| --restore=[путь] | Восстановить указанный файл из карантина, путь к файлу в карантине не должен содержать пробелов |

----------------------------

Условные обозначения в логах

| Подсказка | Описание |
|-----------|----------|
|    [!] | Незначительное предупреждение |
|   [!!] | Предупреждение, на которое стоит обратить внимание |
|  [!!!] | Обнаружена угроза |
| [!!!!] | Обнаружен руткит |
|  [Reg] | Cканирование раздела(ов) реестра |
|    [+] | Успешное выполнение действия (лечение, удаление и т.д.) |
|    [x] | Ошибка |
|  [xxx] | Критическая ошибка: например, при запуске в песочнице |
|    [#] | Статус |
|    [.] | Описание |
|    [_] | Разблокировка каталога и удаление, если пуст |
|    [i] | Информация |
|    [$] | Затраченное время сканирования |

---------------------------------------------------

This program is designed to find and destroy hidden miners.
It is an auxiliary tool for searching suspicious files, directories, processes, etc. and is NOT an antivirus.

## News about updates now in the Telegram!
https://t.me/MinerSearch_blog

Version 1.4.7.4

- Fix of threat removal statistics (optional by user)
- Restoring access rights of existing applications from the list of blocked directories
- Added a thorough check of processes by CPU usage time
- Added legend of symbols in the log
- Added the "Show details" button to open the folder with logs
- More accurate detection of the miner's rootkit
- The normal date format in the log file name
- Improved verification of services, including Remote Desktop (TermService)
- Restoring the WMI database
- Removal of new malicious versions of system programs 
[HOTFIX]
- Fix of incorrect reading of the hosts file
- Fixed creation of the device identifier in the log
- Improved task scheduler processing
- Fixed a false positive for a legitimate powershell script (UnusedSmb1.ps1)
- Added removal of the rootkit task from the task scheduler 
- Improved detection of threats that run through RunDLL
- Presumably malicious files are being moved to quarantine
- The legend of the symbols has been moved to the beginning of the log file
- Fixed the disappearance of the "close" cross in the summary report window
- Checking the installed version of NET Framework 4.5.2 (for windows 7)

Version 1.4.7.3

- Removal of the new version of the miner with a digital signature;
- Fixed automatic closing of the application when it crash;
- Updated algorithm for disabling malicious services;
- Fixed adding deleted files to quarantine;
- Added a check for deleting a file from msiexec arguments;
- Added a counter for all threats found;
- Added ignoring of new deletion functions with the --scan-only option;
- Added display of lines from the hosts file that have been neutralized;
- Fixed directories scan as a symbolic link;
- Statistics are now displayed in the new form;
- Notification output at the end of scan;

-----------------------------------------

## How to use

Extract entire archive to the empty folder and run the application. Wait while scanning do. After full scan the app will be display total elapsed time of scan.
Important note: at this moment, the quantity threats displays on each scanning stage. You can read full log of scanning in MinerSearch_<datetime>.log file, that will be generated in folder C:\\_MinerSearchLogs\\ by default.

Additional command line args (usually is not required):

| Argument | Description |
| -------- | -------- |
|--help | Show this help message |
| --no-logs	| Don't write logs in text file |
| --no-scantime | Scan processes only |
| --no-runtime	| Static scan only (Malware dirs, files, registry keys, etc) |
| --no-services | Skip scan services |
| --no-signature-scan | Skip scan files by signatures |
| --no-rootkit-check | Skip checking rootkit present |
| --depth=[number] | Where <number> specify the number for maximum search depth. Usage example --depth=5 (default 8) |
| --pause | Pause before cleanup |
| --remove-empty-tasks | Delete a task from the Task Scheduler if the application file does not exist in it |
| --winpemode | Start scanning in WinPE environment by specifying a different drive letter (without scanning processes, registry, firewall and task scheduler entries) |
| --scan-only	| Display malicious or suspicious objects, but do nothing |
| --full-scan | Add other entire local drives for signature scan |
| --restore=[path] | Restore specified file from quarantine. Specified path can't include space symbol |

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

# Demo

Обнаруживает и приостанавливает вредоносные процессы, а также вспомогательне компоненты майнера, которые затруденяют его удаление.
## Screenshots

![image](https://user-images.githubusercontent.com/56220293/215475650-25d31515-d52a-485b-b194-7db63e0e9962.png)

![image2](https://user-images.githubusercontent.com/56220293/215356942-8080b05a-f324-4006-9864-6843923ff2be.png)
