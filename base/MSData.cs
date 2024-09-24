using System.Collections.Generic;

namespace DBase
{
    public static class Drive
    {
        public static string Letter { get; set; }
    }

    public class MSData
    {
        public List<HashedString> hStrings = new List<HashedString>() {
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
            new HashedString("f6b793a2352d382772cb7657139b2a37",27), //oca.telemetry.microsoft.com
            new HashedString("ad3d5915ac6f54ce9464a51ef5ae8fb7",37), //oca.telemetry.microsoft.com.nsatc.net
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
            new HashedString("9c9345c05ca20184e8046495224f97a5",27), //sqm.telemetry.microsoft.com
            new HashedString("1ac990351c5efe91882bd5607afcdd1c",19), //stats.microsoft.com
            new HashedString("861cd2c94ae7af5a4534abc999d9169f",13), //stopzilla.com
            new HashedString("90711c695c197049eb736afec84e9ff4",20), //superantispyware.com
            new HashedString("e862d898315ed4b4a49deede1f672fde",13), //surfshark.com
            new HashedString("25da26174f6be2837b64ec23f3db589b",14), //tachyonlab.com
            new HashedString("774f38701dff27e1d5083998b428efd6",11), //tehtris.com
            new HashedString("f39b0c9cd3be259b72d26bc2ca8b1b3b",35), //telecommand.telemetry.microsoft.com
            new HashedString("cbf34a13b567f15597d2f3f9a0b8ee9e",45), //telecommand.telemetry.microsoft.com.nsatc.net
            new HashedString("d58a810afab3591cf1450a8197219cc4",11), //tencent.com
            new HashedString("00d04f179a26f855d01bd52acbf0d0ea",31), //test.stats.update.microsoft.com
            new HashedString("ca867bc71a7ba4529a2d3a9991d54511",9), //tgsoft.it
            new HashedString("64003943175e5f080c849f1744819f48",16), //totaladblock.com
            new HashedString("61cfcb40977412be2ebf5450f4e47d30",11), //totalav.com
            new HashedString("804669ae15f338250ec9e3bd00ef5038",16), //totaldefense.com
            new HashedString("c98e096681a2d1d30b321ca4682adb47",12), //trapmine.com
            new HashedString("1826c35007829d3483ffd18cfcabe01a",11), //trellix.com
            new HashedString("2cf505233a066a02292a1f9062aa12a2",14), //trendmicro.com
            new HashedString("976e17b152cabf43472b3ffd81113c66",13), //trustlook.com
            new HashedString("0d3630958f3c3e8e08486b0d8335aea6",17), //usa.kaspersky.com
            new HashedString("9c41eb8b8cd2c93c2782ead39aa4fb70",9), //vipre.com
            new HashedString("f27e6596102c70bad8aa36e7c9b50340",11), //virscan.org
            new HashedString("17baee242e6527a5f59aa06e26841eae",9), //virus.org
            new HashedString("b6eb1940800729f89307db6162706c21",19), //virusscan.jotti.org
            new HashedString("e2a50e6c79e09a7356e07d0476dfbb9b",14), //virustotal.com
            new HashedString("4098c777fa8b87f90df7492fd361d54d",9), //vmray.com
            new HashedString("97f4c811eec10002f0c31512c46a8343",25), //vortex.data.microsoft.com
            new HashedString("edd9a8bc3fea892c815e156c8f97cd9f",29), //vortex-win.data.microsoft.com
            new HashedString("3ba8af7964d9a010f9f6c60381698ec5",11), //webroot.com
            new HashedString("6c1e4b893bda58da0e9ef2d6d85ac34f",18), //wustat.windows.com
            new HashedString("f360d4a971574eca32732b1f2b55f437",11), //xcitium.com
            new HashedString("686f4ba84015e8950f4aed794934ed11",10), //zillya.com
            new HashedString("2b001a98c1a66626944954ee5522718b",10), //Zillya.com
            new HashedString("80d01ead54a1384e56f5d34c80b33575",13), //zonealarm.com
            new HashedString("b868b32c3ea132d50bd673545e3f3403",18), //zonerantivirus.com
            new HashedString("9a397c822a900606c2eb4b42c353499f",10), //z-oleg.com
	        new HashedString("8d9b6cfb8aa32afdfb3e8a8d8e457b85",10) //Z-oleg.com
        };

        public List<string> whitelistedWords = new List<string>() {
        Bfs.Create("yU2wd2acAptMTac3FuaaOg==","0OyEVNHGxYh6ERyU5zGkKMXLquocQyejyPZekpDwK/g=", "CaYwnP1uUKQyxbhrt6UBLg=="), //ads
		Bfs.Create("6O5soZp06DoKFqc2dDaqsQ==","bba5m2JBzVPMWMJDL40UhT/c+noJgk/h+sZxb9JJOoM=", "++sFiwuGeUaANVEPjjgp1A=="), //msn
		Bfs.Create("uasKMIDwwZa1vcGKEfM0jg==","zcb0/UWrYiPpESktgfOl6QvmmDA+L3xVEdgzkVFDO3M=", "1bqmi+YUE769jO6/A0fHBg=="), //aria
		Bfs.Create("rDNn3EeXAqKZM7WbWI19iQ==","gDlnzro91pVxVeVRQd129skqMOxsaOoE5CP4t8v9sjc=", "5RD27Q2j51j5MQGUHraOCA=="), //blob
		Bfs.Create("OPgF+YCVXnX/M26blg2r3g==","t8rlG1pWz19ajVfVzb5OuN430j3XKupEj/ouHOMKkfw=", "CExcBNhOHDM7ZuSZNYGa5Q=="), //llnw
		Bfs.Create("NyWmBQHGaD5pyowI7KhpBQ==","igSB+uG7HF6DmYfQ1yxNDQ89qBpirw55WYPTM26KmTo=", "BjrMrrtApksTX79ujm9UYA=="), //ipv6
		Bfs.Create("dGVaRoDoVjKIxdbWta1ySw==","6e+robbroI0WBrjhg3UJyXZ8IUeDDktddA2Ircg76XI=", "idIQR92/qDSUc5k/z0w83Q=="), //adnxs
		Bfs.Create("IW3orijkshMc3KylyhShUA==","29iKRxK8eOgXp5/bPDvVuprzww/8BmdUhNEPtmbgUzc=", "iNs2JcXN/fHnstBoTJ5BmA=="), //akadns
		Bfs.Create("lX3ipRWDX7eWctggt2z9WA==","Q92pL9GEXJU3ExZ1gN1pTrLfOuci5gLR4VTGLh4Rd7k=", "e54Kobhy4kU680sEEUteYg=="), //vortex
		Bfs.Create("B+s1QC56PVY71IIDH+Jqzw==","gNfiGlMGHjOBqjgaDOGSAOSr55tbLBs1hEvc1WiwpSU=", "AhzZFH6rMJyGJ7te6638WA=="), //watson
		Bfs.Create("hYqjMwFj9tA7X/nooASYtQ==","m8Z5kmKPmsvTup3dBjzK8jHvCxW5gsfbvjW4h/4gA9s=", "j5jRvo0OMQBaj4Ec5eLJxw=="), //windows
		Bfs.Create("RnUzpPilltuslRUy1ySHtg==","ohB5n+3a6TmbV8By9q5eSZ3QW0q5Ndq8KHzMijUmIgs=", "R5MNMIquEb00dNbEnN/DyQ=="), //telemetry
		};


        public List<string> obfStr1 = new List<string>() {
        Drive.Letter + Bfs.Create("NP/BljSwGdxsVysoi2hrUcI7JyB0Wk5RYIUJkW+ceqo=","zP9GNFpvjlr7SF8Vl9sdSW601X620lb3ab7rv/bWmHw=", "+3F4Ko1vnzA+JqIUSRaM2A=="), //:\ProgramData\Install
		Drive.Letter + Bfs.Create("/waUpzpwCfFg4TXuXoPYj13Z+9TAYJyNSOmOiKrA+gw=","pAUR30EQotwNQdetWDOKNozJCHD25jN9fjlCDqdd2zk=", "+UtW2x87l9N/S4TUYD6QrQ=="), //:\ProgramData\Microsoft\Check
		Drive.Letter + Bfs.Create("LaXP2unDi7ro3rEr4rkHZQB1dOKmMIOIfCFwraanit8=","6oJLQCWeMAWGwlcukbISuLfT+1zaeE7FZcgE5WGZwno=", "AEuEtEx/5KBWM73doW3wyg=="), //:\ProgramData\Microsoft\Intel
		Drive.Letter + Bfs.Create("eu04dRFzXeEQchdLny5U9mft+r/m/QADqyVFMgTmLieSq3TOR4n8+VzJRblCkMwpIf836YDeUQbWXfT+gPOC3w==","09MGhf+AjCE390cvpOvixzJ4MRI+vjvW+oY/zy26L60=", "Grx/D56OJOvBsp6wUvdGzw=="), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
		Drive.Letter + Bfs.Create("1tDYAZZI5GQbCNTvyBT2Rwkpt2zg1R/80Q6QyVAJ/tY=","p3nEDFSFstg7aEYNjPLNlZxJ25reQxXHHTdFbPl8Tl0=", "7MTtSHqH7CLXIyZaarUMtA=="), //:\ProgramData\Microsoft\temp
		Drive.Letter + Bfs.Create("RkVVp/TNdUXsH2JNZ5OaVDAM3mGpyAEFt3SGuPDoKdA=","YaS2t1XkxHXtxD2ZUuYvAFjGiW66hG5ExOOS7Ap3nKk=", "XbWWZJPl9UBpLsmUr78icQ=="), //:\ProgramData\PuzzleMedia
		Drive.Letter + Bfs.Create("qUwQKd30o6YqNoLfz0KhHl/AJ+T/50r6/kNQwhSDviU=","Gf/GFw8XIQ1QslGxGDWLk6bge4cWDzl51vFMBzRRWHU=", "MpZO2ZRY1cs5WkF2YpGErw=="), //:\ProgramData\RealtekHD
		Drive.Letter + Bfs.Create("eHwikQ8pIKWXYeLPhp6+6+d9YWlEpSOaslxFuQtzZJQ=","xD5nK31II4os0mbrn9G1yqwlr5rBKq2WmCZF/Af4Zm0=", "uGCHE2eMXjA02g/5V0C9Rg=="), //:\ProgramData\ReaItekHD
		Drive.Letter + Bfs.Create("+6Mg/dIhcmBvNS2QGPXIauccB6uSTfimigBd2Kh485g=","7RmdmLkHYoWptcuA3w2GWagDB6fFcvDziZAGIXHqaSQ=", "Ndez6t7bgrqF/pW4Zqtmtg=="), //:\ProgramData\RobotDemo
		Drive.Letter + Bfs.Create("2Uezd70ZphkAsRnwx/jz6yqRc/6XbvVAhAtlgOMUQs0=","DWW3BK57VGzfE2iMqdcLlnQ5h/dvPMUL508MIcnVp6Y=", "GUC6hBkoTDPSU9igcFrHhw=="), //:\ProgramData\RunDLL
		Drive.Letter + Bfs.Create("SmymPzMe/BbEQWNGkO+jPvmFv16Gfy/wSQsaWeikVWA=","SBDScFVIb3gpa4BmO1+ymeaSGqT70XOlqpkxveufiQM=", "j0XRZMFiLo+5XLkSym/cUA=="), //:\ProgramData\Setup
		Drive.Letter + Bfs.Create("csgVNS88EXoSuHBoUAX2h2tteTvuKtLx7dcMnaZRnuw=","uhIEcrQcEtPrvvO5TS7kO9IEjnmpMNxrJ9EurD/lXEA=", "CaG/41NBX0aKs/cJ0NoXpw=="), //:\ProgramData\System32
		Drive.Letter + Bfs.Create("KbwEpmBtoUQl2iNcBZz91yjXQpT91us5uKDqzuCD5U8=","lWAfDnU3lkRxZgICI//Ui0QStkSyfV/i8zb9rXcekeg=", "c3NVwdPk0sw+BT8NgcIJ+g=="), //:\ProgramData\WavePad
		Drive.Letter + Bfs.Create("uDWko555Kps3DoYA7oMV4zG0/FoPoCXUmH3tVphYaf2AF/ftY2aOjhPQl8mhtLyD","43NuKogyYxPO377QiF+vY8N8EtKb/rgr+FfNkqfbNV8=", "26/Gk2I9Hx/j37Ka29qqwQ=="), //:\ProgramData\Windows Tasks Service
		Drive.Letter + Bfs.Create("Ysg/bXJNG5UgVa5VmVW0ZBRMk0opfICe/9DsgQOOvGI=","aZwMe65i+7+fUNN96YJ1Q570MTJK/Z7mnH1Ldfi1+GE=", "HknItf+j8zcOEewmpQmL0g=="), //:\ProgramData\WindowsTask
		Drive.Letter + Bfs.Create("2D3sXLjPH5DZnK4QR+DxtVeUy44+fD0ms8ivVoorqcY=","zluvH8RJl8Ky3Z6rGgnGoypJhcby7H2OjrSgdgtE0KM=", "78nifUDfIuRjkcLTEgrxzw=="), //:\ProgramData\Google\Chrome
		Drive.Letter + Bfs.Create("4U0WHv+rYccMCeW2qC7NoWyruePxdA3QJwo1Q0VyqQU=","53KSX6wwSannnOnqXAFD4QL5bHlt3AGpBeAjp71mhVk=", "NZdweyB1JzI/jTrMThLquQ=="), //:\Program Files\Transmission
		Drive.Letter + Bfs.Create("GnFjPBIeSpcEGvazN0wOwyUOTBv+/pjURAq+ZM6Di/0=","CT9MYb4ADiNWG+jUhCNVzbCB4QZBtjnAxy+x2zHEQaA=", "5ZXyI3neYsrFfx/gGsR1/g=="), //:\Program Files\Google\Libs
		Drive.Letter + Bfs.Create("09tsAWg1CLajfYIugV5dWtYHj6+iFSYnUbFJ95Hksj0PJNDRedh2HaD2FL4i1iBX","Sug6S4TewnPEybZvzpI3vQI9J9fxgEAiP31xv6Ljpmg=", "ePiVOft5hktSC47NH7MZ8Q=="), //:\Program Files (x86)\Transmission
		Drive.Letter + Bfs.Create("1+LYmKk6GXI5jP9Ur4XlwUx2yHY8rBeQKlmR43w/ICQ=","UZKPVEtBi9aF8ydVTxzuETIwLADSqcLZyWdn0dSAzVE=", "CUHIBY63Bc//8ppywK5iUQ=="), //:\Windows\Fonts\Mysql
		Drive.Letter + Bfs.Create("jssuq89DpM4BxgWPMnU8aJ0H5oZLDp/SpzmE/ycA7CooL/mfpCWo4tJkizHv3Nqn","+fVudyudvr7kl6K4PXlTM4UTCuvueeEhqEXDln8/yEs=", "c0pk/MSKxZaSKnrGZsJ89g=="), //:\Program Files\Internet Explorer\bin
		Drive.Letter + Bfs.Create("zIIDg+EwQ0PiMe7GbkgcXTIDNHf8cMegkEDoXb/f1XI=","4AlrQO/zPkPnTyA32SF3n8h4az9j7WzHv+NCMwy423M=", "3QIzFOw9bLYCQGiVHNsdJQ=="), //:\ProgramData\princeton-produce
		Drive.Letter + Bfs.Create("i6JwECYS9JnY0hDoqQoZ1qWd0hGTL7JASpJgEHN4vsE=","MXDMDVJH+FqKYdT2J1Xj8mC8ce6AmwP5smQF2vTTA5Q=", "Di5EL0ASlLrv5D2S+nzw5Q=="), //:\ProgramData\Timeupper
		Drive.Letter + Bfs.Create("J/fd8hB9j2aGe1BadCCFSW2hI6mFc02+eMVy+87Pubg=","gYkL0XghQKZZreRuYldx66Nei/WhLLVOkGO5BBPaQME=", "GMn36WNDQAROf24CbFQeGg=="), //:\Program Files\RDP Wrapper
	    @"\\?\" + Drive.Letter + Bfs.Create("dlNhw5J5IuiAtTTI3R8FLdomROpK1+8b/9PG1YDNvpM=","frkHXotWhzz4M3jwj1g+00+bcyVUrOGLPO1fMG0YqgE=", "a6qo7etlSc8UWzZUVGa2ng=="), //:\ProgramData\AUX..
		@"\\?\" + Drive.Letter + Bfs.Create("VBl696ZhnyfPlwpy1TKrxNA01MfO7NOFLkDjXxAz/dY=","h6LPCQ3OzEqHSTvIaRJLU7UEZQuaDiafeTACy5H3ZxA=", "Vx58FSz2wC088MO90xY0WQ=="), //:\ProgramData\NUL..
		Drive.Letter + Bfs.Create("u+vcRwJBIUPaL/vbi150UoqYc8lG/6dvZOWz6tGnQJ8=","Ax+YvaqHeADPKxlWYCyIm1eSTaElaLDdRUgWTzjM2O4=", "B+jWyVBTFqZxPmIh28eXJQ=="), //:\ProgramData\Jedist
		Drive.Letter + Bfs.Create("JBQsd2ldRybWk4te5v+PmqhXHDsSktenuVDqAcSGUcMN5d10dbzscWRFaG4KGEenjGEsz7bLqiqQXpmA27L5fQ==","HwKl+EhXJB+Dkw4l5bc8geGscLKKXNVP+tvTbUMpBQY=", "gOigRjOyClkk757+A1YeuQ=="), //:\ProgramData\Classic.{BB06C0E4-D293-4f75-8A90-CB05B6477EEE}
		Drive.Letter + Bfs.Create("P1fNEgm2Wtp0kO/ZADQYi0fA5lepe8mQziOmoTdnO0nLRmFLW/syrsHNWEzE8JtLL23B/rr+jDyirtXss6IN3Q==","LYS5o/fXYdnj3mBybrOPh07ujfWFz6uQvsn1GiopM9I=", "uQlN7UKRNyymEQdOFvnZqQ=="), //:\ProgramData\Classic.{BB64F8A7-BEE7-4E1A-AB8D-7D8273F7FDB6}
		Drive.Letter + Bfs.Create("ClzQUOsTiBKjr8xuK0/t5bMHpRW1KtCVIOLxjPBmJUM=","CKViQS4OXVy0m7bRM1RTudm4C8ZMZBntlvBeUBNepls=", "tHVcsED3jF/jp+6E+5aYhQ=="), //:\ProgramData\Gedist
		};


        public List<string> obfStr2 = new List<string>() {
        Drive.Letter + Bfs.Create("bosd/47g+9GBB6tD0FDcqzI1KgNNb5UAk4BPTmVyRJ4=","5YpG2/S9U9WlPaEbWzbQCcqym571HIQGRWH5aBn7NXc=", "UiQAMarvB5QFkVJY8OBHdQ=="), //:\ProgramData\Microsoft\win.exe
		Drive.Letter + Bfs.Create("c8T/v3/3lAGix0yOioJoozG7VnBaICm2tb2e5CfDO9y1x3IkVsCKJZf0yNapP7J5","DdB5Xy1UT/RKbfA/PAZ631JirHCVFd5bSnue1JKZ/7k=", "xyg6r1ituyPIHYaoxtE03Q=="), //:\Program Files\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("XQjYh3XHysSUAIctIkffCWQdIaMkIF8I3uxl/FiuufMtYpL2+Wj7wJCCfec+WmZm","CBZgw301bGz3z6/iv5dYuWzFQc7U4YHORrNQkjEOPeI=", "LpmBzW9ByQX2Uo4tnFLvpg=="), //:\ProgramData\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("1C5zvCqPlIAGrBv1Uy8J2lmpddhu4IpyZm05opg9qWQ=","zT5PJbNjwy7BqHVRX6rEG+E2YlWMheWz5AO8jrH7MuQ=", "hpxsIsnH0+s2g7QvkN+VRQ=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("oAHqy2vBx/xRO97ZlOSWuS5pQfZ0KvVz5eEuzYIHWImx98CuxedoBJDuHCtXchi0","81aMONX+g751Hxn0H4YXYt7MVmUt3+iL3KxASmjXva4=", "iIzWYon2ABdTBfG9k+AwPQ=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("U/lerGMp1rAkMihHOQyAZghnA2jhrqca/n35CNc/EfovGKN8Tw3SxlwuF+o9kCGU","/iDAZf28If374ucQS+tNRQ3/EEmqpZkMRHctWoMOn4M=", "N97zPZP1pI+hEjtQbcANKQ=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("OoJ5dLsfZdPz9KuoKr7zibrB6CdOTAZxCKR/4LKhbID2zgdwTO2dTCOkmTYDEdKP","V5jCsju7u57GM8e31fJCkzbxBpOCD6La2SAQxXI7Fhw=", "zVTTih17Wj7eU7ntGbH+gA=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("WCpl+LqfFMdadcgpGaaRRyME4cY78ArGqbBx4iUjM6WBirPmCf1PvQpIguUbuiNZ","xBLPlOlOCjlp8GN6Hjeg85PJ3H3pN42LFo8An2xbdpI=", "thwvJFhaNMa61JzwkxURWw=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("10OeSbxlclDeZBm4FpaHXOnihoSuSksYzhFeK7C8+N+M0MPGA27WyVgnY0bswDgq","mZ9YNstS5MmneV5dDbhl6ihBhPSsXB37bP6o59ss1oY=", "M6xdHNecX/unSPRWwep+5A=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("dhxCYphwqlGuej4D5LD9NasmvTvR43qJ3B2Or1AREbr5MWNYBwhXayH9mr+CRYBP","2FoXO93nuJl+AyJHwwTmv17/qB+rOXswy+EOsZgqTlA=", "adaV2b7wB56wG5BEpLzzsw=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("JJFQapCdarxqarYAGmLfyBS7Fyg9WcuIu5j7XzaIiam/Cy7rSE29XHjfyMH7dNyG","37u/b1zc1qpDl2atFPp0GFPQsoWEWFQ8oa1jEaCDyrQ=", "FtM41VFClmF3POsu3Wp/8w=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("n6JZ2LU8hoP7T/ZJhrtCgMCMzotQKngtiWMrMQ0izLm9vvyCwhUA1ZJEb24ycLuu","U3kp2lEM1ZXfCzhY8ozBuT1ielSkAQQASXBpcEYvbnI=", "hvYqLXnZ+XqRuJuuyGTLWg=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("4UjZsd850IRaf8b7HRpmk649p+uUu5hchxBCtKykHIky/Ek6o5hFY3X3kSAxH6l4","AVnBPVucRBvLzU8CBVeAaOIje3AAEISCiXnvuk7r1NY=", "EW5743Su7XJ3SCcSW2JGHA=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("VDKGrik18NpHf9+bLnOCMu0IKIrW1Bll2OySTDMCPXU1pjLmz+Mb6nR1DUZYsV54","3SduvrwWLE0M0MeaEbbTWVnvoHRN31d6Rg72s68ZGvs=", "biMhLuX0GRwbf08FTUa3pw=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("+wEgkf6goB6x+mq/h8CgM7Gw8bMUFsD5Jzvvfmfp5xc=","KcQmDv7005DWzWkZJ9prmVLSdWh6lUy39XYg+uoB0RA=", "ojtwILnKsZ/ikYoea4pL3A=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("Z/EtdXuk/93ZjWiNbkeOmGWJDvstyZ60P/jYTpUijM0=","7bGyhmiXt4WmpghIET8zdpXUsE4gSDWUV//wMTFpfSQ=", "+ZuKy75qwRZ4Z3cmGFs6og=="), //:\Windows\SysWOW64\mobsync.dll
		Drive.Letter + Bfs.Create("IfV6nrzHSVWupSdVhX8qQ/6Xjw4MTxiR54iGZCUaizE=","KX+NyaizlBcWLoJS8UvYejDi9jJiWsOHWYdpkm09eGA=", "LLNiszKki9mUb92ssWOLBA=="), //:\Windows\SysWOW64\evntagnt.dll
        Drive.Letter + Bfs.Create("X0Dsfle0qY4VBRkEyh6GQmlQJY1fa8t2qGD5PrZpnj0=","LBzuhajv1KVKjCIjt8lOJULDEwcquvHczkprRf1Yiek=", "kFvnR/K1GXKrP6bjNwCU+g=="), //:\Windows\SysWOW64\wizchain.dll
        Drive.Letter + Bfs.Create("VdpZv0HCBkBYQWMKNVvgTIWWLtHWBh6RBmorc1mAtFg=","Xcmxw29apv6VnI5d3uKmtdAoHKi6iKYt321R3cGEnNI=", "oTj6dozfjKkWAsFRIAYxLA=="), //:\Windows\System32\wizchain.dll
        Drive.Letter + Bfs.Create("BDrn4lQyW3BRRqj/+rhlTkhFpWZnCTLpYJyHnxwNJdwj6KoHn9BxXAOO9G8SgxpJ","PxEiVDDv6XAN34JEg6AYwGxmWL3Z5xcosztcBxYD8Zc=", "r8DjW6G6kUS+uBtpJmsJRw=="), //:\ProgramData\Timeupper\HVPIO.exe
		};

        public List<string> obfStr3 = new List<string>() {
        Drive.Letter + Bfs.Create("FaU5UbdJ/w3EpGNytMH9xn2VjZO+s/4sL/rffUM7xMw=","PysafIPZKtMHjTIV1chFKHFz1hY5Ex9o4mUcuUuBTSQ=", "wecfTBCDD8ObTWvJmh1ZRw=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("ksCIfPD5sieuDc3v90Honw==","3N9HuMU4U3D358IH7kAH29Nnblp6RMshAe+dIGT6RDg=", "nFDxok31Zw6h7yo6IP3ejA=="), //:\ProgramData
		Drive.Letter + Bfs.Create("tqtWBeqh7FFLrt9h81dISJzhOZXBJ3d7dJAi4GGhofxTGtsku+mQN3eunhHdywK7","9eN94I7+dsGeM+AbkVjy08NCGnJotoUnJpCCcSCgaMs=", "YuVoRu2vt0ykk5gmSCcVmg=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("hjW01Ak5agr1QVBFaJjm5uE3uKEwKScFdx4dM6sPxcCg6JQnoY2HLwi5j4YS9X9j","MV6yjv8sin6OUDBw/I0NT5Aka5W5KyrsW2BzHmVj4Gw=", "sX9rgkk6fPNlIt4+rMjNJA=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("x3q7wuBg0TZKKfDpmGMRnyFaMzQt/3fRDMJY/x74qHEjylwloTVwepR161IwypPx","OZpuJBG9p+EzDsa9tFcr/hpV5t5fUZVNgPmKiZ8+zRQ=", "7dGydODemZSNrkch42mntQ=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("enN/PUaU1fcdNcdjIqPL9fx64i8smHFXAhL843tqgfXMZtieWNROVTL4ikxkiw/t","4nHiOyRrM1eHCaEMGA+EISszDaFkCbnZPWMKZVwQ7x4=", "s6FhrnaOwcqOxTEH2VX2lg=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("v0ZkxA9TLOrghrI5X2mw/dz+kbC1WESv86UR1wlCoK7qARnwebKPMM7IVXwfOaFW","UjOiKZP1bWIX73Ju7IU1DFhJ3SOvnjyLWilKwgqfwPQ=", "TjpPNLZXSg6/TjR/VazBJA=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("Y2avUCAtaNJ5jBNDPCwN5KU4cJ8QR0QUpYPc9OkQmVa9BlhHgvXnSRINlGE3CNSn","daMEJpSlGpSiHUE+eWE8hKJEQkLIFdCRl/DZ2WfOAEM=", "3JwzvjLfJdTiUYGc4Q0vZg=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("MzkftewqetAvJQ650rTnJLyyWvjcSEHBmReyWIl65LFGdrSqi8pOZpwDbyt4M/ZM","zuFoqe1LUCIBsozDXjQsAIw3N0m0WQyfz8ilt9jVVSI=", "qaWoJbiZjQebHhc53voIoQ=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("n5puC1FEXlSQIjKOhiJruqUZ11KvJE6wugAZUBm8POlsaFWHaktQFKhbp4nNzYma","H9AyxmG+qkHcBAliI7kbpCK3ty0J0VjI2YP/IV4qurc=", "syZeDdWQb0D6Kwsi0fVojw=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("lhcd01HEchitU3N7rLdYCbTY7wl8/Q0VYVB3oWCA4Vpu09/nYWq8sTCEbgE43hz6","Wqmzb2vpX8bqaeaJJpxmsVGTwyNQseTTuK1hT2l5jiI=", "4x9L7i6bqxvnKzehhgaaag=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("f8Ks2UIq+lOKT0l6DwndXxlKIahXmA8MlKcp31cYJeFv2Bs7A0KBgx8fTRLGsTtq","fVnRhCw/vnd+awWo1lqR9Ia2Gq8OBEOWhWJheEQFKkA=", "T4L0fYObQuZExCHDP9hdpQ=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("ZbOwhX7e4J15R2GeQ7oThArnuG5p9XGaeSZz12nfYDA=","DUjiGYfsMrR90jbfsmH7UdDbN+/h4SnxWuVTFZv931Y=", "gD8uQGCdk6aPB2gppahhRg=="), //:\Windows\System32
		Drive.Letter + Bfs.Create("9zGKsR/jVCDifPLt4l7bpBKURIvh948MCbpnryPnCKE=","Ym0ZXs56JrpVQA8Oxk58gn+mv5hvmegwAwnq977FQq4=", "coLDApyp7hnykMTxry64eQ=="), //:\Windows\SysWOW64\unsecapp.exe
		};

        public List<string> obfStr4 = new List<string>() {
        Drive.Letter + Bfs.Create("2rsukaeXsDZyVR9F00piiLg0NPoaA/3ADfzrDVI5xCs=","siNnURNhHsiPV03abD5XAUkUJldYHKVd5a/uG2MIQms=", "W7/ivcMXchVNr7W6Goheyg=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("fhdzJfUsVzctD0asb3fwDzz6B/VGIuJY7tH2xymMHn4cooBnPHMCqVYRwbIdxVCc","NZmVGbzJEq7+OMi4HENBWWqzGYCpFm9KJXIpH+T838s=", "mHzQLG91cFJQ/CIzXk8IVg=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("sJdQbtuNwri5UqfYtq48LZfrNncsIv6hwDm0VSxEqcRFSXHffiAdyM24dv8Qwz6f","CrP6+NuZaew6+s02wNbzS/cS1A1WIlSKMrmoFC9sl48=", "h4pxQU+Gjqaw8Q+2JZ1MVg=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("EaxZ0kdJ6RUY61fqeVBbP97X9x8chtcPZXJQsOkAlclO79vmwniu3MkKxooHcOL6","bJXWWglJoN7cNd5tY2uHr6OL7Jevb55+AHQ9ZH7TB7E=", "uveLw+gTv1/NWVUCmq8wmQ=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("g3kjHOFHq0BHL9LOOJCPdXmGVRSc6sQqdrW2rseDzfdubZTcxX22kZ9CaCp4CDoi","oeBzBLx0FGTtkH0hudFuwlWVZUf8CQYZY13eBSoFS2I=", "HF6qbVsZM8C0kQ+zFtv/HA=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("tg/PQZd+Vb23uCvQFssPH6vnzHofi2+/fPTnLtqfyJP88l46fAmmqxpKfeVo2NRn","jYAMN+l2frwro+j8m5vFhjvZsruqOiYLddLF/6TK+5Q=", "CFo9L0A1hZgQ2BXHfZEdGg=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("nL0iv6TKzbzMTqhaofisLQp/hoFbWL/tVEHJVHG+cXh3V3EZ7ynDVJPcReA0EiYq","270Pz29ANaLuLmPWg1xQOKgALciQG/wbGAGdwQcuBSw=", "fb1pY+sAcByOgPu7GLUzzw=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("c2MAC4I4/e17QWZaY9kmxVrjP0QpRVHQrvREHG802V1CA7jSsr3rAQ1b6kLjHiJn","KEDIV/n6O5Vf09dQu5uZyq4g3mMsCPp6u4oIb6jHQVI=", "x7Zs8n5FGaf1l0GvMqB+kQ=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("hd8dhHPWc4TxPK3qdR3WS1zi8dds+oqy5XMYaneHBCHvfqlDEKwjX9oLp3O5SyuX","UqVoI64hwJNEUEOgnsUMClXHpVckoNRM9f9emP0xJhk=", "WRHItVzr4DTFest5JCoXOQ=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("zPfgZTbAOsHh0sySsAIptOwXR2fFRNVc96fK7JCI9FSTD4NTR9Y7Iz42F1CmJIRU","wEOZCyvgjBQkR3l9yg/dXl9/swteyC0jJvQUPPChyxI=", "Sdn003lkcGqJcv31A4FiyQ=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("VOMnToHTvk7alRr1gqP2yRllDsoTuR6WhXTCcsq4zOfrlcUp//DtNUJcZBp6amS4","3gLivAXb5gjQktr49F5cccBdTzccVCnQGpvWNHEfr6w=", "sYa/ZXC72JcgXBPacJieVA=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("aLAfgJfVqMt4Jslf0RRfb1Fta0g592fLdCaebWAaSsA=","q3VNxyGLoPf2Lponbs1ISWNfHto5O+LN79Smm9v2hA4=", "5RUL/I41sgwdja8joFmg+A=="), //:\Windows\SysWOW64\unsecapp.exe
		};

        public List<string> obfStr5 = new List<string>() {
        Drive.Letter + Bfs.Create("/7QOM3Gqt4/xEAeWaWu5TJJtKEdOSY4ApS6Tpp5fzG8=","lLdLA2yf2qE8bPURFbOJxDI56L0IUYzyOK5ef6lrV18=", "DHgq4YvcwsDBpSupPDKNZQ=="), //:\ProgramData\360safe
		Drive.Letter + Bfs.Create("U+bkj2nQOxmFQozNUlM3HaVMadod2cO2i+RZb1qDmLY=","3r+XRW3kuiAS63EZ6VABHi88iwA7Ia0xRLkvtVzX5wo=", "Xtr5aTeiYgRhazOzblxu6A=="), //:\ProgramData\AVAST Software
		Drive.Letter + Bfs.Create("nt9rkMSYFivSS/IU7X0Uhf76d0b7MelParRX86k38/I=","52+zVcnxOOZt8aCM8k8xzqqUtRNPh98RQlQAJ8iRA7A=", "yPLFEKBqCot5n7FiZ3svRw=="), //:\ProgramData\Avira
		Drive.Letter + Bfs.Create("jvFHza55pNSxaWJRQ2wXDVlhGj/Ma+SCyKxJHrPgrgE=","6gYdQRgeXL5QDznKlCg1IC0R/j17MKW/eGUdVjk/Q3w=", "8NTD7lisgysvKwz1lQjNIw=="), //:\ProgramData\BookManager
		Drive.Letter + Bfs.Create("17XIqUnyYgvmoMT7LhMRI4t3g1tr6top1NY2/418SCw=","r4iY2QFK4DH54yGl4hYKI4xaOFsl5Hrn6yZgsA30Id4=", "/y/77Auc86XouPjY17kQNQ=="), //:\ProgramData\Doctor Web
		Drive.Letter + Bfs.Create("S5HwwNP3LiMm3Zy/WHKi2sdtgtNAii46c21/TL7Jfa8=","ZMBEfvYReJXouG5Uw5OJzQqA3mHIA/ns60ioarZFMRk=", "n6srDNlmq/cxq2KD6Ay2sw=="), //:\ProgramData\ESET
		Drive.Letter + Bfs.Create("OOcUvUhUey2Owz4JaRJAVgjguQLCvW1Kq69VUhZBwV0=","mYhfRsi1MhNd6pb3Euri9GfHUyI9vqJtRh5xbKCWXa4=", "1of+wVSpWt+qTWyP6uBYBQ=="), //:\ProgramData\Evernote
		Drive.Letter + Bfs.Create("U8tUL1VmZSYCyX6iL/Sz7cme5OHmAq8PiS9bxROhMvk=","+rbYklPXCgUH3RjwQe5qLnA9Ksw4qaxgwgrfgQjnlyc=", "Xzn6osKc0QE9+89r7+MKcA=="), //:\ProgramData\FingerPrint
		Drive.Letter + Bfs.Create("4X2Ea6oNqtEKDbonQRH91KutW25hl+AsjrLhn20OtKU=","JPujyZJojdn1FfdfKDoyAy4mllV7bxpml3VQbEE0B7U=", "eX1Umeq944JLRriuF/oytw=="), //:\ProgramData\Kaspersky Lab
		Drive.Letter + Bfs.Create("Im1c0Hpl2TUm6jRqanD4a0Xcfm0OJcl+p3lQ3lDUWhTK+xeJn51x0JudPFwiV50T","254WhGIyBNb0cypjwQ3HkcC0b+wtBDf/S4wsy2DpH/w=", "RRd6gDGhqRP0aVUjLEAjyw=="), //:\ProgramData\Kaspersky Lab Setup Files
		Drive.Letter + Bfs.Create("OK3f2PvRjmVcQ/cgdcZDlvh/sOkcwcXW18nYV58d2XM=","ixfmdMWUt4gos0U7E4/Pdtfd5rCfARyBoR7nwFLSreQ=", "AqUhomdGm/mAhi/WfOqa0w=="), //:\ProgramData\MB3Install
		Drive.Letter + Bfs.Create("Lh8EDSf6YDyK9bwIOjO1GyhXDxXUqJRNydeyx04zNVs=","AVlvNoIOmzWmMf+oST2EF6sm8ZT+NHiwCJzOuGJrM1M=", "G1ZmQBT+1EE54vJw3HBMag=="), //:\ProgramData\Malwarebytes
		Drive.Letter + Bfs.Create("6wmYnFAnM3hmw2bZ1gzLaiXejXevwNO/i/dgBA6UMnM=","Ql+Zm2S1LEjBfE9ajn80cMj3IK5WrH7s7/P+TOVTh3M=", "kqKtmFwduV2VdWW0O6s9Rw=="), //:\ProgramData\McAfee
		Drive.Letter + Bfs.Create("VcmNH0LoTvtvs2QqQx4j+kTavdweIERlSB+lI71CiF8=","0v+Vu9lN+ykhU4nMasBrxLN1+7CSeIbVvRNHU4P6BIs=", "dKAXHq59lxhgOng/Ghsh+w=="), //:\ProgramData\Norton
		Drive.Letter + Bfs.Create("qLwoXBNsJUlINIjqqBY+GjgWxTPI4gv4IwUFkdJeHr4=","tBjX+FoZ7dRBpRiYiCL9bfgB8lNan6nO33ws72ORRTw=", "atfJ1f/7C0fz8iJ/jIpjFQ=="), //:\ProgramData\grizzly
		Drive.Letter + Bfs.Create("rhAlfwQxp6q4fefEPyT3Z8MxwDb2fOIO7vy0cg1mVm8ANEQD91u7nAfnEG0xECJN","5DmPQOzblRfThZHfPTHoQ351zctXszZBz4Elu4e4F6I=", "xhdxyy0icSwjV37cpZM1Vw=="), //:\Program Files (x86)\Microsoft JDX
		Drive.Letter + Bfs.Create("zsejVOkwyT6JT8kFdA6IztFgTr+y/0KN3Da7/Jm6BSk=","qQdD1PngKyBqBC6ZBizSNIJXq/H4fnAXIQUw97WDREc=", "A1mtcIXVL0sydOipTd0XGg=="), //:\Program Files (x86)\360
		Drive.Letter + Bfs.Create("ZQFyS5uZ+Up7IAf2cSzcnPmEz9kQZKnX/G9oeEYIxCA=","HlVAShLLQVheUDtWEaocyIlMJi7WXCXsKB23eafJ1mM=", "BYpkUwHVCMKhdZhwciCUzg=="), //:\Program Files (x86)\SpyHunter
		Drive.Letter + Bfs.Create("DUe/hxWEIMqi1Djjg0YEk4phJi3llS+CGHFJwXXiGgp1K4h1WwiJPIVogci0GU47","3c1ZZdMIPbBCzbg0msTJW+oQ8mZzxkyW9LM9I/Colow=", "MEDwxA9F+Dd/gxcpTtYP2Q=="), //:\Program Files (x86)\AVAST Software
		Drive.Letter + Bfs.Create("0ZViFHSXICp5nyBhnuy2lOKzSV8beeeCicZIY0z6laM=","nVS3rpE8jdocBy5HKsUsP/IIHl5DKhnJsIDYgnLSrbs=", "v6HJVWjgCwMmX4eUCssggg=="), //:\Program Files (x86)\AVG
		Drive.Letter + Bfs.Create("Fcb9WlIsqfkILo83EgYEbUkrX/R1mA1xC1HqwKNxanjQJACpoYcekkTQoXz91Lq6","L9+nSYKBotwO9rI00+jvgmpTQ4T2rgPfqz/jBqXfy24=", "u4Zfimg8RoKjIkXxrAJKdQ=="), //:\Program Files (x86)\Kaspersky Lab
		Drive.Letter + Bfs.Create("ehioLioXW1xUQJbkYC13e+W7RYXiEy65ig+Kkd6KE00=","zYbEFWg6YXjVCcgbWa86UjdSKZHxj/yGW3uhHlhuTeo=", "NVtk3FmpWWirf0jAzax30w=="), //:\Program Files (x86)\Cezurity
		Drive.Letter + Bfs.Create("VMgRjSYDct2SUCMvxZHTO+HT7T3QNeIX0v6cBZ/k8JfWOuBpCA3Ad9k6VDa9TZKM","llYZ6qx0DO72C3vqKjRJj2Cq43Aedr2WcGlxJ//itL4=", "jbzjzFbj9y24fTc2UrmlwQ=="), //:\Program Files (x86)\GRIZZLY Antivirus
		Drive.Letter + Bfs.Create("/GzzQRLYS1usMoTeHZddkyMnrHTHlc3+tUYod/WOXfTgFD4ftZjzEHCgqd/WjEXx","DFmbVzt0CTLP0AhsrBGG6XCW2HVowxb4zzG7kzsPlJA=", "BEozmLWWx2I9zZJ4eb3jqw=="), //:\Program Files (x86)\Panda Security
		Drive.Letter + Bfs.Create("83NO7/X8YjcefDP1wvpktnP3b8Lu3JH8BEAD9ha35sV0T72S++MJSJD8AO2jNs/2","xV00DyU6OtpAltlbyNEorDgFQwWRN6/7pTyVMjNoh4o=", "Z0AsxleVKqiRnw0KNlLn0A=="), //:\Program Files (x86)\IObit\Advanced SystemCare
		Drive.Letter + Bfs.Create("jeZywYahy8eIKbtzv8OF+sfV8WK2zgqLXUo7zHfvEM93miI0FN0ZRB1gtvtc3gbnYlS63UjAg6Bjbi9oCxVL6Q==","ESTa0JY8rUhsDwco9WJpGAQ910+OCqWsPthHWlCGayI=", "Xa5QevBxlea8j5lrGhkA0Q=="), //:\Program Files (x86)\IObit\IObit Malware Fighter
		Drive.Letter + Bfs.Create("wcmv8NeJD1LopUTo0WB8QkvO7AWFDgRRq2VipBoj9VY=","/98fRoNXdCxlIdGFmoMSdyKwCPLuTzxJaGKnL+g6C5o=", "UrHqaoxBDrdavDwqzVKWug=="), //:\Program Files (x86)\IObit
		Drive.Letter + Bfs.Create("91DBI6CS3k0IXO+oex/iBfoPzi2ZhxgM+pJ09zi7X0o=","2lblL552g6wgJ2+YvXCs5jJ52DhhUKWsxzzXJ6epy9c=", "+MFGcj7ZNTUp3uWMu47Cog=="), //:\Program Files (x86)\Moo0
		Drive.Letter + Bfs.Create("mKh0RSzGQCXBwT/Wmi7HQ+wG9zWkvgZa/Qoc+PaCJpGD7iC20bPvr2z3FNztNK/N","IHjrR5xhfo2lheiNTGhk5TjKd3VCYOy7Iuf0cQUhEfM=", "vO5/lElOvMdsBJ4+CDIcow=="), //:\Program Files (x86)\MSI\MSI Center
		Drive.Letter + Bfs.Create("713zLMPe+/HNedfaKLsyzkliGIf+HItpRMKiWeelsWw=","PFfPt1Z9dWStTeBSts7/gE/9aCknPj0KjVDq7efPM4s=", "XU8eg99jgcvBZZT1+YZGcg=="), //:\Program Files (x86)\SpeedFan
		Drive.Letter + Bfs.Create("9B0bbiqS2hQwsCocfxD9W67xzaPQr0mN9VLi++crfMY=","bX/7eidYJFJeG7i/btu9x+vo0XHehEjzAktWemdS1Oo=", "YiJ0oFsEVUYlmym+kpvdPQ=="), //:\Program Files (x86)\GPU Temp
		Drive.Letter + Bfs.Create("Mjp2y4UecczBUVW0fVfs4fAO7dM7rrDGKbx+tXm4K8c=","emd92dfHH32zCA2Qif9AZ3rAktNIKmhJ6bZ/EGaAGmY=", "5g4fDJx6xvGNgdqDQjM3DA=="), //:\Program Files (x86)\Wise
		Drive.Letter + Bfs.Create("fNtcUkHXjNbobCLuj8m56Ga0/hdXfxzIH4z5bnuPV90=","TNUjYaKRrKPBzZsZ32Plqeh8n7XRp08wDbKRjTbkxUM=", "bSQX/Bcn3jYrTxbOghoK1g=="), //:\Program Files\AVAST Software
		Drive.Letter + Bfs.Create("sLz5G/4UJG0bpCgDYJMxsLwbjTsoMRYRNEV0oDbsop0=","6I5NNpmn4cQ0AyrxN8NtFteCx9TjRysybDP2/wnKyzM=", "bOukNnPSmYZ8D1qzFsQjIg=="), //:\Program Files\CPUID\HWMonitor
		Drive.Letter + Bfs.Create("y+RYJH2+NDsOHoAOHNS0phrS6gg8eOdhYHxxwOpNN0s=","PN6f/TxeRn6QRmM1cdJWyC5yF/sfaVEEGooKjJs2uPo=", "DNseqlIUliS1TK5EwtfltQ=="), //:\Program Files\AVG
		Drive.Letter + Bfs.Create("l1pRet3Rz5XXMNoObULuEQKln2CfOHJtAJMGfnEuorUshQJ/AVI2xXiX8fmf8E8L","TLYerWkJwUnXSB7fdHsBINQPXSWZ0hf4HTRvqEb54H4=", "KdUmG3l0nFJwXGe/0IZfGw=="), //:\Program Files\Bitdefender Agent
		Drive.Letter + Bfs.Create("qNJSH0lXmpXvH76zRKBk5fhkg1YwvQXxlqpA3aJ3Gp0=","VinKpYAfCBrbS6vO+dD2UvafV3JC68A7Ogtz7SAv+mg=", "Xh/tcj5f1/X0k8ArlXuLqA=="), //:\Program Files\ByteFence
		Drive.Letter + Bfs.Create("00N7Q+cOptcJIpJ9YHZKu1IzrM+HrLk7da5RUAhE0sM=","N8lsGP1EiCC3no8x9hRqg9EFCZznBtkM3Nk5bbnymz8=", "2JEEKPMsHHg4D/ug0iZGsQ=="), //:\Program Files\COMODO
		Drive.Letter + Bfs.Create("zNTTNg+A3TQT9KRF9FgHNHhem7fucs4aJlj2NKHqIDc=","vhedIeo4CD0urpBqOyPg/WfUl0w/3PvbTi2/L6+0vJI=", "lJ5WCH81xVsiZ8G6OnTH/Q=="), //:\Program Files\Cezurity
		Drive.Letter + Bfs.Create("yZTFmZlc1+tpx+MjX9fyULn4s9RL8r5KXmEC1fQlXIU=","qt0XOxYnydpm1sxWpDw76ZEKK1zsYhYuMvI+a0eLGV4=", "h+S8mJ0JLyd3MQVNUbcbdQ=="), //:\Program Files\Common Files\AV
		Drive.Letter + Bfs.Create("8r61/hx/G1ZkG9KEIxBQK5VS+GUxAJpQ1IUqHA+W8PbWbOq1Odmb4UrnjWR5R3OF","wtLUFZRgKzaafpaR5H312srxZsk4EimQaxkbqGTictM=", "zb0wmOR8W82qgI+zNgws4Q=="), //:\Program Files\Common Files\Doctor Web
		Drive.Letter + Bfs.Create("dgP48W75HXyCc02+Dcm2FzgNS5CpD5jbI/i+ahvtxmLW6tWR2aUJTUe5/aPtDWjm","eNngrB1NnGTUMyHGU49H7720kOrPcbsI1HHAdHK0YBA=", "4vUGwN4WWYB13hkWm8/ctA=="), //:\Program Files\Common Files\McAfee
		Drive.Letter + Bfs.Create("pX2Uxo8ipoQadXzc8F92E9R4wo+Xl3FvTedu5RHfA+U=","zDiS30lSws85uz5H2NjhOk/NtyjyOKPPiQDX3ucKKHc=", "XvkA25KwaKZdo1TLiqP7uw=="), //:\Program Files\DrWeb
		Drive.Letter + Bfs.Create("W02Hg7ahahhEKtQ/ip5fvqaSRHNuGC/RM22N5culXZc=","s+mFe2yMGhl7o2r3RH3/RgDQUkja8QshzH8w0iOZnSs=", "kmjN/8NDkRDYoYfVeWKRWA=="), //:\Program Files\ESET
		Drive.Letter + Bfs.Create("eBcyZe6UBLEZtUB8ilEoAxUIQcJZDiIadKq+vXAhjACYlhumrkFYsGhRRtiO29vs","UZBZToprhT3PiULDMtf8eAqW8o4Lsl3JNQwFmitzOrI=", "eJFEgL4TwrOYWmL2vB+CUg=="), //:\Program Files\Enigma Software Group
		Drive.Letter + Bfs.Create("zRVAg7QwkH1/k9sgRh1+4mBhYryN5Jc/n5wDfUj+zyo=","TextN8HtyUBNYAWFa6z4C+OAPH0neXwUpcI/5cile7A=", "XQEqyHbm15fjHYaUbYhAQQ=="), //:\Program Files\EnigmaSoft
		Drive.Letter + Bfs.Create("uFIjNUWw8LXgdbAuOv84oIAOKyWvP67+xm5NAlPxwXM=","rQ4ic8YiWg9atSLHYZLb62kUavaoCaGqxQHlMZAlEWM=", "dIl1h5MGwFX8qkDqu7nB+g=="), //:\Program Files\Kaspersky Lab
		Drive.Letter + Bfs.Create("j93Q0x6VquFQPi2rm0FAkPJCGvWT5wHcafBLLwnl6/i1DEFbRJnHyVUCT9kvhON3","Wwcp6GsabQbR7JOKqZEIfjh/+CDYE7HZQCYevdOaY68=", "qxjQybm77+rJXHHW0oi+fA=="), //:\Program Files\Loaris Trojan Remover
		Drive.Letter + Bfs.Create("haJp1edxxPuDXyCheCFr8wgS50ucMuijTIwZ12yrLKI=","yIwHq+rrP5SbRG0YY4/+QBrtiZkP3uB2JLoT4syDjF4=", "Hy6zY/tbnk/vUYDkpFbfMA=="), //:\Program Files\Malwarebytes
		Drive.Letter + Bfs.Create("hPcAApG3EGS78NoY2ZMRaSflQdVKUblK1xkQe0/gwqE=","EIHmwXSKWPl5WCCqwPSCywQt3Mf4LVaK9VuUXQZcejc=", "ztDco3ZyCTeEl5oyDCTX3A=="), //:\Program Files\Process Lasso
		Drive.Letter + Bfs.Create("CC609W5jbuZkg4sFZ1ZYafJzX6v2Y3J62C2uBOlfZ+k=","He6EJjLa+G8zcwVr4SO2MuOJVCN5KWFeP8bRsB95/1E=", "KAbt6NKvY3P9fkRh2VR15g=="), //:\Program Files\Rainmeter
		Drive.Letter + Bfs.Create("CcwHOoRenPwIy88Ii68ckm6BAcq/62Vu/cDcRgnUZ6M=","LC07tlMDyPtjzXsSvIp4Gk6J/Nu/LLjZpIWljBFWUN0=", "up1aEG/ZXwz/jkRhWv7meg=="), //:\Program Files\Ravantivirus
		Drive.Letter + Bfs.Create("BNYL61/yJ1mkN+knN4yGUHtvqoXClGMJ+rZKAYv8NoA=","m9luNSE4Lma/t9+zgaqOu/CQNgMBL5Qi4bWz9rVCzqA=", "3AZeTUeYXSQ5/WJrMcnIEw=="), //:\Program Files\SpyHunter
		Drive.Letter + Bfs.Create("J+nmd1wW8XbEYMF4RaYAtDOXoauf4tf2kYaQKCr31qKVYKXUj2Z1Rt+i1DP8alsO","AKgBx0SpCyHu7DUpF2dbgUC3Uhs4tXUwStHiMCIK/o0=", "sPxNoTK+vSCqWOdoZ6EEJA=="), //:\Program Files\Process Hacker 2
		Drive.Letter + Bfs.Create("44F3XIoTYT1v660flT1yIdf8CzbqCU8LjxY2xHJqE24=","Y6lDFlXwYOBIV67BXpSX+xpepblbrg8AS1DxIkpKekg=", "+WgYga43Esff1GkZMlSAiA=="), //:\Program Files\RogueKiller
		Drive.Letter + Bfs.Create("QPP3p+phH4CdnJUEH23oV7CCd1Rvt2faVeVpg9W92AHEfPLVwC2ayq0Cyly8Imas","pjr6XcvoHuQAH2mVi5cvDamzHryUTHZvFi+uTWYpWa0=", "TsZZIufcEpB/Dy00WkNiZA=="), //:\Program Files\SUPERAntiSpyware
		Drive.Letter + Bfs.Create("WpsEp0F/ScidCNp4Y3e0zyyMgfVcztDiyg3tkVHRGvg=","g/F+p052+oFu7W5dKsdjJwWcQ24kfZYV12QlAAZn0G4=", "RcFa4APd3FRPchzosGmpSg=="), //:\Program Files\HitmanPro
		Drive.Letter + Bfs.Create("I3I8JSrgUTL9Mqxgqo/8Fly/qe/qWJQ0YEjPpb4nsqY=","mw2K8uHJA/REOcIFo263GItkkNVmiphKqK+ntnq6LKM=", "DAYAY0OOj2KVY4djjyIH7A=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("Rf5PAVaES1gZR77W3jhQtqrG4owVoZ1B2O6/VfX+dAs=","SU9oScSXwrLT1e1UyUATJzIIUzSmlK7SfJfOKM+Rhv0=", "TbfjzJVli/PS+czBeQEiMw=="), //:\Program Files\QuickCPU
		Drive.Letter + Bfs.Create("iznfizH1ob4o/oEU4e+sd78NzuGFNPh0eK69qVRY7fc=","4eKELGWwalQTSDjd5hdj57jm4ORdzC+CZjeOC6cmgPQ=", "oy5OskyjJf3MUiQAsKNClQ=="), //:\Program Files\NETGATE
		Drive.Letter + Bfs.Create("ejavehHsSgJosg6fsK66HzjyLS93oYOtKMLscA3OgWg=","X673+2ImOQM+kfMmBAgDP+qaXXhpLXeHCa4h3Y2plS0=", "uM9UP7LYeMuHdr+/RXSAlA=="), //:\Program Files\Google\Chrome
		Drive.Letter + Bfs.Create("JUB+PoLds0vKQhZsF+WBRj4p83RvRN7vm2PhtS7Pca4=","hTKCtWLR04aP8nZmvAmWfYtwCTEcJAi6Z1cW2xg7Beg=", "ZlNNYynIhjRme+QU9Vy+Tw=="), //:\Program Files\ReasonLabs
		Drive.Letter + Bfs.Create("ZM/ytIi/DvRzn5/+uESdaQ==","EdXrQrcL5Am+ljbL9uYGn/JycF1L4M7tmhSkcYfphv4=", "k1LRXI0WGSXp5ndenZgcVw=="), //:\AdwCleaner
		Drive.Letter + Bfs.Create("8PGsqpobyxsrvAHygwGg8g==","FNEYjfKTOu6gDai06d+YOFO4lWRoZ5LBWSE00nh5aqM=", "IZFGx+Xg6ObW9VKjnrU3Bw=="), //:\KVRT_Data
		Drive.Letter + Bfs.Create("s6dkiYHieCIPZuaaobj1kQ==","xdygKcaeKBI0wrzhH/lA2x7wUEoW6K5LqCvsiLpkyLM=", "ZFvln/YvJwocfRGvC/XOqw=="), //:\KVRT2020_Data
		Drive.Letter + Bfs.Create("93FIlX1s7+pW6WPU1EGq3Q==","Y2axRjZyHAaJG+VXCltGTrFHzknf6/x6mgCO9c3hKnA=", "ZcqvmIq0U/lIRrf73XXR3A=="), //:\FRST
		};

        public List<string> obfStr6 = new List<string>() {
        Drive.Letter + Bfs.Create("FwdLzkw8pYAb8kSKMHcGCA==","Qdn/1fQGzHfcLVo0TLIvv7vTD8vwMPRtxtIPcNicTFU=", "95CHkuas7t833CPYUFjizw=="), //:\ProgramData
		Drive.Letter + Bfs.Create("XRF07EZ/mEYvMpdk2YQidA==","uajjgdK5lkNBQqg9okbVuxd79ABy+2uAZyLkKrriXME=", "ed9/i78O/CJMz8Aay9HQFw=="), //:\Program Files
		Drive.Letter + Bfs.Create("2U7PpG3ZmbedFHO3+P1za3BPYW8i2l/RrcuIqytIXG4=","Lfrb8KRTviCeo38kx0fkK9yQ9OOXvSx8VxhGrhwlY/Q=", "KC9tUiZdIQoAeZxRZAQ9cQ=="), //:\Program Files (x86)
		Drive.Letter + Bfs.Create("4mfKgj33QTKDbRfJeMhSiA==","hOqJxHHJ3f4+X95d2le9Y5BnKxhO0tF5V+RJaKcxUNM=", "XFmC4A4CWdTOQG6jRE1HEg=="), //:\Windows
		};

        public string[] queries = new string[] {
        Bfs.Create("7pYQo/fzpPb1z5wSIg0Fe34xWDtcuN7l8nBYt+cEoEXStY1U1yI/L7CDdJjbc4+ofF+L7NmJy1vjpwzcg8KRYg==","HGLIUkf2Dx2o7M3ad7XPJ8PpMvloM0HaQMSlEVEAhsY=", "DVpcd83B1gJmn0D/NcEhGw=="), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
		Bfs.Create("QLxa0I0PdLka8E2xEBMiUx55UkGhS3zX09j1LFK3x+w/hbU+bStxFha4z+2l3MrGzcKLR85irtSVHOToNa2B2A==","N1Oooc8iWN+sKGCEi9AONqufmt6TOsjxXldo1/i1Pqs=", "98xWE8djv/tXwZHpsXvPbw=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
		Bfs.Create("/70W1nvhsD4GSHSo8F2xeg0ImzjO0h+eJy0t+3+L4D5CdupOp+iTXiePZecgJ2dgids8Yv9wmpwgWdWbs2sIBoq79Nmv48+3OZJXOjGJsRk=","AwZ7WUDjyD+MHEJrqMCMgmiLe1WWbxeYWqwlRE/npTE=", "gK3oVpDllxjuczcJben6FQ=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
		Bfs.Create("y2VjoVn7y6AOyLnOC6JeZuD9g+boDK7+uU7O0Y0M/1Z2Bh7o/4gny9S3REa1QqUBC+pc4sB1OIXETZy8XeIY4g==","mxqs/10mjAc4kcU0qlkSx5XRog7QMiRsgJyhpANmXnY=", "FYUB0KGeQTeZAatn9p7uTg=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
		Bfs.Create("VX718nTCqIaPP+ivlLCRf0jQV5YwPp5ycsRAUlW3u00K42U+LmqmKN+76id7hQll","yCTjnIr4Wp5rck+/dr1n2d00rKG1Zr18L6wPVhbkSUM=", "T+KggM/a0ul5teaaiZmTIw=="), //Software\Microsoft\Windows\CurrentVersion\Run
		Bfs.Create("U8L4UkIILhSYajrU9xL0CiD33xA0K5hZtufcdaii6Cf+s56TGZGgq9wOk8DpHxcGDWnj0GFD3Olj4NLWyrUWJA==","2kebvIomJ3aK3V1SGu/iVR6+Q/UWk3OkEQuxZvSy8uA=", "E5NHGbTx4iD6Cxmgnnl1hg=="), //Software\Policies\Microsoft\Windows Defender\Exclusions
		Bfs.Create("KUOVVgOkHW/X5HyEow6+XAqJi3GQidmS9bUdJlz5srbkdDhBHZ5JoEmH3hbpBE55Fxj01suCtI+QMkAvr5edYQ==","qkJYF3d+w0SSTSty8aMejSBIw2l4mnXvxTZ54RMJjc4=", "p+2zIHhDljI/tWBdS0zi6g=="), //Software\Policies\Microsoft\Windows Defender\Exclusions\Paths
		Bfs.Create("EsHdNTWYtlc7+6iWv28E3wNOw+YKNI4lkQQ+JY7sc7WKcq4M2J43n4u2WPDBhmGP3DRf8nfD0ZCR54TjLluxCInuhv8bm7O2riSFsE0uQZE=","kBn2bFvaxKzlrllsCOUB6bqyrzSyPRcAcHmxIVy98dQ=", "QRoONBrQ7OM4sRYFRGi4EA=="), //Software\Policies\Microsoft\Windows Defender\Exclusions\Processes
		Bfs.Create("TAcJkg5Js3aeaiQxVbnrJU9edlJOPuJ0TK1es6RVoJS0t2RnMzo1Oyb3A4kLx30Zf1AOf4VKSTjMKYSQWUbv7Q==","EKdQ8gSYvQvvDMKXsR9+p6+UrFKYXGlcwa1aPZTbets=", "NsGba0RZd3Nzo7hVfxWF+w=="), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
		Drive.Letter + Bfs.Create("6CtzkRvroL2bnqkY7uDOSpECoqiMEZNtThJqeX7+7TmC/9G6ITwd18ilRo/eB/2a","vgjIP2NH9AzPUQwJFEfMdrpxlpYkuAIFLIDpOi/OfbY=", "dUtyIHfTfdCPtupwBZIc/w=="), //:\Windows\System32\WindowsPowerShell\v1.0
		Bfs.Create("k5Akb664DU7X/O9ayuJHfvFpjPAuTy7naxtdQyL47fboRY3IbRW9dB1KGQIQwW9UdBz6UgHXkfgY440iwa7I7g==","vtnDHKHW3GOt23morVjFOe5JVTiqjy7usE6LW36H49Y=", "lFuK6ySdCZg3eJ5M7kUY6A=="), //SELECT CommandLine FROM Win32_Process WHERE ProcessId = 
		Bfs.Create("IHggNtH+KmzdqLdMAnQs7fSaa2sbHQZ09UJKu5JoN4sZM6DMJYbQ1dpyfA/2OAC3","B8lHrpCw5JAeZkNCqxQmv65mh7q3JlmBenknraoJTMc=", "HB+RQyRiZJ8wYeKuhJxOMQ=="), //SELECT * FROM Win32_Process WHERE ProcessId = 
		Bfs.Create("ev87GtOHGmJwwakFAxLJnNTcWHxeBR1VVums4Ve9Bok=","iio3UzHXCeeZ7O862hu6Nrd0QExATsEotD3QsboTots=", "dPW+1swO0hU06bTPuCtAbg=="), //Add-MpPreference -ExclusionPath
		Bfs.Create("ODWKbAShvvGH/4gunI6xf0WK6u9AjAm6tTmqbEaj4I8Quyvwuq4bdrkEbnAPgXfWVkoMk17RRxmuZe2sRyNP0A==","IsFMq6ro9sI2U30d8LVGlxoDWZ8ywIfBT6Wbf9dVFtU=", "q99WkX3Jxyo4/jx2IBNSKQ=="), //SYSTEM\CurrentControlSet\Services\TermService\Parameters
		Bfs.Create("t9wjVPWTPvUfvlh5IPuhaAFVuaNLHfkLLlF0EcCyWtASMCENrwM6Qiab9dhW39kw","TWwzLPx6Bu7C7LCoR71zR3nquHGUavUkl3yh/8uIQcA=", "Fj7i1UfDxv4rptP8Db0bQA=="), //%SystemRoot%\System32\termsrv.dll
		Bfs.Create("pH/HrB+1nIEBrLz2In2piP0c0TU1+6C5LYZLeBdJSaD7Gk8SN6Xq5NbuZbVAWC0wNoe0kGZ9y/N38FcYfWvTI/j5Za+yTXQo5CfCHa4imiG/cqzRBexFbDvAP9lTG6k7","/bqfUN8JnbyKGlZU2b6r978VKOt3Dk/GFP1hxhdBE/U=", "xFEKX+bJRC7g/vGVTGdSww=="), //SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
		Bfs.Create("i0e2CRqEj8nEUHXQnCtt8oSzequj8kfim2kXlkyjMuJ6cQzA2j84N54w57dTlNSoqSABFpeNMJOSj8OE68rjS3SRs2nCgg3VFwRkr6VCnaI=","vQwCLhuS1OnH78p10R7FIYzD7eZ7gRiGfiyEvUPcqHc=", "roWidMOVv2Y0J/4EARxYGQ=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
		Bfs.Create("TySXDk2anrMXfC+Tjqpi2AdvU+vl/VURrxczPdk3HhLq4CMsgvRFNllSaW9dur9AJt1whelF7YGZzMS6SdUvww==","1umGv7sCaUHQ42S8eV/JVaso+j8drvocd0fz8nMvAE8=", "vdaUug9KInEZXe4UgHuZXQ=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\SilentProcessExit
		};

        public string[] SysFileName = new string[] {
        Bfs.Create("UfG5moMrfFmg3UGKugc6iw==","B5A1JCfhgZweJkZeb02psW2lWD8pk3T13bV0fZZ3cck=", "aUV/7XqoStSw2Ms3cLgdiw=="), //audiodg
		Bfs.Create("TUNEOdRO16ZkEgHB8qLXgQ==","V17HeHELUm7u6ddgYdoLtmpJJj+pt6iGOv727xZMhOc=", "PV0HGDW36r+HjfORxkxRbQ=="), //taskhostw
		Bfs.Create("WUVC+UXeF8kYvOkkqxbPmA==","+G257A/JEjhJvUoWMc3tT1o+yfP2oNkh3OxrgWSPjMY=", "LmKZXO5ZOjvlnPnAxqQ7eg=="), //taskhost
		Bfs.Create("dsVPKz7Bat559XekFKaZZw==","u0+M4iTcPtNG2FU8I/sa8p4TKc2M2Rmyk4bdlLuEDC8=", "Ex3VkeCmcIjWfivAcSVzRg=="), //conhost
		Bfs.Create("A3cF0aeqQZCK9hP+ft7OMg==","W9eObf2kNAgJR954eFY4f/BF0wSbSl2gnXsI1GGCRE4=", "inmtyuoJLW2fvESsBOEZhw=="), //svchost
		Bfs.Create("emXNTQZgB6Itb4fCbq6qoA==","+WuaeeJy3av55w3uFfsLqMa3y1aOVcaYwc9aTqMPvrc=", "3TaLv6CPBAVHRtkfB6UObA=="), //dwm
		Bfs.Create("URmwot/U4tj+H0acP5bDAg==","VfS23xbnFQmYGOUrOBDC0dfeZ81P3rH/M+el4+Je6+w=", "hPoR7MVOwuLyR+6ZxSbY8g=="), //rundll32
		Bfs.Create("N7qfB+1wuHc+iE7xLzlkQQ==","nYHqJV007yXCqI1s2kX5EbTPXyQC4uJSk13qEd8DNqA=", "C6TGz2llYxmubYlhditaZg=="), //winlogon
		Bfs.Create("FG2KYPoOhpCmP/EucGgNBw==","hY4TgM9OaGz9TM7tCehXEDZbf9XYmX7Uz2H5Q1AuIYw=", "/Td1S5n5q/nV5Gm7SXwzHQ=="), //csrss
		Bfs.Create("15VplMnN9ygbhnY4ZUvk1w==","nI+Z+dPVcznUX0KILcx7bh3lELomE5cUKlJ1V01MLVU=", "qLgOuUsqpb3/mhHUDMBaJQ=="), //services
		Bfs.Create("Z3i+mqfKvHFepeA2AexxyQ==","X3my2gOAJnW93w7O4/TD7DySEZ7VLIdyaPMF04wGKLk=", "EfD6CruCihkTWTQpiwexBw=="), //lsass
		Bfs.Create("opqboqg4k5ag5SqucEkWvQ==","qwAkK4OJffjB/b6QPmEMyrcZF7Yg/LR83juQ6rt9IZ0=", "khQhzn1INMUgStbR+atNTg=="), //dllhost
		Bfs.Create("xvMyRJKXjWTDcs6a8pD6Dw==","r2hNHd0WGmyI5KmLNt2OUI+GQPJP9ipFUTmQatKfwcQ=", "kYGDUxtRFkX6ZjnsggGYHw=="), //smss
		Bfs.Create("t43AuhC/pO7bC9Yr7HPKLQ==","FNlsD8lQT1X9k2ZM6oRvHuqRCOi2whgwzRf6ou9MfGs=", "DyRqxlHgsHyjhHGru92uzQ=="), //wininit
		Bfs.Create("kSB/paE/YrNk+nkkw49jJQ==","XcOz5kX7xUEAOiCEmc7i1cDOnI7e0eiBh5GmHKqCvg4=", "rAWvV+83Sz1kXIUuvVk9+w=="), //vbc
		Bfs.Create("DzfRlahk0CQdB93HAWABAg==","lA4D5r3tm49a17OkVKbKDBlISFaPIaB4pZsMnKvVNS8=", "WA+SniPx0YJc2EVcwXKUgw=="), //unsecapp
		Bfs.Create("XX+Nr9daaCFedAGhLaT3GQ==","A0mT5/TyvHFJyQhI96bvQE9gLA3vVxGSNqPS22Ejt8w=", "aJ1u5OqosILSXfwE0RnBbw=="), //ngen
		Bfs.Create("KsDwoF1qJ6PXKqyIryvx1A==","1g3vRbybtIy5S8OY65d+OL2LLSzx3ahSvsEcIdULlYg=", "YJaLqjYF2kIzI/cdqbtwyA=="), //dialer
		Bfs.Create("AuMwRF+TBkKjTobWoaLkLQ==","1pe2QasrjJ5hnrdxCu3jmQp8YtmQF9TPdjPaRjqZ+/U=", "XJdclg7u8U9gu+X+LL3/yA=="), //tcpsvcs
		Bfs.Create("KHDX4+htuUWhdsbeImQBsQ==","cyrKy2Wlh0vCCEV/u9/oEC6n/70fDYvT2709tANPGwM=", "l6V7BdTGlSsalq/oFnTY7g=="), //print
		Bfs.Create("RmfM0fmg9okY/bqnSX0DLA==","lwsg7ic3V6Q62X2uKUVOOL0YFFk1+JicbyqI+iWqF6A=", "BBK01FHvdybaXitO4UqnQA=="), //find
		Bfs.Create("qc/1d3Cr2M0iSfE0DOsLNQ==","/w6VPR8zg1/JmjiAm/bSHnwRHNpRriQOIzh+61XsQIA=", "YWUUjt8Wg30YypCtqEebdQ=="), //winver
		Bfs.Create("Won9vSkRiUUU3N6m9Qb/bQ==","2wLMx8H9zTeefgtrBc4xeSktUhjjvct+UYyhR1L/Oxo=", "MEz/g8yHoqGPEsr1fYCFTg=="), //ping
		Bfs.Create("4Cchkg6p/YcE10gJ02Qw7w==","//A5lmFPJ26+fUmXy9qFTbqIh5sSkSTwGhXBGZNdaMI=", "wTWREB/kCiy/5mHhQCU+qg=="), //fc
		Bfs.Create("4/jaTnEaiYwrExMX4FIsmw==","i01OnAQkryBCPmgeiBbFGPspOhPhgqZAwMAQ4pLrRtA=", "4mM4xMwQ/fCYBh1xNC9d5A=="), //help
		Bfs.Create("hvLplplfK00knV09ZbDn/g==","zqWzefBD2WOWzKeaH3KEJI48XPZyr81mqrjrFJBZxYw=", "ZYRR9nKneeialFCPXHqDPQ=="), //sort
		Bfs.Create("ikr7MWHgEqYtT2pGK1oXZw==","Fk2CpOOJ+iqwm9FEBUzHAeghdVV2DxHzdPa5Ga/nJmE=", "SfqvNRYPcEX+q79mTHcnPw=="), //label
		};

    }
}
