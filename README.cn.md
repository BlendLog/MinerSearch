# Miner Search

[Русский](README.ru.md) | [English](README.md) | 中文

本程序旨在查找并销毁隐藏的挖矿程序。
它是一个用于搜索可疑文件、目录、进程等的辅助工具，**不是**杀毒软件。

> [!CAUTION]
> ### 杀毒软件可能会误将此应用视为恶意软件。请勿就此事提交 issue。

## 更新新闻现在在 Telegram 上！
## https://t.me/MinerSearch_blog
## ⬇ ![下载最新版本](https://github.com/BlendLog/MinerSearch/releases/latest)
### 需要 NET Framework 4.7.2

![GitHub Downloads (all assets, latest release)](https://img.shields.io/github/downloads/BlendLog/MinerSearch/latest/total?logoColor=AA00F0&color=Navy)

> [!CAUTION]
> ### Windows 7 已过时。MinerSearch 对该操作系统的开发已停止。

版本 v1.4.9.0

- 新架构：Scanner -> Analyzer -> Handler
- 更新代码库，进行全面审查
- 新增删除新型挖矿程序
- 修复错误删除所有 WMI 订阅的问题
- 修复错误判定威胁类型的问题
- 在报告窗体中新增"类别"列
- 新增选项 --no-scan-wmi (-nwmi)
- 新增选项 --no-scan-users (-nsu)
- 新增选项 --no-scan-registry (-nsr)
- 重新设计签名分析器
--------------------------------------------

## 使用方法

将程序压缩包完全解压到独立文件夹中并运行应用程序。等待扫描完成。首次使用程序时，可选择性地向作者报告扫描结果。扫描完成后，将显示一个包含已消除威胁的简要报告的窗体。点击"打开报告"按钮可查看详细报告。点击"隔离区"按钮将打开隔离区管理器，您可以在其中完全删除文件或恢复文件。

----------------
如何在应用中切换语言？

1) 如果 language.cfg 文件不存在，请创建它
2) 使用任何文本编辑器打开它
3) 设置您偏好的语言：RU 或 EN

----------------

应用程序还支持额外的启动参数（如下所列）。要使用它们，您应该：
1) 以管理员身份运行命令行（cmd）
2) 按住 Shift 并右键点击应用程序 - 选择"复制为路径"
3) 将路径粘贴到命令行中，并在空格后添加所需参数*

额外的启动参数（通常不需要）：

-h     --help                 显示此帮助信息
-a     --accept-eula          接受用户许可协议（EULA）
-nl    --no-logs              不将日志写入文件
-nstm  --no-scantime          仅扫描进程
-nwmi  --no-scan-wmi          不检查 WMI 完整性及/或事件订阅
-nr    --no-runtime           不扫描进程（仅目录、文件、注册表键等）
-nse   --no-services           跳过扫描服务
-nst   --no-scan-tasks        跳过扫描计划任务
-nsu   --no-scan-users        跳过扫描用户配置文件
-nss   --no-signature-scan    跳过文件签名扫描
-nsr   --no-scan-registry     跳过扫描系统注册表
-nrc   --no-rootkit-check     不检查 rootkit 存在
-nch   --no-check-hosts       跳过检查 hosts 文件
-nfw   --no-firewall          跳过扫描防火墙规则
-cm    --console-mode         启用无对话框的控制台模式
-p     --pause                清理前暂停
-ret   --remove-empty-tasks   如果应用程序文件不存在，则从任务计划程序中删除任务
-so    --scan-only            仅显示恶意或可疑对象，不执行处理
-fs    --full-scan            将所有其他本地驱动器加入签名扫描
-f     --force                用于抑制潜在危险功能的确认提示
-s     --select               仅扫描选定的目录，包括子目录
-s=    --select= <路径>       同 --select (-s)。其中 <路径> 指定要扫描的目录路径
-si    --silent               启用无声（后台）模式，无对话框。应用程序进入后台模式，不显示消息，但仍写入日志。与 --select 或 --winpemode 参数不兼容。
-d=    --depth=<num>          其中 <num> 为最大搜索深度级别。用法示例：-d=5（默认为 8）
-v     --verbose              向控制台输出进程的详细信息，并
                              禁用对未被识别为恶意的文件的过滤。可能会增加日志文件大小。
-w     --winpemode            以 WinPE 模式启动扫描
                              （不扫描进程、注册表、防火墙规则、服务、计划任务）
-q     --open-quarantine      打开隔离区管理器
-res   --restore= <列表>      在控制台模式下从隔离区恢复文件（例如 1,2,3）。输入 -q -cm 查看列表。
-del   --delete= <列表>       在控制台模式下从隔离区删除文件（例如 1,2,3）。输入 -q -cm 查看列表。


* 不必严格按顺序
----------------------------

日志中的符号

| 提示 | 描述 |
|-----------|----------|
|    [!] | 轻微警告 |
|   [!!] | 值得注意的警告 |
|  [!!!] | 检测到威胁 |
| [!!!!] | 检测到 rootkit |
|  [Reg] | 扫描注册表项 |
|    [+] | 操作成功完成（处理、删除等） |
|    [x] | 错误 |
|  [xxx] | 严重错误：例如在沙盒中运行时 |
|    [#] | 状态 |
|    [.] | 描述 |
|    [_] | 解锁目录并在为空时删除 |
|    [i] | 信息 |
|    [$] | 扫描耗时 |

----------------------------

## 截图
停止并删除恶意进程及其支持组件，使恶意软件删除更加困难
![first](https://github.com/user-attachments/assets/29828484-6d57-4e71-ad5c-641913ce34f7)

综合信息窗体
![second](https://github.com/user-attachments/assets/309e7625-bc57-4b80-9052-4805c33f9486)
