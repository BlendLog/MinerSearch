using System.Collections.Generic;

namespace DBase
{
    public static class Drive
    {
        public static string Letter { get; set; }
    }

    public class MSData
    {
        public List<HashedString> hStrings = new List<HashedString> {
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
            Bfs.Create("5Zso7J+J0KLvLlDKtJu3mA==", "O9PQ4iOW3VNLbPoHWrN53zjrgOh+1lHlWsflGkbVY8g=", "+uzd11D1GicHk/JOdh3XZQ=="),
            Bfs.Create("FNQ76qCxeac1BD4jKWy6ZA==", "8zRith10A7iLZS8mXAMSzoXst0ZC1tL8CReWEzrfZ9c=", "wjMGtCDAxHHrYhsnORpYOQ=="),
            Bfs.Create("sO7OOu5OSXyptj3v0iqxDA==", "HeQFwj9HaNkg+1B6qjfgo55/xWyxa59MBPxu+EheVzI=", "N7eAaFAOApT+49mC+hdIaA=="),
            Bfs.Create("bVnQwAoWlEuEQWO6EZtQMA==", "SmZdrx0xAgDzkfuxlj79pzbuoibYQEI3bZ1+KgZI7es=", "/ccqdImHlOzliweDQo4POg=="),
            Bfs.Create("XAVekiMN24RQnSO6U5aZvQ==", "BQTZ333NSZisbaQOUZmouVBHvSkOi947nFow3P9VoWg=", "caEqrLSMfgzXdxJCRyvEww=="),
            Bfs.Create("AkOfLAt2wO7agXGxJepS/w==", "+qZXiD2eug7fGkS7Dku5sDANhG0ay2aTeYAX746V2Z4=", "c1FODkVVdNYt6zSZ1OObPA=="),
            Bfs.Create("9ikGKBucaLtCSdwI+RgrPw==", "qZZh06HEd2t8rqugQ5ncxLR++o4/ByjR4ILlijd6l2o=", "qgBDVoGhyFsgjpTZSy26WQ=="),
            Bfs.Create("sNsAlSRSsjOuRAg916r5bg==", "pA025fZZYpWOcyzR8KYBhKPnQQ24pAaOkijJFRWhK7E=", "amLU58h3jDtIjSOzVmq5sw=="),
            Bfs.Create("2kodjA8mOdgTMftoDJOtwQ==", "+9rZjTe9lSWm2OZvu3NCnHNvAnqoLWuHVJx7jsEQeW4=", "mPpR0hVFO/c04a7GKJFVMA=="),
            Bfs.Create("rR8BFDfylBAMl+W0nL8XqA==", "6qmty6kR0HdUzFIoKsfYxLW7P3eevelPtDTlgLeWjpQ=", "hKLHNpM39uWrB6wrJtQ4YA=="),
            Bfs.Create("gaut+EZeHQXsEe+3CPOTQA==", "EBgxWrpOxyipLHRJ1Rq0Dvu8f7Zdmb5+Ahk0hUz8mF0=", "v6iG4W8srqy/R150s8vJ7g=="),
            Bfs.Create("5MB7ZKM5Dtdfzl3McxYd0Q==", "hKPG1k6FycKR0GU3NxZYCRUESSjJm7niu5Tj7GUpSO0=", "QTeclSGU6r7A+Ff23Cog9A=="),
            Bfs.Create("RN54DCkqFSVIZ5+zHKBd1A==", "Ae9HwjLGHz5mmXD28zWIOnw4BZu8eSw5Z/UQdPaMggo=", "dt0uuj72BRPB5/qk7n7EEA=="),
            Bfs.Create("D5R6minVMnI4SNqi6w89pg==", "syPbCfnj4n2gUCE2Iu/bs+iIPY4lCaTQbdULQ77OfaQ=", "/sKJtZ1323ntgPIQVDGhkQ=="),
            Bfs.Create("nGZ8SYzCy6XUTRYi1X4Mow==", "AKGXvOTeTtUzZgKrr6Nd2+oh2vP4s07ckVOIHMdsLpc=", "AgiTsURG1QUOHs33tmMu3g=="),
            Bfs.Create("wVRdwUIn2T0EFO7zWWEYWA==", "eyoH78GiFvu5HNc5ZkY5VZPh8cHjUougUAa5RnnJFbY=", "zRIY8+9NUC9N+Dgoda4APg=="),
            Bfs.Create("WbeQNLKcrR/oBbqTv1ytOg==", "Y9NVz+/FuUukZfHeKEE7usyGic5+pBOqE0sgU9P5Lik=", "lelwPKkE4cViIbemmkPXcA=="),
            Bfs.Create("P4FkexIFkrLQBu/OI7ObJw==", "MNrLxjyDueugIvCE4mrxjN9u/ID0OsXST959wW88qNk=", "GLB2n2fid7D5m+1i99ntVw=="),
            Bfs.Create("eAM+jhisVALJ/tEGn3iLaA==", "azxlwdFg4iPBrS2LL3etMec6xAR7lH0IDKl2NNd1E0k=", "FkT1oOIsQZQwA5KAc1XLXg=="),
            Bfs.Create("OwnfzyQj9cOSWRUKTTkwGQ==", "l5h7N+JRH4X/PBYRXRf7tuKoA4EBEe5ZsDdipCOS+Ww=", "HMAm/XRwaTSTW9AmiVlgQQ=="),
            Bfs.Create("MhJ6793U/vUe53v5PAay8A==", "pSmBQxRJONJ5cLgluHcOgY3HcqYDOf8ppkIqBY6PH+E=", "pk/iihEsTRasqBgwOSWXbg=="),
            Bfs.Create("un4mFCS+VyAxWXVm5c6xAg==", "7VQ4ySiqx/wkjLC5lHY6k1g0N/W0Fx7+xfERIvcTpeg=", "B3t4GQdZ7N2Z9ZOCp37ZFw=="),
        };

        public List<string> obfStr1 = new List<string>() {
		Drive.Letter + Bfs.Create("hEh0j2pw1GjUJF6qmaLkcdey99LW0cQofKOECo02h9o=","zDzytiyQBR7Y7FZ9TGqdKEZ9czaodhJil6DkX/2Onxc=", "etV1JiwfTQFwlFW+RD58fg=="), //:\ProgramData\Install
		Drive.Letter + Bfs.Create("O9rANoCPkrdqqJkXGfUKKGTW3G+8bDSEc6dYee3u4Dw=","a3wAZ0vOZvwRhVNR9b84UhQxFxzmZWdlCON3uXZBeTE=", "0hXlUt3Mo1BDRWLI2oaXVQ=="), //:\ProgramData\Microsoft\Check
		Drive.Letter + Bfs.Create("k4HmLadDF03XHkkDasXts3HBjRvAsIluBKNNhlJlu0Y=","hVDFvy84SnOM6rHIOZAJYj5NydTooP0bxSDVwiuxMgs=", "LIHXITu+n0XOFAQJy8wY/w=="), //:\ProgramData\Microsoft\Intel
		Drive.Letter + Bfs.Create("o1PBojYzgjERr7056iFH6sQK5Q52iDvER7ewu1E38vnoAXXa4Fx38M1x1rJi5mS/Sg3bVDA7SCfKIEkbgOYNWA==","7l+d8GUpFmbPHy0TjP5xrs6k1ArJwkFO+4QtxaELv24=", "xrVRoTGj8p4X25fDq3YxGw=="), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
		Drive.Letter + Bfs.Create("t/819pXgXY8u92xnRTFWtAbEDEILa2wFsuqXzwpoDCg=","muIbjWJI0boO/wGue/N3kHp8H2gvWKOWxQNIxP6DAw8=", "W9bQQP9nAlQzhJV4qggB/w=="), //:\ProgramData\Microsoft\temp
		Drive.Letter + Bfs.Create("5+Tee1sYJpPMYt5JJ65/azrZozHM4c/F0WRyDH4XkGo=","2HKW7TLl0ebiGR3V5fe/XmL/yPvNMin0wn9Kx1Xs9KE=", "ayvkEZmtTRBrqdoHw3VqrA=="), //:\ProgramData\PuzzleMedia
		Drive.Letter + Bfs.Create("OmrfS5/nLdIFibr/9GVAgUbf4o9G4l/ghJfdJdPTwCs=","Cpy/H7e6T0lJSeBsbfEn/nc+nkDrIQ0Pj4GgLXsgazA=", "9Zxi1SLv11lnPqm/bREdgQ=="), //:\ProgramData\RealtekHD
		Drive.Letter + Bfs.Create("1jI7XSqb6pK3qNX5VLJI1uDCETSpdIuOwMuhy0mx9AY=","a4mNuygsb2sABd/Qghs9cgjvFptoh+khPqqOoVYd1FM=", "GjE20+StroIsvqpngdLHNA=="), //:\ProgramData\ReaItekHD
		Drive.Letter + Bfs.Create("35PZofplVnmjFq1QUJvLdQVB02nR5RWVdLa9uS/rGLY=","ZL8vWwCs9yELEuvUCg32gKolLE3D3prH8YIWvS9om6Y=", "EqmTwsM+ar3sbz8gzKuVcw=="), //:\ProgramData\RobotDemo
		Drive.Letter + Bfs.Create("FiuXGS+mAsSVQ0Y5Jp2SjIgW+9s2kihUdcY3WlyDNjE=","X5DilP+veIOCCIqJtkuml4XdYffGT16Rzz7ikjSeWqk=", "za80FCpO5lq1qRDjdLwZ7g=="), //:\ProgramData\RunDLL
		Drive.Letter + Bfs.Create("Cgw/TpcjBUYXh5lq9rlOncGaFGfNyImfneYnlxKJTd4=","nRBENyTBiwnD6k8Z8w7zYuDMuREmWwbfeyOnK2fCP3c=", "L2H1qnfDssJr9rFgZ6qjLw=="), //:\ProgramData\Setup
		Drive.Letter + Bfs.Create("F3D2kN+KyKrPyNvsnXW68CPt63nGjVD3KgR1++RXuqU=","TDGk351SCZAY7j/dYNOCePGTR1QSqtrlfqEbBXRid1E=", "CQcGEfJVVKU3mpqdvpuVnQ=="), //:\ProgramData\System32
		Drive.Letter + Bfs.Create("Q5ujJqEUMW69AWLIDcFK8gffXedW43qGWFm2gg6rtYs=","JXtvFivWDAVxTqknFjFIVxq53i1lLza5FfYqXg8laIs=", "ZX/P4c/SXNEQhMQsHGZmZA=="), //:\ProgramData\WavePad
		Drive.Letter + Bfs.Create("eErGoCHJoidx4y0WxBv0QSfKAaVeH8UL0loyIuGqutj1HwYLFD0MesuLAj5E59jO","1VnJ2YmbZNpWzaMA+XT7B6fwU2MHawYXl7mcQH6aJPs=", "9EW40lIbLK07EwmLf59x0Q=="), //:\ProgramData\Windows Tasks Service
		Drive.Letter + Bfs.Create("IdpJ3YzdKqSl72vLaT5JhDN3QpQVzkjwfM2we/XgMfE=","nOJe4SCrUW1CsyzTLzbtWruw97Ek3qJCAw01+SBVPz0=", "Gbu8uw9p07FKErzP35lgNg=="), //:\ProgramData\WindowsTask
		Drive.Letter + Bfs.Create("iGNOMXV/7WtbnZMQy6/u2kIPWiRv7Hi//4+g79/L3Is=","EUwaYEWdVuPS0THG8d6IadHK+hO1DyFPyowlx9LbGaA=", "1Y656PVDrNKlPbC/aw39Nw=="), //:\ProgramData\Google\Chrome
		Drive.Letter + Bfs.Create("v6cDKoPHK4H92YItHvwSBk8ykuSX4fXmy8c0z6E3z60=","a6j0WD+khqwFLhzKPV/VT2blSbqt9zIkeGZIF3VKcfk=", "CtjefSqPqmElZGIMNjWmJg=="), //:\Program Files\Transmission
		Drive.Letter + Bfs.Create("n7ZNtN64Xoj5bGUHL+0sQe+DirKCIMHSldZc4zCVFOo=","cRG8+bodTwk4GUmDq4aLAbTqNCjTbgFlUrBdxuUqzhU=", "cRSMyZffdImACXNurKFM5A=="), //:\Program Files\Google\Libs
		Drive.Letter + Bfs.Create("OnuOG7g+3RUv/r99xM3OGi+0SCp36FSJdsm29kFz0azK1+mwH1m+szPElxfe0dnu","pMY0Q/Uat2XWTIlDQHRr78uIBnt60yFjW80NBCQlO7Y=", "N1VaHPO1F0ylxybqmWAGXQ=="), //:\Program Files (x86)\Transmission
		Drive.Letter + Bfs.Create("6prLzeLT3nb/aAfjvyQRDBqHzuwem9D33PBOtD0gkSs=","jvIP7TFP6JRUNCR1f0nLrdF4fj0n9iM6A6iV5jc73jo=", "QcGC1cMgangZ/FA8ZqgfpQ=="), //:\Windows\Fonts\Mysql
		Drive.Letter + Bfs.Create("/Bi9e1zKQToXaa0Ahoc9/7IGanQkuk31TGBhMiDAv8NIOIqGNDVPHnprJoCz8o72","WOQMkdhjWtktg1cb3wvbLohsR9JhCQNx0V+VrkimxLA=", "svRdjLm5F3fsxgsmwr3/zw=="), //:\Program Files\Internet Explorer\bin
		Drive.Letter + Bfs.Create("ogD+SWj54WZSwWuN/CLEK7mVjNVgjVIgIlKzUMZjpZ4=","HbXWC/7HgYt+d3IEHPrLiinKIOztbxBDunTZQo0YeHI=", "XOb41kQ06f9sDGd1cVgduA=="), //:\ProgramData\princeton-produce
		Drive.Letter + Bfs.Create("Sjvi5wTDo+9MNDjZx5bJb/jxngpEQewWIVVSpt8Cr9U=","JGnaSsCaEu1y8MXBQdWlmmEm9zC25nXPxTfFRbYLQE8=", "A9Llmpn7BfNRdANre9HEGg=="), //:\ProgramData\Timeupper
		Drive.Letter + Bfs.Create("pOeFo9v0WwLl81YCxubf2eEEOd6eFU38xDnoAJPJXYI=","aZNKIP1GpDKs7TnejHwRlh21NKKMa3G7BsKbfgzRV5A=", "xBmGClB9Rb09XychNOYKHw=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("cI3HE8+HHuIeAdy2Ggn0iRMtcBuo1q+QPCp+FSh8DBI=","v9uTZi/rfGbmzqXVAHJCBdDWwgE3Y90DNfyu6Dru5vc=", "B7+4+asTpUb/zxH5/ktAxw=="), //:\Program Files\Client Helper
		Drive.Letter + Bfs.Create("r82RMsClRwi7OSiIS/sHrhTvWpRj4eCcrDZwc+ry6i8=","Xy1JpyAA0UC7BC843U52TGQ7BfyN+q51k/Ktj2Xee9U=", "zbjb85WS08jBqPKkMPMeuw=="), //:\Program Files\qBittorrentPro
		@"\\?\" + Drive.Letter + Bfs.Create("mko5Z00jgR4w8cgfa3KGd2lMXvQjRrkTPxL0OGP49E0=","Z5V3h4zYY5wbyhnPIb0/C50CaZQgGDwTB9fBfQU54Q0=", "NoeVfQ3FqNY9m0Q/86wgQA=="), //:\ProgramData\AUX..
		@"\\?\" + Drive.Letter + Bfs.Create("RqSI/53vb41mMG1tt37Tj4KFjTf9+lTC1plsfJV+n2I=","fsBJodu1xrG76ccIQGEF9b4iC+LxM2dXSqNsj9l5IKY=", "P6aSx9Y9MU5Xl6krFqEV4A=="), //:\ProgramData\NUL..
		Drive.Letter + Bfs.Create("r7B76L7enpvEVffXz+vzoRXSXleJN1U6hqbQvBErwQg=","0FhdwVKjjmtGyI3QQQuk1NHiwECOVDv557/oqC/zgWY=", "zXYs8WPX10Wnkc/INECehg=="), //:\ProgramData\Jedist
		Drive.Letter + Bfs.Create("8teOagvLl/S3Q32EEIS+CMXvGNqElJUG6+dVlzqQkVh6hAwaF4tN7+wgtltckQjQ2qW1uRZyMqTSUtmP55nygw==","q4tOe/QroURMVCoyELeavh8iQldF85VwNliUIiETIlk=", "qXoh+IPqc/OPGN+y7mBnSA=="), //:\ProgramData\Classic.{BB06C0E4-D293-4f75-8A90-CB05B6477EEE}
		Drive.Letter + Bfs.Create("r3oU8escR7cAWEQR4kzg2CLrYiJH+ZmHOj5S+A48joXfuJx2F72GysyJi8RY41eWxErZgC4jeMEKAPRnL8lWKg==","S2Ylw3mOqtW5H76nbz+zHNd+HCbSzz/H7k50Si6gdTs=", "4raMQ71TFJbVr/Ue/n0T1Q=="), //:\ProgramData\Classic.{BB64F8A7-BEE7-4E1A-AB8D-7D8273F7FDB6}
		Drive.Letter + Bfs.Create("AjOsZZHIKf7Q/yiz7IiWsXW+iCzxZ9MaQGS7+prwtsE=","3drA9TDRjNgQ+BETCViqU+8Mu60aL7/j9L2v4WGcycE=", "8TVR5f3LV8ExR905nvFfTQ=="), //:\ProgramData\Gedist
		Drive.Letter + Bfs.Create("8WhEV4b3aLpXUADQw2McvZgOZiTssBOOFFRdCCAlbiI=","H6CzGy6xHsdsy3pGWJO5LfsQ7JOo9vbuR98TsK9mKSE=", "NbztTC1ZIJXsceYDcOGIaA=="), //:\ProgramData\Vedist
		Drive.Letter + Bfs.Create("hkdvv/RCZvmciIpKg55qImi4YmNXa4BWftYslZ7bueE=","/qnXormdEUp+H2j+3LiE/zluQ//r2LhubZLECtcbkvU=", "jGEIiFS/XXnIW9ASOaoQBw=="), //:\ProgramData\WindowsDefender
		Drive.Letter + Bfs.Create("Y2QE8NsCtgkeW4R/OaTjc6zabU4m8z1UXVTWyrhioWo=","iEa8oZUXSRNZbdRBvWDYYHiUpfLy2A7CmubpeGlox1Y=", "PFeG7waRcjq7nXf8zXBjeQ=="), //:\ProgramData\WindowsServices
		Drive.Letter + Bfs.Create("WNZdDszSIuJfwio6Pm5u1HCau6I5F1XQ6ikv/os2M1CHnQT0OizrPQsswZoEd5QA","kJqExMNpB4k/CgJqHKfC/ZYmIpE1rIApYCgu2RbRalM=", "HDj+iElQGBuCtJBhH6HTwA=="), //:\Users\Public\Libraries\AMD\opencl
		Drive.Letter + Bfs.Create("+6/qfabntWwz5Qg5WNBr3aE28rAHsM3IsfK6/GQS5cTtOe4bOOv8I4RSLAlDw80E","PaX5RZ2StRYC86FPfPFFFcd46QYysOyAjMnAL2ziAvc=", "rjcNnSjoxXnVTm4wShHqBw=="), //:\Users\Public\Libraries\directx
		Drive.Letter + Bfs.Create("Hyu0JdG3yjSt9876l7BE67/w2EtGNUoy/js5+PnJ1FQ=","5bvYZnOSubYNho5rdcfVIo3cXsrR0i04LKv4BG0ydO4=", "+w3An4Ahywt05ggh57kxBA=="), //:\ProgramData\DirectX\graphics
		};

		public List<string> obfStr2 = new List<string>() {
			Drive.Letter + Bfs.Create("7cWV4v2YsKsG99pf0dU4ZAyKSz9MI5frPBQlqz4m+Ag=","OVFmtGatgrFZNmn0t08sX9pFN1Y6XVBPIhYLJhT/8mw=", "N4KrNP5TWqs0Aas5xJ4S7g=="), //:\ProgramData\Microsoft\win.exe
		    Drive.Letter + Bfs.Create("MlAQ6C+UELVWAJ+vZwbyNWRzlmG2nOlrlbbp8kuE1I2pJYkCTGwwuDVeaZxyWZrb","KRFqcTtS7dL/TQUb5uRKWZVRH/41u+Hx7mvjdY0bXJ0=", "vH28IiQ615x+lgFpsjYhZw=="), //:\Program Files\Google\Chrome\updater.exe
			Drive.Letter + Bfs.Create("HmYpREENFL83k+otRSlAJ0PzEpz4iKoEtgCnud9sc2fStsEZUnKFrljwIuvptaCX","K7qpxoMdc5vChEYNhQzk3/34DXki3v3sDpSU+JeJUCY=", "MFXn2okev7BruWadEsxWxw=="), //:\Program Files\Client Helper\Client Helper.exe
			Drive.Letter + Bfs.Create("1rssvEITO/F4aaKrwa6cLWWgwqfBCuf+4a1PEIG1vUT4batJT5NYXCulFsLHtsu3Q82SqgsJYYTHKzjJSrSHWA==","BWjMBvrhn6dWbV+MotxHdcyCt5JhJWUbqPKzBui48QE=", "AWw58YhmqO6pTXoFbu+ucg=="), //:\Program Files\qBittorrentPro\qBittorrentPro.exe
		    Drive.Letter + Bfs.Create("2i8cRcw9d04ebPoEGivA/UThXR2cZyBbB6hiKu9SUQMpNkTS1mlYqdrr2XThESQE","I9fmbLnK7VjB4vbtlAuCAlT/Drm3DKabiD3Q/J/ZG74=", "GL/PI9WEVHBmfSM8gtBfRA=="), //:\ProgramData\Google\Chrome\updater.exe
		    Drive.Letter + Bfs.Create("A/xAG7kEIoKi+J9cYaY2/wMQLPAKBP6gSSaxocJqNK0=","vOt58FRzi0igxdxhXw1d3gZzgTqE8RDmLwqfZD9Rq+Q=", "vc6JP61l4ZI/onNyCRmiWA=="), //:\ProgramData\RDPWinst.exe
		    Drive.Letter + Bfs.Create("D+m4P98U1iexBfycOJOpbo+SyanEwM3I4jZFF39keUBNgi/ZZcgm8MpRJWdw5h7i","XrSz2Nu+XfpQjV3yNJeIP5DVLVwGW+PBiOwPixbcjjk=", "8F9SMplN15g1exRXB1vsig=="), //:\ProgramData\ReaItekHD\taskhost.exe
		    Drive.Letter + Bfs.Create("jOBnHkjYdM53qZMzv1XpMTRACbiB3xnTvWGsj5G2l5bmQ9lqeB24XYmyyi8amz+X","AjEEh3NJV8zQ/7P5ANiLQAd2cUgVoxXbkyYSHoDpBZ4=", "jw1UQaLo/wUinnXEvI4E8w=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		    Drive.Letter + Bfs.Create("jxYVfdUhjvZDv81cpnQefEewJjLVczgicdB6O0ygXilGDK02GiOsL+r9JlF/nvyl","3468NdjY56hlsnw6B6oQshl0LiMBweiPGyjSx6bg+L8=", "dlXl3FAtxe2y4lTT4wz40A=="), //:\ProgramData\RealtekHD\taskhost.exe
		    Drive.Letter + Bfs.Create("7nd7Oiequ2HkzeXb/oKuVcQ+UC+vL0BwWSXAya4Kx0GJ0M+RQyr9xSctUIIlgQGn","fEkkPx4FWVU+aYfsyfZEQQmhIJFA+LwwhnaQwQfgxMY=", "V+Lrrtpaf/CkXrU7DUJdBQ=="), //:\ProgramData\RealtekHD\taskhostw.exe
		    Drive.Letter + Bfs.Create("23nT1u0ecdx9oj+KYXI8nXUjqzZO7T4uJ9qKjovlwBiK9w9X4aYLrzcxocZLcpRk","Do2BAC+FmnSgUdnku6Z3qC8Ye1nvcID2UJt+3o//5uA=", "hv9bHfkV7xQ5QlLkuTuFCg=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		    Drive.Letter + Bfs.Create("7Qz+ybqdOhiw9ekQ0dCLU8W+E2ISF7sZmm5ue3bPAosZtS9IPMhJzIk2ViKkWCsi","CqTKjCg53sRYZxhSzeB3g6jLorXDVX2FHAuEUocL8lI=", "07gium1XsVej525X3lPaQw=="), //:\ProgramData\WindowsTask\AMD.exe
		    Drive.Letter + Bfs.Create("I+jjc/7paX6Gefw5ChN54ZP4D/MJ6uhsrfsV8XrhXCJqjK1V03bkLcRHPYJFWHRd","4pSFFxCOq6M42MRmJAH4hpxYVrsmDrDjU1sh8SO8BdU=", "rOiKeEDRxj+D/itTe5zPMA=="), //:\ProgramData\WindowsTask\AppModule.exe
		    Drive.Letter + Bfs.Create("sx2v2jcVavH/gZWq6fku3FyUbayGcFyC290kpjwkE4V+w9ysEcfK9X0qKPFWADM1","nPxtAG5Dct40Cvu9ZIaKHbaQZEIk3ivE5aTruIpCkFU=", "remecOLCF5sWa+7RAxQmFg=="), //:\ProgramData\WindowsTask\AppHost.exe
		    Drive.Letter + Bfs.Create("HatrXgXopKz2p6IHH53zz+JIch9BSFOsGU1E8j+gle4ISQwl8P4Woxctg+AtpKax","PHyvYO4hCbjs2/qjIOaF9ne3lXYYJodfKaoMcbDdoko=", "eALIt97Lmi9tSqseErEV5A=="), //:\ProgramData\WindowsTask\audiodg.exe
		    Drive.Letter + Bfs.Create("qUjuYEk95xuQJcOja1FQ3zM3rcU/5at2GbKXZFwK+lfLxzrd/oPVgHy9shCbyE6U","tQEQQJ/s+KlezxFO7xOeStQ2TUNturr+EOPpnC6Ki+w=", "uD8HbheEQcl8QLTXDakQtw=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		    Drive.Letter + Bfs.Create("6WOJeNrhyjPVm6HzwF+aniIaisjOSqY1up85SIa2Oto=","YHkH6/VAJj2lQRmlPlZh8pkvXnj4GSqp+n1SGCXhZls=", "8G0s3Z7kLS064F26UDQd/g=="), //:\ProgramData\RuntimeBroker.exe
		    Drive.Letter + Bfs.Create("Pks3Is1Ek/SO/4Ah7vIejOElBlrfa/07h9rjAcN+7eo=","nhOV5g+gGKPuVtflJOosbohAUAuJTkIDA9enbN/vmvI=", "mOTM3dOuyW4cN2/ECLMAqw=="), //:\Windows\SysWOW64\unsecapp.exe
		    Drive.Letter + Bfs.Create("6+6FhvHy+31gHlBsvBE+olzs0vU31loE0baFohEV7Rc=","jvPitaaNqrLpeNl1wNN9bOZAh7lTgrZdtLowg99x6oc=", "gFqz9M76NHwd67DBTNzGmw=="), //:\Windows\SysWOW64\mobsync.dll
		    Drive.Letter + Bfs.Create("/+HXUsIeTUY5hUSGBlikz8qIhYc19d87m1HbIH0Y5qA=","nUCunM1hdAEmFsVHZuU9ADiQSwck/Wq+Ez69M0djN1k=", "SBMmpDgcg/fy8JzxSRIzFw=="), //:\Windows\SysWOW64\evntagnt.dll
		    Drive.Letter + Bfs.Create("Beh8/Bfy2DzO5JXiSlsYOpVDigmkLGAZAWE3EbEAEos=","7lj0KW5hm8m2lsZpRAPSf0sQDJifttGzR7dmVDtQQk8=", "TBOqRR4zIuQbaCHmLG/s6g=="), //:\Windows\SysWOW64\wizchain.dll
		    Drive.Letter + Bfs.Create("4uXWS9hiiuuZBTYO6DD2yh9mhmR8xPD7KgA+3p7zg0o=","n9bmRGy6idx0E0ezAvYFCAVuDkj/zVNyHDTQ9/svxPE=", "PU4epPMRZQxsx+lV/G3jfQ=="), //:\Windows\System32\wizchain.dll
		    Drive.Letter + Bfs.Create("Emn7kWsqQmTY+4IMFSw8FKtI/KTjrGq5JJLjF1jguVRGhGLjHc5IGFs9Y/xjdyA3","8OrgtpnJccejKUNKyDfstM3gLqs522/xSTTVa/r+iuY=", "dE5U7QSeN5f+3M382/sQfw=="), //:\ProgramData\Timeupper\HVPIO.exe
		    Drive.Letter + Bfs.Create("e5LtSYjKAkYvTRfz++WN9OeyTQF5AiRdR8e4RsWTayYhLtIk/QozDypiQjyiTlzk","/IhxPraaH5s6i41eXT+0vupJOix2ln0fc12lsotIskw=", "YDr4An5BKL1mcpxgl1yaLg=="), //:\ProgramData\WindowsDefender\windows32.exe
		    Drive.Letter + Bfs.Create("CgQDrcrcWiT+EAgGSupEpep0W716bCbDpK2o0rbzXU6v0jOUhygdWFVfyZzyqw6T22rxaEuUSyGYR//FgF+mKA==","hHiNgccKzh3t2xXx6IjuR/BahDZayPj2NuzUyLFm9k8=", "mF9DQLGmA6rEHWZD3Q+a2w=="), //:\ProgramData\WindowsServices\WindowsAutHost.exe
		    Drive.Letter + Bfs.Create("to7toCUBVjZNxWgc85tSDpNwfsKu7m98srzt+OrE6yodvBHjEbJG8EiV/tyV5QMM","+ZXfuTRyUWh9O0liFHhUgzYj/PadUqneG9GQsNSHyhg=", "TOfM9jhWkO7F+B+uL9bXEw=="), //:\ProgramData\WindowsServices\WindowsAutHost
		    Drive.Letter + Bfs.Create("SeHVGmZkVMarDVMbvXOr4i4nxWk7iCVvDfqVg2xXkSmaZdM0d+YEOmeTlo/g0xrq","z8x70JkaIVbTWEb79pd6Zosly1NcnuYJnj3bOk8tlr0=", "fxJula74S8UQ8H4QuthJ0A=="), //:\ProgramData\DirectX\graphics\directxutil.exe
			Drive.Letter + Bfs.Create("A18qAkfh54Rb+BYVpPci39ShsSaEJLNbtZMCtiJSglhzFG56Eeov0TslRlbJgqFI","94fKBcV+9lxAUPZ4iAYau+vXeeE8Duz4aZZ1tWO0zYE=", "XuI3oncs/igojbXIrri8UA=="), //:\ProgramData\Package Cachevdt\sezSearchApp.exe
			Drive.Letter + Bfs.Create("NdRkfTWqesWFz3/kP2U+/8jDGc5DvsoalceEB7kcuuvQfqkngjdU4mWrCQdTBWMHvxXpJDaS1ZwlulDIY+DS5w==","iJfWXL/qjpZJwg/cyqJFDEy05A/sAUJy2XaBx44G+w0=", "TX9VWb88cBrxtJmD6jKmBA=="), //:\Users\Public\Libraries\directx\dxcache\ddxdiag.exe
		    Drive.Letter + Bfs.Create("f/p5X+C4DQT/DCKHPis4TQs7hFcW19gLDQBathWYSCLwEiyTkZWpmnYI0pzS7ymjfhArIRXC6ZZvmxvNkeo2rw==","IuyZJyTo9y7vzP5VGjAWfAybUCoQbEg9NMqOf5JF6+I=", "/KECqCnJnBHqS/zl4T7aww=="), //:\Users\Public\Libraries\AMD\opencl\SppExtFileObj.exe
			Drive.Letter + Bfs.Create("J7DIDaDNsZupwB24PHqqhr3SHLZ7gKVMYKuuKrDx61YRRRiblpFg/XsMaGvQMvyeE5zRyIwNTyOhA2lUpMESWwkE9FukU5wozEE0txQgQ9oXe9NEI+3uv0TVXr3qYTO9","lAT6lwG2aKg84aBNbmNG1FpnlVXsxjGAXPygW2mj4JQ=", "HmeSEUZXDzkzeyJchrXojA=="), //:\Users\Public\Libraries\2136891f-f876-46f9-8cb2-3ff3320c542b\SearchProtocolHostqed.exe
			Drive.Letter + Bfs.Create("yDkCqzseWETdN8pDcIVgDi+GFBAbS+RyLoMEqR/plFUzKKf1tmsAQX3J0JaoXkBQMB3ZVI0NZYXjHI3SwugdITi2g8WOcDXhjGwuOE5p52+Hlto3LvFsmhN3mHFKzSKU","+cYJGQ2Wcc+LYi4qQHRTfbt+RIyb8e2e1NWu7rYJ8Nc=", "tl4kKCepjTprRf4LE2TdqA==") //:\Users\Public\Libraries\6fe7a17f-a1e3-47dd-8999-d9349a943df9\SecurityHealthSvc.exe
		};

        public string[] obfStr3 = new string[] {
        Drive.Letter + Bfs.Create("xk46un+d2lf27nwrI6RJ5sZPA5fZ7+vGdeuMSIgqpbY=","sKdd9L09pbN5Yd9eNsXJvO16ffr5cct2+L8zabQy6Xs=", "msf792dd/12xrBBTtyknew=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("XhywP2ojkQfxbSREBHC2ew==","vse+RRnICXQbCXvE4L4/U+O1Ib/pJXxsBzJyoUQHvxU=", "UPpQ2aQjrRNTCXP7OZQOpQ=="), //:\ProgramData
		Drive.Letter + Bfs.Create("2yup1t1aoVdnviI+TTsCPtf1QUErNyzFrgugH4DLVyxQrTJUjmwNJns1pjtn3f7c","do2poyK2B8c8EwWb4J7Jojot+9Cskhe4iT+ZHZOEa9Y=", "CipVCnaaqztfIJh6LIa22w=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("9xDgqozRPSkezKjkqU72rNLnbaUzSzBB4aESY+Ay5npFwJvA4+L9Pju3oEoOgQCI","Cu8Y223fyJLxpBQdCuWTiLQI5aeThRgw7DveQvzMNFQ=", "H4rTkp5F/Bnam6m+RPX2BA=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("080T7M4RCDRCK8v3CR6QpsMeXX76fKh+rKOEglxQsarfAlHwxh17y9bWg6/atYOD","erLKiuExVTdMbyTVvVolOJ3MhNlGz2fDxgMWCjx3NGs=", "o4Wiv+4IXx7YtUIAV1vNHA=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("o0IDdnUTADP88vIa03/MT9f38w6DbeVhSPOSDUa8//VIa+ikcw3w2tVOiiLs9KWf","b4356OaSIMbnPpqclQOanZSGMUDsdgUAoMn5UqiZLl4=", "ADeSoJ6cBDClZ2Swkjiiog=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("c762gGgG8vq/gOhijc1a+4QMuKE/ZrC3k6Ota7BWomFygh1TP/CH31pqmU8vCWeu","QFpMsqHXjXdzujstjarfNboggNjKEO48sLaY0e0pXzY=", "XOe/JyITlZP5gsY6RHJJSQ=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("ecBtEMegeG5baw9RoVy7xOEO9EPKzWF3AR9KYZ0gTlNlBof3/dYJO0MVToj/tJcQ","iztUxXlLEajk13bEb3+EtZ3rxKF/DUR3RjkrV7Vyo00=", "3vlNStwGHXbgloTr/LMuHw=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("O3C9CYExF2LNvxvQZ3sG1x9oavczn6VCfyxU+9N3GmHVvtoiEEtR4vNL2BrUUwHp","vn8ivqwj+1RHg1jtH23RcPbri6e8QeM03ej99gtJrn0=", "YpSFvIs3eCPjZf1VGKjWpg=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("Zvfz9bhFMjlACRkboKtfD3IjM4duMoC2jqNqumrJs+qd8LYF/H7MPKpkfAI8pAOq","UCmf9g8QHJly0ucxCe4oZXmX0PRbv4Tyd/91My7jbSY=", "yNOujhrwVCeZtqhvaUn+4w=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("+cK8aEEQa80WnW7ct5+iToI5l/OMAashxDcBCFP5dBWbkG5CDMrm3fKapbW9HqDS","OsAq/nqSlsbc9BVpvJm6mlPzmqdTZH0IysqZf9X1Lf0=", "YrZLGn1FlVDCQqIUwix0Eg=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("JFkasLGJjPrjYvS92VniqzCrbUrevtwAWM9mqXMQ8C6kJwMBV4Jcmt+xT0qgc7GV","3DQclpG2iq7YBN7jfFiHCiUSAIw21DLVG1ulgbZHd24=", "vfUpNcGcBtKVjdUUTxZRdg=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("Fb3nvk/00h/u87+I+IfrwDekoOxYIvQ3nBUwuEKPeg8=","6vn083+x0e7eLSU6mNzdeEqC17HO2d0MSSH0UhIcBH0=", "lB67EUsaeF81Jn3rRAOXVA=="), //:\Windows\System32
		Drive.Letter + Bfs.Create("7JcSM737bGK2HyqtXdtxxugfKh6vwGREmZWPMwer3ec=","NgpxxC7+Gcd80F5WY00N0JfpmRBIsZDzjDDf6fBcH2I=", "zCzVou3aSexZ88z3/zBrUw=="), //:\Windows\SysWOW64\unsecapp.exe
		};

        public string[] obfStr4 = new string[] {
        Drive.Letter + Bfs.Create("QrFY1Z0rY0uXVIi4GFYodFk/cgNqo7FErv1hB/GeqNw=","XhdasOBKJ5QG/PgDdsQFsjm9MNaK/qrcwMpB0cYtCAs=", "sUkP121EyyDlfIVg5lr/AQ=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("kIjPAo4nt1EgkjpLS61oYcv9TRhAync3BI7kqlzH7hRU+70yJ+xEmZwwVZRNmxdX","A7NbuVdVmrtJZRBiOAoqSgIxnsKvTsP7k/S8GgHWDq4=", "6YBLGFJB4jFZPByc7U+Ecg=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("W2LYiUmgmE3mEwEEUAU60s0V7Ht3WZXBadcC6erweUXkSAfRV+dhxYfqHSOOwlOS","l4PXG9ZTzTobEtNxtdszgm31l1uYmoEtQ/BQMEZlbas=", "+g/SoU63wHrbCf3UkoZoEw=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("UuqiKsnlhYsw3rybZw8y2b0X31lceQajEV8iMqelklhq+tymvCTMhEVlJXcT3kWF","Rx438Tc7YRvRnpwexHy8iRf48rqpczQyn5KAIYL61no=", "/71yOiMfvJDpukLNCb1leg=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("iuFZJchZ8N4G6oHyin1KLFRmGmSvWcvZGqps/3Ai+o3jAaqHjCENS8efdy+TX2gg","pjzeB4hvfFC7CO8/mflcpeMhr53N6zSx/jUaDwaC8J0=", "k3ckREaalvAIuqM5WBsxeg=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("x8PK/Li0GSObBwyscK/qDg1sP4FxKScNOJizSCBnY+EP3BD4xeAqdQajR0D8Dj73","0JDUnxDS3mBt9fSbcxRO6TJcoV5Hgu7CPMHqSEsEXHk=", "d/TJZYnaqqWRne1YupNqPw=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("uDF6jhYTTlDcMIl50/LNzkziGdYE+qojebyKIAuaJ3CMG/tNTrF+5sIJWUpWfHqD","0vUrA+DdwxwvZoxjjXwCvWeOJzerKow/ObhWINb6Rx8=", "mHdFUB0yVgZ7TIWQTM+03g=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("MQ5pdSrz6rbfv1lV+1JjxxAHYNsd1YNgAwIqSnSTa6s16QFCoKHxxf6fc2/vdubZ","GwecRJtcvYaINDRAvdkuzdgcZY8KYITpvZlhqakIPYk=", "+8O1xBLKofr5fJxaXntQ1A=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("FpSk/f13t7X9KESUoeKFzzcaIIGvN1RK9iMrr+IczHVmKbfH1cVtPCcWpZHQ7Wnl","rNbe7hYdiabMJEtyFT8P9eOMc1OmiS8C5+N/OYLhtoE=", "7RwTy3dYO9dr8+L8TS7/3Q=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("AyAVDTCUAdjf37My2LN6EzvqoGj3Cg40Cz9qWKyMyPo/m8/q9uDGBlBb5SKNutA/","m14ZLjiJnt6R0zY/NylkLgRVEFx22/UA7odEQoZxv5A=", "fyDBUMHUVpgTJi8/cv7+UA=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("rtFGRBm2Nm8XQ5beAJYMo9oyij6UNoqtDuh1+23BtId8h3wMjt1p/MPjCC1JZyXV","rMS3A7LJLqgd0gqNTd6pR3F1AkYTUU/FhUQjYjRr7wU=", "6BPuGLkEz0VGDQuHP9SdPQ=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("ohEYYjBwmXV4iR0IpULbzy0wX2do91KpYKBShzjoyb4=","Mk4yaqUZB5GB8gnBY80LllVqxwLaVdQoVXEcPSQv6C4=", "GzfFTLikwOZzbDl5xSKfXw=="), //:\Windows\SysWOW64\unsecapp.exe
		};

        public List<string> obfStr5 = new List<string>() {
        Drive.Letter + Bfs.Create("hVkWCygrk+KhMmkf0ETGubLAmMsd+hhLeCSoGP4tBd8=","u/Nbjb8z5u32I/Tjg2L1YsOHijORjvGM/I55OILPUNw=", "ukRoDryoLfwnRdkmf41QXg=="), //:\ProgramData\360safe
		Drive.Letter + Bfs.Create("Z440zTi0VLzbRMZU8kwinTnmQuZxvkeH4hcilzaerYM=","BtzoFUdQO3reQUk/XfZHEyQBHSthdZgPYLZK0LNJoSk=", "S+P+MRBX8f6ifQnmr5uJdw=="), //:\ProgramData\AVAST Software
		Drive.Letter + Bfs.Create("ckDLimRZLQBrnrLPXsd8krBeP8w+jidIFAwMMjhShVA=","h7IrD7ELp25ka1MkJwpD1hTEWlIF+2Dgj2InvEZcJOo=", "24pXFek5TQoDpe/SRwF7oA=="), //:\ProgramData\Avira
		Drive.Letter + Bfs.Create("CFY52ObWf06tfG5qIJe6hqCANGCAyQbxUxU/VnH+/jc=","XU4n7qXOoOhhESXgAe8LV1dVxkNkK0P982lkB8o1jfs=", "84R0/mB1pRxX1BAn36nLnw=="), //:\ProgramData\BookManager
		Drive.Letter + Bfs.Create("+0t+2rQ+F5uC+IiMLsaKOvzaZjP+W2MyWmrQZRkAWBQ=","4Qu9hFlTQwt4mhxRU0XKpldSdC7186+HADj9mdQt/4s=", "8qwuoEUeErAk7QNxnnRI+w=="), //:\ProgramData\Doctor Web
		Drive.Letter + Bfs.Create("1sh5kIcPUE3NI7t5bgOYxBHaY8dE/k77KbzuNkAKxfo=","goiP5f+aGzV4J3jETp/MwB023Q3rSKuLzyz9R5dI348=", "K/F2IGwKe04dB3PoQNdP4w=="), //:\ProgramData\ESET
		Drive.Letter + Bfs.Create("KaXYZCnA9e+VHwsRs9ZFxu2IP+oGPv4fpfrM6rHuuq4=","UA+qSKVLJJG5P+qONJ+3cKGbKuVNCQ6gxs9hqfcIlT4=", "DDSrPQXtHTLVdJfBtXIVGQ=="), //:\ProgramData\Evernote
		Drive.Letter + Bfs.Create("6aIeB2f3pclU1fibZ9L8HGFZQxce2xmXTJy7Tbd8ddU=","iGgzJSt8CUxr7/fbOkNieIqTwoF8vSPcNZpS61dDlng=", "/OyGvcYz8lkBWQjy2MO4ag=="), //:\ProgramData\FingerPrint
		Drive.Letter + Bfs.Create("dd96YZ4D6sM2jDTutDtsPBXP54rXg+7FWWcwDYTRZfs=","K+VEC1xoZbUzJECw5TprBSitRU6tG+HHsUwv7ta+PrE=", "xfThSlHCR6/3ffFOeuPgpw=="), //:\ProgramData\Kaspersky Lab
		Drive.Letter + Bfs.Create("ZFbL12ijv5SoO27KZ8pn3vdfhOOlqYLN5aDJh9Nl9XUK3us6Gm/gYgHcq6/aeQe1","KcT7thy9CdyLJLNkb2iDp4Ge7w6c90LpIDl4a82niRI=", "mksHDsqJvJJx9KsATx0k3A=="), //:\ProgramData\Kaspersky Lab Setup Files
		Drive.Letter + Bfs.Create("hHOqB4N/rMxOl2xBtxHcJSbfN6/zrM8XqN6Cb/4itTA=","xJzEp2rtnplYlCnix344/8pku414439L7SjC3f4dEA0=", "KIt6wYGW2EqcEx0ArDgByA=="), //:\ProgramData\MB3Install
		Drive.Letter + Bfs.Create("zNRm0vfFZqT11ruxJK7x3nNnHdgx9VSLaJf57yzp7Jg=","/FdIuqbnhtdKS1ugb7Y9b/RrspgxbthZCIzKYCObfqY=", "cCTG5AikRSlFbD8Mpx18ZQ=="), //:\ProgramData\Malwarebytes
		Drive.Letter + Bfs.Create("euLDZ4Cy4M/5Ph3DRud/EkNmSVHffpO00cBTSz7uL8w=","NGrgziuzbTfCHErRfeBZJeYnJdLd54WmyFFuklv+e5c=", "sF9eIbZgmN/6SYTO4tDUqQ=="), //:\ProgramData\McAfee
		Drive.Letter + Bfs.Create("6bnuykkA7Sm1Q1/1Ke05L2VLer0Wr7OnedRPu5RdNN0=","Yg9xViFWFHvVS9nOsinlMUMWFn5wRpyUm+XBVkrrg+M=", "TnlQaqAyq5fRiy6YxbYtbA=="), //:\ProgramData\Norton
		Drive.Letter + Bfs.Create("s3QqqN+RP76uw0i9fSKTdX8GhlEk1iGZvqQBKlPUl0E=","tTouuqCwxDfNnup8Wz9p+frpoww1nzNg5iEZB5dFQck=", "zIBX9Zd3d8l0T+klz8HTVw=="), //:\ProgramData\grizzly
		Drive.Letter + Bfs.Create("hDPdU7xeEcAkMX2pNAA1gN209FrSckDTbkazmVGLVsRgWulB/T76eHtKicrrPOz4","eeib6iC+N022GIQq7+L0uXWOglSs7gMbDW+SKYYBuTs=", "YjrHiquMgFLtI62G93SyXw=="), //:\Program Files (x86)\Microsoft JDX
		Drive.Letter + Bfs.Create("4x8UlogHSWFyMpmRzCEug/0KEHG/RbPVQA5SjOoNoP4=","8B+WxTDeoqVQOBxK72UVoKOCk/GvbpNl5oMUNovcJeM=", "R8B1lOi966pCqt2FHAfpCw=="), //:\Program Files (x86)\360
		Drive.Letter + Bfs.Create("UZdSw130YluaDMUsZuPfuZHqVR+RbywI/zv0396ULDQ=","6oowhJNN1BhxHt3S+MJJAOvJmDd4kH3SVsZe6xtNSOU=", "n2isa4fhBYpq983ihHp2Xw=="), //:\Program Files (x86)\SpyHunter
		Drive.Letter + Bfs.Create("VkfuKTqJxlH7QJpxA4N/FOf89Rpv+hqNQKQBv+Z/9omwpix1PmpGpTM34sRJueoQ","H4rbA7OtgZtyQkTy/7+oRN7tNE2nHcgn7G8xAIFr8F8=", "x/TOjTfGdj92HRgYtKlweg=="), //:\Program Files (x86)\AVAST Software
		Drive.Letter + Bfs.Create("eF0v4p8ajgNTDJfdA/6PtFL3MxW2LE8lyJWQYCQPrsA=","6dkybZ1kD1hoxZ7d1BPZ4FEMpnfXRsk0A9Wbl9Ns9/A=", "AQHWeHOqCVtf/qUl1QCkmw=="), //:\Program Files (x86)\AVG
		Drive.Letter + Bfs.Create("QxaOZhlLpR1D+jDcPtFEHPz9IBtrtq/jSZbPiPmnTe93PNbQ7CCJ1vzj1TVCCJf0","2fSXYdY4JtMMY6UTFmFMiDsJs6ci4dSg5c1RbiXcIyc=", "p7O4J3EAVl9ZBGpCxO2z8w=="), //:\Program Files (x86)\Kaspersky Lab
		Drive.Letter + Bfs.Create("QVWnxmKdwiuGakx69wPOQx3Czz742/tpWvJZr8dcZmY=","nlwRGLz0wUGuoh2fKCdMcp0CKZZOUXK7Gqf5Qn6r8mg=", "J3k+a0qcmTrHOtOFMbqoNg=="), //:\Program Files (x86)\Cezurity
		Drive.Letter + Bfs.Create("XjCYWxWqfp8V04EZSHdj4w6oLLdVq1EzdTPvieIODMpb+9RWfDfvQ8Xs1fBtu4Ld","sy5q2Veq3WMw1tGB9P5F7SmYhaeJCCUH7HlY7d4Z8qo=", "52ZZI6Y8R6x73F2Z4dwnWA=="), //:\Program Files (x86)\GRIZZLY Antivirus
		Drive.Letter + Bfs.Create("TDI89+Uo2GnFinbIl2aVIz/PnSioonDUKMW2RH4RIZ1Re7t8Ae0CSG034yUR1UOk","7Ml1rfqbBIcUrbvGtbBqAerBpzfF4pQsX+eZ4/zwrXE=", "kZAr94UNuRjcBL3h8iAJ/Q=="), //:\Program Files (x86)\Panda Security
		Drive.Letter + Bfs.Create("R6T4df+ZuSlOypXsqhy0m9v2yqMwunIGL/oWxgPl4jqszQ5Kbi9eRlu3wGADXYTw","yjPCnYpLesi4H4P9b2RhQ1FxNoJCKOQp7JYwapI1wYw=", "aphpeZJH/p/Q8NJnQ8lm7w=="), //:\Program Files (x86)\IObit\Advanced SystemCare
		Drive.Letter + Bfs.Create("E5e37mdIWYgEExRcbHEJMJxLAdYNjQTWwjLWfqMnoZQ01MB68mYNmnCwxUYTvdJX6m6nBJNsGb5sNnL6w2salg==","fnN4s27YT1fMAObk2mWee+/OvRVYpCzke3NKFSdVpGg=", "Ow0fZK1MD7ujhYh1xfd0lA=="), //:\Program Files (x86)\IObit\IObit Malware Fighter
		Drive.Letter + Bfs.Create("EfHs8TDS7Bq2SSKASES9mnbrKYmeXzZwG57B9juToqw=","2jTERe9kagjl8B/gux9ldcxXloOMPQh81A0ISuY9Mzs=", "of2v2xitSoKUP6qW1YtyRg=="), //:\Program Files (x86)\IObit
		Drive.Letter + Bfs.Create("3taLAmkKoMMcHj1sSwp7XZM1Irs+vPj+wvQf27Wry+w=","ngGRFCeIw2YxwQM9ZafMvUhR6jr1J4m4JE/oIbh8bSk=", "ufnCqHRn4LdE3cRP2DBHtA=="), //:\Program Files (x86)\Moo0
		Drive.Letter + Bfs.Create("u7vPh1S6+uXOvI7oTbZ1L8n48rpDbd2sgPKJWRvwMx6xPGOkoLtm6jaZLxmH9vnY","gCP9JlaqsYEBrSQEpUr/edjO3dS/OmCetb7gy6YdNrM=", "7vW9ya0Nl1EMa5BH62/bIA=="), //:\Program Files (x86)\MSI\MSI Center
		Drive.Letter + Bfs.Create("wo01jFEnBptvF4OKeRLMLsiNJ4HO/XDNqnKGZ0mT+SY=","3Zszyja7+TlM8F//xtG6HSQnjSS06vHZG88P1+vkJL8=", "uIZEOi3LLX79svie2fPgPg=="), //:\Program Files (x86)\SpeedFan
		Drive.Letter + Bfs.Create("C3SmGZXti4sQ23NICWY36t8MBU4QddTMkGw3dWjK8Og=","mP5RMnldcVLDgLFJnXPZE4J1QVBnkvI3kBe7v5cKtkw=", "B9CCYj9Fvs2S7AkY/bPMog=="), //:\Program Files (x86)\GPU Temp
		Drive.Letter + Bfs.Create("SdzWjex8K16Tic+mLHNonIpAg0TMBn8ffwk44ZCnOLw=","xt6u/r58ZrMHY638Sy0j+e9iR8sMlfG/5TCq0c2DTCA=", "IFYr/2oSP7f94HTT+JN9bA=="), //:\Program Files (x86)\Wise
		Drive.Letter + Bfs.Create("JSJlKIGLhCjgT4/swSPAd+sPhNxYUoQ3/klrW736aW0=","MpH5MYVYr/yyV4mPeIKmLLAFSnhSF8+3o10aqnrSF/g=", "/dpGUySAKmbbaG/6ZssMxw=="), //:\Program Files\AVAST Software
		Drive.Letter + Bfs.Create("pa2X6CfNhdLPM4qNL4mpyYeXbnmIbejdf5xxVYkNlV8=","ElyjAdo0b9lfbNak4bNux0rM1SmtmJs5PfgTUCMWIgQ=", "dKVafHu/OqRKkErTAtaIUQ=="), //:\Program Files\CPUID\HWMonitor
		Drive.Letter + Bfs.Create("IQO6ANSqZ6kc7v0AzSnCuBoro+cqlWBLJbyKhikLhZ0=","6xJ5zcSJKhfdaER213LSnlJEXs6KmdXhXfLuLH2yn7o=", "hEpgnOCzLLDdTFmiPrM2yg=="), //:\Program Files\AVG
		Drive.Letter + Bfs.Create("QD6d+KSmilXFdhyQwSp7gDqLnBbGfxkFNN25ogvRTtT2cShzee8SDwE78weTthm4","sFPDXirRrWRTqAvoqcnf5JP+2+W7yP0JR+vaxfuJK+U=", "kMTppKgGoMFXd8Jemc8nbg=="), //:\Program Files\Bitdefender Agent
		Drive.Letter + Bfs.Create("tsJI9KpjwspxymJp7Qt01PgI9B7kBjtSuq8Uy9BTx4o=","J4lXl5nLyAM6aa/j42k5QdMhzf+HsgqDcLs/GY30ClU=", "clViaSpPK5kKzaJHAgAXFQ=="), //:\Program Files\ByteFence
		Drive.Letter + Bfs.Create("Z2fs+pNY9XdAGu6Ll30mXD9DFgkvEnZw2OoZwIB9AVU=","MlteMOKZcucXuljgaTfKf/ftthAe/HCTRItB0i4i8fE=", "/CpSJfwz2IgSvY3hbCCIdA=="), //:\Program Files\COMODO
		Drive.Letter + Bfs.Create("NYSDzsPOE+tfZhbcKcj9fHsiByRNkGLtdAI3MDtkg3M=","tox8Zm+RhKSO5XtgPgbW2UUJKhKrkHq9GPMALq6otas=", "5cZh8e1AUMLe8SvdjSpfww=="), //:\Program Files\Cezurity
		Drive.Letter + Bfs.Create("HqQRbNO4ClIW6OtSp97LkmW7UZDGeQnDzTlGaz67fzM=","t4BIa6K7U0FVt/ze7r0azprHxN9mQWLqcKCuHJHI83k=", "157uURHLgO5ZklG98PjXCA=="), //:\Program Files\Common Files\AV
		Drive.Letter + Bfs.Create("Ts7Ls2BZMIwqtWUw1FDsI8HwXSuZnrN9FuqqI33LHD0nArsz2K4PFB3tYNRmgV4T","Bxm8NbNkOmp7JdTNMv8d3tmiIbGVkdf+3Mqj6AtZLw4=", "PFRUcPh/ujhk/qeBVydOZw=="), //:\Program Files\Common Files\Doctor Web
		Drive.Letter + Bfs.Create("QkzTGd3LffrCJEt+7rPHk4tUKBzwdxNGCom2j0i3eYXhPdhKfAe2Kd47qWd00pXz","IznrBxng1mBOqeutVz4NGtBjLTELEBacpmGcDN3NEmw=", "yXTRrE8A88+CrMErb8dtwA=="), //:\Program Files\Common Files\McAfee
		Drive.Letter + Bfs.Create("WD/J9DAbvKa9MNlAQVlz2mD6jRbMTvm2CG01WQWiV+Q=","f2QJdO2fFmrYeMTegSg0snIVuoE4j/HA/EhfftNud7w=", "70MVN8cG0gGRe+0jeFybAw=="), //:\Program Files\DrWeb
		Drive.Letter + Bfs.Create("3f2XznIKgl6ljQu6jdjRi5sNvONJQIkD1x02hZt0wdw=","kSy4JasXiKtxcC/pnBPY1nBcgOkZVLql1f6lfNX2Mcs=", "WcCUh0sywIQRVL4ngURYuQ=="), //:\Program Files\ESET
		Drive.Letter + Bfs.Create("02nRN3hGXQo1hU829wWOhAUL+ETvMjQb/rJkcyypK2FL0T2FdhKnZgOZyVerZbnF","msjGWpPtzH4c3sXFyLyNDmyLclxV9K3tLJuTJZHOwHw=", "TnjOk/x86GoCN0cPknJu5A=="), //:\Program Files\Enigma Software Group
		Drive.Letter + Bfs.Create("iZW2oXFe4aqIjZe0rlqeM4ih/h/Rn+NUUgAdaZHKWCo=","lRTIr+KImLDq5e4i3IXIBruH8umbODXG0If3/Eq76wU=", "/Lh1AwHURTPlG/PGoA+SRw=="), //:\Program Files\EnigmaSoft
		Drive.Letter + Bfs.Create("5xcyaiq1/RsAv5DEheoAFmKaJ5eaYPx7m+bUFm39X3g=","dSfbyv/gJPZpTInLsED8SRH1KJywAfZeLOdZpREKv3g=", "3/GOQMUQ80nUOCpCAa2bvg=="), //:\Program Files\Kaspersky Lab
		Drive.Letter + Bfs.Create("ijpNJW7IPmCGT004A59q0eOXv0248zwlqYWDpdxb4iqQVeHMs1lEAqNSTI25YxQZ","iYUtOrdw4MjnkbEDoO5UB7Le5bO+nrL7/+aUprXfWxw=", "i+Ja5kmwaBZmpi09erQfUw=="), //:\Program Files\Loaris Trojan Remover
		Drive.Letter + Bfs.Create("BHiP37V0Mjbkv5qN2xQ8Rt8m22vXF7rPXYfYQlNRBfQ=","HST6oetdRcIlq4IVam0+N3L19g5puoGfxLt8vcwXBw8=", "upWy+8GXiVJmQL0S6o8lMA=="), //:\Program Files\Malwarebytes
		Drive.Letter + Bfs.Create("q8WbyOIjfzhGEIoamFPECx7kWSiiUkwPxWA3qlyZHPw=","pGGcUYmW0fNg0UwEQHi8AvCFm3O8A2RM4hfiq4gEEH0=", "QijpUTRqJH35JygzLuKSDg=="), //:\Program Files\Process Lasso
		Drive.Letter + Bfs.Create("KP3NmjLzNjHg235pGj/9bC2jlnU0iad6+CkKp2T9EA4=","ho8rPhjcXRlfgMr5PDAbDZWuZuPvHrBgrj4nWm6aYRk=", "Ry7ETzAMFvi1hrADhwhH0Q=="), //:\Program Files\Rainmeter
		Drive.Letter + Bfs.Create("h6p2fNs8qxxvEEcnntJG73SArztPxs2n7EnQ2YZPYuM=","tEoxg8ZnHqXuuWW8rp6K+G5MqgUc4zaMGv+ppoUj0aM=", "Dcf70vUBOc+kaZyTxD+VtQ=="), //:\Program Files\Ravantivirus
		Drive.Letter + Bfs.Create("mN+q6jgQ9sMaoA+UpJzmFnPQwWXaXer/OdS0xBIKps4=","HUi35jrEVFpbOBWent7a19DBx9qOeuke7slpid9ljto=", "kVOYHQVPNOJo/F9lJGSa8g=="), //:\Program Files\SpyHunter
		Drive.Letter + Bfs.Create("BhC/GVtTtwxclYgxCrmJj4GaXRoHC9pK53Bxl8CmIwYjKi3742LUxzvWtqiLJ82i","FC/YnJg2k4RzP8oVC0SQSSLPei+LgrpgWh6Ss/l/q6U=", "G9/Ech7lycDFYqqe2kIrOA=="), //:\Program Files\Process Hacker 2
		Drive.Letter + Bfs.Create("Y0EOyCLjV191NsoLcnJTpezNLj/9L8rLMLFC0I26tF4=","i+KSKQuAXfEmJFeIAF13aQzwYGYqlx/fjbNiy9NsQ0g=", "XppL8TzKth7P9ev7Dc/DyQ=="), //:\Program Files\RogueKiller
		Drive.Letter + Bfs.Create("FsA0v+9DPYs78MHfHuMo1qTPtCHEdD7qP3RDY3V7RVkYoeWiJzU269TA5+VSe46+","r4JrdTOR+Ly5CLQCunUpIXBF8VBSreO58Be6WF4RISA=", "V49DmokVSnNetMJo9Wkx2w=="), //:\Program Files\SUPERAntiSpyware
		Drive.Letter + Bfs.Create("71mURx42oUJuS3WVsfIE3dYyFO3di27+86qFgCY0H0Q=","LzJ51TDd33GN6kMzud5pRxmBWQ/kdqHQPOLVzQN+iz8=", "onSIgih6+S4jqKBNefIZlQ=="), //:\Program Files\HitmanPro
		Drive.Letter + Bfs.Create("3EJ3Dr1VX8Hb0uPGQWKXuPBI1Q953QE+q4t3pksCI7E=","Y8jUKKkCYj2X2iJesYSDiRJ+/RFuTwoBKKAJg6Va7pU=", "Z2SLsEEQzS9Zx8IvQg0o/A=="), //:\Program Files\QuickCPU
		Drive.Letter + Bfs.Create("3ima0cU+x6mumJtuA6XRRFYzaU7CRkr4eDZOxvpmVfw=","lVe/5snCR0wk0CUTzb9xmKN6Yh39AypHqlXXh11dA2I=", "0bPRdKnq2V70Yd3B1iLhvA=="), //:\Program Files\NETGATE
		Drive.Letter + Bfs.Create("vDaXuwYFfNl3hahooHKQE4uoW7p/y6pdKlnrk8Py854=","FCdzjTuJ5dVR2/w9WCzZ6rctZrkhS93ivnAjh50p5n0=", "jcuaRmlm3JZhee5SS8Ggrg=="), //:\Program Files\Google\Chrome
		Drive.Letter + Bfs.Create("t4YimRDjRazqhx8sumWJ0+HdsSoVODKI3eR+SQByEbc=","sQf4vH/eoij/+QF5PlWxCph0OEkByySQ65L051PeWmY=", "LQu6yLMYrTcPVFsllV1l3Q=="), //:\Program Files\ReasonLabs
		Drive.Letter + Bfs.Create("YCZQ5iKOyzo9Zi2soHKn1Q==","DdmOTppHycH6IVQIjdxlXkMnzPQ0F7vv9vS58Fbyz9o=", "nAkvqyg//BbMql3AHBrvLA=="), //:\AdwCleaner
		Drive.Letter + Bfs.Create("XXQsJaFsgJzuzQ6d65Txdg==","7lQXNKpnVgwxuGgXxuh3/YZLixyLaN/x5PUJ/5EA0bM=", "0kGlSclWFAEgohiR+c5fJg=="), //:\KVRT_Data
		Drive.Letter + Bfs.Create("MVB3OpXy08EMMS+7VEFEeg==","CIsA/xudiY3rlpj4+5RXMRqy6/Lp2ZgdIo3XA37nmj0=", "kJ3kygzGnkMtXYHVoohbfw=="), //:\KVRT2020_Data
		Drive.Letter + Bfs.Create("CZVsJP+RDtgr12SjtpBf1Q==","5g3CuhYF37j3rDPYCOPT57NHTKLXjKIB0L41QsX0n4k=", "7KWEsa+wtx+axRk70SgeQA=="), //:\FRST
		};

        public List<string> obfStr6 = new List<string>() {
        Drive.Letter + Bfs.Create("fyW9riBTQzZRgqRgLGpHnw==","a5e2pfVFjArZwZVPWC0VqdUw1f+dGl4A2aAlB/3Mg+8=", "6gQvt2rWA8SHzBXFkXeftg=="), //:\ProgramData
		Drive.Letter + Bfs.Create("375AJnOBsZrJtB4X90F0Cg==","R3/iIebjPoFREAWr2ftm39xEkMJHdzj0ZiU82PgjBRw=", "4TPVvVIOmzwxaqpBf92QMQ=="), //:\Program Files
		Drive.Letter + Bfs.Create("9IRf+ZkbDOuhc0qkfM7S8w6DdJNnm7oCYJbUT6Dy3Mo=","mmChkuWCxUqGVrj4MNbqabwuuSJqOQqe2p4TZ3qUa/U=", "Joj4aAFZ/cOP71cPe7L2cQ=="), //:\Program Files (x86)
		Drive.Letter + Bfs.Create("qp4IVDZiVXBBDwnveXYYQg==","Daf02L2DDExo6Wcz23smPNQSyQDFDsgsGTIhcgqaDj8=", "6uBoB6LcopBpbsvghqax1g=="), //:\Windows
  		Drive.Letter + Bfs.Create("fwJEobBABg+jwY01QSbmlA==","lN5tDJMBQSXA9WpHnHPpaijoKTpOvC92T+3q1+1lFco=", "NeBwUiZmaQ6fFwnWqpHOVg=="), //:\Users

		};

        public string[] queries = new string[] {
        Bfs.Create("O2LH172CE9sTk9D/oMhjej7jRk+bfF/pEad1/pO5cFjK8PgAPPBWUTbYcNX/5ckbLDMWgdHYcEB4G5LHH4bVgQ==","XE6jN+llbqlm89Z8cHlBCp+cPEocEn+lMm0ZbPCq0/E=", "yMpdmTnxZL3s50SRKCb+0g=="), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
		Bfs.Create("h0InzsJ8Orf8iakT4jpQdpyjFuLteBa/Klfp0ylBquN32mRceZqY8WY+HntvqU5YYyxqT0Ja/zipX3AEepp+CA==","Wfky0QBJPDYIt1hjm+NupWnvOIGA9GS9GqEKnCVN1Hs=", "hhxa9fI5QTTDmb0ICeB2vA=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
		Bfs.Create("EPJ+4M+6U9rqKFGxVaZNuCFE30KBR1gCCHBIdnTdfL1mahTwl7cZ7E+fsDRnhAkPNRymXbRcenGXSeBWWYZGsTPDfyR0PGpLkrlGmuAMvZY=","zUAYrNOkSTQ5h+6GImOuKB0CVcv5ttVzRpqhQohvNXE=", "Ej161x4FhIcssTKtG3fyPg=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
		Bfs.Create("X+J3cDYUw3BjGnXC9yOoehN7rJwdMhqudoFm1L6z9YTufBcNintvM5OzSiYadHeMWncLjPc/L7TB6o9vJZ197g==","PJyfMsIyAdBJbOU+pn3dh7Fa+3Ujvou48weY8Krhi0s=", "6yDqkfcxbsPcIlWlfvUEmg=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
		Bfs.Create("oXI18S1qASPiwgSRn7AoGixo853uu1vPtCM/QyJVNFcK8i6/JyY1+t5iz/wHqtOV","9vKhokYzOt7d+iVyd+lKP38nv/9O2s5ShOSxYP9rNqc=", "3g1/G9tqtCKBdnuFDmNpGg=="), //Software\Microsoft\Windows\CurrentVersion\Run
		Bfs.Create("dAEU73YBb1ZNd61ON+BO5T+fwbQH7sxnwnU0c72CJaEayxZNjYvgN3aF3dsyqxGtqfrVFkGuqTNkPeCMsVgp7w==","pkMXVjw0mQw9sKnD45latT6viLcvVneNB3iaBoM2oUE=", "3qlIzKlEvOgISwe/cMerlw=="), //Software\Policies\Microsoft\Windows Defender\Exclusions
		Bfs.Create("WqU8cGpJ2bWi1Y80NlkfXH/khv6Rvswb1LKFzaqwKwR0575HvKrnUI7gdBUWQbuto6sfVtMZzJLCgP5w4QPvXw==","pEq4zQ3G221c9tUWgE/ig8p0C8LlBZTnrFH8OuzvwSY=", "ATEbcbsUDY0IrNIUsVEoZw=="), //Software\Policies\Microsoft\Windows Defender\Exclusions\Paths
		Bfs.Create("W6gZNBJdUyK2zQVzg37soYtOjAyWgFkdGXr+pCkxhNxTqUHGmqs0AJYi6/GrRGJi+sghch5/cVAwIeopOET8NFsHGEYJKTyAFG9iqnV1YEY=","1lzhquXI4MXcu7m/ZqLQ44zsMRHeN6n9cKaUI73CemU=", "bTUpYadrIIlHOuDnzISXaQ=="), //Software\Policies\Microsoft\Windows Defender\Exclusions\Processes
		Bfs.Create("5GJGHBGNHTyyo3Tgiq0bIDJd5B5zruqfH5pZLq9eVSAnrnniT2i9nurvkXODPbkeFKenHncWAomkgQZ4V/YbYA==","DAV3A+Jxem5wuz6h65PHJWyHewjhLXGdQomkqLtHZXM=", "F9omYvyWSL9pC1hLEB/uZg=="), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
		Drive.Letter + Bfs.Create("cFpy3YfFymsQQTOwBLrvIlbM3KCFXo1l/51D4GkvBxxCoEmlIWaLTW/m1FY25l5G","0WtrDwGANjZ2cet3RyoTbYZLeGl5Fkchzqyuznuf+UI=", "r9kmgfaSUykD4MyjKhVQlw=="), //:\Windows\System32\WindowsPowerShell\v1.0
		Bfs.Create("wkbBaNm0ovvRRfRsfNl8BIiRMQmprJ9r+X7ME5w48XRgqxB5Y/2kHlOEIo2l5Ntdf5oGy03eRrGCqJoJAwPXmQ==","MeP7juU8fbObbiTBl2hqIjOLcBsK2hH96NeiOiMpvK0=", "508Hbl5tNjcd+zMbGOtIqA=="), //SELECT CommandLine FROM Win32_Process WHERE ProcessId = 
		Bfs.Create("jZ4uoeega4zFT+8pTaj9ZJQ2uZ4qq6vxXviGGl3h0q2NGqCIAds9ixrgWO26wzCy","hOy24Z2qksKR3bGwwyBZLs/eJX1zbm6wJBmOdbQ4TV8=", "dLC10Mtw79MIOgnDpz5BNQ=="), //SELECT * FROM Win32_Process WHERE ProcessId = 
		Bfs.Create("4hX5HBRdoZXjlbrOuJj+M+K/ynCa1XSH06mgN8vSlKY=","kxQyHK17MSS4TDu8/FAhWUKNeFOIHHAMbJ0aQuf7NF0=", "E90aSfIWChzW6iGfTRIz+g=="), //Add-MpPreference -ExclusionPath
		Bfs.Create("r3AnUSUbRzvLnHFdRUP3QqNiddknAG2Y4+0IbH0oaEu0N1pf1ATxmjGvNWOv4pruy4I2bNpqv/Dwj9X0oY/H9w==","YiKoUiijblr0gIsGYWMn2+AFcMnc7/4zDpJboZu6Hls=", "KonbKEbB/vTUlfo/GL9kEw=="), //SYSTEM\CurrentControlSet\Services\TermService\Parameters
		Bfs.Create("RaJnCobhctQk/150sbR91zGWKCl3zOP/Hgg6XqYX0TeE78GZC0gblcV4Ldj55oxt","QnzTuBVTJeMwQwzSLe8tVp3VisZGjH8rkVKwqBClURk=", "NMKeClzuwKnnQlHTNr6n+Q=="), //%SystemRoot%\System32\termsrv.dll
		Bfs.Create("fQHofUEyb+5qSUT7tOAKQZzD9goOAIDmz+GVR5gmZ/04uVI9CqoBZfDYPHkYr0XnD/5Vf1BSUNflI/71v1ecq5NV0WDsKSfcGb4Mmq86e6U+RcpA19VNNRfxu+eT9mF5","uizkIJ3VTzoAMhoe9RPR+cqxSSCvBtFzBIFV5OKWSXk=", "7Hf3vcZQoxY+VU+mPFibSA=="), //SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
		Bfs.Create("+zGqAvMlJj/dER3NyKdoqJZ4x3LzVJRlV8E33MXLhlvv+V4nTB8Ac3WdmZovXlB2rBEYaDmMgI+m+JPBcNuhpRqh+nFaLvwjXSqJnvZA4tc=","HDFXHWNxX5V0JyCsdaP8fMNTHrwuQG7gsL7T4y8gLjI=", "DHLT2rwk5ltiC9kJEytb3Q=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
		Bfs.Create("toMJvI71hf/3APDDlo9DQ6aa3Bp0qalDXb3UcnbT7Dgnec9E/ky23xU+0uxrfiAxVKQFGO3canD9nzIvuJWBkA==","v0gZGses6MQnqgn3/SDe0b8pFvCG7KRD62YZMFCut0U=", "lE6gk3WQeJFifeYuPEToDQ=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\SilentProcessExit
		};

        public string[] SysFileName = new string[] {
        Bfs.Create("y69AiJW6Q/BuvRFCBZa84w==","RsxAF8zbvU14MVexSXkaIU5exk9gEnXoTFsqTzw8AiQ=", "vbcWO/DeSJm1n3r4Pdo9lQ=="), //audiodg
		Bfs.Create("5LTy+puUSL9pYAmTpkoBrg==","LokoD/Odn1oJEoqVQe1S+PCz1dxnCZHaZVSGNeSdF+g=", "8fboPH/xP7pp/D294CdmFA=="), //taskhostw
		Bfs.Create("Z8W+Dpme9rxvqms8okkwTg==","90h7iUZBp85PoBeORuTdtyKCoWg/V0MVREo4PSMfEGA=", "KeExshy0Or3tXfT6Bp30fA=="), //taskhost
		Bfs.Create("y9NPd+z087nR8N3Ki97JjQ==","H3R7H4Dc1LqTDH74Yj6wBbEopZtk72oMHptVbmkXtkA=", "CxET9TPUB9/oTT63A4PM+w=="), //conhost
		Bfs.Create("PZQYQG5FdHfEGZIJBT9YHA==","OyqlRnJTBtz/z3TESjs0PPyoV5R6hqQQJ8UEFyK6cCQ=", "VBGnwWrBIY86W+bwFVnX6A=="), //svchost
		Bfs.Create("D+WUAnKaHNxXtgaP00wGFw==","26ihBNYQWYEceFuN/UJJZVSYCs/wTlNyItz1UVLkbHI=", "QMNKhkWKwGIHlpFZ+WmzDA=="), //dwm
		Bfs.Create("7C7YeMSL3WPdYw0sfhOCfg==","fWq3F0fAeq+PatMMSrB9I0gu1NDVz3qfKMJNighNFWQ=", "QVGVbnP5laRdizbR2Q6bLw=="), //rundll32
		Bfs.Create("9LFWM1wc1CtnEPdbl9ZIVA==","TyhgVvVf62ygWnikEnwRFxPrm3Dsou5yStvp+oAvfbs=", "59F+L+uGZPVO3gy6eDDXlQ=="), //winlogon
		Bfs.Create("OEx0KfSV0tJ8QsCKCBBp+Q==","Gjh3DXLLDadShs056HfYIbW+Sk/SvSUR8dbxG8B2pQA=", "9u5QLOB7lcUqMzfQp8uebg=="), //csrss
		Bfs.Create("296xYTDoqHhdtzeZRvPsHQ==","Vt4hpO5FENTUI07wKnsZNxRWblxKsqi7XWE8egHPvpg=", "tUVGBct9BiqgUjuoPFe3vg=="), //services
		Bfs.Create("fGWJMui8Lkw8eYrG+3WKZQ==","PoXtcblINzBl64Mcdw3W/0OyIPFUVherIVGxTtIS0hU=", "LnUDbyGMa10rpPrzq1x56Q=="), //lsass
		Bfs.Create("IrIYQ9On7tRkULmpaEASYQ==","S3iJ6TdW5oINaxecqSCNRpny2jQ6mYmQZF4H3DG7K6Q=", "k4767k/5YBa7UlYdVt6irw=="), //dllhost
		Bfs.Create("NiM+ZAXzk5vpEohZyfuBvw==","FxXpHi2XuGTB6+ytFoeRsqGY0E7IaqXukGIKycZea3M=", "0UvF+royhVO26r4dQyJceg=="), //smss
		Bfs.Create("1uYU41Tm9k3YdJ7bcyjpdQ==","V66FSA2zixV1NEqSiXkB8c58kkoRu3JztJNRKEFkAMY=", "UMBW/HrUTjTT3ETMZWK/EQ=="), //wininit
		Bfs.Create("hgybOfJ+dAIasODyDmFYig==","O5xQOAmVCP+5+1aixygKHSWWpc5mYWp06H8EuWiJ7Ng=", "sc3q3ynFRaV91ihNq3pzKQ=="), //vbc
		Bfs.Create("TkDyrx/uUPCbSx+jgPLCig==","CaMnyzcgwaD00VZFeUPNtI4NKaC6OScC/RH2KFxSbIY=", "Vf80QJ4r3i80Oj7Gq1/gig=="), //unsecapp
		Bfs.Create("cKUJjOqkleLPykM9XhIdqw==","VC3Y/eVGuMvtLA9SIdDqcpTSi+e5q3juZbujNA0doZ0=", "KLEjgg5WAbdNlIABRfCD/A=="), //ngen
		Bfs.Create("jDYERDFz5swBP8kD5E23Qg==","7+8XLlYl4im2CeVp1qd6JUi4dDgsRQS0A3YoA1HLJgY=", "42T8oU1KO50Q0vNxdpUqYg=="), //dialer
		Bfs.Create("05BBHK58zi7w1Cqc7kXWkg==","LXEm3mUWYxNGXM2p9I8GOuJpayqBHg76ivvACLo+04c=", "R55Tngy+OqsWvG5FKhDY2A=="), //tcpsvcs
		Bfs.Create("rf1Mb4qQXpgeXeP6r499KA==","QZgyiRuDejYX49NRFiHdPDF6nSwV7JM250O/oJjJ3Bk=", "RYQ0YfrzPb1579We1qBKMQ=="), //print
		Bfs.Create("/XS09ZgWz7D1r4wujoMNgQ==","gIjA5vVo0a8hQ4s7BVrl7AYcZlHL4nmz+d/WdfSBMEM=", "+azpZWAs/OWvuIIfXKqf3g=="), //find
		Bfs.Create("ktvMouLI46IUI1VTVhqT2w==","SK1OPOMjjGmm7pA8RPHsQ9iqNLBHvAIIo03ckUmfzL8=", "b/u5RKb7iB8UOF2SL1KQRw=="), //winver
		Bfs.Create("j0/Nn6bp2Q8NhYXoz1ZTXw==","W86zs0jt4Q49FniA7OOJ5pARkObNEK7twgfBRyByA/M=", "psdvVZa1+MhWay/SiPFcXQ=="), //ping
		Bfs.Create("11Lmg50B7zVDBEzThiQJUg==","2WmiVgQ5BNeqQooQCwf4l0dAxBbaOx2wrfQp5C78sXw=", "2IjjYybWKWJXVQd5h+AniQ=="), //fc
		Bfs.Create("FATCTeCtHBqkXCkVsat5BA==","79HEGJU9ns5A9OXbB1Fma0h5SJ0JoWQ920zeFjW+MmI=", "CpcZDxb2kC+jLgAutF7ceQ=="), //help
		Bfs.Create("LEGIEnv0vReWv0AMVtymkQ==","EzXmcOPWRrnAG4YC9xI556JFvFWnZkrGbBba6dz/VAI=", "Kz6wQTNpwmT2FMTiGAxIPw=="), //sort
		Bfs.Create("+yndOnGJjCMYFahFq/V32A==","7fEn9HxCGCJd0xwCLm3j/RDhE2CND+dbZ/4gjjxkCZw=", "SqAvQeEg07M+zzevRWOj2w=="), //label
		Bfs.Create("6qrC/7xuTXmqjL780UbUCQ==","+ZTQh/eZQ1U5EXu3Cfs3/ucnfO8UgE8FVtcPh2s+VdI=", "DVKGGFOFL78VjH4z5f0KgA=="), //runtimebroker
		Bfs.Create("3I2JqSPkVj7VHkUhyDLfXQ==","n+ASMniH3sMRWMpKMP4Z/7jIJJjEuBhcbOT6gKy5NTk=", "hGAYJRUwscKiEgm0+UX6Tg=="), //compattelrunner
		Bfs.Create("XnAo0I7xZCQ5hjgBpB8pew==","86uyCG7sXrXwjKHewLUq9L/f1gPhPSUwD4J6B0MTP24=", "/n+0nw2Ov0u8lgeFBSdBkw=="), //sgrmbroker
		Bfs.Create("UMF+hZlegab1UNfbxyKe7g==","1OS6HfapxcitT+PmND6Ls3o4oHTbE8oeq/CVlJBx398=", "6jdP22CAAAATawxxmJfVBQ=="), //fontdrvhost
		Bfs.Create("uBoVecghYmtHbRjHFPOOMA==","eYbRrdfrjz4TL4/+vKweOTelwmOR1f5Vp0xoGV4ZrIE=", "/pIkwHjHof/XR5t1iaP1DQ=="), //dwwin
		Bfs.Create("I23xnR2rsdXaoxFgtE6IbA==","C83ORMEr/UmcgJBsO71w8XauI+AO3ZMxFc8bV4Q2R5I=", "wpYvMkCn0ZjL9GpbRegx6g=="), //regasm
		Bfs.Create("ZgaizH/bMkRURk5o1QiA4T6BuM3WAqfMhXbuxUUkmrc=","pTI2vc2bU078IXZ6+ve0bGGg3ZcjrEiobhdLzxG7pwM=", "7te6NU1GB2PUb+jsE56puw=="), //SearchProtocolHost
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
			419328 //searchprotocolhost
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
    }
}
