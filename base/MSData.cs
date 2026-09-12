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

        public List<string> DisallowRunIgnoreList = new List<string>() {
			Bfs.Create("uNKdE0NmsZ4iC0ax3Ae2F+ub1yBexWRRk6+vZ7ZZqt8=", "Ad5OjPPn9QOzAsemh3wFtLzb+8WYrQVWZ1d5ZAtWwuk=", "MAXtywj3SMADdI3u0DqUlA=="), //CompatTelRunner.exe
			Bfs.Create("/sR1R2thyTC2nBTiBkI2XWDYUYw6GyfMKAwXsre+VWk=", "Y5pPgmx8b13g6jvlBlvRvwTIdjpo3M6yadBwlpEJRII=", "uBTZ4xuLCbjvGvak80dhGQ=="), //DeviceCensus.exe
			Bfs.Create("64e/BaH9WchvXrEANs5S+g==", "CK2UfFYmIbV3mrPCkSthFnXRI8wu65QliVQhNhzpzxs=", "RJ0DPAwcuNNIgH//EoVrSg=="), //MpDlpCmd.exe
			Bfs.Create("p5F95556zxVp7ZNNFvtRAQ==", "glH3FuKetWzwD0hpVzcGl/4OFJKSPJJ5BnB1iHyeddE=", "vrcdX+9vhOkhOzTd9Izpiw=="), //upfc.exe
			Bfs.Create("yibffrCuwkB+MBuy2jVuI+brQnELqSPKAiM3cy31j0E=", "Khrly4rUX//efaBKSpn/qSSwu2PItAe9weEXq0S2EPQ=", "kvqFQlxN7GK5pf6ivB0WZg=="), //software_reporter_tool.exe
			Bfs.Create("Tr9+XFIPe8beGte5doACWYHWiR1iRa8AjSK+dl89G74=", "Cm7CchZkdWAOyL0JT2eAjXENLWnENTv+jB9ByeH4xfc=", "umTgXI1QJb0SyxAiJkm0kw=="), //SecurityHealthSystray.exe
			Bfs.Create("gHCW6zcXiBtkdhbd0o8P2cWyVSD0+HvMlZPwdcGqjsQ=", "kzZPSrAbO/7U6fDrqpndMHwg2xE+19xew2plaYnSZCU=", "wMYzh6Ctlp6kVxagR6/Zow=="), //MicrosoftEdgeUpdate.exe
		};


        //@"\\?\"
        public List<string> obfStr1 = new List<string>() {
        @"\\?\" + Drive.Letter + Bfs.Create("TjLz6L88YoJa+FtXNwyxNef1QLgGH4RNpcAHImcyW3c=", "9CYmMeAlHuRG8gON/kcfTVMMZ/aV+c7gwMHLhLtKZcs=", "k/CfRhr+0I0jvizJyukosw=="), //:\ProgramData\Install
		@"\\?\" + Drive.Letter + Bfs.Create("e1gFDcC2H+yfk+lcqDKFIsg9jrXUFopYqMBcDsaQbjA=", "clCiJ+IbYKQvB7zWdiHGLQ13gcxwXnyHcGeqU/gl868=", "aaFK6zrn2uUr/pc5IVIRnA=="), //:\ProgramData\$pwnKernelSystem
		@"\\?\" + Drive.Letter + Bfs.Create("VsYRe0R7tnDpGCzf86W4toTytsXXwhqy1AfUhu211R8=", "TnXoLV09CLxTL02u9faFkE9p8CWV/ffbwx2OCfHGQ+A=", "4Ids4g9RY2zWyC2mEErCOQ=="), //:\ProgramData\Microsoft\Check
		@"\\?\" + Drive.Letter + Bfs.Create("tMk41KLnRDy95YG/BLbGMKtMkQ2/dFuesR4MUgUW6+c=", "OqiFkS9bF/CmLzlZ/+gqoBlk96NpJyjUBF3P3YDHfb4=", "XhxEgm01FAMweTl83Cl3Fg=="), //:\ProgramData\Microsoft\Intel
		@"\\?\" + Drive.Letter + Bfs.Create("rKQzNO1npjW4wT/2SKFs1sIkZwoN6RnRcnpQWyOihYRjWHStuvfzcZN2lwAIK/qPnuQSuYkwwIK9IevHQUIEtA==", "rFZHyRieKKnv4e4apw8rnQVDCZ8+7twsAQn0BcJ7G5k=", "uGFVp7MhcAIGFUKmZz1TqA=="), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
		@"\\?\" + Drive.Letter + Bfs.Create("gdfxuirHvh8HevNsOyXhO2dZICnG6j66LTfjEw+jPw0=", "2pWWsARR8QteAz/Tk6E3ckvKGOXUlyzCkBOALxt9pi8=", "fMm/6uP3zK1Ft1YThxmYEA=="), //:\ProgramData\Microsoft\temp
		@"\\?\" + Drive.Letter + Bfs.Create("rJGLaKvw1/RhGI/LKdWwI/RTD6C+fs5WTW3G3oRjRtw=", "xhhdBQEfra7dtzeP/vsjOIq8EIeh6CWtdOdNaRaboCk=", "IN0UIkt1TPKE6bFvYD96EA=="), //:\ProgramData\PuzzleMedia
		@"\\?\" + Drive.Letter + Bfs.Create("Gw45c5mrjQJyBnel9PICuGCexbP0qzCIqbqSUVUH3ps=", "pgHGUZeRC3QdXUKh0oHBWj74226snpoUCfff53a9DNo=", "Kn2gjXZ9wGITxB3pJKLSkA=="), //:\ProgramData\RealtekHD
		@"\\?\" + Drive.Letter + Bfs.Create("H5ryGhHkEoVU6Z4+UfMPxeG+rYCodS6GUerUhnJK0NI=", "xjM4A307UudxCcXgyAikC1+XOU24R20+OmJ4V0sBR6I=", "XR+cf//B3j0suAYVnY9APQ=="), //:\ProgramData\ReaItekHD
		@"\\?\" + Drive.Letter + Bfs.Create("9NFJgeceFXdZG34mKGkDYqEjnCf08p2/wRtaznwzN0o=", "gURVImh8GdamqxJRhD/0xnuJhBNqOrXhThlM5JOy1Qs=", "kJ8Pbu/0VoTidtMuAf9k4Q=="), //:\ProgramData\RobotDemo
		@"\\?\" + Drive.Letter + Bfs.Create("zkWJg5VPCX7vP89mwZM0OTB03jisKtQKT9e9PEDrLxk=", "LQgbB5NinUlD7eABnbTgWa9LaNzbITDf8G+r6f84kWk=", "iunmRoek7AA0p7r5is9FAQ=="), //:\ProgramData\RunDLL
		@"\\?\" + Drive.Letter + Bfs.Create("q55Jt/eIFBLxaIdEXqx8msLkteRJLc3z/D3kaVXZnmI=", "6vVmbVM+B/HDXML0MbBXbQyHuhuLx5O1nooQT4PYvmI=", "JzhNAeQY5xwQF2w4JrGuJw=="), //:\ProgramData\Setup
		@"\\?\" + Drive.Letter + Bfs.Create("VbNEEYMxabMU/xw1CXnGtntrU5GYnSzPLfOUoKD+Wdk=", "IYzuSy5dpCJH+XBl5O20oSdwCMlQcuCKPUGhUYAddWI=", "TIYhp10mtslbjuAUAmKedQ=="), //:\ProgramData\System32
		@"\\?\" + Drive.Letter + Bfs.Create("5F/hVBpwflJse/Y5xTzLFfjURpG7xsjKQKpVJZx9j/A=", "LE0qFg4DCdolh5wG1wCD5TIWLmS4kCnh+rmDFhkVKOs=", "VKah/dICjl8Dr9OWo6Kkyw=="), //:\ProgramData\WavePad
		@"\\?\" + Drive.Letter + Bfs.Create("MCI6gQDy6AdOUfAKLWljRotFRdQgyL0tCusvL8p3stYkjUtGi29KHREEk6oU42//", "uD7hQsnW4OH++8ETRKn6uj/25crAI/ooxc1jlrTISPk=", "bA2ZMyBAfLhG4gdqmqEkZw=="), //:\ProgramData\Windows Tasks Service
		@"\\?\" + Drive.Letter + Bfs.Create("+qb9fGIIROMh63I5ZnLV1RkvxuItutqkm7oNHcpNMn4=", "4dcBUmQHAIiMFdxC+Th/VKOwY07j4SXC9VvoK70B2Cc=", "zT5UORGyPRiWeSx2d2KODw=="), //:\ProgramData\WindowsTask
		@"\\?\" + Drive.Letter + Bfs.Create("wGtYj5l/DQuTZqCLLhnaQHfXpoFHFcM4GzsLakl8iVU=", "Luo5vE0jLwja+aLdx4zDK0L6OxEaukw9xeN43my0IAc=", "os58IQwCMeOzBgLxHU4W4w=="), //:\ProgramData\Google\Chrome
		@"\\?\" + Drive.Letter + Bfs.Create("mKM2ZAhKl07kFTXxnDII27WYqADSnAIOKht7DPF7NHOvbhS/wFPwsmPs3dp9Pfkd", "5PziBeO0BlfZDwCdlYHR+N5pTBsW7LUPlWAd5aWz+xk=", "sPlyM/mEBD/SnBd+ezCNAg=="), //:\ProgramData\DiagnosisSync\current
		@"\\?\" + Drive.Letter + Bfs.Create("ihyZJe+gJgmdZL1DZY7DL1iw/RpJl5J/FzI6+qEr+UY=", "YIJU4aeAYil4rpkLMctVmyOFmH8eftC+t3gaCq6TW9s=", "0tAKhWGnxWHAQhsRziCl/w=="), //:\Program Files\Google\Libs
		@"\\?\" + Drive.Letter + Bfs.Create("FlCVGW/a8wFhXO6eSm05gz0602ABScGLmvgRa8iO6kJbUH303hojKrK0dcWDzxio", "K5FW7+zuKAVqKeRzAYtGWjShbELWJs0sjqKvqSWjl/4=", "/BunL7B/Gg2j4IBJUSNTrA=="), //:\Program Files (x86)\Windows VC\
		@"\\?\" + Drive.Letter + Bfs.Create("LDfb3PBiGuLw3i0Ki/g/HKBt53NE09f5hMZXoiNkRyo=", "k0vN0Jv/JT78RWnDte4CuEdne+7MKPylqOJbR78e58w=", "DjqeRbdPstFGwQUHk53hiA=="), //:\Windows\Fonts\Mysql
		@"\\?\" + Drive.Letter + Bfs.Create("hepgSqfqgy8MVQqRsTwIz5ZqtUn4CqgkjUN6YEvGXhK4a6Cr9ay2BcrKRGdSCsQ2", "tF6SZR3a7NJwSonxvSjp77S739Hh6YADTdO7b7oYeWU=", "gZKgawlvS7B9lixJRGUzlA=="), //:\Program Files\Internet Explorer\bin
		@"\\?\" + Drive.Letter + Bfs.Create("TwJlLF2Fbb/JPgDHj63/H/621jD62eyzZwP+xiWq0KA=", "0fTR2HQ29bR4yPfam7K3OPeg3EPHlrN4XckgWAouDRA=", "rgUCB9jZgaFlVg87WB8VUQ=="), //:\ProgramData\princeton-produce
		@"\\?\" + Drive.Letter + Bfs.Create("nQo88j9FNnvbyZ70t24L4EukP2i9v6YNFIvcfm6UimQ=", "AmIlDn3tseSBD0UZNdo0zCv9vv6fe22VvvGoGorcaqw=", "BZCl8P1N1A3NKcvK7DfO+w=="), //:\ProgramData\Timeupper
		@"\\?\" + Drive.Letter + Bfs.Create("6jKm9d0rodpkbfU9iZNaSBY1xxy+bUQWR2d/kKyz304=", "zCIaGzOIEH+YtWS2an4g2lB4FlACoBQfwgyY396ovck=", "3EQSSyfVQoizWOQ6GD8HgQ=="), //:\Program Files\Client Helper
		@"\\?\" + Drive.Letter + Bfs.Create("+049dhRen9V+vqw37rGKWoFPmCJB8kQYx09c1YOEmQE=", "H5OgoG30odt8OwXLuLrUDPSAIudIBeeXHHy7kXuQz+I=", "+qGh2TN+LFW9OqmQL9W3hw=="), //:\Program Files\qBittorrentPro
		@"\\?\" + Drive.Letter + Bfs.Create("WfiW7zaCUagXeloeY4ByXLFojwJz+3lfyBKEysJ9zEM=", "/HJ1Cvvbe29MDIGd+Znx2psRQrhA9U1n2EpVhhwVXyQ=", "lyDqlcwnF4/qPc/6xt2TAw=="), //:\ProgramData\AUX..
		@"\\?\" + Drive.Letter + Bfs.Create("FeMKp/xnaG2omP1dIXNN0vi9GZV8lPI0j+vx+L9Q7iY=", "08uOBYkE0+f43xaaQiHjpMVDZ6NaRSjRK+VwWzpvPrU=", "/cG5sMvbcd1+qCIJnW91Kg=="), //:\ProgramData\NUL..
		@"\\?\" + Drive.Letter + Bfs.Create("+5YRDwvcezrN1x3cRD4Qs5pFgT9x76DXaI3v2UuVGxg=", "SAF79T11OmVWK3EqyTAGQswbTKFp0nWNgxYZEevA6Rw=", "bPgoQPGeU5NryMAupGFPqA=="), //:\ProgramData\CON..
		@"\\?\" + Drive.Letter + Bfs.Create("o4uQf1gSfFEaMofeBa0lbh0ovaaREtpboZbxWzBtI2A=", "MOjzS+X+qaJW+B++oL3dVOulvMM7bEeGxSf/aXIcZ7A=", "2CWncPgWYCIg6R+dGcbmFw=="), //:\ProgramData\config..
		@"\\?\" + Drive.Letter + Bfs.Create("ZejSOR2QtschkIBJ2XOU1PnUSi70cTtTA06wMWz3Ftw=", "woNbXbbS0c95aOHe1ASsCvjmtKeVe/9E14d1Ahk7VJA=", "d00tk/HTE99RG1sThwxGaA=="), //:\ProgramData\logdata..
		@"\\?\" + Drive.Letter + Bfs.Create("ARpbgairqoEDvRFlg3XVdI10gKrBzu+6MPevUgWnCfQ=", "q4y8jH31L/Lys6IGk/ZH2cYh/3L4shXwG20b7PmgU20=", "j/uXcy78lSN3K3XsIWy98A=="), //:\ProgramData\Jedist
		@"\\?\" + Drive.Letter + Bfs.Create("gxWmH+IkLUngSnTUPPQMKEPgqTDAuUTf+f89lbyMSbBcsYxGj+FRPPMIjWabMfyCFdu1DqBernZGWBW01h8x9A==", "COXtB9J3GfM/8jLnKEP9Oo24Pakje44IHTj5zmyzaU4=", "rSYyCOGMafYCy2jaZUx3tQ=="), //:\ProgramData\Classic.{BB06C0E4-D293-4f75-8A90-CB05B6477EEE}
		@"\\?\" + Drive.Letter + Bfs.Create("zlBgog18Sq2kDjPRJp+/Rs4y89DHHLk974H77zS9XQ8ryLG53RXskhZvPJwIxVNUjXaZozr21EqgeShxKrJiMA==", "7yeN0ek20jWL04WLQTf+3sws9/ziOzPTquk/EFhDBqk=", "n6FBpARKZSxpcqjwIC69pw=="), //:\ProgramData\Classic.{BB64F8A7-BEE7-4E1A-AB8D-7D8273F7FDB6}
		@"\\?\" + Drive.Letter + Bfs.Create("/l2vS9JVXDyKCFaInsdhN/AoL8C7mmlOH5yAvGKRJpg=", "WuUEzqYV6YsRhcT8Isi6RE4LB9JZxuAa48/pVlI3Sw0=", "2X0FZ/BfyF2bBBXUHuhMrw=="), //:\ProgramData\Gedist
		@"\\?\" + Drive.Letter + Bfs.Create("8grvFayuqh0zEc13/YHIFqkHxBnQvusWhyuNYlyhY2c=", "R92kh54a8ONnOHvfE6yLBuitOyN6bofQjPINojrj4Xo=", "IZKnHWbmsxM2+QU+CtGSeA=="), //:\ProgramData\Vedist
		@"\\?\" + Drive.Letter + Bfs.Create("pe+aIbWz8xr5v49dDVXlI5YFlyeVIwKHlgFK//ie7Hs=", "lKzufYXDMEuEkk74MEmIi9XYMuN1hR/3OGdDqmit9u0=", "JenuU2dwx9e8CeWr7rdNFA=="), //:\ProgramData\WindowsDefender
		@"\\?\" + Drive.Letter + Bfs.Create("1LjdAaI3f1sc+wGtt3Y9980fQVKF8x4RITALiJUO6kw=", "9FyEtl1EfE5micTrVHIkax4kSrBwY5+isAwiv+++6BI=", "ZuTmdp2Qs5ZKRrdYnUecDQ=="), //:\ProgramData\WindowsServices
		@"\\?\" + Drive.Letter + Bfs.Create("sv37xQuWhzfP7CUMzRy6g8XYaMdM+479zBKv4SeQcfJ3fN44Ljj98AveEnZAgfI2", "H0+/V/77hUvUsMhIsAzQYrPUSZUbjAHFNDaBkU+rZyY=", "f6lvTvwKmsaI0tIhJtwGOA=="), //:\Users\Public\Libraries\AMD\opencl
		@"\\?\" + Drive.Letter + Bfs.Create("XfJzeSDCisXG2kUL334P9coMxsnATTElTY49PorbtcpTKYHstMB7U9hB1FXBhJua", "HSYF1DXQNsE5piguggQ1cSaKMw/o65Ph9Tg8cuiFRb8=", "UnzhdlMTDljHCYurKQAK8A=="), //:\Users\Public\Libraries\directx
		@"\\?\" + Drive.Letter + Bfs.Create("ip24jNzCIuuQYBBFsQpFbK7iEUJhtE+Cx+nmgko5Ryw=", "7pMFi6KnMxembnEnhHQVV/mBoNZtVaGXk7jy4O5L1TE=", "e8JGu7rLv9X0rk1Bx/GdvQ=="), //:\ProgramData\DirectX\graphics
		};

        public List<string> obfStr2 = new List<string>() {
        @"\\?\" + Drive.Letter + Bfs.Create("pCjsezSUxu6d1ZDESiGU7jSgbzjw3saYF4CHiiB807A=", "L4dgcWpJNFAG+mIW00ZPcezTVYDevdatQpQDP4T+ZE8=", "yHPQ1+WKsT59EOrXirznmw=="), //:\ProgramData\Microsoft\win.exe
		@"\\?\" + Drive.Letter + Bfs.Create("uJUIZkbpN3OaEs6mJjSpvo9M2yF8uDi387I1n3WHKC9K4C3JghG2/+2yq5o0Ok4U", "fM7IvBRZbCxEKBdu5tvw322DoH34nJtQH5bLqh/hD2w=", "WK7kZrEYOvxAH15evPRd6A=="), //:\Program Files\Google\Chrome\updater.exe
		@"\\?\" + Drive.Letter + Bfs.Create("sNIVNAxewGcXWOas2VTtEzPKfihFeGIp7luHjUHXLubty9704/9J4fuJUA3jpIs/", "KEBPgkatZhYPWFa1ssimf0DRIwIaPDSKZeVlayx98i0=", "srHwLfyfKdL+y+RLBWOOUA=="), //:\Program Files\Google\Chrome\update.exe
		@"\\?\" + Drive.Letter + Bfs.Create("bOP9eJiT4+AYZytexATfRMGRwRXIaRV7cB+yGa8IoYrrH8p0UTxqeCsQMHXB24rQ", "8IoL8b7unF1KoEYHlHmfOE3fkuSAN7aHbSjUWo4bqC0=", "76pyNKLZZc2gWqGeXiGO1A=="), //:\Program Files\Client Helper\Client Helper.exe
		@"\\?\" + Drive.Letter + Bfs.Create("HiOZjLtkBZ+oLEzIPj2r4ri0KJbwt/KVr7g7OdF2kDclEh8Y3dL7a3xBmnd30FZAVBuHeK/rs3DTSTP26q+vzQ==", "sUIfGVvfWjfq2uqJGdFN3E3xin+7ZPpsih9j4hq7z4M=", "oI9ax2GvO6w/phmlVocfHA=="), //:\Program Files\qBittorrentPro\qBittorrentPro.exe
		@"\\?\" + Drive.Letter + Bfs.Create("KSMoNlt2jFwbOUM4YhLh6jP+UEe4Cj5L3cqfU5KKw7w1fbpdQL7rFv6ZmKnA9IemNg7zmMCL9wGg0mf6bvG+Wg==", "E3UQwgwVgFoNn0FEF+E2ix3n88aj4qUryiYOov8VCHk=", "/GPq13pdcFjkf6j8MjzFRQ=="), //:\Program Files\Microsoft\Spelling\en-US\default.exe
		@"\\?\" + Drive.Letter + Bfs.Create("Nxcjz2HoFYn/d5qph2Ndom0CiRUr4NkSalyPfprET1E=", "QfdRPKIyu0CUINHLSw0Gn1pedbIs2mM4DoCiew9pRuQ=", "4cTXSKCPyUYJi1a5tFjS8w=="), //:\Program Files\Game\game.exe
		@"\\?\" + Drive.Letter + Bfs.Create("L5PQTkffVIlD7w5x7WvS5QPWGios1Qn4a7eWlWJ30H9lyF0X2M6YkzoCdqQDf2DfnSNv0Opx93bZkNQW3LhMPA==", "rWnV22pARhFhaSQZOr58mHRDfmsc3FwKU8erJ0GRyqA=", "9kmc8qcxFycQryYGY9m/tg=="), //:\Program Files\Windows VC\ScreenConnect.ClientService.exe
		@"\\?\" + Drive.Letter + Bfs.Create("q5U/5tc8TNhZX6VAJQevuPxeUn5LIzU53q9/KNncPl5g04qZ++ZIwFt5C9AB1C0NB8PYNc+wyZmmSAsaxjXLug==", "d0k/Uz1NhcS5w6ui1NVdYyUc7RJV7gft3GrLssRkyVA=", "/CZScdkVSgvMreUj5V8jUw=="), //:\Program Files\Windows VC\ScreenConnect.WindowsClient.exe
		@"\\?\" + Drive.Letter + Bfs.Create("4VIhybbHewLeDGUEEAOKag3VXBk37vcFlRq2c/n5fkAtN98GWnU85vB7BQ69aCQ+yANdD3NjCR2bw6aG0SQMos8lp4VDCIAJkG+8jhVcKyc=", "vPDYV1AhgR84r7DrmyIVQ5+wbcV1caOXj0Q0+p/jrRY=", "mLI07b26Nkq9SctlF82oUA=="), //:\Program Files (x86)\Windows VC\ScreenConnect.ClientService.exe
		@"\\?\" + Drive.Letter + Bfs.Create("OCW2UqEYYtM4bMTNVJuW0jUq4fPE8HcWsQsKMesqVD+SDJMc1QUv6hTOU5HujG7lP57Icz+Sh8zqTCvyPQRy1SfUeRkuDyjQLIp9OqUjAOM=", "elDo8Ckj98U8V2Bkkgpc9bzXro4yRK0jHuBcSMNJJ3g=", "ddeaFfxoM2h9VWUAKE1OrA=="), //:\Program Files (x86)\Windows VC\ScreenConnect.WindowsClient.exe
		@"\\?\" + Drive.Letter + Bfs.Create("Yk6iJFwBBOpWS7iKrgTWnzvcbyPhiIAKtW3PCo06+yvIRQXNsSFf8w7PZxVRbfPQ", "l11kAmGWMpbRVATJomlWbtemT5OeLtkYAnuh/qeWpyM=", "niE1PYMDYvJJDIhp4eMt/g=="), //:\ProgramData\Google\Chrome\updater.exe
		@"\\?\" + Drive.Letter + Bfs.Create("sE3f++mIVMcEyDINctstc5WcCxORoV8QiFkhJi6bOlrx+FNFGglDtGK22I53kQyT", "+AXFFwL8jFgweprHIFiLq86ocA9E7Xz0Kp8MGtdbN/s=", "Qgi1YofNkz6Uw7ELyrJ1nw=="), //:\ProgramData\Google\Chrome\update.exe
		@"\\?\" + Drive.Letter + Bfs.Create("wlki5UAUzh/ryTKdVwcjlDH4EACmWeWCdrgTV0IE1PWIEIiIDiwFdT2JJ0vppQcq", "Jw8rZNDkPNbMHC451LA1XEV/RfvQ6GYhm5jx4XiXqn0=", "jWErt4QRpell++tNG0YNSg=="), //:\ProgramData\Microsoft\EdgeUpdate\Update.exe
		@"\\?\" + Drive.Letter + Bfs.Create("Tw1TGiznzb8cVGw5L/Z1AjtgZnB9Te9xQKFP1AfNWw+FGT6nrGreT1SC45m/2eKf", "0/J7sDdpPPEY68h34pUfL/DRHrg5KLM2/e/tZBjAZyE=", "NmJW+l/53T3yjEgn0x5eWQ=="), //:\ProgramData\Google\Chrome\SbieDll.Dll
		@"\\?\" + Drive.Letter + Bfs.Create("1GikVbnL+jCwDA60utzska2mEgX1MxXI6lDrAp1vBzs=", "p/W5SwXHDFEa2g0WguGl4p4gTA8ebBmatrBqvVm/b8M=", "nC/Wr+jWG569D8HOBEnN9g=="), //:\ProgramData\RDPWinst.exe
		@"\\?\" + Drive.Letter + Bfs.Create("Z1JeYNVuJigK4QrF6VhdGRHcsxoYHvGfBovL4RgWk8pni+jn7I8Mf2yLXr7IK4LQ", "iCos5VSMAIlTYgxtXcYZd+6pO9sf83ADVcnan7VaEVU=", "TJVl4Grljyz7weCNibvAzQ=="), //:\ProgramData\ReaItekHD\taskhost.exe
		@"\\?\" + Drive.Letter + Bfs.Create("krqqpWK6UjWENpz+tp+9nmpyy/W0r7u+1bKlgKFVfJ/lscBUxlWpm3WjhugU8Xvi", "Jcoc+VZPAv73JK397n8rQBla89akPhESKi+AadPa7tQ=", "rH2PCxBzErDZTJk26M5fbw=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		@"\\?\" + Drive.Letter + Bfs.Create("nnCM8igHoGCw4xBf8aFASRWvnmNpHB1p/COi1dQAQrCZS//KfdhXVNdnV3qTgJFA", "j7wx4zB99fVarVud7elJiSd+w7GXZUrdEGlzY4CYBWQ=", "c+/KQNvQGJIspFn2wDBGcA=="), //:\ProgramData\RealtekHD\taskhost.exe
		@"\\?\" + Drive.Letter + Bfs.Create("hA93CALSN8YnigvEFBf6/v5SaDjc6vtZodX+r0ce4fUNoK8vBZShOc78BdCB//X0", "hXzLT1Pr0zGy8djjwfs7vPp3/QQUofL3Em58uANHSSE=", "FWnoRdqxYhVcUoLFyUwl4g=="), //:\ProgramData\RealtekHD\taskhostw.exe
		@"\\?\" + Drive.Letter + Bfs.Create("lL7ZcmzFLs0/n5CxElAEFQU0a8SI3EwqN5Z1wK+ZfvLpaTujUUXIQYOlI39Z3JmR", "/5rbEaYhTgNYRJInWW/f6YgnO+TXuOt4EHjbv2OKn4c=", "BroVIej0JPRPmwGG8CK3pQ=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		@"\\?\" + Drive.Letter + Bfs.Create("wiiIAWK8BRp5KWSwPygsqs+K7smBt2ZIfPceNhpfO6XBhgY6G6xNHkLCooFKbmoG", "1WT2vAh0nVXsfirRKt2KuKdAncUC9gOslrXge9ZClf0=", "pnnGRNQ58PiIeMk5Om2t8A=="), //:\ProgramData\WindowsTask\AMD.exe
		@"\\?\" + Drive.Letter + Bfs.Create("qs8gauQIzSyS+tDRJcCBgs8379Bl+5y6b8FpouN2gxufiwlmSl7kIVRKNCCdHGeN", "DBH9Rj/i61YruoZpByRzgYoPdR0S9cTkySI4tp0HsOA=", "s6TuiipSsLXWjC8S6OE7oQ=="), //:\ProgramData\WindowsTask\AppModule.exe
		@"\\?\" + Drive.Letter + Bfs.Create("Ma/nbaxevyngvYa2Exr8NSMlA44NBEG504OjdPV9Ch7sm7WuKAUgvwsoCmDbzTUK", "m1asRK+4tm70n7V6HL1tuOZjOqmUQRMBerdv3cXRkf0=", "KiECjLqv5mHl4sOBzaLHnQ=="), //:\ProgramData\WindowsTask\AppHost.exe
		@"\\?\" + Drive.Letter + Bfs.Create("fDxuKzRKYZyIcfURJmVL2cL51x6vi0F294XuNn18+9fujToUpMBxPlNK8JkLBct6", "UN0ALhAc1CDq410hplDPPO/aYXP0AJSlI8QhVHvQyB4=", "zw2P4t672++eqRjylLHbDA=="), //:\ProgramData\WindowsTask\audiodg.exe
		@"\\?\" + Drive.Letter + Bfs.Create("KnlpVx1aXryYzPGtEQppbWn2wJR9x9RkYSQOnvSqhtIg/kiBZD6bjfcYNigF9rgQ", "x35cIKD8yj8CZS1EVU6rqEEnxtxSGLYss7uHVcdxFqk=", "AScrIx6AOyFegdguhbSYXw=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		@"\\?\" + Drive.Letter + Bfs.Create("sPpbHol/zWF9di4FF1FSaan5TjCmzXDGt6CWr67A/Vg=", "f3Pkqsu4K277/xdyuqtxomDeuTEmAn8cfC99HBeDU8w=", "EYq11DEckz6kZhxVtxto9w=="), //:\ProgramData\RuntimeBroker.exe
		@"\\?\" + Drive.Letter + Bfs.Create("iqSR6Bpw701aKgHpZAyqQPRB8UxKElJkJmRxt5ezCgvTqDPJJmgLjl/yi+gaF0ld", "ktu1Ag28YRo19dbnhEs75jGHMkwLj9mNt7kcqvB+ipM=", "ADZYAsjwZmSeml4nZ+0y7g=="), //:\ProgramData\WinUpdate32\Updater.exe
		@"\\?\" + Drive.Letter + Bfs.Create("QmEyuXMeCWNS3tERyAzMndElATOAZA6YN++SgGBlDN9nSKNSNflBIo4impepUF/C3UYwNPG7S9wsMubAOzHoNA==", "Xa79fRQjRkc1Bw2/PmBMUHiyZqQWxBxsQYAy8m1IELs=", "Gl4hThTWRpNhYqMT7gY9PA=="), //:\ProgramData\DiagnosisSync\current\Microsoft.exe
		@"\\?\" + Drive.Letter + Bfs.Create("0QKLVaCEqWemz7pOEGtBkDoIxS7HqNTL5eJc2AzThqymcwbgFJ258/WoGnbegYDw", "S4rS6wQYqZeIAMiEqqi7P/aB37DfpffR3qZBaJKaVxM=", "HT/ywmyZLy6AlK1bDmi3JA=="), //:\ProgramData\sessionuserhost.exe
		@"\\?\" + Drive.Letter + Bfs.Create("Bb5LEr1KI3M4ata5K6syonczbfb/W0vJzpwS3b5MC9uz+EA1ODW/7dy7R+bD1dMN", "nmmVVlrVaxNdg6E88NpqsAh4zhEk21PxHMLaHbGXyHY=", "Fx0dBwK65S5vU2r0KXNZ5g=="), //:\ProgramData\Win32\CUDA\DisplayHelp.exe
		@"\\?\" + Drive.Letter + Bfs.Create("lXDlzAfPkQO8+Z+d31nWlp4N6yQYGpP3m/TukX5rAJ8AM7CKLwQvWxsnyE1I5Ll1", "NGbe4Dwe5fAtOHDH50KsIiXWq3yqOlgfChxoSGLeOiA=", "RfnVFJehlqq1uPXT/8LoSw=="), //:\ProgramData\Windows\WindowsUpdate.exe
		@"\\?\" + Drive.Letter + Bfs.Create("96RjYnpyDib5IIpf7aueQrfbF+qXzZaNcjnvUOisPQtT7Vh35sujyw21bjAvwHpi31tI/LbOASfjhOF9lJ2hsQ==", "JCbrqiZRMvHClt1Izl1dNiwNA9b/6UstNlAoX0zYYVk=", "TPXJOx0IxMe7y6vMVnm7Hg=="), //:\ProgramData\Microsoft\DeviceSync\msdevicesync.dll
		@"\\?\" + Drive.Letter + Bfs.Create("vAzpUWcVnv0y/lS5URbI7zEU+3TuFDtirhFppT296Q9uqoMY5ScDakUOu/FEVncA", "5/upY7lakU/hY/lt7n6ztSZBWNnIgjI4BbRIvDYDn7E=", "9mWrFlNHgtdDvJ+HkYv85w=="), //:\ProgramData\DirectXSetup\dxwebsetup0.exe
		@"\\?\" + Drive.Letter + Bfs.Create("iBaAzFXs9wpZlMs0HTKLDADjamQzrmMe3IqrCSQsnt7YVYn3oLS2zdF5FI+ffbFw", "al3U4flkmKNOBp7/V5Y9eof1w/Czsw0OPqj8x7jWVVI=", "vb17Ma0Bv7oZoVlSAyC5cw=="), //:\ProgramData\DirectXSetup\dxwebupdate.exe
		@"\\?\" + Drive.Letter + Bfs.Create("vQLQdsJjgstqwPGzOVuaFJozhkfxZXEZzRih1e9HTk4=", "Aa7XD6UlZLsNUWr2Zwrym/lJnEIaMPuamuRoq0+ng4Y=", "yCqeDdg5IJ/Htxrre6QKSA=="), //:\Windows\SysWOW64\unsecapp.exe
		@"\\?\" + Drive.Letter + Bfs.Create("Sq+87JDNVlI2WxBlKMO8nlN/YZqSFvweYKVfFvXrV20=", "k3G0qn3RTcLv3keQ18W9r5fYA5X0wRUej9okxXUKyFE=", "auKMMMkgP8tfy0sz23n4dg=="), //:\Windows\SysWOW64\mobsync.dll
		@"\\?\" + Drive.Letter + Bfs.Create("dfaNsC4KqpSzPSpaGRPmfFHYYoJbY418MU7YlgKolE8=", "tckWluTLEv3FA0sJPTEXTLc8uQNNZlKsNcDiTB/E+C0=", "35GcJih4FzhTr1GZ+MTshw=="), //:\Windows\SysWOW64\evntagnt.dll
		@"\\?\" + Drive.Letter + Bfs.Create("TY/a/+ZixrcgotUqotQ1OZ/j8U2j1/Mdt1ekIroKUo0=", "8KIKzA9wrZNMCIgbogTQGg63NEjax0na1sXMAaI2vV4=", "XeDnqfeiGwGvHrMupXkr+Q=="), //:\Windows\SysWOW64\wizchain.dll
		@"\\?\" + Drive.Letter + Bfs.Create("RvPTvAmIyAyjUwB02h03XrjczwRIef4VX9QZ0Rt3G78=", "f5ZNWHQ5DLGWlzLd6nNWXY7VkDOIkKZkZJ2SdqJsAd4=", "0mzNBN0IbuaqumzDZpe6tg=="), //:\Windows\System32\wizchain.dll
		@"\\?\" + Drive.Letter + Bfs.Create("uDa6J7gewcUyBGVtGQxX/24GZD4StjqWd1LHQr+KDsk=", "DmHjfwt6ZUd9sFpvuTaeSNNa79ilMR+2ueWUkAeCotE=", "2j0kEVASRDt7FLMm5ewcdQ=="), //:\Windows\System32\wizchain.dll
		@"\\?\" + Drive.Letter + Bfs.Create("Wiu14tp6gZQs6P85+nOIc37h91CzwPp4Dia1GBnVKhyqg+X6tcvr6M59EcPwZ/7G", "iUGtL5DAAsExF6jgoctQSlg4RM8Dh8zolfoI3YVkP1E=", "Flw2ki8+kqCcPojPfoKpfA=="), //:\Windows\System32\WinUpdate-NF13A72.exe
		@"\\?\" + Drive.Letter + Bfs.Create("YzXWyeyxcGqzAR9EIzf9DWeK2j+LNP6P02PMCCb45+OUzwWILxFNbllBYRE92lyl", "zBUbDhQIk+YgW3DHOSN4CLCpA/wBOYqgmeAolM3R0+0=", "BZeTJRE7VwGZuuDokdwwYQ=="), //:\Windows\System32\WinUpdate-NF16A32.exe
		@"\\?\" + Drive.Letter + Bfs.Create("OoXLCXQVVvQJgDJoTewAthlDctZrJvIG32BMaIJ78PuCjj0co0IPMp8WPFWmaKM8", "uTg4bxwClSSLKYdHA6oyhFVhcAVV5POK4kSi9lFkPAg=", "oviPaR8CrNKK4xq2woDDfA=="), //:\Windows\System32\WinUpdate-A0sYHTaMEa2.exe
		@"\\?\" + Drive.Letter + Bfs.Create("7sVPd14pNzKY7wsNuF/Ypn0Bi/l0GT9GS254g8rdO7g=", "hm+yQ1aNMltKkI9GCffFR4At7mIT0pLey35Zf4ogrdo=", "qd2SNmfrJ3R2pwHm0w2NNA=="), //:\Windows\System32\svctrl64.exe
		@"\\?\" + Drive.Letter + Bfs.Create("SSiUZzmWSaEhfja1lesA2kLtgDo1OrUg7ps4xe8nl8zbFv+bn3qERo6BKj9ENFEE", "DVUb+iEOHnOdvxE6Vhim1xBt00d+HQK7M6ngc+xFHqs=", "X60lV6bsN4JIa0eZAYShTQ=="), //:\Windows\system32\SystemSounds.exe
		@"\\?\" + Drive.Letter + Bfs.Create("BcPMD6VeBC28dkyBLP7CIVGFGN4ao8gWvPAd7HlT/0U=", "30l7OuLwEJRLNF4AhUtdJK9EPq2anPTultNRYo1enHQ=", "rlUuMuVB1jjuDFnlTM6o7w=="), //:\Windows\Exploring.exe
		@"\\?\" + Drive.Letter + Bfs.Create("qhFrKFgycGGtNdmnkr1+JrP74JJJTO0ioFuRO1ukPzFbSt3rYcUWGGI5VYCMo0EJ", "rdnf6XzMOBw84uBOWwWpfzTvvNieKgRLOfdPWkhzBsc=", "gbFFJv/zGIhzXPAFyniPfw=="), //:\Windows\Temp\System Security.exe
		@"\\?\" + Drive.Letter + Bfs.Create("wBk9kowfNKhmtyZASgBIR04EYyUJjh9Vc4i1hvlrRYNCNNBXOrDK817r9ovrE18V", "Eo2+qIwb2PmEXQInZwRHYnSh7L64Jj3LUpGE5WKOvMM=", "c49cI++ChXzY4l1sH+zV4g=="), //:\Windows\Temp\Windows Boot Service.exe
		@"\\?\" + Drive.Letter + Bfs.Create("Rx4dkwTNhoEU9T5ipglm+nqdS1DaoXSviDOh2Q2sRWa8cLOXYGc1s+/OHvZq89sb", "OnIDEJ2ozBQYO75W+yuXEQKUW3YYfw18HNcwdSorz2Q=", "hAEOkMvgmTo/+TZb8lnrVA=="), //:\Windows\Temp\Windows Host Service.exe
		@"\\?\" + Drive.Letter + Bfs.Create("lfYTar3koaLkD2Pq4mJqABCDhIJW+ZPMGvkObRGADFY=", "qu75Mb9GnXRevu6w5goiuHFtQFjWcAZ7RFRVr3xN+cE=", "Tq7bgZZzSYh9v2orptgArw=="), //:\Windows\Temp\wms temp.exe
		@"\\?\" + Drive.Letter + Bfs.Create("KSPRiQMQpZ9iMQ/ItdYWc23rAKrqHTin2+bBJr3kEes=", "1PXwjkYQ0mIilgDr0aR8cvMq2TrQzPe/I2J/g2WV7Q8=", "+EefIMb4hQLclmq8AMEMTQ=="), //:\Windows\omadmapi.dll
		@"\\?\" + Drive.Letter + Bfs.Create("fsA2YU/oXstSWkwlW1xX5S9bs+Luo8lvcy8SV6Gb+hv2fQZX1k68XQr4UdYfiXPw", "LpcbPSCQkOxWpQ62zXk+BKmceIOpM6z88DE5K1supmE=", "aFHQLcLNSICNQMPlohVLkw=="), //:\ProgramData\Timeupper\HVPIO.exe
		@"\\?\" + Drive.Letter + Bfs.Create("nJxVWJtwnu891hXn7Zics3wecxFAh8DcLvYA5863E+SY/ocBjuHHV/KIxxnhwoTP", "eoPR3OTQJWz+CK2oJoXFowyX/2Qcq5Jf7dDm24xLLEQ=", "ikaPZ2CsVRJ8bUu+for18w=="), //:\ProgramData\WindowsDefender\windows32.exe
		@"\\?\" + Drive.Letter + Bfs.Create("ci9lwbdZh1bCcSxPXYUwlZDqAY5gIkmNXAxp4uI41gfn5LO5lWEWIowzD6w3coXS+e2m9maszWsIqP0abIc69w==", "048U9p4ixj93T9tfw/vlpKwrGsFDL2ibHZ6948XkuGg=", "CMi+inu7G1VMtFxny4Ha2w=="), //:\ProgramData\WindowsServices\WindowsAutHost.exe
		@"\\?\" + Drive.Letter + Bfs.Create("oxpXnRcmOsRhZe1OoSkgcr0dkm978Fx5KBpOwWQ8t33D60vs2imopO9TIIUcg/d5", "xxaWG0Y/ltI34xb/wlqNmROJZb3D5ALp12UQ39SUhvA=", "HwGubrl1/iPCfBXrDmuTtw=="), //:\ProgramData\WindowsServices\WindowsAutHost
		@"\\?\" + Drive.Letter + Bfs.Create("6b1dAVymRg4FZW504+H+pWi4sXx/HrRQg5/tpVojd2JpV8YAwlvYkhUa1fylx+hy", "llibowKXUTIRoVX+cqMyFaDiUUHPBkd4LDn4Ny5YF54=", "8xldvJRR4q3KwmN3gCOTgw=="), //:\ProgramData\DirectX\graphics\directxutil.exe
		@"\\?\" + Drive.Letter + Bfs.Create("QrEZHpjfwbkA7DNTQobFmZUII8ETtPLuAriPlBPZXUC1J9zSUb28LNEq3ibeXbcIc3UbZn5KB2kWfxJX7roDDg==", "71sJv13xzifaLSgXykih3fQXAbUn5sRqtBdYe8Yq/ME=", "5fiImC/J2GqiHVMqQTSiLA=="), //:\Users\Public\Libraries\directx\dxcache\ddxdiag.exe
		@"\\?\" + Drive.Letter + Bfs.Create("WLxkb9tvy8UKXeAnwHCS/xDSTZoTpexbf/nNq4tzetsJceanpE0iCv2XCmHJRP/CNQPUQn4Lw/Pmh7hObj6QZA==", "F96+4dNM8DRT+ezTbiG4meCCojnKEdNvaVgSLFmwMXs=", "fGZsiHA9WtufwS5ABs9s6g=="), //:\Users\Public\Libraries\AMD\opencl\SppExtFileObj.exe
		@"\\?\" + Drive.Letter + Bfs.Create("PBcM9jW277Ucb0IxBCJpk6SUDQyBkdyXCKi9y2PTiP34DWmB5hfNzD9j2k025mmFhcUKk3hBfwHzcB7uWzctwb0qkEmhWuUaawmSq96ypqfnN6yQfgz9RUrhbpkFVkfV", "Jvfy0lH28mGAdS/87kys/GBNwSS3Qo841fnODAz791k=", "dpEcDUqz5H1Zv9dFSONHZA=="), //:\ProgramData\WindowsService.{D20EA4E1-3957-11D2-A40B-0C5020524153}\UserOOBEBroker.exe
		@"\\?\" + Drive.Letter + Bfs.Create("ZDHKdSQd929ZK/okbfdypon1R0VZ4Z/1nk/+KCGJGjxDQGI2951qlU0cP3O7b2lLUBksOyndnuHaX1Wap18uIzZgOYDo5DNKs3eSU++pN6cHSTxQjN3kQevjdMZh5xvZ", "IYvuD1e8unp1S5LZqgAK6/TIt3o7EjBqvdyBZm2bgU8=", "C27r+fX6ngsjaCV7/+WHnA=="), //:\ProgramData\Microsoft\wbem.{208D2C60-3AEA-1069-A2D7-08002B30309D}\WmiPrvSE.exe
		};


        public List<string> obfStr3 = new List<string>() {
        Drive.Letter + Bfs.Create("sZPMRULoTtv0Vre+2pSVrQ==", "5oV6qa/WSbUVaRfivyZiV8LNtOOlI3rDyxDKTZsCnm8=", "p/a4LeiCo747SfA/HsuGLQ=="), //:\ProgramData
		Drive.Letter + Bfs.Create("HJhHfP3EA4cJM9eVG3oxOp3vFjDwK5dlL0LjZ2t9UCdzrls7ee3ZcM/cirTdslJb", "ZPKLFyyDxbvfgLn2zLOsnvAhx0J2tZoYLamBq4igHig=", "7Wwzdm9JUZBQdXicE2LOWg=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("Wu8xucBO/NtqpJ5wmX/bTMf7Ivj/0dtECfGrq+fZwG82cZWx9EQ9qC/nyZrsg5nk", "8L42WcFwYFdbx1N0FwQe1wuwmhyBJ44kF2sVpt4HnME=", "H9YSPAsbyWyt8aY8HucYsg=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("90r8KVbtcx/XcejkjYP+wv0mtngSDvz3+FLpIn/M9xACuCNO+whPDVkXyVBUlyvz", "TQKusLFHpQBlHWGqKwaxcSxwJDKutXNEPnFFHz6g19w=", "nM2gOblvCbT2PRFNSI7o8A=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("jg9arQJVRraAol1LDu7ffjRfevZyI/PibcxKssTXPx7zFX0e+P4qalG/wXSueRuV", "VIJ4Ze9VAs81lL1QoZ27ThdTjqd6eWA3eM7BdPk4LqY=", "QREtJKkX0Ukz8bxuH7QRRA=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("ZJjWZqij/82Xyi7GvzI36Q9GBDM/Z1ymLbp3evPUuCawm3UpHSM7Fg981+15Rm6e", "MeKuF9tZ0XJu0LelUe/395X6fXUg+NVqL/LoxyRVdtI=", "kj4bwQTcQM6ogXteuC3Wtg=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("6ykIpnStyrL2mlgoCB185rucLtIChcfZu/xQ01SzeJpJt/oXIhXln5fBMCXmcNWn", "xjyH6KYogwNoYTqbPTfH8kv/mJP+0IdADU/CwXKAlXk=", "dnZW337ZwlDq4Pie72/chg=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("AX+Fu8KrS6fgEEkJAhg4bmQiKFrVefSyuBrk0wZMzm+EuuZwoxEKXtdGdkt3By4H", "odvpYIojx/IGYN+IxSpG+7sibMnVAoBF4dNSbex0Nhk=", "+O/2QAgt4A0UMTX8x2TiNg=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("yKKQDQt+QAbRVXHhnRUhdBCg1X9IFv4FkR2n7i4CL7hPjr2KKI3GWYE+0K4zJP6A", "S4xibGRlSt3KuFou98vJ7EZM29ObYrOPjj+KWPEZ8o8=", "PKq3OKvuz0EUFm9Si1RxUw=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("Y4RRk/NOA1xV44tGpsyZfO0UWM6qqbwmvF2i42pyqhKR/tkEBxfs5UEgger+zspi", "Br37kHJG74Hj2fPGehhgUf8JREpuKRj07Lm0xas9SYI=", "W+CHWxkvHgRlE+DSjzg3rg=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("krWJTtGGpfGUB8RjyJJPAHcveoCyCDkjmMrEXZw/4yd0YsFm/dIDlniQPwWNflcL", "xDmwggrMkKc5WmCKhN9GtJF6OeKjtFHRiVa7s3BiBf0=", "mYoBN0UYykZqrJEGxUj3Cg=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("WdKGtQ7+m0sspgzIk8wdzvhDuBP6iB9WByu22CGo1VE=", "qoCIGUxrpp6+ZYc5Ib4gXivZtunC8Fx3fToQsi/QqdA=", "qAR2aJxFSdmZeJIUReAS7A=="), //:\Windows\System32
		Drive.Letter + Bfs.Create("ABlIOrdb46kF2r8vop825o90rpWzuyRjf2d38SUTDyU=", "PqRiRzDiZOnTPXZjgnuyhdBg5u1aCFi13Zu7ISr87qo=", "zrnNHYBfBGhY/M3Ldg6TYg=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("clabgEQgGXV+/+0XML6UtWJzTfj/3WVeHQAv1X6V2A/6nlO8FUQgEn0Og+CsVjQe", "zex6gmwm97AudDcEPGJrwvGPtRYo4zxHhsfF42DMDK4=", "NMP2jTFMHA83fjf0DXdC2Q=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\
		Drive.Letter + Bfs.Create("ucs1pDssRokFiJi4KleKrxletoetQTXdawXNHrRuCoZDdzh9OKfuym3TUMudZe9cbIx/bwKRGD3r1KDDm+fUYw==", "85lyVhm5awswbo2FEcBS6XWx9oWtMjAyb8V/LWUkg70=", "Y43RLLNsdA09GSQZfEDFOg=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
		};

        public List<string> obfStr4 = new List<string>() {
        Drive.Letter + Bfs.Create("NbshGnFTP/agobxbuFj1BKQaDsTlwxDhjQNOMHNlm1I=", "lJMotYBzfHZx7q21NPZRqwO+6mvJi1DZpQhO4jl9qxA=", "aIu6pceaCgxjODQtwM11yQ=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("GFEmNsSl7ymUMXbAf3u2uTNVLFzxDMN6Ge1itALR3CG+KKunrFzdWErGBnOwih3y", "CwXyOSs1yc+65RFCOj70gfSjmC4Wfnd6wln9rEMqoPE=", "udWP9sWVdeaHPyF+I4xzHQ=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("e09MTJ5hmHsGSF+TQdgX6ZZUA8F0z+KYlzSBpNSG72dzR87NzX0fodLJVh8FT3xm", "eST0upDaMvRfMjD0w9mo9xolfz6iSUTHECW7SzM0jSk=", "matJSqqG/T82Ml99e7j+PQ=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("zrah650nRV+cSbkdm6WHvJw3Tuiwoga7yC0HOMoNt+DY5Us3DcWl3c/urJCTGFEj", "1RFqP0/ECYC943NwBTJmy7y5RI7Zlcud1YE8j1yWNAQ=", "iHoL2PuDKcL0Id+xvqYc1w=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("EiaIumUc/0WBTtiJMzQ7rXk7EqhEYIuwsBt3KItHvWfFLaseyskTsL66C9r2Ma7c", "slxfxiPNBUp6ik9FWRNXv8RnLeI9dYdqJzTcOfBR0Ws=", "++/u2cVyMuY7MAi60nlRMw=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("kUGmdpVnG4rXrmx12+xoSvp2AMuP7tqdTvuZiVP/NbOTiemFRm6pDb9CXYOFWzGe", "Zib740wpJrR35iR7iak/LjtNXb3e0nYhCt4xl5IcmOA=", "Lk2PR25O8dmtMAtVr1v3vg=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("McVw6taDnP/97PernY6EUKEJug/HNXKdWwv1bTMMQa9o/gVf7A65cZyRfNjFyMTC", "b6uFDjAFfAwORQf5iv6vTvXBNUE7KTw+2nrdQCp6CkU=", "gqIeED2dZPYtROwoECeZVA=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("HU+bINeSK3ra4nnDbMh75ocj+H2J1KDKNWUnYVelwQHn4VPGIr39TnYFp+XtLLXk", "RlBhPA5ahPrpegVnTHXQxA/eWbVgQl9nKHQYovYQ9J8=", "VgRgxojv1WQeDJ15wsz09Q=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("TNLpboTEjSIbTtkECA12jsCzbQfkooReSwIddspM/O0ZBQ9IfQsZuVh/DA3hkr8u", "z/DjM8sLK4z7N0/XyIch78/PDYJqDk/VPrb9H921VvU=", "ebA2JpgpPk+gNhkqFK/AMQ=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("Qrc+jof3+sSkxWnXkoj34AMSguOrv6dRAlJCqRW/dUZtJck0lx7oJurDvhYPHdfb", "0M7jrj/JGRdp2cO0WNsK2z1rMKIMpDV9Wo8MFTdoJxM=", "SVtF34d+JQsxmybNkkfnvQ=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("+q+omWQTvo4fqXRIa5f8ItJBgbpthDN9kEMUMiwah3sISum20rtIOkDbmR811jpI", "DX/i9GgnnahpQo4FNIKQAZ8rifvePT46YntWu0P3V7s=", "FOLkSBNELEXyHzesfNHOsA=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("N86E02x0S9fwPnJsm592wymqRlWMI/Lqr7pJpRPypuU=", "F96ilDXnWVrIIoUbvtAVOZylK+MiFQ4tUyBVFFxvQr0=", "tFe17OBX0I89Qj2UlQYFnQ=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("JT8dDjwwPKVufd8igm4WGMcBnyOtaeqpLBasyC0vcV5obUyhJXAQ6GIn8FcAxmpVUy1sbwq+XWWfNHj36H+HtA==", "nArOVjAPvT2DUMNjqWdga2bOeKCD+2kesUKWaTfMz3I=", "hwt/x8bxqDRvBkTWCPRzbw=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
		Bfs.Create("7/cfSSDxdE2mgzKEpthT9ig4TNvUTyYog39LhEi9rrs=", "FQRQDtpRf/EFUJIC6NttsG9hdf47k8fUC+S5Ub9DShk=", "3Atxs7/VxryFkS+Hzr6EbQ=="), //AddInProcess.exe
		};

        public List<string> obfStr5 = new List<string>() {
        @"\\?\" + Drive.Letter + Bfs.Create("w14an+rq3vnQyOVB8/fsf1EBl9FlmIL1yn9zojBG2PI=", "6dOHM/3CDnLXs632penCO8Dp/sKZr4XwNJ7kOlhLQLc=", "K/wgevSgpZIcFjlYwb2abQ=="), //:\ProgramData\360safe
		@"\\?\" + Drive.Letter + Bfs.Create("Vgc8zRfEKTDB3ca/lTcDWYoPFWK7lCa3EgH2+FDBrYE=", "IyvdVnU3Q848SjQP6oNjIhzoTncGLe8P9Z6GWseiQw8=", "ogxfNhXXSOEyMbzslwmmSQ=="), //:\ProgramData\AVAST Software
		@"\\?\" + Drive.Letter + Bfs.Create("Hk1G31PrZDjbnCDOODn0bjgq+2XfKbyitf9aktASqrE=", "GWPpFKOgrW3EfMLbmZOVaqHQcRWfnUJWxnSNZURg5dA=", "WvebIjbfL97VW//CfxN7eg=="), //:\ProgramData\Avira
		@"\\?\" + Drive.Letter + Bfs.Create("6mBHzDAlufTfQsBreHQUssMl3hfR7blnUckJu1Aq/9M=", "LWamPS2Er1xULEdsJgIpA3ocXa/2PFXHkY6KKsEW80g=", "PolDOyr720TxEMhMHyNb8A=="), //:\ProgramData\BookManager
		@"\\?\" + Drive.Letter + Bfs.Create("b9wvk3vldw0ychtNc2qTZe7T6xlLV302FXe3Z0Ide9g=", "NfcxTalha6pMiwoVXlq7cTXNtlDYtfHJh8lPlE4wBXQ=", "nHubRuZl0X9iIMsMp9bwkg=="), //:\ProgramData\Doctor Web
		@"\\?\" + Drive.Letter + Bfs.Create("6KBW35yd0kSmYkOeA+r/a9k6AXSmoHKQhWvBx+qqrcY=", "yivnh3oUVQ5NXGX5uzBfzfw/CPgAQrAe28HdlMigfnA=", "U1XAJpBXnMO5Bs4YSnyXgQ=="), //:\ProgramData\ESET
		@"\\?\" + Drive.Letter + Bfs.Create("RPxBi7IuSE1JFiSYxvDnMRAdHPyGhm01l5RlJ+WkDBE=", "s1QUNkAeHL9R+YCpQ3zY3amo7BdYcjKJH9azeRaRqSs=", "+TyXwT+1XSH+kv1lC/Zxig=="), //:\ProgramData\Evernote
		@"\\?\" + Drive.Letter + Bfs.Create("QRM5UFm5rIoTHWK2qlRv0zI7m4OdXVnllaKa3fCwod8=", "G+GQBIOZdjSr4wAoJSnQpHwiB7kg1YWnWePRDTgGHBk=", "TC/3ReGbGZ8REShfUUCTpQ=="), //:\ProgramData\FingerPrint
		@"\\?\" + Drive.Letter + Bfs.Create("p/Yw/yzbZBH/IJ0YUaeWow2Uj7kuksEazw5dsdZ61gw=", "+4HbizSKfOvm8m997cQHNm5wsliSOUD4Q31N65+UOg0=", "4J7my25+WUbKjLXzwwKjDA=="), //:\ProgramData\Kaspersky Lab
		@"\\?\" + Drive.Letter + Bfs.Create("K9Ac/RA7BUHIs1wFS70nxS/eeTwltV93VujoIfl1jpRonP0l7sJvrQfxXgZ8odwM", "V9nsLU2p4JL7mO2Wu+N9GbdILViq4eWMpBISE3lFvXs=", "hnk+e4IT3xC2Ezkf8w0b1A=="), //:\ProgramData\Kaspersky Lab Setup Files
		@"\\?\" + Drive.Letter + Bfs.Create("7AF+8Fr7dUZdsorTXgOZn9CkxKbSvUDKH+neG0ziAZM=", "JSRQJIzP9ju9ceAQdWFAfzORVLMlcvg9QSxtO1S2Sgg=", "1CM/tgzTeSw/2J8dWl/4UQ=="), //:\ProgramData\MB3Install
		@"\\?\" + Drive.Letter + Bfs.Create("lVfBvas3VOMz+tdfY34lKV9UaWI4pFr27yQRKtBEyE4=", "XdRvONXGY0VKNayErNJxZqzXRAUrc+EHl7e+WfNxgrw=", "aYh9nYtHTUasdMcFa49xuw=="), //:\ProgramData\Malwarebytes
		@"\\?\" + Drive.Letter + Bfs.Create("4OLoIe0akaYlevVVui6z4EPS5yaFqpz80eLjJThu9nU=", "xH/Kq3WuWESYDDsZdFcbC+jffrzxOPwonU6ZglxGpJE=", "oIyy9vHB1x4sQyZE6ZdfRg=="), //:\ProgramData\McAfee
		@"\\?\" + Drive.Letter + Bfs.Create("a29FnSebJoTLohi71avIKmhSXOdm/lcrd1167y3P/vQ=", "2j3b9y47H2SplMLBzBDcDZSAEyDgl5LOFYtjOgTmIxM=", "0tsU6ufV/I7A4ZLrHJlBPg=="), //:\ProgramData\Norton
		@"\\?\" + Drive.Letter + Bfs.Create("KrCP+8ZFNbbyGIF854RvxIIZCh/y2XVTmHr2LhMXys4=", "r2CoNAlav2St46b0eoMYXxczFkkgetfqGoYyA+6rCPk=", "wCGo7UxamA1NOViM03+p9Q=="), //:\ProgramData\grizzly
		@"\\?\" + Drive.Letter + Bfs.Create("ohPVI3lq7zQ6dA9r6NiyouJabOrJpkuwkFTn7ydySZs=", "f0O8X++z65x49hzzn2XoiW+rx6/ELVOVC01kYd0U2cU=", "Z8QfLV3xdaXRH/jEEB1bTA=="), //:\Program Files (x86)\360
		@"\\?\" + Drive.Letter + Bfs.Create("0nHTHzgSGwaVnstc1m4WoiX+77jzMQw4qXKdl05Wx8tglEZwTM9LwpGFpjm+/jNM", "NiH1gR5IxoadI2UZgyIWXEBb619O21yw6nsYOFSsRtk=", "s2GVvYtYlgbxFLfublczpg=="), //:\Program Files (x86)\AVAST Software
		@"\\?\" + Drive.Letter + Bfs.Create("2+JGs3823V96e5JTYXI4tT3I/KDU8pz0/kO6uWKD6PI=", "JS1n9mhA9GesTAUOltvdwuUNs6HcUZXtbbpaR+hV+xk=", "DwgRlpdUrlM+kD4oogFZ8A=="), //:\Program Files (x86)\AVG
		@"\\?\" + Drive.Letter + Bfs.Create("+eIZyYaR4Z5ABbX6lT6zQNxjWiYlibdHQ2R2Lm4ATp8p4V+T7phTG+oqKTQKtxk2", "RAnYz8w8gsVkc2okc9JZklhFB7PMYRy9GGeEx0w61Ok=", "Zex1E4n1HpQzU5fueW60rQ=="), //:\Program Files (x86)\Kaspersky Lab
		@"\\?\" + Drive.Letter + Bfs.Create("tiW6PiLwZvjtuYyAnMWAmyGu3vPmk3sasy1aLoXwDdA=", "LKtnO71K6TNShlbngImREH9t0V5anZ14TtyL2Zf2794=", "uDzi2yeapG1FvqQCwa14yQ=="), //:\Program Files (x86)\Cezurity
		@"\\?\" + Drive.Letter + Bfs.Create("GrbLuzoJxZowVFss/4gsxHuSuJ1wNfg5kq3T94/Gp+pL4Btu9IILyrxzp2AkEqNh", "B4j1Cz7MHfkP8PqdicuVNfcoLB2Jd0E6E+3HOmWPTBE=", "a7/l4f2f4QH17nnQaKHMgg=="), //:\Program Files (x86)\GRIZZLY Antivirus
		@"\\?\" + Drive.Letter + Bfs.Create("Nav1yAfJrAho1C73QV0rMy4/D4SHjwzqfdI4U66wff5c12eIbBFtnLul7rBawR+o", "o1NDoU0hokDVS0hNR9EywY999nwqDxH7TLSkOwpiKWg=", "VVXdFIpeDG0b9VUlEWtG6w=="), //:\Program Files (x86)\Panda Security
		@"\\?\" + Drive.Letter + Bfs.Create("Ey7BvDvfFD1UTlMyibjH0m0o1/55ky1fckImZg+urJSJG08sAv+k5vE+syb18cmK", "fSYWTMKv9uz5+2dhZSSRVbs/YyyA9EZL+sJe5Xrql0c=", "iM5w3tpY5LNqZf+Wqumk+w=="), //:\Program Files (x86)\IObit\Advanced SystemCare
		@"\\?\" + Drive.Letter + Bfs.Create("igTf8YQCvpY2IO/74OeAdOSL1QAjjISHCBJD0c5wH0hm9mMTT9FpDzZoi7nqlvIGtk+QLHQAV8m3zZiyIixemQ==", "2qVHpa+vz2Nn7bwQ+WJS0XAn0Gb003Zb/xnTyzHEjSA=", "HCh3Bq9EesY/RM/62EnlBQ=="), //:\Program Files (x86)\IObit\IObit Malware Fighter
		@"\\?\" + Drive.Letter + Bfs.Create("CToK0zevBezX8e4nRkQcdWkNhylLjg5twXiTHHbU388=", "8bcXj1YB1GWl57e/eLJp1fZ9rG+qaU2HuOec+vUHtAM=", "SuPhSuMKAln2Gdiz5qyNRQ=="), //:\Program Files (x86)\IObit
		@"\\?\" + Drive.Letter + Bfs.Create("HlaUvEc/yiqsM19T2pSPqShzuTSjRADUaZYsfdk/OUQ=", "xepbNpMWvFhUkrPk7rzUL1BfCTvIU1KHJrmnqN4czG0=", "NLSrLCPsD666aDpch5TOrA=="), //:\Program Files (x86)\Moo0
		@"\\?\" + Drive.Letter + Bfs.Create("bjhKz3k5sCquvHKDcIoPXZbfQP7iuSsXcJm+wrcDrvwLz3CiyAsfamvLld2TXU0P", "AW0tcun6f33C5uI3AnHtaRi6iudPpvWg9oebpDgSa/8=", "RH+cZkCRtyhTw9qKQlgL6A=="), //:\Program Files (x86)\MSI\MSI Center
		@"\\?\" + Drive.Letter + Bfs.Create("7xGspXDW5M7B3sixuZ0/5jxH57fYCzstA/yBIyH6n5Q=", "H5b5NpwM9lj+7yR3GYenx5YFfy3JTSwV0p+pA3urYL4=", "bKE09b1Ddu5fbHTKHAmOmw=="), //:\Program Files (x86)\SpeedFan
		@"\\?\" + Drive.Letter + Bfs.Create("7sVGPvc5n84HsLi2WvNnbg9fvddQU82un7ac4IUEKac=", "sNgns17Zzu5mjt4EdwAB2sVpbe7hDMzqVAqlAyt7Onc=", "C5saP7Z8LUjcZn8KDYyB9Q=="), //:\Program Files (x86)\GPU Temp
		@"\\?\" + Drive.Letter + Bfs.Create("2tWrjcS35TSK+ApCplTV2cDxKkiLiulwugVHJ19O12w=", "moYH/x3og9qeMIAWmQv6gPx6SXeWDFZuXCUdkDDJcVg=", "k+qkRL+hBtnfmMjTaKVKoQ=="), //:\Program Files (x86)\Wise
		@"\\?\" + Drive.Letter + Bfs.Create("61CUN3SPGpMID71FrKeQAeKu8e7wuWFroy+eUfSkf/4=", "6XIigyM+c3z46T659RZpnG6Ks2FMBLiF/7KP3x4T+DA=", "czb9UWulH9tvSBJUjuuQcg=="), //:\Program Files\AVAST Software
		@"\\?\" + Drive.Letter + Bfs.Create("bfZgQqF/k3sV+dQ1jZsUr0lS/WwS8hNlD62kkVJHOEg=", "8BdorpsxtSRN2dzF53WfVEgGriU8BqRket+V5pWPESw=", "Sybf8fOWmec7Eam/61Br9g=="), //:\Program Files\AVG
		@"\\?\" + Drive.Letter + Bfs.Create("TjmEVPgSjbsWuBNUjy8BpFdifkVcnS85lLdbUvNRV+ZJTkCZVHkKJyUKvwa0U/Mj", "c/Fcbk+E2UXvlKWLCB/EIbGDpe5pTHrzSz3+8zhgIuI=", "Gjgwdk727u9Py4flArzK7w=="), //:\Program Files\Bitdefender Agent
		@"\\?\" + Drive.Letter + Bfs.Create("Cs7/DffRgcjWcBzQwNegfTjRsEkKfPEZ0t3o+4FB3cM=", "BjFmxL9EG0OUAIOx+LW7uLvHYMW8Saag8FW+aUZKymY=", "mkNp/d/RAFbxV3MkXWJI/A=="), //:\Program Files\ByteFence
		@"\\?\" + Drive.Letter + Bfs.Create("G6xLih1lV620fMplQTUFSYWTiN6a6qGwq3d6u+WcJ0o=", "+1sFxtzjaTLxVmGoEjwwlpc5WwmR9IggV27v5b+RjGc=", "iVIlnq6sBe4nBwbl4coaAw=="), //:\Program Files\CPUID\HWMonitor
		@"\\?\" + Drive.Letter + Bfs.Create("ggMOgcI9hAJbSEbfT0/4WAirMA+VIczAyq3/CSwjcgA=", "/15GFd4ZH72MIkxzejFruLUDwZDvexLCOreXoHDwMgU=", "Ij3MN2X8bJ8Z8xOqldSajg=="), //:\Program Files\COMODO
		@"\\?\" + Drive.Letter + Bfs.Create("4mK9sO+pqAT6/53Fz7Ibj3o9f9Nf/o2/yV8XjWoUyaY=", "Aa+4xUM7+GZiPjYjcqJCQ9a895G1iFwDDkK8Ky2DCa8=", "RCthxjRfSWHSGhBcYi+5MQ=="), //:\Program Files\Cezurity
		@"\\?\" + Drive.Letter + Bfs.Create("JI2BxDOyDc4I9gopsUhs8w40tTSW9lCHSe+wkl1sCQg=", "vJFboHmktEVXO6BV7ZGzLbCutb5A06l8ELgJqX/N5rE=", "pIWAsWEJn+yOdVu2E4eaCQ=="), //:\Program Files\Common Files\AV
		@"\\?\" + Drive.Letter + Bfs.Create("Xa4L+fyYQwCVTj/FQTvWky+3W0ITPpIQUlJnBCRGAwW8tK3kGb6rwO3+RnRs/p2v", "XKx45KL7WEweztDbcBi+Qp78U9yKp6oHlXfRg8Jftsg=", "FckA9dxee6XjYwKtdaQiBQ=="), //:\Program Files\Common Files\Doctor Web
		@"\\?\" + Drive.Letter + Bfs.Create("d47V+ozwvCHQ7knv9KUVxTDj+QznV15vQqcnK9rrbvJ+FIU21h0BJzKElbv+5tHS", "cUN/aHLtAEBa/aMXVD1yn3qQFug0wa8Jow8BuUtO7CE=", "iv9Ox/AvonV06lQFcwimxQ=="), //:\Program Files\Common Files\McAfee
		@"\\?\" + Drive.Letter + Bfs.Create("y4TqEn4Eb4rkVHb6FFjWV2yO0ZqjBCtGspV9AgcqzFw=", "MPUyG3mo8/Id1nhGsg4VkRZaOm5bpz7YK/omATfv/g4=", "+xP4HUTfZWjnqUcOi+nUDw=="), //:\Program Files\DrWeb
		@"\\?\" + Drive.Letter + Bfs.Create("2X6VYnLQdhotKhtEny6C4ijfTAxzPRgsDNYBwhlEJpo=", "ceh8nqXojqx8H8hu8aVScdVA0Ke2UP7T5qara47Jgxk=", "FylHOU7exJhW9z1ct3lNxw=="), //:\Program Files\ESET
		@"\\?\" + Drive.Letter + Bfs.Create("5Ps20+AjxHlqPaTltDGiy8Nafn4YfAyFFWOZCR2WFY4=", "70YiUUOQrvIcDuHwUbPGhopxcrxDQT/MBeBN/JUWofI=", "GiHSbApVo1jti1zHbbvdDw=="), //:\Program Files\Kaspersky Lab
		@"\\?\" + Drive.Letter + Bfs.Create("hFioEV3V3Zc0wkZoOnStWiwOT1EdV+vaQRZg//OwFDJo3lRoNau3DPm5mKAUo8Tf", "KtQVrXIXufnpw4sG43jAWHar+cNdJ3FboNxrVjzo3vY=", "N6sVO2cdXJM6aq6Ib5Mrwg=="), //:\Program Files\Loaris Trojan Remover
		@"\\?\" + Drive.Letter + Bfs.Create("qm3x92Xp0On44lU0W7CMcg2C5kCJbOkKWzc+24eaVD4=", "l8o1hUBxRBvJl23B5pggsHQRqCwe2tlBT/yhWQY8p7E=", "kUclSDb4Ec8kLvwZDYwVkQ=="), //:\Program Files\Malwarebytes
		@"\\?\" + Drive.Letter + Bfs.Create("sQMGVcjZkJE7UHAeQGIRy76jr7TfblcqQ0y0kvW6u4k=", "y2BxKPHc5mdFFamTQzpL3XcDU1QbhiJXmj2erQ+tB64=", "3GBVVJpwLBrFQJ9GJTFUVw=="), //:\Program Files\Process Lasso
		@"\\?\" + Drive.Letter + Bfs.Create("3i/gw2LLlqp842IpvtmfJXwHuDf1RChYiq6ycezEGBE=", "KuX8tOsihAWwtvzrq8h+ahO6x8T+oXE4rBw9/5kYVIw=", "raG09X3vflgpgy7FdKA+0w=="), //:\Program Files\Rainmeter
		@"\\?\" + Drive.Letter + Bfs.Create("+5yXizOc8DkZ41VURyUugX9qdkLkITuV84yoCaZ+gOI=", "TKRjrqkL/k6cds94NeQHBwVOJ3oH8hTgqH8df2ZimZA=", "teiCHpJW45tmWF5BUYfCKA=="), //:\Program Files\Ravantivirus
		@"\\?\" + Drive.Letter + Bfs.Create("VOdxjAYXnnGJ/+idzQqZcmH3Cv/mWZgTalvFVnzXieFTZPBZQ8DUYris6Zv9s8h3", "SS1DRAZ2a5gP5/ARmXUMiL6iRJkuPrwRpZeYtzCNL/w=", "IoOSBwzBxAp+AD2SWqS/9g=="), //:\Program Files\Process Hacker 2
		@"\\?\" + Drive.Letter + Bfs.Create("/CzIZrShymsOb6ktnsf6ldEOJGY4r2O3ywrnoC2+hZg=", "u3hiwP03WsCDX2CbcFxWdwp0XcJ6rmLTKrfPuQ4S+XM=", "Lw2iVD1O2P4l9e29JUFIKg=="), //:\Program Files\RogueKiller
		@"\\?\" + Drive.Letter + Bfs.Create("S2/v04pRfmTrmTfdlmoVj0QqjojjY06Oyx5zH/o8N1UNNKhDoD6EEoGlIi9czmuu", "4iU4900L6B8CCyLc/pm8DPIczAB3lXnJ5TwYeKPAn2A=", "pWA6ErPvOtO69F4/juik9w=="), //:\Program Files\SUPERAntiSpyware
		@"\\?\" + Drive.Letter + Bfs.Create("bnQP9+75ZoFuk7JyfSxHs4LBg+r/VI4aBURplTOlALk=", "zRenQtoPvvlcl4UeRJiqh2Y3JhSuVN7B1ioPBtuP3Bs=", "Xg/C3rLiVhKXCI6hI5ih1Q=="), //:\Program Files\Transmission
		@"\\?\" + Drive.Letter + Bfs.Create("whjil6R7tK0jqjZfc/CZ5iIGqdPPoXkSl5098VoL6w8=", "mDYCLvkCFYmLYwW72OxWLqXXQ4zzkqdZsGuC97b4jw0=", "gUX774HzD3UXZpMzSUaE0g=="), //:\Program Files\HitmanPro
		@"\\?\" + Drive.Letter + Bfs.Create("hKBJYLDwgIi27AbL6TBYipfCsrGjFpKv6lOuqG6oPFk=", "DvV/yc3wlNLWpL7hRM5v0kgQu4oFdYByQeFjdrRg73g=", "Gzx9oY3/i8Yuy6KJTet07Q=="), //:\Program Files\QuickCPU
		@"\\?\" + Drive.Letter + Bfs.Create("p8fr8TzLh1a8lwI6GaEgRQ/htALiUev4H5WQlTPazis=", "FR6PS75bOKGdlOoeZLRtkZuE9VExSwbT5i/vB+98G5w=", "OaNk2entTf4Byu007CFg0A=="), //:\Program Files\NETGATE
		@"\\?\" + Drive.Letter + Bfs.Create("QNoVJfxzRqexrcL/qfBn0no/XcVeboyZN3AV61Dujn8=", "f+/NvScZVSsmDwnJepcOJ+MUn/Xds9vibN6x55n9FtM=", "dEs/QZu/k8N/vVvAW/XEaA=="), //:\Program Files\Google\Chrome
		@"\\?\" + Drive.Letter + Bfs.Create("gHIi95NmeomzLEtWS5sWG4q+Z8C0ovueLtNWswaMAlI=", "6G+/qfp7Qa9qY2NG+mxxrn506OcGv2POhQocGRalzJs=", "8F2c9Ix/7iydh62uvAcgDQ=="), //:\Program Files\ReasonLabs
		@"\\?\" + Drive.Letter + Bfs.Create("JRVZWt7FDO+z7ILs/dqYJw==", "o+PbwKLQ4h//eb/HN1aRJ77+jjLV7hUUjkuyyX+ob7g=", "xbUDTQHfZleZ4WKsfV0V3g=="), //:\AdwCleaner
		@"\\?\" + Drive.Letter + Bfs.Create("l9UtWZ0dtW2KUjCNdIFBfg==", "UZUZ/bH2MrwcG3wEvVx/gNdybZDQ7pmombecAL1GDC8=", "OAB5M41yIe+nHdZjL7lhJQ=="), //:\KVRT_Data
		@"\\?\" + Drive.Letter + Bfs.Create("VjjvJy8751ZhOyk6OeC5DA==", "KatLMJbSrdjvPKMkgwijTaHx54ksitqz1vLBuJjmRaw=", "DiDzv24eo87rQoOKXvWE5g=="), //:\KVRT2020_Data
		@"\\?\" + Drive.Letter + Bfs.Create("oca4Iv4UT61P29PzCcGTCA==", "37m/KCiiXFc0xSEgmXqdzlAKIZebsncVNiPjh7rM96M=", "P98cV2YvXeRi+V6xkpiI2g=="), //:\FRST
		};

        public List<string> obfStr6 = new List<string>() {
        @"\\?\" + Drive.Letter + Bfs.Create("53R2Sjy6IP/7cyqcNR0aag==", "GqwrtNxrpURV40+2ETZ9D1eNlzC6yyw3FL8LD9Pep/c=", "jcnGKcTQPXQfzvbdXErQxw=="), //:\ProgramData
		@"\\?\" + Drive.Letter + Bfs.Create("amZHHJ1kYsAel5vzio5PTQ==", "rXEoKgOz6fu0JVkPgircNh7dMAwRrh+uKejR9pt0gFM=", "V93o7sg6iWi4gEwhYAaHuQ=="), //:\Program Files
		@"\\?\" + Drive.Letter + Bfs.Create("G5j8YqrO7TdlqKgSQdjfXCFfKKd5qnNnQ7QBT8QqiCI=", "bmYV7tWSd9iCHFy9JtxMMFLTvLol8h+Iv6+3NoMpdb8=", "uauVjSvv94xBRRpltjibTQ=="), //:\Program Files (x86)
		@"\\?\" + Drive.Letter + Bfs.Create("K6Z/lzPnsl/PbsGzk18FgQ==", "8LBHnRifSkzJZ0Ex6SsGHHRmB2sXItw4iSns6qz0rGg=", "qlDPDpNo6Yjg1hvSfKFa6A=="), //:\Windows
		@"\\?\" + Drive.Letter + Bfs.Create("TdYJqJySYeJi/6HK+fiH4Q==", "PqtWaFwWY2Cva7ABeWnj7MiAFvEbVUH9ysuyIs9bJME=", "pDAfFhalQgVLSK0X1BFElg=="), //:\Users
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

        public List<string> JohnPatterns = new List<string>() {
        Bfs.Create("6G6b4czn85AO6Fny0oqiSA==", "8R5rVFJaibwvbhh/QYuG35IcvKULZsUw2dlfQfS44Y4=", "ZEYIbm/EDWdvuXRRhyoyuQ=="), //winserv
		Bfs.Create("RRF18uxcVva2CdUT4VxPqA==", "al08K7Qidyp6p62fJ1RqzCD0gs+6RZMPEjp1FXMzs14=", "JQ9BAFyN3f/+4D54nkOAXA=="), //audiodg
		Bfs.Create("/tiOkA059EwZXZojBfZlZA==", "cqJDWwc3uKMsmpY9J3T39hqFlxEuwJykVVSYHY7QC0Q=", "KaBwJXtWt8ttPM0q84Csmg=="), //MicrosoftHost
		Bfs.Create("T8azJigJb8vasStIbAmHHQ==", "C507xiHez23aL8ObzEQZzlmLfVcYHiwpzJoI8c01+58=", "dZS7VY8X0oma4UqhJd+QcA=="), //ReaItekHD
		Bfs.Create("VEYArjQkkotAFcwbEfUfYA==", "6zaKAZjZOtCdKlpigjZin71DN6Z92svYiFf0NqMjkMs=", "DfTEyaj9t0UtL6oyZ4CREw=="), //RealtekHD
		Bfs.Create("HkKHK+64cwu5dNP+hvoVFw==", "9MR4JgVsOXzBdzjVg3Nu6RS4bx1LC7Hvr0aTTHv5woU=", "+20etMF96Tg46hLQCmcPfw=="), //WindowsTask
		Bfs.Create("NhbT0VQpoh4eeL5C+8nw3CNk70Z8YK+sjJkoC37/0v0=", "WjYQc/YaKPZpiSqtS34rHR8VnKxNSn6Q+XHfxesumIw=", "/Ikie9kohbe99Hkfpv0CWw=="), //Windows Tasks Service
		Bfs.Create("djnpExRERa1K/X0ZrItbBg==", "ed6q2mLHeM1FmuKhym9YXP2yKVlv/vScThZ7cU2fOtM=", "c6D9VM8C8Emwr64Xuk/YvQ=="), //RDPWinst
		};

        public string[] shellPatterns = new string[] {
        Bfs.Create("RySsdo5ff5767k+aNu32jg==", "hUqyij1C8J3az3Pk2CHrt1K9+6ZOh7+h56N/y0t6cdU=", "d0RZSbxxxI7tgzmIJADAFA=="), //cmd
		Bfs.Create("q386ouaKOuo+etkZ4aIQFw==", "A3vVFdTBGufMIXXM0bTbSeWBRmZSWbv1VUYDasHJaKE=", "J7h2epHVlOVnsJf9Et3E8g=="), //cmd.exe
		Bfs.Create("4008HZrXK+aNy4cP2G+Qaw==", "Hedhdn7ZBPZVuypnpVThfWb2NQDjBLBS1o0bsv7vE2g=", "P7CSuRPSNFlxNOtHvxowhg=="), //powershell
		Bfs.Create("uTZaszL35HforTMGBV6LtQ==", "niX2Mo7MFrT4S6pIs1PgepZN2WCTZ4iC0iK3L8SN+eg=", "qF6VflO39GczStp2Dn6aog=="), //powershell.exe
		Bfs.Create("cyLv98Ny19/8qT1hc5Bukg==", "tsAjS0H0JRSFuTCbPVjz1FdbSgrQPnpglsI8NRh2FSY=", "WhInqgVRe+VByLHEeNNPXA=="), //mshta
		Bfs.Create("J39ITOFAeTuO9NWdipQM7w==", "7o5U9TxfZ9Oo7pTFvyThVMd2UvlIX6pwjxZot6adYgA=", "pdi6eu9QA87Cvta+1zCNzw=="), //mshta.exe
		Bfs.Create("7Qf8jM86NMCgqxWy5DomkA==", "oTM21SMa5+SEgNEqHXf6IljWbdDfr40Xn+7UPHgv+CY=", "D1pSWEgQpyK6w3oA9mn4yw=="), //wscript
		Bfs.Create("QUqb5RGBt8tEIbw5e7yGNg==", "XLq33j3Wt3dXyBAIX0XaT8iMxS3oRmJwETdotj+Kyrw=", "k1m3B4LIZZUGLWt+xjFTlg=="), //wscript.exe
		Bfs.Create("FvU0D/bJGLYFY4/6mjev4A==", "eBBTnsQj8t5imNPaxIbbj/IFxhgrrEIipwza8zK8x4E=", "Q14C27K2Q4cTa4/Cc6WopA=="), //cscript
		Bfs.Create("NDmVe7aTAklEbJRZzan4YQ==", "v/ky1Peb1hmKjCyYGNFUvoMwHYPv0oxPIIey4IW+ReE=", "CeJ4xr0/ersTAg3kKNDGgg=="), //cscript.exe
		Bfs.Create("bMSKaEviumvahZdGdABslw==", "iFi5NCkY6r00063Jgd+Z9zFsXZqzUy1NVlnvrHVlA8c=", "ZyXTNp3qHsevKhTOIvN+Sw=="), //conhost
		Bfs.Create("YCkCqps+Bh2J68Rs8axvoQ==", "n60iKoOBX5ytLnlcIvuNRRIDHTgpaishJJ45IfQ4czQ=", "PBraw2ByN3h3YMx/sS4agQ=="), //conhost.exe
		};


        public Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            ["TcpipParameters"] = Bfs.Create("ytHDg1QbUyHMb86WHSB3V0Ff227/VJ22h8hvgElLaHIJ8+D9RbAmCXLFT1FHXrS8jQFeI2wANbM1x+t33CxP1g==", "orhB+MIh1tGK/Y88M45FT6zBHJsNyulY13LczFNvV2o=", "YP70uZUbumeVZ3xBZglkDg=="), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
            ["SystemPolicies"] = Bfs.Create("oESTMdgUoa3bDzYKiP6GEBdehIiimJKVQP7ek4p0clVYrhI9S9/MtG0nLhNrA/VPIREdCSEHT+D7qGm7BXPYgQ==", "jMsgxeiRF+5DkpRqTr8d2vbR86ZDk0fKH7v5mNZzl28=", "18A5xKcPxJhBZxf3qfkxVA=="), //Software\Microsoft\Windows\CurrentVersion\Policies\System
            ["ExplorerPolicies"] = Bfs.Create("HzgwuSfjZXZuHxjFHO9F7SOwubQXOHw6SqR5v8IJ1iX2RAd09uh0FnO0Uf284lxYhZQh+mMz+Cne0msLyDM6dA==", "0mQqVqHRuhvUGtlwco7hQIA9mkJp5zz3/kk3fgqQ0oA=", "BmJmt6FBjDzAGj1sKa9klA=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
            ["ExplorerDisallowRun"] = Bfs.Create("ev26pwGaB4r1iuWjRdHv/6UDym66R0SuIVmdfaGlAZt/U3zcHp8824RvxWsZZkRpejX2+vhiLsAW4XKyjGZp0iYj4j5ajQbxiqA5ZvJHfmY=", "ttCO2H6pnDoy1cb3KQjasfs1/g9jhHcHo1FvsKaCotU=", "k/B5DOxScWYNBjsO2h6oWA=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
            ["WindowsNT_CurrentVersion_Windows"] = Bfs.Create("y+eiJDZMBrYtxmQiPxWhJCxrGtA6R4O7X6TjrOgwF8aWtFCMsupV0pqyzHO6qs3tMcBeU1GpgDx8fTSZpC/hsw==", "KFiiW01u1V5aoAnE94I022F3f1NSjINhvRjyPfo+FTk=", "fTmIF2/HDF4TVtuAkUTgxA=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
            ["WindowsNT_CurrentVersion_Winlogon"] = Bfs.Create("Hj9rKVsV0p0RNWG0oH3c8p68nGCPi21bO2mgP4yk4pYwJ6KRblKLwQ/1IXErpedESn2We91VcRPzUtMl07wRhQ==", "0Bjn+WXrGTP/MGLWVsk4hXBAtnjZnB0SbSVwU6TWZ8Q=", "iVyGFsOv8qbNAPiKJVnqpQ=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon
            ["StartupRun"] = Bfs.Create("VkAZXhNpDaSQYunTXAP06PvhVaNN13fcCcM2QuC0400At+NIoJeRyMYUajPPzAja", "PYuDGVKLcBQ7HkOxTCK1V9V4i9KDak1TQoQzWOJK15Y=", "fE2CmM9j2hYIO4dX2ECvQQ=="), //Software\Microsoft\Windows\CurrentVersion\Run
            ["WDExclusionsPolicies"] = Bfs.Create("jw207AYJ1AIyl1Gl1MoEC+i234PHIXFfGz6BTvHoMslOG9M5nLz/B2xSG2/EIxv9TNy7wdamNV/DRN7Ex6arGw==", "/eSaR0SQX+iTF7XKq/QBKEks63MhsIA5WDyFk3IVSpc=", "K691oZjFNL/QnXpFHiQ+UA=="), //Software\Policies\Microsoft\Windows Defender\Exclusions
            ["WDExclusionsLocal"] = Bfs.Create("z/qlEghv+Oilg62sLRP3cuk4M7H0OJ8AduIt5nRD/bLbWGDbZiNv7R3b+33RIN8n", "SUaYEuStNQfygR68aV3IdP34p93J3hkmAZ2tdEpf0N4=", "tFOguEE5ZCKrwjVozlzvbg=="), //Software\Microsoft\Windows Defender\Exclusions
            ["Wow6432Node_StartupRun"] = Bfs.Create("/Zmm8EnxIQF/p2KhFAq2OLRRGCZdYuZByL/FEVbwWoOvEZRsyuEYoVfGP56EL9fm9hAhT2MovOyOv2/u6WSNLg==", "gZ/aXbPfWrT5haMJiLdDqqG/CjGJi32mvldNGueoUk4=", "ilReVFMp6vOaxo8QPxLVOg=="), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
            ["PowerShellPath"] = Drive.Letter + Bfs.Create("IQe3UNy8dckbYwg60MsQCSML4WC38PbWP/YIctXHk4Wpp0kvPrKfBfsqMm4/w7kr", "HOBaM10qALm8/l7U/1I9ZMMYNCfK+WqH60ixxv9M5YY=", "h8xe8F5Zbg3z8xjYRdRRKw=="), //:\Windows\System32\WindowsPowerShell\v1.0
            ["Defender_AddExclusionPath"] = Bfs.Create("aXr9nztMKKAL1rxZxqjnJa7CgqbylOLscfOSF8gG0ck=", "HAkONb3xDZ8DyVQwvBiYPKXhGSFn56CWgp5fyUlP0nM=", "IXvv+IhCzeheWW0LOnDuEw=="), //Add-MpPreference -ExclusionPath
            ["TermServiceParameters"] = Bfs.Create("jEIqkP4fxdM2IgvTfK17z+QSJwPStowPri27hBh0CKqOEemCfRnMpZ9c8W473mXDkBYYdAdUYy9/T4zIffNZWA==", "DFhZjuEJvNb/7E2QhzUOEtrRPeeGLVEzcl6a9JI7nvk=", "ZyMHUCMYQgQLLCF4TT92Mw=="), //SYSTEM\CurrentControlSet\Services\TermService\Parameters
            ["TermsrvDll"] = Bfs.Create("gwrGM4YqFwIIvcBXfBPrEH7wwV9x+eA0bE94GB+akuEGVVJAYTmFlVm9I3znT4Jy", "RcZkMRHzfD3fMTyCz5MtaIQHoX6kBxlkFv5Lrf35FRM=", "vYzHBrzHYoyhQiwmuNKO/Q=="), //%SystemRoot%\System32\termsrv.dll
            ["IFEO"] = Bfs.Create("Grgc3Bc8NVcpHGS5xb7YTTxsabhL4fbFUmt6r8bnLO4T658WO/SfOa1DwJKtYJHo5N3Lywdjl/64DfvsnaH1UlVuUv4CEkE6tyGQ7uijiVA=", "cka0HDdwabU8tgpw1eNtTPlARO5rI9NY2HiNaCWfudM=", "jUMUHXF/nvmQM2Oa1OWzTg=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
            ["Wow6432Node_IFEO"] = Bfs.Create("dJAP+r34ZAbOYN4py+v2tmGZbK6a6lwsuSORgi/kpuXFdVQB3eLUuzM6RvTPiXT/euS/2sgm46waD5cDnEK9uo0YRLYU2rNQpw4I2SU8VrF7vsRI87/huHUqC7NgmA47", "UmTJrX+moLQtafzF78qONw29pGTJLwJeMOXeLFEkO78=", "2qDW9kM2kzGz1Tc3PsNqHQ=="), //SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
            ["SilentProcessExit"] = Bfs.Create("wflLj4cojCpLL5DpXF2WPM4eTcTNf4cZUryspyGkGNLCYOqke1puxqHArlLYfuvACnpQWBTdgf9OpPX4LZCBgg==", "Q9U+sGI8a1Ch0GuR64vx+BoXott2jMLhJm8ehMXAPvs=", "4cnynnEHRzhCMZFAFesW2g=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\SilentProcessExit
            ["h0sts"] = Bfs.Create("82AfztXVtn8T0XYZZm5ZeVgOZWh9jk4UmUjaOFf/B/4EoSJqMf5rYoyehTezcFCx", "QooJ53+am01bYpmHy6n+VMpJLx+3Hy7w60QnFJXffmk=", "GPvcBf9YRznwfduYDws7Zw=="), //:\Windows\System32\drivers\etc\hosts
            ["appl0cker"] = Bfs.Create("LD/1pyaD2dMktqoqrIV+XF04jHY5acCDA3UFSo+xDZF/vgGsFlPZm7ZhH/npIq7+", "JBwxG2ZLNaN1b//3egN+HwODPMs+MlSWa4Grpajex8U=", "EkPMik23pz/UzztAyu0MGg=="), //SOFTWARE\Policies\Microsoft\Windows\SrpV2\Exe
            ["Tekt0nitParameters"] = Bfs.Create("D7Boxj1LlNd/au3ONhiJLZuK4pUDZCNSiT0O8Rm95u3E95K3e5CUs2XgmnvEiLzjGVG9zaOdKwPrHmbfzfP0qg==", "2pTnr77kjLm4lnZ2o/DQY1q6vP9/4NnuXuSRbAfNpLI=", "SSVVtiz5gie4V3uRejinZA=="), //Software\tektonit\Remote Manipulator System\Host\Parameters
            ["disableSmb1Script"] = Drive.Letter + Bfs.Create("Slmp2l1rASfTyTvibIInYfRniP6nWpfZTw2b7FY+oBu9CUx1aaQnQjyPqHZ6n8fTlgfUDnWf76/yGX3hNn/GtbUWYsCefrrkXdQDOMH2ejw=", "SrCLxI9J4EmbIWGC44yxMOUp8nNkdv+oLtD5iJ3sxLE=", "fWEUsPOB4KfWtRd5c/XcyA=="), //%windir%\system32\WindowsPowerShell\v1.0\Modules\SmbShare\DisableUnusedSmb1.ps1
            ["LsaAuthenticationPackages"] = Bfs.Create("PHcLU26zmHTWZunWfUaJGrJzc7r9f9ThKgbsgy/i2Q9BhXq6AUME8Dzy2UjQkXcF", "lZz0M02eomLYGwgtD34PejBJNYzS/L6oIyhSkn5jyeg=", "KiIJo3MERHjMe7RRtP39tQ=="), //SYSTEM\CurrentControlSet\Control\Lsa
            ["AppPaths"] = Bfs.Create("MUYSHXrp7fKvj8E7ytB/VSNIhQnHF6/s5HtTeFdX5cUAAPtntsqOLYVY61T/9vlIVwzZTiaPtdwPaUmFIAon/w==", "kRGB3NzotSOmDyuqWzCMLhFYggRkjoL3KL+0X/9gzlg=", "T3/T2rZokrncGJjJhuUN0g=="), //SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths
        };


        public Dictionary<string, string> conhostPatterns = new Dictionary<string, string>()
        {
            ["convert-from"] = "[convert]::frombase64string",
            ["invoke-pattern"] = ".invoke()",
            ["policy-bp"] = "-ep bypass -w h",
        };

        public string[] SysFileName = new string[] {
        Bfs.Create("tvkRRqymL4wqdsOE12LBTg==", "lf4UlJYDpXtghy1X0CXROwfcP0Y0Y0WFh9jBkskxg2M=", "8uZmZQV+eAB0Giov8RQhyQ=="), //audiodg
		Bfs.Create("P4aFrJnU5tSY+Ii+fPy0FQ==", "u0+8FhkEVxye03MFS38BoBQp4v/UgY0o8ezbO7qS9mM=", "HdoGgCEqmIrC4Dpi+yRD5Q=="), //taskhostw
		Bfs.Create("lvgJDw3goHHfTk+J+A9HhA==", "0+hH9wiqOXzYJOZyvH4lzARP80ff0OFUqHZTpeEcI34=", "/hD1CVfy69HpS3tE+abeWg=="), //taskhost
		Bfs.Create("SlwyVW/k+2U8g8XQnWa+1A==", "5Jt1mE4Y8wksbUq3rg3XWk8DHnLtZwQnMyBX7IliIAo=", "1T65ZhPxeW7G1IWShpXBRA=="), //conhost
		Bfs.Create("DQRq+sol6tzUuA+/b890hg==", "xDTWohwkSfNeAx1CfK9H9Dt49jSgrcvOTVaApayc2Zc=", "S7VRCTkkuPOfTclbahUVSQ=="), //svchost
		Bfs.Create("36vobpgAxEmvXkXdPQUb/A==", "Yyaiqduw0xUyuRUYqp99cXy0TBFnZz3YFXm2GPkt3js=", "mK8X+qy4+9QvWLcoGLoA2A=="), //dwm
		Bfs.Create("TZ5oKXojUCc8Tnkn6F/tnA==", "t8rCNVLZtY+QSBj3bz5b6XxJ/dFse3lkDb6s7gW3kpE=", "4LNYL2JZlqplxnxT64Tgyg=="), //rundll32
		Bfs.Create("hFMvZ+EAp3Hoh2Cssy1PPQ==", "f//lw7qayeFHFakGm9EGOeqsYc3qVqtGtmcbnswOTKI=", "/ftx0VpyymfG0vgZaklpmA=="), //winlogon
		Bfs.Create("3J4AYB7//rWlSKGSWoI6Yg==", "5DEWRXOvJijKdH6ggImIBoYEOW5GlMa5p+b9r7Ynvt4=", "Tf0yH0N3uIPjr0eUHuiZWA=="), //csrss
		Bfs.Create("dduNDcKjYzc1MqwH5HMB4w==", "hNf6TtybMhyd9DaOEAThT+ZahiHCP+J+bv70AAsUQtE=", "eLJkWxjAjOEShiN0/fip9g=="), //services
		Bfs.Create("bAeFzqBitMmkKW4xyMozWw==", "+w5zB4GM+/1v0f4ClNt4jp2ACLfpg/jGsh9jNe0NOOM=", "85ZFup1NhBnNaWeSVTRxSw=="), //lsass
		Bfs.Create("qE2JPeizWjdVL4omvb3XBQ==", "M6lbJ7XLbPodzz2PMcGondYf7NwTVVQGNTshMpF+G28=", "qMCIC6/M7gLfUyBNy+fKzA=="), //dllhost
		Bfs.Create("pYtAoYNnURM228V3fJ7Zdw==", "pecx2He6E11//IsfSaf/Bnjf04yJHDuN7oCfthqtp2k=", "3NvDCpNK6zWRB+D8rXRp2g=="), //smss
		Bfs.Create("UDN5dvAYhXFrk0XKsfB17g==", "IXo/H0qXaJjjQBydlAhTM3DxRuZwqE/msfLSZChHkGM=", "LO/i4tmoHpi041E35Ews3w=="), //wininit
		Bfs.Create("H9A1ZFSGnSP7plJMEsUM8A==", "08g3IzzkqtGOG8jf+qAUmjEVDnY7lrzDWQfsdSP0S60=", "J/ae8Jn+m7XvqoIwiD7W4A=="), //vbc
		Bfs.Create("4CeZiKLY6Tdo4Q93ziI/Hw==", "Sr6mLC9A2kLGOp0sEe8HVh7Ekh2NTsBaRXaDjjTDUJc=", "HBwLBSzoxJuZsaSTYY94/g=="), //unsecapp
		Bfs.Create("BZdGKdPq5wzSi14Cr00QjA==", "61b6BCZnq6HFED+2is3iBGqi608MhSjgiPlUFVmOmes=", "7GeFEuDjnzy8TGLqoOTX2Q=="), //ngen
		Bfs.Create("xP77ZllPFdjF98W6jZe/3Q==", "H5Y3/6F6xVPB1s42+rn5kTFQdpZNOW0zrHA/kk7rDGE=", "3sw+6LPHtQDRQ8dsgkFRtg=="), //dialer
		Bfs.Create("arRC2lOO4q1kTgI0Uz9zqw==", "YTQpa9/igCE4PhRI15dSUwPNI0pO3nILyIUTvtRguew=", "TAcneKVSmO9hbpUc++5OQA=="), //tcpsvcs
		Bfs.Create("eep2PuJA47aZkb8gHpd3bw==", "Zgo3qbqPtOBsHPfdx1EmtBXMuniO7AMnW6WWyrpdlIA=", "Farh+YiepuYhb2SqskZYSw=="), //print
		Bfs.Create("y8o/Y9VwExu6gOOFZH2hpg==", "YpG7oshsM8hkup2R+qER5y6FH4wzia0ab9hCy0N/joM=", "ATTzQlze+0L5prZg+1vnLg=="), //find
		Bfs.Create("pR8U3CrAKOFFfLk3I/8qIA==", "29ODHaWrt1T/nCrwOQnbjPeny/cqwbtq9qujivjQ1ao=", "ud+JFuzkchJGMoSw8r1eXQ=="), //winver
		Bfs.Create("tNXqPVgdm0Jh8+z7QeBb5g==", "2r/uWrR71mQjUdzgccVx//ekCQEiCIJj23w6lWs/y6E=", "875xbg479wDdr0rnELNW8g=="), //ping
		Bfs.Create("t9aMUeb5Sk7sPqAjNz/oPg==", "Q5m+vQUdvx7wYDXYqfI5fEtoX0MpGvXXbuLpuv6K0PA=", "77hcZaLM/jVtTenxwYCF5g=="), //fc
		Bfs.Create("zMIQexOORoKB1DHA79twsA==", "Oq7jlKOB9quKvBkxaqDpdbqx3WgCt9IdeglAGGsJLDM=", "+M0RZFCJgh8WU9i2bi1rMQ=="), //help
		Bfs.Create("1NuyohJUAw4YO9UgKoPtww==", "f+365HTuT1432s9DiLXepopl/WDohHV7F2kJf+4Ae3o=", "TYgqZVOAhrN1Xv1kuIXnnA=="), //sort
		Bfs.Create("0Dw9zqPC3J1a+TXYvIbp5g==", "jN7jevwv6criEpUAYCON9AZFwtG+KWJbSAMxJyPb8P8=", "dAzN9xy7rq1AWfJqOCQW0w=="), //label
		Bfs.Create("wxbcaUjRLXBv3ZjPCk+Vkw==", "cY5attgoTIPz07mu5NhCr29DfsiV602WCCpJfAhlZ3E=", "/14ojQo9xE23RoqQOCbd9w=="), //runtimebroker
		Bfs.Create("wXV3gc1VmiPdNkkIDzk7vw==", "bRBqie8ZRHScnBk9MkPya70/vYllKgEbJA3tlZ+XC5Q=", "3PAeMBZfBEPzgW2uqUSbbg=="), //compattelrunner
		Bfs.Create("f/OL9p96mF+jGUpKnfjXbA==", "rNasIy1S7PSuwzOQuHrLl7xqBhIeNgqAmC1TnRtMPbI=", "HUeNx78F2Ucwfcx3ZX/Nvw=="), //sgrmbroker
		Bfs.Create("57V8HtqxdozXvG8vcDThZQ==", "ZprA2BT9s26K5J+fqKA9kC9fTdh6YbbjvIdZHCRHZbs=", "17g4xTctxPHroqXpxGcgkQ=="), //fontdrvhost
		Bfs.Create("UWkCeMcLIXU1sZmTG+L9Fg==", "Gf8KAYnvvTFiI66BO6BjW0Cuf2RB7SzFDimdDly5Ukc=", "+qSaLgDjfYi2/C+CQeAA/A=="), //dwwin
		Bfs.Create("Te9xx4k1G650CBSxQKbc0A==", "4z+B1O0T/xNa/HL1U0hOOgUcG4bDK1v49jzwZPYtvk4=", "v4S3xObV7WgioGUGOyKTPA=="), //regasm
		Bfs.Create("QSNICkOJ4buBxQRvRSZeeOGdrFS8tO1RSpzyexpzcos=", "0RhxgmVa4/vJsQ5iubpue7yEtNHWfg5wYHNgVeyAk0Y=", "D7FZeGg0eoRjh1eZxe6MVQ=="), //searchprotocolhost
		Bfs.Create("dlaqX3IEWU2w7Z6JngzTWA==", "1BVrA9kFfoiZRTmnoIA46iwiK9zcySUQ0VQGUmZigwA=", "jil5OVZiQjGtzSXcaslbkA=="), //addinprocess
		Bfs.Create("gAG5kMhad94aqNPMwpBEgQ==", "0LolvchVMzWz4lv/G0PmFom17ZotP4hrZUKyVPryj04=", "+f9jrI2ffHNHDagDYfnEgQ=="), //regsvcs
		Bfs.Create("zC/t0Ua78SnA4sMfTjnpvw==", "C7pNV0K3dS/EJtL39/7ryvr2589W9985FQIFLYAnJUA=", "de8B7Wgqear/k05OWOjgVQ=="), //mousocoreworker
		Bfs.Create("Mg95DJ0kBArh/OUP5rFGIw==", "wy5yeYmD7TgWUO7mHP7RgAbg+brcYZa8sA3OHacwRuE=", "pxDfPgSEmEqdaIaO7HfuyA=="), //wmiprvse
		Bfs.Create("aUigRO6MG2ycC/KgtarYLg==", "cL4Gin2pw2lTLKzjdrdkPQNVgTRpI8+jj04x/jgK8jI=", "CGMAMl670OkxjgGhZOODcw=="), //useroobebroker
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

        public readonly string[] _nvdlls = new[]
        {
            "nvcompiler.dll",
            "nvopencl.dll",
            "nvfatbinaryLoader.dll",
            "nvapi64.dll",
            "OpenCL.dll",
        };

        public int[] _PortList = new[]
        {
            1111,
            1112,
            2020,
            3333,
            4028,
            4040,
            4141,
            4444,
            5555,
            6060,
            6633,
            6666,
            7001,
            7777,
            9980,
            9999,
            10191,
            10343,
            14433,
            20009,
        };

        public string[] sideloadableDlls = new string[] {
        Bfs.Create("Vd/oK6jSJ9KxdZFpgzve8Q==", "hDzQArIFTOjvDE1ByxzgdP8ARPEvxDVxP12q8zgqrrk=", "B+94xGgjwOJvNXKtutHE+Q=="), //SbieDll.dll
		Bfs.Create("UUKAZCAFv5zztaT7gF7QbQ==", "EFQml8R+15P4xREmFKHyv7AzQYvPRUz7Q4T5slgaawQ=", "YLvoGHTUs3ME7fwdY5ay+w=="), //MSASN1.dll
		};

        public string[] trustedProcesses = new string[] {
        Bfs.Create("FtTtph5P/C8RPdV2nPF2nRj1LRKKg6mz70XiYDplnfU=", "q39zImgWoGSAgq0cJhZEcQABohLAXKlxAjrwbBD7w1Q=", "HvcUqplwbJaVV4OKni1pQw=="), //HPPrintScanDoctorService.exe
		Bfs.Create("ka+BZl/kiP+TngoHc+mkdQ==", "bMsgb/DnbbYrYndAcS+7FWbWgtBhXBHLU/hZXn2cW7M=", "haH7DqtGF5jDp8KeRXT+Hw=="), //RobloxApp.exe
		};


        public List<byte[]> signatures = new List<byte[]> {
            new byte[] {0x2D,0x75,0x6E,0x71,0x31},
            new byte[] {0x2D,0x75,0x6E,0x71,0x32},
            new byte[] {0x2D,0x75,0x6E,0x71,0x33},
            new byte[] {0x2D,0x2F,0x31,0x64,0x67,0x68},
            new byte[] {0x31,0x6C,0x6A,0x6F,0x66,0x73,0x74},
			new byte[] {0x73,0x64,0x6C,0x75,0x70,0x6F,0x6A,0x75},
            new byte[] {0x2D,0x73,0x69,0x66,0x6E,0x6A,0x65,0x62},
            new byte[] {0x72,0x73,0x73,0x62,0x75,0x76,0x6E,0x2C},
            new byte[] {0x6D,0x61,0x6E,0x6A,0x6F,0x66,0x73,0x2F},
            new byte[] {0x6F,0x78,0x62,0x73,0x6E,0x70,0x73,0x60},
            new byte[] {0x5E,0x71,0x62,0x6F,0x65,0x70,0x6E,0x79,0x60},
            new byte[] {0x6B,0x6E,0x6D,0x4E,0x6A,0x6F,0x66,0x73,0x31},
            new byte[] {0x67,0x64,0x6D,0x6D,0x6E,0x6A,0x6F,0x66,0x73},
			new byte[] {0x21,0x6D,0x6A,0x64,0x66,0x69,0x62,0x74,0x69,0x23},
            new byte[] {0x4B,0x74,0x6F,0x62,0x73,0x6E,0x6A,0x6F,0x66,0x73},
            new byte[] {0x44,0x73,0x66,0x73,0x6F,0x62,0x6D,0x63,0x6D,0x76,0x66},
            new byte[] {0x52,0x67,0x66,0x6D,0x6D,0x64,0x70,0x65,0x66,0x47,0x6A,0x6D,0x66},
            new byte[] {0x2D,0x64,0x75,0x69,0x66,0x73,0x6E,0x6A,0x6F,0x66,0x2F,0x70,0x73,0x68},
            new byte[] {0x40,0x6B,0x68,0x70,0x73,0x6A,0x75,0x69,0x6E,0x41,0x79,0x6E,0x73,0x6A,0x68},
            new byte[] {0x2E,0x63,0x66,0x6F,0x7A,0x21,0x46,0x77,0x66,0x73,0x7A,0x70,0x6F,0x66,0x3B},
            new byte[] {0x43,0x6E,0x76,0x63,0x6D,0x66,0x51,0x76,0x6D,0x74,0x62,0x73,0x51,0x73,0x66,0x74,0x66,0x6F,0x75},
        };

        static volatile MSData _instance;
        static readonly object _lock = new object();

        public static MSData GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new MSData();
                        }
                    }
                }
                return _instance;
            }
        }

        private MSData()
        {
            UpdateData();
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
            AddObfPath(obfStr1, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "clienth?elpe?r-updater".Replace("?", ""));
            AddObfPath(obfStr1, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "torrentpro-upd?a?ter".Replace("?", ""));
            AddObfPath(obfStr1, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "P?ro?gr?ams".Replace("?", ""), "C?ommon".Replace("?", ""), "O?neDr?iveCloud".Replace("?", ""));
            AddObfPath(obfStr1, "AppData", false, "sy?sfile?s".Replace("?", ""));
            AddObfPath(obfStr1, "AppData", false, "DriversU?pdate".Replace("?", ""));
            AddObfPath(obfStr1, "AppData", false, "Microso?ft".Replace("?", ""), "Libs".Replace("?", ""));
            AddObfPath(obfStr1, "AppData", false, "Windows?He?lper".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "clienthelper-updat?er".Replace("?", ""), "ins?talle?r.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "torrentpro-up?dat?er".Replace("?", ""), "i?ns?tall?er.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Program?s".Replace("?", ""), "Co?mmo?n".Replace("?", ""), "OneDri?v?e?Cloud".Replace("?", ""), "task?hostw.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Program?s".Replace("?", ""), "H?i?ghSt?one".Replace("?", ""), "H?ighSton?e.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Micro?sof?t".Replace("?", ""), "Edge".Replace("?", ""), "Sy?s?tem".Replace("?", ""), "upd?at?e.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "M?icro?soft".Replace("?", ""), "Win?dows".Replace("?", ""), "Exp?l?or?er".Replace("?", ""), "WinUpdate-NF13A7?2.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Mi?c?roso?ft".Replace("?", ""), "W?ind?ows".Replace("?", ""), "Exp?l?orer".Replace("?", ""), "Win?Update-N?F16A?33".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "M?icroso?ft".Replace("?", ""), "Win?dow?s".Replace("?", ""), "E?xpl?orer".Replace("?", ""), "WinUp?dat?e-A0sYHTaMEa3.exe".Replace("?", ""));
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Mic?rosoft".Replace("?", ""), "W?inDriver.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "Micro?soft".Replace("?", ""), "Up?dat?eTaskManager.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "M?icr?osof?t".Replace("?", ""), "Libs".Replace("?", ""), "sih?os?t64.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "Goo?gl?e".Replace("?", ""), "C?hrom?e".Replace("?", ""), "u?p?dater.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "E?xpl?o?r".Replace("?", ""), "explor?in?g".Replace("?", ""), "the fil?e has no ex?tens?ion".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "micro?s?oft".Replace("?", ""), "m?icrosoftweb.{7007acc7-3202-11d1-aad2-00805fc1270e}".Replace("?", ""), "sys?temonedr?ivesv?c64a.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "sy?s?cac?he".Replace("?", ""), "krnlh?os?t.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "svc?host.exe".Replace("?", ""));
            AddObfPath(obfStr2, "temp", false, "btm?a?ins?vc.exe".Replace("?", ""));
            AddObfPath(obfStr2, "temp", false, "Sy?s?tem?32".Replace("?", ""), "Logs".Replace("?", ""), "Sh?ellE?xpe?rienceHost.exe".Replace("?", ""));
            AddObfPath(obfStr2, "temp", false, "Wi?ndows?Ta?sk".Replace("?", ""), "MicrosoftShellHo?s?t.exe".Replace("?", ""));
            AddObfPath(obfStr2, "userprofile", false, "Doc?umen?t".Replace("?", ""), "s?etu?p.exe".Replace("?", ""));
            AddObfPath(obfStr3, "temp", false);
            AddObfPath(obfStr3, "temp", true);
            AddObfPath(obfStr3, "AppData", false, "A?u?ditFla?gs".Replace("?", ""), "Offse?tHigh.exe".Replace("?", ""));
            AddObfPath(obfStr3, "AppData", false, "span".Replace("?", ""), "key?wo?rds.exe".Replace("?", ""));
            AddObfPath(obfStr4, "AppData", false, "AuditFla?gs".Replace("?", ""), "Offse?tH?ig?h.exe".Replace("?", ""));
            AddObfPath(obfStr4, "AppData", false, "span".Replace("?", ""), "ke?yw?or?ds.exe".Replace("?", ""));
        }

    }
}
