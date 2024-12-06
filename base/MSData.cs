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
            new HashedString("4a73bdc9cec00bbb9f05bc79cbc130b4",9), //mzrst.com
            new HashedString("3d62ee7e9bada438b991f23890747534",9), //nanoav.ru
            new HashedString("84eac61e5ebc87c23550d11bce7cab5d",17), //novirusthanks.org
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
            new HashedString("0d3630958f3c3e8e08486b0d8335aea6",17), //usa.kaspersky.com
            new HashedString("9c41eb8b8cd2c93c2782ead39aa4fb70",9), //vipre.com
            new HashedString("f27e6596102c70bad8aa36e7c9b50340",11), //virscan.org
            new HashedString("17baee242e6527a5f59aa06e26841eae",9), //virus.org
            new HashedString("b6eb1940800729f89307db6162706c21",19), //virusscan.jotti.org
            new HashedString("e2a50e6c79e09a7356e07d0476dfbb9b",14), //virustotal.com
            new HashedString("4098c777fa8b87f90df7492fd361d54d",9), //vmray.com
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
        Bfs.Create("lD6JRIUle5jOoq2hA2rnwg==","xoH3MGVL+LCz5hPOeC3KtAiZFzbrX4ooyL5ByDTlmMA=", "HMc6BuaUpTnvz7wtepCoLw=="),
		Bfs.Create("eJmCVaBeuXzLRjnbO9adFw==","TFhcMEorYVdSe6ubcO1EdYRGR+/P38hUbEb4TYedfos=", "IB7muVwDdsMoe9afQhPY8Q=="),
		Bfs.Create("rKPxydYMCPqY8P9Ts4w0Hg==","hohc6g0DDEeDyOXJBfFDB5c2dEHPKkDfDzQMVHup83g=", "i2pvARRK8CPuJXvEhR68oQ=="),
		Bfs.Create("g7WoUZqR5AWTTRaSYvQi4A==","2FRVcHE5BySZ/+UtGgyQzRSAWePe2y3i+DlkicO1sZk=", "dN7eLjs8z/0Zr7iroPG72Q=="),
		Bfs.Create("aTi+eO+jUFU5rFCxcsMC2w==","ASOBBqWhpvy05KQaZgqZLo2894/ks+df5ojn10Mbhfs=", "GGTi45G4LX5bVs6Y0SbiXA=="),
		Bfs.Create("9vDfLR5cFurozf5xb9s70w==","MnOnGb7VPZz3ZxBefp0N5veG6RxwB+Kxh9ICmJkUcAA=", "rpSjSzL1DfS8PRDhyyWuIw=="),
		Bfs.Create("j2Mb6O+ixb7PB+NEcdF3kg==","32hF3ccunDNPKlqYN91yxkpR8TylUDfC9Id2Iu/xHps=", "/pCenn0aCjyyhY0rIhMCaA=="),
		Bfs.Create("ylZTSOPK9/aSalPKtpUNOQ==","lCPUBQ2ch4e5Ci2MMMcNDZXu2jnaJ1/LlQck6i6CjUk=", "YyqGSdOcGKs5OMK+DAJ4rQ=="),
		Bfs.Create("yOhOAOZCxNve3DzivMORxQ==","9PvAw4lGi8LBZRuZ4/hibhQ+gdwElaoA40qiIlpwtaY=", "ZIZVZ+ks3UEOw/Q1Prsq8Q=="),
		Bfs.Create("8mko+UFbDVDImCTFlQzMbg==","ZMgM7lC5K+qaGYpeHqIlE7X3uaEpOBC8WY1/s8TxgLY=", "AImTGacM10Fa77M2rOov4w=="),
		Bfs.Create("kNtlCmsBGqbeiy0JMI4pMQ==","fy9ZZeSyGUE5gHVn9ylj+1rzdRApgr6cgRTvpVRNWRM=", "RKTeEVSo+ORlE9qwpEsKRA=="),
		Bfs.Create("mVN+LXlIW+xHf1QP1SlVdQ==","panz0Kw4zIXbMzuxfkKuwxb6M1B5w1+M8gOg2mhMErQ=", "P8bwFlkydKkdLaQUpmqq0g=="),
		Bfs.Create("4sfG0+ucs2yZJ3NCLoU+HA==","3QByI+ymxqotGh6KfOk4NexwojTKNcOlbtwG6LI52FA=", "Vyceh+0aw3KnsU1tHdAmRA=="),
		Bfs.Create("0CrtcnL+RwhZMRSidFBV6w==","mIohgzJFL3tTSmB6i/u/PZXjn5qvESYKgsPzCMMGoPg=", "qwx7rh557YHJHxmpy901+w=="),
		Bfs.Create("+b2aeAMoFgalQ+46DddMUA==","O+h5QdJsyosZOdF7dJ0hrHyQuZ7+CwVAv6p8rTsNn2A=", "Gxyvcfa0c/xHZlgDs3+JoA=="),
		Bfs.Create("KXXZzEV9H/EGRU9l+76NDA==","ZaApptm4OnhaAbiG2f2XKQgBB2aSiGK7yyH9ghVVeQU=", "spNPcftxLe3bKIjUzcrRTg=="),
        };


        public List<string> obfStr1 = new List<string>() {
        Drive.Letter + Bfs.Create("tmqWBuxAKbELifsxcfwO9y9eYh+YpT8Vi4q2y8lJjxQ=","MgYUQjvuGlseTsQTYzlntlV7RO5sCU3itXCf8v5K+ow=", "hNViT4tOxY1rkfTMcfcc9Q=="), //:\ProgramData\Install
		Drive.Letter + Bfs.Create("5B6B/1ulGtjcWvdoSbCAz3H8COQvPDpEsmCLP4cyA+Y=","nVmdrJ40zZbh9tgiDljt6Kl89pXPGO8AZGVMh/zaic8=", "LdPiCpahPLu6IOjRsip0DQ=="), //:\ProgramData\Microsoft\Check
		Drive.Letter + Bfs.Create("mLtE9WSJ9Iv7LluL69B8upAFVB6zHmCsiQFG4DxKsUc=","n8ByTTLS7lqSYeyIbpjcyy4ygd3Z+QMyxUQQ4feziA0=", "W+lZezyGw6yrihIWUWb0Ew=="), //:\ProgramData\Microsoft\Intel
		Drive.Letter + Bfs.Create("0R45QVHAHeOIAMtxZrRacqIs2IYmLOFmD+IS2VPyJ616QTNQqli4zUYTDdphFnVf2zFklLk2pOGzGTGxYPoOmg==","pgpnGg78Ultjk4Wde7+1OXKimWxB+gnSE46l18uoztY=", "78ROHxOTNlqSZAJYA2DMQw=="), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
		Drive.Letter + Bfs.Create("ebOugpqtIpyOlfXrG/7AuSEzyuzVYuy466Am/nFiCAg=","sg0BTlf3938/DUkonyohlfJbVWDJNxcDX8R5wB+lgl0=", "ubGsbcXqUymUKgsANr0e0g=="), //:\ProgramData\Microsoft\temp
		Drive.Letter + Bfs.Create("dKJHSlTIZBFh4tWMs5g7V9UlmimMub5FEWGnzxwXzdE=","QMQrlPdfmjz5Higk7ijAGADP3bHp9r0fat/DlHzuwEY=", "Mq9s7FIpbW8C+vqsD0MhbA=="), //:\ProgramData\PuzzleMedia
		Drive.Letter + Bfs.Create("1TpTShwqYEBLo61ZgH3ZF/Pj6QpJH9Hj97c1MeaU4R4=","lewnUGRXU+IVUxDdIlDVyi3dozv11j/1DuJwd1hUtmM=", "CNE58K469BKXhLySbFRG2w=="), //:\ProgramData\RealtekHD
		Drive.Letter + Bfs.Create("X7QwD8O8nL6bz0AA7GBw7euF2C8lsSy9QzuYgVQ/CpU=","0yMLFGF+Z5J4k4BTPMxMl0hjuAN5HhxtHjECJVXMDhE=", "dlYbGqDFCP15ouJaPAHSiw=="), //:\ProgramData\ReaItekHD
		Drive.Letter + Bfs.Create("TnsUXjhPGOX1LGP46iPx0GXvexcGdHJG4zoD+ztb0AU=","bzjF8LeLAmn4EFgEDso2+Yi/zRvxxhzg9Dp6EcQ6VTw=", "u9HmKysnohS0K2CxWfR4MQ=="), //:\ProgramData\RobotDemo
		Drive.Letter + Bfs.Create("ss+e0hhCRc29gk8cDOwyuF4uevg+vldFvJ0slbk4JRQ=","n7yyV3iUhz6mFSErUpq8kxYl3Sb4cDG4oJ7doAsU79k=", "zpEQyFGLH9L1ho96ZBvPmQ=="), //:\ProgramData\RunDLL
		Drive.Letter + Bfs.Create("H8a+4+/0tm+7WhX+FHdudXtCyNp425IY4266v1iaMrw=","tYcjoAM8EkYI3COONRoNPRrlZm/ZaDY2IJ9ug6R6wks=", "9yYMm953tOBOGHQUx6Mteg=="), //:\ProgramData\Setup
		Drive.Letter + Bfs.Create("y7K0nJbud/2IX/q2/d2qWVlw0in0gd3adOyBHuqbN/c=","ASINboOAR6INJvp9fiywhuFisH8FWD9STiamnMEooh0=", "0Tvu/f4ViQOzuuyAT641Cg=="), //:\ProgramData\System32
		Drive.Letter + Bfs.Create("C5IImDo2S26NJsfznRP5tNZhv1xWVPYOUSNukMhJxi8=","a/JIdnHICANMbBtEyxrHycUKnET9wa1Op6I9JjKwbE0=", "G6IwjphPolCUq6BXOt7ZCg=="), //:\ProgramData\WavePad
		Drive.Letter + Bfs.Create("8/b9KZGv0vRE3rSTiQSeG1/FKTWuLaCSTk9Q4S0RUzdVpJu5DDDHhpPE+063NxW7","ltVnx2QogcltX8yN37N7nytZLWyusgaULt7ok4161fw=", "DvUrk+j18hpvVadBfDD+EQ=="), //:\ProgramData\Windows Tasks Service
		Drive.Letter + Bfs.Create("fifVsX4lPKALZFV1ncRzrCO86dv6SZR0P+L58UnnYXw=","wJrrl4+ZBll/KKZ161ldH8jHLa7oV5R2xB+srO2K+sU=", "bRvT0aTbP8ig6fC4stZX5A=="), //:\ProgramData\WindowsTask
		Drive.Letter + Bfs.Create("7V3hm4Er1RbCkQehVy7f9AztGJiivfLQY/VOS8cIXlU=","l4wSFIMDrnEaDz3DuSyZB1zMEByx9vFWT/Nan26Wbms=", "yg8DD2kGJNENOMhZXHnjvQ=="), //:\ProgramData\Google\Chrome
		Drive.Letter + Bfs.Create("HqkqMFETAuaq2zeBX3USn1181D470s2L/xmtkQKcxE8=","xc3lSNyG7Gat2ffKAUAHpXEpibf+EezNHXyZpflCh2Q=", "6RNKbio7CEnOndoNW6p4Ww=="), //:\Program Files\Transmission
		Drive.Letter + Bfs.Create("iNnoXjJstLTcEwWCl5MAIkkSAP3tFYgHFs2LbcFEifQ=","M2NgHcafxA5Lx1WkupoVzMPvt1p+Out6WsasszIskBU=", "HVmVcL0Tft8CCWsUgzYdxA=="), //:\Program Files\Google\Libs
		Drive.Letter + Bfs.Create("VO/tsDjficS4qkdYEfjQslFNZaBw6w+kN9IYyEpUaIVQgbi1Q1JqWcFBjQLmtaOg","k/503r8S3a9U7fJS47MX5LUQhspy9yZ1TA+2JkXO7rE=", "h0F6p4srsCz6+3DQ9O7XYA=="), //:\Program Files (x86)\Transmission
		Drive.Letter + Bfs.Create("Tjd/XhXW1i5qiFytFSrzppnBVY/5TquGNlWxXFUyKeU=","XA4j7BeL57X7Iu9H4uv5pzLS4V/J7rSRtDqLLt4FiLo=", "B6st9V4Q8R6k9gfXoaY1Rw=="), //:\Windows\Fonts\Mysql
		Drive.Letter + Bfs.Create("HEeGfJOGyzBo8dcRwm5oK1rNetILT5BHcQ77p4PyjMg8Mk7dCh0UgIxfTZLi50ch","c9UpYDLQTF5kcJwQo/is7s1BGXl0Zgvh5+xL6RLB9UQ=", "K83H/UQd2h+INIuesah+jQ=="), //:\Program Files\Internet Explorer\bin
		Drive.Letter + Bfs.Create("Y5U2Ork55BQq1K/mKn89Xn9xeA8i1RSt5Zm7FFV8vrM=","APTqEaU6vI12Xc+XSYe2Cvn/dWNvRKsgbpjIbokgMxk=", "uYBi/J7tgHrZJEB3zGP16A=="), //:\ProgramData\princeton-produce
		Drive.Letter + Bfs.Create("tuibvKO5nzmTVUIo6ck7qcIHO1mG1OEV8y/DaHmCqK8=","wD6MZqH8YtlShLvlmc9xpccq9H3IgTJoKdTDTJnhQQs=", "NumfSQSVmMikZaB04+xFaA=="), //:\ProgramData\Timeupper
		Drive.Letter + Bfs.Create("WeL5Nt+taRpmdxW17tNo2AbT0FvATZ085CCSadLksdc=","s8tq9uQHpqqkJH5mx7SbQqhmR3RmurRgjssSTI9EaII=", "8ntzRwV7LoWfnaDdJi5OMQ=="), //:\Program Files\RDP Wrapper
		@"\\?\" + Drive.Letter + Bfs.Create("F4I8PfHEXvLbHBvYOGUUxnAP6Ru2E5Y5T3GWIBAsrc0=","ba4uooU+h/bHS3ptVtUahys2l0v2FH1b5NmfwpuQiFc=", "kA3Y9e6E48beZ7tSh9DlPQ=="), //:\ProgramData\AUX..
		@"\\?\" + Drive.Letter + Bfs.Create("egSREv2cwx1ew+1z3SQUe1LFfR4t1/fywVJkBef1p3Q=","9rTGYTRn+MK3p5ffsVMfJ9IDLk+wqHtxO++Lb5z6izg=", "ZB1HKio8DHYso8eHgHqUvg=="), //:\ProgramData\NUL..
		Drive.Letter + Bfs.Create("I05FlHey5pgLURO4Vmeq3jtzILA8sC5oKD4DWsCA11I=","OUSB6Du/i4KTsEC/QTvDF4PdFL0dALHmIb2o9qte0ao=", "2eWdTAd1Z6AVoBCi57DRgw=="), //:\ProgramData\Jedist
		Drive.Letter + Bfs.Create("Gcou7LdIDgwbOPImCbZM6N04g12VWt/NRftztI1wgxmrDDL0oSkyzH0whYMgePgCxoNLRZvjRjyYfYrED+/YBg==","YRWpWVGkUKMxmsQvJ4uhwJZAjrb5kfzDovpVu3wADuQ=", "02jq4Dc3XYRJEYw+zSwj1Q=="), //:\ProgramData\Classic.{BB06C0E4-D293-4f75-8A90-CB05B6477EEE}
		Drive.Letter + Bfs.Create("Wo3KFY31tDfCQMNOfQN3USRpGovlTAQO6tbGrjjAW/vdlQcgRzYNV+MPXDkSQi39SwxfqM3wOsKyixGICLA0ww==","2phvc0Vb4cc9uG/f3mzQv8tA97zkWSnsfcKw7503/Yw=", "WZ34TpY953UhgjB9btUDDQ=="), //:\ProgramData\Classic.{BB64F8A7-BEE7-4E1A-AB8D-7D8273F7FDB6}
		Drive.Letter + Bfs.Create("4TYbF+AScsuQgHfnw7M5l/y4J8XUBre6KHAPk9VpxUc=","VjRC028j1IZS7ZJYoST19E506BfKJf6mUHWAyzRhJS0=", "IOOAf8I1mptRNdWYBypVVQ=="), //:\ProgramData\Gedist
		Drive.Letter + Bfs.Create("cFJUCeZiIqkYheeXVw/8MjH88rFcxFLfdaTQttO92VY=","3YPzdLXJs2F6acXp4TPuJ/4IovUvoZ2bPFRoDjz2s9k=", "2zY9gX0PEPwy/gPWrATGtQ=="), //:\ProgramData\Vedist
		};


        public List<string> obfStr2 = new List<string>() {
        Drive.Letter + Bfs.Create("UoagQcCdu36QqcAfPwG3k2plNzi1O23Ep94rfuwlsQE=","5qq0CC7mFx15/i+sZxcwH+U/LFV2mhyfn2D1I8xkkYk=", "0OddUoZzdo6Ydi6BB4NWYg=="), //:\ProgramData\Microsoft\win.exe
		Drive.Letter + Bfs.Create("mCUKWMLlPgLOcBdOaWBUpLpTNXlFzam/Cus9N89CyndZdelIP1c3tbVz5Vo1mPa2","nyjTnmr8ReJ62RrlgYAsQhOBtSx0MwSu+02Y/rrXPo0=", "Pt7o4i2N5ZvK6/alao3g8Q=="), //:\Program Files\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("lXXyMSEgeFlcXxTTUoBA2gjX7vip3I/DSORkNyZ7e77MJddk/iy7sU/NdW4f7utc","dadQIpj3JW1RJzXMKi0itmoS/sKfnDMDxWcrm+dgRBE=", "co46m7s/qWpN+uZovfCK1g=="), //:\ProgramData\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("x7Uw8YBCJaupYo5LASbQymzNoLZYcFC7PxCvESeAC3g=","2FqQSV8L2DOTO+6a88phO9R0GJdCz6sQ8xdbk97wTUI=", "7XNqHuIaxOqM8lebIG+TCQ=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("5bOig7KW4kqA7UEZhcw+iwikHYttPaJ4/E400pdXgCCO6gHKMoD232eNhF+E6ZyC","pajQICQHYy1Ovw5rTzr2kPU6LYNW4zCINGJbSNdc2VI=", "4MdcahMW5FZuO27g1fTdaA=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("YWbhYVfGXHIgHCnxB6YQJw8Ve4bRz9x7NNvEuN5svueJ+QsBkDglHcV1bu2mStS0","XRpODKsOGOeWjDR/BeCtqKqAtTjceMap0OjxpzdHfxg=", "cvzJg/GoFhuQb89Fr1UyGw=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("yNGZ+q/nd0BfHWMAOBz/PRrNQmfiVAOSJ574vF9jwHFH/ntxREkZalylzQ5CLYlT","FzJeMb62maIsSvvvKWkYmmSzRXnHVT8ZXFBFDSX+MHE=", "tQoh4gDpGzbfP5wbeRlFqQ=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("8AuWOJgwPnA0jkyJaJLQlUQwwXHX/94vf6QN5UTz/vZavHhYjoE6m+6iquW8FeZX","gT99KEwMMKvKwzKhqpQF1fZwJyFX+qfvz3wcTSOgdw8=", "mNyEArlz9ZLuEHWkuOCb3g=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("dDaQ4wd5vctWz8jC0FVDlTSaVTHH4MD3y875Y5VWaocbxJdh01MdDNl14/vnuGSS","+V3ZzwwhoZtk2Hb/u9iFa8OcDNk/apypQUcPiTpkq+4=", "C2crHrQ7aKpmxtYoOvvQhA=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("O2N9+V62gLnEu1/uyKguT4gjOIDeb/u+hkQO81ro5IGTynfW7syXpqYvSo6fa9Ic","tF/oAjGHgBuY7MwaZFSspQigE/jag85cKHivMHfx36c=", "Jao/AEp7fwQLHnOqe+Hugg=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("1Otj+NcYjdg7WvE0oYEkm7yQOZM8KmN3hh6js/CqjQz1qkxnA8jOcgWO44nPkV6r","4KQFkThqb/84zxT1p32P6VrqcxbzoeA2p3GQmpbD+fw=", "dboSXtkHJaL/X53dwu/nLQ=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("172ZxeIW8Z7ljxl+Mc9eB7p4rLE742t4vvSoDi6hvVBmRevYIIWgD7FNKzC+CftB","V7n4zqlOIddrvXoUvdt9AwtSMjtBSG1CiRvVT6/MQyk=", "7khDvClZkgwjpgQcNTzO2A=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("EDqxcx/doZEAzB5Rxmr8LkBkerbjv2tdtTOyY3oW1boUimXf1zY2KA62ucnYGKK7","Ey9S0iUXHSXzpK6iAFa0/HIiSOtiVVC5D3F7JafQpG0=", "NupZLQtV+jBy13wkHTLVmw=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("A7iVlwkzYb4BlS4kHzmLnI/2nWn57/ivHuv6xWoLOH6RGCCdtrBqnKXSOIW4hAZG","uBKdrH+/W17L1N4PFB6oAxC4eEmZ8pdJdzqNoPbr/Cw=", "zFGisSfanvnqPo2nnQn5Dg=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("wpZK4sfe6Nopne6hmnYtj9m/gk/AHMDAi3vALa7YPjs=","AlX2okXyUkT7KCnKnGXeWkgT1QEN2dC1ylsgGtLBUHc=", "hwJQmP12Aq2QeqI+zoZ2lg=="), //:\ProgramData\RuntimeBroker.exe
		Drive.Letter + Bfs.Create("W9vTgcXPfeuxY8MVuhfn1I3QtO16NhdxIjeGqC1Xp6U=","Sj6p7M77zJNQmxj2YXsJVjpclzENX+sEMKqEBElMIZ8=", "6WC0CgrcvM2DMWE52Yjc4A=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("LJqbyS8bSUD+TWEIYcunBXkCh9Gneo5Or8+5UJ+Uqeo=","WcFwLyxsWSg98By9+POsT27iGgzGtBPBWTM1B8YT3/A=", "MzFsn7t8idL/6EJLmL4Idw=="), //:\Windows\SysWOW64\mobsync.dll
		Drive.Letter + Bfs.Create("qWh5SRbgC4QgFtVyxX57m1yCRKCsqFUiBcI9duhnp2E=","iM9WAHkC7XnGQKypn8R0UqF4nAM9fOh9J8FKO4sCbww=", "+v79dPQHoKKELGETuLROSw=="), //:\Windows\SysWOW64\evntagnt.dll
		Drive.Letter + Bfs.Create("N8+P5nPiTLiKaMWrnwC6ZZO5XpRcopH0vUFzQ0JFhjg=","5iO7d+xEMHAGweH6QfL1bNSKy4u/WujnDDnNAm/D0YQ=", "puJXQvPvhRrcd+wUbesdfg=="), //:\Windows\SysWOW64\wizchain.dll
		Drive.Letter + Bfs.Create("X9NtOY+I8aPKjhSPaZSccRO1N40BaHgZMPI4EekvRGE=","ZtGOr5eVhFj1G/9qEla42Re5VxeIcvathjhmD2wJwsA=", "9E5K/EZ9BsBCAuIFN1IDag=="), //:\Windows\System32\wizchain.dll
		Drive.Letter + Bfs.Create("+O9rRLRlMPp439iOu/0oN8vp06LRGNmbnxS1rnl0yLc7Bi8fPY0R2CV0vCh4imGR","rKFbgC2O+Fv0F7zygfEHQG5kglEivMe07l8kA+dq7F8=", "bNIX0ZXsMcAlwuBS8wEldg=="), //:\ProgramData\Timeupper\HVPIO.exe
		};

        public List<string> obfStr3 = new List<string>() {
        Drive.Letter + Bfs.Create("J7+OZU5wbtXNxPd+/PlNDE5pH38gk36FhaxCG2xDxCU=","CuHX0BO4Uj/I7zF3WwwLpfjpXgL6hOEe0M+IMYTBoPU=", "05lDEpHZPvCjcu5px6fMtQ=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("L3xttP0vkwF3GVVscWl6hg==","i0oKIPQNX1BTN/5lFKtQiPDSi2xzxufxI/vsHh/hl70=", "Iq8MgImzXuEi2tkD4GN/RQ=="), //:\ProgramData
		Drive.Letter + Bfs.Create("T/RHjL1ZrH/Rii4QjImeoyNWWVd2ajV6GY0gbh92YFbOTP4h7O+WnOjVHzNKkYaL","hqoBQpUu9bXg37hwQjUWRY189CZ/iKAOJaGgutfl/uM=", "pQl1gvPseFFywMIKAEVGLA=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("VpK6LXgSWt2Cb1E9rlzrXprfs4mdlVecOLCdyTJJOIuhg9HsKhx1v2+NFOM/RV13","Tp9rKEG7QpG1Ep9uXcVJThAxAgcJ0mnmTuS/nICLxBE=", "zsGyWJJHDmRi/jHt0/8uJg=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("tWCpQORMYJyay8boSUxsxyYyVX6Pnch93VMaxrhcA6eD1dOD42LYFCwRMlBCMom5","qbkiztQuwX/FkYawD+p9YE1yoST4ix++N9m++kZ65Oc=", "gGerfTbWAf8mL6oAxzpYlw=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("7jpvKKAYuC40lMb2RrQYICUe3VMsByPaRAreVno86DLEgWnUQtaydWuM9ANN2fO4","yS+Qp7FQENX1No4a+L6HMwCEMFoGdT9C2Y2F+LIVVq8=", "OWRgXofhKTyXsWbCdb+nQg=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("iZFOiZyJmpjpyce2qZoXtbiVwUly3kTPkAt3jQvo9kizsXYA5zeI8Z/0EpGujxHr","XNo+aLMtQzIyQE5VKnHZMI8+iO7CYGz1PB4jeyMx08E=", "FU8MjbEsLaRXI81WfUPVvw=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("eIvb3LSpWEZDGOHYaPFnkzG4YB8FXB3oyP/NkIxxG7/hYR3YHwv3crmpLhgVre1Z","oFn6sv3oRX528GKUlkpgAdgtqN12sIdBG65ZJvTCAMc=", "8DOgNRhVhZqCKhoAM5f30w=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("VTnhgBSExrFdvdMQ8lwtKvEWlr2JfDzeidvs6vg6q5JVBTEDxr2w2MLTMK/TELjU","b1PuI30EWtkJ0GOxIXBHBaHHMsVEDFtmdRu82NXgGJE=", "OspvgKNE/ntI++UeC0artA=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("G6daBip/m5dp7jYnu+56Z6c+YQxf1uEXqGRb84cBSj/+ZMsPGgyOORnJBMVewLXv","CuHtsVF9u9+J0NSqE4UxxVJB8uBqS941cUOsFSsMOic=", "/flopZunyWYBDCGvVhdPiA=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("mhe3FfR0XsiRYC8MXASJZhy1AecTtHGPzqrWZi+wdBu1KcnNEL+gOiqM0IMaelN9","bn2xy3ok4D/8/nW+WL9fFRR6LVYndBupWXH9ifpGo2I=", "SiH5K7O4UcQfaJrbto6OsA=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("93X5DVl22AlOhOgeEXnzpiKKAeQ0YKYGbFkE1fb2z4wY1icvdtArH/Q8elcx8Ozm","bakrbzFgAaG3VTIG511ToXzspgEisZB87Bkx1apS+CE=", "9r4sRn6YKX+mospVXRw66Q=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("3EL/f9B1N41Mhf+a89O7hLh2F4zdVaXCGltSuAvPE4c=","F4BGVI7rYKf2aFtaUUGvezmycQXFi0/byFk26cApooE=", "THSYh+AAaTYpChqHH2peiA=="), //:\Windows\System32
		Drive.Letter + Bfs.Create("XBHjD0g5LaWyf5Ixys228kv4OwLYS6spKLTSTnM9Fqc=","z7V0IfkdjP7KA+i2ty2jjT1+dexFSsSsyqUCFJv+TWU=", "5d4eoGU7K27OGVdDd/SxRA=="), //:\Windows\SysWOW64\unsecapp.exe
		};

        public List<string> obfStr4 = new List<string>() {
        Drive.Letter + Bfs.Create("xRJIFhMlqSlajNjWCsW/sxi/8+dyGtgb2PTyMnJafIU=","++5g8BMNM/CfyluzLZNh4ahNuPuYIMV3cyQgF57xqnk=", "nNSZww4hBEVUBLqDsGH4jw=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("2q+xS64G//PZFG8yK+8AVTq5Ss+xbN7bc0xSXJurk1aNlCIhZ1yY1lK2yxsuY+8U","62mHiOgbnQl+FGgrJ9TFfEr8aDK8hvB/JS0nJ3mcna0=", "aCcZSM1BA/hL1zwuXzWbbw=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("1IL5F7qDNTYWHVyE+x8pxy4ip99uKmf78FeUMKvf8n6MuSiBP4N9rwh4v+h/5mCr","mXASEmfJZYg/VwsgChsypEmFBealW9NMrqNIH90ieBA=", "E+xOVxDOOvFoXaxHtaq1ZQ=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("gn2+JWA5P6Z3La4cKoRaTpKPpd6ixFVnuhEvl1W3Hhj5x8zR0VmkbtrEKpSInK6O","g6wk4XBm+LmBXBBAhzpXCd5wZZb7NXqZVRKjzzXyZPw=", "zuMTHjP2tj88pwSHsEExZg=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("2NPDbo72U9IuTVTHW+V7dvBtb5X08fcCdqfF4VZJRQYEpUPNmMwjmTcABxl6yVPn","7Jj46DPNrZgtmFRCNwcCMWm+CHYI98jjHSkEk7aBEUQ=", "CrH4t+B7xtp8ADcaAYZx5A=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("K730NNBR2alMxIaSBtQNPEJOaRocENlTksvj4pvuL8KtNnqs9WM0N9xQ/HXcdxAe","Yey6rci4B/+GUpYuuZgrGDoAAA60GojQXfDwcfdqmPM=", "Yjzv/5V84zTstrLb0MlRsA=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("cRsBOuYMrFI++jybAPoIkake0bklRMQcKqkJvW0y4r+RiCXIBgJN0Lu9J4kt1aY5","nCW1j0DOdPPEFBSJMWBn6C/UvyxR7026LZ6VRaOFLx0=", "DETWQDbkrae7SWV9hj40Og=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("6vESaZJYWe6AWOt7hZkv3nPep9cqSgGuLNtgG/MFDwaIBY43dxfRc+hU/O0uznwz","QeM2VFVGN63kmlaEurD5uV7lD+pYwXRf+UHJAO+BcEQ=", "JLCPKSxXhOtY4sf9jXMwTg=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("NAU16e8RScQZiVcJMk+WVzX4JaHuTYtseMgBqqEeH1gWfZOKQuBMtjOx9dbp3x5v","hx4wvafJ4MpEl9za3aBpdj4sDHV6l4qZYLAmIFUmhJ4=", "uKwYOYaQeZRRhi+Dx3LffQ=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("HQ4C70sqnZ0u49j+FLPkNosXw+0/UFd2YOb1bz2R/KIwZBw6xLQXqI/b7k0suPXe","0zErFq4ZTwICwBFT5aUOlo0pfQeacg9VopTqEcP+Dag=", "1B8N2f+cck4uHmvpIuGGWg=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("6HmNLsLw+gLw2pP05ohdr8wmSwYfzjYEpyDZ8X5u1sxGl5L/CNk+nTMo0U/Yk1aT","UEdfLm9O8DswPPc4lvA3YaiBTCt78GB1UN9XbETs3s8=", "6b3ZJDgRlqqGPY9eysbigg=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("qPoSDF60o62QuvNNC5e+bPZlzUAYhPVNMT1XYh8zjFE=","VGpL/NjwmYoqVfwTSY/Y/4+ReWDLak4UaV4xkAj1/qA=", "VADIXbsALACL34NGtBiGow=="), //:\Windows\SysWOW64\unsecapp.exe
		};

        public List<string> obfStr5 = new List<string>() {
        Drive.Letter + Bfs.Create("gjLn82nrao1mRg8h4UZBIdQZ6b247aIrsHDrDEzeOAo=","Eh/3VbZO7pPvwoSNyWNlHVWjZVNs7DqlhROOmwfGzWo=", "xowgbwc7Sa/zhYY6oGGbFQ=="), //:\ProgramData\360safe
		Drive.Letter + Bfs.Create("FtAew7S0Cv7bieCTpoiZzlu/DGIXDV9KLoVyd1ba+vA=","1LoOebMGIEVP+EJxCE9PVGLes2/vXoZ89F72hxtaaEw=", "Ij6540Mm5eKvzec9TmlRkg=="), //:\ProgramData\AVAST Software
		Drive.Letter + Bfs.Create("B77WU6RD9eSHyjNdJf4emlrpMtPjB3GPTxotgo6sW7Y=","NA+Jnp4mBUqxPRL9NATM1n48vLsTVCTar2tGZU/S2mY=", "5wFEyMb5hhKooCGGzMF3+g=="), //:\ProgramData\Avira
		Drive.Letter + Bfs.Create("6PGrLlOruHf3hMkDbVPXHAiOpb5wvRcmkWjsOYHfRso=","B3XFs1D8pXZsbqaiIRT4Sq4+lutE3MOtZus2xpa7yek=", "ifHUSukfJH+cOHuJye9ycA=="), //:\ProgramData\BookManager
		Drive.Letter + Bfs.Create("R7BcdFBlX+ujcE9W0gwYMFIvt2tS1E4A/P8LBdKl6T4=","Kth8xkbEWtkLconNj98JkS8zaqtDTrTEbH/pA0h8g6U=", "+l2E2pKbS34oghTS1sFOqg=="), //:\ProgramData\Doctor Web
		Drive.Letter + Bfs.Create("Cpa04ql9qR/9B2lBVpOi7PQwBKGClKD0/wtpAvYIhCU=","2QY98gy/ZI9GZX2s18lfJZ1gB+AALvg5ZT4R+GzxpFQ=", "eep17zLT6QrJqcT07TtCJw=="), //:\ProgramData\ESET
		Drive.Letter + Bfs.Create("3Me9tw4MkDLWE3NfXwmRwzecAolDKX8kO/HbiYPCLA4=","sbE9U96R1Rn+84i9h+1e1S0OdVP6HalXjpF0z8jVNvQ=", "2drhnvlJpdJJWqIHxH16mw=="), //:\ProgramData\Evernote
		Drive.Letter + Bfs.Create("wEAeXzX321uJcEI95+gU7xu6+Ioa74sCdVHjkRtz5dI=","hGY76Lo5RT+v22qx6YaoUhJf/MyCzv8HvyNYyeywPdI=", "mFkh7QcFDAKY1Ou9JVQcwA=="), //:\ProgramData\FingerPrint
		Drive.Letter + Bfs.Create("RTXTGOQpILB0ibM7J34GLWplZi4yJjK9gqnU4QrfiqU=","FAAs6Musf9VykxE3sVeL/6qXCNzC6vxIVvgibyJTH+0=", "1ZfkHZSkGzA7utivZl3+Dw=="), //:\ProgramData\Kaspersky Lab
		Drive.Letter + Bfs.Create("3L1X881chsql1mtTaSlAl+Lm9/I8E+B7/E4sL2GizaqMx80WCncsDcGfzbz911jW","l940pD5x7v3yK6JdrzSqC53oR8KBVtNIguQMFJGgItU=", "cW3/aIc3AuvjKepfcfBIRg=="), //:\ProgramData\Kaspersky Lab Setup Files
		Drive.Letter + Bfs.Create("GNGI1PbdA2SXl9xmJtEbjj6vbTiEqX9Sy1uYJo0OYqo=","iDbfOpNaoBsRkQKsrIYZTLXUadbB0GVa9zn0yPVJxb4=", "Z/caTkFqtTauUyEH/fVDnQ=="), //:\ProgramData\MB3Install
		Drive.Letter + Bfs.Create("Gaec3pvxOGUQp6jDxvsoxKhTR+6ET17XpkzNQMTM9oI=","YxwGCpoC9viWnRXNxADBTbvI2ATHPX7xXsZgrSynzkg=", "MjvWSyD40/RarTtjQ9yG4Q=="), //:\ProgramData\Malwarebytes
		Drive.Letter + Bfs.Create("vVKmkrgYt4yLlesWA4oCYnCqF10haDldVJpkhfA6v1U=","MvMOz4xxo8Co913wmvthpYjTU51PKfRbLx++uTAXssY=", "RRlt+L64U7/xldpDT/kYfA=="), //:\ProgramData\McAfee
		Drive.Letter + Bfs.Create("SQSuKd6rNF/NLuE1nup2fqKTI3Z/d7Xc3RU6gt7wouo=","Y8bvZyzPDSt2Uo8/EiWG9b7gnp1czee89f+v1bVW+fo=", "rITu/0JakKDfj6sJMnbKvA=="), //:\ProgramData\Norton
		Drive.Letter + Bfs.Create("s8BRW9gXiCAe8fV6lhOte3zWU2CLy8Arnl7D4yYP0E4=","3JpnhQvNtETpgZDpYcxu7MGQUqQdTgKcpnvC2cdefNA=", "O9AGPNVNSjLhyNVij8gBog=="), //:\ProgramData\grizzly
		Drive.Letter + Bfs.Create("5N0LyVysKCkIha5PRULkyzCm3jpYQacVa+r1wVJk7ujdlOU/9Hn5aLpAcJFegGtI","CHDkF0V60gef2k3kk9BdJiAtv+nsKzlu8ZGGzCgkO/Q=", "e7IdM/ozwoC4nSOYITeyvg=="), //:\Program Files (x86)\Microsoft JDX
		Drive.Letter + Bfs.Create("k00dQVUbJRmkpLzdpO5oHLmlGhs3zeVIRCbGbG0gyT0=","93wyfuCLenMaK50LLDvRxyouoX+EVl1sdiAr1g4b7mc=", "0m5zvIj4eqNROzsTQwEwsQ=="), //:\Program Files (x86)\360
		Drive.Letter + Bfs.Create("jTel/fRSHVLIGypXiLlCSjXn2lvErkzDIyZIdRaev8U=","XJKzhQIHr+ccepd2GWEGiaD97EpG/HaRW1Eq/3Br7Pg=", "OYQhpBqrqRU4ww+Sw53h2g=="), //:\Program Files (x86)\SpyHunter
		Drive.Letter + Bfs.Create("10v1Pay7EamSYX3fiRU8q+UQiGMN+d4msarU+2YGKAY4hWoIkeDfBCCwOAUjAH6S","Kid9v1CavhV74dTAy7c1Q+O168lbNOx9HmAZ50wCQFw=", "yCLWVbryNDRR2JsMfqPfbg=="), //:\Program Files (x86)\AVAST Software
		Drive.Letter + Bfs.Create("xmR76UWBlfuNRTyFOt7+NG3cevGDyLwcQ7nphiHJpdI=","gskd5dPvWGkC4/MM9iwDf673Z0VwEoqg2ckNMiFuk4s=", "vrcf7ArfV+K+4mJEUtw+Fg=="), //:\Program Files (x86)\AVG
		Drive.Letter + Bfs.Create("miKt+kilVF99pCYQALAlCgkCSM3/93zbeIPCWjG6GLibOLODVYjXEoLVODQwEKgC","BsFTrUcdhxaPMJ31z3gNPqDN2sWK88Eah1x+Qz84xxw=", "BbsQStGvUJG7r5J3EJfScQ=="), //:\Program Files (x86)\Kaspersky Lab
		Drive.Letter + Bfs.Create("h7QQrhB7ddPaNiPmXdE4ABN9T8F0mjCNqoNdDBNxTe4=","EBgDKnNlVAoXfFK/KFX7IECzQ/Zw8IUtDsSW3OeFz6g=", "0W/1vBTSl0xHARmUPHEXDg=="), //:\Program Files (x86)\Cezurity
		Drive.Letter + Bfs.Create("3Enttfx83C4YH3TwLbXLi06TteX0bhiJqAUUsluhLXPFzLd862p9wRRR5eqKGq53","tZ8kT7KBT8qxbGPPP3yP9+EQqYpXAnz1FclcusSHbuE=", "LAliIogKor/zSm51DJP4BA=="), //:\Program Files (x86)\GRIZZLY Antivirus
		Drive.Letter + Bfs.Create("T8Ona6NLvK039AIKs2LhBy+PHMbeb9lz1/2ZbSu+RIRLTkAseILusiwdyfN035RE","lbKhRIMWkWKPareW8OhRw5/I/dW2DCy669x2o0xKg7s=", "0R19qNqIp4cMMPPFREYH3Q=="), //:\Program Files (x86)\Panda Security
		Drive.Letter + Bfs.Create("0PHp6viUSHJtim4Ii0W7dNC4vGRY6UYqCM8x6N5rnEx4tvBoB5c5HdjXUwRV3ANt","OGfZyVVUKZ3UXTL2UXAeftn/zcOgCyijmqA7OG5TMjk=", "Y+kieyl+8E8Ygz5Sec56tw=="), //:\Program Files (x86)\IObit\Advanced SystemCare
		Drive.Letter + Bfs.Create("aDNe4LGPKqCg7k69DAraBIel2Ox6BEcy2y/rOj7TP5wfiUhefPd+86C1bG92z33UVnCW3NG20Vw90h+IOBXowQ==","AAqv3d8DthPa7EmtNVmE4z3OO+RmjLXwiFRMoqrXF+g=", "xRFooDqXPK7QbtPr2iZAfw=="), //:\Program Files (x86)\IObit\IObit Malware Fighter
		Drive.Letter + Bfs.Create("zVyrexrjikKnbCnklObmn8/oFZc+q+vivjXpzLZVFZk=","RBabvZCwQq3It9CBwej8Gx9k5mRf3/K9P6gDJgMacjU=", "lVTviRayfqKAnxt0VPwWyg=="), //:\Program Files (x86)\IObit
		Drive.Letter + Bfs.Create("/DLYPX/BfkVZIb/wG342Q5ThJrS+qQACmySgWzdknvw=","UBEgwTV7EXFLMnh1xdj4T+qOAUy4NPuN3zrvYJ6gCs8=", "PsUJDoCGITpbA7iHnS1y6w=="), //:\Program Files (x86)\Moo0
		Drive.Letter + Bfs.Create("uWTTX1AVvFTuWxKZXkY6Do3JW6EZnKRoP16zeNzlAjlKgLfVVJaHLT7OFj7spUAO","+VoUotUIlQ9S+I448rGrRX9BjhZqddAt8OQl0YzGhQs=", "REDo/jCSVU9+qdL8oRTrxQ=="), //:\Program Files (x86)\MSI\MSI Center
		Drive.Letter + Bfs.Create("g3zj1MzfvbmU+uwX1KKADqttY9kY2VU5X/OJO3LlLEc=","5T5KftbAqD70kPGGriMpJb9T1PEp06nXErDwVc/+Ru4=", "8pFoVpXz57xTY8riHTDlhQ=="), //:\Program Files (x86)\SpeedFan
		Drive.Letter + Bfs.Create("B7VWLfu5DlrCqYB+ko3PD0SqmoyXCc0pCrNPATKjjLM=","2oX0n8jQ+Y6RhDau7VYxrMxG3NGdEohZcql+b3p9ctY=", "jS4vrVNeFnhnLZLlFJEpUQ=="), //:\Program Files (x86)\GPU Temp
		Drive.Letter + Bfs.Create("vqeaB1O6Nk4muSB/QIGeBXbXpw2DSYFVi7gHGA161Rk=","pz5xYIlZuXqOXqzR1auKhZq4NaIRkh7qEqMw4Oc/eRs=", "vGI2ezbl/XcRIvqxYixpAw=="), //:\Program Files (x86)\Wise
		Drive.Letter + Bfs.Create("P/rQOLVLh/lC0xyvHIednmxY26+Mzvsh+fVxDb5N6vI=","cYaVbS9vLe22OYfcuBh+zUxnDoGrHK+KA2AToHoRlqo=", "ggPgVYBChwsB+hb40HpeWw=="), //:\Program Files\AVAST Software
		Drive.Letter + Bfs.Create("nqR/2YPVNTBr9lg47YYAD4TF4bYiRnkihWYLgwp/sCU=","vHW+aZW37yNIYqJKlylOPKLBfbnjS0r/yQmU+G53OZo=", "BPgScTHkuZyELcCdjMyCPg=="), //:\Program Files\CPUID\HWMonitor
		Drive.Letter + Bfs.Create("BSaj0CRkJo+6MkbZu9E0ytzDvxFlyw0iSL83x/DPseQ=","AIZGJJNnjQ5RQUAtxhEn1MPeFeCBdLbCePtX4BhS5Lg=", "zZsDfLW/nZJL3MBZnb0KMw=="), //:\Program Files\AVG
		Drive.Letter + Bfs.Create("pIokI/l7yCP7T4KOrRpGbsnd+TDog+vPpHige8wRv+TKR51GXtN8fFKw60GqLPOK","B/lruQbLOR2n+OXjrQJkgchqLMuQPG5AbXOAUenLvbo=", "uRCoSRGzx5ffst8gpqP+Pg=="), //:\Program Files\Bitdefender Agent
		Drive.Letter + Bfs.Create("wDl3TlpQ/PWE8+Anla9o3KfcpNynoSR2g5s/rvKCBRc=","fQRh+5v/jzkDEyBJ8CBMasuS8NLDxz3qd7dfCE30Sos=", "esokOCtU5LyObo7OVFkx9Q=="), //:\Program Files\ByteFence
		Drive.Letter + Bfs.Create("Sk3IZAXFZrdd5t3M7YMtUSiv/2dN6O4hPHDzRWnLpxk=","dvFbOvLt1cS8EM3ZOSYEPOCIVG+GGNAU+0gcizbGv2Y=", "3K6H4FLs+diD50C5xtuYbw=="), //:\Program Files\COMODO
		Drive.Letter + Bfs.Create("pTzgRDz4SBbQD6FHJeZA/7qSBEkx1Bz6YD0MhSZXsRU=","rcGZOGQq3SqAmTQqGCnwQ6Y9yiLETAQ7cI3jhEF1QeU=", "CyZcRZt/iz6W1bvMwc9AVg=="), //:\Program Files\Cezurity
		Drive.Letter + Bfs.Create("+hrkCYk6LFqdHn3ctYGF8EIhSiZgYZaeEZ+EWhdDIEs=","6hN8f5BRiyPMvpJzb1C2AdYaWtMLqdFOWiGjZN7KYyc=", "4GSWJY2IvIfVo09pTB/vog=="), //:\Program Files\Common Files\AV
		Drive.Letter + Bfs.Create("KlMUn50i3gUxs5yH7usvKTG6XprcqGunxTvV+BWmep6WtUoP6HpvAx4Ig4Zr3Yf1","7tswf7kM+AoHUDQVQ3js/TG/Xafr5hgyXaRb7xzdSe4=", "PAcM3nTxuYuBAomPwFV7LA=="), //:\Program Files\Common Files\Doctor Web
		Drive.Letter + Bfs.Create("EKvPClaP04zpBbiZ8sdM6NSHhUkKD4YD4vUiYZucTwosX2+iCRW0f3zELeyJMHGk","gF/L2SzhCq++mxdUKwBtzK6Kj8tK1VMLkC9yqWgLE+Y=", "vvvjEyAwQDDedVJNtUFPfw=="), //:\Program Files\Common Files\McAfee
		Drive.Letter + Bfs.Create("LaJ5SCqowJVzgVMKKt/f+rlq8eHANxwfAeymAQeNptQ=","GN4Xr5f8Wbk8DsoUc8U4Qswd7tVutfDQyaK9li9zxdg=", "mkKgfMKhG1L5qCP79Hw5Ag=="), //:\Program Files\DrWeb
		Drive.Letter + Bfs.Create("aF5VbNU597HkmcJ9St3EFGGVdGNI5q2I1NssUFT4Zfk=","8VY5lRWTVGNCWqSgWGo8Q2/dHZeBy5Epph7YePmyoa8=", "+/RvOiYp4R7OMeodUD+0RQ=="), //:\Program Files\ESET
		Drive.Letter + Bfs.Create("pppSnUqOzuktSSciffO6Sl6YggHxmYxB3oC15t3wqtWcQsFaIrG5oMPAZ/D8O6Ev","rzNQd7wAj2FBwH0ZP+IjoUGF/3mcNhr4HYMdfYmE+hg=", "ifYNPGAryJgVx7XV7eTdjg=="), //:\Program Files\Enigma Software Group
		Drive.Letter + Bfs.Create("069JlBgHA+1Gr/oWPcCpCVpXqDOYXYdKNCRRmfwLQVc=","UoBKWsOhzawm65HEEv2nZ+UKZTlIo7VqV8MUTKdtmEA=", "BgwxPswB0K3mfywd/DUSVw=="), //:\Program Files\EnigmaSoft
		Drive.Letter + Bfs.Create("018RLVDJrlIFsleUmxXl3CZugV3KEDAnpVwNmuH2nrI=","tKR4vN2pd54teIoQLg9fUX2Jiblm7iRAucVp6kACEUI=", "HHzxsD0abGvGjMeNtGl04A=="), //:\Program Files\Kaspersky Lab
		Drive.Letter + Bfs.Create("ed6PanKj8FYjZegLtwDyV5oprJCHBMQzMABhTdKltQkBrvqKQ+j6o+q1YA+1gOzr","qAayo1s4qZqT/vCxKtQO3XtBhlOealmdej9GOXv77Ow=", "EUKlLSXNRlgiS2tifxP43A=="), //:\Program Files\Loaris Trojan Remover
		Drive.Letter + Bfs.Create("YiTjBXEOUiMpPwDTiSjFMmp5PaHt6dwyq0Mk5iqefyw=","RCdEVPQR9DIy4gUjPP1aPohK6AZYUTyCUFev7OSrRCk=", "AY6bRlBvm3HolyoyCdB6BQ=="), //:\Program Files\Malwarebytes
		Drive.Letter + Bfs.Create("YqxkNeF5P3deLXEffVgHzfSLcSnZGDmOR2RQQfTguiU=","m3jQLAJcCTK1H2aaxo+9AT0OLBUMx8twn+LdKFeXnH4=", "KjhpXx0PultG+8vTiT22PA=="), //:\Program Files\Process Lasso
		Drive.Letter + Bfs.Create("cXU2E3GUF1ljr8xnNpb70XEW5QOHys+Q5j8enNSut64=","otYm3UYDnjw8gR+UWTfIrfSHDkfTKV1sVWYxSf4Dupo=", "tuoeXCte70TKZ/CxIU4Bfw=="), //:\Program Files\Rainmeter
		Drive.Letter + Bfs.Create("uqKOdKRhgyi2BdYPYlnBXIR5zs8iznVoFsHD8R41YgM=","VzcUJbGalQiUMWOcM6C9FLf5trFlAkIvJ/WXRdv0gT0=", "G+CYEFpJLyAesjh8DqsyZw=="), //:\Program Files\Ravantivirus
		Drive.Letter + Bfs.Create("rFNqH5L/CwCOcZ1MgV7GFY2iTYfPHOdIO802Zd7b9XY=","0s5d2Rp5FA7DBZtc3CuNs+SUBXZHvFY8zKY4cFBy7VY=", "haw/lTNroyuH2ZLl+8D9UQ=="), //:\Program Files\SpyHunter
		Drive.Letter + Bfs.Create("cUqeOVSLd6WXBC0OxFk3C40mXpIOaMjVBR2JqXFnl0W1iB5D5tv3krdNRJYsv44F","lGk2maZGyRDYfpZzO04VqnyUkDeGObuC9sHnU6EOcxA=", "QNhK5vmLrSzcAjvhL5EGjQ=="), //:\Program Files\Process Hacker 2
		Drive.Letter + Bfs.Create("C+nChfcnDNFlLwrO8JWaj2eFUJVHxjL4qe+gMYetsAQ=","9kEuDPS5f8gjlBvSy1z2s/yIdldW0O9zyKb87H/Y7lY=", "0jNY7MzKGhhD7xLvJPaopA=="), //:\Program Files\RogueKiller
		Drive.Letter + Bfs.Create("LRGrFYPDpV+aIFh0AJPOiFMF092AdQZU3vDgAHtXDurZF/bdXciQR5yEekYoOpnj","3v+vSckkUpb9p8RX5fQdw2tPnllcClyspC9VDmpSJok=", "QEIyLDX7eazQiDSg/aj8DA=="), //:\Program Files\SUPERAntiSpyware
		Drive.Letter + Bfs.Create("1go2u75bB858xpNGLOt5UtmqW55oK+XDQ98J9+wYOAc=","bHiBbqsi8SY4k8n+8V0dbn0nnkuxFkgF2m0XVFhKtBQ=", "+DK0OxUCj0froZMetERMwA=="), //:\Program Files\HitmanPro
		Drive.Letter + Bfs.Create("3S4mdqpiXZIXgsgJXvgn4ouSFZHaiNb+AQbMl8MAK6c=","+by4MWKgnyPaMNL79jlE6RAAwskHywrdc9VemaCD1Bs=", "3IrjhVmfv0YRtgAyTGlfjA=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("jld3Ndf7eGD8WfnTC/OhxYvfb8lOn07vNJazl4oBagY=","oS/EquvKMCmAVmNp+tqmxHUV3p7IQsYnovjPA/yEUvo=", "w/WfFX8hWA+/enLxYAW78Q=="), //:\Program Files\QuickCPU
		Drive.Letter + Bfs.Create("5eOJ+zeD6MyTr2HAB9cUjz17cLDxdDP+zJga0BoBTvY=","TNIXVr3NQyp09sEC052Od7Xup2BHKBtC6quNM1ssElM=", "Ml/P6CmXZVbI3PrlleOJzA=="), //:\Program Files\NETGATE
		Drive.Letter + Bfs.Create("paFqw8rzUgDnU+BRjeSYoMgoKpBeM9DRrg8UpDcFxsY=","op9fmg9b6UXGVhwWcZsZ5OJuK5III9xVVCk3HmtHvns=", "FC2+C1Ge8mcm3BmqYamdSg=="), //:\Program Files\Google\Chrome
		Drive.Letter + Bfs.Create("LUtAfwUzt4Hot5BCbU6tr+j6UYzIADXtA6c+HwBeQkk=","I6+A76VHOkyNefcVE5aMcq7qTvAZTUEfON0rFpI2gv0=", "qX1hZdqzm3x93H7fZwfX6A=="), //:\Program Files\ReasonLabs
		Drive.Letter + Bfs.Create("KwqYVIcmQIyP+ey/iiGrhw==","guphTHl+YqmgM6F8CJ6p6MCgGzgQs/Y60pzhaskKbxM=", "yHGtckEhhshxOsiH6YsMBw=="), //:\AdwCleaner
		Drive.Letter + Bfs.Create("v4ut1h5H2KgvMIrmEgnIkQ==","QB/OBX9u6dQ/MLjJNoOyf716VLWc8jJU9XMAuf/opUQ=", "Endq1K4vqtjzmUIJ2irP1Q=="), //:\KVRT_Data
		Drive.Letter + Bfs.Create("/MIdEd7/IhWVTf9d1YWDBQ==","I5eh1QiCatbNa9LtLOs1zl8FsC9Dmq+5sJ12qJHffkQ=", "s0/CHyfA8RSxYYKwTHvp7w=="), //:\KVRT2020_Data
		Drive.Letter + Bfs.Create("h1NE/FCHed9BOV4K8BFr0Q==","FvMoWyX1NOCLwmhR2KNiDJwHk8/FSWfQqf0M72WKjYM=", "siu+skZlUH90Suunp/oYXg=="), //:\FRST
		};

        public List<string> obfStr6 = new List<string>() {
        Drive.Letter + Bfs.Create("+ApxzgEj8EEnyeAFAKO6kQ==","6ah8ha8T1SU5ZV8cYvft1ivZNcqoTaVjCM9LPC2MumA=", "NnXrpovkkI+VPLbpZ6MTow=="), //:\ProgramData
		Drive.Letter + Bfs.Create("nQ+NXzPPdFIZ1++KjPaZKA==","7hWVlxtPhp28wn2pOiSS5w01FQ1XixxIsGgX0ckJ/Io=", "5logvuR2p1IzB2WwlPk5Ng=="), //:\Program Files
		Drive.Letter + Bfs.Create("E5EXRbNOZkHTqpKBzw9QpgHnO+y/AuFvMi0STRMVLtw=","yLEjboWKHmlTDobNV440gQmw2pxmsF4Q07m1uIoToso=", "m1oY+geg0m5DZe1k8qZU2A=="), //:\Program Files (x86)
		Drive.Letter + Bfs.Create("znt/x7cNpEerAd5EmlYJ3Q==","ATvWdKmIJP21MGbhSrqOyT8cd3vaKM7gbGfOrEuWZ1o=", "I5prs40kZdbcf3LcDY3v4A=="), //:\Windows
		};

        public string[] queries = new string[] {
        Bfs.Create("ggPgy0jsYbPcSiAOQA3WSXCuNZy0vcLWtbUdA1c/5jRVeFCMugGIFtzSYL3Dbjuve+FDbJoJofbtiQ8xmo7nDA==","X3ZYUfjMI3DxE7f8k53e7Kz/4R/dz4L4oiofN8LFc/8=", "/m6NWuY7+lJiqp9cJFDqdQ=="), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
		Bfs.Create("C7M7Jhbw+P7psUHMt6uE7JsV6iZOeThrSjIYbiZcDQ6EZCNewzvEaHxrYxhTF/g8U5zFqlla0mEkyMzTez857Q==","00Qp1T4ymfFCRCGaPp586vXwFQWnB9/WBlCssHxf98k=", "o8X+V9vrhz0ARbZlT25C/A=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
		Bfs.Create("DFaR+/NlEOHK7G2qEnvJT01TjPKi35bS8xtILfF4oX9pX0fRdx7aB0aDytiOVPE8LeW1nxg+SEc1byjF0zSxVQymium/iWnXhVsWd4COLMk=","LwALquCRPSSleiUhAu2zykQIln8wxCgeXD8Um2NwQ9Q=", "Fpye+K7BQNNiaaP4RFjbFw=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
		Bfs.Create("ibfikmyHZAnsfUoYIIXBcWzK64RYB8XEHYvsYk4dF8+ocI9AND3w4julCHkWkjtZn5+N8IgXhg1wFwSnDjBySw==","jjDb0KhlQ6PaAfQk2LGjreNlIPrbXRJ3OSyg7wYMDd4=", "te9jEv1selkMTADtwqKz7A=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
		Bfs.Create("Li1puUhU9eJFM60cGAbSX3Chk5NiikhS3gIv8i6xd/uhxjkZqhUyFuzlcJWNnYzs","JSSErOpWvHQzkxRAHlAty0mlb2emMKSj60W61od+nYc=", "sLr6Db6LQSb7mluoL/zGRQ=="), //Software\Microsoft\Windows\CurrentVersion\Run
		Bfs.Create("TVahzX8wP7BXMQU0sIHsC3n75zLDqlVl47Xmr1JN3AiWHWOPptB5MDZRqvTjeSffNOqf9wwIV6JeXYKAZNGPbQ==","g5bWHAdnM0ThY1uYjWttinKQ89d5Z/CyVM+KLiGUY4E=", "4stS11mYZEStterbhUXJbQ=="), //Software\Policies\Microsoft\Windows Defender\Exclusions
		Bfs.Create("hhkJV+U3qSJFyN48XsgdPx1puDX1ACLySssKG5DCCMkKZwwWaou94iziWY4MEVg3IYp8O7WKp8OztpRK87ImSA==","G5ozfDcYENx+XxHimmShZ7VZCKdsvxuZTeIjaTUWArc=", "WkcvsA4UfUts5VcS1PVQgw=="), //Software\Policies\Microsoft\Windows Defender\Exclusions\Paths
		Bfs.Create("4ZnpRDDAd+rmfXvQKj+zy2D633FFBFhWXB02b8kyO/UU/7IRDigqofn5HhvaniCGbTnnuHK8QHP8egivw+lty2UGH4ASJwDskNRBUeKI9qA=","vEdhVvxmfV5p9ulT9jC6ZSye+Nt9qOIyiXl1x2ZX5zY=", "0EMR0EcoD5TAvBJH/tR9Sw=="), //Software\Policies\Microsoft\Windows Defender\Exclusions\Processes
		Bfs.Create("vApeOFtLEzqcKBR8AweSqK5jncxwn2u74N+N6uQqmytFdCsKmPNtL6wm0A9izEIKKgYwvPKw9kGGeQ2tp1bmCg==","5VgxCJWSm9yCaqOSYRHRABnd4HJ9v48WCnlXyfu6VGI=", "4yRbS8QuYcJEEOFP3YDoiw=="), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
		Drive.Letter + Bfs.Create("3GAkIMw/Ot85YqgxJCK3St9GA8sPp+c7naVWax2fbek08Qk6HF5TmnV59zGuOxPP","dphoU4MPQPP0at4BkB9RTj6DqXoxPMkup3s9Erjz5CQ=", "6Fm1Sed/GYbeBVMmpRFq/A=="), //:\Windows\System32\WindowsPowerShell\v1.0
		Bfs.Create("FQP7NjjlbKQ1MXxLpRKakMfaGrwgEAflHFT6M3mxmKnQhYJASlcoysKK9BSBFY3g7Olr0HYAIkPN+kF2EaBnGg==","irQT1NYGi4yA7LdrpSz9EAJnwDzuWGyQtgmhpWSDXxw=", "ld71hI3kebpsXheM0WJQMQ=="), //SELECT CommandLine FROM Win32_Process WHERE ProcessId = 
		Bfs.Create("mdDPfQIlQndvYd1w4+WpBwWMBG4lOpjFrw869wb/ODA6A9+06wlLKFcF/DRw41DS","b2r0XWhmgMLsFYNkq2Zbqn8DMtXyWdnMl1WR1dSXkx0=", "bRDNEoLSW1VzX9IvHfpV6g=="), //SELECT * FROM Win32_Process WHERE ProcessId = 
		Bfs.Create("YPELJHd0oWUzmcgI88iRhGu3IksRtjbYGu4rLuvYqj4=","CtSCV8JXZylw4LCXQYnHbUAE4JURrV+xqWdOXYNzttY=", "zXn3j+zG92BZqGG/prZnKw=="), //Add-MpPreference -ExclusionPath
		Bfs.Create("J6Nk88hrCMKOuFGmAU4hMsfrhwrNHF1Aq7KGGqLtzbGjG6VFCLS3/fcbya6F6lu0unt+Wy/n/zKsB+zz4TXr3g==","o2xKArvXVE7F0/gVYHJE1dBToluO/dS0Z2REswtKiOI=", "ZX036lXUNYHAq4uqpR+SnA=="), //SYSTEM\CurrentControlSet\Services\TermService\Parameters
		Bfs.Create("HyiG08ouYu8AGwlT2oRV9uM67mTD1VqDjk0CACWxrfzjFCUcoYEnEZ8IH9MlOkNC","IopdVBJW2jpBli3XGFHgjfqWk4MMJcegFODzompRtwg=", "kU6DIi64EbtTCaFpDamLFQ=="), //%SystemRoot%\System32\termsrv.dll
		Bfs.Create("txN2aq/x7YZqz3CvTKgmA3NgpLV82Cyp2SFpntKayTYZ2vnxPTmuh85DdWJZs81yaADeeyY15ijosbr8EqyxL/H8lWtlO8glqHm2TImH1AU5BTQm1tN1lA7nq6wMtBeX","cRoGQFdvSlbohBrQUV1IOSjtf/hePSiOVWWn2QCFkiQ=", "xCYj92oSYVhiGcMazLetXw=="), //SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
		Bfs.Create("LIM2dApZ0t57k5deSB94oNfZdb1SNEmD7YFxZItLaNpDkGxDiebbtilVsQU1fylFqGUI2+1aKguhdDGuCL6m806lfaWdhmFyEJF/96qACt4=","4Ls5Wy8dzG74t9G3iimSTS1dLFgfMzk5qIixT9TMy04=", "dDIoB8CIo/Ngo0BocLpfog=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
		Bfs.Create("fY0668809rH6xQb9vMEh7Eps7kFmGgYWn0RXULI51odNcUkQTSz/jmzjy+udbftniyNR5jr7+sxCXjgNXnP62A==","xiO+oKLrH72Z0jU2f8MIez2acSBPM2ranrxZa48dVgk=", "vPmp54isPvT4JvYmSIUcKw=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\SilentProcessExit
		};

        public string[] SysFileName = new string[] {
        Bfs.Create("AiL432/EcUbJgZF3Wt6s2Q==","/sIhCPPrNS6pcgg2lUfN021LYaZJ6IuOuJnbaqIzr3s=", "B182K/nSycnODX/lHbQiWQ=="), //audiodg
		Bfs.Create("QoxIQH26l/QJFjSMCT33wQ==","1vGMvHzVPpnTiZDedRMzgNSkrgMAuzp43qDtp566WD8=", "q+5wsSGbTWiIcW0Ft3bZDw=="), //taskhostw
		Bfs.Create("Jtcvqdqvq2WfvrZsJ9RPtQ==","Us1ZSEzzvnZicfgqhD/kg/e3X0uuqGtUGQDGiXLQM9M=", "sdDJfqa/9AWWvTNVH0hH2w=="), //taskhost
		Bfs.Create("rdEv59M0GTxlqYPpAzfyiQ==","WWWl/38HXHg0WpqQi4ncNi3x8fC6UYX1agt1K8H2Vrw=", "UE+RMZorDeP42PhCShLbsA=="), //conhost
		Bfs.Create("2aMLPrpP9+Y0NbeHs1d8ag==","MChvrq2Qytit8NmhhgJ/ZpTwsD86Oufq3lJF6Pc9IYY=", "woygWTebW+vOb6HbrgoYfg=="), //svchost
		Bfs.Create("XpTx+ZGho44dIYS7k0+LIw==","YYZEh8RTQjNGzRGyazytlJvwNiJtbwaNZ3Oglgw6ZRQ=", "Hx3044UREuOuP/AvT/jFDQ=="), //dwm
		Bfs.Create("p+mzT9OGU1csokCjOxXnbQ==","c7XmquvyI4E7ZsZE+Z3DLmcAh1L0Ks1xshCF9+fal0E=", "rNbvHEZ7EnV6A9+1NM3nOQ=="), //rundll32
		Bfs.Create("hfFwxy+awMrtiSoXCBndeQ==","NnzE5nc+HYMXvsLnqy92Gng9EdymjKPwKbO0gatIJhU=", "rIi6NOck1ja071e5dxAB4Q=="), //winlogon
		Bfs.Create("vGpd+jMqiTKicOots2BamA==","H7kPOEaIkV6AVV2HTDQsYHhenha58nYIkYHZ0MHccKI=", "SiaFuNVyjjOw/84+QdzOOg=="), //csrss
		Bfs.Create("fPxnhtGn6uHk8XqFMKG7Ag==","leHiiWhTOuM7W7i0QO/sy6qntcUrNkeGhsg1y1BxW98=", "dWFnj9+sq4ANkoXV3qdKLA=="), //services
		Bfs.Create("bB7c11vfdaKikUC8fsXP6A==","ED2rz5+3kKczL4Hm0ldpOJivrLXjZWpdYwd9u2A4lI4=", "4ll7SH7gGIeVK67mVx7GWw=="), //lsass
		Bfs.Create("KfudjOIp+n/Zk5y8LGHGCQ==","FWzNnuHmxlkZfVJKeHCb3ZvDziIpsWG/mbS9mPbfHQQ=", "T8WJ5sYIcxSaHzrZeAANyw=="), //dllhost
		Bfs.Create("l84jo3HY/UZDfCZJ91+R3g==","lJ2tPJ4Kj+HKfPVRtNxhRS1+jqDsBUtGDeV3sKAhb5Q=", "1y/CbpCAm3knIjAU+E2zJw=="), //smss
		Bfs.Create("fqI0zXzcOGpnOVOYem6AuA==","Qf8QExOGKym6P1GvCIovVBkeeEP/u/N9EB92h0clkKE=", "62tVSYvfp5KAxZ9rjIbPMg=="), //wininit
		Bfs.Create("/Cm0Ws3asCoQwlAsnP8/bQ==","ojGtOfYGHd14NSpURNXCX68bchG7HSkhOheI/JbViC4=", "16vaob5zuzA+wP1AOvggDQ=="), //vbc
		Bfs.Create("7Y8Bp3HHasxmNOz+JhpSAw==","9+TSh1HbG+esNuV7qW4FWbLTW0OkUIyMeb5KCgdkeIU=", "EYXdrPFHMaWy3SYOzat8gw=="), //unsecapp
		Bfs.Create("VG2+tecb3GlYPZf/C0YlqQ==","QBR8lTaJ+45nEPhJzxQXqgpCB6580kQjzZu4QLSQ8fI=", "dFzgYkXxVjHdJilwF4bUyQ=="), //ngen
		Bfs.Create("gVgxFn6+MxuRRe0c4iWjZg==","5AIOlkoT9sXiBO3XefZcSRbs6N8vKIyxokBNYytzBTI=", "C+i4G210qvp6Y+LA4oKilA=="), //dialer
		Bfs.Create("yVs5JPcsrFO/2Y9QicH2BA==","NQPTZfvvMYlA7KLjxbySbQgveEy2+Yhm+R3TqS++vzY=", "ASHETxkS5pylCL2pLD4ofA=="), //tcpsvcs
		Bfs.Create("8jWjfkJqEYkYRRFtcVqAvg==","yDDFBDIV3K4/FccywS8NxwyKNb57JL+FueEWqkPIMDQ=", "LO3BmfxZWQgzCYxrPqdrYg=="), //print
		Bfs.Create("DxDPADKgPotVMFRlZivfCw==","JJzlVqyTuwpgNXYqjiZR6eps2AspUnpKft5qMUrCS8A=", "GT7YPqQ9/ZpXHbnIkPQSlw=="), //find
		Bfs.Create("Kx24J+iQ1uNv7i4YjRC2cA==","tg6L1ps6bJlp//VzKqodLZ32GMxZML+vgQ7it75dI7g=", "J0wCsZwWPn16zy23EXxB8w=="), //winver
		Bfs.Create("RbQ2SfYsmlWlqxm4DT/yIw==","cHpEbjAftwpAsdkVcqBSlUcSRYgRHgqZUQRKveyPrpk=", "zvPi+jxk5vGVFmwCzcGayw=="), //ping
		Bfs.Create("W6pwIblgmmS+4lTZ13vj1w==","rKrtSt8YlDrzgSg3YhA7sWSgRYGrmwgg2huvxp1y3Uc=", "RSvvC2LvZ+umJy1OZ1K/KA=="), //fc
		Bfs.Create("z9+pm0NY8cY/P2ixjA9/FQ==","hxB+tHr8ZkjcFPcx0a2viYg0arbQHWZOnyWCPuOvI20=", "sPFheS1PHwITvtYrivzrwg=="), //help
		Bfs.Create("/+AqOi41RNRzAMDI1JRf4A==","a2FvjM4tQ5KJ69OP/RW9V+k1hfpkqCqBsbnGmUiBExM=", "9oM+/GeKHU+LCobepNI4uA=="), //sort
		Bfs.Create("bddvxVbjjVkGVHmIxJACug==","hQ5ifDt3Fpw3ecpJXenHM2Bo2aOW5VDv4lG//Rtyugc=", "xIIkkKtpq6QsLgg2jng6xA=="), //label
		Bfs.Create("GEzATVRQCQmO1jpvf+pumg==","Wg4JF/v8l3XSachZKcmreAMYhOa1NJrisiAzUz7bSfE=", "2k8gxqTIbd82/Njo1eXIHg=="), //runtimebroker
		Bfs.Create("OZoazxwC/hxdGczaTdhwng==","vadXGEg+mZlCOadPgRXExLHADQejQmyutZjQ3v+HcWU=", "GhjTGhEW7ijAEdRw0iOCbQ=="), //compattelrunner
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
            new byte[] {0x5E,0x71,0x62,0x6F,0x65,0x70,0x6E,0x79,0x60,},
            new byte[] {0x44,0x73,0x66,0x73,0x6F,0x62,0x6D,0x63,0x6D,0x76,0x66,},
            new byte[] {0x65,0x6B,0x7A,0x71,0x70,0x70,0x6D,0x2F,0x70,0x73,0x68,},
            new byte[] {0x6D,0x60,0x6F,0x70,0x71,0x70,0x70,0x6D,0x2F,0x70,0x73,0x68,},
            new byte[] {0x52,0x67,0x66,0x6D,0x6D,0x64,0x70,0x65,0x66,0x47,0x6A,0x6D,0x66,},
            new byte[] {0x40,0x6B,0x68,0x70,0x73,0x6A,0x75,0x69,0x6E,0x41,0x79,0x6E,0x73,0x6A,0x68,},
            new byte[] {0x43,0x6E,0x76,0x63,0x6D,0x66,0x51,0x76,0x6D,0x74,0x62,0x73,0x51,0x73,0x66,0x74,0x66,0x6F,0x75,},
        };

    }
}
