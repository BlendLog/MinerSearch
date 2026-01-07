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
			new HashedString("3afb052104deb95bc99eee868c8644f8",18), //msch3295connect.ru
			new HashedString("974bf1d93d81d915800bb2e5352b923e",39), //msnbot-65-52-108-33.search.msn.comments
			new HashedString("f6ce7e3db235723091e59a653e7d96f2",9), //mywot.com
			new HashedString("4a73bdc9cec00bbb9f05bc79cbc130b4",9), //mzrst.com
			new HashedString("3d62ee7e9bada438b991f23890747534",9), //nanoav.ru
			new HashedString("b0655a2541be60f6b00841fdcba1a2df",10), //nashnet.ua
			new HashedString("13805dd1b3a52b30ab43114c184dc266",13), //nnm-club.name
			new HashedString("4e42a4a95cf99a3d088efba6f84068c4",10), //norton.com
			new HashedString("84eac61e5ebc87c23550d11bce7cab5d",17), //novirusthanks.org
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

        public List<string> obfStr1 = new List<string>() {
        Drive.Letter + Bfs.Create("ZWDrXMKOPjFFeKX0LdDw7sBI0ApPNTSqOKQdAZtuCoM=","mSDkazMcjve1TZ8tjujl76tSEQAH4bQV9AGAON48k9s=", "ihO5s0NBMxx2Rzcpy5Ekjg=="), //:\ProgramData\Install
		Drive.Letter + Bfs.Create("pJzYG1cD9abGxCL/QaI4705VXn+WP1wbHfbIESrVfo0=","qGjSMe8dWPusIuSJzo9SLze8ZBe0JyaryheuOn+rQIU=", "a85ORxO/WaWJKFpi05wn+A=="), //:\ProgramData\$pwnKernelSystem
		Drive.Letter + Bfs.Create("llEERGwyVAt3lfla3Abfez8RDLqyhFlNc15kj7vdpIM=","6gWNltHwEHFsb9vUBUKWfeOqicWA4V33sdwuEAzlhl8=", "EwCo0cWRRQ2E7uECDNS8NA=="), //:\ProgramData\Microsoft\Check
		Drive.Letter + Bfs.Create("zwDIxWbvgiuIgx0ogScLAwQVuBkHB0spsAOSiLjff1I=","zLNjQTeXTPtbEwA3lHOAdeAjFHeKRXC5SkrI+V/oL70=", "Q1PkxbO27a9W8vBqfuNoqA=="), //:\ProgramData\Microsoft\Intel
		Drive.Letter + Bfs.Create("dfX4gNwZvKr6f9OLPM10o6Y4CPt4FgdjUy/mx3F0z5QJUfh5AjYe9LhDZRMFEzrxVIL8akd/SWOU/TC3ynYFuw==","mZQF2yucbM35GSl7SLZzgs1hfC012vbXX7LnBzmFnVc=", "lhr9azpNIXFRkLP+Vi/fOA=="), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
		Drive.Letter + Bfs.Create("w/bstjaCrTfm86CuL2ze6fMbjmKGgpWEMTzGbL6+cXM=","kDPOLbVIwnSi3VsFwgxkBBy88dTS+QpwCBtJdCd0kAM=", "hZmlD3ZFRDDRymqCKvINlQ=="), //:\ProgramData\Microsoft\temp
		Drive.Letter + Bfs.Create("jeNWV/X8WIg/GfGFNf3i3bTavbul8e2bgoGRQ0zCWLU=","YBaax+cQJSKa2nFYLKJk2RHvLsCNMp2eTWXn9n6xpFM=", "rmJPJJxnxnRdR+2NTI4jTQ=="), //:\ProgramData\PuzzleMedia
		Drive.Letter + Bfs.Create("QXQp4dH33hMd0q1VQSv7pk5eb5pZo0wyBGoBAztOqBk=","88AeJOEkkSYGFR/YW/LfuB4ug2rVIEceG7KwiZTHBp8=", "U9EpMImw+iDavQhi2naVjQ=="), //:\ProgramData\RealtekHD
		Drive.Letter + Bfs.Create("tpz3kkABkpOIkoxmPhGeRqlT6i13qlOuSYwQGDLZuTE=","8lh/bjk/KhaGsHEI7W2o6AXb4ZuDJGOZVuIAe8L5h24=", "sJQF/UnCF3zCfND+56niNQ=="), //:\ProgramData\ReaItekHD
		Drive.Letter + Bfs.Create("+vvViTlhYALkCBLE5gXapwA1rrpyFF0m/HR88ZgYqJ8=","eGM96Vgh9tTEIjWsmKK8aIhyXUpiX8tKAWdtTeLUMYw=", "jj5gVWUaUnUizMrfXFqGZQ=="), //:\ProgramData\RobotDemo
		Drive.Letter + Bfs.Create("QAtxWhTZ6HiMpO6YoSTByFow2DPPDvQAqRINc6AA3/8=","6JDXAT78QZ+p1jxTW1vHKcWEDzwQyn3nZikScUDy61g=", "AFwRnyR5P1nqYjE/Gz3+jA=="), //:\ProgramData\RunDLL
		Drive.Letter + Bfs.Create("pK+AqJfau6JZkkzqXvbjVxlc6pnG0y9e7ozBpFs8xX8=","L2tYzJhbDezWyX7cjcSYNNLWFNFeMt7E85Q6AUgTQNQ=", "nOO56OWFvM+2CIFUgf6puQ=="), //:\ProgramData\Setup
		Drive.Letter + Bfs.Create("HIQtpvX1d4tMd2vPaQG48O3Yt1st5v+8BSa6BNxIizE=","3oknyTwB2BCZd2VZL5bWKwObZ6YRJwFyo1hOknL+p7g=", "mt+Q2KDCdiui+Q7xlV3iHQ=="), //:\ProgramData\System32
		Drive.Letter + Bfs.Create("jM7DSTf8kBLiw1JKCM3sgyigegDDnJdE85AOrSlnlo8=","Eia7FM77NT8lBOnHEROnbJ2h16ACZiPAPIw3dKF5Dxk=", "ndwA2FARbZMNyBR1eVlNuw=="), //:\ProgramData\WavePad
		Drive.Letter + Bfs.Create("x0jEo+EQnD6sug83xflS8rqpy0HGVbQ0UhSNT8uKbOkHgzrM9VDMd0Sw5FTq/IzH","X7bxY0RGKKl5k0mpmCQqdwvfZit0me1awrIXk8fi3m0=", "bBvG45lPTinQ8BMtQslvNQ=="), //:\ProgramData\Windows Tasks Service
		Drive.Letter + Bfs.Create("93aXfASAZbUciwjACT5UgJ5zvrPN3keOSVqf/gd+NQU=","DkiJwI50fyXdw1gIiGi9Q5cXtIgSMNJjDtr9qXLJ7gY=", "lHfS1nOIGFDzBZm0xm+8AQ=="), //:\ProgramData\WindowsTask
		Drive.Letter + Bfs.Create("6zpsNIvrrA6bmgAcAlm7TUCizZ6DcK9AfSKwC7mx+H4=","WKmfRybQ9D6tfdxpokdQHh33uR0NhaXoX8MmCR+skc0=", "LLV4POWLIibi6V4IccqNig=="), //:\ProgramData\Google\Chrome
		Drive.Letter + Bfs.Create("YliE3bYM6TdDXBps7IdG7+I4nPRWTTqwUczjBYpYsJiB6CD5SvABaq/D8pKjGe6x","P3antJz5g2iolTnCmvvt4Z6AzxgLLFPxO/oC8cuPaQM=", "NoSgO6YJQJ0UL589ehLT0w=="), //:\ProgramData\DiagnosisSync\current
		Drive.Letter + Bfs.Create("rn+Tz5C1K4A6X6UMagshK8hQie0HTbygcYiTxNeCcz4=","SGM3eLDxMXw0IYdCJl+eANGq7KMe/w9+k+XNQigyaKg=", "VYw3UOmFm2+ek7AXHz81gg=="), //:\Program Files\Google\Libs
		Drive.Letter + Bfs.Create("qlwocIuC5A6mOJPbOJfkS66bB46dFOOTJK5IWK6PPE4=","6fQPJB/XKeKKSKPY1q0ERcAG1uTWUD/5AL7Evvh/bl0=", "sCT7gBUt6ehfs/TWFZ5tQQ=="), //:\Windows\Fonts\Mysql
		Drive.Letter + Bfs.Create("3Cd390yuYpBdojCKhYeAWm8Bj6xjrKOIxuxyajytGDJpQF4BEvRlWQ+dsY4n98ZH","FEBBmtwd3p/w0wLQLFsmzbQ1/Rlxy7VxjnkdmppiDEE=", "ZLq5z+hUH03p+Luo5EQCDA=="), //:\Program Files\Internet Explorer\bin
		Drive.Letter + Bfs.Create("y3e5lJK09OD1CLCt+HM0xGELkostX1CXgrK1OXI1bhQ=","nfr5Hzjofp0U1ep3WhOab510g3ySK6LVs4kjH/CBvMM=", "BMKJb7qppKAH1rYGkPuMmg=="), //:\ProgramData\princeton-produce
		Drive.Letter + Bfs.Create("lRXYcDtKQfZ1NWNQGPELcBE0uykzwJixh5e35ouR3f8=","YspjB7pg+PHRUyZ+IVl/bHa35AL5wiXJ7HAC181NSfU=", "fn6LoEVGaZyKmL+vWVPA9w=="), //:\ProgramData\Timeupper
		Drive.Letter + Bfs.Create("n4ufPfyu0NATHmu4zeza8XLNwf7sg3JhYAxkr0kO4+4=","B3WiuSxfuhAE4JVBtmykLcWCnrP2KfHWo7CbH8AtjaU=", "tfSkxMG8YYacBIuQnhnj8A=="), //:\Program Files\Client Helper
		Drive.Letter + Bfs.Create("wi3MvlONY+l4w4827WDT7ZU12vyJtOBCn6jb6uIqLrU=","mjkOJXFER47I2Os/XdL7BSeIUud6hZvrERIrPhpMwVg=", "scUP5dMgFM5Y5YgKK9BnFQ=="), //:\Program Files\qBittorrentPro
		@"\\?\" + Drive.Letter + Bfs.Create("Ua8fsYCI1luRvEfMpc5nFOtEcTjP+t7/LXxZ6RKF52c=","5ZYgjbecqjY8UO8lfbd57DKH3ELL1n/t8h/Z/VPyAP0=", "7VD2QnULrOAVbuKl9TLBVQ=="), //:\ProgramData\AUX..
		@"\\?\" + Drive.Letter + Bfs.Create("4bvB3Fy0CCO5GeDZJO3VafQraOtW7tuHg1QL5obX0Ck=","L15LmDMuQKRZ+TsBlE6GXAS1r47qyFi28tvRh7ko9bM=", "Fv2ED4a52JHeU5queUffuA=="), //:\ProgramData\NUL..
		@"\\?\" + Drive.Letter + Bfs.Create("+pTWZrcZIwLklXO/pS8IOSUgVZNZvM2ibvaYj+TFleM=","OeFcUwu0HeTxb/jZovuWsSUhi8I/FzNPHXO2H1p2gxY=", "fW0vWGrZbu/FubQIG5wRJA=="), //:\ProgramData\CON..
		Drive.Letter + Bfs.Create("FI7nbR/qLlUend9nhiZet07isLQFIcyNPukFISZrCAY=","QCdR+xEC7wrS9TtUQUhUMLxpmT5W+m7hz8HSMmBsr74=", "BoJYQxijTH7xKSJCNNK2ZA=="), //:\ProgramData\Jedist
		Drive.Letter + Bfs.Create("DB7USvUsii0vP+vvCQVAVUfP+gRDIATco2oDJTQsajvrg6WUNwo/SrVqruOvlwBrpV6ybKabzG9ToNgY8FED4Q==","IyrZvlPGJ8yb+aOHE3KawpHyq6Ca7AeHeDJXcZyLnkE=", "Y2KxbZ60NFtJZgkQNHtkRg=="), //:\ProgramData\Classic.{BB06C0E4-D293-4f75-8A90-CB05B6477EEE}
		Drive.Letter + Bfs.Create("S4Bajny70amy5qnyAN6+DRyeFnNQ0/owH0MDN7ird39+7jxa2dIVoRb8jvrqsaJbj1/qgb0381yaWwhseOREbg==","fuXOx08m1WCrJsGu3iBTeDquq5TROxigu3mVVr3qe8I=", "f+c7fQRB7hg2R1NEHk9XAg=="), //:\ProgramData\Classic.{BB64F8A7-BEE7-4E1A-AB8D-7D8273F7FDB6}
		Drive.Letter + Bfs.Create("dU9SQk05LagLPOLDuHHPRro3rpNO43W3XQPqi7m/d7g=","gVypAzwM9Ez3AIPzpAaNZkoUgD/yfMj5Ew6UHIqoxdI=", "/LiYnfZhwXTAIiMQEOYyiA=="), //:\ProgramData\Gedist
		Drive.Letter + Bfs.Create("Uwqv8hJSD+5TX7Cx5r4gK1J/0ZRmtaJIwrfiodEoHPU=","Rb6DLuqSpAb5ofCkWtQUsnVd4FndYHVeeadEposI5Go=", "tRcu3hSHTvo/gRe539VrcQ=="), //:\ProgramData\Vedist
		Drive.Letter + Bfs.Create("j7XgPW5//IPxMYpBpPekm4CQdO3XEfYcJLzeoMupOTE=","7EE4PPLnciF43e2h6NlwxkDMr4GpImY50C1l5a7xHQI=", "wsZd37upxgE1NtLG+YQv+Q=="), //:\ProgramData\WindowsDefender
		Drive.Letter + Bfs.Create("hbf3sD848r7OpCbtGzQJfHHOAMxxm3JN51pwBa23dF4=","YHiz4rVMoKBDmKOvuVHTL9HWowipNYfYHWBYhv64c78=", "tFfvOakOPe++aq4LsQ81mA=="), //:\ProgramData\WindowsServices
		Drive.Letter + Bfs.Create("zFjm8hOsbcjREwksJ3y9PHw6puHeh6yJ04PZduBOvQatl7G80EWMReHcrW88BJ60","aqboKkqDnYM94Xsx1oGi+xV8MG0qcdXsn2hBrclOLSM=", "6L4YZzk8W2Aj0sOkLaxdXg=="), //:\Users\Public\Libraries\AMD\opencl
		Drive.Letter + Bfs.Create("KFHOlmVXM/rj8IWC2UlNu0KJQRXvBzz47Aqyv7NphfI07vLgWKQjXRUkisTMku4U","KhgAJEBsLDUBcUnyUk9y7m1ohGhlb6fXKTpvmNoMI8U=", "XEA8VjaqZLiqlWuZrjceJw=="), //:\Users\Public\Libraries\directx
		Drive.Letter + Bfs.Create("bZSnbpl9KIN9CLo3iDRDftT7tN7cm2A7c9pDAt9xHfY=","V2trcWjoDmKVZpUpqNG/ILoZw+aMUkLf0GhoiNPZIUs=", "nFt+cgeGwImfb7iG6YDONA=="), //:\ProgramData\DirectX\graphics
		};

        public List<string> obfStr2 = new List<string>() {
        Drive.Letter + Bfs.Create("3JlW52aLRwdUVU/mzTC5hQAoNU83ielOf3pYeROFwdA=","Clw2hhMMuiMshBLr9nqSq9xvbfS/JAHi9a1hEd6dyTk=", "dQ5tlHY7KUsUJWFZeb0ppA=="), //:\ProgramData\Microsoft\win.exe
		Drive.Letter + Bfs.Create("5+DKUxHCxWORST3zKwafkM5DtvChLlIxNo/B+XvfA8rWh1TCp7KynytaZIXs5fs6","NPWihER9ih9R01j8HscE6jY7spl0ajgLDUc6s1/M/No=", "F3BF2I0Z6Hetndi5+l/9LA=="), //:\Program Files\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("6AUYegy02p6Et73+U8UgnjcQH682GThJu/2IbNob62KcqvR9dDOKqAxmdnGydHTH","xVY+06gqLJ7VjvCiw9MaSBOUyQsdIbz86RCQ7ch2u2I=", "LHTnuqjHUjsJWY+o/Xw25Q=="), //:\Program Files\Google\Chrome\update.exe
		Drive.Letter + Bfs.Create("FRX8A4hpm1VD3LnxG+okCB0cBa/thnuzZZl40/ebcRf1b5MD75la9N0qr81m84Jm","+Eh0+hegswdfPM4vGV6GqyjxJrx5wmN4Qqxh18N2Wc4=", "QShVzNhhvRXw0mJ5ZUP/iQ=="), //:\Program Files\Client Helper\Client Helper.exe
		Drive.Letter + Bfs.Create("LzUGGwkDc9M7b28Q1r/UvaX+El2EbILIQ+TVWvOpbI5+q6bVPzWKMyQcyu5i/nNxmT7y1YBvdEZx5IX6BkZEGQ==","DIk//FA59nb3wf33MnTMO3d9QEwQwvNXPaprrOpFw2Y=", "h5UI3rn/Hh0xCXoI/nccIQ=="), //:\Program Files\qBittorrentPro\qBittorrentPro.exe
		Drive.Letter + Bfs.Create("gHKQYTtK+uUjMUZwZHV6IAvd0dTXbT0yshW1YYWPVan5ugMSLRlKfxi5MV1bJNa2gcj9dKZF2XyNKHzuVEPaHQ==","1CDfc1pJbdxOkA06xYCJfITHYOLpYkWHJ418bW4vqJo=", "FmqUSb7nDmn6WbwsbsITcA=="), //:\Program Files\Microsoft\Spelling\en-US\default.exe
		Drive.Letter + Bfs.Create("LHPokz3ZBU44JNE3MX69ZS4/KZJamF6bazeKjYTgwUU=","c1QaDITb/KmlTS2Wndnbvx6RaI3le7zKfCrFjvgqfxA=", "aTbM11K5TXmbSXjOnQEOFg=="), //:\Program Files\Game\game.exe
		Drive.Letter + Bfs.Create("vY0A797EGsr6grnkbgppCpaUwBs5i+acP2THoJQRr/5uY8QL9lr/x3wWexbGOY3o","GgjenknNESk/3uEG7MN2YYnQfaRRZd5n1mBChIvZNCw=", "VeYEsytg00vbVpp8lz56pg=="), //:\ProgramData\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("f5KbzNGXli/D/dEMpGkNgVlHFPAX3ysmwGmxsTKqQpmMS3UMOlnkwhDQ12eR4RwC","wQ9kBZfV4fYM8wzQkMbGHq8+MYN+CEscnXY2/gpO32E=", "A0RYHqaXkJRQ4NJtRsq6Sw=="), //:\ProgramData\Google\Chrome\update.exe
		Drive.Letter + Bfs.Create("HlpylCWLJIzhOD5qmw9TyRXVwzwD0lGtwNmTYxLO9cDc+8H/XSfkRPROIw9gF1Fh","Srt9+Z0PwvQy4/sJDc7GSrrCVlYE+3ufLI0cmEh3dpc=", "6JWzx2HCSSD1IZWc8HXTJA=="), //:\ProgramData\Microsoft\EdgeUpdate\Update.exe
		Drive.Letter + Bfs.Create("IGEio/utjs76gy1H9Co8zWXEffh0F/UEEbXr0jT84GQ/Y7C0xwNCL5g0bsxRCtfK","un5Si5QG/cTBeK+2zaxHcwe88REQOYNjRN4mj4qcX7o=", "f1Ln/15lO935mdsAxsAZBg=="), //:\ProgramData\Google\Chrome\SbieDll.Dll
		Drive.Letter + Bfs.Create("/pSg5zZZrVuQfdGKaIxNlN+SgeAndYoLOQz+WFb70CE=","ASghTZhi8+dyXD/YlucM0kiWx73BFqXNHNljqmIWfC8=", "58qajWbwVwfQfKWwa9kRCQ=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("/soUZRcrih9BVlsZVmtXIjaKSoip7U5rA3UttsS35PapjYT/cnWvkzKqOAHpe4R8","Ad08OTst2wREHJZo+uKwfGPrM3xE/X8CHeLwPLTh56c=", "+fuHwLX3LJxZE+Y3rY8AMg=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("v2y1g+wD8KzJ+QMF8vX8d0/N4hZL0nSL62mnli3tj7ikRoEX43b4xfYCKaynKx39","+POoyeubty6OE1c7KMi7Yq/weZ85W+kiG0nchXyFVq0=", "kHWO9gVojnQ4IT1CKvjT+A=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("RzuGvA+oiItkmEEsYt6yBg+roVZSp596tx5qQb5uMN6zdclrJK+ZMHdGyLluCwOP","NgxXsOIZ6Q0yqN5XFdN5pdtS4DrYgak1CulTwi36ecI=", "fVeHIRtG6lBgirgK5if0Mg=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("yONJD0hITTp7MhIpPOLJm/OUuEn9d9OxqQgFt7Fe6J3cIbt28WMYdxRwq+ONukat","T8V/5TCO9uewG6IzmbtqSCXrl/jvpaMHcL1qGqD2QMg=", "SH60OLAKNnx8XGWcDk/N4w=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("MZNx99oCMo5n/MrWWnxfWMKOoodw9Ugm7EA6gDtGzCEiKVF2L4x6bUQptTMc0jzL","RAy07RRO+d3ybcrTtmNEWqV4FiyMHi47CnNGmlj8Nd8=", "JnTir+LFr2o1qXVBc0H3jg=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("I946l/UTQEj8TrKVFBi9OTZKbuESlCEIOiVG1iqTv8MYisQ7Zi0c4j5zVBAyzQVk","Ixn0vA1zE5u0uf2NBf1O/aftmHy+83mnqS5+VdAFIyQ=", "cmGqTNjvOEKp0q5y45Ek4A=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("t67gqy5064NlxHAeMzq4sgvkw+UcXmBKbUzFGX/KDJTH5bRRhksUosHInBMD9J2M","7qf1BOhKDKKaH1+s4jlRk/+669fh0Z0iTBBmDTN3jNU=", "BOj1AwFGyWOl5RhbjqoHNA=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("6MMorKcSJ4Bmr8NSzHa0eTZrjjdeRqGl2BCXIIRWnJPuUBqyBjP1QVYNiAFVqBmR","Ujus8FPTGMED7oJsAuv35ewPB6iQ4ba6siFgjSBKRt0=", "ApGs1YuMuqqGRhzLcVpJ+A=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("ocC+hoowtCxTW/q/Y72Ofs9U+ayk50Kj4BFhGPeUDaOD/TUIJR/0cMZqgU909yiU","/3RFisYjRL5/crP1v8a5LdA/Nv+v17SQZJaNJcu4DCI=", "yedmI2h/mpnqUUnxYMGabw=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("ChWLLwABFHNpHoDeMSIAOutWU7HbqZoARGT9pFnGogPM7okl5G6NN4ZCs8TmT0dF","7feVEgdbPPGoyBd7kk6DEsAhXe/wNz431FuYcV/7HtQ=", "NYXaCPbSEX1PieiGwD+uvw=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("3zX+qV8YddMLyf+OGGljXKC0fi8+XTuRD7kHPXQFVeY=","Zfe1b/Ygww8HrjMCOMLqI8sVj289QxvSDyLC0Hc8LDA=", "uT0cy+aJBfPlSvxgHNHrAw=="), //:\ProgramData\RuntimeBroker.exe
		Drive.Letter + Bfs.Create("qZB4gSqFytIKVWMxwoXtvCLejFw1z9ZpIDbiorTOg1R3lskdtV6v9PLWqlT5Yazr","peoqBoSSiZydIFeEHhehYB8P9M9YmS2NAMiYa1PRZ/g=", "5nf4rP251FOG9HtKbNVvvg=="), //:\ProgramData\WinUpdate32\Updater.exe
		Drive.Letter + Bfs.Create("SQePmaojqDwGVOKFZgUXUU99YAylNdyfjCYYqUnrSp2R38j/2ztR5/RBYLyS9wZML3fmJV8GTqh7BKHF3onYjw==","L+3+1ApG0gbv259eKUltrpPRrmf/2kv6ZLA1ip6zWIw=", "E0abXb17g8pVx9lwhuTtqQ=="), //:\ProgramData\DiagnosisSync\current\Microsoft.exe
		Drive.Letter + Bfs.Create("owvHzeDkPxlwzsCMYSlRZ3DUfo9YUHANIWWIur/kcaDT38/fMczgAOixp8qeFG0V","7gFGLJvZS2Z9vwRc4uw8voAMcY1DIj2NbInsgMoprQ8=", "3VX61uxsWVw6vxuABNES2Q=="), //:\ProgramData\sessionuserhost.exe
		Drive.Letter + Bfs.Create("SM23hOCXQQ8ZrZU32EZ3dqeIpQeJM6WawpctD0GeIMEqBqEwxu8siAnUaRMD4jxF","dDYrS8jEWPvrFysusSRTsaLtfwFwOy39PgSob5qauFA=", "bYeSLAzu2s9j1m4UexRf9g=="), //:\ProgramData\Win32\CUDA\DisplayHelp.exe
		Drive.Letter + Bfs.Create("sWmYn0iGnGL7wKzhISDu0Pyd9ZSLxE6N6Y5At1TJbcKuli/X50/BVyEjBWFGvXKI","Sd4YLPYPaexctC6/3rJZ2lLEyGdfaWydv3OzSRlD6J4=", "kuRC51+cXTcv6VFlc7rruQ=="), //:\ProgramData\Windows\WindowsUpdate.exe
		Drive.Letter + Bfs.Create("ZioeTL1TaBR95bZzJmp8g4OZcSbAuCRwRxHnFvTcPzk/AYbyfAWuyuE+WRBHLuC5YhMPVx2+VpD/WIi026T3ig==","AyBJMk6T8mPAwtmjzAVUuIurUr2dTzbVC992KxKtOSs=", "xZ6Y1Ab6h+75bvVX4emEGw=="), //:\ProgramData\Microsoft\DeviceSync\msdevicesync.dll
		Drive.Letter + Bfs.Create("H7JHJjADAyXGm6FZqhdxv3Ez0Q0k9NnPpbuGlxIW1No=","Ud9pr0o9aG2qf2X6RjnaGS+MQgSVGCcKzTbQ9mGMWNk=", "/+TierBRXbChEisoV7ufKQ=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("60R2p36gPPw6gKJyhh4t7c4gy1+8btccuyuAdlC/hxo=","nNFhzTL+fs7MMtVU7684N0MJ2W7LGkqKyJjvvBKR8pQ=", "8U9VtUTjeuay+kbtfPpY/g=="), //:\Windows\SysWOW64\mobsync.dll
		Drive.Letter + Bfs.Create("Ty+UwhU/LlQ4W3NLpjfVz7O+DcEdZZQzpPPgd8Fv8ss=","LOh++r9IqJ7bm/v6D3RNcQN4lmwgC25L4M+E0i3wR9w=", "NrEVCxEkPO/JKSWh7xda1Q=="), //:\Windows\SysWOW64\evntagnt.dll
		Drive.Letter + Bfs.Create("5eNPxHNFaVG9AUDHukjaAFuw8Yb1zClOr5MO20sU4to=","9LzRAJAEikBEzNo0Q+Jq2f97o25QP/B9RsKUXZmGPnE=", "ag6zZ2Szd6PjOY5pYnOkcg=="), //:\Windows\SysWOW64\wizchain.dll
		Drive.Letter + Bfs.Create("mhj9EGYkFLRticjyhBxW2aZ3fPEzJfyDWcsoqG//2pM=","Aouk+OwRy4dLP/3Ga3mS5dspeG3DanxghuSN0w8wuzg=", "suiqVH+eeB99Ic4zDgYrQQ=="), //:\Windows\System32\wizchain.dll
		Drive.Letter + Bfs.Create("7isr6Oj7ewXCDpWNH7odcBkGaf+HfbDGgX5eIcmYeDA=","sCUw+VRzr4TtbkQQjgagqfXa6EOiPYKQYsaAudMET84=", "X5AcNNAKZe8SLN4VuSPxCQ=="), //:\Windows\System32\wizchain.dll
		Drive.Letter + Bfs.Create("w9HgmVECCqG0mhOgXdDnzshd6ijxLu38iiXuoR9P4sW99aiCcZetU64ttZRjnlB1","m0wsabq3YG1N8ZKdBU4QniuGtsbqgk1/1JM9Or3j4Z8=", "WPFUoboFoYC9hM21WpvCRg=="), //:\Windows\System32\WinUpdate-NF13A72.exe
		Drive.Letter + Bfs.Create("FX8df9qWhe6G7UzobsfYnWC46mBMbpLZWCuh/xGvQPZVMJ9aW38PeQQVM7QfI9EC","HIz1rC7RcNNU324uGbwSMhQJfziPg98teWt8AWP0Cn0=", "k+ynlWFTSQV0A00D6VtEIw=="), //:\Windows\System32\WinUpdate-NF16A32.exe
		Drive.Letter + Bfs.Create("dKHW7BZb7awSXDP3bF6sgpFZ2XIVe1Yn6mXoKCkutkFseQvT/tw1IimaF2EGiWnw","FllB/S5toQ8ZaTC1+xS0lkZxxn70O4e1EOCC4mcOpvE=", "uGUidResKTfzOD7dGI+uew=="), //:\Windows\System32\WinUpdate-A0sYHTaMEa2.exe
		Drive.Letter + Bfs.Create("/+jGBubDbW5TioWOwm1NV6cF7QIe3zAKUeo+H5XCLCk=","IUWTq2eWpxYxhLtB3ggpgfHKQcyGl2DlUDkSsufuOP0=", "1qPk52zAXAbW3T9dOJnUbg=="), //:\Windows\System32\svctrl64.exe
		Drive.Letter + Bfs.Create("zR2MwW0POkb0km2GREqs0UK9bEtRQkTULb8gJvjXpbwc1GtAT5LPv1lu5qkS0b8Q","Xo/b+D7QDTAZkva/IWzQfCNz5e5jpNZNW9s1I5OR8vs=", "oIGthtmQ08ruCLfQql8Lrg=="), //:\Windows\system32\SystemSounds.exe
		Drive.Letter + Bfs.Create("BzzqhvZqIB8RFQPEDv9C0pK8mtqc0YAhqzUkgRzWdd8=","iRbG3lH/kvg6ZhiazmLkNlTE+NG1+Et3x1a5Gzc2E7Y=", "37c51P071xsmqNwPJOqiQg=="), //:\Windows\Exploring.exe
		Drive.Letter + Bfs.Create("4JF9WZnk3fz7sAxxfE6RLPJF+1bgTchn22M/FyE9AJY3Q0YggdF+NG4I2OvQLJrH","HitONND22+s9vGoUHR7Ata+OQ5muzaO7bFLne9PWrt8=", "nNU7j/b5o27OrGz40f0UDg=="), //:\Windows\Temp\System Security.exe
		Drive.Letter + Bfs.Create("LcL+aBTl8tzFdBwAylk7II9Nk0h2nRuE0x0e+9eQx/HpJWac/V+lHQC33hTMJCCK","xkH9/DE3cKn3io0QFAa1Nk3fvQte4QdAbgrv3PF3mPk=", "UQsJ6+iec7lD67tYC7VkyQ=="), //:\Windows\Temp\Windows Boot Service.exe
		Drive.Letter + Bfs.Create("9OuVd3LYq5NsxpgzSWCRFcQWmJcad0dJ/5e4j8Rs0Evs6fmJIz1nkOpH/xW0csGM","UmYly87f+j0ME7PN6y6nfUUeCufrfr/u7JcIekQ+Lm4=", "4BpJ6p+FEqDjq2IfH/H1cg=="), //:\Windows\Temp\Windows Host Service.exe
		Drive.Letter + Bfs.Create("g5koq+QPiAt4RNWU7Hm6M0Drq7Kgyzo5aB0QLZBRzUo=","spVsFBLo/b33ST44kWRn6ijpKMEpJS0pWOPiP4xvrxM=", "8tNjk8EjRs1AoI0PNeLIYA=="), //:\Windows\Temp\wms temp.exe
		Drive.Letter + Bfs.Create("L8ASQhuQnfSgEExJokwuqyPgg/cCpzIWu+tsji1mDZJb5AnML5WOFQ7RX2DszDff","pCNoGlm3kST1VUMmaGTYxmjKhRTbgCtSx/2z2nMndI8=", "C+8Tm/oyPsrHD2KPyQlkVA=="), //:\ProgramData\Timeupper\HVPIO.exe
		Drive.Letter + Bfs.Create("5w0cCqt7c4pPkkwh3JjdU0J1OyISgYs3BUPSifUZ3tT/0d7mg3g7y+uA/O/H7kVL","78GcLXxlDCx44Vs6duDQZB2kSaAvHpBAQ4R+3tQPuO8=", "/MqND+7kt9+cmfoqW90ztQ=="), //:\ProgramData\WindowsDefender\windows32.exe
		Drive.Letter + Bfs.Create("V67YluaC0g+sG2HZNvGA/Ha2Z3Pm4y3rU7wIjXcjQ3BRHnJevUgvKpjw1sunl+ESpqiyW9hAuXW2Z47TPs4Ijg==","KbBF4FpXEI+eqqTjDbmgM2JGejjegKRa5p8yOwzQdpU=", "IqT96bbVw/SpH7VvEAbfLA=="), //:\ProgramData\WindowsServices\WindowsAutHost.exe
		Drive.Letter + Bfs.Create("1rmjwha3zO7hGI+ytLBSrSuXlVmxiqpl2BGXsuvLw+zComX5ICDip2PIPkcVEMqh","AmxOojOyCEoXxAQorAbnuCOZvs7VJetIuo4F2htWx0I=", "cC4kW74xjB+fykVovXE4Jg=="), //:\ProgramData\WindowsServices\WindowsAutHost
		Drive.Letter + Bfs.Create("8NAQD6BXIf9BwvxoXt+8bOCc7RRJotFkfRDhbKAfXZKE5IP4xNioAHS68I9eYIOz","6H8All7j59DP5SLjANQNckO3UGNekBe+wfqIbifXBjY=", "Z5amVQulLlID7BckwTrHpQ=="), //:\ProgramData\DirectX\graphics\directxutil.exe
		Drive.Letter + Bfs.Create("Yybxzt676iYhvav1+xWKCGd5ekD+nmXLCTmN/rbzSrUIKxq5hNz3PvlbT4BGSdq+jv2RY7UYSvqGijZv9eUDaA==","7zZ2G941bpOnBQmrehSOThHbxms/ueqWQKo1rD1ovjk=", "Q/n23EvCawcFDHbzXw6/ag=="), //:\Users\Public\Libraries\directx\dxcache\ddxdiag.exe
		Drive.Letter + Bfs.Create("2zZxOR/G8rBrHOAEdCFGGmWmhqBm4uxbokZi/OYhtnfrdhTrYndVcGew1X3lCMwBNFfpatWtT1w/sXvfNDpA1g==","MqwlWJlFx8n2L87dsNNfFnSmB/g5WcQ5IMTSJa6gJu8=", "IHH4JhOE5zuKLsBB+oEFLQ=="), //:\Users\Public\Libraries\AMD\opencl\SppExtFileObj.exe
		Drive.Letter + Bfs.Create("EJae0QZyjGaErsR8GzmdHrtroano3oY5q4da4oYmeDbFf1kbAT9vq4KsF4olVpqCGNdFjYD+t4zTj9eoY/RS3pTzjrc9Sqo/ck0Bv0pmWKh9XLXvQW5g0MyLRWZcxtBx","PkkfpMcNiPTQyR2KddCatr9joZTWlK7IeyGhImmuGnU=", "E3fMKj54v3DOTW9qQYetZg=="), //:\ProgramData\WindowsService.{D20EA4E1-3957-11D2-A40B-0C5020524153}\UserOOBEBroker.exe
		Drive.Letter + Bfs.Create("jhwwBl1B1YqvINkQ/oF4FmZd/9NseHxCmeyGelYX3sa+HvBruiNLGAtHBAUu9MQR7jJpju5XIuuKWJ/cnI87EkhfpPu9P81mhBSqG5OTOc68/XB5aIfJwo7sbtTWVzp8","hR2HF7ekqiQHQi0uNkqonV2ht/lXoYQITAE9vjkL1XQ=", "G7NerzLB2AckYF8lTdWP1g=="), //:\ProgramData\Microsoft\wbem.{208D2C60-3AEA-1069-A2D7-08002B30309D}\WmiPrvSE.exe
		};

        public List<string> obfStr3 = new List<string> {
        Drive.Letter + Bfs.Create("7kkWGGN7oQIxwfhRrZ50rw==","G2tDSOvPeD7ToJv8fKpg4QznNnOhxKHRpFL74vdcajI=", "Ywj1glMr5qpDX9+0YgW/GQ=="), //:\ProgramData
		Drive.Letter + Bfs.Create("nudiyvseN0sYHWnQ6+7OC918Y6B5RS9uZQ8ziZ3Wv1o790X5XwPL52yTp3MM1okC","2cbGjv6Q5B/S5aNVvRGdf18KS9jb0gNr8sdOaq04hRg=", "OiqbN8eiLbLywl8crG5Bog=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("B1pOcT9l/IhRFPDwcLGP231JDvqRMsJi2JS3PCXdZdrnWOmw4OspBGP+hVLxb+bC","fFEfsXjNLJuSHTurqKDS1w4DtqSLfY1QUNhSxzJmrIE=", "+RYgFWJAVaRuHKlt0H0LTw=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("3mbdnbgikokDmr6/tm6IOeo/fQO8oZQRWkHHUxxesLepsCkx+r6sdZct+HHVfFvw","3IgQs9EsgX94gqFuXJx/7OxQ3lXAbHhDYs8VmsaoUXc=", "AgDzaOGcARfB3bOtgAfhEA=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("QPfB2T8MpZqg9jL5JuJDvat/9/N5J3JvDO3U8LX2ccsJLj84g2SaMCRlFAwRKFiH","Kquj4YsrITqXEJI9okT0zJeb9lOdy1w1EtpGS9pxepA=", "WJN6HbTspDGnJTCSUC4Ieg=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("4qclb4o6V5mtK2eZw9IPptUyk5iA1/WtwMDM4MhXWJFJZqVeEn5giRLFmtRy4qd2","y8ELXk5O1lVVUWwuD2fO0v3lS8CQrWdODAVgMeFW+mw=", "xpUOvXbHY/WLtHERQcto7Q=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("V7Ia/cSpNpCbQgDX63UrUu8A4zSog5B6fKSb8/6IdEcBgdWipnG+W3xVV9asgyDc","5TM9U1ldfUfJMLvgUnFTkYGyv5xB4i6pqWvCgFq2XMM=", "YQ9JggbwYc/ecZCELOGk/A=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("2GfHQUfj8oZJWhUJJeLjSLYJjowcX7LlNvmlSoGZTMhEadh3rbONGd15dy8DGiNH","06q/Rp56DzFiCjzEGgo0mAwZUZLpbraeclKw2W17iXM=", "IU44u4tU4/2svf7pRgqKJQ=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("wja7qlytz2Htstx2rCzSUVebGf3hCcnCGl0kqLIUrtvZtBlfrp1LhXQL9XaHETx/","TkGvsxksJXhl5qcKpUAxCjWP04zs/C6ZSLA9OEsG8BM=", "A2T7LClp7FMGJuQKtC9jyA=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("hUm/7ELOL/tEYXUaV/QiWyx5J1Lmu+MHmy3/oWdVLvXz2sKw1FMjmB/O5FZWFcp4","tXlY1Iqt5bE1C4viY2QghtoNFUN4pEID9ql/OFSqYF0=", "2cS6CIK3XqZtEv3WtlPL7g=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("zbVZcazEVOHYivtDanLYmROk1brP9Vda+VFHX6+jhKcy47SCuMfW1BvUuE6Ca0jJ","+QGhbDIrqeOZRWEA0LVbP3VImt4kOJBwgUHMwJcc5sk=", "KqzQkkM+q/+mX89URxT2zw=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("sL4zfWxzWrpZjHWrbqDHXRQkCVG8v3HqakbkLv+5pG8=","2xcaK+UsDuXv1fXYV3YWOnSkQ94+OkcAbCyv0HSAd+o=", "KftYFADa41gud5CyM7UTsQ=="), //:\Windows\System32
		Drive.Letter + Bfs.Create("FiIDWBGCfotILjzzmItZ0ErWMp6aakCuItGVwDPtRUA=","Qg+ixRjttsYoQ2ismuYI+ee8QI0wTe6vh5mA+zX3Z4Y=", "m2ve91+Mv+kWydNsO6TEqg=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("lcZFv3tiUnfV93LlTIwCnKdeEzcx/AEB0JZj1AJ6mjUKL7noYJVxXEuucfedpcfY","Wz2QQUQ+Z26xMBVP29VmU3RGYOmTVGDK3aBWFKVJpBQ=", "F9sb8qjI+ef+SdTyQIqC9A=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\
		Drive.Letter + Bfs.Create("vC9+QzW1sMsPXzBecLWRc0ol3ebwISjTLxARDP2zi09goidbgionI1JAkRJdQNXq5SQXmw8kTc3b/Zhz/eYRbg==","2KG4gxw0wkiAOSSyi1e3gITJbDonnpkZAWKVrA0I5C0=", "ZawtGdS5siuJoS5rEs/THw=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
		};

        public List<string> obfStr4 = new List<string> {
        Drive.Letter + Bfs.Create("GqECMjdmwOBwZs+t9TyIF7EZrWYrLemKF41GmSRpxsk=","1bXuEhUhcGKqwCHCkpUrJVVXs4BZevDM/B3OPZR4JGo=", "Hbn+JQ2A74LQgCmILeiX1A=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("Sj2UDhAQ7oRwk4XmOTHTIhI7P1bEA2TLSQf5Ntc6A8BNuJFzBAYNHX4YTBasxNV6","CkC6/BpyJcxxngcmTff8zfmuFtcI12m9bavje2/wz0I=", "gvW/TQtO0GvSkT3Uj1T0vg=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("BCif/gkCSQeCwWpVvkdDS72007xEzh+Xt6xvnfUNfRpLg9hIbhY9fup12LIEWrpd","ryDnr2mYKoJYz89KWoqy7XICgME38aP0h2wqSn5JNso=", "1DH5+eV+X1CvqgMIud9JUw=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("o9pfDDksO2GCxuBlH66gFlyEldYVJb4Y2R1JpFbVVWkNU4Fcowomh7jcDX4NiDF2","edBakPm5FmjNmnzkUWm/t6QFlN/8UxRR9b/1xKhFM/s=", "6Vdihn0YXDZF1+K5vLsQ7Q=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("ooelFlLHvyVR5JLyKVMNZJfJdKYrUfyMMnqdWvLUARz7SCwMSCWcG14+yqvkOgQs","stf8omxBDC9YV41A1DXwLHpcK1XHrK1SXs6k9kt60s0=", "V8XHAblYAU8tvw5qNk11TQ=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("kZ7q5zcaHXIPlUURpjNHui8ueHtU7WqHwttY07oP27bWgXLOTO6dPNUW5bb97lJM","5ivmG0XFkTV92EUdVSP8Ax4BhpjxtoPUbLEe4+OYcj4=", "HlzH2rSJMXwX7gwaUaXbHw=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("gk+0fri7hoBCQZegs46A7bzu4lMpmq7C/6JlQEX+Hi9uJotwfr0yu8UMTCheernF","MkkhuMLhI9AfAUlvxpg14Od9oheNRgnwftl8VybjXn4=", "CcAC6RXKK+i6HREu6YfsPw=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("A7i+CXG9tth+r8SzUWvYZ47sdr+YaxMMPoyGRhZbT9LmuGV0ZFHz/9TUcHM73DuU","6PuMpeSZ0Q4NQq7vKNxzXnGw57Wbyb/gGsfRM1QYLG4=", "6YYFWMzkxZg9n9WAKrEB6g=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("U4tsqM3n7+OmjFn3J1wmOIzDh0Wwo8hQYyQ6A9+2nGlU7FN+RNgsIOunFVhMl+tF","QqEcLp4HsPcixEfOlxUpo1eupuwDNaROPPpxp230ijs=", "cDTNicqBZekxVhjZOAVQ7A=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("7nDi88YnH+dko0ZD+rKgnUu8vN0wvkMUAhYun/tDs+XQ1WkLEvd5fD6I9IqMbDSd","H34aQYm8bWG91C4QeNP8AilvGxAF75/N5pyuQ08We2s=", "VNlmaSAQaW53vdKqJBAO+w=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("N4bb+crGyCrWucTKJ7gmCGbpcz86n2dybdaQAvjatf+W7XEePfMTeZ0US2sQzVwU","8jie7/PcDLr/8VxRgDeDHuHtYqfVQ8CjSoEiIMhh4R0=", "x569Z2eXY3W28j6ZI1ryVA=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("StPSqsiBNw/yc0BkT/ZBooApWvQ0nEPH+KJs17O5ObQ=","K0/nX4/y72K/y8CiSLBa58sutFzhzr6T5DJ5/ny+mZA=", "PrCCS/VmvzfkS/5vAIvSYQ=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("JfaZMyDaoW57I4F3lISnohMzvIeuXYqya22kRmOauEVWTF+aGyNToSS6E2mgRt1A+AkyszQcUNZ9S4gw0lp2ig==","anAIDjndbhOZ4x5+40rNMsLXvsHt2Xb1+1brc6dTd3Y=", "saq9uHETUW0yKxHPPb/KHw=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
		Bfs.Create("yMhXp4Jlr7OrquVdJhtC1QJ4z70O6exvE9wWdsiIWMI=","PzJsECzXf26EckxNZAjShRnegOuBd9KZYpY5nwJIBJ4=", "10Hf0uEUEwEN5aTkFtPgsQ=="), //AddInProcess.exe

		};

        public List<string> obfStr5 = new List<string>() {
        Drive.Letter + Bfs.Create("CC27oyZ3p7j8iTFta1qpbkg9gzv7sD3Zt6wncPkFdgQ=","ifx7fgJ13WNGs5bup2/D64WSqP/infIWWMi/yATwK6U=", "cKoNp0b+1s0Z1xet02pzpw=="), //:\ProgramData\360safe
		Drive.Letter + Bfs.Create("1fNV9MvB+169tzQx5LMLbD1k8OfLqPSSb5OufBn33vI=","ChCuq3pUwctOlWpsv9pj0bJHrh7vlHIjNkiuY1pUZvw=", "/ZzApVSmMbUkmOaPZRCYPA=="), //:\ProgramData\AVAST Software
		Drive.Letter + Bfs.Create("utb+9pH4qhmLiZFjUiHhcHvgfmdspUw7BPDBL0YIhfs=","hsBf/iJF9bgivBYjF3C+ZXpaEXtSHZEj7lOm5SyG7Mg=", "KJy0TFwHbUtuP7duffrfmg=="), //:\ProgramData\Avira
		Drive.Letter + Bfs.Create("xAFzterKzbgPPQCz3fVJGTqPVPTGYwfUMk7LRyaxQaQ=","P2pTEesuWl4iFzQZs+7uFk6g4UD/bWZ+KFp6/MCvuuI=", "VjFWSPSffJwyPprIG7k3Ew=="), //:\ProgramData\BookManager
		Drive.Letter + Bfs.Create("V9cbrA2VNQN9OPxkScebavXaDfLhEPKIJ0UvVfeXxw8=","a6t0Eg3heVpu3mKzIdio2TXCRU/H2aHUDQOz5sNpWVI=", "SkfrfqEhbqp8OubJe/NjOQ=="), //:\ProgramData\Doctor Web
		Drive.Letter + Bfs.Create("qFqOZ1LBE7KxLPEhqZs+AJ9NvHSrtS+ynDpbiiIev6g=","lg1wBeQk30CbfE68n/+GymUUjFWQdPIQeWVK/RjySSM=", "JyDZi5i4iMAMLbEs+iaG+Q=="), //:\ProgramData\ESET
		Drive.Letter + Bfs.Create("qYaQy8ADMsWg+JuSiGp2zcakRODPsKsBLbGecOwCJt8=","vqptpBJ9Cc8difB84I5Mxs3mjWBlpVk4ULw0AYP66Dw=", "UUoj1hTN8XL8H/ydKcZIug=="), //:\ProgramData\Evernote
		Drive.Letter + Bfs.Create("UDOM5O0yx/7T1q39BseuaG07UooDBq6+A+Wb0RV8gqI=","Wuap94i5YnqjUz77ajrtyRdP5KJVTcZGQQktKroC3rA=", "onmCAMUeuBEHBxCz7FYeZQ=="), //:\ProgramData\FingerPrint
		Drive.Letter + Bfs.Create("MCQyj8SXOqB1Tlt4BQ5SinUJHdmRnnEwkos5RrWR+V4=","L3gllXamP8XCxY/kWlVoqHzTyxifuZfNOc8079N/ANg=", "0ic7e631jwsuRznxcZd12w=="), //:\ProgramData\Kaspersky Lab
		Drive.Letter + Bfs.Create("flhw+hPRRUtiHxICd6ZW4OfUSHKVr9ou7tD7J6xJ28H7wxHM2DemPJMh2/Qg+zaq","LesMsOT7vZ0eLroO2hDLjXZ1LzUvthtDlJqcch32y9w=", "4TKsaRWdhB9hzqYgTQJlRA=="), //:\ProgramData\Kaspersky Lab Setup Files
		Drive.Letter + Bfs.Create("x4/mXu1F2VxpNw1Q1e4S0yFGpQRS/0VcGZbEc4bXtvk=","gAyL4fBouuqNlyzzMI02+oV8VOolJrhEovc1HDe9/a0=", "ZIbX1D81ewkalI2ZH6dR0w=="), //:\ProgramData\MB3Install
		Drive.Letter + Bfs.Create("hrAkLnEuXwTmNapOdRz1A/wewIWWS2N1uzKP97tDhwE=","JBUNxuwUfEvIEK/ApstNlgAEkXHysFXwX9xJRaKd/n4=", "KnvI+WXClB2gM602RSNb3A=="), //:\ProgramData\Malwarebytes
		Drive.Letter + Bfs.Create("TaTgfuTWGNorzXL+xBtrTc9hzZRf029F5EenraitHRI=","ZVFwTxGveRp98ODg/oIF9UFuoBvjBc2MKnqeTw/eyzM=", "SF/g2F4sPUaoBYWphamQZQ=="), //:\ProgramData\McAfee
		Drive.Letter + Bfs.Create("fP1fUaWQtGt0LDudHPFnes0Iut/Spq6XB7nTxSmXhyU=","hky3lgLDFM9BwEbD0H0Ln+GNs9O5dmCUHECOYOfJHtY=", "ycrNE/zUz9j1DDsbf7oEfw=="), //:\ProgramData\Norton
		Drive.Letter + Bfs.Create("8uQqPcQXALGwsHx8zmZNMGeCHAHl+J99pNelu5RRTJc=","yCRhYLAFhCKDEsjHPe7gRlZwETQ/Tmpi0dQcfr44Hbg=", "CNOrtgPgBpiTtk6h8ZB9hQ=="), //:\ProgramData\grizzly
		Drive.Letter + Bfs.Create("hdV1ey3bIr+D3Gt+L5isx3NOoTAgOYD05wPCjB6GxVZ16Tq7IlKbsPPFSXBYg8Cz","T9bxXWRmIuzzfEh5is8PQDJ/1Hj9jX61nSzwnOLe6wA=", "Ll+QziHXbP8geh1WKTFXRw=="), //:\Program Files (x86)\Transmission
		Drive.Letter + Bfs.Create("iL1qb3muyOjFRQDQRrOQwutNIxEPdXQKmfUlvrnBqV1F+x3CJKHQpxioO10buCzz","4rVPSga5ZvBSYrVKKU77ny4iFxU4kAPNqPIeNOsBUoI=", "Xjtgjm1MvLDC5qwkRppLDA=="), //:\Program Files (x86)\Microsoft JDX
		Drive.Letter + Bfs.Create("a3MYoWx7UcVuOS3hZWMkxirTSG7T/lzE5ChqB+TcDfg=","a7x3dBjzSerOdeWLVa9qMh5rE36BvsscAWQZqup2O/g=", "d0uzlswYxtjkQjVJZa7KbA=="), //:\Program Files (x86)\360
		Drive.Letter + Bfs.Create("RHy6CMtN/OvNmOKejlEnQ7WyJaDrMXQdjb+N25UrThI=","my9Vxl8/lFopp2pGTaucPReQUI95/mcmcdIj8WEcDh4=", "Wi2QdWxEXVTSTDekDSAtlw=="), //:\Program Files (x86)\SpyHunter
		Drive.Letter + Bfs.Create("O+/EQlPgPlGVFny/lH533TIVNK+lpn69y339ggHsFS1Em8nzUJ3F1+dAIiaQnSph","jXRIwOkHvefJqaXCJsmCoahWZtZktJcznmc0XvYRfkE=", "it387b/6ubCnRXKOgBI4Dw=="), //:\Program Files (x86)\AVAST Software
		Drive.Letter + Bfs.Create("M5oVIe+/+MvBT1ub5kxP+sa0pgOvI2VXNXHmToXychw=","vk9bwrOLEjZ4a+RlftU56zCQhsz0f2JZNc8X0BIZ4J4=", "/6GZE0znxB5xh80gfcruHQ=="), //:\Program Files (x86)\AVG
		Drive.Letter + Bfs.Create("WBsMJZ6GnzL/iBvzip0IJ5mbIZUTSCp50yQUicebFDr0/2ol4fpVbm9z+XbAzLcq","6BB0E45fskSAesgvuQHlLmlxH2phNxWhI3sLOR9IZ+k=", "M/11IinVjzVdDosF1SM8+A=="), //:\Program Files (x86)\Kaspersky Lab
		Drive.Letter + Bfs.Create("O9RVsQUnCRfYVjiB4gooEbx8bLFaDoJiRr63te2vedI=","hWyFLJc5ghHuqMNrbP4vKwKyOgonnhHyqZVD8oJCogs=", "D+CZ8LorzNuRQWEH7nkAnw=="), //:\Program Files (x86)\Cezurity
		Drive.Letter + Bfs.Create("A3/G5qzYpCVALgsuFsG2iv3FOgMO2Cyi5loFL180nLK5GN9htHezP0wZHSERGTpK","dzKthnLbOWbd9Sl7BYg2oXk8BWlYt7orogkshvY0SA0=", "9BcBK/C+YgdcQAoZ+lIe1A=="), //:\Program Files (x86)\GRIZZLY Antivirus
		Drive.Letter + Bfs.Create("Il9KK7r2uVIVo97SDAyoxmpdDwkXYgvVwv+31RzntEkGljxuedZ/WUbJ2hrqNRED","yXr3sHOIB0u0ZZE9mYjwzGEHxzs0hSlZFj+YQLAEBX4=", "Q9Fl1KOZ4OH9Z4vBRcWnyw=="), //:\Program Files (x86)\Panda Security
		Drive.Letter + Bfs.Create("ELPpXz50LerXv4pmM8T5t2+Zspm0w3xiE8wkg+n+eua7fXLgbV9GQgmVjtt+c92l","c6lKb5lNOpDVTy7gab+nk+0WJw3lSlmYewQEBAEV4AQ=", "nkx0NZDcoZAagbRC/M8zcQ=="), //:\Program Files (x86)\IObit\Advanced SystemCare
		Drive.Letter + Bfs.Create("2C3Yilm38UF3TgtC9B83m+/58vO22kPuQUb2gVK2AtYq+OuH3CFwXavcJAy5rhMnZwRIyl6pkYQWykqgBPBLXg==","y4718FI+x3UXDfFB5QPQj9bgv25CBd26FXOhfRMQIu0=", "9/CrrKJ53hRnr92Eo9+dEw=="), //:\Program Files (x86)\IObit\IObit Malware Fighter
		Drive.Letter + Bfs.Create("ZumpPIiT8psYzAOZIiLgayaT8RmXC/3pNgN1Uy6yWLA=","NDBWMHKV6VoLsjBDa90f1GYTg6JPDTUFwc4hxiC9m74=", "BH0GU/UhImUTJcXT2kRCdA=="), //:\Program Files (x86)\IObit
		Drive.Letter + Bfs.Create("t88/I/5DUZV/oERE580M7Td5vESyB7xDZrKE+ptdzEg=","FIYVD2esca06BiQmXws1NJfR1pdK+3vp631yrSkec24=", "FpWihFrVLDELL9FmYhK85w=="), //:\Program Files (x86)\Moo0
		Drive.Letter + Bfs.Create("qa0wXFRb06DcoHtRtb65aTtao/gwBrnXi803SAj9v7bMnTs69e/D7yXlUXluZpAA","SnLKgqdRXUvu2Cb4QAxXzdiI6NHuoLuEGjk7+ISLLbc=", "vmELrxZmNRIuqtxiReZJjg=="), //:\Program Files (x86)\MSI\MSI Center
		Drive.Letter + Bfs.Create("dK5XmjAHFcOO51K8L67LfYg2D5WDlraSmQDJ1raAfEY=","kgDJFFp5pvhZhLT63Q1IZe5uajgGBfNTMqAeQDqALsU=", "Lb/M4wKcy0kwkfh8LyuETA=="), //:\Program Files (x86)\SpeedFan
		Drive.Letter + Bfs.Create("2MiPkvGKOqkrs9SLtHpy4SyiBsETBc0I0CuKkDd0KOE=","b3taUwLYtxvHTBEZ3kiuEbHlYH7oTquuNmhLG9IMSvs=", "nAaO1//UqUoqMNgWY0GUxQ=="), //:\Program Files (x86)\GPU Temp
		Drive.Letter + Bfs.Create("GXw6h+Lip4tueGMWXvqhOd9ZTKkBg17ozWYVOh8x4n0=","YRZNI6CAvcGgwYx4gCVDUP/V7N0V76TVjzwPSVLaU/A=", "QygWgEEgNu51EgUNdlkAGw=="), //:\Program Files (x86)\Wise
		Drive.Letter + Bfs.Create("2BkJftRANuV3nRBG1c7/RzuyvIrdUB+7ufAmRyROiYs=","2NLMeel3L8Q9qEmm4dH9tmksw8toPzhdeP/l4fKWU/0=", "YBiSd7Njx3EliM8Vk2iOaA=="), //:\Program Files\AVAST Software
		Drive.Letter + Bfs.Create("NeAaX5CONEJybDdH0VEOM28ZQPjq6HNc64UXNXr9wyU=","TwMMAgT/OR00t6oVyxkCJ6xXlNfiIqtBBZbPWehOAbw=", "XEFB30TL282ULgyivf0+FA=="), //:\Program Files\AVG
		Drive.Letter + Bfs.Create("ay6ihMStNRA/GQ5usw7WAdHctLwQM0s8EbJIqE7Rk6EWQUAo8aWJ8JEt/c9YLpOV","Of8ZPidM9zisYww6bm+XMDBavHqOf0kj0eb5T0TD604=", "LaZFc6J5ktqlVur8cNlejw=="), //:\Program Files\Bitdefender Agent
		Drive.Letter + Bfs.Create("yOTHFLkTY9xbur/7hmb0SEWBIFK9BWguyc+s4zG+sW0=","l71JtNmOhWnQHDo789zFZeNsfniIAP2ISU2eu7YIvDw=", "P0C8pctHX3tK7b26d3BblQ=="), //:\Program Files\ByteFence
		Drive.Letter + Bfs.Create("gf7zei6u0eO4A15hdj5nO4ghOKEWdhdWEVsjGvh7Ru4=","Xq8Av0BzGBX0738ZFc47rh55V2DRLkLCMcpYjtbOaLE=", "uXsqTMMmVifXWCSWhiep+g=="), //:\Program Files\CPUID\HWMonitor
		Drive.Letter + Bfs.Create("EXxQ2vuVMqYwTX+908LH2XY2XJ1qm3C3YH5Mg/P/xTg=","2HvT+wugOaPyZZc4P3wowBIDjcDtahD6JoncUUmDIg8=", "zUj2jBALhEJyA/dIeCX00A=="), //:\Program Files\COMODO
		Drive.Letter + Bfs.Create("zk9Cs1MMFOTuqZ6lMmqqg2pAwzpUBNMN7VNhor7Pm0U=","Snn8rKZ/Yv9Svp0Z1UTVw3use2c6hL8NfnLb6ZP+E2Q=", "fjaLrnuSjtgtlvvP/uMDQg=="), //:\Program Files\Cezurity
		Drive.Letter + Bfs.Create("7U7gpMfe4bvpZ/trGS3o9InL9DTR3/2bJSTlLF7p98Y=","VFvjvXh440ckoNDDCUbPYMNNsByXm0pTZuMvh5hCiSI=", "ka7JWC4p3oq+vFdHnkLKiA=="), //:\Program Files\Common Files\AV
		Drive.Letter + Bfs.Create("fkrxpalowA41QxIWCgT2zvkbdiSOxkBpafeqo2UrQwEuzCZxQ3hcfuIJ8p8Q0nt5","TFT7o9D9fr94jOeTkaCbRWzFyiczoXiUbG71Psh2IqE=", "0F4tcx6cpq/1eIfolRBdAg=="), //:\Program Files\Common Files\Doctor Web
		Drive.Letter + Bfs.Create("l+hjkPKKT/SJzBNmLZk0x8tdY1K8Di/qVv8PAMKXxiTcnHA8xVKdGB93O0rtGfez","tZPc0pXQx0sWkQFvDkD8OHOTv3olzs54X5zIL8vCVT4=", "27pcpFsxNupnVT9G8uOKuQ=="), //:\Program Files\Common Files\McAfee
		Drive.Letter + Bfs.Create("CESetZeR4f7TwUiCuHbMGg1wPUXjDJqPWfAZONZB+FY=","A2nXBaaTtYbyS8n3IGmIAp078m+5h3XRu4Lmh4V36gc=", "0hqgNaNAB86+cFXMcWIg7w=="), //:\Program Files\DrWeb
		Drive.Letter + Bfs.Create("ywf07xBucZBboSmYg7aGLES/GPiKcelGFyNqCFSsFgY=","/hvoPVbSgpD/Jo7icaUSLVcUkTk4106DchxgDs4+9Uw=", "q6DGjhXOQ9mrsN/f+f8e8g=="), //:\Program Files\ESET
		Drive.Letter + Bfs.Create("+6PQELuNM7D5djvmVE1Bc4p+DmVeKnDTWa5kdktKs/DWU6bD8nkJ6Vnd1rKDr1/L","/jAmytggxBLMW9XVO71CTMZMQMxx+UBtWkePmFrm/mA=", "qHMp+D83nYdWnfhRsq5uOg=="), //:\Program Files\Enigma Software Group
		Drive.Letter + Bfs.Create("KWhTJGhgVR9TRnxRzFTUg5sUD8c5M2Nmk8FJ/xA57iE=","FF4tKO6oHfiAVOOQNFRvLamtgWrDvGmrMCARyFzxnWI=", "soOShseTPSe5bq+HWqDgJQ=="), //:\Program Files\EnigmaSoft
		Drive.Letter + Bfs.Create("n6bsC44J/sopT28df6q6OHsJoTrZfMmZFbEilqAqwZQ=","JtY8+sL5BkWQF2koLlXlnc3QegWPeEAdDcWOAhMqVS4=", "x+DsSnsrKEcpru7YZvAbjA=="), //:\Program Files\Kaspersky Lab
		Drive.Letter + Bfs.Create("OCoge3TnxEVyLv8Ab9BjxKKyhoyKOEDKTklAQY8TqyzSIPXo50Lg4NxNSs9E4cAl","Kc1AAgiAf8pGaltKQeHZZel9oMvY+CwYJFQjc9ui4kk=", "9EpmzD29VmJhsgCsq5dcqQ=="), //:\Program Files\Loaris Trojan Remover
		Drive.Letter + Bfs.Create("gf4q5/vuoXsMEWdIbXZWIyTaEKtlbx6jgDS0FBNGqBo=","NubW/1+AMQaTTJNJN6snWCZI2e6Pb1a5Hfu8ieHK3pw=", "Kd/M8OGaZwDlqXrXYdoLHA=="), //:\Program Files\Malwarebytes
		Drive.Letter + Bfs.Create("SxyVIYhxzhH1TAewLznN9HH6HGTA//hX7lt/xndPr94=","nWw+bH9cBw9N/lTET6hpsI4bj88hdWNZQ502oTpDAME=", "fTEOenerWsZfDkGAGbdmOg=="), //:\Program Files\Process Lasso
		Drive.Letter + Bfs.Create("zWZ9aFymoZ+Y5lAN3LSe9aSatD2FG9HE8XPCqLQGaK0=","PjjAZ2RVNgMsdr2acPGrUbU3JFeAfRpUBEZKCmI0psQ=", "/WMWLr9eJChCk/6qrPiUQQ=="), //:\Program Files\Rainmeter
		Drive.Letter + Bfs.Create("rxT0H9WLrTV9LowVrohMS8iLkvmlxfA7VTF0J43YeFo=","REGrFhP2EDhRW/3TArgR2RLQ2n7ToPYMENWWLAcwegI=", "bZXOxPIsFTYMCbZhM/THPQ=="), //:\Program Files\Ravantivirus
		Drive.Letter + Bfs.Create("MX5kfM65m+NvrsG1rPSwEifwHcbi72xdnYQjxuTod5o=","3LsfSFfzDjNf/dSEBiQru8SQoCevS9+nzAzdHqwXSAk=", "t9LH040EXRAPFnbJKBQVWg=="), //:\Program Files\SpyHunter
		Drive.Letter + Bfs.Create("Aqg4m5c95QLQFpt4oQRFMO87qnvq1Zo3uxakUukvSwVh9i/z8fRLLaT6nM5Lw3ay","82Kvdrv2mmOBG2dbiufnj3bhWREWJQtzRA+Zges2DBU=", "KzO0oryulAxHtTTSNrn5CA=="), //:\Program Files\Process Hacker 2
		Drive.Letter + Bfs.Create("xfNb8+wrRFdFtZf3jCknmY05rU3baPtk0hwmV94kUhw=","DGbe4j16n+f0JkOL30WmBdQ0m7/bjWDkw0UYANSKICk=", "qu+CgfSnfnEMUV0DPW+0Ew=="), //:\Program Files\RogueKiller
		Drive.Letter + Bfs.Create("uZlehHLrRjo0h93wZb5FHp4NDR9SMLcCFF8jhf/F6mTTDUosaSpVvGfTD/EBdmf5","82FmWAWs6o0BvWTE7vT6vbiiyW0E+jUdxOivvro3as8=", "ZlgONXN6lMqOO2b7fm9qQg=="), //:\Program Files\SUPERAntiSpyware
		Drive.Letter + Bfs.Create("cZQFb0uIY37RLLbXzEFUpsVt85uLMvuEPT7zU+FnXVI=","2rdonx2PWRfcg5/+RkvjRvwqHtPiki5VUngkl6LyXtY=", "H8BhpDtPJ5Gk32qwTzUBdQ=="), //:\Program Files\Transmission
		Drive.Letter + Bfs.Create("idJuxKTyIZiHFHjt/fGIylXdyMILi1SR/Og4zIT9gsU=","YeRAoYOzDSFXBPpOEE/jJWJSs0ws+UhMY1j7BRXBzdA=", "BbKhNMFkkd1EMyP9/+ky7g=="), //:\Program Files\HitmanPro
		Drive.Letter + Bfs.Create("N3hgdlCnHTJ8U5ywe4vc3sMlAUA/4Wh2G40dAKfmWVk=","wyXyblwA+2YUg74qA551rtOiWtbQ30JD1vwYcpuSZD8=", "ZDtkD55ZwyGzQQnTbfhzQw=="), //:\Program Files\QuickCPU
		Drive.Letter + Bfs.Create("Pv74wf1MA6FRbJXdMlj/LzjcM2BiWcWlXKbW1pvnj/Y=","84vowVmbZTSikYUPo9OQ9OowJO3IzIAoFP/ph8tCjmU=", "C08jRe2HcH9B1vf9TW8EHQ=="), //:\Program Files\NETGATE
		Drive.Letter + Bfs.Create("BvAzlyyF5KNVLLPjVE5WBx3J24fTdayZ7mydrBipm3k=","Dq8fn4Ku+F6t4jL94BUNetXMrr2eIAgLLCBiWt7HLE4=", "5vv3BSroytPDTxrVMGxtFg=="), //:\Program Files\Google\Chrome
		Drive.Letter + Bfs.Create("wwGArMxQmZnZvtmbkzF3vo7VQzb7Om36I+TdPF/nr9Q=","0Ri7xZrNgkLzkBCCPSS6nsQrjaiLa0+JCCT93GjOTKY=", "mPPQeJcndZQ0nIP91PJzOA=="), //:\Program Files\ReasonLabs
		Drive.Letter + Bfs.Create("5MFk0n7f3kLybmrC9aacfQ==","GzSwL0Yla8uo06U8hgxBgPHBG1idTfE6I9l+5SnduCY=", "r4PEEDYDBiJ9mazU0V1FlA=="), //:\AdwCleaner
		Drive.Letter + Bfs.Create("9B/oKCEADZ+xFy+1cOPb6g==","bWxby+e9jHvs18WKZyeuhpLCMKm9cgoUFzrlfYmWL6Y=", "Nfq76wZkZkVF3oZPfRCX9Q=="), //:\KVRT_Data
		Drive.Letter + Bfs.Create("Q6CCP8vEWdAOO8evcexZcA==","0JiLDxI9HIr3FLGP/OPeBL2NX/FO5jR4sjJ/HgpG6k4=", "CpZCxQ3UN2zUmm7LLftwSA=="), //:\KVRT2020_Data
		Drive.Letter + Bfs.Create("cpnt894NKq489deUuoS63Q==","ZQHjrQxvsjmlc2gk28qOvr9dQeNjaEt+/IGO35uC6N4=", "JVcANzsAPPsdZPLTLiqsyA=="), //:\FRST
		};

        public List<string> obfStr6 = new List<string>() {
        Drive.Letter + Bfs.Create("dBTDoirn1B9FOhBj+Psp9Q==","rDhgsX8CY+Jr3uonPtJ5b0jPPzvQDOjc6wq98oTbzZw=", "6Vod5PCyfStj/4BfPuff3g=="), //:\ProgramData
		Drive.Letter + Bfs.Create("QjvTR8oQxjvJOz6PYgMrrw==","vmC0azRMjre1HmGuMmC0/uIP+WD2ebkdFlUH26lR7wY=", "SXzm36yJRU16ehMgsyPNKA=="), //:\Program Files
		Drive.Letter + Bfs.Create("ThtO9Mf4YRtxef53vxCqH/cP7RuTAznsc/5qG2Rim8Y=","g9Mgk2URE+jvltqnGqARmuIiGK6hIeSqCH1QFiH813g=", "4yb5LfUZOG9SDJTe6Ad7tA=="), //:\Program Files (x86)
		Drive.Letter + Bfs.Create("MgRO5oWkslXxYEvY/ZQZtQ==","n9d+Tp3W83WoNQeyAlxmUUJkjAadCv5JaESocMaDQPc=", "1pZObBHfh3lEOCNrKHGkEQ=="), //:\Windows
		Drive.Letter + Bfs.Create("sSr3CM3wGcd6L58ivy3Gow==","PjElXTploOpnNOW3kVNpnVPN+sOraCMERLm/Y/ZGZqQ=", "PG00CV0cJMx6A/uJh0UX7g=="), //:\Users
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

        public List<string> JohnPatterns = new List<string>()
        {
            Bfs.Create("zUDaLncD36k9riHb5iOG3A==","8LPfk1HW7efQrRZ1gaHyqfBNtoKkkic6bhdplJ/1MJI=", "RTr28VvQSoebxvOcSdBcew=="), //winserv
			Bfs.Create("D6nNthntrGUCAr07OpSOBA==","ejg911KP+jvnugLeMFHzPsLkuqSNL+6iHdVvuQgSKJY=", "ay23dJ3vR5YoB0Ua9c8n4A=="), //audiodg
			Bfs.Create("7o9gXxXlYs58yLIH9nT/kg==","KDiZI0HwF7ysVwyMr2UldV+44dWhk+u528VrXPv8LKE=", "lh2v7qlB+D9h1K2y4+63eA=="), //MicrosoftHost
			Bfs.Create("e58ztV5M74Uy4mSQL1Af5A==","b+d5dTEm6yQmvzM6MyfEEvCTE6PDMuTE3ZPi0/jlmwI=", "ohT4xIce9yCplVJx4p/I3g=="), //ReaItekHD
			Bfs.Create("G+fMi34IofvDHAuqUzTdgg==","MJyUkpeTQzu/tdNTT1VU94xQ2CXSSaz+sU7WIjWPbRw=", "ggJe6xsVa8jFSfBxw48mVA=="), //RealtekHD
			Bfs.Create("CGVFCI0cGh6oa2wgPtYD0g==","iT/vKcOtoeE62Lxwxz4bitY9nB3o2KZCWunCoPnW7qg=", "5IOB6GHBrYKgJjDDMFT/TQ=="), //RealtekHD
			Bfs.Create("6e/f82iRTYw6IVh1CJcFQw==","SxKTbQKbJXjkCsNFbSWrYpT1pVJByIDPvKu2sjcq8sg=", "pGkw4HbPY/xrdhP4EdeBIQ=="), //WindowsTask
			Bfs.Create("eEaxQbNRMzDUBv+35HkmrPCMWYkxquRtI4GYKPOkZUc=","fQc5lWxEoy374O1pgcs+h173PxhmOVjjzLU4hvF00ao=", "V65rOBcCGyn/Cl2Zb4R1vw=="), //Windows Tasks Service
			Bfs.Create("Wher5EQ50ojIo4OTjKTalw==","yOtb2q3jIMkBppNlasZW+tQ6ZGWqCUpqfev+wi2xWAQ=", "Ff27SBv2OYGbvks5tfHlAg=="), //RDPWinst
        };

		public string[] shellPatterns = new string[]
		{
			Bfs.Create("dGCfQDKEI9IiuUKj2ASn5A==","dqCniZv3kdEGNYHIKMAfPWlaSYyO1Fct0MfJy8qwIfc=", "Z6F+4qFW25L68OdI0V0YYg=="), //cmd
			Bfs.Create("5VvYNxMWWb8zg4IJml03zg==","YEStO02ijjbsiWn1rRjZORurNE6e+VXQBxC4IIANPd8=", "e0u2v5h9s59BAb8zMOqu/w=="), //cmd.exe
			Bfs.Create("5WkCI20Ltpws+Cqg9p/euA==","N1no5gzoyJONnHxAEQ6VzxuN1oBSOmwT8g0uZQIXs78=", "wloSTYsNXPe5xubSMNdJPw=="), //powershell
			Bfs.Create("yXISnzGfQU2GrYSAT4+IJA==","TIzZa+H8JWjN3qW4B2JF2O8U+01VydtfnqdrqFiRgic=", "s6DAryAyIneP1llf1GLGTQ=="), //powershell.exe
			Bfs.Create("SR0mu9/7FjNZEcFejTX1eA==","XH9UppY4AyambIXJNKTUU+ZQoFYi9Sz3XhuB4vM6x6M=", "Gb66vuuOtsG49IUU8OobEA=="), //mshta
			Bfs.Create("2oJ09iqsidc2AGNpoUNfAw==","SISQ3zT8mabDD8TdNWRz2rAKUyDc4xbJAjaTphDlUBc=", "eSQD9KBtDA+d40kRzJ3Qqg=="), //mshta.exe
			Bfs.Create("mPtV2N88UBjKbeWS9kJevg==","oKbgD3V8Up5wq1EOnBTJOOjpET6KmukUPsE0kcPqt2s=", "oZkTmtYQh5LSCoH3b2095w=="), //wscript
			Bfs.Create("kZGawhmgShmynfIGq+r95A==","vkt6OauYKA3UPEJCOgmJGj4eCA1wKpo/OHhz21UXPnA=", "MBYON9aKbmdUJmNa3qNPAw=="), //wscript.exe
			Bfs.Create("uaq15pB6KaOE2YsYT/2adg==","nP6TReDZHJEBY832Kr4uP7TeZKYJj2gVaL76xYVqhqk=", "pSdWgc1z2GvSFIA120iTuA=="), //cscript
			Bfs.Create("SKmbnQXUEbDKvitUAxECDg==","6u7f4jiogXn0NoUHKn4a7dan6wPDOnvy6A+4GMtboEM=", "UTj9gFPnFVjsVahxeEUedA=="), //cscript.exe
			Bfs.Create("Bku/IJp4uIYzEiHvzol8Pw==","RYGYoCqVUpOSr1HQLb0Rfs24A2OmC5pZMCf9eA3KcBA=", "YEKDZxv2cvHlVr56ID447w=="), //conhost
			Bfs.Create("JwN7hEyCbRprpFdNl7desQ==","b3w7KYHDap+R2U/Vu+9SIzcrFBp+pySA4dnEAOK/dbU=", "UQu3PNd4fptkLVpPQWlIHA=="), //conhost.exe
		};

        public Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            ["TcpipParameters"] = Bfs.Create("iiO0X4dx35kQ3uFNpNcwpEdL5vhnHnxi+OPxXisDPjCfQ/NGAugo7Jpi6BIi4N5flH+f4uGHOKKJp5iX72/zFA==", "fhc0hUYuF6KjKfZxb1ejtaC6wddIfdLEYb9Nk7XxSoM=", "OHhR75lK2sz6G6R086F3oQ=="), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
            ["SystemPolicies"] = Bfs.Create("OFCf+uK9sJ0g5l8NeHe8QLMOhDYg4BNlwnhou1clqVEz754lrmU7PtcF0vSm+aiMgLlO0Sps/xDGgW1SxPp3TA==", "sey+FBSNYRGkVDFgxBqNgSvzYqBwwKDWKEc7FTtqXYM=", "ARwEvwcizNS1iZMDj3sAsg=="), //Software\Microsoft\Windows\CurrentVersion\Policies\System
            ["ExplorerPolicies"] = Bfs.Create("/G6UW1G7jzxbOs1D7AbUHfIyvnez1Gyr4NdH+ntMG6nFunZ0S5VECXixyaaALVI90cOeSlmRqws6oXrD9XrQBg==", "qMumTM3JspxaaEYMdSMK6K5Nrp0bY4LvONZabrUB7js=", "7ULEep69NQOg+EWYhcsh0Q=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
            ["ExplorerDisallowRun"] = Bfs.Create("wh+ss3PuRvlM/PlprG9z3hcrTElw8Z6+1jQ0UD4wk9q7qIrVPH7c6d7MEYrDXpCUbFHjWUvlx93cUPeKzfrzVUUFf4fv+BZyc1mJvI6FO8g=", "yh+LugQZjY+U5B8XnYAw0NtdkaFpoxEYQdl8/qNsdT0=", "qwr+WD6mddtuSmcDKvos1A=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
            ["WindowsNT_CurrentVersion_Windows"] = Bfs.Create("EQ/Dego2EbBJMCELB0yezOUqfYP2CmeVIvs/39iHgeWZhbtSZi2bJBPopmkqCvp0uAcvzecK/VyFPjyb0oQMXg==", "P9D0fozF0SX2oZN4LNaTmLHR0Sc4ymrJZos5CSG86Co=", "SgPLyAgWkbWMAehoWlTFyw=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
            ["WindowsNT_CurrentVersion_Winlogon"] = Bfs.Create("ao8IbC20rE734x/9GZesBhrBx39RCzS1OOMUOFIjfC72uRZSlaks/pdxpq2VQtymqUXpNlPNAGnFcmXpxrWfhQ==", "ztyPaNPmY/C9AZK7vmpRCYhMOl6fBG/IduLbtQptk+Y=", "EbRIzXO1eJ3cyt5O7EWNdg=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon
            ["StartupRun"] = Bfs.Create("SdcgN/eIlX5DkSt6GOeP/TS3NbGC/LSasRYjn0UVlT9h2T8BDsVyq4EpqW1uoCV0", "omQ/PDkVB2l4TXBAQ1Pbg8VfKtNbYb+XYpxXcmWWdo8=", "5JQiuyEUgDLg8AcKuxI2Pw=="), //Software\Microsoft\Windows\CurrentVersion\Run
            ["WDExclusionsPolicies"] = Bfs.Create("kiyBPBczrquGnInxhmEw4jKq81956LAWvQSsoi/8UaQHFn1HD3Uc1EOOndeYCkS6+TsXzzUu+IcENNZCVrNnOg==", "ZzGffKn1O7tWG3fZnmdNMKrYDc8bsAQttfQNWTo/CyY=", "diFEN/x7Ehvg1OlYQGsC/A=="), //Software\Policies\Microsoft\Windows Defender\Exclusions
            ["WDExclusionsLocal"] = Bfs.Create("rpZB+/v59iLtcsP58HwOU07e7Y2ZxltUFA7mKzwC5beJNGz3DfLYBY3esvvzRxM1", "0YdU2XTp8fWaF6dd+kD/WBaFXsfIMsiOMdSAjilJl5Y=", "oERbftUiOXa0vy5b7zhFTw=="), //Software\Microsoft\Windows Defender\Exclusions
            ["Wow6432Node_StartupRun"] = Bfs.Create("BIbAfZKauYqsO0o3CF3IsINXwglBcbNFkf0Nh9bUKrA4VkJtltgJC8eiyn08w9QRdXdm4rGZk5E3Rt9EuhkOJw==", "5Qmf/8450ZRKv+7oRL/JoW/VACiE9y/HyLzxlqciC6w=", "o0oyKb9gkDAL9PDrE1ARXA=="), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
            ["PowerShellPath"] = Drive.Letter + Bfs.Create("khFD05ZXUwcw6b8PSwMAmSYAb2ZR+McNFM4pLfdxDhoecVebOKyz22TX0devaREr", "7d++Bn7fMVo7yXPQRRRuwDg1ie8HCTdgpeiuGNNSU0Q=", "+LkXnKK55dcYqyd1R6Wp3g=="), //:\Windows\System32\WindowsPowerShell\v1.0
            ["Defender_AddExclusionPath"] = Bfs.Create("qPB08HEsb7PID2prplWvPeHQGGpbYpz8O/Sm7lGfIoQ=", "iXuVHq4xNDCi/Buw//0y4r/mAvbXaDCiZmaTBrVqlKM=", "4azT2UqSZOuQhLgFo+WtFg=="), //Add-MpPreference -ExclusionPath
            ["TermServiceParameters"] = Bfs.Create("j1gkRulF4MlKOZODx8TowPRQ+wcV8O6nkDhlxbJ1RgD3aQwU0t809B4w0oMGPlfuCIXU7ME8ov7CzRhzGosE3Q==", "VngKLHhsYvrm+eTWZCNF6F8ezAojdmcpSrT4t6z9gj4=", "JjQlQw06Dc67hQzKnqeSkQ=="), //SYSTEM\CurrentControlSet\Services\TermService\Parameters
            ["TermsrvDll"] = Bfs.Create("29/QLdFm+RPL8bHvuoCeXI9HCxEJKxGtvUxXurnJRAeKa566Ktf4JlVYcQcTmK5N", "FkBpVtP9pa9kqwIxW1OBfVg1bQaEq+j/qJHyEyIwsC4=", "xQNA1GYiGdvwf72lnCQMcQ=="), //%SystemRoot%\System32\termsrv.dll
            ["IFEO"] = Bfs.Create("9rtsSiq+4jBcNtD+zV98TOzpaR8vQS2mfXQXAHBDIMm2qVb8NmcDdZ5qkEia5saZOYTh3wPvV05jpfAYQTb+wbFD0TC8mSylIAv6lt/m950=", "7bVBHvrVN5Hi1Dkldmqi7ZsuM/I0fBD5f4xJBCqouaU=", "eDCsq2u3PC4wxkj3GKSbCg=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
            ["Wow6432Node_IFEO"] = Bfs.Create("5Kgplq17GchZGjqt1w4EuTzhpTLsAD5zOesigK+LujViJPzzkkw0kRnbKgfb5470WnryYaTDpkuXspBK9e5eWMD944C9W5NL1FS86mu9rcMxSbo+gIgcpcVxlnWUbrTg", "oA5uueReT9/aeyQyg50FXx0BANDxRhi3t/lfaVN05wc=", "JiCVVkr1ucg/CCVQTGTTVw=="), //SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
            ["SilentProcessExit"] = Bfs.Create("dYt3ax6uR8OqaQJIcUb7Aka4gznUNSJDYlLgoF8YySQt5bUZPJpJFZb5ndIB3f4/G0QQKgdz41uMHzuXqvmjVw==", "CUT7+RNAWLIMT/xcpuXw0T/hl3bmTKi/LPlzYta+L0E=", "esGV0lHQSHZf5KuRr3qgDA=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\SilentProcessExit
            ["h0sts"] = Bfs.Create("POUxkLczVX10mKddp9uZwiedNcKTPVUUrtgodV8h1GVORGlpYmVQVmLPWtKIszIN", "7ipa2U3M+JWVIxyukmnw/Gt7lbemjaBuPuUIPLweAmY=", "JvVcA5X/IbOqN+X7FRAsmg=="), //:\Windows\System32\drivers\etc\hosts
            ["appl0cker"] = Bfs.Create("amsxPbp5bi76vqtDFN7UjGglLvDldnRW7gN4+CYRsIPogE9qBTDrCyMpsadNRAuO", "v048NJrIPgLOw6MKcHzlaLi6UFluD8Vmx1v69VzJhZc=", "wWIIWKA4kO4elMaGxDQYOA=="), //SOFTWARE\Policies\Microsoft\Windows\SrpV2\Exe
            ["Tekt0nitParameters"] = Bfs.Create("hPqIxBR2gOtx4mW2psjz7hYe6MAFtOgj8Ao6lu+rJcsD6nHz4i2znU3DbgJWqKpBKsuJVo5ZDFkizLqY1xiAJg==", "G/ImUwQF2YRz8LM8NwZHqZKghQPrCDgiajrXbjcCpvs=", "3qUrLSu06ZCOFSnYVWbMsw=="), //Software\tektonit\Remote Manipulator System\Host\Parameters
            ["disableSmb1Script"] = Bfs.Create("/llTgYBFR/ihNZcAdmxzS8FGGS+cx5uhkpCR8KlVV5Yrt9v2SQayrTUIcJ0XppEMjy/5jS6g86/K7aXHryxnPUZlFpWzH2riF+2JIw0o/Qs=", "pmQyRre41VXl0ldMOX+lGlz6vHKC2DN1K4ER/tJoSzA=", "zPKzL04TvjohN2HvOKskiA=="), //%windir%\system32\WindowsPowerShell\v1.0\Modules\SmbShare\DisableUnusedSmb1.ps1
		};

        public string[] SysFileName = new string[] {
        Bfs.Create("ZLzaX6rkBz8ycCMGqz38lg==","xOimzhXIUYDIO+KHqkMLo3gdyLv/pgHxaLZwdlwqSro=", "RGcOgP53yoWIcKa1UcUc/Q=="), //audiodg
		Bfs.Create("t6KI0q+Rek3f7g/SY7nDOw==","Vq+Pz6wXCRnQ4KV6svVgze9ww9r7yKGWt5z6aiJ6Qm8=", "6Pbdw6kMnPLdpNbSg5S1jA=="), //taskhostw
		Bfs.Create("SHjZ32j5vm+bcdwApq9S6Q==","ujtNdjGnOUALsSKVMuYdjnmvh/jSAA/WnVLlEkfailA=", "l9dPultXOae5ouPbguNmBA=="), //taskhost
		Bfs.Create("0u96gTZMzw/IeWFS+VzrLQ==","FbZn/+tTzhzakF77+I3YRQUE6j9GtOaiY4FKzkFF9bg=", "jyvmfk4pSgeiNbR4Xj0zOg=="), //conhost
		Bfs.Create("WGmWRWnZFdS5uZgh7qu03Q==","iC7EE0G+LOBp0+4h6PrU+g3ZzFesMiMwRKdOdRpfNaI=", "a+7+B6YNTmqZHzxXWGSluw=="), //svchost
		Bfs.Create("1xgokIQ+JmVlESrukZ9o3g==","OEHp3629bDTLHSZkbc9FSIACToIR3xtHbwxlIn8S6l4=", "Uu67plG4J7gkA/AlkiSmBg=="), //dwm
		Bfs.Create("JD1CRfbEl3ApEM/ruw+biA==","nfgGTlyMbh48MpUQrvNtXwb3X+upWcQoTQCz883yVPQ=", "pNSWXE6KeGvVjZeiBVUhDg=="), //rundll32
		Bfs.Create("bA7Yl5f19ffMk7AKZhs6Vg==","4t3apwFBDAOwQrBdh7DN3QD2s2Wpycr3cFFWE7ZORDQ=", "dYZVyEpj5VhGBv2G96eoSg=="), //winlogon
		Bfs.Create("ejIQYkFX6So+F9WsmujDcA==","Bu1mE00ITtoMqSgmXPiASaxS6X5M5NBfCGo6c8FgLEo=", "5MF6L2y2AANWGJTTpR8J4g=="), //csrss
		Bfs.Create("2W6+GDBRLiFQ8sePYO8+eg==","P43crKRT4aiAPEcIphxMBMhq/VMvZWPkVrTyznjlXhg=", "QVrAGdpU3krXqtoo7YQ9rg=="), //services
		Bfs.Create("3Fqo6ZMi29tdsc/PuAo1eg==","VWaDTWdaTfPtba8SVFmBdUo1mC8ZEI03kPjnWh3FtUE=", "E8BPGG8sOTRHM5Mh/SZgkw=="), //lsass
		Bfs.Create("jimjrU7XINZIpyPSqBNZ2Q==","WMhXxCk0QsIMLs/BHlWb+4/Hb0XVOUVqlBO87Df8caQ=", "3+UPytbVQjUtdez9vM9Hbg=="), //dllhost
		Bfs.Create("by3ioSdki7Hp6U0WLT66Vw==","mi4lWPJVY74QPc7sb8sqDDN5xw9Mgaoa/Bt7w/vjAkY=", "P5tmIL1S5Mfi1Y6J24jojw=="), //smss
		Bfs.Create("P54W4aDO20UqLPQsBcrdEw==","Wh/zrNICbaVMgO6qUalTLdRZncDdeFY8Zucxq5T93L0=", "G4wrjUhTYFI3Beu/BSpO2Q=="), //wininit
		Bfs.Create("527gIo0vswY3+qjP4qwWMw==","nDp17vsXzMJKSrZwrlUscFQpOStpQ0kgpZERIm1qnMA=", "2ZRdbzPU3Nxkyq8FAe7Fjw=="), //vbc
		Bfs.Create("bQyoyRf2wlFKgTHLXh58Tg==","Zl8inBQsCFDnvEbqcF1yiUUMox1B/Gt9peUy0UtvBcA=", "tNQdMqSF9dWGLX+dxX4HKA=="), //unsecapp
		Bfs.Create("ekU1EUXAGlFCCmZLVyEdrA==","kko5XmCAfpnxWtv8bbmRywKzQkKFrGGz6KbjadUp8pI=", "+PBBU2mcBwt1hByMQbS46w=="), //ngen
		Bfs.Create("SsHMzyQ6z00WQ4BeiexaYQ==","OG/75NVFxZd+VzcVqo1EdxerAWFr5vbs7g1MaNgy2mQ=", "sCQG1AhOy5XC+RvP5pVCGw=="), //dialer
		Bfs.Create("mhqJv2OOx+jNhBq/YMPAow==","LVElftVB7YVSxxuRtvofMqfa3yiA7rUOWzpIsbN7Ow0=", "xQOMv/stJHDZz0/PFql6Tw=="), //tcpsvcs
		Bfs.Create("BE2vv3kJSjwCxO1XIsgSJQ==","FCmajfN2PrOtNqM4evp2cSyLt1kMIW4fYz3NqG6fcoA=", "q+U3jVt/QM2j5a6Up813iA=="), //print
		Bfs.Create("xNxmA8E+ZfrchKYwej8Wcg==","h6o4Sd7rcSiT0knhqbbVTwRZ9Dm1hMiGtfPzjyIX5ds=", "f4PDbbyTNYpMujV8h7amwQ=="), //find
		Bfs.Create("j21TvWb3DuJWXRDwAQovpQ==","kp+bDVB2uMP/nj4oMKZ3AVWKWPMG8+/bBYicPfJS+aY=", "w9O5qKfM8LLoqrPfAgOmrg=="), //winver
		Bfs.Create("Xvb3++8tyEMKlNEd8H1VpQ==","Br25m5ajcrxva8m+QsZJzwUUKbGXBamdeXbUzaasSXg=", "Op6xd1joRLB8NLitDbcKHA=="), //ping
		Bfs.Create("CDgadeEFn+ZzZzWSm6gWuQ==","j6603nUqQEU8iDxfYhmXTjMZsPNBhXnn/+CPbC87xLM=", "2VqUPPWztuIgqrVYOceDhg=="), //fc
		Bfs.Create("hyhUZMHPv8iwyf+eRe+Znw==","JRjfEmh0HeJrIK4yWwnym5O0x6+f0V880h4lkWEQfXA=", "tP0q5vSSWDiiJ6F7nXGu9w=="), //help
		Bfs.Create("79nXepok2WvXfHsS9+GRQQ==","SS2oa7KbzCAaSGZEoye19+ZymhOWlwHi0xR35r8XEwQ=", "5vMBhuUOPfwDyff2Sgb/aQ=="), //sort
		Bfs.Create("hFbJOA+HARSGhp+Z2amQOw==","FTqWOz+Z/OJJyLP2s/tcQpp4CJhMeOaUxbV6XutWGRk=", "hX2TRZDvAU8VsKPVlBXNMA=="), //label
		Bfs.Create("NvPUqHaa26wEu+BDrpKvYQ==","dkQUMW+tDq3gOQrALAq3zM23v9DY5MFGRb1RpfBbiuE=", "1ow48usNMj+/XznrRETp2g=="), //runtimebroker
		Bfs.Create("HbEKYytVECzt7sQmJQT+sw==","Qtx/czHT0caVEVUVbK0Pl29Fr/VIlpW+1SrcgEg55yE=", "rRCyK7gsJy5neuh4Q4zEJQ=="), //compattelrunner
		Bfs.Create("fN0+MpufLbcCIbBdVuK6CQ==","5tx1rdyvLVLr1DEmWFZQPvV/kGbQiGCxgZ8gq7o7Z+A=", "2e3F+hIIewzgvbdMW7RFLw=="), //sgrmbroker
		Bfs.Create("okMBRpb3M8EKKc6dEpCbXg==","Yjz2eDZQNDa1/Uz4PQ0HfwJhm/6AtAiV7sGbvu/kfCk=", "6nukriktf5dd7VZFjf3Qvw=="), //fontdrvhost
		Bfs.Create("ayor57e0Nb2bIz+u5ZCJew==","iFpNyBEsevKrC7g2SMs0PQskk51MPHeTP2DT1L0/YwU=", "5btMgOeqraLxtP+/fNeDtQ=="), //dwwin
		Bfs.Create("ujcN8vYxXtc5y9hkWzpUaQ==","Sc1OTjkCTDwt46iMwyh8K3LQoRZpwHK/e0a41ArVleg=", "KkGZWZ0r+fiG+ZjeqTYJ/A=="), //regasm
		Bfs.Create("u6hA/XgvGdRw3349uqvFGu7xA59V0dncF3T/j+hDA1k=","0ieGxsD1llSKZUQZ5MMj0F/L/jSdwgCi99hFNdqEJdw=", "jJ1AoDskmjkC6o05kjbXBQ=="), //searchprotocolhost
		Bfs.Create("1HA73N99ak314hFjNZmDSg==","ej900yERlL5skgIyZfZ6zJb5GzXhhWzSJCGDhsxc55c=", "P8/DIKNlGkEZ8F7RmP9EvA=="), //addinprocess
		Bfs.Create("TI6VOIH6tj7+/Lon4nZf4g==","N7wb1PZdLqYCrE+GQWiocJIT+tink6Vtr3SX6O009Ws=", "figDr4lnd/mM9Z7+v60aDA=="), //regsvcs
		Bfs.Create("7cjLbfAvo7lAu7fOm38Otg==","dO28FL15nLjjPF5h2P/RRbqFVbe4FsDHdArVxZpqbro=", "QznOJM4fZjZtkNY9iwM72A=="), //mousocoreworker
		Bfs.Create("SP/yqJ8rWYF24tRFgj1qJg==","XJoRXF9KL/fwrpye6xdsTUhuPN1ZZ1EwA8S4ZcVNWhk=", "b9TtRdzEROObMvKq67p5Cw=="), //wmiprvse
		Bfs.Create("JM8lT+eC7Rkxt5RD2OOSTA==","eSSa33O8l7ghJ29LxfjpVZdlNLoKOxTUb2MO9LwqVvs=", "kI5OwgJlVm0X6pnLnQUF1A=="), //useroobebroker
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
        Bfs.Create("HLOGbxe10yHxwWaSsSv9iQ==","/vfPYS5sXB9PIxDC3UfGcyI5mGGd1YtlMy6Z4HlbjJw=", "A/bU+OI7VH76Ub8l1CPYlA=="), //SbieDll.dll
		Bfs.Create("0YNRwXohXdbuc+QOWvaq8A==","5vZycxIwxXOIuqjHUT6i8rxXNSevaEmMY4jpW1GBvh4=", "1Ejqcpjee+7JbQW3swb4NQ=="), //MSASN1.dll
		};

        public string[] trustedProcesses = new string[] {
        Bfs.Create("dl3D414XzQfocmxmTvJPUA3GiVOFJWjtC5tiTeBZE4U=","e+4ou6NDPUzD5aRBPCN5AdxWNr518T80bwhirQr7Q6I=", "SGSjC7SpfRbRHR2vaT5K0g=="), //HPPrintScanDoctorService.exe
		Bfs.Create("NhzD8S+Cf5X73k0E6cdnDw==","t7ZZiL+qW0UrMFZuQROe1PjiWtlMP4/x801RF9rbkKc=", "Fkf09n4IOyTa4r9uB5p7zg=="), //RobloxApp.exe
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
            new byte[] {0x2E,0x63,0x66,0x6F,0x7A,0x21,0x46,0x77,0x66,0x73,0x7A,0x70,0x6F,0x66,0x3B,},
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

            AddObfPath(obfStr2, "AppData", false, "sy?sca?che".Replace("?", ""), "k?rn??lho??st.exe".Replace("?", ""));
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
