# Miner Search

[Русский](README.ru.md) | [English](README.md) | [Chinese](README.cn.md) | Español

Este programa está diseñado para encontrar y destruir mineros ocultos.
Es una herramienta auxiliar para buscar archivos, directorios, procesos, etc. sospechosos, y NO es un antivirus.

> [!CAUTION]
> ### El antivirus puede tratar erróneamente esta aplicación como malware. Por favor, no crees issues sobre esto.

## ¡Las noticias de actualización ahora están en Telegram!
## https://t.me/MinerSearch_blog
## ⬇ [Descargar la última versión](https://github.com/BlendLog/MinerSearch/releases/latest)
### Se requiere NET Framework 4.8

![GitHub Downloads (all assets, latest release)](https://img.shields.io/github/downloads/BlendLog/MinerSearch/latest/total?logoColor=AA00F0&color=Navy)

> [!CAUTION]
> ### Windows 7 está obsoleto. El desarrollo de MinerSearch para este SO ha sido descontinuado.

Versión v1.4.9.6

- Las amenazas bloqueadas por Windows Defender durante el escaneo ahora se cuentan como neutralizadas y se reportan una sola vez: se muestran con un estado dedicado y ya no se ofrece el aviso de reinicio
- Corregida la visualización del número de amenazas omitidas por el usuario
- Corregida la limpieza de bibliotecas de servicios maliciosas (se eliminan en la primera ejecución)
- Corregida la eliminación de archivos detectados por varias comprobaciones a la vez (las decisiones en conflicto ya no cancelan la eliminación)
- Eliminados los falsos positivos en archivos legítimos cuya ruta contiene el nombre de un directorio malicioso conocido
- Mejorada la detección de rootkits
- Añadido el escaneo de directorios de caché de usuario en busca de archivos ejecutables
- Corregidos los fallos de la consola (redimensionamiento de ventana, salida redirigida)
- Corregido un fallo al escanear reglas de firewall con rutas no estándar
- Acelerado el escaneo (análisis paralelo de archivos, caché de comprobaciones de firmas y de red de procesos)

--------------------------------------------

## Cómo usar

Descomprime completamente el archivo con el programa en una carpeta separada y ejecuta la aplicación. Espera a que finalice el escaneo. Al usar el programa por primera vez, se te ofrece reportar los resultados del escaneo al autor, a tu discreción. Tras finalizar, se mostrará un formulario con un breve informe sobre las amenazas que han sido eliminadas. Puedes ver el registro detallado haciendo clic en el botón "Show Log" (Mostrar registro). Al hacer clic en el botón "Quarantine" (Cuarentena) se abrirá el Administrador de Cuarentena, en el que puedes eliminar completamente un archivo o restaurarlo.

Si se encuentran amenazas desconocidas, se abre la ventana "Threat Review" (Revisión de amenazas). Para cada elemento puedes seleccionar una acción (Cure/Curar, Quarantine/Cuarentena, Delete/Eliminar, Terminate/Terminar, Disable/Deshabilitar, Skip/Omitir) o establecer una única acción para todos los elementos ("Set to all"/Establecer para todos). El botón "Set recommended" (Establecer recomendado) restaura las acciones recomendadas por los analizadores. Si la ventana se cierra sin hacer clic en "Apply" (Aplicar), las acciones recomendadas se aplican tras confirmación. La aceptación automática de decisiones para amenazas desconocidas puede activarse en "Open settings" (Abrir configuración) o con el parámetro -norev.

Al hacer clic en el botón "Open quarantine" (Abrir cuarentena) se abrirá el Administrador de Cuarentena, en el que puedes eliminar completamente o restaurar archivos en cuarentena, tareas programadas, servicios y entradas del registro.

----------------
¿Cómo cambiar el idioma en la aplicación?

1) Crea el archivo language.cfg si aún no existe
2) Ábrelo con cualquier editor de texto
3) Establece tu idioma preferido: RU o EN

----------------

La aplicación también admite parámetros de inicio adicionales (listados a continuación). Para usarlos, debes:
1) Ejecutar la línea de comandos (cmd) como administrador
2) Mantener Shift y hacer clic derecho sobre la aplicación - seleccionar "Copiar como ruta"
3) Pegar la ruta en la línea de comandos y añadir los parámetros necesarios* después de un espacio

Parámetros de inicio adicionales (normalmente no requeridos):

| Opción corta | Opción larga | Descripción |
|:---|:---|:---|
| `-h` | `--help` | Mostrar este mensaje de ayuda |
| `-a` | `--accept-eula` | Aceptar el Acuerdo de Licencia de Usuario Final (EULA) |
| `-nl` | `--no-logs` | No escribir registros en un archivo |
| `-nwmi` | `--no-scan-wmi` | No comprobar la integridad de WMI y/o las suscripciones a eventos |
| `-nr` | `--no-runtime` | No escanear procesos (solo directorios, archivos, claves del registro, etc.) |
| `-nse` | `--no-services` | Omitir el escaneo de servicios |
| `-nst` | `--no-scan-tasks` | Omitir el escaneo de tareas del programador |
| `-nsu` | `--no-scan-users` | Omitir el escaneo de perfiles de usuario |
| `-nss` | `--no-signature-scan` | Omitir el escaneo de firmas de archivos |
| `-nsr` | `--no-scan-registry` | Omitir el escaneo del registro del sistema |
| `-nrc` | `--no-rootkit-check` | No comprobar la presencia de rootkits |
| `-nch` | `--no-check-hosts` | Omitir la comprobación del archivo hosts |
| `-nfw` | `--no-firewall` | Omitir el escaneo de reglas de firewall |
| `-cm` | `--console-mode` | Activar el modo consola sin cuadros de diálogo |
| `-p` | `--pause` | Pausar antes de la limpieza |
| `-ret` | `--remove-empty-tasks` | Eliminar la tarea del Programador de tareas si su archivo de aplicación no existe |
| `-so` | `--scan-only` | Mostrar objetos maliciosos o sospechosos, pero no realizar tratamiento |
| `-fs` | `--full-scan` | Añadir todas las demás unidades locales para el escaneo de firmas |
| `-f` | `--force` | Se usa para suprimir las solicitudes de confirmación de funciones potencialmente peligrosas |
| `-s` | `--select` | Escanear solo el directorio seleccionado, incluidos los subdirectorios |
| `-s=` | `--select= <ruta>` | Igual que `--select (-s)`. Donde `<ruta>` especifica la ruta del directorio a escanear |
| `-si` | `--silent` | Activa el modo silencioso (en segundo plano) sin cuadros de diálogo. La aplicación pasa a modo segundo plano, los mensajes no se muestran, pero siguen escribiéndose en el registro. Incompatible con los parámetros `--select` o `--winpemode`. |
| `-d=` | `--depth=<num>` | Donde `<num>` es el nivel máximo de profundidad de búsqueda. Ejemplo de uso: `-d=5` (por defecto es 8, máximo 16) |
| `-v` | `--verbose` | Muestra información detallada sobre los procesos en la consola y también desactiva el filtro de líneas con archivos no reconocidos como maliciosos. Puede aumentar el tamaño del archivo de registro. |
| `-w` | `--winpemode` | Inicia el escaneo en modo WinPE (sin escanear procesos, registro, reglas de firewall, servicios, tareas del programador) |
| `-q` | `--open-quarantine` | Abrir el administrador de cuarentena |
| `-res=` | `--restore= <lista>` | Restaurar elementos (archivos, tareas, servicios, entradas del registro) de la cuarentena en modo consola (p. ej., `-res= 1,2,3`). Requiere `-f`. Usa `-q -cm` para ver la lista. |
| `-del=` | `--delete= <lista>` | Eliminar elementos de la cuarentena en modo consola (p. ej., `-del= 1,2,3`). Usa `-q -cm` para ver la lista. |
| `-norev` | `--no-review-interact` | Omitir la ventana de Revisión de amenazas; las acciones recomendadas se aplican automáticamente a las amenazas desconocidas |

* No necesariamente en orden estricto
----------------------------

Símbolos en los registros

| Indicador | Descripción |
|-----------|----------|
|    [!] | Advertencia menor |
|   [!!] | Advertencia a la que vale la pena prestar atención |
|  [!!!] | Amenaza detectada |
| [!!!!] | Rootkit detectado |
|  [Reg] | Escaneando clave(s) del registro |
|    [+] | Finalización exitosa de la acción (tratamiento, eliminación, etc.) |
|    [x] | Error |
|  [xxx] | Error crítico: por ejemplo, al ejecutarse en un sandbox |
|    [#] | Estado |
|    [.] | Descripción |
|    [_] | Desbloqueando directorio y eliminando si está vacío |
|    [i] | Información |
|    [$] | Tiempo transcurrido del escaneo |

----------------------------

## Capturas de pantalla
Detener y eliminar procesos maliciosos y sus componentes de soporte, que dificultan la eliminación del malware
![first](https://github.com/user-attachments/assets/29828484-6d57-4e71-ad5c-641913ce34f7)

Formulario de información general
![second](https://github.com/user-attachments/assets/309e7625-bc57-4b80-9052-4805c33f9486)
