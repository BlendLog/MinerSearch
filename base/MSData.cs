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
        Drive.Letter + Bfs.Create("aMcYRt1ql4hcanbg8T4810sk5i1kSeQGOwwPacZhHu8=","QIVi+mcfTw67mjMHw2H95pKoQ36lqanzTamyRabnwBQ=", "jaH/BGShW3UlNhHjLTNH4w=="), //:\ProgramData\Install
		Drive.Letter + Bfs.Create("dJexrdR3TkCtdyXBQ07FteKYxGGZdMCNGRJ3YlxVS5c=","rofjUOZPgXI721KRgSKb93eyon3SZn6Ni9Oer8EMrnc=", "qODYt9pXcCi0ZJrRFoMJ2A=="), //:\ProgramData\Microsoft\Check
		Drive.Letter + Bfs.Create("1E3rGrAacEIILbmVhHAOYTpDYPKsB7awkEFSET0GQKc=","CShNH/nuGiE2fr04cK8+9O3/U2oCineoxgY8rjAHql0=", "sf7T8+ovjkSisdrvbI+hJg=="), //:\ProgramData\Microsoft\Intel
		Drive.Letter + Bfs.Create("9zNx2phkT0e6XwIn6xcES975mwIQldp4oiIKlZQBVPu3W7GQMggPEr7uOtdH7AO789DTqrpafOWbL4OKuZVguQ==","PVpvNQ4bdNPoOL9l27zpK1RUCreY+LLvtgcorn69zJM=", "87PQ7RaH/vL/2WOO/1OoZw=="), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
		Drive.Letter + Bfs.Create("Uz4TeCN/DRYWzbstg3U5/INREqXW+IEGzygvNkmS3iw=","ellG6DOEyRyhfDYQSVlOtsK915nlxfB1jjvokqHlrE0=", "bzH0h+CCKnRMrkVnuSUQcQ=="), //:\ProgramData\Microsoft\temp
		Drive.Letter + Bfs.Create("5//CxJXIYN0vLbCrS6N3APt8aFAk6vz1DaMAXYJZhTg=","dIiRDPT+AhNABEn/80QmRngPhwvtk9KpSkTZ4QVnC0o=", "1Bimg/aJ4WDOp9eIzLp60w=="), //:\ProgramData\PuzzleMedia
		Drive.Letter + Bfs.Create("hgbFS+CqmT8gqcvRYzwPjHoSO5OL9qk1ed/I2qQZHGI=","vcEkI6SQ7t0MbfOSxft6zMFAS/3D2tzJHec8+QM9lHQ=", "1kGTVU/Cz7bkLeCxUXXfpA=="), //:\ProgramData\RealtekHD
		Drive.Letter + Bfs.Create("Jx//7C8vYLpYX7fqo2UyxFXLlzD2vtBuV2O9GrrSiuQ=","lOY+rjiwMVyrwRm6VMxuFiXaxRFhDEdHEGDPO1PV0KY=", "EOW0kpvJjpE99csDe39YgQ=="), //:\ProgramData\ReaItekHD
		Drive.Letter + Bfs.Create("9qxFjLlvK3p8+wDEt0sO4dVn5XQoh9T+IRRsUYFgfq8=","RSRt1Km9ek8T5mEWVi2uWuEgN5YZfuD3VlndISLiZA4=", "Np+dx5ObXqdN0+Dv06w5MQ=="), //:\ProgramData\RobotDemo
		Drive.Letter + Bfs.Create("fYy1O1YuPqP2PKTCuyWzzRCiR3hHJf2vLevOlwOdxyg=","92Wl+FkiMAzWtB+1vHxxPOMAxCDc0NF14BO1vVMVT4w=", "MEA1x1618082F/ibs4An5g=="), //:\ProgramData\RunDLL
		Drive.Letter + Bfs.Create("9mFNn+rTXG99o1DIClhiT/dNI2eDyUOPSDJlPp86MbM=","Mr0u9knkSJq3ktZkQvmrQxbIU8z+QsDryT463tR/0nw=", "xuMp3NW9kYTU3l/xQot8+Q=="), //:\ProgramData\Setup
		Drive.Letter + Bfs.Create("6Sbu+Hy2DPnve+XWBHnWI3XlHm11Yf8Iad8m+HpzKcY=","DthFivlDEhUiiiK7a4kJaF+GlpZIEsaobas7T6pfU6U=", "T1blb3irbJSxZc8tRJJqlA=="), //:\ProgramData\System32
		Drive.Letter + Bfs.Create("60EqeX79JvNiPVQpa6Ep4ZRQoVBxChWq2olYiIpbSM8=","P+M3/Vcvw3XqscuzSriRK1gexipvgSxesUOyJNov7+c=", "1Edp9NxwNTJy6kO8bmy//w=="), //:\ProgramData\WavePad
		Drive.Letter + Bfs.Create("MiXS+CEegkN0XP0FF/b97Xqn9+6ha26JtSVCTJ7ygi/7fI5W7a5IlGGd8mY1tpFB","Aq9BINEtgoy/wxsgihciVn00ZY8lsSqw3fzBo47x684=", "LsZk6HIpKHq6/ClMfhc70Q=="), //:\ProgramData\Windows Tasks Service
		Drive.Letter + Bfs.Create("xD69fz2P+c953hmLHb7vtZZuMhoARkt4/ll1RFMQb/8=","MLlcommPT4yrXD6NTlPknxqv/4RuHZf4IXgz1aDqio8=", "ftuakHM7PLI/3GroJOqIUw=="), //:\ProgramData\WindowsTask
		Drive.Letter + Bfs.Create("DNzl8K6J2LwBw1pVtqmZhA+YpYSpQAr+YJaATkUopgo=","OW26e4DUAWCW4JsbnBbBMIhSodlnJzOL90lBTRmuW3M=", "ABgTc4sjXGnNTx043wpRMw=="), //:\ProgramData\Google\Chrome
		Drive.Letter + Bfs.Create("8IFl8qkeP9NACIZq/fokQQNMrYHfWGtolGUIazKV6Fw=","EfYV+oSqD74Qg7GvWXRkqKCOx6xXVKafeyCDn3PQWQI=", "Bz6/3+Ut9illS+gReLumiw=="), //:\Program Files\Transmission
		Drive.Letter + Bfs.Create("ysqaAEj4LKiWRFrJtQmx25T3ZWfOj6kmE79P/RbeXwY=","BsAuYq6m7MjTqm1KoFgi5vPwdXxoj1uP4x7HcOb40vU=", "S3xlwCSjoxcOQ6PnI8MLnA=="), //:\Program Files\Google\Libs
		Drive.Letter + Bfs.Create("YZNUQeN7E1WoqFYVVg7m3DgnZyUSDX2a5LeUrmbSKY2pfsrI/MaeACI05iwobWol","5GPhfmSDvz4UzhkCw+WTwNHLwSxbQWyg5xI/hEcmx4A=", "i/3lX1Io5ebcUMcRX5IWWg=="), //:\Program Files (x86)\Transmission
		Drive.Letter + Bfs.Create("1A7IlxHeXxikAkmIE4Ke7mqBVZ+cqg/64O4qbJMJ6bA=","heg+sbz91zFhaecFqlkDSs/J8D+xy2ee7i/wua3ONmo=", "HptlhkaiFlcrm/0dV4pL8Q=="), //:\Windows\Fonts\Mysql
		Drive.Letter + Bfs.Create("5o9toiDC+uH4PPDu9FxHp69WQYeKsoUUnFDmoai8i634CzvLbdnDUdBx5sRMf38H","KDO8M28lbPuF0hR9wEMzztcgDzFbmfS58dWrxsCNls0=", "v/ACCqwybY8NzjY2iJ09lw=="), //:\Program Files\Internet Explorer\bin
		Drive.Letter + Bfs.Create("adIBLS8sOWtNDaOY7Le7WOPJZd8CzDToBcVVjstgtyU=","rbtqbbf50yOlJyP2MvwwfjgmercU5gy1wpqOO7WDHp4=", "FlMfllu071FIXywRYEHm7Q=="), //:\ProgramData\princeton-produce
		Drive.Letter + Bfs.Create("dAgYACI8HO6xB6hVTjCtF50oEHTVsWURZ/XDkRRCGlA=","lv/3fXGF15bUF3MzghEMlcBKKyTgjdFrQ/O+SD02y6o=", "s3GOBfAF58LEnqK2Q+ApFQ=="), //:\ProgramData\Timeupper
		Drive.Letter + Bfs.Create("qIDDyWn3hxndoorZ6LKAnkMsiBWxabdbavGZXrY/HBU=","fGkKNSidv8Zhr+a+9Y186/TI6vXJac/dJcu2bpp5Egc=", "JHJYp4d9I4wgiSCWcm/Buw=="), //:\Program Files\RDP Wrapper
		};

        public List<string> obfStr2 = new List<string>() {
        Drive.Letter + Bfs.Create("gV6vhIwFVmtKxR9K9Euizy+Vz23LofZyIfvN0Xt5iBg=","4EoHU9qTpxngFl2hnw78Y7ZJYTnw6Bforp0m7Kj/vxM=", "KV5Tl6Utame/pn4r2ah7Ag=="), //:\ProgramData\Microsoft\win.exe
		Drive.Letter + Bfs.Create("foOhYqBR4Lv9OlnZEBw9fwWtNT4YTs1Corpk+Q/7TDfwzHbV7PNOtJDAuetFUXEH","5+FYpqDySJ168G/qa1zBIsLUkllHxkWZ5v+xido95XE=", "Nd0J+z7V/LZdg3MbWswT+w=="), //:\Program Files\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("46WQ9NQsbr0TyieK+SFfBYp5DJCh3CoCkhLVKwpj3r5pceS1swiGVECgp/obQHVg","uGEDdVxWfZQEA+wSg5mXNYAFtm+B8xxzSw0ct8v8+O0=", "qBDCd/v/BmUotIo6vS84qA=="), //:\ProgramData\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("xVcDn4TEPQxVUgqDRr2lpS1HiHc5oVzjlmR+A8m4NUc=","js9d6bpLUoh2+yXEiwOc3T3OjHQlnExwWt7RhNo4a0Y=", "na/xQMD/kmo7zDjBiFlw6g=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("2DfwR5gM1KQX8mjWOF4aTti/DzFghw5KOz4qNU0Z4964UThYzUwZvtSJwX+YT7s/","fPw1TRYLYRgGw35eDwg9NQ79RpruLqbtg0E8MhLeIgk=", "GIQ4lAr/wTSq64GSbpesJA=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("MzlTCWhpSDBFgcmPotCe0qvKaA8aAFX61fVXiMQBitrXPeXeoMEV0FoZVRVtJiNc","kz5JqRSw1oqq66hkEDcgeU2ZBVWZwFD3LZkUfsYK4C0=", "ILegDOUSg57Rwxt0mZV3dg=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("X/eNkIS7cPbhxWgUl8M7IrAocORPLGsGzCKACvwWNnYrwkhYY7kcwafznSd3i8Hp","QKZlnlHJnrE0wYHWsvKTmOr9+XPFmzQIDsmC+jLpa9g=", "pCUK/Ww5LBwVks6oAjPRHg=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("l9asbOWjvoOPxVmN3fq/BNp4K7NkO5Bu9lWkjUIf/SvW3KgxowlrdatUoRFOs9wz","8qf4KdqCi6oORhrU7s62+HecAP6CDF9kGVrbYog3oyw=", "UbC9iFwIv7gXHD4caMTQdw=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("o7LlWGS5wANcaFUe43LQuAPyyT73Y/F5vEL5GvE2GrZZi0xnofyT5pLzXA4kg29q","0FVISMmzz2sBwT1s2loy5hYadNAM73Fo63D60fOm1U8=", "j9R6TYrMnGW6BsxXYsTLdQ=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("BzLTCgskhzMgiFf3F9qkl2qFiIS0b2zdjiOqFkGRTY5qgKajTIkPfzFzh300oWH1","Z/Q6DuWe893WcgM130uUC05pZQb+17aYo4zddaUjKes=", "BC50J5Nv9H0BqTbCZfosmw=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("SyaEZ9btaWAG7Cb0EuJJ0VKLqSHuKoy2uAUFCdvgB4U67npCBJscVLNLiWO6Wq3Q","sBHEjPxsFWFUbsDgQKSuMUPrtJB9wOkX8QwbQI4qeAM=", "U1Sbbdbz2oHSr0dBpTi60A=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("tZdYxE+QOFMkdjBRRVxD/pAP8KpIRFXmOu5+B/r0t4Gocbz0Zhl764J/fhW/p0Sw","Tmb89IOxcsqC+vMKmaSjqR4gA7EBQA9Cq/3cQGnRBsE=", "26cFb5L2mRP6PxTTCSTa5g=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("7riPeaqi3txcqEEUtOhPuwYZnRjoutZDuVFAKMXPhaNUDf5T9re8kMv4Vr7C3haY","KDv8S0mvVcXuEGlAaSMfBG9LqDT+fO9JF2kKTa+vxrM=", "zUnNEw7Q2umW7xI9bS4UAA=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("mitNyslL1DBPdWZnbn/yJa/nWt1RaadFI2GzXS0VNVN/uZtqAZcGFhwI/1q6TY8D","3wGOM3+MElkzZL0Kh/k4xKSiMlunP6e9TsgzuxB8s84=", "cdZsi+fQEpiVROw7vt9OJA=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("DrogMmD9/iVR9VwRV7L7wBqSo2BIkgL9j5hH+67Siyk=","or7miDdMmceManZJ2rVEjnrJjzTi04+ZD+9VGMR5b3o=", "Bt8SzY1jJAjt+udhbj7bBA=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("5RtV8lr47PL7WAJ9tIlnYKeND7JUOhM80j+nz/PVAMVJ8QO4j2kYxEjcl+5Oyh1r","scqz0xJOZ8znoIU/jy9ytBu7IPGGDc7uYNWmNwV8Ams=", "S7i30mv7tgHG7IrZnGtTlg=="), //:\ProgramData\Timeupper\HVPIO.exe
		};

        public List<string> obfStr3 = new List<string>() {
        Drive.Letter + Bfs.Create("wA6q5hb2Dsu6oGnGRswIjcDs7zCclMS1Syvwe85cH3E=","N88jJ0X/RF8LWT87jKQ3xO8gMnhGf8GE6PZDlKoS+1I=", "UAR6e0txv7P8WnQAngHseA=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("6flw93mofteVSlopoJ9HmQ==","HVPz6vxuoMYsaJxrIND86H0z/AVSbf9YqKAl38Ma0TE=", "6TubscwEulNSwqCRdhPr7Q=="), //:\ProgramData
		Drive.Letter + Bfs.Create("7us2tGnfdR6xRAwMeRyPvOIocgxHbQwscTjGSeQN0sR3yQ5eoCeTM0lNVFLmJOso","x1zsKbbUxPJ2IKPJ6scPRlauIvZmZJ3AwyaDu73h86I=", "DlvTyVwNaF/3K70ZWEzVJQ=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("iVSJ7xsjmr+u/EN/b2LrDLLP2FTkixfHn7laJ0NDuxNgdwgxbVNi4pFZLOvr9mDJ","5TW94jrt7ctJRSf2Tko4Jt4rA62TSxGOIgXUhvLLUWI=", "iZrOgJ82yQxUSfqAg4mzRQ=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("WkoSKSK2gu6PkzxS8ODRa3WmG3aKPHhVJX/2DAGosEsLYgOd2ZiEa7slKWG1iKfF","7VHuHf6Som8Mcqgt/qmpodU1/WT1H4RZtPnBrI//2SI=", "dWMT4LYBfWTrW3uRWQflzg=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("nU3DKi1ESN6iQ3vuxGhIg3wbJ4p7iRe13xazHfRvL+B8FUMkGWmaHo92yeywe9Di","nbgxdvEO4MZX3EB4oCk+x5V0aQUt8swVFCiAd5OsySs=", "wDsF9a4QqBi0ehg2GQuzPw=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("awP8E+ZUHBHLmM4hGcRol11jiL7J8dAyGiwZH30ZFsJFPoG/p71nEVssgg+vZoTR","3C3y5X811rd5pxHoNmlXw95/PUavue8N5/XgtPAirJ8=", "pcb9tKx6lpdKtQVxSzmh6Q=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("O8lbT5kNCgzCepC8yc+GUF9okUjk4BGQ3adWBn3EBpe5ukWWyvn4ZTTT3KiGMf59","tineP6jMrRQOYHotgGlC5mZ6uDLsEgL3wori4bDLho4=", "gDf2m0VL7J3WJFJLJRLoKQ=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("18m5dR1T3PiojLEMl4Rgx0rLtqkAKlZTerZ0DaBT7OakhLC1h2NVBsjj/9kfaxpN","lSj+LNoxiQpZXl10ifQJetj6V81yBGewLh2wDu22jBQ=", "KL0xXfI77rJbC7ILLXYTTQ=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("SQXjbzM9+XWMFxACKRo7DM5jsTtPK8q36ivG4uuQvgUKLVAq8ML817bBmnef5bkE","TPamjoNW3pSkVL3T7k/Ja01StEB4lkDO2YYHfJd/8Ew=", "Ox5tY3jO1G3Uk6CAUekUkw=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("b5xFPq7SojoIcIDRMyYHaQ4bPMshHloQ6sERkxEE6IIF5e2LKOaVTRv40cKY85Rz","/55mpMC2bnWZouQ3cEf0X2RV4QkPg6ZQtyKJjoL9enc=", "qivX/vXSHmh7tEs3HV1u9A=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("2nKGTB08Z4GvGcN2SUIZ1lpTIMhtQkQtxM6YO2aSwoyYRjKec06i2X3Ulb9aDdim","hQps5ns73BWN6XsJMXPIbDcHrxh8f+frkcSES17JVYQ=", "DhGYIOmA+FZWaIFZ5AaS9A=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("KHi6E8uxsS1vqS+ai9FE6draq4TsYuO1NJORKbu/ly8=","c0+OHlsGPpTfdfGmXkn4GspAcxpxN3k3XCZ+hgWtSP8=", "cosj0CRpgZEmRo1FGQe6Dw=="), //:\Windows\System32
		Drive.Letter + Bfs.Create("NVFyqMTRFcBTMY+nstuTaZ8b7CIU6phtEKhg7fFao+M=","4miC4esLiGAwPytiTRstsg2hbPbGlLd64NH6zjFGpaU=", "Ut/SGZroHpFxETTWtp8oRw=="), //:\Windows\SysWOW64\unsecapp.exe
		};

        public List<string> obfStr4 = new List<string>() {
        Drive.Letter + Bfs.Create("vV/cR/VhhuD8mTVXGiYzQA92KZUZ62ZJMZ3bY2JuQEo=","Bebavv2Vpyut1YgkZHUsT+5FNUtgq+vlr6Iqktd6ekg=", "+QCB7ST1mS9vSHjzQBC7Kg=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("AuwwsHYbfWTeAzKRXAKZpaNO4ueKisLVyPlmPsq6Tll5QDgots8436A/Cm9Zfnec","qVpHs8TcP6XZZQZAALybhcxQL1CkmL6lELInedUDId0=", "/wDiPB+612X5fUYkWxf3NA=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("g7Pjtl+vFqNLq0OqejAW1YkiN1RpKpwS3R1Qe/bcQLYFf8boy0K1b+2N18CIezeJ","CmiewIpyucfzaweAXesxBI2+85dvLa+A0VNABeDlS1c=", "DLDm461PoR6sp+dBVQHkYQ=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("29+Xrqnb67ymEBcckFclR/K9rxNwmAGJ9GrZOes8ifpwTin5IsIJdy2SZQT4LZ35","Ims+EVdcjggt6cIj+8nvz8Jm/zI2CMrXMWenhvtXpPA=", "JYBATKagOpOpD8duMDqUIw=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("nNUCd7bwygEEXNt/nxNrwM1cB1C401pC7RlzoQUWtFjzKwqyIdPTrg2lUEds2K/q","QW/uPT6EDX8jJFPwE8KNVg17wsr8sjzeJKqA7pmZyXo=", "e7T+lc7xdyFXij62UBc0LQ=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("yUWh48tIGx7K0yhJcTSse3qYhMgRNF8iZ7QyIXLP/WM1MUaOmdASpIC3/FB2ZLW2","nGsuoBA4zxbWt++zIuDFE3V1OTWH0n8cXZcITLByVhs=", "VQQQFU4fqzf3VQKkNTGKMw=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("eTOylbgT0f+qF/Tp62+FblCZzHdlTU/wzsD5NTUsoJCca41wxQNF+Z009BR1+b/Y","jaP5K4J0oQKu2jVtnZczn6V108FgXSlEftW1zv/MKFY=", "0iJBKZTgsa8nzFz4TcSPbA=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("tFmkiMFi7jeFjLQIjIYBIO34bGg+prAzwwTtpv7QgQ68cftdwuDWJYKznqtgSqZc","MNIgy1lAsn4mQvv9W+qpQI10rfn3mj2k+seVUJFGI1k=", "Nx+WXXgVlTL14PCJCCEmEg=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("lnz5XvW91minGm3yo5IS+cjbcVMBzfH3n3VSXP78iXFnWC6IJYwyAquXBIAXl9YG","vHj+/B1HWpdb1Jrlyy7lwqxK7c9ym37AyzhAiGawSIo=", "jhuEn1bQII3exhFNBmhQlQ=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("seZoP+rLPArX94Qufuk9sfWxw+z6lJ7PF3SIVqNFaqMcnuKksB1RFIZaWjmOTDWs","MNB7Kkcw/xgKa+rEP7MAZTaTNkd1V/Pmnl2Wu7BfhO0=", "dKUBG+VZr+AXR5My6FGjxg=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("KleQnPzDFJgdThmKo/j4gWDQHrgOWmqp3IxTlSNhDo7uvHY0kLVe3n4Zfr3qt5lo","FgikkQtt44th74FrrBiudniCS3hDz3IOo3xG7nI0cno=", "ftG4NPzUzq0UAFk6Rqi03A=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("qVnNKjh0bpc/6r9H4ah2H1ZjhEneVoDuuea4H/3hd5M=","wEDo3HXUJy+JYxlTaszCTQeVk8FNfG2BhtAhpOBRksc=", "wlI0fdY4elUSfZcCohdlyQ=="), //:\Windows\SysWOW64\unsecapp.exe
		};

        public List<string> obfStr5 = new List<string>() {
        Drive.Letter + Bfs.Create("wQlSsDrMTWExceHs/O/W3O0fWXZxQx10ogPcyaaQ9Pc=","cy6XSBcagB5EXKQtO5EGEgtlL8lJukNZUiMABuZmaDk=", "8grjsqvq/i51aZh3rICLbQ=="), //:\ProgramData\360safe
		Drive.Letter + Bfs.Create("RNHj2r8jsPpukyB9+vxQjyB9VBeMlF5NMLvz1/Dsqec=","0AkY/L0OdZV+itn67nIs0iyeEsR3eZik872XGYCEtUs=", "kXPcn+xWjA29HNzMbrJEmQ=="), //:\ProgramData\AVAST Software
		Drive.Letter + Bfs.Create("LntQJ/GJQzC+SgRWW5QZLp51zAdnwNj5OWSOdlOs2lI=","eyycOKYMDGro6q+FfmHvoBxpuBRNizW6wNZbhhblYBs=", "Egf9EHdoDPPvI2IithTfjQ=="), //:\ProgramData\Avira
		Drive.Letter + Bfs.Create("RIxRWQxW8wCLvCkANsO9uFwD4cFshMvKupG++PZCQQM=","Br1qEH7JArA7pF9STWRxtjGFQ/qqMcd50G5fv4aTUrs=", "2bKqofFV4HIzCz00nFirPw=="), //:\ProgramData\BookManager
		Drive.Letter + Bfs.Create("k1Rbg3V7TJN2QvzAAdkdCoQm3+Z9ejvelyX/KZAe5eM=","3YPND9jyjbdIsZQUAj9cAq1vzjRPmsdJ82cwabmZyZ4=", "GqPQkoKXBDjoNktH3V20bw=="), //:\ProgramData\Doctor Web
		Drive.Letter + Bfs.Create("BSAsGR40nUksXGPA/XKjTTbHHpZXc/xAsu4o628wOpY=","1qe7OYjSt8xOCVR8a2HkwEm3rGOMrUn4Pipmd0MciwE=", "5fWG/fbvK22sYqnfE8P8qg=="), //:\ProgramData\ESET
		Drive.Letter + Bfs.Create("/6StOdGhO7aJ98wj4IlmF7HtrfHpdBlqSFlXJqED+4U=","pV7tnJUChS3lti1PnYHKtGt62/xh7yMEthMa7TcDXsM=", "sEZA85yAHf1N9YXNf3VJJw=="), //:\ProgramData\Evernote
		Drive.Letter + Bfs.Create("kPPeczVuyfLWHPDZ3z/dp0Kwlym7IFucuhhg7E1XQZA=","QD/4KfQrWPBE8Lcc2j/rrmUb4P+besHePLKDl6Va7hI=", "byZCkaNJZr+bTe19o3OHjw=="), //:\ProgramData\FingerPrint
		Drive.Letter + Bfs.Create("iwU0COoMCHvKyYhlkMmcJZOFlHnh/nxky5ajGZ6Frxw=","UOQTLmYNyZsm+7tAGcnw5aPuyOJl47wDGnnUOAP1bKA=", "SfgAbY+4QdyFEiWwmHuKHw=="), //:\ProgramData\Kaspersky Lab
		Drive.Letter + Bfs.Create("S4Lt+xyoB4PSBelF5fKOckX7vSq1/4ullN7VaQ4iqZBM6tVEk7JtZg4DS92wyVWB","LxzQ/eYXu+eRUhphslj0WCremUYqN/93s0+KltMW7ek=", "SgiWtsd0y4LEqxJLOW7eJw=="), //:\ProgramData\Kaspersky Lab Setup Files
		Drive.Letter + Bfs.Create("6vbUpZm+cZKg2GaIY3/OVMSsIouRBvfi11z23KVNRRo=","li40AgJgu9j5UlywgPHNPdZIK37gy3DdcaKDvkQ4+S0=", "H0uMPLRD0nSvQ7BXk4MFfQ=="), //:\ProgramData\MB3Install
		Drive.Letter + Bfs.Create("8i5F6Qnqw0Gg1fNf+xb6Gj62ae2gLxl1YIuMaB30LZE=","7yA612J41WOMKN5N/DoNdfv6C4Vo0jynR4xltdMYb6w=", "AiJhWKcn7ada5adA+uLLeA=="), //:\ProgramData\Malwarebytes
		Drive.Letter + Bfs.Create("hSOW9fE3bHFepzsP1A/JPTrvDrE5Ejrwny0DfmfoycQ=","KCAqM+aVIiSXFsYtDEx4bAcZiVu2WdXdBt4OeHfFpcQ=", "UfkhSsdo5TxWkMhqr67FpQ=="), //:\ProgramData\McAfee
		Drive.Letter + Bfs.Create("Ra+1ImJ+NGAUhZXMyvZKiQyJc5uy+f2Cj0ujEwoORIU=","AFbPFwvOgmUgTYLPtsFqRf2zwb9tj+rXc99rYujNOcU=", "8JD9yAR3N2xGgiaSpq5igg=="), //:\ProgramData\Norton
		Drive.Letter + Bfs.Create("KyjWtAz7UNTQYvVMEaWeYQsCRrSRmtsojtL38Ut0hXY=","Xch15hnTjsOHH0FjsPAC0V+Weggal7VPvnyYx6Ollio=", "jEsbcYVUMaZjqQgU+GvtPA=="), //:\ProgramData\grizzly
		Drive.Letter + Bfs.Create("WfXdcXvDFUZ7XFrw9adcRfxjhpkYsjK3v2ltiZ4HcxmI7VgRLXCIRjCApEoewqIL","XtLSdeyZ7xUT2eN4uko1eM8NjM17MvNJZ0X8ZXlRSj0=", "4xcj0DX1uU26C1QJlDbNIg=="), //:\Program Files (x86)\Microsoft JDX
		Drive.Letter + Bfs.Create("mXKP6rBQjfgRLZ68bH4tyRixEYT27OVUWlB8GMo4jHs=","V8HSEObaaoFOKInMIT7lqlhnimwytN3uSaRNSBKCpZk=", "2DRovoN7jF+FE7Ao02mYEQ=="), //:\Program Files (x86)\360
		Drive.Letter + Bfs.Create("FTRl25YgteRQdjERmL6HYttPB4bbKzP0Y5eh9v58dsY=","Q8BfyujDFDpLlaYBiNdmoAAWmYRWnfeAANwmNseVuzE=", "lqL0bUy8QCgyGU1rhBlnBg=="), //:\Program Files (x86)\SpyHunter
		Drive.Letter + Bfs.Create("4ZCXJHq04TM5rBvvKMYuFzS6TzVnwTEqqAF0YpoGYfrNlhgY3JyOBVRH/Jz/lV6S","SiiPrAQiIYHNs3qMKnMeqJAKZL5D47kq6RJpV+l3ny4=", "NVtyakoO44w1VLealfgzJw=="), //:\Program Files (x86)\AVAST Software
		Drive.Letter + Bfs.Create("tGd4O5gOKWXce1ysV8wkso7xhHYkuwsRoQccWumzWWs=","DSOJVqRapCn9I6MJ/zKgNWC3m0FZqju5Une7HXlleg0=", "JKxT3XVgvshonaDSkdm8zA=="), //:\Program Files (x86)\AVG
		Drive.Letter + Bfs.Create("TFKROJFEq1tIUeoVaz8UWO3mOD/OyJ1SCMCAeYJKaLPE++r7MKfyBNuC5EM4Lp+/","ACFfpUYxKbmQFm5WJe4hYcIWIKzrDH4pwZt3LdJVU4w=", "XdHRvcKCPcUyjpITyNzG3A=="), //:\Program Files (x86)\Kaspersky Lab
		Drive.Letter + Bfs.Create("AinvL/G6KKyM7TyJitWJOcmN4QEKAOygUzIzF4xOuHQ=","FgDblUxf582G1aQHxHbIpyrV3RwaeGZFPc6DxoL/oHg=", "WyWCdZ/WD24B2Z7yg5mvyw=="), //:\Program Files (x86)\Cezurity
		Drive.Letter + Bfs.Create("kxhY744XM4UUQGafksPtR7tTwbJeoLR9LAKIN2/VhcPe6C4+QU0BkRyZkMRFJprM","N66oSn15t9nCgImhrraKnjKuCjZa1IeV4VDqad/uD/8=", "GFFCVbKhPoib2xy/iUnX3A=="), //:\Program Files (x86)\GRIZZLY Antivirus
		Drive.Letter + Bfs.Create("gCM0lLoJNT/rfpl50SIT2KPvNw7y1TKqpzBBsEoO+zVIXZdsu/7w9ECSMPnWfYfq","Hm+MM8Ko82inMFRXqK92wMsj90lTkVZ9y/1BPWZcCos=", "VfvAOJzpXzjvV1/FP3ucXA=="), //:\Program Files (x86)\Panda Security
		Drive.Letter + Bfs.Create("TeyNYDecgzAcleXMU6UywL+Ev2XA0cz+ImXLIn/MspAGDcV2gezq9YSNY7AGLBjs","hRojWNy8K96XpC95PFFh1vFv28IEU3oSxhCXPSuBU8I=", "Ohkz6JWLfW6MbdGQYSkiZg=="), //:\Program Files (x86)\IObit\Advanced SystemCare
		Drive.Letter + Bfs.Create("zAX/0aurTel3u7Ii3KdNxsmhK+xAIiaQOiDsrCGijh4EbZ8jZERVdWZ87z+obJx5+UrQdh+5iujaaiIqbeSRCg==","pX0Blc49sV+SageUCOkM9vGXzm8wIdkzw+2wr+tK6PE=", "fGFhm1LQNkhQE0Wo7HbaPA=="), //:\Program Files (x86)\IObit\IObit Malware Fighter
		Drive.Letter + Bfs.Create("JWh+gD/3pHPt/pO4dLN3ZUKq9Ipb/ZJE3hdumun59k8=","wGrD8TLSEHHLCythJ2HNPd6tVitCLjefMfXHZ5xVDJY=", "Seds4DdT5BdaN7pbE8a+/w=="), //:\Program Files (x86)\IObit
		Drive.Letter + Bfs.Create("mwN3LKzNRkbRa3lXQh9EMIJ///PrXdxta2fca6kdDqM=","wEg67Xr3Wtj5U8qELH9XYtK/jUIcSucpYxZLflH1biE=", "cpQ0DJc/1cnC6Z7DSk4G5A=="), //:\Program Files (x86)\Moo0
		Drive.Letter + Bfs.Create("K+Ev2z/BDP/F9hv9y6rjSAAocur/BbyDlIagkoHmn+lgxbkyRD+sXTDjRyR7sAIk","93SK2wBINBaIgJDAdguQntRfA7Vo/fB9g4I0ubdeX4M=", "41UMPKkNOZvFqC8DMC3bjg=="), //:\Program Files (x86)\MSI\MSI Center
		Drive.Letter + Bfs.Create("aILOYsAE3S++ZZsPyVpYLiKahs8s15tio2P+aUTx4n4=","EkC/KeD6bJgLRdbtd2RdCDmOds4eX//+X3obhlCeT0c=", "kaZ/7ZwqlqbnPs188AEPUQ=="), //:\Program Files (x86)\SpeedFan
		Drive.Letter + Bfs.Create("4kWWlwfS0CdA3u4PBWIJXo6aG1L6pNs8E3+GrdjhguU=","RPmBDXfx64ZrqOi3QupXXUO/0ITtt5uLuyXZ9Ve18GE=", "JzIa96Vnh6BkSNEiZ1UOgw=="), //:\Program Files (x86)\GPU Temp
		Drive.Letter + Bfs.Create("/JFpo3TOo7Yy99zSaQpgfA1kri022vnPRwMU+i69KMM=","2bqGATyXbG32AbeLvUQraPeP8IvGylJ3iOziw+3ltE4=", "qwYzKgqrZUd1FQKiKAREMA=="), //:\Program Files\AVAST Software
		Drive.Letter + Bfs.Create("M/SunvPvQEq3GXM22SBPwzTlk3poSzvKzEJFOEf7Emw=","Ole01PgNSVAkt204t0DLz+tj9xjXgemhJujZFaGbh8g=", "sPh41QLpfLJg/PpcIYUshQ=="), //:\Program Files\CPUID\HWMonitor
		Drive.Letter + Bfs.Create("gGIHTJXAdAuv57rbTF937jfq41C2XkxZp/p9PdccSes=","iG4tfyDC4+Fo7tGsXw+3wVcVnF5GWSokXK6SPVO66qI=", "/CKLrclJBALraz0uvfXIeA=="), //:\Program Files\AVG
		Drive.Letter + Bfs.Create("62hSqmDJhGT2kuBE+/CmbtV2AlJTp3gQ+RI58D2TfmGOVEQIcVVH9qDreJse7ZjY","zIsBOsfK5oun4fsUnoZ1ghYuNaag7NrHSF8CNwNPU6c=", "f578pKBA00SLpKMsP+vkWw=="), //:\Program Files\Bitdefender Agent
		Drive.Letter + Bfs.Create("/QSmSruageWQHekZ2smSv1wkQqghqSfc1tA3k4UhRxM=","KsxMm3BhMxP/YHoo+M4NsWJ+6RnztNYrilpt6bx3JPE=", "bbcnohVVQXS5uyAhRyWupA=="), //:\Program Files\ByteFence
		Drive.Letter + Bfs.Create("GIjw3HJZ1vE+TY7URsG8ARopSyl5iwyfZjTbc4Nnv6E=","/kDkDv+o5rvsxNvbiltY4rnz0/7UQD5TlRdcXJmjVXg=", "HJaQd9PywZQdq6yl2AZUyA=="), //:\Program Files\COMODO
		Drive.Letter + Bfs.Create("mUZoVktffjNYankG7JaoVGs+S5lH1ctiEe19Y0aQ9lI=","StYkXil81vKsRXwmtZI0yXdX9Gp0LnvGS8LOnwx6N+M=", "oG0jqO8w8UgeYhxZ+ouq6g=="), //:\Program Files\Cezurity
		Drive.Letter + Bfs.Create("olZGJRwl9ig02Hd8kvq8r45GqO8hke0UonH8SDOvHEI=","Q2ySBYn3sntHsaGmonrAAPISkCcZ1pynrz8sx2RJeJU=", "Tchl3sGsf57Y7solRV+fhg=="), //:\Program Files\Common Files\AV
		Drive.Letter + Bfs.Create("XIs6GvS0uMz4RBZms3C7L+LrfqFSymn3oDmE+HDCyKI0brdD9GrPv+iKB/P5PCyS","OV+NJ4xvA3vZYfOORkYe+yDsr+yzd5YXBuXvz7Im7hU=", "LlpsJ6J9QQ0//TCXuBNNxg=="), //:\Program Files\Common Files\Doctor Web
		Drive.Letter + Bfs.Create("TU6wQYnH3+rEUDur436ud3tgDw5i5PioQAy2H73sIlpRyuVUb5omtMZhrlg1CiR5","yej+/gr08VOYUdZQ/lZyDRjx15+02P273AxBrCBXLfM=", "9HFD3SsAOETYOvvtIpgTGg=="), //:\Program Files\Common Files\McAfee
		Drive.Letter + Bfs.Create("3VR2ZzWaHWkI1FNbV2QO24U3UXxoBLdzmsdP/CGz8mw=","bwdUn8KZQpba4e4wgXK9YpV66km07K7LBW4oyph93mo=", "4Bi9QsYNruOpVuuhmigyBA=="), //:\Program Files\DrWeb
		Drive.Letter + Bfs.Create("Un9xurCbz7jDHwJLBGaZv52wsZSqXkQijRZXIpr0mWY=","GmMU0mr8ToNQRe+CUzqPZyDZj1iahhbp6Uil8sYgDMA=", "fGBHzgaDg+097qbmdD1CTQ=="), //:\Program Files\ESET
		Drive.Letter + Bfs.Create("iPWOQF1NZ8BBTzCq4RtcEZ2OI9Kn3n8ViTWOHLv21c3U23cEmBnhQaSCQEY/K1tu","gxuJiyPfGvvL7QwI4bSwmU06fS9+1lNiW+C3gbc5/m8=", "N50Qse6RH3JOIpZk3soEbg=="), //:\Program Files\Enigma Software Group
		Drive.Letter + Bfs.Create("wwRsvE2s8Y6Pn+i9JoaTH/QsszsTlHXhGw8onv+UhXE=","CSgFXObQevE42k6+bt4SM8YMzbdyLoWCV3ffEtee7cI=", "43PHb0EdaeyIByjZUinIOQ=="), //:\Program Files\EnigmaSoft
		Drive.Letter + Bfs.Create("a/8per0YDt+6XgV+9Z3Pjf0OJ1K0G7Fwou7qkk5ui+s=","Tz3SQzG3oC/ITsO8V2lDMCI9IyRExKu97xSEGdujW5w=", "3IAShsHeLOxgrquZfMm9fg=="), //:\Program Files\Kaspersky Lab
		Drive.Letter + Bfs.Create("ie0rOaTFINvV7I5f6G/Fsx+4uXs9005kAISyrm0ggTLIHAnN6js9zylUrIpx9lJo","U1DjoXlGCVt4kGjZn1LTYTwWVwNMaEzU1TsZwI70QDI=", "sW3WJVCdu4w9UdM4yjZcHw=="), //:\Program Files\Loaris Trojan Remover
		Drive.Letter + Bfs.Create("6SS7LUU3Ci/bH21dKFfz3v1KPeHDw5/ZRgS6o+pH20c=","efFQldcrGsKLDeirk2uiSlesW2NXGDXYq7Bb4ODgJaY=", "ePLlBijnwnl26lW7GDRG1Q=="), //:\Program Files\Malwarebytes
		Drive.Letter + Bfs.Create("3d5MW1mUjJuXvZBxOsUDw9z5eYGbXM9BkNP5widQpUs=","DodbT/00W7vOx5FTY5DuLvgSJX+aXLw9Df9o82O+7Fs=", "igOlq2pzymojlnNEtYTRCQ=="), //:\Program Files\Process Lasso
		Drive.Letter + Bfs.Create("e4/3SaO3cdWDZqTalBFz2yusARfzFqcJXKmSLs6/aLM=","Fs5NnfhI+5xqu5T4r6JCryaYLGs3pPr0Ab4BQYDH/7k=", "6lPJiGmtbrvi0x1fUSSQaA=="), //:\Program Files\Rainmeter
		Drive.Letter + Bfs.Create("zKA5cD30+PbB2BC+V94GGa9NORgDRKxeZp0PtyLfW4g=","95UBI+agaPyZstjZdOD4b/susnMzwj6j38zRLtN2rek=", "+GOnXmrPK7prFhukWGl25A=="), //:\Program Files\Ravantivirus
		Drive.Letter + Bfs.Create("G9D7qPhMZgJj6x0IHUPW9vUOmSfxpW/hmQjkejLW9Cs=","7tTse9Jq40I9rLV0NbSmyxhPi0Uqilhw+Cc/W9nmhcg=", "cvYS0bgDgnuy0OGENs8xBg=="), //:\Program Files\SpyHunter
		Drive.Letter + Bfs.Create("toDyUC+YCkP+6XBj+FZqEL0UTV0gdGwVX0GqOOEsreDY0uxQHS1NPgiSeF5D3i1f","RsfhQ3zA2oJdiHKJz19GIjUJ7TQBvBh2ytmQ9hKLfKA=", "NEhikCelmg6QZE42SRwzCA=="), //:\Program Files\Process Hacker 2
		Drive.Letter + Bfs.Create("XEE505UBBpK5PtjeDJoUciWmTIBKKD8pMXssEuwxAqg=","2yluMUr/G8YxZkYwb2CLkfO+ylIuxJWUm+bMmpe3oNE=", "6mA8/Uc9/AxtNuuliQaf+A=="), //:\Program Files\RogueKiller
		Drive.Letter + Bfs.Create("eMzUEK1lUKoyMOiexegDMzncZuMjNkYhgHiTof0NtZlb0JUeaVguBn/j7EGnKBkK","1F3zCaVVqgIQi2psAJaIBlAibbqyLWB8pFjrmaLmUmM=", "f5acRU2clKBrg7Mnhts6jA=="), //:\Program Files\SUPERAntiSpyware
		Drive.Letter + Bfs.Create("XEDzac+xh7E5EfmaMVH59rOff98xzt9HjJ2zrmJvi2Q=","sKXn3VU5vTn2ecgCXODKOACeNyuPjVxo22b101cEZ/4=", "LshzPYaUEgmV539WZy9w3A=="), //:\Program Files\HitmanPro
		Drive.Letter + Bfs.Create("UsKdxbWS/YKBPTh3MDoYlASEYq/LKisbmnjjzGVtH9s=","VWzbd0yNWyQnDS+yFPwzTdE71Z2RYTK+SEif2vjQPsQ=", "/sFVMSucgQ7ru8Xf/SyUYQ=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("tYJEVzfxi/kqXerOU8+j+IguTeYHDp/r5vweE6kfDRk=","YRk6AWas8w/EmYxJ+FPEzbe7ClhV/6uh83jg3XZS11k=", "Lkc4eNxkyi/B1Y8aYmSq/Q=="), //:\Program Files\QuickCPU
		Drive.Letter + Bfs.Create("h5OESYmW5ssMHkW1COdQ9+UZdEVpnEaF/1LHP8jG75c=","d2XqX1hwVFwowGmR17adEH+FbcalrhdRCZqWCdunPtg=", "GjuZiXbhFW7SHGH0ONieFQ=="), //:\Program Files\NETGATE
		Drive.Letter + Bfs.Create("ZqWUTGrM9QfPokHaGz0TIhaUBoP9kKcpgwXPgdzd32c=","RAI/3Uk/Z0i1SnHWCUomhAltOYx3C/IkdiC87ufVrKw=", "33U1Ewe4KqdH48hPfBhKuQ=="), //:\Program Files\Google\Chrome
		Drive.Letter + Bfs.Create("R8tykcdZkSsp/jiHYJjXGQ==","S+dYzbJ9EQr6F1HBEemh9Je2kyCUFTkPmbopPqCaCP0=", "I5tNYV/yxa8W8yNnDIwfpw=="), //:\AdwCleaner
		Drive.Letter + Bfs.Create("zCEeXKMvGh0ylsphJcptng==","7jYxcJ+IbDWn8ttb+fDXCyep0VGyocmlyY1Ndx/JUDA=", "XQ6EKC6NsNdD7oKZ/CcShQ=="), //:\KVRT_Data
		Drive.Letter + Bfs.Create("mK4bIAP4rqiLQQudw6N3HA==","vq/O+TUN/wxot2jt2u/jyPVuZ88rdtsjKYqlsZgQtMw=", "s2Hhjujpeqr2lvG0FUytVg=="), //:\KVRT2020_Data
		Drive.Letter + Bfs.Create("STTJrM2NL9jGXVrA3J3KCQ==","VLAAfxFavU4UNI6Woh4+8QQ7XWdxcDdiX9nPn8jgu34=", "vua87TbRDwUwWyx+KWr9kg=="), //:\FRST
		};

        public List<string> obfStr6 = new List<string>() {
        Drive.Letter + Bfs.Create("b75Rz24jPvos63qV3k4Thw==","9i5hqi3vkYlVRwMzfiuSXhrOeCMKN+dvumUgvR7HaN8=", "Ydem1kzBlCsatkzlV21nxQ=="), //:\ProgramData
		Drive.Letter + Bfs.Create("UPbH10k6oqsydJ/9SYsgTA==","lK2UdIQO3YKsK7Xpp9Znn+4vljWrD53c6+Rdvhkfpso=", "0TaFMERHqzx1j5LmT2N4qA=="), //:\Program Files
		Drive.Letter + Bfs.Create("2bcUxg1Xs92YbUAj8lo8099DYUn0zsgTz2g8qTA/oSU=","BQqOiOBr8ul5/UabsjNpQZTI8kWijNvMCKFXSgi4uFg=", "Lbr0qbCdflv1ndzAVVMkQQ=="), //:\Program Files (x86)
		Drive.Letter + Bfs.Create("wBpbo4rPP/+o8VqyerTm4g==","+bg5Taganwy4gICiay9Lv35/oAbrsJHH80uXdGWYV5Y=", "RB6Bh6EQPNVCsE5dX/y2TA=="), //:\Windows
		};

        public string[] queries = new string[] {
        Bfs.Create("rdkt1CaJCg0G5TX+50RDL1TqxvFKGnSp90O0s3MY3l1ONo+jxhKvjIfmpMs3ctny1jTwpxtRw1U8gFx51YVBhA==","iGgND13jpgXh27BB+ZetD4cSlH7SeES/QSCkRFUQpcs=", "Y3ra4IJNLJDJBTA3q8CtHg=="), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
		Bfs.Create("ARH8qoDtx+w2mjQX6ptlrVprQSjvregGB53hZWQXVfcswrRjtDDlAXyj6g6p0aMUWJuNlwZrSLtwi4jvT4liqw==","nA2lh9SJpn199ZnL0awoql12WdYENWjWWRTVmekq2d0=", "VtJUfdNHm2D6Hr5mzdfA2w=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
		Bfs.Create("a967Vl6EL0lTXY37qp+A15hEn0XZ0L6l/xHaWzNn9mmzF6SGQh+r5POvs1HaJ52szNWrllTx1kAk3ihLdYR8wf4ipcGzfuouxYHY4kcyKBQ=","V7iZtoIv6/9AoTipP2k3WrjHjR76gqx7rDFwLLqxNFM=", "Twom7XVJXfyAZXLwYHlDEA=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
		Bfs.Create("wznocRSHQ/Q8zXr5Tj4dCxasIla91kZbWXETHE1Cij1FhO3IvZmnIamwAPvjTcue64EnQXo2ecmwBckcbZtORg==","ftUc28ltY8ZNq+j0rx19L8Ur03jBZ/Zg33SpoqvplQY=", "Cs6ZnZNISJw0qvK4PQoDDQ=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
		Bfs.Create("da0xBOUGNnpFlHraYr7TiIdTAjEcS+SLM09T+pmYTZPkQejMo/maCpvm1egLwjaA","tWMgE7x+9JRlPnhBTjAvfF6MPZqEk6uPmJdXldvmVkU=", "cR0LbHNdRGxHWCUWe9wxVg=="), //Software\Microsoft\Windows\CurrentVersion\Run
		Bfs.Create("eiQS6ahF74eb8wLOTpJe5ug60Wvyw9OI5FFcWKSdrfdxksZ6z2IZr2VxqM5n570y3PU575u/XoNkiX8Z6XO5kA==","4AQhnndhr3Osi6cs20nI/8c1qrJOzlFB9jedZFQEokA=", "HVDw5fX4yWlmQ/9mBvMPLg=="), //Software\Policies\Microsoft\Windows Defender\Exclusions
		Bfs.Create("ep6vuWFYMq3HG9CglbA+9T3LB7Ui8XCDGRGsM5q4Q39RL37kUYvNUfECw10Kz7NQ7Zjmz5aLQ6LrLe0QHXIWHg==","pyeBG1y6YeEHs7B3Q5pxS2sxCx85qD5GOTsyKnW6F2U=", "3g8Q3YWaIN0aG5U02MirYA=="), //Software\Policies\Microsoft\Windows Defender\Exclusions\Paths
		Bfs.Create("1xQtuoYG0rOzdpEFV4e8um86SWePIl8YApDDxIu+Lfa48resxLY7PkNxyWJS1xsLBD2K/lIakQoxCibAnjGoKHNB2lFS8rVRCzKNQ4pn83I=","TZwOpn5HudSQ2zbRiwPPR1wtsrVDRwohUVvZm6KIRAg=", "YJ6GYfAAeRvlyKIREFv6DA=="), //Software\Policies\Microsoft\Windows Defender\Exclusions\Processes
		Bfs.Create("Bvij+T0BzpSX/FXyjDPZ+jbql1U8QLleTEwtOKlQzQQyW38cg7Z+0xNseIlQq2uP60dCyQ2enW4x78CRDBTqjA==","mPNonwEKOtrsmgIsVjdbG2Cptuw0Z+h9pdktmff6i/A=", "IAFnlnlGY7f20/housSQaQ=="), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
		Drive.Letter + Bfs.Create("gT7vZ7uooq1kGndQaXR4ptMrIvzTobvpFvYCDhrUG0c1W3vqMJLrGnSVw9KwviBO","Inij4Os0XdKu8ssxLZAj7eh6Qydilm4L2q/kuBC2aDw=", "1e0ofEYzIbp81ZdwwP/l9w=="), //:\Windows\System32\WindowsPowerShell\v1.0
		Bfs.Create("aulmojvxJxntjUa7yjJR3/S69RqWivdiqN9REl83OJwlb1ryxkJudLTSu7RLUwsTAd7HuL09YQ5owpJ8DHiCNw==","uqQEmPa6GvCPjVMI4G780fHYtJqnd6yVDvqog5gOfp4=", "bqCTl/tYHEL94KmP1vf0Jw=="), //SELECT CommandLine FROM Win32_Process WHERE ProcessId = 
		Bfs.Create("JVs8W7g+h0pUfg9pQI6j1CIHhxB5CwjK/KoOgt52A/QTSVx8QDTHBsXLSNX2E0K5","jx27FXXCB1hAyVKOsAIH2BSKKPvBTverpjEzTms4KMg=", "smXJYt+hi3DRMBWddXPNzw=="), //SELECT * FROM Win32_Process WHERE ProcessId = 
		Bfs.Create("RPfNXTElvM53OKCWk0Mi4IdU80xMr+bYt/f6RpCeAIk=","SyrZEWUHmGDouc/zqp1/MnBU7aciSvFTMoAh0ViVu4E=", "DkdJuVMFNZH9qxJJCXhyYw=="), //Add-MpPreference -ExclusionPath
		Bfs.Create("aiId454P9QIGSx94pL3AJiH6ju5WUHkG4WBTEDsOOupYdf35qJUY8pCMHYGqFwOf5UDpMRzaogoe5t09916WdA==","1wQiyi1xJG/qC8m8lUL39vE1iJOm8nvfcH7QJJnskPw=", "iuW7UnuUs90Vrd2zsjWXzw=="), //SYSTEM\CurrentControlSet\Services\TermService\Parameters
		Bfs.Create("QFExHVJUU8C+yWz99+VyAwi3TLIFlb09DharTm9l12jsysYtesxbHuFPFFh+hIZU","tGz67LGDPlKcZtzOtL7p8E5w2R9ODqDaFUYaAFToZ94=", "SYchYXeqF08dR2mc+1T+AA=="), //%SystemRoot%\System32\termsrv.dll
		};

        public string[] SysFileName = new string[] {
        Bfs.Create("BAeMUtQzMWODE3pyjY3Imw==","xdSZkpMRem31YilK3JjWUc6Y90jrSHOjQ7GSIavfR84=", "2lbqpFEC5WPAeujDdfVTtQ=="), //audiodg
		Bfs.Create("PevsezEpLEGhwQPdSERyyg==","M9pOjA7FeXTymgFbGFaZFrF4FqKva1BZFnT7+5yavSY=", "myv/SRlLibyrEvFyicrEWQ=="), //taskhostw
		Bfs.Create("QB1Mz+yswZpuOfyt8FIhWw==","q80wlO+xSVEpdjCuaPxHfUM8bT73+XsWbFYKmXlVcaQ=", "1wEbvgcef8NmjMTRRgULdA=="), //taskhost
		Bfs.Create("HHiBk2p9ZZXEFG8gXvp5/A==","Bmc9Mg/5X1HE6NdXxjlTg07Pbodh2EJ0S91lF5ehdO4=", "zb9DvLkAjOFELtM6Sv+w3A=="), //conhost
		Bfs.Create("GLAtC5VgArSes+iWotZRIg==","aik9E7kuy01B/py9PgxN7I/GxUsS86IwaCXVMy4JU/E=", "OhtKE7jIjIoWNlZ+08bspg=="), //svchost
		Bfs.Create("dxtWFIcgD+xKw1SUidj3nQ==","ux085wkEu3Yd8QNTuifBtQES8NwRyzopg925TDRFjyM=", "KFXlQe9Lt/4tlaPQwc0bkQ=="), //dwm
		Bfs.Create("bGIANoUE6blsHJx3Eer2hg==","GCt12zYeD6XZIi2xYRMomthJbvmZ3TQUvQXW/QahPn4=", "PNLVKaMhgHUiaC8aHJ5l9g=="), //rundll32
		Bfs.Create("LOBdyKQpddihwqzVMkeE1w==","V1TsEGUiSQpn+nRGzZhSxbF6d703RiFCD56f2dWaaTM=", "FkTdwplroZByQbRUd3rjfA=="), //winlogon
		Bfs.Create("vpfq7iPt05ZDW49nqJJDVg==","6dsaPrGbK62Phgr3HlJ3ehVC9L7DP2pj+6LWytrpSkk=", "ulB4drIskRPyPwbq432tTw=="), //csrss
		Bfs.Create("tt/4rysMEvSdb/VG5n5sqw==","HRzKUI/PgEvcZaOM2y8u7pbI/agSHd487DVgpwgwqKc=", "XdwLBuOCMu3NqPeU/sQ/Jw=="), //services
		Bfs.Create("kKnoKk3dwnZ+ZGsMi9Kffw==","9jNqnWRbx+vdYmfXKEbyuzJpPURA7r7ANUkQzyI6Z1E=", "jkuy4pO5czy0DbJe1SbgGQ=="), //lsass
		Bfs.Create("rjNDlI+NbXESuflvcCF09A==","lbPT4dAbgcl4lURKYQREbFrlknktzjD9Hby0aX2rpRA=", "aH1THq0Zow7KlFksz4mUPQ=="), //dllhost
		Bfs.Create("iryTry4QsyxnfSrFVvOZ7Q==","0Y7nFvg31MdkDMh94G5YBS/SXvRjsEUdsvf9MNDRo6Y=", "om4UTAYGaqyG5DTgKXKIUA=="), //smss
		Bfs.Create("7ckmmo6+WjxiV9uVWs445w==","sMgUtn+KpnXCq2i0WWHJXtXDCXzEoJUHu3w4m1LIC+k=", "HVe+tUfskaeQBijsSJUgDQ=="), //wininit
		Bfs.Create("9rsxnqWXSUcmqB1OypDx9A==","QdJ2Hfo9WaKl/XL+EosO24Xtr84B0dZ4tmSgmQAGo1k=", "FxBUkUE5RRH0Yoa1ywWdIQ=="), //vbc
		Bfs.Create("yONuTNhco4xwtUgrHD0vAQ==","GH6tclclp2qqgbaAx2B4002EWXmAzzCpEZWxr4+iaMQ=", "PlYv0LQwudBy3BiJBLdSEQ=="), //unsecapp
		Bfs.Create("UWuf27qX0UvV8p8OpMPzXA==","s4TqPqiYKtDsFfNt7InWBZm44F9+gzlwbOkmGUXr0pM=", "bXS0lb0/vzffkJ46OWwlzQ=="), //ngen
		Bfs.Create("nn51CqVUwZMUWcB3J99UgQ==","OeZNEWG8jITuHB1dufqPbqov7yWTKlKzEv6G7lbrkBo=", "9WuAet4ATxA9/nhdsUWCPw=="), //dialer
		Bfs.Create("HTLSSryJWaJInbJqVbs8cQ==","C+08tThmdY4348q4spO8qFHEbw2Gx8G7ybY1xOYpVLo=", "YDi0pAg/qNj1y+5PkYkZ5g=="), //tcpsvcs
		Bfs.Create("H23t4RPOaRsj0rQ7XE3eSw==","TTDR+hw2PUgxtpUUSheQe+iLGvclIOLH+xOO4QmU/8Q=", "WsUPsNwvVy22hQVBCcqwlA=="), //print
		};

    }
}
