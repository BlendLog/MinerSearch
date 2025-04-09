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

    public class MSData
    {
        public List<HashedString> hStrings = new List<HashedString> {
            new HashedString("d82e179187d1268339dcc5fa62fa8b1c",14), //api.github.com
			new HashedString("99cd2175108d157588c04758296d1cfc",10), //github.com
			new HashedString("a2883d9faa219af692c35404e8c5c05a",19), //codeload.github.com
			new HashedString("5fb3419335f5e5131ab3fc22d06ad195",20), //support.kaspersky.ru
			new HashedString("4bb1cae5c94216ccc7e666d60db2fa40",12), //kaspersky.ru
			new HashedString("83b6a29ee489bf3e976824b763c212e9",14), //virusinfo.info
			new HashedString("0282e441b801ef6fd6712b60b907417c",22), //forum.kasperskyclub.ru
			new HashedString("4360f8ffd51b17b8bc94745c4a26ef2c",13), //cyberforum.ru
			new HashedString("e752141e6b76cf60e0bf9f850654d46b",12), //soft-file.ru
			new HashedString("23c807844e8c9c0af34a82cc145b04b2",20), //360totalsecurity.com
			new HashedString("bd25a074d01c2eeb74d8563a09f9ebf6",12), //cezurity.com
			new HashedString("1a01fc7cc8de2fa07c52183572f06ac8",15), //www.dropbox.com
			new HashedString("6319434ad50ad9ec528bc21a6b2e9694",13), //193.228.54.23
			new HashedString("39cf9beb22c318b315fad9d0d5caa105",13), //spec-komp.com
			new HashedString("44d93a0928689480852de2b3d913a0bf",7), //eset.ua
			new HashedString("545f4178fd14d0a0fdacc18b68ac6a59",18), //regist.safezone.cc
			new HashedString("3469d5aaf70576a92d44eff48cbf9197",13), //programki.net
			new HashedString("09cf5cb0e321ef92ba384fddf03b215b",11), //safezone.cc
			new HashedString("2f814f460634c256b37b3b827abbf81d",16), //www.esetnod32.ru
			new HashedString("54cc7b8155fe3c550153cb8f70214343",12), //www.comss.ru
			new HashedString("460049e8266ca5270cf042506cc2e8eb",16), //forum.oszone.net
			new HashedString("a6891c5c195728b0c75bb10a9d3660db",10), //blog-pc.ru
			new HashedString("6c366a99be85761e88558f342a61b2c4",12), //securrity.ru
			new HashedString("4e42a4a95cf99a3d088efba6f84068c4",10), //norton.com
			new HashedString("41115f938d9471e588c43523ba7fb360",10), //vellisa.ru
			new HashedString("84b419681661cc59155b795e0ca7edf9",20), //download-software.ru
			new HashedString("b4de3925f3057e88a76809a1cf25abe5",15), //drweb-cureit.ru
			new HashedString("133dbe014f37d266a7863415cec81a4f",13), //softpacket.ru
			new HashedString("a2c665f4f9d1b72b6cf88bf0ec3de52a",17), //www.kaspersky.com
			new HashedString("80b73c20690f51646fecf5bedd00f14e",12), //www.avast.ua
			new HashedString("5fa4d0d3dc665c270e1d8f4f36742398",12), //www.avast.ru
			new HashedString("34c51c2dd1fa286e2665ed157dec0601",9), //zillya.ua
			new HashedString("626575b255ca41a9b3e7e38b229e49c7",11), //safezone.ua
			new HashedString("7d2500fc0c1b67428aac870cad7e5834",12), //vms.drweb.ru
			new HashedString("91c394760272fc16c952bdba553d3ea6",12), //www.drweb.ua
			new HashedString("b8d20b5201f66f17af21dc966c1e15f8",13), //free.drweb.ru
			new HashedString("348ccdb280b0c9205f73931c35380b3a",15), //biblprog.org.ua
			new HashedString("9bfeda9d06879971756e549d5edb6acd",20), //free-software.com.ua
			new HashedString("78e02266c69940f32b680bd1407f7cfd",26), //free.dataprotection.com.ua
			new HashedString("82ccc585a90ff5da773ed6321e1335d4",13), //www.drweb.com
			new HashedString("5a6822824a14727fd67a75ca9bcc0058",18), //www.softportal.com
			new HashedString("3277391ae8c21f703aedfa065382025e",14), //www.nashnet.ua
			new HashedString("820c5a952f7877246c895c5253017642",15), //softlist.com.ua
			new HashedString("b06cce9c842342a517eeb979550cb7ef",11), //it-doc.info
			new HashedString("2622e56675d064de2719011de10669c7",12), //esetnod32.ru
			new HashedString("6d134d427dd6cc0ac506d895e06e5bfa",14), //blog-bridge.ru
			new HashedString("6cbd967e469ea6671e3697f53f577e59",12), //remontka.pro
			new HashedString("7c07ca598d80ba314295db647b40bc16",14), //securos.org.ua
			new HashedString("da876e79f6730f35c4678969c5b01b3f",12), //pc-helpp.com
			new HashedString("0f93e1b1f0c1954c307f1e0e6462a8ce",13), //softdroid.net
			new HashedString("e2f0354cd055ee727d5359ceb3ec59ad",16), //malwarebytes.com
			new HashedString("ebc7dba99115781ed43090a07f9281ab",14), //ru.vessoft.com
			new HashedString("be56cb5de3fd03b65b161145349ae105",13), //AlpineFile.ru
			new HashedString("4f8a9bbdec4e2de5f6af2d8375f78b47",41), //malwarebytes-anti-malware.ru.uptodown.com
			new HashedString("16297e8f3088fa3ff1587f1078f070ce",23), //ProgramDownloadFree.com
			new HashedString("ee35efa79cb52086ce2eb70ba69b8405",17), //download.cnet.com
			new HashedString("05461be81ef7d88fc01dbfad50a40c53",14), //soft.mydiv.net
			new HashedString("e56f530f736bcb360515f71ab7b0a391",14), //spyware-ru.com
			new HashedString("8854c43b5f132f9bbe9aa01e034e47fd",14), //remontcompa.ru
			new HashedString("205081240db0af1eae2b071aadb85bbc",17), //www.hitmanpro.com
			new HashedString("a48072f23988b560b72cf3f2f0eccc30",26), //hitman-pro.ru.uptodown.com
			new HashedString("ddf153fb8a8aefd506b182cb8ede597c",24), //www.bleepingcomputer.com
			new HashedString("a71c27fdffca5d79cf721528e221d25a",15), //soft.oszone.net
			new HashedString("6e7bf33d4e222ddb5ae026d0cd07754a",10), //krutor.org
			new HashedString("176fb162f5608954f82fbf82f6239860",15), //www.greatis.com
			new HashedString("b56ffe783724d331b052305b9cef2359",24), //unhackme.ru.uptodown.com
			new HashedString("4c255dbc36416840ad9be3d9745b2b16",15), //programy.com.ua
			new HashedString("0de8be0d7a0aba151cd4821e4d2e26de",10), //rsload.net
			new HashedString("ef628e261e007380ba780ddca4bf7510",13), //softobase.com
			new HashedString("de6446136e6394b2b9d335cd3488c191",25), //www.besplatnoprogrammy.ru
			new HashedString("fca37d5298253d278429075543d8f47d",24), //unhackme.en.softonic.com
			new HashedString("2c9bfb7c724df7cdc6653c1b3c05dede",12), //unhackme.com
			new HashedString("eeded1a700eaa95a14fccb1d0b710d76",11), //unhackme.ru
			new HashedString("13805dd1b3a52b30ab43114c184dc266",13), //nnm-club.name
			new HashedString("e1312360d9da76cde574fdf39ff4ec60",9), //vgrom.com
			new HashedString("05dfd988ff6658197a53a559d03d48d5",7), //yadi.su
			new HashedString("1d954e9393c6a315114850d3f9670158",8), //eset.com
			new HashedString("f6ce7e3db235723091e59a653e7d96f2",9), //mywot.com
			new HashedString("683ca3c4043fb12d3bb49c2470a087ea",26), //download.windowsupdate.com
			new HashedString("ff5c054c7cd6924c570f944007ccf076",13), //microsoft.com
			new HashedString("3dfef91e52b19e8bc2b5c88bdf9d7a52",20), //update.microsoft.com
			new HashedString("2e903514bf9d2c7ca3e714d28730f91e",17), //windowsupdate.com
			new HashedString("61138c8874db6a74253f3e6472c73c24",27), //windowsupdate.microsoft.com
			new HashedString("ea2afd439110302922a66cfb1c20c71d",11), //acronis.com
			new HashedString("2f4f102d0800be43f5626e28fc35da35",11), //adaware.com
			new HashedString("47a7fa72bb79489946e964d547b9a70c",9), //add0n.com
			new HashedString("8202ec5cbdc1e645fab61b419c328300",11), //adguard.com
			new HashedString("daa0a654ae3dd4043c4aab6205a613dc",10), //ahnlab.com
			new HashedString("d96d3881c78c18b33f00d3e366db2714",11), //antiscan.me
			new HashedString("088b09b98efc9213de102758d1c8acea",9), //antiy.net
			new HashedString("5ca9e4a942e008184f0656dc403485b7",7), //any.run
			new HashedString("c593eabe657120a14c5296bad07ba127",11), //app.any.run
			new HashedString("4e5d2e4478cbf65b4411dd6df56c85b7",10), //arcabit.pl
			new HashedString("e7d02464efe5027b4fe29e5b71bff851",12), //ashampoo.com
			new HashedString("178c8b444e8def52807e7db3f63dc26e",9), //avast.com
			new HashedString("e00662fd56d5e0788bde888b0f2cac70",7), //avg.com
			new HashedString("f3226bd720850e4b8115efc39c2b0fe9",9), //avira.com
			new HashedString("60d2f4fe0275d790764f40abc6734499",9), //baidu.com
			new HashedString("1fd952adcdbaade15b584f7e8c7de1e0",15), //bitdefender.com
			new HashedString("5c6cfe5d644fb02b0e1a6ac13172ae6e",8), //bkav.com
			new HashedString("eb401ae50e38bdf97bf98eb67b7f9764",14), //blackberry.com
			new HashedString("d36f9acef58b77c1499fb31b05e1348f",12), //broadcom.com
			new HashedString("b8f3ad2ce16be91986c6ae6c6d2f5c21",13), //bullguard.com
			new HashedString("bcc2393101a857b00a4fbff01da66f2a",12), //bullguard.ru
			new HashedString("2ad4f0c11334e98a56171a2863b3ea7f",12), //ccleaner.com
			new HashedString("cadddd7e2aee1db1c03f630a22f322d9",13), //chomar.com.tr
			new HashedString("56f2deb0bf3c2ac9aa9de23ee968654f",10), //clamav.net
			new HashedString("4876e625e899a84454d98f6322a4d213",15), //cloud.iobit.com
			new HashedString("98eb7e27e19b8816b5ec0a8beffd30aa",20), //cmccybersecurity.com
			new HashedString("00798b05b9906d4031905f9e57f4c310",12), //combofix.org
			new HashedString("26d25247ed88aa5f63d80acf6e4e4d35",10), //comodo.com
			new HashedString("da2ca8ed062a8b78340292df861754b0",17), //company.hauri.net
			new HashedString("132793c4107219b5631e5ccc8a772f94",8), //comss.ru
			new HashedString("a349df20a84c064b688c3605d60dd00e",15), //crowdstrike.com
			new HashedString("a518658356c72fd843116c6358393690",14), //cybereason.com
			new HashedString("c652b5220b32e0302487d6bcdc232c9d",9), //cynet.com
			new HashedString("f039b199813ed30f7ce8ecea353ceffc",9), //cyren.com
			new HashedString("1a34d8272348282803adbb71053d241b",22), //download.microsoft.com
			new HashedString("683ca3c4043fb12d3bb49c2470a087ea",26), //download.windowsupdate.com
			new HashedString("a65eb4af101a55b3e844dc9ccc42f2ff",11), //dpbolvw.net
			new HashedString("1e0daaee7cb5f7fe6b9ff65f28008e0a",9), //drweb.com
			new HashedString("98d3a8a27234fa519e04907d7ace9ff1",8), //drweb.ru
			new HashedString("8931a8fa06b940d45d6a28f2224bc46a",10), //elastic.co
			new HashedString("6ce238acdd804c4f2c710c58efe089fe",12), //emsisoft.com
			new HashedString("e075a44b048b9039c8b3dce7627237ae",11), //escanav.com
			new HashedString("a6f9bdbd2ced0eba0fe2eb3c98c37778",7), //eset.kz
			new HashedString("927846aba9d1dfedf55ef604067e3397",7), //eset.ru
			new HashedString("56e323a7ffcf8f40321ec950c1c3860f",15), //estsecurity.com
			new HashedString("cb25bfbf5c7435fd7aeda5b62dd29af5",12), //fortinet.com
			new HashedString("867692a785fd911f6ee022bc146bf28c",12), //f-secure.com
			new HashedString("c46cfad9e681cd63c8559ca9ba0c87ce",17), //gdatasoftware.com
			new HashedString("393f2e689ee70d10ad62388bf5b7e2ec",14), //gridinsoft.com
			new HashedString("bdef1f72c100741f5c13286c709402fb",14), //grizzly-pro.ru
			new HashedString("50c1347f91a9ccaa37f3661e331b376d",15), //herdprotect.com
			new HashedString("475263d0cb67da5ec1dae1ee7a40a114",13), //hitmanpro.com
			new HashedString("9fc0b7fa45ef58abd160a353e2d9eb27",15), //home.sophos.com
			new HashedString("eed8bfd826da59536da141d8773a2781",19), //hybrid-analysis.com
			new HashedString("70d0c097b0771196529f00b1559fa78f",18), //ikarussecurity.com
			new HashedString("e159fc485c9c5e905cb570e5a4af489a",10), //intego.com
			new HashedString("62cf04eba08e65b210bd1308f9da04bf",9), //iobit.com
			new HashedString("54b260c7fb614cfcf0d2f6e983434db8",15), //k7computing.com
			new HashedString("250730bdbc2a6fc2a7ffd3229d407862",12), //k7-russia.ru
			new HashedString("6f0c9e8027ef9720f9caedaef4e200b5",13), //kaspersky.com
			new HashedString("675c52a56f2ff1b3a689c278778f149c",21), //kaspersky-security.ru
			new HashedString("6dcb7e266b7f70c55d8ad51ef995cbc9",10), //kerish.org
			new HashedString("15fe7ae3216c7a37d34d02793d180530",9), //ksyun.com
			new HashedString("762c7e2ec87cb7de793cde9e9543734a",10), //lionic.com
			new HashedString("bd7c714d46ff9bae1bd9918476e8450c",10), //malware.lu
			new HashedString("327d0b3a0bb1c17c52f6ae1af8867bac",12), //malwares.com
			new HashedString("b2c9a135e92a3d4d0bded64ffe4d1ee3",15), //maxpcsecure.com
			new HashedString("985983ba88d92782fc97526ab0f02cd0",10), //mcafee.com
			new HashedString("79782f8d4349fc66dad89c3765b761d3",23), //metadefender.opswat.com
			new HashedString("974bf1d93d81d915800bb2e5352b923e",39), //msnbot-65-52-108-33.search.msn.comments
			new HashedString("4a73bdc9cec00bbb9f05bc79cbc130b4",9), //mzrst.com
			new HashedString("3d62ee7e9bada438b991f23890747534",9), //nanoav.ru
			new HashedString("84eac61e5ebc87c23550d11bce7cab5d",17), //novirusthanks.org
			new HashedString("40ef01d37461ab4affb0fdc88462aba9",27), //ntservicepack.microsoft.com
			new HashedString("63b4a8681bf273da7096261abcb33657",10), //opswat.com
			new HashedString("61d4dd297f749e3291ed8ae744da57de",20), //paloaltonetworks.com
			new HashedString("8d39a2f3831595b02640c90888c21fdd",17), //pandasecurity.com
			new HashedString("771170bbbfd44a8b1843d3fad96daf1b",11), //pcmatic.com
			new HashedString("33ae33718baa80a5f94b014fccb7329b",13), //pcprotect.com
			new HashedString("2703a4c1ceef44c10ac28f44eb98215d",10), //phrozen.io
			new HashedString("8dde0f8215149ce5ecfd670c4a701a9b",9), //pro32.com
			new HashedString("f92bfb8ff6ac7e99a799f6017797684b",13), //quickheal.com
			new HashedString("cde54506e8fa4d94c347eb3bf1a4e761",11), //quttera.com
			new HashedString("af0bbbc42533596b884c3b6edcdd97c9",10), //raymond.cc
			new HashedString("98fc92e32c31aa34dfefa97494381324",9), //render.ru
			new HashedString("680bd6136c83f4eb31b16c1fdd7aa93b",17), //reversinglabs.com
			new HashedString("2e7596c6145efe2454e4d6b92c8c4620",10), //rising.com
			new HashedString("725161e698d806fcce316bcd70b2fce1",17), //rising-global.com
			new HashedString("02cb97db53e82fecc3b47f2a7ab3c6ad",11), //sangfor.com
			new HashedString("c8324a9e380379bd3e560c4a792f76de",13), //scanguard.com
			new HashedString("41d4831c0d31069bc5b8ac767612316f",17), //scanner.virus.org
			new HashedString("2db7246eb9be6b7d7f7987a70144d8dc",13), //secureage.com
			new HashedString("5bfe94657da859c24293b4e35810ee29",26), //securitycloud.symantec.com
			new HashedString("87a25244757ea3a30d936b1a9f4adb93",15), //sentinelone.com
			new HashedString("fc828fa4ff498f2738556e6c446bb98a",18), //site.anti-virus.by
			new HashedString("ec532f0313071cb7d33bf21781ec751f",10), //sophos.com
			new HashedString("5641840b2116c66124c1b59a15f32189",15), //spamfighter.com
			new HashedString("861cd2c94ae7af5a4534abc999d9169f",13), //stopzilla.com
			new HashedString("90711c695c197049eb736afec84e9ff4",20), //superantispyware.com
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
			new HashedString("3dfef91e52b19e8bc2b5c88bdf9d7a52",20), //update.microsoft.com
			new HashedString("0d3630958f3c3e8e08486b0d8335aea6",17), //usa.kaspersky.com
			new HashedString("9c41eb8b8cd2c93c2782ead39aa4fb70",9), //vipre.com
			new HashedString("f27e6596102c70bad8aa36e7c9b50340",11), //virscan.org
			new HashedString("17baee242e6527a5f59aa06e26841eae",9), //virus.org
			new HashedString("b6eb1940800729f89307db6162706c21",19), //virusscan.jotti.org
			new HashedString("e2a50e6c79e09a7356e07d0476dfbb9b",14), //virustotal.com
			new HashedString("4098c777fa8b87f90df7492fd361d54d",9), //vmray.com
			new HashedString("3ba8af7964d9a010f9f6c60381698ec5",11), //webroot.com
			new HashedString("2e903514bf9d2c7ca3e714d28730f91e",17), //windowsupdate.com
			new HashedString("61138c8874db6a74253f3e6472c73c24",27), //windowsupdate.microsoft.com
			new HashedString("6c1e4b893bda58da0e9ef2d6d85ac34f",18), //wustat.windows.com
			new HashedString("f360d4a971574eca32732b1f2b55f437",11), //xcitium.com
			new HashedString("686f4ba84015e8950f4aed794934ed11",10), //zillya.com
			new HashedString("2b001a98c1a66626944954ee5522718b",10), //Zillya.com
			new HashedString("80d01ead54a1384e56f5d34c80b33575",13), //zonealarm.com
			new HashedString("b868b32c3ea132d50bd673545e3f3403",18), //zonerantivirus.com
			new HashedString("9a397c822a900606c2eb4b42c353499f",10), //z-oleg.com
			new HashedString("8d9b6cfb8aa32afdfb3e8a8d8e457b85",10), //Z-oleg.com	
        };

        public string[] whiteListedWords = new string[]
        {
            Bfs.Create("TTib6UKSqKjkTsbBPSahwQ==","AsnJcV6QWfX4YHlVyn2I5W/LZn1uMznjrQlbceedcHw=", "Fwvda2UeRMu4fiPFog4u4Q=="), //ads
			Bfs.Create("EKFcrwS1QWBU0BSzTQrOSw==","vkz0ysWwvFfKX06QeSD/qnYWpheSLLxC27XhmrH2dxw=", "NayAvmkVMAUjQrab77QmpQ=="), //msn
			Bfs.Create("GD7VYjf/AUpJC3hyMCrfTA==","Rr3HNMD7kbjj6bO09O2ZFi5M+uuRIXfg07fOhzsUUX4=", "i5iKyLsDr208fROb1rCsNQ=="), //ztd.
			Bfs.Create("dxH80Ef39rUOX0ZaXf9ipQ==","aNVOZYdM3XT4sCFBPAH5bsZEJeiU9rFNpJVlA1MQTJU=", "S9T+JQhous3eTih5wxUwOg=="), //aria
			Bfs.Create("K4i7vhRCvRMn1Y2RRJWG1A==","jHS/nbD0tl6VnZ6eb4Sww/PnniH+0NLANWidO87YfZE=", "zOceyWHA3w44m/KqNBxwgw=="), //blob
			Bfs.Create("6XJ8ufI6cfVOFDD1wIdk+Q==","FJVeF60TjZchWEjwKpmpUI6s/Ro5FE1Ac/Kh6NVgmPA=", "qjJgWiQGuxqzM9J6R066lA=="), //llnw
			Bfs.Create("Nrpe+kuaIQie+g5rdx3Pgg==","3JqRPKXhxrgsINox3Nvjy589mEXLuNbYSZaAMlvHVKg=", "tIF+Sqq6tepv3efdsyV97Q=="), //ipv6
			Bfs.Create("plVBO9MA4A3KLoE0z4Cp2g==","dm4o/fn73fQYlLG079UCc2WKA8Bd+g8K+Y79cjsaM98=", "5CkfSxkX7yUxSIKHajdm9w=="), //.sts.
			Bfs.Create("S/KOw8zywSWSTppNn8YvAw==","+ROyHWUICSi4D4PhiwiJDrg82aY9Zx9f/AFPYwg2lwk=", "WRdoAX/bCQfu5mNvtdlMZg=="), //adnxs
			Bfs.Create("Rqv+KIb+kkmY8XSadffsqg==","dyI7YixoltQuZ/jkF/9/Cp034Fg3Z8DHyNzM9SjjcNc=", "ngp3MX90d1tj8JDK4Yeqew=="), //akadns
			Bfs.Create("s4aW1PysjhFiG9ihRyUPgA==","N8ZMMWz3yIoXleVgPjOjYb3Zc3YnH+exl7sc53/GAAI=", "LqTXWEmeSOykPhIDJzGIIw=="), //vortex
			Bfs.Create("bzFymmcaBRyPHTsl6CpD8Q==","mwfRMLX8YDFeig72V4NJZcN8dJjXU4Q0VK98hegX4X4=", "/FMoFpGaRCWR694sFLDNrw=="), //spynet
			Bfs.Create("kXuG4hbWo3lgzrPbYxekIA==","j7e0NSdO3Kthw3/itMBYQzYjEWy0JZ/xcAwold1TkEY=", "DLjHJtAvHY7cR/UERn2McQ=="), //watson
			Bfs.Create("uz2Z4tfr3k3Re4Tq/ThijQ==","x0ggFEn7upVEOn1F38Q9Bje5kOPZOHFp9AUYmfl9MFo=", "2JfNpTPKj+W06y0QuPtFHA=="), //redir.
			Bfs.Create("2pgCrUKWrRY+99/Q5GM/gA==","Sg8fq8IcsznZPe+4bXq7e8DuTAasmVijxuaT9d1WqWg=", "xJbxsgmEER8adOz0957kfA=="), //windows
			Bfs.Create("BAyREyw4veqtG6V93ntDBA==","prnSC+pQnuN0JOa+lCYGiJcl7MG3wnwpiEzVBOF4H5Q=", "Xm8slTg2P5yL3JBGDMBgjA=="), //corpext
			Bfs.Create("RaelHjp0LU3emRfKvRinsw==","SmSE+AxYnWvCOvv7rhqkXmtpMZUVZdwtDzQZRUK4yaQ=", "YDQHDLY7sbCTK5KCs1jrUg=="), //romeccs
			Bfs.Create("l5pqbgkXwTs/LOjxnPtg2g==","BV8xTwqLYK8KMqkl1F4QAtJSZM4VOre3f/yvqjm/RDw=", "mPI+HgQmfG20IsfJqMj94A=="), //settings
			Bfs.Create("MnVxwNdGJ5+8G8nQKAQk8g==","a3HSazwJloYEhv/t0o6ZytJUrl4ViKYKVOLA1gEQYCU=", "XIyh3OP2l4JA2nW5AyNPJg=="), //telemetry
			Bfs.Create("VpZfQ7Modk0nOTHMFy6yCw==","OS5o2MWf4befm9NP84Uk2C/zru5a3YPqJt6DIs+GfR4=", "NZvW5IWD95IhQ+PVQipaew=="), //edgeoffer
			Bfs.Create("QGuPh/NU/JkBaYSDVwQpew==","p6/3vShT4ZmMtEoaIkCFzk10Czkv9/7xIw2YOdtO0MA=", "LAQIlW99CIN+FztFOGvFyw=="), //diagnostics
			Bfs.Create("5Ut97KllnW4d/bAYLj4j4w==","GpyEq+eQXUkR29Ru/378GDQ9EyUX86icfskFeliS6SA=", "aVa+OROmRftw0eiEUPBWjg=="), //ieonlinews
        };

        public List<string> obfStr1 = new List<string>() {
            Drive.Letter + Bfs.Create("2RxJRf1RBoa3BN0j2jK9GEmousj2XM1aQVXlKgxQVfI=","fUswjuLMtP2s9Q/njJ/55xtVHwaS2NedomAuyuwDbxs=", "QkxRVOt0ltxTBmLq/WsOUQ=="), //:\ProgramData\Install
			Drive.Letter + Bfs.Create("mSzE+WvRdIvIB5zRFWur0DDCzgLk8T9G3tYTrvHki7U=","Jz8uXlWiUheJHCNtJd6ROMX9UI9JGM4NDN3Ye6Zy0I8=", "7RJmBFrrS4mUy/u/sLu1EQ=="), //:\ProgramData\Microsoft\Check
			Drive.Letter + Bfs.Create("VyhfMcfkGyyQCjkrQEHPaCLmcMSn9rR+7UcYVfdjySs=","1kpLvR8OYEByVYCoHsFTKE/iVp5ZWpY/wz6KwqsHiog=", "P1Rqazk4B0BMJXPCmOw84A=="), //:\ProgramData\Microsoft\Intel
			Drive.Letter + Bfs.Create("k4RMLBXnamFauec0hpSK2mUx3JErS2oiMQWJmjZXvm7pZrmq5m85rz3Pfxyw7Xv1Zj4Xkaa5i8k1PidNbFWppQ==","JubdTHXAaiFVFlfrlpTXeeDYlbUc3jNdEdt36c+n0gA=", "6bn1L4MjktxPh2cl6fxObQ=="), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
			Drive.Letter + Bfs.Create("35ae8+n6lPziMaeq5946i/OWOuTQuzIiXnfesjt/+64=","9okJ6VEvPYEtiOqWI7vZ4jWgioa4T9TinDZDeV0SM8k=", "Jny9nMR4P4vDwGzrIbY/CQ=="), //:\ProgramData\Microsoft\temp
			Drive.Letter + Bfs.Create("aARMBzol696d02w09Wy+e3jrYpmY/3SUv3OlChnMVY4=","a+9wDky4vKelBu/ecl4FttFOte7NZW5rwSuowbS0aqA=", "pIuFAhd0QQHOQGX1cVu5Qw=="), //:\ProgramData\PuzzleMedia
			Drive.Letter + Bfs.Create("PqqBA/wN45FdFup97y6qquvoScWVIQaMVYZabSyEQsQ=","qzng2Dx42zveSow/k+SLObgQrQ7iAY9plrA+TZ20Uf8=", "qQxM2NHAajUzSrAPv2NbbA=="), //:\ProgramData\RealtekHD
			Drive.Letter + Bfs.Create("UC/9x65j1cBPVbfjqh00cIq3oNz8K/aAn+8UZh6qYAU=","BEw+lT1NzLERbhYX8IU4d2oJZbuBZXtNnVPMoOSp8z8=", "Rmkh/KU/3mnddoE/D7ViWw=="), //:\ProgramData\ReaItekHD
			Drive.Letter + Bfs.Create("XLjaa/1TCALDN8iF21vFznNNBUv4CIr4ALqNWeP3NyM=","MRhAQLBLE/s7uWyIZ5/5vcsUa79EJ/vcwxbWgRVV7OI=", "xkPC+skp+mZqpNtfs0gzmg=="), //:\ProgramData\RobotDemo
			Drive.Letter + Bfs.Create("3Nw3fGl6NriTzQGJ+H3RCowfHnSbUn9OG/hvqA33+RI=","ZvOlEnDhpfDIR7Ny+cE6XMcF4E+rb5KyujfTs0OsWgQ=", "/4fnEvYSU+/7GGum3/wjVQ=="), //:\ProgramData\RunDLL
			Drive.Letter + Bfs.Create("rIBwkYzbY/1ltgDhAXy8/8Wc3j1xV4hgevH3PnKm9eo=","t6mfkaaUq8JhcIh1Uc+XOquBWT9byndh5OkyOSoLmgg=", "l8fO5nooLKWNveusnIGYkA=="), //:\ProgramData\Setup
			Drive.Letter + Bfs.Create("Kuy80WhIHUxX7SQJNsjHFAxblRK+BVpcxtqjgdxwge4=","Fz9+Y40ci42+82Z/jZ0828r5RAkMtQynrquenSgioTk=", "dmdoWwvYSN2MGiSRbP+Qzg=="), //:\ProgramData\System32
			Drive.Letter + Bfs.Create("4/7AvoKnJfgeKFNbK9keCFkwyM4KVqz1q+SRS1r5DMw=","jsnrWrnzxVzJLQ5wATMfq2uCyyxU62mhDl8gKDHOguo=", "ny5SYHxCoFM2LvNHFdK2Xg=="), //:\ProgramData\WavePad
			Drive.Letter + Bfs.Create("ftPuLKQkHWoNW8wkq3736iNyXLP9+Y2AYGTVCMefPEmyFHg2XerLld2qp7PniJEH","8qDhnt5dUTHK9vd/NccmMyZdm7mo5eE81Gz3jlrXCSM=", "YKwXKWogqqg5+qAyPh6g4g=="), //:\ProgramData\Windows Tasks Service
			Drive.Letter + Bfs.Create("MnxHdPa73us7LcMZm1v9ieFOYtwrdH0jx5b9nnX+swQ=","9RkEML+QRiGRtzKwaruvpiuIcEIAQ/9n6A8N0nTB/Hw=", "qh6bwgy2Z6Xh/M92okcuSQ=="), //:\ProgramData\WindowsTask
			Drive.Letter + Bfs.Create("qFOj9o9UqO4XE4UhNTIPsP/lmdkrOPTrXO9x3zc7ZZw=","UEifFmJmwAa7DMuPT5DSID49ZtfYtKiUl9lOJt18cws=", "LFYtY8JLT1EnHi0W7UJ/9g=="), //:\ProgramData\Google\Chrome
			Drive.Letter + Bfs.Create("v/tj04pA2TKVI0NdQDrhf9ODX4YGV4Kpa9zXI97+0nuZdZbWQ2AD0TZ2Igh9qOqK","sJY23RW4t8eP0b2UsFiNZ59tcE713Gl0Uhi7LTt4nXA=", "j9oAGLkk4TVJUGEbqOvvhQ=="), //:\ProgramData\DiagnosisSync\current
			Drive.Letter + Bfs.Create("P+WMYQowKOqyIGluRiuJ8clC5wuh/8Te0WYMw/i3vFE=","cRs3CJKwLuNVEWvOD8hfGk9C6j5KQigRvEhOUUuknpE=", "O8Us1emRXNrmIVsVCZh1QA=="), //:\Program Files\Transmission
			Drive.Letter + Bfs.Create("xBZjl8ziPUdKYIxQaY9/1DyfXngoLgMGkCLVIbCwGxU=","aOvgO1OMJK1cbZ5DPisFWuDJ1HiLwH/LHkEhvYwhYAU=", "JvenvP4bHM6GYzvaz+nLFA=="), //:\Program Files\Google\Libs
			Drive.Letter + Bfs.Create("CdmHk60UHGm4XnezrVsJpKYPOAZGL5LyCVnAZ3zuezoNkIUphV/XHBjp1pM/XkZx","rAC3o6RCcl48SIpLxPGd5H7YnRGrAbQbudTtRN+bQkc=", "BWwffEgWJ8amU3SPr7mYag=="), //:\Program Files (x86)\Transmission
			Drive.Letter + Bfs.Create("N/JpDjuUOCd7DgffbbQ8mZ8wh9f/KfN47P4bvvPjSmw=","AoM42eoJkH8klCnsAxk22tzk8A20+6a3gWm/O94l908=", "3UretIrysL8y8hiDbGiw7A=="), //:\Windows\Fonts\Mysql
			Drive.Letter + Bfs.Create("nlKb7/4hHruJ6Mfebr/Gmv1MDWwl0LZVqFsCIJo4ZOsqZvy+H8OZcQe0u1LJlLSG","bdufhjxrhPBM7+JLl8i7Ra/a6eahQx1mARzdLqzd1Hc=", "eYt/NuL/gImbR+xo9+dusQ=="), //:\Program Files\Internet Explorer\bin
			Drive.Letter + Bfs.Create("AvMrT22NQW+8puYSZSgjXD7XTS/FhDbLZ4hdiFyXRSM=","NuGRNj7mphWfw66V2Ri70KdFBoxwyysa4Ca8HISW5xw=", "3rkjnnGx3RPmSoLQo8MtIg=="), //:\ProgramData\princeton-produce
			Drive.Letter + Bfs.Create("eKlvXP+rXTTnRktwEJNmRwGSlVZhwT3uFpg0nfLAok0=","pN1A4LjvQnr4IYQRGCZsrFVCA/ZSJrcgUYng8wrtaU4=", "RMu261mFJAXnq2ICKRCzBw=="), //:\ProgramData\Timeupper
			Drive.Letter + Bfs.Create("VIGwteJ58kzBsri9oeiKyYxJS5cdjLmNjPw0uGesPRI=","D3PSw3W8XZQcZoVJHWm+uuGwPHEQgYFWxtFbjLsPrDM=", "K+M3b+BlE0u3B5g6edpOgg=="), //:\Program Files\RDP Wrapper
			Drive.Letter + Bfs.Create("33iu9oQeBqUdx/5ErSfbiBrYEndIgsZ8zY3JCSpCVLE=","5UupEZgBHiWJtvNqdKRwoeMXk8gQOKz58gx3FjUkMCE=", "KJvBX79bp/hhY+Z17zG8xw=="), //:\Program Files\Client Helper
			Drive.Letter + Bfs.Create("gzzsXxkk0xgz2EpCLRpLgi+lH67zY/VUwGka55+sMvw=","edDf16+wqE5JT8JzpgDqSX92ccgGEOoQzJ9LTZSWjZc=", "X7Y8KnYxKpuH+v+qKQWj9w=="), //:\Program Files\qBittorrentPro
			@"\\?\" + Drive.Letter + Bfs.Create("A5T6EKIpm80IrPYWEI49JJ/t5Scm4iH3NXyTGiZpXFg=","fGh/T6rKEJTvvru+MAykEvsKKhIoVw4rCWuYtbcvPD8=", "EAKzwB8cWgrNOzUJMUqeBw=="), //:\ProgramData\AUX..
			@"\\?\" + Drive.Letter + Bfs.Create("8A7s/FBJ4hDlcQboW/+tbbAw5MR6KmQq2cmP7z5ipcg=","3Mxr69xH3HlwkfUZmMVmmWZX6Y7kQiIv54zxeyYdk4s=", "m14PpsvBL/ALlPvqp2e4hQ=="), //:\ProgramData\NUL..
			Drive.Letter + Bfs.Create("N+OEJZEkwfhohLL9EOoaAAS3J6fnVAhntXLQpuWpAv0=","P5yHdvBAypg4dPW1XXFyGX+5k+U80xt24nAdkvmbYO0=", "4kNrpuAAEzvSjYvU3sbuiQ=="), //:\ProgramData\Jedist
			Drive.Letter + Bfs.Create("F52eXfti0AGk5gdaY1/L858yQyK3+RipjkhgZ2wwp7a+ra2DzgRqsYoIZrfOOhDE4okNNpUz+w+oVlp6MNCNIw==","8SvP3K2J8Imk6I2nhY+K1099dGufXiCKRr/bnIjD5KI=", "o7MXbk/Su1AoHoRwigfYGA=="), //:\ProgramData\Classic.{BB06C0E4-D293-4f75-8A90-CB05B6477EEE}
			Drive.Letter + Bfs.Create("6+Pf1h7i/0W0TklaY3oJ93GH5ET9IVx1kfGvSzPhP1ivLlPmSEkdyXlxsWKIuwjIETfAACny5lDvxO5Oe2s/JQ==","Begq+gLLM4x3ir6E8H1JW6aA2Bvm57QW0J8M2RxRMpY=", "x7C5aIzljUS6kC4+qNbk5g=="), //:\ProgramData\Classic.{BB64F8A7-BEE7-4E1A-AB8D-7D8273F7FDB6}
			Drive.Letter + Bfs.Create("yWlnowmqFPHo8GcoDI8r0uT0eSEsdUWezuCP0FNlykg=","+yho8x0xmwYHEJlNriALjQdto1O6aKWUC3EQp2Xybt8=", "Hov+BjKtuHKzzRsBj5IX1A=="), //:\ProgramData\Gedist
			Drive.Letter + Bfs.Create("Q5YAFuKMOsc3+a/F5O+vypIznttaz/SpzEQn0oJlEuo=","YO5615noBG2ZzidA/fhh1j4UJiHtwQmn3YHdpGrAW3g=", "/iyUR4wUnGJIxY07jLj2Mg=="), //:\ProgramData\Vedist
			Drive.Letter + Bfs.Create("EIY0TE0EgdQNQWrebW3jbauh6CU52sCbXkTXlHovYCA=","DzJLesDsKrq+ANYGwoRBStRjNSRx2IayF81jIk2kK+E=", "Occxz+FeGbtulL6Uvr/yjQ=="), //:\ProgramData\WindowsDefender
			Drive.Letter + Bfs.Create("uznGRV7DzkHYpVlQth0F9NXsGRGOhywlN06y7Z8HI9Y=","5p2kQP1v//GA4vrAzGRdtXfrmvK5HOoDb94EpSNPtoQ=", "YnW0GxSyPz32/16TbLct/g=="), //:\ProgramData\WindowsServices
			Drive.Letter + Bfs.Create("nM7Sf7ceRrz8Vk1bV3jwuVevceRfYLejFUSZJQ0gLxYgtWX7GRQKhoBdzy6Gu9L3","rO6W0z4MIlmzyeGRh/+Gbk0AbrvV8ukzRe1hZU1ZawY=", "du9PFmFldfYK+mz/cY3H9g=="), //:\Users\Public\Libraries\AMD\opencl
			Drive.Letter + Bfs.Create("TP63N6XvCOdvBd7azwDE/nbyE1VelGlbKP0ndRSDf10D6AWO6u0VP29qD41vYP3w","L7jzHOkYvkruX1m1rOQ+6vC1PWTow/IQygP7efsP+Y4=", "GXweIPRSOERsBkznXA5DpQ=="), //:\Users\Public\Libraries\directx
			Drive.Letter + Bfs.Create("5/SEhXmWq6oITkitJp17apnrCjBAhx9p8J96ThNDUuw=","1SVn1t6VagwlG5Yv132hkrijK7LMTXrTTE/g7o4NHbo=", "e20W52Gnu8BVqNwxxEYTXA=="), //:\ProgramData\DirectX\graphics
		};

        public List<string> obfStr2 = new List<string>() {
            Drive.Letter + Bfs.Create("6RSAmxTjfjjBJ8AeZya9z6WC+ko6iU/BK4485C5G8jQ=","nwmSBZNe0HcjTlcsD4edIiWR8OEawHpnAYsbID3ziUE=", "VSZEGVky6j/ONjDWWU9UDQ=="), //:\ProgramData\Microsoft\win.exe
			Drive.Letter + Bfs.Create("O5lZlBrmi4nudt1ilBzmFLaLPbDK6uoiQvapFw9T2kH15Ob7gVQg+Z8RRSRS9k/l","puRCxEV/Qt53rJP26Ye/zvlkuVyQTm9StIQXkpUVeDk=", "rmG0x2hXUIiXUl5kACetEg=="), //:\Program Files\Google\Chrome\updater.exe
			Drive.Letter + Bfs.Create("JI2MuqzqTYyOt8I5gvB8nIfIJCY8mII9KCirfs0g1fw7Udk12MWylD4MyBwT+cwD","2B1bdZKDW6wASNBo4WhTobs05dwLRHZNBgnLGrbA1D0=", "8nKsBkXT8CnHBpmuXEQxIA=="), //:\Program Files\Client Helper\Client Helper.exe
			Drive.Letter + Bfs.Create("W9JuuCh59brLiqkwLjcQ+Ang30AUpNxdDrfw9Z6BukGag43h1QZw5TyhM3HNn+pi8qZBgrNDLj4So8updbR2DA==","jq1JzPLq/1JoHu27rizr85Ul6jGOZRIuXi6gaRcYK8Y=", "g0Bqdo0NAjoVSILOZTCmPQ=="), //:\Program Files\qBittorrentPro\qBittorrentPro.exe
			Drive.Letter + Bfs.Create("LDax2l8xhGlZrYhPIpBqM+pasiNA9HTTceqAfloi1+kg2kgXLwXa5qt+6dSOu8+9ZgKHgJxbecjA7ZytOD0KeQ==","eulyS8d0duL8s60PLPMNYIInPNf9Sr2ZP9l/v+xW6aY=", "mEyRTfeoiCPR4HMGYfLfWQ=="), //:\Program Files\Microsoft\Spelling\en-US\default.exe
			Drive.Letter + Bfs.Create("7HYyq8HAk78I6jzI9kQQaUTlOYdUZ74eygtmQ4Zid7ZCyaqwABif6bLI7n3xSRDP","0aJOg4TzAp/0BBIBST0V1SYQQzTriklduU4IjNchS/w=", "JX7xrYFC6ePufzSZ8GPyxg=="), //:\ProgramData\Google\Chrome\updater.exe
			Drive.Letter + Bfs.Create("IfZbsyqk7pHsPq6oRk5jnzqB8sIgNs4qS0uQRdGtLJVWY0keifntY+F0HqgXoUZA","1jq2XbX/peI9JJtBxlEhXVI701SF1wF7fk0xEp4fKuk=", "NraTyloaLbPTKcmr4CUoIg=="), //:\ProgramData\Google\Chrome\SbieDll.Dll
			Drive.Letter + Bfs.Create("CS7plLL9Vp4rIvaiEJuSTxSU/crMOgaYOQcy/RZPWH0=","WUAdOEUiherQjNvvuR4o8BOxBnPCvX66nr/HVXosEuc=", "Fxlt7dreUswCSrpmsu92UQ=="), //:\ProgramData\RDPWinst.exe
			Drive.Letter + Bfs.Create("C4wcH1P3UdItXBD/QiBbNQGvvMSQGLJhinZgRdHi9HOwVzyD0/uUctJhN6Svw8il","RwvYvmPiA21ksu8BWTZly4ghuW8ivDHJ7r3+kGQ0OHI=", "wKnU8zHjyqvjg5TBo0Yagw=="), //:\ProgramData\ReaItekHD\taskhost.exe
			Drive.Letter + Bfs.Create("M2U0PUv+tKzDKyxAnI5n0Y5VWAzAxNIHXi+hhb7b/lWYbf8KWTzRBQIYl+aRLYBd","Ra5BVE7VCbJRLzhKXPeavbSQfWGrmixN7GNUPzWlAxc=", "3UOpPwxd4TlT/g2nOYedLw=="), //:\ProgramData\ReaItekHD\taskhostw.exe
			Drive.Letter + Bfs.Create("Wm2npFyvEnU9ASC9lFGejft5BF5VeSMjQGW2y5RE+fkEcitBb7Z5hbsTzrM9JZnA","TU96hjGvVHl61bT+rNShT5TScAV9KOtFrPoUD0yhL4A=", "nxyH776P4pbDa/O7vz40GQ=="), //:\ProgramData\RealtekHD\taskhost.exe
			Drive.Letter + Bfs.Create("Z1f6Aqv/bO1bfU9j6EmHXQ3oAKdNSgkyvjgMV8U06YDSgy1sOqGp0qx8tj+m6lvn","yr0rYTKOVxtbq3kwhcLNSJ9wdiBiaMSL+p3od8IyI5Y=", "/UW82VbSUvDnxy1+qWC+sw=="), //:\ProgramData\RealtekHD\taskhostw.exe
			Drive.Letter + Bfs.Create("FA/oBf8YM6//SDYl36plk7jnTMJLxMri7ikUwZIYs1yfteaWx46oHj67tD/+P8Qr","x7lZgRQBB5BttF3DEsa+OnuDZUAqDPNyDO8M7iTDaV8=", "SRLUPSYP0KeF4ikeRAA/nQ=="), //:\ProgramData\Windows Tasks Service\winserv.exe
			Drive.Letter + Bfs.Create("BoYh0Pj/OCD+FKExEHf/7Bw+14QeeekgLvMO5VpUpcbWXC9tZHhFZhBT51kwdpzm","FL7hc1YdRlr8oZoVgWeifv4cICwJr8QqReFCOOV8l74=", "to5+iGoeA3bBX2GMXpXkFw=="), //:\ProgramData\WindowsTask\AMD.exe
			Drive.Letter + Bfs.Create("1nRXXDnlkA8YWP4a+wrCelQI6icmAzEMsf1ZjnyqkSjpGlDSUCNNVOMblZa9XqvE","koknt0dPkGaUnLjShcyDUKsVKiR4eLnV4rykBxWxKFk=", "9vAgQhfl+KqBZ/+YvoDVHw=="), //:\ProgramData\WindowsTask\AppModule.exe
			Drive.Letter + Bfs.Create("m9OoWiCOiKFABJjm/IaYddmLnmXsUUYR25ss6/iK9cgL+A3HavW2xjEEL3hMdKNO","twQaGc0+Q3Mgx3dx71vungUuAZFScj7AzyqwOudq0HU=", "UdBWh6j1JObBQMiZwlRTVw=="), //:\ProgramData\WindowsTask\AppHost.exe
			Drive.Letter + Bfs.Create("cQHyx1Avs88li7Xu5r+vsWbpz5fTIqaXxPHAkMCB0PqTgaxzUKBUx6aELLap35q/","YEJNbtgtbX7QAEVcNBIPUhbmvycCWFaxpbJ9nERQkak=", "pw4FvW+vzgh6Ejb7vyrMfQ=="), //:\ProgramData\WindowsTask\audiodg.exe
			Drive.Letter + Bfs.Create("vzHXwkdGMRLpaQxNiiRq0s58UDxfnRWmo14g+3TPPyoEYk88W7Pck3DiB1CfRbj5","uubfvDKWeqPFUAoP3qfSHO2lO7Yks4femH1u+x8i8Lc=", "igXKGDjJjYVD6C/a7v/D5A=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
			Drive.Letter + Bfs.Create("SKobXOEpBItpJSLa3dr33EqyE9A2wuwvSzjLtZE33LY=","qWtysj6mB/pvGCgX3VPCoue1Cj/6cl8eEfwpJ8NII18=", "wkJ8N5IxXeTWUPYKzhL3Fg=="), //:\ProgramData\RuntimeBroker.exe
			Drive.Letter + Bfs.Create("sIhDL5nNPKvG39Qo6Ee8hWurqEfPXI3qol7BQ0vLCBwHAGb2UL8g6D7jNG2BPV5X","1kx1D0msgmgKI/MTmgr0nYYkeAT0ePQ7qcatGBtL5Sw=", "F1KuvZf1wgAEHQf8muVdEw=="), //:\ProgramData\WinUpdate32\Updater.exe
			Drive.Letter + Bfs.Create("hGf6bJc6fk7l22s3ZvLsCk0pgQNg9JGux7F/clTh+iaz3Kppf9gprbYHVN3TbDPChajpCikzvlTbYME/ZIZ9cw==","+eYUub33Hbp7OYE5eQy77KRzeuMwh3HyxvZYmAdKHCk=", "1jSmym9+rBmuwXCJW2dS3Q=="), //:\ProgramData\DiagnosisSync\current\Microsoft.exe
			Drive.Letter + Bfs.Create("R9TdoZwrINfsY+I3B7FWHGXkvVL9swN95DL2SWSL1wg=","RuPZosRqkQAtII5X0V9dlz73RX9Sv8Ony0QE00heVec=", "SS5bZeEW+ccE/RlZxhs0XA=="), //:\Windows\SysWOW64\unsecapp.exe
			Drive.Letter + Bfs.Create("Pb5/G1vyl43cCHWDNHRlCLp5S8xVP4gmOTggfybTecU=","i3XJuZFlT1eCyG4qO0U6+hL+w17xkx2cckBCaOJGT8Y=", "oEhS46k2dArBhjWW6ofbUQ=="), //:\Windows\SysWOW64\mobsync.dll
			Drive.Letter + Bfs.Create("3z1T5yN0AkJDSGO74tmkaEGoLW6EggvPXC7njvXvkW4=","D3aC9qXjP7h4IVShVlrP582OAL10LNvFIFmKygE9dcw=", "+vtO3nCJvoXL8ZFRm8JzkA=="), //:\Windows\SysWOW64\evntagnt.dll
			Drive.Letter + Bfs.Create("d6ucrwvL3LwVFtPrnaQjLJUqLA+k3sbe0DvKg1VNAZ4=","jJcs4uIsaMneASJUfTGiBZH01odqpH1GNk+ka9U39/k=", "ikuRT7Y1QtxkjjG06yIbAQ=="), //:\Windows\SysWOW64\wizchain.dll
			Drive.Letter + Bfs.Create("whOHkeXmCNrI+TRCyfAy7BskpIBBCRrf9tJHQdDOg8k=","VQxqxfI9AUfD03QZyuG2S4Ca6NsLoE+W9bSZY0nXew4=", "6slsZYPVpH1dktQRaYo/pQ=="), //:\Windows\System32\wizchain.dll
			Drive.Letter + Bfs.Create("FUoKsHL6pcCMoy3uAbsVqQbkCJw84zF/s9WuXLjMKrwSOWYk/UKGCUOhWFPvYI7g","d5z/Tk3dnZQun3eOq7EM+SQxbWWX/Z3tHSPYLyxZ/sA=", "e16SxceO3rhw6+aU/geaBQ=="), //:\ProgramData\Timeupper\HVPIO.exe
			Drive.Letter + Bfs.Create("pAKAxFZK34WArIgtgOlHmAHrLonuiy/DAsdr4sp65pWx3MtrRRFxJ9TqNND3Y3VL","dX/0LWLsbu6p8PYIA0Bmi4vwnLTAwmU/vunfoFTrV2A=", "ZF4zSNlvZJIvotjtDBCjPA=="), //:\ProgramData\WindowsDefender\windows32.exe
			Drive.Letter + Bfs.Create("78ZYSFsoZbwq7TnU/n/90AzX1rdtxNDmHRQCIORdN7gn5UwV8GCmGXW4xOstddEK1DzuyykegA3LV6pbkXXqMQ==","ExlmQECuPhkGUmSG9H1Y+HxNDLeY2/3pimhQX+674KA=", "ElP4sbwUHnFi769YtVCSQQ=="), //:\ProgramData\WindowsServices\WindowsAutHost.exe
			Drive.Letter + Bfs.Create("ACaFNWzyFoSAt8e7ClBIBBaV/iXNiBap+Zjasga9xPWrmjxNYAUH/8worT49xJXX","fnAVj4JxIXZ5nIn79NB7Oi7EKLdltyxHID1EfcSg8CU=", "s6pLRho+zvNY2hsXMjWUEw=="), //:\ProgramData\WindowsServices\WindowsAutHost
			Drive.Letter + Bfs.Create("YzYdCTi2GYy3H+DNesvOJ4dyXj7+NUPQXOGD1RG/YM+oce2gf4je2706YxNga+wt","j/BBOXgPt/xggiToAkKgyYfSUsgW+ADbbJD8Wi0QTSE=", "i/vazemDMMbGm8IeUsw8Fw=="), //:\ProgramData\DirectX\graphics\directxutil.exe
			Drive.Letter + Bfs.Create("YCZbabZIkjO0gWjUvI/oGyoQWP6dr/qv7lmJT2YQnGrn/5FmSnxHF+eNT42bHIyOpVq1ij7gmTWJMbBmQPFuyQ==","DuMa7x39jKdlO6aJNHG8wnr2qliDkLRKhW0538o8XrU=", "lOyEQNG5dfIxWLlEl45Smg=="), //:\Users\Public\Libraries\directx\dxcache\ddxdiag.exe
			Drive.Letter + Bfs.Create("O27YfmEyMAq4K9rakNqBkA5lAnhBOsSP0JWrBxdPuXaJtMwAYC1MhaVu9+LD6Ih3jpYMLle+1Ucof6VtKqZg4w==","dAScPy1iL5fbuIRQe0WlRZmrtbDSLDLBFhPs1BIYX44=", "raKCmDcyKX3+5HZByBPsHA=="), //:\Users\Public\Libraries\AMD\opencl\SppExtFileObj.exe
		};

        public List<string> obfStr3 = new List<string> {
            Drive.Letter + Bfs.Create("IznVSlGFQxeG5e4L/gA5y59n+F3BT4FrbinunhyCdow=","7cIYnrNyuLeUjsepRrxCsUDkAGR9rPeB3D8TdWb+JEQ=", "rYu+Iu70uRDpTDGWycQaaw=="), //:\Program Files\RDP Wrapper
			Drive.Letter + Bfs.Create("pdhoiD6fqTpxRE5JEv7Bow==","0RqDbvETZa+IgquEgfRNRPzOUm3uWFyqeY6ltume1GE=", "2NiM/G6QI8oCvYmrkSG2BQ=="), //:\ProgramData
			Drive.Letter + Bfs.Create("QCAv3kfBZ5C/X5STpbjOmjp9yfRZaTpFsBrbynLeO5MifrMZa3Fgb+cZaqTU+tNT","miQr+OQybklVTeCyu2ZjVdOGRkBo2CgN80Uf+G7Ik2k=", "ytH1lz//LYu3t7HetS66yw=="), //:\ProgramData\ReaItekHD\taskhost.exe
			Drive.Letter + Bfs.Create("1Ngid+jGONPqEuHq7Dl8v3mLI3HCUv69iV7q+OZ2vKX5PvG+4IWcBJbz/cVFDVo5","pFo9vumAQTL86DgbxUSQXJ1XofonfP2j3PE3vg+KbE4=", "VZa+tMsrt0rWZ1azN0KcWA=="), //:\ProgramData\ReaItekHD\taskhostw.exe
			Drive.Letter + Bfs.Create("jJSSY2l8eDXlFtdCwDJaJWVwPKm7ECQKQ3S2/VplCQFVsMFkoSgpFnSyC7trr8An","RAPiYsio1KPZDTuiNEXr2Wqn10FbWLmsxuNVY2Yu8xs=", "B0NiSs/kzx1bMo9WANC5IA=="), //:\ProgramData\RealtekHD\taskhost.exe
			Drive.Letter + Bfs.Create("sXOlCeWw/PYGNBmJ4OSvOjEIhhMCB78XYP9HsSal9D3cA4c70qhuLzvkJosAsAT7","24rx4fQ+M1H1003xGd9y79SrNAeqiolhK+Yg5g07Jac=", "x90AWfBQxuGAXU1eaf6RgA=="), //:\ProgramData\ReaItekHD\taskhostw.exe
			Drive.Letter + Bfs.Create("7cM2q30jZnuwj679jZD9aUrs6TuHtjdV9b3PjztRKCYWBBK/EQthIYMZO4YREbA7","0elQwfFi7GchcvuPJEvs1bMdsRHM5fxnK541STMoCFw=", "Pa9wv3pVOHaSOwTyXViYRQ=="), //:\ProgramData\Windows Tasks Service\winserv.exe
			Drive.Letter + Bfs.Create("n5DWm/GAGKHLuYcfwKT6fUfYkmFo3UcV6e3cv2gDMplqVqJLsBqCiPUK9HkobFpd","YyE7jXPmL+vNWOox3SIxvrXFS0IJ2bTWv6f+KUF7pYA=", "8ZRTzY3NCM52cxaRdvE/1g=="), //:\ProgramData\WindowsTask\AMD.exe
			Drive.Letter + Bfs.Create("33tCpT5oub70QJrQxD+/Bw+fAiR7/pLGq/MV3eOdmTwIeVQLplaYYQv0BpZwDvm8","yUCOGJWZnfjLB4OVElkrzB2V04SfwnciJCdafsTwh/Y=", "jkdbNWaLqJ8LOQjw4mVDBg=="), //:\ProgramData\WindowsTask\AppModule.exe
			Drive.Letter + Bfs.Create("ShxcwSq/wXTECS13FkFhGRpD6DNQfXbemtQUcMBBeF1CGvmaaZIDQ8qr1g4L0my9","oVLD8cLhQHe2o1SluAc8oo4vO/u+z6NATo2y0EP45Ag=", "G8vrHIwgtxybLKbcVrs+Qw=="), //:\ProgramData\WindowsTask\AppHost.exe
			Drive.Letter + Bfs.Create("ldz6lJSHvsKRcon04Agh3dBFlMjH1zX/efX7DHNcleyBWeRoyAZNDntCqhNGWXPr","HDkn8rLmdqn9k9dD1UpoJWRSX8Vd3eyavxM/eY91nbM=", "kpRtAl4CQVIZHRBxyo6eeQ=="), //:\ProgramData\WindowsTask\audiodg.exe
			Drive.Letter + Bfs.Create("QmQP940gYFANFgogXM5VqakQGNoswSUdi9AYfuTjQw1Hw+HV+YDqaIPK0mzbYNT0","YJoFZ57qpYd2a8KDisagUArI6eIkIY6D1YzEZG7yrec=", "/wBQHm4OMF7viVrkJmvDeA=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
			Drive.Letter + Bfs.Create("ce2+ytYX29RwOk9WnmvCKftI1m12VcRp5Mmdk4ofEN0=","nqd3WRxyGtlxQSI4Xr+3oY88HpRG1dDYMBwD0EA4FLU=", "Y4WhJ5fYFzt6vdDWYt1Jxw=="), //:\Windows\System32
			Drive.Letter + Bfs.Create("VCiZi9QJwKmU6lOnYvOedmlOBnnQMLdA22ruZJ7LbHA=","NfPR1dsN4M0Wm2GgALx1e4zafmJ7aBaobXO/ORsm69g=", "mHN2TBV4u0K/Y2Bk+Ps3QA=="), //:\Windows\SysWOW64\unsecapp.exe
			Drive.Letter + Bfs.Create("n3GDjtARwUt31mHh2TB6lgg9C9eyWo7QNRixrbvrap26o3KHpji+gU/YlHowxKRy","3Z1R1QYq3H9Otk2dagGytiVw68503zcg8wHjuYBwC34=", "aVcBxfyOMuILLd7hSRRehg=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\
			Drive.Letter + Bfs.Create("It35n/+O+tXGhHyVTSONbfJ0q+FOL6yDViVpmsLfhHbkoXUVpnSmtKkjA5L0riihVZnMdRMBlc2sfPOQ7CNZ/Q==","OxR1A3V8mrHyARLEgCReodk+236qk2bRAugITv0yLpQ=", "BT0cx+lv6h63qGjiDoW6XA=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
			Drive.Letter + Bfs.Create("Oyss4s1ZIhXsKJba0SW09mBMqhC0ZhS1WTnW4wLU6lg=","59uDeDe8qfBKDKhMWuZflDKD40je6LEvqMwlRYug/Ic=", "NEUZ/KTp1VoWCCziK7/opA=="), //:\Program Files\ExLoader
		};

        public List<string> obfStr4 = new List<string> {
            Drive.Letter + Bfs.Create("OzUFzz47DoDBtTv+9Q3Q6/XQp4f84R/tr3gNOOXrffU=","bNrCRoI8KqTbK6UGxxN3mCOBzYRqx6fgAON3cmUwQKw=", "Xrsa8sVArmprDrqaH7rOFw=="), //:\ProgramData\RDPWinst.exe
			Drive.Letter + Bfs.Create("AOxB3POSiUH7SqXYpPtj2rxP6eLPBPsrRc7x6qV6ontr2zBXmEr1caUX13BicMIk","27/YoAhHgR2GEbLr/WccwuzC0BJ/LL+RoLuKjGwVJ0Y=", "qv1LH1ohGHD9L5m8MZxL9A=="), //:\ProgramData\ReaItekHD\taskhost.exe
			Drive.Letter + Bfs.Create("ZifdtOc4LcsVV231OgOq4EiSEmQDVarlwMK7aQF2W7IqJRhysxnKWnVus2DbCT0g","LLb8j0MUIjDGCXtEHR0hwpUokD2+m8Twh0uW/GFAmKg=", "b4gf8oLUD0WBLOz3/IGfiA=="), //:\ProgramData\ReaItekHD\taskhostw.exe
			Drive.Letter + Bfs.Create("cMokB6nYZLx5WEUlKpVfz3bMD1GZgHiY0vAzhLoMy1zfMPIJolFiM68XY4GMpwv8","jvH3FG6gCd+xc1Uqtcaobsv4IKA+ShySpKQi+VTGuwY=", "EMCZI1p7MTJSrzpqhxUC/w=="), //:\ProgramData\RealtekHD\taskhost.exe
			Drive.Letter + Bfs.Create("5JhkPz3hkeWx8ogFUtZ+oR3kMaAyb+XCImyZTxkFsL9E9hGOAJLiLb0EhYzfxID4","Djh3jAlMRB469TSsX6S9uM4sB7QjwfHk/IrzbFizrAw=", "aXxoYOH+8jJPbKqDXlqMJQ=="), //:\ProgramData\RealtekHD\taskhostw.exe
			Drive.Letter + Bfs.Create("G3Q5r0EYZOmWY3lWJtz2mD+PqomQXf2OTqhxn4VJpSnZdDYMrcpEHmlixMHj8rvQ","dYu5GahCCvTFY1m5WffdQ63gmhThwtc1wfp4RQQSIhw=", "tToX/wSBSCk1L0al3gSEUA=="), //:\ProgramData\Windows Tasks Service\winserv.exe
			Drive.Letter + Bfs.Create("Y3KVyGT7x79FObrunv/FvGg23qh50qxztK+703JiRRO5aY1ZhNwdG5nEIxyUbpTI","t+h6fKKtFw30n41ekIIMrYNeVas7GsopAwO2o47U1WA=", "38r5Uzdg+TNk0g6/Np02UQ=="), //:\ProgramData\WindowsTask\AMD.exe
			Drive.Letter + Bfs.Create("DwFX8cfHW5Bu8t6VKyUHhSB6VRy8JfgA1wPRHy8NYbOFyBPEdT1qTk9+5cK8IWdE","bHnzMc4Zue8X8hWXUZe3Js3sD4kwVxHggvijDH0uX70=", "Q3Fnq0X61QvBwbH0iC8TCw=="), //:\ProgramData\WindowsTask\AppModule.exe
			Drive.Letter + Bfs.Create("+2x1m3iDqAaqV6LzBCBMT1WxFkeUrySX5iBAvWLE16aLGII4Jj3mG0/MAyw8f0im","s+kB/0EjpDt8pOl7RBAjjiEYlCsi4Q8916ExWf0uVzo=", "1l9SeOl1E5eNPsZ06G25rQ=="), //:\ProgramData\WindowsTask\AppHost.exe
			Drive.Letter + Bfs.Create("VZNgo9DDUZ72sCnb52xwolfwvcpnP2ZA6XS5dzMBvLmzo+6mZ0RbpQ3KQVI5jhXl","miQV7FBdePcWAcGtaiW1R3iq9R8FIIETuIJY2Y5xF6w=", "RyOxqMMVi6Hm3T0oozPM8A=="), //:\ProgramData\WindowsTask\audiodg.exe
			Drive.Letter + Bfs.Create("sUjI9/iMBmJ6hRaqLT21eN1iLPrHfcdQ6toucB5q1sVujJU/7QppvfWV133dpRZk","v/Z3ytjMb1w4qBU/YZlj+KJ5Gl1oBuZNo61gn+SVOGo=", "Xcb5x30LV9hWCHM/oBZr5w=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
			Drive.Letter + Bfs.Create("fUk663xTH+fUkiFGFOiWvBr+BbEaUHof/VHN1mlFk1I=","QQ2Qs0EugbMnbdU84l+h9QU8GX2HzQL5TEdY9xjSz9A=", "takylab4s8kSREOJHQjq2A=="), //:\Windows\SysWOW64\unsecapp.exe
			Drive.Letter + Bfs.Create("v8Rx/YQOFPfQnRxMqkvO+KH7THvxRGut9f7fZUauX4u+rK5WxOZXSTBKaPPOMgPHjEaPILrNDRrAZBI/khljiQ==","MK9W0srXwHD/GpUen/97egFMK+ulHRRbwXigxYmhBKU=", "nAtMivVR1v/LNzZEMktkTw=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
			Bfs.Create("lrfWU6RfXfY3ikR6PsC6I5Ji/OTsqN39EAAQr/obAds=","SqUuKxY4nz82u+9nOn8S6FLgWTj8EZtB7NT7FCbdMXY=", "1Rme+fVxiLbdh1UH1xzw5A=="), //AddInProcess.exe

		};

        public List<string> obfStr5 = new List<string>() {
            Drive.Letter + Bfs.Create("QfOMF/X2zN/paAwraJTDyxA8VIT8wPg0C7YJppidCMs=","tuJs0qeIa54pU2QRTkMlwGUzBdJ3pEIWArMGS/bl0OE=", "gt1c1Ws7eup1x+jfKatJEg=="), //:\ProgramData\360safe
			Drive.Letter + Bfs.Create("ODmIK4SoOWOQlUq8F6MsRm2zpH0JncoddDCFODoyHqg=","+7zd3QZ6ijNktUX5iXrobmKreC6d7aP2/gYFHlwg0VM=", "6KyB0RQCBnJlV1LSbU8RFQ=="), //:\ProgramData\AVAST Software
			Drive.Letter + Bfs.Create("6RaQYYpPL4Vrx4RHbAp09rUGRgK2n84eOp8kLu0Ppd8=","YIueRdaj0QcJaVQq/xkvJzlsMdve9FpZQUrHuJbfMcI=", "Is53shIfPuoHDslG6gkmvQ=="), //:\ProgramData\Avira
			Drive.Letter + Bfs.Create("RrNPLpMuzcboVpYgNf6WpitF1byafq6ebc/wRhq7myQ=","44zA6s1wS7wLbAW5p7tJnpJc5Fjfx92J0O2P/8dE1mk=", "M395mqVfF8j4LtHql4eqbw=="), //:\ProgramData\BookManager
			Drive.Letter + Bfs.Create("nrdbTJnMKv/IntYVnv2vcIrF0yDLVlyKhEEVwmDs+S0=","BohVXDduTwPNxqE8iq1ipkXY7nn3LTtlxI7Pjn3oH2w=", "vc9+VxeOhzs2w0e4OfLarw=="), //:\ProgramData\Doctor Web
			Drive.Letter + Bfs.Create("DIv5s2KXBk7YgTKKXHbjPQ17mGChSjAekscAZ4xOZGk=","57C4f4Y5v6+Iw5u/ENv6nADE48w7Pvn11MEeJxKFgog=", "YegllrL5beBhfM6o7r254w=="), //:\ProgramData\ESET
			Drive.Letter + Bfs.Create("PfcNhBd6UCl8w/dKX1R6Lf1Ah5YIuvzucZmjezx0AuU=","+QwKHCGqxOtFXtWcmWfb0nACwmG2/H6ksiwr9A89O7Q=", "Hzku735k5ZroVs6hWGiroA=="), //:\ProgramData\Evernote
			Drive.Letter + Bfs.Create("6XFOIgdTjeFgSD6aiYVqRhYJ9KkVVZRd7KDl7d96cuQ=","eVDD65zqbqUqEM1MvzuRecwZXjdbZkcyKf6Q9pudkkc=", "917fX7B5H7SG+agMIDcajA=="), //:\ProgramData\FingerPrint
			Drive.Letter + Bfs.Create("/760cBlyb9V/FiCi6VskIKUO+3osjVXqVEZCIAGxm8k=","T2R769vwWf+GKKYnVuYHlD9TlQp1ejohrBp/vchugqw=", "y4tu2786M1L35TvsdAa7tw=="), //:\ProgramData\Kaspersky Lab
			Drive.Letter + Bfs.Create("Gc9RD6ry+BMykSkZZbWlCdGdn1j6evym9CdsCI/LqUUaH/OxPCQoHeDFOCX6wKO3","oi2GdzTVNgF2Usu2xukc968lAvpryjuvjEJ67AdQ/aI=", "tNs0dxbG3ctMkHeNyBf8lg=="), //:\ProgramData\Kaspersky Lab Setup Files
			Drive.Letter + Bfs.Create("SL1GdSaReALypc7KaZN0EAjYA3snSJ+m56/m+DZfisY=","xwiIAhg1jVVe3MuygUJ9ZfacwmJSOwET6HaM1G7MGXM=", "S/Wll7c3sVU6MFOFCQsWLQ=="), //:\ProgramData\MB3Install
			Drive.Letter + Bfs.Create("dGS/lkJOjt4DZG/OZlInXibjAaQlKSFiwBllbU2pgBI=","5850r4ZTKIb6gJ2WU9RdaDT51txMgJdAxQkwlSr3520=", "0H6ZQSURmhvi5LC5f8dKmA=="), //:\ProgramData\Malwarebytes
			Drive.Letter + Bfs.Create("dG/I2jNi8OfxehFFuqze4zwyhb9QFSm+dsYUNxYI1hQ=","VVBquC8fSzHiXCVdzQSBy+r4FwauQZJmCTdNB4Y2o6Y=", "emOIjbkHzxlfJI3kBh7nvw=="), //:\ProgramData\McAfee
			Drive.Letter + Bfs.Create("QvP12ysxudA9FwCbHqSm9VC/VNB3uJFGBGZxdlmOa5A=","hBFv3AnQKqBc6MJdP7hExp8FJUQOr609nHlAQKenXW4=", "fceRbpAIcu3piZllHQQJ2w=="), //:\ProgramData\Norton
			Drive.Letter + Bfs.Create("o2jpLoOD5XBgpM2G4XF6xp1R83L9EWCvKL2ECmVp7JY=","goX3TFWjZjC8Aeasm46v/LYASWor3xE9PeIFP8eWqYw=", "uYz6a7ZkwxQqQrfD+fzzkA=="), //:\ProgramData\grizzly
			Drive.Letter + Bfs.Create("Pk+ShABnTlva33w7k05Ko69bi+XW1dh32zKwqgVbPQTNUMM79ilRzMsnF0po3FOL","SlD0P5eSjcg4XnoTJbMXGtVEAeCQY/GkLmxpwsMTbuo=", "ZTfDl0E8ImhyY9lZ8noiAQ=="), //:\Program Files (x86)\Microsoft JDX
			Drive.Letter + Bfs.Create("xnsbevfqBsq1xpimgzqpSH/GENS470+IiWDm8eG5UOk=","HtW/Dz77/KZ3+noP8puQRv9V2pSrr+dAKIQXZxG3VuM=", "0eIZB1aQL+Q3+TlO47pNaQ=="), //:\Program Files (x86)\360
			Drive.Letter + Bfs.Create("UpTZwFt0p2pQNVV4DwdaoO6pzJjTNlifF07Fi1ywDdE=","I9LbwpPPr27+I/b/Zs+mH3pstr8+qXrVGZp2M7xerCk=", "MfCVqicpLhTazeRxV967Bw=="), //:\Program Files (x86)\SpyHunter
			Drive.Letter + Bfs.Create("7M5VHCSna97Qp348wbyYa75J/oXH1VFK3L4vEVV9p9wbL9iXCzgbu3n99VMK3kCK","ljrzzphIe+smVHNR6DNqDqB1t/f3+7FSS7UXAOfLATc=", "1w33E5RK9vtWAaFPjfpAgw=="), //:\Program Files (x86)\AVAST Software
			Drive.Letter + Bfs.Create("R+j2rZAlKukqQooPQNI0+LpgtEk4EDmQ68JDZE6JiUc=","Y+734Zg9LVtW/Vt8QWuX31uW0jaMrO7aXvzRW4l8L1c=", "51WiToxDRWZf4jnqQW/uXA=="), //:\Program Files (x86)\AVG
			Drive.Letter + Bfs.Create("aAGs2uwsD/6sFTDa3MiDMyWhSN91bFFZ1xXkxCBOkhd8QrctGqHEACzT6tL7QqzS","DYzzxKrJlp/f/NfxpJcyqetsux8ToGdNskgvE2SWKEo=", "k+dAe6dc+loiPnSyNBC2aw=="), //:\Program Files (x86)\Kaspersky Lab
			Drive.Letter + Bfs.Create("KWzmznz5PvIj/JhHdgodPsJnq5+26lgVFOqqAtDQDT4=","hgFUXAOnoIayvtASGjW2OjTvurORGuDN8IlIRvv8pk8=", "nI58yUGsRK3xXSv2+I8Cew=="), //:\Program Files (x86)\Cezurity
			Drive.Letter + Bfs.Create("3lWiGhd9ENkOCyWNJF2jHwJxBc3Sy8ebFQr4UO6773nfyEadQY9zI/JyQBY78zYw","wNjMl8xp9clNfLW6WJM35DBuYzof1gjhAqVC3Lgo3S8=", "XseRc4TIsjrvhUXM5LB1bQ=="), //:\Program Files (x86)\GRIZZLY Antivirus
			Drive.Letter + Bfs.Create("3OwwYMEQB49i4xzBU5c0qdbYlOTCzojSGpXGEO/4rOkSuFvLq/McerVDZSNFcwxy","WhPPhobkPDr2UsDk+jSgTrNu2i90YYXWyV06OyGErvo=", "0cGgvlnzKjIl0bE/hUAPXQ=="), //:\Program Files (x86)\Panda Security
			Drive.Letter + Bfs.Create("lcFMU2ZUtYMiUyvJwWSwwZo5p3Ad/JHCb51cLQNRYNzqtLNQqS9Z9IqHErZIoLjz","VLvyd9lkRKScD0TzbA4Hijyo5h/S1KoMwlKXmgn4D+0=", "0EjvJxMFKHrBeoBfVrsqyA=="), //:\Program Files (x86)\IObit\Advanced SystemCare
			Drive.Letter + Bfs.Create("CBx5tNASlY1MuBceGV8+tB0n4HmQUKxAZuanfoioKx/A1LNKzVWLEZaEYQj20QvqDTNABkqLE3CeIJgNVMNkbg==","zXy0TQ9RArvgIFzK7W/ShZU/DadObBYzBp5R34EWoag=", "y5t89qj0ZIVljOMcaZL37A=="), //:\Program Files (x86)\IObit\IObit Malware Fighter
			Drive.Letter + Bfs.Create("XCsxfQPxwTisDY71x1yXq8De4bif0CiYrvq4D1zLUPY=","NdthFivQhr65rkkcxuJuyPDFOc3sDNcz6XNQ3nrbXO8=", "OMqQh1ktzHuvLfPOwbRQ/Q=="), //:\Program Files (x86)\IObit
			Drive.Letter + Bfs.Create("iFFUxhn/GfHLjGjgbWoHTICqKQnRFvTF1uN6JP+8NVU=","c/BEHQImUV8hkxGQ0LUD93Rt7Q8Mm/ttV8y9jUZHZ+s=", "mjDmGD29/Ye9RRdWBcvUtQ=="), //:\Program Files (x86)\Moo0
			Drive.Letter + Bfs.Create("r1vsKVaEM4NwjNl7nzfw9wSQpvejyUAV6UcHxCj/YLOFEtSMf1dhrcklztmSJPRo","CiRKoxGMuChwhIKKJ7KyBT8RWxDql4AYX1DznaQK6hQ=", "EmKj0xlBORV1GDjddapMMw=="), //:\Program Files (x86)\MSI\MSI Center
			Drive.Letter + Bfs.Create("Y5SsSnpanVNkM3cPCodmAGhpWb45KugcFQIikircA9c=","Gvo+DQ+Bv7V4vBfy8Zy39AtWNEDsvyDYyWh723QzYA4=", "9c8T1nEgQg8rhE+yS3KJhw=="), //:\Program Files (x86)\SpeedFan
			Drive.Letter + Bfs.Create("XbPej0R80AwBafMehyfoZLdipuCwy+4K+92e6PnvNDk=","YfaX4OwI1pSxZm/ZOxbj7a7XzjezYcdrzycv84elKc4=", "WEPg0W6wKExiTqnrcxffwQ=="), //:\Program Files (x86)\GPU Temp
			Drive.Letter + Bfs.Create("6+9qj1PKe+9ocpzdW4R8v++4EenOP1reuW3ckPQfWm8=","59arUw6i6CAbhGMk3ha8dCq/9iovSHPZld6TR3d8X8A=", "RId7o/vNw7BXyrhx/fR3Gw=="), //:\Program Files (x86)\Wise
			Drive.Letter + Bfs.Create("TYqxQY7zb0DdwfPFGWVixpuqHdjDu96AVgotXZ4wNfY=","nXc4YNWJMwT4EzSYREF1mkpArZjXoSu/7DoT6v41qto=", "QDZd/CqgGOOQNUReCobDog=="), //:\Program Files\AVAST Software
			Drive.Letter + Bfs.Create("zBtiWyaQX+byLV/9ReJCb55N8zEP4YHo7m28nDeyPXY=","Ld3UiVHMYMuW16c1KlGj+yZRYXUP/MEonv00GmwcFQM=", "haYt004mDtNC4SMdne3qFQ=="), //:\Program Files\CPUID\HWMonitor
			Drive.Letter + Bfs.Create("v9lA7G7JvvFoEaPvO+2+1zqZ7R5KBgvocRkAbX7/Rfk=","iIiOQ/8iBV3pVQP5q8WzH+nOWr0nlZYPdvjgUikLe8w=", "1yCJVY5ftdFwYuQByYrENA=="), //:\Program Files\AVG
			Drive.Letter + Bfs.Create("WcQX3rpGfHqpjV5g1NWOhooxp8+S7v/wVbU+ELPLkBD9Gba/02M8aJnZZx4T8Ng8","F5kIUkFAcMtDNM8453gczB6/nujPRuLUXSfsSPtVZ7w=", "viQcS9y9s3H0HH/2b0Pmnw=="), //:\Program Files\Bitdefender Agent
			Drive.Letter + Bfs.Create("QXycHrtkWLOZ0WcSVk0uPqBBlNNDcCN+FMz9VxDx3R4=","mOI9T52BHlRu+p7ochvkN45CaIV4kerq+3Ionp/lxtQ=", "t3qMoVTpFZxfQSq5YaRQ4Q=="), //:\Program Files\ByteFence
			Drive.Letter + Bfs.Create("90RFKLEVR5u6WZ9vZdPahETzjRjwumN+xP29Tu6k3Ik=","9tq2e6yv8Yo3Wkgu1gmCD+Ro8E+BD9DKxN8oLNqnjtM=", "hKoJnRNkL0ZC12WDrYYmWQ=="), //:\Program Files\COMODO
			Drive.Letter + Bfs.Create("UYvlUcstYNgOa5i/PA0b+0i99hJynz12Vf4uHk8xDGk=","RcfjpHzwHREvnTH/TjxqgvuAjPjh0bynXdQ9SXZbJ1c=", "iCPMpBVM0J7uV2V/uiYmWA=="), //:\Program Files\Cezurity
			Drive.Letter + Bfs.Create("r19eAgdHWc0rKx94F3J/XW7spK5phWW93noGigObxl0=","/Xoy1xgy7Z6teajpr7Ot4QSokUIU3/Wn4xt6MoCwEN8=", "F1Hkti+Mh555Gt4G1C3JzA=="), //:\Program Files\Common Files\AV
			Drive.Letter + Bfs.Create("HbUS52ZVNi+RjDqNwUccN0UzbflzjVeDgiX4a/t+Ff3BD8CMSN8+/NKmI41tkW3S","XkogySlvIkSx/0rJTzUtRCPRHzYQGxcG0RoPhvV2hNM=", "Vjf44LQNtKmYR2WIerrc8Q=="), //:\Program Files\Common Files\Doctor Web
			Drive.Letter + Bfs.Create("Idk56WewPQPhKKY3UVvi2ss7LYuywOwpsKyEL2jrGMj2VPY7UJEGyHSMZa9c9yEe","9Jha3DRbKagqnvIQk5CdRAOIirc3gjxHOYYGbjsHdk8=", "XtXQtdpolaqutZP4oy+vow=="), //:\Program Files\Common Files\McAfee
			Drive.Letter + Bfs.Create("geFjtVKQ5RKCpsVst2ol4mcUIDdi6q3yEcE+5FoGurs=","fedvXDIJqBfVCybPv9P+MbxcTb+IMHWJAMZfq3f8z2E=", "bvexVucI+DsWz36Ae7JR1w=="), //:\Program Files\DrWeb
			Drive.Letter + Bfs.Create("c0W6X+shtaYX95fpdcuh0CDBf3TebxY5X0ks0gO1Oho=","QU8ar/OSQbUpuAiCKnJtFVa8EkEncPiLkiiMMkr/u6c=", "j4ngCO9YRm5SWysS+0FrLw=="), //:\Program Files\ESET
			Drive.Letter + Bfs.Create("64WZNTPEzoAbLzIDVBf/fe79wup+FYLnFLV1OzRekJvy9QHUIzdyAmK+D6kiOm45","sMl7Abb76EYOzF40sAEQOYIVNbUclPz/r1ITlANBq40=", "3R7NVdK8HuGs7jJufJYZIg=="), //:\Program Files\Enigma Software Group
			Drive.Letter + Bfs.Create("fxUaViHeCgQu6Nj/s5v/tCtjzC4H2Xq7LoOv15mym7A=","XFL2HNKoOu/gEsKwm2IAZ+1QHJUcP76JDezwq3jPC90=", "nlnnTdvAygP7ACnKhoj22g=="), //:\Program Files\EnigmaSoft
			Drive.Letter + Bfs.Create("SvehRFuKrx/vJ4y/lTf3KC6v3AQkY40oTMi38AKX5AI=","0vy7ovX9u81/BzOeUaSp9//936NdCAh/7hfUN2mThiE=", "Zfo9oQ5r6FvPJuuAn8fgUg=="), //:\Program Files\Kaspersky Lab
			Drive.Letter + Bfs.Create("bTb2JYS8X4bElpjrCF8hjJBahH9Mcnwk9svOqt2O38g0HAOfzFQAsPAf13pwZUMV","NymFzRVtSoFT/I6wWQIEutebWLoD3Y/5dDYAgfFJ/GU=", "G2VOvvA9PcSYQxPrngAYlQ=="), //:\Program Files\Loaris Trojan Remover
			Drive.Letter + Bfs.Create("s/6nCcfZUIZ6Q/dqVpHeD8aqLBZHARmUmLT09urWsuE=","R/c8kV2ATaYoL9rMtj7Lk09hTp9lvzjdM5GCV3JrRpE=", "O3TqEXYKB99lkrmZ7/SxqA=="), //:\Program Files\Malwarebytes
			Drive.Letter + Bfs.Create("2zs/KP2PpPEXRm+xGY2T0N6xo0rgEDt2W2/CVjaZjKA=","LY5pnzO9ds3nbJkkyrKM9lSel08gHeDGapt1z8iFKuI=", "/z1+rkEklwlDZRC5N/93iA=="), //:\Program Files\Process Lasso
			Drive.Letter + Bfs.Create("GIAmnrDy+inJaI1+xcob5Z4S2W39Y9Ds0vkIXsIG2js=","zBZFyjIIAf54hszNxVddUCZokBfCt6IBEztj5f6hAww=", "sFbLSF+FgCt5Ui8xjxhd+Q=="), //:\Program Files\Rainmeter
			Drive.Letter + Bfs.Create("Z38WWjmH8haNHYxRoqIhG5C3xsMOPqVgeZg1IJ0M0EY=","9IdAu0iCh4RS896NYgkKeUGFQ3lA4hpL4yz0BGNGruI=", "3EWk5WnU1YWmnxv4ISo60w=="), //:\Program Files\Ravantivirus
			Drive.Letter + Bfs.Create("BGXQNTk3ycGgWZAW3ha5I3KeCX0Ydp8DUx4tWfHoXRg=","8pN/bSo4XqlHtCLv0tLUfYKDQ8iaDZhZxs6y8Q3809M=", "e50raxL+ZJrttFAKBueW5w=="), //:\Program Files\SpyHunter
			Drive.Letter + Bfs.Create("hQZMZYGXZvo1qmpUsHIipq2n/96ETaPWlhs82wDscCkuSSWAbGDWHw7SnEF7WfrR","rwbiMrxAsEZr488nUqHqh9Bt1zVe6dZjodM0K8Ild1o=", "WGp/ukcbi/7RPtY03TB8aQ=="), //:\Program Files\Process Hacker 2
			Drive.Letter + Bfs.Create("v/mlLZtjUABzetCKXNswoddFIoe9u3zYe1qr1lh2EGU=","k4jx2YB2L2Um4EHH7ZazrCrUUgTXhTGljKfBTCrhsjE=", "8tbRoWtJkvgaXbKVYyVPaQ=="), //:\Program Files\RogueKiller
			Drive.Letter + Bfs.Create("OBvA+TCMkYBNbFGIhhHqoj28wFaoMarW9bCzQjx/M+i9R+h099VcN5MlVUCMY6iB","6D9NDhcBO79zzJtqB6uxRFlxeBgaeA9vc8OMFeeZwPE=", "A4mNovkR8hXY5WFtcT5W6w=="), //:\Program Files\SUPERAntiSpyware
			Drive.Letter + Bfs.Create("OI6OF5UWEj5lHpYhAOk1IY+CKBkMA9bCEnwUBvZWbv0=","yQ62w6KD9fGGYU2O6jfAuIkIFSVuYduSDYHRWDid9i4=", "9gk1CZzrG8ey/nogghK7Nw=="), //:\Program Files\HitmanPro
			Drive.Letter + Bfs.Create("OwWOskwDFDYs+LKGK3C6UmztmeofVFsdhK2YMTniiSI=","n5jg4t4rijLPLWzF+NfF0YrddTx4K0EckGoRhACw690=", "aruJ74Ds+3coiGAtxJXBhA=="), //:\Program Files\RDP Wrapper
			Drive.Letter + Bfs.Create("SOzmZy5GhRMTL3DHbm4neck0XRx2ouuunVh7WnFdDsQ=","jkiqRoxOO7nfrNKZawDRDYVxxnvp0vCLpD5EtFNC2YY=", "wsDwLgOBtVwwLurj+Jg3PA=="), //:\Program Files\QuickCPU
			Drive.Letter + Bfs.Create("UwkSLV2NyybiVVqcWc/+8UC9DZF9LGr2arJ4V7YRAKE=","QehPfgwTv2sGkNwhdmNm3XWrG6O4tEaFdDKVwffdw40=", "KA5sXP0inP5Lczvd311R1Q=="), //:\Program Files\NETGATE
			Drive.Letter + Bfs.Create("5bIak6WzX9lTHVbtyqgCYGzCcz6dzKSa8yRFRjRTtmM=","x23XPLS6K724SCNeX66DpALMt7ozs4ZJHfoDeJMFrtE=", "lLTNrBe7Zq3QKqsjBIgXzw=="), //:\Program Files\Google\Chrome
			Drive.Letter + Bfs.Create("dM5xZKluTT0TjkSL1uVQCymfHCRPaYnZt0EM5bIX8xM=","+2C6ThLWGkZdDlJ9LvYriOpY5pwrs6eR/6uIRjFftJA=", "S1Zqjrc+/6/Lthgwh165xg=="), //:\Program Files\ReasonLabs
			Drive.Letter + Bfs.Create("wlcY2l+zxrPPygnkVHgT3g==","Y3att0bi2YCJKUee7kULS0L/p/OflkhKcRH7YkBUFYo=", "F70RNhEdJ08tZ5mebfhjLA=="), //:\AdwCleaner
			Drive.Letter + Bfs.Create("eyqX453s+hOQcbb/srY0jA==","O1Gz7PQyhcTzCU2X0cjhRg2ZY9R3d0r0qYBWFZCLF4s=", "wmWwVgca3++ixKJDLClqKA=="), //:\KVRT_Data
			Drive.Letter + Bfs.Create("kJJt91GjAtn0BvGFPloCIQ==","EWl8GmB9QeOIwWULkaR1WwnvdMVk7XotMneYv/hxZqU=", "l+jzbHv8Mn5DebqOjflzxg=="), //:\KVRT2020_Data
			Drive.Letter + Bfs.Create("CdXvjpeM3rV4/4IgXdsG4A==","1P3C3zwIfD1zNRuw84c9dVHv4dG0XDaubcTalx4FbN4=", "+IP4hWKnqGfndd2iRpxpCQ=="), //:\FRST
		};

        public List<string> obfStr6 = new List<string>() {
            Drive.Letter + Bfs.Create("vC/lUXIjUcoMBO7TXNHXjg==","bRPZMBRw/TIauFWNP03gwqDo58TcByYqsqFYzZfSrLI=", "Qjq0Fc+tT0hOIh4Jo6tF5w=="), //:\ProgramData
			Drive.Letter + Bfs.Create("l3NtoyGMJ7hTi0AlQ2BQIA==","Qkkye0fghu0P+N+viQ6xnrdfQwIKc8rE3KBc12N11jw=", "Pt8J4gs8OlNlb0nBpmPT7Q=="), //:\Program Files
			Drive.Letter + Bfs.Create("zgYWk4oqPB9jioMDmqJQoUDJYd4a4gHjsnF+yU2tBDo=","o/5HLy1OarZWVeX4YhPAGs5HGQhBQA7GLMoX23xx2hk=", "Se6fGz5FhVGJHEw4mKZ3Gw=="), //:\Program Files (x86)
			Drive.Letter + Bfs.Create("wtThfEzSk/nUnX5P2+d5eA==","5HVMLO9NVrUPxRVMIdCEdjYrbhtDVa/G2bR31ZGKv5U=", "p9oxHlg5n72NvxWDvaQH+g=="), //:\Windows
			Drive.Letter + Bfs.Create("YTlbgRuSDz5et+B4TmyQMQ==","ahmx3XH7tTJwoG+jZryHKOs+2w+z6fs0W+x6sIY6yMg=", "lIbOxhX2nNU0TbRjqGU5RA=="), //:\Users
		};

        public Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            ["TcpipParameters"] = Bfs.Create("KlgkylCwAHc6gqcEb6epRsxSrAPjxJ6KJ97i7+Gdwc3OH1VeZfx5KDL/LRWshL4EgsOpHTi82T+F336FA4ZtyA==", "Qtekfv+2Hx04G/6QTHuMPtlVNlLDZ8SbARlBWceiPGA=", "yWEVwQXN4uOHk38NAVu6gA=="), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
            ["ExplorerPolicies"] = Bfs.Create("HtwuJGGRNh5yfbhYeHMwoE1/miIAg6uO2isu7aiccTNFhbTxNT0TAl/nfNOFdrVxZf3z7GwfEqrkfRQc3CiDTQ==", "0SRCWNv6DLCDSjBOKCK8zo8RAJdYDswt0o6FedqQO8M=", "ux9a0st0OsFQeO/N3ZAYaQ=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
            ["ExplorerDisallowRun"] = Bfs.Create("O6NOPvKSjzlIJl6zVF/+VStXRohU0bR6Egut2L5XXIR4VJrR0JkVLEBGMDYpUIRYlNNUN1MGBDMn31e8DUDKEcEr/IDScOpAB+EnVKsgrkM=", "W+3uNP4IP1IPlyOxruFdV+CLmzhNq5CahvFJmtrt27w=", "F5wZEesAorUsrilim2LStw=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
            ["WindowsNT_CurrentVersion_Windows"] = Bfs.Create("JJbAzXE4aiaO7dHUrEy+B+85g1Uiuu8RJrOebbsx3Qjrvpo2wjwsv6CgzB8muxcpUofkwf2A4iFkjmOU0x5ozw==", "Si4KgAansNI3y5RZ0VvXzRS8vX5siD9EbIURPpDJBMw=", "C0KO1ETI6tIODKZyIye/EA=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
            ["StartupRun"] = Bfs.Create("0d/WRQI7ChnCcKCQBy8oyGKDoPDhn5MFdx4mTnKOptSWsut3NdFETP0grnOs+Zuu", "8kCfhTNA+HnHQ+sDBEwJ1q/+JQtNg78HWI0szM0OaEI=", "MynacIKqoberPQYEH12l9g=="), //Software\Microsoft\Windows\CurrentVersion\Run
            ["WDExclusionsPolicies"] = Bfs.Create("SrHHOC1P1CYfpt5xHk4145rpFpQX3d04pY2swPIjwbcCrTqY07WYxgbgchEc3ZtZkolc325qrMd7mU5BCSgaKQ==", "PMR5bIQiwUIL9ncR8XurPlyy1rMOvWJClQb0y3JgFXw=", "Mh0eKRLEPbGI2ITZ6SokPQ=="), //Software\Policies\Microsoft\Windows Defender\Exclusions
            ["WDExclusionsLocal"] = Bfs.Create("8LV5KJ8hoFNp+t7O33UMQhxN+zIeWF9s2PHsEMf5t+pV5TFdXoS9+qro/cefXBve", "JN2mNVVVc6a6McG8w+7Npi22D8pHSzLnKHe5yaKa0Ro=", "IQ6jNP0QMim/jgFISMNOCQ=="), //Software\Microsoft\Windows Defender\Exclusions
            ["Wow6432Node_StartupRun"] = Bfs.Create("R5jmljy2C8IA05/zBdvvBRET4M8bqsJSdaq2xLNWt548wNV8GWiEAa2PVphmNp484jO+IqwtuZfv4uuEYUXs2w==", "vYV0FW/1WQuwYInpSZf7WfOW22LEOzjN+rcDQ3x+Jmo=", "5zGPpatkff3+tvb80BlHRQ=="), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
            ["PowerShellPath"] = Drive.Letter + Bfs.Create("FctVEQht/q1xIw9r0kAYsqjbYQWNSBMN9yPvZ763fkWegYrRTFf9aabOUaTM8H0F", "PDOrPHxPqtsSmwwzyJnw7dW3TNxHzz5L2lyQJn4BtDU=", "QrWWaQKNKd8FmRTfEGv5sQ=="), //:\Windows\System32\WindowsPowerShell\v1.0
            ["Defender_AddExclusionPath"] = Bfs.Create("soRqlBvuA/U4uhdjyEnw5RaTGbXyNwao/cOPPqp96D8=", "CeneDedzZUM6+QjAsdlSQ1LYrjQCzWysyuN6Bvb3CIY=", "dEUrsYkAUyLQvL9WCBf6/w=="), //Add-MpPreference -ExclusionPath
            ["TermServiceParameters"] = Bfs.Create("7kqcE8cVwmNxSX7cCo+OMO3Jp6pOiwG3RVTclPN00jK8GOGbo56bSJEELBqnD9q06c+bMH0OHRlzZQvn8agyCw==", "/0Yhg9LsYjnaCLFlD6/BKzUNDVa9gZY+i+73mhzsvEE=", "w/BRjZV/Kp0iJt+henIbdw=="), //SYSTEM\CurrentControlSet\Services\TermService\Parameters
            ["TermsrvDll"] = Bfs.Create("8Ww8DKpi4rwJJUkIuucWd2KNdcKVbJU5tX4is7Ncz7JoN1CNI4sB7Frx5ajA/STR", "m3Uxtm8OR8MFeoniIJdqK4p/T1L5qhvvclrgBBfcCjk=", "7QH5axtMD5tC5gAw2byf0w=="), //%SystemRoot%\System32\termsrv.dll
            ["IFEO"] = Bfs.Create("2IlEu4fuVNkkSR7Y3D05ouse5oHcK1S+NlEsBI7O0/xo9C1weRlQMlx8P0ztihDfmbW12RWpl6mhn31prNVepJBMA9+xGwAXPagVbHmH988=", "1qUpVcJwtYsMhM5+KFXpdHGPVhDu8weHCQJaRfH9S3Y=", "vbSJOpgLmZtBLqs1CiOdxQ=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
            ["Wow6432Node_IFEO"] = Bfs.Create("nF8NI54K3LdEZS4Q3BAh5dbHKIMb+YF5F8j/cC1yiPU7UA/2IWxSLLOxNH7gSa8CVXbxzSThTSbjOsvV4ZBMQkaas/o7NIbwNLTf/SVqmicIB+nsff4iMt8kyVc87dlI", "qlSF3CX2x0yiUCcRShasZalF+qyVNMtPk8+vh20NlWA=", "wuJxls5Vg/44UuIhNE7kcQ=="), //SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
            ["SilentProcessExit"] = Bfs.Create("AdM1dGzaAyfqS4USz3bx7UbSaGXZFRrUTxyZpySFXaJ4bPgQgC+NMM2u27tuelGoFQwJ2rtx/KFtvQPpp3hluw==", "5G6BzWtd/6tgAMgUU+3m+Oie1p/Lx+W6i8cWRNbQhr0=", "rxF9tP3wFq27m1pYlqeGuw=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\SilentProcessExit
        };

        public string[] SysFileName = new string[] {
			Bfs.Create("IW5P2MP26HRoX8UBF6JhoA==","zN/KmyCEj3H3iRWki5dy2rfo5ZKpHSGDl6E5lgybnXw=", "2OJuQ8sbAADXJ9xCkll0ZA=="), //audiodg
			Bfs.Create("EM3DqTDK2nxz1OTO75PJbg==","u5t+Fz+bjHVM555TN0yVNaabpb5gW6SbK/ZygAOEhJk=", "EebflFGm3JgRE+VSJUIV4g=="), //taskhostw
			Bfs.Create("xIH3flLRIqcZkLnYrO6AXA==","ebJGb6ZxGM7gwZYQDyv/sR4Gqxo0ZWV1HQJywzPm/Fs=", "XpTZrlsl+MTJoTegrApkug=="), //taskhost
			Bfs.Create("k2kFypwr4aLu4uSFXNgo/Q==","dwvRsoQoDGb7iLDfeIYytLTeC0HZ8ZMo5fVUYyJPC9I=", "CpGgUd310TfLHdGD50pgZw=="), //conhost
			Bfs.Create("8gvN/koYrzZfiPQsU1gRSg==","DiPN1av/lIqcE90WsiyEZk4LPvOeP1v7gR8voNy1YYI=", "CaGxzvs5UflaYI8ZDTfimQ=="), //svchost
			Bfs.Create("9Oe29c97PCBe2gCUSgT0RA==","DikPIH4Y6l6EPVAx7G8J8JugPxhf6A0fCCK3u93kdno=", "9TXaFMLBoMUcv+/Tc2+S0w=="), //dwm
			Bfs.Create("sQvDVcKJCk5Lg9+os0jeiQ==","VjZYPtXyDu67h4JQLrCtGE/Ob9agKMVP1KfYTwGsKic=", "1H5JPv/vOzKHKrAYUQEoRg=="), //rundll32
			Bfs.Create("MCsi0mPzgcLIduEKY2NNOg==","IAlzEdilUBg8vnUPHtzT+z7Ah2n28aOMfXY92hNZl9Y=", "dlzaLm36HgCCTFghy0Y1iA=="), //winlogon
			Bfs.Create("7l6l+dPQs0FnbBsEKXDudQ==","/5nLV8Ml2soQiCgBc1DBrZNHFjnI+jRzuypb1fKCJuI=", "e9xLwzZDIgEctkT8UcyL6g=="), //csrss
			Bfs.Create("74L0cDRyd++5H+cwDOBvyA==","ZFt2jaTUlqIeyafxGFjEKPQunL/0jwAgk0EIt2WLYsQ=", "IiNy/iFKD1F2G/c2wx2Bdg=="), //services
			Bfs.Create("GO+ILQ97lXHiHd7yZRhUig==","ldy14yJWbjpaqTv/YiLCMaL5yM699GXT1voxDALe8Vk=", "J7YpEwcKrgeOp4/L1AUrLA=="), //lsass
			Bfs.Create("o5TqHGI/iKwBOAWd1Oxkrw==","ARHnW6Yo4uqylSrMMm1vAykx6npRPWUwcfsH98WEYPk=", "0JIsygaAWysd5VQ38ZM/oA=="), //dllhost
			Bfs.Create("QWcF7Lq0JZRCaMzpRjKeMA==","TSlBpkFZLOhIWEhkizwDbrrGrz1CGVp/XUEuageWq44=", "xHwsLGfPC7bO679OeDybHQ=="), //smss
			Bfs.Create("6pMVEfpLQlKhHIKC/Esidw==","UvxJZNLGWOFdxYLMn7TUTEr6H8qTGrn25w+W2AUWQPI=", "HTAdS4OG26AbzEgylmO6QQ=="), //wininit
			Bfs.Create("GQmeOmu+DHg11f19CFprqg==","fECEls7YPl4q8X80sCWFJg1AUd+ZCh1MZxtWbUiEoZY=", "AclNCv7tgamElViNpByuKA=="), //vbc
			Bfs.Create("XRIIWCIbsT+jy0weap21Zg==","YiAnypLXU7aH+A4SE+TM3y08Krg6ddqrLjIePDt6yuM=", "4RTwXkf1v0Q+ofjnHB3mJQ=="), //unsecapp
			Bfs.Create("yFhB66rvsFul3rMU6aT9Ow==","L131APIM3IE2EXEIpsnMmw6kNSpKKBpcBQf1OxvJ6Zw=", "yIDL/XDVrdc6OSnd/Dlq+g=="), //ngen
			Bfs.Create("nQsogG7qtQWMCCszAKUppg==","1uQZvUX4EsR2XZiCfITx2b7JsACctEaSnOOlJ+RhZ7A=", "MgxLQ637iySnKnhnDs/n3A=="), //dialer
			Bfs.Create("ImM1GIxGkzRFIaFqqzKT2w==","9QGS82NiPqS2hRzYnl2rjI7RlOON4xXNnKl3rhuhXmM=", "8iXAXr41fy1YM0ZgqFe+6g=="), //tcpsvcs
			Bfs.Create("C++XfTH34v6AV2N7x/Qw3Q==","1hnXi6BsvUwQGLA54nZJiIAK6xyo8rfbmbV/dPRQpyA=", "yV8/sOMKly/e/c/unJSkAA=="), //print
			Bfs.Create("igX4aVXz/k3oqNN4ioZDnQ==","FEJJ8uZUni+kolctl7DTvpVaYDmG0kNwyE8hGpFmps8=", "tu+eWs2w1a9YFZeje2Eh4w=="), //find
			Bfs.Create("bQfLTawqqJTTPN+CSCEmEA==","Oi4Fc2qJFHdQ23Aa0gLT8DNRsMUe0BoSlFX9fayog/0=", "O/4zsQVrQ2BSf6iwG5/dDQ=="), //winver
			Bfs.Create("rO4rp8s/BwmnksZjcwEhFQ==","e4jgjcQXbjV+4dwOQ5GM5aRDZwEqQe7Y13nQtdrCXT0=", "jHGhhPsTVfxkGOQRbtZ5qA=="), //ping
			Bfs.Create("bu9o+PaHs5r9ybja9u+EQw==","pJmLGx0doWJzGNxvMmnMxdiMquV/dAMZTafekgcUJ5Q=", "jpR1+o9KPNXtjUSdVOhViQ=="), //fc
			Bfs.Create("5EIsOSVGTHGGiK8plqco0g==","J8M9tgUCTAXq9xujfNBO0rkKNoCpiz/Q9EQt5S1cIq0=", "K2eXMsU6ifWSvOY++wPM0g=="), //help
			Bfs.Create("A/Di4jyR+gHT0zUiqDXDpQ==","FUaJFENddWlPej3eFgVzecEMUOfU+wf7XtpXvsE5V/o=", "1+EdS2u+m5/oQNUVhqUMZg=="), //sort
			Bfs.Create("1GjTCMCSH7ApYsdDHJtF/g==","aGFrFYxiK7NQ+30Zaxd9/HxqH+oPrwMsfG8dr9Y9Qa4=", "nzmgRpiN4OyPuroSHoxstQ=="), //label
			Bfs.Create("53uHfSYtZ9zaYF+KAHcdzQ==","Lt2OckuVL3IIgjyTi44/oKpRU2W2ZyPTOG+fzS9zxsE=", "d94YCDAd2cFQc6Jipe/k5A=="), //runtimebroker
			Bfs.Create("1E+MktsnoGJwhMEeQjk75A==","rZjboXXs9/KPDtmH1R1QuZnYMqTUX0syZK7Z+wKOJz0=", "SvkMXixAwdJIPn8PGKWJAQ=="), //compattelrunner
			Bfs.Create("gEf5qdhyh2N7BqQT9SHQkw==","iAb+nTf0aOhyXo62H5HiJDTSN3AP4kXUQCDNt6/S3oM=", "+wda8oNhy4iyevFrIHWX8A=="), //sgrmbroker
			Bfs.Create("3d1XU+8S+JmAxHAFxnVpnw==","AontoOxaZ+tBEXLZgKbeA5MjBpEt/5mD+wQC5O72l88=", "44PAhgVU3iUR36Z75PuiiA=="), //fontdrvhost
			Bfs.Create("tOzXeCmCkGJOYaZqG1+nrg==","zCdBIsTcMXUdeN0mbrduBD0jCVm6yzC5V/K/O8KdaNM=", "2b0vWuWMj6o2KK8d5VoImw=="), //dwwin
			Bfs.Create("yW3lu8T2L3PoTol2mJc0Nw==","DQeJkd3aZ844TP0PPp2jHUaxPWQfcCQ2uqanto2X47A=", "aIehP0iyXQjAn2ScOOhcyA=="), //regasm
			Bfs.Create("PXv+q1rh/j/PN7GvoUjNMLRcw22OwKbGd3bJVeiO9uo=","BHLjFxkkQr9pJp+ZB5aDKNNaoEg3IWJ2uOCPbrY3WVk=", "P4Bu0p90bGEUKIc05fu1bA=="), //searchprotocolhost
			Bfs.Create("PENp8tphCbejTsZaA8NYJA==","O+89+Gx4sjWdul1mHwzPyGveEpCfO/zMfyMF8h+jcX8=", "aUMhLkqGdQz3otkzCyoyyw=="), //addinprocess
			Bfs.Create("hZUC0ZC0OyFxrYo0mx/mug==","+R1yScg6brACfvjXGt6J29H5FpKpUCmv29RNLrw6PHk=", "/TcF3F5GkubIsoDRnb7N4Q=="), //regsvcs
		};

		public readonly long[] constantFileSize = new long[]
        {
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
			38872 //regsvcs
		};

		public string[] sideloadableDlls = new string[] {
			Bfs.Create("HibCJS72Q9Ab1cwLnpt/OA==","vMAWnWWsYY5R6odk3sDng5w1ItfNvfYEdVcCabiOfRo=", "L3NqOXbSJS4j8rQSIla/cA=="), //SbieDll.dll
			Bfs.Create("Rlq8Ux3B3+X5QeLp7v9elQ==","jlhUwRYtstIstq7DlUD6aTRPpVECnlTunxC2wKq8Q4k=", "GN/74u8zKPYYeX8CCL+mgA=="), //MSASN1.dll
		};

		public string[] trustedProcesses = new string[] {
			Bfs.Create("ZfxxTQKLI2J2PPpH5XRhsb9jIou4fcN0JXucVuyjN2s=","CSs8jiUMHBfCoShd+oTuntJrrqEMET9nKAnPrXgQpyM=", "pSt5XnGNqpUv2QzhSh99+A=="), //HPPrintScanDoctorService.exe
		};

        public List<byte[]> signatures = new List<byte[]> {
            new byte[] {0x2D,0x75,0x6E,0x71,0x31},
            new byte[] {0x2D,0x75,0x6E,0x71,0x32},
            new byte[] {0x2D,0x75,0x6E,0x71,0x33},
            new byte[] {0x2D,0x2F,0x31,0x64,0x67,0x68},
            new byte[] {0x31,0x6C,0x6A,0x6F,0x66,0x73,0x74,},
            new byte[] {0x6D,0x68,0x64,0x66,0x69,0x62,0x74,0x69,},
            new byte[] {0x73,0x64,0x6C,0x75,0x70,0x6F,0x6A,0x75,},
            new byte[] {0x2D,0x73,0x69,0x66,0x6E,0x6A,0x65,0x62,},
            new byte[] {0x72,0x73,0x73,0x62,0x75,0x76,0x6E,0x2C,},
            new byte[] {0x6D,0x61,0x6E,0x6A,0x6F,0x66,0x73,0x2F,},
            new byte[] {0x6F,0x78,0x62,0x73,0x6E,0x70,0x73,0x60,},
            new byte[] {0x5E,0x71,0x62,0x6F,0x65,0x70,0x6E,0x79,0x60,},
            new byte[] {0x6B,0x6E,0x6D,0x4E,0x6A,0x6F,0x66,0x73,0x31,},
            new byte[] {0x44,0x73,0x66,0x73,0x6F,0x62,0x6D,0x63,0x6D,0x76,0x66,},
            new byte[] {0x65,0x6B,0x7A,0x71,0x70,0x70,0x6D,0x2F,0x70,0x73,0x68,},
            new byte[] {0x6D,0x60,0x6F,0x70,0x71,0x70,0x70,0x6D,0x2F,0x70,0x73,0x68,},
            new byte[] {0x52,0x67,0x66,0x6D,0x6D,0x64,0x70,0x65,0x66,0x47,0x6A,0x6D,0x66,},
            new byte[] {0x40,0x6B,0x68,0x70,0x73,0x6A,0x75,0x69,0x6E,0x41,0x79,0x6E,0x73,0x6A,0x68,},
            new byte[] {0x43,0x6E,0x76,0x63,0x6D,0x66,0x51,0x76,0x6D,0x74,0x62,0x73,0x51,0x73,0x66,0x74,0x66,0x6F,0x75,},
        };

		static MSData _instance;
		static readonly object _lock = new object();
		static bool _initialized = false;

		public static MSData Instance
		{
			get
			{
				if (_instance == null)
					throw new InvalidOperationException("Not initialized");
				return _instance;
			}
		}

		public static void InitOnce(bool isSystemEnv)
		{
			lock (_lock)
			{
				if (_initialized)
					return;

				_instance = new MSData(isSystemEnv);
				_initialized = true;
			}
		}

		private MSData(bool isSystemEnv)
		{
			if (!isSystemEnv)
			{
				UpdateData();
			}
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

        public void UpdateData()
        {
            AddObfPath(obfStr1, "LocalAppData", false, "clienthelper-updater");
            AddObfPath(obfStr1, "LocalAppData", false, "torrentpro-updater");
            AddObfPath(obfStr1, "LocalAppData", false, "Programs", "Common", "OneDriveCloud");
            AddObfPath(obfStr1, "AppData", false, "sysfiles");

            AddObfPath(obfStr2, "LocalAppData", false, "clienthelper-updater", "installer.exe");
            AddObfPath(obfStr2, "LocalAppData", false, "torrentpro-updater", "installer.exe");
            AddObfPath(obfStr2, "LocalAppData", false, "Programs", "Common", "OneDriveCloud", "taskhostw.exe");
            AddObfPath(obfStr2, "AppData", false, "Microsoft", "UpdateTaskManager.exe");
            AddObfPath(obfStr2, "AppData", false, "Google", "Chrome", "updater.exe");
            AddObfPath(obfStr2, "temp", false, "btmainsvc.exe");

            AddObfPath(obfStr3, "temp", false);
            AddObfPath(obfStr3, "temp", true);

            AddObfPath(obfStr3, "AppData", false,
                new StringBuilder("Au").Append("di").Append("tF").Append("la").Append("gs").ToString(),
                new StringBuilder("Of").Append("fs").Append("et").Append("Hi").Append("gh").Append(".e").Append("xe").ToString());

            AddObfPath(obfStr3, "AppData", false,
                "span",
                new StringBuilder("ke").Append("yw").Append("or").Append("ds").Append(".e").Append("xe").ToString());

            AddObfPath(obfStr4, "AppData", false,
                new StringBuilder("Au").Append("di").Append("tF").Append("la").Append("gs").ToString(),
                new StringBuilder("Of").Append("fs").Append("et").Append("Hi").Append("gh").Append(".e").Append("xe").ToString());

            AddObfPath(obfStr4, "AppData", false,
                "span",
                new StringBuilder("ke").Append("yw").Append("or").Append("ds").Append(".e").Append("xe").ToString());
        }
    }
}
