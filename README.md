# Miner Search

Настоящая программа разработанная для поиска и уничтожения скрытых майнеров.
Является вспомогательным инструментом для поиска подозрительных файлов, каталогов, процессов и тд. и НЕ является антивирусом. 

## Новости об обновлениях теперь в телеграм!
https://t.me/MinerSearch_blog

### ВНИМАНИЕ! Некоторые антивирусы помечают этот проект как троян. Это ложное срабатывание, просто добавьте исполняемый файл в исключение антивируса

Версия 1.4.7.2

- Наличие руткита майнера определяется быстрее
- Разделение базы данных от основного приложения
- Исправлена обработка путей с прямым(обычным) слешем

--------------------------------------------

## Как пользоваться

Полностью распакуйте архив с программой в отдельную папку и запустите приложение. Дождитесь окончания сканирования. После окончания будет указано итоговое время проверки.
Важно примечание: на данный момент количество обнаруженных угроз указано для каждого этапа. Ознакомиться с полным отчётом (логом) сканирования можно в файле MinerSearch_<датавремя>.log,
генерируемый по-умолчанию в C:\\_MinerSearchLogs.

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

---------------------------------------------------

This program is designed to find and destroy hidden miners.
It is an auxiliary tool for searching suspicious files, directories, processes, etc. and is NOT an antivirus.

## News about updates now in the Telegram!
https://t.me/MinerSearch_blog


### ATTENTION! Some antiviruses mark this project as a Trojan. This is a false positive, just add the executable file to the antivirus exception.

Version 1.4.7.2

- The presence of a miner's rootkit is detected faster
- Separation of the database from the main application
- Fixed bug of processing of paths with direct (usual) slash

-----------------------------------------

## How to use

Extract entire archive to the empty folder and run the application. Wait while scanning do. After full scan the app will be display total elapsed time of scan.
Important note: at this moment, the quantity threats displays on each scanning stage. You can read full log of scanning in MinerSearch_<datetime>.log file, that will be generated in C:\\_MinerSearchLogs by default.

Additional command line args (usually is not required):

| Параметр | Описание |
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

# Demo

Обнаруживает и приостанавливает вредоносные процессы, а также вспомогательне компоненты майнера, которые затруденяют его удаление.
## Screenshots

![image](https://user-images.githubusercontent.com/56220293/215475650-25d31515-d52a-485b-b194-7db63e0e9962.png)

![image2](https://user-images.githubusercontent.com/56220293/215356942-8080b05a-f324-4006-9864-6843923ff2be.png)
