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
        Bfs.Create("yb2gWHe+9e9acFtlgiVcAQ==","svj2/rI6dHFjVQOmW/axA3GsiK8JsIJsyj13Lxb4Y4Y=", "bdM86vqp60Sum2e8a+HOCQ=="),
        Bfs.Create("5QgUM5muil7HzxFj7cuS2Q==","6kI7thd4uwdAbbP7WBo2etS3nQnocILfG5KIX427QYg=", "wJOO7+JSQEjZEalXKpNtGA=="),
        Bfs.Create("VAzKaq8IbusbB2/hgfix+w==","uCdRuBlNZ8B5s1rO4ll5OOOWPiAwjiIWfVuPbVnlrFE=", "N8Xo7pa3mvE1p+ZDP3BmFw=="),
        Bfs.Create("pLQqICEi5/y2SqAoy8cqeA==","M1seSehgO/pKjrdYCCvVjbpfcBCJfJzzmdcJAUjUP+U=", "F2ZNu82tIUQc3tsS7OHkXA=="),
        Bfs.Create("nmTq3++ZlT9cw+JWGkrwWQ==","EnVsoQeN8YrpJ3FhBJ6nZkkmz98TXbsGZJ4y+AzotN0=", "mUusA7EPvHY90tlU5VHyCw=="),
        Bfs.Create("ynWxMhi5BYZf05+7a/w21Q==","8MZxJxwQ2R6xtraT8I6pTpsKW1mu2Xl6dB4ZxwP1vaE=", "83uFMv0Xxfds/tInahHFsQ=="),
        Bfs.Create("bv27xjKpx3JIY23xh+IkFg==","E5T/dFJv9Xn2XbVvn0eZ1sWaM1pT8KVBcpdrwMsU+JQ=", "hCFOKC8aWKYV3uJArbbnKg=="),
        Bfs.Create("f0RWk8orQa7mXiaWGKCmig==","FOs4H0j7YbZBBpgsPukDjl0Rz6BExqvV4gpolue9vks=", "mDXPFmBUQ9RmOcGNgg1eaQ=="),
        Bfs.Create("xF+oIAUwPh5M+uG/jRdRPA==","yns6pxEncgRnztvFM9CzUM+itmAC08h+5G3V+D8Y2Ew=", "vp18I0ZEzjN5R8C/egPTPw=="),
        Bfs.Create("Yl+IaWS4MPxuhstVcF7yMw==","rMXUiysQJckmixid9T28FAjILUtiQtG8UaNOfjIBix0=", "lLm25qJ/YqiwrG0E/SwftA=="),
        Bfs.Create("DyEpUoGBdLFYb0jiurI7yQ==","17e4QhMepV/sE7RM7X+WdfcGagcyLBuIC13uMpEnkeY=", "dbCWZO3EdKdkfn6i22HndQ=="),
        Bfs.Create("zt+bJDRldvtP1X5JE8aBIQ==","YzFGYQWDVffWMSv+/ZuLHvIv5/7/jhhKX81hsZnHb94=", "2ft/D4Ks1OsFwxpPBjThpA=="),
        Bfs.Create("LpNcBkRr0h2vGC3X0aob2g==","g8w4Bjkq/vbcAdgOwlwOXBWEyVegf8/vxLvyRlgL/vE=", "OLBsi5IucWHnKjB41nW+Pw=="),
        Bfs.Create("xXBetzKb06b8lCHQXQjvOQ==","DHfCfMk6ZAufhkbz1rnSXmee4ZkQmiqCTAxwGDlsnLk=", "yQP4NhD62vIvQx66cAuyuA=="),
        Bfs.Create("7eqms+hoIcg4aevZm51cBw==","xMt+1/R8FwppXhQEstLTz+JS/Zo/pYIBGaSIBLpXBWY=", "jWkjAvGIxZgiR4LIWayeXQ=="),
        Bfs.Create("FYK5fs1Fhlqvbrjtsv1prg==","qx9STUE1b6rh16je7/WHubp7ClAImtKSiR4P4pnEpnY=", "ffi3AH8KB2bL2Ut8uao3vA=="),
        };


        public List<string> obfStr1 = new List<string>() {
		Drive.Letter + Bfs.Create("UXlnroScElRioqo4fdEtnyERAF4iErAZA/vi8eDtHUo=","bM9VSUP6H2PsLpzh8+2emHh+Xt1vz1JIT6IbHX6qWqo=", "2gvo8zolGMQDmqMwZCIleQ=="), //:\ProgramData\Install
		Drive.Letter + Bfs.Create("2b9TvUfRjemxQ1MAiIN6OAZ+LKBi09I/ujl84hXY7LA=","SknANTrvSBakDaRJ0p7V6+JmCMV/ZYmKl6Qk2UydpHY=", "QRKIIxLt28zw4P+mydj/LA=="), //:\ProgramData\Microsoft\Check
		Drive.Letter + Bfs.Create("uJmbqXCXB5hUpgNjPeh8Ha9Jax6i/w/QtkcJseSIibA=","5MSC2y4kOYux4Bv1cYQweXJCCTTnnAj4amAvQHUJ9Bg=", "cCfV2/iHBi6EoGOj5Z/7kQ=="), //:\ProgramData\Microsoft\Intel
		Drive.Letter + Bfs.Create("k5VjuYMq5IkivNgcUTT24BK0kI9gONscUq9UQaJfb8AavQje/j9dO1OBuyhJTAnUUsKZqRUa2gV8/7l9RW/Bqw==","/dh6iq52ORsztLEdrySAZwJeX9NUqwFCBes6Hh3EvqU=", "z0/UAfNAjcoabTe7yY4drw=="), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
		Drive.Letter + Bfs.Create("TtuSbwT3FYKnIOoUinxN/cW4nWOx24Ng4sdtA9TKc8g=","6zYZbr2RnfR3LKOQknpW/gxoLE/DxFcubi/FcJFhPMY=", "mxAC0uAX/+sfUAlczsBzXg=="), //:\ProgramData\Microsoft\temp
		Drive.Letter + Bfs.Create("39H5OqFNNCNwcIsnVoSTCeKMqAPG2KR4eelxL3+CfYo=","Xji/h1LPQHhy4HFAxCQ3Z+Tp7AwdrtpDQG+eaNlDrH8=", "ueSOgccQdHzAnfRszfmuwg=="), //:\ProgramData\PuzzleMedia
		Drive.Letter + Bfs.Create("Jw0dtF1DJ3VU/elu10IAlKikuzkCl7WOHX/ioou3bZw=","cI2YaXKufK/oF/YTNHsCqF4We3MMKSJKz6J1rvRr7ZU=", "TJu3TYbprJdjeahYDRFEvQ=="), //:\ProgramData\RealtekHD
		Drive.Letter + Bfs.Create("vfFGWRmIbef0R8alYDocxs9tECP5f5dIkTfdorGwVy8=","NQgzCVmZi2eFtBdk5bjT6izff7xuG1X/PrsIan654lg=", "9T36RJcfpZB16caeLWFSRw=="), //:\ProgramData\ReaItekHD
		Drive.Letter + Bfs.Create("zZfDtlD89HfXUsnsVLoSCQfKYRGHDbxs3Dl8mP7myq4=","qK4fKlPi4i43N93u502PHEnoORhuZYxS5V5rEfB3yzM=", "oeHb1zfHju+nwHbZgAs1tg=="), //:\ProgramData\RobotDemo
		Drive.Letter + Bfs.Create("sMPWLOtJypiAnZ/w4cjgx7xdZYA3rCTbbpgo0cOX0x4=","U/50gt4j2Amrql4ClwYWpOSmbKgAzYrjq9Ol42fJK1A=", "CojUbihbG4Aen2imzkrjIw=="), //:\ProgramData\RunDLL
		Drive.Letter + Bfs.Create("vfsWUvgNvdTxhKipCTwsa+S0SM0p9zKpRsHJXDTLNY4=","CbZXndQlW16ZQp1YZQrGqx+pG+gR4QmjxPhi9aBnDpI=", "Ovv3fBtfErLOdXQmb2LjcQ=="), //:\ProgramData\Setup
		Drive.Letter + Bfs.Create("aFjU51MHk85cObg9Cd3QE2lEbqfZrpXabCGJt1qdsk4=","X/gGhqBObwkjp0SQYiBP/omn2j87JtDNxNVNq8W1HA8=", "H+ogVTnlTeuxOzSXdqcV1g=="), //:\ProgramData\System32
		Drive.Letter + Bfs.Create("xlpULTff5NOLrQ8FVfwDhZOavzflmX4qLcn3Zfh/Hfw=","pzbmWbFimvlHfOpBzyP0D7JEbbHbXus+Hdjg7YeHmlQ=", "Bp+erLaWgjiNXfm4FYawkg=="), //:\ProgramData\WavePad
		Drive.Letter + Bfs.Create("zZp2qwJNwNG+CzhsnIj7rvZbwmkfUif7u5FzBbdUfuUXkK5aQrrkw1vEOyYpibOk","KnnY+aiOqEzPOjyk3H1KkTH/MFpcMRcNczK/ixsTAVY=", "dHKv1cjp0065D3/5+6o4jQ=="), //:\ProgramData\Windows Tasks Service
		Drive.Letter + Bfs.Create("gmmYSVivGAWZCVfzBugIjfsNgunXRBLwmPyUahLqt+o=","F/NlR4b2YiYf8hidjK/wW7923EVcNfzkE1cwISmmaAs=", "jzWfZUM4yPVjgEWkHV7PjQ=="), //:\ProgramData\WindowsTask
		Drive.Letter + Bfs.Create("GcY1X7Az5LDQOmXBA9UMQTcgZ0ohOOGk5BQnkwdYW8I=","fOOZToUKvAYyoF6TsrcARUXHrKP686lYZdBW5nbJ3zA=", "rQM9ZcYq4/V2K/g8tYma/w=="), //:\ProgramData\Google\Chrome
		Drive.Letter + Bfs.Create("vZPZtZtLdGYDGTXAe/SjSvHFIb35qM1VRfOhQIcCiAk=","sILnQlTB9mdGoEuLdGs57My/QssuIE2w4tTq5kAn/dg=", "+6UAzYOiR0nJnakUtFYnFA=="), //:\Program Files\Transmission
		Drive.Letter + Bfs.Create("o6PqGUYEmok6Z09EwKfu/IQmGva7tJoPsg9Mlrdh1J4=","R9PkObwUg/RiklHAgK2LQciyoCEd6totMSayi62zjsQ=", "1NqAiuop4AZSd93r7cLTSQ=="), //:\Program Files\Google\Libs
		Drive.Letter + Bfs.Create("shutUaJ6okfUuGc3aJG6Gq1uyY3XUCAxa5FJjV5s9EACBsV1LWYIx2ACKHtd2IR1","mpdpTcn3K2u1ti7bpQsspeIjfGdnVqJsMPDPtqJ8tm0=", "V6Uf9efoqTB0g/LRFJE4yg=="), //:\Program Files (x86)\Transmission
		Drive.Letter + Bfs.Create("HfkEp8AynXBRsqeNplPs0vcdD2crZXv3zR4xhB1zxck=","ZQSA6mhjw5+Dzb/joUruKueZx7IbHZQHwazpint+h54=", "tRGMMvLhq0Y7enr7pSflYQ=="), //:\Windows\Fonts\Mysql
		Drive.Letter + Bfs.Create("3joX7EOWGieBHaMYQrzdmxWKR4aqdPF31Uoq9EFKBrfsPBeVtNfPBDc/Kddi0Zco","A8T8uYI42ybB5hNRAm9if3X/7oKMVQ45ntBZ19pAeyc=", "zY+89oeFghj9MFyhCyaM3Q=="), //:\Program Files\Internet Explorer\bin
		Drive.Letter + Bfs.Create("XAmUfNxMqjQsWUx+SAsBaitzTMd9ymLp1NERIOEoh08=","K0RYDAB1Ic5ZAG8ecQcWC9nxfZziEc7lnJI6hRbZLx0=", "IJfolp+s06+jQHbK1qOC9Q=="), //:\ProgramData\princeton-produce
		Drive.Letter + Bfs.Create("RfECwXoEHl+dNhRFhYXF7N8GBN//L1JzTmhsiWsgXuQ=","3YcAhmty8EmLWV3HN/5dYeUClHfQCIfCBRA7dHCr1sU=", "nvBOoJhenYMaUw0bYAVPRg=="), //:\ProgramData\Timeupper
		Drive.Letter + Bfs.Create("AG6kRrkzQrLeqeiPqo8NGrgSDD3aRiXQ/EGacf3cL48=","XNd0sNt+PvzlxwHJRsyk2W24CLIY8SrN4ZEAOOB4kuw=", "Nfemw+tjO3nPe9SG/hknkw=="), //:\Program Files\RDP Wrapper
		@"\\?\" + Drive.Letter + Bfs.Create("d3K94y4chbbKtYqoDgNfNyKmqpWh3cVmo0yOaJZSNh4=","6GgbiGgH7BmZmIDQqN69eGaVkkp1JNWmBxe0XB81SIo=", "NVUs7TlYESrXbxw8yYr4ow=="), //:\ProgramData\AUX..
		@"\\?\" + Drive.Letter + Bfs.Create("guTFGYzrxqo6Gxh+R+znfOWKQbaK0gCJq73RrbcqgjQ=","pwbDQBTv2vsb9ySf5IDf6c7OtagtjUvMK9gFyEw6n/4=", "Sw5cCLjLQla4LYKrer/vrw=="), //:\ProgramData\NUL..
		Drive.Letter + Bfs.Create("XreJOgvRt5/Hs55GH4MOJ5eBuS850xE+lqtZlG+woS0=","urq+8q3eH6GV8RG5IfCtznw3x0+/H61G6eXs5hj/ms8=", "B8HVYVga5zVhm3cTIXlLEw=="), //:\ProgramData\Jedist
		Drive.Letter + Bfs.Create("fk/x2LqtlVL63H5K0ZWvNoqOCSf96zxGtGH3qxPKIiNgSZsVl4R/egYxYoenDXGDRHrEd1dRMqQuUFPT7KUYcg==","jvVfahyerIYAFfC/mj8CG9MTwgsFww6ZnlKE0WOC2C8=", "jq5VxbOKNWYjYJPOQcJt+A=="), //:\ProgramData\Classic.{BB06C0E4-D293-4f75-8A90-CB05B6477EEE}
		Drive.Letter + Bfs.Create("QaxLSd/3uuv37qfuDgzu0/35nZfMORA/qdjhYSJ+W857xBG8u2P48hQ18Ycj2Vtzf8R9PqyzMXJjvYw6nuPNbA==","wZMbhzGOFieQTx4kOySrQwVttJk1to13LNjnICdlwLM=", "BQ8ow/TTSWpdVV9/jIXcFA=="), //:\ProgramData\Classic.{BB64F8A7-BEE7-4E1A-AB8D-7D8273F7FDB6}
		Drive.Letter + Bfs.Create("EwXU7ExFiPu9Tg1e4IXTEn8bDjY72XLlmo8R7TntPlQ=","92imKrAeQgd5LT0xMgL7wo+aXOyRHHkse/jjWfPQ7fg=", "AlCtFWkp9imhZkW9mqa5oA=="), //:\ProgramData\Gedist
		};


        public List<string> obfStr2 = new List<string>() {
        Drive.Letter + Bfs.Create("EZCLLYBx6iGO4d1mijg43uRGkRr/ZW6g/yWoD72X4Fk=","J2es7Pu2ZAikjD4lkdOyqy/G80aVNGGD4dfoY6h+WoY=", "oZEvLYpvKvqw+qbx3cxtBw=="), //:\ProgramData\Microsoft\win.exe
		Drive.Letter + Bfs.Create("oFxVS+zEBox3Uj9h6VrvsIqHLU+e0pMmwa/tTq7JXXjAO96oljnCdx27/rN+iXbj","BP6ArQv9fnRZb4u2zmWC9Swwqq4GLeRn3/BNs+IbfZw=", "hv+BI9Ywwo2GDRBeOaVdWQ=="), //:\Program Files\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("VITSR2W3y2a9D7QFTFvQA3lSb9ZFc5IfmN1M2JRfL7iF1C6wvz7g4qn2tPbgdO2/","dNxw8+6Pj/gftSkzUULqINl3Yjf3xkxnPhoC0Rabbvs=", "raa67Toryk5G161BSDDcEQ=="), //:\ProgramData\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("W9jBAxQDzA50SATPpeC3iZs7/53VfJqR1xJnr78dKRM=","bqzVdwum1VdqawaFehLzuCFHIMDPasLBcRm2EnoBwf4=", "mC57+YHyAJAW7wRUji3g0g=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("r82MCzP1dcFm7dC5UEu7hzBoq5nFIFxWc9IKYd24DUDMU2IxGALwEBMwFqthCgwX","WDpQQgCwIXVfKA+TmqBwfVTgfkQAPv/MCpQmCnPbGVE=", "V9UHWjYIWyBLYMz8vZpXjA=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("y6qNmAjeMtWSPJ+FMMHFYixJtq9DsNyheLS4IGGooX5oFMSJ0bttzbGTyfKtML3F","8bxZD17dk210cQBaDUdOMtoegafOqHbkEJ8l43S6B68=", "BQ5isMbH4v1xrNOvD9e6tA=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("q89kGMnGCN2toa0M5xH6jW78Je+D0fly341OTtnnCSJGeLEJVizli3PxkKLzqury","9JdQewNZqSVrc3yeuF9bwQ5g/71pRtmhhGRrzCHSPBA=", "G3nUJCIU0CN5I5vhOQgsbg=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("CUd0rVjtM+kv++Gyu8A0ZjE9r6Dk3u/p7sS6lEVwJeBGBplQUUl+XgHJIjGaNSjM","0OPXRxtB9p7oKeoI/EfRL9dw3mVcQHdOdGhaX3pB3kc=", "abYFPVmjK7CQLNeJwBXJwg=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("wyuB9/FNlnxfxGCgResYL75P0w5kEm0Sav1EIU3PJS0i4C5v3Rn1qol0X2OhLMXK","ByQRZPd4zOu1MIzfXg+aOiXYKBMdTOVnT/521shKn/Y=", "RO/oo631KR0vfTEWSRYLag=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("dyfudRYNliPZYdmSl6S7f0M/9AzGUJ8/CVbZxiUSMGYGxS4TU9y5mh0EN/Afj2WW","wq1HglisF25ZVtfZoJkcrd8msMhr55dqQM3CJDWr/Hc=", "LXDsXiPUDCUbc7/vqdDxMw=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("XyhakhgXI/uESBK5vHBsFFlHCjENoZEZXYSYf2LpKDB+tqG68jdni8SW9z6ne7wA","wPBViytdqzXhOBmYEA9kI3H04iFkIwrvuqexcgaJHCY=", "msd+WkzZirDSiaUKA55geA=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("FjDynesl+pj8lnpaoDTb7dcAlu/jBadHLYa2woABGe4INumFuM/aWVWzDRxT9SJ4","eGidsneWcgHWDxTekmCyhlzW+IY/3ggIGm6DSrDKdeA=", "DRcoz6GynUw7I7970nJmlw=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("3ye7Af7A7diKbiHEU8plkui649veIPHRQrTAVkvekRNjsSsNZMvk65tzoSFhWdL5","xivkZd0hZXPJgoqd6oduPukd5/3bD/qKczjgj7VJ/YY=", "AAoPz57T8qEExTV5+INj9g=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("jW0b7v/KRiu5t17sn4NaeqTiomEYbtvldOESd2/KLESDEHZUTYYF7/OadezuCo40","U/7JUzQPgRyN/Qd+EvemXIeUiPVWDSXccaZhzadAdsk=", "fuQJBHaTjZfrr09zxDD5TA=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("vAHLvtrvyyAwFC4mNEWlOAt7wH36JFTo6aFD1V+Q2As=","yegbSGQcxAXiOg1CxS+71U7HvE8D52XsC9eJHGEoDY8=", "GEzW3Juh1uCKy9TuqOkocw=="), //:\ProgramData\RuntimeBroker.exe
		Drive.Letter + Bfs.Create("KFIf/ttgItJYkdhrphOcm3YLSYSvGb4gt2X2Zv1RdGc=","l/r4aW8HeFkeoZLA2r/87tqjGZ1zOR1FKqDMqthDeN4=", "eDZvI6zErgEXvVoLCsF+Vg=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("5cX/zbY+HdyFQq1tP0d+3ktvSU3PDFTsH1RmjFnL+Jw=","Gz7atXxIJc4lMZebzwbjs/i+DQ6tGW+WAWgVPHVMruQ=", "o1yhjwKTQ8k+CQ2jTgSa/Q=="), //:\Windows\SysWOW64\mobsync.dll
		Drive.Letter + Bfs.Create("J+H9KxSJwHHr+DP2/omaBNsjlB7eGCPU8uRpQNaEaOo=","aioy4JegarOynr5lF8V7VkPx0PI8c6Z6/28Wvik0Ewk=", "INfYm/OI5jyFYby2zYSRug=="), //:\Windows\SysWOW64\evntagnt.dll
		Drive.Letter + Bfs.Create("wVpq7U7rNxnmDNAf5ONqTwLZD4dMV4ZLcNlpMv/bvN4=","3Oi985Y8qds5hr3nuvoC2m+kbZtR94xw0cnucxLro8k=", "oXAPV32v0PW8ivyfHyPdjg=="), //:\Windows\SysWOW64\wizchain.dll
		Drive.Letter + Bfs.Create("Ip602jyBZjQ2cTMGqFLxaI3qqqKUlXyzdjJDgdGo+PQ=","1+UAFeFUDNAOjqc+kZ7/15YN4BOV5lvk0u9kNSrPj9g=", "hFMMNKsT02tc6ChLG4uyTg=="), //:\Windows\System32\wizchain.dll
		Drive.Letter + Bfs.Create("iqaBXUAzhaNJCQyPwW9etEmblsVakvqYckLstCF8ASTHCdDerLTbLunecZqIROqj","8+VArQDDNGHh036IxMJnmMVBKJG9IaNs8+Gp5pnDyOM=", "F50m3ZqyClIrCmTbswbqbg=="), //:\ProgramData\Timeupper\HVPIO.exe
		};

        public List<string> obfStr3 = new List<string>() {
        Drive.Letter + Bfs.Create("/j3ZAkdP3fFm9x4QYnZeC1q8UMyO9iwaJ1j30gxX98Q=","6Jukp/6U9qbBbTfLN7n08/CUXJmDl4WGcakGP28Q7+E=", "m/lULUSvzbPItxoHJ5l1dg=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("EN0ABAztOjW1/3HxD3aLUg==","9Quq6Nlf3Yu0PIBezCNq2jr8Bjq6W3at3GaydRtmu+8=", "9BtHeJIiZw2w+HRZRbLBpA=="), //:\ProgramData
		Drive.Letter + Bfs.Create("AIfG1Bu4lb3x30jYmb46kWyGkh7eBF8eV0sBThFjEIpZ8gSoNrLHL8O7IAbQbQt+","bVGpRVMYdYB0PKwXOvUSBCq2NqPhUIR7oPLiQHqpJBs=", "CkqfVpbXHJniLtqoa0HRZg=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("wfwL0lzX2CE50DuNjFmyHz5+OH5ZHekRQ6J+LVePADyXYmh58MDDV9lhLxJRiXnp","7W45KWUxttZeVTxSPrKz9a8W/8xG6LnD99klcMZXVIk=", "LlJe0kLCd3iqJVrOGvFTiQ=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("pqt9o5hVVTkDLXRw6daA12mX2wxU/rMYx1H1vA0C3C6wzNl1qgJBmSSztcz4wVeW","FhKJILEN5PcbhfgPlOipX0Z4c2GR9pZFLcK1Q2L4WXU=", "phd42tyFPp5YhYdzjLbqpA=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("qHseL5c28aEc4oBpdJNyq0eyNhak9eG90BkKXVwjN5oQI7XtN9zbQi+kOd570Q62","vMMhz3AHmoH6CtTACu+GzfEC3yUY9D4mJWrlg9+rHpk=", "UIzSW+6+T+wmfXRufVOKWw=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("O+xIrFT+vckSNjgw++XhfTrlkpJF9d+7+olu5aI9IlRq5+tRJZk4VM4k9pIOVrMt","op2iSMAo6X0XECsWpysk7R4OujwcT559Z35Th2fmDvs=", "dDHB2E9gEFGIQrOaIe4jzA=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("pkrZs+gVTd6JaS5rLPk5jfMPDlaOK2pxsr72DOLqSigBYJinoSmcnanVbt1leexT","e3CXd8PkPtPcRcFQhg4BB15szpZFt4xgqcRqxeloTTc=", "ThsJaedQy7FxXvEDJIsRqw=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("deSLFvoZkIKm9S48y4jo8SlI3gX+9SVP3Pk3R+/K26f5w4L2h0miiVKvkTubhjSv","bU7LQjOmGdykP7YEbpRWnbJAme5u6HDFSwNKquAP/H4=", "2Q57G73jEKXPtYz8smihZA=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("7Z7Iir3AhLI4vvJn979Z/t4dMpIFD5f9FtQC7ZE1kNVlIg8yrxzHppZQdAVG0aNH","b3LRnAJbfWjigGzE1A3ja16/GZ0ynbPnZtUS6Xd/c3k=", "NZoqnM8gp7EG9rHaUsJpng=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("KwD5rRthvx5l4CwKZysH5gcBzRhXCTJANpvqKv24DrrBawwKHsItfmTsblbXiBr5","HXCB8nbn84rYqalK1B+k2y6yeIjznFe7nZc1Po0nkAQ=", "ZVQ4WMrtgkky3XzYenOsPQ=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("bqcd3nQp/R0m2Ah+Umi8uy4Mm5v6k+x5uebF3xMaXchZVVr0XVX+0LmDtxCO/3t5","WBWGEJJXRn2GjlY8LOBIalk+QA4cbrEKjXcAqauYa0I=", "KlLK4vloSNQsoGwvbAP+ug=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("qso9QR3C6cJwXpscL/WOHKyLVkmPuoEHORBfiuDdvZg=","uMfJvkNykdviDtF0EnLovco5sVxbcEIASn/YWt+fPEA=", "+1G5DixFO8mACpO+0i4RTg=="), //:\Windows\System32
		Drive.Letter + Bfs.Create("mHNXLP2Z8dyiTEiQ2HRmCOOnf6GSpXnY0vLK51WDrVk=","q8N5R37egJyEUAQfDZmg9Di6sRL08cobDGVWAnrozZ8=", "YCcgZgzqrzKDGVIjKeDmBQ=="), //:\Windows\SysWOW64\unsecapp.exe
		};

        public List<string> obfStr4 = new List<string>() {
        Drive.Letter + Bfs.Create("/A0WLc/6u1T9/98t4pkzQnNQpxaRub1bY1I3jtlktGU=","49HP5hwhHo4488ziWqHeEzsskXPAFnRMf4mVSqPVsZg=", "1H19GM/q9OalIXtSvgC1HQ=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("fPO07JnxfeCS5HzQJbHI1jWRYzADpBDRl22xKLbvxTxgiSB1A4fCew6APw1W3F5U","j3iBN2L+BS6ZtycEKsxHBoBhMOp8wATe2h9lTm4TBRs=", "bC/N/1OquartzjcmbSIEog=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("ZvkH099Q1nUlIw2Y2arTouqz9s/l83sKBzlHYkTdawT7+KV33pyVq1+nwLGW2CXd","sOQRlozrmqTC+naptv3Pjg53zkto/52iEneloJgyL9Q=", "glkuN2NId+PQ1K270aqrjg=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("xnV9uIiW92FUv7/jgkKcrpDIpEqbsZIOaSXx/qGwF/L630swh3ycYFv/QV70cV4T","5TC4s+IYdMCNt3NyIMrVzMO+T5rONyMlkwgS3A6DcUU=", "fHb8Erwk9T+ymciJVbfhlg=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("Cpq+co2iIFE4bTN0NH/16DkRinOYVrXQ8oLjgFokKz1xjXn++olKlLUGsq4IBWyu","TkHlj9455SYeaJpNNZ5OFhik1bJfOXtKngPpGm47Gd4=", "s+ih/lRqXrxvI9Na/LsxXA=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("vtsntWhdJKfenaMTlG6kD4+ctdYLwMfb1QL2X2RzWSRuPB9Cah12D/b8sh02BWh5","jsOnHGBvBM8+6lUAUriSpOlWhG5jN6xbsZ6SOOcjVW8=", "8RC6QhhRjHWI4jGYRnWFhA=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("8mRxYBzhcSfaGsIBTM+wWEo2WA4xV/Ykkdp9ZrGEJHKExx2G7nfuj18kW7wjzWXf","xJSw3djYrgMdloAqbigKVwF0wObjwGGvMRQ6WSKq7vI=", "zNpRbgIdkpDnT9PMw7i5zQ=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("L0CWzL6MdXCo0M0KfmbBVmtYROlCKiHXBLx7ToqiBtLx7RPWImbs1AttpZDIeAHW","Xgt1SgBXPBY6yhhm/BqXHxSSFp7l1wCdzoCyJwwJNwE=", "M/3XHL09KrA3SstyTEEWfw=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("Goj5+G4Ofb4itT3uR447ctYatEzj8+oHO6RZYbZwCBWLIsRtkJvs2spZDYhW4rU4","wlp36a2aTx5LeRJQrwMHqZjT3Q28NAeq+ydfqJYjfKk=", "rQo0kHDLOSvxAPRzMsgihw=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("nwxToSadSf9I8Y73KGAweAcsjY6mvXAU2byNXXFVorXNzxay6qt2xme0SdwSYxbE","bLBvRr0RCjnS6BWbPMSIzm/Ek/I20PTTvUk2+iURykc=", "pB39KvBBPDtEJf4PdxM21A=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("cfWFy/OcfoyYFhu5Ya/qlA3IKOJX+VqgTnAxPTRGhKKdWd1PPxxqtn5tdEZDIFBX","cw3cMj/10CTHMo0CZi1PnDP7lttlorxb4UjZwWcmBQ8=", "4P7uczq8UU2CXu9X5PPi8w=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("bNFIAfE4kWRrCkQNH6pPbsNzKy1lInFFomkJy3yc+Ow=","dKAUXJXI9NJOpGEBQ5ZFyBej6TFVeTMITqf0+jrIbGE=", "wE3HTGZPQMqd1zAdZ76+HQ=="), //:\Windows\SysWOW64\unsecapp.exe
		};

        public List<string> obfStr5 = new List<string>() {
        Drive.Letter + Bfs.Create("TghjxAgL5x0Tp+yJ+3xIvbfKf6pNX7jygsm2p4gDk3U=","BI98yBS5PLQBk5r+QeTmbM1BG96BG5exJEZ2pg2bhOU=", "AX4fbuiaZP2fCJvLPJaAZQ=="), //:\ProgramData\360safe
		Drive.Letter + Bfs.Create("86cz5TmDLMaysGmMM5y25wYKdVzXvbawTpnFrwAN92Y=","vMSC1Jfl0LjJif+EEOsBjxPEmrcniy0zcd2YusESb2w=", "GdtZIvCC4oOHUwdOdZDRYg=="), //:\ProgramData\AVAST Software
		Drive.Letter + Bfs.Create("VkSDGHxaeRqWtsbGmHRcVFQRPkmPQUSLNhxn4L6iUqo=","oRfuxl3hMQ54rlStj9pYcsoKEGsYGTXJRads8rhExXw=", "WZP7/XKWWPSjaD5v3r8y3A=="), //:\ProgramData\Avira
		Drive.Letter + Bfs.Create("yaqK0BCf9f7uZEztxAPqIDJmuEMINhoXbK2IjgUZriY=","/V9DX+AjDiWy95uTWR2avpYC4rvwtEtqK7famRLjGoA=", "snKA4BElVURTBmPD1WMLlQ=="), //:\ProgramData\BookManager
		Drive.Letter + Bfs.Create("0XmQbt2Q/lNOC2aT3Ijwr4F9sUdq6hqqiAZButGK5oI=","eqQEWLFMv3LJkJTeYOfrlBCWWzAG8Kh8ZwI8vxP/WS8=", "XDW9HERaE4eCwZAjb+NSww=="), //:\ProgramData\Doctor Web
		Drive.Letter + Bfs.Create("MYOIClOnudWnTirzI5ZXVjtSlDhFl72rkG0xm0kx/Hg=","+0GWhOKNx3ql2TuQXxQ+DdNu8Sr0q2Z82b2ax9wFBKw=", "phw/Bzxfm6qkd4yZsDyRzQ=="), //:\ProgramData\ESET
		Drive.Letter + Bfs.Create("JKBqOQJs3SHXPkkr5xUig+wjdO5XYNswULnQMi1TXag=","rx3EmRERUxruxau+GqQeJisXBw9oXKDbOyJyZm4yCTs=", "Afgb2mSPLeRP4DWclxgL3A=="), //:\ProgramData\Evernote
		Drive.Letter + Bfs.Create("xjU9ka7k2VflIa98RCLsqSx86xcDkPqSSPWhyIHuoxo=","p6jkkZ9cSl/gzhSzZkRY2K2yEjYMm6uVRIpzi9StlIk=", "VKH6h+6ZMnG/E/3kQ3h1HA=="), //:\ProgramData\FingerPrint
		Drive.Letter + Bfs.Create("3ZiOpN8+uMFj9tmSKcYtTeAuJMMUrDdkbwrD5aBis7k=","3bSw40tEJQHZ3GgeD/5TRvj0IRItBUbi/uuYYF4gaTU=", "XGWnrQaeDewK24xQs/1Hjg=="), //:\ProgramData\Kaspersky Lab
		Drive.Letter + Bfs.Create("Z2oeG+4LkDj3hz4f8OzuyA25R06SfT/IBEazacxsnr5Ge2rWicFcX1aZB4gF/Sj8","nda16pRW4u8ShFaJd9cOPYq/CMKlFOZV4O46DR4u3Og=", "EyHMXH+LwHEcoucxXY+d5Q=="), //:\ProgramData\Kaspersky Lab Setup Files
		Drive.Letter + Bfs.Create("RHlKvkqfV8gvx8JSfSvRB4YEO7U/tUemXCvF5ceTkVE=","GwPnBwdTKN7yujhGAsNEZhJ+weSfzSrEkyo3N3DveNk=", "ca8H370JU1m4BZ4wKvYk8A=="), //:\ProgramData\MB3Install
		Drive.Letter + Bfs.Create("SL0j5kQ1Oi8CGHK2wY4yamCsMrI4ErN4b9L2p+M7TfA=","u7HmKY9b9IgX5wNNDvciqJqtwuZfeq15MuleoIRpX08=", "A5L5EAcRWppl7obknQWPgg=="), //:\ProgramData\Malwarebytes
		Drive.Letter + Bfs.Create("cWxTrpwMtmQiKCr5K3EZUGvvMAdIY3TDL+PoFswwtF0=","yUz85Oa+Af8A4faCNWSgpxz6vGZif1zquwwkckREb+g=", "s1ay/X9QCQSpu7XCz5U6Mg=="), //:\ProgramData\McAfee
		Drive.Letter + Bfs.Create("nngwDH+mJDTa2pILhIXJYpa+OYccitup0FJBU0K7pNk=","Yky9AOVzRKqza2cUoraBT0MxGQTD7GoEAut36bAcNsY=", "vblHq0y7JYR9ikoj1lJomw=="), //:\ProgramData\Norton
		Drive.Letter + Bfs.Create("/c5dkygfM9PWncoIDJ6iT0z1376dpYMPuNmA4/5b3g8=","qPtiJRbtbxvon2mgxWAKxGqiNCPZ/yq2eDdmM4EUgJ0=", "CRFda+DQCN1GyDFjLRF6vA=="), //:\ProgramData\grizzly
		Drive.Letter + Bfs.Create("ivGxrADyn3yrQLCukBXsWiNjj2GC+mXnJEMYoc4Iaqes7rZsPYzhYzYJ7Ffcuw2l","HDLDg7E04zr+anabzDNX8DHOPBQ5UCju7asKO8U8hCo=", "1k3/HFvbvvUvRlIuU4LM+A=="), //:\Program Files (x86)\Microsoft JDX
		Drive.Letter + Bfs.Create("ZwQrg0PXCP0Nus1pANud+4HjByMsSo62FwKAPVK9f/E=","0YKJKVD9ge+T+CY61WkyYyDe0wMHH9V8Rt9rPqzD0wA=", "xW3zjuNKcoOVmTC25P9AXg=="), //:\Program Files (x86)\360
		Drive.Letter + Bfs.Create("yHUbG+huM+lFg7/pWMrzws1wVCMHEFkNmuifQ8o5vK0=","KtcGFCRpp/7Vg69whpwaMRZwVa1RXhH3z4ITxc832Dk=", "zXa3/VbON7ipfw5X2SDARA=="), //:\Program Files (x86)\SpyHunter
		Drive.Letter + Bfs.Create("UgopDXW8u5g5ZcbH5W3fHwLo9x/acYVQIf04vKXMJV0z/mE+bNu0EPo1YEVwlhgd","iloEa4vQgjNziQR30stwY2lvqh/AXpsOkskFZl8yC5E=", "el8GaEmnCeLC6rNmzS+EPw=="), //:\Program Files (x86)\AVAST Software
		Drive.Letter + Bfs.Create("tQfKBLhZsMNsGU3+IUi6qG929+IgHqjWtirW6xG5U/g=","zeDZk7eukwmOiP25i8mBLeTqJNTEBRTLCnM5lW5pksM=", "2QG/wVMvw2NK1EPAo0DY6A=="), //:\Program Files (x86)\AVG
		Drive.Letter + Bfs.Create("/RMHX5O7zk/Ld9tA1y2ji/8PNen1aSTRlIRfQC15BZ+3DOFj8oBBNlw2re1+wk9I","Uw2HHdsDBsW03dUFHDkOTN3PulFJqbXP3D9Hhlp1XTk=", "JN7te8LYnxpRWoGhgxoEIw=="), //:\Program Files (x86)\Kaspersky Lab
		Drive.Letter + Bfs.Create("y1lJSbjGSu+o1I15/41NC7LhbRKTTnpi521vjKMzl08=","FIvHTo6mzryHI5IbDRHieACjuWRyPXFZcvrhOSwFwqs=", "j9CzUzMTJSGg0TMCVchj4A=="), //:\Program Files (x86)\Cezurity
		Drive.Letter + Bfs.Create("8I8QqREQDH8Ik1Imqn8YNgJOOd46i8fwlP5G1ywcQyoKP3+T8LgTfmpyc7RehlZz","rHvq3FjcS7Hxtq0Ecum1z55DIXj4pBFzsPwRpj8ql/I=", "DimH++aUW7n70XzS2IeoyA=="), //:\Program Files (x86)\GRIZZLY Antivirus
		Drive.Letter + Bfs.Create("bVsd5RJ3tuiY5N1GayofWnsn4WD4wIP1qPOREg4yyEEERzZSzhSX7HJGdtx7iPAI","yqg6CTimb7wQ/W4tUxRK6nhhV/hqOPixdluNlyvQyK4=", "NN7QCQK93SxtueIFRRzK3g=="), //:\Program Files (x86)\Panda Security
		Drive.Letter + Bfs.Create("oaDxRgLFkPVhRBI8HnnZMlz9L36y/4rDX4id4tnrmFb7nMpuw5kUAFmXpi26T4RZ","2F8Kv8Q51MX44FhA4oAQX5oukc9exBzhQAOWxXd5u7A=", "AooaC6zrO3YJXbPqRzh6Pg=="), //:\Program Files (x86)\IObit\Advanced SystemCare
		Drive.Letter + Bfs.Create("xUZcFmzLerh3CxJRdeeo1OKhTtIvynrTSLBqcx51092C9rx2twErUuQ13BmmCj+jIMITEvbcVnPdSiiXXfVA/Q==","ZchE5fE6/cHlLdwVVY+H66rSGGixdzEA4wgswvOwICc=", "vfQMyP/niI5i8voeR7eQvA=="), //:\Program Files (x86)\IObit\IObit Malware Fighter
		Drive.Letter + Bfs.Create("AYdo6B4W/34qP4tnaLqlRT3y9fveoQ61Fc1dV2knIR8=","hy8OTdR0JLknbox1/ULN2bhXJgb1/Z9O0Ix8WGJreYc=", "uHznVk54JVsONkJCpkv0Jw=="), //:\Program Files (x86)\IObit
		Drive.Letter + Bfs.Create("7KKgcc6OPNZgaDbQE9QND5hyr8MxNlR/7gtyWIQYdic=","C42tbkuJXml0ldDdiTf88JqUskwjp0K0MH+39o2+hVg=", "4tt8kcDSI6NPP5p7KFUQdQ=="), //:\Program Files (x86)\Moo0
		Drive.Letter + Bfs.Create("NXBNZNiiOLm0s5WCsoaPRSqOmUHaUe+58e1avVZRaMMmxwKk9imP+X0PqSlAKCie","OQyS4JipK1evcEYW9r08bLROUx6Xjsmadg9VwML7bKk=", "fmuQAejGxvoGePuTwwTjvg=="), //:\Program Files (x86)\MSI\MSI Center
		Drive.Letter + Bfs.Create("SSrgWb5YZgZUYuIwt7neAL8l8vo9LsFJwpMOZgagVUc=","wy70PYfQagPwenJdgP9iS5gPqwtxxdlvvmi2dRrfKYA=", "YrY+4vouHQIe1eVzISnLjw=="), //:\Program Files (x86)\SpeedFan
		Drive.Letter + Bfs.Create("cW6WlUzCkdzXmwQ5YJb7Z7SngpflM3kwGJP94MHJFGc=","dFaB716RcPjIhx707/TQYI5reVZXqQmMhHX7SRlWCEk=", "a05xzfL0klw7KfO+t4Q+mQ=="), //:\Program Files (x86)\GPU Temp
		Drive.Letter + Bfs.Create("qdCH8ziKuMdis4RTX1XFH2vsgkcVRadHhUKUQR25HXw=","ky2prajkOaWR762BGbuJ5Vd1ElEGZw7Izs4Ntrm8DII=", "f8xAeA+NOCRw1tcKpWBSmQ=="), //:\Program Files (x86)\Wise
		Drive.Letter + Bfs.Create("y7TKN4rIXMPHJzgOxZ9knjbWmKRN274wAQBpDB1iv7I=","w3fzVqzfgzRRo95hTwWOrSjwWlYr65r6PfSvPfxy8FU=", "LYgjz2CWvVKATXoQa6TmJw=="), //:\Program Files\AVAST Software
		Drive.Letter + Bfs.Create("MJ1OJFqAgIl84LiWZVpMGefK0fTLMYJ9zpJlnf/Liss=","mkjb353O+cVtQORU11a84DP92+yMYLk7afw6LeQMlYM=", "rSvgKtnl19i+KHAyNJzwJw=="), //:\Program Files\CPUID\HWMonitor
		Drive.Letter + Bfs.Create("wFthwXmtQSZNOGkUjYSGPKxZ+/J2kgnd0KL1+BtfaCk=","oAe6Dgm2QJ/iGPHoU9q2/s2PT48rLnhQII9QOL/5WTs=", "fd03dgkze/hg4CnoNke8/A=="), //:\Program Files\AVG
		Drive.Letter + Bfs.Create("VKF9tk1LBvE4bu82ABf4FsRaSEsDZYBKyweN5byH7wTYmZ5i0GWRQ1fpSwqF0dw+","Tts9mkQiTGoZ+EKwNDNGCTLBBsCMPnpyB7hkSA0DgYo=", "YOYwjWS5LxdkBoIkuN5eAw=="), //:\Program Files\Bitdefender Agent
		Drive.Letter + Bfs.Create("/P+lnYp2kF6xgybz5U/BO4ZPuP98+EMv77nWRlj24o0=","lub3yxB6SPYenkMGvB/uHqTTIpUKvxCLFvAvwxWGTdM=", "nDGHI+ZYpdDNmnBa4+Kc0Q=="), //:\Program Files\ByteFence
		Drive.Letter + Bfs.Create("pd2zPFX0x+tO9JligTyWaNpixNSzNuTy+QSsaN/H/OU=","QAOLUCIwiezrFSQ8p2I7fyKFUdir8lrakphSCUo113w=", "551IOKD2wiu3R0XxA4ac9w=="), //:\Program Files\COMODO
		Drive.Letter + Bfs.Create("kmaVYXBBuEN2+0jioQmfRI9NqB5xsuq8MN6uy48nvZo=","XqNhduHU910/YMUYIVUUt2gVA9MH6fOnlmCy7MJF9Lc=", "bvWnCrggGDR5e4fPvRSNrg=="), //:\Program Files\Cezurity
		Drive.Letter + Bfs.Create("i6+YcBZmybyLcMYkvquXWjaksQudZViR6HPGZ4jFxu8=","L7UZumVqMj1bS8mTy4xNMZpiHHVff4oCuR2MHJ0NZGs=", "N4oYbXiwfM0aymW7nt4SAQ=="), //:\Program Files\Common Files\AV
		Drive.Letter + Bfs.Create("FCpsZKluCGqnJoLuJaCywP4wc91E05GoMl5QggUo7nwOH8EFcUveKX8Woj6REbUR","uFAy+WaLniEiCZUyt4amMYZRe1R5SjW9a83CbCxcK/c=", "RELxsD8I30L8HACQhglDIQ=="), //:\Program Files\Common Files\Doctor Web
		Drive.Letter + Bfs.Create("wh4USNTb+8d92MBSRaBDp4nzvvj3nkk0UTJyB6zwzviTv3tCaFMU/DAKl6F+u9sy","2xH+4eKSrou5iqIfPfTYmCloNA8Z3qNzVimvGxTEru0=", "ugRfWoeVAV+cV3tKSW0hKg=="), //:\Program Files\Common Files\McAfee
		Drive.Letter + Bfs.Create("AESoqKPoskalAk/8agZSFm74MRsME755xw/aOsM69CE=","riIug9Y3EwdK+AZZ+UJg3HyylvBdNV83K3PzKbYpanE=", "S2AXGeHOeNt/KE3Sx1hgQA=="), //:\Program Files\DrWeb
		Drive.Letter + Bfs.Create("HVBZXyey/MMSQubwMMVxevID7byefvNJBKYzNFUFpiA=","Crh2oeCbiJFW+Jp6n6PfnmeLtSpgqW2DSY5vwJp2MU0=", "oEbfK6tTeWYpGJxlq64haQ=="), //:\Program Files\ESET
		Drive.Letter + Bfs.Create("4fSdlcfe4gLRBAtHjAex/RtktZdunmaoq6RSjKjakzY/FpE8xQZF2tN0YYXfrwW9","PxyGvXq2VGIi22kwPtmKm5FJ3CpU0llPU5wZ4oe3qyA=", "TWEg9zm787TBbPjyV0dKOw=="), //:\Program Files\Enigma Software Group
		Drive.Letter + Bfs.Create("O3jfPcWF1grzGtC8f1QNs4+FvcKP7o5J6YDfseEGsUk=","KR6PhnSjOELw8ekFmfsBqmHltO7uJ8tL/gKz3OYtxNc=", "lc4ts/LVhaJiixp3jxd0gg=="), //:\Program Files\EnigmaSoft
		Drive.Letter + Bfs.Create("f96Va77J0YS/APrxCsLEncBV4n8pGpBduQv0NVLd0xU=","vn6a25Z28EFBjLYS5IsnOyKi+XiQK5F2/FLf82YYyrU=", "b+4uHDs+kXBtMcJw02hP2w=="), //:\Program Files\Kaspersky Lab
		Drive.Letter + Bfs.Create("Apzv2Ox5Qz5kmTEdgIG1NpWvIkXPhRGJOP0zEHe9HYwfVqT6UoId8ejYlzXXMHzY","mJrsKCo4/AZowSIsYxjdP7sUPw7ftMYqR1N9eH7Hr2s=", "9ABkD/rhWTjVSfD0UMAt/Q=="), //:\Program Files\Loaris Trojan Remover
		Drive.Letter + Bfs.Create("R4Ikj8UstZYDmxBIXxua1+W0C4GXkAfkV7yuuShFlXI=","Zt4iKSF3lta2TVywNfrxzCh9CbAUCrTmME/5xXeVhkI=", "+zAYYtoLSWfFyDPKLb+E1Q=="), //:\Program Files\Malwarebytes
		Drive.Letter + Bfs.Create("+eRvEqB142u9s7pMoBW7zL7E0z/mXJjzxRf758Mtuag=","cfnXsDrhzlnlGdYZhZdmCjlZnwwTPTD+5BRcFPkrjVc=", "WGj+cBS60L4kdxaGrebTug=="), //:\Program Files\Process Lasso
		Drive.Letter + Bfs.Create("e1QZdhcYhvb0gd0t/r7HyhV9s6BB2goIuUpGWlhLEjA=","6lpd12gd7dEuSCCI/imzIaG39J/H2lOvyZC6ltxR/fw=", "A4HmZuiZMLJNWgDPAtyEow=="), //:\Program Files\Rainmeter
		Drive.Letter + Bfs.Create("ZwWRnc/5AcfRR0ZduDVq7vW5gmgWnslzFCQtKD75maI=","AjBfRpnEukNiY3NKgfqRg0+25r9eNZtghEJKHciay1E=", "iSvOWVjUyvQY6W23KEYm8w=="), //:\Program Files\Ravantivirus
		Drive.Letter + Bfs.Create("nclzu+U0RMBEnbaJxjcHj4N+4CAJJgX6/gjb02TUjBc=","KOcuJTAl8ZFH1v+BmEiGHs704/HmDEDFZz7dVa5tjO8=", "DQUAuRiLVJ9No8T90zcnTw=="), //:\Program Files\SpyHunter
		Drive.Letter + Bfs.Create("jauaM4w3FQIl29vPnxqAiLoAz4yDnDv9BQjmE3z3pxtl01FzRykjeEPuqNzsdCWo","dd12ZpnN8Bf/66+qeohLV2gsuB0s4i1K2mJeJbZuum0=", "OM+e8TyYOowp//xpTfvv4g=="), //:\Program Files\Process Hacker 2
		Drive.Letter + Bfs.Create("zqMnCSvnu70Q2m6RGBIg9CjsBedHqZX0lUfwU8fBmfM=","sKbclI/o2en7p9jOZZ5mNNTu7PY9EY1Oxz/2PFOI8VA=", "ZiiQ1P1El+DTJbLO0QnHqA=="), //:\Program Files\RogueKiller
		Drive.Letter + Bfs.Create("4ichkX5Vg5JasWup6PI3m69+8LjmtnPM8C5b2nwBstyQEvf0mT+lvgZ7VJeVYfN8","0Mlxu7ZlAb+Kar1NayLZ2itf51YEvECibhzv2tOHCLA=", "UFVKhLGvk+afUNeD51xi+Q=="), //:\Program Files\SUPERAntiSpyware
		Drive.Letter + Bfs.Create("kQ0BSTQdPukE+x6cCgMd/8DsTEIlW3x3VFpMlNakJao=","/ePLTWKdkDH9xxttAOPGQC8WRMvHPT2FqyH2Ay8m6yI=", "x4ZXqv2vDcjgCZ4/naLT4A=="), //:\Program Files\HitmanPro
		Drive.Letter + Bfs.Create("PkwDfeK07idWHOHSKs4GNV9b08RVbQPTs7tMk73yw0w=","HekbVWZ9ejqYWBfO6Vnb9HkDQLk+csq0CVnJ5XvUCk4=", "i+QgmlqIAEKJrefRHHbr6Q=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("oTGsCAJPpV7KU2AiElETfr0kN+yJur37GW2R8DAbIag=","vxuhWu7GzdTbxr6M1mEVfraEA1ZWpkh2ZEwAA2n1MYs=", "5mlCpbGfqa0oHVE5X/O2Hg=="), //:\Program Files\QuickCPU
		Drive.Letter + Bfs.Create("fI15SnIP3n6BUre90STJ2r0ATal70u3a8IhRJWij3ac=","aWmXFVnmyECZ2uvP0pr76d7R8qSR8aT5lzqUdmepZic=", "80KVzCZB6NyRmnSqU4TnNg=="), //:\Program Files\NETGATE
		Drive.Letter + Bfs.Create("iGNTRW02VI3mTVSN3uLjAZvb8++9TDj9iSLrU7vOILE=","dc2bEY0xPsVaLaj0Q3EJnkntkLNqJOrQI/BPeP+FEYc=", "uvW3cSyu+PNI9IOa3+/xtA=="), //:\Program Files\Google\Chrome
		Drive.Letter + Bfs.Create("kPS7lCxrn2QNYkQZPiDQGPx+ahy87JVxehU4VUN1bos=","f/2ssXcuTJu0x0fMDZcCVZTuXAu5kKLOyiD1cGIfQAE=", "uaZMNdL4p6p2aZgJzEVerA=="), //:\Program Files\ReasonLabs
		Drive.Letter + Bfs.Create("zq78xOtri4dQUN3KK1WVBA==","MlXxagHkPVOD9LB8uOpPAzDBUGsvT1WQy8GC8W70sFY=", "xyCn4RZopvKsvVX7+Ujehg=="), //:\AdwCleaner
		Drive.Letter + Bfs.Create("MD7vdJ7eBV4QaLEJYCOyFw==","mwl/MqXc1bb3mywZUJ9ihuaEVLg9z1SjZXRQ7lkpT7Q=", "qnvBSkYP2eyc4QmGmBHndA=="), //:\KVRT_Data
		Drive.Letter + Bfs.Create("5MHWgBP/NvelftoK7hHk1w==","kafihXpmK9BqyoKtt0qOdzId8hzPJ+HoyBfYiI4BneM=", "z6EzJMpgQDhsBKlmzhrIwQ=="), //:\KVRT2020_Data
		Drive.Letter + Bfs.Create("NCiUDXHZBJGiP/A+hNzppg==","6CGDcCWeysGe9+OCbP2QPzkfT1xC/qnOgsXCuaxHpRg=", "AwdIWL5OCC25r34MkdA4Ug=="), //:\FRST
		};

        public List<string> obfStr6 = new List<string>() {
        Drive.Letter + Bfs.Create("XxI+dGu3xDxg11ZzSLaK0g==","bwRg4lo9VCpnCkmGxu6vH+hlMxqGMlpFQS8RwHJwpaE=", "37z0nHvrCth0T3bfUDUy8A=="), //:\ProgramData
		Drive.Letter + Bfs.Create("0UntaNBA0dZnISdQYOYn9w==","3v1iMm9wcMTGV/4hEjKjI/sJnvE7NgyyROxScRH+L0g=", "bF+KnqmPVyNPkS9K1Hoi2A=="), //:\Program Files
		Drive.Letter + Bfs.Create("WG6fGFzPEhWPak8rhn5J7uMr1u7W/Neu1xGOW2KoTUA=","/TP4Qmw1ekgvzpYfr/DhY0H/SEv9rveYivf3TQg8J84=", "RUihxGlAsf7lRwuTkuEPpg=="), //:\Program Files (x86)
		Drive.Letter + Bfs.Create("wVsshrFgAW+5umTzH0ka2w==","tdnGMHjAm8lNYbPkd6CFYWkyUfvFXjUEsXpui6YVmws=", "ymrXCyss50Uf4xMAcTk8wQ=="), //:\Windows
		};

        public string[] queries = new string[] {
        Bfs.Create("MndgCLVScgIDccX2/x/hNEf6T4/WYnxSC2kUsBgRaclzR0x5voTGUb9hzbvZc+LSfOjFaWrRgVqx1XXrkCIzCw==","DK9RiCkk9ceS9XyULw/f8FhjLfn5J2VhQJp5kOnAmg4=", "fb92CyrwglKEL7wMr19vtA=="), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
		Bfs.Create("aM+HgAQuqRe2SjVknwwXIMnE6O99FXGvQJgLAfnX1Px0JhrCY2YMgqa1B42IN7b5z9gmeUF7k72umrUzCOwlXA==","HNeoxGtsf1sC7VUF9LR7ZJM8VhN0rthjNtUeATr1NTM=", "eyObAmjJhje8YAWp7eZeSw=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
		Bfs.Create("9HQ/cuo/Or9gg9ChaTjPcykGmNDLI9NpGR3Z37uK8bD/TcEEcrt44/eGEvhkMYzrD1UKOa2Bua+NLEkidoCXtxu75sNhHMGqqAJ2ka5J7tE=","2VuG/Ene/c3gqEszJkwiKDwYKIrvZc/EKQjVHlq8dYI=", "0MS5LWNWq9VfzadOPiNm+w=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
		Bfs.Create("T2The8b3iyb6lBP3+907dDHnyHSisA/zCKvI02eV/cofNrsj9+61stoa5lk/Z3QNI91/kz4WwCbGkpKQ/W0hXQ==","kL/v0Q94Cm64Bi/fIKC0k6LeoupSVH5c20tpcEieKTE=", "WMl0dh2jVlC42E43CBShhw=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
		Bfs.Create("yaVF2eTcVQjDfvf+8yVlb00z1VdvL8wJG+MdNFEAp6svBSBPOi8y+6lqyae0w+OV","ix69z61VNOYP8V/TsLZhRfC7c2e5ewoHOwGbys6frTk=", "UPxbQ3g6s/d/dozY52z0XA=="), //Software\Microsoft\Windows\CurrentVersion\Run
		Bfs.Create("SZLkzIAI0r6TwzCtGZFL2v5RpfD2xuvKMBL4z1GJTRxed6PhQRu12a/GUdoyzhL7VqBa3CurHBAjYH2mCGeAWg==","Sh9fRL94zcJGoHTtOOJcxUXG7TsClPZAAM6ob3ZQy74=", "1i9IIrzcINhuUpaUS2k8Dw=="), //Software\Policies\Microsoft\Windows Defender\Exclusions
		Bfs.Create("ee8n9KGu5bhPIyWm3tSKsMOSl5dMxOngRBEVQmTiRkpzT2NOiYRDb2M4Vt0DhpT9Vxf16jhFoPXbxEewxEZIHg==","a2Xbx2Q6Uz3LCXo3Ro4jKl4YhtubRnhAqKGgAWyDSwg=", "7AmWqp5PF6th1pVPGGQqgQ=="), //Software\Policies\Microsoft\Windows Defender\Exclusions\Paths
		Bfs.Create("Qcr2sTEKsi4q0FglUwSn1+9T9vbBFfImyV+HeaauCN+kDUTfXBh7RIKuR8YTMl7ZVNLHJ1sJT0DjqKMT02diL6va5GpsPQHfjedCVnEgMr8=","5siwncCTBZNwHgJDMNiOOzroVcpygmj1r7ZP8GQYUMo=", "uJJ19A3wB4fcSUtETzKlTQ=="), //Software\Policies\Microsoft\Windows Defender\Exclusions\Processes
		Bfs.Create("aGJREbjyPAKFXOVsx2y7o0ZpkCEuRsdR0PknYPVjHOKVizBKYzPP8GNTrrFLdtkIGjMy6Jt9GB+FxBlVTovcTA==","Q4l7j/PytiDvULgAOFsM0W7MTGB5lxgt+T9Lj1vBKCY=", "wz2/QiADCvBFCj9Pkui7kg=="), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
		Drive.Letter + Bfs.Create("eetaLSANwx+KNYuzrgoU/kTB22Pd5sbB1JSzqrIlHxeoiRgozj1DWVoIR2YugtqD","WJwwLlZOjZrZcUUkTcsGylBfJ/jAAiNJqjr9nnDMxmY=", "Jw57SYzwQAyxCdMV4FiMnw=="), //:\Windows\System32\WindowsPowerShell\v1.0
		Bfs.Create("nqJ1rCre+K/z2bAim/sax3IGJUK6d5ypFXFLtAtHZXtHBV86EXXdwzEHnazDa9K5+au8jKvX0f9DZmzl/X9klQ==","Rjx3SGoeR2yM9AQoyY1uBg9cghsSpa07xpeCL7DAHkY=", "iejdEid27dC5WF2dE0PXRg=="), //SELECT CommandLine FROM Win32_Process WHERE ProcessId = 
		Bfs.Create("FA3OiBIE/0ZqpMoUWnssmeTGsZ38dHbRHTDMK+q9icdz1/RMFgQI2HrIzxbhTJWO","VrYUtOJw3/vNN/zJq/bwE3kKlRmXZVtdgQ5LwAtmUOE=", "y7fyIxhs8hYx7LdJZk4PyA=="), //SELECT * FROM Win32_Process WHERE ProcessId = 
		Bfs.Create("qYZMiazA/27L6wCD06Tx5WcAgokrXFblEqfKB1qu19E=","+bAmAsBCHMnV4uiuwZPok3AmM4M6mjHCcuwAAFL1vHU=", "dWXa9slH/Uj4FZOKFkz/7w=="), //Add-MpPreference -ExclusionPath
		Bfs.Create("qCIGXAunQ84lfw9pBUSEDcY22Qg00Th2ovElHo/1MC2OKX3SSXhDzMOSqrpX50As2aGsGsX5hE/jzYmNVwhPjw==","IJItq5hSTAG+WVeWkZKkv9X1HtKVmnPqkVA8E10ordU=", "CYHVdm96Q2K3aZcfrsX+tg=="), //SYSTEM\CurrentControlSet\Services\TermService\Parameters
		Bfs.Create("XrnGzlMwgX9TyfKJgTlzXiVEiJGAdYTiUfmnbde39gP8YCn02kaFPCOz7O6Ed9AD","pP9lyD7U2+0G05kNkawo9GL2+V74msPxs7WUEfNNi2o=", "SIk8hIstaeXxPJ+395QMwA=="), //%SystemRoot%\System32\termsrv.dll
		Bfs.Create("tRJHnKynbxCvsu8ApoNGpm7kg5ADZ1RphHzRuuyV3irUqI568v4RzvM+sHy20neSRe1J0yJ6FaHPSZuVaIKUGUqDJJNIfBrgRmgfwAYHQe7AEwxMEOyI0XazQBFseHSS","9SZkq22CjAOY3C+xqpb/p1hph+J016/fnqqX5/aLVG8=", "tbv7UagyAk4UoPdwim2JcQ=="), //SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
		Bfs.Create("RUqc3xDOmuzeiB7kPCEmsrHVwK5WCMzbsMQqmu2R3KK3V5EH5YTeOszUasNGJjquafRGdbFGcnp6Ezgdid7m4q0liNlk4P/piGfe0s7Jkfo=","z82EV0fn9H4/3GYIDjRIZgf3L0NvoidQFDeus3JUCy8=", "8PjZPce/oLERWYDITsEz5g=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
		Bfs.Create("Ly5mWZX9eyxBuUGIUIf0tCPZtoTINYqhJAZvdPHuSklFEzy0gRxXOc3q/vC6xIAFFUHokILcZHFTXoPGFITRuw==","kltVY3FObAOhI522JujUtWb2KPw/e6e5iGBPaeN85dE=", "a81v/5B9A5qQvCoen5+dlw=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\SilentProcessExit
		};

        public string[] SysFileName = new string[] {
        Bfs.Create("NCQoXB99amHmkLrKDYsKUg==","Z/6rB47v0EiWhOTrjeqzOC1AArx/nv1zlsp30Ty0avs=", "xHM0SJoduPgy7AZhOxJ7ww=="), //audiodg
		Bfs.Create("gZ73Z5TV7izf2RBDLYSjKw==","knRosDCFllWAL2DHO+ya47wwM7//BhCbm5RqIbxBbcY=", "g+EuxiaXY9QsLcOBEHQ5pw=="), //taskhostw
		Bfs.Create("P7fapyYqyciJ0c0R3v+jbA==","BsF6As/kch9A4uxgi560KgT85C4kqokfH9DvDjLNzQU=", "YDzj4rLG3jOhD6l/tQV70A=="), //taskhost
		Bfs.Create("co4wbn3LX6fDiMWhvhpwyA==","EoZNGXobi2POY5vcSvhhRVRfJgytrlDl4+fiqxkzUAc=", "HEx+hauYrwDMohJk2lWM9g=="), //conhost
		Bfs.Create("M3TrZDhznQwXlyhKeuKuWg==","3SBUNGkZKD8LlFGKeQFan/BfVxbTt2nTUF1k1e9c0BE=", "nLb70vooXEJpRJlQ4giOLA=="), //svchost
		Bfs.Create("KZsw+PX2dZd4AjJNifDEZQ==","wtubOzmfpHsHsQpOR8Ft6XnsG7gG5YQFG5H5LqGbu8c=", "OIjqWhw+wUZjQW6ZIzAkSw=="), //dwm
		Bfs.Create("db8TGnySSBFPv5N/CWothg==","qSAKw24NjJiLMgPht4y4UqGYEVGRBc7UFCESdaKELhY=", "7MJg3Pwm3FdS5WFg5KXDaQ=="), //rundll32
		Bfs.Create("Oo5SFEr5paul3EqxR1UwTA==","7sL5Nl2KD42Xi4OkSp3fdjVxYjSt+i+yLMADpms0Atk=", "WYE4hNmffYLsyWqJY/lWXg=="), //winlogon
		Bfs.Create("qtTUDSU4xTfgptlATMzhfA==","nCFIPSxJn1kKlBo/H9kdbygzkCWK5nGo98WXo8igRds=", "0G+ISKru2BzwP6h1zFw7iA=="), //csrss
		Bfs.Create("XtV/FOI0G1mhctnNZdshyQ==","iEwgVKaIvdDwYN2Axn6b73pZDBz7e6IkJEZKPwgBT94=", "PBewz40TZ7ABcEKbK8cZgA=="), //services
		Bfs.Create("+udZ7jsw2uSy7LxKIwpTNA==","/8sBUdynarZVVygiAEKBRBvSxS/KoV19io/KBBIvOjU=", "/pyYEWSChEPWtJI+tO4cJA=="), //lsass
		Bfs.Create("my1XNrP/hLA7zZ+NHo/eiw==","zj+Xt0FkMbhkLAQQqlyKlUNM520jQPwuoQ19RabuOgA=", "ZcQr82flKtjRrY2SOhBiZQ=="), //dllhost
		Bfs.Create("Dl8RGh0szr/8P5QaX9WvfQ==","AMHLFjEnJuADlJVGMwn15hGXStBFfzlgJTBjuiTnkH4=", "zhflMncSauRAqz3lYxwcpQ=="), //smss
		Bfs.Create("IUWMZxiqzjk1AxHylQgDZw==","QXQwHCpi3Y+5InNzQUlGIOpzi+GOy0hNinaSlyusF0I=", "4cPpZGndxsmJydixC3yFXQ=="), //wininit
		Bfs.Create("e9XEWgQODyW5rFRvwc5wHQ==","jGq9G1MeZJAd33Lioxvjt0rh+yy4+MVptfVdl+Q3nAI=", "1Q+QOIbni+uY4IiVxqJFMA=="), //vbc
		Bfs.Create("yoI7rjFAxcmzBkFzeFn4/Q==","ujEIqtcCnzNDHFYLe+QAzL1dqbpgUUbmzykDHsnB09o=", "r+mZLl3Aro1eLSgkPu8C6w=="), //unsecapp
		Bfs.Create("K3ZZCLAfaGGJX08QJM/QrQ==","P1xtmAdQUGYwas1/aMM+GbSL2gpBKEobgCYe4pvzobw=", "HMmkN5ru4zlmdyo/QuwVBQ=="), //ngen
		Bfs.Create("MuZ7NTU0bI0MI2QufpO0KQ==","WiwimdlYizno+pFmjfyrk4TluFa7fHvziuwpeEALq+s=", "VK/pX6erhqAHt1if0z1brQ=="), //dialer
		Bfs.Create("bojBT6CtsNvKvgbZyu28nw==","ABZCqqFX2VpfPOmSeyLyz2hTP6jLMNg+/VPkxTW3seQ=", "8KZxxZ5AySOKtPlb3O60Mg=="), //tcpsvcs
		Bfs.Create("ATBy6jWg2Q5kFCk/LQG/ag==","sJi2WN+fOnMugcwuzM8HlQCAlrFxnBBJ9QCx8ekEdBg=", "1pJIhyoaMaFJ/86S9gTXOw=="), //print
		Bfs.Create("pNM5v7kBNeJPvKZBlsvLEw==","dQMETl1ZsQvQQc3McWXcTcLxCtH90PiqVK1xKcfKeTE=", "acm7acsgpES+kWDHjjkp4A=="), //find
		Bfs.Create("ujEUQOL9v8xkeWbAODI0Pw==","X31pcVQyKZZ2ximIROCugDCRRlu5/Qszc+/ToffM3O4=", "AyIF5zcsXv3/+0PYn+FvPQ=="), //winver
		Bfs.Create("9Oh4881XhqFqOSdRkRU80A==","1FaXl4O5inzdtws4AOF2H2yW33wlDw5iCd5YkkUtqiA=", "2b93EjcNNfqv4xh6XQ8lqw=="), //ping
		Bfs.Create("TX86qnbbkaUYyCF4AGTPzw==","zI4VJFb/ciitQS6MpLVGGBj5cwKQZWUw+bbmxVu7SKY=", "Lml5vtU2TJkpUU5Iodnsrg=="), //fc
		Bfs.Create("lvLne+SS1IaV1ZZiq/rqbA==","HqDXFCUB/0GNHLaVsoJ5RNfErqAqxtx77kQGhmcSqjY=", "QACx5e7Mq7uT2Bl5vbegTA=="), //help
		Bfs.Create("hdSW/ipE3/Hc+UavrN03Ow==","G2kFTCKLth3lPJzOK0aVZr8/UeAF/pTYbg2TT0qxNoA=", "1enYBgTEH7bz4DPIURSaOA=="), //sort
		Bfs.Create("ek41IxBZFyzouVYrsJvkIA==","hDuYGvT+ZLbIHx50JFDfyO6jGfK3y1CLcon893UHqOc=", "kgRq2I/6Rnj2YVY5kOv4vw=="), //label
		Bfs.Create("RdLvlgutHlTDhHfo33Hpqw==","9GqyMjMRfET3ao28Q6ydiCdd+73gI+IxHd/UjQbWDJg=", "+/kxHR9G/rdxT8/dPiPz1w=="), //runtimebroker
		Bfs.Create("crrIbheR0FZKyRzAOdG8LA==","MqlXmiMthTGmR/ItIG4Q9/173eAl6jOh1YpgIe5Fr/E=", "kCmQXu+6RnTIxdqAlQF6Lg=="), //compattelrunner
		};


        public List<byte[]> signatures = new List<byte[]> {
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
