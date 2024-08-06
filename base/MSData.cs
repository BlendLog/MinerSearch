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

        public List<string> obfStr1 = new List<string>() {
        Drive.Letter + Bfs.Create("nfvgWaJztsXgh5fRRsefdc+soivAoiDtn23Of/LbhTg=","OktoD6uy/Zacr92HwThHztq+U92omQBYPuDdIQ0W7T8=", "qU4v6+ecJxIUzIIydaG12w=="), //:\ProgramData\Install
		Drive.Letter + Bfs.Create("uXf8zu2VaApTpP067iV0hv+cFdceKuYX6wM5KbCfVzs=","9bv+6J2SwXSDnISEaTEmggDqgXmUXLO2jj6zElhyrtE=", "MllfEDOWlPYrUg+6g9MVcg=="), //:\ProgramData\Microsoft\Check
		Drive.Letter + Bfs.Create("zBVhNps5z4N2CPyQ5+8wrqmDauIN9+AaOzrLvb45dRA=","YplDGq45NHdJ1WIyIah79LNMbyAxdIbF6KKW7dZlXgE=", "x2bV+sSKAPt/hJGHgDKwvg=="), //:\ProgramData\Microsoft\Intel
		Drive.Letter + Bfs.Create("/XBU22EmLzIQWMIhpiJU6eT4HBXdPvySc/GZ+RDn3b4DNgFzjPKzTLf+8CPIs3Hz16zV+k/D1/ga7MdmxNVY7A==","1Xe9Beca2Ow7eu5VCiib+ea7FF/OMvIFy58hdJQ83b8=", "KDmPeESvtAmnYJe8J0ORDA=="), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
		Drive.Letter + Bfs.Create("Cl+rN5CqM3ZehNUMgU+HIxK9fiZY9NBuQUNkw0ggzX4=","k3UJC+D34xJO8goPks57ObHpWqd5vINUDbH/bzpNs10=", "r+boJUrlWZ2RuNm9ICpqow=="), //:\ProgramData\Microsoft\temp
		Drive.Letter + Bfs.Create("Mi+ysZAWVU0fnBnbvLfXdZ9TTp4o3gYR6J9gv0OXh1o=","ofooEi6ZcBYeiWUDkJi95xigiu/hp6w+H4SQmmAMiB0=", "iBWK36tQihR+6RXmeVjcKg=="), //:\ProgramData\PuzzleMedia
		Drive.Letter + Bfs.Create("vxlHornj27QP1bDpo+iaxdUXbp/sYco/VZVfemeqW+4=","mCgRcpUIeyJIQoNRnOc5/S98fR8iCmqx4JXcR0rCr5I=", "sEGWQ08JTviV0fCBb3JATg=="), //:\ProgramData\RealtekHD
		Drive.Letter + Bfs.Create("NX8mXXaVTne/ZB9bEJtjZU4xyDeGpNqh7JqqNZZdmE8=","7zoP3EYuYS55gzdl0DuIoe7p+v/F7mfa4V7R33Fd66Y=", "LfMqfQo3yQ93hTxx0jIV/Q=="), //:\ProgramData\ReaItekHD
		Drive.Letter + Bfs.Create("2um9u9dkfDOZOCQWwf/FY15/YDfYfBHXJ9V84hf85Ms=","ZJAWNamME/DaFqWft6RJEaz3HZsbyNQRY/3J2ZyPe8g=", "jMQeuUdMJwmpZNwSF7MnHA=="), //:\ProgramData\RobotDemo
		Drive.Letter + Bfs.Create("O+av1ET+RSqt8/4k+NvBpD8bTpP/SbTqv7+fLE4T+ZU=","2htsOl4baGFSfTIOHbYzr3SbnGZp6vWIcWo9J8RHVxU=", "GZGIL50GkON4XY+V5PgwUA=="), //:\ProgramData\RunDLL
		Drive.Letter + Bfs.Create("0yemHWmslA49zlrtgxpdtbSBn97sFRQn+4KshgF9jnQ=","Tpboc15f7tH6XEB/7+M2LJP5da3PzZkgKrSOqqJke5w=", "LiCjQ0UwJiR5WJaX4I5Dkw=="), //:\ProgramData\Setup
		Drive.Letter + Bfs.Create("tOLBVxts5VJCSLVfDnLtR+r1mOGjEnVksWQwZnc/kCk=","ZTU3ehZ1up3ONtijtrbOaiWxR1LaCjeMmRecXoHhh0k=", "dRtJNjlZkh6tlsFXUTBJfg=="), //:\ProgramData\System32
		Drive.Letter + Bfs.Create("DXoeuF5S6bpamoV/VeboT/Hk9wOopzMB8ABrFk0to+c=","aTgaXVS+YaaSTu7XeAMfM7KRAidYxOAffEmPoEFSClw=", "AtEmMilAYzfFi1unEnLMEA=="), //:\ProgramData\WavePad
		Drive.Letter + Bfs.Create("YPZAzTDE/oOa0sE7mTxe9rU1ig/4bm64YKri0jQLVqR9JPkl6UdY+hGffklRMdzX","nxQ9oCFHEbeGVfvjum0/naAUHiKFNEzWPJ2mNFZYUTs=", "gKBm1ddub8C7pDua60swSQ=="), //:\ProgramData\Windows Tasks Service
		Drive.Letter + Bfs.Create("Rt880mu3efMcvU4IVzD3E5wQYw0PwdvGMFffu7+fAuE=","Ix+CnMdPVQoYBXOJkBONoX8TVdZRS8eP9obBSQwJNLw=", "IFEeMppWnY5RtEHCQVmX9Q=="), //:\ProgramData\WindowsTask
		Drive.Letter + Bfs.Create("VktIPVrJw6VuhZ1UzaqU9wFPRGBMwg548Q7D5FlwWJo=","W4UgM9DgbZZrdgfczN9qjNEaZQmoTxDaFwBt5aWTIxY=", "JieQbKPVkiRtplpkpKQgaw=="), //:\ProgramData\Google\Chrome
		Drive.Letter + Bfs.Create("+ZSD+3WTJZIa0J4sFnLhuFpss/soGvNs271t/P+1W94=","9yA8wJuUkoGzNW9jzoBSJ1C1Jbvwackh5efyAljrfh4=", "Chg0WS26+gXc9u2QUSdjfQ=="), //:\Program Files\Transmission
		Drive.Letter + Bfs.Create("bx4v5MenCGgMXdXa0U4dEQj965UcMw4wAv4CEofvaCI=","foHhPGsnyx4RwFkApe0rYwiuviwcrulZ8TjpbCBUhXY=", "NG6bIojSkFWqUjNIkjxcfQ=="), //:\Program Files\Google\Libs
		Drive.Letter + Bfs.Create("3G210+/Ek4PvmSseufY9ZI8FNf6SOVRVTwFkZEWqWLt68ka+OdAvzhsoR5p5yc3b","eqmIZwxYh8+BMPlMHQ3grLBHPa3sVnnmXrMqRRW9a7s=", "RYFEBK4IA+pnrRkm/81GjA=="), //:\Program Files (x86)\Transmission
		Drive.Letter + Bfs.Create("hbJCwS1oZGkHt0kv8bt8aRDLt+lA55WloNg5xKNr6t4=","3nvXhpjQX3hhorelVyuYSwFDPrFG3D5Mziw3Tnrd6D4=", "2fkCQ4CIAdluea25xzb7fQ=="), //:\Windows\Fonts\Mysql
		Drive.Letter + Bfs.Create("3qKRGSAtNM/ImLLJWSbe1kiBw1GiOtiNu3CcyM9KuCaZDX9h4kOoFet915VxQ+ej","vwlCHI9Z4oKf14jrTEwwU/qDbZk1cIirBVJFohOXFPY=", "KtZAXA2faPQ2t6ywHLUBfA=="), //:\Program Files\Internet Explorer\bin
		Drive.Letter + Bfs.Create("LbHXsNP8ghg08bPkqtQmb4/sEjiMbYfHba8oXuSRm0Y=","0n1ILsVqpnEguQvotOQR8urShm5RSYh/0X+CAx8SMQs=", "EtP9/DX11lXR7YPFaVNzMg=="), //:\ProgramData\princeton-produce
		Drive.Letter + Bfs.Create("6czdlkKO+PnfZz4T2q0CYmasM0o77xsHHAbYmsDGnp0=","1j6vt+Q4nGo3qvczrnzgm2tXizmMX+GwPoydBsk743Q=", "iHkSapBwjV19v4WiSE56sA=="), //:\ProgramData\Timeupper
		Drive.Letter + Bfs.Create("Un2EIIY26nOHZPRVU0Y67onS8MPHlBvOvosIRl4pfQc=","vfaDETxOfsZvkNXnAZwvnw9KemlZi11M7LgrlwyA+8s=", "7ZPnAYLDvy0W8L49il6YYQ=="), //:\Program Files\RDP Wrapper
		@"\\?\" + Drive.Letter + Bfs.Create("yznlgcSp0qpKYqExBOY78xV67gTA77ZsvR/jCph7YW0=","wBnCQkX3dxInNXgi3EbDYeKrt5YYZkdCtE0FzNWumOw=", "dAq29Fp/uaQX6QpsEehNXg=="), //:\ProgramData\AUX..
		@"\\?\" + Drive.Letter + Bfs.Create("C7+idKSkOLJ9uvhhgzgwSd2LxfUTdaAMh5KZ90Z6Ij8=","tgLM8lelJpfBAcWXWsOqqbA0aiNvOOQFWrb2BbS91Xo=", "hChlkwH1O5IsbLoChOab1A=="), //:\ProgramData\NUL..
		Drive.Letter + Bfs.Create("D5NrsQwstvq6HZ2cfRMZ0gYfql5ctA63LRs0Gi1SO0o=","MmL65VWLjs1g80pMFShA/jH/psD/CDnBjAg+Ak93BxE=", "DSrDdrVbtCwdT+IOzMDCqg=="), //:\ProgramData\Jedist
		Drive.Letter + Bfs.Create("Cy1tc2rD4moISUhpood1c+e0assI2qjFQcWrEMd5BPvDodMZX0exEgdywLMv/sV0XyTTMDsJ0+XNR/Ba/2mt1A==","6dxrzF2EylkhRHdGR1cWRL4lYZorMoVgLrzzmhOqsDE=", "MgAaJb90O3rSE6+GMvUNLQ=="), //:\ProgramData\Classic.{BB06C0E4-D293-4f75-8A90-CB05B6477EEE}
		Drive.Letter + Bfs.Create("nXtl66luX1nMkS8uWZij9tYoABQdGX0AWkJjbzDg03sh5q43bXpTsnEU+XUg5JBaOQjS0OtFbVTVRgl9ovEl4Q==","5h3N4qGpMiXmnWx3DXF/x6olsSalZ6WoRh1TG6YsJOw=", "7Bs3hsQ1rz+zoebggGu1tQ=="), //:\ProgramData\Classic.{BB64F8A7-BEE7-4E1A-AB8D-7D8273F7FDB6}
        Drive.Letter + Bfs.Create("6j05r3VBUo4rZFsEpaQp7eNBlEQYm4qhqD5yoAuwgV0=","j6ivYSP43Z9tDTkfA8VNgMkVV5oIauNvEi9an6zLw4A=", "LJp7yhTdA/ctUBoQbtTtoQ=="), //:\ProgramData\Gedist
		};


        public List<string> obfStr2 = new List<string>() {
        Drive.Letter + Bfs.Create("XaAN13uG4gHOvToqNjV/S2oaN0pP3vc0hLYHivqPItI=","i4S6m4XZ0YMb4RfCt1duOZHZ/WHhnMoMVaaQhllEJRY=", "GY6LgC0Ku+bN81qsxaiz4g=="), //:\ProgramData\Microsoft\win.exe
		Drive.Letter + Bfs.Create("ao6urfdX+uBJl38TcFzJ/zunmzWC/0bzS8nW7co4mqiQmkQc6ab0AVV/LK6Evxjn","ykvWpNo9BpzZgy3gfAs3QQlnaVlAnjRADNt9WiGg4i4=", "tgDG+pw7N8HlCvUDUDX6wQ=="), //:\Program Files\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("8IAW7x91zQiA232HPC8oeC16SxwFH01EKevqRvgdwDVADnMJ1DEQW19jHJzpgVGe","EawNiR8mcI0CN1w3Sb/n/IfuRqdgBqdWyS1dluiV1Kg=", "kUNDRDbPv7BwloiSDDvczA=="), //:\ProgramData\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("T/gR8BD3xRBSnVJs2lve9FvpUeuORrG1skt74UZdGmI=","ZszqgG2I7kmgAbHCPirplmgoW/IdexyPDXSdzeuErGA=", "rUWv2Y/m7UUbm0+L6ozSNg=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("Bt7HdFdun/cX/tVFFM1QqM3YlXVqrF9v0Yext/JQ4Jx/r1ZerLblKBxQSmlBCIUp","bqE822XChRYP0Csedaui/ER+wKKKjp4zVy25OJXkEj0=", "64FLF0t3++93RlhjEwRy5Q=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("sDlQ6RojmLOpTDT5y7IX5ChUl0CulpeIsotm8Wlv+uHaHP2W1LUxw+BtWDuEEUtF","fJiu4CmNsgsF/aQf3wfqZfhe0F3NlbZPe9yHACtWqWg=", "8OmPjNOyaBYBgCdWmPyqMQ=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("f5CIp/f3B7OjAVV5KURkIQyb2veniThRmojjTh7Z4UUakpxQcuyotjOb92f1B8yb","/lIrapFUekL58vZJAHkUumP4WzjUov5mcF7A120W1lo=", "iQNVaOx8VPvleveEVDfdEA=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("37eR+w0UY7aV981MJeqorMTi7mITyOW9hhpAX4LFg3ait8OKhqSuilGrc71sBUhX","heNpSyvDDEr70POQBpWH99PxBGJIzlBy96SdZxcxZCU=", "w02cAls+0RCtblMi3YrljA=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("ewbWONBYXa65WQ5Ebcl92qksZ5N95n3gLSyyFuRRbX4LfBJ2cRcisIyVa+JIYUEr","gVasuta04DhGASMAnl4yZ7MBtS+RE2AY1ekDUM9HCDs=", "a3POSsOEEwK9YWedBuNy6g=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("7GziPrirQ2NjXRIRyOdLr3S51fTaNZqhpogPwZ0dHW43ylrqFNAIXOMqCzjZzjFd","UaD6/UEsHryVmcLXPf6ekvH9IWJVwYqebp+oQWE0U3o=", "2M+7tJDf70LUOM0pHiHyvA=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("JdgNCSD3TseBNU22XsCdNanYt9y6KsS4TeCTflSVIVZnWBevF6TLBleTAvdbzrO4","meBaK43rAKtXXMEBiAk81FBoOlFWa9KZ0YSHdZD3gc4=", "SnRr+1BKveDdl6eEduMF2Q=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("8GUUb6EUbja/SFWn13elyd6usyw6YAPiDKBPkWoRzmo7CFXPn4g2TOMZzF3e2r/l","Aug0Hfqtc9kGnJt7ty0OyjpYE0UVjla6ta9D/R/9aJ4=", "6i7pMq6E7nsYrrL9uWFcIw=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("phILsdgWVarKEC7T/331LksxOaplCD3AhbL5fm028RuMOllQKRULxKc3hglvFu27","ipJiPgl5nLBG31+Fv3tVnAlsfEesI+Jh2jTl4EQDVEA=", "7zF3vPVdyKEoaCIerscLPw=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("m12Tam4NNIZURmow5tErVkXFCFri9vJTvO21MsUNOeyDKNa9XK/str1Vp9ipNiqX","naZ1tw31HryfxPstobOFt0qxSL+YUXgDVlnRmxd+aZs=", "vYOH+hafkXPFnI6sddN5Lg=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("dCs8EhXfo7onW/dnvB0n9W6j2zAqqLlcKKP4koKItpM=","vpd4RIwK6APcjio3zPYWgC7G5Rf2wLbUCdYxML66ACI=", "u9rGyd6WJnd0MGl2M6xjxA=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("zG3NiC4iF5wSFWZPxLn/a5hwjTbxpqn4wjhi9z0GDoLcU1ryrmC4WhqvwmIa/YCN","5KZyxFNIzzOROwD9sF91LKSOJ1NF+hOOWsh22fBh/Xw=", "fhNAGx/v5pPFM9b7KukETw=="), //:\ProgramData\Timeupper\HVPIO.exe
		};

        public List<string> obfStr3 = new List<string>() {
        Drive.Letter + Bfs.Create("klx13XJ31DfWCJhnUYXVTORQRbyZgJtdsrOziAvhWG0=","6gZxf94nj31cPcOz5e5eP7zUBJrpFLbYEMsjbZ6kJhA=", "zASSMfvFQA9HrYmGtLDgDg=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("mvqd9NaU9SPecrmhDidqJg==","qTGcdhwt0xgbZ/0+X3bqPHtG5e3s+ErNYsdtC3a6sAk=", "UigcEhQdIMww0HYTQjGIng=="), //:\ProgramData
		Drive.Letter + Bfs.Create("BmHagW3dcYSokKH/RKW+GvjGlcsan5jcxK3GyTQlGQWSSDTXEjMEeM7/ewPOLKyZ","Oy+xE9yCNzbPm2+ja4Mn4Hqsr8aVnscqaAUm/WdXhHM=", "d0W90o96OH2Vr2EImued+g=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("SMFhl0C/ZrUAUpPPWho1jc5+rywyqVIirFb+L3cdnzVifGrnX+e9PoLsP1D1LuA/","roXiO3RCSwS0L56viprvPC37lhGRfh5vpToRniR1ukY=", "O2KFHLgnfdvF6HanzqmOHg=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("X8pAk0lbmRrjMmtEQZSsm0Gm+N/BRExpllDxT8LpoY1u6NNYCHDz6yHnjwAyG9nZ","XTh6aBonrH1elDaUs8I4jPZno4wbd2Jkq1B060GTkN0=", "yEPcnBXszjXspPdEcQdXpA=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("wv08E62EOZwbaX98JexeAFjebjRxNR918AVtRPFIv91wR8quKFORaJCznPMuwXGS","R99AlMRhkOSbSImkp5o3tezp6HKF1CdOPjiq7fUqK1A=", "LLGkz2wMQSeVurPBF7II4w=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("zcijMq90Q/p+8ogw5a/OSdc0BQIe4f81tT62f0qgD5xANI3HBdsaiYmxU3EdMNOY","uaxiCVhZu7jRsmUVTlz2h2wAbWfneWRaGVh4LBpd8is=", "S5DDz/bB30ZZr7OZbTUOfQ=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("HXz7i2bQ/D/Bq/Zt4ZoSAMENLGrfZ0nUsJbOKz2xVVWtDlUU2yWpbIsORkkBvhJl","cY+Fx5ERhUdAO5QY9jWHRcMnAH1TcnXRkn+HWKvBu34=", "HJqTjpTHddNnFQDjOjGbHQ=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("1wVLPB7GD3jSYA7K56CTLR+5Bcwpf2TCTW4y0IBzpGZltRTaTlBhi0H0cOMXiCI4","GQx65r6ybJ7P2gn4tuQWQ9kTbj2hYb8/a0x/+4gokdc=", "2pGvG8esK2NzQ16dPyXfTA=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("mFPaqmrDg5p34NiEyXVUk2WvkV+uLF9mR2RZBi628da7xXrtdSeI3EpytldaOcR5","VLAptsGTBTuA6+b2pHDrUvFO+orW3WdNSftN0oJwl5A=", "aIE9tPgjvtAdKmmQU8ohsQ=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("EUdnid58jQ/edmn7FWpLexPimXs4OOGkfizUPSmPOa679S5PTqwudEAo0BrehbQe","19t/2RSzLea2NN1G/QaLwglMitNwHXkjuuxZ7OfOHe8=", "ga4da1g6xqIaDOdzWoOtEg=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("NYGSlX/O6I3Fg2+E3yj4s27pS14KAxHLncDlulPgX0MvqjeNbE2xfYazccpKJsZ4","BiagKzQuJcsdiKnijAd0utAAaIYJeWrSlzcSZjsW4ZU=", "t56PqOPsTEgFLfxOltff1A=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("bixhwnCBFW1H2Jt3CfQkEf2kv6y4Lenw+uvzpIzCk4A=","R+VWocYMaAgrzSiGosVAz80sDI5+iMGzeaLax52oHVw=", "cq4WTOPkjaXz9k0SO0sOHQ=="), //:\Windows\System32
		Drive.Letter + Bfs.Create("c36sgA9fAEswy7yY3fFhzsmSKH+qWdlwmk2wTxforc4=","5YgldK2eqWSS28x+5BYoodXGfSlTrJT8LX4VSSreSXs=", "t3tc1Gn+XEdcaddU2xGVmg=="), //:\Windows\SysWOW64\unsecapp.exe
		};

        public List<string> obfStr4 = new List<string>() {
        Drive.Letter + Bfs.Create("yPmRHHIlKY9AMxSkStFgVQbyb73NMqCPYASwoqY9dao=","3sojBIPute5wG5PbC+dayw04BXt+NjtJKAaE4sc1eGI=", "IdktFr0nYcYO7eslWTQtkw=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("cAMMA2ZG849wfl4WQZVx4kM5TvwYfQDo/IKV7pvzVxl/ci8poyHyo5jt7thtSE9c","3iOUirctWFWKrSRuApUIIuTutYNUzeQoGbjH4fcyqME=", "LIYFvGghN7TayiZQN/jvIg=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("A2YST1XlDx82vSJo6IEamJDMCryNim/n5LvyrW1AiFS4egUKn1y+4v4bwb6dV3nT","2UYSoiadaXijYBCzWByTHGze3rK5IRaMhlopi0ExF0o=", "W7uDrZ35xhXjBM3ErJsEsA=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("Dd9Ye8DbO4s6E/hd+Qgq2vfAMOsZg7WUmrYX0fiELb86CSPka3mYcLcpp4vDc1tI","KPa3SCeYypahB7lG7cXh0FTLrOQ4fHa0kCNM51ERvAU=", "IaQNjBYq6jYIf9DD6yNbvA=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("vHMTF6T12BUZuLcDb1vEg6rDwGFVg/tOVARq3kroxSxzCqOSF9uIYYAwMQ4mKsHX","tkMJSeYzIJvvZB+B/7ohhrMQVXqPlpSzBQgzEtPyqk8=", "Q74J4+Sr0UxTwgsFwZVvOw=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("zzfSILRt90Oaf37gA+Kvl5fF7yixJx1Ty8pr0H1Wo4B1t6xswy6HHCQJpxypijKS","htkl+OtUrTVR7MczCuGj/jJ3fsx3tCYHiCl9NSy7BBA=", "d0mUoW/MKDfRVMPiBSA+RA=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("H8+NrYOSpW4lHBJRUlJhdSmPmwaa/ihFvCh5jcZZgDluX8jIEwzm1qEIorNEPtUC","hrMC4lzv5dZeqhRdXLt2cJC2X49RfbRd5aw/spA2LSo=", "daLhZAsftcFUbEMJBaEaRQ=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("UNYIt/ZWicVKts8sF0Wq5RMmFT0hUykQyu3J0mbvzPjCcxC7LnMw6RS8gUQoKXbW","uZJFVT6GAu7O911PA1hQJ1IAE8m3U0C755geH/H4Re0=", "aamaXFE+MJNovIKRcLfteA=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("O/1U+/McG5j8NdwCtw1YXAuDcmDNJUOP2qDwRSnqgK9iw2wkEyhP8wDwK7wrjla2","X357OhTHGbMJmu1MR+VMmBP62SXseN8qYTv79oyGXTk=", "Bc50bptmjPfCase/Bjg3xQ=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("AisW1Vg2jUAeq/RNrMwRhs6natF8ZtzlHJVukfb60GMSEwnCfIJ5797dsamWtn9d","5snumzgR8i9DbxT94a9l5kceFjXUFPMnnzETJEF8Feg=", "zRYzPNGWx2mKCipYSHPM4Q=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("cxwHyUqUMVFKOFKuwPzF9Z00FbwdVkhZ7FfHX0VooKNG1WqmXjmqRQ+q5PJMb1aZ","MrMAN9Rl7QJ2M9t6TyyzrJ0SpcrVmVrhYIovfw1S0X8=", "tZHqV2quJrkVrovSWw8PLw=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("wRfEgCNxpFYJAf8E5L6NfPqtTHFbGIdLqLyH1jQ5CLg=","333YvFaPVNdGy69JoMqKfE0e75s37rbQgTsucHGuQ5E=", "4rUkKnKMabEXgF9jLnvz1g=="), //:\Windows\SysWOW64\unsecapp.exe
		};

        public List<string> obfStr5 = new List<string>() {
        Drive.Letter + Bfs.Create("3RoJQnrYQgcT8zc03SHSXfsRzi2rGhWqA7i0wlgmk1o=","eiibwdUJxKdxiNAy3ZwoM/TLCg2PRktrZvHWn0H4OQQ=", "D5T57klTKo0BkYBMi4eFEw=="), //:\ProgramData\360safe
		Drive.Letter + Bfs.Create("ockk1+usZjvP4diNjaa3+hxgUn57AHcOIla/Is8ioOw=","indqWKUIoj9RoP6R8FXR0eKl+60ph2LYtN6Ujf5uKGA=", "++VYN1tW3Pzp9qxcL6GmBQ=="), //:\ProgramData\AVAST Software
		Drive.Letter + Bfs.Create("+0PbaXtZaGHmVV9q952ulEKUjxLD9HbOZ3vydpsnBsI=","g0tovQMDqXeH/Wu8AuUu1U92i+SxUCLkrZALgs6DYBQ=", "amzu6LLTmlS0SG9cU0QFdg=="), //:\ProgramData\Avira
		Drive.Letter + Bfs.Create("ce5yAbIN1qCuQAerT9iTueA+2UZeSvlngRaRA37Lnns=","3Uipebki+IHES4mbg1Q6iG4Nc0A/982xnrVGsZBuWWY=", "cse1kbx16tmn46TbOvFg/w=="), //:\ProgramData\BookManager
		Drive.Letter + Bfs.Create("ssD970k+c4HMFGBXEeOUkFtypqEBWYplWiLAavDc8p0=","gL5BTfC7Ombpe7z9AzOZY3jp4TlQooqFDvMUnSOnO/Q=", "zaQS+mZF2TtpSkoLtuTZCg=="), //:\ProgramData\Doctor Web
		Drive.Letter + Bfs.Create("5rgjXVDZXY+BG19sN7kKcRklCTAtDFduQBeWnEn6awk=","v9Vj28KRel+R1ljkl0To6Ij0FORWdvpXiXSdtNL23R0=", "joc3+/uuDifS+aCW8a1irQ=="), //:\ProgramData\ESET
		Drive.Letter + Bfs.Create("dCRuKvTr4Y3wI4j/BFCY5JCRA9q7clQSznad0jRuNcw=","yiNMgEPNRC/YBsRHOr4O51G1mHaMi8Ueky4ObV2NbG4=", "Bby/ZKkKMzMsIikqZp7p2Q=="), //:\ProgramData\Evernote
		Drive.Letter + Bfs.Create("9dDL26ul7PbB6vjoN3CETT0IOzI05xRmqpxkJpXy+GE=","jhKRQUWdLwK1KpsmpC1B+lKkrn0+6dWDoKTM/vb1l5M=", "AZukq3pk984oIdcE4VNa8Q=="), //:\ProgramData\FingerPrint
		Drive.Letter + Bfs.Create("z022RVjBG071HXA4zj4+qYt1lbF0oDyFAmCIVcV50qo=","d6RVJ1QiriiwhFp5gtJwhSIi0WueTDpBpVy6SNZn/vg=", "KPa/AqdX3FWHy9qOd1eNcA=="), //:\ProgramData\Kaspersky Lab
		Drive.Letter + Bfs.Create("RJWh3D//rDubiWQfUSIbqnqG2l4sx0wXBsRQr3/GnsryUCIsUp+mnpC5YOGOr5lo","8Acf8ZSdNX8GxRe2AmAkcL1ziKKl6Exz9To6q2FE1H8=", "2cyJN09E1rG2IkVsKVqhuQ=="), //:\ProgramData\Kaspersky Lab Setup Files
		Drive.Letter + Bfs.Create("vBmeAP45oKtR9f8RPfaDsHWKQJp+Q/hlaXm2dtWyHSw=","CYL6adBrM2YDFHsFAQhtW74lNYhy767AGUByD0uH8+4=", "B7dpAwdBde925ElRcmFrug=="), //:\ProgramData\MB3Install
		Drive.Letter + Bfs.Create("eoWbkeAb1YXuEV0TMpjXeLDQ069ULYIc7LBlBInjZc8=","VKu+tDez+BpcgwsSCfpoTB0ZGoJ8KJYHjt75lbgDx8I=", "zmX4CZq5sZefqbIF5H5XqQ=="), //:\ProgramData\Malwarebytes
		Drive.Letter + Bfs.Create("gVzZikDGbRXc+prTw9U6FlPWfShpQvU6DOSQhbhZL8w=","GeaWa6CqAd5zN0D0TYwxuRH8/PXB+E6aNm6GQGPuoIo=", "U+0MVoVJs8Te6Js5RIP2Pw=="), //:\ProgramData\McAfee
		Drive.Letter + Bfs.Create("/sSqOWzrwAzl+hvAWFKxwNGSa2+8866PPoxlH5ATu9Y=","F/uBTpMKLYaY2ZsiguCXdL98vIhlbsG3zXN7mAs8Oj0=", "7smqH4Eh3VLmdZygAeQ9Gg=="), //:\ProgramData\Norton
		Drive.Letter + Bfs.Create("rLsS2yMn7qoWvxumYwpxAoIkqfIFjvElrRNJ61DcBT4=","n//PYcawNtEm5iFijfJEcHKTMHL1C/sKJ+ux4gUnoUY=", "vO6Kwb86mT3VhH/o11gNyA=="), //:\ProgramData\grizzly
		Drive.Letter + Bfs.Create("vqEHLeEFHQuOtv1BoeiO/e19Qmr6k+KkCQ0AVNk/BHHWgbM94sfB8lPnGfdoAyfZ","qsC4Jk+HmabmVkrWFIEENbexVjQYx1TTwZoEAV/tKoo=", "g1FnEMFcSkljU5x6IqDbBA=="), //:\Program Files (x86)\Microsoft JDX
		Drive.Letter + Bfs.Create("sCUHgl0s+c9hdbSnzJV8tiI1kVKW0nnWyJWlskL+43U=","U4V5ZpYa6gsoHQhx+yc1CncaGWbIkOhgu9tFPbJnO7g=", "zzBeIIPRJJeIzXn88aTqmQ=="), //:\Program Files (x86)\360
		Drive.Letter + Bfs.Create("t8LgNnkd07lK/eoB+bhQEXma0KbmQ0xOxUG2yp2Yulw=","eh3HR1PyynyTerSayorz5/lhsSFjIr7muv/+D0Qr09k=", "Pa+OhF/vaFT/pdIi4XYWQA=="), //:\Program Files (x86)\SpyHunter
		Drive.Letter + Bfs.Create("bLVPljg13re+QRC+QOa5LWKgEF2CZAlrQMJsVD/4KInRU3WliKr1rBq8kuxTrzey","+TVippEAb4wExGVI7JP+H5CjiOcPZyXD5f4cE+9kASc=", "DgxCBwlQ1v6ijlkY++utOw=="), //:\Program Files (x86)\AVAST Software
		Drive.Letter + Bfs.Create("5xmP+eCSG83qCT89UT5Zk3EEG6bQhqry0SfyCvqhHo0=","Y87dOL2fmq/VTNUwYY3x5/fC+5Zh83zeoHsdKW4g5uM=", "36+XMa/NwPmXFU9SIxwhyw=="), //:\Program Files (x86)\AVG
		Drive.Letter + Bfs.Create("lFs3O0UH6HJRvkIg9m/orxs+KzVP1JoQK9C3oDddCCS+OLpmhrSihh4XLC0mqvVC","jN6kRH84gR3HBOS2dpJrmXMxfwBZZNAJZG5BmJxlJwk=", "b/rUt7w7+qOCA2/nNCqDCA=="), //:\Program Files (x86)\Kaspersky Lab
		Drive.Letter + Bfs.Create("u5tol2fOifpUAr2sZwDFEjXowWrGewMymvT9TaXIwDg=","U2ALwnb08g+Q68+l/P+HQiUI1FreWB7KVmAWzfucIcw=", "KNrkyMrBtP+lfkoV2efhRA=="), //:\Program Files (x86)\Cezurity
		Drive.Letter + Bfs.Create("mBV9Dft+pqRCEtCKENCutpPB2IOrQhVJIB2MxOmhInrr3imlEL5G7WQBFJ9MzecZ","w3LEK7nRBbLCDttbllLo1TU8yYBW+bPMfzWkapyOS+I=", "hWlMOkeBIFvxDWh+MspSDg=="), //:\Program Files (x86)\GRIZZLY Antivirus
		Drive.Letter + Bfs.Create("imzx5MPv5exhrSbNVDVjNVuTwXEz7jLZjApWDlV+4dugzT9V9d6eFodHYw0LOxWf","fFZSO4IZGKVosGvzj6KOyGT6ki98JqttmuCCYWd+pmw=", "upSsomdn4boNhKzAZbPMWg=="), //:\Program Files (x86)\Panda Security
		Drive.Letter + Bfs.Create("kH8i5WlcL8wPcfMqy3+tHH41uYc/lFC0VuceR2UbPEJYXU9dcueZ1QOHJhxVcrzo","Ki3JtoS2JdKqOyORmHlIOhbYDgHGkRHsC+laMhNbg38=", "jflUv0Ea4hcX3pKfl4vG+g=="), //:\Program Files (x86)\IObit\Advanced SystemCare
		Drive.Letter + Bfs.Create("QL6MSumRyv18mZzYE6hIc3+Ft68x94zbFTH4tIJuOFHC1vhtwLmsey3q+7fi8T/EavQvbE7e8Ykoo7TNDe/gUg==","SJyOldbOUdj3+yLh6OeCoanpnp7WB5+NHHOKtEQDftg=", "p5c968QVkrEBi7k42I0nIA=="), //:\Program Files (x86)\IObit\IObit Malware Fighter
		Drive.Letter + Bfs.Create("wAZ9MDfvgT2aLXkm1B4bbh8SGtgPAFZYgViNMyCCL1c=","9c5h4YvJMAcQNr9yEw5zy1/UTCf1Y2zVR746iH5BDo0=", "ztCXc/dES3Ex4GhQy9eqqQ=="), //:\Program Files (x86)\IObit
		Drive.Letter + Bfs.Create("gmv3x768rDAWCpZkCu6OKewooRIeJsEB2hw1w4iCSgg=","iJF/IwBGAUtPIClWmY0cSJSssxuz1xY37te1Y0iJrZY=", "Ds1ADXPWREXXKqXEok5Xww=="), //:\Program Files (x86)\Moo0
		Drive.Letter + Bfs.Create("xbZm7SKTkOWFB8nFx1KmQrN9OyouYChgVj+1yQiYCQSe6RmLdQNCqpFourk7XuLQ","k93Nn1IcmYWZQMfMAJcI9Ma4W/Im5zXnf8HYExk1tv4=", "nEWuTDLT+Z4XLySSPz2ESw=="), //:\Program Files (x86)\MSI\MSI Center
		Drive.Letter + Bfs.Create("S83ST8l+dCNpx2iq5gASL1Bzr1vWy1IWdhyk6dhbnOg=","DUsx8RbrF9kcan0E37v5Ck/EijonRQueREFKm/TC63Y=", "g7Ugpe4+XcbpR4sVeefXcA=="), //:\Program Files (x86)\SpeedFan
		Drive.Letter + Bfs.Create("LKaZEjCTKT6iYFFVJ4YMkxTH7PZ1Bz9sLquESPckN3I=","Ulikmdgu51U8A0cmRK2RS0yEQbKUFNgo6qUn7ue34MU=", "mOG8BFQ55ENHdfSJ5FWFuQ=="), //:\Program Files (x86)\GPU Temp
		Drive.Letter + Bfs.Create("Vbm9ZXTXVV+lzl3hunj/hvR1vHDaASh7DsnACqOgxj8=","Ux1IH4nE0OMafWLgbuXAc+kH13odrQmuVmAK8LlsU1c=", "vLPHFQj6iMjsPP6W4XY/Pg=="), //:\Program Files (x86)\Wise
		Drive.Letter + Bfs.Create("rrD5TepvOhl0V2wFcsNjvqIkefgCbSLb/kPwrAPBGxw=","AeQYoZaumFF06JYdMTGnP491rzMc9FywOfu4cYiS3uM=", "bPlWg30n2dIemlFN637wtQ=="), //:\Program Files\AVAST Software
		Drive.Letter + Bfs.Create("3C7wDQtF8SBb5ZpCRWzZCFt1m3dupaTfMhHpILCvOA8=","1Pu3FPjlhxspqwpkua5q/jzj6OV2t15zMdhpXpDNaMY=", "95CGJXscq5qkdSRSLo1ykA=="), //:\Program Files\CPUID\HWMonitor
		Drive.Letter + Bfs.Create("vw6apQdnHevkWLGq4/Kc46yeNZ+Zb/uk9npDd1q7uLE=","3Obc6/GF92+s3kAYQF2j+nFiw367+Y8FQdA6jBmME5E=", "Om1ZRjDD1Y7k+fhFWP8Q3Q=="), //:\Program Files\AVG
		Drive.Letter + Bfs.Create("22I03YBZDfZJecdI1rkc9+9SjC4mXKANnJXHPjw6KQlspl933LPT/IPihpOnmaZX","WNH4dWQtZ9jqDLBjtrm5YDB5N5otebjHrQZc56b2yZI=", "7VDSc6t+sscIcIIwN+vPLg=="), //:\Program Files\Bitdefender Agent
		Drive.Letter + Bfs.Create("pAwmaSN3OAyT0mvzVujcZ7uvPEEYud6cQXdZXupHlkA=","FLwyMPovcHNyRYQ+hX6ioMWqAo2lT8A8vT+FUxhDKp8=", "UHmkAucpryWqGm7kyGwb4A=="), //:\Program Files\ByteFence
		Drive.Letter + Bfs.Create("JS/+8JAcHo7m8WP8kkXPN83DrL5yc4KUNBAoC9U0i3k=","knbclbNPgcAO2g/ViHCgTOInicGVa+q8nMU/L3Z0Y2U=", "10P8oq8icYU94i/Tm04C6A=="), //:\Program Files\COMODO
		Drive.Letter + Bfs.Create("PQP6ITwtRWZhuAEbgxHFgzZon2/4gSJXfQ4P7iTLSvg=","yRByjwVRJbOy4PsDBw5J9y0sU1dLOyXgWo5AS4MS2G4=", "e3EpNZgywurB8JVHYdMeIw=="), //:\Program Files\Cezurity
		Drive.Letter + Bfs.Create("Gq0+dPaEFekptJPHqyISDw03I4w4zf7r2ANwCrMTz5c=","ui65Wsqw4aEjCQ0JAa/YWhxv/8DsxuvC5ZbR37NTNMA=", "WKgAS05oo9OM9a3CI+fKyg=="), //:\Program Files\Common Files\AV
		Drive.Letter + Bfs.Create("q9P55FR2CMUo5fSzvk7QaYBIMOelRg2y9sL9sZ3M1cuO3IKqzUHwXqn5q/EEw+Mm","JjgCdHQQzg9ILO/yzOftbKxyWI7lBq8BOza6k0JuOWo=", "5/QS+zrZ8gwnuKr7vGWxiQ=="), //:\Program Files\Common Files\Doctor Web
		Drive.Letter + Bfs.Create("fCFD0V3qlrdcxnt40Y+SxtvHHNbJvz7oY/Bltp/a/Ul5VyhuQTAC2S7UC/kYOJv5","zvbhUCarC9Zchj5MUyLSujE7nomXPYM1TBWaxLrnAVQ=", "AMzOdnraQGouUtJ13LgV3w=="), //:\Program Files\Common Files\McAfee
		Drive.Letter + Bfs.Create("dBo9DXDqwW99oAel0Cf5Tb7LxtSdCoww7VL1C/MiKcw=","zrDLIdi8syu4cVZcg7f040KBKL13br2FIFvpUxulMXg=", "CT3TsfIlO3hd7H6c9ovNuA=="), //:\Program Files\DrWeb
		Drive.Letter + Bfs.Create("KeoRUCW71rKo/czWDXAR4I30zY82I2SCIHwKgJG/MCo=","3Sa1y4wOResnW3J1fmwbmNReLSJqFbD/cWF/CNwBZNk=", "kVkyXO/Rvh/w3KBWaZsU/A=="), //:\Program Files\ESET
		Drive.Letter + Bfs.Create("EPxEZ7hjGJ7ibjRxI/eYS0IwRUjqaFn/CwfZarqZlQ0JE9PWG6fCyOGN7ENHZvT8","yrTu9mJ1rF6b57+PpBaiIYXPnhtB8bLLBtK5AF0+lso=", "bdbzYrj7WxAzV0wDbn7kbA=="), //:\Program Files\Enigma Software Group
		Drive.Letter + Bfs.Create("2gMnczE+TQ5JEp5P8CNjiSDkfgJRr0iE0pPLfG24JCI=","O2PAtzCRFtURjDnGfrCTK0aArDTxxeuArxfzNMvlvos=", "01QGccOiwrBnvlqW1luFmQ=="), //:\Program Files\EnigmaSoft
		Drive.Letter + Bfs.Create("7ehrDuOsVy4eD9ET9+RrX4Vsy+4XNKwqs/7WFHID5X8=","mlr+g+7HQ1WslmBHupsAw2fti+ul9dklqQrMwSy7VRU=", "82xIn2XogtDnS1tdCZPQ5Q=="), //:\Program Files\Kaspersky Lab
		Drive.Letter + Bfs.Create("WbliZBQowpxKjmNgCVxxO3eRPB9PYsWLzun5AtVv+gtewmTRug94v/DPeOy043o2","kLlbdv0VkPH8Qy3Y3kNAEtrOt5w1c2k+n9KRD/2dZi8=", "YFpTaFaHuvciomDTIxq3hw=="), //:\Program Files\Loaris Trojan Remover
		Drive.Letter + Bfs.Create("tg3QDCMVnwyEv1XY6ur8NYwh1cPz1xm7fw9bPBYW8ss=","PHRo9g5BfE1yk4/D503cVhE8Fk6oJ7fbKNfKSUg5Wvc=", "/lfJF8fmu/1gy8w5/g05hw=="), //:\Program Files\Malwarebytes
		Drive.Letter + Bfs.Create("dT8P5dfEYmg2mlzfLez+8kJc8sigWs6BUWRqbpzjcqM=","BJaKZ2dy39HuICLc1HoKuGkHNcCFmZ5GyimU2NesK9k=", "UOZDII5HvhZq84xeEOu9hA=="), //:\Program Files\Process Lasso
		Drive.Letter + Bfs.Create("LRrohsZFXOu96IxgvUhyv1zmit5ReEeKYv3isN7ZKy8=","t4O+LFdXISybM9bIt1lpXfMM+xc3l/uW6+CczcFSH3U=", "nnKPR8xX//MgCuliRsLh2w=="), //:\Program Files\Rainmeter
		Drive.Letter + Bfs.Create("tG1M+6BfsejJkoFv07ImsgOcEo6vZUk8Bz6SuEbOU4s=","IWlNbx1tqJaE7Ynta3dXixoXYg5pcm7PnjAlFf6PraE=", "PKP6Y35Fsk8Rmf9UR/w0qA=="), //:\Program Files\Ravantivirus
		Drive.Letter + Bfs.Create("IJZwudb+1oF8Nv0jto0+dAWv4BqHdgCOT1pXzK21yns=","ctUo0dknmMff6va8BhEJT0jsXaD9PjTKVQlK9kraEgw=", "8FrhpbrcrU1ELUvzcXoGMA=="), //:\Program Files\SpyHunter
		Drive.Letter + Bfs.Create("L1Tpv+WTisRYYyqcYSVkAYZ/jmiaK1FlQg6uWvu5Y6SjCP+5TnyxeUZC/E2MQy3n","neW6nvN3xSmludKPBI/v9Z+VBKfVr1x95HlbzOSpDlA=", "re5QfLulvtMcCQCkC0ZkCQ=="), //:\Program Files\Process Hacker 2
		Drive.Letter + Bfs.Create("jSaFFWH5Oc+/bPSIm/LQQUKgc3X3acB2UqqECQbzp5g=","pQphaewD+tRy4QlcSM8wSJ5hBSnG+K3waM6meoT+rWo=", "KGTqF8k6eKS7ZsjHmaYaGA=="), //:\Program Files\RogueKiller
		Drive.Letter + Bfs.Create("U43gI9wmZwj3feyl9lYAUSN6etsKwNaKmfGUrdIljtagE14dCVATLBhw2bXX9p8f","KaD1EFx7jI55qh3uu7sJwbRyrsjST4Y0MTNhQ3TgWeA=", "UibTbX/V6D7CXv/nezfXRg=="), //:\Program Files\SUPERAntiSpyware
		Drive.Letter + Bfs.Create("kA0b7Hef+3mc46o6dmy5mfsmM+82illg5U2Twk7bEN8=","F4e4PQ4qNwHUXr/WhdcUhQpzz2latVyGFOvtnPWTWCk=", "AiqCINzNOXb2N3LYYeN+Kg=="), //:\Program Files\HitmanPro
		Drive.Letter + Bfs.Create("HKs7dS4lIfusS8Ger809rEOi9djpR3mVZHaQZqhzDug=","5RWn5dsVwFguGZ3798epkuu2WIB1QO1ogG5cCc4ERUg=", "C3mVzP+7nUiK1ktNYgWLZQ=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("JMssfCwBgT4hnG2WjgwdyeI1nSzwKu1NQe28t6kjHtY=","8LBQrjnL6R7j9sS5GE4u9doJun9X1qKqkMcbVplAcTI=", "O5wrGEaN4mYTGxZMNElwuQ=="), //:\Program Files\QuickCPU
		Drive.Letter + Bfs.Create("s2AVlp4ZxJgrudhkd4zn5G0ISKzyFWUpn+tc3eoQLx4=","LoZON8ayNqTOTNwGV2xg6i+QaGtRWSb24DmJYeVZGZM=", "Q508/McqyIu00aZRlN3qHA=="), //:\Program Files\NETGATE
		Drive.Letter + Bfs.Create("JfTNWJwzNPp2shp5F8bjo9NLYAyaDeyYHcT7cnvc3sE=","3u5hqrICDO/R6nyICqC0rs1pBC/U/caubtUcSgpVd7Y=", "8vHQvyfuVoTswqokcpd8zQ=="), //:\Program Files\Google\Chrome
		Drive.Letter + Bfs.Create("zKKhA6RGWwOOZMboucrSCrVs6byqUjWUnwZJK2XPtcc=","lCNkB2CHApA5UjtPgf4vyMNR9WP9fRpPG/BhGT1z8Ys=", "Qj1FMKqMWCW3DGOr0QR6xA=="), //:\Program Files\ReasonLabs
		Drive.Letter + Bfs.Create("+LOt7Zev1KI5jrmcAHE+cQ==","gC7UoLvnsc9BSZmoDnIedSHPSwBRi/esMxtkhpdD9mo=", "8uhIKCxnx7jhTzNRJ8R5jw=="), //:\AdwCleaner
		Drive.Letter + Bfs.Create("ibDdbHcyMoxw+UfCM8YIoQ==","/dE4hp6W2wUcFnrsoo3BJX/NDSBVVFJ4RDRxynnaRj0=", "9axxcOUsqtCsOKSx7kbo6A=="), //:\KVRT_Data
		Drive.Letter + Bfs.Create("M66b8MVYdGYqhbJT6LXCWA==","mmp0A3aWZcD9IYtjK6TnbmscY6h7jOnXMPCRc+4xAl8=", "IrvzNRQT9gpH7nh8aCo1uA=="), //:\KVRT2020_Data
		Drive.Letter + Bfs.Create("vggs66Xd0CdrYRT1yZi4bQ==","L0Ah/pR4BVkIWnO4y7fVlreOGacj0c2VVACzz/o8kaQ=", "IXM29xC0mR/PHI/5EtKd2g=="), //:\FRST
		};

        public List<string> obfStr6 = new List<string>() {
        Drive.Letter + Bfs.Create("V68WRBVXOpHvaxp3MvVRKg==","Djy22sR5YU0v/7YNXeBBb0Pg5iMMoUx0vkBg7/sGZU4=", "MrxGbq9GnaCfVN2u5L5a3g=="), //:\ProgramData
		Drive.Letter + Bfs.Create("wZKRrZ5I190gKzg/nYcX0A==","2Gor0qUb/vqDGt9RE92A/mBS4whkBiypCT1bH+HT2go=", "Rc5DmqDQ9WocE339eWmYrg=="), //:\Program Files
		Drive.Letter + Bfs.Create("S7xkFo0CNvygGq8FFgrZWYUZVCoYuTnFESH4W8DgcO0=","R9Kie8Qe6g+eD22IWIaViTqlgT0DaTx/DwBkgsZaEXI=", "7S2xrj6mF1Lfz9fDPPdvkQ=="), //:\Program Files (x86)
		Drive.Letter + Bfs.Create("hoWjMCYUL2+uK8Rr3gDSTg==","qFM7hupr9Tiic+L26KeeJsYbUiUtqkMbwJ/ObCySotc=", "LbXD9O8Qcj9Si1OFBunNxw=="), //:\Windows
		};

        public string[] queries = new string[] {
        Bfs.Create("KT5ZThtAz68vIJCJ2rIbkxfR800IKRJkx8799jDAjiYT92Rv3MqGEmIP9b7RmIsgz1i7AJLOzsAQjPdFZVoYTQ==","vc05vu1PIpfd+f98SMSIKDCVQmjFbmQtXt6khEk0AVc=", "rHof+d9CbaBJQwmA6JWBtw=="), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
		Bfs.Create("//Knqpvz4FFWgmNwNCBX3lNh+BLs+f6RxwZ1tCeBOUH8cqFXjDJ4s2oqgp1dJiEv4zhAgQYL1xBvBd50EiHBsQ==","cfIIXZmFk3ewoZvCLgGz7WepICyrSKYGIriXWEEy/PY=", "mpQUMEfu/Qcq5yTln4tHnA=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
		Bfs.Create("3Ieas78+x1i05YksdPvdarbTn82L9DkwndXsGyzNwFZrM8jUr840+YFeXs4ZbOCc9sUy1+bJCkF0irdj0WIOc7+FFYgfW6msjJaAk4zPi/o=","GOw4D8hRERxD5U89NZC4PSFQtaw71B/Mv0XdLEDLq0k=", "FgQZmfF/6TqlAXXXyhJsgA=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
		Bfs.Create("qz1Isp/6Jf6KCFL/OLzP9uFfLiCzGTV4UkFglwfTk/B5l4H9/08hovGwSOynca73qeExU5IEUeAgNrW2/lukSw==","7Wxr//FDrrtNmMrhXPIqZItwcE3JB71qtsfI/uJcB5A=", "PMzkKeUXnky+K+qSeDwCWQ=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
		Bfs.Create("Slgcxcx6Zh4mltseDLQQcxLFyCEUr133B73aHFzaY2C1UKNs/XQrMAsIivRqtSLL","8yKsSMuohMuhPGcF1CM6j/4++bND/5BZ65ew+fLG/kE=", "ZDyIK+iMOuX4ahDYEiS5DA=="), //Software\Microsoft\Windows\CurrentVersion\Run
		Bfs.Create("chaVUEAV/LQkSvG/AOpOYSC7wZhomLeEupcsO4/ldX5BVTzw5bDupLQw7Jwhochcu7gi5DYL4n6/FgLi8ZW9aQ==","Upyb/6YsHltmPr4gZ6YgRm7xlZMGrNDrMQ76B53iK2U=", "2JfdFPZFa3fuyFR2htLzJg=="), //Software\Policies\Microsoft\Windows Defender\Exclusions
		Bfs.Create("K5THfkrh2CrNGQCPjSYcpxh3D62WaS61cbx0MJzxOmNIlBWCgRhwlMJfMRhOjeJAWs13wJTHGHVSDwZ6rUlOLw==","uVYRhgin7bLP8rPJBh2mTh9cUzBcGcTBC+n6I6+oYKM=", "OtgJL0I3yHwCTss7X/jn2g=="), //Software\Policies\Microsoft\Windows Defender\Exclusions\Paths
		Bfs.Create("vtv/JySNdmQADA6S2O8vE0mZ6J7Fsvw1kwMSdFu59qgs+agAnoLQrLpUZH3i0436ah3OELyaCOLxnHa46/V1YLbP+lBkwzzU6WCCWMblDg0=","Pl1BV/NsAVT949eeUU0Zy9M5FDjCfKPklR3YcZkcLNM=", "yMxNDqHV9pREDp6gA30XZw=="), //Software\Policies\Microsoft\Windows Defender\Exclusions\Processes
		Bfs.Create("7rpTjOf1Jqg9ebx8pjX0Waju3CakzW+bJjNceXqn7Vo7EVQxMyI9jNFyMmH3OgWlV6zQ5ZisfQce28QltpI1Lg==","0dXuasggA3/RAaWTeXjnwjrv1htHR5I6ghpDC+p1XtM=", "w5xfIchZbvqsRQXCBY9hCg=="), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
		Drive.Letter + Bfs.Create("vPFV0ICz2ws6AGHjjJhnCpjGW4b+DTl/n621qMxGiBrjiuebBeR8uJJxC47lMk3D","IzyYdY0cmXfu774mO3zf8Mqcp/6SWCdXJtdDxILFSUI=", "4SfjneNzOtPDsY+wIUR7ag=="), //:\Windows\System32\WindowsPowerShell\v1.0
		Bfs.Create("GPbxi9xJfDswIOz5xxxz7XKV8yEoe47D1uO00y+/dPFznh6nnvjRd6U8FpsMETFhVybeWNhKmS+Iu5iwySEZkQ==","sEQWA3bJ+MVFl8C4O6genpEKRRarm0HN+BIpRu0byjE=", "LnpKHuTI+7Qs+asdSp4QPw=="), //SELECT CommandLine FROM Win32_Process WHERE ProcessId = 
		Bfs.Create("BncNr15MhaXwwxYhoEQNMNGqTX25YcR4Wddu43Hj8ReoCY9X+I0tbrJ3r09+FGuJ","r/uzqfzy4V2uKgSgTFnel/KRgUAh+fOjy/yyMwdPh/o=", "MlJhiS2iP0Qc/MlDAto7Ng=="), //SELECT * FROM Win32_Process WHERE ProcessId = 
		Bfs.Create("KVjBczb3qOrgazyUveHLCPabHyeuFajQN0po7+6w504=","m0Ysb3fffA6DW6kXE+jO4bdKG1/xOhyVEw1beqQWpKY=", "FB7MFp4IK3FvwUcIUip0Bw=="), //Add-MpPreference -ExclusionPath
		Bfs.Create("YuNnugkqDqnOTzYDAl/LLxnbw4dTsb/T3FnQ4d7aLFM/5NH3AqBUJnvcu4gHy0Q1+pHO7rsfdOn3/pOr/EmR5w==","Iv12MrlLpCAotxRshSLIdwZhfB59l6LKinbT9TRwT6o=", "7X5vm3aP49C7w0Ez9hjWzg=="), //SYSTEM\CurrentControlSet\Services\TermService\Parameters
		Bfs.Create("dAQjYZlq1ZesND7BEiW14p1XEumGGIAR/jGVT5SB8dKe7hzO44D/1ophbGIF5v/1","mwu0xIDbjTMStcSJM8x/LrMzfwI2BMvI7cDQU/FPXcQ=", "ZUkHrOtqdEJc3M0mxXongQ=="), //%SystemRoot%\System32\termsrv.dll
		Bfs.Create("keC8UaXfUZuMv1B8J+oOvXxgbiTRPiwR352IKLKYoajVFV7EEcG03ffS6RwfjlXcxRh4xkUB7vhXteJxjGmtJykK/YHur53lRzTjXj06T2HqYkwPqy4P8atZ9ky7rh8o","0+adGb34Xc9bGfTAgagwANvdkqFhog4evSFPihEh+IA=", "w6vZGeigbcH5DJIFWLSkMg=="), //SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
		Bfs.Create("9YAhlP/V80V1NK9BQRjznqEGAnMJZ0yEK5sF1Mi+NWSAx0odDpQI4xkYlm4btuWhL3QcV52YXu8ZotCXLzuEu6VjeayxC6QUPpp2LHXlUgE=","6uHYil9+6FuP0LbaROiySJid8eVT9yfko/3HJXVOugo=", "/Lr/9T4ytxju6onAJasLWw=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
		Bfs.Create("xNlHc4YklY7qBFN6eGVs0MmlairXosJtXnr9kODO4FOIhFR/w+j5cIqHbJbSlCW8r08qFxMNeZjksmGod01oVA==","FQ6IW26w4bhpgEkOJnSQmGT7aA0tNTSCH3zl6GKuHXE=", "rfswiM14DBEBMMS+qS8kig=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\SilentProcessExit
		};

        public string[] SysFileName = new string[] {
        Bfs.Create("wsWIiCS2LMcvbefVQSlcPA==","ecttsCPoWCzMbafUqNvM35kHwSgAp/EIePkzl58I+3k=", "JLf5jqUhCO63nWi+v7KVoA=="), //audiodg
		Bfs.Create("l/P0ePuWMYi4bV9cJgvczQ==","D3NG68zaWNj11RRLmFtKhuTMJJ76WxN6sUWwTKgk7BM=", "MIPnLOo/amY/bNdyFTzWLg=="), //taskhostw
		Bfs.Create("79OM5QZWy2pldlxetb4B6g==","Vn9ww3Q8nxSuPa//i0MHNjkVqRVZBkRefSaG/5BhmAA=", "lf+BEGvNsz1yBckxm7L4QQ=="), //taskhost
		Bfs.Create("8QVwKftpGU1aOYtLM1nEmg==","XtfYQf9bkkLdrlhvjtXEJtQDIhEgR7UrMpDGEIwQsrw=", "YW2nWwEW+NCzn2g3ZLXejA=="), //conhost
		Bfs.Create("bCj72DHdsqjWYWgV5kIMkA==","gxLoRsgK+wcLd38TfGchxMFrThHmDR+brX54DCXWVnM=", "CUupr0qM7klIhkUPg81ceQ=="), //svchost
		Bfs.Create("wZWrdvN+5RpFRlIrq1RPIA==","ldoCGMd5+Pl22onE1aII9olWebY3TW6X8JAc90d5PaM=", "uUB9UcJYlFjX5aIThbE8AA=="), //dwm
		Bfs.Create("vzgunhf8dEy9+s2e+MANNw==","OkBS1NQ4pIYeB+SURorxsHtajJQI+hE80QMcG/L3ClU=", "MzxyJGRvi30lR/BiUmBKrQ=="), //rundll32
		Bfs.Create("taLQm4rKFuYOI96N/9iPNA==","WSA8yoK1BmXE8KN7ANxQ9obnIKveAb7MA5COyxsWgog=", "Wzb3amlEF0IC4/og8N/wTQ=="), //winlogon
		Bfs.Create("EeDPtTRge1NdmJupQwVhmQ==","tzXsoKRT82DEJthWPdUXTg+fjf6rrBti97Megykli4g=", "DT0lYKBMaOeq6BEueUWrhQ=="), //csrss
		Bfs.Create("/LbAATrSbreEzl1AZNL1Xw==","O7dVxbhlofHT89PBwrwaeNGOrwVfLiWMZfEBH+5ULpc=", "g242gvQ+5hD8669heCjcdg=="), //services
		Bfs.Create("m/5/0DX6w1mYiSm6hfheiw==","ctaw0Ibi7Z43kRjzjcVO55/VlX4D/NSwdkuDbmcLJHE=", "4bFCMjDFVoHxejGZgZUA9A=="), //lsass
		Bfs.Create("Ze7oU5ZiYNxKlcMfooWBBw==","zMvaMaTljRVlmyZ48+eWSfNi+ddzse2KQAVM/FxOqLA=", "1LrjX4oVV9V7VXCy6WPAfg=="), //dllhost
		Bfs.Create("w77WedChqbex/M7eSfrkAQ==","kMMZXQKPPX2af676oDpuj8DB9UPveN8iRRlsoeKZXrk=", "qr5dLRroir69jqaqDRvIjg=="), //smss
		Bfs.Create("DSu8Z8gqYxwbmKmIhDDCcQ==","JSoU63CtSeJYi35AGmGwRsvExzLezFlcyknLu0BxojM=", "ry9VxQWsd/ezYkHXkhZw0g=="), //wininit
		Bfs.Create("Tfs7quHTahDyjIfcmBTP/w==","eOR3wc2L4IF8XW0n0J6OoLH2qcsbuoXk8T4xt29/Y8g=", "3D3AwfJO7i2P7w/jFcImbg=="), //vbc
		Bfs.Create("5VWzdTYYUPKpfKQp6aCnNA==","DJC83moS+L3SYWAJPTU4N6eOl6Jj0fOwcg0DIHsX5wU=", "32M6wwbhuc+O0p2gN8idKg=="), //unsecapp
		Bfs.Create("K/uIv6DyrwOU5bjuYS016g==","VbRXMbv4Je+KkpWisZTSxrRetRAnXL13jUOxgjwIe5M=", "r8U6F9BE8GkwBIt+N/Kxeg=="), //ngen
		Bfs.Create("lcq7C1JPtYN4WRqSDQrvEg==","+uYw2pDVa1RFCkiTLHJr/X44wVwFrxX1vS3dHQRHfzY=", "gy96X3jv/kHF4eOS0rvkNg=="), //dialer
		Bfs.Create("QnjJqq1U2wD7lsvVEw7LZQ==","WJYUIY+DH5av2lK9s7Au/2aAkY8GAs7Cnv04J1cSpfs=", "QbIVoX26H1dWp2zO1BxcOg=="), //tcpsvcs
		Bfs.Create("0WhDnoXx5K3zUvWcDUi5jQ==","9w4TooWxXuS2sfEoA7KDl0DcZGDYqqzlrW+N5aLAdOU=", "WWugH60KPEyV6s2Y7qgC1Q=="), //print
		};

    }
}
