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
			new HashedString("1a34d8272348282803adbb71053d241b",22), //download.microsoft.com
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
			new HashedString("ff5c054c7cd6924c570f944007ccf076",13), //microsoft.com
			new HashedString("3afb052104deb95bc99eee868c8644f8",18), //msch3295connect.ru
			new HashedString("974bf1d93d81d915800bb2e5352b923e",39), //msnbot-65-52-108-33.search.msn.comments
			new HashedString("f6ce7e3db235723091e59a653e7d96f2",9), //mywot.com
			new HashedString("4a73bdc9cec00bbb9f05bc79cbc130b4",9), //mzrst.com
			new HashedString("3d62ee7e9bada438b991f23890747534",9), //nanoav.ru
			new HashedString("b0655a2541be60f6b00841fdcba1a2df",10), //nashnet.ua
			new HashedString("13805dd1b3a52b30ab43114c184dc266",13), //nnm-club.name
			new HashedString("4e42a4a95cf99a3d088efba6f84068c4",10), //norton.com
			new HashedString("84eac61e5ebc87c23550d11bce7cab5d",17), //novirusthanks.org
			new HashedString("40ef01d37461ab4affb0fdc88462aba9",27), //ntservicepack.microsoft.com
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
			new HashedString("3dfef91e52b19e8bc2b5c88bdf9d7a52",20), //update.microsoft.com
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
			new HashedString("2e903514bf9d2c7ca3e714d28730f91e",17), //windowsupdate.com
			new HashedString("61138c8874db6a74253f3e6472c73c24",27), //windowsupdate.microsoft.com
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
			Bfs.Create("z3+Xdh9a/9hkXxdj/gThDw==","bWmxmn7/DaeVxKh8Lm6yn8LKhz0TZqIGo4ch2MjTQXA=", "c19otUeco/KP+7HXrKHjGg=="), //ads
			Bfs.Create("NwPCcHyGCfig0gFnWzkJpg==","9agt5XgJHkgiiIOAl1lq1pGU1D7QW+nVBcNB8Mk2mx8=", "A+I6oiVN5TSf7V0K1w7Czg=="), //msn
			Bfs.Create("ZvZxBrblOUDoRFhsW38fOQ==","DCX5qpwBiFOgS2260L/5RMOdWnZfz0+RRdpqiA+FXhk=", "2XpH+OlX+jqB+wf4Wxe7Sw=="), //ztd.
			Bfs.Create("KeA6pr9GsRahionA8FtLlg==","WH2ZQ9c+vttBy3feTchzMjUa+LtfzLGhxTE2RIB66uk=", "mO8EnZFES0sl6jAk9OjtOQ=="), //aria
			Bfs.Create("UiYNioQ7LbFMDUgHDUYGkA==","nbMHYxXxkX3Zv09wKhQ2EDQsc4+ywRGheounDongfw8=", "Ih4iISgIJgXvHa9waodGtA=="), //blob
			Bfs.Create("iSpQk9cYQL8R1zWfdfqTUw==","6E+9xE1e4ReYFHaa1DHzJsYgmLe88AGMdnWmjKxUA2o=", "JIaXjlm2V2mL+QJUH6jzZQ=="), //llnw
			Bfs.Create("F6feR6suz7Inef4LhbTmog==","Cnzy71yH1okg62U7NHFtCqYSTPC1AGFzKTGuzcZCPR4=", "vc08tSFPjMYjM8CsjW0bDg=="), //ipv6
			Bfs.Create("4Q5SF36B4oaqiWN5yfRz/A==","ACvnkjUlUlbCYCS9Kc27PF6sWp/6TrXUCY/qVjOZ8+E=", "U9Pzy8gzXELMHazVvuc0NQ=="), //.sts.
			Bfs.Create("UEu8vhq5uTpxnm1MlR+Cfw==","3f8lN0X79v9DwlVtdnR/2191a/D0ag6/sEhvwLGwwvY=", "Moa4eKHq+vgOCFwA9rGTGQ=="), //.dds.
			Bfs.Create("p6HSyX4V8w9kIQR0pg09cw==","5h7rBZ/LUXgKWSSLQE0oo6KaVRk8oIgogh3mJ6SddxY=", "0zW5O29PwwCzGjmCqh0XRg=="), //adnxs
			Bfs.Create("xqaisNWjX0iy7/n9uU5m2A==","QBSrzlllHIvg2Rj2Ch3CL7RFFaVPPkPbEt5ql9cHqsY=", "/UK4lKB6YwTPrSQP3Sgu6A=="), //akadns
			Bfs.Create("z51hHGTMiC7gQwTkuxWKKA==","OyEt6WfMP3Yt7+K4rcnVUu/gAuEwQnr7FtR5wkJ4oaw=", "3ljyNII7wSe9J6ecwHUwpQ=="), //vortex
			Bfs.Create("ttUYkGagzNI7RKhrAMRoXg==","skVtliZdEItEWDkv9OBwgaLE9EPl/FbdsXWkG6Lj15A=", "uRHTCCXYklkyVUsHyHqExg=="), //spynet
			Bfs.Create("MOFzjPNxw4cTFl9ZLqV5fA==","ahlakNdrpdlfoQKTlRvw9Su0+peaZ/Wq4p5b9g8U2kc=", "z/jx3R2YKKawHCZmNqYvNw=="), //watson
			Bfs.Create("PpsaHvTbNTT8klvne30F2A==","wntzBVofjYLtwmmozKMv51tc2uyZaJD/mUr6W7++NZI=", "/+P0ssBcKDjselRnkkEypw=="), //redir.
			Bfs.Create("WslOyydYq9X1X2dk9f+zpQ==","R6u95h/cppwZB150wTNHFfayEXvgA1omM+UF2WnLcSw=", "FCJqcHtd4S11fwZneLUYvA=="), //.cdpcs.
			Bfs.Create("IHcBuKk96ynDySeYgUfUQg==","hShTwdM8xd/8UDXLxiZdVP67kpbE8aM6Q8EjM9rr/V0=", "5dxiZhng6J9JrcJ+FyNdNg=="), //windows
			Bfs.Create("0YSuvklp7axLe+l8Pd1HWA==","Qrfgi89A85mpw8zHl4Ooj6YVXzxfZCSWew4XNvJysro=", "ImE4Ko30Qgydcre+WL00bw=="), //corpext
			Bfs.Create("1QueFlbiIIxnXAF3dXK2uA==","5tTjbVXkEwHUk+BgO4GPmBBSoMrKn1DaYnfD1ZakyeY=", "InjYk0g1WtWXHCH2iWGQsQ=="), //romeccs
			Bfs.Create("p3PlzsDIMtYbn/FClOejZA==","VWOdfHT+E74retbzGkb6a8cz8VPf4ic/0WrtgfcGWgE=", "YS7KVg84WdEs8Gs7ERdOBQ=="), //settings
			Bfs.Create("0LyZ8oyoqffz3tBgKpHXvA==","NX9xDAfxt/kc6cHYQiWsK972fKkit26dOBiUjj/LmJE=", "s2ZfUxjDlfJiSh/uv7Q3RQ=="), //telemetry
			Bfs.Create("8d8NcxFyj9F5+GHuwGifaQ==","IFR53aExKfJ22RrxehSWbIAVgjXMnPXVZDYXXs1Gcv8=", "tkftnXzcQVivGtrt3XPwFA=="), //edgeoffer
			Bfs.Create("RJeKBv0Pb3Ww3DC9sSfyHw==","EpaTaeu4ys0MDyOOXI06BZ61R1p+3TfndaWajRaGXRE=", "jIdTeOuD0X+Pat4VlPOI3Q=="), //do.dsp.mp
			Bfs.Create("wf7N+h+kUZ4977rLHsg0YA==","BjhPlxcM//cESz1U0dWJPke2m+KwQuvRf3zd78bEhTw=", "ZhI2LcnNLWCqAomnZYmMqg=="), //ieonlinews
			Bfs.Create("pH/APiMjAhtx/gzqf5p83Q==","3fraLkEzY+ns/GIgb2Ar+IFcgVRkWZVlDJpAIo9+pGM=", "EFDFpH4uKXD1SMObhds22w=="), //diagnostics
			Bfs.Create("VY3JBl03PoDz6PRhvulzxw==","q/ak22KNTtv4+Zk1xLueUcJP29kGOopiMs8niNneUsU=", "XYAPSD8fhwoY/xqt3qIqbw=="), //.smartscreen.
			Bfs.Create("3TdIMpuo1KWuObDEdCWREw==","UYswxJgkCbDctbJMbYqdt1k5EEU2qokMWRTF4yHs21g=", "3GultIhJ8LLd8Merhxdh3Q=="), //.metaservices.
		};

        public List<string> obfStr1 = new List<string>() {
		Drive.Letter + Bfs.Create("wsoF4WeZ5GsBzOVwoPvTnt77B7WjfWYjgPxwd65/7sQ=","ZZKUd+05n75LTf8Bgyyvj6/Axk44o9K90gK/oOo1n+k=", "NckBqt5RAqoIQD35v1BZUw=="), //:\ProgramData\Install
		Drive.Letter + Bfs.Create("PkN5DM/ow1td6ERikdo2p/mc2IRowA/bxwU0JIMnrKo=","zTKVZYQ8qgO0aAV9LJxuAMTkb29kMLstW+JfkS1W/7Q=", "qmlHTIo2tnFSGRTxRE9cQA=="), //:\ProgramData\$pwnKernelSystem
		Drive.Letter + Bfs.Create("3uzI4wOVTo7SshdFN2bA2Xn7UEMeYeajW3E/vaJHoLw=","v/nUJwiaBV8aKyMjz/9t+F9FSesM9WxaNeQ2VBPecSI=", "cKjzvwRjAWFh4ifUEKNYfg=="), //:\ProgramData\Microsoft\Check
		Drive.Letter + Bfs.Create("sufAwskJHEn6ldbXWNnBPZcmF4YZeMJz3/SWYKwBRCo=","1r9OSjtlSMYE38xDVzH6qqJ2gZ75QgHaJP3gfP95frw=", "5+1u0KIM300XhAcnrVACyA=="), //:\ProgramData\Microsoft\Intel
		Drive.Letter + Bfs.Create("F8I7XOpargwZB0upgfsJuCEdgiQTvoQSH1uG/CXZPDsj5PeDrdsvWggS6oMycchDEYtL6iY/RZ3ZHF8UUeBWMA==","huV0plqJ6a1yQQHK7pBnDO+3M4WSpbGlz1JJXe/4hi4=", "YnTNQvjg+Fz3D3uJovRdDw=="), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
		Drive.Letter + Bfs.Create("okgKxM2OpiO43Ixel9vbHQ+D5nDM/YCApR9yxFcEcbg=","ntb+ybFcL2qthjwaWYBGrRZ+6FYLganSS+kmCwt6AI8=", "W2BNdBv9rwPT+q7R+Zo7Uw=="), //:\ProgramData\Microsoft\temp
		Drive.Letter + Bfs.Create("WJEvhFiiumVhng2/7dzFOvbZjJbyV+d0laASxE/u4Fg=","fcI6ilEGwxNlWuGFrbg+ZL+B4GZ/w9ytc1saboMjLys=", "hDCcRQ5ZK8JLDxzJaWWV2w=="), //:\ProgramData\PuzzleMedia
		Drive.Letter + Bfs.Create("OjrOwNyBKkroBTlmMU+uOoEb6+gzxxTgMy6s8csSVjA=","CB+l0Bo5wZTQz013eLOmhfBdi3WHZEC1tfVZrLOZd+0=", "Do/I7mgGHnvbQB87XBuKfA=="), //:\ProgramData\RealtekHD
		Drive.Letter + Bfs.Create("ruMMkFYUYetaTrLswmAaOfbRpUB8XQRNVoQmg8tydLM=","WIDf1eGKZ6ShDQVHk/QkEobobFlkljHS4y4SNbEwqGo=", "x5uGSNFC8OD3gK8s23fOEQ=="), //:\ProgramData\ReaItekHD
		Drive.Letter + Bfs.Create("TC58tn+77gCDDoFVrn6rJUB2p98hyoBzYqxzOX3BmcE=","DeSNc9W+L2lNVqrWEbw4sd++2eclkjiw7U8M3ukEKiU=", "FXU10U9BMB5UAiSOrZbMPQ=="), //:\ProgramData\RobotDemo
		Drive.Letter + Bfs.Create("kr8LK4LwngNFR024baoR/PuUvmoHN4pbrbu6HZDYy1o=","X+GwBjxlOtW4yxoL95Jby3pTciFRo+K4THg3NgHIu1k=", "ChVMOVimsYITY6+hbSgDbQ=="), //:\ProgramData\RunDLL
		Drive.Letter + Bfs.Create("fxpa+YKu/GBS769+KxF2AAXY1VB7FBGHaduHtajQb2o=","SQq8lWpNFGclaRlyBtZcb51XgbideRD958JCN8NAPAE=", "FW0DLJB78m/7R2GgS1/k3g=="), //:\ProgramData\Setup
		Drive.Letter + Bfs.Create("dPsfZ55Ye5YkOA0e142y07jIPPGeZJvzYxzjoFfnwxQ=","JHjqWmkywcazLZ8+BJdBvsc9aBg5nGNsimuo65bPk5Q=", "yoLRc1nL77+2moscA6lWyA=="), //:\ProgramData\System32
		Drive.Letter + Bfs.Create("vRU0LedDcUFYVXIqP0tvMobtI4XQ9WHmLJj7FC1/+zc=","mEF9DQQoablm52cKnqdahIIPnEvIKnvxnNjuMGXIW/4=", "YyDGPyin4wIG9oB6KHs3qw=="), //:\ProgramData\WavePad
		Drive.Letter + Bfs.Create("AtgcZOVYSX7/zg0LseDserfJbbKnhwb4qc/Gq2dznok/6TPhPeCQqDsEzg2smaOJ","0Hrb0BtBkc+PLBARZlLSBxKbOzf0/MCuwtuTZ7L0c2w=", "N33ewBMyKHcI1evfrPfaWA=="), //:\ProgramData\Windows Tasks Service
		Drive.Letter + Bfs.Create("KpgMRzeqCK5Sm4l3O230dgUMg+DM3YPwCAJdMr3Agig=","H6Nida2413jeJsVIJdIxtfKLnuzocg1A1RF7n0+movs=", "fBu4KwUyE/dOnRST3gV2hQ=="), //:\ProgramData\WindowsTask
		Drive.Letter + Bfs.Create("6thVoHFqiPqXdj4v7mfN42r7Cnnyhzl92Y1FAqAouqo=","i+kCybtk64Q/woUp31cGcH5Mmj81Ma2LYbcjct8MqT8=", "wAboC0RTDoxMyVFzztbwEw=="), //:\ProgramData\Google\Chrome
		Drive.Letter + Bfs.Create("5Z7O8BaJXkem4Gn5XtyWrCC1IyfRP+Zgtj7DlKy+kXJUbrWSAwWaxOsfhXCQWdsi","fcFYWjHKmuwtzmee0GemcXq7DMty7t4Y6JR7riGEOQU=", "w6TPNZZQNfSxX9LUImh+5g=="), //:\ProgramData\DiagnosisSync\current
		Drive.Letter + Bfs.Create("C8t5Xp1VZgp78b2dX82Bb/1/nL22lpumc0d5dloCQic=","gWyrh7L/k8UdntlMr6NhJioq5G5pYPWWXEeN2tmhrh0=", "0wc7S3PWlFjeBZXJPvv7cg=="), //:\Program Files\Google\Libs
		Drive.Letter + Bfs.Create("9gQHSOyV1XXtIcIW6Ls6KbcOAnKv3r++f1fXHzaCrfg=","iz9/K5OQbVeY64ziDTiSBjGw15l0APPbSIRdj+zJa0M=", "nFgZqJYNt6CRtjjti2gsRw=="), //:\Windows\Fonts\Mysql
		Drive.Letter + Bfs.Create("Cnu8PsQIJ5+6Bj/+h360wmmfoR7wkrU7yHpFZ9R1Z92LHtmjXJV8siQdwZzaiqJ1","rK3UKJuXY8LVQkTAQfrFiTOAKNtlg+vriRjP6LSTurg=", "6WMrfjWQDfvWfzxluaQteQ=="), //:\Program Files\Internet Explorer\bin
		Drive.Letter + Bfs.Create("Uz6g/n5+mdJhvY7pGsZ743asYuSfCaluJ5aLnrBvVgk=","2njwddYxFN4L9x1MVwFfCRXpGnZbzh1kQpsnLe1Ngk8=", "gsyYxHPFCzKwjY3uRy5S3g=="), //:\ProgramData\princeton-produce
		Drive.Letter + Bfs.Create("N7R283qbPd5YOxqYCVq0Wpswl7MNc0BGFbU8X/lEc54=","qGXvxGTAKOr+6+nHZAfgSv9VN7xs23ynl1lFU3nMjGc=", "6BPug8qsKeN/PSLmAG3P7Q=="), //:\ProgramData\Timeupper
		Drive.Letter + Bfs.Create("2nZUfLASRRbp6BajkMyJrb43RElDyiMDm1Y3DP52vEE=","L9+2l+s+zpKU8K3avf4jOUij0XtlatQfP6bpR5ONsdg=", "WD5WK+DgZOLCBBRm/pZ1cQ=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("gWVT75UTpWJ4wXK+sK53EQdYJ3+Aezqiv2+ENDF/IqA=","gxDVcY1Ew7kx2RPCNWF6eoH0pqljdHAHQcjJROJsQw4=", "W4zUEVbqhMcG3DuhLhvKcQ=="), //:\Program Files\Client Helper
		Drive.Letter + Bfs.Create("z9daLsqMwv2mTJTV2Q49z6LodOz8YkrB1IVqtX611Eo=","KEo5n1xqivjyBu6AkzNVToFrl4PUtBS97QuqCn8Kw6o=", "1s4OKcQCd46e0AFerChenA=="), //:\Program Files\qBittorrentPro
		@"\\?\" + Drive.Letter + Bfs.Create("FFTQUwGeaTW+DK14AuIJSP9Q+Kk7sKnKwdmbNuUnqQg=","TYLoY1peHvzZtW0wR607esYVOHOSKd53Twhl5Eoh5EA=", "GZUkyU0h3dLRRZnDe9VVTg=="), //:\ProgramData\AUX..
		@"\\?\" + Drive.Letter + Bfs.Create("NV9NqiDkc1IbUDbh96iOWX7kMN3nXXy5x429N81Kfkk=","/r0jbTPXdBaaZ11EPX3qi4NFrMqPS7+ZvYCtfdL0RJQ=", "dUie5mV3iy8I8tdCr8NejQ=="), //:\ProgramData\NUL..
		Drive.Letter + Bfs.Create("Uh0ID36IIX0pIkLJPhzXLF1NP9NV68RGc/TNfpFiPRQ=","tQ6eMT7RR0vzQjamjXIcf31pcF94sZfdD/F9GFPl0nA=", "KTjJWSJKp+O7NIzGXiz3iQ=="), //:\ProgramData\Jedist
		Drive.Letter + Bfs.Create("w37jzaOCblIUJNY13i6JqdkFyKyMUrSAN1pQj97M7k6Q9wtcn4bn27J5lRNKqiYw904x15JE13bt1L+LwXlfzg==","afuBVmXZhQSfUPH16C9TZIzVMwORd6cNQBH5iVCYDvw=", "1uY3cL4vHTQmpqM3fBM1Ag=="), //:\ProgramData\Classic.{BB06C0E4-D293-4f75-8A90-CB05B6477EEE}
		Drive.Letter + Bfs.Create("VxA1eozu+KYyQMxVpnihIFf1dZOgi+KsjtSYIFdr5XcpE3fsYNDEqAmL1uKgUJNB8I3yfH1YT99flieGB1XQtA==","eFU53Hpm1dIvTz/xUo28aOB2rd/M2P5eQNQKeE2eRDs=", "78lg3UdpbRyjqb9wOsRW8Q=="), //:\ProgramData\Classic.{BB64F8A7-BEE7-4E1A-AB8D-7D8273F7FDB6}
		Drive.Letter + Bfs.Create("ed6UK4fV11tS8fuaddwxQKxpmD6mF2sYKgwDJvs4UEM=","AMJ0eMxJXhtHoLeRxK/73H2qFavy9I+LHvq9YDJhkMk=", "si0KNrwDwupU0yuHmU+D+w=="), //:\ProgramData\Gedist
		Drive.Letter + Bfs.Create("x6pMJihbWq/zx10RRUXrfVh36KqoBTQZfinATkpzGkg=","2PL8abE1Y/F2QNEWB3wTjAPd4sTsUYVq0I8LEHvqZ38=", "XpWirGdojlzmFtuz/KNkFw=="), //:\ProgramData\Vedist
		Drive.Letter + Bfs.Create("Z0Dzx9AXgAACw1KdnyM3CMt72aYxX7CPfEDyzkIDm6w=","KtVZHcy4Az8ntjSeJzyf5eFkBy1XzDEw9L9E9TintFY=", "adBeVsMU7bLzyY6rLFSRSA=="), //:\ProgramData\WindowsDefender
		Drive.Letter + Bfs.Create("eFArZZrvocTe4lgcgkAXehvPitRdnxUeI7Gfo5rGkXo=","/cEaTQSKuLU4gdYzNArQ2SprbHTrMa7ihC/U52nmmYg=", "SsVZxpsL36RR56aoFL+ffw=="), //:\ProgramData\WindowsServices
		Drive.Letter + Bfs.Create("2HVUgqIju+KEQcf7YA5L9HacB+BeJZIpo3QvTckPTo6Xg/deyO44ylV/QOpXHlHd","A6MOuwCpPLBo0jHj3O1iQwVGwU+ZXQWZjfapPttojnU=", "UnO1OBu6tfg59D2Q0u6VIA=="), //:\Users\Public\Libraries\AMD\opencl
		Drive.Letter + Bfs.Create("c47LEaw75KJr2QSNllq8HrJf/EJzte8akT95qia5c7MoolLen1K07chdK0M4D93j","z3tdQtlOsFyUO0HulWYcolo1Gm3Gr/ELQq/LABs/H5E=", "8hMDfvb6CRwul9I4Fi68lw=="), //:\Users\Public\Libraries\directx
		Drive.Letter + Bfs.Create("DopuhzGabiiXEZ/kN50/UmjWmh/hhswZDxwl/Ei3sgA=","pULaw4UO1qqMs1vjdYXhKiOyyo44yQycf3vOR9w1tvU=", "+yp1rSP52Vgma/Yx2MmI4A=="), //:\ProgramData\DirectX\graphics
		};

		public List<string> obfStr2 = new List<string>() {
		Drive.Letter + Bfs.Create("5epq4Nf9Bx+lVjOvn0SwIsfwqMpwk8utb9QloOxLg+0=","sBZXQyGaKzdDiQWb4LKISce+ZEpJUkYYsNdWxb88ca8=", "nyFTshd6Lb2PX5oPGv0W7A=="), //:\ProgramData\Microsoft\win.exe
		Drive.Letter + Bfs.Create("XQ4O1+hRlTtKz3Aj0/P/2cOkAlNVruJ2Q/ujmETxzMa2FLtzxYHTaQnJbRuA9ZAx","63Z8Z7IQKieSf4IdlYj5ILdfjnaTTPz6VvXlYaTVvuk=", "s4uI/YB2d59eu6xz7otwHw=="), //:\Program Files\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("1iRbYiuTHYRsaKYutkRN5bZHZFMurKICEop6D5v7U+tYR5E48KvI/GeSCRuLUWd/","4/dU2PpgSqVS1qgB0gJQWBWgnOOFck8d37fhO5OI5Us=", "1+sOV4QfDOSRAKV8N8B++w=="), //:\Program Files\Client Helper\Client Helper.exe
		Drive.Letter + Bfs.Create("YLoTzmVDzFOJ+oUPcjsgmAZmk+LAq893sVuIUCF6Gm4H01zbYtsW4KqnHMVaJUOb0a28qHmWiHCVT4+Ll39KZA==","00X/WJ3lxwju4rxZvR2RBvAFl+nZThVK2lhr7WMpUng=", "WpXvsr1q4UnavX9JYgPB1w=="), //:\Program Files\qBittorrentPro\qBittorrentPro.exe
		Drive.Letter + Bfs.Create("xhlSEJMQa/h3yWB3D4W8JO58mpZSL84cQJfX4yYwy53G/78IlPXKIrVPBic5HYdu9lU7OgHx6X65250VTPOi8w==","uAo6ODf5XxXO2HZlnVty+fNi4hOEdrFz/jBmAUUnSVw=", "jxIvhkYakXYHoyGXLJfdUw=="), //:\Program Files\Microsoft\Spelling\en-US\default.exe
		Drive.Letter + Bfs.Create("nnkhLWf6E/xMY30UFL41l0/cmuAVP2E3Lw+4DkqpnRkrVZMcSfcFBFWB4p/jMudP","gG7hPLNMlu1Y9AJU0TCh4rJ2JuwOfDt3bD4XmpWjRNg=", "GrwYPr2+tJ+Nn52dJ7aWaw=="), //:\ProgramData\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("cc9CVYjCGqrtf/XCYROSj7rWf9SNfe4GjPyTj3esfUNRKIubS3Bj+Sz9YopuXAUm","uK7k2+2rYF7Lp3qIfZuOI53KGT9WBghBHhAdO2NrRnA=", "/JBgCUP58OADQprtEXozmA=="), //:\ProgramData\Google\Chrome\SbieDll.Dll
		Drive.Letter + Bfs.Create("rywHp61BiG1VULqSnaNfmO/zMxdHdZyToMhP3iQJjhg=","0RxdItau91W4Qo//FT9mBsqphaNYVjIjo+L44S62QQg=", "4MyIVeMrvmGzyIuj23Q7cA=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("Rh4CutpIFiOHZtOjpHvzl2aaItqMWnZh8edfNXQZN+TqNs9nL6c/+hsmopKV27kh","nXWbyG9kzwmcWrIhQJGlG/JvWQNiXderEYiGpqfY7AA=", "RYgVRRXkdv5Ek01uzBoc4g=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("NVwOMExNEeFICmHgk4Sg2GERBsDQJqbvyCa649I+fg6Dr9yBUHnp6Jmo5bRJ2EWs","NuP43FI6KrSaLnfX5uYd9x1FTMHWsB2oTbWHlsLHpO0=", "lzSh3670Dp5ufxcN2Ex0BA=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("eyB0gNf5sqDOjGBPFDuoheNBsj10vYvD3QREAoGiHEZ4yW0xvfFrhvc4SJerUWu4","WhDlzfAaSCYNH/GDNLgdve1d3BDdpZ1ALefJhtCGu+g=", "7RrQhl/9ksqWLOWTTrfoMQ=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("JRwXEjsoSHxu8kt3Kcpf0QjzDoa266O89jQi96pHoSPGJaq6Kg4QEtxs5YVtOWjn","nIwOWx6vjYHI1zaLCJDer5Oeu7jGcXlFVAPsxrDuobs=", "O9QJ03Kh4lLquyGf9nHaqg=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("D8q6Syh3xqjI9BRemELbPykUUAoAHivcvjgIGp5jlFpb4m5cax+o3ANbzzxG7p7G","ZrAhrExaGVEJAAQ7HKrN8J/6akKKD2Olcu4E2qnuoq4=", "sXCPc/7fzd65p5akaB9hcw=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("pbEWL1GRIfxBM17wZQkNYEKJ5A8pFzMLV2EiHacwmYtehmzfMaEIvAbJcv4zxUAN","mg4kVSjglbmO2yAcRYMk9/vO8J7gmqnKYYR03ygnxqo=", "JrF8bP7dobgAQmS555OA0Q=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("Mkq/dQlWC5llxNMlYmrrr0FATGeuU1z3bbnhkmc1LX3AUzT/R5Dy/ubB7jfAIZZm","Q1nVDrHd9RH+SOP9IKCo4SiAXHq7F1s1KA2sA2tJPEY=", "ZFOKd5lbjdrtQAVx3HqOKg=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("Dr6pyqiqpHsf57v7z39+Cl+IsJQ9/xd/TNFxr1bhuPhMnlYxmjTQTSOg8E2kG2Hi","dOjw2nh7EPM8B/LRG1uHTXphdf8B/qPfA46408uyKpE=", "C03o0BWkd9wm2vNjM+jfyA=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("sBh/W0NxWArPMHMq4lEbH+WzKivzlV3D5EhcayTY60akiIkDYaltVhvtunikn3fS","MbqZ0p7cqLYcZeLYEeAhxIRZQtT9iZyQIhVJCAUt/ug=", "rbRgR2uiqHgFP/IdskTO0g=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("C/4gJw3oMlZyfm3tpiB0gV+dIakQdiziPxiegcSDy/WSDLGAgdtOmWxjg/4KTVbZ","8SpmuQAZ1ZOmlbBV9ne1bf5IvDxsCA19TOGsvBO9WWY=", "bFJXVRkVKSOnsfDsSSEOQA=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("z4jD46+gIcfFrXBikK1sF/d1eNLAsw5jJTZqceXUBck=","7o/69/b+GK3uBAcvdQZ9XFabVOFydBmonrVO08yToUw=", "MIfK2OD8KxwqJeKlSn4YRA=="), //:\ProgramData\RuntimeBroker.exe
		Drive.Letter + Bfs.Create("MO/9zKJCFq/sP7z2iMzcm3R7CyOC0/8k61nASNeGhi/VRasYFFcbSASOzKcGbdzS","IoInWrvClrGt0KtjsST8zUS2kQ8ZB97FhmrMQxqZPSU=", "N0KSnVenDU9Xhx2+LgEBBw=="), //:\ProgramData\WinUpdate32\Updater.exe
		Drive.Letter + Bfs.Create("Wrtq34sPIdHMEUwwnmILQyQPhHv6hjj/9QM4trcJCC4kzsiZD5QSyAvR263UEoIFWqdfboZr48RuO3sW1BuFGg==","Fa/J2ps/UfBTObEynRp9KjCmnGx6ngyEuzLZKVxR8bk=", "lyr5gpC8iMRwhzquN+yfPg=="), //:\ProgramData\DiagnosisSync\current\Microsoft.exe
		Drive.Letter + Bfs.Create("O1KurDnVmRFzZoE9rArg7B+bJG2SIy/zs0LjMy8oR6vdZpHUd2N+67IJaxXRYJHC","jgsN+b33QXcX6Ix2PSiei2Iv+wwLnUg5r+5uNvtffzE=", "fR8zMDkkZRQZXKa0HMkDiQ=="), //:\ProgramData\sessionuserhost.exe
		Drive.Letter + Bfs.Create("6s6tOl1gfS6/fHzyKHyodaGfHvVe2UO9yClyG03HNptgQvmVz3TSkgIrDDDnx3y5","TcAwht/4XTsiAslfWDUcF3+Qwn05gdqAwrrqb1QrrJs=", "gx4SjSXbfE0ZpHVZWi/D/A=="), //:\ProgramData\Win32\CUDA\DisplayHelp.exe
		Drive.Letter + Bfs.Create("shP9i3lqik3kG4BvfkG07T4qbYzVHu8QJYxuUQwxHQnfVPrDxM7uX9QI/VhkuUIH","YBKpWOGxgCoWaTwQeMQVSUvqefGngbXhIRk22i/PGwI=", "RPMmKP+viWr5rI6ycEjnOQ=="), //:\ProgramData\Windows\WindowsUpdate.exe
		Drive.Letter + Bfs.Create("c1CHtYRhrXeiagzmdO+WbcNwpCsXYaOfuyQEEn5YAkU=","tM5RPSfS3FW1rM/bhBNr14zuNuXYSP4KpRVUcdbqfVY=", "dAh0NktMLtZlCHc97JNVmQ=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("lMRJjo5CYcZr6W478ArlJjDY3IsBIjOiphX0EIwk0lQ=","u4mI7XrHF79EcuqhAPmD/QlELDPXSIXaWXUJvCb9IX8=", "3qMooNilEEVtmDbwdzj8SQ=="), //:\Windows\SysWOW64\mobsync.dll
		Drive.Letter + Bfs.Create("gOtrRQEkbiCvyzHhQ2QbtnUxo+WR6cEdR81Do2R4i0Y=","+zqORAlDOVDbHQVZuhLN2iEvzztgE03kc1pztOIMxVA=", "3ahPbK4FCwIfUM1DJDajiQ=="), //:\Windows\SysWOW64\evntagnt.dll
		Drive.Letter + Bfs.Create("GNHbor27sZo2juhg0IYBsKtUItJUUIz1/AmeiZnB36U=","E84Fl8dNyEg31R2r9+KMp2oP4Mzfn5tV53SMIbl6ERM=", "36DkqgkZz0Jqtc6UqbUaiA=="), //:\Windows\SysWOW64\wizchain.dll
		Drive.Letter + Bfs.Create("tTF4a9QLfYsJuH92fUMa4gbbNDLcb8XFcsxoZmP4W9A=","EoiXEwbyeaaELflp7fik2UhoeYmhzMDvW2pIFHCIUEM=", "eCHshHJ5yBwxrlyxKz1oKQ=="), //:\Windows\System32\wizchain.dll
		Drive.Letter + Bfs.Create("xsFjG/+4VHdIj7yUOlhBzv6cNeXxDwEnlLmFK/x2X6Q=","voyEHrgk6eWy3MBeL9dylxSvsIMwOYz8FcIRvpvkS9Q=", "GICuZoSE8EcnFK28CmoQQw=="), //:\Windows\System32\wizchain.dll
		Drive.Letter + Bfs.Create("3rmKoGIzbbOehynUwfoVtJrEMr2w/82DBikQCS7BbPayhke1nRBiUeVFgmLhlQrD","TnzUevpvG0LtUh3nixL3wHYX+OX0kL4bDS2A0MUO1dU=", "RZpcNmYFBjwbRL9AsFPQVA=="), //:\Windows\System32\WinUpdate-NF13A72.exe
		Drive.Letter + Bfs.Create("WxdeSOvMPoMxl58Papo7I8BvlZ20LoJ5CBYhMQUX4do86N/sFyVzlC4opSmqi525","L19x7StH5XPtHsjpsvzZ5mrW0mjr63JSfjeQgr0Vleg=", "oWg9XaZKlbChoxUu+SeBQw=="), //:\Windows\System32\WinUpdate-NF16A32.exe
		Drive.Letter + Bfs.Create("uzxI4I2sjGuHQOcTjRkJvr/eKwfp7lbevZmTzsc7NMzcsRsKkLtfA3OalrLNghs3","pPxt/qO6p0iR5Pq+j6Apy7VUCKieopwwg4T49LT4xbU=", "elVjcDtf/cFnPBAU7jfpfA=="), //:\Windows\System32\WinUpdate-A0sYHTaMEa2.exe
		Drive.Letter + Bfs.Create("VwAybhWSWEHVgz1hZpEMQpl/IZhOfpTQLLovXopCayc=","gGEQYU7XJPmsrsBqnms7/0KZNZFER+BvR0GIOA7HP24=", "GGS/LMiBOWP9R9VJvEDs+w=="), //:\Windows\System32\svctrl64.exe
		Drive.Letter + Bfs.Create("dh8D39rjuWI6Wzk189lp+XlhJT320M3RlvBZ2YFxEUE=","30Fw0CoJ1njfY5bF8Bfm+LznxXjP4LbpsKnLmd0yAVE=", "l2dVn+eDQVvD9mvpnd933w=="), //:\Windows\Exploring.exe
		Drive.Letter + Bfs.Create("Gxs/YKHNm3vgd+856GcNScOhYRpwkuYo6I3I4y6oxwkBMfN/BePpX6YrazjB/+g4","D816Wlfwfvm/h9940jMmIXDChqwnEvHqlqTqCiNi90o=", "FRXJzTphTHE4iWHkhVZDEg=="), //:\Windows\Temp\System Security.exe
		Drive.Letter + Bfs.Create("/xpNhYfH3XG1cRtyPBzNCuN5XqjjE0HhwB44gcMTmOMSWW9fFmu7C0rRkADCEHs/","suatFklxzUAZ6yVPOyuKV02ihOvThu9FY2oVkfhFtjI=", "YdxQoLCJghfXjh2WUPMpNQ=="), //:\Windows\Temp\Windows Boot Service.exe
		Drive.Letter + Bfs.Create("3OmlaeXbAAfS/oAl/3zNlQLHTZFsd4Y0nGb4x349gTCrbwv5qRfewKLgJa7+byNK","5QI13IdjWIz6LA/XsuwYyqZF9erqDeALLFiYxUxLLuo=", "07Vp0/Sx8P/RGa4VM++pWg=="), //:\Windows\Temp\Windows Host Service.exe
		Drive.Letter + Bfs.Create("yOnEZXX2z0yhxzqfAXGzonOeK7Yk1ZQVzY/CgAkxo+k=","YUh2pAqooEAtyhcyoR+iX8GQzmr+gGKV+5+KMfRI51A=", "mNIJjTEFQeglL1Q5v+V59g=="), //:\Windows\Temp\wms temp.exe
		Drive.Letter + Bfs.Create("f6+yLXBPCfWcGgmsn5ho2073B36FIfw6ZACRD2qxuep3auktp6PEUbQyYqdd/aNx","R8lIuGF6dtxvutey5xBedmhtMVJL+mWRBxnsW6ifYKY=", "9oxL4jqG+tqCLShdYN/gmg=="), //:\ProgramData\Timeupper\HVPIO.exe
		Drive.Letter + Bfs.Create("++99nCcAEUO4X1Doc44dcIdaiC4elrt4MrvwyWiuI3mm3zJPtZdVNF4a6uwX5bWQ","E1yXMLCehw/ZbiTs78L3Q3VcvZNnWgUfV6bWY/LlvKw=", "6qG6Hyax3RDoeT/c2nEkUw=="), //:\ProgramData\WindowsDefender\windows32.exe
		Drive.Letter + Bfs.Create("FxNfZNGFcxjQfi1ZfcsI6wQQLlWPutS//eyfZnP6NiwNlhjjOE3NNNJUXHyZhLNquZhkrSKXh1yNvFkKeO+fGA==","ezrqA5DaN3iliZuTt/lUZF7WcwObwoQTWbmgeLu1cNY=", "8YO7lGu18HgUztBpAJ9OaQ=="), //:\ProgramData\WindowsServices\WindowsAutHost.exe
		Drive.Letter + Bfs.Create("mUafOKJ6g7cFFXUGoEaQBMDJmKoVrD7oL3xuhV6ee0rEFwCGaoL3WodEzwH/Swxq","kNAgNSGQeE7bbiR+TuCAhmeeezmkowiLXTt23s5R5ZI=", "8VnAeUFYr9CapFRsj339CQ=="), //:\ProgramData\WindowsServices\WindowsAutHost
		Drive.Letter + Bfs.Create("keinnWWo82JcvVmxZTklY4CcRz+CRcuIY/etRo9tgEsYFQVFXMp10X6Gp3RRoH0D","8oz4YEuyTDziBdLsA1dRfTrKC8Jvvd9DLw1WSNRWaBg=", "ZCA9o2C5ww7RFeulsxy0Zg=="), //:\ProgramData\DirectX\graphics\directxutil.exe
		Drive.Letter + Bfs.Create("QZeEJbEB7GnLPzdiBK2u6tkMqET/FF0g6qgV8ZYtSG15oTS3Y+KxDd3Oin41U0YvnjV2NpioZiE338VlQ9xtIg==","QxUynzvdoTFCZx9OLurgwtD7Ckp4EivU086bBPo6ZVQ=", "03G+9mA2SFXMNQQMNzYs8A=="), //:\Users\Public\Libraries\directx\dxcache\ddxdiag.exe
		Drive.Letter + Bfs.Create("D+l3MzpALcPHxfBI+wFhS5KCkqWmiutwB04MQkwfPi3heOdKw4NGVY+yMSONmpcqb7Y/cxau2A4htg41DeZi4A==","18t5x1/G63+lxrgfWvopUXiw9C87BsahSFolGPcguxo=", "ZG9fjgOIaTI6mzf3XSwCQQ=="), //:\Users\Public\Libraries\AMD\opencl\SppExtFileObj.exe
		Drive.Letter + Bfs.Create("wJ5j+sRPwo/C12KixRwPcA0IoNH2OqZ2zgSQM7hH3VhyohMi9zOwKxzf0CjWtW3LGLlmtsoVS2aPflT8Jo7e/VQAzFQT3pljhw07PCga12fC2elSy/wOzF35Z1XGLMW2","iyTiaQ1qHbPsOMBsp4K1IFrvIWzvb/OTeFYBj3cPxPc=", "2TVcinAFjT4tlPbRg8SHgA=="), //:\ProgramData\WindowsService.{D20EA4E1-3957-11D2-A40B-0C5020524153}\UserOOBEBroker.exe
		Drive.Letter + Bfs.Create("+2g0/a+MRWw13s7NQ+SBD2bVXwPWGAncY/QgW143sA81C6nWzxW4yuqoO2RCYTbft1O/kyTYAs1MwFXsj8GWId6cBFnrzl8aJ4kdkt05d19fU0upZgHxqFXIYKz94pIU","CvZT+eeR5bhPWxf0tRiAXcpqV0QvAg3z97qgH83qjMQ=", "HKPm7XsAflvm23JuF48Xag=="), //:\ProgramData\Microsoft\wbem.{208D2C60-3AEA-1069-A2D7-08002B30309D}\WmiPrvSE.exe
		};

		public List<string> obfStr3 = new List<string> {
		Drive.Letter + Bfs.Create("K1WlAvOUVBCfQeV33CM7hLuk0yzIvZ9Om42yvvq5V6A=","Pr+OdGHQiBKvdyfpljsxFmDNXi5i/qdJWfVwE4ZO17g=", "trVcIO07shKhfw3r2R9F4w=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("QqElZ+B3JXvo7PL8BxfIsg==","sYMTxhbXE5YbUjiQhJxltfBojJo2zR82bZkVg/s0UNA=", "MmceaWExin675boijNaSbw=="), //:\ProgramData
		Drive.Letter + Bfs.Create("2aPuKuQMZ6wl3e4YNiATB/vgN8n6WgTwVXhF4tVqBm5yrZwPAwsPMqJIoxwyoe4q","/RZyHl7BehDIqFGdGnOvrs6Tec0SoqWIyLW0a2Jo450=", "tciTWpNHbZMB8T5hwWDv3A=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("z+yLNNIAv9ZkHI5sMpeuDEupf7lfdVGkCwT0y/iPSV9G/UYEpKi87r9k2/THYQIR","SEoQdgt/BSUQHRuFL3mt7gRmfaelzdg4ikW6rOruBAA=", "Qmb4RObIbQz5om87wtP9WA=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("2L5kCh3Rt6rrIHXGx0NMHvYMXJdCtFhZJbyq3sDrOq1Qr6haX0c9Zqf1pl7gWBfc","umoeUBf6k5s5o1LM1VvMedUJg2lLk8p8jecJN0NqbZc=", "M1ueOtk0GI5Zaic+KXdERg=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("UUB1/6M9+2eJ68iSMBCrvW3H8SXYutZxUBLAmqZVdzpztvNnf6brjopNDX3epgML","GC8nD9GWR0hg9xKeR9dNAbZCdfh0lm3uYnRyHVsVEq4=", "wQMFOlTf6UD9E56f8m43Dw=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("lm+qyKfGAFfrOPaJJJux0/nIYZFtsuWiQ7Xs2dLbc9doQi9+mBZNlOAnkaEBBPzJ","JwBD7dkV4d/4gzkVZWUjkaw1S62kilu7SKV5A7iQy0U=", "Ppoal4WoD8Nky2h0MRrIFg=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("RKg+fd7vzt15sM8SXpzjz/BYB5vdECyGzZKHCp3w1xfSxTahFhnK4Sut9iZIXL2K","y78mUmXKzytPTq5J95tQ0oTgghLj+KjOWuKOBEUQzgI=", "woRFw8onXh4IC1Ejko7Bpg=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("hqAGAIMxmWz1fCP3qd1Inl7poYzP9Lm99qF8XXvzszvMDIv+/XjclbCceuKRVOw3","ObUGkEsCH7ahfqr+9qvrFLVzZYz3RcXDiBqmpN/p7gE=", "DESPwFxAicTM/K3I/P1kvQ=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("P/vImzfC1QdkZxN/GxY6CQkd07kh1Vlt5ySnXgvB/+rMqYPBcWsXI/+Nqe+wVeoD","UrDMnsR6KORQC2OHWjiwHpOqCRZWm6kAZQvWSB5eqno=", "7iZKABQwzMhF8EncMBhyIw=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("eh6GCHDQ768gBk3J1WvQ6y34lOG63o5RBfuph3rniuR5DdUKuF1QsAiB1cSxMPk+","+vsdmrvpvnYWBGoC7keImsF1IxoMZTBDB5Mknr0Idfk=", "7WkT/towxz/mOsbcGtjIIw=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("GAVp7d/s/H4L1BdpYzsP1ijtvQyskZlK34nmpRQ5wpe3dOmzBb7/SUQl/3B/L7Du","3MFLLsf+8BfMceabKKoXRAqX8Ld9PtZ8uwLiiiOkzI0=", "yCwcW4CR+fOvddZXCpjMvw=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("0pITD5FudgWZu43XEATw1CgfPSJPtmhfvTSsQpfdZuQ=","KdJ80LhZHYcMRcR0zjRJhER5WaGcEC7VlAFvK3JFCGw=", "RNpXw1YR4z82vlkvdr2TEg=="), //:\Windows\System32
		Drive.Letter + Bfs.Create("noMTWIJdAEMhO5QRHlgVoljYNOXr+tWHI+/DDsujSY0=","iRAw6QASxLrB8WhaQ6gC3r0fa2wAgrxwPSZqlIE/ZTk=", "OhYKiDzUxDsGOL4RjSMzVQ=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("6Sz1KXCjgIuklGXgUpBkBy6acAb+7f1Sv9adzyiSx7M6a6g/CZe0bc2ekhNpDST1","KBUZFkb7Ee8sHC8sxUtnL/jfedT0NfWuTGemlJuV6/g=", "7W8cZvToDimQHc9a1qbaeA=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\
		Drive.Letter + Bfs.Create("pD8/x6w0lhXDnTRiF7wpMYafQ5g9pWGBVf7h2JMiEbgyUAYQ49DZePcRS0rxNb7APQ1LNpx8D9NcdJwR3mYJOw==","2KwgzYZvae05b4TFuszUeL2oy66g/513miXPokZpmjQ=", "vb1XrGT32aIk0vo5T0Rbrw=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
		};

        public List<string> obfStr4 = new List<string> {
		Drive.Letter + Bfs.Create("YFJWsF8r3fP4MAu+2ThUkgEHFb+vs0JABitaVeS+Hgw=","zDdRLZt77qg01oip2sYSnr0godn/t9lk+zAajfBdHZc=", "MPRjLkVpTQRcZ7wvJHqrAA=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("ZMvkzHdlpLUZGtQ7p/LP3WuCIWv3vt7DqkaeGXNfCmwXdHQOcm5igIZNu8WAlkP0","Qn9tjHAu4zbe87g+/SinAhcFtnZlNXgTa9R5PI/hB7A=", "K6dwcHeSMv8sz4Qk9IQqFQ=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("iIn1dTcABVgh/POsrMG6/fqtXLwWFXnXRBxo0P1jqko9SzAzv1KyiMy4lP/EWiq7","LMdAD/EKgLkl4ZZIu1aPB6qEIHHL6Re1w+b1Nxx054k=", "gPrexVPpG+lh9S+zvOnkLg=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("iESFbskuzA2tRGLbaSsJ7RSpXSYZMCw6sCoKu+H+eTrTpf75Twle6DM4MvZ/mjXZ","HsMsbrna1i1Ss5rOQwgI14I0z1esBD2fU8QhmsyCD3o=", "nAsUhKVL2DuimM32FwO8SQ=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("rYyv0GXIUSlmcPPOsTDD229jSKsnN2h2xt3WxnDQ5pdm3SaHwTMctt5Ef/Re6xZ6","otkmtvhiBehVTVT4huWrnkvNOCBE8lWulOUwckPsEBs=", "TOni4VZDf+eQm0sJjBOXSQ=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("qv5NI41/2n8DWvHlxdnWHDL4lvC/8FSt4dw+peNtYQcr2RrOPvxgb01bHt73i7GD","J/LMnQ6Zu0ixKMWWtAL0BEgYprLBI1G0Shmxd2wXKn4=", "uUafGycTkrO4YK55Zioq4g=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("OhL6bW+GJVS25zdxTCu10nd/Y/gZxJxRhWZl56WD2YC3SFB83E5tGqeYEdKosm3n","LTaKM3pvE50Xs5SfJ4bW25DjlNXWjiy8Qqi98bBCuRc=", "rSKgRbA2A5pDnyAVXQNO/Q=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("9BVVLDEhSo5CXym+X6ctc+aaW6KyHDJ6E8i8jN2lAuM6f6xNnPwaXf+FhDTUut9k","m517b7vG0WeOIl/H2rdLSI23qwTSjNHJGNmgLUqg+Pc=", "zNlezdBX1k1EZPEeq3FZGw=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("hFpvUiCrcOYw0yEIt3Da+rjPUyifGnDgxJB2Uz1IEwRHMBkGrYPd9+VmNTOFeTB6","YGtJsllNCsmYkJ8LhlN1enNPwXc/0cYkY1P9pnvMofI=", "YcTOyzUIbbDOIeq8fzxD+w=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("/OPKoCwF1WQgBN9tlwVfvm9IPF/OwbHQRIojToHE36Pp1FKeyhngSWDowEvyQCrC","1TSFOiQk7XvfOHqo4d14TB1I8t4qPE+vi9sEP38fHOg=", "5blAwgJehEl+pqm3p5IKPQ=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("J4LiwGYNQ9oaL/Z6TItOtPBjeNcGr+zcaUJpzF8szyEkRLqW0hRc1ZONzULbpOJZ","YFj0mtaC0JuFfnMpYHtgSTZesR5AzfCOge45Krlrw0c=", "4j4moE+zJQJvPhE7//010A=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("KTpsB8mNKrr4Du8yLml8PovQutkMMeKwTuQtV4VJLyo=","OVA6PwHOMpMQ5upD5Ov4qrGaF4WgHpIMr6Da5aRfO8o=", "ffXxpCdd1cuiP9rVWFTEGQ=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("Ut4RfDJjZ+KYdiTV9XUx+U7lckWSNlAiOn6eP0Zd+AorpGedezp1Dkxv5IEp/p2ZLZENJ+NJvMNSpzjSN7kMkg==","aRA9er+MG0idUoRO6baFOQ3u/JccnHm1dfWOna0hfuY=", "t/dCEG5yFMaGTd0WehZ6wA=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
		Bfs.Create("98pWGnI+u6vlwGXqw5RcJw2s/fwyiQV55NkiNABv2NU=","Vy/dJiqHbEaKgToj8vtS9Zf8NcpdlJFAF4gLtU2fO4w=", "IyBJMnhBx1ADrDW12vMkuA=="), //AddInProcess.exe

		};

        public List<string> obfStr5 = new List<string>() {
		Drive.Letter + Bfs.Create("qK0CcWXw4A10MISO4PE9hSytSEFnGFbIU6DwiSUruHc=","6W8y+ADjML38lGltvfkuSQrjzY7muDjb/AclTt7xYsQ=", "LrRXVqbRYMDfKzjMTAreVw=="), //:\ProgramData\360safe
		Drive.Letter + Bfs.Create("k2VBSIm7IQ07Doe6lN+Kt1duL93Lu2qr2TUiihz0Ka4=","iIy17tcscx3q33pQE3h2WvTGgtIpW7JcADpYpnc4SoM=", "P3O4nj9NKPijd9LdnXqDuQ=="), //:\ProgramData\AVAST Software
		Drive.Letter + Bfs.Create("qPfdxfhgxcbUFgh49vbHiU3z/wBEwQQKZ36a3o3vzB8=","VKCZ+evRHP+0rgptUD6V6hVZuoI2hCfPt/KsSZp8BP0=", "x+b4FwOtHXLzkPr4OTY3vA=="), //:\ProgramData\Avira
		Drive.Letter + Bfs.Create("f6xoHl1V6UNTriRWgFjUIu+GufP08cflqtlPITqtGEA=","iSBAJ2VUvODjgNz1vemE+iLzonBVkCdzOWh7B3qYCTc=", "qggV8vB1AA86ZqkGIyW/3g=="), //:\ProgramData\BookManager
		Drive.Letter + Bfs.Create("XgTGoSBdXiw8BPZC2APWger5qpySxaev3GX/UrvzTz8=","JTIQYKhxXDgg7xFaW00c7iKI6FLAhLdf3TYyNbGD57Q=", "w9BwF+4rVNv9MEk7JzuHZw=="), //:\ProgramData\Doctor Web
		Drive.Letter + Bfs.Create("7lHFuQ4nrvF7dpWviXkIJQSwFvdY35F7W8IBziGR9Ck=","pwD94y3x7eziMzb3ob8dPNaVuVIxz0pMzGDW27piO7U=", "4uv3QIBgeuxmjPVC2svnRg=="), //:\ProgramData\ESET
		Drive.Letter + Bfs.Create("EIOPct4D7GTeMtFp22up4CT4XDyvQD0iuiNIUNnH9LA=","dri8eY+gS2XicVWG+HwAD0kC8MffrYQA+TqBCukXnmI=", "MrwEs8fjUImv/nHxLGQ40w=="), //:\ProgramData\Evernote
		Drive.Letter + Bfs.Create("v8ENR4hAr1RkGm1BsBuP8J5q6uDbqsyPVt6iWQacr9A=","GMwe3JAgM7SZVqPyebr70hHXmqcdsurxfm0erc5MV7c=", "z8Qzc0i0wu9PBGCW/bmiww=="), //:\ProgramData\FingerPrint
		Drive.Letter + Bfs.Create("bnNKxeyLXwczoyfkeCqOdqUB7roWFf09+GV/rcXn0nQ=","x6vgPwCX7z3pt/J70U2orn2oIh3I/Qix9uEpD9D81xc=", "Yl38Sl710tY2i0/zRbdSYw=="), //:\ProgramData\Kaspersky Lab
		Drive.Letter + Bfs.Create("NvNXZJ9atlYe9WixBHPaSWJ7TVAWzQt+MnhA+0rs8jRgneSM/0PjbmrZ/7Rv4gyR","kKJ7HUK8wTDtnxgvB3TiWwq1eTlPuXLA706FXG1dnTc=", "bhGFSUYlt6mBfmH0amKNsQ=="), //:\ProgramData\Kaspersky Lab Setup Files
		Drive.Letter + Bfs.Create("Jy6kY6pylADDj1da0d5WfIpZFGZBXF8vrpw/CbszWas=","T1fHMBCHEa9smaZJFn/YJhchRcQOV0AOMrl9eLCfatI=", "TjXKZKEqv9ogS6nPUpYI8w=="), //:\ProgramData\MB3Install
		Drive.Letter + Bfs.Create("srEkDRR3HkyoMO/Ih/+HtAOmTmxQWBfAejTSL6fOVJs=","BGU1SAtmBzsSRmqM8swZ06TfjhltQ1tYcstbAa3n0QI=", "1PaKpLP14uPlqBlcIxvOQw=="), //:\ProgramData\Malwarebytes
		Drive.Letter + Bfs.Create("npg2zQ2tWRP+v5KxOFPm7qsV73nx8XMH8ZfxEKjZMcg=","qFQEdXMJyibmtCHuQwbh97Ujcd/BrrlZXfk8sPwUDNM=", "H+q6TP/dl725R0+cG1UTUw=="), //:\ProgramData\McAfee
		Drive.Letter + Bfs.Create("/LbVMW0OvVii/+UhAjTdOyYzsM1umQ8NDL18C2SqHPo=","fFAEf0eGOeaqx62/UG3I7/CSVnCYvSqJo4A2PTiT4/8=", "2jy5ovGotohwAxgZvASedA=="), //:\ProgramData\Norton
		Drive.Letter + Bfs.Create("5/fMr8g3fHs9pdk5R/ZHCHTUK7pEz8GEkhBaIHS6jtY=","1j+CXUNevbmwkQYfjX0n1eposlMy62xFuFH/RpGumi4=", "ZcepCU7Gq8U2dS4GregCXg=="), //:\ProgramData\grizzly
		Drive.Letter + Bfs.Create("Ll0t1UNmRtwU8hdz1kaSoMJRJu+aUDoN0HWDpefmaFe1MU04ObjQdMGhghIofd3A","E1d8ERTNRoHmkpk5TG0GO/HE8WOrfo8OSFUaJf04dGY=", "gXUTIflVeYMG1sMatNnJ7Q=="), //:\Program Files (x86)\Transmission
		Drive.Letter + Bfs.Create("4z2vJW8HqlYXHdow5gBZt3JXjlYG1tNhyf4+6UHxWGByIZZLHvz7AFM6CiMz5fix","hTfcBYA0ta1efDMHDC0LCPINb4mtJVshZl13muO9gWc=", "oMdnTmMjCEFkYTER0uzVfQ=="), //:\Program Files (x86)\Microsoft JDX
		Drive.Letter + Bfs.Create("ruWrHP5tg8ALBJRah4wOndnF0PC04jMLrHK+hvxpRnw=","ZSO8JwespFzb9nVL0Z9GcLwoh6wyl09oNNS+0kgxJTk=", "804BGnp45vlQF97SdMF7zg=="), //:\Program Files (x86)\360
		Drive.Letter + Bfs.Create("HM9w2Wl4Xno+1BZmVMyiPqjgGzmT2De8QyIuXrDDgS8=","FjFoVRq47qeNG4FQ+EK6N+uu7Vk6hfMiY5sJZwsxJxk=", "8DHOybDmVHRuuGniNYvXbw=="), //:\Program Files (x86)\SpyHunter
		Drive.Letter + Bfs.Create("msbskLsum8rk3PhniR6CxJp2drMNrPChfTDYA9/ZVks4EVaY7GRD2FFGwhco+8bt","l7Pw1I4slOrxQbFZaH5yD0wmXVFFEZmG9X5ciTPHadk=", "kbl16xYc+POPVHHekDghfg=="), //:\Program Files (x86)\AVAST Software
		Drive.Letter + Bfs.Create("rRj3o8BrRgSRuPJP6AxFJdEBeWpDTkQ/80OQU13bUw0=","pXgcY4vO3y/MCUy/9loGcOYtyjUoibbNNlk7bWiVTJQ=", "Eq6srFwBap1F2tl48tqCWw=="), //:\Program Files (x86)\AVG
		Drive.Letter + Bfs.Create("nrRVghTaWGIXLjOimTV6Aidf3ovypRbcoVjHYvBafa0kYRetWTh1x0sX0NzQWTkf","kvB9gSxGo6wo9yOJ7ZFr00gG0gHK6nJ2/33NNJ5VeB4=", "2Oeg4ldHQwCjE6HwnNUf8g=="), //:\Program Files (x86)\Kaspersky Lab
		Drive.Letter + Bfs.Create("EGP/xeH4BFTTYsi+6KCmvwKx2IZ8S38c2Hse0yhRcSE=","KOl9RhuX3+G01Q2iTnTGIhs4F0vs+iWDw84djcgkZnE=", "zPqLitJEqmucpRHiuVFLKw=="), //:\Program Files (x86)\Cezurity
		Drive.Letter + Bfs.Create("FkXekCG0U813djFi90OIUardqAvoR6R/7oOTFoiMmzORyLwBMJVjwIWW956ySWEf","aCgcgF7nzfCk9KKDUzCo8k7XYlaRVHiHk6zukRPgQyk=", "RRJmpBGMaIN3FKSYlpieJA=="), //:\Program Files (x86)\GRIZZLY Antivirus
		Drive.Letter + Bfs.Create("QJIKgRVfjg9hPUjTylDEODccqdaVsen5ogvFBlRS8lnW+LUWXK6UdNwR4axydVy7","Ick0mRPrhykPI1dJDyr3WAnpCwebBur1JCXaXKFRV8U=", "8rd2/buLF8BppOeu1Jrmng=="), //:\Program Files (x86)\Panda Security
		Drive.Letter + Bfs.Create("y64MWbTj1kgl4Llsi8bp/kuWONFAA2nlHzLGteZWkykJGrQOFPw322mDlNmxyEN+","Bj5FK/O9bCMNL/vUkTlT2YzsGEHZ7GzBmJv6yfYvhAg=", "D6wxoM8HRJn/Od4ApJxrKQ=="), //:\Program Files (x86)\IObit\Advanced SystemCare
		Drive.Letter + Bfs.Create("EhWtY2rh7Pvqq58ye71/Emonxox8smjGJeixjak5Q8UeNQMT9UCVVZXK4KunyfieTXZxid51qInhVZi/hTZQvg==","EHnw+TcmRgqqBgSxmA6oYV2V6yr2QNVXPwNHxIQR4a4=", "clhgK6NTnmTBbdHjIKTchQ=="), //:\Program Files (x86)\IObit\IObit Malware Fighter
		Drive.Letter + Bfs.Create("enccEW+Ez2iCHIVKVNaEmVn6mlb9FKiwOv6yWNZ7q3o=","JTnQZ8khvdR9ACUVV80V/7lpjK9UWJ6A2P6t9E9oqEo=", "KXx1DQL8LL5RbKwiw/st9A=="), //:\Program Files (x86)\IObit
		Drive.Letter + Bfs.Create("s2Fg3aVRukFBSaPpj2UO0ExLyFIb5SfB1XSep973ZbA=","/SfyhN2i2mZAHc7Gsx0w2Ep0Ag7tfU7MoyGWsP6AZM8=", "SCZmY7Zq9NvHGIIhpzgeAQ=="), //:\Program Files (x86)\Moo0
		Drive.Letter + Bfs.Create("pPKM6esJxGKv36LMUpmfSaL/XNKgzCe/I2K1Uo1senw3rQQwk/yq4hsNMfeW6hcC","jQsgqlwGKK+KcZ2yGyVrftRuemKF009WUaSOrQslh+o=", "SfLJQtCvFAmaDyi1C8DFbg=="), //:\Program Files (x86)\MSI\MSI Center
		Drive.Letter + Bfs.Create("5Bd/9Z+XcUx6acyxsmuI0/AflN5+kRgOv6qJSKJeN34=","2J5mTKRX0FcPH9nif6DWpa6OPZWMKA0rL8mF7JsLchk=", "zno0w8AzSUOGibWhkdsD6w=="), //:\Program Files (x86)\SpeedFan
		Drive.Letter + Bfs.Create("r/LJhH7yKS8e2ZXUw/cYUtnuzy2QHXtUd+oTkHQ7d6I=","0Si6P6H60J6801gCEuj5xYk6fuGWrYfYI5ipu7PyWAw=", "V01XH4coI0bNqA8j6jvNRw=="), //:\Program Files (x86)\GPU Temp
		Drive.Letter + Bfs.Create("TTd0loh3rjbKfkHuUmM898D2u5d8DNCun9l8ny5CDQw=","MiE6A/S7Cjn6+ZpeUOb1VSgrBiYiwSm4OIPBhNbMmQI=", "hGA7avnXINf4/pYOfKFNzw=="), //:\Program Files (x86)\Wise
		Drive.Letter + Bfs.Create("aL3IGekEsSGZeHPKUJGfYZReRZYLEgtigaFOz1GPVW4=","xDnL6aBGkXqN0r/Dp6YinAdaAiaiIZe0kmkEIRoz40A=", "q79R3717F2Ps8BDfo80Hbg=="), //:\Program Files\AVAST Software
		Drive.Letter + Bfs.Create("jaPp4nx+LYT6hexualLvNb1fJLxIO3Z9kmbzTM2c4OA=","e0nXIcGPoL9KVrwKdj32shs/MLnmr/UqK+64Wl+ZFgs=", "W3tF8eR1crAKeIm2zJFSeQ=="), //:\Program Files\AVG
		Drive.Letter + Bfs.Create("V/tZMU+VHf+znaxT8HGoA45j0zeqkv284qWlH9aAicdfQIntM/QCNmWbft1Kg6Y8","TD7jisbDqPcIWk4xgLqhkSGOVmTnRB++uDG3xX1Bc5c=", "qIfMXLZDlb1SvEmbYaMoSw=="), //:\Program Files\Bitdefender Agent
		Drive.Letter + Bfs.Create("oIxpdhJjBOeo6CQJXaRPwNCTxCAJE/X+ll84EUT+lJw=","aBdMrRcpTKBAfjCnNWe+Zuif3goakNAanaR7d48L5YU=", "vGCBuSQc4zGaQ/0TueIDmw=="), //:\Program Files\ByteFence
		Drive.Letter + Bfs.Create("zuahtQncO92fpNYaHvXVShAaU/icZrNOMXLyrs6B3nM=","cPbsTXBne/OSt3Hxn9QpuPnHcs9V5eSZ++lD1/w+mIo=", "HgiIwzKy1YTCN+Vhao24gQ=="), //:\Program Files\CPUID\HWMonitor
		Drive.Letter + Bfs.Create("v0O/VshnShPhwK+ZdkfgC8LJ+Xd4c8Hs8G1nBWr+ZfY=","OZxDupc6kBmbJUtqQ+MABym++uNfawYG3lho7yiuQic=", "7tXMlDhHqX4WsDIKmuZNcA=="), //:\Program Files\COMODO
		Drive.Letter + Bfs.Create("rJiqOXYHczQv00cMoShzWZeln3rRCz/7uH/tkXTlaWg=","3+umrj16vwxY4l4/bxNTaj3yTD2KgdE4kdfG4uHmnLI=", "SoRCQg1+0jRqy3KXeelwvw=="), //:\Program Files\Cezurity
		Drive.Letter + Bfs.Create("b5PfTNoIywYU3dH5dsQ+GWdFPgA/+/9tLlqn5dgixak=","bDCNE7PrHw5pj87Ccxvx13ieNp+AcOao0VOEnx6dftM=", "Omy+ywBztF1f0p+T9eCNag=="), //:\Program Files\Common Files\AV
		Drive.Letter + Bfs.Create("FV8Yoqu7yqkZDdTzejTagBqVoLI7/dULlKYJZ4GJHUHqK9WzjQRUiY4sqIdsLarJ","1uMLIFglt5T4etM2yzdU0F2KoYvz3cSFRb3z5U69yJE=", "cXU/xAUVzrWw1mIb8pVsNA=="), //:\Program Files\Common Files\Doctor Web
		Drive.Letter + Bfs.Create("zFnyAkHP4/IuXapppFbn//GbjjA1rfzsASXlYJUx2Hy1cVN19FA0r0bArbf9oUdn","J0cDCT5kHe1wEDAkfPwKllju+i+xKh9gJIEeiuxXs0I=", "wZRbWBkrTdt/mYoooXy2sw=="), //:\Program Files\Common Files\McAfee
		Drive.Letter + Bfs.Create("gjDmXEUOKC5tZDDwwYj0p5ahxGtHfPT68vo0jlUxS1c=","2EjPpbpVZx8+S2EjidW/vjmjDnirQ3gc9cvSICAXSds=", "7atvazCbw1hRZmoh72Xyug=="), //:\Program Files\DrWeb
		Drive.Letter + Bfs.Create("1oTFnYIyLlajz3k3Okf7mmaZPGCoj9yBWMzBU1DtFT8=","WPpW7+4QWrtxLYSsHOzDLqCZB/Sn8ul5qGH5otg37JI=", "9crWOevr7hPTaj1H5EI8Aw=="), //:\Program Files\ESET
		Drive.Letter + Bfs.Create("zy1nIINHtemsQfo/292SN6NbLRfWRA/MoEgrJNe4RSTUoi95cFVHMe+8xuoXolte","l54Mt2Z/qaMXAhd+h5N6c2A70iQHO7L7YkNeDYNfQGM=", "VNghjeNdqR6VUQ6XvzfwPw=="), //:\Program Files\Enigma Software Group
		Drive.Letter + Bfs.Create("GOiyO4xdkEQsFYboWnpV3gMAV5gUZ+IXCFcshvxwwKU=","97Plx7aA2wjWGK00PcLBefNfW0J8spP02rMQOvb9LDA=", "PtEuP47Y8A6oSXMm7IPygg=="), //:\Program Files\EnigmaSoft
		Drive.Letter + Bfs.Create("Ux17obPXr9ELwwyRzz4Gd+DjI5d+NzotrXu2obj/AWA=","4xsKqA8CO+USPTnhKoxCUzTRXgn43Yv6mOGrwHoLzFk=", "ZHIZfd3n1B4tT8qL3PMcJA=="), //:\Program Files\Kaspersky Lab
		Drive.Letter + Bfs.Create("7Z8k6QKPlnJEkgFQQq7DqbBbEmZE56vyVgB2UezpsGRZDIk9J67WckT3crk2jcC+","Kq3esgzdW/A0HoEFmrp4r93jEJfum9ORbMy7SB8rR64=", "ny0Y5WS2q/1DDOIzS0sfzg=="), //:\Program Files\Loaris Trojan Remover
		Drive.Letter + Bfs.Create("MJRyMzIKSGRuhgehpHxXv2dcNFeN/AZWSVnxHRNQzVU=","3mJrPX3H2MAThK8V44ABXpOuGUaFmuSvaht0QuluKTk=", "ihdhwnx1sTCFbbAA2dhDaQ=="), //:\Program Files\Malwarebytes
		Drive.Letter + Bfs.Create("dxYwetCgnPPR/cndBeXs20cSHj2DNo5lyXQjCpogCnw=","s6SoYPMc+c1VINZbAAbjknwqH6XR16MVA7dlojwcG08=", "NO4GYH7wqVqLzLJJwugkrw=="), //:\Program Files\Process Lasso
		Drive.Letter + Bfs.Create("ag0TDxK9BVNY16vNhKe+du7EPnMHdDtFo0EEskfuQbk=","1m8gZag2fv5uYC8o8duMEjSISLVx5zcaMNo67gc8xzc=", "5bUbK2ArDNIQzEKfv279hw=="), //:\Program Files\Rainmeter
		Drive.Letter + Bfs.Create("El2Y4fx8Su+lTS5p6GMjjOb+3STwXxdKcZaL2hIA4eI=","3MfWrU29Km+BQXr1XtixFbnk2nSniSgV4mU2TE9P8hc=", "G3zVKPQE3OtB5zxD//2djQ=="), //:\Program Files\Ravantivirus
		Drive.Letter + Bfs.Create("/2A9cpE3RS521DRT7Uty1XPimOH4U1vnENRTLdMuY5k=","KWpUcJX81SCgGFE6BHyDz1apDsIzQOBK5jh8AFfRE5M=", "mtnnQT6HXZuAxJj402fkTQ=="), //:\Program Files\SpyHunter
		Drive.Letter + Bfs.Create("HWK8qNxvh5wTezvbMgzDoBsn4A+O6nFJk146dx5BA6uzrHB50w8rS2kIp+9isurM","GvF6tN56WZTlCHk4L57gLB4wW6mkn/MriSl1MYs423I=", "hYrY44bcXyue0j+Pyl12UA=="), //:\Program Files\Process Hacker 2
		Drive.Letter + Bfs.Create("7r+SHIlCnKuCOu4qIt3PU4wGWzrwv2cHlfQyBBPQsIw=","eTIPqRDErBkOh7q/X6mINjm9esdzLBacLaJEPe+rVPo=", "XwhdNulRKoSDh4rAAIOH0g=="), //:\Program Files\RogueKiller
		Drive.Letter + Bfs.Create("eA/Yz3vg5FDQHQI8DZG/+ol80S1tCDUMqSxD9JUS6FxZX+hqo/516j5ZzsN1QTjU","WJ6Py3ssPbyElRgpyL2k7sUGL1S8Qagxst/q2A3JNjY=", "7W26ixFDtoy3RjyivUOtzg=="), //:\Program Files\SUPERAntiSpyware
		Drive.Letter + Bfs.Create("QTEyweIeMGFWkMKmCq4WDZEx5KUF3yCn2Gp4u27rSV8=","ZcdtSWLTDIpLljZkp9ObXvi7C6BBkzKt2aaWV7UzSSM=", "lIHZuoRLWng9T7DuBIpXtQ=="), //:\Program Files\Transmission
		Drive.Letter + Bfs.Create("P0CzBDmuunNm01fDbveddMN7rgTfC81E32wfzSmysVE=","c9aW7yCo3W+Z/QEkJY5Cvu3+qtEWrHQd4IIN6DUfCnQ=", "oXkTZMPhA81nKPqupVUCAg=="), //:\Program Files\HitmanPro
		Drive.Letter + Bfs.Create("LPjdaFvp/M/zQF8REKywmV1ObPLrV8ALyc2jLEYPhbM=","mFntqGLvKE2598sQkai5olMmCh37XKuje8MOjjdZz8A=", "YqUHbcGvfJc1EZSrC/R6yA=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("9F1Hhb6e+y5L+ri7VBr3wkpKe5kprMKs5H1dcKSZyg8=","wwa+7X/8iuAjGj6s1nPsxKFZszmMEIV0Zx/nKc9oVQk=", "KhjGBBvtLkUsmytWjh9llg=="), //:\Program Files\QuickCPU
		Drive.Letter + Bfs.Create("Ce/3BAeSyuzWDxY2lUXB5DF13NqhhOO6STaP/euZbVc=","Kvwn3b9s57QF4iNZt5nqT5xP3BkdAsJcTdDePFhYML4=", "NGPAMf0GBO/rzySMg3qLCw=="), //:\Program Files\NETGATE
		Drive.Letter + Bfs.Create("cvABOt170U326KdqytOtTu09tTYPfdHv0otgerz1/zQ=","YZH9S8ctLQaJR/gEF7Fd5Ab2TdA4OJrazQ/wwvNtLUI=", "tRjHQPpNfwDTtJY+5rs6pg=="), //:\Program Files\Google\Chrome
		Drive.Letter + Bfs.Create("S+pwAxYktOP7AICjxrBEweKqsg6avKsQOCKzj2ALUpI=","+mN8FgSTlYOS0b00BfmH96K/RsvTXmIhmGxpVwPKbjU=", "BY6cgL1WO6+RrygXqylHnw=="), //:\Program Files\ReasonLabs
		Drive.Letter + Bfs.Create("E6q/6SmsIuP4TnVr55HstQ==","3t6gsH6+HPvCrR3xm0Npg1IbdSMQOx/10XdGjJ2Tojc=", "2XMX8KyEsYcPcnm7xRRwNQ=="), //:\AdwCleaner
		Drive.Letter + Bfs.Create("rvZpXngZQkKmkY5y/btiLA==","8FnlA6wrEfjmOIqJppOmRNByNUFrit0vwINZg9ceU/U=", "XHNhG0Sp/D59tMmVYK17Vg=="), //:\KVRT_Data
		Drive.Letter + Bfs.Create("Cna9IRCKBezV8atSrv9EPw==","6Oeh016y0mD8qk0VazXX3T5BN6tqkSs6J6rz1kdl62c=", "PKJUGgjSvY6YRzdJbi9BJw=="), //:\KVRT2020_Data
		Drive.Letter + Bfs.Create("Z9g661yvnf8mLtzOR+Zhug==","2s7T8nc9QlczMiwkB2VljE/V7EVOA9mWVKS1jlf3XfY=", "RyyfxMWnH6WuQPHa6WBSVA=="), //:\FRST
		};

        public List<string> obfStr6 = new List<string>() {
		Drive.Letter + Bfs.Create("3GbWV0uYm7rLr7uOQYSdAg==","xuJT5tRPRHS+n/e88yKShNNh5tSHD6ZF4lGyQvEe/+0=", "BMUd67N9OgVikDieq7P7gA=="), //:\ProgramData
		Drive.Letter + Bfs.Create("ZmW/lsONi5cwi0169ATc7w==","g7w/798Vq9zDGa013V60ejjH2X7F/zdxDTex3AhsuCA=", "8wLGHuqGwIgClIumK6BSQQ=="), //:\Program Files
		Drive.Letter + Bfs.Create("SYqRer1mVzL3XCoL3E4IqOx8i0vZSxB8mkiKqMGbfVc=","JdhkbishV69gdY1YjZ/E3sBM/k7fKXzf/FM3RhwXVKI=", "mjCkoY2cKACoE0PGqQs3+A=="), //:\Program Files (x86)
		Drive.Letter + Bfs.Create("YGn+LQQ3KQAyTn4CdruZ5Q==","1NI1qgQNAMqhzW0jWyOmbW+Zsch8tsiC37I5rpEVn7U=", "0v1VuVkePCSFbzboqJ1xuA=="), //:\Windows
		Drive.Letter + Bfs.Create("VaqENzkZO6cBB4TsscxoyA==","+T3GsCs07dQ+3kv+zGH1ziuGnxIKaMgCmDL2fE8ozfg=", "3OVEDchj1IscvoMfHxgucA=="), //:\Users
		};

        public HashSet<string> badArgStrings = new HashSet<string>()
        {
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
            new StringBuilder("r").Append("eg").Append(" co").Append("py").ToString(),
            new StringBuilder("st").Append("ra").Append("tu").Append("m").ToString(),
            new StringBuilder("st").Append("ra").Append("tu").Append("m+").ToString(),
        };

        public Dictionary<string, string> queries = new Dictionary<string, string>()
        {
			["TcpipParameters"] = Bfs.Create("dHO9zLYjz2i1Wetnu410PfBNI7D4gVLxZ6amhQqymA0PX0LQDLRq8xgpeLQQMO834MMVOD9QhHcnu+dG/fqxYA==", "gUmWke5QYV1SGm6pnU4ojifZJ4Lefc7IVlGdPw6nh5k=", "jiJKCeNDUQw45i9pXr0pNw=="), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
			["SystemPolicies"] = Bfs.Create("fH1tjQdNp/VRgSYgXqB6O9YR8eM3+ACuWUPqtmig580L01nsqjDj0/7+81pcS6tzPM2ztBcZBjLSZ89CqcCOSw==", "rYFEjeG86WL45LWXzVwN7muJS6hJKcbq92XvFK4hW90=", "UfOYiFpKkUV6R9cRORxX6g=="), //Software\Microsoft\Windows\CurrentVersion\Policies\System
			["ExplorerPolicies"] = Bfs.Create("Jx8JAg+55IwB/5pXm1QOInut6ymn2LmjhnemcqW1C1cDi1mNXFJXx9kmf5lRe+dmIobpARxPg3I0nFc2kBLVRA==", "lG+2++2Y09GGUZ6+rphh/4f+n6WJpXZ/l4/oOD132/w=", "6k2Gvg9v9/VbHZc6oe3SGg=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
			["ExplorerDisallowRun"] = Bfs.Create("EIAO/zEJwU7TLrI/Cvzt9oFvSEs2WR0HJoKxuze32Uia0JJNst1hc2ITEQd2GTTlAfJlq+nk+iLjtB7qld6bNLhtMwQWCILokH7x7YmXR2w=", "Cwpyy5S3UyFyG1f2daaZoIXHW/TZ1p4mmRm/LxrRQv0=", "AXkG5WwmBkTQBHVvWRuFEw=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
			["WindowsNT_CurrentVersion_Windows"] = Bfs.Create("OxilXus0Vman9cdzawT/4WqXSXL1ZkBTymgKk4diB1cKKDoIlF6mdsLFqmYdpohUsUG0LKQSpzdGHIoFMlzfBw==", "sSE88w5gnHsVpQsop6jGQ0zTEcEHqoagUN9oUd+UqxM=", "8VYD6yMOJ4l2CsAnoQCLwg=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
			["WindowsNT_CurrentVersion_Winlogon"] = Bfs.Create("G191y/xnZ//lCv5gCiCYSYq6N6Ayd35QAISB8LMN6mvly07XGwQrJqC6K0LXXoCjJIp/T3dzXzUG8chdkWScmQ==", "6t0YXgB+9d06EkIMmoYU3L4x8Y+Xln1NF9TPfHR1DIc=", "8TI7Kc/gAqndaSPPzmJG7Q=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon
			["StartupRun"] = Bfs.Create("uYiZw5rg+Ebz/GlSHCZpNYMTWxFohfLuqqoUtnoNGXvBDuqCs3+KwE2SDjGvfRmg", "xKYHU7BRWc+lb3yttOyG0r31Z5chYYlNKk664dOp9HI=", "F8AGdzzw6R36M4lGb9Yg8Q=="), //Software\Microsoft\Windows\CurrentVersion\Run
			["WDExclusionsPolicies"] = Bfs.Create("drUeNDNEUwz3w8J9FnDtvqDt4ByEayUbr+FmFG3rn778/Tbp4Icp/MMxfTTYEicVimQ9xfzTJ+zQcm//lo026Q==", "wpVrTHGO27Tqpal/wptOLQdDJn/KofUfLq9NF2Mh4L0=", "0tU4/sODm4FEwyF/OBAHgg=="), //Software\Policies\Microsoft\Windows Defender\Exclusions
			["WDExclusionsLocal"] = Bfs.Create("9BwM+q2sMyudwiTJKKDk5t4ZX3zzQHTiTCa1tVGUcfY0FKLZ4Pb8aEhCtVinDIXu", "TT7MiQQH0apMrqrQ6bUhKMOhOMaIdHZ91rkYVMLGGmA=", "WRdk6eKIhwWwqYTj2KTBQg=="), //Software\Microsoft\Windows Defender\Exclusions
			["Wow6432Node_StartupRun"] = Bfs.Create("RPS01rxTr6de2+VlBcqHcCnZNH8ZrAUKVy4jAG3G6zsd08fBXo6oP5u5W16BI6OE3chG9PbhvEGKdP7YEaZdsQ==", "rfyk4Gvf6OoU/kMyN2imwPIf53xYYFMjfxAf8bMN+bA=", "CZ4OzGrcWeAgU7KH8QfByQ=="), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
			["PowerShellPath"] = Drive.Letter + Bfs.Create("hkR0+d37cxgebUwlG89WbwqEOPugKkzuMhTbelEC6p7KeG1sjx/ot2uC0wUA9BP6", "h9p5DtrHRZlBkDy8WBH49eoBGTCV3ekjcgEfoe2asEk=", "LYM5VW6/901D9MQNmMIxzg=="), //:\Windows\System32\WindowsPowerShell\v1.0
			["Defender_AddExclusionPath"] = Bfs.Create("AfTxoPnsm6QSiDJ9O4GHgGNMaGFMkf1ZRQ3fbyc8JR0=", "W4KGvqvuKgByq8XsXC/cFEGn0SZHmqu7Ox7BxARr5jI=", "YsxKu6rYNawmfSV1v//VqQ=="), //Add-MpPreference -ExclusionPath
			["TermServiceParameters"] = Bfs.Create("GB/wuoupfBGS6BcrbGv1U+GR5V3GQ8xrCMX9kqUuBES1z8sc+5yBrv+iMvWrOvncl5MQZ5zEOZXHlwki2AYgIA==", "pn8KoYTG/Rfu9kvooXCmj1cQq7581b/Qu6bf19iA0Ek=", "CE+8v3i50DgtzIyfo96mZQ=="), //SYSTEM\CurrentControlSet\Services\TermService\Parameters
			["TermsrvDll"] = Bfs.Create("PtjJ/WBJaLk8cvLDyWD1gXWP/8UawAKQ+WmB7qRczosUXEkeKuT0ViWo1+AVLuQG", "390R0+9cONUGKGO6lV1WWslNMhynLZQwq17MqdPxyOM=", "x0Tr5lE8SbsWvxMW4vKHcQ=="), //%SystemRoot%\System32\termsrv.dll
			["IFEO"] = Bfs.Create("wt4NG0ZtQH35vGbiPZGPlU65d2/s6M+w/IxSGcqND065L+bOELQh6G10aX6FOSEx9sC7A3NfPumRW9T9d8Gw3RmAHwg58t8+uuk6Nv6eeXo=", "wIc26cvrWDMyU2bjdPT0phkoFXHOtfbfVpZgcx+Lgns=", "um029ysCk3m2oktF8EcfKg=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
			["Wow6432Node_IFEO"] = Bfs.Create("62dTr5cpvUMaKzOSasGHFWE+5ZGgYiNakZFwsWgqHdwGGMKwSBgZpzqVfI5fzhcSmrY1ZqU5ezhjHl23o7owNhY9tg4nG+nG1wSCn0GmZ6+O0NsRHgZ4w/5dJR6unMes", "Yl4LRa84yHM9Ixejv+Fc/8S6SLVs8uueWu0jIM+/930=", "UAbtEx5+A1hIFg8z3AbpuQ=="), //SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
			["SilentProcessExit"] = Bfs.Create("bIznTKScOKlnwJR/IMBDkssXPNJ6uGVP/ZaHprZbzmL6n1+/yliKondTJ8RPHfQJkgxUrkPRw3UYpFaF976KYw==", "dwDm9vpLhhBLmodTRY+MTzC0i2BZronA0XpUwAmCUaY=", "krcOdjpaC2BGanofg1EL8w=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\SilentProcessExit
			["h0sts"] = Bfs.Create("Vp8cqvh+UGiEwiiU8adV2ohdgIHodgOVlJNAmJYtkQ3KHgWsbI9lxPgoLCZDk7TI", "Q/78Jc8c6Ck6ZQYWBOV1sUaJixHiNjBO26RnRRfaLyQ=", "kypRRcNcKbB7xwg+iGdzNA=="), //:\Windows\System32\drivers\etc\hosts
			["appl0cker"] = Bfs.Create("L6zA0R8JZYw3zi0rRBporv5brttBMNVnqPhpJcWRBkautX+WeuPonz8Bfri6MbpU", "u+PHC1bkPBrWsatlTfldhCK2aoIZlYbr3wOabbCiHp0=", "dQ0sHbGE8n3RQ6mDqhIuWw=="), //SOFTWARE\Policies\Microsoft\Windows\SrpV2\Exe
		};

        public string[] SysFileName = new string[] {
		Bfs.Create("K1XC9VMMonWwFV6PYTtL4A==","Od+/3yuELv0lZvECZxZ4R3YlE9lmaD0OeYLV3YTbeDI=", "WBW9RtRBBSsPH102MpIDsA=="), //audiodg
		Bfs.Create("0ayECFetiSqVUg0zWxmq7Q==","0ZjEZTnRHqIQlSYN/SozHVrBLsCfbanjfwKwVJaOA/8=", "E3/Q051j3O473L1VKumC3Q=="), //taskhostw
		Bfs.Create("hwnKcn5A+ELBMsKuyagtUA==","jILsFDOkqrzV1CD6oFbw+FqHz/dlNn+VZGgf8UHnh3I=", "WM9BPkPeYRqGNy4mwdZowA=="), //taskhost
		Bfs.Create("UVYIQKvtRBinD2dIXbp6xg==","nfhQ7kY9TccWvLzi8h3f/INEubp5Ft5zKtV2k7D+LBw=", "PNzBMTYzhzz1n5GoTHq4IA=="), //conhost
		Bfs.Create("sGjs7mhMNBqzGFytlTet1Q==","L+kBjqOI5+O/08OiXmjPxszcv26IuF30w1V61mSHf3A=", "W1r75H8V8fji6YvoHFKgLQ=="), //svchost
		Bfs.Create("RRRPbHOK2nSeWbHvzt1YCg==","8pVxsLALE9Bx9sGQXPjiRRWnRoD+froME9ZfMTxng1Y=", "jByL8pZ1yDzdMuIkjUxjoQ=="), //dwm
		Bfs.Create("lQwwb+ua9EI3dUHY0I2G4g==","TNfViqPUq8ybhuyrCr08uN6Gj9Rs8YyvzG7MgUJKZuk=", "AmBIcKfGZVCaOhqkr3UewQ=="), //rundll32
		Bfs.Create("sfIBSAOEIVidJSaT1Pb/fA==","LZ4tY5oNNZ/8cypEhqlcZIuZZ0Rfu6zwJLOnFKrM5L0=", "lv5nFer3xkUp5lj49e0FRQ=="), //winlogon
		Bfs.Create("X7LphwTTkVh+efqoRvhxyA==","kw7kqbkcpbvCJkxkDaaSUVrOf9pxKTEUhFCMfqqhVrc=", "WQ60oFmMAt9W5wF19d6uOg=="), //csrss
		Bfs.Create("HIIGq4Z1IZUOtZcuuZ5dtw==","ccso4KOnxeBAnTnayrCLcN35PjeEwUSxjtv5gLpeDWc=", "At1EzHrLKOx/BPT5wsgL3g=="), //services
		Bfs.Create("gEUleDGR+tsbeMxr2ABltg==","/IfB0QR5LO+kFfthl96B/CI4Gfx3x4MI3j2IksKW4pw=", "EGCW5OYE1niXmueJP4Jm+Q=="), //lsass
		Bfs.Create("eH/nIqdlYsHJvBJ6pIsLIQ==","UDwWBCjEA71nE5q1DjukhCwHDKKEE3w29dTVTRtoNb4=", "8UejiIg5JIqEzbwXWoB6BA=="), //dllhost
		Bfs.Create("N6TAQFy8MTrzjBQ5TUWy7A==","SqHAAWR01KZe9226+9t7icyGaLrolvKI1wc1/8Lq+PM=", "ZFAidfqCg0SCLHWpMwcQ+w=="), //smss
		Bfs.Create("iwRWfwZIZIlt0K2oZdOW8Q==","3w2IOTakzGM/5a5E5jhePNBFLrmqwViLI+6/lQ+iN0w=", "JWV2ch3tmYZ0/j1J7sjfFA=="), //wininit
		Bfs.Create("H2RalWnX/wH10uVGro4Wdw==","HKsaWcJ4q0EmjLlX/m3bjwR2DBtrcpfP9bg0Xomai9c=", "+Lq3Rw+SJh0q0LMXbTyYdQ=="), //vbc
		Bfs.Create("yTLytrVyVua4yTgJ3d8pCg==","GnWnWdluqFP8l1zn6K6S7i2oCPjDMD4O5Hzqu+LchGE=", "Ls1DAKCqpLVdWQofpRMwTQ=="), //unsecapp
		Bfs.Create("CrNn10J29kIiM/1sHelCoQ==","PTqFdQbmeiBw+S5P2l5rF1IrwPoUP7PG8NMEY9xA0XQ=", "IrGmVh3p7yszZNvStNzEuw=="), //ngen
		Bfs.Create("sZN0Mj+D+Hj3/LaWfubvLQ==","e9C6bgNtFyDDSnOGwJido8ga6SUKVB5WL0+hDbwWYko=", "o+EcmrmoQfeCdn+zxOJTIw=="), //dialer
		Bfs.Create("DFCW7keZ+4R87DzNi6iCMg==","SZQtfZ0c1QGmBHMzNFVkbHJir6etZNohVbf6C/Bw9gw=", "IShDuI4LHdnIzrT7CKgYGA=="), //tcpsvcs
		Bfs.Create("G894BzcD1PQcfRuJrC1H7g==","Fyzr+ul+JDQhzI0B/2yyTVWPVI9/ckIcWPW3qjokKik=", "aVQDBpvVX/x55zZnIY4NCw=="), //print
		Bfs.Create("6XPCcPTyV0bnyU6j/lY1jg==","jiCJydaCrwByrAwxCHMEijy4Ur/3rugvmosbtf4Dfec=", "uI8YiHbRJASw7JAATFsq7A=="), //find
		Bfs.Create("Oj095D+80kVlAKLJuqvfuA==","61hFHwWPbuE3HLr6nut+nKfRoHP7CcJFaHwj/q3Awus=", "x1pGpQzquxLXZxvF84WWDA=="), //winver
		Bfs.Create("WhK2aijTxOikOW8g0bdZaA==","or4eyJzQzOwHLB5DP02QULrRnTdmXEnwMFjL7UyhIoE=", "6SEwodjjyw/ABywJTrfNRA=="), //ping
		Bfs.Create("F0zyw2SP273Rui7LF7PTJg==","cVXQGRIgPmRByJ8HdYMp33GZaGBgNE7FSgRyJ+9rz3A=", "CHgY9piWz1q0dMxKZFwugQ=="), //fc
		Bfs.Create("BzWYIglRpBGKSaUsZL/9IA==","U/WQQ4Sc2cf1n/5ONl3JCnfT8VCUPwxy5yIxqRZNXaU=", "2jDeg3ZFTYlYo4NK1kpycA=="), //help
		Bfs.Create("qGk67rYzihoHuzpnUg4Diw==","vLyS9dkD8b3ryCUAsXD0rJQ/3EzX3k0Y6T32jLZAPN4=", "hVrc4c7HU9iHkc/78wP3yQ=="), //sort
		Bfs.Create("x5KGf5tkXsWhBsxk3a4eHg==","BoczSxgOGJbrxyJL0E2pVC1zi5OMS5KcOA+omDCDwkE=", "KjgzB/ghJyJZoQTx7Xv3hQ=="), //label
		Bfs.Create("IvQvvg7p3kKvjB5xg1Elxw==","njHQyLG7qoJbT42UkKREg26PwmfKSP/ufB3zDZzqFME=", "M3Va80Ftkq4v8RmJjuP3ZQ=="), //runtimebroker
		Bfs.Create("BQAixGHflUgLsPOxH3lcdQ==","+ufDywzkcUe/VvqA695WGhea4D40xA+11mnTQ1hAwwQ=", "1BZF78e9OMUku1T2HUjbMA=="), //compattelrunner
		Bfs.Create("cHLX6VMDLkAc8cO4FHlqGA==","LBh4jcd6ossdfR7lWnFyvgWCjOoy2W37uSB2qiVgH5E=", "2f0iR0o1dNpxteAqeGsqEQ=="), //sgrmbroker
		Bfs.Create("kpM2FcrVx2ZNyY8my/qV+Q==","7TG/Eb3NtlQIUWa8jjLEudY44GWEjfaKLpW6Ec2C9jI=", "kBXN/9D45dYZRFiUF9Jl1A=="), //fontdrvhost
		Bfs.Create("watG0bgV1tX9sHo2wsO6qQ==","cnHbiILS8J3izAX6DKQ8aQAi3iRMHbQwY+JCN1J1Jls=", "fhCniRYEvfWCq1nDdDEgRA=="), //dwwin
		Bfs.Create("u4FLR1aqO7lA3p3CXI/pOg==","O5dSC8zQ9BOmxp+QRi3+rMLR8oQjlez+RglJX2Orp4A=", "l90t9BXa6ESyXh9YheEoRQ=="), //regasm
		Bfs.Create("4A480N828Yfz4S5vpsCQn0RSl9VyHEwiAfBtyOHnw6Y=","qezqRS7NutPo3MrvpwhB+cVjbslgkKxAmFinNgQxgZU=", "STvPKcvXqgn//IZ7l+EqRg=="), //searchprotocolhost
		Bfs.Create("BY+fhkefgYQlevAmFV5srg==","hyA2k43GAIYpPtZAm/ZOQo7uEJWrc1tfgevY3jHgbjI=", "RrNk6gpf4MBKkAsoHon7qA=="), //addinprocess
		Bfs.Create("i87Hx+IZnZdfFmcjBzjslQ==","vTGFra840Mc5mLD5wOAl6DV6tOFHk0u2in95/a7AIvo=", "rjuM4fMZpv7KzLBZ6VUdxA=="), //regsvcs
		Bfs.Create("69zsBuoOEYGaBu9WVkCMLA==","V7TF9/2UN9VYKGoEhZdvE+MP6I7pAiwxma/VczjY024=", "/3FUxEWTrSWTBO9Vz4/o9w=="), //mousocoreworker
		Bfs.Create("YV5QZfwi7aQD1kSpah/VXg==","dmIdcicDDqOqPNP3HoIJzamnHAHvYYLxkK5KTXT36o0=", "VEW7hvewJIVHKbRxTyPw5w=="), //wmiprvse
		Bfs.Create("CL9wi0UWN0UIAYkdDsHEpw==","e/y9nfRj8bnGlnh6TqAlJqBko5DBdswqvH6iXcSGEL0=", "RqzRg7sCYwC/0D6C2z2Mfg=="), //useroobebroker
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
			38872, //regsvcs
			1974272, //MoUSOCoreWorker
			499712, //WmiPrvSE
			90112 //UserOOBEBroker
		};

        public string[] sideloadableDlls = new string[] {
		Bfs.Create("9mMiXTq2bEM94JOjeB0p0w==","r71kzUFDh99SS6mcXN1N9au5HpF80nGUlh6Xdw6a2O0=", "9dQhPQG/rtZnzD/sSjKxpg=="), //SbieDll.dll
		Bfs.Create("jrwVOnxSbaK1bR0KX2+vdQ==","6zuc9zMhfqty/Y20i8PqD6ht33aPbb/CtuWitZ8V42M=", "ANT0dIXfx0RJ7JlBSYNRyg=="), //MSASN1.dll
		};

		public string[] trustedProcesses = new string[] {
		Bfs.Create("uyAsTtr/nchTfVpMe/i5gZ/PbyKeoQKIhdYJTZ2nqk0=","GveAIPgDr5zSmFkv4xj5Vp61ITd384DoJdSN0xWHaac=", "BEUmASTH8YZsJ9UOhQfsqQ=="), //HPPrintScanDoctorService.exe
		Bfs.Create("x9uuaHAq4aE9dDUOIrUfIA==","M3eY33u5sZNrQ9CfDMVh4F6GcTVgqENXJ/Rh2823uo4=", "YvCWjwcM4+J3NyL/4pn8xA=="), //RobloxApp.exe
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
            new byte[] {0x67,0x64,0x6D,0x6D,0x6E,0x6A,0x6F,0x66,0x73,},
            new byte[] {0x4B,0x74,0x6F,0x62,0x73,0x6E,0x6A,0x6F,0x66,0x73},
            new byte[] {0x44,0x73,0x66,0x73,0x6F,0x62,0x6D,0x63,0x6D,0x76,0x66,},
            new byte[] {0x52,0x67,0x66,0x6D,0x6D,0x64,0x70,0x65,0x66,0x47,0x6A,0x6D,0x66,},
            new byte[] {0x2D,0x64,0x75,0x69,0x66,0x73,0x6E,0x6A,0x6F,0x66,0x2F,0x70,0x73,0x68 },
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
            AddObfPath(obfStr1, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "cl?ient?hel?per-up?da?ter".Replace("?", ""));
            AddObfPath(obfStr1, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "tor?ren?tpro-up?dater".Replace("?", ""));
            AddObfPath(obfStr1, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Programs", "Common", "OneDriveCloud");
            AddObfPath(obfStr1, "AppData", false, "sysf?il?es".Replace("?", ""));
            AddObfPath(obfStr1, "AppData", false, "Dri?vers?Update".Replace("?", ""));

            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "cl?ient?hel?per-up?da?ter".Replace("?", ""), "in?stalle?r.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "to?rre??ntpro-up?dater".Replace("?", ""), "insta?ll??er.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Programs", "Common", "OneDriveCloud", "ta?skh?os?tw.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Programs", "High?Stone".Replace("?", ""), "H?ighS?tone.?exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Microsoft", "Edge", "System", "upd??ate.ex?e".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Microsoft", "Windows", "Explorer", "Wi??nUpd??ate-??NF1?3A7?3.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Microsoft", "Windows", "Explorer", "Wi?nUpdate?-N?F16?A33".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Microsoft", "Windows", "Explorer", "Win?Update-A0s?YHT?aM?Ea3.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Microsoft", "W?inD?riv?er.e?xe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "Microsoft", "Up?date??Task?M?a?n?ager.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "Google", "Chrome", "up?dat?er.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "Explor", "exp??lor?ing".Replace("?", "")); //the file has no extensions
            AddObfPath(obfStr2, "AppData", false, "microsoft", "mi?cro?soft?web.{7007acc7-?3202-11d1?-aad2?-00805fc1270e?}".Replace("?", ""), "sys?temo?nedr?ivesv?c64?a.exe".Replace("?", ""));

            AddObfPath(obfStr2, "AppData", false, "sy?sca?che".Replace("?",""), "k?rn??lho??st.exe".Replace("?", ""));
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
