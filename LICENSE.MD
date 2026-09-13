## Отказ от ответственности

Данное программное обеспечение (Далее ПО) создано исключительно для поиска и удаления вредоносного ПО, тайно использующее ресурсы компьютера для добычи (майнинга) криптовалюты. Автор не несет ответсвенности за причиненный случайный, косвенный или непреднамеренный ущерб любого рода в ходе использования данного ПО. Вы можете использовать его на свой страх и риск. Приложение является вспомогательным инструментом и не заменяет антивирусную защиту.

## Сбор технической информации
Приложение автоматически генерирует анонимный ключ-идентификатор (DeviceID) на основе конфигурации устройства (аппаратные характеристики, версия ОС). Он используется исключительно для:
- Диагностики ошибок в работе приложения;
- Технической поддержки;
- DeviceID не содержит персональных данных;
- Не передаётся третьим лицам и не используется для рекламы или трекинга.

## Где отображается DeviceID?
Ключ-идентификатор указан на финальном окне приложения c кратким отчётом. 

## Необязательная отправка отчётов о сканировании
При первом запуске приложение предлагает отправлять лог сканирования разработчику; это можно изменить в любой момент в настройках. Если отправка разрешена, приложение передаёт на сервер разработчика лог-файл сканирования, DeviceID, версию приложения, число запусков и статистику (найдено / обезврежено / пропущено). Лог-файл может содержать пути к файлам, имена процессов, служб и задач планировщика, обнаруженных на устройстве. Данные используются исключительно для диагностики и улучшения работы приложения, не передаются третьим лицам и не используются для рекламы или трекинга.

## Проверка обновлений
Приложение периодически запрашивает номер текущей версии с сервера разработчика и из официального репозитория GitHub. Персональные данные при этом не передаются.

## Часто задаваемые вопросы:

Вопрос: Почему мой антивирус указывает, что ПО содержит троян/вирус?
Ответ: Сторонний антивирус регистрирует сам факт вмешательства в систему и не может отличить конкретное действие (изменение), что как раз пораждает такую реакцию. Невозможно явно указать, что производимое действие (изменение) выполняется в благих целях. 

Вопрос: ПО закрывается во время работы / зависает / не запускается
Ответ: 
1. Сторонний антивирус мешает нормальной работе;
2. неверные параметры запуска / слишком длинный путь / не установлены / отключены важные компоненты;
3. Вредоносное ПО блокирует его работу - в таком случае необходимо переименовать исполняемый файл "MinerSearch_{номер-версии}.exe" в любое другое.
4. В коде была допущена ошибка, которая требует исправления. Об ошибке писать в issue в официальный репозиторий github. 

Настоящее лицензионное соглашение распространяется на все копии, а также на предыдущие и последующие версии программного обеспечения.

----------------------------------------

## Denial of responsibility

This software (hereinafter referred to as the software) was created exclusively to search and remove malware that secretly uses computer resources for mining cryptocurrency. The author is not responsible for incidental, indirect or unintentional damage of any kind during the use of this software. You may use it at your own risk. The software is an auxiliary tool and does not replace antivirus protection.

## Collecting technical information
The application automatically generates an anonymous identifier key (Device ID) based on the device configuration (hardware characteristics, OS version). It is used exclusively for:
- Diagnosis of errors in the application;
- Technical support;
- Device ID does not contain any personal data;
- Is not shared with third parties and is not used for advertising or tracking.

## Where is the DeviceID displayed?
The identifier key is indicated on the final application window with the summary log.

## Optional sending of scan reports
On the first run the application offers to send the scan log to the developer; this can be changed at any time in the settings. If sending is allowed, the application transmits the scan log file, Device ID, application version, number of runs and scan statistics (found / neutralized / skipped) to the developer's server. The log file may contain paths to files, names of processes, services and scheduled tasks found on the device. The data is used solely for diagnosing and improving the application, is not shared with third parties and is not used for advertising or tracking.

## Checking for updates
The application periodically requests the current version number from the developer's server and from the official GitHub repository. No personal data is transmitted.

## FAQ:

Question: Why does my antivirus indicate that the software contains a Trojan/virus?
Answer: A third-party antivirus registers the very fact of interference in the system and cannot distinguish a specific action (change), which is what gives rise to such a reaction. It is impossible to clearly indicate that the action (change) being performed is performed for good purposes.

Question: The software closes during operation / freezes / does not start
Answer:
1. Third-party antivirus interferes with normal operation;
2. incorrect startup parameters / path too long / not installed / important components disabled;
3. Malicious software blocks its operation - in this case, you need to rename the executable file "MinerSearch_{version number}.exe" to anything else.
4. There was an error in the code that requires correction. Write about an error in an issue in the official github repository.

This license agreement applies to all copies, and to previous and subsequent versions of the software.
