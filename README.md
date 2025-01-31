# Miner Search

Настоящая программа разработанная для поиска и уничтожения скрытых майнеров.
Является вспомогательным инструментом для поиска подозрительных файлов, каталогов, процессов и тд. и НЕ является антивирусом. 

## Новости об обновлениях теперь в телеграм! 
## https://t.me/MinerSearch_blog
### Для работы приложения требуется NET Framework 4.7.1

![GitHub Downloads (all assets, latest release)](https://img.shields.io/github/downloads/BlendLog/MinerSearch/latest/total?logoColor=AA00F0&color=Navy)

Версия 1.4.7.8

- Поиск и удаление новых майнеров, в том числе nanominer
- Добавлена кнопка "Поддержать проект" на форме с отчетом
- Добавлена возможность вручную разрешать или запрещать отправку результатов проверки разработчику
- Опция --full-scan теперь позволяет сканировать весь системный раздел, не только указанные по-умолчанию каталоги
- Добавлена опция --verbose для подробных сведений о процессах, а также запись в лог строки с файлами, не признанными вредоносными
- Добавлен параметр --run-as-system для запуска приложения с правами системы
- Добавлена опция --select, которая позволит сканировать только выбранный каталог, включая вложенные каталоги
- Корректная работа с опцией --scan-only
- Добавлен каталог C:\Users\ для сканирования
- добавлена краткая форма параметров запуска (смотрите в --help \ -h)
- Добавлена проверка потребляемой памяти процессов
- Добавлен кастомный MessageBox, так как системный может быть перекрыт окнами проводника или другим приложением
- Добавлена обработка блокировки .dll защитником Windows
- Исправлена локализация кнопки "Карантин"
- Исправление замирания рабочего стола на Win 7
- Исправлено повторное сканирование каталогов с опцией --full-scan
- Исправлено удаление файлов при перемещении в карантин
- Исправлено частичное удаление майнера, маскирующегося под explorer.exe на Win 7, Win 8.1
- Исправлена ошибка "Не запущена служба сервера" при удалении вредоносного профиля John
- Исправлен "Сбой загрузки поставщика" при проверке служб
- Исправлено ложное срабатывание на VS Code
- Исправлен баг счётчика файлов в карантине
- Удаление записей из планировщика задач, которые добавляют рекламу в автозапуск
- Удаление новых вредоносных скриптов в планировщике задач через forfiles и wscript
- Удаление некорректных параметров MinimumStackCommitInBytes для всех подразделов в IFEO, мешающие нормальному запуску программ
- Удаление java-майнера в планировщике задач
- Удаление вредоносных версии uTorrent, MS Teams и Steam
- Оптимизация при работе с памятью
- Файл hosts теперь корректируется только в случае заражения и не вызывает ошибку
- Обновлена справка --help
- Карантин теперь доступен для всех пользователей

--------------------------------------------

## Как пользоваться

Полностью распакуйте архив с программой в отдельную папку и запустите приложение. Дождитесь окончания сканирования. При первом использовании программы предлагается сообщать о результатах проверки автору на ваше усмотрение. После окончания будет показана форма с кратким отчётом об угрозах, которые были устранены. С подробным отчётом можно ознакомиться, нажав на кнопку "Открыть отчёт". При нажатии на кнопку Карантин откроется Менеджер карантина, в котором вы можете полностью удалить файл или восстановить.

Дополнительные параметры запуска (обычно не требуется):

| Параметр | описание |
|----------|-----------
| -h     --help               |  Вызов этой справки                                                                                    |
| -nl    --no-logs            |  Не записывать лог в файл                                                                              |
| -nstm  --no-scantime        |  Сканировать только процессы                                                                           |
| -nr    --no-runtime         |  Не сканировать процессы (только каталоги, файлы, ключи реестра, и т.д.)                               |
| -nse   --no-services        |  Пропустить сканирование служб                                                                         |
| -nss   --no-signature-scan  |  Пропустить сигнатурное сканирование файлов                                                            |
| -nrc   --no-rootkit-check   |  Не проверять присутствие руткита                                                                      |
| -p     --pause              |  Пауза перед очисткой                                                                                  |
| -ret   --remove-empty-tasks |  Удалять задачу из Планировщика задач, если файл приложения в ней не существует                        |
| -so    --scan-only          |  Отображать вредоносный или подозрительный объект, но не выполнять лечение                             |
| -fs    --full-scan          |  Целиком добавляет другие локальные диски для сигнатурного сканирования                                |
| -ras   --run-as-system      |  Запустить проверку от имени SYSTEM (для опытных пользователей)	                                       |
| -s     --select             |  Сканировать только выбранный каталог, включая вложенные каталоги                                      |
| -d=    --depth=<num>        |  Где <num> - уровень максимальной глубины поиска. Пример использования: -d=5 (по-умолчанию 8)          |
| -v     --verbose            |  Выводит подробные сведения о процессах в консоль, а также                                             |
|                             |  отключает фильтр строк с файлами не признанных вредоносными. Может увеличить размер лог-файла.        |
| -w     --winpemode          |  Запускает сканирование в режиме WinPE                                                                 |
|                             | (без сканирования процессов, реестра, правил фаерволла, служб, задач планировщика)                     |

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

Version 1.4.7.8

- Search and remove new miners, including nanominer
- Added "Donate" button on the scan results form
- Added the ability to manually allow or prohibit sending scan results to the developer
- The --full-scan option now allows you to scan the entire system partition, not just the default directories
- Added the --verbose option for detailed info about processes, as well as writing to the log a line with files that are not considered as malicious
- Added the --run-as-system parameter to run the application with SYSTEM privileges
- Added the --select option, which will allow you to scan only the selected directory, including nested directories
- Correct work with the --scan-only option
- Added the C:\Users\ directory for scanning
- Added a short form of launch parameters (see --help \ -h)
- Added a check for the memory consumed by processes
- Added a custom MessageBox, since the system one can be overlapped by windows of explorer.exe or another application
- Added handling of .dll blocking by Windows Defender
- Fixed localization of the "Quarantine" button
- Fixed freezing of the desktop on Win 7
- Fixed re-scanning of directories with the --full-scan option
- Fixed deleting files when moving to quarantine
- Fixed partial removal of the miner disguised as explorer.exe on Win 7, Win 8.1
- Fixed the error "The Server service not started" when deleting the malicious John profile
- Fixed "Provider loading failure" when checking services
- Fixed a false positive on VS Code
- Fixed a bug in the quarantine file counter
- Removing entries from the task scheduler that add ads to autorun
- Removing new malicious scripts in the task scheduler via forfiles and wscript
- Removing incorrect MinimumStackCommitInBytes parameters for all subsections in IFEO that interfere with the normal launch of programs
- Removing the java miner in task scheduler
- Removal of malicious versions of uTorrent, MS Teams and Steam
- Optimization when working with memory
- The hosts file is now corrected only in case of infection and does not cause an error
- Help has been updated --help
- Quarantine is now available for all users

Version 1.4.72 [PostFix]

- Improved miner search algorithm. ((TLauncher Legacy, goodbyedpi, RustMe and others are no longer considered a threat)
- Fixed the inability to move a file to quarantine
- Removing malicious scripts from the task scheduler that run the miner
> [What's new]
- Added a quarantine manager (option -q or --open-quarantine, or in the report form just click on "Quarantine" button)
- Removed the "--restore" option, since the strategy for moving files to quarantine has also been changed (incompatible with previous versions of MinerSearch)

-----------------------------------------

## How to use

Completely unzip the archive with the program into a separate folder and launch the application. Wait for the scan to complete. When using the program for the first time, you are offered to report the results of the scan to the author at your wish. After completion, a form will be shown with a brief report on the threats that have been eliminated. You can view the detailed log by clicking on the "Show log" button. Clicking on the "Quarantine" button will open the Quarantine Manager, in which you can completely delete the file or restore it.

Additional command line args (usually is not required):

| Startup params | Description |                                                                                                           
|----------------|-------------|	                                                                                                          
| -h     --help                | This help message                                                                                        |
| -nl    --no-logs             | Don't write logs in text file                                                                            |
| -nstm  --no-scantime         | Scan processes only                                                                                      |
| -nr    --no-runtime          | Static scan only (Malware dirs, files, registry keys, etc)                                               |
| -nse   --no-services         | Skip scan services                                                                                       |
| -nss   --no-signature-scan   | Skip scan files by signatures                                                                            |
| -nrc   --no-rootkit-check    | Skip checking rootkit present                                                                            |
| -p     --pause               | Pause before cleanup                                                                                     |
| -ret   --remove-empty-tasks  | Delete a task from the Task Scheduler if the application file does not exist in it                       |
| -so    --scan-only           | Display malicious or suspicious objects, but do nothing                                                  |
| -fs    --full-scan           | Add other entire local drives for signature scan                                                         |
| -ras   --run-as-system       | Start scannning with SYSTEM privilege (for advanced users)                                               |
| -s     --select              | Only selected folder will be scanned, including subfolders                                               |
| -d=    --depth=<number>      | Where <number> specify the number for maximum search depth. Usage example -d=5 (default 8)               |
| -v     --verbose             | Displays more info to the console and a log file,                                                        |
|                              | including lines about files that are not considered malicious. It may increase the size of the log file. |
| -w     --winpemode           | Start scanning in WinPE environment by specifying a different drive letter                               |
|                              | (without scanning processes, registry, firewall and task scheduler entries)                              |

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

## Screenshots
Stop and remove malicious processes and his support components, that makes deletion malware harder
![first](https://github.com/user-attachments/assets/29828484-6d57-4e71-ad5c-641913ce34f7)

Final report form
![second](https://github.com/user-attachments/assets/309e7625-bc57-4b80-9052-4805c33f9486)


