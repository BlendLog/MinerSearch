# Miner Search

[Русский](README.ru.md) | [English](README.md) | Chinese

这个程序旨在查找和销毁隐藏的矿工。
它是一个辅助工具，用于搜索可疑文件、目录、进程等，并不是一个杀毒软件。

## 现在在Telegram上获取更新消息！
## https://t.me/MinerSearch_blog
## ⬇ ![下载最新版本](https://github.com/BlendLog/MinerSearch/releases/latest)
## 需要.NET Framework 4.7.1

![GitHub Downloads (all assets, latest release)](https://img.shields.io/github/downloads/BlendLog/MinerSearch/latest/total?logoColor=AA00F0&color=Navy)

版本v1.4.7.91

-修复了读取文件和目录时的应用程序崩溃。
-修正了一个视觉错误，发现了不正确的威胁数量
-用于检测可疑文件的改进算法
-改进了第三方网络服务的稳定性处理
-增加了语言切换功能。 创建一种语言。cfg（内容应为EN或RU）
-已删除的Windows Defender限制不再在日志中重复
-增加了--no-check-hosts参数-跳过检查hosts文件
-为快速报告处理添加了标识符密钥（设备ID）
-实现了解锁任务管理器和注册表编辑器的机制
-折叠按钮的正常颜色

-----------------------------------------

## 如何使用

将整个压缩包提取到空文件夹中并运行应用程序。等待扫描完成。完整扫描后，应用程序将显示扫描的总耗时。
重要提示：此时，威胁数量在每个扫描阶段都会显示。您可以在默认生成的C:\\_MinerSearchLogs\\文件夹中查看完整的扫描日志，文件名为MinerSearch_<datetime>.log。

附加命令行参数（通常不需要）：

|启动参数|描述|                                                                                                           
|----------------|-------------|	                                                                                                          
|-h--help|此帮助信息|
|-nl --no-logs | 不要在文本文件中写入日志|
|-nstm --no-scantime | 仅扫描进程|
|-nr --no-runtime|仅静态扫描（恶意软件dirs，文件，注册表项等）|
|-nse --no-services|跳过扫描服务|
|-nss --no-signature-scan|通过签名跳过扫描文件|
|-nrc --no-rootkit-check|跳过检查rootkit存在|
|-nch --no-check-hosts|跳过检查hosts文件|
|-p --pause|清理前暂停|
|-ret --remove-empty-tasks|如果应用程序文件不存在，则从任务计划程序中删除任务|
|-so --scan-only|显示恶意或可疑对象，但什么都不做|
|-fs --full-scan|添加其他整个本地驱动器进行签名扫描|
|-ras --run-as-system|使用系统权限开始扫描（适用于高级用户）|
|-s --select|只扫描选定的文件夹，包括子文件夹|
|-si --silent|在没有任何对话框的情况下启用静音（背景）模式。 应用程序切换到后台模式，不会显示消息，但仍会写入日志文件。 与--select或--winpemode选项不兼容|
|-d= --depth=<number>|其中<number>指定最大搜索深度的数字。 使用示例-d=5（默认为8）|
|-v--verbose|向控制台和日志文件显示更多信息，|
||包括不被认为是恶意文件的行。 它可能会增加日志文件的大小。 |
|-w--winpemode|通过指定不同的驱动器号在WinPE环境中开始扫描|
||（不扫描进程，注册表，防火墙和任务计划程序条目）|

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

--------------------------------------------------------------

## 截图
停止和删除恶意进程和他的支持组件，这使得删除恶意软件更难

![first](https://github.com/user-attachments/assets/29828484-6d57-4e71-ad5c-641913ce34f7)
![second](https://github.com/user-attachments/assets/309e7625-bc57-4b80-9052-4805c33f9486)
