using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DBase
{
    public static class Drive
    {
        public static string Letter { get; set; }
    }

    // >>> BLENDFUSCATOR:MSKEYS:START
    public static class MSKeys
    {

        // markerSets
        public const string ScriptMarkers = "ScriptMarkers";
        public const string UrlMarkers = "UrlMarkers";
        public const string HtaMarkers = "HtaMarkers";
        public const string FilelessMarkers = "FilelessMarkers";
        public const string ExecCallMarkers = "ExecCallMarkers";
        public const string DownloadCallMarkers = "DownloadCallMarkers";
        public const string EncodedArgMarkers = "EncodedArgMarkers";
        public const string ShellWrapperMarkers = "ShellWrapperMarkers";
        public const string TaskCommandMarkers = "TaskCommandMarkers";
        public const string AutorunChainMarkers = "AutorunChainMarkers";
        public const string RegsvrFlagMarkers = "RegsvrFlagMarkers";
        public const string AutorunWeakMarkers = "AutorunWeakMarkers";
        public const string TempPathMarkers = "TempPathMarkers";
        public const string WmiCommandMarkers = "WmiCommandMarkers";
        public const string RealtekTaskMarkers = "RealtekTaskMarkers";
        public const string FakeDescriptionMarkers = "FakeDescriptionMarkers";
        public const string FakeOriginalNameMarkers = "FakeOriginalNameMarkers";
        public const string FakeProcessNameMarkers = "FakeProcessNameMarkers";
        public const string HelperMarkers = "HelperMarkers";
        public const string FakeTaskLocationMarkers = "FakeTaskLocationMarkers";
        public const string FakeSystemCheckMarkers = "FakeSystemCheckMarkers";
        public const string RegasmArgMarkers = "RegasmArgMarkers";
        public const string IfExistMarkers = "IfExistMarkers";
        public const string UserWritableMarkers = "UserWritableMarkers";
        public const string ConhostArgMarkers = "ConhostArgMarkers";

        // regexPatterns
        public const string EncodedCommand = "EncodedCommand";
        public const string TempDirSegment = "TempDirSegment";
        public const string WsclInTemp = "WsclInTemp";
        public const string RandomServiceName = "RandomServiceName";
        public const string RandomProgramDataPath = "RandomProgramDataPath";
        public const string SddlDenyAce = "SddlDenyAce";
        public const string WinCaches = "WinCaches";
        public const string HtaPath = "HtaPath";
        public const string IfExistPath = "IfExistPath";
        public const string IfExistHex = "IfExistHex";
        public const string IfExistHexPath = "IfExistHexPath";
        public const string PcaluaArgs = "PcaluaArgs";
        public const string GuidFile = "GuidFile";
        public const string GuidAny = "GuidAny";
        public const string ClsidDir = "ClsidDir";
        public const string StartProcessArgs = "StartProcessArgs";
        public const string NodeScript = "NodeScript";
        public const string ArgsSplit = "ArgsSplit";
        public const string AbsolutePath = "AbsolutePath";
        public const string Url = "Url";

        // regValueNames
        public const string AppInitDlls = "AppInitDlls";
        public const string RequireSignedAppInitDlls = "RequireSignedAppInitDlls";
        public const string Debugger = "Debugger";
        public const string GlobalFlag = "GlobalFlag";
        public const string MinimumStackCommit = "MinimumStackCommit";
        public const string MonitorProcess = "MonitorProcess";
        public const string DisableTaskMgr = "DisableTaskMgr";
        public const string DisableRegistryTools = "DisableRegistryTools";
        public const string Userinit = "Userinit";
        public const string WinlogonShell = "WinlogonShell";
        public const string FusClientPath = "FusClientPath";
        public const string AuthenticationPackages = "AuthenticationPackages";
        public const string RunAsPpl = "RunAsPpl";
        public const string ServiceDll = "ServiceDll";
        public const string Parameters = "Parameters";
        public const string Start = "Start";
        public const string Default = "Default";
        public const string InprocServer32 = "InprocServer32";
        public const string DefenderPaths = "DefenderPaths";
        public const string DefenderProcesses = "DefenderProcesses";
        public const string DefenderExtensions = "DefenderExtensions";

        // hostNames
        public const string PowerShell = "PowerShell";
        public const string Pwsh = "Pwsh";
        public const string Rundll32 = "Rundll32";
        public const string Pcalua = "Pcalua";
        public const string Regsvr32 = "Regsvr32";
        public const string Regasm = "Regasm";
        public const string Forfiles = "Forfiles";
        public const string Wscript = "Wscript";
        public const string Msiexec = "Msiexec";
        public const string Msbuild = "Msbuild";
        public const string Conhost = "Conhost";
        public const string NodeExe = "NodeExe";
        public const string CmdExe = "CmdExe";

        // consts
        public const string FwPolicyProgId = "FwPolicyProgId";
        public const string WinDefendService = "WinDefendService";
        public const string TermServiceName = "TermServiceName";
        public const string WmiNamespace = "WmiNamespace";
        public const string WmiQuery = "WmiQuery";
        public const string WmiConsumerClass = "WmiConsumerClass";
        public const string UserinitDefault = "UserinitDefault";
        public const string ExplorerExe = "ExplorerExe";
        public const string ExplorerExePath = "ExplorerExePath";
        public const string Msv10 = "Msv10";
        public const string ServiceCmdStart = "ServiceCmdStart";
        public const string ServiceGuestPattern = "ServiceGuestPattern";
        public const string DcomLaunchArgs = "DcomLaunchArgs";
        public const string SlashCArg = "SlashCArg";
        public const string JarArg = "JarArg";
        public const string ProDirMarker = "ProDirMarker";
        public const string LhsArg = "LhsArg";
        public const string TArg = "TArg";
        public const string VerysilentArg = "VerysilentArg";
        public const string SddlBlockPath = "SddlBlockPath";
        public const string StartProcessCmd = "StartProcessCmd";
        public const string ArgumentListArg = "ArgumentListArg";
        public const string R77_SERVICE_NAME = "$77svc";
        public const string DIALER_SERVICE_NAME = "dialersvc";
    }
    // <<< BLENDFUSCATOR:MSKEYS:END

    public class MSData
    {
        public List<HashedString> hStrings = new List<HashedString>() {
            new HashedString("6319434ad50ad9ec528bc21a6b2e9694",13), //193.228.54.23
			new HashedString("23c807844e8c9c0af34a82cc145b04b2",20), //360totalsecurity.com
			new HashedString("ea2afd439110302922a66cfb1c20c71d",11), //acronis.com
			new HashedString("2f4f102d0800be43f5626e28fc35da35",11), //adaware.com
			new HashedString("47a7fa72bb79489946e964d547b9a70c",9), //add0n.com
			new HashedString("8202ec5cbdc1e645fab61b419c328300",11), //adguard.com
			new HashedString("daa0a654ae3dd4043c4aab6205a613dc",10), //ahnlab.com
			new HashedString("be56cb5de3fd03b65b161145349ae105",13), //AlpineFile.ru
			new HashedString("d96d3881c78c18b33f00d3e366db2714",11), //antiscan.me
			new HashedString("088b09b98efc9213de102758d1c8acea",9), //antiy.net
			new HashedString("5ca9e4a942e008184f0656dc403485b7",7), //any.run
			new HashedString("d82e179187d1268339dcc5fa62fa8b1c",14), //api.github.com
			new HashedString("c593eabe657120a14c5296bad07ba127",11), //app.any.run
			new HashedString("4e5d2e4478cbf65b4411dd6df56c85b7",10), //arcabit.pl
			new HashedString("e7d02464efe5027b4fe29e5b71bff851",12), //ashampoo.com
			new HashedString("178c8b444e8def52807e7db3f63dc26e",9), //avast.com
			new HashedString("116d64b71844e91f9e43dae05dcb6a6c",8), //avast.ru
			new HashedString("f5fe102ec904aad2e20b80dcf40ae54b",8), //avast.ua
			new HashedString("e00662fd56d5e0788bde888b0f2cac70",7), //avg.com
			new HashedString("f3226bd720850e4b8115efc39c2b0fe9",9), //avira.com
			new HashedString("60d2f4fe0275d790764f40abc6734499",9), //baidu.com
			new HashedString("35c18e3f189f93da0de3fc8fad303393",21), //besplatnoprogrammy.ru
			new HashedString("348ccdb280b0c9205f73931c35380b3a",15), //biblprog.org.ua
			new HashedString("1fd952adcdbaade15b584f7e8c7de1e0",15), //bitdefender.com
			new HashedString("5c6cfe5d644fb02b0e1a6ac13172ae6e",8), //bkav.com
			new HashedString("eb401ae50e38bdf97bf98eb67b7f9764",14), //blackberry.com
			new HashedString("10e42be178e1c35c4f0a0ce639f63d44",20), //bleepingcomputer.com
			new HashedString("6d134d427dd6cc0ac506d895e06e5bfa",14), //blog-bridge.ru
			new HashedString("a6891c5c195728b0c75bb10a9d3660db",10), //blog-pc.ru
			new HashedString("d36f9acef58b77c1499fb31b05e1348f",12), //broadcom.com
			new HashedString("b8f3ad2ce16be91986c6ae6c6d2f5c21",13), //bullguard.com
			new HashedString("bcc2393101a857b00a4fbff01da66f2a",12), //bullguard.ru
			new HashedString("2ad4f0c11334e98a56171a2863b3ea7f",12), //ccleaner.com
			new HashedString("bd25a074d01c2eeb74d8563a09f9ebf6",12), //cezurity.com
			new HashedString("cadddd7e2aee1db1c03f630a22f322d9",13), //chomar.com.tr
			new HashedString("56f2deb0bf3c2ac9aa9de23ee968654f",10), //clamav.net
			new HashedString("4876e625e899a84454d98f6322a4d213",15), //cloud.iobit.com
			new HashedString("98eb7e27e19b8816b5ec0a8beffd30aa",20), //cmccybersecurity.com
			new HashedString("a2883d9faa219af692c35404e8c5c05a",19), //codeload.github.com
			new HashedString("00798b05b9906d4031905f9e57f4c310",12), //combofix.org
			new HashedString("26d25247ed88aa5f63d80acf6e4e4d35",10), //comodo.com
			new HashedString("da2ca8ed062a8b78340292df861754b0",17), //company.hauri.net
			new HashedString("132793c4107219b5631e5ccc8a772f94",8), //comss.ru
			new HashedString("a349df20a84c064b688c3605d60dd00e",15), //crowdstrike.com
			new HashedString("a518658356c72fd843116c6358393690",14), //cybereason.com
			new HashedString("4360f8ffd51b17b8bc94745c4a26ef2c",13), //cyberforum.ru
			new HashedString("c652b5220b32e0302487d6bcdc232c9d",9), //cynet.com
			new HashedString("f039b199813ed30f7ce8ecea353ceffc",9), //cyren.com
			new HashedString("ee35efa79cb52086ce2eb70ba69b8405",17), //download.cnet.com
			new HashedString("41080139c830d9d6d2e78c7886d49985",18), //download.drweb.com
			new HashedString("1c6fc893d59bb20742951b0e53d4eba2",17), //download.drweb.ru
			new HashedString("683ca3c4043fb12d3bb49c2470a087ea",26), //download.windowsupdate.com
			new HashedString("84b419681661cc59155b795e0ca7edf9",20), //download-software.ru
			new HashedString("a65eb4af101a55b3e844dc9ccc42f2ff",11), //dpbolvw.net
			new HashedString("782d9e9abc2de8a1a9fdd5f4e41bc977",11), //dropbox.com
			new HashedString("1e0daaee7cb5f7fe6b9ff65f28008e0a",9), //drweb.com
			new HashedString("98d3a8a27234fa519e04907d7ace9ff1",8), //drweb.ru
			new HashedString("de7e2990f9560ce7681d2d704c754169",8), //drweb.ua
			new HashedString("b4de3925f3057e88a76809a1cf25abe5",15), //drweb-cureit.ru
			new HashedString("8931a8fa06b940d45d6a28f2224bc46a",10), //elastic.co
			new HashedString("6ce238acdd804c4f2c710c58efe089fe",12), //emsisoft.com
			new HashedString("e075a44b048b9039c8b3dce7627237ae",11), //escanav.com
			new HashedString("1d954e9393c6a315114850d3f9670158",8), //eset.com
			new HashedString("a6f9bdbd2ced0eba0fe2eb3c98c37778",7), //eset.kz
			new HashedString("927846aba9d1dfedf55ef604067e3397",7), //eset.ru
			new HashedString("44d93a0928689480852de2b3d913a0bf",7), //eset.ua
			new HashedString("2622e56675d064de2719011de10669c7",12), //esetnod32.ru
			new HashedString("56e323a7ffcf8f40321ec950c1c3860f",15), //estsecurity.com
			new HashedString("cb25bfbf5c7435fd7aeda5b62dd29af5",12), //fortinet.com
			new HashedString("0282e441b801ef6fd6712b60b907417c",22), //forum.kasperskyclub.ru
			new HashedString("460049e8266ca5270cf042506cc2e8eb",16), //forum.oszone.net
			new HashedString("78e02266c69940f32b680bd1407f7cfd",26), //free.dataprotection.com.ua
			new HashedString("331fe5de6501de2bb404d9033de1cab1",14), //free.drweb.com
			new HashedString("b8d20b5201f66f17af21dc966c1e15f8",13), //free.drweb.ru
			new HashedString("9bfeda9d06879971756e549d5edb6acd",20), //free-software.com.ua
			new HashedString("867692a785fd911f6ee022bc146bf28c",12), //f-secure.com
			new HashedString("c46cfad9e681cd63c8559ca9ba0c87ce",17), //gdatasoftware.com
			new HashedString("99cd2175108d157588c04758296d1cfc",10), //github.com
			new HashedString("dab7894721da916ee815d3d750db2c33",11), //greatis.com
			new HashedString("393f2e689ee70d10ad62388bf5b7e2ec",14), //gridinsoft.com
			new HashedString("bdef1f72c100741f5c13286c709402fb",14), //grizzly-pro.ru
			new HashedString("50c1347f91a9ccaa37f3661e331b376d",15), //herdprotect.com
			new HashedString("475263d0cb67da5ec1dae1ee7a40a114",13), //hitmanpro.com
			new HashedString("a48072f23988b560b72cf3f2f0eccc30",26), //hitman-pro.ru.uptodown.com
			new HashedString("9fc0b7fa45ef58abd160a353e2d9eb27",15), //home.sophos.com
			new HashedString("eed8bfd826da59536da141d8773a2781",19), //hybrid-analysis.com
			new HashedString("70d0c097b0771196529f00b1559fa78f",18), //ikarussecurity.com
			new HashedString("e159fc485c9c5e905cb570e5a4af489a",10), //intego.com
			new HashedString("62cf04eba08e65b210bd1308f9da04bf",9), //iobit.com
			new HashedString("b06cce9c842342a517eeb979550cb7ef",11), //it-doc.info
			new HashedString("54b260c7fb614cfcf0d2f6e983434db8",15), //k7computing.com
			new HashedString("250730bdbc2a6fc2a7ffd3229d407862",12), //k7-russia.ru
			new HashedString("6f0c9e8027ef9720f9caedaef4e200b5",13), //kaspersky.com
			new HashedString("4bb1cae5c94216ccc7e666d60db2fa40",12), //kaspersky.ru
			new HashedString("675c52a56f2ff1b3a689c278778f149c",21), //kaspersky-security.ru
			new HashedString("6dcb7e266b7f70c55d8ad51ef995cbc9",10), //kerish.org
			new HashedString("6e7bf33d4e222ddb5ae026d0cd07754a",10), //krutor.org
			new HashedString("15fe7ae3216c7a37d34d02793d180530",9), //ksyun.com
			new HashedString("762c7e2ec87cb7de793cde9e9543734a",10), //lionic.com
			new HashedString("bd7c714d46ff9bae1bd9918476e8450c",10), //malware.lu
			new HashedString("e2f0354cd055ee727d5359ceb3ec59ad",16), //malwarebytes.com
			new HashedString("4f8a9bbdec4e2de5f6af2d8375f78b47",41), //malwarebytes-anti-malware.ru.uptodown.com
			new HashedString("327d0b3a0bb1c17c52f6ae1af8867bac",12), //malwares.com
			new HashedString("b2c9a135e92a3d4d0bded64ffe4d1ee3",15), //maxpcsecure.com
			new HashedString("985983ba88d92782fc97526ab0f02cd0",10), //mcafee.com
			new HashedString("79782f8d4349fc66dad89c3765b761d3",23), //metadefender.opswat.com
			new HashedString("3afb052104deb95bc99eee868c8644f8",18), //msch3295connect.ru
			new HashedString("974bf1d93d81d915800bb2e5352b923e",39), //msnbot-65-52-108-33.search.msn.comments
			new HashedString("f6ce7e3db235723091e59a653e7d96f2",9), //mywot.com
			new HashedString("4a73bdc9cec00bbb9f05bc79cbc130b4",9), //mzrst.com
			new HashedString("3d62ee7e9bada438b991f23890747534",9), //nanoav.ru
			new HashedString("b0655a2541be60f6b00841fdcba1a2df",10), //nashnet.ua
			new HashedString("13805dd1b3a52b30ab43114c184dc266",13), //nnm-club.name
			new HashedString("4e42a4a95cf99a3d088efba6f84068c4",10), //norton.com
			new HashedString("84eac61e5ebc87c23550d11bce7cab5d",17), //novirusthanks.org
			new HashedString("b1cf94483ae1298267da65475b6f8d53",21), //opentip.kaspersky.com
			new HashedString("63b4a8681bf273da7096261abcb33657",10), //opswat.com
			new HashedString("61d4dd297f749e3291ed8ae744da57de",20), //paloaltonetworks.com
			new HashedString("8d39a2f3831595b02640c90888c21fdd",17), //pandasecurity.com
			new HashedString("da876e79f6730f35c4678969c5b01b3f",12), //pc-helpp.com
			new HashedString("771170bbbfd44a8b1843d3fad96daf1b",11), //pcmatic.com
			new HashedString("33ae33718baa80a5f94b014fccb7329b",13), //pcprotect.com
			new HashedString("2703a4c1ceef44c10ac28f44eb98215d",10), //phrozen.io
			new HashedString("8dde0f8215149ce5ecfd670c4a701a9b",9), //pro32.com
			new HashedString("16297e8f3088fa3ff1587f1078f070ce",23), //ProgramDownloadFree.com
			new HashedString("3469d5aaf70576a92d44eff48cbf9197",13), //programki.net
			new HashedString("4c255dbc36416840ad9be3d9745b2b16",15), //programy.com.ua
			new HashedString("f92bfb8ff6ac7e99a799f6017797684b",13), //quickheal.com
			new HashedString("cde54506e8fa4d94c347eb3bf1a4e761",11), //quttera.com
			new HashedString("af0bbbc42533596b884c3b6edcdd97c9",10), //raymond.cc
			new HashedString("545f4178fd14d0a0fdacc18b68ac6a59",18), //regist.safezone.cc
			new HashedString("8854c43b5f132f9bbe9aa01e034e47fd",14), //remontcompa.ru
			new HashedString("6cbd967e469ea6671e3697f53f577e59",12), //remontka.pro
			new HashedString("98fc92e32c31aa34dfefa97494381324",9), //render.ru
			new HashedString("680bd6136c83f4eb31b16c1fdd7aa93b",17), //reversinglabs.com
			new HashedString("2e7596c6145efe2454e4d6b92c8c4620",10), //rising.com
			new HashedString("725161e698d806fcce316bcd70b2fce1",17), //rising-global.com
			new HashedString("0de8be0d7a0aba151cd4821e4d2e26de",10), //rsload.net
			new HashedString("1a4cdbb224bdad3d9f51781dab21a71b",13), //ru.comodo.com
			new HashedString("0ddbed1cc866ed859f65164648955a59",13), //ru.norton.com
			new HashedString("ebc7dba99115781ed43090a07f9281ab",14), //ru.vessoft.com
			new HashedString("09cf5cb0e321ef92ba384fddf03b215b",11), //safezone.cc
			new HashedString("626575b255ca41a9b3e7e38b229e49c7",11), //safezone.ua
			new HashedString("02cb97db53e82fecc3b47f2a7ab3c6ad",11), //sangfor.com
			new HashedString("c8324a9e380379bd3e560c4a792f76de",13), //scanguard.com
			new HashedString("41d4831c0d31069bc5b8ac767612316f",17), //scanner.virus.org
			new HashedString("2db7246eb9be6b7d7f7987a70144d8dc",13), //secureage.com
			new HashedString("5bfe94657da859c24293b4e35810ee29",26), //securitycloud.symantec.com
			new HashedString("7c07ca598d80ba314295db647b40bc16",14), //securos.org.ua
			new HashedString("6c366a99be85761e88558f342a61b2c4",12), //securrity.ru
			new HashedString("87a25244757ea3a30d936b1a9f4adb93",15), //sentinelone.com
			new HashedString("fc828fa4ff498f2738556e6c446bb98a",18), //site.anti-virus.by
			new HashedString("05461be81ef7d88fc01dbfad50a40c53",14), //soft.mydiv.net
			new HashedString("a71c27fdffca5d79cf721528e221d25a",15), //soft.oszone.net
			new HashedString("0f93e1b1f0c1954c307f1e0e6462a8ce",13), //softdroid.net
			new HashedString("e752141e6b76cf60e0bf9f850654d46b",12), //soft-file.ru
			new HashedString("820c5a952f7877246c895c5253017642",15), //softlist.com.ua
			new HashedString("ef628e261e007380ba780ddca4bf7510",13), //softobase.com
			new HashedString("133dbe014f37d266a7863415cec81a4f",13), //softpacket.ru
			new HashedString("a0f591c108d182f52a406fb1329c9322",14), //softportal.com
			new HashedString("ec532f0313071cb7d33bf21781ec751f",10), //sophos.com
			new HashedString("5641840b2116c66124c1b59a15f32189",15), //spamfighter.com
			new HashedString("39cf9beb22c318b315fad9d0d5caa105",13), //spec-komp.com
			new HashedString("e56f530f736bcb360515f71ab7b0a391",14), //spyware-ru.com
			new HashedString("861cd2c94ae7af5a4534abc999d9169f",13), //stopzilla.com
			new HashedString("90711c695c197049eb736afec84e9ff4",20), //superantispyware.com
			new HashedString("3827d198701ae4f8670b7e0721711460",21), //support.kaspersky.com
			new HashedString("5fb3419335f5e5131ab3fc22d06ad195",20), //support.kaspersky.ru
			new HashedString("e862d898315ed4b4a49deede1f672fde",13), //surfshark.com
			new HashedString("25da26174f6be2837b64ec23f3db589b",14), //tachyonlab.com
			new HashedString("774f38701dff27e1d5083998b428efd6",11), //tehtris.com
			new HashedString("d58a810afab3591cf1450a8197219cc4",11), //tencent.com
			new HashedString("ca867bc71a7ba4529a2d3a9991d54511",9), //tgsoft.it
			new HashedString("64003943175e5f080c849f1744819f48",16), //totaladblock.com
			new HashedString("61cfcb40977412be2ebf5450f4e47d30",11), //totalav.com
			new HashedString("804669ae15f338250ec9e3bd00ef5038",16), //totaldefense.com
			new HashedString("c98e096681a2d1d30b321ca4682adb47",12), //trapmine.com
			new HashedString("1826c35007829d3483ffd18cfcabe01a",11), //trellix.com
			new HashedString("2cf505233a066a02292a1f9062aa12a2",14), //trendmicro.com
			new HashedString("976e17b152cabf43472b3ffd81113c66",13), //trustlook.com
			new HashedString("2c9bfb7c724df7cdc6653c1b3c05dede",12), //unhackme.com
			new HashedString("fca37d5298253d278429075543d8f47d",24), //unhackme.en.softonic.com
			new HashedString("eeded1a700eaa95a14fccb1d0b710d76",11), //unhackme.ru
			new HashedString("b56ffe783724d331b052305b9cef2359",24), //unhackme.ru.uptodown.com
			new HashedString("47f7ff2b74fdf4ad0e8e3c5f16fcd04a",13), //us.norton.com
			new HashedString("0d3630958f3c3e8e08486b0d8335aea6",17), //usa.kaspersky.com
			new HashedString("41115f938d9471e588c43523ba7fb360",10), //vellisa.ru
			new HashedString("e1312360d9da76cde574fdf39ff4ec60",9), //vgrom.com
			new HashedString("9c41eb8b8cd2c93c2782ead39aa4fb70",9), //vipre.com
			new HashedString("f27e6596102c70bad8aa36e7c9b50340",11), //virscan.org
			new HashedString("17baee242e6527a5f59aa06e26841eae",9), //virus.org
			new HashedString("83b6a29ee489bf3e976824b763c212e9",14), //virusinfo.info
			new HashedString("b6eb1940800729f89307db6162706c21",19), //virusscan.jotti.org
			new HashedString("e2a50e6c79e09a7356e07d0476dfbb9b",14), //virustotal.com
			new HashedString("4098c777fa8b87f90df7492fd361d54d",9), //vmray.com
			new HashedString("7d2500fc0c1b67428aac870cad7e5834",12), //vms.drweb.ru
			new HashedString("3ba8af7964d9a010f9f6c60381698ec5",11), //webroot.com
			new HashedString("6c1e4b893bda58da0e9ef2d6d85ac34f",18), //wustat.windows.com
			new HashedString("f360d4a971574eca32732b1f2b55f437",11), //xcitium.com
			new HashedString("05dfd988ff6658197a53a559d03d48d5",7), //yadi.su
			new HashedString("2b001a98c1a66626944954ee5522718b",10), //Zillya.com
			new HashedString("686f4ba84015e8950f4aed794934ed11",10), //zillya.com
			new HashedString("34c51c2dd1fa286e2665ed157dec0601",9), //zillya.ua
			new HashedString("9a397c822a900606c2eb4b42c353499f",10), //z-oleg.com
			new HashedString("80d01ead54a1384e56f5d34c80b33575",13), //zonealarm.com
			new HashedString("b868b32c3ea132d50bd673545e3f3403",18), //zonerantivirus.com
		};

        public List<string> badApplockerRules = new List<string>()
            {
                "046f9638-b658-43ee-97f8-e15031db0b6f",
                "0cfc12f8-7909-4835-90dd-68d33e7f0f10",
                "10635fa4-7a5b-425d-838b-689f9b246807",
                "17034547-0c43-4381-b97a-ce8a2d5e96f8",
                "36bced03-d5ef-47fa-a598-a6693a3bc59f",
                "3fb8bf6b-9eed-456b-94e4-00022745779e",
                "443594ac-609b-4dd7-816d-f4f1e3efc726",
                "489640ba-736f-4381-9b78-b11b5fa07fea",
                "5766b2e3-7cad-4f73-9c67-762db4f8d63a",
                "5c158d85-7483-455d-8f96-a1888217e308",
                "6a0278ea-9b21-4c53-a18c-a0e6411ea624",
                "701deaa1-2dad-4f95-a15a-1aa778b4b812",
                "71e498b6-68f4-4c4c-9831-b37fa2483e24",
                "72b5c9be-1cf7-43eb-af80-63feaf6bb690",
                "7b63de66-5456-46bc-9a2a-2fe7a84cd763",
                "7fde4b58-4627-49c7-baef-4a881d3ef94c",
                "808be0f0-b8ab-46c7-a3a0-bdeb742ccde9",
                "839d18ed-9e08-492b-bfca-4a53c1e7c8c4",
                "85a18717-d5f9-4f3b-89b4-1ed4f02b1eeb",
                "8c9ead7d-b294-4159-9607-9b9b7766f860",
                "8e27ae66-7447-4de5-8759-475393f09764",
                "93b1f30a-51e3-4582-a3e0-582d1ba1987d",
                "97e69d73-af4e-4d3b-93c0-de2d00492518",
                "9cfdfc36-6bd5-4b9c-baf1-56ba7df44ec6",
                "a395fe35-b771-44e1-b640-8877314b2643",
                "a439a434-146a-4c9f-8743-051f522f36bb",
                "adb6a6f1-9af9-496f-b8d4-ba695911f83a",
                "af801e3f-3fa4-4910-b559-b9c956783ee5",
                "b1a2abe0-68e5-4632-866f-2c6215dec459",
                "baac2a1e-8890-4bad-998a-c11534e1b44d",
                "bae342c0-8b15-4823-80a8-fe5067a75f90",
                "be235b32-21ab-4dd8-bc6e-61649ec11f3d",
                "c1abb5ee-85f8-47dd-b567-cfbe3ea51516",
                "c2d49146-e267-4fe6-9867-b2d42fdf52e2",
                "c888e849-8015-4f41-b2a2-d18e4c6bf02c",
                "ca90426a-78be-4a8b-af20-d13452175d73",
                "cb5f59ee-d2be-4d9d-99dc-7657843cece2",
                "d16c6ab4-3721-4e52-9902-64e76212094c",
                "d8e659be-d4a5-4cd6-bf96-c92736039685",
                "d8ee32c1-472b-41dd-a204-b198cb1ae9b8",
                "e8a3f75c-ee02-4c96-958e-7e31352c196c",
                "ea9fa9c5-2743-44a1-99ed-d9ac26a135e7",
                "ec544bd8-4a5d-4ae7-8c5c-044f4b6d60fb",
                "ec77c5b9-3955-44f4-804b-c678504c16b6",
                "eedeed7f-e2e7-4181-8050-4a4f90361328",
                "f025c3b3-d9d1-4c09-be3b-bfc05fdbe243",
                "f2be1651-b3c6-477d-a183-8f2946538210",
                "f9729781-9d66-46b8-8553-f0099fd924d3",
                "f9b3908f-4f58-45ec-a9a8-c1b88e9dbe98",
            };

	public List<string> DisallowRunIgnoreList = new List<string>() {
		Bfs.Create("Y6nhqGiEOuzi3CeTP7QloDHJNClKx5Yr40IVEGQHC4I=", "MZMjas+F4VAugGkSy4Z7HLuXKYcL+oG8GnkRbectpDo=", "FXZB+Vun3htMJcBYOKtwLw=="), //CompatTelRunner.exe
		Bfs.Create("KwfnN9G4dRUKDZgJXok1Ec3HVFvdiIpTpjBovMG5s/s=", "BSKu9VKsXJqnPjRAtev6Zt+XqV2FH8yHZV6qrRdu2bQ=", "ecoMtgTn3m0eJ4W84fXvfw=="), //DeviceCensus.exe
		Bfs.Create("rvUyTdAVh7uhctGxfhTqwQ==", "fQ1DQ+gLFYrBKTOcbI9Jt9VSTL+S/gI3MR5Jat+qlKY=", "YIjC0VEY7zmF13JnBGJ5wA=="), //MpDlpCmd.exe
		Bfs.Create("Pt1CKn5yo2HIcDlUQI3Rsg==", "hosXkHN+ZN1oU5FmN6i/NIOlPcefahrqIwMcvf/dpPo=", "DXsiLFeCED4kZa72vZusBw=="), //upfc.exe
		Bfs.Create("Gob0BLmYyPUvvTU6dbDQ1Ar6VHtigRfq8QJqO87niNQ=", "5z655NT0Onv+87FOBPPGny5vzxlVpkHfDEftmvOWBSg=", "8BKJfNhDyn5k/Nigp/yMKQ=="), //software_reporter_tool.exe
		Bfs.Create("+ew7Vn+BXcsrTesRAWtHQeV60R+Qt8NFj4tpLv+k0k0=", "dIfS/bVHr89RNBXbqE2phmY5dxk+Q1TwBnHcQKo6ONQ=", "i74A+C3w1eK4228cjk9LTQ=="), //SecurityHealthSystray.exe
		Bfs.Create("jAci1KAxtSxz9zPjQWcDMfrXL5dtKdXJ3XAM1+Z79Uk=", "qDV3BhdqiMKuj2nTR8NnB735jG1xZWiflP2lkVllmvQ=", "q5zjWptJPCLKK/cOcJqQFQ=="), //MicrosoftEdgeUpdate.exe
		};


        //@"\\?\"
	public List<string> obfStr1 = new List<string>() {
		@"\\?\" + Drive.Letter + Bfs.Create("spT6QzokpkStONw5GGtoctejIptPvC+Zd6LF5cpoo34=", "Mn8CfyVykMialFZd0COASmXADWpYVI2J9rDX6iD5UKs=", "bI0lSssLL3Ke/5bEgwuweg=="), //:\ProgramData\Install
		@"\\?\" + Drive.Letter + Bfs.Create("1HBov/Y41quZpClXfOsibdEHsEQnCOJs75cpv4wHNAI=", "C/FNtlH5Nyuq+yZz+7cyRL6/0R6AkTuRXI+E6twBZN4=", "+3fen83eOmh7jU6cym4fEA=="), //:\ProgramData\$pwnKernelSystem
		@"\\?\" + Drive.Letter + Bfs.Create("6tsZ/myoK0jObyisEO6QNBEk5KFqgQucd4M6Migv5jQ=", "nD564dwFond+oq6N6gO7DuqdZ5ottOTCPZiXu5GeUvc=", "UgVRfcfEQVVnez7tmfSm9w=="), //:\ProgramData\Microsoft\Check
		@"\\?\" + Drive.Letter + Bfs.Create("uwrNsSxTG6h10SzD50LxtSFIMKfM6CpilPbHe219cVs=", "tFRU5IqRXOEv/zUMsBWu/FKkLee5aGpkLj8rRlWNtc4=", "TtJgs91tq9m5GWKtKq2XrA=="), //:\ProgramData\Microsoft\Intel
		@"\\?\" + Drive.Letter + Bfs.Create("/2d7nFiQhHGd403QM6doJXg9n1dgetGml4tCP77kCzYnd8hyOp/vTQo1nYjfKL743WHzxF4tyqG7QTsLaARpwQ==", "c0Vc7sYjqIjn2EWxtMpEj0XQ4jR5tqmw6Kes4Xx3pNs=", "owfqzhiVsB0oldL8jRj5AA=="), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
		@"\\?\" + Drive.Letter + Bfs.Create("omRPkXJW6KHe3vfv4yRkCXufk///nAjAJB8S8sjcyUE=", "JsdXPn6YT+NDMVRD4Klws+cF6+r4ZXhP5TTtFxSeEcY=", "eOtBp3oqepbP9crp9SAHVg=="), //:\ProgramData\Microsoft\temp
		@"\\?\" + Drive.Letter + Bfs.Create("03DplP2NcGthPsOC/Xgdz0w9YekjU5m52Llvd+3HG4g=", "JRfOufnK3niKVm26OQnP7V8ROWxWLUDBx0uMrIC3ZRQ=", "+Cj69ZujoY5wg7aNHQBgmw=="), //:\ProgramData\PuzzleMedia
		@"\\?\" + Drive.Letter + Bfs.Create("YwlEl1UTz3TrDkYafSLw3pjbu/giKpJNrNPEjSF+jaA=", "CCho4fLfThBO/fUDafbCql1OlLRKznTFrRPg7GnUhU8=", "avn7v0CzXFrZYMUVmQwWCQ=="), //:\ProgramData\RealtekHD
		@"\\?\" + Drive.Letter + Bfs.Create("prpAUQRs6WFC847devvjC5T2Ce8/YlkbobHIqPnFoJA=", "JVc6ZYvB7EqqGul69dcwIbP6JcTmvUv6RYjXaUYUmVQ=", "BgrIDNFqQUvmlXJYZf45Aw=="), //:\ProgramData\ReaItekHD
		@"\\?\" + Drive.Letter + Bfs.Create("5rQunCqSuewUwUk3bn1rc97wtv/eD0Qyf+rteZfG3xU=", "jM+MxXEVoez0uVr8hMtWag3rWZCVPLD7j5K2uTaQNqI=", "eT5EYDxfxdl+GRy9lXsGtg=="), //:\ProgramData\RobotDemo
		@"\\?\" + Drive.Letter + Bfs.Create("zKCgKzwku9bdHAtkmUQxK1bqxmRCuKdJXNiiOMV0nmc=", "2kHeKota+/jIBR4beVqh7A6Fu+3W5NBoOf4og854lJ4=", "lUnzwj82oWvxwaY6MSl5Iw=="), //:\ProgramData\RunDLL
		@"\\?\" + Drive.Letter + Bfs.Create("ZkgnsGo0ib/1AcZp4S5BxZvQHm8oIV2xXAy415vKaJA=", "h16pct9+1/7gS8iqgc5C7W7pmATWqrXsBRijlxXV7zY=", "PZR6qr8Qb71+BwsBg/q/Ew=="), //:\ProgramData\Setup
		@"\\?\" + Drive.Letter + Bfs.Create("BcYeUvmEsLZQ2xqnrQY9f6WqAWac6AyjjUMDrctZCCo=", "WGlehZPxRYsTIYHUEvWEE0r67ALwod0/hcfwaRQR/zA=", "E6g/Nwmeny96elVri8lzeQ=="), //:\ProgramData\System32
		@"\\?\" + Drive.Letter + Bfs.Create("26MOK/DroS3cpOpWBQxtgSNcmwtSOJLmJ7fVSTOQzpM=", "0N9389TcPJCwZBaAHzAIn6jpgNiozqi2dTVxOxy22vI=", "A6LMWTyHEiEzkyMdkm/OFg=="), //:\ProgramData\WavePad
		@"\\?\" + Drive.Letter + Bfs.Create("nkORNfXq87Hrsy5RjdeFpHUnFT5tUJvr+Ov6dVSDNgUA1gN/NoCOlim0hbcgtVn6", "Q/XXVQQNBw6CtiVp7hXL+WVsjTttbwqkmakKpUR6s8U=", "YABsVWt3RJMCWgVNe+csQA=="), //:\ProgramData\Windows Tasks Service
		@"\\?\" + Drive.Letter + Bfs.Create("dPd4JDuAssSkS3J5OdYvO4KNnFnrIM3lMUbWH/FljLc=", "D09LsdRyFjMQHp/VYn1tz1x7X2+8x8O58OVgAuLw5KE=", "KVXTBdrsz1o4BStDc3M1Fg=="), //:\ProgramData\WindowsTask
		@"\\?\" + Drive.Letter + Bfs.Create("CuexNokmdPiec3V69ihSJUhmnOzgzvafIbZoZffhBG8=", "HSK/egQBtWu986M3GJ7RbsZCpnOqhiBpDcSyG32jvE0=", "+CLK/vhj4pWGPqvlDuhvyw=="), //:\ProgramData\Google\Chrome
		@"\\?\" + Drive.Letter + Bfs.Create("Su/gshNm4fpTtSJckMEelnAIhUXF0RPTw/N+MtUoQS5gC57UWFiJ9J9VMBy5uCT1", "yybP5onT5M0ZRpxQnUcCSaKNAw4hXawQSU6s0ZmdgfA=", "Z0uSE8R8zfSM/kET3IbLBQ=="), //:\ProgramData\DiagnosisSync\current
		@"\\?\" + Drive.Letter + Bfs.Create("1JxiD/G4YYK2DZW9EwpPonN5/wGg6bk047R54UOZ7JQ=", "e1qR/L1+i0/YcfZvlTmV1iY8DAV7VX3PYdEGN52Q+gg=", "Its7N4kRCNGQHp55zyok6Q=="), //:\Program Files\Google\Libs
		@"\\?\" + Drive.Letter + Bfs.Create("8+oSqkLe5AZ4+27axn2WM7h9GqyDsqcQgM9rwp+wXa5sMpCx2oMexqw01+12bxFC", "U630cxWAR5OHLF0MDwG0QBtE8sa5TW/FG7Ee4Jb2nAU=", "3N1QS7RCsOjsLh6fKgiuZA=="), //:\Program Files (x86)\Windows VC\
		@"\\?\" + Drive.Letter + Bfs.Create("XUTtPL3P+S9b1K0h//XV3dy8d/SYkJwQVXBII4rvQso=", "NyHrE8DA52PuJK04CBEE058h3fQCkSF5jAReYSHC/6Y=", "2cchLp9Q1tsjji+SlwsprA=="), //:\Windows\Fonts\Mysql
		@"\\?\" + Drive.Letter + Bfs.Create("9rZYd43R/09rbTd4bc/yjBnX1O3M8f2k0kNgXOsXfWvjvyXlJh9nMxmaN4fWCpJD", "puVVcbmp7IYLQhftWEZiqgZ9IgzxG+Lt7vuT70MAluw=", "p8oELVuZem/m1PPB8XM/Sg=="), //:\Program Files\Internet Explorer\bin
		@"\\?\" + Drive.Letter + Bfs.Create("UYH/+6l1rT1iagUOQIRGlSLTxMy30cocZ691zdBjDwA=", "zEK678J85Db00mSJXLgbOeidu90izPs/UOVhmPnOJXY=", "ab+k13qEii3F5WMfaE0psQ=="), //:\ProgramData\princeton-produce
		@"\\?\" + Drive.Letter + Bfs.Create("QvjHdcaxhERZBvMnKnHPNZzRRfAOCXtUW4xrKB1NVKk=", "BV7GArFy5MiEasI3XJ73zCLOoEaiQIkZcAO9+HVWOhY=", "mUVidRQ3wTbktKKhNFkmGw=="), //:\ProgramData\Timeupper
		@"\\?\" + Drive.Letter + Bfs.Create("iv1/Ph2zsjEjIDqNEkAOltfNGUEvpAiCzdvJDNHmPhE=", "zqb2QeRG1XIx3OdQrWYC0sdPOuocUrn7IQDd1Ym9/mo=", "/VdeKNaZH4svwowKR0/Puw=="), //:\Program Files\Client Helper
		@"\\?\" + Drive.Letter + Bfs.Create("PT2sTvkVP6TrYPAe9nfqRuna1N4VNL/c4cibKIj0l8I=", "y3nWv7mALLKQFdX0LJHjbjpiYSXKwWnrwoHw3ggZQ/s=", "fVlrHY5i+76AD0RDXn448Q=="), //:\Program Files\qBittorrentPro
		@"\\?\" + Drive.Letter + Bfs.Create("Az7XUbK/i5Sy16Jo+dsnEwSp0AO3iXJlGoJ39dPo8d8=", "LxpCkGJwUyrnCKWephOyht/LUha2rNMaZPZTmQpRqXw=", "9KIRHi/fsKy2EZ1oLE5L+w=="), //:\ProgramData\AUX..
		@"\\?\" + Drive.Letter + Bfs.Create("Oj/Apu6cLxfsI0e3GHZef9L1+o4Crf89/DfP//+16tM=", "OljA0T9DYi2f/oPqCSbpwaPUe3AZ14nJp63anIXyV3k=", "J6QC4jskFI4m1R2wG6qPpw=="), //:\ProgramData\NUL..
		@"\\?\" + Drive.Letter + Bfs.Create("+0RelL+vciXlE7KQ3Rs/g8Tgh6eeSDQnKO7/FBW2uvY=", "nMGuR3xpHQWIOZZ4h0yBEg6TnwvnKhDaOP5vaV5FVQ4=", "BQWqVyxjxb+zPP0fNs5rbQ=="), //:\ProgramData\CON..
		@"\\?\" + Drive.Letter + Bfs.Create("ThxVPj3d2yzazyIUIc24dNO4MDClmDq8u2JORFtNias=", "vh/zom8+PAsGx0zLnaLbzjBNHVIM8+4uwg9cnya+KZA=", "RoBkUpz8m3quOeCtVqVZcA=="), //:\ProgramData\config..
		@"\\?\" + Drive.Letter + Bfs.Create("LabjVV9dVDGhSDBrfcFcMLDToft2e77NH06Ndy8uEN0=", "B8QSugyRfKo4gwJDXn9+B95tNtvtj1H4Vy4sxvpmTIA=", "wtWXBIGsvMdXQfNC2FAIog=="), //:\ProgramData\logdata..
		@"\\?\" + Drive.Letter + Bfs.Create("KxVvcLnBd2+RJhK0HqM3Xx2pUXe8aO5KGfOQAq2NeVY=", "+l7Sx+nauMTlTnIYjIK82M8f9OoIrvtpKeI97sf7Aio=", "AMfrYHleXmJa26rdDO7P6w=="), //:\ProgramData\Jedist
		@"\\?\" + Drive.Letter + Bfs.Create("j6906vNW39D1nP2HqMZx/dQMla+oW9p8QBx8AyxFo7+QWIUMJEYOkphcBXJi0qUPJTlydsml+Ax/r9OXX3a7UA==", "Wi9CmdinGwN/VtxyS1TDTleV8b9Zz+n0FsZN/OHyN1Y=", "obvuLhsLAK0O9+rDTwpdog=="), //:\ProgramData\Classic.{BB06C0E4-D293-4f75-8A90-CB05B6477EEE}
		@"\\?\" + Drive.Letter + Bfs.Create("vMzmgLoRBQIfmnA3j2DUyWgnfvSN96zu/cOteoldY7DMIoj0JSNcL6/a8+YGwkg/goepII0e4LgHSbtYi/C8iQ==", "ain1agXkUgph5RGkogv5DsDPMDMHX4jGSyppbEQcJa4=", "ZzksOhez3v1ytXyh/jEdzg=="), //:\ProgramData\Classic.{BB64F8A7-BEE7-4E1A-AB8D-7D8273F7FDB6}
		@"\\?\" + Drive.Letter + Bfs.Create("Djari7YnniExavTBCsO863iwEwnHWSLM2Y0R18NpRos=", "311HrRb0DdhxpGiPk3ZSYXJmJTxFPZtQL08ahwsekfo=", "/EHHnDhIS1OojoXaEak/SQ=="), //:\ProgramData\Gedist
		@"\\?\" + Drive.Letter + Bfs.Create("t5lLVuCQ2aLEr7nHg1ZJm/FbhjyDN7ZH40irEAp1qxw=", "QwJnoPg4viMu/in/phKgUCCRWgy80w31S3LSuk++ZMo=", "FTJg8mNQJ/9HV/fEHor9rQ=="), //:\ProgramData\Vedist
		@"\\?\" + Drive.Letter + Bfs.Create("tqmILIr4R8Va8k9CwRc3V0y/v2unJPBkeanDid98Txw=", "8Gi8j/H1iYsznhwYcQtk+bUqCR2CksqRNNu2zvCUvTU=", "liIQYrmXi8yQTFyHnJogKA=="), //:\ProgramData\WindowsDefender
		@"\\?\" + Drive.Letter + Bfs.Create("bqM1e8noTorRI1XiGR9f5TWr1rSuVncsQ1U8vs7WayILjXE5Ei8zDFPT6l4yuc9k", "DmUM8pCyz7+xTewCJKcM1xnZXei2WNvjIi+/JOUv3lk=", "iN0ABMMK010q+6cHptm+Qw=="), //:\ProgramData\MicrosoftDefenderA
		@"\\?\" + Drive.Letter + Bfs.Create("LJqxrr5kXYZkFyj/7pCTK0iuMkNHIImHBWr+o0R76C4=", "zKZYXFPXEmfQ9ixFaCDLgwu5kb5UxEWKv5db9vY9YJY=", "3oKYeHMb24XeqrpVkw5M1A=="), //:\ProgramData\WindowsServices
		@"\\?\" + Drive.Letter + Bfs.Create("4nGPp5Nn6CG7nK+jasws1j4PaCHgx1EnUsYe6F2ps/Vgi08H5w/GTV5sNVvKwn5p", "5/id/zBJEaVArBi7TN2KXgRWEaqWfnITRAcLbqpo1Qs=", "WPBoZIBFxzTa8HNBLnt9dg=="), //:\Users\Public\Libraries\AMD\opencl
		@"\\?\" + Drive.Letter + Bfs.Create("waiC4sNgHojcmhDmgN4cqZ+3tSas3EEzG4kdyJtFVoNUDyGAPoDD4xdCfCHudR00", "J/2VxOQIr5IAdq9BBo2GjCfC5TAk7VMRzoDR8tr3vUc=", "sHgXki/F4CYSBoPZR/XAGg=="), //:\Users\Public\Libraries\directx
		@"\\?\" + Drive.Letter + Bfs.Create("OFfSd3inEYIqzkCaqkQehZtCvQVW/7SPQd1PQietsVQ=", "Z4il8XNscv35Z0MGbQrTSxWjkj4wCdYjLVn1uP8KgKE=", "JcMiZghY5uNKdsEzrg0aog=="), //:\ProgramData\DirectX\graphics
		@"\\?\" + Drive.Letter + Bfs.Create("6z3UYd+kVMZDhl9bxA3+FUkmIEjg1ynnAVsp+USJQWs=", "fVvuWcR1MXhZJELdeXy1IWDjF0g3X1z4XBqYGR2fhJ8=", "eX3tiBtMlHuuwQK5o/rgow=="), //:\ProgramData\SystemUpdate
		@"\\?\" + Drive.Letter + Bfs.Create("qGvMQbQLR0tqFHrghuIt/OP0n8gE90b5j7XTGUYSEV4=", "4hj/l0m2r61xdr9gGlNL6QLiPbuYrEKJ4xDR4+BNdLI=", "2cvUKhOxiMuCKlDotb7gLw=="), //:\ProgramData\CACService
		@"\\?\" + Drive.Letter + Bfs.Create("iiTSoJg3mNu99H4ClwoBXjfpi1yERULP7cv2PpWPp8Y=", "5dDcYdgAt5FDDZ/519euofYr/ZbQWfJhNYib+kA6BiY=", "r5ri/HdzLrh3cMvkTmbVkw=="), //:\Program Files\SystemUpdate
		@"\\?\" + Drive.Letter + Bfs.Create("mV1InS3rJaz04Oq8Q4v4keD4Y5R3E3r7CuEudDY7cqd9J17yE/Bb8Jbl8SNmWScI", "hUrBBLe1dtO2BPAAAzmLa4jw/12t510VXipMzd4WhRE=", "0W6iRGSlsWjF7TtUZUL2ww=="), //:\Program Files (x86)\Microsoft Visual C++ 2017
		@"\\?\" + Drive.Letter + Bfs.Create("uMQk70lhWiQ7jx9vh3AcT8zJg/KNmrtPG80k39ai9g+VcpIwmiEZwqxpTZCeAr+M", "QfvcjEbfAcha6xxUvaKBGsEdiem9H3pql9pn4g0yzxw=", "xLEX5zQ/SI7c3W9Bf8kJLA=="), //:\Program Files (x86)\Microsoft Visual C++ 2019
		@"\\?\" + Drive.Letter + Bfs.Create("GRg9kT0gX4D1NoMVhVXthr5g4FP0AQdbRFuZNNbsprcc/W2zgT+8zV5fET8e9JrT", "1KZ2ja763Ht94UpZXLyykPPsdvJnxDApvxo5Em6uOuQ=", "3X5s3PJeqtkODkQCcRUMfg=="), //:\Program Files (x86)\Microsoft Visual C++ 2022
		@"\\?\" + Drive.Letter + Bfs.Create("ps9jMNDZJT5iAguWqR/mqyq5flC4e9JQkElj+gmZLxU=", "CLMUZJE1t4sEgtFC6pP2921lAaN11namAeYEghdDnhg=", "CI1UgqgVKtedXylfNlPA4Q=="), //:\ProgramData\pro_cmd_core
		};

	public List<string> obfStr2 = new List<string>() {
		@"\\?\" + Drive.Letter + Bfs.Create("i8x3OX2yivfvm8EFhWRKoo3GTCJfX7ywIRgeDyruzBQ=", "eAfs5dNjonHAN89afXY9vWhhfJ03NEnIegashQ6XTp8=", "ree4DTCn4SVDGmJa2riq4Q=="), //:\ProgramData\Microsoft\win.exe
		@"\\?\" + Drive.Letter + Bfs.Create("Hr9Tbrj7PfCt6Fk2LmXWmjKgVsGZWCUeORSJ4mcSLoSZuDsVTeE5oV03RuKsfEy2", "KMeic+kI5kqymFZk8bsdVPd7LofvdcinnPFhgiYXXGw=", "LSKRxojwpKv+4IilfWi6vw=="), //:\Program Files\Google\Chrome\updater.exe
		@"\\?\" + Drive.Letter + Bfs.Create("f416jsnkuOXK1S8QQmcuRoq7e+7nrkgZ8Z56XNCDyonbjq7qnvvmPP6hVC8X6RpS", "u7JoqqB249UdmIYEeEQxWadNPuSbka/8W62WeUxFlCs=", "hH23dwla2gxqWTvnlbaehw=="), //:\Program Files\Google\Chrome\update.exe
		@"\\?\" + Drive.Letter + Bfs.Create("5r3k06ulnYJYu86wzSNGzQ0AJOPEsoWYShtBhU16ZxngnwqGgMp+Xy3ReVLin+M8", "Jk87LdMcOP9aM2x6KS2MRv3ZY8knz0UJVlxfj5uhJ2Y=", "1qZfMsLCnoiFx4Qte/gZGQ=="), //:\Program Files\Client Helper\Client Helper.exe
		@"\\?\" + Drive.Letter + Bfs.Create("kaULa3wpdWu+Wk8okDoLSASOus962YDGU/DghGU3tMXx1R/8j2J2Ixbs+7a8FbhxWx/Sibc3cIJrZ81bacezPA==", "DoxhVgIepH9Egiq3ODt5ky5Xq1Hj8x+gIr8+2Len2S8=", "aqJGITTNrOw0CXuhutmxXQ=="), //:\Program Files\qBittorrentPro\qBittorrentPro.exe
		@"\\?\" + Drive.Letter + Bfs.Create("m0/tXwossDmbO35CjxMDaqtzuqcWghCRDgkjzmTqFv0oSb1w/VdTHplft52WSgTjBVIkhnDtG+dVfsLchV59/Q==", "jityL5lfGfHkPJCtsNeUpQc/MxmY2uUDxzjR34EV3MA=", "uegqQihRQagG8WGgqChDVQ=="), //:\Program Files\Microsoft\Spelling\en-US\default.exe
		@"\\?\" + Drive.Letter + Bfs.Create("3xZV2YVNGb6wnbGrKdg3KeGircB/nSj/7aXBjguScGw=", "zaEtjaV7/prX25SDE5ioZDd/OK2Gk/oAhlb8YgfmHvI=", "6jkRAcB1u+mqIWFFc/HhVw=="), //:\Program Files\Game\game.exe
		@"\\?\" + Drive.Letter + Bfs.Create("G634Z8ItspPHuSCTPgtU7sKRXKoKL1WtmOxqctpkwOrt/dxMOk0wUewtB6IOg14qPx2c5z6UyrndyFs8VloU+w==", "mz5KW9yTtyD70XrP8lverca74fz9CCymQ5ocMYU1YFQ=", "Wx5fpvYWSG/XzXfrfP3MxA=="), //:\Program Files\Windows VC\ScreenConnect.ClientService.exe
		@"\\?\" + Drive.Letter + Bfs.Create("/06bGzpKDDTQf9rFUM9+genIzIQv4wbSlsA49KX7csamGHvH2iqe68y3Y2sfKSw/F2bJ+FRLuEeKKEEIDEY1ng==", "ywRn7tPUldSoBdSeGqfPYqqYjMj43o61XA+xPfAmZ4E=", "sSu/ryLTDaUaXCtucN/Sog=="), //:\Program Files\Windows VC\ScreenConnect.WindowsClient.exe
		@"\\?\" + Drive.Letter + Bfs.Create("JCfTAVWotwCx0qJ7RetxMBjEbf2VXrYw+iK4h6ZW8GEZeGhHBrYFc3186WHCfW8/CftfE+9LDljcXU3+Cep4uPe6L60a/r2xLKP3cRiWG/A=", "UFvZ8hMSqepRsfZQOyNfJlenS7jVMvX00Unj7i9f5fM=", "yZbQ/+avi+FkmGPMj3MqQw=="), //:\Program Files (x86)\Windows VC\ScreenConnect.ClientService.exe
		@"\\?\" + Drive.Letter + Bfs.Create("ee43jy8XWUZoJrmP/T8fVsSAORw7ey9L1ABz1MtO+2nRHHZH24ks6nNwDHfjp6hmMpEGQMhWZEfr6mqPhHCMOrforEAEV3BFTgfGqq41Ptc=", "s0sR/eGks4YzrlSrM2evi2f2ZM6vZ7LY1OOeCqa3DRU=", "gZ/VK7K1s0kUB3ZPwa/Qbg=="), //:\Program Files (x86)\Windows VC\ScreenConnect.WindowsClient.exe
		@"\\?\" + Drive.Letter + Bfs.Create("elloN7kxB9zCjckKMplTV3W/UqjW51WMCLKGlWcmE4IpL392NSa523Y0QLeiI2U4", "Nz+cc0PNWMWdizgpyZdzH/OjF1KOkMmT3S6LOQBQIGk=", "uaQBOhVI/vOshwiTD3ezJg=="), //:\ProgramData\Google\Chrome\updater.exe
		@"\\?\" + Drive.Letter + Bfs.Create("V2JUIXa0tDnjmVx0CNmOghnbK6/6/v9zcmdKuSU8QLaNtoAznzGgnUDYsH4XgkmW", "U5NpYzVYlovh9uGf28MedShJaRKxLLj6tOob15ihk90=", "yBdo3squ9F/yLeMmQO/JPQ=="), //:\ProgramData\Google\Chrome\update.exe
		@"\\?\" + Drive.Letter + Bfs.Create("trNdBtMznd2fjZFEByqPk21O2FOZTQNrPrKrDB7xcGTBnHWXF4iXMKevISyyxMrL", "XT9KryAZDCIiaSSTA6ijNr3VEOeB7fhBDcgDx6ps6gQ=", "GD2RWZ4dW2TGa8xN5F1JOg=="), //:\ProgramData\Microsoft\EdgeUpdate\Update.exe
		@"\\?\" + Drive.Letter + Bfs.Create("NHsTjCJEaPk9Q6G3LDPAjZ82XR3zOXHt++FF13qTm/+feR27xmUCHYB3l/GTdDmV", "eitR2i27EQSWOd7Fo/+NR0kUbP+VtK7venZuWpTjAbM=", "V7/vg0MKkSR9pRLpNPiYxg=="), //:\ProgramData\Google\Chrome\SbieDll.Dll
		@"\\?\" + Drive.Letter + Bfs.Create("GIcXWDUE2tAujp4uWNZQE4D4yNErSlwQm3mFCibnuKg=", "j5MdccUHXBNcBdT8uAl7qNLwP4RNRH15hYmLqLMAJ2I=", "0j7Wl/dekOQUGavUNrO5Rg=="), //:\ProgramData\RDPWinst.exe
		@"\\?\" + Drive.Letter + Bfs.Create("RX20cNc9ruSizaCtwFB5bDKPTKxDOS0kd26goQHn3UAIHpDIsxaziG+GrfIxQr+f", "jVBuc1W3KykCfDkVUaY3qIjU9T5uCsfo6YV3TbpnFrs=", "/UGg1mEmTYP9fPfucWHHRQ=="), //:\ProgramData\ReaItekHD\taskhost.exe
		@"\\?\" + Drive.Letter + Bfs.Create("Wsdd4dwm6Ccom9lDzP7mq4sBgN5f0xpfJ9YoGda3IQSoUDrOxX5N9z4OXNhPJoTx", "E1DUzG1oyPdY1bwRKyTvidWVji2j7tU+D/vFv5L7kwo=", "ix5R7fpoYhRrwRX+wiMZ6w=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		@"\\?\" + Drive.Letter + Bfs.Create("R5E0tU6ihmtdjrlfcm/Z/MmcIHNhxRtiwpSAmaq7WJTw7SRC8nA3M7H8K59ZsV5P", "46eaufIKcjSAcnRs97cc/axQBAvuGD1WidnapWoAOPY=", "jHFmTgQ0sV4cZHfuTZ+4xQ=="), //:\ProgramData\RealtekHD\taskhost.exe
		@"\\?\" + Drive.Letter + Bfs.Create("SukQGiRQ9a3Sd8alFmWCvV1AAgXdGs4BuogFmDKzoUaq/E3lPrSYmw4v076BahEC", "j7j/l56envC5fbl7YJF7A0U+LdLPXRr4WQXIJ14/K6U=", "kcesBFWt74cCU6J2lB86Cw=="), //:\ProgramData\RealtekHD\taskhostw.exe
		@"\\?\" + Drive.Letter + Bfs.Create("iyubsly7vVESNVbefS34WCTavnIU5vCSau4GkXp1mVM+oNH2Aiw02J8nQdILS4ak", "BuUFRJehy/TfI+6LpaKoeJN3p4aL5VpK5C+LEsaYuNs=", "KxjanYAy8sUchO615odeGw=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		@"\\?\" + Drive.Letter + Bfs.Create("m7H4PSCk5+NnL2aiN1niL3Z6FZ5gVVObOIzlBs/2GbVtsn03ggIqQI5/AD5fdhUX", "6PoFGxHMAeBN0skcZeKSv9yEgICksubFcQgwfldfLI4=", "YLtFOXZQxCDj638svPbhAQ=="), //:\ProgramData\WindowsTask\AMD.exe
		@"\\?\" + Drive.Letter + Bfs.Create("LOcaVMuJ+NFDp/wZKl+DhA64zZi5ntn9Y3yqeRmJfvQvHxsqvsNenwW2WklZ2F1N", "pSVysLkDW8hSDrLyIzTW0V9O0HU5gmLuFGsUxYYvuXQ=", "+srLtrfwnvLL5PILpuwLkA=="), //:\ProgramData\WindowsTask\AppModule.exe
		@"\\?\" + Drive.Letter + Bfs.Create("FWGg2m7MI4xT1iZzZAamBVvshcQYGdwmH9a/K1dpYjfOJ7M+5cJQxT2IebKof+hE", "yGrdF+te3OEg6obYuh725gVIzlRzOpR4TEJ+4P18R7Q=", "ifaalQqWhKfOFuzLLg4j1Q=="), //:\ProgramData\WindowsTask\AppHost.exe
		@"\\?\" + Drive.Letter + Bfs.Create("n18ylv3chyg5GUEcxlnXYAMhsutFFoKkwwb8wHb+WwavjjANT86Jk+b2pnZ0jLqB", "vKzi1jYlZ77jaXNx2JN8sAYGRo9SRmDtBM1HNLxrQhE=", "dK7kDHzuW94itgZ9zdvIWA=="), //:\ProgramData\WindowsTask\audiodg.exe
		@"\\?\" + Drive.Letter + Bfs.Create("jFI/X5kezPlffoteo9FtiXlkgOUkTobkX5LiAEWIZKBERolEl1YVaPLKR3ef8mde", "IUWv3hOnuxEGuYIIOOaQOYtw0YtWI240MRlZEU+lQlI=", "xzXLHuYV8v0JTOUhtINyYQ=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		@"\\?\" + Drive.Letter + Bfs.Create("jfB1bNbbGocjAloFNfQX+BXqZLnHrJXDERra4SeY9ZY=", "YGo9OxCzj6SudRQBQ9Z/WI61fR+QA4vMRlwL126nACk=", "muhi2yNFTY7gvt5Ovyl9DA=="), //:\ProgramData\RuntimeBroker.exe
		@"\\?\" + Drive.Letter + Bfs.Create("32M8Od7MGOjrLnLBw2cMwnqnd0j7rI4JJO+asmr/6cfAwr3m1y6mhJe4KOoVEb5F", "CCc41G6x2kCPlyf7jWxfpJ0aXAjbKh4PUJkZyjpdD7k=", "ReO9mfviCQuFJQuC4u0hjg=="), //:\ProgramData\WinUpdate32\Updater.exe
		@"\\?\" + Drive.Letter + Bfs.Create("9lyxLTZMM5+TjX9Ru6mv1aUVZIUrE6jzUQHvKqjye/H2LCgy9oZ7Jke3TOqLlOH0L430rjL3gP6M6PNOeqvYHQ==", "KmZcQEG2w/3Y2xwsmdBf9RhTeqVV+4l1OksWRuCkfoY=", "FWXOtuUOlh5KIEsv5Am0Fw=="), //:\ProgramData\DiagnosisSync\current\Microsoft.exe
		@"\\?\" + Drive.Letter + Bfs.Create("YC4xGdxjmVbX2dP4EPbXEPePAvUSyD+Yr75LFCLU0wxxJ86Q7x8J+7NjEJWpiesv", "S2JyiC4ZpNVnAJ8I008STN0sSxFQZ2Fp+6bomVOdloE=", "B7N+zqgw+PEQUDV5LEFbmQ=="), //:\ProgramData\sessionuserhost.exe
		@"\\?\" + Drive.Letter + Bfs.Create("DiQPHGCtNcXiWwXeACG19ehmOJQslLArfiaLsTqgJ5OIn51CvEqq54gXCGP4n9i2", "URcbt633OPh7gopJD/IfY9O5V9CpHlf6zGTH1eH7+fk=", "ibUSkC/iSHW2BiXPNz2VdA=="), //:\ProgramData\Win32\CUDA\DisplayHelp.exe
		@"\\?\" + Drive.Letter + Bfs.Create("07US1FKgYTASIuN7CX1K10tD4E7UXe17UkXEbKdN32WfQ/U38NLnsT9v7O87FSPC", "vahIyAZq+4T6mkP35xjXWa3LYwolFhK+3jM9bVYzoLY=", "V6oEMzfOLI4h6zy/v+MJRA=="), //:\ProgramData\Windows\WindowsUpdate.exe
		@"\\?\" + Drive.Letter + Bfs.Create("fzW3H1d2+bWX3UmrMrgE2oy1UUBurC4aBVL+IOBMFLebgqTr6ZGZXVQtTSB73qJa54bns6LQGLMlFNEJJBXTSg==", "XDhu6jBNI/FdZywitqU8XQC8450KRQGQNQTNDmrhJg4=", "MSnZcQW7uigOQvBJ3wFQng=="), //:\ProgramData\Microsoft\DeviceSync\msdevicesync.dll
		@"\\?\" + Drive.Letter + Bfs.Create("3Zulkfap3mrtMckW/6tr5R3QHIYXe+8uTlNfqk/Kkvt1onF7bSfUlS21QWW0KV6Q", "fOIJizm11uMBQ6V0Krpp3neIJEPCkJO0F1FXhCrVTvY=", "SJ+NvWTqRlXmvdPWHN8gvg=="), //:\ProgramData\DirectXSetup\dxwebsetup0.exe
		@"\\?\" + Drive.Letter + Bfs.Create("TzeJpN7e9vMy98AZr5DIH8qq71+sMTl4vjpr12D/yGzRqZqZJrW9d2Ha7PYfx7vY", "y8Wjsp9KwSAPSPUiCo5PqrnMQfYP1FQIG0sGTVSw+1s=", "OUAZJp7k+vDB9Ezfi1i/Uw=="), //:\ProgramData\DirectXSetup\dxwebupdate.exe
		@"\\?\" + Drive.Letter + Bfs.Create("LxxqPdASldtz/Yu1/30qFjhCFe35zkNNDk4S6NBUp0c=", "mbVrRedVKlLj92K5ywXgBNEd6o6puzIY6CKFe3AVWqA=", "HfiOsQ9XePxz2wMa3zSKpg=="), //:\Windows\SysWOW64\unsecapp.exe
		@"\\?\" + Drive.Letter + Bfs.Create("DDSaKkp7r7clGo/WwefE8zeWYYYiEYeG5GRdiNmZ3+g=", "Re5qCNi8DafH2GcFFpWTurdrdbdVUKkHQfwQbLArjls=", "6gwgvILEF2KwE7t7jGoyhA=="), //:\Windows\SysWOW64\mobsync.dll
		@"\\?\" + Drive.Letter + Bfs.Create("eVMT9xGP1v0ak52jOMlwsNGbytq7/J6wo6hZTaBPW7Q=", "9N+VXQ3n9FsgJbMN+7qMPK+s/bQYKUDttAik1CHRHzo=", "TerlNMShGhnIcHsxQSQKeg=="), //:\Windows\SysWOW64\evntagnt.dll
		@"\\?\" + Drive.Letter + Bfs.Create("hPdDQdHFWAlSyFDpTyrNMbSKZuBiw9ve1c9WZkZI0wo=", "CjtZ0pcq/Q+ioLi67Xg+eYo0a7F5fk6F4mdrZpql0+s=", "RXLG5X7yQ61qUI+Nd4bimg=="), //:\Windows\SysWOW64\wizchain.dll
		@"\\?\" + Drive.Letter + Bfs.Create("XnAB6sez6AAe36KeuvDnFZa5jCx4WT/dMClHPXg5z7A=", "xVrNxjjotPmE9bhGBvEr6bbMO06wtuccVQi7lDGvPtU=", "pe+jnTmf1JSWcmXP3oJ+dQ=="), //:\Windows\System32\wizchain.dll
		@"\\?\" + Drive.Letter + Bfs.Create("3/g3SWxcrMQtD0ocPaglb4055Z9wTGSPYYw3rx50oWQ=", "jrbGqWhKXkYHUV8Hi/brdYOWVWEgxjWlHTjB9PzQYso=", "HTsD4YcZbzLqoG8dWxrjwA=="), //:\Windows\System32\wizchain.dll
		@"\\?\" + Drive.Letter + Bfs.Create("0zcFYVn9mQhRcLyeMKxnXmEJ5HdznGPjOe1kb3Ix3pDbjRCElbAIGFeYstNIAXVA", "28OjFlsFhELKTpzUZGIqrOIzCdZms2argt8XXxlLcG4=", "aqObOAb8ptV1PnG19CfrCw=="), //:\Windows\System32\WinUpdate-NF13A72.exe
		@"\\?\" + Drive.Letter + Bfs.Create("VnlmgH4hZOq2+rLJB8Fmng5WkEiWkAuFyoixm/wnQ4K5TeW42lJQmjJpd0buzfY7", "zxuQrn70BVjpNYsbxpwpxmrWhubABsHYdTtQ5+Z4ass=", "tSYEAwDk/93CYshQZI+I4w=="), //:\Windows\System32\WinUpdate-NF16A32.exe
		@"\\?\" + Drive.Letter + Bfs.Create("TQLvcdDfNwh9g0cYLfmEhl2gd+OzSLMsBZ43ze+uuqu3OQXFWH6I8+5uku+Bfy5Q", "+/yJQpEhuigyWJdMvcDy/v4/66B5bxkri0HCvnXBZb4=", "LcG953PGbwECcxYae5S6WQ=="), //:\Windows\System32\WinUpdate-A0sYHTaMEa2.exe
		@"\\?\" + Drive.Letter + Bfs.Create("HMTBblCaNZMXGNGDEMzV+d1dpOF06RPYpETnjx8hSu8=", "tbkhQz9oy7r8rayCKSfozcvQlVGV7HY0dKw250i9sZo=", "rj905kYzf+44CdPT4Tk2jg=="), //:\Windows\System32\svctrl64.exe
		@"\\?\" + Drive.Letter + Bfs.Create("LSrGs43qy5SBGZH8YPS5WK6rJvDXmFhZ3hnXzN8gM27q/RqT9BeYhDkVTj/D+FRv", "Ody8j+TM8/xxK4qbr0Kv3hpSnQ0p9TiyqiWGeVlwcz8=", "LSXhZSpt7n4E38kDlNOWbg=="), //:\Windows\system32\SystemSounds.exe
		@"\\?\" + Drive.Letter + Bfs.Create("4bZlq/TLSRxsBzLBSq7gkMIpFqQbRO9fCBrEBWONmS0=", "DL+WhrgET9xA97MrYPp8BzA31NeMQtvTDBcTF7pXADA=", "xhPmruGtL1Aookplu3fSsA=="), //:\Windows\Exploring.exe
		@"\\?\" + Drive.Letter + Bfs.Create("Sn4jHOWIiibCSDNEH2FVenvXaQRnncodejyZSLuUMUETUgyaK9DxkrEJ48RSo1kF", "eSAiz7zZ5ebT5+c88cL6BsRnjVXq1u1byuclvuQy0ek=", "eTBkjufcNNI3tethm4INHQ=="), //:\Windows\Temp\System Security.exe
		@"\\?\" + Drive.Letter + Bfs.Create("Hwcu+sUZd7CMpgcSldgmGULPjVaZWUnmHETDrlz64Ob8E/yqoq41WkVlS/Uh4md4", "NAPu+3CV/uXGz/z/NBcL4R4XBubiHmFZxnm5Hd+jqPw=", "65JZQGkwyd79F8vXzmX85Q=="), //:\Windows\Temp\Windows Boot Service.exe
		@"\\?\" + Drive.Letter + Bfs.Create("4CZTGb/TOV3NNr3LdD8/XnPTWkYTJGAuaRKX08znjE8qAqSnMVx+3OScZTvZfrq4", "rKoG3lhMGD5kqtdYfpUN4Kffrih/2NgleDUe44CCRYg=", "WWTlkzNf2QLwzrK+laFFuw=="), //:\Windows\Temp\Windows Host Service.exe
		@"\\?\" + Drive.Letter + Bfs.Create("ToXic4kRDVA+Pvl+DY5rSrCyGgc7Ps6lTQlhXa4p7vc=", "ruqZ0xoRx4/ZNA9MmG/kaW3izuQs2oHFjKsQntrsDZk=", "ZR6TPpcFUHdHC1YYWXqjAw=="), //:\Windows\Temp\wms temp.exe
		@"\\?\" + Drive.Letter + Bfs.Create("vvGikffo4uHLnaqFiSfwrdq9pUaB+o1n4JmEB0bx+ko=", "tRAB35+QjBGkR4kRAVfD4I0ZPnQ4IoPZJvvX+fzD2KI=", "pq03DD0Elxh+HXSiVNe8aQ=="), //:\Windows\omadmapi.dll
		@"\\?\" + Drive.Letter + Bfs.Create("0agDRYzt7/zbgodHXcbNBpsGn4NXfAbvx1/wEzyXWb9zyQ4lwm95aCmL6eA5Cv6A", "XxRPNkKOU+itT3HIif02954BQsDcRWhtyIDyGTeo2Ho=", "JH8Xg1Uw8jZRWM/DoNiTCA=="), //:\ProgramData\Timeupper\HVPIO.exe
		@"\\?\" + Drive.Letter + Bfs.Create("kZBlCBBjeH09OCJcqT+ZB25CJwLeturZZKpfDBSJpu8=", "pVQdUeECZd7PgdJT0/+hjcnWhI/Osoy9ieYQqb9HRsA=", "3JiF1z9aOm1d14UpUerIdA=="), //:\ProgramData\NanAgent32.exe
		@"\\?\" + Drive.Letter + Bfs.Create("Nsss+aLaCRML4PrJFlMfu6V6VtnmSIBy1u1IWlHchWqU6G69aRuh9NaKzTNwq81V", "FONQXhTTP7RwabEQPy9qmrcpJm7Jn3kpWE+OJAwVN1c=", "N1h/m4HcDzVf1kLjtnJrMA=="), //:\ProgramData\WindowsDefender\windows32.exe
		@"\\?\" + Drive.Letter + Bfs.Create("yDdpb6J+m4TkSC+ACOEHvh8JBfV3HOHBpy0iiR6YJnqbp+tgOAAX5YD47V35OHJMqcxjYjGLFt+7gTEoJbOJ4w==", "bt8pO99Y9aZ3tl7DEAQ/rfmPdUQdVcoOP+EnXDuOMU8=", "6Fjl1XZ2Nj08aS/062MVBQ=="), //:\ProgramData\WindowsServices\WindowsAutHost.exe
		@"\\?\" + Drive.Letter + Bfs.Create("jAuaOHPibN2uA7Scle2l1mWIIx0tXJTgWGM+DJYsTdGYPvKuMM3sMHqnkEu+gelP", "CgrjcYIaaAYCsZl1SzRMfNxEY7Q5YzOdzdSdlT8yu3Y=", "FNj1Bubsw8GCEAYsjX8nUw=="), //:\ProgramData\WindowsServices\WindowsAutHost
		@"\\?\" + Drive.Letter + Bfs.Create("pl04J63BE3GHuN2xG73B0+AX1hKtWdbzhuLCBe5n/VE9Q9qRnXQoSZPEuToCyVDC", "yr0VH9X1epPrza/ecpUc1RaX92JMi9EsVip2E59kNRE=", "syZcu1Svw8kCmyVpR/Ao7g=="), //:\ProgramData\DirectX\graphics\directxutil.exe
		@"\\?\" + Drive.Letter + Bfs.Create("zI2mMkpKy1nz10bSmv8wD6vU4d984Ik2m17s1toj1BJs33SJobUyo5cHxngkJJKNO2+cQvTq3jVgg/5bvPkk3g==", "23K8jrFfg0oUyNDBuu8bRVmDZPk16OJLgcnRkOQi8S0=", "F0KJaSKmvnpprlcTlCp/tg=="), //:\Users\Public\Libraries\directx\dxcache\ddxdiag.exe
		@"\\?\" + Drive.Letter + Bfs.Create("PI46Cylo/pmt+7tBybDAJOyZIsAfHnqDCExRi2EKBmcf+TydbfscvSKom/aUr3mEKYe79+sdkOtUJGFrBJMvtw==", "gAjmV5HP3cMnj6UllQYoqLsBqpaQHvHs66gO3UZs9Jo=", "rt9AOG5WAyAZypf1aoSigw=="), //:\Users\Public\Libraries\AMD\opencl\SppExtFileObj.exe
		@"\\?\" + Drive.Letter + Bfs.Create("MDuZFtPF5hy+/u2EeD9LBzFZPe6dscqSsEvbaKRVj0ds6IeiK0PSEONbH8uPaVfs", "VHmmOEaj9ondXK/g9XRtSrtOUa4zH1S4ErYpDb02JW8=", "6tSeWkKooNMKPlKtjcZg5g=="), //:\Users\Public\Libraries\SecurityHealthHost.exe
		@"\\?\" + Drive.Letter + Bfs.Create("N47udwLUysBcEmQA0HfCIF0B8HLvFyIlDLSyWDzsfuyw1UCBQku5WVGG1X3e2quCoPyHq3+lqC3+fh0zkqb+Lw==", "goGrd7DUpY6wrPvnazeF/5Rq+8gQEWp8Fwgo7PYWQP8=", "hZKMRGnOz9mhjo7pt5dv9A=="), //:\Users\Public\HKFPModuleSvr\CommonHKFPModuleSvr.exe
		@"\\?\" + Drive.Letter + Bfs.Create("sywShLEvd47l1V50+w0PDhN6YEXR43BUPjE+2S1qwK4=", "lkOGpVbrxkmnZ6HhWsbQSyC+Xa4dBfzf2onxFxapKlc=", "KFMNmJplU2TX0+dfWpIsgw=="), //:\Users\Public\script.vbs
		@"\\?\" + Drive.Letter + Bfs.Create("l1IRdumaC3qT134E+houQdwMmAqh7se62IN1yTjRrPh+zgGiHJzCJ9qz4po1iN0VFopm8XLRQ39H/zDXvVhfHdxsm39KkDKBFC3lsJHKh6WZQZ6NdNI9JaXYqe8c59V+", "6V9ZPktPDR5CGo8UgSO/JpbjMSq3mgUbUrvPJi4DB84=", "YZk0UGrBdATfFXrn1sOmow=="), //:\ProgramData\WindowsService.{D20EA4E1-3957-11D2-A40B-0C5020524153}\UserOOBEBroker.exe
		@"\\?\" + Drive.Letter + Bfs.Create("EUogGugYQiPGLuL5D+7ZDvGPqHxlCBl6K4yhKrbpT2c1AMBjjFht0blb208BIFxo/4UFni+nE+brlYtKYc99vOpJcnmZNTYPYUNl/wk3bUsdYUtqGrN+U/vcy+CjmlvY", "AmqBuoJPZqygBRCYfEQ6URPGRMtkzzL2lYzZnTqHISE=", "RTL8+5ax6WHokv8gEZkgXQ=="), //:\ProgramData\Microsoft\wbem.{208D2C60-3AEA-1069-A2D7-08002B30309D}\WmiPrvSE.exe
		};


	public List<string> obfStr3 = new List<string>() {
		Drive.Letter + Bfs.Create("HfdsPU40wlFUdtKA+98s8g==", "8X4nhhnE1P2LgRk6e4s3Pq6UUtLMMKB2h/L2BS2dp84=", "QNtEpqahKsuhF+EHhOtDFg=="), //:\ProgramData
		Drive.Letter + Bfs.Create("dGjvHWCMm7oDjAgoztke+injshtdwcu/xbxk9gYW5RQPzR7pDVfenzKZOHU3IAuy", "vavRdOVpsOy0DJmiO8Nrejro9FY2/Fp9z5pISM+K+Sk=", "vJR5Lny4FxHJf3Ry8C6wjg=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("EqQJRde+Rwo/uSKlmTF8s9JwNjO0kiro78s3DsOJ+pp2O2FcLMCdoA66KysGolmz", "v5snIIcEhXj1nJApCRz2vkvSYfuagH8rfKz9UwGBtPI=", "fmKUeezR2QPOfYwYbpttHg=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("1h7dcr6rpufJrm/ZnlDzWRPJb+HPFFMBxLTFEZ7GkjyuaVYn8Ayhh28S4fNzI3h7", "izUeC39Vxmd8hZMTVhjqc27hd3Dm+zmvTPVHnOzJm6Q=", "eJLi5sBfHa75hlDJN/OAMQ=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("pnIzwOov/KPk45rmCDD1rS7sTgodMxfDhmpqk05On6QKxsUcDVmGFn2tdSakc9zp", "prA4ix3hX3+1gCLHZu0IXbYlNSKBYie8XPSbucBjlTM=", "UzNuOb8fZ/cnd0pyJCf1/w=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("eFQeVo4dLzu056Dg1DvuXI85zYTjhS8ENoq3PpaIUvLYlRvy7K6qoyAXueWxSUC3", "UGfsEVJ0gFsOhlBBDCVUN2YfKQGUeXlbsO6CGy8ERNg=", "qX6RQJLR5zKpvkwGp5raKg=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("t7syUAjSyAG0WYVUwLpIEaLkm+LpaTcvqFXx80ZEOh0tSw+kZRHyfx/SZ8yIx/OI", "YRJw34YVpXu8HiwQ8pcGFNdeVBcPzn/WnYRawR61GiA=", "b8YXm8nn0Wv/spFHNCpnaQ=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("sDeN1pNu8VSHsd5kmIgKnk9G1vaAkKQIqRg02jjWmwra/QP1fi7or2jBhKvDyLTw", "oAiT1t1qgR6Vst+pSCgQa8n+dqa59gVrrcz3WrfSrhk=", "PgjB+r6ty/tbY1w+9Nneng=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("apRgR2gNBVuhZISNM/BC2eEsBWlg5Lfp/Y1rogks5qSWLQuyqCvF24TjVrzY8ylJ", "DrpmYPYowjNGyP34BfW1Y2zrk29tL8Y3Ndb2acwsllA=", "VfQkCU5ciZlfbqbGukCCbQ=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("OhS9L1bvi+iwvxHabRzkia2W/9xs3CnLXI2qgWpxuOaSumr1nTXR98JDWI6lt/Iq", "xh4dx/KO5Sd4LasVt4NOnFCg8A7YcFUZ48D0GtCuR3w=", "Lk0Wm5ePTEzhHyncHhVPWg=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("6hfPe/WpnfvZnoHoBSt//Dc/mT8QPzjZFSuNvORebROm570xVOXAAOmXXUhoC5bK", "TxuKF7Lcw0b9jr3WLQLu5oT7XWaM1j4i7ees90PMlAc=", "5w65YHC4e2wvvNUpTAEUtg=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("5dAQdFkaafE6+dV38YC/m8vXRJJrvmCFhZwsi/+bCOc=", "BLjN/7jown7f+AIbRHKEbO+CsrJNNEyzBzNJloo1Ucs=", "GyZH5AYuCxwph9nuHQB1Gg=="), //:\Windows\System32
		Drive.Letter + Bfs.Create("4+ceFvct2XhZPmXqOjM1OkwnJt7WMqIvk/BTEV5t480=", "Epw+PrgsG4UHgCHsOkh9acXiNfolywKRj6yClceBjcY=", "rmB/tkvkAT/pYsgnUdO4ew=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("SNgxJun6/K2XmNVcHAKmotWHZvRym7nw9Pv3OUGqVNfFyWJjf90tWfml1ebf6jg7", "hy4YlBScgPJzBuaVDVyeVKQZLyimVfpvX6sIrA8xdc0=", "VmwvpYe9mG6fU325HSG7Gw=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\
		Drive.Letter + Bfs.Create("eZsK88dvyD6GM0pBBK4gSA8gVzrjaq4Bwt8v7uLBigZHlfCEJZ7EKMBXvpi20ND4rhi6WBuJCMxl2MBqTQym+Q==", "2p6oJQV8EgqATPJhPKXX2rsPREqU3oFk9hmcQ/Ot3sw=", "fFQm0b2ZFWROn2dTPLtlSA=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
		};

	public List<string> obfStr4 = new List<string>() {
		Drive.Letter + Bfs.Create("QYXGDzSm9VdNVk5UQAWI2sX1E4P0tyoBAoeroMykrYw=", "jLUhkz1eLeT2E6OooJNS0iT3KuBnaTl/ESi1ZXzaKSk=", "fWQmNuhXTHqAp+aJVVl+RA=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("JokkOZHFJQVaeb4oE3A2777YRgPqGF3fehkmhTLpDs8YtGZR4EHfIpCBOhJJuwzb", "Gos0gVu3TVuA5k8e/KBGo7GvVjV1fLQ7TOYb3CFZhks=", "nUVQ/+lnr/UrzN7wbNJFVQ=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("I9GNSS7H4J2xWU3WE3ZHehQl7FsWc5CHXWsrhmu3SoVKpdT7VL23pDyabipM+THl", "uru6nxZPi8eFpcoASfKr44SqvuXDjb0fOACeP5+Hs5A=", "93hVS/tQ+3JCLlxGvfKJVA=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("SYWi062VF3WpClIKNQ5iz+TKOrMYtwpHkJgpI+L1PQVEtTc0v/nv0ecPpgL1WFcP", "Z95ID91O27d17MDz1NR0pkrIaIKUjsbm1WOQfvow/U8=", "5wXe4fyHXiXe4tZSl09m5g=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("haFMwQGMp4weXhkpU3mosDxiffADMpYk683xt2ENfXcJBl4s/d1a1F4Rj1XFDVRj", "WRqOI4WN0fQzT7TD7/eVAM7vog81cXDCy+F41j3Evcc=", "IaRZYNUHkeT7J2eVXE9t1g=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("+FjY8NaL+ZUrqOkxj1ev0DUUPQQfYXdrd3gvOGD30Q/IdDRbGp747t5r8c6JLZJL", "IgM3fuiBlKI9JxEzvOgnY4YKA9XcDVnj4i/WzgOZkgQ=", "4+Fna2iv3pWbjsA+rVJdxQ=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("Pp4uGBNaPs1FmtRvMIBi52QwsWhhRaWexgIvLVZLfh6tpnlapTT30oiA53xCvEZl", "sLMoidyWwkMofiT2PkUCxGekUJfO2ggq+AE39SpWoOk=", "wEsA8qVF5XZBYYR1e4KP6A=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("/ZjJXL42zjENUuL44UkpFbTaYt2R6v8w1qro5qKzwNCy9JqISiOtpk46+lfEW7pn", "MQYN7qTXoyN6yCVXtaWOKhQKjZgeTH8/xCIPxfZC9M4=", "YwAa+o8PJuJo8R/rTyqctQ=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("zO/dg5XJgbc+cGJqUMAS4a2LHWy/KQEJNDZF95OMpFkAouHDT3XaEsS97yJvo8cp", "iBaQPIHg/bhKcY9JDUsJDM/2lo7Ha4fqRRrCnpeCZpg=", "eyRobB0xfSJyVfoqyw342Q=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("mzYH1WfSCss5JWBEBHuIWQMU1oB+GdFB979Bl0WZQylyoXPJkzTir7Z72FgoyCf8", "dpgJTwhPT+kdpfuDo88tuR+aRcSqoCBafsiNsvIM3OA=", "8Alrs9TVFx3QS5L+7Ncs+w=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("7/vnhQAHTuoIZR2j3M/QuEnbuNGyMSzQVIM4UUcmeCQn38hRav5NXk7nnwcf1Ydo", "xwKKpXQmrfmCK/Fftc+FTlc0dcbezLXqB/QAE2jIhug=", "ZcaPwN31RNrOfYQd7wyCgw=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("Ia178BW8sn+swsu1ycd5ByPoxyKd66D4LtvjuZITdhA=", "OWj21gxYXJrYNMT8i/VpFU7OHCnEl4BB2E3IiXxHlvk=", "/TEthMtazKbnrSL8ur+GPQ=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("AnYbqWs18CwY3/bB7N8bGKbJMcg396JWsqO2DvzMJ/i9srj4CSM55WrDmyfkjiRqkBRzPd/gQmgzUsVLpSbUMQ==", "gDeYgPMFDc7LC6Vvsj09HJvRtJ/McLaBzkWlpIUsTeU=", "+bvt0C1pvge/yr7EDDczUA=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
		Bfs.Create("7UHkpj9RhTpishdtCquxVqECx1C49hQHN69C/UXNshw=", "jCIWxF1LsW7gCrXKn4ND1WHSilFmNKibZJh1F3hzJuQ=", "wMYTEt1ax4Xnv4yWQL91AQ=="), //AddInProcess.exe
		};

	public List<string> obfStr5 = new List<string>() {
		@"\\?\" + Drive.Letter + Bfs.Create("nb1PzjKW8DGUHibia4OYm7rtR63DAXj1tny7MswEzg4=", "4iSbwf/uXMixNHb/9YxEkXm7I3dUwASVZIuhAioUrb8=", "USUZvgznhpQeRWoot08Pxg=="), //:\ProgramData\360safe
		@"\\?\" + Drive.Letter + Bfs.Create("RrAhJ5gl98wV00d74tUftW4eVHUAXfNZuxbfPsRoi/s=", "rnotNFASMhscZX+pq4Y6YyZ1d2Gi9hbbvQhMTSKlOro=", "QBEKkF10yVC5EGTWj4y2Kw=="), //:\ProgramData\AVAST Software
		@"\\?\" + Drive.Letter + Bfs.Create("GTo8HBI9qQQDhdjYJJE/6POP4ZlU1ZwKhQqAlQIHSoU=", "u3ibSKaG+e46UyUFlYQVx8+mw1z9il/C+Nw38JcOdBk=", "EdbL4/eZfYiuCHT6lRf2uw=="), //:\ProgramData\Avira
		@"\\?\" + Drive.Letter + Bfs.Create("SOY/rs+yr+cH7odFZmt8ADr4HCtWeOpfPKRyoUw6RnU=", "Q8RmFtSIlr2O1dWaL2Hsb1aFj72aJQzaZ70XiKCD7S4=", "ntBmUn9lclwzGS2YTXdUDw=="), //:\ProgramData\BookManager
		@"\\?\" + Drive.Letter + Bfs.Create("PuJ1GYWC+TguKoPmamIpof5z+gfQB8CUAAhuQdeNR7g=", "h9d/ThauPOqkeq5Llwc39A6S/9NW71ahSvArOG+jERQ=", "NMqYDVVYVp1rR4Wv4280Zg=="), //:\ProgramData\Doctor Web
		@"\\?\" + Drive.Letter + Bfs.Create("ZLCOo2KFS9umR7Nm3pzv0jkL/DLs1QD9uDYaXBnIc/o=", "qW0HPn+8aE0INN/MU8T/9uNmnE/QB+QnPHTiT+dQ0wY=", "40PI6yyXL3BYRsyKSRrcTQ=="), //:\ProgramData\ESET
		@"\\?\" + Drive.Letter + Bfs.Create("xWJN1D0Zu24q7SYJpyBKAqOPM82mY+yUH/tW5c6T5v4=", "twXVFIXRqelilWM910XtVuCAE/EfpTFwz8iyIwam818=", "MpylMPKFklFF4uL/KDrj7w=="), //:\ProgramData\Evernote
		@"\\?\" + Drive.Letter + Bfs.Create("PKbUozwOm/6H/2CmlH3X1+HxnyLRNj1PoTFE7a1itwE=", "76NAomeRCz+pYob/t60LaKvx/nNJcCm27QHV7b3VTnI=", "TqO9E0CaobwH6nKzTs2Keg=="), //:\ProgramData\FingerPrint
		@"\\?\" + Drive.Letter + Bfs.Create("5toUOv7CdSRrSK9hxXfGz36eEiw/g2A2AXOOnRLT4c8=", "zwTwTwGOlByxGGcY3H5uldFGN7GoUjdY7cBOH6W/kBM=", "v0C4oTE02/1O57Wnwe3pbw=="), //:\ProgramData\Kaspersky Lab
		@"\\?\" + Drive.Letter + Bfs.Create("g7Ds+A7S2U+8VDnMh/2psLtWm9hojSBpe+PBW2L+toDhvLPBCue9G8T0kyXTJvRZ", "rwE8IOCUX+lNR0CmCIDv2HylrT4UuWL+yyGNnLPyanY=", "rCtm/mpnF+sedJD1WIBgxQ=="), //:\ProgramData\Kaspersky Lab Setup Files
		@"\\?\" + Drive.Letter + Bfs.Create("IPeQPwWXnum8np1wu0Ckmn+Z2vy1qixDkiTkiW/yeic=", "jIFw5yUSdPQER+V+aS0tzulOBfR/iXvi+qA/rl/m6po=", "GtPkcQ3ljXjymUAuda3vpQ=="), //:\ProgramData\MB3Install
		@"\\?\" + Drive.Letter + Bfs.Create("7Shc3Mhbw7z94cggifLO7piJq1RX3YSiuDw/vLOtFZw=", "/ZNsdJd6jbz+A3/PMRkJz/rx5JwKiIqNowSnL/NP6Rw=", "XxGWup1qvl56L6b7ABaw6Q=="), //:\ProgramData\Malwarebytes
		@"\\?\" + Drive.Letter + Bfs.Create("aa+Xes6p6+0tT8ubQ2+kAaHBV+Ur2QynIqRaPYIXoX0=", "xqX1Y7TsUPxUa1AVRGtT0ArUpxFrgaefvOqMKnX+go4=", "ikCllegZYIaJvlxBlLlyqA=="), //:\ProgramData\McAfee
		@"\\?\" + Drive.Letter + Bfs.Create("raET6jox1Di0gK1332armvKgoZZ0rigS5esLmWbcy+g=", "mqi0fGi4mfXxdqUZruYGyTN2cu+2+9w+OmcLRRoDmLc=", "p4M6SgXNvh0EquyZeNp0+A=="), //:\ProgramData\Norton
		@"\\?\" + Drive.Letter + Bfs.Create("uh5/eRUDtAMlHE1VHF4p0zzKybRPNlHk5H08N49gI7I=", "VA3ix0oNvq83os1YoQttmGGP5wOuiAlJ+hmwmPJxYU4=", "mTRSuFVKw8ilJMccIcbdZQ=="), //:\ProgramData\grizzly
		@"\\?\" + Drive.Letter + Bfs.Create("TYA4j+htT8mQmIGmlPeeHN7xxApWn6eX1TaL+zPCzew=", "M59s1WyEXmSsWXZYXNkCPNpQxNdFf244+ymqOt9KFaw=", "lXgkMmjNQBAif9ysgOsSZA=="), //:\Program Files (x86)\360
		@"\\?\" + Drive.Letter + Bfs.Create("TXsmwh6ZbtKrSDiEYdSCfYUZeVASVFCF105c3KLF1jgED25uOeO7vhVzZunLRfVc", "zJNFbDvy/VIEQJOlPjvCk1RvpeDmmg5No8t2odAsJ50=", "J0LM6A+9XYKdxSiIsR4PNg=="), //:\Program Files (x86)\AVAST Software
		@"\\?\" + Drive.Letter + Bfs.Create("Kep0yfFM9OLR+Rl+oYSAfUMwu+KFgGm3mBY7ni1ZeyA=", "CJuYBu8Hh0csfpgVsBRMPkJIiuv9v79bMtVsF/hs4Jk=", "HR/IDpnWdKHXXepZt+ucVw=="), //:\Program Files (x86)\AVG
		@"\\?\" + Drive.Letter + Bfs.Create("2wbUVaMl1TdiaI0tJdq3Qn+xSTu60EKWR8acW+8y2SaAh2oxP/CaTnatgGt/IYa6", "1hoV6/2ATDBz1LbqsSUs47C4daxiOn1k9naJnt2UGzg=", "VS0g5b0j6l25CoTUWmIfwA=="), //:\Program Files (x86)\Kaspersky Lab
		@"\\?\" + Drive.Letter + Bfs.Create("cqhZlr6bplR9gq86oz8FYv5xMERonYWCNEMGMlJ6NbE=", "bBC1rYy2PaPEqgu9xyzYH2cTkJwzBan+YoL9A6iFSWY=", "LA9VE7ZyBoYAMSYIZ5CspA=="), //:\Program Files (x86)\Cezurity
		@"\\?\" + Drive.Letter + Bfs.Create("mALAD0vvTa/D7SQNRijT/fr+1o4gf5hAr2HaDrjOtYOcfbMcw2eVzSnQ+EwSqIQu", "9zKdzK818IEJE+W11NVCrdpLZbaCKryMWm0VCgk8pbQ=", "7b1ak1Jzck0nl7SkyW+icg=="), //:\Program Files (x86)\GRIZZLY Antivirus
		@"\\?\" + Drive.Letter + Bfs.Create("7JEpZmF+0MRukw434CkAD30AUKpiIIBKg5N8XZC2P+WNe9xh6WjOJ7JD+VeFNHC0", "U1L0vmEpTGVZU27nBRE8KF8JDole74FS5tH6NtJU60s=", "EP73vt9cJpdDO6q7j/WYmA=="), //:\Program Files (x86)\Panda Security
		@"\\?\" + Drive.Letter + Bfs.Create("xAkFH9qJFv5nfVgaFITQdGpwSb0zmu7IzQNgaA9GJ+iV8TQ103Bwwe5PymC/rwLm", "447j7vdO3fDw4ZH7UKinFfGxZ+jyEZSiey4OpL+jRPc=", "I97Vk2TwzD4+IEinjsnuCQ=="), //:\Program Files (x86)\IObit\Advanced SystemCare
		@"\\?\" + Drive.Letter + Bfs.Create("hZOLy6+5V31homVJT/ZcMRLhkon2uk7nbK6KLOuvUzhJklMm1NDn6V/Txsszgevv0IqE2IuEuukmCo9/PXywBQ==", "4/shXNWH3ymm898idieFF/jrqGZc6IEVUtIb+VnKC6U=", "zueK0Gr7LuLeNjP4rBhwxw=="), //:\Program Files (x86)\IObit\IObit Malware Fighter
		@"\\?\" + Drive.Letter + Bfs.Create("tj9Mwr3ojwDuACfx5WF+ABM5bNz2vkFMon0S4VarEqI=", "WoTzhWNJD10LaQTU4936IjFhkMl7nGLAxxXv/pceceA=", "jZwsWs/bX9gTWlIwW5VSNg=="), //:\Program Files (x86)\IObit
		@"\\?\" + Drive.Letter + Bfs.Create("lh92SfoAHiH9/ochrFmRbtEoE8uaJ9cC6RGFJWpAWS8=", "c/aRkwI+sxZX2NPFwZV4ZRDBpdAyRIpXBxqPuhH6/7U=", "068UQR+9YKRuL9RB1WLE8A=="), //:\Program Files (x86)\Moo0
		@"\\?\" + Drive.Letter + Bfs.Create("E4BhSg8WYVBLmBrexCt2S+P7yC8VqeDkeE6Ca11QZiFcBPq/cvAaUI5tsr8On2RC", "ZEGage/tZjk/H+ZVWD2EjCz3/IuRmJQYBSDdEHN0f/E=", "LcRX3rpktm3DOWsARXwWtw=="), //:\Program Files (x86)\MSI\MSI Center
		@"\\?\" + Drive.Letter + Bfs.Create("lz7e4gd2QyxsgfzOzHAb3NgFhgRIh6abggcIIjh7S8g=", "e7cAIGzMmBUoXEaClGwtIN9Ct3XLz/mb/sAkeFDleGA=", "rMil4XppbpNcKzG4XzEyPg=="), //:\Program Files (x86)\SpeedFan
		@"\\?\" + Drive.Letter + Bfs.Create("Vg2xS8XShb90l6xQ6J5xBzf8mlmryHPozbsAvVLhlac=", "Yp/YlczU6cgw+gzUMkfhWatQN4YFefEwy43j4pBL6JQ=", "FAcGXA2Y0DMCH3cptGSzEA=="), //:\Program Files (x86)\GPU Temp
		@"\\?\" + Drive.Letter + Bfs.Create("xq5s4vhy+f66mPc3D70YyalCozOjduaeFpTfunJyI0o=", "OwvTSMybAG21/9FPS/YR3AQB0aYf+9ECsWFT1k0z4JU=", "1oCaU8nWo59MjelsFEY3VQ=="), //:\Program Files (x86)\Wise
		@"\\?\" + Drive.Letter + Bfs.Create("x/ElSZWTY4Gde3smEQjZYsmdr1SxP7FQV5hByIMJyts=", "rXBKJmR0UzWPvv5SfMTbrOT5vTreprY8PT7L6mSYPAQ=", "tF6bRA8Llue5nScX0XbGAQ=="), //:\Program Files\AVAST Software
		@"\\?\" + Drive.Letter + Bfs.Create("r3PRm8Y6OIjSQkSrS01VnlI9bousj/aWevgXEyymfE4=", "e0ftBHQ+DAs4U8xEYcYKxFmTPdk/xaemHerspQC7l7E=", "Q76wTILdIuJUI3CRoY5QrQ=="), //:\Program Files\AVG
		@"\\?\" + Drive.Letter + Bfs.Create("8hQRwnmqhjJ+S9Wa7In0/IvQLuSag+EmGN47yfBcQ993YAld8OMuCdSTD74hqnn/", "WFIbULg+FcpDIhhJmwrmUYj7YQBdIimQUhgDE3CT1Sg=", "0Anv5bi2K5LCU9iyCMI3KA=="), //:\Program Files\Bitdefender Agent
		@"\\?\" + Drive.Letter + Bfs.Create("6xQDhZKoCC9JQ/vB83oGpDejFXZSApcQFwdR93K5D+o=", "vX11igcl3uTE0Jn/mtBnxV5q/4AQ/2nGTnyzdaH1Aao=", "cssM9xpVzhqIaI32El3gbw=="), //:\Program Files\ByteFence
		@"\\?\" + Drive.Letter + Bfs.Create("pDAvLicxmpfaibqr/eN1JNrVqzWS6Zp6yGoNTM+dobI=", "aurTa6CYR6RyWVuz41R4agcR3bHr0It4LhNBpdkC2DY=", "Tjn303DGAAMI4B0q/pH4nw=="), //:\Program Files\CPUID\HWMonitor
		@"\\?\" + Drive.Letter + Bfs.Create("u0i7a2eSrgOeA4fEaGvu8J/I92D53YPJkyavm4gdG2Q=", "WxB86+snhMYjK7hJREGOTImWC9Zv7PoPB3HUk21GiX8=", "kTfPhT3c/o/vIKcrI1Appw=="), //:\Program Files\COMODO
		@"\\?\" + Drive.Letter + Bfs.Create("36FhOLDM3PyGhCGhTt2pstKkGOfslkeNRpRSLZUGPno=", "BPP4CWoMCIuQc/mN4pnIhLsfvcsWm6AXK9yJZ4FekCo=", "sK6gM5iEIpckqJlanF1TgQ=="), //:\Program Files\Cezurity
		@"\\?\" + Drive.Letter + Bfs.Create("o2rDFyu+qCCAKIInQJCIzq2jpLBgHN02ZhsjlNcQcOE=", "ZvpMuGI8yy0k2mDYxiYHZYnvZZrnLdNU9l5ugcNtKYs=", "ERp5qDzg01OwaoGXHnvUJA=="), //:\Program Files\Common Files\AV
		@"\\?\" + Drive.Letter + Bfs.Create("rzULKLmRm2fQmrsxw4KwKLQBi1oce4SvyWEPre0Yk9vad1V6+p8k9X5FxievCBy7", "fzGBLX5ECIKGtSQG3JRGO1N+hcp00Ky0mRabrK4G5Ks=", "aO1qn9pPaSp5aMW0Z2Yq9Q=="), //:\Program Files\Common Files\Doctor Web
		@"\\?\" + Drive.Letter + Bfs.Create("tWKpdoEdkz5C8iPQTiq8NBmDdfrCH6birwbr4BUkbk5D7AmkR1mC09eBhRszh93t", "InFL4uNm4mjSxDHBKYEDZ+CjfMdRanNESQjyOoonYX8=", "ouCphu+PXx1/w1U44yIhPA=="), //:\Program Files\Common Files\McAfee
		@"\\?\" + Drive.Letter + Bfs.Create("zcnvG3bgcaoGPsj5LwPaE8eE9Off0SrKxq4FiUURyWM=", "iyb/a5Cvg8ScZ1DyJEyBSlmA9cuda736uixT+6e417M=", "WU/ylSOUYLOLZaHkcTYdRQ=="), //:\Program Files\DrWeb
		@"\\?\" + Drive.Letter + Bfs.Create("BQaAUGp1L+qWGqvT+Nkzr1DZiXgFcqsgS2vEGKn8/CM=", "g2dfpQklT3aAxxX7Gx5sfkcYViv5Guox3+4UhtJckrY=", "vP6B7bbqM/IUAzFiarm5zg=="), //:\Program Files\ESET
		@"\\?\" + Drive.Letter + Bfs.Create("ZfjrmJXbJEicH1txy54k4jIFllFe4xzKZ5QQIsMIuHY=", "rfPYwxXKwC60ALIbu3LAsFQvtWNTY/Wb8nRq83N5uEM=", "fvPC6ltv60HFCgob25hrpA=="), //:\Program Files\Kaspersky Lab
		@"\\?\" + Drive.Letter + Bfs.Create("CX19w4wqj+Sgtp6M1kdzRIUTpKDTfEe09UF4c1GDDXdM92M3mOEKOQRWCrKftIJ5", "++5UQq0wiOD9NsqTXObLNO/LG9w0+UUGWCjDS/sE5ZE=", "kjitkBKG4EMjy4J85lSw6g=="), //:\Program Files\Loaris Trojan Remover
		@"\\?\" + Drive.Letter + Bfs.Create("eyvoXtGpaBGozUn8Fa90WooXeNt6Vda3L2bXMrr6Ipo=", "6yFMqwns6Nse2giaO98xVSKehv9DmgLJsYEZtCbo5iU=", "3+7VzwPo3Sf4M/x23KeVMw=="), //:\Program Files\Malwarebytes
		@"\\?\" + Drive.Letter + Bfs.Create("xd21uKT9HRoHBLN7XnQqN/D24BZ2WPeQ29ifNGYMm6o=", "KfnCWi3NXX2KhkwKJC8xGfVsaSIkooU4OWDYDHBRW14=", "68JflUnAnMVtEOxJMLfbsQ=="), //:\Program Files\Process Lasso
		@"\\?\" + Drive.Letter + Bfs.Create("RJYtS553E9/XV86jl3U6zTENjh4T8NdVwKFS5RC05qw=", "TTl7qRCUP4qK43f/OOlAdkez9Xg5VmwAVcVkG2FTUeM=", "i8I3a9Xesn9bjKk1Elz+qQ=="), //:\Program Files\Rainmeter
		@"\\?\" + Drive.Letter + Bfs.Create("sZ67jV/830nAKQrK772wgwDf8//9TPz6/7r1b05f1Fg=", "QUHhYdN6DH160t4nINcC0rWsbElL54LKLX4Ep9KWaP4=", "K9JIHm9cHwd4L8iNf5trBg=="), //:\Program Files\Ravantivirus
		@"\\?\" + Drive.Letter + Bfs.Create("CrsF9EYVnHvSev8GE9in+xL/rxGb2P5jFxEO4RcSMQl6vNGUzNtpQMIgDapuDKvZ", "n0ywcjSuS4f0V1CYp2CCC6FCEde+/B3Yfc9lOfH95j0=", "3wB5jJyTdaRW3FH7wKMtOA=="), //:\Program Files\Process Hacker 2
		@"\\?\" + Drive.Letter + Bfs.Create("jf4qddcnNIu2kuL6nh+LFLryR9/Fy2jBi6jAENCqrX4=", "rFKU0P8hekvWjV1hDTtr3pNHDBlSZXOtu2PEpUij4b0=", "gPFnXqd6CuEoNH+oGRrjAw=="), //:\Program Files\RogueKiller
		@"\\?\" + Drive.Letter + Bfs.Create("N5jgDmjYuG8LfFup3XcGWQZCMFG4dhsVML0CVNviQ/KSvyPe9kVbx8UuHxmX4nS1", "/nsvIc9ut9plaGyKFd3afN5jjj3JXIpOTSS9K9eLRq4=", "zPb+F36sWBfsbOMwrHh5oA=="), //:\Program Files\SUPERAntiSpyware
		@"\\?\" + Drive.Letter + Bfs.Create("Jp1Nn40xpEcGyxkIfKSW/GU2g7uZUtS4bETPoUsZwmY=", "jue/M6KKh4G422Ce9hMl37mjnflXqPpRm5HWV7VYDhE=", "YcAo3STD1MwQGTusp4dhBA=="), //:\Program Files\Transmission
		@"\\?\" + Drive.Letter + Bfs.Create("mmMGWxIKjaQcPJK8wyK/wHt9VQ0HRZkHt6G6hNeeYBc=", "Dbc2N6rb3bfJSldHljWHGwIqVzVaaAFLPxjcRHN2LEs=", "tSv8JaDK2zSq4Xld5OyBHg=="), //:\Program Files\HitmanPro
		@"\\?\" + Drive.Letter + Bfs.Create("GTftee7uR/noEOl7i6F+tF2oWHekYD3b+4bxfiiEBhg=", "bYNsILLfq0hBmqUBaGd/AzBLAI8lvjjJ1fJUt7DuJO8=", "27z8vLLH/fiy55hSSfkALQ=="), //:\Program Files\QuickCPU
		@"\\?\" + Drive.Letter + Bfs.Create("45yjiUoP/DJqufKFcjlqe1BntzXgU6T1JOJ5FfDwXgg=", "dtBz/e5Xr6I7p2qVPmjuJfp7y4Qt0hvhbWy1qmX3wpY=", "DspMyrcNqFdyDWfpp40j7w=="), //:\Program Files\NETGATE
		@"\\?\" + Drive.Letter + Bfs.Create("lXcT5YaFc4If9qt6IOHXsPWuvuLBQ/geJ+J+Vg9eUq8=", "ikVMy7ZQIJBB8COSNGNUX1ZFVvXkGGo2f7PlaenzQ7c=", "Dk7LV5tpFHxxV3x4SNPZiQ=="), //:\Program Files\Google\Chrome
		@"\\?\" + Drive.Letter + Bfs.Create("mZpgbdibPd4kRxLRDt+S6I/M+2pHa/9gNX1BM1hNt3M=", "ULdx5r3JDk5cZ2CJai/efEGu15ciC5ERt/L9cmblzHk=", "w0BQul0uJ1LYfV5Ta3mIWQ=="), //:\Program Files\ReasonLabs
		@"\\?\" + Drive.Letter + Bfs.Create("LxOSUoO7RPcz+sG+knfhjA==", "xtoHs8TJuf27HXn1kuGxAmJpT38Ich29QqcJEcLR+VA=", "O27IYUgZxYfnMTGTxOzlGw=="), //:\AdwCleaner
		@"\\?\" + Drive.Letter + Bfs.Create("a2aQMlb0UAySMmC67aa6IA==", "eXR7DJ9l2DJSFeug/0hvpKL8DmiQPL6rb3zuJfRix9c=", "XVOt+2j1g2dRu0+qhKzOIQ=="), //:\KVRT_Data
		@"\\?\" + Drive.Letter + Bfs.Create("pg3YMD78EmHggyfdwlej2w==", "6qkYbJ8SMwsQwXgdo8Z1Zd57nQSv0Zxhaz4rKsptJ9A=", "Qcei/aui7XoAglC7PuuZhw=="), //:\KVRT2020_Data
		@"\\?\" + Drive.Letter + Bfs.Create("HG+l+0OZzXqS00oUof1JFQ==", "DjglabDzn/nOLlG76+JJ93QR3OHixkICcvEPjURDxYo=", "qLjw5tRLrfd3Ul67xCOjqQ=="), //:\FRST
		};

	public List<string> obfStr6 = new List<string>() {
		@"\\?\" + Drive.Letter + Bfs.Create("6crCgAQ+NwtbusoV0K6RUw==", "zYDePSHY+6c/0ptG5jTlL2M57IGElqg0F2lWem4macA=", "1YaQ9nDPNSXNAAo5jxyQFw=="), //:\ProgramData
		@"\\?\" + Drive.Letter + Bfs.Create("mYrx7JgLAWBZqrUoPv0fGg==", "1eKKe+7acnD5QwB2XiDUFipUReemRpdDWkM2Dy5KiOQ=", "cOixhgwo27QV1IruLpZLeA=="), //:\Program Files
		@"\\?\" + Drive.Letter + Bfs.Create("3eM8HYeKI1dP1HiiIVQ1KJ0yBpju2KPwpRPA1mXnbn0=", "kCNEFl8oy0BAbb2WFDsU7nOQIY2mFumXAWdZqwsMHkc=", "7v56EM2OP/swDNei6E8WmA=="), //:\Program Files (x86)
		@"\\?\" + Drive.Letter + Bfs.Create("84rqgRa481oSGwJD4xEhzg==", "e0FI3lmKDjggLZjx4SqghIuq9Hk5eWuWWTftLySZjGg=", "yXj3a2DN89p0gmlPgFotlQ=="), //:\Windows
		@"\\?\" + Drive.Letter + Bfs.Create("vhwlv0a4DxZDgASQKKb/xw==", "vNA7C+CxHqk2TVNqF+T0yUutfNjWiCDrrHyWC5Gq1TU=", "uA6HogDwV7fCP9mpOpo6eg=="), //:\Users
		};

        public List<string> aclResetForbiddenDirs = new List<string>();

        public string[] aclResetForbiddenFragments = new string[] {
        "\\w?im_mou?nt\\".Replace("?", ""), //\wim_mount\
        };


	public HashSet<string> badArgStrings = new HashSet<string>() {
		new StringBuilder("--").Append("al").Append("go").ToString(),
		new StringBuilder("--").Append("co").Append("in").ToString(),
		new StringBuilder("--").Append("pa").Append("ss").Append(" x").ToString(),
		new StringBuilder(".p").Append("oo").Append("l.").ToString(),
		new StringBuilder("mi").Append("ni").Append("ng").Append("oc").Append("ea").Append("n.").ToString(),
		new StringBuilder("na").Append("no").Append("po").Append("ol").ToString(),
		new StringBuilder("-o").Append(" p").Append("oo").Append("l.").ToString(),
		new StringBuilder("-o").Append(" x").Append("mr").Append(".").ToString(),
		new StringBuilder("po").Append("ol").Append(".c").Append("om").ToString(),
		new StringBuilder("po").Append("ol").Append(".m").ToString(),
		new StringBuilder("re").Append("g ").Append("co").Append("py").ToString(),
		new StringBuilder("st").Append("ra").Append("tu").Append("m").ToString(),
		new StringBuilder("st").Append("ra").Append("tu").Append("m+").ToString(),
		new StringBuilder("--").Append("mn").Append("r-").Append("ta").Append("sk").ToString(),
		};

	public List<string> JohnPatterns = new List<string>() {
		Bfs.Create("8bTo0L80tjR2wC2T7okyTg==", "FbynBtdwKedG0HZgCIVFKVKJqDTfzWgTRqGW/xWN0is=", "OD1SmenAmhM8txwZKU6Ldg=="), //winserv
		Bfs.Create("yNexiBYRlIuWnt9yXsbsaQ==", "benkEoxOhIYWHHELXllrsn+wzVeIdm9F2tPOAFp+fHY=", "uUPh24J6l9q6Y4q/kOuDjA=="), //audiodg
		Bfs.Create("HY2Jh49fA9B25PCbQZk9Wg==", "NA1cslrLnQPvlUwMo/EmDw0rhPpupHCFBNWkqjMILuU=", "LpbbQV1b8ycxe53WHRpfsA=="), //MicrosoftHost
		Bfs.Create("FhaqFMJvWoSh7WqcI+yF8A==", "lQHevc5NPdmxR31aEvRIKTjDIoQXqYJciwi3BRI6Prw=", "7fs4BkRmuSzpts/fWw9vPA=="), //ReaItekHD
		Bfs.Create("RmnoYYSAN9otxCNTDH1qpA==", "jGHTKI8Rv8dOLKTdbhwx2TTZFU5ShCkwPAzgU1TFxXA=", "+9OXrY2cYjrFy7N5P1X9Zw=="), //RealtekHD
		Bfs.Create("uUHKDowiva/BejFItnnxzg==", "dVMSca/OPeH86I6/9qljBdr8eDHLbmhXKaGvdbQOHh0=", "3EHHVq53P4aw4XZjoyHf7Q=="), //WindowsTask
		Bfs.Create("XfSoK7uK3Zllh9RRfeMZVir6B1SZTvWykmQTLcyvZys=", "Cz8EgVG01YuX71zMiJfPr6XF/Xegh5RYRSvv/ppY/DU=", "ZJ6bJn5eoOQ8NzZ9ltvGQg=="), //Windows Tasks Service
		Bfs.Create("ch4MsAmPhp+PC02WT9FD4Q==", "3eLhQ5vgvDmfgNlL6hNt2echKzPWGrvByWYX7PkwnB0=", "0lJtvwTyN73HRbnyejWOtQ=="), //RDPWinst
		};

	public string[] shellPatterns = new string[] {
		Bfs.Create("0DLyDUA0XGVMdtoenkRirA==", "i0SNbEHqtdRweCj4DD3GFPaw55Xmz/A2+PBFCQ/2QwA=", "F7KS6KNPqsVwngBsb8ue5Q=="), //cmd
		Bfs.Create("JgQX1s70i+eP1t4ZgkUR+A==", "zlCMYjC5IraP4ObxywqlWZc8+88oz7xozzHFJYFqXpM=", "7qW2eUO2bVHsmW73ijcgqw=="), //cmd.exe
		Bfs.Create("l4iQWC1QednvvVzsRIl5IQ==", "wgWlFHNIv4RbZsJtQOB+mPeVOYeBP+ue3swu+v5sNDw=", "L1zDyqCbJskIzgLJf5mDMA=="), //powershell
		Bfs.Create("a+3hR/Azsoa0rIKiJUjmjQ==", "qip6gmEfkJJ/6LzuxTifJBrZNRmj87iyjPshIbZy5Hw=", "F27kEWfTwqp96w1FtGEqKw=="), //powershell.exe
		Bfs.Create("Enbk+Jip3iJQGX94QPHpIQ==", "8f1C+gdHZNppLb/UIn3uFktWCom2V7OHq8ogh++MfJw=", "dYYu/KsTD2Z4ND/JyMlERA=="), //mshta
		Bfs.Create("Kezf2kvsIwekTmVwj0UeAQ==", "CO07Cig6HSU4jIbUhluGFdaZQTF10VF6N6Yo5zgc18k=", "a/Lig7EAzi03ju/rAye3pg=="), //mshta.exe
		Bfs.Create("JWLOic2opmCsAccAc5hyXw==", "LRTWwpYnrQDdzlFjg9G6MDiqyHAHRdtUia8I8AsQoO4=", "QzzXK3fftgX86wjf3cF9fA=="), //wscript
		Bfs.Create("MZDCPk2aRSlud0Rrx4AWKA==", "iSp4oIYvt4RMAlA3fEyoZNu5QZ1iEVnHfLy2jPh36v8=", "kcWDZI9r+N/hOtYmtoc16Q=="), //wscript.exe
		Bfs.Create("FEa4Om79N86IGLzFuKcQbQ==", "tDyD6UeEAp8lkoQ9y7sVGAKExCRTVv4Sk8buoAmAods=", "mza7CDQzTiU+EkSMSUu1Jw=="), //cscript
		Bfs.Create("s7jgB3UyGh1Pn/sLBLUAkQ==", "939fkTACA3LnNGQDNCl0uEjZXo9drzE5IaFuTjaDgWk=", "EYO+d3ljVB0WLP9ImrU7Ww=="), //cscript.exe
		Bfs.Create("KOyY2Qrchbfq7YIMcmZgBg==", "2X0eBsrZB2+OmNpm2m39vdAvZ3q87rcls7sgH5LMi6A=", "+3JebnD0QT7qB/Xkp26VcA=="), //conhost
		Bfs.Create("3yH7Ce5FEfsSurnHBCMylg==", "zjulaPxjbcMtMHwIBNyz/XO/Mm45PW6SGELCXVbwfIE=", "W8Jj5YlL3zF599T0kmRIkA=="), //conhost.exe
		};


	public Dictionary<string, string> queries = new Dictionary<string, string>() {
		["TcpipParameters"] = Bfs.Create("RT8iyfIUDFNQqE99RKOMUr4iY1SqAN0KLpX3bQn4QoFtf1OvjB+ABbnzSWQP+udrBYS4oNGHyNuJ2M0NL1Qm8A==", "H0YxcUpLqX3B+tv5kmTWxqJ+5demBiTUwyXUSDRSufY=", "QrKZZ8GR2TLISCBDspowwA=="), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
		["SystemPolicies"] = Bfs.Create("TRgc/gkMKLLBcRUKi7Z1MkNFLV/1Wu1uQ/XskSRaDgV0lGklUOb/K6aciZuB6nufGHqM3PIzayhGiEboJun2yQ==", "xYK77XkNiAxF5g12dLsueZq6IQ3bUYAOgOjuC9Aww+A=", "UymsIUBNpHYrzJphcu++Tg=="), //Software\Microsoft\Windows\CurrentVersion\Policies\System
		["ExplorerPolicies"] = Bfs.Create("Xnkgb1UvfrgZHpxJ2dlS5PG3zTyCI9xEES6GZ06WeLBHuHArlRy4BkKEtuMagr4nhO6aKdbL2Jf9zjgoAXSm8Q==", "qz2pEM4sx3HSyoyZqI3FkNlyP5d3eiPKFBrYlQQc7W4=", "/QgITQkrgnZa1lYDeR4Gpg=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
		["ExplorerDisallowRun"] = Bfs.Create("wHc6YLwGv6N4IH2hIyHDpC0gZpyN+9Z5h3SE2Z/ovDwNhRS0qCNke3RHIXJVxErauNTbfo9aEatuimHY1G3tuekGl8cdjsThM5Avsfokr/Y=", "ZbB1s9sO2HkER78cERlGenAcS1KsCI06O4d2z6Pv5BA=", "4G9bQR2ijCar/dnalXMFcQ=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
		["WindowsNT_CurrentVersion_Windows"] = Bfs.Create("luw9b1zv7BFEd3kyh6ekOXTfCp7Jwr0TdMA5KrlNtl1phSzH3o8dNj/PQGVAleHQCzW18z2izgDzNWoFkUa7yg==", "1x7QnF7CUQumWiy9BEuvmUaynnZkjyO9n1czfMIo61c=", "0rmTyf3yxtG5NsFMm9TiAQ=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
		["WindowsNT_CurrentVersion_Winlogon"] = Bfs.Create("LHHK0YBQurE4IOyimfyhqAP0vHg0y1cJorMg9YOE9ZMDV5RKY4y57ZAYHeaH+QMInD/P5MBPUY8uMZ83YK/kJA==", "qCuu8XHfJTG51UvwiNyYcTgwLM4ms2V9Un1M2SZm2/4=", "zhsjAz8jDSCN84DV7Pm5hA=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon
		["StartupRun"] = Bfs.Create("tFBffswavQHdafnHwH5twGrZtAS//k1XHoHQpnvQjt/llnA2KaAqtzt2u6cplZ5E", "MWIGgzZj0vO52thX7CRZ/32S89s6TBn/xqH+7lJmHrY=", "rORcqYb2Y1Ii3wz+0Udw1Q=="), //Software\Microsoft\Windows\CurrentVersion\Run
		["WDExclusionsPolicies"] = Bfs.Create("w3O4sqApVYIZWEoVyYovMd8fF19uztuFYFPIzFzG4rHMoYFlgIN7Ps0xmQIao7jCVgPNrVYt4NAHBmEGojzjVQ==", "YHDl81htzZQ2owNAFt8mpPgGItsQlZJLLBa4JVxtPQk=", "abLd3R1mye5Gbh1eJDc4Vw=="), //Software\Policies\Microsoft\Windows Defender\Exclusions
		["WDExclusionsLocal"] = Bfs.Create("tSziW1C+fUcPpEC37BZTqx1VM8OFfNvmMF1mSrz2sqQ/yGxX+JHdDX8j3zv8i2pL", "Qdbw+IEDhnl/dGHT51hoEue1e2lBRGAIzmRySm5PBjM=", "jHf5GrX+V7Ys+8XgCLeyHQ=="), //Software\Microsoft\Windows Defender\Exclusions
		["Wow6432Node_StartupRun"] = Bfs.Create("BnoGsaSM16DkQuE6iyeozTeV6hiEAebucz/P2x1pLxqvMcWH165f2xBRd+2FswveAvwFrDtkwzM72c6auAPM5g==", "B/bWdcSPD2zeFvphsdWeLw1R0lIqjHDBhU9e6NYa0iQ=", "EjZZcnyZoSFaXVyU5fosKA=="), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
		["PowerShellPath"] = Drive.Letter + Bfs.Create("SzfFObYPwnqiROo9JhNI7WErsW/bUo7IlgbngXG35b9yrNTSkc0sGSEXTeZyCqih", "W/k4I3dF6w2qdfmmscXwopdGGwmYE1k0FPWV0NMuzuU=", "CkEUIsINf9qIqnzRWE+ODA=="), //:\Windows\System32\WindowsPowerShell\v1.0
		["Defender_AddExclusionPath"] = Bfs.Create("Y0zncRhg+ctUj5R0mTVe7CJbsN2hhfsj1vrk1FQlhJM=", "0t8VWYdzhRsZcN4rEYCVitgW/SiA2eiYNAIa/Z2ITPY=", "WmVaYvnI/rJ2m8DvJ9uAPA=="), //Add-MpPreference -ExclusionPath
		["TermServiceParameters"] = Bfs.Create("wWXmNMQYveTfqiQAgQfBJuAjKWaqFpFEGl5oUrr3iHhbWdNbVCHE9ZKDea8b9S60x2TG4yc6Ae+huT773LrKxA==", "u4xJjx75JDY1YiG9jVHMYU6n2SoTjuwCY14bT24ZeLo=", "LNnK9XUNO558gMl7i6qk0Q=="), //SYSTEM\CurrentControlSet\Services\TermService\Parameters
		["TermsrvDll"] = Bfs.Create("VR82l/keaqECUjVC6bK/fFf6J9th1dxa13xw8nlS08N1R26DzmTnNOMiAsafbSzZ", "gO7eAhjQTuMJciUSRQiHxQP73RfSBgMhlAy8kOJVGqs=", "QAdHXU4CjHFjV8fTyb8qzA=="), //%SystemRoot%\System32\termsrv.dll
		["IFEO"] = Bfs.Create("v3GyRYOYg8cBYP2tKpSNLxxHy9pDWbo27EQuZvk4f1u8iwH9tXCrII37JGaPqUT5x+hwge7PRnhnzjFodnMHG2m6bao4mId2/wWMQqmw4BQ=", "l/qDmKo09ck7bBdoud5XmLStdgvn8vXXMd/jE164yP0=", "5MmsGeH2cAhf1b42Y78/fg=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
		["Wow6432Node_IFEO"] = Bfs.Create("/+TgMO3Bsy/Fxwd539C9ZX6YdOBglWnnjCtwxt2ox0i/beamGSbN0v1uqXF7ohaE2qyu3ZQP+CixXGWxqBnSe+G5bp2utj342afCTdc0ioEWcvMOCDqbIqB+RreufTPZ", "/+DCr2rLNmzciKi3+8vTcXbGIPpdbazsNM3UPmhSeow=", "ZVmU+awAL0ei4CxfoaoJYg=="), //SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
		["SilentProcessExit"] = Bfs.Create("8iDUHsZK+4fNd+v+C0KcTdytfQmiCYov9fECBW2OLiKLq0q1Igi6Xrcze6dksR4QB77mTv6Y+y0+cCKee1JkBw==", "LI5vQpwUl6cmrpmXqCw/cCeVXY3OvLnU43IpLdFih6g=", "CyQ5A5iB1zA1qUpO6sEe4g=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\SilentProcessExit
		["h0sts"] = Bfs.Create("4v6gMivHePtIqV6wCqxNH6bmLWrKZZVJTbrcxtccuky2Xt5YrRpmXusPy1oA3/VA", "wlDl0avSzlB/DC7ZQNq721RTJFHLZCD9n9bF+C4B3e4=", "00lgaJ3TBfNMxqF0zyDDRA=="), //:\Windows\System32\drivers\etc\hosts
		["appl0cker"] = Bfs.Create("lOdRYsTmxDgmzwHIV699Rx2r6in1dl/BPwVfoHY1oovBXNQw2RACjrkOZ9/KTIie", "MNHOZ+mSFYmbZxK6s4gPQhXv9PS/O6l32DdFLAPHBaY=", "FlxOqCS/Fq7yQFfse0U8/Q=="), //SOFTWARE\Policies\Microsoft\Windows\SrpV2\Exe
		["Tekt0nitParameters"] = Bfs.Create("qK0d/7Utv/Wq5bW1rK2lfT74kn0TPmdGJgq5UV6Ql+3lCwvFoSvcjmUsstlfve6VCNzTEACUMzoe5d5VHFcNYQ==", "gSA3yrXskS92NLUhUBlr3jZynxlpyRuHQL3NJoQyfN8=", "89g0Jivn0e7Hn3Cvf0Yu2w=="), //Software\tektonit\Remote Manipulator System\Host\Parameters
		["disableSmb1Script"] = Drive.Letter + Bfs.Create("tGYzZBhHysPzadV+KLf/sBVq68D62MMaGJJPq996CQKGX5DasY7zR8F4P1YwOuzR2Z00pytB4spowNEYv+vSVSOuAwIar+DYfbNY12bGrDA=", "RD2c0sZuJFuA+2iUjkxzcIjtj1DfDl3ILuQ3SSHIERQ=", "+TUjDxmfFdUVaSVgHpeI3g=="), //%windir%\system32\WindowsPowerShell\v1.0\Modules\SmbShare\DisableUnusedSmb1.ps1
		["LsaAuthenticationPackages"] = Bfs.Create("7HXlJzcTCn7mt6Sin0UNBXmc4O+sEihQf9SD2B8e3IFKO6ZCOzLOTMPOhPhV27kW", "o0m92zpgIwIXckQ/otI7KFky+VcFjIKviRBqvtyok6I=", "tJh6/hc9SHZDtJr/4Rddaw=="), //SYSTEM\CurrentControlSet\Control\Lsa
		["AppPaths"] = Bfs.Create("cuFBl0ssb908qHK/Zy73xRLaE620YCPA61FfoD+b8QFdDAHUmH7GXfZ+K8/6WQRdVcPbF//oxH75dXJ10Y3yYg==", "vSwnPNbcMg2I7qnIN3LnIzdtGHMgeXTKADr557uGSxo=", "Q9TfvqNT1zgY1nXmuGcuBg=="), //SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths
		["SafeBootMinimal"] = Bfs.Create("8VDCqA2vHhW3Ct/6oz3MlUOZoeGnT65A628SMcZBpQdGi+ZfajhgYvmrjaXlCD5zdgw6w3bbV0s1v8Ae4zfwYQ==", "FgNoDmq/Lm4LxAvHB2knubMmG87q2BIyx3fb5Ng/fXw=", "YQf1IfMujK078JFIiwWmjQ=="), //SYSTEM\CurrentControlSet\Control\SafeBoot\Minimal
		["SafeBootNetwork"] = Bfs.Create("ehsyw1cE1NB8MMh/tGmpjh2pzyyTLEzrKx+zMV20MAlHAMklcF5y1t2jzSfkOzBvaoDJu80MdKwm9LbHjAiLsA==", "VANAsFazXL2VFAbuhBAEJWqHzuf8GOwGXtbuzcZqyVk=", "2aUN2j/Ee8PtxZJ7nmoEmw=="), //SYSTEM\CurrentControlSet\Control\SafeBoot\Network
		["TaskCacheTree"] = Bfs.Create("S3m/uFh6ziUIhI2/SHmG1OQfRtWNQ58KF7zzHR00APzAuSkuYbBu0YarVxAk5IgVWoomNzipMBtoKBrclrW5gdjj/RV+0rq5vTEkid6focY=", "5ux8GjxM2z+VkjOYEQq0MiRy+MrX9krilIUZE2ATNf4=", "j0n/2WIXM5EjhMrEnRNhQw=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Schedule\TaskCache\Tree
		["TaskCacheTasks"] = Bfs.Create("hQRGpsblRE8FIRA01E4U6PUT4vJ6VMyxCGMI8PYDEZJz1Z7E5uzJDzsqnoJEUCQWZ0dk/RvVy0Oq6cBaHsikx7zP8C8TmFuxDiNM+G7Z/tY=", "BEfaH/gAfJtk40Ms1CF4iETzCQferbAY+k480YFMoRs=", "g0ft+fmBC1bfXcmU1ukwyA=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Schedule\TaskCache\Tasks
		["ServicesBase"] = Bfs.Create("NDn/msRpwe5nAOjlvaSFaINHjiMP84zekQGTMqyWk8EkF/ep54AVKGgWtVo2g1pU", "311YQHaqaf913eMjh+RVe8lIxNeTb4alvghajOCBmg4=", "7HL9iU15hfLNGbt+F5+P+w=="), //SYSTEM\CurrentControlSet\Services
		["SpecialAccountsUserList"] = Bfs.Create("CsXsV3q6sb1Ze1erO5fHjmOG3nwcz4QSzSc+nPR2fUrdtojRisCOKOPHFhKg1qmvr/CS8Ux56xrlccHCzZSIPVlvP9p9knplHfcSHQ7Ks7k=", "cXEqnJGmqVyrE0eLbkkaiXTz/QTgYGWz+HNeJtYVx3w=", "HyorAIZVMs6x3vtDDeIG5A=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon\SpecialAccounts\UserList
		["SysWow64Dir"] = Drive.Letter + Bfs.Create("Iv7Dcyo/g8KwRb7OrYAeP6K2BpSv6BpPl5QR6mou+aA=", "d7MIPcW5jrBVqIWcqXq1qHZFR9e2v++OrVMV0v+G3R4=", "AFCaP85vNfi0JghTHrsvbw=="), //:\Windows\SysWOW64
		["WbemDir"] = Drive.Letter + Bfs.Create("9E/slIQw3DKnXjCVaZVGynEvx2DiNg2gmaUZdOD482o=", "8Q5m4XsZGnXg8y1q7d1IZ0xoHzzj9aEPS/n4y8QHFxU=", "TAIjZn+KXFuX3wdYlkQoiw=="), //:\Windows\System32\wbem
		["WDBasePolicies"] = Bfs.Create("jSeiLY8AFrP+aRCNrAbnInqkRH3ev/86nIu2p/Cgzb+cssYtaKbWHD64dhXVP+y+", "AQR5z8TiVPQ7uFbc0xXangZRkmZDjgJssUkj8L24pVU=", "jNMF6XmxJX7P5tX/478YEg=="), //Software\Policies\Microsoft\Windows Defender
		["WDBaseLocal"] = Bfs.Create("o5NpiFwc3CuZ5zCtqXEbh7Lt0SOQYKr4majwAA8bUPgkxg/VB2MhNGxP+pDRmpcp", "lzQzIFHmxTXPPWl/mEe2B2T/9I/OkVrSlCuVayXjhJ0=", "b5Vie2zcaJuBQvLzuRe7qA=="), //Software\Microsoft\Windows Defender
		["WDFlags"] = Bfs.Create("KjTWlcZoIGanVXjAKTPqltev9dfQD8TZiikazFPsjk+Jrp/bt9iAyrvRhO0BGppO", "L0maoATQ/qJoQXYtWdanr48GVqvWd8e4ap/haAOPPCw=", "SrGv+qpb8Q4G1NkO9mJlDA=="), //DisableAntiSpyware|DisableAntiVirus
		["WDRemoveExclusions"] = Bfs.Create("/zUFVTBbYXZIJKtYlEbRxzR9Yv0C0mpNjr7GHXFWSDGLQ/FL+dpvcLkTgKgvl1oUBb2w/4BjnFIg7eTG89X0FQ==", "2+egqGjYCQVAmuotVTdVSWlm8NIrI0DyHHKfiHS0bUA=", "h2qDLhFOzWoaBDx44yYeXQ=="), //-ExecutionPolicy Bypass -c "Remove-MpPreference -Exclusion
		};


        // >>> BLENDFUSCATOR:NEWSECTIONS:START
	public Dictionary<string, string[]> markerSets = new Dictionary<string, string[]>() {
		[MSKeys.ScriptMarkers] = new string[] {
			Bfs.Create("X+jsEiFHDVRS8ZlUUdJu1w==", "IGxRyDOCyXcrv4Hcn93mleXxutlt5feCVYvt7wBwBV8=", "y0Ky9BY/6j5LyUfrIw4Zjg=="), //javascript:
			Bfs.Create("5fZw5ZUXcanDC0414rI+hA==", "2X52klGRnGp6jYvla1k0VqYlzmRS331KANPhAtaWBqg=", "9zvV+YLbO5cGbc6qcVtnkQ=="), //vbscript:
		},
		[MSKeys.UrlMarkers] = new string[] {
			Bfs.Create("jiycmbls9ltTXXS2HGgckQ==", "9rpsUM801OUeEquMauefZLuSIYdu5QfohA3jRH5hURY=", "ac/57FrNSD7Bza8Ux7eg7w=="), //http://
			Bfs.Create("yChsVMKpTQiriZPIu2ivDw==", "3c8qEn8fDEVHgnXmSy038IyeV0wWm0ADqyMv/gs+LxI=", "/sbwBITDhNA/mVbp1EcdLQ=="), //https://
		},
		[MSKeys.HtaMarkers] = new string[] {
			Bfs.Create("x8SM4u0mvkS2ENeNoDFo1Q==", "ga1cehHpfpGGa3dE+7ul50N0iK1QidQVcj09DuwRL2o=", "uu+Nlzy6pHZIS6zJ4AVbAg=="), //.hta
		},
		[MSKeys.FilelessMarkers] = new string[] {
			Bfs.Create("gaa14C7IITfTuM1nNZQ2zyHoD38Rp8116NjzDclGbB4=", "e5k+6IegmQ6qWrEzBq+7WatwBUz6M3S12OdKlzOYB8U=", "8OB+BIdtxkiwhVsEGNz6cA=="), //[reflection.assembly]::
			Bfs.Create("DOlrqXeofbrGs3x2+cFE5pT0hZUrAujdr+sM4NXAKSw/evI2GbQ8ZjFguVSM8J7h", "EqP13x9ngoClYFNWVL1DlIkaSx2SDQvMz9xNltDwpI8=", "W/AnPV3Oq3qmAD+vs4HQmg=="), //[runtime.interopservices.marshal]::
			Bfs.Create("b1/3waibegZRRvoRlhvTufxE2UsfIoGu2lbGks9g5Cc=", "/vE/S0EtTdGEM8enkPN4/+Gzj+I1pfW8127ce6/F+IA=", "/SSvzHr/ZNy/LledHWNIDA=="), //[reflection.emit
			Bfs.Create("r+rl2NwquVDYvzGIB3IV/PqXx9UdVWH+KX6Acv0/wtA=", "cqmnqoJC7hguKBajEmO1Ceg0hpxo3VfRjZDKPLDI+sc=", "rzp0/Z5npqjOv8Wq8Ph4KQ=="), //[microsoft.win32.registry]::
		},
		[MSKeys.ExecCallMarkers] = new string[] {
			Bfs.Create("owBNbMbODmY1x0SdnJQFAA==", "isvYOWhPAc4NIa8pK9JlQT0NMsUDrM7HL4Z4M2v8PwE=", "p+wDuDK4KNAtvAQ31josRw=="), //iex
			Bfs.Create("XVeoNCkvEkUX894Xlt8CFWBcCr8i8MKt0X8VvugZ7U0=", "ko8Iq8czzmadvQjZGC1MKZzJHdVaEqrbIOhmL6fqf10=", "yvTWXSfrlC4AuoXwH71t8A=="), //invoke-expression
		},
		[MSKeys.DownloadCallMarkers] = new string[] {
			Bfs.Create("GeNeFrP6yIPDTLRtYkCttQ==", "VCChAqLVUXinmq36E8yG8e45Zz/GDr45o8vx18lNDUU=", "JR4B7dcrwe5rlr5RzogRhA=="), //irm
			Bfs.Create("Nyh/1LIDcgBtGQvXZXSn7nPmGvNJQi7B0AdDL8U9l+w=", "V1g8xvI3quzfog+hG2ko1ykJxN+6iJXrIJKdJ0B8ZT8=", "6EU1zL7PkI5sjLahaETNvg=="), //invoke-restmethod
		},
		[MSKeys.EncodedArgMarkers] = new string[] {
			Bfs.Create("RjbSdrgyWZsV+9c7kid9fw==", "IG0t0nAbMcuD5JynLmZvrwrs8NPhfBnA2gsk0ng4CDY=", "cH/opuT21mVHZYnLD33Lsw=="), // -e 
			Bfs.Create("W5EJxN2Zid+gRTj7E535IQ==", "MRb/dfPvPTuyMHPw7cfGmowknps3QohJRNsWEBYMf9g=", "1ZPWZIo8a2QP8I0Vr4PToQ=="), //-encodedcommand
			Bfs.Create("EsNmNuXXtL4dZ30eyfFbKQ==", "gm1qLI9BX5NPZTcSoV+4TRZzTu143+P8+dT8/JEPPms=", "NYBy/rLMCAvool067or/YA=="), //| iex
		},
		[MSKeys.ShellWrapperMarkers] = new string[] {
			Bfs.Create("l0DhGBUzbxQhbNlhirirJQ==", "j4n0YsL7XIwaxbDNs81ecyXxyxxp1syBsB5Na4CrL1g=", "zE4qpRSxLuBtVP9zE/Yrvg=="), //powershell
			Bfs.Create("ZHigTng2wHmGQkZbsO8HLg==", "GP949/NLa1FDjnTRNSly4mSp6ILB3FmutydWKu6v2k0=", "KXBy6PbbB/d2aoAXS4orxA=="), //pwsh
		},
		[MSKeys.TaskCommandMarkers] = new string[] {
			Bfs.Create("XialrgVJh0kJEky68eSDWQ==", "SG53V+SD+hjNbCxX/556UUL7jIoLSmex6e1OdM+ihGU=", "lwrxBk+a/ouSwlPBOmMS5g=="), ///c reg add 
			Bfs.Create("98g3aqTtuzyhLrI/Sn4HVdJLKY9lgOAj3pYnE49YXJA=", "XekmikFtg5yE0P38cBq+34DgrpuJfYdE3sDNsE6piEM=", "v0r8n6nCpTFItPcfhBhhzw=="), ///c echo function 
		},
		[MSKeys.AutorunChainMarkers] = new string[] {
			Bfs.Create("cYxhTducYQtr+9skm/ukew==", "4HXHKFnEShAbAJlcYnAWwxoo68aFDYAB3jmEqWThenI=", "JUJfa9ZBj+5oASshU4/F/A=="), // /c cd 
			Bfs.Create("JlPkIaQpUPKD22LtzFZmvA==", "+FAq2NTx49x6TLSvXMGn8yrlBl6tONa4wZaj/B5L1eg=", "/DsZKFl+wmsgPqy54HkTKw=="), // && 
		},
		[MSKeys.RegsvrFlagMarkers] = new string[] {
			Bfs.Create("B4YbS3phTlG/kukjP1bc+w==", "O3Ss6RJmrWLp0H3NS8z2v13RRFStYBZ+2ThDKDCd0WU=", "Q4vVo7i9hV8rwAUNBncKig=="), ///u
			Bfs.Create("uW07zSz1hBLPGAauizA6Rw==", "S463huKdvr9RyVEUE9Sy7Cz3it7BL/IuHa6db4h1TUA=", "6BL18B5yDoj89cYa7ic1rw=="), ///s
			Bfs.Create("KW1mS182kYPjYPGmyX1h2A==", "pbSuo7iDE0XRWSEYK9ANx07RxSgqascguLU13IxXDyw=", "X7b9bk5OWwI5IBdvzibPyA=="), ///i:
		},
		[MSKeys.AutorunWeakMarkers] = new string[] {
			Bfs.Create("lIQI1tgJr13rhubjQDUsAQ==", "KcLbJ+DGuw9TNIyvHHEDRUwRd+/CVR4PPHs3OwyzbrQ=", "kNuLZUDvfWNOuxGtqv3GuQ=="), //explorer.exe 
			Bfs.Create("RVyT0qq1zLw9JPtHj4vXXw==", "Ij19P8fjl5tbuuQD8fKIDzYEjr0BSWj2d7S6N7CgoUc=", "WqtVct92SSMulXAe1crthA=="), //cmd.exe /c 
		},
		[MSKeys.TempPathMarkers] = new string[] {
			Bfs.Create("6PYftClZ97vKfTMm5qBkUA==", "j9yB9Ju6JIDT2Y3UfwD2A1FKkOW6qfh1juUmL25pj7Q=", "y0xNLqnzdwg7LvvVxqRfJw=="), //%temp%
			Bfs.Create("qca8MY1xCtOfGcGS/OMf9Q==", "hwgfGSP4Rl/3MQr4gbxK5t2l2qX3ehJdpAgP1LqwQFg=", "u00JANjN0wAomV7p6cE8bA=="), //%tmp%
		},
		[MSKeys.WmiCommandMarkers] = new string[] {
			Bfs.Create("8ixV1gdondeXo68pKsWStg==", "CqAWOauPq9JZr9DPp/+4w0bte/1aW/80+8r+abtsYU8=", "UT3gqu/w7Valh8dWLFt+6A=="), //cmd.exe /c 
		},
		[MSKeys.RealtekTaskMarkers] = new string[] {
			Bfs.Create("J4Pd1BxhP5AUSEUsDzeiXg==", "nEm9zThfMZ2P6oryW8wzOIv0F65CT9Cdy/mEG32UL4c=", "p1otRqCbCR5Q/cFU8j5yvw=="), //RealtekHD\task
			Bfs.Create("k5mLHHKboQ8+vGJkDqz7pg==", "aGmkTku956/FNLJzYmcFlNCSMdwloUjAdHE5ZKsiDts=", "aYnNehEPNTme2O5rH+rARA=="), //ReaItekHD\task
		},
		[MSKeys.FakeDescriptionMarkers] = new string[] {
			Bfs.Create("lYHdYrjVYMPLdyZpB83PWA==", "Z1vA50DGqyN+G82OqgjgChdEC/liQCRzPEMMVDk6DPs=", "hf11GYgOlJEBGBvYZzhYXA=="), //svhost
		},
		[MSKeys.FakeOriginalNameMarkers] = new string[] {
			Bfs.Create("1kH5wdWz+1+5JvGcpjgCrCJcixmL5VuluR5Ug24y5sk=", "W8OLDkuw7/Bvvr+usNY6Yf/MIlTohOtWtH7wfl9xYzs=", "UBEwbr624ToSwrvTQaVrOA=="), //SpotifyStartupTask.exe
		},
		[MSKeys.FakeProcessNameMarkers] = new string[] {
			Bfs.Create("CCivSVN2mLraioDjLUFYWA==", "K7hpb0Beoju3kvfF9sKP2nxT9pwkAHdM+kSkIW5b6yQ=", "yLa1nPJuZ1rkJiT4l3xlhw=="), //rundll
			Bfs.Create("oyxF141Ixj3M82ScRl+0hg==", "ysmifQkjvrw2TYSmWAe0CtR8aX7YzQf1lmWy2YhL3xk=", "5jDfqQyEXtjwFN1MhQV9zA=="), //system
			Bfs.Create("MlZE9eO409IqAj4VBYXrag==", "igdX+50/F39pmCZfCWAxZswVzX2xeOncEDnC8CoUW8U=", "mfk9DNAP1NieuJPMZMYx9Q=="), //winserv
		},
		[MSKeys.HelperMarkers] = new string[] {
			Bfs.Create("aKmkck7ALoCA0Cu8jGjASg==", "GwpiVGXutC14sOH+9nghaNVdASXoo6d1QLZLtAdjuz8=", "cQIRN9F5PsdsC2DxzA/Phw=="), //helper
		},
		[MSKeys.FakeTaskLocationMarkers] = new string[] {
			Bfs.Create("lF+wBs33ajlzra8e0c1HYg==", "mMEbE1nVDe1pa65rD9gRzuTuvWKLL0eqlMO0hIwTEPI=", "iGE/uNzjtSgPBFz19PSVFw=="), //appdata
			Bfs.Create("mbR/QTU5hHIvbz652V4bmg==", "7clQZ1IIp/VyhsnFWwY22cGOj7mjZFpISvsAVRBXo0Q=", "bacvv2rZAKvOGvir8PQ5Aw=="), //windows
		},
		[MSKeys.FakeSystemCheckMarkers] = new string[] {
			Bfs.Create("K82U4kfnhn2y3MxFdVg2Dg==", "olel3eZhfJo6rolqewFWaAu9N0SahXG5imlqy2nnoug=", "f2BfLp3xO9A5YbLjZmJYfw=="), //-systemcheck
		},
		[MSKeys.RegasmArgMarkers] = new string[] {
			Bfs.Create("tbIC6GCobsYe9lmYIwS0Jw==", "JmonfTO2jC//0/s8G1R6SDU4SEPG4E/mvd9fAVyTpqI=", "0I7eFFssFP4sL7/x+6pHlw=="), //#system32
		},
		[MSKeys.IfExistMarkers] = new string[] {
			Bfs.Create("Bvgxjz/f8t+khE3hhLPsPQ==", "Ksh67X+YT2QVzdFcJJeGwjI1DV7nxDEQ0NnLFCSbpuM=", "23Kj+g5mX9OjJt1s1mOirQ=="), //if exist
		},
		[MSKeys.UserWritableMarkers] = new string[] {
			Bfs.Create("Lop1XZN+SfCO61uhD47DwA==", "MpTCmMBdS8LtBJYe8gEXGES1/mt4VWYlp8IpHkc9o1k=", "55uThd5NRJ1q5hom+wpTIw=="), //programdata
			Bfs.Create("+BxN6FZgw5x66otGXUM+DA==", "0NrChsOQpbQc76sb2F50IsimhPAGariH53FCuGKJJtg=", "0IS+ewm11yHBGu9lr9bDng=="), //appdata
		},
		[MSKeys.ConhostArgMarkers] = new string[] {
			Bfs.Create("EHCHB3K+eaUTqJYatMBFKA==", "5UWXxCDNMCmC6r2HtjUqQzszxS7yM2XbXpG9AJSPldE=", "ZRBEbp3lhCn6yMWNAWESTA=="), //--headless
			Bfs.Create("2RA33crk7R3/UX+pqne7LQ==", "+habqjw7PXvRRj/U3N9LH9q0k3DG7pe2az424KXyQ/c=", "XO9m2jxKGJzqa8mLAQmuog=="), // -ec 
			Bfs.Create("mAY755l9XVi04m2MBGQinSaXnZMnTmhD0whTXYTD7tc=", "KfgXQ95XJkINrOOmAyWiciSeTqfYN0dywRy62nM/5Xc=", "fJxrEaGdvMgs6gESCNygKA=="), // -encodedcommand 
			Bfs.Create("+ZK2PU/NkPl9IV2km78dtw==", "ptWKJcNuPsIc5Ea1QfVL0nOVxg1q+J5121gGFBkpxrE=", "JattwjzYnfsbwZrNcEf+cg=="), //appdata\local
		},
		};

	public Dictionary<string, string> regexPatterns = new Dictionary<string, string>() {
		[MSKeys.EncodedCommand] = @"(?<![a-z0-9])-(e|ec|enc(odedcommand)?)(?![a-z0-9])",
		[MSKeys.TempDirSegment] = @"(^|\\)temp(\\|$)",
		[MSKeys.WsclInTemp] = @"\\temp\\\{[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}\}\\wscl\.exe",
		[MSKeys.RandomServiceName] = @"^[a-zA-Z]{8}$",
		[MSKeys.RandomProgramDataPath] = @"^(\\\\\?\\)?[a-fA-F]:\\ProgramData\\[a-zA-Z]{12}\\[a-zA-Z]{12}\.exe$",
		[MSKeys.SddlDenyAce] = @"\(D;;[^()]*;;;(IU|SU|BA|WD)\)",
		[MSKeys.WinCaches] = @"\\windows\\caches\\",
		[MSKeys.HtaPath] = @"[A-Za-z]:\\[^""']+\.hta",
		[MSKeys.IfExistPath] = @"if\s+exist\s+(?:""|\^"")(?<filepath>[A-Z]:\\.*?\.(?:dll|wsf|ps1|bat|cmd|psm1|psd1|psxml))(?:""|\^"")",
		[MSKeys.IfExistHex] = @"if\s+exist\s+0x22",
		[MSKeys.IfExistHexPath] = @"if\s+exist\s+0x22\s*(?<filepath>[A-Za-z]:\\[^""]+?)(?:0x22|\s|$)",
		[MSKeys.PcaluaArgs] = @"-a\s+(?:""(?<filepath>[^""]+)""|(?<filepath>\S+))",
		[MSKeys.GuidFile] = @"^\{?[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}\}?$",
		[MSKeys.GuidAny] = @"\{?[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}\}?",
		[MSKeys.ClsidDir] = @"^.*\.{[0-9A-F]{8}(-[0-9A-F]{4}){3}-[0-9A-F]{12}\}$",
		[MSKeys.StartProcessArgs] = @"-(filepath|windowstyle|verb|workingdirectory)\s+",
		[MSKeys.NodeScript] = @"(?i)""?(?<script>[a-z]:\\[^""]+)""?",
		[MSKeys.ArgsSplit] = @"""[^""]+""|\S+",
		[MSKeys.AbsolutePath] = @"[A-Za-z]:\\[^""'<>|]+",
		[MSKeys.Url] = @"https?://[^\s""]+",
		};

	public Dictionary<string, string> regValueNames = new Dictionary<string, string>() {
		[MSKeys.AppInitDlls] = Bfs.Create("GEXsy6UYMFLqSvTfsHqMYQ==", "mO+iIh2XoK93IIRx+L2RFjcv4ZChDIYk6ILlZeT8htw=", "YWUxycRsHdIOzHszqFTQzg=="), //AppInit_DLLs
		[MSKeys.RequireSignedAppInitDlls] = Bfs.Create("PXCIEA9h0apeRnLVQtgWPIbZjwCH0rsizfn/fhODDmg=", "k88IR30cHzZlS241ODHKeet01YCGj9RpWwFFNUodpkc=", "jXliYJnQVpXpFnpU9ZkGNw=="), //RequireSignedAppInit_DLLs
		[MSKeys.Debugger] = Bfs.Create("EYaNnd1+jnFEzdTPXNSlOg==", "VE7gIWYI8it8Xfus3dJADDDVOTA07fh/xOHdt7UuCjE=", "FJkWfD7uH6kUAXf6l9z/SQ=="), //debugger
		[MSKeys.GlobalFlag] = Bfs.Create("BcSq/5yhOfq9f41gXWRJRw==", "OgN9Z+ymY+Yons2lZAHuMPCIEoSoXmNZM+mAQdoykmk=", "kQzD7b0eII7sRE7KcLe4Qw=="), //GlobalFlag
		[MSKeys.MinimumStackCommit] = Bfs.Create("HySB8E0iM8/b4DsRtq9arZPbxRP7+EQC1AHO42rAGNo=", "qWP0d9PEyuIlA32os8o4UKw9WJ3utflqpg2KMSV86ew=", "yeRvEsMcO6tGl7vCb2fG3Q=="), //MinimumStackCommitInBytes
		[MSKeys.MonitorProcess] = Bfs.Create("TCA0jFt4JE4PUWf0nSKMRQ==", "4KxfKMyPtnqfTGVFU1Tq0DTuFatCN5Top8XiOdaahbE=", "AhS8CVKzOaAQSGQJEbhNcw=="), //MonitorProcess
		[MSKeys.DisableTaskMgr] = Bfs.Create("vgedo57Ams24UaenbZln8g==", "/ZAMzMDbeI5P4kfOxhPf8VSvJesk6wnAS3393dFVKfc=", "p854iZk5KRhpscWuYPnxUg=="), //DisableTaskMgr
		[MSKeys.DisableRegistryTools] = Bfs.Create("g4mWRR6Q46zUNexTRsh3jIbq/6l88GjS3k8T3o8XmY0=", "xHIC+T+AmHsFEmZO7uTc17AHtfZa8YMQvGxrVo015TY=", "Yytcq7wt11cOP1tGf5Rz7Q=="), //DisableRegistryTools
		[MSKeys.Userinit] = Bfs.Create("uZlGnodSBQCpdCKtMSb4yA==", "EilrQ+IDSzISuo9uLQkS2V5p73MyKamld0he3m9E0fM=", "Nx1RIYsd67v2xcfTqz5etw=="), //Userinit
		[MSKeys.WinlogonShell] = Bfs.Create("OwstM6bwGpl7joPEAnkOUQ==", "qvkJFPoonnvynDgaKf0ILko+dRlXp7L5fKV4UHBPWMc=", "VeVkeEEP2c+0uu8clUTxbQ=="), //Shell
		[MSKeys.FusClientPath] = Bfs.Create("KjSJc+LQGglgVvf46AAI5A==", "bRyFzZt7m6zfI2aVF2gOYNTimjgtRdVhW2W1imnyhGc=", "Fg1IHC1unTNZjqI9Xqr0IQ=="), //FUSClientPath
		[MSKeys.AuthenticationPackages] = Bfs.Create("AyCRGAu2Qn2FgZo6tdhNaFjGG/3SP3F0Tv+Oz5LPRc0=", "wGk++O81xsFJ4NWsbwfTi0TkoWvzeomQTj255nelS4A=", "GzyiHhNSawitz0/jXfgUIQ=="), //Authentication Packages
		[MSKeys.RunAsPpl] = Bfs.Create("SOZdHaWfXKB+CbkO0eRNCg==", "zn3KCdc7evAgNteAyIn9Cpx8bO3NgSGFZJajXxUErGg=", "1APNkSgpqP4Qw35eDVwqgg=="), //RunAsPPL
		[MSKeys.ServiceDll] = Bfs.Create("JAzy83qaGyWM0hYqBy3D5Q==", "Z1GvK27WaHpJuhsfDfHxOjaeVutQmvCAFgONsWayMLA=", "HMs1ky+scQKfTTCPoxPa+Q=="), //ServiceDll
		[MSKeys.Parameters] = Bfs.Create("je5ICpOtNVi0Y/kd7wx5fw==", "qNgYF0UR0s+QYXR3jug0/TKbMOMqiw4C1b5CAsZRuj8=", "97vkekabP2i6DovF/2Jy5g=="), //Parameters
		[MSKeys.Start] = Bfs.Create("ih49lokxdlEM2mIXkSO+SQ==", "uDjw5liiZVxuTvcnGbKYBJH/avmFrC06I2q8NZ+dwWM=", "UPliSmiSzRgUrgj9AmTH6Q=="), //Start
		[MSKeys.Default] = Bfs.Create("Ytv4i/TJNPtFjURs9Ja90A==", "U3d15+8zUXS62R55Zw9cC5rAzYcB0PYpr8LAnxPmmkk=", "nN+4Gcadn+SZFF8CSOkoYA=="), //(default)
		[MSKeys.InprocServer32] = Bfs.Create("ngw+tCADxL42z3bQr3bGZw==", "cf4q/yeZogpEEoqUf+rbR33tNobhHtO0bK5bmetHtpY=", "7/llE6uTb81dmvr8FMW3YA=="), //InprocServer32
		[MSKeys.DefenderPaths] = Bfs.Create("rLtqmHjiCzMTMJqVRDRdsw==", "MIugeitb3fjWfXJW7SaKzvyz08tyrl6PYBZK0QHXe98=", "Ypy+l9fzb1zGlBPCY1AD+A=="), //Paths
		[MSKeys.DefenderProcesses] = Bfs.Create("uIFtJVnBbsKSnFDAqGpktg==", "7fan30ultpV3fmokXfsc7XZzjfFbjADbSwv8P+IIXOs=", "gfi0tArIFuymdqbNESxvBA=="), //Processes
		[MSKeys.DefenderExtensions] = Bfs.Create("p1Aa6noxZcAfWsh45WcS5Q==", "84PrRnCjI3D0w/HFCCawpH8G5xEN6oH4/b4whaa0Reo=", "Js5gYgyv3jdQRlYBgfkDIg=="), //Extensions
		};

	public Dictionary<string, string> hostNames = new Dictionary<string, string>() {
		[MSKeys.PowerShell] = Bfs.Create("1OeDacatsvrehhmGsa0cBw==", "I3QI80gMyoB1SRp5T7Qc1UVj8GhgA2pMINiSMp1+FrU=", "4d6NIhNbhsuUc1p2idRvtA=="), //powershell
		[MSKeys.Pwsh] = Bfs.Create("R2XmXDMJG1/1Oe15jPSCbw==", "s2D35FZdo1bvaPn4n3C7WQNckUbf/oHLGR41WePSdII=", "pTCKtkkEQydG5AxNULHqHg=="), //pwsh
		[MSKeys.Rundll32] = Bfs.Create("A2QCPwbw/asXxVtsVIJpsA==", "pEqZL/NZLf++IN+Tx0DyWJfDQGzXWlkZZfqUJetKSQM=", "h3NQhEPJxboiN4ooWoWWaQ=="), //rundll32
		[MSKeys.Pcalua] = Bfs.Create("2CRUgJDSskBCOjnOJg5IMw==", "PFT98u0Ao9r0rZnLD16q3DnUWCKOAUFsrF3aUjj7kOg=", "W6snnW0EKs+lZ+tkB1icRQ=="), //pcalua
		[MSKeys.Regsvr32] = Bfs.Create("4GNs/ZxmT0W1w+bMFuZWOw==", "MjO8hE8d2uIYkn5DHqzjn4XXEA7fN/Lpt/UJDP4sqOo=", "optGZMaQOhOXCJV/5DbF6g=="), //regsvr32
		[MSKeys.Regasm] = Bfs.Create("VzLQJxeZm1EGUMRUBPvCDw==", "yRTnJjXmw9VYZZVtu4Y4SAR17MAVssWgVSxJ6YoQ2ww=", "+LpmP4bBkSmhrACzKdaPYg=="), //regasm
		[MSKeys.Forfiles] = Bfs.Create("hKJnWOhWAzhF7Xs4FCvW9Q==", "R+gLGFkaqlsuufRXlcEN+c1juTfwq3MAgfNNcPxzf4w=", "BEN1PcaDc378nFAB0Im9pw=="), //forfiles
		[MSKeys.Wscript] = Bfs.Create("IJFkbwjmtivT26LeqM/QIQ==", "N4ywEu186mFXYr9eNHl3hWqn8206nXlW/Nh4eMtNQQU=", "l7otvowZyQre5vGvaU84Kg=="), //wscript
		[MSKeys.Msiexec] = Bfs.Create("QiHZELqkEBnBobC+OE07Dg==", "AjqMh+YJLm9zu4uhWhU1FDU0xP4gnNnHrZEtFHLPDFg=", "xyBLIZJVRCDUVliBMC/iEA=="), //msiexec
		[MSKeys.Msbuild] = Bfs.Create("L7P9sDat94UmdwRcIHOSMQ==", "sBknrWhK43GIo3DVUBEVRjfulwQ39MfFg41PH94ITaM=", "AEXHsy0K8cV84TYMQHi/ig=="), //msbuild.exe
		[MSKeys.Conhost] = Bfs.Create("/URs92NmkQhR9mrfvJ2NUQ==", "drEcZQnHRs1z8q7i9QhM5Pt0UUXeLgSkm5271Z/qzDE=", "GMDuvQqoH9d7ojFMrZ6AqA=="), //conhost
		[MSKeys.NodeExe] = Bfs.Create("S5Yg9Bu4Q0D7bkfIFxqtFw==", "lX4KAWyMU9p3BZAY7pFAsW1/Wng8kegq/xiPFjM9RYY=", "PoCue0nFqNDjk8U5UGb0Iw=="), //node.exe
		[MSKeys.CmdExe] = Bfs.Create("7BJI0EXckA32WMlPSpjjbQ==", "9USJBIcSm/VANIm9mOKWmmmYNSIAmp6lbgdI6vGgZgg=", "aJeW1o2htxXSuI1F4P0W7A=="), //cmd.exe
		};

	public Dictionary<string, string> consts = new Dictionary<string, string>() {
		[MSKeys.FwPolicyProgId] = Bfs.Create("cHfOTPeb0aHbQNloWQ1eURleewDy0Vkn6+hlP01EtJs=", "OvieU20IB2j/fB1RR/ETWuxjMrhdHtGc0aYd809TgUU=", "1NZvmZnyi7h9RFU+/veFTQ=="), //HNetCfg.FwPolicy2
		[MSKeys.WinDefendService] = Bfs.Create("NvflvAlWiwZZ6kQSGA5RSQ==", "WPYBQnnAfcMmxNayOausFOdN/+ZSbjgS+2eypbj+14Y=", "ycdS+Me+Us8ApVxkF2pA3g=="), //WinDefend
		[MSKeys.TermServiceName] = Bfs.Create("SbKCb+wYAT0ieCTlgbhcCQ==", "N26yU90w7IU5mgc9GIeG2E/XHEvCHTT7dVJLTrrqe/M=", "Cc7FxxykCFluE647ssLgXQ=="), //TermService
		[MSKeys.WmiNamespace] = Bfs.Create("7WY/yQfJUjdhsJT/nFglqEX4SpRS9NKlJjIZoUloTPU=", "o2oGM/b6DYxk+O4LiNoqts+RVx3sX7+p3jIaa1MbfGU=", "K3ejcLhri9CJKkOAKQ1rpA=="), //\\.\root\subscription
		[MSKeys.WmiQuery] = Bfs.Create("LWbpkm4ACfsZ86OkzJx5dsAme/BeqqX75Q1jnu5A93ANKmLHlfkefJEfj+Bg9isw", "I7KHNTTGo9lB0Ktk8mouGYcvORj64KvDgsgYevm0Kgg=", "3tnQ8MovYIxnKyRZmNy/7w=="), //SELECT * FROM CommandLineEventConsumer
		[MSKeys.WmiConsumerClass] = Bfs.Create("TxlxXqnY/j+ES/b32jATTnwHPuZkw8iw5i+zGPT5uYA=", "gSFIx/6+CTp5XhpTzrCzm1Bp2ZqZ9MKuqAh8Aqpo22Q=", "4PcIkdnKaOWXMIbnf8fKpg=="), //CommandLineEventConsumer
		[MSKeys.UserinitDefault] = Bfs.Create("/JATCPKDS5Q+J4LfYlMwvwWm71P26YoFmswhpLZRGyj+KLMuAoTMluNrIQWWvnzj", "EwN1Nsi99/55LCvX9u86dh+FhEivbG4GG+g/bwEe0kU=", "b4SLpIgOTApJxkj/0D0yaQ=="), //:\windows\system32\userinit.exe,
		[MSKeys.ExplorerExe] = Bfs.Create("IxrAlQg1fyytKTohXMUYrA==", "t+p9BjAP+wp4LTm++3e0nUT83DLJRK59i+ErNbGjAdM=", "HkVO6QSOoATkBW/2vYiHIQ=="), //explorer.exe
		[MSKeys.ExplorerExePath] = Bfs.Create("81TBVqvxdIJBRPrGYQXA4z1oX5fdG4UzJT5j4r+EWfY=", "xp0sDN7e1gKus1MksBg5O+mGIUC1cdVjV8I9/9NwwSo=", "goxThLyhJiQaLLgFlOANyg=="), //:\Windows\explorer.exe
		[MSKeys.Msv10] = Bfs.Create("zAGNxcH2xi0Qwi8di3V/ng==", "yVO8bxLHD6qRgDy4r2yTQUOX0CTpt7/V5pojEtalqN8=", "xMbg8SA+QpbxEWoBsnbwHA=="), //msv1_0
		[MSKeys.ServiceCmdStart] = Bfs.Create("zbsneoXLQcfwe+SKvQkdACowX1WdQbfRHbnwj0Qt84s=", "1NF8VDjhAxZxrCn4KsLUzFcGVw3XJNNld6Pp/w/tSG8=", "AWIra+84ONhZnIKvK+Z44g=="), //cmd.exe /c start
		[MSKeys.ServiceGuestPattern] = Bfs.Create("9Y62Aq8y/HkWRgqtmPIOQkrdz2ycKEmsGgJdMAvRNPE=", "YpgVTzWp65b8OiWHY3dfFcCnBGY388xJZgf/eZ0DGEY=", "AmRgIRtgzw0G4flm5bimTQ=="), //e=access&y=guest&h=
		[MSKeys.DcomLaunchArgs] = Bfs.Create("dUt5uWE2BByXOl/NT4FeF3WmFDoPwrUK+ECUj8gmWNU=", "FUuijFODG51Ommgia/yryxpCh0NZqfan1RqcAnr8uHc=", "x0oj/csv90/0QzgaNqxDVw=="), //.exe -k dcomlaunch
		[MSKeys.SlashCArg] = Bfs.Create("KuAsY9TiuTHLHRqBNJXOZQ==", "eFjq1DKbfgJ3EC2VsSxE0eet3Zp+P9cpXidyjov0whM=", "7YFnxpR7RkvmNqqZswQktg=="), ///c 
		[MSKeys.JarArg] = Bfs.Create("6gr6xi562/hPr7C3V4mzEw==", "YbHoTK67G44jtCSA7Twng7lTFWhWou8ZwNM1Hm/L1zw=", "DLdBByQY/Fv2ApNQUh4STA=="), //-jar 
		[MSKeys.ProDirMarker] = Bfs.Create("WMHh/kgY98rENvzKKW8/9A==", "Q2XKHTK7vf7cDrk6NaaKkxTbQYvNu3B9x3aQ8spJ/YM=", "4aXQUdrFK7pyuxYkcCbjSA=="), //\pro\
		[MSKeys.LhsArg] = Bfs.Create("WfjBVpAXzDlbU6CQO/P8jg==", "ePjd9xYzjSv+4m2GWcyHHgnQXTmdj3Gk7yOdDP8qZnc=", "QmE1vspAaiBtlbwGrzEf6w=="), ///LHS
		[MSKeys.TArg] = Bfs.Create("WOmfwzqVMl51egA2DP+3vg==", "6/jEN3L/NcXTjKxuXVf/wgyI1YJV3YzDmQj52B1+z4k=", "Dc+iSZwBdLeiX3VPetPnBw=="), ///T
		[MSKeys.VerysilentArg] = Bfs.Create("uViOQa0GqWThuOFpItNUoA==", "zj5EcnHyq4kqRsIY/xuOv+fVBG9GRZTkqxVBfAUWYw8=", "b9R4JXQUI+W/Y4UeFyhUyw=="), ///verysilent
		[MSKeys.SddlBlockPath] = Bfs.Create("gAP7qPM2QCaRBBcZZaiVMv6q8ZZprTNPRda6SGSGyGM=", "abgRkpvP/lPUOi6v73X6Va2lfA9qqxaTRJUeH6KvQoo=", "kD/rOGJ5R71pDZ1ooV9iig=="), //\\.\c:\programdata
		[MSKeys.StartProcessCmd] = Bfs.Create("Kw9mAJp5SeFdToPiGZp8gg==", "kNtPszjIuTdtKGGeU/hXgD1MXP3mflX4XxXYE6Kk7i0=", "mqtKqH7sU2c7oyrAsObEtQ=="), //start-process
		[MSKeys.ArgumentListArg] = Bfs.Create("7URISfJ/2YRLl9MgOW64jQ==", "ovBVP0IMXAqKcfPlZYKMUrRaT9I6J8C4wxOKX7tpHqA=", "IisjprZAgLJnTXWThbsVNg=="), //-argumentlist
		};

	public string[] systemPathFragments = new string[] {
		Bfs.Create("YyE9bA9pc+8ov79srbac7WlgjUKXjrXvax1YRW5SBGU=", "MiWb80zF2ihiaez0HM5QErHULbPp4KAzKUztKUdX6BQ=", "eU3NwWJ/LIvn4N2uz6k7hg=="), //:\windows\system32
		Bfs.Create("S2bEiuBtMzMavygUCUd94MO7n3/lWVyJuwVgFSNYjpM=", "mg/6USaY9Kj1gO+VsE6ngoLAn5H50/CZpY/46VHEWe0=", "Xw7wamQNk7gpVGzDPRrahQ=="), //:\windows\system32\wbem
		Bfs.Create("/LpFQhWPs5In+tMf2Q1Q1LNorl6iz+n3VaENb3WlvmU=", "mTwHqsQl34GKc92XGXafJZIheMayxB7xPYULpmG1H0A=", "iAXc60fJS1QYvqUFrvCIEA=="), //:\windows\syswow64
		Bfs.Create("zO9aQSGCu2bK4crXAUucRWK7VJdKmjlxIWUeaCPxmcw=", "NOXGLEfPn8qbRDGlc1JBWiP/+CQ6kOAdN/HLzzDDWHw=", "miOqHNAiiHU2n2f+aUhc4A=="), //:\windows\winsxs\amd64
		Bfs.Create("OVZpv+0fhkXxhEMzXWMiwjQG6PXCOizdbgk1E2bQgLk=", "NdEi6SCHWrHwMISRBdxghWAjhlzBidw3NydRbsyKK+s=", "F+f0N+fiOln30c/yfeE1nQ=="), //:\windows\winsxs\x86
		Bfs.Create("9p3+yr/vDxEcVYxLn97vjYT7b/H9Eb35x53mgbiZnr44Y/kjcFlJGLAfgyx0c0oq", "m8lrxD+xB7XNu28Vk5kMx1T0/p09D3jCHKtCAfOi+yY=", "hc+hVK22B+wYEOQOeG2kFA=="), //:\windows\microsoft.net\framework64
		Bfs.Create("HDhRLWfUbkPpEyn0/irgGRKSw5ETYZwTQ1aaokzkj4o28Qpragns01Dgyb9cjfMl", "JzRV+MQ/dbgaVmgtAITXHgub+1cAdv/hZ/xCYIAsEkA=", "mGOEjIENQhunx6zh/M0H2A=="), //:\windows\microsoft.net\framework
		};

	public string[] systemDirFragments = new string[] {
		Bfs.Create("L01c4HZUeeEwVz6Dv0WwmQ==", "1bO9M3c3PA0UQqRLMfsNPZZ/G/h0PiEmSdt7EAggoqo=", "CfEiVnbYDTQGGPefw6cq8w=="), //\system32\
		Bfs.Create("NOlv7WkREneMtrvINSJeIw==", "FCKuh/NcSBPzG7iIqFCraCicNYhG49Ktl75tOmjM038=", "E0wuPx2T6W2xHw8Xe/DPWw=="), //\syswow64\
		Bfs.Create("PQusLvMHfZwAQiWh0lCrmA==", "S6U2EAluqq6WvQVpJXbtT2jKxD3RRr4wcuX5OO91N8g=", "w2FwnP1GGnwz2gmhlVifOQ=="), //\winsxs\
		Bfs.Create("JRmzmenIu79MtLyb2PLYrw==", "GKFavvaMU+5/hzuF0sQ31KlEhi890M22hilzhMYQCfY=", "u61W2q8NieqTIZTeTvG6TQ=="), //\servicing\
		Bfs.Create("d1MXgu1v/A9Fy/dIa1Qd9w==", "cRtha5ows0t0wmsRaxTsr3ylMBWFW84Y5bpaFFJwNuk=", "YS/igilZoN+ml0dKcGpb6w=="), //\microsoft.net\
		Bfs.Create("oW+MRBG1d34RHRLaoIwVhQ==", "+d6je+x/wyVFLnG+S/NziudZTz4cgiKCpb/DuZB/Kno=", "JYBzRHeJ+updDD94PJpkQA=="), //\assembly\
		};

	public string[] batchExtensions = new string[] {
		Bfs.Create("54Mmy1ApcMoYju8qAk9IGw==", "VmtFKKYgYbj4bt2661O9QbR1iRjCL6Brix/Zd7ruOis=", "VVPjWmEtcTwKDHb3UfY2Ug=="), //.bat
		Bfs.Create("Rino/L0DpbM8FH33HIwXvQ==", "UmmxQ7dMaCl6VBfmmRWlHEWfcpaKq27mWqAFlm4f0+o=", "KVQm8+2oGwGZjYac0IK10Q=="), //.cmd
		};

	public string[] certificateExtensions = new string[] {
		Bfs.Create("nMt93fMaMKvVRb2zpo1psw==", "+xlcfnuCAAOEhgZiaaLK9pgMBrECQbXwLqQTUaBlIB0=", "E0A+CV5vrD7E/8Fuzpe2Sg=="), //.pfx
		Bfs.Create("pdLVlLHQDbw+PRWPdhs+bw==", "afz8OjHz2s4IvdL8fBmV9Q7/FZGAumDBCUsEFwK9yCM=", "KUSc/SmzTf0S7DJfhhhd9A=="), //.p12
		};
        // <<< BLENDFUSCATOR:NEWSECTIONS:END

        public List<string> suspiciousUserDirs = new List<string>();

        public List<string> exeForbiddenDirs = new List<string>();


	public Dictionary<string, string> conhostPatterns = new Dictionary<string, string>() {
		["convert-from"] = "[convert]::frombase64string",
		["invoke-pattern"] = ".invoke()",
		["policy-bp"] = "-ep bypass -w h",
		};

        public string[] r77SubKeys = { @"dialerconfig", @"$77config" };

	public string[] SysFileName = new string[] {
		Bfs.Create("74IFnSnjbGyGoQfHeqZ7Bw==", "CStvALzu/XxryEWs+aRSXxhYcZOA2hNSAix46fIOOuQ=", "DsopCvjBJcVMvikSb51IIg=="), //audiodg
		Bfs.Create("XFCzIExwBeMm8sxbhAxPYA==", "CyIV+O88fWGnrxPDGO9P5PdP79jN4KYNKBiSidtJ5Hc=", "y3dA88aMa49bbjCjG8weOA=="), //taskhostw
		Bfs.Create("rVk/1rzVsXVE2MYqbg4mSQ==", "qQoY0dTMgoaJ8e5+w9oEFvjKvTbeyddlp7H/s7gIlVY=", "IvddY16Hidc2qhWX3j4jaQ=="), //taskhost
		Bfs.Create("Ijw5Ha5wA3VcDdK57RL9mQ==", "ePFSCca2JhteJdELgmgW1iWK1q6f7igRRvd3RWyZD5g=", "ajGd8yT1yhq7o66d3hHfbw=="), //conhost
		Bfs.Create("F2oihO3sTh8uicQ8eGwLLg==", "4uPQz8siDxU4kqQmR6kXo9mgV2wD0NRBoJ2Ocl8nV3A=", "wvylyAuEI6MroZJjzCfJIA=="), //svchost
		Bfs.Create("37EirDuI1Ah1vwWfzQ/Tqw==", "Hyh4YuMjoB+4q5FaS4ExSfCsL0UZGtVG5L2YNeyMEMM=", "wJO5l1A1IFF5EJTYhSUQ3g=="), //dwm
		Bfs.Create("5HdZzp/0pWNbHmZcnDrHLQ==", "hrPtidGrKs6qT0wfpXxa6B22gAoKT/wNlfh6LrcbBqw=", "J0+EqFnw97TNjLzBzXL5IQ=="), //rundll32
		Bfs.Create("/i3UTDS8JjW6mqj5jg9Euw==", "NldnLjNaP7+cqWuVH/JjOoHaKipXRuozy3ytzcoE7u0=", "YIf7DGMIn7bA3laqI4Fycg=="), //winlogon
		Bfs.Create("rt3JQfyCn0OPntCXDH95iA==", "jeBRrXK1AuEXwgrUSRQu2tbjG+jbDuZ06ak2FHEEAGE=", "SuQfjI8LU/9XJZQuczr2QQ=="), //csrss
		Bfs.Create("hvg2UBDIyJ4+I/hMmtGebA==", "UhUEir2ORLXR/VSS8uImOerrA7fLYubyKFM7eldCnVI=", "yKUlQYvTZ5wJx8OW1gExZQ=="), //services
		Bfs.Create("0Q3/SRmytteOGzSnj5nJSg==", "d1wtordGvjokEHU+5/enn7WbbHHi4mwCA0IZ8nPDmIk=", "D+JnLvFEfWmpoM9UhaOUtw=="), //lsass
		Bfs.Create("DXth+IIwwiTL+18x4PU/hg==", "KFa4vvLZG83JvdqhVFsmYdcrE6hHxWa8vU6wWx08Mzk=", "qM4PzP9iO5jOadwHcHRYXw=="), //dllhost
		Bfs.Create("tUgHhoUNfv2iEUXGBEUZHQ==", "6VyaYgfNabsUzV0p5OeSsGHBiA1YsOWbGFKY7ndX2sA=", "xfZuEcahxxzrvL9sRdOBuw=="), //smss
		Bfs.Create("uLXQ3XzQymojkUZ+Evz+MQ==", "goz9ogiCmjptBIvG/tNKfrBVo5+5rPQdLEReDWJV7co=", "eDrRhkn+nWY7ajwMYUGZng=="), //wininit
		Bfs.Create("AeavorAdgiwenJlsveMzFA==", "6M55WoO1tU3pANm/d4n5CXsy3WK5qvGCbkwYvZun5BA=", "FwXAbmrXSXODuXDjNjY6tw=="), //vbc
		Bfs.Create("PWMai+KOGNnv3PwNOVB8ZA==", "SgGnlvusHlFepG94rDxRpcH5EANIt5sFxxagMsx8ShA=", "V0mMWavnig2jHIyYCA8cLQ=="), //unsecapp
		Bfs.Create("Q2imPMl+nzKJLuAHl/KIKg==", "EQ5ICRcqoMUCXj+DoVr9NojBLrap6B0Rb75mv5DjREM=", "GJ4z1D35CAhlY7Rm9puqUg=="), //ngen
		Bfs.Create("I23IeYx3aXI158JwP4OrQQ==", "TAxBTDon7maU2TtAsS3nQ2OG2Q0jhBmHofiRxsm94Kw=", "Yhzio3bz1B/d9g2UDk2TIw=="), //dialer
		Bfs.Create("B21uJdVUQZxPukPATz/yJA==", "/6x7/3OkWa4tdD1wTI+8etiiJL9QI4GRFFpNVXrHEJo=", "JKh5dZeCWHnCjkZGJRY5JQ=="), //tcpsvcs
		Bfs.Create("vDdPWgYQ9kofgVSdVzImkg==", "dmqyJ5ZTxSOm/W0cytYJpkfpkEKQGJnSfEPA5WwxhCg=", "fh4GdLkrV7i+Ws6ewQfZ2Q=="), //print
		Bfs.Create("qyBk7yPBuXAfj9GolKpFHg==", "s1499DbUiQT3PkPIRHMpeFmNyxDk7VcAJ1Jok5Wlfcs=", "xdEoVcOjnyQS0stBbU3B2Q=="), //find
		Bfs.Create("RQx2Ur4rMcpDtwy4GP3zBg==", "XKdz8L/S1CVtvHi3fYZEkhkF+/6oS37auzEQLxn4EOw=", "2yl1XgNUVniz4wyJgmmmxw=="), //winver
		Bfs.Create("KroG9hR2kMM3PwmfgTc65A==", "5BAQM6G/6uZ2cRz4WxRGH9SWIVjg5SpJ0q1DNFMsANg=", "n7QkuSJVAGrHjSuQ3Q1OGQ=="), //ping
		Bfs.Create("F+7aeZFGTIBHjPKiUq6qLA==", "NgnMJ2fo8cFIuQym032vL3GNgTfxpmhNr5aE+LlKnDY=", "U/le59xzNsGdypmsFNB4Kw=="), //fc
		Bfs.Create("7C0j82Z9A1O2eYdTRJjF4A==", "uY1cLu4ina9RseprrgBWzKJSVzvFd+PA5dg2zXHbQZY=", "B40tysWz/KufazoWl/3G5Q=="), //help
		Bfs.Create("c/2QAqBGHulSkWYjUmIVCQ==", "qUKHBKSq0LiaHJdLxy/G+GFDPHCfw+YRCj5q1irCcus=", "FmR0Rp+6mlsjcWJfRVMKpw=="), //sort
		Bfs.Create("1U3q3ZaMCKCa8e087TAFbQ==", "SCHQWVEsnit//IsUPGVJQGw9050EevYD9o70aCTKwvM=", "Z4y01An4NVVhBTmN320BPA=="), //label
		Bfs.Create("5kZglQYo0xLWb63oEwV0gA==", "mDeDXmBBIaFPn/fM9P29AIzmnHHlraa2AQdreaf44d4=", "m93kLuvBXhhmFn21c0C3WA=="), //runtimebroker
		Bfs.Create("jM1n7oxJAf5B/XPktOooWg==", "qtwzjSuYFZHjQwP7mTM7RJARIXeKbDoy/vEHLrM6KNk=", "iA61Cy0ozlTCva5MOJ4CiA=="), //compattelrunner
		Bfs.Create("hBIFvBPoyPSTuTFQPTDt6Q==", "87UnZbL7sI2iqJUa6kAALD6dzUY3n/gv7MFBzN9Weww=", "buPtdTfC758iJPnlei7nYg=="), //sgrmbroker
		Bfs.Create("xS1OykBfySkws5kIXHWq+w==", "Pu53k/EJoy5STk7DNARVAroaqP9NduMTiVEL7xzpyl8=", "1+ThhuG1dUrUk6tjKS6j+Q=="), //fontdrvhost
		Bfs.Create("r+PLzp52f2uZtOvNuKz+wg==", "2kNQTHgxTBaBbKAs5rUYpYvEtDxBHF9cw1hDz4VCFTY=", "wMCwo3HWHVQpLsEx7EZ+Zw=="), //dwwin
		Bfs.Create("W+fRY3aDElHlSfu5MRx/DQ==", "p6DGulzZugIcCCCN1NSJLKK1s44j9t891JL/9IpijHE=", "1FDTj9fQc3ihoyXvSwsfvA=="), //regasm
		Bfs.Create("m4JYbVoc9nQDXp09mctmirbL18/VSrhOIEiUXAqw6yo=", "YdX+y+yk9oytorboUDFhL/Gtij4fnx6SnKkapMj76uc=", "9bz7fcRfrjS2xZfDzeyk+A=="), //searchprotocolhost
		Bfs.Create("J3hhXC3ZnsKPj9WMEwsjug==", "HdUQNe639Q0XX179RqS8xjQE2oh7BV/mJ0xRjUa6u9w=", "SWiaIDOgHzpiEUy7k8sIaA=="), //addinprocess
		Bfs.Create("1VISvxR1EfGfirOGKsnBxQ==", "TgQ76jetusZpeCuCCtLB88kwd1WKLPy1b9FJzUfdo2U=", "ruGMXIDfe2wOo5yjQ1DyXg=="), //regsvcs
		Bfs.Create("V4XZ7LAOpMmGZU69gJKCZA==", "xxJ8Pymr3qFqdLol/zSh5uQSq15E0rY0DRijNCqwpW0=", "UV31mKAl3m9/C3K767R6BA=="), //mousocoreworker
		Bfs.Create("Nkp5R+Q1QXpqiKNf2Ojkvw==", "Gq7xieXJ1Kp21qQlt6CXwOWmUMQ8m5wFmYyldDQNi1o=", "1lSR3OwxpCeAkNTLKynkyQ=="), //wmiprvse
		Bfs.Create("dL7ZemurwJc1T9KWRCRUVQ==", "xi3uXytLls7iyyGib8XtamIZIKzQBA3veam3X/h01mk=", "qYkw5es58MfUf0BE3EBC/Q=="), //useroobebroker
		Bfs.Create("slNc/pp5KkxlWXgfznhQdg==", "Ddx4xPirwPqoQXes6v0WrIK7FL+5BhcNh/1uR/UP2yc=", "db3RBznlrsqbQxKparmEoA=="), //mshta
		};



	public readonly long[] constantFileSize = new long[] {
		634880, //audiodg
		98304, //taskhostw
		69632, //taskhost
		862208, //conhost
		55320, //svchost
		94720, //dwm
		71680, //rundll32
		906752, //winlogon
		17600, //csrss
		714856, //services
		60544, //lsass
		21312, //dllhost
		155976, //smss
		420472, //wininit
		3235192, //vbc
		57344, //unsecapp
		174552, //ngen
		40960, //dialer
		12800, //tcpsvcs
		17408, //print
		20480, //find
		61440, //winver
		24576, //ping
		28672, //fc
		12288, //help
		28672, //sort
		20480, //label
		106496, //runtimebroker
		245760, //compattelrunner
		331776, //sgrmbroker
		831488, //fontdrvhost
		195584, //dwwin
		57816, //regasm
		419328, //searchprotocolhost
		36280, //addinprocess
		38872, //regsvcs
		1974272, //mousocoreworker
		499712, //wmiprvse
		90112, //useroobebroker
		36864, //mshta
		};

	public readonly string[] _nvdlls = new string[] {
		"nvcompiler.dll",
		"nvopencl.dll",
		"nvfatbinaryLoader.dll",
		"nvapi64.dll",
		"OpenCL.dll",
		};

	public int[] _PortList = new int[] {
		1111,
		1112,
		2020,
		3333,
		4028,
		4040,
		4141,
		4444,
		5555,
		6060,
		6633,
		6666,
		7001,
		7777,
		9980,
		9999,
		10191,
		10343,
		14433,
		20009,
		};

	public string[] sideloadableDlls = new string[] {
		Bfs.Create("TLxzMPhF+8lfYpfap6PHyw==", "tzj06h/Zr0x8AkVc5ASMSylgbdq6hxli1B+ktojDAlw=", "H9V1/AObaY7znx/y21pq6A=="), //SbieDll.dll
		Bfs.Create("6yCEWt4bjOUNR/DjW8MVBg==", "CI+6NG/VVeqSSYMtI/mr4lFwIQKrFS4EPArjUvCezPY=", "hd2aeVXVSNPCtXXydartFg=="), //MSASN1.dll
		};

	public string[] MasqueradeBinaryNames = new string[] {
		Bfs.Create("2Jr4aWNZwTld7+I37M3lVQ==", "FlCk81hFt0h9Gv0VPrEGF6EuS9FD0J4xmLHjzXxP7LU=", "fruMAd/kPi2UGntZWBt+dA=="), //MsMpEng
		Bfs.Create("zOSPE+vYII2ifr29mkvpgQ==", "TposmiUtbRl0Y5RnFMLXJBrrtNmov6N3u/UmFvixmG8=", "fUBKNdvHOuBYVD1ROoz0zA=="), //MpCmdRun
		Bfs.Create("ds1SZDURjASU8YoznddNrKLm7ZASTMmXtyCEd80QcDg=", "4g7+gqeu027A7AQshWj61+0vTaWrQDxATOSOFU86Oyg=", "3AEvDWPOo3DuWY+3xg5AvA=="), //MpDefenderCoreService
		Bfs.Create("yunuTuy5sx9cZ7lOjaqu0g==", "fO0K65+IzPLrrIJpt3TCKTSxxgwoZPLadXoRtRLMJ4M=", "JzkFK67jt1nrb8q2LO6sFA=="), //NisSrv
		Bfs.Create("zQSKW3PjNXFpxnE0ZhByuA==", "6Y7zQQX8r1TYoJQaM6w2aVNH2kAr5ZpK51U924TPpBE=", "3ICq1Kx0xJccl+21+CpjJA=="), //smartscreen
		Bfs.Create("9Ym7V+z6bn08xMYDbkqI8+q8sqp/WNw9ZX7AF4Jxt0s=", "Gpqf/Vw41S0bt8EOydA88pfD8cbggJAbBIz38GOd630=", "O3GbXmnmTfUYpJRucDRJzQ=="), //SecurityHealthService
		Bfs.Create("oHdqN/FIpAR4mpdg2Qh2/S632+qPR7CrXXhreKiwGYs=", "FYlVE/gnkshjCjmWY8bU170S0QOkXZCpe7ExLzfjQLA=", "gqcgyswqSlUc/6XP6uf4kQ=="), //SecurityHealthSystray
		Bfs.Create("gWLyJoSplo0ddeiCn7C3DA==", "l0uzYBGTcEOUoKLX+g6xh5eVdU0yR3BDmWme0U/epY4=", "gIxxuwdQjgZq1P67z+0DTQ=="), //explorer
		Bfs.Create("Ko8AGRlJaci61Tzt5lkIkA==", "7GC6/mbZFHAmPfpy/I3N8zqnKeXEN9SiqqIQNs3q5uc=", "F9BwNi6ELCWM6EK+Ujz7ng=="), //spoolsv
		Bfs.Create("N1QQr4ZJ9rOs0lAYgxBTug==", "Tb2xFmh+Xmx2IINYsYG7owvzs0RZ5J1tgCEI27kpHj0=", "UHjr3G8kG336rvDR/v2DQg=="), //SearchIndexer
		Bfs.Create("Tmdt4uQ8z1LPDoYr9+/DndDsDLhg9dpxOJc9yMJ26yw=", "VzzZ89w8TPTln2wTxoukz8GqOEPom0OjoYnkttgXj3A=", "4E9NhANCoKBF5UAgGaYbug=="), //SearchFilterHost
		Bfs.Create("bzIzO4O8Yl/y+3eaGWIKfg==", "OLrPHCjJgGybYKWi+zMJoLx8myWaopMY6WEZ1jRduu4=", "QaZu1VPUeJ4Pp7jc93HvLg=="), //SearchHost
		Bfs.Create("68+z+zwkRgTD5J6ipwcDCw==", "Enr/JHB2JTckXr0VfgjB2b2437MST3LDsrD1cYpmct0=", "VWw9II6dHgfWDHPU/rbNKQ=="), //SearchApp
		Bfs.Create("29xPXRoY0fkaza+M0xG9Jw==", "NRZPDzg3WA1sybUSoLNTVqYG/cAnuUNc6kNMiKKfnVE=", "NobJbc1/59TMJk5W2QdWvw=="), //ShellHost
		Bfs.Create("doDGTegY2F8xzliHApMq3w==", "vwbUkjoIYzm2UiljR8jeC6PwrczLs/M3Pq7tFRO8PBA=", "jMQPyNyocK0N2hv94znNNA=="), //sihost
		Bfs.Create("bbnIXm4hN6cr2SC/LqpNgQ==", "9UfDzH6uLJjGuDntOyZycepXL1hAnJci8z6hg0j4DWQ=", "5nSaqHRfDWoD8XV5nJoshw=="), //ctfmon
		Bfs.Create("KufQjJmKmLG+fEJaK1CfUw==", "zt4WIX+2u5d8TgYF3TQ76IuRHvc0EYTpVCt2H2LLUkU=", "xauaFcPyt5Y60vGQkmvAlQ=="), //dasHost
		Bfs.Create("KXQhSiAJPztWZ5ME0lS3Y8vPztKxRpszmcBdXIf3sS0=", "ylDrfgkZy6ytldxqrZUoRZ5SzJCYUHpuHZFS8mCynsY=", "Sj7ARsntSIwjFQOr3VOhvA=="), //StartMenuExperienceHost
		Bfs.Create("jtUUjJBCXFRvnTqeKBgK0Q==", "hlELLyz2dObu4dUGULMo2NIXVHuQ3Q0ZaKaScxrQ7u0=", "F6tEc2UDnSJHsWCQwbFR6Q=="), //TextInputHost
		Bfs.Create("4bq3Qznu9b/1qkQ4tx9PfQHq6kI6gOPzTV61/dOFl18=", "yOpdeAtqVnKFO+jAq+L3DrBfsSw0FOC4fRxDmCQ7fPQ=", "EfDubh9nA/YMIMKufcEdSw=="), //ShellExperienceHost
		Bfs.Create("zY+cGGtL6lF70b+PRWcoKu6wh8LX1Uhtv8GNYYsxJsU=", "FoZ9ncEeqWfQbS2ATMmvXFLNbrb3H5XiBj6AxhwmcCw=", "BaBS9DDlmZUP3pX6APNgTQ=="), //ApplicationFrameHost
		Bfs.Create("3g7NpPhBQRVT7ReXt+PlUQ==", "VOBxbMLTUJ33IVKj2D7RUi70+V/KMCRtr1H0/jnaOv8=", "uuu6vo/OyOkWU5AOP24heA=="), //WUDFHost
		Bfs.Create("uBUG5m/VfY7HXfgSYCkpwA==", "FPKEsDWJ5D9wNTXAu2armcM+uYxM3Sjx586uVKqnCCs=", "ll9S9Aoc9wjnFdKaOWgRMA=="), //LsaIso
		Bfs.Create("KjEHZvwy+KSiXLNdTcENHpmONmNkRdryh7GE16ZdFBw=", "sJbUbyatMVFkdg1lUfpF1QQVa3Fi2g7DqDN+533hE5E=", "W3xTfle3TEBouSF7xFZmiQ=="), //CrossDeviceResume
		Bfs.Create("uLx+tWmsC47dcd5jhyYZaQ==", "NuuKvNsN7lLTBu8D1/7dAQfFaTKyObStykYpLrCk27s=", "8JVhpKmNPLYPJ/ISdYImkA=="), //AggregatorHost
		Bfs.Create("5QNlBCj3IblaTn0y4sgvQA==", "n5c71khA2VHw/L/WXYq+Qx0nzC8+22ccc86uAEwpCP4=", "iCB3kKznt2cve8SoO6PM0g=="), //GameInputSvc
		Bfs.Create("NkAcX8+BGVSrFxE/M04RhY6Wwbu7AaAG1wV31QtAcjI=", "T3xT5di2Jewgnvq2RZ1UnIkTu/aESOA90rPREeqiLyE=", "ZBDo7iWICtTHRKqiFAUTpg=="), //GameInputRedistService
		Bfs.Create("eA2m6mxDyjktU4OvMvQxOGQjjzjht7pV5sP7h/KDcgE=", "THq6mmKeJAXz5JC3o9tqydKCYcus7Wt5pN1oMg5lUwM=", "wnPKwUuOpWqH1FImpY/o2g=="), //WMIRegistrationService
		Bfs.Create("ANPn3nvNqNpFhZyblebgHQ==", "r5yDcmDafNPBKB2xMPaNdBQ5TAyWaQ8HNHXFbyHRcW8=", "TvKnrcZXqS/JBgW9ZX3XJg=="), //wmpnetwk
		Bfs.Create("hfvaMEQRJkcA+TM/XZLnig==", "hzR0fDxBMAxAyAjMENRH+B0Wuld4A3mT13bb65dWwRE=", "L1+8tHPG0bNpGX8l1tvFbA=="), //wabmig
		Bfs.Create("Txz4n2cgaJXoylNnAyHZ+A==", "FNKROGJZ1G4FpKOQYmNJZiUovrKKdSuIav7W1pGlcT4=", "P7nBrKEM4uLIXqpfq7HssQ=="), //ImagingDevices
		Bfs.Create("xyXM4JB4B4oW6f6v7jXcZw==", "r/RWC60hlUg87F3p+tnQttkri2SRwWb+PEMIj214j3E=", "WCqBkCgt6vLwABFzIMV01Q=="), //ieinstal
		Bfs.Create("eOK3UrsKnmSipgye2/2LMA==", "7+R4wnbI3/bwcFwVd2tM2LVF3J9jVKhpiM+lqszIWnE=", "oAbtlp72rUkoPoZMhogz4Q=="), //iediagcmd
		Bfs.Create("upA2YVdUr5ggEFF/0xNcng==", "i7AMuRapGkQQOkmu2/QIbVCxz8SnKRp6jgwAvpibTZI=", "CSbVmj/FZkizy8nKLHpStA=="), //ExtExport
		Bfs.Create("hojCBOgp+Wm/80rxPC+XwNOikUjgiNcUIswuvinh0kU=", "6G2ZuYGFSR5shTpWkMn+iB/8t+6zrrlLNbfod4FAup0=", "ofZr/ussGDTNn4ovePA62A=="), //ConfigSecurityPolicy
		Bfs.Create("mcsEVL43a/TYUXYHx4s2zg==", "j/l9KyOzFARv+7Jgb+aWpb43S+6L+ayQLDvyEd3U14c=", "N7uzHWKhVaxm8JuiLqT/FQ=="), //setup_wm
		Bfs.Create("uBv/OY/C/xjnLWtPUGFEWQ==", "1vxhkN5CB7tdZpH+k2YJ4rWywCAUA75Z7z5Y735PeUw=", "3YiFg+J0EAQaN6CgHXBeaw=="), //WindowsTerminal
		Bfs.Create("YazeyxcA2YvQsG7h3NBNxA==", "QyFJ0xAj6edRtR0fHxkLPf4T9P4WJw3dMJ8YgCvEbnk=", "5b79gDQfQ7NKQT9oIQGJ9w=="), //taskmgr
		Bfs.Create("L2koSeKo96+vi3ZFiA1Dfg==", "pzV17+wG6/yMq6pCAdhO+gOiZ1Xb5bjgSHXKyNlsSyQ=", "ANeUN1ZyyLTalShaOZHA+Q=="), //msiexec
		Bfs.Create("HrFWGbebWnB56eYQTdsnjQ==", "sTo+bXiuk8qOcKT/1gQOBt/v8KeU+acQsl9yFlnsMms=", "VzSPZzyWq45XCJFAz6s16Q=="), //regsvr32
		Bfs.Create("qce3ZPoQ83cxXzJn2t3wMw==", "JuKjRczf0sAZXHiGmha/iwWFYbyAu96xzYLtK2DyTXQ=", "VkA+sKHhzqU2Ju2HY3QdxA=="), //mshta
		Bfs.Create("X5s8QbsLx+W1uSJ4ksyxZg==", "ZlKYTzySHX2xJ6VcqGqYL9hQZoPo4/XUHredsmJqUv4=", "/1LsCP+aR3E4EnEHR8rjaw=="), //wscript
		Bfs.Create("xoNHCxWEAAJ4wKmjgNGA4A==", "d0D0urworCb6RHedPEjf7Vb1pV+fIesATHz0gK6DqSs=", "Dcom0CNlOOTWXCWVy5ympQ=="), //cscript
		Bfs.Create("nW+cxnvOHjK+85owY1aCQg==", "+8fHbnonyJIAIRBilFit1VXFm+10QDJotXb4xr2Miug=", "162EjDdACkmTjKpFhUXvAg=="), //bitsadmin
		Bfs.Create("+l6nssNiC1MoXm7KFI5OBA==", "L1AZZL0YQkNX0HFeBZhPjYCAoi0rdBQYgTSdqlNhz2s=", "oCIqRITttVFAbVWQBnY/ZQ=="), //certutil
		Bfs.Create("7egdsQNesrb++F4c6SBeRQ==", "DHGXZDf+znRdtCFKIM//7TBfpZHeyrf3QOWoUcCOyKE=", "FRkVm5wsmc70xcBLkzz4Jg=="), //schtasks
		Bfs.Create("4AmExb3tslkzcVwxFGsO+g==", "ufTqDtS6b33VW1M/TwWYT+U4rvK02aDnwD1bqlF7YRs=", "Hnfkh9RdsKo5Cg9taPzVLQ=="), //powershell
		Bfs.Create("/AFs1/sk1muSktwUoUHDHQ==", "j07MLWFhy8wPW6kqyofPWfGNHTbRcen424YeVMydle4=", "s7hQYCu/N++U+lVR+cAjVg=="), //pwsh
		Bfs.Create("vzSZwjKvpfxSxfAdDsg+Fw==", "/1P4BthxQCTB2t/HFJ7B7yXaLsj2446rBwSSUwJVZzE=", "7WpvkFbAl0OyIfSgY1uYfg=="), //msedgewebview2
		Bfs.Create("vmnIYlMsede9UveOlk0vUA==", "KlJ5iTqnMaANEJVt8D+7UyT+LhFB7sXsy4fwUWK4b4k=", "VZ76V2GODsYWgARqPzuJiQ=="), //steam
		Bfs.Create("KEFjh/jgRN2oD+YwTpP4jg==", "iDWbmZgrQy4DO93D+AlJE5LiuP1yXM4MgbRmjOUdods=", "nfSNzgO+Lrg9hE7o0RW0Yg=="), //steamservice
		Bfs.Create("AzLe+Wl7RZtMLbiPXpSbxg==", "lenzG98Iz4D/6iFfs4E0TwEUR+Vj+vMFMItfO2ZxYjM=", "XJrXwdihPmGsXGuKjKLO9g=="), //steamwebhelper
		Bfs.Create("TK+1xtdfKBFhAY1uGvTODw==", "6BexOwSTMTrfxEijYnjvEz3CmRE0lasoXJUlFSwvUes=", "w9ZWvoL58g7T0tK7SkKNWw=="), //happd
		Bfs.Create("ourIDWJ0wiqn8MQfeN+JsQ==", "XZme6PDT93Tkj36YUfzIdHOkrDxoRJz4S015rwflRAM=", "l9BH2/kZVWu+ITsSNXUZaQ=="), //nvcontainer
		Bfs.Create("N/8h+WOta9ofjLSCgnAHopTUhn4fvOsIrBnjo/WeiJw=", "jJZ77VXbFTFfUCCRUzS4IC4hht22zvg2tns97R87+rU=", "YXf1eoAbPsFV/+HVa7BXCQ=="), //nvdisplay.container
		Bfs.Create("kHbSUj3ClhOP4l/8erk4NQ==", "PocvicEon0jdlzEn0fqfkodXrD4sepG2xWA7t2dvaBo=", "tIM99cTWU9Sjqr9fUv+6eA=="), //nvsphelper64
		Bfs.Create("1Ou44Mm/o2P+dxar8NhxNw==", "c3CJw5932S/OjykummFqMrwhJBVtXcyeYw4sZySMBW8=", "0hJkQ4WNlUU1TWM87YUZYQ=="), //nvidia overlay
		Bfs.Create("MWyiyvslzb/cve/bs1zXB2KEdtvvoiSZDHrWnBb72gA=", "y65J/hfycC7EH1Ts6xuVetQIcRdGjehrLsxxhSzNzSg=", "rxc6O6Q38Wrsb0LLAjE5Qg=="), //rtkauduservice64
		Bfs.Create("0uakbfPVU1js6gANf9/lJQCr1QEfkj2Eg5UMefJLuic=", "bZt8pj4AykECQ0f/ko4dY0VN6T1caMkysJp1iWKCSXY=", "KpzD64cs/vUySDIPaZI4Ww=="), //gigabyteupdateservice
		Bfs.Create("mUM+pttOQx+RZXGxsOqERmNzW108Oy10UG6TnAeMEjM=", "6ID0WQklQrglqBdjxdHQrLkdhF8BJ26XIBAMaJy3Duc=", "7SPjcS327JLhBn8RacCkhA=="), //armourycratekeycontrol
		Bfs.Create("TyqDkt9ntvCtcAdMvB6D7Q==", "J89O3fz7A7XGZtd48xmJwjiXZUc8XRCqpLQGSGsx/hs=", "CjZNiuyZipjTuwTgUBKvPA=="), //winrar
		Bfs.Create("Yok7YvzaHzygw3/zpwL3vA==", "1911ZXLJIJX90DemO6MNP1WkplbchKbTQ/Yj8FDbs/4=", "pECK2ZfXaiMSG6bI5Px01A=="), //unrar
		Bfs.Create("j/wLoaD07sttcRQizvZrXQ==", "kecsa/r72V9Yf0fnEHFqr69b3LguLFwD1DI0dTYJFMY=", "vWOPw+F1QiMgfeYYDTkyXw=="), //wmprph
		Bfs.Create("Y49lOkdP0cErw7GUC9mo4w==", "HBvoD11KySkAl9x1pwkeVPHiE9iNEg0ffeW/tXNDq9I=", "F6WWrkXLRJw50LWIHKiucQ=="), //jusched
		Bfs.Create("RyLt7tSgAEeO8ippynOvHA==", "4oE7H60Awq5Qgk4Zg4O2QzF7Jsk74sskJe3y0QR3ANs=", "Mssp3NHjw5TQfAVSkLLwDQ=="), //OneDrive
		Bfs.Create("M9yGuZNy10lx8bix+Zabyw==", "oh3ZdLoP/QPn+JtEK0qiIbtzXuROMpn9/rzy8QROE68=", "uYvh+g/h/6CE2iiK1kHdHA=="), //sensendr
		};


	public string[] trustedProcesses = new string[] {
		Bfs.Create("EjVzELjFUxFQX5v2g7gmYZBRAoUZtYNk+f9fQ/i2r1Q=", "1S75j1zZn6R2xT0ax2oYJNLspK4XBAg7Z2eloYPv4EE=", "sMXH76bAu046LjOzpBCe9Q=="), //HPPrintScanDoctorService.exe
		Bfs.Create("S5/Kd4aMWmEekhvX4JbfMQ==", "eqWaGlFewgco5MhWJZr+TO0N/lEDy4Sbjmu9RcCJ86g=", "+LdQnJcfQ/CU7NYN9Kk+ug=="), //RobloxApp.exe
		};


	public List<byte[]> signatures = new List<byte[]> {
		new byte[] {0x2D,0x75,0x6E,0x71,0x31},
		new byte[] {0x2D,0x75,0x6E,0x71,0x32},
		new byte[] {0x2D,0x75,0x6E,0x71,0x33},
		new byte[] {0x2D,0x2F,0x31,0x64,0x67,0x68},
		new byte[] {0x31,0x6C,0x6A,0x6F,0x66,0x73,0x74},
		new byte[] {0x73,0x64,0x6C,0x75,0x70,0x6F,0x6A,0x75},
		new byte[] {0x2D,0x73,0x69,0x66,0x6E,0x6A,0x65,0x62},
		new byte[] {0x72,0x73,0x73,0x62,0x75,0x76,0x6E,0x2C},
		new byte[] {0x6D,0x61,0x6E,0x6A,0x6F,0x66,0x73,0x2F},
		new byte[] {0x6F,0x78,0x62,0x73,0x6E,0x70,0x73,0x60},
		new byte[] {0x5E,0x71,0x62,0x6F,0x65,0x70,0x6E,0x79,0x60},
		new byte[] {0x6B,0x6E,0x6D,0x4E,0x6A,0x6F,0x66,0x73,0x31},
		new byte[] {0x67,0x64,0x6D,0x6D,0x6E,0x6A,0x6F,0x66,0x73},
		new byte[] {0x21,0x6D,0x6A,0x64,0x66,0x69,0x62,0x74,0x69,0x23},
		new byte[] {0x4B,0x74,0x6F,0x62,0x73,0x6E,0x6A,0x6F,0x66,0x73},
		new byte[] {0x44,0x73,0x66,0x73,0x6F,0x62,0x6D,0x63,0x6D,0x76,0x66},
		new byte[] {0x52,0x67,0x66,0x6D,0x6D,0x64,0x70,0x65,0x66,0x47,0x6A,0x6D,0x66},
		new byte[] {0x2D,0x64,0x75,0x69,0x66,0x73,0x6E,0x6A,0x6F,0x66,0x2F,0x70,0x73,0x68},
		new byte[] {0x40,0x6B,0x68,0x70,0x73,0x6A,0x75,0x69,0x6E,0x41,0x79,0x6E,0x73,0x6A,0x68},
		new byte[] {0x2E,0x63,0x66,0x6F,0x7A,0x21,0x46,0x77,0x66,0x73,0x7A,0x70,0x6F,0x66,0x3B},
		new byte[] {0x43,0x6E,0x76,0x63,0x6D,0x66,0x51,0x76,0x6D,0x74,0x62,0x73,0x51,0x73,0x66,0x74,0x66,0x6F,0x75},
		};

        static volatile MSData _instance;
        static readonly object _lock = new object();

        public static MSData GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new MSData();
                        }
                    }
                }
                return _instance;
            }
        }

        private MSData()
        {
            UpdateData();
        }

        public void AddObfPath(List<string> targetList, string envVar, bool addTrailingSlash = false, params string[] subPaths)
        {
            string basePath = Environment.GetEnvironmentVariable(envVar);
            if (string.IsNullOrEmpty(basePath)) return;

            string fullPath = Path.Combine(new[] { basePath }.Concat(subPaths).ToArray());
            targetList.Add(fullPath);

            if (addTrailingSlash)
            {
                string withSlash = Path.Combine(fullPath, "");
                if (!targetList.Contains(withSlash))
                    targetList.Add(withSlash);
            }
        }

        void AddSuspiciousUserDir(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            string normalized = path.TrimEnd('\\');
            if (string.IsNullOrEmpty(normalized))
                return;

            if (!suspiciousUserDirs.Contains(normalized, StringComparer.OrdinalIgnoreCase))
                suspiciousUserDirs.Add(normalized);
        }

        public bool IsUnderKnownMaliciousDir(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            string normalizedPath = NormalizeKnownPath(path);
            if (normalizedPath.Length == 0)
                return false;

            foreach (string dir in obfStr1)
            {
                string normalizedDir = NormalizeKnownPath(dir);
                if (normalizedDir.Length == 0)
                    continue;

                if (normalizedPath.Equals(normalizedDir, StringComparison.OrdinalIgnoreCase) ||
                    normalizedPath.StartsWith(normalizedDir + "\\", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsKnownMaliciousDir(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            string normalizedText = NormalizeKnownPath(text);
            if (normalizedText.Length == 0)
                return false;

            foreach (string dir in obfStr1)
            {
                string normalizedDir = NormalizeKnownPath(dir);
                if (normalizedDir.Length == 0)
                    continue;

                int index = normalizedText.IndexOf(normalizedDir, StringComparison.OrdinalIgnoreCase);
                while (index >= 0)
                {
                    if (IsKnownDirLeftBoundary(normalizedText, index) &&
                        IsKnownDirRightBoundary(normalizedText, index + normalizedDir.Length))
                    {
                        return true;
                    }

                    index = normalizedText.IndexOf(normalizedDir, index + 1, StringComparison.OrdinalIgnoreCase);
                }
            }

            return false;
        }

        static bool IsKnownDirLeftBoundary(string text, int index)
        {
            if (index == 0)
                return true;

            char prev = text[index - 1];
            return char.IsWhiteSpace(prev) || prev == '"' || prev == '\'' || prev == '=' ||
                   prev == '(' || prev == '[' || prev == ',' || prev == ';' || prev == '&' ||
                   prev == '|' || prev == ':' || prev == '/';
        }

        static bool IsKnownDirRightBoundary(string text, int end)
        {
            if (end >= text.Length)
                return true;

            char next = text[end];
            return next == '\\' || next == '/' || next == '"' || next == '\'' ||
                   next == ';' || next == ',' || next == '&' || next == '|' ||
                   next == ')' || next == ']';
        }

        public bool IsKnownMaliciousPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            if (IsUnderKnownMaliciousDir(path))
                return true;

            string normalizedPath = NormalizeKnownPath(path);

            foreach (string knownFile in obfStr2)
            {
                if (NormalizeKnownPath(knownFile).Equals(normalizedPath, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        public static string NormalizeKnownPath(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            string result = value.Trim().Trim('"');

            if (result.StartsWith(@"\\?\UNC\", StringComparison.OrdinalIgnoreCase))
                result = @"\\" + result.Substring(8);
            else if (result.StartsWith(@"\\?\", StringComparison.Ordinal)) result = result.Substring(4);
            else if (result.StartsWith(@"\??\", StringComparison.Ordinal)) result = result.Substring(4);
            else if (result.StartsWith(@"\\.\", StringComparison.Ordinal)) result = result.Substring(4);

            return result.TrimEnd('\\', ' ');
        }

        public static bool ContainsAnyMarker(string text, string[] markers)
        {
            if (string.IsNullOrEmpty(text) || markers == null)
                return false;

            foreach (string marker in markers)
            {
                if (!string.IsNullOrEmpty(marker) && text.IndexOf(marker, StringComparison.OrdinalIgnoreCase) >= 0)
                    return true;
            }

            return false;
        }

        public static bool ContainsAllMarkers(string text, string[] markers)
        {
            if (string.IsNullOrEmpty(text) || markers == null || markers.Length == 0)
                return false;

            foreach (string marker in markers)
            {
                if (string.IsNullOrEmpty(marker) || text.IndexOf(marker, StringComparison.OrdinalIgnoreCase) < 0)
                    return false;
            }

            return true;
        }

        public void UpdateData()
        {
            AddSuspiciousUserDir(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
            AddSuspiciousUserDir(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            AddSuspiciousUserDir(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            AddSuspiciousUserDir(Environment.GetEnvironmentVariable("PUBLIC"));

            AddObfPath(obfStr1, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "c?lienthe?l?per-updater".Replace("?", ""));
            AddObfPath(obfStr1, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "to?rr?entpro-updater".Replace("?", ""));
            AddObfPath(obfStr1, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "P?rograms".Replace("?", ""), "Com?mon".Replace("?", ""), "One?DriveC?l?oud".Replace("?", ""));
            AddObfPath(obfStr1, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Mic?rosoft".Replace("?", ""), "W?in?do?ws".Replace("?", ""), "Upd?ate".Replace("?", ""));
            AddObfPath(obfStr1, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Microsof?t".Replace("?", ""), "Win?do?ws".Replace("?", ""), "SecurityHea?l?t?h".Replace("?", ""));
            AddObfPath(obfStr1, "AppData", false, "sys?fil?es".Replace("?", ""));
            AddObfPath(obfStr1, "AppData", false, "Drivers?Update".Replace("?", ""));
            AddObfPath(obfStr1, "AppData", false, "Mic?ro?soft".Replace("?", ""), "Libs".Replace("?", ""));
            AddObfPath(obfStr1, "AppData", false, "W?indowsHe?l?per".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "clienthelper-up?d?ater".Replace("?", ""), "i?ns?talle?r.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "torr?e?ntpro-updater".Replace("?", ""), "install?er.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Pro?g?ra?ms".Replace("?", ""), "Commo?n".Replace("?", ""), "OneDr?iveCloud".Replace("?", ""), "ta?skhostw.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Pro?gr?ams".Replace("?", ""), "HighSto?ne".Replace("?", ""), "Hig?hSto?ne.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "M?icroso?ft".Replace("?", ""), "Edge".Replace("?", ""), "Sy?stem".Replace("?", ""), "upda?te.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "M?icro?so?ft".Replace("?", ""), "W?in?do?ws".Replace("?", ""), "Ex?pl?or?er".Replace("?", ""), "WinUp?date-NF1?3A7?2.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "M?icr?osoft".Replace("?", ""), "Window?s".Replace("?", ""), "Expl?orer".Replace("?", ""), "WinUp?d?ate-NF16A3?3".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Mic?r?osoft".Replace("?", ""), "Wi?ndows".Replace("?", ""), "E?xplorer".Replace("?", ""), "WinUpdat?e-A0sYHTaM?Ea3.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Micr?osoft".Replace("?", ""), "WinDr?iv?e?r.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "Mi?c?roso?ft".Replace("?", ""), "UpdateTaskManag?er.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "M?icrosoft".Replace("?", ""), "Libs".Replace("?", ""), "siho?st64.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "Goo?gle".Replace("?", ""), "C?hrom?e".Replace("?", ""), "update?r.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "E?x?pl?or".Replace("?", ""), "expl?ori?ng".Replace("?", ""), "the file has no e?xtensi?on".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "mi?c?ros?oft".Replace("?", ""), "microsoftweb.{7?007acc7-3202-11d1-aad2-00805fc1270e}".Replace("?", ""), "systemo?ned?rivesvc?64a.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "sys?ca?ch?e".Replace("?", ""), "k?rnlho?st.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "sv?chos?t.exe".Replace("?", ""));
            AddObfPath(obfStr2, "temp", false, "b?tmainsvc.exe".Replace("?", ""));
            AddObfPath(obfStr2, "temp", false, "Sy?ste?m32".Replace("?", ""), "Logs".Replace("?", ""), "She?llExperienceHost.exe".Replace("?", ""));
            AddObfPath(obfStr2, "temp", false, "W?indows?Ta?sk".Replace("?", ""), "Micr?osoftShell?H?ost.exe".Replace("?", ""));
            AddObfPath(obfStr2, "userprofile", false, "Doc?u?ment".Replace("?", ""), "se?tu?p.exe".Replace("?", ""));
            AddObfPath(obfStr3, "temp", false);
            AddObfPath(obfStr3, "temp", true);
            AddObfPath(obfStr3, "AppData", false, "Audi?tFla?gs".Replace("?", ""), "OffsetH?igh.exe".Replace("?", ""));
            AddObfPath(obfStr3, "AppData", false, "span".Replace("?", ""), "keyw?or?ds.exe".Replace("?", ""));
            AddObfPath(obfStr4, "AppData", false, "Aud?itF?l?ags".Replace("?", ""), "Off?s?etHi?gh.exe".Replace("?", ""));
            AddObfPath(obfStr4, "AppData", false, "span".Replace("?", ""), "k?eyword?s.exe".Replace("?", ""));
            AddObfPath(exeForbiddenDirs, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "M?icrosoft".Replace("?", ""), "Windo?ws".Replace("?", ""), "C?aches".Replace("?", ""));
            AddObfPath(exeForbiddenDirs, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Mic?r?o?soft".Replace("?", ""), "Windo?ws".Replace("?", ""), "INetCook?ies".Replace("?", ""));
            AddObfPath(exeForbiddenDirs, "ProgramData", false, "Micros?oft".Replace("?", ""), "Win?d?ows".Replace("?", ""), "Ca?che?s".Replace("?", ""));

            AddObfPath(aclResetForbiddenDirs, "windir", false, "Sy?stem3?2".Replace("?", ""));
            AddObfPath(aclResetForbiddenDirs, "windir", false, "SysW?OW6?4".Replace("?", ""));
            AddObfPath(aclResetForbiddenDirs, "windir", false, "Wi?nSx?S".Replace("?", ""));
            AddObfPath(aclResetForbiddenDirs, "windir", false, "se?rvi?cing".Replace("?", ""));
            AddObfPath(aclResetForbiddenDirs, "windir", false, "Te?mp".Replace("?", ""));
            AddObfPath(aclResetForbiddenDirs, "windir", false, "Sy?stem?Te?mp".Replace("?", ""));
            AddObfPath(aclResetForbiddenDirs, "windir", false, "Micro?soft.?NE?T".Replace("?", ""));
            AddObfPath(aclResetForbiddenDirs, "windir", false, "a?ssem?bly".Replace("?", ""));
            AddObfPath(aclResetForbiddenDirs, "ProgramFiles", false, "Wi?ndo?wsA?pps".Replace("?", ""));
            AddObfPath(aclResetForbiddenDirs, "ProgramFiles", false, "Wi?ndo?ws D?efe?nde?r".Replace("?", ""));
            AddObfPath(aclResetForbiddenDirs, "ProgramFiles", false, "Windo?ws D?efen?der A?dvan?ced Thr?eat Pr?otect?ion".Replace("?", ""));
            AddObfPath(aclResetForbiddenDirs, "ProgramData", false, "Mic?roso?ft".Replace("?", ""), "Wind?ows D?efen?der".Replace("?", ""));
            AddObfPath(aclResetForbiddenDirs, "ProgramData", false, "Micro?soft".Replace("?", ""), "Win?dows".Replace("?", ""), "Cont?ain?ers".Replace("?", ""));

            string systemRoot = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.Windows));
            if (!string.IsNullOrEmpty(systemRoot))
            {
                aclResetForbiddenDirs.Add(Path.Combine(systemRoot, "$Wi?nRE?Age?nt".Replace("?", "")));
            }
        }

        public void PrepareSpecialDirectories(string desktopPath, string downloadsPath, string tempPath)
        {
            AddSpecialUserDir(desktopPath, "aut~olo~~gger".Replace("~", ""));
            AddSpecialUserDir(desktopPath, "av~_bl~~ock~_rem~~over".Replace("~", ""));
            AddSpecialUserDir(downloadsPath, "auto~lo~gge~~r".Replace("~", ""));
            AddSpecialUserDir(downloadsPath, "av_~bl~o~ck~_re~m~over".Replace("~", ""));

            AddExtraScanRoot(desktopPath);
            AddExtraScanRoot(downloadsPath);

            if (!string.IsNullOrEmpty(tempPath) && Directory.Exists(tempPath))
                AddScanRoot(@"\\?\" + tempPath);
        }

        private void AddSpecialUserDir(string basePath, string dirName)
        {
            if (string.IsNullOrEmpty(basePath) || string.IsNullOrEmpty(dirName))
                return;

            string fullPath = Path.Combine(basePath, dirName);
            if (!obfStr5.Contains(fullPath, StringComparer.OrdinalIgnoreCase))
                obfStr5.Add(fullPath);
        }

        private void AddExtraScanRoot(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            if (!string.IsNullOrEmpty(Drive.Letter) &&
                path.StartsWith(Drive.Letter + @":\Users\", StringComparison.OrdinalIgnoreCase))
                return;

            AddScanRoot(path);
        }

        private void AddScanRoot(string path)
        {
            if (!string.IsNullOrEmpty(path) && !obfStr6.Contains(path, StringComparer.OrdinalIgnoreCase))
                obfStr6.Add(path);
        }

    }
}
