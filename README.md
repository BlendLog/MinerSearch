# Miner Search

Настоящая программа разработанная для поиска и уничтожения скрытых майнеров.
Является вспомогательным инструментом для поиска подозрительных файлов, каталогов, процессов и тд. и НЕ является антивирусом. 

## Новости об обновлениях теперь в телеграм! 
## https://t.me/MinerSearch_blog
### Для работы приложения требуется NET Framework 4.7.1

![GitHub Downloads (all assets, latest release)](https://img.shields.io/github/downloads/BlendLog/MinerSearch/latest/total?logoColor=AA00F0&color=Navy)

Версия 1.4.7.72 [PostFix]

   - Улучшен алгоритм поиска майнеров. (Tlauncher Legacy, goodbyedpi, rustme и другие больше не рассматриваются как угроза)
   - Исправлена невозможность переместить файл в карантин
   - Удаление вредоносных скриптов из планировщика задач, запускающие майнер
   > [Что нового]
   - Добавлен менеджер карантина (опция -q или --open-quarantine, либо на форме с отчётом "Карантин")
   - Удалена опция "--restore", так как стратегия перемещения файлов в карантин также была изменена (несовместимо с предыдущими версиями MinerSearch)


Версия 1.4.7.7

- Добавлена форма с кратким отчётом, в которой перечислены какие именно угрозы были устранены
- Добавлено сообщение "проверка обновлений..." перед сканированием
- Исправлен парсинг задач, включающих rundll
- Исправлено замирание рабочего стола на некоторых системах
- Обнаружение и удаление новых майнеров
- Минимизирована вероятность ошибочного перемещения в карантин незараженных файлов 
- Исправление "Неверно задано имя папки"
- Исправление "Неверная функция"
- Исправление ошибок при обработке служб (сбой загрузки поставщика)
- Исправление ошибок при обработке задач планировщика

Версия 1.4.7.6

- Устранено сообщение "Запрос выполнен только частично" при проверке на руткит и без него
- Исправлен парсинг путей для rundll в WindowsPowerShell и с ключом /d
- Исправлено некорректное число удаленных строк в hosts (включая подозрительные)
- Добавлена проверка обновлений (на github) 
- Минимизировано кол-во ошибок при скачивании обновлений Windows
- Автоматическое определение WinPE-режима
- Увеличен диапазон каталогов для проверки в WinPE
- Минимальная обработка исключений в WinPE
- Определение доступности необходимых привилегий для группы Администраторов
- Отображение содержимого в AppInit_DLLs, если он не пуст 
- Обнаружение и удаление новых майнеров
- Новый дизайн окон
- Добавлен счётчик подозрительных объектов 

Версия 1.4.7.5

- Более объективное удаление угроз
- Добавлено предложение перезагрузить компьютер, если не все угрозы были устранены с первого раза.
- Минимизировано удаление блокировки телеметрии в hosts
- Устранены ошибки при разблокировке каталогов установленных приложений
- Минимизирована попытка отключить критически важные службы (выводится предупреждение)
- Исключена попытка удалить легальный инструмент для анализа файлов (т.н. Detect It Easy)
- Исключена попытка удалить несуществующий вредоносный каталог на диске 
- Исправлено удаление несуществующего вредоносного подраздела в реестре
- Более тщательная проверка системных процессов
- Анализ файлов выполняется быстрее
- Устранен некорректный сбор журналов найденных угроз на Win 8.1 (не поддерживается на Windows 7)
- Исправление других мелких недоработок

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

## NET Framework 4.7.1 is required

Version 1.4.72 [PostFix]

- Improved miner search algorithm. ((TLauncher Legacy, goodbyedpi, RustMe and others are no longer considered a threat)
- Fixed the inability to move a file to quarantine
- Removing malicious scripts from the task scheduler that run the miner
> [What's new]
- Added a quarantine manager (option -q or --open-quarantine, or in the report form just click on "Quarantine" button)
- Removed the "--restore" option, since the strategy for moving files to quarantine has also been changed (incompatible with previous versions of MinerSearch)

Version 1.4.7.7

- Added a form that contains which specific threats have been neutralized
- Added the message "checking for updates..." before scanning
- Fixed parsing of tasks involving rundll
- Fixed desktop freezing on some systems
- Detection and removal of new miners
- The probability of mistakenly moving uninfected files to quarantine is minimized 
- Fixed "Wrong folder name"
- Fixed "Invalid function"
- Fixed errors in processing services (provider loading failure)
- Fixed errors in processing scheduler tasks

Version 1.4.7.6

- Fixed the message "Only part request was completed" when checking for a rootkit and without it
- Fixed path parsing for rundll in WindowsPowerShell including "/d" key
- Fixed incorrect number of deleted lines in hosts (including suspicious ones)
- Added an update check (on github) 
- The number of errors when downloading Windows updates is minimized
- Automatic detection of WinPE mode
- Increased the range of scan directories in WinPE
- Minimal exception handling in WinPE
- Determining the availability of necessary privileges for the Administrator group
- Displaying the content in AppInit_DLLs if it is not empty 
- Detection and removal of new miners
- New window design
- Added a counter for suspicious objects

Version 1.4.7.5

- More objective threat removal
- Added a suggestion to restart the computer if not all threats were neutralized the first time.
- Minimized removal of telemetry blocking in hosts file
- Fixed errors when unlocking directories of installed applications
- Attempt to disable critical services is minimized (warning is displayed)
- An attempt to remove a legal file analysis tool (e.g. Detect It Easy) is excluded
- An attempt to delete a non-existent malicious directory on the disk is excluded 
- Fixed the removal of a non-existent malicious subsection in the registry
- More accurate check of system processes
- File analysis is faster
- Fixed incorrect collection of logs of detected threats on Win 8.1 (not supported on Windows 7)
- Fixed other minor flaws

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

这个程序旨在查找和销毁隐藏的矿工。
它是一个辅助工具，用于搜索可疑文件、目录、进程等，并不是一个杀毒软件。

## 现在在Telegram上获取更新消息！
https://t.me/MinerSearch_blog

## 需要.NET Framework 4.7.1

版本1.4.7.6

- 修正了在检查rootkit和没有它时"仅部分请求已完成"的消息
- 修复了Windows PowerShell中rundll的路径解析，包括"/d"键
- 修正主机中删除行数不正确（包括可疑行）
- 添加了更新检查（在github上） 
- 下载Windows更新时的错误数量最小化
- 自动检测WinPE模式
- 增加了WinPE中扫描目录的范围
- Winpe中的最小异常处理
- 确定管理员组的必要权限的可用性
- 显示AppInit_DLLs中的内容，如果它不是空的 
- 侦测及清除新矿工
- 新窗口设计
- 增加了可疑对象的计数器

版本 1.4.7.5

- 更客观的威胁移除
- 如果第一次未能中和所有威胁，增加了重启计算机的建议。
- 最小化在hosts文件中移除遥测阻止
- 修复了解锁已安装应用程序目录时的错误
- 尝试禁用关键服务的行为已最小化（显示警告）
- 排除了删除合法文件分析工具（例如Detect It Easy）的尝试
- 排除了删除磁盘上不存在的恶意目录的尝试
- 修复了删除注册表中不存在的恶意子项
- 更准确地检查系统进程
- 文件分析速度更快
- 修复了在Win 8.1上检测到的威胁日志收集不正确的问题（不支持Windows 7）
- 修复了其他小缺陷

版本 1.4.7.4

- 修复威胁移除统计（用户可选）
- 恢复已存在应用程序的访问权限，来自被阻止目录的列表
- 增加了对CPU使用时间的进程彻底检查
- 增加了日志中的符号图例
- 增加了“显示详细信息”按钮以打开日志文件夹
- 更准确地检测矿工的根套件
- 日志文件名中的正常日期格式
- 改进了服务验证，包括远程桌面（TermService）
- 恢复WMI数据库
- 移除新恶意版本的系统程序
[热修复]
- 修复hosts文件读取不正确的问题
- 修复日志中设备标识符的创建
- 改进任务调度程序处理
- 修复合法powershell脚本（UnusedSmb1.ps1）的误报
- 增加了从任务调度程序中移除根套件任务
- 改进了通过RunDLL运行的威胁检测
- 可疑的恶意文件被移动到隔离区
- 符号图例已移至日志文件的开头
- 修复了摘要报告窗口中“关闭”按钮消失的问题
- 检查已安装的.NET Framework 4.5.2版本（适用于Windows 7）

-----------------------------------------

## 如何使用

将整个压缩包提取到空文件夹中并运行应用程序。等待扫描完成。完整扫描后，应用程序将显示扫描的总耗时。
重要提示：此时，威胁数量在每个扫描阶段都会显示。您可以在默认生成的C:\\_MinerSearchLogs\\文件夹中查看完整的扫描日志，文件名为MinerSearch_<datetime>.log。

附加命令行参数（通常不需要）：

| 参数 | 描述 |
| -------- | -------- |
|--help | 显示此帮助信息 |
| --no-logs | 不将日志写入文本文件 |
| --no-scantime | 仅扫描进程 |
| --no-runtime | 仅静态扫描（恶意目录、文件、注册表项等） |
| --no-services | 跳过服务扫描 |
| --no-signature-scan | 跳过按签名扫描文件 |
| --no-rootkit-check | 跳过检查根套件是否存在 |
| --depth=[number] | 其中<number>指定最大搜索深度的数字。使用示例 --depth=5（默认8） |
| --pause | 清理前暂停 |
| --remove-empty-tasks | 如果应用程序文件不存在，则从任务调度程序中删除任务 |
| --winpemode | 在WinPE环境中启动扫描，指定不同的驱动盘字母（不扫描进程、注册表、防火墙和任务调度程序条目） |
| --scan-only | 显示恶意或可疑对象，但不执行任何操作 |
| --full-scan | 添加其他整个本地驱动器进行签名扫描 |
| --restore=[path] | 从隔离区恢复指定文件。指定的路径不能包含空格 |

--------------------------------------------------------------

日志中的符号

| 提示 | 描述 |
|-----------|----------|
|    [!] | 小警告 |
| [!!] | 值得注意的警告 |
|  [!!!] | 检测到威胁 |
| [!!!!] | 检测到根套件 |
| [Reg] | 扫描注册表项 |
|    [+] | 操作成功完成（处理、移除等） |
|    [x] | 错误 |
| [xxx] | 关键错误：例如，在沙盒中运行时 |
|    [#] | 状态 |
| [.] | 描述 |
|    [_] | 解锁目录并在空时删除 |
| [i] | 信息 |
|    [$] | 扫描耗时 |

----------------------------

# Demo

Обнаруживает и приостанавливает вредоносные процессы, а также вспомогательне компоненты майнера, которые затруденяют его удаление.
## Screenshots

![image](https://user-images.githubusercontent.com/56220293/215475650-25d31515-d52a-485b-b194-7db63e0e9962.png)

![image2](https://user-images.githubusercontent.com/56220293/215356942-8080b05a-f324-4006-9864-6843923ff2be.png)
