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
			new HashedString("00798b05b9906d4031905f9e57f4c310",12), //combofix.org
			new HashedString("0282e441b801ef6fd6712b60b907417c",22), //forum.kasperskyclub.ru
			new HashedString("02cb97db53e82fecc3b47f2a7ab3c6ad",11), //sangfor.com
			new HashedString("05461be81ef7d88fc01dbfad50a40c53",14), //soft.mydiv.net
			new HashedString("05dfd988ff6658197a53a559d03d48d5",7), //yadi.su
			new HashedString("088b09b98efc9213de102758d1c8acea",9), //antiy.net
			new HashedString("09cf5cb0e321ef92ba384fddf03b215b",11), //safezone.cc
			new HashedString("0d3630958f3c3e8e08486b0d8335aea6",17), //usa.kaspersky.com
			new HashedString("0de8be0d7a0aba151cd4821e4d2e26de",10), //rsload.net
			new HashedString("0f93e1b1f0c1954c307f1e0e6462a8ce",13), //softdroid.net
			new HashedString("10e42be178e1c35c4f0a0ce639f63d44",20), //bleepingcomputer.com
			new HashedString("116d64b71844e91f9e43dae05dcb6a6c",8), //avast.ru
			new HashedString("132793c4107219b5631e5ccc8a772f94",8), //comss.ru
			new HashedString("132793c4107219b5631e5ccc8a772f94",8), //comss.ru
			new HashedString("133dbe014f37d266a7863415cec81a4f",13), //softpacket.ru
			new HashedString("13805dd1b3a52b30ab43114c184dc266",13), //nnm-club.name
			new HashedString("15fe7ae3216c7a37d34d02793d180530",9), //ksyun.com
			new HashedString("16297e8f3088fa3ff1587f1078f070ce",23), //ProgramDownloadFree.com
			new HashedString("178c8b444e8def52807e7db3f63dc26e",9), //avast.com
			new HashedString("17baee242e6527a5f59aa06e26841eae",9), //virus.org
			new HashedString("1826c35007829d3483ffd18cfcabe01a",11), //trellix.com
			new HashedString("1a34d8272348282803adbb71053d241b",22), //download.microsoft.com
			new HashedString("1d954e9393c6a315114850d3f9670158",8), //eset.com
			new HashedString("1e0daaee7cb5f7fe6b9ff65f28008e0a",9), //drweb.com
			new HashedString("1e0daaee7cb5f7fe6b9ff65f28008e0a",9), //drweb.com
			new HashedString("1fd952adcdbaade15b584f7e8c7de1e0",15), //bitdefender.com
			new HashedString("23c807844e8c9c0af34a82cc145b04b2",20), //360totalsecurity.com
			new HashedString("250730bdbc2a6fc2a7ffd3229d407862",12), //k7-russia.ru
			new HashedString("25da26174f6be2837b64ec23f3db589b",14), //tachyonlab.com
			new HashedString("2622e56675d064de2719011de10669c7",12), //esetnod32.ru
			new HashedString("2622e56675d064de2719011de10669c7",12), //esetnod32.ru
			new HashedString("26d25247ed88aa5f63d80acf6e4e4d35",10), //comodo.com
			new HashedString("2703a4c1ceef44c10ac28f44eb98215d",10), //phrozen.io
			new HashedString("2ad4f0c11334e98a56171a2863b3ea7f",12), //ccleaner.com
			new HashedString("2b001a98c1a66626944954ee5522718b",10), //Zillya.com
			new HashedString("2c9bfb7c724df7cdc6653c1b3c05dede",12), //unhackme.com
			new HashedString("2cf505233a066a02292a1f9062aa12a2",14), //trendmicro.com
			new HashedString("2db7246eb9be6b7d7f7987a70144d8dc",13), //secureage.com
			new HashedString("2e7596c6145efe2454e4d6b92c8c4620",10), //rising.com
			new HashedString("2e903514bf9d2c7ca3e714d28730f91e",17), //windowsupdate.com
			new HashedString("2e903514bf9d2c7ca3e714d28730f91e",17), //windowsupdate.com
			new HashedString("2f4f102d0800be43f5626e28fc35da35",11), //adaware.com
			new HashedString("327d0b3a0bb1c17c52f6ae1af8867bac",12), //malwares.com
			new HashedString("33ae33718baa80a5f94b014fccb7329b",13), //pcprotect.com
			new HashedString("3469d5aaf70576a92d44eff48cbf9197",13), //programki.net
			new HashedString("348ccdb280b0c9205f73931c35380b3a",15), //biblprog.org.ua
			new HashedString("34c51c2dd1fa286e2665ed157dec0601",9), //zillya.ua
			new HashedString("35c18e3f189f93da0de3fc8fad303393",21), //besplatnoprogrammy.ru
			new HashedString("393f2e689ee70d10ad62388bf5b7e2ec",14), //gridinsoft.com
			new HashedString("39cf9beb22c318b315fad9d0d5caa105",13), //spec-komp.com
			new HashedString("3ba8af7964d9a010f9f6c60381698ec5",11), //webroot.com
			new HashedString("3d62ee7e9bada438b991f23890747534",9), //nanoav.ru
			new HashedString("3dfef91e52b19e8bc2b5c88bdf9d7a52",20), //update.microsoft.com
			new HashedString("3dfef91e52b19e8bc2b5c88bdf9d7a52",20), //update.microsoft.com
			new HashedString("4098c777fa8b87f90df7492fd361d54d",9), //vmray.com
			new HashedString("40ef01d37461ab4affb0fdc88462aba9",27), //ntservicepack.microsoft.com
			new HashedString("41115f938d9471e588c43523ba7fb360",10), //vellisa.ru
			new HashedString("41d4831c0d31069bc5b8ac767612316f",17), //scanner.virus.org
			new HashedString("4360f8ffd51b17b8bc94745c4a26ef2c",13), //cyberforum.ru
			new HashedString("44d93a0928689480852de2b3d913a0bf",7), //eset.ua
			new HashedString("460049e8266ca5270cf042506cc2e8eb",16), //forum.oszone.net
			new HashedString("475263d0cb67da5ec1dae1ee7a40a114",13), //hitmanpro.com
			new HashedString("475263d0cb67da5ec1dae1ee7a40a114",13), //hitmanpro.com
			new HashedString("47a7fa72bb79489946e964d547b9a70c",9), //add0n.com
			new HashedString("4876e625e899a84454d98f6322a4d213",15), //cloud.iobit.com
			new HashedString("4a73bdc9cec00bbb9f05bc79cbc130b4",9), //mzrst.com
			new HashedString("4bb1cae5c94216ccc7e666d60db2fa40",12), //kaspersky.ru
			new HashedString("4c255dbc36416840ad9be3d9745b2b16",15), //programy.com.ua
			new HashedString("4e42a4a95cf99a3d088efba6f84068c4",10), //norton.com
			new HashedString("4e5d2e4478cbf65b4411dd6df56c85b7",10), //arcabit.pl
			new HashedString("4f8a9bbdec4e2de5f6af2d8375f78b47",41), //malwarebytes-anti-malware.ru.uptodown.com
			new HashedString("50c1347f91a9ccaa37f3661e331b376d",15), //herdprotect.com
			new HashedString("545f4178fd14d0a0fdacc18b68ac6a59",18), //regist.safezone.cc
			new HashedString("54b260c7fb614cfcf0d2f6e983434db8",15), //k7computing.com
			new HashedString("5641840b2116c66124c1b59a15f32189",15), //spamfighter.com
			new HashedString("56e323a7ffcf8f40321ec950c1c3860f",15), //estsecurity.com
			new HashedString("56f2deb0bf3c2ac9aa9de23ee968654f",10), //clamav.net
			new HashedString("5bfe94657da859c24293b4e35810ee29",26), //securitycloud.symantec.com
			new HashedString("5c6cfe5d644fb02b0e1a6ac13172ae6e",8), //bkav.com
			new HashedString("5ca9e4a942e008184f0656dc403485b7",7), //any.run
			new HashedString("5fb3419335f5e5131ab3fc22d06ad195",20), //support.kaspersky.ru
			new HashedString("60d2f4fe0275d790764f40abc6734499",9), //baidu.com
			new HashedString("61138c8874db6a74253f3e6472c73c24",27), //windowsupdate.microsoft.com
			new HashedString("61138c8874db6a74253f3e6472c73c24",27), //windowsupdate.microsoft.com
			new HashedString("61cfcb40977412be2ebf5450f4e47d30",11), //totalav.com
			new HashedString("61d4dd297f749e3291ed8ae744da57de",20), //paloaltonetworks.com
			new HashedString("626575b255ca41a9b3e7e38b229e49c7",11), //safezone.ua
			new HashedString("62cf04eba08e65b210bd1308f9da04bf",9), //iobit.com
			new HashedString("6319434ad50ad9ec528bc21a6b2e9694",13), //193.228.54.23
			new HashedString("63b4a8681bf273da7096261abcb33657",10), //opswat.com
			new HashedString("64003943175e5f080c849f1744819f48",16), //totaladblock.com
			new HashedString("675c52a56f2ff1b3a689c278778f149c",21), //kaspersky-security.ru
			new HashedString("680bd6136c83f4eb31b16c1fdd7aa93b",17), //reversinglabs.com
			new HashedString("683ca3c4043fb12d3bb49c2470a087ea",26), //download.windowsupdate.com
			new HashedString("683ca3c4043fb12d3bb49c2470a087ea",26), //download.windowsupdate.com
			new HashedString("686f4ba84015e8950f4aed794934ed11",10), //zillya.com
			new HashedString("6c1e4b893bda58da0e9ef2d6d85ac34f",18), //wustat.windows.com
			new HashedString("6c366a99be85761e88558f342a61b2c4",12), //securrity.ru
			new HashedString("6cbd967e469ea6671e3697f53f577e59",12), //remontka.pro
			new HashedString("6ce238acdd804c4f2c710c58efe089fe",12), //emsisoft.com
			new HashedString("6d134d427dd6cc0ac506d895e06e5bfa",14), //blog-bridge.ru
			new HashedString("6dcb7e266b7f70c55d8ad51ef995cbc9",10), //kerish.org
			new HashedString("6e7bf33d4e222ddb5ae026d0cd07754a",10), //krutor.org
			new HashedString("6f0c9e8027ef9720f9caedaef4e200b5",13), //kaspersky.com
			new HashedString("6f0c9e8027ef9720f9caedaef4e200b5",13), //kaspersky.com
			new HashedString("70d0c097b0771196529f00b1559fa78f",18), //ikarussecurity.com
			new HashedString("725161e698d806fcce316bcd70b2fce1",17), //rising-global.com
			new HashedString("762c7e2ec87cb7de793cde9e9543734a",10), //lionic.com
			new HashedString("771170bbbfd44a8b1843d3fad96daf1b",11), //pcmatic.com
			new HashedString("774f38701dff27e1d5083998b428efd6",11), //tehtris.com
			new HashedString("782d9e9abc2de8a1a9fdd5f4e41bc977",11), //dropbox.com
			new HashedString("78e02266c69940f32b680bd1407f7cfd",26), //free.dataprotection.com.ua
			new HashedString("79782f8d4349fc66dad89c3765b761d3",23), //metadefender.opswat.com
			new HashedString("7c07ca598d80ba314295db647b40bc16",14), //securos.org.ua
			new HashedString("7d2500fc0c1b67428aac870cad7e5834",12), //vms.drweb.ru
			new HashedString("804669ae15f338250ec9e3bd00ef5038",16), //totaldefense.com
			new HashedString("80d01ead54a1384e56f5d34c80b33575",13), //zonealarm.com
			new HashedString("8202ec5cbdc1e645fab61b419c328300",11), //adguard.com
			new HashedString("820c5a952f7877246c895c5253017642",15), //softlist.com.ua
			new HashedString("83b6a29ee489bf3e976824b763c212e9",14), //virusinfo.info
			new HashedString("84b419681661cc59155b795e0ca7edf9",20), //download-software.ru
			new HashedString("84eac61e5ebc87c23550d11bce7cab5d",17), //novirusthanks.org
			new HashedString("861cd2c94ae7af5a4534abc999d9169f",13), //stopzilla.com
			new HashedString("867692a785fd911f6ee022bc146bf28c",12), //f-secure.com
			new HashedString("87a25244757ea3a30d936b1a9f4adb93",15), //sentinelone.com
			new HashedString("8854c43b5f132f9bbe9aa01e034e47fd",14), //remontcompa.ru
			new HashedString("8931a8fa06b940d45d6a28f2224bc46a",10), //elastic.co
			new HashedString("8d39a2f3831595b02640c90888c21fdd",17), //pandasecurity.com
			new HashedString("8dde0f8215149ce5ecfd670c4a701a9b",9), //pro32.com
			new HashedString("90711c695c197049eb736afec84e9ff4",20), //superantispyware.com
			new HashedString("927846aba9d1dfedf55ef604067e3397",7), //eset.ru
			new HashedString("974bf1d93d81d915800bb2e5352b923e",39), //msnbot-65-52-108-33.search.msn.comments
			new HashedString("976e17b152cabf43472b3ffd81113c66",13), //trustlook.com
			new HashedString("985983ba88d92782fc97526ab0f02cd0",10), //mcafee.com
			new HashedString("98d3a8a27234fa519e04907d7ace9ff1",8), //drweb.ru
			new HashedString("98eb7e27e19b8816b5ec0a8beffd30aa",20), //cmccybersecurity.com
			new HashedString("98fc92e32c31aa34dfefa97494381324",9), //render.ru
			new HashedString("99cd2175108d157588c04758296d1cfc",10), //github.com
			new HashedString("9a397c822a900606c2eb4b42c353499f",10), //z-oleg.com
			new HashedString("9bfeda9d06879971756e549d5edb6acd",20), //free-software.com.ua
			new HashedString("9c41eb8b8cd2c93c2782ead39aa4fb70",9), //vipre.com
			new HashedString("9fc0b7fa45ef58abd160a353e2d9eb27",15), //home.sophos.com
			new HashedString("a0f591c108d182f52a406fb1329c9322",14), //softportal.com
			new HashedString("a2883d9faa219af692c35404e8c5c05a",19), //codeload.github.com
			new HashedString("a349df20a84c064b688c3605d60dd00e",15), //crowdstrike.com
			new HashedString("a48072f23988b560b72cf3f2f0eccc30",26), //hitman-pro.ru.uptodown.com
			new HashedString("a518658356c72fd843116c6358393690",14), //cybereason.com
			new HashedString("a65eb4af101a55b3e844dc9ccc42f2ff",11), //dpbolvw.net
			new HashedString("a6891c5c195728b0c75bb10a9d3660db",10), //blog-pc.ru
			new HashedString("a6f9bdbd2ced0eba0fe2eb3c98c37778",7), //eset.kz
			new HashedString("a71c27fdffca5d79cf721528e221d25a",15), //soft.oszone.net
			new HashedString("af0bbbc42533596b884c3b6edcdd97c9",10), //raymond.cc
			new HashedString("b0655a2541be60f6b00841fdcba1a2df",10), //nashnet.ua
			new HashedString("b06cce9c842342a517eeb979550cb7ef",11), //it-doc.info
			new HashedString("b2c9a135e92a3d4d0bded64ffe4d1ee3",15), //maxpcsecure.com
			new HashedString("b4de3925f3057e88a76809a1cf25abe5",15), //drweb-cureit.ru
			new HashedString("b56ffe783724d331b052305b9cef2359",24), //unhackme.ru.uptodown.com
			new HashedString("b6eb1940800729f89307db6162706c21",19), //virusscan.jotti.org
			new HashedString("b868b32c3ea132d50bd673545e3f3403",18), //zonerantivirus.com
			new HashedString("b8d20b5201f66f17af21dc966c1e15f8",13), //free.drweb.ru
			new HashedString("b8f3ad2ce16be91986c6ae6c6d2f5c21",13), //bullguard.com
			new HashedString("bcc2393101a857b00a4fbff01da66f2a",12), //bullguard.ru
			new HashedString("bd25a074d01c2eeb74d8563a09f9ebf6",12), //cezurity.com
			new HashedString("bd7c714d46ff9bae1bd9918476e8450c",10), //malware.lu
			new HashedString("bdef1f72c100741f5c13286c709402fb",14), //grizzly-pro.ru
			new HashedString("be56cb5de3fd03b65b161145349ae105",13), //AlpineFile.ru
			new HashedString("c46cfad9e681cd63c8559ca9ba0c87ce",17), //gdatasoftware.com
			new HashedString("c593eabe657120a14c5296bad07ba127",11), //app.any.run
			new HashedString("c652b5220b32e0302487d6bcdc232c9d",9), //cynet.com
			new HashedString("c8324a9e380379bd3e560c4a792f76de",13), //scanguard.com
			new HashedString("c98e096681a2d1d30b321ca4682adb47",12), //trapmine.com
			new HashedString("ca867bc71a7ba4529a2d3a9991d54511",9), //tgsoft.it
			new HashedString("cadddd7e2aee1db1c03f630a22f322d9",13), //chomar.com.tr
			new HashedString("cb25bfbf5c7435fd7aeda5b62dd29af5",12), //fortinet.com
			new HashedString("cde54506e8fa4d94c347eb3bf1a4e761",11), //quttera.com
			new HashedString("d36f9acef58b77c1499fb31b05e1348f",12), //broadcom.com
			new HashedString("d58a810afab3591cf1450a8197219cc4",11), //tencent.com
            new HashedString("d82e179187d1268339dcc5fa62fa8b1c",14), //api.github.com
			new HashedString("d96d3881c78c18b33f00d3e366db2714",11), //antiscan.me
			new HashedString("da2ca8ed062a8b78340292df861754b0",17), //company.hauri.net
			new HashedString("da876e79f6730f35c4678969c5b01b3f",12), //pc-helpp.com
			new HashedString("daa0a654ae3dd4043c4aab6205a613dc",10), //ahnlab.com
			new HashedString("dab7894721da916ee815d3d750db2c33",11), //greatis.com
			new HashedString("de7e2990f9560ce7681d2d704c754169",8), //drweb.ua
			new HashedString("e00662fd56d5e0788bde888b0f2cac70",7), //avg.com
			new HashedString("e075a44b048b9039c8b3dce7627237ae",11), //escanav.com
			new HashedString("e1312360d9da76cde574fdf39ff4ec60",9), //vgrom.com
			new HashedString("e159fc485c9c5e905cb570e5a4af489a",10), //intego.com
			new HashedString("e2a50e6c79e09a7356e07d0476dfbb9b",14), //virustotal.com
			new HashedString("e2f0354cd055ee727d5359ceb3ec59ad",16), //malwarebytes.com
			new HashedString("e56f530f736bcb360515f71ab7b0a391",14), //spyware-ru.com
			new HashedString("e752141e6b76cf60e0bf9f850654d46b",12), //soft-file.ru
			new HashedString("e7d02464efe5027b4fe29e5b71bff851",12), //ashampoo.com
			new HashedString("e862d898315ed4b4a49deede1f672fde",13), //surfshark.com
			new HashedString("ea2afd439110302922a66cfb1c20c71d",11), //acronis.com
			new HashedString("eb401ae50e38bdf97bf98eb67b7f9764",14), //blackberry.com
			new HashedString("ebc7dba99115781ed43090a07f9281ab",14), //ru.vessoft.com
			new HashedString("ec532f0313071cb7d33bf21781ec751f",10), //sophos.com
			new HashedString("ee35efa79cb52086ce2eb70ba69b8405",17), //download.cnet.com
			new HashedString("eed8bfd826da59536da141d8773a2781",19), //hybrid-analysis.com
			new HashedString("eeded1a700eaa95a14fccb1d0b710d76",11), //unhackme.ru
			new HashedString("ef628e261e007380ba780ddca4bf7510",13), //softobase.com
			new HashedString("f039b199813ed30f7ce8ecea353ceffc",9), //cyren.com
			new HashedString("f27e6596102c70bad8aa36e7c9b50340",11), //virscan.org
			new HashedString("f3226bd720850e4b8115efc39c2b0fe9",9), //avira.com
			new HashedString("f360d4a971574eca32732b1f2b55f437",11), //xcitium.com
			new HashedString("f5fe102ec904aad2e20b80dcf40ae54b",8), //avast.ua
			new HashedString("f6ce7e3db235723091e59a653e7d96f2",9), //mywot.com
			new HashedString("f92bfb8ff6ac7e99a799f6017797684b",13), //quickheal.com
			new HashedString("fc828fa4ff498f2738556e6c446bb98a",18), //site.anti-virus.by
			new HashedString("fca37d5298253d278429075543d8f47d",24), //unhackme.en.softonic.com
			new HashedString("ff5c054c7cd6924c570f944007ccf076",13), //microsoft.com
		};

		public List<string> badSubkeys = new List<string>()
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

		public string[] whiteListedWords = new string[] {
		Bfs.Create("i4OHDJjliBgyzHzRmNC7Hw==","Bgo07TUmrovh/lf7w4fZ3WGccZBa7n0hsy7c5OnZwF8=", "6jObAWKkqerSXxXB6iL+7g=="), //ads
		Bfs.Create("YX/bEfp98VL/BlkH1AG03A==","IaIfFHNn+4lbnYIVMX4wBFmkB9pH1iwXzsyfAE2b5L8=", "P3KyaojIibD+JscUD+WX8w=="), //msn
		Bfs.Create("Yt2pb0PLahNix8HoQ6QqiA==","PzjLeakR7kbpwUC9hTKAVratpuTIA/utcAHkBPctHmY=", "IaeifCXyXnd2le7WP1vz0Q=="), //ztd.
		Bfs.Create("neCjzF1iUTY8198ayw3chw==","bYeitz6lBKPKdysKJSOest5Vrzs0XJuXQG76Opk7jL4=", "hsoiqMnc3fkJPnBX1vodXA=="), //aria
		Bfs.Create("x0xmq9E4vy/raLcDMSIofA==","EoGg6HtiMles5Vy2iz5JXv3ggNMOJ7csC+lW6gFwDs0=", "wjicIJvSNKMqA5PcCdgteg=="), //blob
		Bfs.Create("6vU20xayKitdWETY4OU2fA==","cciS+N6etbE/LQDgPGIRvLNBqdzhgTikgKey9yhio+o=", "LzsvJySOWVRs0aRUmHlKzw=="), //llnw
		Bfs.Create("EJiS8tNs3d/FGugCkjzqYQ==","xWHO9Z8Tv14ceuMwP3dXYxFql7PBkETkzGfhESbtkV8=", "QhGZAd/SlDlQ3mQ8ahJDZw=="), //ipv6
		Bfs.Create("B2Txm9p+UIsXW/BavUK6rw==","1Vzb8DQKkt6r7YQL3ILYOBx5n9//ZGGdEUu56XniEss=", "kP/jDDqIDOA6Le91uLp2dA=="), //.sts.
		Bfs.Create("SEGpej4H1g/ev4GGz05icQ==","NqAP6PmQ/rAxCD6iC+rHfvP7uyiW4K1BU7ZfOVrj1uA=", "deRrTDff74RRUCb6dnhnGA=="), //.dds.
		Bfs.Create("UULCxeCq9mTCOHZ+z/8Csg==","cuS378bqZnYxFoWw0H/mIQQiF78QCmN6glCApHU+seQ=", "W4AJ0+UAfHogOiq5djEdIQ=="), //adnxs
		Bfs.Create("kmD5B0wuVLBCROSui4SLpQ==","UyxjwifpvreZxHEb2zDR7xrlu8+0nBei7Gh12b6dI/g=", "edZOrgHrxggyh8HtwqqPnA=="), //akadns
		Bfs.Create("Zl0DSUSePCdUxh0JQkEXaQ==","R7yDytrILrjiTyAZ6mKzzBovdjj1QkAZx3M0RbIlMEo=", "8bLTvVGPz0m1QzXhjWbUiQ=="), //vortex
		Bfs.Create("oX2242VPMYOcvCzqEzTvXA==","M2KHUDwFSy0Nfrws1AORgrSmTUwFkNi1H6A7RFC09KQ=", "xUcqZ8tSbUme6xbhh5xnTg=="), //spynet
		Bfs.Create("0BeIwfDSqTO5d1apdnL2wQ==","nApp+U8o3URYmAUN9yi1w0IYWjix/9aO8mqxqSn41ko=", "bMHBZ1bxUceOxY7rpK9t9A=="), //watson
		Bfs.Create("xb8+yTRX8vB/hFMdqVN30Q==","lwjkIRj4MeU+iWtjUUuwfMRYOeMcg2UjXY6IglexLbU=", "4GVuv+BMlsMT11DFqapxvQ=="), //redir.
		Bfs.Create("AKKL3eqivhLH8AEfwm6njw==","FBRpZMiuhVDTM78UrWF70m0qO6JM9VmIENX/y2/nlDY=", "q0BGdQ1/zuTcv3ghQcJbNg=="), //.cdpcs.
		Bfs.Create("KvO2p928lEc/SU+wqJyNkw==","g2JxQfdROcB/gXU/eO+SRpk9MrOMCGt73Cl6rq99ZRQ=", "p26M7+vDK6OyvM5H11RW2A=="), //windows
		Bfs.Create("rYC0MmjZsVwl3hukGceERw==","qURKyXGI2+PH4zTkqfvnh7PBz4jp/xQlEGkg6VaemDA=", "BePeUL6/KBeOGlao81KuYQ=="), //corpext
		Bfs.Create("AlJ/HfykiiZ/2aPi0MD5+g==","o7yIwMCa+m4tKbuiAPLHrROO0hoo3MQfGTrRRP1v/Tc=", "3ibu2UbyIwHZQKAB5c5i7w=="), //romeccs
		Bfs.Create("yJ3XNisA3KVJp8AsgeT/0Q==","KTuILiuPh5dRJyy2pRDMO88HMil5eucgbjKSoyKkpV4=", "nIPOBswrOGwRtSwUMZDAZg=="), //settings
		Bfs.Create("UUteNfViejV3O1jITkQXqw==","gQ/bVUE9NL/HiC+5UPiYbWwDIRFEr7Gk6lBmzuKcYnA=", "lVFXEWTe5uJyPv2xyQ8DlQ=="), //telemetry
		Bfs.Create("Copz3FkEDtdqwCzVlJPs4g==","Ak35w8knEq/UN6CAEqEyO9p4CmZfugatvtdKRuc+kac=", "sKf33bRjYKHQU86XRq8w8w=="), //edgeoffer
		Bfs.Create("0cIZVqi5Fz+K554WaUl3OA==","N+NjjCdpfG9O1B64vUvywLDJbbKvLtuGyiSHBMxn+34=", "Ts/b/MASk5lGJXcfWZ+nPQ=="), //do.dsp.mp
		Bfs.Create("gM50qHE8uY8yTb/JZiv9BQ==","Q3HGDGoSUeuE9/T68ZYMMnbptkzfsPuA3kzmt5x4AMg=", "p002rNhuYHyuC8AX14+b5A=="), //ieonlinews
		Bfs.Create("T8aOLmP5FH1YTGvbsNkS3g==","j0n1XSXgHz1uSDcdNwoZlOQQ10rimWJQjBWW6k0TdU8=", "K0GKu2rKUPl/EOTDF53D3w=="), //diagnostics
		Bfs.Create("siNBk3oeP5xyCfO/Yc699g==","IwxYYG4xGIGZzWJcMNKPk45DZvEtEVJ31vjmdNPjdOc=", "tXNV+dK6SPqcx0YQ2z3QFA=="), //.smartscreen.
		Bfs.Create("YBx9Lnab/34VsiZpZuPDOw==","3fVauxYyWWFuc8r/6Qbes8j6ytmhizSUNA2Px1mexnk=", "8DeLoX1N2GvLdN0y80suqQ=="), //.metaservices.
		};

        public List<string> obfStr1 = new List<string>() {
			Drive.Letter + Bfs.Create("5lMaqgA0ARQchz5bfG5VVubt0VIB3I9QPYyLAyO0p/g=","pRl8UOgtuHYGNmQZb9W3VIc5re48guuuNgdENm3uErg=", "LpAp/rFE7lG0/dJi93cEoQ=="), //:\ProgramData\Install
			Drive.Letter + Bfs.Create("9LAabNQKATpWY8GP2+iPPeF2G5a+THNnv3VcZulQ5A0=","7DUK+zHwmMLhPXsOOI4nQA0MC+w0gFRLNaZvbXB3v1U=", "YqQFVukfhXeQlFpJ69Znuw=="), //:\ProgramData\Microsoft\Check
			Drive.Letter + Bfs.Create("1K7MULX0VWOXjakEJ/UYjt/dmlS1tYAVJg++7blM8t4=","c4jYPpY94E3lTyWSOQjVCdG7/XyTijAB3T+gWj/6hNg=", "sc/eYt9JcypZawRDMq3uRA=="), //:\ProgramData\Microsoft\Intel
			Drive.Letter + Bfs.Create("YAOdx2FTfCnZLOoXygEfxVkDH5FTFipF8bOhsZvcNi2V6IAkAXF7twa/N8lhXWDsOkhkhu7SATNNW+7ANgO/Zw==","LZq2jtnuvwM71YEpBgRXNQIA/UkG9iMUfL2NChSUKo8=", "nWLOKUgy85SlI0rUzd0rSA=="), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
			Drive.Letter + Bfs.Create("qDbAdezNqIu8psjJmhI7rYwbyxZIs8wkhrUNTH1PswU=","4wiCZrdCxYGP2fjv1b5wwgZ7bREO1r/gdQDHWEq2Ucs=", "4Pgu0GOfnyZNqtgOEQB4SQ=="), //:\ProgramData\Microsoft\temp
			Drive.Letter + Bfs.Create("dP96k1moTdytXCU6OyCdO6d3cAp0wCfwXeCFBEM+X9g=","jf54/qH6wibDF48zqtqzsGVbe/ZJNAspJPLk1uSMmWk=", "Tutq6ZpRklWFLSYc2TSxdw=="), //:\ProgramData\PuzzleMedia
			Drive.Letter + Bfs.Create("E+t2DqPgB85cY7EdcEaiFeaFh0xF/OiaWWN/OJ4CgiY=","CVYN1d5lhA654IOzOv2mXljdN2uyIuE6nj/8ZjJban0=", "2hdr2kVJhJ9MI2ttVxAggw=="), //:\ProgramData\RealtekHD
			Drive.Letter + Bfs.Create("UBbqv0pL/o7x7xuGIKwr6OoOOOUtnmsV1meRgKXLTXE=","EV09vseqc8z4a9dFhQoUnfDo8TwX326g4lQuCG0zNIw=", "1mM451visfCmVqFMIATkGA=="), //:\ProgramData\ReaItekHD
			Drive.Letter + Bfs.Create("Da7BhgxCsIHcAqADUsYSUF0HYMXY/JZrXTIjWx2Cdxs=","eQ4WlfRlVlHquem3QXZXzbsZh+WYFkbTChw3IqZjRqI=", "dK728ID10qzhP9uVpYEL4w=="), //:\ProgramData\RobotDemo
			Drive.Letter + Bfs.Create("Ia1AReuZdn+3DGdzKMWEt0FXXF6JGyJeA/cluZ/vO/s=","nrjaP8qMyzmCEbDjbPzk2Us6GPF1EJ+F7jrZYSJg0+A=", "bMa5HDkNxfqGqjeThgLXTQ=="), //:\ProgramData\RunDLL
			Drive.Letter + Bfs.Create("PAG0+UiSXIzgNQAwQHswmwkPrSegpbCst46YEoaCVVM=","C1FwD1Jj18lj3v/IxVOOA0FxcVSrMxkDMRUPOpfc4/k=", "FL7pQN4pty4NzqS3dZWawQ=="), //:\ProgramData\Setup
			Drive.Letter + Bfs.Create("703MkdFmcz04vBf/VeCR44liLNziHsIqw8s6N0g0nLY=","EGgHEnafpZ9Wzys0t1DNeceUjbbZoLUfXz+w302XCXA=", "4Hu2BKmgvhVggCflnsQ1OA=="), //:\ProgramData\System32
			Drive.Letter + Bfs.Create("SQNRtscxaNRThOUzYVT1wqLR83V5IJuxmSaN/SE0pt0=","HB9DUFT+ldK7wOWbDQPr3R6XRAskPCRPQWB+8fGyh/M=", "G87Pve01cTZ5RIYSKNDtdQ=="), //:\ProgramData\WavePad
			Drive.Letter + Bfs.Create("y7/L4Z6dzTFYTmOGnwWidYPyX2nuts3UrFuqHmqY1sHC2S9bqDTmkvvIQTCkuUev","eds0AYsLyNnstFwyXU/aG0qYeOLWYYnjqIk7i36D4L8=", "WlRGahQxd81OXMjSa+AQxg=="), //:\ProgramData\Windows Tasks Service
			Drive.Letter + Bfs.Create("X6FhPDA3a8U53lS0lxWU9xFEB6U03ph0S0t0cSHhbp0=","DfptKOQMiT0vf3s3kijT0ArfAWnvVoLfT62z1TRAVm0=", "FiMdoA/MGbItuQF9JVhvfw=="), //:\ProgramData\WindowsTask
			Drive.Letter + Bfs.Create("2+9ggVMU2IUQo+lM9b5pSS81UN4t/E0nll76Z+gQ7cI=","+/vrZXUvDyKr1XFuKLmwR+VL48YSVh/InKpaoY5atok=", "iPYHkpT5GYk4IC5eFKuoGQ=="), //:\ProgramData\Google\Chrome
			Drive.Letter + Bfs.Create("JqhOKKGJM5xs7p0Yns7NPZv5aY7MGxej5+5EHi2LhUo0kJtBGCEfyTzfTU8WZUki","Sk/Krh5G266THH+v4mQUXyeOQpwpxZULcz+CNxrxFo0=", "EY7E2XHxMUME3nDtAi//YA=="), //:\ProgramData\DiagnosisSync\current
			Drive.Letter + Bfs.Create("hQfJmgxOEYGwSgvYokONuiMgjWVRkhtZ6oRGOMIG8VQ=","7Hs5Iy5J9xQxNVmWFc76O77wzEtqDdrOPnlevOe54mM=", "kdTTLH7Nmdq24o/Qi3bH7Q=="), //:\Program Files\Google\Libs
			Drive.Letter + Bfs.Create("W1FlAMov9ZQg4rUqJd4vt1Byt8umfbZvfertR8GZLqA=","Xz57oz+snTK9OXfrhed8dWYgrF2cq/o6FIsHfIdFK5k=", "TnSUysj6nwfmxRT26TICAg=="), //:\Windows\Fonts\Mysql
			Drive.Letter + Bfs.Create("oVKpu4MUFMCwP6wbClNeqm7YQ+1LryaDSX7squxVwiMqfk2qIPxdqx5Hq4sgIHuw","WpCiIOc1onXXuqszxSG17T7ndoPtCDiQ4+ttRBdV0WQ=", "+A/fVWGbm+/J195SPkMp7g=="), //:\Program Files\Internet Explorer\bin
			Drive.Letter + Bfs.Create("DwQzfIK0NPHRyhffKznst9we1EPqXY9FZ6QRb+bF8po=","EE529R1gR37sHPLuozT+cTNy8rKnaoJdIdl4v6Wls2U=", "sF3JufLnX5It64RMtQc1lA=="), //:\ProgramData\princeton-produce
			Drive.Letter + Bfs.Create("+Fthczfi5eIYM8ucWcFOcZGbvfxpBhXsJYnzswJy9Zs=","VoWBsp0gj639h0mFyhEMNn1D+lArT9ggOFWWvTVoWPU=", "TDRnG07yEKXCxgLcmJNLgg=="), //:\ProgramData\Timeupper
			Drive.Letter + Bfs.Create("4xPenV7DOKeP9+TZ66WSKfaAanS8k6TD6G7svi5nBIw=","TVFB7zVaS4r8ptqsnkJqYAYdSywSBesvckC1CdUQJrs=", "6kbGXVlsJLx9/NBS52aeOA=="), //:\Program Files\RDP Wrapper
			Drive.Letter + Bfs.Create("uhJ4SRMWURK1OxhzZ7oQYSgrNLpGqw7STStX7iPHSFw=","UehaZ9R+kBGUQAiacxNby+7hKB/LPtZ0C4UwEDN00UY=", "gSYrVxXPVoGku46htekaPQ=="), //:\Program Files\Client Helper
			Drive.Letter + Bfs.Create("T+u6N1xtmAwlbdJI6AiA72rNVG9ZrE76MHONCHk/la0=","g8MT7pDQNmIMcZe4TDkNAjLrBprt15c6iQgvAJ7DPVQ=", "FzW/4ybxlrta85sfkB6xow=="), //:\Program Files\qBittorrentPro
			@"\\?\" + Drive.Letter + Bfs.Create("4pY8UC8pLH8fdNaxEd35hiXNcfEHAla4xhP+5PUXr0s=","KrKxaOTmZWQKFFcX/ccBg6TMPmj5Nqxs5XcsfalL02Q=", "ES/ZqjLD5PyVuDZUoZud3g=="), //:\ProgramData\AUX..
			@"\\?\" + Drive.Letter + Bfs.Create("TERsWp29opd9eppyNLSuu2WkjI1giNwDGTOAeFFcBZg=","jYg/1OgYxknxwqBpzPnqbO81rw+Fn3G8U2yCRKBRdOA=", "THW4YH9w8HM9pzygMhAjeQ=="), //:\ProgramData\NUL..
			Drive.Letter + Bfs.Create("QzpkmXm8aQz/O/2V1vzr7JpDnhf4EOqbQhR4hy0xKd8=","7DaVEa/3uB1n+BeXC+S+BMLqXwyaoHMyuJ2UswzXDqM=", "QClZGgWtXRgYbBsy/momcw=="), //:\ProgramData\Jedist
			Drive.Letter + Bfs.Create("h9bw1Raxaayj5MAmRItM37rhVXTdnAAmj9sXkSvwsNFDbsGdKXaoiQwCs5v007u6UdB9KYG8qFpflhSJSokdKQ==","aKWyHPJp0KPMl97xbCqMwKgBZHlybUlPQQAyhCBZyJI=", "9UzKenmewAt165r6BdYvJg=="), //:\ProgramData\Classic.{BB06C0E4-D293-4f75-8A90-CB05B6477EEE}
			Drive.Letter + Bfs.Create("gAR7oKAU8eiHpbD5OpDFsStVK2fN6NbfDjzghqiP+TFQAi9cPQnYJyoUZnidbfP+KDC1mOpJacLoW3TGpiqx1g==","PkYxRhbKE672T0yBvXUZtszQ/gTJXYdeJF4RJRG3G0s=", "ofADIEvZVeSZJVLTr7huaw=="), //:\ProgramData\Classic.{BB64F8A7-BEE7-4E1A-AB8D-7D8273F7FDB6}
			Drive.Letter + Bfs.Create("e8BT41jzpMhYOHqeS1CCnU+Dno7mjy47AJ+Aox5PvQU=","4HcV94wvTjw78bmLzkXd6m4tBvdlpKsFL25BBCDXWPE=", "e+W/63+HU8ooqNvAPe/Shw=="), //:\ProgramData\Gedist
			Drive.Letter + Bfs.Create("TtxIHdDDfiwTPH+Fd1xF9rz2s8xrPEp4JCboxVhyG+0=","4nf/Knw/1NkCtnrz5xkWkDL4zEaItr7Q1e2yyS2Ze04=", "wxglZzpAsA/oFZKkl1HPCQ=="), //:\ProgramData\Vedist
			Drive.Letter + Bfs.Create("z69BnGr9UJ4BLhHPLl6BgJiDFqHnZzHhyOIgyq/2RIg=","xtAnBOZC0CKysa2RrReqTGg5/cMX+mBHsWlvCJrEXgY=", "NFTpNJCnDA+gsw/oiQK4mg=="), //:\ProgramData\WindowsDefender
			Drive.Letter + Bfs.Create("gMeKHi9teIeE2E/sjGSyxAFHTeVbWCqmYrFiU9frnzk=","4DMuAKcRF1JL8gqwE2khG73zTtZ6+/HEVb+oIEa0s4A=", "rxslzRP7O7PEV0DvFHzM8A=="), //:\ProgramData\WindowsServices
			Drive.Letter + Bfs.Create("MnWTKGh+qKhoKdH3VCOQoV3DdV49F9CUNL1ItRJQkxBt9zSWCTKdSjF7kFPJpcLH","vv12sLIi62VNZkdu09YmoZOUcDV7WcxDKOCNkhv/vF8=", "/wr3X70G955Lqe2wHB54Vw=="), //:\Users\Public\Libraries\AMD\opencl
			Drive.Letter + Bfs.Create("tlGn/JLHjYQuU+Jy9SEkp8SzTceEQ99ef+flNtTHbz5jiNvxSrBMSmKQ/GF+R65B","g5/FD3ccto8dMoISnPxcH9ZK1rtwIO7oA3YTA+IeshQ=", "Whf2O2/vwTKpr07eaSBb2g=="), //:\Users\Public\Libraries\directx
			Drive.Letter + Bfs.Create("tM3hhonTw/eolepE0FBgodhUJv+RdBpcYwsl+mTtXFg=","L4X8c8am15SZAozSdlpigXFCNle8HOb3PP3CWcIFzlA=", "HL8n/qlTSu4cvU2U7p6FDQ=="), //:\ProgramData\DirectX\graphics
		};

        public List<string> obfStr2 = new List<string>() {
			Drive.Letter + Bfs.Create("o46NdQQJ0B7xYJAqC3YoCBn0diWBta5u5wIrP2Y0Ke8=","YNt5bV2dvVo8SoEf+lRmGpfWe2cE+SCSBL+MExjyC4Q=", "w8I6tggdYlYLS+HghsnKOQ=="), //:\ProgramData\Microsoft\win.exe
			Drive.Letter + Bfs.Create("vNzArH2IhAZRhGNskjKCE4dGW8COug/sbvBbrXM+vGFqSKtnud/kUVmHKQrSYztM","iiG9POweXVVd9ffHIPvh2Jl11rR9s4giq/QyxTbpZLY=", "HXWeD6YHHg7rjDHYZTahSg=="), //:\Program Files\Google\Chrome\updater.exe
			Drive.Letter + Bfs.Create("tQ9oIUfrxxR++xw/Ik6Nj0U0ayAPql/88Y6eWDg+Fs2MuOiC9F0YbV7yqgvP0uwZ","HappoB7O6IZ2JO/PAUNIW6o3ToyEmH+dklojwYX+woc=", "sExAK6yCXLugpIzDEcuJDw=="), //:\Program Files\Client Helper\Client Helper.exe
			Drive.Letter + Bfs.Create("QpgV4lnCw2QVgCvSHVlZKAErmbuYYGNvkAjn21R9JbS5EkDWLKup8TXFAm9m5Y3XzW9khAbQrXEimo6/lZITuQ==","HQ2aaOZw60naqh1CmJZ5fFoY3ga3mBd2rXsMmIacxHQ=", "OABPWA8KWiYZEOum4JM1jQ=="), //:\Program Files\qBittorrentPro\qBittorrentPro.exe
			Drive.Letter + Bfs.Create("h+6CYVUgqPxizhFkt6eX/jCTrpxRsr962zt9Q6hQsaWrDdKctMD9rwktJN8rpUGkYgCeqtqTQmzwS79wKghGew==","xOuaXAf7IvGXCZB5TrriGb7SsKURoJQgLZElq3wh034=", "6t+JXp+DOPuivvYOawL2Ag=="), //:\Program Files\Microsoft\Spelling\en-US\default.exe
			Drive.Letter + Bfs.Create("pOziG33WFAcriE9rXyi4tgzN8TNNymwExi4VXRAUtBb1gr30P4y0JKbgp9Bli5Od","9a6NjFJKwhkrl7FOJywgeoqk/6MrxYRYb6WeGq0beeE=", "8f54rEfa6yNccjRJ3WnxJg=="), //:\ProgramData\Google\Chrome\updater.exe
			Drive.Letter + Bfs.Create("eivmmvHq1Cl37DjWKYG86r4R30XpE1VLssYdyHNUoq/2duigGZ+DiqeXD0dH5C/y","yPd8+EfQ51Qh53mtAw/MJyyQPeIVYCD6ZBD6GwnRib8=", "Qj60LqvT+o8TQR1ph55HAw=="), //:\ProgramData\Google\Chrome\SbieDll.Dll
			Drive.Letter + Bfs.Create("IP4P7ijcqrB/sDs3gA1hW9IsQOS0IDe9RuGxNl5zMYU=","K2DXC1j7h7EnZi4Lrfu2Zxoia06dMS/z1riwmnZ/nnU=", "wJniWJ0sb8Ppy16Tbt2Fpw=="), //:\ProgramData\RDPWinst.exe
			Drive.Letter + Bfs.Create("qSCqZlxBr3xKdVTU8fv9A+CMQAee1XtJvz/uq4CBFUkvaGfCC7LINM+jNkmb2mhZ","MMBK6TUqKbIlS5udAk1F5gp+Ieutxy/q5bQQGZWJnXQ=", "fuQ1Q7iHN2v/ZgWgAW/9uQ=="), //:\ProgramData\ReaItekHD\taskhost.exe
			Drive.Letter + Bfs.Create("opM2UZOfEHqmnvy8BABNQYk1j/hAVPh9gVuu1Pnw53D665+yNJiasC6ZHz9cPVXu","c7P/gwFBvp48AZQI2djBduhTNFvHWxWG/08Qlbum7IY=", "agKZe7nVzGBOrpbpT+BYZw=="), //:\ProgramData\ReaItekHD\taskhostw.exe
			Drive.Letter + Bfs.Create("RWyXN2qK6+CVL3W11FwDzO69e8YxaO+rd/ujMZYyhRb1XTl/V8xVxpBNnhvjuJdJ","ZxekJMCNbEy8yQcOsMriW5kdevqKawRYYpOZOo9xaOo=", "32J7corUxRoO1yS87t+20g=="), //:\ProgramData\RealtekHD\taskhost.exe
			Drive.Letter + Bfs.Create("gKYBOOy2HfHqPOIBDWV8RCDECW7TiiEzdBFF27AGLYmGy8pYaSZ5/SPRDgD2u6zE","ac8vTiT6c/Gagl5LagID+4SufDmM2YLbzHj1jYlMRIg=", "z66EFBZzZGku2AVqmUPg1w=="), //:\ProgramData\RealtekHD\taskhostw.exe
			Drive.Letter + Bfs.Create("xSjiasj8ucHLxCXFUT29t+ZhajbayjUPpnFp+maPKASHjTnLEkJySHubyy15e8vx","vKYUuk3rvlnLk6K9oa4GnNL8tld380IT0e5nNHymrd8=", "nNycFOjT1/RmcSgALcTVig=="), //:\ProgramData\Windows Tasks Service\winserv.exe
			Drive.Letter + Bfs.Create("gzGuoENo9xERMCaaNtQqelXUkPAIm7nLqL+takvd6MsQXlzPyOKypNe2nlhwdHwO","i68qJgGn6urhdQyrxzMB96sWEH/3xCD4IhpkxzRpyqA=", "yqtSYIddnPAXlJdepYJ8cw=="), //:\ProgramData\WindowsTask\AMD.exe
			Drive.Letter + Bfs.Create("X7/zRbURfOs6dBkwzxumZ4QocQASVkz3rj4IMGDN86Am39enZp0h44WUQAaFN7Ds","VfF4WLsMFB2X0QskGr1qlBnn+QyPh2Ha6v/UFgukf5o=", "WdVGyxyhiuZ/mq5TJlJDHA=="), //:\ProgramData\WindowsTask\AppModule.exe
			Drive.Letter + Bfs.Create("EiSr+t2DlVMrpqkJ8N8Ds8Yk5qbxhsbOTEAXTqETSxY1ZzlZJ9J1CbhE/mifibPw","OHOo2D+tAeYNMPzuSTlE9f1r5uZ69O2xroyh48pQqWQ=", "lzTujn1YVH/zij4NgAMLXg=="), //:\ProgramData\WindowsTask\AppHost.exe
			Drive.Letter + Bfs.Create("QPD2NZc+NxWqrsnZKdfrKMYrVjsyQSjvGHkRA8Y45QumB89SFGf3Rj7UDPeqd8uf","1oewVXMpB/lkfGglLMbA0D7e7nm5UtY6iYRNDiPV3R8=", "0g5KkX2yzVxhhT6LTzm3gQ=="), //:\ProgramData\WindowsTask\audiodg.exe
			Drive.Letter + Bfs.Create("UPGq1fvv+Mmnx2PoQ94+p1/U89cV8fi4Oj0gctaI/+oT12Ve+LKvVHJx9E/6tBOd","8aNH41XXqEpBE+eHSNVk9zz9Vm6y9TXji036umRDIJ8=", "ChJwlhbQ4VeHoO50tGMzEw=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
			Drive.Letter + Bfs.Create("lCjdhvseMFzAumqjHLkykDq16Cq2HW3bWP3brf/kU6Q=","pYAZfWY7nRTM71WIkohYuNZbRpsi/+l3rThJDeN6VpA=", "cU/zIfIZnwSunkqlsE/Wag=="), //:\ProgramData\RuntimeBroker.exe
			Drive.Letter + Bfs.Create("XblR8MU3MysDcnXx3P7wX5s1vCt2R9fJVXt6UUr9Aq9Di+IiAvw7G8NGt5mifxVt","ymZBb5XZz0Ue5H695jKWTbeHEfWNifMhmVkkYDJxwLw=", "FfcJn6+CQMdAET/0zYWVKA=="), //:\ProgramData\WinUpdate32\Updater.exe
			Drive.Letter + Bfs.Create("a6Tdj4rJsaUnDZ7F8BwzkVlwsTQ4U4j6P2C4u5fczwpelTK1H+Oct31+D3N8o3w5fHiloVtuZqOdk3ChyaeVHQ==","VGiRU5I7x4h63NlWtydTbRYW0mOWjCHNHOP8x28V0Os=", "8tYWIiRvtJtnxA1MoBBCNg=="), //:\ProgramData\DiagnosisSync\current\Microsoft.exe
			Drive.Letter + Bfs.Create("K3TlKiuu4MBWpsWl5H8F8szK36dwAFzJ+jo+cfZU5kgb57kq4WDbhJCKNmcM6PJE","ugvYe2oBb2Kq2DQAPQQToHT5cNo2zRaoCBxVMv2EWow=", "kR6IbYwQ9u1u+D67qyymwQ=="), //:\ProgramData\sessionuserhost.exe
			Drive.Letter + Bfs.Create("OMgwBYidayVuP1Ws+mdDuQUmtG2ge+KXP3hrZT1aMMg=","bqZ8dQ9G2CYVpCB9VQjatuWdcEmmQnojIb4c74KWYBg=", "EHEPWcPGXru4EUDPCXrStg=="), //:\Windows\SysWOW64\unsecapp.exe
			Drive.Letter + Bfs.Create("TNDBTAmIQ25vAuUjivWgNYr2KQuAsy7AFzUI++GWNXw=","3W5e4hYwQW+loFtZxMbLBax+rxiagwUUJJPgjBgso68=", "FtG4PgemDjE0nM393+B7tA=="), //:\Windows\SysWOW64\mobsync.dll
			Drive.Letter + Bfs.Create("j1GAvx9WtAjXyQTCeUis+Oe7qG8ZmFmF5QUu2R56LNY=","Bh+LDwDhcKWHnvYVMNrRxfLKEjH59GaCziAp1xRjEuA=", "sBcP9MTpOonEFnnCIPuY6g=="), //:\Windows\SysWOW64\evntagnt.dll
			Drive.Letter + Bfs.Create("1Bm4mZUVo4z8RETIQQUzyn78JWfs9PV0KHqpDV0sXXE=","I9sgn+PWMSz7N4O1u9uaZrM4GvOdKe5SXnyizB7M+Es=", "fy4/kPMxBH4jMTzKQewxsA=="), //:\Windows\SysWOW64\wizchain.dll
			Drive.Letter + Bfs.Create("oD+SQUWephJ0bEfwepFPHACHwfx0HMEmpK5uiPOMozI=","0GT+YQ0dONp5298p9AQuj3tbrLRtPZvK0u7bBZgfgT8=", "ktX9PJqBRsQrL7VFPwg/hg=="), //:\Windows\System32\wizchain.dll
			Drive.Letter + Bfs.Create("lsKhtJY6zx/TKP3pvIZpvDGNSzGOj/eVh1wDIeIMaP8=","Cy3r58s12ZQTVCRpuyAtUQmvKlagjeLh/KftJktmcWk=", "7yazX/PYohQrcHiNKo2m5g=="), //:\Windows\System32\wizchain.dll
			Drive.Letter + Bfs.Create("3os7nbWocFBFIWvO75YJscJa9WYM1PYRi30dJyy+RFE=","6BX7nL/PHDmXExsADEiWG4EL5CLl1yWhI8Yt+vOKY34=", "GQXB0zh51KFiSwRkNotNxg=="), //:\Windows\Exploring.exe
			Drive.Letter + Bfs.Create("mFC9bxSU6wx6Cy+bnYK6N6upCq9jUEuzQOfCY/nEfOuKzRnI2Nvn2IdeEuQXCakr","EWvWxKP7R4AdX3qsWjM8Kmn1t898Khx2OfHQosbZ0x4=", "xRXMtG/5999c1qFC9fE0zA=="), //:\ProgramData\Timeupper\HVPIO.exe
			Drive.Letter + Bfs.Create("UIkTQANVETh2K/uW8N5MmL7H9AqxVPybGjt0S1Tu04XthPxi1ow4FmLDy3d2ku5U","b6lz1k9YcPQg+3Lm4Awy9ZAREiwMRHg9jJfEQAVlMDM=", "2I6hVX8mYKAvQ8mHcViTTA=="), //:\ProgramData\WindowsDefender\windows32.exe
			Drive.Letter + Bfs.Create("1YfMhX/NB92yJbRvV7UfIlpA8VeRgFKwunVvOWrgFIvYBJQah0xXG1Yzr3nOdpH9+x9n6atqBAWlGx7T+XMmWQ==","fUJAJlIf2seRqfEGg59hffcOiR/i4t6jKNL/sLxN0Ns=", "LNf7JPEQjrH1GL2YaDU8Yg=="), //:\ProgramData\WindowsServices\WindowsAutHost.exe
			Drive.Letter + Bfs.Create("RCfxUEaCpCJa250ljM8iwaGU8fJ/XBV36r5IB48sPMH19IkY8ica0GxSmCFQJQm/","vNC85Yrcylt+y/ef39FWyfHHPqPzVfFFlMfNNwnvsf4=", "5Tw6hij96ajrR83TZ7IggA=="), //:\ProgramData\WindowsServices\WindowsAutHost
			Drive.Letter + Bfs.Create("UFnGcxhlQclLWzcxUwt2MVf54CNb+fzCJ7i7qwgyCBpgrG385rmExd7eJ2hABBap","IHyLdBSOP7mHAW4lw6gaMWCwcqelDK+6oH/nqfC/R6M=", "die1Z53FQDzO8rW3c9B05g=="), //:\ProgramData\DirectX\graphics\directxutil.exe
			Drive.Letter + Bfs.Create("0YCoVu1WpfQPoW38Q96zNmtphKA+HE8FtVoN6x4PVnU5HJM4g1GL/TApnzkZoHKI/TZgBRxeJTRGQjlvCrwxxg==","lYNKTb8gCohy+9ExRtVNyZt2wlmX7aim2lDi9F9KVbY=", "3XbxxLREAcKsWZlFsgfUvQ=="), //:\Users\Public\Libraries\directx\dxcache\ddxdiag.exe
			Drive.Letter + Bfs.Create("y9GHLrx/B2+lM4Ddt2dEW/kJzR5j8uT5ctsT+bSxwSDnvFjgx+Fx/qsnpWNCBRbjZcZyvPo29MTzg5iqJFzqGQ==","XnlGLFdm42S7HzJY4mneSsFvjaazNCy/CkFSfv93MOs=", "g2LhVa8oxaOfPf6uKV0Rsw=="), //:\Users\Public\Libraries\AMD\opencl\SppExtFileObj.exe
		};

        public List<string> obfStr3 = new List<string> {
			Drive.Letter + Bfs.Create("OV2cAI0ZnpU2Wm65XjUToFRDQBWWbqRQb43aoAOhzaQ=","9ZgfVeENG+uCCTz5oH5Z/U3TockYo8Aet1vA+0DUB/A=", "7bHFHtDPaWhpEWkKIA+ruA=="), //:\Program Files\RDP Wrapper
			Drive.Letter + Bfs.Create("2ov9dU5mlSvOpeZEKH4y6g==","NAUbeWZtISuCH5tlLvoz3thW3jrpvv2KPG1VE2khjvg=", "7ZhmiPTo7mgi/GMvC2UD6A=="), //:\ProgramData
			Drive.Letter + Bfs.Create("hqinwVb4fqQyKtshVkvW4aW+1DvGOfo5uABGgM3BosNx9fYhgOBZxujFurDg6bHG","PxPIvOxVl5CkSNu49R5JGwcD/Zf1qrIrKalVAvM9hgM=", "xl/ba3TJkvupSAIzFpGOZg=="), //:\ProgramData\ReaItekHD\taskhost.exe
			Drive.Letter + Bfs.Create("j1RspUXQqEx3yAYiAv28UNCZZ5kKn1DTz8pg5cdh1ngbDnjFoJ+xM7a16PxG0YZo","qETJ2nI+D+HVM7JFKDXsEPdIyswwUwTqmrEGOORyohc=", "TPKXIM3DAUw5cD1tw6hfXw=="), //:\ProgramData\ReaItekHD\taskhostw.exe
			Drive.Letter + Bfs.Create("9AAu5X+8kQwvGPorD/tK4BkO/YWdFlGXyXrwxuBedS0B8J6Z5wDjvmfrtL+2SJ+h","EewWh/sLLW5eR6KLvOxRvHif5YnuyFa8NZF79KN7eXY=", "7J6lYanRxeuCMQ1CgNIBQQ=="), //:\ProgramData\RealtekHD\taskhost.exe
			Drive.Letter + Bfs.Create("WLQIKUL3MsUSN80eg5mpGgCqmonGGpFN/EBjGb5hDlCYncn8nloIAHcpyFQbWxkd","RlfPsoKzFRUZ76NNUARw9gj1eH4ZE/+PvMZ9zncvNuM=", "EoPvDI+4vRqjK9ZJsAO3CQ=="), //:\ProgramData\ReaItekHD\taskhostw.exe
			Drive.Letter + Bfs.Create("7io4u3b6VSfQEE5Se+o93LODJWlv8MBKvyYYvun2aumJq3fKzkUsJov7zWC3TR8O","JGx7tDr+3D84ZBB6txrMSZchdn7xeYD9z26AP3MZoOA=", "99wGkQNzN6eUERFQ6lMFBg=="), //:\ProgramData\Windows Tasks Service\winserv.exe
			Drive.Letter + Bfs.Create("fppsnO0Iawv4NOdZ8cLu0WGRZz4Uxfr5nMC3kcCbiAnyc/PqJflj/cMn0hxzsOcz","fXneIDDheUMZl/GL6hp6z4+1XrQW+F10+Y5qu53o9zk=", "IJTwaWIUMDCQc0SLbJPf8Q=="), //:\ProgramData\WindowsTask\AMD.exe
			Drive.Letter + Bfs.Create("zsQULTLkABWxCzkI59s5My6l0H7P1RTaqFvOPRznMC7LfBGNe1oP/6h/kYF7yXad","XXB35zoak6AipN/dwm2B3dJ1uIve+PL3bhBt0nTwbSA=", "p060fDPKFRY1NJdm7GDUaw=="), //:\ProgramData\WindowsTask\AppModule.exe
			Drive.Letter + Bfs.Create("kItwoeJUlte7rFq6tHjOSOibuXQSL0sdjovcVTDm0Lo0PxWaJUpzXkAkxZeoIA4n","p2y0Ly7ivZ/3ZuRsVmxNQrge5egkIfCrwoqj8GhMQSI=", "03MWYOeyQfRjiKDs8W1FiQ=="), //:\ProgramData\WindowsTask\AppHost.exe
			Drive.Letter + Bfs.Create("FBjVh9ey0YleNYH/CquGdKkXJ0iXu2gg88AALrJEagV3BYf52vB8+JoFxq1D4/x3","NFnh60e0e/O7vpoVMKaXgNu+hGbwJ/eYE4z0qB/r1po=", "VUjUpR4EMiDaqI+Gzg4dEw=="), //:\ProgramData\WindowsTask\audiodg.exe
			Drive.Letter + Bfs.Create("oEV20Q2l+p/0vj3oehWa8pKZYT77CuAtCXuGKddXCx7nsaWqFN8SJKeny/JJMPvG","2QdvyFWiV33pfsJ56nEtg+6uuZ9eEQ94H9eRiDupaEM=", "1v7DoLXPO3DaJTPzw+9j/g=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
			Drive.Letter + Bfs.Create("QHKFu7NxldtFM1NomoIWGtutALAjAQ3mleLKvUiX2UY=","BTqhZ7khQTWt9hrBX2vXFw893TGYsKumQTUTAtV6hsg=", "KF614c8JwAjDuK6nSSyi+Q=="), //:\Windows\System32
			Drive.Letter + Bfs.Create("m41ej+jNruj2bj8/3JFgf5nZTgZXWYtGM/9TlJyeMx8=","1RDq33HfWz++C78qi3AkRfMEtMbAG+UOIYFpJzr6io0=", "GIUV00u4bdYfb4q8Vwx1SA=="), //:\Windows\SysWOW64\unsecapp.exe
			Drive.Letter + Bfs.Create("fczW+rbGWJVwDLcTi4cZ7hYZi4Fbzxxk51w9jq14d/yadRP1dDx0u3fOi0EJh7dy","lEoJRdXkkw/mH5+72L00p2HXRgoblUYCBu9o9za6XaQ=", "+dAFMKROfHfHI1C/LAENNA=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\
			Drive.Letter + Bfs.Create("UKnH+hUbAOMNv5q9fn+elHS+E3N+gl+G+JZUe/QZGy25EImvJnqG90XkLOfpt2UVaiZUpdFvUk3cSArsJuK3gA==","ILkXIWzrmt/q+sS7aCpzTBig+6OnasgMrYx2CJlbiTQ=", "9nFadEJUk3IIgzkor5/neA=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
			Drive.Letter + Bfs.Create("pSkVFeQzaBFtzPMjegY5I+OnEdyO1GsxjL84myHoZ6g=","DPTXC74Yav9oGaHFojPIXgwUkmoZ89R3g3ICW7J4SCE=", "sHstObEVzy+Vcbgx762g3A=="), //:\Program Files\ExLoader
		};

        public List<string> obfStr4 = new List<string> {
		Drive.Letter + Bfs.Create("OjoJ/BZ7h3w/+wrGfJb22wYLG6DwTJ6mHrN8UlRrDeE=","4UGCX2DZVlrLTdAWSHtqBkkT/go6gp+87CTS8bNxaDY=", "MaWEqmY29T5qEZTRlFI6hg=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("/VBYi3wim+nw4g77ziuSg/QXIGLqy65XnXvZtYcMahPmQs8QuGSyqOKuBk3McrPA","MUOA68Z6QyVg5nAH0qpjktpnZkjgjLNrZHHCHqRTpik=", "pr1a6jKlpc/GkDF92veTAg=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("OzVShB77bc+SWbI1Egg2pzLLmbpSS1MO4EunACEQXiJA+MSP5ag8b4Oa+FpKx/6V","ZFi8GXTyvkJ7sCuKsbhcx4aD9tT7OJExhZG6Fqh1DEc=", "G4cUc8mEYnXmIXIGA2bGMA=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("0mw49NDHreD2ss3n9CA1o3eU1XYO/G6/B/4G6s2Be5UG++4Nm/O6uaEConT/cxH3","cd8wdptrG63cw9PJIdDijcr1BdJZugIdiU8xxmjWaUg=", "wak9tXLEmI5EAAqvhsABJA=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("KZb3THwrIIPCNMaSKaEKCDnIvNJX8g39TuvbeUMOLzNkvA3rifHDp+MLpRlsid1D","pGTS50/mHj2yPSFPgQWexqkcwbjtOZI9oiFkCVA9hE8=", "g8neqfmhLb+THHF7bbYzoQ=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("yJCpwk+H6vuKUts51NUnIbQVMiZ3nDT567l2KEcYspVzN2kHjKyMJE+D8vYDtm8m","ETA/XZtKsD9/MROB3598aD9IvnP6rh2tPC7CCbEswoI=", "QdhveR0IKSGnTbICJOC8KA=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("nrdpVwD87hSyGaaVxFIxFi7lzbNvzhfd086oye5K8ievG6mJj8ntF4asqJ3YQZsb","UHQdnqjRWeGiD/ntOGZRO8Y14TmGefOX8PXHX7cw83o=", "gR2NQkYXqZckUZRBIVpntw=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("Iss+P2/m8pNI1AXnXtqbS9jAAR0BZfbpJ7l1hFULX8eVbMMcNnyJy8iGW20L0xiO","s53R7o9pm5cPAo4Hs3mZ5qz5e9uxl988VwuO0xRQJWU=", "qqc6utDzZ0r5aC/yg0f83w=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("EBFYupkpdrUiZaom0dSUL5n89pR9MzeeFoS08ChMkbylTgh7t3uSDVIk8haxBnQn","5hQZHSqWSLoVqzdejxg6H9/jRM1YOsffmYplRCpbVDU=", "FbuAzBxSLoxdXm+qmFuVrA=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("l1W8x3JWTfsq2d0IydkIaUQ4XwBL/bma2xgHfBdXLmqFGN5PLs5/2y0ijiJMSWnv","6NDnYzkcZcTAB0G5USGkUsC8wMAB3PXQhsoSVRwLdhc=", "7ySSyntjO9bZ2nhE2rFfeA=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("Il1Ds8nmyaS4FXB/n/UsXzFOs5YwcRUfq3inCtuE/+KUBa3upfSFI0rR+kzwrdc6","wLqtu0bfuHmWkXp3p5Wp7ckyjP60QwzKpxRmRWoQQT4=", "CZaji1diMg8XJtx1sirvrw=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("WzgTKYgCDMABXTv2yIfS88CLzgEJEp7cQmTe63qqMLY=","ML1x6BNo+o8QawPcb3eXNWk4uXKbIyXkQ+7cZ/qnudQ=", "55uzfhNDNO2p8q8L+jBX4Q=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("thZBX1NfVO8RehSXgtVRNcn+T+I1Ydvt9+V4qBVbChiGAzZj7kuDVaRobQk7oYEZ2XLMfGv5zCYXbwhAyhXUDw==","vbWeyxqf38LFrXAdmXr95du+FrZBwjz9/9RRRphticY=", "Jw/AHLiE5BISD86tb8QEfQ=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
		Bfs.Create("9zfPVj2Y0f1KDjSv/gx848m0VQb6XtNpV7maczDeUIg=","7i3gd3kNNyD+Pmqd+OPlvbBGi8uqQlTHnA3GDaBvwpc=", "RQVhF/NMO+9fIXUzwrvjtA=="), //AddInProcess.exe

		};

        public List<string> obfStr5 = new List<string>() {
		Drive.Letter + Bfs.Create("DBbNCyIAsliH74QL7ht3wJ45LYv3x9r2Oyc7RvAv4AE=","Sq7Zn7Ic5kzJ6yFmKy4NCYL4s3OOc8v/c7aRR0jJJK4=", "nynBBr+PqPRFeaaTP3+15g=="), //:\ProgramData\360safe
		Drive.Letter + Bfs.Create("uFeQvw1LjDI2bKMKUaaI4SNyQGqqZ8ESFr8cN5i/7H4=","PS3/tVY0C4+FnDAD1QSuoPMhx3l6aUeejpNVRKXnSAY=", "fGyfoPaCCCoLLWRn81FfYg=="), //:\ProgramData\AVAST Software
		Drive.Letter + Bfs.Create("DvntwUi5M6PihJaBPmrj7Pj07bjnwqzbWBP6mgnoTbM=","6ve4Hw8YOCi9lTbkHTLX/69lFTwMxx9Q+kvhAtz24cE=", "KFUyZ1H7ARUf2PxfzJUFQg=="), //:\ProgramData\Avira
		Drive.Letter + Bfs.Create("HUmfSfeQ4kejsnPWo803kkOn9c+xrG8DRmPUY0aa6xI=","+McOPmK0kE7gQ4Oa6QsNmIYahkbDQc6boBq30YI3gB4=", "w31NQgwyIpxIaESOHP7/xw=="), //:\ProgramData\BookManager
		Drive.Letter + Bfs.Create("+al4svv+82Skq4U/IvjfWz5bQx14fFQAZSTzZ1xqzE8=","nW6L1v4VC+uBmy9CyPp5vWwUxN0FTrDW1/Du7q4f3ms=", "LErEvnYSxm2euQ7p2meIIQ=="), //:\ProgramData\Doctor Web
		Drive.Letter + Bfs.Create("rpdKSwzGZL0UiXmhYY80bWxByRc2ICI94iVoQWK1YlA=","En4EW/Xfw0tST1wx3Cdx3Mpgx1vBERh9aDy2+G7L8BE=", "Cos4RKmODmb0hRio79Bd8g=="), //:\ProgramData\ESET
		Drive.Letter + Bfs.Create("+qq0hwBNMa0Q+t7txPaqKo6K5jtwXn9gnbUJ5wF2Iq0=","undj4p4apJswX7/leECjIAKxcMzapPGb5EFYr64GpRw=", "lE1fAZfwUW9HnflzICW69w=="), //:\ProgramData\Evernote
		Drive.Letter + Bfs.Create("QOTC6saoKrOeZWqaPIB+AFHB1Qr8t9aZDKCtppimodU=","/sNRz1YF5SgZISohmg8n9iex5Lv0+QRDfxEepZ4eTbU=", "BUREQM3RbUKs/4j+qE9Waw=="), //:\ProgramData\FingerPrint
		Drive.Letter + Bfs.Create("Z+JjAcyZGeVmUrEyR6aLgWxz6KiyLDAiHsuB0rT0xY8=","etHslu4H6uY8KHseezDE/hd00Q+Ik8PDWXwpw/71cnc=", "/5vSvYssSw2oBHkXpZm2Bw=="), //:\ProgramData\Kaspersky Lab
		Drive.Letter + Bfs.Create("IZex2zkWxUjSfI+1RUs1SbKtKrdUhiefBF9S+/XpqC9TwC88Aze+r3kRvRfpWxVo","inz/CCxEsdEO1wNan8l0S5oUMhIyZwI2+EWf2Solui8=", "lZjTPPDCxtgtzyb6Mz2Okg=="), //:\ProgramData\Kaspersky Lab Setup Files
		Drive.Letter + Bfs.Create("sppAYTR/8/DJ9i3UnNjcNAHjqW1m1mNv/7eOvTwyFfY=","aTXu1wUirZWzJNpE2Gam25M9qrodleG3HdIrphwxqcA=", "LGInFfAHmSaw4/k/9RASAg=="), //:\ProgramData\MB3Install
		Drive.Letter + Bfs.Create("LUg3a04LOogbHbtDCIrpVDnEJL7ono+JAJvmPFZXmh8=","A3Drq1hLytdWGNd2C7m5om9rWR99IU+TEJcOylnpLXw=", "mjs2hxLG6nyZbsPoOorEdw=="), //:\ProgramData\Malwarebytes
		Drive.Letter + Bfs.Create("S/8Cg8KTCtM9C6IKzUWHAj3nPe44KPwzZjlFlKLGstk=","jJcgFXMhGMq0H7p5VdzNMhNQHAasNj3jYK7OgWcyAZM=", "4Ki/XFHr3NIDXiNtnC8FAQ=="), //:\ProgramData\McAfee
		Drive.Letter + Bfs.Create("LW5rUibascxRrqz8D/m1QCsO5P7atGRwEQYij5qOY94=","uhphgn09yFS3tIx6fjPos9oWTGt3mmwTrNtEtTCzNf4=", "+/ZWWUV2fBaZzoxNo8x77Q=="), //:\ProgramData\Norton
		Drive.Letter + Bfs.Create("yuyKyAX0pZCQeI3upEGRZXODJRxD0oTEjipN1FQVSrg=","jlAbvhLfBL4SUB3sF2k6ed1fJMD1xvahU6M9SHV6ofM=", "BHrjihJI6r3EJSCrULEwFQ=="), //:\ProgramData\grizzly
		Drive.Letter + Bfs.Create("6nFYYJ3QKqPftL2/tf40CsogHpAMXAmEqowl+ASGVPPuvi/HcUIEZ11u+hFfjkdw","FgoDOPhBghDzfJP+g6Nu3UY7pv4llNU1LQY4I7Hj6gU=", "676xa31R2ugf1Ja1XRu8nA=="), //:\Program Files (x86)\Transmission
		Drive.Letter + Bfs.Create("v12s//VT6JLzJXKNcI05qzsCmWAX9cr8p31fp3qLwZkaptUzH285qKt1iyPpwAEs","e0Ld8ImahqLoNaYVuUQXZ+PP8sYDy8GvF0ey4Mj69/o=", "POTObib2x6YWjGOmzSWwrQ=="), //:\Program Files (x86)\Microsoft JDX
		Drive.Letter + Bfs.Create("5VtFLo2rNOh78GG2X+z32Ea/JlroZ1f7tV9anU0cJ6g=","Z+1cXdWUsJPqXlFX6rfr3OJPZ1JAxMcFYmPuWMbuN2w=", "ZG7N4dpnIe4OIO3fMmFuGA=="), //:\Program Files (x86)\360
		Drive.Letter + Bfs.Create("+3j6R7s8MhDts8sMexysiuCFGHwgQBNMKIje7gN0kzM=","XWTykhjlMDVArBpHf1CPUrhbFCPMElnAmNTNrZ5RRX8=", "xOHPbQiYNiR8ePazhSlyTg=="), //:\Program Files (x86)\SpyHunter
		Drive.Letter + Bfs.Create("yIuRg1uBOPbrUqW7yoKYxPgIRYlPVfnFyz3aWsOm7zsagSXfGm7bxWbUSHJOvPpA","WWRo/QDNbPL70s1Fe+Hi5h1uJnStmb/56YLtdM4Lnpk=", "YCBVBHHtCvCRwnKCH78BPQ=="), //:\Program Files (x86)\AVAST Software
		Drive.Letter + Bfs.Create("i+MxFsOjRwUscKfEjAEErVhIHHsKtzcoQEp37DPd1Tw=","ZhFHohDLGJMa1quscdEdubEUWtbZZQiqzuWBOMQZgMY=", "RPUD9kNcpRSz27nxKSoaJQ=="), //:\Program Files (x86)\AVG
		Drive.Letter + Bfs.Create("qrR05c+GoXG0qmcKNBUArs61LhFoLFi1jNevK/Eh/AJBWYQ9YmD1/EHvEJxt0gcd","w0Sy86zjFkun1U+KsSkVdA2Kjs3wNa3yq5AA64ntLHc=", "d3cHQdYf04Ko0QPVdbCNYg=="), //:\Program Files (x86)\Kaspersky Lab
		Drive.Letter + Bfs.Create("NX1LAujkK90Jujx9dlOcgzeEm24NZnMAr/IsoghrNw4=","ZE58QBa52Enp1dZVtHmYc9EdO/jrW/VsLKAZiZ/Pd9s=", "ftVn4zOITJpowRQMUgzEqA=="), //:\Program Files (x86)\Cezurity
		Drive.Letter + Bfs.Create("dLHH5alwB4lkj+KixulJRZ5QmKZPDSWoy2luTTqLz3G4CumReiTMfeo5VCGWJ53C","z9BIqItBbLcN8vRYhywtRF4jmPCSQxMhfNm3eVXGBic=", "B5TiL3IOjwH3FNnCXfnl9A=="), //:\Program Files (x86)\GRIZZLY Antivirus
		Drive.Letter + Bfs.Create("iy1TE4zhVw7SBtxRTfmc9M6UahIl0UgoQ3Uo8eMD9op7gcBgCO5BTp7pLXa1f35p","i/LpydrnFQDEZEFnu8JeUYCBloqkYTlJWBJnFLwPZXI=", "WbeEHus35qiJop1y9J2Www=="), //:\Program Files (x86)\Panda Security
		Drive.Letter + Bfs.Create("bt6UMTc5ZtscZyJEZp3ea0HUVHlCrsfnamd2h4h9rAscIcJr1sOf+QNtzu34qA3t","SebOloR5Ohwfk4jvQ2+5YOHlNfW1bStLAc5v6dTJp3E=", "B8W69W9bBY/MtG1LZjNSjA=="), //:\Program Files (x86)\IObit\Advanced SystemCare
		Drive.Letter + Bfs.Create("bqNVlPVdPzH/8znbfIbJ2USQbOOBh5gM/Ce9c3lRg6Tg8p7Upaks/tr51iG7I+me8OHVaGMdopTBDMFcJXXU9A==","Gxmy4Ha4h2Lbv/ComkwhI4CdS6vHi7rMi39WK2Cp67w=", "Uyznt71OfrkpwtfbhVuV4w=="), //:\Program Files (x86)\IObit\IObit Malware Fighter
		Drive.Letter + Bfs.Create("3S+IbCQmMzv8TLV3BuWl0tGa0FXcWpJ140q9+AjQ944=","lFviXYeJfdFbxi4A75lH4ESVVRtpMK2l41kOOxCqCYk=", "TOXTEmc0RexctpZ9gt5BIw=="), //:\Program Files (x86)\IObit
		Drive.Letter + Bfs.Create("9ORRubLkISVucknBZJpOI52XtVdmmZ4ZjE/53P7tdGs=","OW4qDYyphTOaWwhEmZDkdrNlRgOLRDAlEiwzKTy0ugA=", "Xe4kNwq1MkDlHVB1REQnqg=="), //:\Program Files (x86)\Moo0
		Drive.Letter + Bfs.Create("txGXeQqeorLEITT21aFnTaykedysZupxdH08A1sbehjE6fowa5LaNb/abXepRH+G","8Vvee5VEX0OvCZoULdf4jnerzBQTYEe09fcvd3Sxgqc=", "ZVtjx6KpT/L6ZIm+iLOxxQ=="), //:\Program Files (x86)\MSI\MSI Center
		Drive.Letter + Bfs.Create("zQLCiDLWJSt18Vh9dSVBDffq0aVsJKJ0VL7iZFKJ4nI=","wIBT8xUnoMvtZIjrt1QTBXt5pJC9OALKkjGlJqv9HFA=", "gRSXyP3Mfmv+YQKcHvPW3Q=="), //:\Program Files (x86)\SpeedFan
		Drive.Letter + Bfs.Create("/ofkNzcmJbRwlfiOCZVMpwBeLeW0YwdUvG2ln4NmEsY=","Pg+P3DSPRWagvE8gbFZ8/q+7LHA9yYwqMmFG5A3XM+Y=", "2cHHbmrQwAHav/P0FLb5PQ=="), //:\Program Files (x86)\GPU Temp
		Drive.Letter + Bfs.Create("cxsXJJS/aAW4pasnxFnTP4Oxn5bixY0SDMHc8AA1v+o=","GMkpknM8pgsWhh1RzIfoh/xBXTUFp8sS3A0NUY14coc=", "Oe1/aSC/HUqBND+wXUKlqA=="), //:\Program Files (x86)\Wise
		Drive.Letter + Bfs.Create("Zv4DlnMxYA2q7LOtttfQ1mFtT7bRZoMDPHLMhQWbZA4=","t+dQ1qnCDfnrX+E5oDS93vq48CfEhrzLQWeObvLfkCk=", "racDmQEX8tlCo0rNVEe2qQ=="), //:\Program Files\AVAST Software
		Drive.Letter + Bfs.Create("F2YMuYgUSa6cv8nwd89l7MzXYwwAyJ0LbDvXyuKIM7M=","GDhIUk7k5jsTm48wYThyH0/t4OmDB39lpvHRNSLk64o=", "xpwM0M+u/2XzvSDSdGYAtw=="), //:\Program Files\AVG
		Drive.Letter + Bfs.Create("SpC/cnQKDbcX38R47B3UEeWPpRd0Oi/WRkHZXyTdI980cIexvoKbBRAqgKnQXA2T","EUyqJN4Au9Hpq+rbJbVMcOv9NVLNEtHyPsO/DFs25Pw=", "fczkQ4ohyIyBsBSKnfF1Dw=="), //:\Program Files\Bitdefender Agent
		Drive.Letter + Bfs.Create("tr4mbTNn68HOhnGDoT+QbxhdD21oMhHmBmCae7ObpMU=","u6N2vf5wViCaLactPZgYMm5ejXarYzM8K+dqtLPyPTg=", "S/XTQmFJZCnWiY9+NcIQeA=="), //:\Program Files\ByteFence
		Drive.Letter + Bfs.Create("Dila1eizLBQBVXYhYdxchTv0phWQU9MsdB2+ZU6JUNI=","L2aT8Y/ZWtSwR6bjQfaFJZvW1PHrK7R92TSGPqlrEjw=", "Gb1Y2jeXzEz2LGOXcZ8aSQ=="), //:\Program Files\CPUID\HWMonitor
		Drive.Letter + Bfs.Create("FdqX7opbr0v5p7h2hUKuR85Tj9brrmZJt5skMFFk6+c=","Q3n5Jz0p1hMs1KPhGuv/9H0Vp+pJ7MvQdGTwp+sd/9E=", "BrkMc3FsuCWdpiouTqy+IA=="), //:\Program Files\COMODO
		Drive.Letter + Bfs.Create("SCA8QuDtB4NRln0FTY9qan81jujjdqhm8s20Cp+ubzc=","dSFQyyz9IfkdU0EY0NYXQMfhdcrkj1rt26dbH7CckGA=", "UB2WUruzZ/Boj3gORLXtbg=="), //:\Program Files\Cezurity
		Drive.Letter + Bfs.Create("dAlCDt/Fc1TIcroCNqup2dJ8JJLnVOhxqeQ7uHzzOfw=","9N80XBpL4OMNRg7OW8z29fp94ssq4K4fNjCn1VxDYP8=", "IQK/uQPBhePaX/Pjd+SIZQ=="), //:\Program Files\Common Files\AV
		Drive.Letter + Bfs.Create("VslSKl+7OmVANtXPsSM4LWNhmRaaas1Lz+KWbWSU2GwM50nM+43tI3I7CHo+cv6J","OzsR/TkcxKj89mXqPQNF/X9fPhhSyH433+dd8KcQVuw=", "x+U1JmrVXIIrjWiepyGK4g=="), //:\Program Files\Common Files\Doctor Web
		Drive.Letter + Bfs.Create("gZvzTu9UHF7/XLL219ySAtGA+qPBTcIOWbsh8J5h8Pv3FChX6JgK0b71tEq4nBD9","kOHDSv7ytyqwfHcr9qWf5epN6N2zUTfkz3Ks9CmB97Y=", "j9dPTaXNnFKnGeY1p9jrTQ=="), //:\Program Files\Common Files\McAfee
		Drive.Letter + Bfs.Create("kE+pdZ++icLsaV7ZOWvclqxmqH576SLrafHaVkPNvDg=","DrzfhjJLPzbghK7dyx95crlohZWiqG1O+00kjUUmFek=", "gVkFX0Xs3eCcPnwvDbqqyw=="), //:\Program Files\DrWeb
		Drive.Letter + Bfs.Create("LEeF6MudMVvjS7quHz+KOW+tTu6UaphzmUx1pwtnHDw=","umaWwPpRkOxvfCq9uoAziKuthfQ7iEzNOEJPyujnetU=", "/YDvu3uSW/2dFseucXbQ1g=="), //:\Program Files\ESET
		Drive.Letter + Bfs.Create("zIizCwSMttDBmHZHWd2TYELEyFSgmjI4Xcu26xrrn+lvnkyOCKl5alCGkKDd4K24","uqff1Jbt+JUAovgnu62jA9q5LFA7V2ty1nqgXlxpuEk=", "kk7x50Iw9hIeQ6hLt22idA=="), //:\Program Files\Enigma Software Group
		Drive.Letter + Bfs.Create("yBjbtL6MIacomH4fnhBvV3JSjVxGhwTw3X2Fp9RaTp8=","b4x71ffoDDK1qZUb+U+Yxz5ZZI4zHn7K1DcGwTEffag=", "Ky+DYH5A9KKoFbWJj23Cpg=="), //:\Program Files\EnigmaSoft
		Drive.Letter + Bfs.Create("xxs2VgOzTpom9ibhD3PBBEZRTwdaGMZ6I6DVtNyCl5E=","p6A366nko/E6x6880yPWUmXtp0AhP2pzMST98dNSWHM=", "carJcC6w/BSZ9eC9jQTorw=="), //:\Program Files\Kaspersky Lab
		Drive.Letter + Bfs.Create("EFDepzyBqtpeV7YOlUvt3iOVutZuydOZJ3yK1W2EQUO58ztdKbBUD3tXysyKQLUj","6VqScfglAZAgzMwfhLUDKin4DZ/N6afm5SfGq+GMax4=", "mY5Yq1nYhly8Nb4ZfcQF8w=="), //:\Program Files\Loaris Trojan Remover
		Drive.Letter + Bfs.Create("FWJf3iXzG1MXso+IFChq6Nrt1IicjGUjbkOsO0NSUFg=","N1mb77GqbbIvLytJFaxwqzsLocFuJsyMaWa2qB5/u3U=", "maSWvCrNHMqhimlfilo0qQ=="), //:\Program Files\Malwarebytes
		Drive.Letter + Bfs.Create("W99fGTKVT0g5/DN/iewNEvlFkfCbXtmrrJnGddvIP1E=","IyT+y7NiSYk0RuFBxsheizUBa5kLByRKWunWBHuoWX8=", "rXhFiZCdRLd4qGJb53vTWg=="), //:\Program Files\Process Lasso
		Drive.Letter + Bfs.Create("okOgspBEz8suid4XOQU/isvWALxvJIoaRJQWryFjZXw=","T3vL7I9EMNs3epnRquuUT4LuDhEbLdApY7zWyuoCHvI=", "vsNGzRg+gaIwC4GtJN3pyw=="), //:\Program Files\Rainmeter
		Drive.Letter + Bfs.Create("MNc97QBWID2WVmB1Mo8DGCVZfYF5V9Afq3rRyPfULH8=","hacRsFNA74ls4P+d6Iu01LYx1eWkmRNxnO58c5TVyEU=", "3JtzTac0/8jTObqOPuq5fw=="), //:\Program Files\Ravantivirus
		Drive.Letter + Bfs.Create("lB1/fb59C+l0uB5skg0Bzn9GNamfTff/mZmXkwDEp00=","53FbN1aV4jpd1iBY7UVYsU9IoOtSrxYDZApaWs/32oo=", "hPsF+vq0BNaD8nmzNPc85w=="), //:\Program Files\SpyHunter
		Drive.Letter + Bfs.Create("N2YNmVWJ/5bpg0Sow+nDR2+SrH0oj3IcgWVsltwgVnCFkGuOUIllFsag1MuKaNFX","zr4tcno7/mKAIjaJsS/ecl5jJh5A9GWkqM/wdjseRCY=", "0KIcBXbPfgeYKWu6+ggXGw=="), //:\Program Files\Process Hacker 2
		Drive.Letter + Bfs.Create("LSevyq1lFL2VTyLsoN1a0CupnwsWu5C4WM/NzCDxAPo=","nH4mW8PddPvJ8JuMR8fn7oRa8KXK3gFQqPJtzH1GbFU=", "dR6NuB56unl9FPaRUMAO2w=="), //:\Program Files\RogueKiller
		Drive.Letter + Bfs.Create("6LUMihnwinrNdXb2hEUL8cVTSYvzhyzRBiKAJdEnWW1A4/yqxVs9wTHUeiGNYnaA","hz1nEsasVYuiY9lQxZI8IhzVauX5fdlqBBINJnSAhow=", "L4l+dSxhEWLRkU8JErJRCQ=="), //:\Program Files\SUPERAntiSpyware
		Drive.Letter + Bfs.Create("9FySvoKyzig8RAeWLXLWFdz3XprAo3e4K/qCwD4c0mg=","iznFn9ad98M6Egs/xqxlXQENCs9xJnfzG72SXgxojaE=", "GyH/AWm7zPThaq1+sL0z/g=="), //:\Program Files\Transmission
		Drive.Letter + Bfs.Create("Vjta4z7tYnFWA4I441l1YoDZ6xu6ufDkR//3wRzSj4A=","M4X+vN/tp/gsFxbJ95JztlG8boayA5cfOX1wkQSEAWE=", "fIrsZBaGRvsT+L71VRbZLw=="), //:\Program Files\HitmanPro
		Drive.Letter + Bfs.Create("xVnUeMhID69BpkOP9ickArZZ+oWZURBPeBP9OGOf34g=","udcpfCG25vU50ykUbpludI2EJ1Lu6qK5rzpx4qRbZm4=", "YBQOV952YhvKipa2by4pow=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("cCGJcJ+A2N19Nchgy55T7d4Wqr6GriMTzaDQ2TK1+bk=","3ExJ9nq1hQ+84UqYKwZnzd5OIG40oeHDHD0EEypZBgM=", "lpfWFmz76c/djFXEGlVduQ=="), //:\Program Files\QuickCPU
		Drive.Letter + Bfs.Create("XAdlPZnTS9JgLr+cBDlILFA7x8EgGdSjKUu/IK4Q61U=","88YqQsIXG1AY1FbxYbh5kEl/wng1GzRyGVmShNLwLQw=", "nVJLtrmTQDp26p9ZKD3NmA=="), //:\Program Files\NETGATE
		Drive.Letter + Bfs.Create("vAtqn1KonnHqFJMRJm5Yk2S0PEySyqfv9uEUtbgyOIg=","J2N2Aj8HKTzOuCiFBCIsGbArZo1oZ72psMBG+f/FkzI=", "0QXfIQn1Ri6Tja/czIPpUw=="), //:\Program Files\Google\Chrome
		Drive.Letter + Bfs.Create("tr3d6HU2Dz6cyNupJx33fut7/yGpzHNzEQAR/0SCBnM=","xBHP3HyzVvimX9O8taIk3w62KYZBsgrDEiOCfwy69Nw=", "1MsuIF4Us7NzFkDl1Py1pA=="), //:\Program Files\ReasonLabs
		Drive.Letter + Bfs.Create("TpT42fLDRxnKSd0t1SXx2Q==","UMB43uhmj096sprSR6tX5U9F0YDwRIKFDaS5lDjP89Q=", "bs6Ka+veIbhYQYluIWwAEg=="), //:\AdwCleaner
		Drive.Letter + Bfs.Create("JC/yNjUSw+2LqqF4fof2Hg==","RzzJXQllY4HSXDyTou2ppdFpndvhG15/ZKCzDaLG8J0=", "uOE82kcjyktDuf985iiKOw=="), //:\KVRT_Data
		Drive.Letter + Bfs.Create("9EkpzSry+xf1RkH0TlIn5w==","T55zOMSMmy4ghqXuQkGMqmj4Gded+BcHfJVjlxrOV4s=", "iF7ZmnyYNaaLd8onPyJU5A=="), //:\KVRT2020_Data
		Drive.Letter + Bfs.Create("N39nOJnR8JE9YE9nCow+Tw==","i3wABjRSB1ysxVqYXFovG3Iddq0nuFGZFMHTO+pKlG0=", "jDeyJA+eL+3be1Y8bURd6A=="), //:\FRST
		};

        public List<string> obfStr6 = new List<string>() {
		Drive.Letter + Bfs.Create("yeKioxxLWvy9kptHY163zg==","x+6p8oqzpqV9HxO6qESAUYvp2BPDqbeOT+ORKXLOlWc=", "5PjU6RMkSJfq0TFuQookBQ=="), //:\ProgramData
		Drive.Letter + Bfs.Create("uHCYwJ49VnG7R9cYDBSIhw==","QsHrhJR1BTKEuJxmnlR2viVsWUQ2aOhwnpJeUyfjqZE=", "PPcDwIdA35gGdeg25Y6FVQ=="), //:\Program Files
		Drive.Letter + Bfs.Create("hX6s4lFo1+Ntlbl2WEprbxQ2ZdcNgKgia/+5T2BgLz4=","WN0A2etSjKm0vj2lLRrZMHqxA2XJLsC4bC3Mx20QH30=", "NvxiAZ+0OE1zINgtxWoRIQ=="), //:\Program Files (x86)
		Drive.Letter + Bfs.Create("GYriEophiWIKE41ibATsjQ==","ixTC+RNbSFsVrB3v+99AlR2L95/AhtarRVvCoOsyKBA=", "Wy/uqvcVNQzEr8xJRXKSTg=="), //:\Windows
		Drive.Letter + Bfs.Create("4xpS6vYOMgCj4gueOLKP5A==","FREENo7OhsPAHp4navL1emSV4QN2lvhI8/yIp6uKxr4=", "zWA3ruo2S/p15g0/Bxn8yQ=="), //:\Users
		};

        public Dictionary<string, string> queries = new Dictionary<string, string>()
        {
			["TcpipParameters"] = Bfs.Create("0lM3ed0kseUCe4fmq+FiuP/n36hJa6BrOfAqwHjlI5MFE209JNbcMte6vFGSBdsouhIsPl3w2HKgsiAdfkHRkw==", "/ppNCqAIGABO9r5Vk8QuJjD28CZQMPQn25JCeMEhCFE=", "cvnHq98SKV+rFotlCi11UA=="), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
			["SystemPolicies"] = Bfs.Create("nU7dLElciba2Fz2UAzXrkUfUTixuu/bNl508U2GdqJMzwCiLSADUauOXAK4SPeIvAGj2/9CX/pd+7T8pMiqhAw==", "UsUUknMM85SJmPGW58SeCGk9UAAsgJQPNvE2QVaZatY=", "aF24QJi0AsIklrMIZY+cug=="), //Software\Microsoft\Windows\CurrentVersion\Policies\System
			["ExplorerPolicies"] = Bfs.Create("iTM/H495RkLyzBr+cQKoSbcVpApBJ8xW15kWCh9iUzmZ0uraM7i0SVL/nUga1mL6vTAmIcdA0OSsM5tZkyOpgA==", "wM3ScpJUNL/eIyOS37QNaN0cVsXsjz89eFwZahZuIxs=", "VGJHK6fFNmNK4pB7g/mpMw=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
			["ExplorerDisallowRun"] = Bfs.Create("DjSD8qotyEU/eRP1Pl7xlMxutW1DPU/88jzrzwIUz6UORdnr5mIeB/DP9x3QjGgsEgF8R7ynMJ6IVnyuOK1Pmse1Zhmy4esF75Wgzjkx8yg=", "j4i7Ws9RKmK8MpJur4MYbuet8PPyJv8iGgf/eKbb26k=", "tyIM2WZMW9gNs1OK0D3Mig=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
			["WindowsNT_CurrentVersion_Windows"] = Bfs.Create("1kjzHRPSpcUQ8TyCVu6zrNInaITQguzHz4Xl/m3ECAxkGL4kZKd95mAEaZHB2EPpB3adEYeZUyBghhhCahuj7Q==", "jAckdAT/yCBSOwoUxPIP4h8jhqN1tPMKGCCYJM5iMro=", "GT7Sa9veO6VKG5DThH7hog=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
			["StartupRun"] = Bfs.Create("EpYIAea1hGXqCvIvTmmiWiLtSVY+5nGxLXWx41LZjtzqhRu+TRQpt3+C/1fMJz0e", "rZf8tI7SxATvyLaNENy99TPdBQM9jxK/gS7SBOKIDKY=", "LyPWRg1NPKBF24VdVR25DA=="), //Software\Microsoft\Windows\CurrentVersion\Run
			["WDExclusionsPolicies"] = Bfs.Create("xWzar8HJLLxgl8tGlAg084T8CsTC8qDO5hAiEvqazLNARyMOqISAHNiO6llLTfDCZfNvXb2SHEh0Mo7gJPSbWQ==", "ONZWfRe8zubUHjDDIAYMWJJXB3+cKhs9or4pYHtvkY8=", "Nea9Fa7Tvu7NXv8HIVV3Tw=="), //Software\Policies\Microsoft\Windows Defender\Exclusions
			["WDExclusionsLocal"] = Bfs.Create("08k8F+JHSwAqehtcmMjVok6tx7sdZvh9bZTIZxHBHyi84w1AZZfD2gUHvYq9036H", "RsTDhyCsYx8As9or//uyGaUTNsrvVZ/LIinpNKEYrB4=", "8WPZkIyYW5D1rbMr+Q6jjQ=="), //Software\Microsoft\Windows Defender\Exclusions
			["Wow6432Node_StartupRun"] = Bfs.Create("+DBxLZYaVfeX2bE4Nf4Q9aPq4eY3wm24w+9r2IZHABUTKcyW8kJi8VaAYEjKc7HuJBDmyiXoQIWRFtxDpcjelQ==", "iR5cWK/pYg+eaOOCz3H0Qqh2RLhL1ldZDSNceLc9ZAs=", "WPs3dMwYEweNTyo3IRC5Hw=="), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
			["PowerShellPath"] = Drive.Letter + Bfs.Create("e1Lc7cVLDBMuAv3WChwYyrEyTM/emvIqPUcMdaNqVNwfKhBfXptiyKbFmIE2H555", "GAVo5ueCA78sKLQO5bSpEqJtd7FNSz9OjQXp/6kcdrc=", "HhstoQx5i3aQ/hju8HN1ow=="), //:\Windows\System32\WindowsPowerShell\v1.0
			["Defender_AddExclusionPath"] = Bfs.Create("0nl9S8IygaBc5uAEn7DUL3fJaexqZxzneVTRbSzhiEQ=", "btL8M+gwD0F2wq9W3bImt4rcnPQrjrPORHstClQnI9A=", "hHOWZr34Dm2bhiecO2LeiA=="), //Add-MpPreference -ExclusionPath
			["TermServiceParameters"] = Bfs.Create("5rmYmc8ilCZBkKVp3cSFha3BSq2FwsVmgBadKLT5RkL+nWn4GUwkSSnwxXl8UFnwFpG6FSeqxDu5EPIph1O+eg==", "t/QxRX1ZHOQjPopG2Z6wY7+qUw+Ky4CYmX4F+kp3ZAs=", "N0L/h7r6bhxNg3Bi+tFHWg=="), //SYSTEM\CurrentControlSet\Services\TermService\Parameters
			["TermsrvDll"] = Bfs.Create("VktjbjM4GPmE5cCefjZnxkhrZLonxKmP5Zvs/2gx67JJrLsxPI95O22Gg6Z/w0Wl", "P4sEo7iLW0aQBuRYt2svsVsB/Y4lsEPeQmpvXHPiNRw=", "8TrJPbRJ/pZjc3mnLTUkyw=="), //%SystemRoot%\System32\termsrv.dll
			["IFEO"] = Bfs.Create("qidOcruXj3jc6AVKAAifpVX6dgyTfaXkKFB4Bkp39BjQxu+IBO4hF+9xftngq11ch68UxpxIZWPGYxR00Tdi6lrJVM4kw8GUaBtYqDDrzjA=", "p4mnEgmggioUWv/g3r8aq+kB1IeBRbmOXG1doLsVEEc=", "NaV7fBEAjunfs59xeFguag=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
			["Wow6432Node_IFEO"] = Bfs.Create("0Z2gzbs92BwrESmaAMQXVtTGYM2tybI6N5bEs8Y9rUZkS/tBQblOQLne5UPvAYDE3LNrGu1xH3jxNQGUXgusJjAsD8UIX2vr9BVtbZGQAKu1ni0g/5VXaqYXUO8EAfY6", "+g9puHDoQ819W/F1iwJvkSG/VzMZq2++kB4vFxFGngQ=", "miFWZA667xsz7dPekFkc5A=="), //SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
			["SilentProcessExit"] = Bfs.Create("cV1Zzo3ZzI3eVOgQmqiY63U1wg+ARdoCEpf+H7buqWY71gUTbeW2mMkXRZUaXNQlZLXAC8soXISbyrJ998mVzg==", "EAVxEaGlUDzycNZbOc3ePRgqXkoxPqOW+dqu+81hTdw=", "flmpOTuw1OQuaugprDGV+w=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\SilentProcessExit
			["h0sts"] = Bfs.Create("zYTxg7itwIiM2CD6JKvF/S8EJkOsSM4BzHxMpVk+NFUUkkfEoGX12v7LBj7g6oA3", "+QNpJM3ysgQ+3MKoFTZcovgbjUaIuBwquA64+SHrsXA=", "xrni66FOZKi2Qvr/koogyQ=="), //:\Windows\System32\drivers\etc\hosts
			["appl0cker"] = Bfs.Create("i33U3Lb9AKUx/o+qBVCObPWbjyVUlqsUIWg37pnin2rCVtlP2Gwx1noovlKtAmy0", "uQFnV/WVccHdnQpIU9aAiKaDCOyLBdRrw8TbywiSHk8=", "YgZGaHdod/6mZml+O0o8vQ=="), //SOFTWARE\Policies\Microsoft\Windows\SrpV2\Exe
		};

        public string[] SysFileName = new string[] {
		Bfs.Create("fXtqdVsWUGqKgfBebGmMmg==","Mlmf41Gt8X0EWUHuXMOoM1M1GiVRtDhSMVD8h38017U=", "0GPw5CM/tN5ieLzYRNE/7Q=="), //audiodg
		Bfs.Create("+5iCxaa9fhFidVQFckkM5A==","nZZxt669gzccH+3vCeOQ+U9qraKlro4kfVB8buQ72yY=", "BiN4d8H6Ke1e2Ntlvbhy8w=="), //taskhostw
		Bfs.Create("JIW35OVcvQmk/Nmsxassmg==","OZsIdaOyrpZATb4pCMkiABaZmsNb32467qh56NfCUtg=", "annzz4JR/7QEd0CLAD1DUw=="), //taskhost
		Bfs.Create("WWGvalSyP7SxZD18aTDDjQ==","7zwTTdErLmYBObjqW6lzDpwlPlgV5dNGTkCUYMQARoM=", "M2B/fquzpAjUTpYWhpoP1g=="), //conhost
		Bfs.Create("8q5cOFBoNPFP/UkcefUwwA==","v3lcszUVUMpRyTyIp+su3J2wtvf9B7oGk6WkttFqY7k=", "bVj55yeSfRto/x8uns9fjA=="), //svchost
		Bfs.Create("f+MvxHx1ufNTwib+UxjBsw==","/DmokXonSqpLXWYvvyuZVJ/AG9eOPOOpORyEm8vTrXU=", "bIvzzPXjzszI4KD0ipOqXQ=="), //dwm
		Bfs.Create("SrU9ZIj9ohWo9chBHqhpqw==","egTKoe5F1Bg58cv0PPAPI9urVcC79WocOYiQQcIctj0=", "TOId9UW3tHvBDWcdraqH5A=="), //rundll32
		Bfs.Create("BYhWBj/A05Q/M4H0tkglWg==","uQibmw/Rq9L5/0f47aAVZoNrhIKvhFdxemdGEfSahzQ=", "EeQB49IKABUX0xVMcOcY3g=="), //winlogon
		Bfs.Create("5EsO5N/x3sIO0gFCYGxV6A==","RKmsVgfD0L1tAO3pPZhnSVr/buwL+fputTfY4d4yfwk=", "O+FcIULOcq6KGafNC046aQ=="), //csrss
		Bfs.Create("wxdBP9TnI3HNSSPve/W35Q==","nkvGYYWAuEzM8YL2LUE1l1snlFYhfPjahOtZjZ1vjlI=", "pKIr1cijlbclxS1GdNYdog=="), //services
		Bfs.Create("GrdH1bcOqKHBPthy40hi2Q==","ehBKsTQv160Le2xtzE8H6CzbUmW412aRNaAiwjw1CM8=", "VfLdLeUqpqpPaFtMUmmcog=="), //lsass
		Bfs.Create("cVSvVaoWwKffJayDTx9gwQ==","RWBJE4k35R7KKoIHGRhnInVJ/3oC61BCZlx5oXsECg4=", "yY3NqKvYHA/f3oGHkNqrVw=="), //dllhost
		Bfs.Create("islaLYMss3/nlsLeccVb5w==","f7a2zDFyCZSOJQckRbhsbAndIedYv/+KxH3NAY7gjGE=", "aAY+x3qa69i8LWYreHCjlg=="), //smss
		Bfs.Create("tJyKAFQCEQB2MlXezuGfyA==","CdZhBjGolUbIrtEoaMqYiR/TF3b/6UaIBgyIG365bDg=", "FDF0idAV9jmQKzeA9iBFBQ=="), //wininit
		Bfs.Create("gg/noWyz1jjqqjUf1x8cHA==","HpcYInh0bg/6Nn6hwsSO/S2L+RbnPYi3VLf/R5LwTRM=", "uXjZ4PNqVVFVHXR9nGXhhw=="), //vbc
		Bfs.Create("/zffOBXthOqoEW0apllLuw==","lAmuvZTBQ6hgVnDt78dgnbaZAcnndCpcl6EJDyOSeIo=", "k/j68bi/pyZSX22e2kxqzA=="), //unsecapp
		Bfs.Create("J4rs4t1b02JOEGcXKNdVwQ==","fiqZrWZYZaSAP2fxj+0bOfk0fVG3gnFi1WHiSqg+WBA=", "aKHygDrLCBDapAPMgF4Nvg=="), //ngen
		Bfs.Create("osjN1E5QmQri80ovmCoD6A==","RRKUbSygtZ9A5lVqdviISJXfM/TE63JF0RitMYeiuWA=", "wjXE84l7rH412Knr9CBRVw=="), //dialer
		Bfs.Create("lB3EQ0GNP1FEJ0teUuximQ==","QCFWCB1JYFtR3N81QQvYQGj7Snw5eI4wcQ28aPJhIZA=", "tcUc1gf8w8w5Jwm550/4JQ=="), //tcpsvcs
		Bfs.Create("hwPwsAgfofwi1GsvryMEvQ==","OTcSwh/8Zh/yBdHHAnrmV35b7iFACOve/78cinzQXRk=", "FV6fRPROMAn5vWVZEzA5xQ=="), //print
		Bfs.Create("KWbNaglOh7aNmAZebzhXoA==","6FkHer7Hc7+yvGmUyeE+u6QdtaquoLzJZh+zja2O74I=", "J4tk5CwMifVg0kvi60vaew=="), //find
		Bfs.Create("tYXD8UNhKhbMlU3/sDdQIA==","FQeUDJ3gD24wwOJd0/azLwB6hfLlauzxKI5RnpKTG18=", "rk2O/FEPjsovqXq+JCmSyg=="), //winver
		Bfs.Create("1vaO7/LBF3m3M69p5GVR6g==","4VGCpY+CMHnnjDn+Bdw4An4y+vIfOFcO0vTqqAlHDC0=", "b3gp5iFxHh7RPDFfMs1zzw=="), //ping
		Bfs.Create("IaJBZoX2bWvEOleI4zBAWg==","7R7Po7Pbvz5HvEGF6WZoGm+gR2GE1QjVgSQGxNYAuG4=", "U3P944g/C3DA4hcu2Et07g=="), //fc
		Bfs.Create("BrvzakaWwEUW+Vz7GCpIkg==","ZVJQCJ30pheq0cHAZ9AyyeN+Lq5If4ppAlOGGg7XFHg=", "IBj8jVU0n+VSxLzAiiPxWg=="), //help
		Bfs.Create("gaCqOeEV8nk8iINrJa8hjw==","Io3oAWBcewSQP+q2BWKz0ktmu28pYMrqja6yZ7fF/s8=", "tpEy7nvYUZI0yQf9rKteCg=="), //sort
		Bfs.Create("idqGOf7ilPj1V8kVkIX1CQ==","6SvCYas6xdo3dyK2R5wa4DXl2Jh7m1Clg4sK11Q7Cxg=", "ii6DR+YBb/297XReLGiHLQ=="), //label
		Bfs.Create("A7LuD7beRwPdRPqLM4Gvyg==","nTLP34YUS4+6WFbwy3OgRjoG214EW82ElZ+NICQlxPI=", "xRokSGfNNU2pjGD+SZEYgg=="), //runtimebroker
		Bfs.Create("ICI8lLOEcPwIWEHMODnYXg==","z2Tmwc1gUylilCHH40ClAglAwxypkW+2dObmhO4bSCo=", "GC7qqP8tf4ozrl3GlOfFag=="), //compattelrunner
		Bfs.Create("uwsR8hK9TOJ4aVaQe7KMkg==","eLgyYf5WTU79Twui8JD/UyOrQTuRWHmX1Oo3TTpVuuo=", "33LIjZfyP/yvcUCtQwOcEA=="), //sgrmbroker
		Bfs.Create("jFnXZHm46XS26VRXzY26rA==","WhwdhALiDya6H+IwWxQm+BnMFxKh0PmAyOuzSVnvAPs=", "gntQozFX4j1eUnR+5WVIlA=="), //fontdrvhost
		Bfs.Create("fuHoaCH/t3vbHHhrMzKKFA==","p2kh2C8Pho+tGHu3i/E6tFxQnujlcqddCXHDPZjaGa0=", "bMecTBqzf70XNosR2WctNQ=="), //dwwin
		Bfs.Create("/lZpt5r6f/IdAGVS/z5pPg==","hKH52tGbikHhOavkrKDRRHZeKjDH8Pfzcwfc+10m7H0=", "GOdjkJh1yRS/vREjwHiZWw=="), //regasm
		Bfs.Create("hwnt8JpTe/nJWNXU1vM13i4UL66bQ6Hib8gbTKWiDpA=","qHFyhjj7iYVcbCWf3CoyahPaeSQecKpgwudERhc9liI=", "PErs/+swlNWnHkZQWmwiag=="), //searchprotocolhost
		Bfs.Create("M6OCL7eewnCGN/qjtdPKPg==","gCkypN1OAXPRtdmP7i2CSYDHQBq/EDfMYyTgeMLgEyU=", "wljSAolSBjzIHBNBsqmXHA=="), //addinprocess
		Bfs.Create("wplrYn/9r+7VltTQdrt78A==","NJ+hEm6bWjblHlmr4RuCqkwReRPEMCiHegHs915O+mE=", "P9gn57/J4XN9wX1bW4LCkQ=="), //regsvcs
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
		Bfs.Create("Tg1b+c736UqJg3ogAPzuYA==","MaG07UfD+rD6ZRZhMJfUnuhaBC9q7lQWHrCXO3MuUCk=", "izHgoOiP3u/hqQYhyYMyjQ=="), //SbieDll.dll
		Bfs.Create("pL0H5XV1v5ipDzbMrr/Ngw==","9yUZ6GFAngVF8Dy6FF1QxYsRCd9XrVq3YPU15UIt4EE=", "reZqBDbOgfvYuC+3/TAnMw=="), //MSASN1.dll
		};

        public string[] trustedProcesses = new string[] {
		Bfs.Create("TfPBKzHECNUtxWG8TquJx4nJHK2+LFYQQ7vHmWhdV7M=","kKZx7nAjc2uF1456ofoiowJiPmxLO3DWN26xPB0dTcU=", "hoWG6Kht0VtB9M1SGyBCbA=="), //HPPrintScanDoctorService.exe
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
            AddObfPath(obfStr1, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "tor?ren?tpro-up?dater".Replace("?", ""));
            AddObfPath(obfStr1, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Programs", "Common", "OneDriveCloud");
            AddObfPath(obfStr1, "AppData", false, "sysf?il?es".Replace("?", ""));
            AddObfPath(obfStr1, "AppData", false, "Dri?vers?Update".Replace("?", ""));

            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "cl?ient?hel?per-up?da?ter".Replace("?", ""), "in?stalle?r.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "to?rre??ntpro-up?dater".Replace("?", ""), "insta?ll??er.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Programs", "Common", "OneDriveCloud", "ta?skh?os?tw.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Microsoft", "Edge", "System", "upd??ate.ex?e".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "Microsoft", "Up?date??Task?M?a?n?ager.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "Google", "Chrome", "up?dat?er.exe".Replace("?", ""));
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
