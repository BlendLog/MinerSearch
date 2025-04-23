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
			new HashedString("782d9e9abc2de8a1a9fdd5f4e41bc977",11), //dropbox.com
			new HashedString("6319434ad50ad9ec528bc21a6b2e9694",13), //193.228.54.23
			new HashedString("39cf9beb22c318b315fad9d0d5caa105",13), //spec-komp.com
			new HashedString("44d93a0928689480852de2b3d913a0bf",7), //eset.ua
			new HashedString("545f4178fd14d0a0fdacc18b68ac6a59",18), //regist.safezone.cc
			new HashedString("3469d5aaf70576a92d44eff48cbf9197",13), //programki.net
			new HashedString("09cf5cb0e321ef92ba384fddf03b215b",11), //safezone.cc
			new HashedString("2622e56675d064de2719011de10669c7",12), //esetnod32.ru
			new HashedString("132793c4107219b5631e5ccc8a772f94",8), //comss.ru
			new HashedString("460049e8266ca5270cf042506cc2e8eb",16), //forum.oszone.net
			new HashedString("a6891c5c195728b0c75bb10a9d3660db",10), //blog-pc.ru
			new HashedString("6c366a99be85761e88558f342a61b2c4",12), //securrity.ru
			new HashedString("4e42a4a95cf99a3d088efba6f84068c4",10), //norton.com
			new HashedString("41115f938d9471e588c43523ba7fb360",10), //vellisa.ru
			new HashedString("84b419681661cc59155b795e0ca7edf9",20), //download-software.ru
			new HashedString("b4de3925f3057e88a76809a1cf25abe5",15), //drweb-cureit.ru
			new HashedString("133dbe014f37d266a7863415cec81a4f",13), //softpacket.ru
			new HashedString("6f0c9e8027ef9720f9caedaef4e200b5",13), //kaspersky.com
			new HashedString("f5fe102ec904aad2e20b80dcf40ae54b",8), //avast.ua
			new HashedString("116d64b71844e91f9e43dae05dcb6a6c",8), //avast.ru
			new HashedString("34c51c2dd1fa286e2665ed157dec0601",9), //zillya.ua
			new HashedString("626575b255ca41a9b3e7e38b229e49c7",11), //safezone.ua
			new HashedString("7d2500fc0c1b67428aac870cad7e5834",12), //vms.drweb.ru
			new HashedString("de7e2990f9560ce7681d2d704c754169",8), //drweb.ua
			new HashedString("b8d20b5201f66f17af21dc966c1e15f8",13), //free.drweb.ru
			new HashedString("348ccdb280b0c9205f73931c35380b3a",15), //biblprog.org.ua
			new HashedString("9bfeda9d06879971756e549d5edb6acd",20), //free-software.com.ua
			new HashedString("78e02266c69940f32b680bd1407f7cfd",26), //free.dataprotection.com.ua
			new HashedString("1e0daaee7cb5f7fe6b9ff65f28008e0a",9), //drweb.com
			new HashedString("a0f591c108d182f52a406fb1329c9322",14), //softportal.com
			new HashedString("b0655a2541be60f6b00841fdcba1a2df",10), //nashnet.ua
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
			new HashedString("475263d0cb67da5ec1dae1ee7a40a114",13), //hitmanpro.com
			new HashedString("a48072f23988b560b72cf3f2f0eccc30",26), //hitman-pro.ru.uptodown.com
			new HashedString("10e42be178e1c35c4f0a0ce639f63d44",20), //bleepingcomputer.com
			new HashedString("a71c27fdffca5d79cf721528e221d25a",15), //soft.oszone.net
			new HashedString("6e7bf33d4e222ddb5ae026d0cd07754a",10), //krutor.org
			new HashedString("dab7894721da916ee815d3d750db2c33",11), //greatis.com
			new HashedString("b56ffe783724d331b052305b9cef2359",24), //unhackme.ru.uptodown.com
			new HashedString("4c255dbc36416840ad9be3d9745b2b16",15), //programy.com.ua
			new HashedString("0de8be0d7a0aba151cd4821e4d2e26de",10), //rsload.net
			new HashedString("ef628e261e007380ba780ddca4bf7510",13), //softobase.com
			new HashedString("35c18e3f189f93da0de3fc8fad303393",21), //besplatnoprogrammy.ru
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
		};

        public string[] whiteListedWords = new string[] {
            Bfs.Create("ogHqzf/+WE11GOkhaI7uKA==","o1LN7zmXC2o5JzqLODXqUIwKCIHgFcE7/grXTtoZUFc=", "pojQL9oQfcN/RL2B6wcyyA=="), //ads
			Bfs.Create("oVXY4xFfu8yO89L8t21Wvw==","Rr3Ibo2NtHxLZuia8V7nYgRsxe7wXnnIt0phXBWXiWo=", "DAm7p38bYedLDcaWBMnHbA=="), //msn
			Bfs.Create("u0JtufpAwyasdcqpw3EA8g==","ZC4NrJWcfTUQkBHv//AaY1kjTVSV70flYbiBGWB4Ca8=", "h6kPJYYLwK5scE339YNTcA=="), //ztd.
			Bfs.Create("VpmKsILvrUCmEmxyrgdXPg==","wBxkdu/WsqoJ2PYNVoJIqDaxyskojDo7ki/B8Cfz5Vo=", "LLOGVrQzr2Zz+UP1dHDOxQ=="), //aria
			Bfs.Create("ZExaVm6bd6HVz9b1ObpD7w==","H452uP9haqRIMXrZb/JPkdwYjW0G0V7+25XCQWqfh6k=", "H4BRY68W18TS8pz5CpFYyQ=="), //blob
			Bfs.Create("zShof74/7t1G3WJL2sikUg==","5chvpCAXNAKW7ZaH44FnJmnUYwN9QrgC8HHZLQVGSBI=", "FJJLVP2eI5/mJJXV7SHfKA=="), //llnw
			Bfs.Create("u2Dy3tfOWv9DtAKlCeX+QQ==","tf2f9obW0UzOPY8I+KEH4KN7WpVNVSAzHs2HO6EpZwo=", "4Cptm1ENEXeL5V7w4CFsCg=="), //ipv6
			Bfs.Create("eTzNquOQiP33PxTZOLw5Sg==","mU/HLirSEtnd5aI3ox8GFxtPVVxpny5rfI2cTgaewbY=", "GcP3QB6Q1HL5Tvaj0DsPbg=="), //.sts.
			Bfs.Create("g16U2zGsysojsF9DxeoreQ==","rsC2kUujTMDcTBHoTEKl1p+EwW22kM2e/7Jx+tA1IYc=", "hLDlh7Gz2Cj+GLgMua8FiQ=="), //.dds.
			Bfs.Create("Oa08DMTNaJB5qAz5Y+IcyQ==","luk+01dJLZ9HdLgA37GmEThSgtZ/rwRnDiBCgeLIrnQ=", "Jp7QGNHaSpIVKjaUxboVvA=="), //adnxs
			Bfs.Create("Vg4s2/41/XvOMo+uxoKZ3w==","zY5zQ9s8+oDeQiGMV7nn0+M+8WVhC0LW5X2GJlwa4us=", "gISJ6w+HD3t2DgxPSB5xUg=="), //akadns
			Bfs.Create("spxj1VGvjj8Nvp9PBqBeyg==","1pKcVgnJmHHwslaA3hgPNyo6YQXJlowCMR4AtmnZbuI=", "6UFcLdlm5btWZXjDC5LGtw=="), //vortex
			Bfs.Create("xxocEul8FlQ3TLiubKbCHg==","KeNpYbdbh777+kVghITXYFqGNwBIwrL+OVWLq57+dRQ=", "EHWXq1zytUstX6yrxWngJA=="), //spynet
			Bfs.Create("3l1TkCC1+glj6XFYIsbYig==","NZrWCD6KToVbnYP8iRwlTd0B+LOks5T52oCuFliAFTU=", "kCJTKMkhFJT/eIWz02RseQ=="), //watson
			Bfs.Create("QhBxdAYY4iV58rQUv9izDA==","ssJY44RsaXC3QrrMDk3kXgDmop+W/go5zDlKYw1cpMs=", "oX0y6HQG5asJyslyqSQZVA=="), //redir.
			Bfs.Create("8aY1GhC89vWyt/S3HxTqbA==","7pbFLTNkxjZeuA8YpiIl33iQOiWTsUMGAQ+HDRsd/DI=", "PAUip9kjMgRfONx/hVekEg=="), //.cdpcs.
			Bfs.Create("cLHB05eyCpbvQAOLlLF2jQ==","CmD0rFbiXrlvC0ZLJ8mtD2X76vnN6nOFNz67QqHPxvE=", "6tbQE3IjKTfwGHGbg83EOQ=="), //windows
			Bfs.Create("bXGLCDP1ulb4vyqsdQuHBg==","uQ8dGbieQCPvIXwrAse4kU03IfayxYel1aeDUp5CRMo=", "QQJgdkYApRcigBxSjFO3tw=="), //corpext
			Bfs.Create("9gXCPUFH1M9uEPPrh/vBhQ==","I8dyaH3jO0bP/NONsyXHL9RLMR1DmnTm17YmUD5odpc=", "6xkk8XaUCUqOXI9VBX8dIQ=="), //romeccs
			Bfs.Create("dueFhFHTR+npqjOtlcvzyw==","vQKfZLqz9+Dfl2q4jsW/MGIQOge8COG6ULFokgs7Vm0=", "C9phEjFCZKes1x9pICLCoQ=="), //settings
			Bfs.Create("ZNNeEeCyx7NbUMP+0/aGBw==","XIqTLyyI01xJQyjg0niStgAWLOo7Nzz3LdPHzmRRPMc=", "/tK440FNOV5LQbkZmrteTQ=="), //telemetry
			Bfs.Create("uTORWr4iV9Wl0aVnVjkmYg==","rDcQ39ZpA0WWuG7vJc/m5bkmTrv8sfAH03YisiIJJkI=", "XegF9rvhpXj5aj3C9ih6Ew=="), //edgeoffer
			Bfs.Create("6L1omwbwmxTrU3Vvsv0fHA==","IEFAyAUBUGwbbTnxThX+0Pq+rRPum4w590i0K9eKRhQ=", "K9OWGwwbWAHSo5bN2m9cFw=="), //do.dsp.mp
			Bfs.Create("8BBxT4IBKgpes10/mWr+wg==","S2MNMq6US0LzGHGL+VTZ+g7QaziqgjjU1J6fhheowGM=", "pK5ZuHEL6qNCWmry1eFbtw=="), //ieonlinews
			Bfs.Create("gaR47uNAYtoRCd7jKFouzQ==","1/taea1xR6qYQBvjjBHPKn4EbFKL6uTu4mFQ22yhQPA=", "so1QWyQhAsqO5v6KeQy3ug=="), //diagnostics
			Bfs.Create("FmhNgUleIZEvHa+QExGgXg==","RyP3ipTu1uoWa3Eg02+vxN0Jp+pC9Dv+rfUdg0TcKPw=", "5/e/GgzAvlQY/8QsaUJ+Fw=="), //.smartscreen.
			Bfs.Create("ss4D6rnaZWmI1yQ9v98S2w==","O3jJk41ds8lng0ClvbfDFG+ilbd2QsNAgYyMKOfp3GU=", "WyTN0zGpbU9dGUAx4RVYxg=="), //.metaservices.
		};

        public List<string> obfStr1 = new List<string>() {
            Drive.Letter + Bfs.Create("ZnlNSmmLVX3ZobEKSGlLbwP5QsCeTicEH+hOFeZgKQY=","xbSOi3q4ftpxGE3A7Ma6PF1a3PBU3hgX20Sb08RNcpA=", "FIkJQlhTSaUl11rregMeIA=="), //:\ProgramData\Install
			Drive.Letter + Bfs.Create("pDrN/UxBV2tgQqUK0i4/VMrFDkFJ92rSbJubxfbIdOg=","RbfBEsw2bGpqfPxLx6N1LxOO131sG0jWBG58aFP4dbo=", "v5FXoqWe5bHBvqesEYSDeA=="), //:\ProgramData\Microsoft\Check
			Drive.Letter + Bfs.Create("8dxAi1x+LKvIngMgevfIdgtkUqSTMSVDNiVlr+OHe3Q=","A4D05UN+lcVhqG1vRrgO7IPMd+XrUwLPt0BAvP3b+F8=", "RdVrtb7dzaVJidhB37cQZQ=="), //:\ProgramData\Microsoft\Intel
			Drive.Letter + Bfs.Create("qomfCLGAdnnalE4Ayni6aJD2Gs0CHdOcfTg3wc9zoAK3eA5v8M37HD1aTnvfc9/QrBqKjOmxztAZ1CZ8/zSi2w==","3yjQpP0+uBAD/f3QZbtcpHArNLkc/RQ5BDHHGVEDBZY=", "uRY68dr495XBxLXM7cS8SA=="), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
			Drive.Letter + Bfs.Create("M8mif0lSQISuB3TB1PBuGy5PoeBwwE1LyW9tlKRlEVA=","QUGi0nP6Vo+t7gr/Wj9tnAMngxIkf+X5Jji9yGvRk4Q=", "gNut6uy2jqxc6WSohydKkw=="), //:\ProgramData\Microsoft\temp
			Drive.Letter + Bfs.Create("8NiC7kWDZvkVymjGh66oovBEz4ZqJMTn0dR/gkLMBF8=","MefT3JUmZBLT+JNBFOMJx2L7+badQL/o2JtW0NdeI24=", "J2CupvJ8XNMbIpRafin8vQ=="), //:\ProgramData\PuzzleMedia
			Drive.Letter + Bfs.Create("zRY00hhQxfU2YZalvb2RkN+3qk8sxNFi+xIClkAUAZM=","MxzPT94MhBEGXT6Kc/EojMMyMTBGMtzrqnJPj3eY4y4=", "fJLEN+ZkbUlP2vRwrtvOjQ=="), //:\ProgramData\RealtekHD
			Drive.Letter + Bfs.Create("bcXBtuoU5lRx6uFVkjm+yrxdDe6kZTI3auWnEGuTloM=","rzA6QkR8bQoGR2r4fGwJJSRCBPhoTi/sprvdb+WdHRc=", "oIv+sBcHncRiuCO4oYpYRw=="), //:\ProgramData\ReaItekHD
			Drive.Letter + Bfs.Create("zYSUxf2m8wtDPyDPXXBN22oEoNDgQTM2/UX1LydjmiY=","Hly2UV8qEtRkqNs4yuLD0k7IyxnA55odlrmSMMSCkuU=", "Rp3q0DYZeRQEeut9GsBmog=="), //:\ProgramData\RobotDemo
			Drive.Letter + Bfs.Create("b3Ba6sNtHfbyM1tWH97JIMahRDqDWTCY7Jc2+px2BwI=","Q/0XlhOJJ9hKwRgAI21pt8dAhlk06MrfaLLFuSJQKqI=", "igGPSWWRwFfcGzg/GcLP8g=="), //:\ProgramData\RunDLL
			Drive.Letter + Bfs.Create("vQlrHiIo8euAZQwbDS0FpNKWE0zHaiMO/MZ3AZbqh88=","cNFM3MXuYWDipMX4zGpO07zfv79UDzFdl7Uplu1lN5c=", "Fyq0H9ZXTBvZ48IGANabjA=="), //:\ProgramData\Setup
			Drive.Letter + Bfs.Create("523JgBro24sYHqi2Sdxmpdy4W3wc9ROhpfcDfdWt9E0=","tv2uZj+CLo85c69JnEFcxmNlBkjx4APwzQ9pnMgBzD8=", "uXfX4XkZSXPWmMl4fk9IUA=="), //:\ProgramData\System32
			Drive.Letter + Bfs.Create("J23D5XJ6h1osVQhBkxjpJf0UvKuw3xrQhrU79FOrJM8=","fXAU0ISHIx3qXnRJe/rfIg0eBz1OPtjeo/7C0Z5N01w=", "Umwyxp2LSGes08Fb9KnB3A=="), //:\ProgramData\WavePad
			Drive.Letter + Bfs.Create("ObG9nv6oznpBPbyjRycCoAV5Thth0dmhJzCo/VUuDZHMCMTZeMR4g6B54TO7Gt3e","JeND60snvDCab90pWAfDa244gkWlGkje1U0VYoxtHbg=", "cT1EsU6Cjt8cMkkcprELYw=="), //:\ProgramData\Windows Tasks Service
			Drive.Letter + Bfs.Create("EspOssCNQ/Pg+dhbY7MVwvQPLwqxbga0/iyWawoKQgs=","lzQJhhCpiSn2XIzYh/g8UWP9XlfjwXVZl4GcCQGbxOs=", "jdFxD42cCdY8sfKPelMoFw=="), //:\ProgramData\WindowsTask
			Drive.Letter + Bfs.Create("JHpTaeVrZlAme5Ezzf5vSE4TPIUUdfXF5N6iFD9aYwE=","JhIdVkvOh9cjuUoGuEIB5x/lA6bEAIF3D5wdDVnIHsE=", "6rx4HZY3mMLZfx7WkGUKIg=="), //:\ProgramData\Google\Chrome
			Drive.Letter + Bfs.Create("QIZIGttxFpivDua3WhnGC2HwkIISlnIrjshxZlnEyXiO4Iez5n0zl75B2mLlenmi","CGhOwgAnHxsPtMbIUaJesZrGXcvP0KD8wVSioaiySo4=", "T22tMyFpS2Y6q4m89tZiOw=="), //:\ProgramData\DiagnosisSync\current
			Drive.Letter + Bfs.Create("yJq6dobxae0Wl9ZhIPM8xIHvIeBP0TS7QqJDMEgqtao=","uV/4zV7GB66PD+xPk1QYnq3Q/Ql+fxBRqldFu3e5z2w=", "9FoSPux0bFkgZh4+BeG1+g=="), //:\Program Files\Google\Libs
			Drive.Letter + Bfs.Create("jWL2vcXkZ5sPz2Bd7tMEda5p3KKTHg1s1Xfd7+MhgT8=","OFq7JfMeMLX115H9wwcjC4zsVcR4D6P1h/FVm56Z7/s=", "Oz+cZO7WcD2GyO/XQsO4mg=="), //:\Windows\Fonts\Mysql
			Drive.Letter + Bfs.Create("zruAI8UdKn2r6RU5PqoiVIDRzC3AHSwlRPIlYgghmqN+6YPl1Nf/pQoHLPZEKZUW","mFyYKrfEEt17XfWKhvM0+z5KDGIEXt0qluSf/jemVLg=", "3Cin7SIldARQmbuxm7yphQ=="), //:\Program Files\Internet Explorer\bin
			Drive.Letter + Bfs.Create("L+i7cUVKcFerTcZ/ha5g+75WXsICgFoy1XR7uHQtrSw=","PtBrFU7EuXitQdSKViMjLoIJPydCNFsVItFCJMLXW7Q=", "cXZ87u0SK+4mjxfRisorKQ=="), //:\ProgramData\princeton-produce
			Drive.Letter + Bfs.Create("pS04mbkbBevQP1sW29vW8+jof6GeRkKu6G46EYgWT2c=","p2gfSllNEfLi8MSGRI6YV+FBXywYn3r1rHeuUQuZzNs=", "Anu82AwAUgkwz9o5OQPfjA=="), //:\ProgramData\Timeupper
			Drive.Letter + Bfs.Create("V7kLneu65lmF3tHx4EYpmS/5KsgQHaMaygSNg7T0aa4=","Ffd1K4Kd5cRSAlnU+lGSxd+MZ/iQM215WsRh9ThWDyg=", "bXeaquN3kK2+RpPhonxxsQ=="), //:\Program Files\RDP Wrapper
			Drive.Letter + Bfs.Create("f0yARjkIQosGAlGEeHLLGqByZAZszpROPxFa2gXakh8=","tr9wh9/5ZgMwY8cxT5gi6kjWmjYWYIhkKlsw/b6JLBI=", "IjqJGiZ//3GqwkUize2gHQ=="), //:\Program Files\Client Helper
			Drive.Letter + Bfs.Create("GVVPI+B98lCWj1Fwsl197dK0670LttqfsVmu4g17VYc=","PR4Io7YMvq9tNFgIQVMWHbJmWady2C8mh1U6P9P7QAU=", "bpmioPR+4wQU3u8IuERNBA=="), //:\Program Files\qBittorrentPro
			@"\\?\" + Drive.Letter + Bfs.Create("nbn8hCr/q1AzsUZZ1lalKi41EL/BAxj4j0sfQaGV5C0=","2iEHH/rSRCiLNgg8E2AuATfGEgerjqfHk8Xy+Htbr6M=", "PfnMQEIB+g+ZxeX93BGL2Q=="), //:\ProgramData\AUX..
			@"\\?\" + Drive.Letter + Bfs.Create("2Cy8US3V3xtJsPGmO0qBQRDq02yXhMkUOBSGUzqUd9M=","sALwx4bG63E46XC72sPoLVfchk+Qq/7D9aEGTsIDUgA=", "BY+9J4eytnnvNr89v0INyQ=="), //:\ProgramData\NUL..
			Drive.Letter + Bfs.Create("QeGgkPiCiQ9dGukSFbro/itKoIv057Z5cqvgM/Sb81E=","aQmm5EtTvM1Z5qp3KtBTSn7isJbbd1ByX9lPxa3AkXg=", "3asbtW4KEjyKYv6ULNKeZw=="), //:\ProgramData\Jedist
			Drive.Letter + Bfs.Create("5W5C1VSv3OX+yishhZW4DvVJ67sbbJLwhhEbHJlPqz7p8DRCdWOT9Z3/DnWHCoxyXM4K8Oh6I5uOjshuSTaF0w==","ww6xykLc2KTHQIruamsTH3sxDlyY5m/olT7+tvjb14E=", "XVh49dEBcCc9wRb4WkGlyQ=="), //:\ProgramData\Classic.{BB06C0E4-D293-4f75-8A90-CB05B6477EEE}
			Drive.Letter + Bfs.Create("q2INvyBLB1Phtlo5AEUnvRFdqwfsVKUi8grJHXwPGZRq5wGjD5PBAY6LbVbpL3tP8ijD32A1f9KOqEzABt+Ukw==","+HCkNE5AZsLeuFJRIQcRAtu5GMRKv2mFfYt95MgOIDo=", "0jj1p1lK7W3x91kgNaRglg=="), //:\ProgramData\Classic.{BB64F8A7-BEE7-4E1A-AB8D-7D8273F7FDB6}
			Drive.Letter + Bfs.Create("NN6agxXjD30TwBcAzlswdydQ50pQUSa1OyTCQePjjGY=","HPQA/vcghqHgjFb0NCL7O9ohyDZ0fQ6JNiBP1g8eyTU=", "k9N9y1Zm2em+lxWbr9uR8w=="), //:\ProgramData\Gedist
			Drive.Letter + Bfs.Create("rklZBXbD2uBaQz6zT0T61CBZyZ1Xr4e53iP85g8oXaE=","gfZkUHRKgBiLHKWea0u3PAzG3NjTzQKwt/nzpxV+ams=", "iBiHJGIfzfjcfvF9iizwnA=="), //:\ProgramData\Vedist
			Drive.Letter + Bfs.Create("VuVHvDFdDfkFq4Zn4lC3vxxi6yfHRp7VuR4DO3k/qvI=","/pAtYRx7OjAuiSpi7euYOczVB1Ycale5qy5s7G5sIII=", "yPWMaoIh9A3SNHnY+jlQGQ=="), //:\ProgramData\WindowsDefender
			Drive.Letter + Bfs.Create("G5xv8/RBFjU3RAkMmmZ/AJWrzjoNPEHBBQkWQ6Wgl/4=","ywbYo+fww0K7VGq8Ki/WnJ89KtMI7Odelm6H1Jq69NU=", "TU/sGmmGTee3/KDeaY4Iww=="), //:\ProgramData\WindowsServices
			Drive.Letter + Bfs.Create("9OMSPZtg3V6mFtZnWiA5PakX3Lu/Qs+68DcQYVx5/W2j49bGLIa0JH041tRSqEbA","+mww8tSTgDst1vLUyhw05pSITUzIW4xBuiGKwe6O9Gg=", "zNt4Lak4EUX9/u+/p0M+UA=="), //:\Users\Public\Libraries\AMD\opencl
			Drive.Letter + Bfs.Create("NGa9yVP1l6gLLmAuuGPAE7bTww3w70piU7PF87UYSzq/SGDwVfScTtEUGOvbiSrN","M/+ol/QJUP0ueaI3fx77Ks8qqPowp9yMp18/jxi/M2k=", "2gChJZkh/yKbRERH9rLU1A=="), //:\Users\Public\Libraries\directx
			Drive.Letter + Bfs.Create("Ss6DIEVr9HtvKrT9TjUGLejw99YtXvLepgB5dTn81WY=","9r10Iz+o6zQnza2QiWmtY03/9b08z1KwtofBX91TdkQ=", "1PNJpAKhnlVdaEKz+39rEw=="), //:\ProgramData\DirectX\graphics
		};

        public List<string> obfStr2 = new List<string>() {
            Drive.Letter + Bfs.Create("CZp8Sb9Ib7mmgA+h3E3u73w1I7uT/BNH65RjIklfIpU=","GzfUYG4IYVxsVc/kFZTUzrPNg3clFktUmzp9TWXOUGw=", "zYUdSshItM4vtXEZyWPIMQ=="), //:\ProgramData\Microsoft\win.exe
			Drive.Letter + Bfs.Create("Rqt0hAdNnJWgGNgrYnUts8tMs6l5AQJnIeq39ZIFGuBgcw59URaahTVdE1Lk/SOt","BeUViWbfog/ieyLSHeQ+Xr5GTwV+IxaOsPVAbH1M1Dg=", "ZqjQWCvCCrHF1kXnlPl/Ag=="), //:\Program Files\Google\Chrome\updater.exe
			Drive.Letter + Bfs.Create("CPBFywqFWXbNddmpCz5qAV3y2glYg4TpS9pcF9NfLa4bIiwZcYDPBah2lhCNng/B","79bw8o+CaPQJ6L5Dg49Zlu3JBMgMJ8ulRjO5W4qZ6jE=", "XUOYvHrE27HZrrP5ZjeDwg=="), //:\Program Files\Client Helper\Client Helper.exe
			Drive.Letter + Bfs.Create("GcvU0NnPr7abEZG32tsF/XSgQsdQQt1Ev5D/1mtjwoeAVkqtgy1UU+U9715sP2jiJ7dP/jczq+LxAMTA8jLNEw==","AifbWilHUmrdERlcV+HDLe6EBpFLJR/jc27JOcq/CSE=", "+BjgPbHNkawudMf7CKDdsg=="), //:\Program Files\qBittorrentPro\qBittorrentPro.exe
			Drive.Letter + Bfs.Create("lxW11sA70614F/FuGcra82z/haOEjnBu4YPz/vsFdxnYPKeni7bql0vWbojR9Z8wcoePovlX2O53DZzOJjI/yQ==","mMmmmzih6zjLR+q3Q7pFA9czK/td5dRsbs31Rycv19U=", "DTVAQnUldjDMbnJdeQDohg=="), //:\Program Files\Microsoft\Spelling\en-US\default.exe
			Drive.Letter + Bfs.Create("swDGU6yBGugHC0PIYd0G9sl5wS+WNcDunQl769mi45cQ0DATjpw2Vh4V4He8B0mR","qtID6ThRQ06tABwEcZ1KkS/wy3GVRb4p2S5YZwEe4tw=", "ZjfeLqszYyTnEPPT8Yl/SA=="), //:\ProgramData\Google\Chrome\updater.exe
			Drive.Letter + Bfs.Create("1Fetfku+VMT/5MoriUV4V04AnlPv8Y2B0DozR7UUjGRRFguEpvGKUQkNH7oLA8r0","BuxIv2IwI9ffONQJ8t5WlckCYeY0cl61MWpMoZcqkZY=", "eIotBaX2gaaViLcS/sPkVA=="), //:\ProgramData\Google\Chrome\SbieDll.Dll
			Drive.Letter + Bfs.Create("sXWDV9WhMMnvKNVCj6xKPXG3MS819Eur1TrtIfwt0SY=","C+l3PkmWihmCUU2wWOCi1teqmO5D6oApl2N3twPLJJI=", "s/BWEumI3GOvxyYDN/bcFA=="), //:\ProgramData\RDPWinst.exe
			Drive.Letter + Bfs.Create("XZJ7LkPIJJi29v7RwVgEBPkym/CLvLEne8RHcMhMVyCqJp6FbZNpuMhUncBj1Fhc","o15yrrnjC2X/c398pb6zGwBSZSZlptsqd1bU+M9SaIY=", "RfrGbtSyzBx6ZIOdSqAgLw=="), //:\ProgramData\ReaItekHD\taskhost.exe
			Drive.Letter + Bfs.Create("NyYoLIvy4OGZSzR+ch5XFJaf2enTaI0jdnczX6IsJ/N1rrOpVS0rpuOaM54OKCz0","DJQgepSf6TXBrzExL59resTggZgk9vz9BAEtvr2WL78=", "+TvXaW8C0ezaBrc53OdIOQ=="), //:\ProgramData\ReaItekHD\taskhostw.exe
			Drive.Letter + Bfs.Create("2sLjFOJmRaMOC3fjM5EeI0oFtZCdh/2SH5NpGQ3UAyV9BaUwURxqMjZa8Dlizda7","VwcrtmXmOYE1IOrXZjJRkqC5XY5vRhPQGuXqe9Rd110=", "V1lA7yt+xyGsfLIgBL2QWQ=="), //:\ProgramData\RealtekHD\taskhost.exe
			Drive.Letter + Bfs.Create("7bD47hZUncVbiw4uE2KZab7JwP1ac/9O/bL87r8SJZv4y2aMyK70IUA6S69x9irH","HRKZ4CqaFqXz/Am1Nh7NyH5oO2tWf4Y72VVL1BBx7RY=", "H7tHBrfsEkARufFG30vsIg=="), //:\ProgramData\RealtekHD\taskhostw.exe
			Drive.Letter + Bfs.Create("HNaBkEPn5cg52QYoH3mG5sRL2JU5YOyeUXiHiH4qheGLQJGkYm2v6n3+8dNWcGAP","MYhypP6/ZIgsEqd64bIrhlm7UPAkXbDyo4e1FuwukuI=", "K5izHxAIVhVtaQHsgHYhgA=="), //:\ProgramData\Windows Tasks Service\winserv.exe
			Drive.Letter + Bfs.Create("8pNOZKAOKObNd86kXnwGZUC2HZiApdUC371OEnNtbP7GtXjprKrNmhJPUHxsMLh6","1BnCP+fE13ktIUeQdaElnx3tPnJxdNaMHqErBtSflmo=", "jkF97XlX3u1yd1I0xgxOZg=="), //:\ProgramData\WindowsTask\AMD.exe
			Drive.Letter + Bfs.Create("dVcwfTRy9YRfolVsC+8phOqbVraYxhJjKKSZ3n+i/RFP0Ao3afZ8HwIQzN47IC43","3y5EIOKCcew2J0hQ5h5wzZ9w9dxiP9Nbq9rhmmZgbdM=", "nhIb8Sz66ky4WJtr/ovglA=="), //:\ProgramData\WindowsTask\AppModule.exe
			Drive.Letter + Bfs.Create("aTfXShdykeuEbMeAIwHv5MhQbvLX5ixPFkbK68Yipb35yVQve91IBw8ERqz82LGN","X8Ocn/7exL9MaQUWFTPGYFTfaKm6KBdyU5Id8OjFaGA=", "Y/W0NUNs6rTi+wMKZRQPdg=="), //:\ProgramData\WindowsTask\AppHost.exe
			Drive.Letter + Bfs.Create("aKT4LIUMzHkazjPPJCQdbZLf2HFR5hiRptzt5wEQHOTzjRME3SXxbIIe6IpmeG4o","KK75pMo8MsegcLsLEWjPkXmR6qcjNKJl72v7siqrc5A=", "6euJdfgTkMkynFN9QFvRYQ=="), //:\ProgramData\WindowsTask\audiodg.exe
			Drive.Letter + Bfs.Create("ok5BktMgxv5/JTgSgTnF9L73iL0pupOJLyVeiOFTiH3Ai01Ag17Cy2olZsR7GUEw","J9iyeBnVmCr/psiOAXsq+sNSEfsHedticM71Ol5y+VM=", "SqmaJtlzDfqOkQ5FEEHKcg=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
			Drive.Letter + Bfs.Create("qsbgTVl10EHDJ/9+JJ+6nOBoGv19/ML9WZU4/T8zoTY=","o0OgZ9GeUbMM4cH0wCI7fnMxhnFDOmb88aNjaDXWcC4=", "iWzXHakR/uGppLOfvXLNnA=="), //:\ProgramData\RuntimeBroker.exe
			Drive.Letter + Bfs.Create("6a5SSV7slgPc5V/C+2GIZEnvi+kgljoCtKFc1IynBYsbNh0UYzxoexL5joHblTRr","pYzW7guBkU1EMF/DxzHxSk0Abxb/GZpnVBzKVEBro68=", "gIq74UIbxZha3+fTKuKMpQ=="), //:\ProgramData\WinUpdate32\Updater.exe
			Drive.Letter + Bfs.Create("zVycHRE0XUeMcuKHVyNhzXgScHnAaC/+16GknJQrpxIkPhpot507x6sVxjbkdMyYtNhCd0606GEzTC0FzlLATw==","ZHAlIFyjyJGBcCudJHxgN41VsdUl8riXyj44MWXaXus=", "S6qifOyBJ3XdFHRQ+mZuqA=="), //:\ProgramData\DiagnosisSync\current\Microsoft.exe
			Drive.Letter + Bfs.Create("z7ACJ979DUenHRiTPbGrgF6ECPwVdvF18Fe1wMW8EVE=","sibYP2DXzO8z+ymew2sOriYwuVq61J93Fxxea72SAvM=", "HHCxxVTaKxpZz7eigqFoaw=="), //:\Windows\SysWOW64\unsecapp.exe
			Drive.Letter + Bfs.Create("c4YAXugsLmr4N0CIpz+2olcQpuDn4vWjhjhG3XYRgso=","JSjK4/6OoqqDpD6sMFulQFhPTm53h9w5Wh+29OTR9+Q=", "NLXHlff/1tVv6tXkDVBeyg=="), //:\Windows\SysWOW64\mobsync.dll
			Drive.Letter + Bfs.Create("ZKcUJk395IM/N+qqppj5K16UMRAvVRoL2o/3et4iGcs=","xsyeGCNP5fV9DfbJr1OvNH2VlCoLhLQiaxJBCzEA9Q0=", "c9uyMhtwiG4eR+Y9Rj/ayA=="), //:\Windows\SysWOW64\evntagnt.dll
			Drive.Letter + Bfs.Create("xTL4D2Tch8NeOHX9SQMZuvje9zGhvL0oad8vKWTMD3o=","AfMzvzJIKiwnW2DXjVpKU6N/5KY9TC0HeeJsasE6o/w=", "z/I0NkX8aKgAvlr/sRiAxg=="), //:\Windows\SysWOW64\wizchain.dll
			Drive.Letter + Bfs.Create("KSpHKpoNGyFaArcSpDPybF4r2DhCDk6/SiKUp/7fghI=","AAvMhS3MKmWWHRWWiWM6XxCRrnlmzZ1BugqFbkwejc4=", "EMWgLLlVxKCsiw8PKZt1EQ=="), //:\Windows\System32\wizchain.dll
			Drive.Letter + Bfs.Create("bbxnoO1qg5HnL/vUf95ix35DdfFqOaWOFIkmouLWdLw=","5Yla7X0PBN77Jjrss2tF1Prxy8l+OjOmOaP6WLL8MlY=", "AM8UGaW5Y7zFRyQSisz1mw=="), //:\Windows\System32\wizchain.dll
			Drive.Letter + Bfs.Create("jL6H4chf+uBucdtpf8q+4mrlRfShWkExNXwQwN7vfrg=","Xvur9AAnEI7t+z9ec9JNRz/gjT75k/yOGrTZyoE/iVM=", "vtpuy4yYfaDWAb2zKnFvWg=="), //:\Windows\Exploring.exe
			Drive.Letter + Bfs.Create("LHDLR3QPITJJeaqQTN5k7vfIvr1wlkpGWnJmA7eYPQX4kWvPklllVbp6HOiwj0rp","uT4VYGCxVSN7rXJybB0nK6PIIHWbhFHkupJNW5loTRU=", "JiUG087T0AtW0RV464MkmA=="), //:\ProgramData\Timeupper\HVPIO.exe
			Drive.Letter + Bfs.Create("dt6XTFLqxReshxsx8GjHswXrzWDZYIC4iPUZQwk372dRZWRol4MhRocEU0b9W2Jc","Nq/2mKZRB8sZlsYtJ3lqTHKzKkg2CIztNAaSI6VEkU4=", "CRzhN3kRaR/h8ZTt5Tb6/w=="), //:\ProgramData\WindowsDefender\windows32.exe
			Drive.Letter + Bfs.Create("x83lXJliSSGJ+R6IpUu3+Oo+7NE/ZmkJ4Vl+mvnRWIgsdNoKoz4vAPc6um5zPK9S/wC52mqjOJBvgtABYnWtoQ==","XJuLA91yXRm47c80GUAOD8S0Gta5NCDLSCE6YO6dVI8=", "WUEYCrYkcFY0qxBg92gaig=="), //:\ProgramData\WindowsServices\WindowsAutHost.exe
			Drive.Letter + Bfs.Create("y1UzT1a/Kp4dlnSbQ4l4kneO6oxoxThBH7xjT0lDK6w9056yMjo20SfeeNIoLHRf","cHsSgXKewugfAVIkTF1PU/rzYjX8YbVOYXFFvZOldhc=", "n/arVdF4MnsJ4PEeV7x96w=="), //:\ProgramData\WindowsServices\WindowsAutHost
			Drive.Letter + Bfs.Create("KHNi9bIVyNioMFJVE9sGqsbsK9pu76LMrUBynH/ETLRf99/DRwJl2DXmEIWsVGB9","oIDbii4GwNWYx9tY5ArH6+0VyAlgZ4cIGAmArX/ft4o=", "2FQ9g3crkZ4PZXVi7lTGuQ=="), //:\ProgramData\DirectX\graphics\directxutil.exe
			Drive.Letter + Bfs.Create("8QZ+FJmvVYy1KaWwKvKfXpfWdQeyFooE+kb85ukEsNVpilQifVRXbiqfgwzSDq/VJrCsywYUUPPqvr0tHr0APw==","9wezNkv6XLb/QWVMY5KJ0ZrxPW9BNUMlRd5rCRMJvWo=", "1V4hG6dkOHv5zcqrBOLDEA=="), //:\Users\Public\Libraries\directx\dxcache\ddxdiag.exe
			Drive.Letter + Bfs.Create("GftaNSJNeWyGH00HOv7HcdCabJdFxHBJjlbFrLfRYKk25an/FkS3aSKNtDNkw/Ox8PtGwN+m8Kzjc2VwYSbGEg==","zHmz1qBqnAMWcG3GYiBZsKa49EN6cd3RakAUfDPfY7o=", "sAm9raZz7GuLMEpUD4/qiQ=="), //:\Users\Public\Libraries\AMD\opencl\SppExtFileObj.exe
		};

        public List<string> obfStr3 = new List<string> {
            Drive.Letter + Bfs.Create("HFuw+c3DpJ8IgAj0gGkBeXimziID8bHXhOAU3pSeDYg=","DB37Lx6io1qN4KRpeTeBoXSzgwTaD6dnmvC00yNWkP8=", "IrmbU56Gn3ZdOqaN4NHtIA=="), //:\Program Files\RDP Wrapper
			Drive.Letter + Bfs.Create("7KBTuIfKaoVs0BiVmrrYlA==","uZhNyuWLTNAyXMI48HrlaqWf/jdpHEi5+xLAM7Lmk+Q=", "RU6tQjXdeoUw0D44wWSTNQ=="), //:\ProgramData
			Drive.Letter + Bfs.Create("3Km8Tlr2Z0asy8ZR/fkEUWXB1a6CKV5HwiKPvmGiZkiVdvWVLbbzTkgonXkwxrtg","pHitCLQMlKAb1tIzlzR0irrxsxuidzbmDqVKfUSr+J4=", "4CqknDVTTDZLw1A/60riqg=="), //:\ProgramData\ReaItekHD\taskhost.exe
			Drive.Letter + Bfs.Create("98zrlRVbC/Bba4mC71tvaca3gWtEca78hY0+pKaTDGm8KV6aEcXfdaxLqkIuEjxZ","3j3ETeUF59jaiRfIXNxktspPETXoFwTvduVnbEnNKX8=", "inBbcAyndfigSeKO0H1xXQ=="), //:\ProgramData\ReaItekHD\taskhostw.exe
			Drive.Letter + Bfs.Create("DeNgKcxBKRJVpRpjuvDJln49RRArMqJCWXCcKeloNKpFCGk5Gir2A+Oi8ZpF+yxR","IqvYdUuzE8wK+CFKM5bCQCaNjTtj5OqE73C93hXTAcA=", "pt5k0yIxmDKUu85zx9q/6A=="), //:\ProgramData\RealtekHD\taskhost.exe
			Drive.Letter + Bfs.Create("t8vTl6N+9ucJb4EtcObqALSaYlVqdZtIFOxwF5/KmBr9Muot7AbojJx5MXdLfEuB","OGsRkWMXNOTjNwk3p4tLgDLp0UFiXeawAYEemxtjysY=", "CDOcvwwtCPl8D2fo6Ahqtw=="), //:\ProgramData\ReaItekHD\taskhostw.exe
			Drive.Letter + Bfs.Create("vD0qM/fhIFSdThkSMRnrhr2VciKGDTBuUtsBe5c4Ka1qGBTFUg5cqN4ABiz+mvvu","jflbMAxKODUBm1kanknxDDfdXlqYEFe6wtH9J+IXmIM=", "CfHRRkfiEpDnhWdZINNIlw=="), //:\ProgramData\Windows Tasks Service\winserv.exe
			Drive.Letter + Bfs.Create("/R+JGkbnij63G/EhlgrDuqinD+xZbIqXQeRLqUYoQXi4MXFqJMhLpqanfPfnlJqZ","2AKVMW070K89l/OFEXXUNAhYQX7X9miEX2yje6F/oLs=", "lmQaok9fDw5xku48S49o2w=="), //:\ProgramData\WindowsTask\AMD.exe
			Drive.Letter + Bfs.Create("j3xwDfDDiH+K0JOTz5df+a5f40laxUgVWt45DeZfCHl1old5NNS3K6DMqh6CZJHF","Cg7ippwn2zO3K4hWT0BwG/vkZjZEEhX1XsAUnKUbns8=", "ntvyu8SjeMjQyx+HsSQTmA=="), //:\ProgramData\WindowsTask\AppModule.exe
			Drive.Letter + Bfs.Create("JYdMfEUPYTKYSNaCnZklg8VMfJ9FqKi0A6M0pXcy31rJ8IxaMAw+cPjU/q2kF6ds","eXwJsllU4pohPeqlRjtq1povX7AO4wZhtt3G9QHJs0o=", "aB5qRmO/MH5+iQ835kxMHA=="), //:\ProgramData\WindowsTask\AppHost.exe
			Drive.Letter + Bfs.Create("SQtx2zEBLTzH3GQyXPxu7boVSrH1q2UBRYBdDTAHPYQwJpHC2SVHJ9E4uNLHbaXx","I/Ui+56McXxcqPf7rH5WB2NsBlCtQL6w8Dp+Hcx/1/0=", "k2Iw39TzULEbOWKyMkuu5Q=="), //:\ProgramData\WindowsTask\audiodg.exe
			Drive.Letter + Bfs.Create("WuF1nYl05VdLvspbTxGUwQadgbdFi2I9mWDXQgLDOWWcj8o3FV4+CayETYjOmQzV","tEbh2L4g37r3Yr+o1tOn3DyMifJOVZ7zZ6tE3OkTUzY=", "2AH4s6J0F1K6kXI59dLQBQ=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
			Drive.Letter + Bfs.Create("dOfohceWIoMRNOuT66HMmpNjZW7xz87WmxRY7yt7xPo=","cLcevmZPh8DlXZWU1ec8PBmS7tW0jxjaDJb43ofG5RM=", "9BOO+TjOZjzn9g7V27SVwg=="), //:\Windows\System32
			Drive.Letter + Bfs.Create("enEtNlmMISpx1vxDOfb7JnY9s+g7ZlNOyFmdv17crfk=","YHJOyzjR1elijUn9YspAPW/QZ4rNPCnbq4ZU+zOrjuA=", "41iFsDD9biYi5tDokxKp3A=="), //:\Windows\SysWOW64\unsecapp.exe
			Drive.Letter + Bfs.Create("T0IKSN+tKDrVqLL2EybrDZnz3TitHgk/WmEV0R0IRuRpZ2qZutQpVBi46BV7gSwF","9Vew/nJsanckXqCreVEoZrBBrZ5E4k4+SdP2T53m9J8=", "iAS8vote3fHzTp5WlJH1zQ=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\
			Drive.Letter + Bfs.Create("ivdyF7KowtR4f/+Eq10jxsVvM741LZumL9Vc8zF5OMoi/kQYzfu/o8+BiOwo5ywcB0/fASkR3iXACGq7xWyaxQ==","3zKdCvbWQld1+vXNl15aAjTLftyQ7ImKOg8fLcMgSGs=", "oEZw19qmVP4HqXzwFADiJQ=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
			Drive.Letter + Bfs.Create("5gmGeu6WT4Y5qNobC/f3Iy++Jv3NGwJM3/qCNC2yjdo=","S0VD0lwq8BmzKACPJBHgV4xUsdvQcri4bhGDO/Q1F1o=", "S60VR+Q0IgRTLJqtg4ROhw=="), //:\Program Files\ExLoader
		};

        public List<string> obfStr4 = new List<string> {
            Drive.Letter + Bfs.Create("sQUPUDQvsLZHriCnjpxATUnC5zxSAKpbVuddl0L60Vk=","NJhmD/v8ymGhdZflWoDJNYi11vVo9E2N7OPWiZwWiKM=", "Jnnj9A69baWHDF4PjdZO8w=="), //:\ProgramData\RDPWinst.exe
			Drive.Letter + Bfs.Create("hrPRwR7vk0GpJYDaB74GIDCrW8IfRMvhmdybRQDZrsNLnsR8e+OQNJg2E1m/Rlau","70m5E4YbPVvJlg317l96/bIkcYKlrackbMdCeJDs41Q=", "QOvyVZBVy3EUNl1JxPhKLQ=="), //:\ProgramData\ReaItekHD\taskhost.exe
			Drive.Letter + Bfs.Create("gfh7E5DXxIxxv2gqn26a3Y/X5wIOKttJFzZ7ocbnm0KMyhdau8/q1lbBwoBrpmMi","1MBruGv6hSvq6tlMyZ4cbas6cZOHH3xi+xkNCn5o9TQ=", "Rgzj6Cr++11ww2D7mYNlrQ=="), //:\ProgramData\ReaItekHD\taskhostw.exe
			Drive.Letter + Bfs.Create("giRrakhnTKvUYWm5hqBxvaamQurPhvS826mGOvVoB6OJeeom/6n/8yE880a+XP7V","9sWoqxIpTPQD7YyCBT5N7/o+Ok872CQO1iauA29av74=", "ped2tH23Pp5XZst567RQiw=="), //:\ProgramData\RealtekHD\taskhost.exe
			Drive.Letter + Bfs.Create("fJXP3ZyIoMVGHMpcmctMJ0wDLCbxW3AyQRbaSOSmDVLYXTx9CVu3s5+G53/lHnoy","Mi0Saik02BI7UxECikdQO5jaVuOiKpjCwACVb7fyKU0=", "MpGk5UO8OmWWD7KM79HCyg=="), //:\ProgramData\RealtekHD\taskhostw.exe
			Drive.Letter + Bfs.Create("Xv1ByZ/VGWAPZ5go1HfJtUjdblLVWPOG9CbQT3PT00H8AorqFkOpCwetM01DzyND","tjUr9oxmM5431Rp6AMD7yRSsZEyVDActeiEFfE5JG3w=", "HJHLqnGvjGDs5j2UY3m8WA=="), //:\ProgramData\Windows Tasks Service\winserv.exe
			Drive.Letter + Bfs.Create("qkiv6QIYfoZvuz2tnUPGvXJsDDZUmO2RW1Z7ArnlP+AFiuF5k2+BlagObqrgTa1k","Tivgq3Y0mkCAcHqe8DTf8y4bTCxpNfn0/AFDyAltf0Q=", "PeELULHxeq7Dx1Pfb5fExA=="), //:\ProgramData\WindowsTask\AMD.exe
			Drive.Letter + Bfs.Create("/lQsfXsjZ1AxwmtvUEMdc/OWdlK4XM2OHHVzQAqH5TorIHKFeKN2hVNkNiKiE1KJ","2S7sofLIyU6gu0gXmESRXfJYnQbbzIzHj/OzB4rJQME=", "b5ETGvGFMp2FghVd9W+n7g=="), //:\ProgramData\WindowsTask\AppModule.exe
			Drive.Letter + Bfs.Create("MvsZ45g9HQ/+ei7BesAszic41WdyXd+h1kI+uu6tuhOhIxENNYKMh7XjNcRnrFhB","nxp7FB5GXMsdv5hs5LrG/yLAWaFNGgD7vqvOIoD+djA=", "BJsDFWHrsQpTqhFxU+9JCw=="), //:\ProgramData\WindowsTask\AppHost.exe
			Drive.Letter + Bfs.Create("fTzwIAMulfAMnbJiLHQ7YF4szKHnGb9FujNzU7GHlZZgWjknlr6ycjc+y/qpFHMY","/CnYro2A3LbMRGPi8KqgQykwbXPfr8BUNtRAWOi6H6w=", "PCdR3mIzQbRAEXNQUYVM4A=="), //:\ProgramData\WindowsTask\audiodg.exe
			Drive.Letter + Bfs.Create("Ft5yGXBnPOf6ukIuRwdUBstWvi++DFPDXk4J4uAcrwX7Uuvf3NgAgm9oB48p+jD6","HV8cDqBfCwFUiTuiUaYfrq/GiEK1KTx6N73OKm1eZUs=", "SmFv7IOpDn7z3aUYPU5C4Q=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
			Drive.Letter + Bfs.Create("dBTJA8hpog08OoL1oFhbSA7FNPYYnWsxQrf/Ub7vv/U=","F/oBnNA5jfxd0WZTac0f4JxAbERT/NXguEzmG0V0irI=", "BmOzIa+204Hq91mZqZUULA=="), //:\Windows\SysWOW64\unsecapp.exe
			Drive.Letter + Bfs.Create("Rtzf1utL6JEUhlZb240qNL2nBts5o3Ln7kD+cguHEt2VnfooixHb2akdQImDUviVrSwqLXFnXZNIbPIrjU8Osw==","05KSJZiMf1zl883WM126QT6avGKmi3Rxi439s41sgLU=", "C41X4Fg5+pmFWo4Bkk7gYw=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
			Bfs.Create("1lLqsRXiShrgXB0i3CE9mVYMIsbn7nO2h3QRTAI/ahk=","8vLnwvHvfDrKTXVG3ULyYnYIevjSD1HSLR4JWBsoKIk=", "x2rvFrn9wxyacB6cstG/KQ=="), //AddInProcess.exe

		};

        public List<string> obfStr5 = new List<string>() {
           Drive.Letter + Bfs.Create("tGjMik1RVpkKEM8hYcF2Oy3C/QD40NO0r/CH6nfE5ks=","1LO7f155pDcGZeenSVrCbPfWoc8w2ZDbLYCrFh84b0I=", "NVl13aGV/IlNhfFVJ9jphg=="), //:\ProgramData\360safe
			Drive.Letter + Bfs.Create("cJSeZ51JxDQ9X930iU/dsmrJ8GXe+4HPh96ymx5xeJ8=","reah+ncGWVlOJf96e7TE27267gJXwHkTQsRfvotyGiU=", "aoOP33xBLCZ0DAhzFm2XOQ=="), //:\ProgramData\AVAST Software
			Drive.Letter + Bfs.Create("ARJfFoqqUdVFILqhG19ljNHbphcKFb9Ni2P6i1uv93I=","YM52egN8I6G3bVVZdSwpiTjwkjTdWykF8M4noql7wDU=", "0DrjU6zAuU7GBxUxoEoGTg=="), //:\ProgramData\Avira
			Drive.Letter + Bfs.Create("8Hih+2sRlb+9Sb7vdgJUiwfc2ORM/uBBvg403SIMVO0=","riydgoWVTbLCrO/+Wib8kD/FSBBxspO/oQ06Hfz68RQ=", "PguYT2ENwIKnnW+4wJoxdQ=="), //:\ProgramData\BookManager
			Drive.Letter + Bfs.Create("6YBsaxB1dJk0U+d5r2FqJKsC/7QT6XpDOQUaUufoZno=","GA67cJkmqereN8koV06tx1RlB8PKx3BI3w/hH2jW5uY=", "OEdFsmsZPlchKE57RXTylA=="), //:\ProgramData\Doctor Web
			Drive.Letter + Bfs.Create("e/gUrDlC6bXlFkPyqg2foCJBnrss2pcLTzq+cdbhC0g=","Gg0DtN9n/sdn7lv8Xg+FwnAADjju9k6jhZloKIpQ2zw=", "GpBJmG7At9QoDuMRrziiDA=="), //:\ProgramData\ESET
			Drive.Letter + Bfs.Create("fckn55dv4GRjVzmoRZHW9ElWf6WDxw8sGAzm3dSMXQA=","GvY2p2Cab+LDFB8EZevlSAClxxjaKeTMhWr9tvr8jQ4=", "kFUNZN5QSzPLJ41JBzBI7w=="), //:\ProgramData\Evernote
			Drive.Letter + Bfs.Create("UEzHoVNeUGhd3Ts28ilCBT4HtlXbQ/VOWOfg0ZEn9/o=","GpgS2NBZXVoGCtHNTlnQLu9dcQppbouE5CU6XyCnqzs=", "NRuio2G6HtHopaJH5w4u8Q=="), //:\ProgramData\FingerPrint
			Drive.Letter + Bfs.Create("mxPorX2lJPTsrDd5Eo16Aj6KY/QomjDxXpnhcJ4Rr2Y=","wIWIrqfXnokd045pNTt+U9ll9sZB2IWSKhoMiCkNxHw=", "nqQZn9revuL7awXbM+Pnlw=="), //:\ProgramData\Kaspersky Lab
			Drive.Letter + Bfs.Create("L5V3soIaoYZ4G1KHjxz9YIQVbyfOCHtxXn7sCQVXx0RaJUC7Ux1GNRhrvX5xf2T6","lR9z2/eF8MPdvnLaPyUEy8Is8pbUcgPcwZWHgGOPz+c=", "Ns/UJhNb09NCUHUvY/lNRQ=="), //:\ProgramData\Kaspersky Lab Setup Files
			Drive.Letter + Bfs.Create("jOWHsNfM3RcmXoE/gDUDkFzIrmi89VSy4vpLHbTelB0=","Z/8QlIiTO51ZM+t2qwHyxUEJTqkoRh87arDkaFMxNSw=", "A7xPIIoqFuFV0NAT4YAUbg=="), //:\ProgramData\MB3Install
			Drive.Letter + Bfs.Create("ASjxl8849PdP7WInXbWXsUe0I1zZW9ydKwObeKJWhBU=","3Su29WxDXVNQHJ2xMMrk6EkTROFxkVJDZsThzE7k8Sk=", "mNgQKzi1kMoC739CvH2peg=="), //:\ProgramData\Malwarebytes
			Drive.Letter + Bfs.Create("RxfmwqAZdeP2LJeO3ay1vsKqy1BE0fGB4+3qWMfOjRw=","kO/APkeZV+95OV1gre4zgnDJLAtnqpsoETD46ZITO2k=", "3Y3vvsW0ElJWOiCQe/z+fw=="), //:\ProgramData\McAfee
			Drive.Letter + Bfs.Create("WIRNjx9KKNKwnRRwzTdCTbP5JtDao1hkfo66cwbWUfU=","wzH9dYA7y5WchsPxW4LhL6eNwO9jhTfHgx+SHANSBsk=", "Gr9TKJYc/GEUBm/osvOf1A=="), //:\ProgramData\Norton
			Drive.Letter + Bfs.Create("+GqikiQGqiYvjSyuQ2s/pIwvfjhntjWvXdesrG63yoU=","eI1IQ29PVQOuJc9BedpRGye/Efo1ZlFwwb3y557foRY=", "3weuOIoWnM+h2kaI8OunsQ=="), //:\ProgramData\grizzly
			Drive.Letter + Bfs.Create("3v0y070rpHGNBDQrilojs2bW5I/qEqNmGaii84JHK1ZcOVIUJ9S1d6EFfnrg8gH1","KVK19eRT/pdbhnGCgyl4VBj/Tgx5kVkWkytKig/HCps=", "gr7qfDIrY9sE/zUI2Poasw=="), //:\Program Files (x86)\Transmission
			Drive.Letter + Bfs.Create("x0Brpb3+y4mmdemEwZfGcXG9E4GuW/NahF5w4klIxIMjvfyMlmmxtqUbAX0QNlgM","2m01M2NNe4YVo1kxO2o/z3XZuCvAG/MzGZjpBaktSuU=", "vs0GNzSNl+NzxnLiA2L7zQ=="), //:\Program Files (x86)\Microsoft JDX
			Drive.Letter + Bfs.Create("em5jvRrqxGbqFUmuXOlpXDx87+05Tmsnk60CDAmCLfw=","08G/5UWc7ltEoGdh9HJ0/3mSZKOvC6g8/dnNjCYMhPg=", "rq3M8/9i2YKuz9OSymaK+Q=="), //:\Program Files (x86)\360
			Drive.Letter + Bfs.Create("q1OBjxvJyknKXYiY6Mjeg6bDFJPFxzwpdFUxKcuPMXc=","gBhLJLNttS153I4WQZLZGmK1s1xC011YgF827/czkFM=", "g6WNogQXeFCUUVjE+p+URg=="), //:\Program Files (x86)\SpyHunter
			Drive.Letter + Bfs.Create("D/dgdjMZfbFTqDCWv9TYyUu8Q7fZqzPSSjT+q6gEdZyQxAqf09nqPESaKbB6mjPz","DrJKE8NnLI1dpTImexfE22jcy+hIZPd15zZXltXYCQU=", "cX6Y38KeAELUcHCf1O+RSQ=="), //:\Program Files (x86)\AVAST Software
			Drive.Letter + Bfs.Create("qR4SdBvy+ZsdXZfuMoCMfsJcJalgQAXRZLvjIoq2mgQ=","cSdFb9ga93xHe/3FzW+gwCfOP2rbuPkpNuHEicpbYaE=", "jm1EKN4sHZBR64Sc4WiOLw=="), //:\Program Files (x86)\AVG
			Drive.Letter + Bfs.Create("07IInEuzGM3HPjnHSdnIxCG6l6WOd/tTunw55JFBB0oZHg0e/4cIUuvGU8yBLaLo","51eBpYjeY7A2EIyUfGR8KYRn4kJYFcH5z1WJ6MQeQGc=", "G/8xNMhmzxaBp131yjCElw=="), //:\Program Files (x86)\Kaspersky Lab
			Drive.Letter + Bfs.Create("CAZqVTl+5Pc8Q6Tc8M96NSO/l1/A4+qz9x/62qp1kJQ=","Ar/hA51LyvNw678U7kfbckTbgxOAiRmc57G8ffn7VX8=", "Z0YPkNcUfJd8G7uUq0K2ZA=="), //:\Program Files (x86)\Cezurity
			Drive.Letter + Bfs.Create("cp+fKs0MmJtsZ0Tmu+uQ7v6sQAf98pRPYkyMDGaN4FnWzpGrskjOjiqp5Ye6TQKN","d0CVXNH0Q/tYg1eu+1wPOkWH0JbmI/x3UIKfbgq8+aY=", "o6TCSi/RZpr651ZKoDQRLw=="), //:\Program Files (x86)\GRIZZLY Antivirus
			Drive.Letter + Bfs.Create("JUt1y/l10JqnzSXOy+MiAd4sHiwxBrhw3/o9b/a96MBKy3+tlayhhORdil6ICrxQ","kAK3kup/PKkdLZrwKj3qGd61Sp+nfrIgVP/dsFiLL+8=", "zOeGukFD5HUZuVXNvKOoBg=="), //:\Program Files (x86)\Panda Security
			Drive.Letter + Bfs.Create("2qDi76lt/NNqYzTXGCfbJw1IWJJ4Gto7PtbBkv5gjrWeGOpB1oU34jr2jHDy1LSE","rYtbHjIkVaqkwf6KMIMLtSnmOSTFiXsdfTxvPJPJqL4=", "NkAZ3hKGOlYu6iKCEjKO2Q=="), //:\Program Files (x86)\IObit\Advanced SystemCare
			Drive.Letter + Bfs.Create("B5jkLOlOyKzGKjJKg58TMjimHEsQEVgsrZcW7m2rJ9HV8Qk5GFSuE+Yi5AuEERCumAy9vIOQmuxnKM779Kxydg==","eW4hMVtVIjSy830SYAlJp7YJskgP1kX+Oj3H1JPt0DE=", "YZjb9vA8HAGy0p29/tZrkw=="), //:\Program Files (x86)\IObit\IObit Malware Fighter
			Drive.Letter + Bfs.Create("dAuvNMUZiMx3SMjxpJeztH+6a7Y1kXKq6wn1HmFawRg=","1jVgzmc99QjFVl7elQ57O6o3ORw4wRjM5dVvagqnAbo=", "pjDoNZieaHeKGf4Lp3qQuA=="), //:\Program Files (x86)\IObit
			Drive.Letter + Bfs.Create("Cq5ZUa99hRFynwEhYssVG/shy+RgAxTbaenCL1zxCNc=","saPWOlNR8+AFI/t3tLMsUI8q1KH/ONJhIqZU+b+Fp0M=", "x9QmzDGb2FCnMEpYIWmCdw=="), //:\Program Files (x86)\Moo0
			Drive.Letter + Bfs.Create("bPR3zGkqaf0khwNXSXbmlnGwDazuWmlEEt2biWd/JBlx69v9Xbzp0DHtSj8NFa/Y","x+zZPglEhQeyAoVevHKutieRhuEyjuxj4bQTB3RgnzA=", "cFvxqC6PHLxYexBdd95AzA=="), //:\Program Files (x86)\MSI\MSI Center
			Drive.Letter + Bfs.Create("FnjzC1YIVaKePTgmDbfQAnLTr93eeavS//NGHnjKAUM=","Rp2BfkqrCbR/bVLSUT+pajcftMYV2EioBUUjoiFdxXw=", "FFQRGIAc4W34JzqDjyWskA=="), //:\Program Files (x86)\SpeedFan
			Drive.Letter + Bfs.Create("gVj1yuKRiuR/4qJMdAqJZPGMeWoTTb5ROrn2Nu68J5U=","CJmuOLbgtRlAWxBR6qCujMCUOsTOgG4iL4EX/6wbIWY=", "khI0PQL5KjlKtx5bDI3WYw=="), //:\Program Files (x86)\GPU Temp
			Drive.Letter + Bfs.Create("lYZHDDxyaxF3B06QrnOd2g74A+hDCk6bidMaLELQjQo=","I8acE/5/ovScFNjwmyVjigN0rSZxFRmhPaGJj9se6Zw=", "Mj/gCLWgra0490LKVY9cWA=="), //:\Program Files (x86)\Wise
			Drive.Letter + Bfs.Create("MQ3Gogr68iMgNVTTLahoFc6CkOrIwpci1B8NW3jX1CI=","zIA8e9SpNJqLVfkWmJbMzJ5vnqu3sgCtbalSacX5nzw=", "jpjLKaZMp7LuMhtTSXfrOQ=="), //:\Program Files\AVAST Software
			Drive.Letter + Bfs.Create("2BHi1gBBHWOdb3rtg0e0pttYPt42Nm8w2tqtlmw7TNI=","JIW8ax+zuylVXyg+VrGYB96L5hfxGBxEoEqX7n9rmAE=", "wbJeW96m7kvp5e+BkFC/+g=="), //:\Program Files\AVG
			Drive.Letter + Bfs.Create("6tEaNcHfFvNaMf0Wq7EBJaQOtyAZcUB5ob3NTlAt+2CWz5UHhQW8rCmBqgPd79pf","V22LvNSn/50FQCRtHHsEqalwUV/Aj+MxXguFbM82DvE=", "mD8rAHwoX7tlIy82um82cA=="), //:\Program Files\Bitdefender Agent
			Drive.Letter + Bfs.Create("Q+ZrWTXKfiIlfkUdyanmkUxB8xnkBj6nAtL30f/3EJc=","4fEULqlKVpYmWxMa7rmUiC49Ncu3Ld9QF126AcHuS9E=", "SlXjmlchLIcTRYa5Jn6l2w=="), //:\Program Files\ByteFence
			Drive.Letter + Bfs.Create("J/86PkqMPnChJT0f98Jd/B0I/dlujwOOlXzawrgob8U=","9LrXIdiy9RA7+GopinvJUXAmnufSdXgxa0nXy4SG5CM=", "C+0jD/640webzFz/JXpprg=="), //:\Program Files\CPUID\HWMonitor
			Drive.Letter + Bfs.Create("G2xWKppM6nTL0octUuCYlrHzkN99YRH7XDQZZnhb9FQ=","Jl0UeCrLd+BM48jHS2l9Z1q1dZubAhF3iQ4noyO/Mv8=", "80Cm6tIzaTvX7IsFiEvaUQ=="), //:\Program Files\COMODO
			Drive.Letter + Bfs.Create("87XUyZEGS/zo4ENvjYUnlK47z1FUcAhh2cMljBEMTJM=","7ExCeUaFBAHN4VmaL9GgSHjwZMcFd/4/fdKFuFwk5fg=", "feTM73w1V669XlvIsFQjfA=="), //:\Program Files\Cezurity
			Drive.Letter + Bfs.Create("mP23cDc2msvxJSuFZiWOwZO33tY9Ia3G736nlkc8EBo=","PA36fHsu9Qm72HnGo29C4A3BtLb415zm+w5qv4vVTpE=", "aMxi18UjHDpH0qg2IPFxyw=="), //:\Program Files\Common Files\AV
			Drive.Letter + Bfs.Create("S4M4UQhDaZMa5thdcQP5KOFg6fz3/UFXhH2njoP8jBArfcAIYWhFDhX5v3I/rvqj","almw2TN2/KA34cg2RZ1QkhWI5V1cPPuo9IXbn4tExMQ=", "TgPdFMfPsrDVEQrokx2flA=="), //:\Program Files\Common Files\Doctor Web
			Drive.Letter + Bfs.Create("J5xi1m/zUk5u34O87aFEeQU1uYJnv6ulVZ51gS6iVJ/WWaWynljiE6stYgGKgDXs","6kyu4rqA9VE72KwR0I33/tzRSYp4b/AvSf2m0/J3/fU=", "FfocO7rgV/ystO5ipns2xA=="), //:\Program Files\Common Files\McAfee
			Drive.Letter + Bfs.Create("NyEfYodY43aojlHTSF73swlgEvmr0Eff1hntT/rup+k=","E4MiRlmgCJ4bwuJ8JXwO/kBRgj3+Bw2yLduEVAeL364=", "UDg0rAGzcrP9iQYz3/liBw=="), //:\Program Files\DrWeb
			Drive.Letter + Bfs.Create("1zJOlwuqZk8JP0CQ9I8fIKbk94k/oyqWRrfDcweqgwU=","tgoOjRAMX/vh95rNyRDk2+fdki5f5LTKGipfTEY9yWw=", "1EV+Pqnx7H9XEY2eVtKdaw=="), //:\Program Files\ESET
			Drive.Letter + Bfs.Create("WBW5CfkxS4tSLXg9HpwweQr7Os9rBmYJ7V/H0PuvJUAqqBf8wxsq1ND84T3Sdw+K","uDpnKt2DXUOwCnG84tOt8KoH6V0mEBp601clUOIdBGQ=", "ZP87XDNUEmzB2FPNb6RnLg=="), //:\Program Files\Enigma Software Group
			Drive.Letter + Bfs.Create("vR0G9CS6T435S22hLwct9dGQ8ZERHFB7MN+Gb6TPibk=","x/eVFGesB6T91yrF4WeCaAilZdvb6gl2KVF+ynLfXQ8=", "lUEjuHsGVKvUp3YbYHY3ow=="), //:\Program Files\EnigmaSoft
			Drive.Letter + Bfs.Create("0n2vNdaV4RVj47bAVejB2vbBqgUpvKuPLm+ik0APOw8=","m2M1INil4XSReIRFLWgBeIpRMbZUWA12XjPj2fM4wJU=", "1E+gg4Ai3nnLBEQn0WqJiA=="), //:\Program Files\Kaspersky Lab
			Drive.Letter + Bfs.Create("UJyBI00zsiuPmAkA9B84kUPSa/k4J3zdC4i0L9jfIsyNiF+n2Y7uGEvJ1imL+X2z","ER98jikdWBtqFA1diaTz4c6OhaHol4SPF4UFzr5X/yU=", "GQyUXFggvZK1K+a68cKztg=="), //:\Program Files\Loaris Trojan Remover
			Drive.Letter + Bfs.Create("BbMFm7tPn7w8B7H/ckKb5dpz8TJlpj5aOca0VDGVOKA=","g3O2mPQuEy9T0gmBeJRoP2mvdksch/KanjG/OICyN6Y=", "nA2Eq+NFGzaoUljKToBwpw=="), //:\Program Files\Malwarebytes
			Drive.Letter + Bfs.Create("9dp4MUJoNUdi1PTQwiV8PQPWGwz0v1oDS/ywvHzqOe4=","P1e2luu5dySdKVVnEKbVVVSN+GE4ec57pcWCWB2uXfk=", "nW+wVcTIIw0kk+ZKBCE/tA=="), //:\Program Files\Process Lasso
			Drive.Letter + Bfs.Create("GAfdlD2CIDbDbGzotG0lvEOIKPGyQJpOLwVddJPcHR0=","sonHNkR0IFu0nZqkgAGW4pRPj+4PwyV66+dfMOvmlZs=", "taUesD3XA0e3N+sEqOrs1w=="), //:\Program Files\Rainmeter
			Drive.Letter + Bfs.Create("3zaOsgBpNe/Tc0aTpSupruKnYC/gaXUitK/ZmYaIMbc=","KsQI/z0hgFreuUZF+5a1wCZfoBHFDmDD1T5MOVB9qkY=", "qd+NXG4tlnIB/wohc8qsWg=="), //:\Program Files\Ravantivirus
			Drive.Letter + Bfs.Create("nBzcc5hy3X6bcvMS6vkuNV9tytn6/scFxpAy4pqsGI8=","Qgu+QB3hkertRWtvGEdRfA1WmE8oFztYWCWhZRpcdFQ=", "vm04T7jC2CfVmXFvOE7Y3g=="), //:\Program Files\SpyHunter
			Drive.Letter + Bfs.Create("j5MwqE2b15wY8Ehpu4BGHS7be62RoSJCcfaFSeot0JIaGU78rXI7G00LwW94MX5g","OMSkjELuIv1o1RMPn4Cj9npia25qt2yelyUPKwaPKe8=", "zVMcqoDt1h3/H15/uyarNg=="), //:\Program Files\Process Hacker 2
			Drive.Letter + Bfs.Create("XmRFyVms9DSlmzGnu9wGFn6CWkLFROE8+JoWrSmop8E=","n+6jA/EvKt3MNSyV8yNBMIh8iej0qL6DV+2USKYme54=", "xwGNmP1usItlXFnYU+2CTw=="), //:\Program Files\RogueKiller
			Drive.Letter + Bfs.Create("edBSdjywODLcZyeAwilQrft2FdmMfSvAoTo0QxJP4V5173nJerpqhhn02sY+t/6/","AD8yp2/7LiV6aML4QOSJlJJuOtw3CgFivvPp+KZL0Iw=", "MC7jlIUlUQDGpghOVnFIjg=="), //:\Program Files\SUPERAntiSpyware
			Drive.Letter + Bfs.Create("pVPZCLuDrg7M8BJZN5VNeVYR4MRGaiBjw3N7nKwE8Uw=","fxEFevV+QifIXLaheGiQVgrqtmor/yJ6/UzSHry84cU=", "26RiU/QQwLustde34cOGrg=="), //:\Program Files\Transmission
			Drive.Letter + Bfs.Create("86JMz94DAapl6UyrvTBdyJisH1Od5PCnfX9C8no+o6M=","qhGYIxiSbjYCJK31IxuXSy00zC3v57rbjGsXRgc1Xlc=", "1gTS0HtSz7hR2/21SnE+wQ=="), //:\Program Files\HitmanPro
			Drive.Letter + Bfs.Create("qfr7ZsNvRIWnmT/Ps0b8riH7EIHwQYpKLziWj0FHYcc=","ol+JhPitu9hxfKVWmwTpamB1zxMCwfMCKiSrtxNC9e4=", "ppKDJhn9gEzGtp3rsPBltg=="), //:\Program Files\RDP Wrapper
			Drive.Letter + Bfs.Create("WcFHUERDJYS/Fu39fmEJmJSSUUX+GbqLgX/nd2Zrsaw=","GtEYdjXVov0v4MhAZijcKwvULzY5PgLzZN2M05oabkE=", "G0XBNMPhquyh8CbIcKHVEg=="), //:\Program Files\QuickCPU
			Drive.Letter + Bfs.Create("/rlm0BeZCEW/l0UBvSAgnSnZugs91r8wWc6nna2CFgk=","cb2qNatkS2bcj4tD/No0l4+sjh0IjumulOPibf4aCV8=", "iU3sBRfIRRvLzHin3JDwWQ=="), //:\Program Files\NETGATE
			Drive.Letter + Bfs.Create("goLjZzg+zW1wzjJ35tUiwQ1q9R0IAWyaJ+UAlvYxTLI=","4ShCiB7ynNUTTO9/U9ua1FwBNH8CtacwUU2j25Q/fQk=", "AGnw9nlBtqRYE7OXrxh/9A=="), //:\Program Files\Google\Chrome
			Drive.Letter + Bfs.Create("FJehGieKHNE87BszD/ttL5CDq4naL6pHeZ6E4mKbHT4=","RXPvArh7yf0MxStLXidMal+qvhlD3nfPBEeztBQLT4I=", "t9XxZ73UFK863IEaTzeQIA=="), //:\Program Files\ReasonLabs
			Drive.Letter + Bfs.Create("Yr3saQkqZQ833ebrO+/K9A==","EGNu9HOS61o75GhbLmJtVM8MeTQhvHOfz3ft/P+9Ls0=", "WOqqragHK4VVJLQZPreqxA=="), //:\AdwCleaner
			Drive.Letter + Bfs.Create("mGFGAOSjIADizHNhgcxFGA==","WfhiVwEock/02N09/KMkbNkUoofJNrTXt+g2w7yMMhk=", "4IK1Arfc8dCQSJevW9cX1Q=="), //:\KVRT_Data
			Drive.Letter + Bfs.Create("05oo+bDMKjNoyycU8tFSyg==","oCPTs8FWh6oYnuvZDYx3y3b2eR5JXLcqCpHxILODXKc=", "T/XrXitxHkIuGTP5IpRkCQ=="), //:\KVRT2020_Data
			Drive.Letter + Bfs.Create("efdAmlmOojGL/PjVr5Jd6w==","w8ISNBPBEfQzcbcLvp+yjjUkXOYnp8OpdQStMjsIieM=", "L2guS+WNZsuh5GApWlh6DQ=="), //:\FRST
		};

        public List<string> obfStr6 = new List<string>() {
            Drive.Letter + Bfs.Create("Qcm8cj8PBr0xDFUZCYtoDg==","FvuzwlKvOhN2Ar9ZyDHMp5KQVQ6BVIuCj/H/vEAOwkw=", "3JuaDWu4+v80KNB5xCMbBw=="), //:\ProgramData
			Drive.Letter + Bfs.Create("uCQsAJfxxOUO6RkFbbuPwA==","BeJ7A2YovZf1HhAx5APzpAhXuWFANx1JSR1ssbFpZtQ=", "yhgFs8R/lZrkwSwOTeBo0w=="), //:\Program Files
			Drive.Letter + Bfs.Create("8xXL37pVt2EY3O2gukw7fenNJCA+rGA+ygznUeGYccI=","Uud1UGzVZCOOSiKiqRHKeJ19fAwg6c1lCW2cG8jRTYA=", "c8mHC0CiIIq8hv2dlSRkAg=="), //:\Program Files (x86)
			Drive.Letter + Bfs.Create("4xfbiIxqEMwC2LJ6vcdscg==","f6u93g7fgT8JJKPNpx+yjluqhYieWLj/zDZ7iOcUUbU=", "kSpfF6xB2gSkNJ/1gycbWQ=="), //:\Windows
			Drive.Letter + Bfs.Create("alGSnpJWyDo/spaGhtiifg==","QqZYWjs6q1Vts4wMplDxD2weg+9VQIFHksA8SlcvQmc=", "NA+3AqdDLmO/lgRFO2NPnQ=="), //:\Users
		};

        public Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            ["TcpipParameters"] = Bfs.Create("YWZclObNj4ELWfrOEspE2j1wg9PLJL8cvjDJLNNo470Y/KnV9m3wNoV7T/YR55vOLyfql1lgfDO0ISadEkJwtQ==", "VgcctudoBzFFDxscRUYBMvdHKEaN868My2oNgwURKGQ=", "vk1hYOThjEvuje9Q7UyV3A=="), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
            ["ExplorerPolicies"] = Bfs.Create("L25yttpwkmO2W58eI1M8RLTjXCP9YaCyC2ZpXrUoAxo1iJdWqaaC2LFHqbZVUENYHr60Swmq1xV3p5ThUL/hyA==", "PJbr6MvKu2BX8t7RxHTnbTlykMaH+lfI+U9Uq4N38e8=", "+7PFMnAkQJE6PE8hmUN0lQ=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
            ["ExplorerDisallowRun"] = Bfs.Create("bVqBlRvpgFiDSOANKVjUzYDXj8vbv+153Cleg9hRy0+dOdnC57pJEtbjAyD5H3iFbYEN9JKai4BlSn+XWrvsnQmr99aZYYLHbIDaQv+sYlY=", "B6bmjIeV/bfYxv5Vkq0kWZOaBtiLX74sm70wFUxlrNM=", "SdOt6IvRECxFZrjxiHpyvQ=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
            ["WindowsNT_CurrentVersion_Windows"] = Bfs.Create("J9Glk1/Gul1wFc+GTd4QLSXv1BP+hgt0J+K1GZdQPsNUVDmyAdtC7sOSmYubTt9gUoOoGbvZgvqxnp6oTo7SUw==", "mn7tXi9VJbx+GroPCZ54bzt/rDB0gSuf/aXYW0sEMR8=", "xH3lomK/4OoAHRzs2DrEkA=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
            ["StartupRun"] = Bfs.Create("NGtwA+7BX683EkFRqzlkXU4Hnt847PYn8nUlhvvJR72mw1n9t+YaAttUq1ZnCY7L", "n7jpA5sGJpfLtuU80NS3nmUCq64opzQIc64pzd9tHn4=", "ElqVtZMaw96nZji2Q2czMw=="), //Software\Microsoft\Windows\CurrentVersion\Run
            ["WDExclusionsPolicies"] = Bfs.Create("D9GdNYZtK0nzdHREiMsvYMYhs69Qkr9Zif4sQtT0XPmNhRGSGwIjc5SJO3wVeTuLKimyx2JZu2lEm0P0QcLOQQ==", "i0c11ciMz5GwkJFhElILaEH2GauGAlzZMfPf/VgOk5w=", "ZrWbV19bmF4SnOQhdiwnxA=="), //Software\Policies\Microsoft\Windows Defender\Exclusions
            ["WDExclusionsLocal"] = Bfs.Create("L3pgn8AyYJanVVqsqJZ20Z8F1XwoWAbz8cECL7HeIEfFU4gTMGxk1l6wdZLDVWMO", "Vn8WELP+/0VP4xz9L1ru+zWQ4KaWe1Ia38uy2GouYFw=", "t4cZLKZK5INwl0brNUAuVg=="), //Software\Microsoft\Windows Defender\Exclusions
            ["Wow6432Node_StartupRun"] = Bfs.Create("SzBc1OdTj19zl8qX4zY0dOFlQEbgWwDwqj3WqQa+Zoy7z/Kr7KAPPImGSVsQgtlqqZu47NfJYSB2t62C8q/zdA==", "vgb4O46GRHB09P/I0TIvyObpPTQ9g6jCZ7SltADE8ZQ=", "8X9BWVSMLJFxweECJWbkPQ=="), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
            ["PowerShellPath"] = Drive.Letter + Bfs.Create("RqV2id6SnCkUPOA4GDBF5tqdpOZMcksO7xAZH8deqmlPh9tyZB5L+ouWXk0cN610", "bs03PtYuLPg1D6mYdKUYf5lIMVwJj+l8ye8JAvhwU8E=", "Q/gA/Ovp5rAiRCHkcUn3qA=="), //:\Windows\System32\WindowsPowerShell\v1.0
            ["Defender_AddExclusionPath"] = Bfs.Create("zRvbImIVQTMjP+21nXouEeoip6cJ00nysX1derxCHW4=", "+f7y/lRoo8zHzAS3erRTNMKq7e0Q6A5uJ8/j+F9zafw=", "Eg4nJ87pA/osf8ratLDMQQ=="), //Add-MpPreference -ExclusionPath
            ["TermServiceParameters"] = Bfs.Create("+V7LCyS1wYsm1cQmj68UL/+/53PjOIrEFAfndm+uxXxsNAEdXUEyWVcHaZT3KNtH7DyP2cZ/WFdvLVq1gTvamQ==", "OLs3uptDtjciIsZqZRwyP60q+gLUZMdBs3wBPW9uFSw=", "AxYV6WkiFcRUIS+nt6JQ3w=="), //SYSTEM\CurrentControlSet\Services\TermService\Parameters
            ["TermsrvDll"] = Bfs.Create("Ppe/MjngX5UrLjS0NTHK0XIFp2//mfwpFw5yngW2AcSfTwIdMcVj12WgGTMYqgZf", "woAsq4feNWYuNHcIEp3JEpEBqgyDyMdyBC9jI51yo8A=", "zBoDNrSUPm817CJUL4L1nQ=="), //%SystemRoot%\System32\termsrv.dll
            ["IFEO"] = Bfs.Create("sLhAnr/JlwWXD45gEoFMib40VdcqhGN2d9i78BmnFNT2lyPmhTrGIlfDxXASIm8torpAWjxUu0gJ1SGd+AEuqFVMI+4qtGXWuSQFRrq0j+k=", "VqwqSvbvZzfx3gojhBdUA1tkrfww3QoN2Jvut4PAxzI=", "dRyREQizuFkPgBydqxf4Eg=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
            ["Wow6432Node_IFEO"] = Bfs.Create("3GApyXf1jBC3JZDHNGEm0Jzd5dBm2SONLPNOrGY/kOBnQfylsATUWWDYSBcLTapW2veGoj1KE8t+T6Y5JnJ7NeRUg9tpNsd0p+gIbXy1VhPHlfyPoJ1PKrhDdj+jSHXJ", "3U/ikQ5Rar7X7Q2HceiiWPeDGgxaiq7/0dNySpeIUcc=", "LbzkmFQpYE5Pi1uAFxy3vg=="), //SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
            ["SilentProcessExit"] = Bfs.Create("lpfKS8T1qQq9UkPjaFqm0+WAGGC0kKjtxsalQQN8HxXMu/14nNYbeV2t75Lxp+ctWqq0dy/a4QyjsK0LhygtVw==", "T4rvum8YWzPxk1IPFoyOFcFIw3nMttX3bHSRJ+TyUUs=", "eus0T0jjBd94rNFBBOUpAQ=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\SilentProcessExit
            ["h0sts"] = Bfs.Create("E/RRNqdtLScmgFRr2PsWMM5K2l1DukGPvkKXWuFcyH8Hxtg7GWTjF13LnDxtOwCl", "QJ6nQXUnRC7dXGbzQhGijlK0B4NFMdL0vSM3OqbnjJw=", "VSLVeiBpW9A4CxIhCA/j7Q=="), //:\Windows\System32\drivers\etc\hosts

        };

        public string[] SysFileName = new string[] {
            Bfs.Create("bsQs9mKT11gUc20HJntgGw==","c9TSrqmq1/BY5k+Z17m//GyrDqJY80z9sP4rXPdmfbQ=", "FzFzh+WijHE2CdeVCPFwow=="), //audiodg
			Bfs.Create("v4y/KljHph0pQ31XABQQXg==","Aey0MknIiiEQBc1vkBqVwTvD+mXM5agO2i/a0lN9vc0=", "EuIoSNoqB3TFtf5WYlxVMA=="), //taskhostw
			Bfs.Create("DiGXCXogIYvXpTnZikEAGw==","37865tLGhWoKRLqlL/WqlOJ1rwNyEnFEmluNo+Ze7eM=", "xuUXx3hZewwfTpWoNBEtKQ=="), //taskhost
			Bfs.Create("3oVRmZc7fKBqgTqvHRPSvg==","PlAg6vGuHi85intR0tQ2UKuMkhMbrIVlSCfRBzyDy9A=", "Oz6lhmQS7Kc691mDpQPwhw=="), //conhost
			Bfs.Create("P+7ripOPlkJiHtZ7EkDqTg==","dCnNfOmdmaJ/KAAGoTufgiAg5k7A28sNzVHAmRr8jfM=", "2wlQYr8obzD2ieeBHZ1PmA=="), //svchost
			Bfs.Create("5pHiZ8tFoh6uLKNNpEuoqg==","2RmPsjWS62AO2X+VgrPSlD6WpuHPtZblgWzuoy5EjpY=", "DswAvzT7m2HrqVTIBPjvBw=="), //dwm
			Bfs.Create("n63GlgCy049qwevbeCXlAA==","zGEujGpgAbQh8KPwPCiGTYmfXM7UxJ/zXlMU4iUTzu4=", "vbN/rUORcPgkD1nYBM6l6A=="), //rundll32
			Bfs.Create("L9qncsrqYtfgVJyC3mH/zw==","As2WdIEJanfigzom97rBHbtWPTKUIWPcQQ7uVVSe8WA=", "hqRjyMWlEkRTw0I1DntOjA=="), //winlogon
			Bfs.Create("TxfCflnaZ1uc8Xvhnc4yQg==","DFYYiKdD7Rm5VkRYyNT8V6sPzjBw8KGEXtaCZ+xMp/k=", "b4Rmfq/T/meIABy3WmFpaA=="), //csrss
			Bfs.Create("M1W55z017H46Ci9dYctGeg==","XZer9aNZZu6dwLYAzEnXgjkZQJbaV4zV23dzldhs38o=", "PRrFSRabJ4/0zAttvhdUnw=="), //services
			Bfs.Create("/xhZfpwFjXuzI9enCucyFw==","xpTX/B9DsxIy5Y5GyPGwuw4jDQcQDN/U3M8YFfdrs8E=", "TaA+0/ZMDkuGjKj0EVCHPw=="), //lsass
			Bfs.Create("cY3vH1yvhUuiPit1qhgWzg==","IcKEAE3hl0mp/m0MbSJpZ++wwcOCVxIYocgm/DPanIY=", "8QNamySGrceg5nYLudx83A=="), //dllhost
			Bfs.Create("dC0xoHEL8F359mFBk2zwvg==","wzGRgGdUPngErT5oNqryXOetFvvo1ut42uB0zov/KpI=", "GB00VEODkknPe2E18vuQXQ=="), //smss
			Bfs.Create("Gncwa0GBvl3CrqDohE5lkQ==","Byg9vo5NTChOVFuP2aHpl0byVx4JlnS9E3N5kLqJzx8=", "fhsHFOqpFgJRktJOCAJApQ=="), //wininit
			Bfs.Create("FpjaVEMeFPdxJLPBcH00+Q==","OhCnPE9NcdC2TvlFoTFrn7E1mFPYpv7WYoeWVlBjZOw=", "oK8yeIZiusLu6FgXIa4JHw=="), //vbc
			Bfs.Create("VZaC0RQXPiDmMCtGtXOnmw==","rVwPe3cXiZq996TjRnhMrtRstg4GOchFOIgCwCdV5Uo=", "loAqQ8M1Xxku2S27Mh6wOQ=="), //unsecapp
			Bfs.Create("igdlj9iRW0nvbrp+LNnDXg==","UBklNQEoe6jGTfTMNheAQYzRNOuwGhCJBySLQdYp4rQ=", "VKvH1IMMhOmQG2K4HyHCbQ=="), //ngen
			Bfs.Create("2isPDyFVdJXvwJGKTa5N0A==","vTXyQdf7bOIFlkDFZl/04rirawS8pJl/F8d3DVJHzwo=", "gKPouYVgJGR4S//KKbvZPQ=="), //dialer
			Bfs.Create("S+6KSX1wmiamKaTsjNPN9A==","Kv3zdlMWquoRtPlr7Zz0yG4tmJUyBlbfQQhJ+S9jUmE=", "aePcNUMIsA34r0DnUxKybA=="), //tcpsvcs
			Bfs.Create("7l+FbzJ3Ck8Crkdz3zAd3Q==","eyguhE1TNqvisb/DP40A5VJo8rRd4SS2TMIgGUTFn20=", "tYPME90+T8poblP3/HyEuQ=="), //print
			Bfs.Create("BzzhJfsX6B4zTLWxHVQG7w==","MNymZLFcQIjtaNWgY/Yag+vvLgah6pyzTY3Xp0FjRzU=", "oIBiE/Y7WSvceCFN2AbLTw=="), //find
			Bfs.Create("bVI9nbO91JH3Gc8s0kdtEw==","yw59wtfJElbUIpdcIaNii2R+oNufvVm0daaO+h77gmc=", "14st0DqYOyOjwm0xInsclg=="), //winver
			Bfs.Create("plfun73R+/b6vDwmVTBp7w==","EfwgYOiCCNTVrYzDcoI/vRtvLXlv0DMKb2o/74V6+js=", "BEqmJo6woztmWkx2CYBy0g=="), //ping
			Bfs.Create("1VLzDQ9H9Phs1552X+AgiA==","H6jvMwVq5qJU74kGieRoQVsD7DhVacrFzOfyFHpqhYY=", "TYBAZzo596ONo1QfTLI8yA=="), //fc
			Bfs.Create("pRaTCnfY+my+fWIxfs1fHw==","dwJeVttuv/TQJYhUGu3nMU06EI4kDfMeGLo00ARdqIY=", "c93QPCgOh3ID0SilwNdQyw=="), //help
			Bfs.Create("yGLiehx3VWT0mF0vfpj+Qw==","2IV/TpbdJT/F4e3Rj0fZFcGBMD8+p9D4J2TIEm8BUi0=", "0cEAcG+DkYdvDgbti8aIFQ=="), //sort
			Bfs.Create("IaXb23Xcf4BJNKCeCOpryQ==","pZRuvxdvS0QgWkBEMFKJXGzHV1yMsngKb5Ap/QoCY1w=", "yL5Nx/hreT0dCZVGwX3cwg=="), //label
			Bfs.Create("ajvLlKZXaU/dHQylW4S+4w==","LgWdxfHbSnam48kLfmgWxQFEQl2U93yf5BKf024/Q0o=", "rScZziNQLZf/isPWdLzm9g=="), //runtimebroker
			Bfs.Create("EVcpvbbEtQDqh2QR2jvFrg==","PJYb8JPBscfR8Q8xauaxiRa5+3Wgv4t43MG3cQlC6D8=", "SrF2yiTbvdpygEMBs7eMhw=="), //compattelrunner
			Bfs.Create("SV7ISqAXGZrsaxOfO1Y3Pw==","4a1uJHrjaC7eXJCBeGMQGICLhp9nNXoK/5BiHCsPLgc=", "RXdO0Lcd/koeZo5NpKR6+w=="), //sgrmbroker
			Bfs.Create("XfqTKho/pGknish2mIFBtg==","9ZwNhGku5hIATS+o5h9dOas5WySGycjmlOBuZlohVE4=", "xP5GuJ/yOEbcwl+CKKKH4w=="), //fontdrvhost
			Bfs.Create("xMoc95rXHhuSDIPwE9BjmQ==","1F9NZ56USZdLjnp+8ouC54s2y0UElmS7TET4CLNshEU=", "oru3Ujmc62CMdrOEXTB8xg=="), //dwwin
			Bfs.Create("n93ejDTcCgmiTvonqyoI8A==","DQHyBANUJ439BEm1QeQKNlKMixc8uJ3A+6AV81W3SLk=", "6zQjWhITXX1f5wFaiRkIIg=="), //regasm
			Bfs.Create("rSFpOvqLHoj7Siqy1X2hW061zr7S0Sb68dPOhPXQC7Q=","f5c4DzIupvr0LOTnolkQA+h+N1ABoeQQlhAQGf+IO90=", "6eTisBdIF8/xSnrY7DV3MQ=="), //searchprotocolhost
			Bfs.Create("U+QJ0imOH9l55RAbOCMYMg==","9NC2ZmGyY8bK9lZuW4N78lEWF9DDJ7clyS7PgLFTj00=", "r8klPDgHOSqhWFhbzvOGOQ=="), //addinprocess
			Bfs.Create("C7hnEcS/P9xyz8gCqZX26g==","Ty8uBLDnGa3ODkNzuWjsGt1Wdp2RC7VzHuxxRpZyM6Y=", "GHQ+zU6xFI/gepY5lutiMQ=="), //regsvcs
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
            Bfs.Create("SqSR2j5yuMyPTyq6QFMrsw==","BWqTIwMarWQKbelbMYHMIjDDzASueRTHEf+FH2wLzN8=", "zOCr3ru6NjneEeA2m7aBHQ=="), //SbieDll.dll
			Bfs.Create("FUzuk8M/+naARcs2T6tZsA==","GWsfTVQYnm9f3y+yDT28Qr3SrZWtGPZCrsTK64ohO4I=", "u5PYeewGCq3hgRGPe41AUQ=="), //MSASN1.dll
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
            new byte[] {0x52,0x67,0x66,0x6D,0x6D,0x64,0x70,0x65,0x66,0x47,0x6A,0x6D,0x66,},
            new byte[] {0x40,0x6B,0x68,0x70,0x73,0x6A,0x75,0x69,0x6E,0x41,0x79,0x6E,0x73,0x6A,0x68,},
            new byte[] {0x2D,0x64,0x75,0x69,0x66,0x73,0x6E,0x6A,0x6F,0x66,0x2F,0x70,0x73,0x68 },
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
            AddObfPath(obfStr1, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "cl?ient?hel?per-up?da?ter".Replace("?", ""));
            AddObfPath(obfStr1, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "torrentpro-updater");
            AddObfPath(obfStr1, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Programs", "Common", "OneDriveCloud");
            AddObfPath(obfStr1, "AppData", false, "sysfiles");
            AddObfPath(obfStr1, "AppData", false, "DriversUpdate");

            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "cl?ient?hel?per-up?da?ter".Replace("?", ""), "in?stalle?r.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "to?rre??ntpro-up?dater".Replace("?", ""), "insta?ll??er.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Programs", "Common", "OneDriveCloud", "ta?skh?os?tw.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Microsoft", "Edge", "System", "upd??ate.ex?e".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "Microsoft", "Up?date??Task?M?a?n?ager.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "Google", "Chrome", "updater.exe");
            AddObfPath(obfStr2, "AppData", false, "Explor", "exp??lor?ing".Replace("?", "")); //the file has no extensions
            AddObfPath(obfStr2, "temp", false, "b?tm??ai?n?s?vc.exe".Replace("?", ""));
            AddObfPath(obfStr2, "userprofile", false, "Document", "s??et??up.exe".Replace("?", ""));

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
