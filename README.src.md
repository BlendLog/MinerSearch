<!--@nrg.languages=en,ru,cn-->
<!--@nrg.defaultLanguage=en-->
# Miner Search<!--en-->
<!--en-->
[Русский](README.ru.md) | English | [Chinese](README.cn.md)<!--en-->
<!--en-->
This program is designed to find and destroy hidden miners.<!--en-->
It is an auxiliary tool for searching suspicious files, directories, processes, etc. and is NOT an antivirus.<!--en-->
<!--en-->
> [!CAUTION]<!--en-->
> ### Antivirus may give a false positive reaction to this application. Please don't create an issue about this.<!--en-->
<!--en-->
## News about updates now in the Telegram!<!--en-->
https://t.me/MinerSearch_blog<!--en-->
## ⬇ ![Download latest version](https://github.com/BlendLog/MinerSearch/releases/latest)<!--en-->
### NET Framework 4.7.2 is required<!--en-->
<!--en-->
> [!CAUTION]<!--en-->
> ### Windows 7 is outdated. MinerSearch support and testing for this OS will be discontinued coming soon.<!--en-->
<!--en-->
![GitHub Downloads (all assets, latest release)](https://img.shields.io/github/downloads/BlendLog/MinerSearch/latest/total?logoColor=AA00F0&color=Navy)<!--en-->
![GitHub Downloads (all assets, all releases)](https://img.shields.io/github/downloads/BlendLog/MinerSearch/total?label=downloads%40All&color=ff6600)<!--en-->
<!--en-->
Version v1.4.8.41<!--en-->
<!--en-->
- Removal of new malicious services, files, tasks, and directories<!--en-->
- Added the --no-scan-wmi (-nwmi) option<!--en-->
- Removed the --run-as-system option<!--en-->
- Fixed a bug when reading the service image path<!--en-->
- Fixed a bug when opening a log file using the "Show log" button<!--en-->
<!--en-->
-----------------------------------------<!--en-->
<!--en-->
## How to use<!--en-->
<!--en-->
Completely unzip the archive with the program into a separate folder and launch the application. Wait for the scan to complete. When using the program for the first time, you are offered to report the results of the scan to the author at your wish. After completion, a form will be shown with a brief report on the threats that have been eliminated. You can view the detailed log by clicking on the "Show log" button. Clicking on the "Quarantine" button will open the Quarantine Manager, in which you can completely delete the file or restore it.<!--en-->
<!--en-->
----------------<!--en-->
How to switch language in the app?<!--en-->
<!--en-->
1) Create language.cfg file if not exist<!--en-->
2) Open it with any text editor<!--en-->
3) Choose your preferred language: EN or RU<!--en-->
<!--en-->
----------------<!--en-->
<!--en-->
The application also supports additional launch parameters (listed below). To use them, you should:<!--en-->
1) Run the command line (cmd) as administrator<!--en-->
2) Hold down shift and right-click on the application - select "Copy as path"<!--en-->
3) Paste the path into the command line and add the necessary parameters* after a space<!--en-->
<!--en-->
Additional command line args (usually is not required):<!--en-->
<!--en-->
Generated markdown<!--en-->
| Short Option | Long Option         | Description                                                                                                                                                                                                                                                                                                                         |<!--en-->
| :----------- | :------------------ | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |<!--en-->
| `-h`         | `--help`            | Display this help message                                                                                                                                                                                                                                                                                                           |<!--en-->
| `-a`         | `--accept-eula`     | Accept the End-User License Agreement (EULA)                                                                                                                                                                                                                                                                                        |<!--en-->
| `-nl`        | `--no-logs`         | Do not write logs to a file                                                                                                                                                                                                                                                                                                         |<!--en-->
| `-nstm`      | `--no-scantime`     | Scan only running processes                                                                                                                                                                                                                                                                                                         |<!--en-->
| `-nr`        | `--no-runtime`      | Do not scan running processes (only directories, files, registry keys, etc.)                                                                                                                                                                                                                                                        |<!--en-->
| `-nse`       | `--no-services`     | Skip scanning services                                                                                                                                                                                                                                                                                                              |<!--en-->
| `-nss`       | `--no-signature-scan` | Skip file signature scanning                                                                                                                                                                                                                                                                                                        |<!--en-->
| `-nrc`       | `--no-rootkit-check` | Disable the rootkit check                                                                                                                                                                                                                                                                                                           |<!--en-->
| `-nch`       | `--no-check-hosts` | Skip checking the hosts file                                                                                                                                                                                                                                                                                                        |<!--en-->
| `-nfw`       | `--no-firewall`     | Skip scanning firewall rules                                                                                                                                                                                                                                                                                                        |<!--en-->
| `-cm`        | `--console-mode`    | Enable console mode (no dialog boxes)                                                                                                                                                                                                                                                                                               |<!--en-->
| `-p`         | `--pause`           | Pause before cleanup                                                                                                                                                                                                                                                                                                                |<!--en-->
| `-ret`       | `--remove-empty-tasks` | Remove a task from Task Scheduler if its executable file does not exist                                                                                                                                                                                                                                                           |<!--en-->
| `-so`        | `--scan-only`       | Report malicious or suspicious objects but do not remove or quarantine them                                                                                                                                                                                                                                                         |<!--en-->
| `-fs`        | `--full-scan`       | Include all local drives in the signature scan                                                                                                                                                                                                                                                                                      |<!--en-->
| `-ras`       | `--run-as-system`   | Run the scan as the SYSTEM account (for advanced users only)                                                                                                                                                                                                                                                                        |<!--en-->
| `-f`         | `--force`           | Suppress confirmation prompts for potentially dangerous actions                                                                                                                                                                                                                                                                     |<!--en-->
| `-s`         | `--select`          | Scan only the selected directory and its subdirectories (recursively)                                                                                                                                                                                                                                                               |<!--en-->
| `-s=`        | `--select= <path>`  | Same as `--select (-s)`. Specifies the directory path to scan.                                                                                                                                                                                                                                                                      |<!--en-->
| `-si`        | `--silent`          | Enable silent mode. The application runs in the background without dialog boxes. Messages are suppressed but still written to the log file. Incompatible with `--select` and `--winpemode`.                                                                                                                                             |<!--en-->
| `-d=`        | `--depth=<num>`     | Sets the maximum scan depth. Example: `-d=5` (default is 8).                                                                                                                                                                                                                                                                        |<!--en-->
| `-v`         | `--verbose`         | Enable verbose output. Displays detailed process information and includes non-malicious files in the output. This may significantly increase the log file size.                                                                                                                                                                   |<!--en-->
| `-w`         | `--winpemode`       | Run the scan in WinPE mode (skips scanning processes, registry, firewall rules, services, and scheduled tasks).                                                                                                                                                                                                                   |<!--en-->
| `-q`         | `--open-quarantine` | Open the Quarantine Manager                                                                                                                                                                                                                                                                                                         |<!--en-->
| `-res`       | `--restore= <list>` | Restore files from quarantine in console mode (e.g., `1,2,3`). Use `-q -cm` to see the list of quarantined items.                                                                                                                                                                                                                    |<!--en-->
| `-del`       | `--delete= <list>` | Delete files from quarantine in console mode (e.g., `1,2,3`). Use `-q -cm` to see the list of quarantined items.                                                                                                                                                                                                                      |<!--en-->
<!--en-->
<!--en-->
* Not necessarily in strict order<!--en-->
--------------------------------------------------------------<!--en-->
<!--en-->
Symbols in logs<!--en-->
<!--en-->
| Hint | Description |<!--en-->
|-----------|----------|<!--en-->
|    [!] | Minor warning |<!--en-->
| [!!] | A warning worth paying attention to |<!--en-->
|  [!!!] | Threat detected |<!--en-->
| [!!!!] | A rootkit has been detected |<!--en-->
| [Reg] | Scan the registry key(s) |<!--en-->
|    [+] | Successful completion of the action (treatment, removal, etc.) |<!--en-->
|    [x] | Error |<!--en-->
| [xxx] | Critical error: for example, when running in the sandbox |<!--en-->
|    [#] | Status |<!--en-->
| [.] | Description |<!--en-->
|    [_] | Unblocking the directory and deleting if empty |<!--en-->
| [i] | Info |<!--en-->
|    [$] | Scan elapsed time |<!--en-->
<!--en-->
----------------------------<!--en-->
<!--en-->
## Screenshots<!--en-->
Stop and remove malicious processes and his support components, that makes deletion malware harder<!--en-->
![first](https://github.com/user-attachments/assets/29828484-6d57-4e71-ad5c-641913ce34f7)<!--en-->
<!--en-->
Final report form<!--en-->
![second](https://github.com/user-attachments/assets/309e7625-bc57-4b80-9052-4805c33f9486)<!--en-->
<!--en-->
<!--en-->
# Miner Search<!--ru-->
<!--ru-->
Русский | [English](README.md) | [Chinese](README.cn.md)<!--ru-->
<!--ru-->
Настоящая программа разработанная для поиска и уничтожения скрытых майнеров.<!--ru-->
Является вспомогательным инструментом для поиска подозрительных файлов, каталогов, процессов и тд. и НЕ является антивирусом. <!--ru-->
<!--ru-->
> [!CAUTION]<!--ru-->
> ### Антивирус может ошибочно воспринимать приложение за вредоносное ПО. Пожалуйста, не пишите по этой теме в issues.<!--ru-->
<!--ru-->
## Новости об обновлениях теперь в телеграм! <!--ru-->
## https://t.me/MinerSearch_blog<!--ru-->
## ⬇ ![Скачать последнюю версию](https://github.com/BlendLog/MinerSearch/releases/latest)<!--ru-->
### Для работы приложения требуется NET Framework 4.7.2<!--ru-->
<!--ru-->
![GitHub Downloads (all assets, latest release)](https://img.shields.io/github/downloads/BlendLog/MinerSearch/latest/total?logoColor=AA00F0&color=Navy)<!--ru-->
<!--ru-->
> [!CAUTION]<!--ru-->
> ### Windows 7 устарела. Поддержка и тестирование MinerSearch для этой ОС скоро будут прекращены.<!--ru-->
<!--ru-->
Версия v1.4.8.41<!--ru-->
<!--ru-->
- Удаление новых вредоносных служб, файлов, задач и каталогов<!--ru-->
- Добавлена опция --no-scan-wmi (-nwmi)<!--ru-->
- Удалена опция --run-as-system<!--ru-->
- Исправлен баг при чтении пути образа службы<!--ru-->
- Исправлен баг при открытии отчёта по кнопке "Показать отчёт"<!--ru-->
<!--ru-->
--------------------------------------------<!--ru-->
<!--ru-->
## Как пользоваться<!--ru-->
<!--ru-->
Полностью распакуйте архив с программой в отдельную папку и запустите приложение. Дождитесь окончания сканирования. При первом использовании программы предлагается сообщать о результатах проверки автору на ваше усмотрение. После окончания будет показана форма с кратким отчётом об угрозах, которые были устранены. С подробным отчётом можно ознакомиться, нажав на кнопку "Открыть отчёт". При нажатии на кнопку Карантин откроется Менеджер карантина, в котором вы можете полностью удалить файл или восстановить.<!--ru-->
<!--ru-->
----------------<!--ru-->
Как переключить язык в приложении?<!--ru-->
<!--ru-->
1) Создайте файл language.cfg, если его ещё нет<!--ru-->
2) Откройте с помощью любого текстового редактора<!--ru-->
3) Задайте предпочитаемый язык: RU или EN<!--ru-->
<!--ru-->
----------------<!--ru-->
Приложение также поддерживает дополнительные параметры запуска (приведенные ниже). Чтобы воспользоваться ими следует:<!--ru-->
1) Запустить командную строку (cmd) от администратора<!--ru-->
2) зажать shift и кликнуть правой кнопкой мыши по приложению - выбрать пункт "Копировать как путь"<!--ru-->
3) Вставить путь в командную строку и через пробел дописать необходимые параметры*<!--ru-->
<!--ru-->
Дополнительные параметры запуска (обычно не требуется):<!--ru-->
<!--ru-->
Generated markdown<!--ru-->
| Краткая опция | Длинная опция      | Описание                                                                                                                                                                                                                                                                                                                                     |<!--ru-->
| :------------- | :------------------ | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |<!--ru-->
| `-h`           | `--help`            | Вызов этой справки                                                                                                                                                                                                                                                                                                                           |<!--ru-->
| `-a`           | `--accept-eula`     | Принять лицензионное соглашение                                                                                                                                                                                                                                                                                                              |<!--ru-->
| `-nl`          | `--no-logs`         | Не записывать журнал в файл                                                                                                                                                                                                                                                                                                                  |<!--ru-->
| `-nstm`        | `--no-scantime`     | Сканировать только запущенные процессы                                                                                                                                                                                                                                                                                                       |<!--ru-->
| `-nr`          | `--no-runtime`      | Не сканировать запущенные процессы (сканируются только каталоги, файлы, ключи реестра и т.д.)                                                                                                                                                                                                                                                 |<!--ru-->
| `-nse`         | `--no-services`     | Пропустить сканирование служб                                                                                                                                                                                                                                                                                                                |<!--ru-->
| `-nss`         | `--no-signature-scan` | Пропустить сигнатурное сканирование файлов                                                                                                                                                                                                                                                                                                   |<!--ru-->
| `-nrc`         | `--no-rootkit-check` | Отключить проверку на руткиты                                                                                                                                                                                                                                                                                                                |<!--ru-->
| `-nch`         | `--no-check-hosts` | Пропустить проверку файла hosts                                                                                                                                                                                                                                                                                                              |<!--ru-->
| `-nfw`         | `--no-firewall`     | Пропустить сканирование правил брандмауэра                                                                                                                                                                                                                                                                                                   |<!--ru-->
| `-cm`          | `--console-mode`    | Включить консольный режим (без диалоговых окон)                                                                                                                                                                                                                                                                                              |<!--ru-->
| `-p`           | `--pause`           | Пауза перед очисткой                                                                                                                                                                                                                                                                                                                         |<!--ru-->
| `-ret`         | `--remove-empty-tasks` | Удалять задачу из Планировщика заданий, если исполняемый файл задачи не существует                                                                                                                                                                                                                                                            |<!--ru-->
| `-so`          | `--scan-only`       | Сообщать о вредоносных или подозрительных объектах, но не удалять и не помещать их в карантин                                                                                                                                                                                                                                                  |<!--ru-->
| `-fs`          | `--full-scan`       | Включить все локальные диски в сигнатурное сканирование                                                                                                                                                                                                                                                                                      |<!--ru-->
| `-ras`         | `--run-as-system`   | Запустить сканирование от имени учетной записи SYSTEM (только для опытных пользователей)                                                                                                                                                                                                                                                      |<!--ru-->
| `-f`           | `--force`           | Подавлять запросы подтверждения для потенциально опасных действий                                                                                                                                                                                                                                                                             |<!--ru-->
| `-s`           | `--select`          | Сканировать только указанный каталог и его подкаталоги (рекурсивно)                                                                                                                                                                                                                                                                          |<!--ru-->
| `-s=`          | `--select= <путь>`  | Аналогично `--select (-s)`. Указывает путь к каталогу для сканирования.                                                                                                                                                                                                                                                                      |<!--ru-->
| `-si`          | `--silent`          | Включить тихий (фоновый) режим. Приложение работает в фоновом режиме без диалоговых окон. Сообщения не отображаются, но записываются в файл журнала. Несовместимо с параметрами `--select` и `--winpemode`.                                                                                                                                    |<!--ru-->
| `-d=`          | `--depth=<num>`     | Устанавливает максимальную глубину сканирования. Пример: `-d=5` (по умолчанию 8).                                                                                                                                                                                                                                                            |<!--ru-->
| `-v`           | `--verbose`         | Включить подробный вывод. Отображает подробную информацию о процессах и включает невредоносные файлы в отчет. Это может значительно увеличить размер файла журнала.                                                                                                                                                                            |<!--ru-->
| `-w`           | `--winpemode`       | Запустить сканирование в режиме WinPE (пропускает сканирование процессов, реестра, правил брандмауэра, служб и запланированных заданий).                                                                                                                                                                                                      |<!--ru-->
| `-q`           | `--open-quarantine` | Открыть менеджер карантина                                                                                                                                                                                                                                                                                                                   |<!--ru-->
| `-res`         | `--restore= <список>` | Восстановить файлы из карантина в консольном режиме (например, `1,2,3`). Используйте `-q -cm` для просмотра списка карантинных объектов.                                                                                                                                                                                                   |<!--ru-->
| `-del`         | `--delete= <список>` | Удалить файлы из карантина в консольном режиме (например, `1,2,3`). Используйте `-q -cm` для просмотра списка карантинных объектов.                                                                                                                                                                                                           |<!--ru-->
<!--ru-->
<!--ru-->
* Необязательно в строгом порядке<!--ru-->
----------------------------<!--ru-->
<!--ru-->
Условные обозначения в логах<!--ru-->
<!--ru-->
| Подсказка | Описание |<!--ru-->
|-----------|----------|<!--ru-->
|    [!] | Незначительное предупреждение |<!--ru-->
|   [!!] | Предупреждение, на которое стоит обратить внимание |<!--ru-->
|  [!!!] | Обнаружена угроза |<!--ru-->
| [!!!!] | Обнаружен руткит |<!--ru-->
|  [Reg] | Cканирование раздела(ов) реестра |<!--ru-->
|    [+] | Успешное выполнение действия (лечение, удаление и т.д.) |<!--ru-->
|    [x] | Ошибка |<!--ru-->
|  [xxx] | Критическая ошибка: например, при запуске в песочнице |<!--ru-->
|    [#] | Статус |<!--ru-->
|    [.] | Описание |<!--ru-->
|    [_] | Разблокировка каталога и удаление, если пуст |<!--ru-->
|    [i] | Информация |<!--ru-->
|    [$] | Затраченное время сканирования |<!--ru-->
<!--ru-->
----------------------------<!--ru-->
<!--ru-->
## Скриншоты<!--ru-->
Stop and remove malicious processes and his support components, that makes deletion malware harder<!--ru-->
Останавливает и удаляет вредоносные процессы, а также вспомогательных компоненты вируса, затрудняющие его удаление<!--ru-->
![first](https://github.com/user-attachments/assets/29828484-6d57-4e71-ad5c-641913ce34f7)<!--ru-->
<!--ru-->
Форма общих сведений<!--ru-->
![second](https://github.com/user-attachments/assets/309e7625-bc57-4b80-9052-4805c33f9486)<!--ru-->
# Miner Search<!--cn-->
<!--cn-->
[Русский](README.ru.md) | [English](README.md) | Chinese<!--cn-->
<!--cn-->
这个程序旨在查找和销毁隐藏的矿工。<!--cn-->
它是一个辅助工具，用于搜索可疑文件、目录、进程等，并不是一个杀毒软件。<!--cn-->
<!--cn-->
## 现在在Telegram上获取更新消息！<!--cn-->
## https://t.me/MinerSearch_blog<!--cn-->
## ⬇ ![下载最新版本](https://github.com/BlendLog/MinerSearch/releases/latest)<!--cn-->
## 需要.NET Framework 4.7.1<!--cn-->
<!--cn-->
![GitHub Downloads (all assets, latest release)](https://img.shields.io/github/downloads/BlendLog/MinerSearch/latest/total?logoColor=AA00F0&color=Navy)<!--cn-->
<!--cn-->
版本v1.4.7.91<!--cn-->
<!--cn-->
-修复了读取文件和目录时的应用程序崩溃。<!--cn-->
-修正了一个视觉错误，发现了不正确的威胁数量<!--cn-->
-用于检测可疑文件的改进算法<!--cn-->
-改进了第三方网络服务的稳定性处理<!--cn-->
-增加了语言切换功能。 创建一种语言。cfg（内容应为EN或RU）<!--cn-->
-已删除的Windows Defender限制不再在日志中重复<!--cn-->
-增加了--no-check-hosts参数-跳过检查hosts文件<!--cn-->
-为快速报告处理添加了标识符密钥（设备ID）<!--cn-->
-实现了解锁任务管理器和注册表编辑器的机制<!--cn-->
-折叠按钮的正常颜色<!--cn-->
<!--cn-->
-----------------------------------------<!--cn-->
<!--cn-->
## 如何使用<!--cn-->
<!--cn-->
将整个压缩包提取到空文件夹中并运行应用程序。等待扫描完成。完整扫描后，应用程序将显示扫描的总耗时。<!--cn-->
重要提示：此时，威胁数量在每个扫描阶段都会显示。您可以在默认生成的C:\\_MinerSearchLogs\\文件夹中查看完整的扫描日志，文件名为MinerSearch_<datetime>.log。<!--cn-->
<!--cn-->
附加命令行参数（通常不需要）：<!--cn-->
<!--cn-->
|启动参数|描述|                                                                                                           <!--cn-->
|----------------|-------------|	                                                                                                          <!--cn-->
|-h--help|此帮助信息|<!--cn-->
|-nl --no-logs | 不要在文本文件中写入日志|<!--cn-->
|-nstm --no-scantime | 仅扫描进程|<!--cn-->
|-nr --no-runtime|仅静态扫描（恶意软件dirs，文件，注册表项等）|<!--cn-->
|-nse --no-services|跳过扫描服务|<!--cn-->
|-nss --no-signature-scan|通过签名跳过扫描文件|<!--cn-->
|-nrc --no-rootkit-check|跳过检查rootkit存在|<!--cn-->
|-nch --no-check-hosts|跳过检查hosts文件|<!--cn-->
|-p --pause|清理前暂停|<!--cn-->
|-ret --remove-empty-tasks|如果应用程序文件不存在，则从任务计划程序中删除任务|<!--cn-->
|-so --scan-only|显示恶意或可疑对象，但什么都不做|<!--cn-->
|-fs --full-scan|添加其他整个本地驱动器进行签名扫描|<!--cn-->
|-ras --run-as-system|使用系统权限开始扫描（适用于高级用户）|<!--cn-->
|-s --select|只扫描选定的文件夹，包括子文件夹|<!--cn-->
|-si --silent|在没有任何对话框的情况下启用静音（背景）模式。 应用程序切换到后台模式，不会显示消息，但仍会写入日志文件。 与--select或--winpemode选项不兼容|<!--cn-->
|-d= --depth=<number>|其中<number>指定最大搜索深度的数字。 使用示例-d=5（默认为8）|<!--cn-->
|-v--verbose|向控制台和日志文件显示更多信息，|<!--cn-->
||包括不被认为是恶意文件的行。 它可能会增加日志文件的大小。 |<!--cn-->
|-w--winpemode|通过指定不同的驱动器号在WinPE环境中开始扫描|<!--cn-->
||（不扫描进程，注册表，防火墙和任务计划程序条目）|<!--cn-->
<!--cn-->
--------------------------------------------------------------<!--cn-->
<!--cn-->
日志中的符号<!--cn-->
<!--cn-->
| 提示 | 描述 |<!--cn-->
|-----------|----------|<!--cn-->
|    [!] | 小警告 |<!--cn-->
| [!!] | 值得注意的警告 |<!--cn-->
|  [!!!] | 检测到威胁 |<!--cn-->
| [!!!!] | 检测到根套件 |<!--cn-->
| [Reg] | 扫描注册表项 |<!--cn-->
|    [+] | 操作成功完成（处理、移除等） |<!--cn-->
|    [x] | 错误 |<!--cn-->
| [xxx] | 关键错误：例如，在沙盒中运行时 |<!--cn-->
|    [#] | 状态 |<!--cn-->
| [.] | 描述 |<!--cn-->
|    [_] | 解锁目录并在空时删除 |<!--cn-->
| [i] | 信息 |<!--cn-->
|    [$] | 扫描耗时 |<!--cn-->
<!--cn-->
--------------------------------------------------------------<!--cn-->
<!--cn-->
## 截图<!--cn-->
停止和删除恶意进程和他的支持组件，这使得删除恶意软件更难<!--cn-->
<!--cn-->
![first](https://github.com/user-attachments/assets/29828484-6d57-4e71-ad5c-641913ce34f7)<!--cn-->
![second](https://github.com/user-attachments/assets/309e7625-bc57-4b80-9052-4805c33f9486)<!--cn-->
