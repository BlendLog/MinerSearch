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
		Drive.Letter + Bfs.Create("gzqM5WDLCKfzCQhvaAZoOOo9oveFrTBANNU7aIvooDs=","lf3e0zA95kkL5elztCEfNSy6EN57kdkpT7GBwvrQBP4=", "nxSLSs5WkRSzqRFJ66kWEA=="), //:\ProgramData\Install
		Drive.Letter + Bfs.Create("uV5AsjU3i0NdAp1p4zaNFpGKKSuVfu6PbgG9mgdXmm4=","YckzhxvWF/DbWL3PArIz6VqwJ4cWvrhpEA4v/9wD4eI=", "17ENCkayb9o1CQm8kUqn0g=="), //:\ProgramData\$pwnKernelSystem
		Drive.Letter + Bfs.Create("Z/FZbEhkBRkNMRWtlgSYkP+uABrIgXddoAc0xSJ018U=","tDu3o58DjxJoScXc4A6l13IiS8D2JV/GiwzsMNaBi80=", "XcB27AIGaHdnf7PrGCzxjg=="), //:\ProgramData\Microsoft\Check
		Drive.Letter + Bfs.Create("eTWk7D3EEsx/fPuEVHzdXt7wGo5uMx8Eui9dE5layZw=","HBvQvQSc0whqkAK0uMxt1GoHG6SCthggFufoPyNspA4=", "n4ERMerPAMZduIAutRFAiQ=="), //:\ProgramData\Microsoft\Intel
		Drive.Letter + Bfs.Create("f3tQJhMtgXK/AKrwonF9sIU0Hooj+3Zx9bX2T3U88LrzV+9G5O7anp4v92FxeSctx9O7+GA+xcfqmAz2h/UzjQ==","LTM2aVBFxQR/v8BjlyBmBClYJyCi6aS/5vJz/QWnKII=", "LDKFl2KJ3lfn21j378t60g=="), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
		Drive.Letter + Bfs.Create("yBWdcIGw9m25PmPM5evXSDuM45tY76lOwcecUwPIvJY=","KiEl+DjL3IaRESsCLi6YZZTAcpr4ZIv82xZ/keN4SfM=", "9nETd0mqNj5snqAneacuHA=="), //:\ProgramData\Microsoft\temp
		Drive.Letter + Bfs.Create("3f7W94O3b2FzRVyAy0MGyHb/oehggKcIr+kqzdo3rI8=","9J/0ArbGPHwDje6kIU2oQsQJCgKEnaR9u6XYkKbmSrw=", "Bf4u1/xfdNIjAUiZzkxrlA=="), //:\ProgramData\PuzzleMedia
		Drive.Letter + Bfs.Create("tV0R6P5hipsbw6WiZen/aUcOlqnABT3toizPu9qRhiw=","9sw/Dpq5ly6Kqny7VAUK/4+vV30S+JdEGyBtIQC6HWc=", "yK3AKPeDE3vpqrRcM6w6OA=="), //:\ProgramData\RealtekHD
		Drive.Letter + Bfs.Create("QAn/rEmV4kEfDr6UYrBfzfyVunLsV4fwb66VHWAe2sc=","B116rpVO+uBEHHtV951DFT7+fwRgeRJtCqq2lFx66uI=", "1s92CzH0NyHuXmM+7RkZJw=="), //:\ProgramData\ReaItekHD
		Drive.Letter + Bfs.Create("LQ/NYqFNit2I24esFGKJQlS2JQRLM9RnAmk0sc49UBw=","7wFW4E8SijdtJNBrGr24P7h671nAb4/r/nd4SzC63J4=", "hdM9cx+xo2QUPf+n3Q5PyQ=="), //:\ProgramData\RobotDemo
		Drive.Letter + Bfs.Create("kV4cJGBqfNt79Hk39UhRekTclL7BDnXXZs8QnMaXgb8=","8Iy3cVk7hksEPP5gV+oN3snrTXvrVX8u9WQqFJGZ60g=", "hqsNm4GwYN8vmwjkMqkMFg=="), //:\ProgramData\RunDLL
		Drive.Letter + Bfs.Create("m7CgU0utMCBxyj1C3pXvP4JZ0SDkSParphMr1klgIfo=","/g4NK+c6JDgYLSt2IaG7Rs/Cz6hlwprFz5lsY1VJlIE=", "L7PrUtXcYe09wyOBf2tvZg=="), //:\ProgramData\Setup
		Drive.Letter + Bfs.Create("jLJHal/h+lJCkkZRDriNKfZrs9FjB4Ac0vfmR2zQhQI=","CQd5Jow5VUH2o3OEEVPVYGPmetYmZirdphhfPIx06eE=", "yx7ONjd+AhGiHvB321VRdA=="), //:\ProgramData\System32
		Drive.Letter + Bfs.Create("qDnes2Jsozpj2UrciXu5X5/vepme0+kz96QVAb8jSg4=","q7LLE0YD4dhtKdP/VZjy6d2Dc799og7mHMk+hXFMkzc=", "OxvWVTNHtVTxsxMbOJiRgg=="), //:\ProgramData\WavePad
		Drive.Letter + Bfs.Create("0kdAjJqfHukjEw4S2j4QZTsd1XSZ5ZTeAFAlp+ltccOEE3E9Y3LW15DSHydEtrpo","YarIaOTX/IbJHZBTj37vh+OWHJ7iST1BzsovbU3GRdE=", "rJqMTEUvCfQNvudwfjlkOQ=="), //:\ProgramData\Windows Tasks Service
		Drive.Letter + Bfs.Create("WAwVdHqbogcMmMdAs2cnLkYiPskUASblE+y0jSK2aPQ=","fBSZUH7X4HXYOF3qR7Qd8gXzYZXIXv2uM2qpSgNcTIw=", "0Ym+SWahYUEz9mvjmd4tiA=="), //:\ProgramData\WindowsTask
		Drive.Letter + Bfs.Create("UDcNOgIPd/f1vGAFs68KxYUhtukA7bKYI0kTW4ArdWs=","vdBE+uzbu4362WFV31mOvB7lbun4inMukgGqfITP7/w=", "F4vIUFNaPl8k2rv3dMWN0A=="), //:\ProgramData\Google\Chrome
		Drive.Letter + Bfs.Create("OFEU3jCQqLbMdvvvjFj3qsJ9mRDcx5umektzxPr8J6WQjxLHFpxb1knDOe/24xIP","yglLrc9BO+f4s6nzxAtiAiSUkhYUNgBJcrGzOhz8z3U=", "Yj3Bv514Up+A104vep3YrA=="), //:\ProgramData\DiagnosisSync\current
		Drive.Letter + Bfs.Create("4bVv39FyJb60mAFDm3ZCigq2Mp8LZnuE9Pc2/gWilDQ=","TC/GcStfRmT12h2OjUTqqO8FikfKCDYc3pkj9kkKn6c=", "Ym9kjPGm1gOfyuaY6y+e6Q=="), //:\Program Files\Google\Libs
		Drive.Letter + Bfs.Create("D1or2wWggwZFlZtcMDpADm/BlcbLfUEre5dbh+upool9BGnmpXorRuQANn5zoedL","miZb2b91BcMrhOBXfmlOqKKbnEKdlHPrANdDqjatR9k=", "BJohkgnTXEYj5XZfxtMBOw=="), //:\Program Files (x86)\Windows VC\
		Drive.Letter + Bfs.Create("pp5FRCpEssHZIMn7R0lxrpQ1uj6JyTs3Fb+VQPBSp3A=","83NnFeUXOrGZVD0SOt7/L1HZXIabInioOW3kCLKM+mM=", "ftmq68Hx2l3LOzlLzdYufA=="), //:\Windows\Fonts\Mysql
		Drive.Letter + Bfs.Create("mknOE0aAbWZ4e7qw3a2n3YHQP9DIz/iiaRWCuBkDMuA8xAlZDXokrIMS5w4ItTNj","86yUoNIUvUuOYWt44BZq74T+Uk/ifj0pEyCbIuYHFBc=", "RLODWO4ATIMgF05B357FMg=="), //:\Program Files\Internet Explorer\bin
		Drive.Letter + Bfs.Create("Mths1wLFC++zglQfcFrsuIZsBHCrvsJdmgR1oRbgmjk=","lUeDpDujP4MylDGXZoQ3juU60afjz/1OvuHSQASz4fQ=", "m2oxpgI3dolT4L0MfLsNtg=="), //:\ProgramData\princeton-produce
		Drive.Letter + Bfs.Create("IBlnpAug6t27xbrM8P+1OJtbd2QuW1nxD6yEWzRU7fY=","UCZmwg9npXvFcemOgK+8NRwMLwF4BUBl7cnwMk2RT3Q=", "Necgv/Xy04JdYyj5zTEnmA=="), //:\ProgramData\Timeupper
		Drive.Letter + Bfs.Create("26SDwJ2TneILarXIm2N/ZWiVRWUGDEWQ1WyfwTrt3xs=","XcvYPinpV8n13ag/1DYO1Z3ifZNpygGs1n1XSIj3z6s=", "bVxBuOaGGrG0G3H9bL6Jug=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("4u/KHAewXjaLCx6+bR82GKoErnTVFHrT4Zz3CpZNodQ=","vWMATeIIKq8ZehVUuWm8xr9eAn9TuibrWHKgdzBJk24=", "V6Qa/WYGSYfsMU45zhxhBA=="), //:\Program Files\Client Helper
		Drive.Letter + Bfs.Create("eiHI7zFIu3bNTjxXTKvPUBCT+xgVaTEwChAjzlmf64k=","9q6U3saEHYSklMTufhHHmqg/qQtEPifuvh5baR2mfdU=", "Jq0du5pH6/dFVK5iiFalgQ=="), //:\Program Files\qBittorrentPro
		@"\\?\" + Drive.Letter + Bfs.Create("NsTyVk9CVZu5loUB7QQqYReoR+ll3eb37u06K0wudzw=","3ZbWO9e/phabWVTrvKlgFk2cELMa0HV6uJfdcmdH/YA=", "yh5LK5Ef+rOcaPw+TOUXFA=="), //:\ProgramData\AUX..
		@"\\?\" + Drive.Letter + Bfs.Create("4ETYV7DHJ4NZ0DBD0/qz/gFfbrjcZScOueYcl9HfFrk=","tfPlafHkJibFvIyia1TU6GzOZ1BHgXIGlF0nVYg+spw=", "YDOX/HYKl7Cm+3cPhxxCaA=="), //:\ProgramData\NUL..
		@"\\?\" + Drive.Letter + Bfs.Create("QNJAyxDhNGTBqyP7xSQT4zlNVDTaOg10JnxMQ+JkPgg=","SL/7tlQa3OHqUn9ybEYas+OOvlAjreF8am228UmXQtE=", "4VpqQPo/8Cz4pAFdsHBQhg=="), //:\ProgramData\CON..
		Drive.Letter + Bfs.Create("O4KCbi/w/hBVFktmFI3eXZU8HcMzaDxmiEjcenQMDYA=","NgBZxANdxxbVQEZJ6jEljvnHFBOgK0v3fLiHG6yhLlk=", "82iIOsQfMApP1pPHXPzEkQ=="), //:\ProgramData\Jedist
		Drive.Letter + Bfs.Create("LipRxpDe37q+RQ13x2yGLCOOyIh+OC3LbwjPtAhrl5q0ID756nxgNEUtu5Rf7b3JFK01osdonXcHC4ITzvn8ig==","I0reEo4poaAS8PIbZ5meIsowRB4vhfu0x4bdmV6NKME=", "S/4fcOWefGfBMnL2bW1IDQ=="), //:\ProgramData\Classic.{BB06C0E4-D293-4f75-8A90-CB05B6477EEE}
		Drive.Letter + Bfs.Create("Qcc1syUBgeIbI82IW2+nAKoF3pCvjZGPitoXAP88Oxe+obaMsnJt9bjmTyRYnT/JNVoc8FhcAZCNq84Lz9dQNg==","O1in9nhF8cKjrFCkTUk+RVwhkW3Gb3yuwXKgavoOLNI=", "EnjJycZ1F/hYw1rYosvPqA=="), //:\ProgramData\Classic.{BB64F8A7-BEE7-4E1A-AB8D-7D8273F7FDB6}
		Drive.Letter + Bfs.Create("mNiVDYv3EHHJwDBMD8Jt9NuZ7ZWI146LtT0JT/Juz5c=","VonDYuO1cIUq/na086pzpacpblELaSj+M9tLqiBSY70=", "Qbd1ASI1rEI8L1yWclMYGg=="), //:\ProgramData\Gedist
		Drive.Letter + Bfs.Create("3jYG2tN2IyrB0XYdz8X/3d7Ef+vmYWrRAq68qFg4dgY=","2C11gDy8ThnI4hNJaCKOC49d3IgKnxzgbM7FWnZZDFQ=", "F+IfUNvzEgmxTPpr9R17Mg=="), //:\ProgramData\Vedist
		Drive.Letter + Bfs.Create("xzakRbzRdmBSV0VlbAuzly8LtB5W2siZtRHCqmUgcxE=","wxo+E6Pq2CRv+cAYTf4e4LdnZQLk7Ep0Efe8ebveCAU=", "T0OJsDDFxVs6evw6567SJA=="), //:\ProgramData\WindowsDefender
		Drive.Letter + Bfs.Create("zPiv6WMwuhPgcQQG2rX6f8ujQfeJod6o/3oXd7IYL5A=","XwKe3130oOsCJdnxvg9J5v2Zyr1/yk1C+Dc65/rnrbE=", "YbElc9aNfrNqygpYseAhTw=="), //:\ProgramData\WindowsServices
		Drive.Letter + Bfs.Create("rnlfW5dXu1U7mcv5yXSjaSGdQga0BWdA90VHw8y8sW2V0ebdTgKalpPkL2EfkHdG","UjtomowiyWI89c52cHwHTiAYbQquf9EyPT12y2jVAUg=", "1EBjTXntDLi7S7nzOVKcVg=="), //:\Users\Public\Libraries\AMD\opencl
		Drive.Letter + Bfs.Create("G+t6SNn0mi0AZmFULpVhK0GxcDflRNL8YqUBKXXH2kJrV+m47uMHk+AKWrIbNgEd","e+72Qt/9HwJ9+VSTTFAOspCz3RagnuBY8CVWif6IT1k=", "3CumGKDZdaDHj0Xrfjhtpw=="), //:\Users\Public\Libraries\directx
		Drive.Letter + Bfs.Create("zcZUvA2WOnQ8j3Vs7bpiC8PVe5NvIFjH3pNLxWeyudA=","2oh1Iz0XOrVPzASyk2Jk2m38NyqOibQsRam0wNlx4TM=", "10M1+FJ2hsCdX+Tc8hkXmg=="), //:\ProgramData\DirectX\graphics
		};

        public List<string> obfStr2 = new List<string>() {
		Drive.Letter + Bfs.Create("JN04w3945m2tsOzDN4N5cm+7K7YFXpLhVToNqnlne30=","bGe0gJA6k6QBLE74A38k/KwVNvsvsTC5pXpjIQMt9Vo=", "UI6Pbu0olplVI3FopwSb9g=="), //:\ProgramData\Microsoft\win.exe
		Drive.Letter + Bfs.Create("SHISg4oq0B0tZF4Vv7k5o0vzKKmvqrZNBWtY1DDkGAhTehfS7wooeNHB9bZ7Bd8y","/C896o2U9xgrdFGqVaB7MCv9H/097k7Vfjtca00EeVY=", "GRQKicr2EXkXsJFjZ+g5Ag=="), //:\Program Files\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("1dsvjndBDADAYgl2yu/9dTDZSkV0qDaqKqggqgyHH0sAVqq/KM3H9X4Lynx6TS9n","8UfqPDmYZAWLevz5yuP92NNCRc1BSlECAqgowUAemRU=", "TOyOhwvw/F/bFOlRcl83hg=="), //:\Program Files\Google\Chrome\update.exe
		Drive.Letter + Bfs.Create("oF+6xYPSpFQDR6ZNPYgk8aTlcdGsahPA8iv7FhacR7V9lMOgeg5MM5qGxAn8YACv","RFSkNgVv9/VtbFj+DDHLatltAGPWYqW3R4wumsu6Udo=", "S8tDqevXeFbmgNoq8IdhFQ=="), //:\Program Files\Client Helper\Client Helper.exe
		Drive.Letter + Bfs.Create("DqIBX5NGLGoYtnbcUbdYRA/YmbxCKCUcrbJraEbjtBJdXauHj/mRlQJMq4zY9MWftn0LkY99tp0pzza9EuEzRA==","kEl3bgvJrYjhGmYEdekNitsnquhl8RfmixrG3tmwL6c=", "+nhTpTfFgdKnprmTtyZmRg=="), //:\Program Files\qBittorrentPro\qBittorrentPro.exe
		Drive.Letter + Bfs.Create("dG7AYUqA/iWpIxqRmeCJMF96H5kz7iBPCL9tnM++8BhOo/igiLSkg914OJeH3ZcB3dtqU14p49idXmbluxjLXg==","FZ79GsOgpjFzJssXwcC10eVrI8VWTuFVzLM2SfvmWNI=", "TTdynpqcL7g5QbtX+tmw2A=="), //:\Program Files\Microsoft\Spelling\en-US\default.exe
		Drive.Letter + Bfs.Create("c1vw+EXkdZUSXgr2yWX5YViEHZiTGW7tCebKKrUiEwc=","+iR4L1zNk8p8ub6AjmqKzP2zsyf/smrOXABJds0B7To=", "I4spV1aEkAmW+eNIRSu3Ew=="), //:\Program Files\Game\game.exe
		Drive.Letter + Bfs.Create("AazVVkNSubtJujQgho5XhgNfcbvMMfY/CDeIoyR0FgjsGeYAGOQjwuw3pw/I3Lu4FdBq94E5BEMR2u20Mnt14w==","PQouMn8uTDSaqlY2Giv7F1Ms9Nk4ZcdWOyGWC/QaFAg=", "KFCE+MdRvWfw3/RNosoajw=="), //:\Program Files\Windows VC\ScreenConnect.ClientService.exe
		Drive.Letter + Bfs.Create("jzjNhCbIgfcnXNXkbqi1SeCiKqio+JnCoXzzw0km0Bt2jy5CuLSOGRatwUN3QtcAmiaaVKUaHp4G/dLdVbmJyA==","Sej6KcgPvb9TnO4XAMs1JU7Q0NTs0BwcH4VAzaRMtN0=", "VaipqIC37iMLeejLkxm4aw=="), //:\Program Files\Windows VC\ScreenConnect.WindowsClient.exe
		Drive.Letter + Bfs.Create("X9/6qy9mX2DYunq2hCig9JwagSwVbF9bwX+loTWi7XRGLcnHS+kb84ST6XcOHtEiVifL6gJVWUg9XOfuR9FrP/5utSnTXstk3sDjkh6v37U=","H/JFC9wJteRsiQ3DIOjxQHEfsNqvVzNhIIU6SIy80kM=", "BxGj3xz4qhN4SEj8pDdLgg=="), //:\Program Files (x86)\Windows VC\ScreenConnect.ClientService.exe
		Drive.Letter + Bfs.Create("mp2/KIojeOQL65P1/j/WqkeZcwdjVsVddCjHei11P0FEAIO/WVifNTqNmNAbTufQtPM6SvenYNa1SlbGCi0Z9KqO6xNfiqG8wZCMeJ8UQxY=","A1LLt/3gdlOlnf1vGmowUF4gCo4H9Fe4J1FWU9dvgBc=", "r0uPrryJgL4pXcXrMoFrrw=="), //:\Program Files (x86)\Windows VC\ScreenConnect.WindowsClient.exe
		Drive.Letter + Bfs.Create("iD1WqcTCvyls41O4c8MuvPjnIum+QaLx+1kxU6nsOVzIh5b6J1roiYVZ80Kp+Nws","YGbFbbHJq8SEan4SC3KBsa2K65tbxWu1g42ajZEPDkQ=", "sb/f7818Iifrd/MAKwWGaw=="), //:\ProgramData\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("r2r3mjxoDhXjNn+Bu4XYwhtRy1Ru34Ylh9h0Bj4zNwAnfuTYSR331l2hlmHueUJA","Zuy3as5rurxnuN8IC1Y6ZU0c8L1f+KsmQOfI52ZSNsQ=", "j/IKXIfh5/F8N5MpkHYpcQ=="), //:\ProgramData\Google\Chrome\update.exe
		Drive.Letter + Bfs.Create("GgCR/3p0GWisLbbnBA+7aKtzrMseDfWFiygpS9av32YmT8GN+QgelzhRVUCXn8V2","Cf5ZmE+/LzbfdEjt3XKCMu0GJFNGTSOW1XZW6tq28d8=", "h05Tmr6Ah35p51RVubvKEw=="), //:\ProgramData\Microsoft\EdgeUpdate\Update.exe
		Drive.Letter + Bfs.Create("qO/0FSjEkTkeenN1NV/kzXtKgV5/6strQeUCscccbPXEB8otPLjAyauYzJ6nUcyr","FPmA1Hw40e0wmjLXflRa7Jf5SX0Meu6XGMNcs2Vs1xc=", "vOf2m+vPnl1vqimb+D9IQA=="), //:\ProgramData\Google\Chrome\SbieDll.Dll
		Drive.Letter + Bfs.Create("ifOrN+mjT05FqRe0sJJuSLq9pbU0sPdBgjqWrrELIoM=","iVyQ+EPe2ndp0v5m/SGHibgB7XfYMCjietphxtfLwU4=", "qg5k8YLxwgad5654fFTTlg=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("XyAT9pQAZJSiAA/CgB1ldgGkq1GduvsjZXcnUygX1JQQrWOKVVYYH8QIbM7xA3NQ","UTRHz3PAjNyvkLmFvTC+oXrtM6UKMVIDD9end8yyGic=", "Q2XE5EGI5QGfUUMHFbMoAA=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("RTib0cvJX4DL3jt+c4QxT5xsKC6nam8ASQzV/6AWGjnslTLBKYIYwakPTr+cBJwp","V7PiAh8mZXF2AB1FIFNUKKgj4z7FG8KOSIFB2Rs79Fc=", "f/NGS8t3ru6vPk73ZSr62A=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("E11YNKjg4fBtf4eA1oX1q2mT4B5xFPn7fK4DjutmxmCAnasSYLoLNG4l6pqWM31h","3HT1t7JqADnQu5jczGrfi6aD+gRZOdFTYt7MQsKVBds=", "OPpjuO1PN3XTTrO82a1ApQ=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("jfVX0JxE7RwcDgRHNM0EcRIKCfbKOquYlZliTZj3pwaoduDqQ0YkDHHpxJ1lDAL9","73wk+dBpVpZ6de7FYpaYfpyXhp4Vv1F/gJVLg0CBSUk=", "XPt+pWrqUsN8Mb6+JaOo0Q=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("yaiEL09O6/WxjvQTNxKgGQU5a/JTF6e4oYmpJn1Oz41tzg0y+todniX7v9MH8y0J","cVTZ18ALKS6vpoU1YhyO38J6tAP93/tGnYSxhoPugQQ=", "3g8OHAiwU8mwkSE9tQ7KmQ=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("Wyxr02bDCDJyYHmOq4XpDnJ9lQzEFIeAOXaucuQ80zIA6/hDN7r2UB3SXSneObVf","qK1GVAh8drgKRonTUWXab7VWtzOP6aLymU4cIZS5tzM=", "d/JSE/oyY9s3Otn6osNe5g=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("zUksjt1TKF4yLpYFxaOmv2q3lrejnWaa7g/wqvP3eniuWfXE0H468XOeXbXR8yM+","MLvkD0D7O+CvUX49Xvb+zOaSbMQENt293yBR9v174S4=", "wc96tV8/zLN1R8x+4W+qZQ=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("cF997Uk9H0IVLr66v9+0Tq+CUkpClyCZM3Q3a/2xjbv3KyRCrW0FLHNEi8PzppF9","dgSROJX1dsCYs2qkTnTToDqSTY1vmUIX4JTca26bbzI=", "yXT5nupMi2q/jx8Q7l3pXg=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("TdbxYOlZb6uHNTcMrAkKVGVa6Uc2tE+h+m5vdTWxNcgDqHwhA+Dqr40CUg4sgcbY","L+6xxh0iMrBAg7N+P4oP2YKIA+o5MDeir1YYI82Orp0=", "haTp8Trotg1NlRMWmBkG6Q=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("l8zXyyHqeevvbcWJ9q0WCokaxT8J9PqWM9GbmExSAvJLhCqMI2TV2WCrwc0bXk1A","jrzHMjBPmCsauYk+giouDX0rsdHAJeKDrYsqJLFBqho=", "FJRJ+QxAPS8xiL2quTxcaQ=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("n6zytvl6ocd2goNWYGe8tduK5EmXK2ULuIWGza+BY44=","sjyFO2x11J9ge2nSzVEWIkyRE5yNpLy+6nJ1d3CNSf0=", "MI3Tsp1nWpWg77E2Aa/eBw=="), //:\ProgramData\RuntimeBroker.exe
		Drive.Letter + Bfs.Create("Hx0L4cdAdRBVgwinaWzvM+39PsJhZqspPQaMCBSvIpIfLrChi+xIcSl4ZnvP//yS","VXi/DNDNxH0psetZbjHSFTMI7dVMMjFOdRmJvz5PsqY=", "yfQZep/rbXGve4tf6aGm1A=="), //:\ProgramData\WinUpdate32\Updater.exe
		Drive.Letter + Bfs.Create("wU8AKlHwANVvKgZo6zrDrzeV54QyMgDsvX/sm0j7AT8W98HcU5PNKaz8HksTKHYLkCZHGFGOftgkjZQt6o+uvA==","jhrBGP/zrseG9HP7TdC9UcIJtyPi7ppUY4VuB62hHr0=", "/vyiaOjOMyV4bLH6diKD3Q=="), //:\ProgramData\DiagnosisSync\current\Microsoft.exe
		Drive.Letter + Bfs.Create("aelwQPJT2iQLlw9biWKvaTxY9Pm71Y8G1yfgq9p3a5s1t40SlSAElcC4jCbngYzx","IxB4P/DFo4AKi3/HfPJgau4ksMdJKdKlPHtmNz9qeWk=", "QyT0GYXjrlnwhkSz2rbAPw=="), //:\ProgramData\sessionuserhost.exe
		Drive.Letter + Bfs.Create("A6FlXREwY0uqEIZ/HJZ8DHTk0BiIOtx3V1U0gC6jPklKwI4CSM2FSSKYCvp9h/Jz","CGl1HYkdCdGofnsZUlLBNeNO7Mhc8uxxPaIAw3BK/ms=", "f6+pZ5x9l5pFx9STTNRNVQ=="), //:\ProgramData\Win32\CUDA\DisplayHelp.exe
		Drive.Letter + Bfs.Create("C9/suG0zByJfrnuMaE64u60p64zBgAX47Y1IVHnYCXz9yPaxQu12H/0l1iGAvhNq","h4A7OlKHLuBaA4Qcl4An05w54cSSkhyZOpln6q5cOOs=", "/ZMBkCsx1J6LG1tL3raO7Q=="), //:\ProgramData\Windows\WindowsUpdate.exe
		Drive.Letter + Bfs.Create("0jssMmKP9lEQoZ+wLmAdP5Ga1IWl7vUCPxy7PTOsa2UKWQ6fYfuZlFf1SfBAw7cM33C/XqQ//sUwclA6RnDyag==","T/taKKvYdmLbB9ZTHAyzr2uVcDMOPOe5ps4c5DMo5z8=", "bTirb5VKC7qaQXMFRSdV4A=="), //:\ProgramData\Microsoft\DeviceSync\msdevicesync.dll
		Drive.Letter + Bfs.Create("zP0CtSiSHnZybKEjR3N/crlKSQc0tMu3YnBLCR774ho=","9bC2HD/qlh+Qfykzvx4gw+TOVOpo3a1el5aHoN7RGH4=", "ToMZ3kIUPgMfLhLpl3Q+vg=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("wvqWIiV58WhFZ+ThQRF/nfyPcx29lil/Skodv1LriCU=","o0j/ZGBQyRArpXHQzq7hI1DrwPBTXBKyOmnMe0B06Wc=", "bKJh8lM+PiNGFCs+E15jfw=="), //:\Windows\SysWOW64\mobsync.dll
		Drive.Letter + Bfs.Create("+Z9xGpFV9lCCXRyNy0/G55fxV1RgP89Dxb0XNaZFFSQ=","vc/9MorxGJQ5zupUNqQzGZw6SxbbO8ScSM48AB8TEdQ=", "+SUpYRAv1cxW30+25fMsRw=="), //:\Windows\SysWOW64\evntagnt.dll
		Drive.Letter + Bfs.Create("yEPPGPIf7K97KIBC0qqL7RBGyWVtI6oxdjTBbWl5FMk=","rQQg/PhejMMdZoMoKs8GtwQb6rbZGaTjL1LWbTetJHU=", "O2q8FpfB4r85VVmR2vzbAw=="), //:\Windows\SysWOW64\wizchain.dll
		Drive.Letter + Bfs.Create("Y35u+0oF0hdCx6ZVp/Oa37Sx3Dku3H6vJ3N2pUVAvYA=","YSKEvDPd7h0agv3pTKl/0u+pVv0Hp0MztWGcG6y6eys=", "Aj+43kbi6WBX0KQQJ5j1Rw=="), //:\Windows\System32\wizchain.dll
		Drive.Letter + Bfs.Create("xoSMrgakGA0L2/SAtUwy2p1Ay58YSa3OVo6glkQsVJA=","ODVRKK8/C+YhuVVWXc3Dfb+RphHXoCpXWvd7mIdXKAM=", "rbatUNoNN+u2t6YZHm6GjA=="), //:\Windows\System32\wizchain.dll
		Drive.Letter + Bfs.Create("BGfjk66WBA5bmhakjCO9L95dtZJltiMkYy8KwIA5AaHpPB8/qi4AV0UOnPY3omKK","kRwZcl+0lIO1AONd5IT/Cc/grR+CutAMtWuMr/0ol5s=", "5JiGujwXqQJTUjba0GJBkg=="), //:\Windows\System32\WinUpdate-NF13A72.exe
		Drive.Letter + Bfs.Create("fFphBp1U/BsnHtlYyfccreAcSZLpmW/yJhvqjPzTqzX6KVXE7lj/t9twkwpniU9X","3e5L1PSg5gWx/1QClf1rIN/GBXDG6A2oe3JTtpkRhDk=", "W+B9paP0B5C7PRhbN1lAjg=="), //:\Windows\System32\WinUpdate-NF16A32.exe
		Drive.Letter + Bfs.Create("+HqjjcaYzs0hgVvnmUtuoS2SAro4ENFDKbt7Bet2MOc0u4IhlkYz84vEL3LMufzi","HbmZtsyX8M3xkJI07pxIRv+xFApUbO8IOInvGqWqNq0=", "1O7ttLQNXgUSBRXE6Gw6AQ=="), //:\Windows\System32\WinUpdate-A0sYHTaMEa2.exe
		Drive.Letter + Bfs.Create("WEqllTlSnaPN2IwLicGiU/NaUkVasSmdyn0W/VUFCcg=","RXfE22j25JfXXacw2B50NZpAQjACZ67aBlBv/EGYFno=", "JyUBh5VEnsAr4GnxiLNNHw=="), //:\Windows\System32\svctrl64.exe
		Drive.Letter + Bfs.Create("zDDUaTkgbaKd8A7GciupKXv9/evarmP+FPKptVuFZUOUHh86PqXXxBdmCyZFb/Py","akIjAbP0vj1pqctQYfm7OJV4vk127jFwfuZZcKbd+8E=", "vGWr2ECIGiAwsvnfE8MGJQ=="), //:\Windows\system32\SystemSounds.exe
		Drive.Letter + Bfs.Create("SxUvoAl+6WhjgVhk5tDhjPfDAH5iwAH+lLxcpNHzAEM=","DXECvqtWg3GA56FOF4didtVob4jhPev8PVxRB1frIrs=", "J9MAYymaUyaTiQOkzrkvaw=="), //:\Windows\Exploring.exe
		Drive.Letter + Bfs.Create("Huy2IDRwC4CjLzg4P53k7Q0SUlb0kyBp6S8mdwg+Rx+N5HWBUcV42nb3Q2t2ZhPy","A+4GmM2hwZ3mQo4BxdWiMhkMdTxPtDY6WE9xCBE/2mM=", "8Nh1/qibh9WbrqszpsyATw=="), //:\Windows\Temp\System Security.exe
		Drive.Letter + Bfs.Create("ef6MraZuNMkWhSvxuVik6CyBaJ7ZhY/zovdnMMcRoVW9w495PoQVclSFpOcV5tq0","SirDMhC4Fy9xwe1gs4L6wDMS+sKIgcTDJk6aWSZ743U=", "qI43ny77OQIbtnyoZe1vAQ=="), //:\Windows\Temp\Windows Boot Service.exe
		Drive.Letter + Bfs.Create("leKH4CBKvMNyEQJ8vSHAxpIWxW7bH6LGkpAyC0Y1GKnaAYOm0/wLbuiGRJUB7HhF","yn+g4tMnXz7x7sBpXGnQ0Cy+tJ8pYwWHw2iDnXr0Y4U=", "M3y7F85MkQi9XVcfXpFrmw=="), //:\Windows\Temp\Windows Host Service.exe
		Drive.Letter + Bfs.Create("ks/q+49vN7Iitw82kHRJHKglF8LYFJFyh98dDiABEQM=","0IfVqpqBe7aWYJucCI+VK4Uj5E8ZjyZrZNpwuFPCWfg=", "EEmcEjO8/vwBTkl5uZlUow=="), //:\Windows\Temp\wms temp.exe
		Drive.Letter + Bfs.Create("TUFui6fkHfNmUksTxODFJHob/z8JJ627ZYCLOo7g7HwJimwdj5B+Mc0IdhfyHlLr","UAK2k1Nhd1kuvFQ4e5drAZ9XAEsaGlM9vJk7sLQc2yU=", "b6b1f49dZetwThHoT7/qdg=="), //:\ProgramData\Timeupper\HVPIO.exe
		Drive.Letter + Bfs.Create("FVlfBcYaD9r84b6e/9QB9fMXkWgxO1lQOmMxGQJwDPF12jMFcBaYCFjqIEnttsQ8","l0yh52phCc7xcHkoZ5o3JVWzGsTdTbaqi0L5YCDy9MU=", "DsSMW86gLM27jHp7JpEQUQ=="), //:\ProgramData\WindowsDefender\windows32.exe
		Drive.Letter + Bfs.Create("Ilu6qF9rnA8ztzFEUIeXmdF572Ieziz/CkVo6Ewdhl0kgJYdxuy0e11O6K2gZgtCCU99FAY+DFASI6odNB+DFw==","Pvod4uYVGSxWsAtQ8xu60EzA18T23dSJ9WLDjZxVd54=", "Pg+QblUn2cOW7ZUKNvUIgw=="), //:\ProgramData\WindowsServices\WindowsAutHost.exe
		Drive.Letter + Bfs.Create("JvmgvEIskUh3mVx8EiPxhx9mc12yHX/fyk9S7cv8qgZMdat+w55iOVE+dm/Of9u3","BHJxWze014KQT6PXK4k6+zKnSwrMo0zWkm3pek5wTUw=", "g9CTFxiicxmIeKtCx1sK6g=="), //:\ProgramData\WindowsServices\WindowsAutHost
		Drive.Letter + Bfs.Create("PDbi1x0VvwFoFeHXSRwtTfL9pYMPZwZdlTnfpudNp80XcWkZh132L1qOh9VZ1cYN","3ef7nNlGNWEBJrVzublJ0yGExEpocmeiriC3JoLCS+g=", "UpQQxEJxUQ07RVOWojjLAQ=="), //:\ProgramData\DirectX\graphics\directxutil.exe
		Drive.Letter + Bfs.Create("w6Z0aU2/9WWxGrwrNnKRPpE2LHppxCsQgciLbuL+5xtRuMT0yjEb4nr04MwrinsL2SN5kk4iCHdwl/qrNykk7Q==","3dHUSajMc0Qe1rRmfYTM9p7+3ap6H3lI6KCcLAQL2+o=", "TTBLclOX4bOikcJOxdAA/Q=="), //:\Users\Public\Libraries\directx\dxcache\ddxdiag.exe
		Drive.Letter + Bfs.Create("ydkWkbZgvAPrRJsRmkBA6l2ln76M9CenV6HN4siOqPdzXUKogvQwbhdn2LPp2g/G9lfyXQvjuenkpsSiMBLh+w==","TEHyUBj0b0OgnVjBsd/8QPUliYhOLkev0ftRuCF2gaE=", "+ODO1+gWXmZgudlZcZijfA=="), //:\Users\Public\Libraries\AMD\opencl\SppExtFileObj.exe
		Drive.Letter + Bfs.Create("9O90mW0pMjfpkRucsMKOFpxoKEapgys+uzAQbEtBBaNm8lE+PdLcQgruo1EPd+EPBaFrBkbdYZt3HLInVLeysETv0qQIAGc67trbhn6DQgbYm0qFEGL8jj1xd2rz5LVu","3vbgM6CRQTpS9vwYNOJGeX81sUG9e8aqZSojESPEdUo=", "NEXeZHpwW6MB24RgsZ+Dmg=="), //:\ProgramData\WindowsService.{D20EA4E1-3957-11D2-A40B-0C5020524153}\UserOOBEBroker.exe
		Drive.Letter + Bfs.Create("mevSuHZfg8QUZ8fStzQC3xFMsRsLQDxRx4QKCmgc1+azzJ5Nk4wO02tlZ+zJHiuCJAVKq0RhW3/b07uN8xw8XRunEwEfmuVEj93lJUP7wLv9eNP8OhiXJ10UwN7Q9j7I","vcvkMRwye1JjeG4MecBMDJTXSUNZQSzBcu+lAqpoyc8=", "IkO435++iJ8avzDkaH2XGA=="), //:\ProgramData\Microsoft\wbem.{208D2C60-3AEA-1069-A2D7-08002B30309D}\WmiPrvSE.exe
		};

        public List<string> obfStr3 = new List<string> {
		Drive.Letter + Bfs.Create("fQk/251VJQplW/2doizb+g==","2v4Wnk/fwCsmYeAfEKfxHu0eVHTTvpEC5RGsAB6MjBA=", "HNBN09MWCCQ6HF10goPeWw=="), //:\ProgramData
		Drive.Letter + Bfs.Create("pzruqcBgvhtq7IdrmdLjX2CaPuaRgfUso7Uk3FA2W3H6jOFCXIwhfgbjCtZ+QYDu","MlYKJH0tJPBJ8Qq3Q7BxjqvYrQB+9AXZ2yqdOeteeB4=", "e0TSJBjM9Q1QJai5PRSB9w=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("Z0YD6FcxkbbfgBkJVURxsB/fpQ2D1NruWNXe2AUV10BWOOvnWxL6Tuh74rC6ABLJ","ghoihWpBpeqP2ZckgN1Jmt7wX51lpg9UvVf+dmq6hFw=", "+asSDJ5tmrdgYsYiOtYWKw=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("IFx8fnsqWoK9nkw4xMlGtRhY/9KLPI5QAmqfTP7aG54x2aimCyVlWXkZMtVoeBCA","D8j+7mTwfJ2CRlAVfHRPOv3k8zy15cnAx73Mnme826Q=", "m4Kc+3EnHdMmt3Uxt+SmPw=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("e3NQMuoYRJqh4F6NLBPBAoOEfu4ly8DjfsKDEWfrZ3r/zdPuBBrypMRmVY9ED2Ah","ofK+KAW7XclNqeKuzHu6DwCTDnFESQebE/eO+CARHFg=", "+gdxiXsrWewuzgQrxcm5wA=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("hmQivmnNylxpjXB89uLnjv0ZhEXGaZSNDPeyiu/wEP1RGu9L8ZcZQV1f0uYKHO+T","QMi1rrHdBt5hW2fNSaV68hGSApBi92HNV428Gip6NE4=", "Ob0LK3QGa9zzVV/uA25RAw=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("/CzwKQZu8pl9L0fWEdr3holBAyu080AbQ0TrExNi5GXcykUbvMKH9Z+67mkQ0dxO","oqk9UdLF45QxV3+A7Fy8MAFSyPfejDz8s8TYEshfLJU=", "4SMwaue4G0WeMsHgSFAMCQ=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("vWp1+q0Qc0j0PzeatVLiERiPAl+vsiJeZoh8LR75j/fyEIH9Sykw1rirsdGkbqWn","H6I0/hc6Z9qSQy3FIO1kSGPe80TefG2xWEjC5vP791w=", "NoygVg0xEsgLMHwhLVmO+w=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("VDNTAnFJgcKpfIPmDe4IyJ0EmlR6uuRCcVJOm0pjdvcgNuVjqo9+zvaEVxpP/qvg","coqXjuuGIaF5qeRV5Z9O3nLc9/3u8b/mSWWqVSsXGhw=", "nVrJS8dVag3xqSGxTDJHyQ=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("JB2I4Wy94dqOxxsFxxqNchAXoB3L7sLDG1F37M5rk3juMB+cKOFVBpOtw3fhYjm2","8HOZI6c4//LVPDJ/cNuchcX6P9Nq+0LwJ/kowVaWoGQ=", "cSZKnS40yLDmg5WTuZxqsg=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("h5c9UqD+WFth1ugfuI/+L6HmNIqfYZBl+imFE6KHS5GhHUOB2f80ojCuQau8+ecf","UH+FyMcbaAgmq8fRwpSMjctoWRcRD4fx0jKbOfW+GQI=", "/69lX6jI2OauxfsiXxtHbw=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("OGPz+Zg0nXHg+JAUtljOxnbaauPTniu8xA/DxGTvzT4=","ENkig8K892UrB0hjqeUBiaquj0iDpK+hLqHsOqfFfvM=", "TZeoPw86pDne3ifEuBElVA=="), //:\Windows\System32
		Drive.Letter + Bfs.Create("puzgAO4yW/iW0iV11mBkjBNlen72FzrpmAxrYTxZlnQ=","Qb0g/9y0uvAWqUpOoZcpw2+Kd9wRstaMXOw4fAPBHhM=", "7zaKjyQh+yrWQ4jyG1Qbow=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("3YgxG9P+w5lL10Efy2Rm6bBYWl0L9Jv+qMD01zJEBbCmvda+6dv+s6w7lIEcCcDw","kwIfHXP1GPBsWlN0PyS6RGVe0yCcv/yJ8qG5Zh3mu8A=", "tCF+nSH4HZHTkGLzZCmNlw=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\
		Drive.Letter + Bfs.Create("6yZJ7VGi6NaOMyejLOhsbmoFyrOTEQnAoMIIWik06KvX7Rva4FOo7z0shoy203nhoB9xz5TWRgk6d2xkN3WiGQ==","XA/WWuPXd1yMiSg14gcHbzUx8h4Y3xbmz5xDnX8UMGU=", "pBaXI0Bg67gTz3OmUbLBrw=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
		};

        public List<string> obfStr4 = new List<string> {
		Drive.Letter + Bfs.Create("nR2QWyQKq0g4OREmVCutpaK22yYE60qpxj/L0W+bfN8=","xcY96coaAmn8y9gc4vy4vPnlBJnuzbbcqab9l7aEUYw=", "nHIsGJQWQaeBN5PFWJZgaw=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("Itdi72PD9hlBJVKaEDMkhkvbvFstArd77Tt4+8B1iQjQLfm2gB/IfikE8P3dIkZD","HOYNeJvuluPX0Av089yKIxOV3OGDQsatq78menhb5OY=", "FhSZIGBQqhElY+oRTtZovQ=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("vYPjFHiP29nv/SvO8ZzpzUIHbDu0E1Ph5g0oozCelUpTEIvPRYb+TtCgNNjhQEHQ","S25NrgOZQoJi+yIvmQro63snPmic5RfGRPGZq3n907U=", "wjAlD81uR1Kx/hpHd1lSwg=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("cVBoCZgBafshA4CUkg+4gPXD+aCvYAJJvUQB0VijynTHqUa+KustzFFL7QxIk1dm","Hsq0X/ild395hN76k3SUVS4cYGuRI3vqc1Oq2tcqFRo=", "fb0NnMGtzCN9MTCrHIYJEg=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("IGVOQn+Q5yvr2mNsPRbAH4NgTdFmJj8Ask84LHkZQhUe76E9AZ7nzjNBSXxZLn0/","8m0BZH1OD7XGxlD7XB7I0rPUgn9Ufz/N3o+jDOTq1v8=", "8x3IiC2Asb3JkBT29r/Q/w=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("9nC+lj8u8uLFiMyw60EdogmGnQYpDTMfLKJGt0jzp5l8B523kcxgJ3W+uQSIm6AT","Tz6RgmPnLMV9FaEa+ilswokCcrYcukP9Qwa9g++JYOk=", "lSofgS6J4d6a3Xqtf+mxQg=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("oaJr0VxRfsqOPxf/QoDU0geubI4NW9X3czNY3+3Lti/nOHt3nAf+OZsn1OmYFrDs","V2zLoFTGnDMSzXCRTyyQ2daECheeUAubIwgZvqFEkGg=", "VTeYxIBfqsznVME/QYynlQ=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("CQTPucQTxpZu0Mw4j1gc5U3LPVPOWcS5L4oCXdnx3JlBfBp+ihk0vRDzxybW+Pjc","LeuTBBVybd3tLvBJVjYOxpqzDkkjXUiq9KvzWK2oxnw=", "VbSAYkGeSQmlsLQbxDjriA=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("r00aOZDqDWAACNFzO+4pjCgaU+Ae5ixWPgC8bas7sy6vG+DZ/XoILbXrwrhr9GZG","v/NnRorqCcHzgq8Q6r9K5qVJTB017pK/gDORIQF68zg=", "PjOvTJQpz3VS+8H13fR3Tw=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("oE70TuFh3MtDPCwwshA+kH9iYQtCECA6hPjUCpT7B6SBYQs7gxOH1jKerjTuJifx","KHDepDM+dKQ+dzyNLGM62zkHzGtS7Nqr5HecEr1UYFY=", "j39FrcRzPoH8LzkQ70GtqA=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("Wcmxq3rtUaJS4TKrDNU22oqYa3HqbKgLSivoPDkTegYVv/Ke6hUVpqFlrr1ZW0Gi","2D89hXgZ8L4p2ycynblJkvJVizRsN2Q3CAATnlIx6xs=", "08Teu2YZMzuU73xZoWz8jQ=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("H002y+GFYezMaTlmNh4zfH0S9zWwCGuH8nuPNIsNlMs=","1YEhmsHMndR/6MaWEQnWgyQlJL8H3+2k6dtATFTC/Lk=", "7z5BEarWH6PhLDorDEv9Pw=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("mpcoK+dH0al+Lm6MVy0j+nRSj0h1BO1lC7QzXiw48gZiPpkFm/eqANS9ylzhIlf1pp3vtwsAL3GSq7ON6UUAZg==","oIfI68WVmPpJ23CbawWcHPE0ibssvzUoi8IL9P5u8GM=", "+uiwId1eNfJbPf/4lFgScQ=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
		Bfs.Create("0z/6FZ/m041/xVnqjFsGPXcefNkHV3n4xTwQp3ABekE=","kgRBlJC8SVEm88TZh4uVyf1WvADhxiGwMoaI/rCMkTg=", "SOzlZcP3F3MRiIvGliPzEw=="), //AddInProcess.exe

		};

        public List<string> obfStr5 = new List<string>() {
		Drive.Letter + Bfs.Create("TMMgXnL90RPR7pFSMUQKQ/Jjus0KYkw0AOZqscUEb4M=","Tj2+hnMXUaBHRpxHk24/8AdIfh/XdioJhUxasH/vkoA=", "m2iqDdEtHm4SckziciWf/w=="), //:\ProgramData\360safe
		Drive.Letter + Bfs.Create("+PkIdtq9GtdabzifiEeG3pfFdv9NXqMm1RnphsL2Dl8=","9rsQDFv0xINNqny7T3td76JutM8SHucw68ZHNKWvM7Q=", "t79xFufcYsTRfGhi1SM28w=="), //:\ProgramData\AVAST Software
		Drive.Letter + Bfs.Create("Tgy5ZbkpjGz3TYXtDEQaMlT+cyA8LXX/FkUDisU7Pzo=","Ezf7XWB9Tim9giP+hmMuhayi8cp7ctESO4lkitTm/NU=", "X+Sd4A1dCR3Quz0spbqJzA=="), //:\ProgramData\Avira
		Drive.Letter + Bfs.Create("wqLPOGJ3aQiQi0Grn+3hmEwDj5vES8+/lSbXPsbpqs8=","u0W8fltMeDhKBOtduZ/nJjFN9r3wbD/htZ65jUTQj5s=", "5G4mYVKmcJCz2ujkgAkXxQ=="), //:\ProgramData\BookManager
		Drive.Letter + Bfs.Create("txKScdokky0R7WYIxSV3syK06TCnbc9YYCggaJVW+yI=","IYvUPz8OJQboAPKQl9nbaVbuH0WxlbNpNCL6nzYc7e8=", "0Gk9f6z3YXh1xNM6e0oizA=="), //:\ProgramData\Doctor Web
		Drive.Letter + Bfs.Create("cUfgm23U7fEdb9vRnD6NA5zlC8ZGszdvoHTO7qCCZOM=","4BXq3v+ScIDuawEetVQc7TufWezRGJFN9MHRDuo6SAU=", "pp/iCAdBS9Ch2vv2vZt8Hw=="), //:\ProgramData\ESET
		Drive.Letter + Bfs.Create("nm1G6/6RWKhWrVvcwl3+HXZitMZBS/YoANQeZUWQlnE=","8R/UgswYEnDBRJAz94+ydvRZnvE569Y5w2heKD0UQiA=", "z1MEPdsRxGbiIGnzPDqh1A=="), //:\ProgramData\Evernote
		Drive.Letter + Bfs.Create("uIlxuo+zk55otSqPqnrDcwBmpGY4bYwWc1KHUPdvFlc=","4PrpErV3oii6ITDWlA6vVcwmmPdokD2xcmUEqn1UDCo=", "j/azH1O6Htx5EzCoiv+0oA=="), //:\ProgramData\FingerPrint
		Drive.Letter + Bfs.Create("BuYP7EewHIkKeKbTe38jCNqE8Brf3DI4pwrNVx96s1Y=","5ciYgPP9ayEwv25mMPGY34PPM/GcNAxKzrfPKKgGTi4=", "uofywGEogCkUcVR1lEaxxQ=="), //:\ProgramData\Kaspersky Lab
		Drive.Letter + Bfs.Create("p0aR5kmBCXXHVgGknM7Q/NUqy9xCenxhgnUDIMKUvp+ACuWOWSbRptv2khA9dJiY","qdt9plhCUNhFblmcrtPuEM7Y8TZ6C63IINgWhlTWFTs=", "ZwnpL2fY7637O4rOu2LtlQ=="), //:\ProgramData\Kaspersky Lab Setup Files
		Drive.Letter + Bfs.Create("zASL5u68Gy96QwIYwfbpIQl3fhewodKs+mRgQJFkSpM=","TxkC9SN5kzibiee5R3pb3nNQ+n2rbxrQihG2gydAfXA=", "pMUshb8mvo7MXYNIBGQEEw=="), //:\ProgramData\MB3Install
		Drive.Letter + Bfs.Create("NZOHvlKTcbyYtjowLbqwOGQU77ioPPcSrwofjF0AQ3w=","Mt1qFMaVc59ZczjqXmgl1vjubEhleFp2AHK0aPRIZFk=", "1Z2a98u1ic4ZqNN6AoNLEw=="), //:\ProgramData\Malwarebytes
		Drive.Letter + Bfs.Create("DX9P+f67nzIX7mGWNsfHPG9nHwtxrFlf90XWaIpGjhc=","9BtPtZ6mzXC0VGUAFO3w8HN6XG9NowS5JdXWxOykA1k=", "XE+l0Gpmp9CiUq5RGY2NFw=="), //:\ProgramData\McAfee
		Drive.Letter + Bfs.Create("lUzHhjmU/arkeFOzCQZeUSPOq5hVGz1cz+6of3zVKwk=","W4nw4BLKZPJQhrK7Qjgi/9bBzD/HWDVkMtj741LyWVM=", "Ne2kKnpQKHzxquZJKlyWFg=="), //:\ProgramData\Norton
		Drive.Letter + Bfs.Create("cGb2Dn0YPpAD6/GLyZLjMlbgTiNWiZriC/7QGPQbzGs=","m7a7ZKT0uJt3iO+bkL/Q5ZmR/jukpVWRHWMy8szETxk=", "tRJwC0eCAr9CkgHiLDavMg=="), //:\ProgramData\grizzly
		Drive.Letter + Bfs.Create("TKgYJGtuqm/uphuP+Hw5Tyc577RYJXV0K/9i722tkzwUtSh7lntWfttHTBtt800o","p6bvgpE7PYm8v5++RzsqJKFtP/4L9IbffPYg4CCIino=", "N4y3wrsvvq89TMS+ceQVmQ=="), //:\Program Files (x86)\Transmission
		Drive.Letter + Bfs.Create("PlPCbfq2gF+qfzDE92bEpAcTL1mpyM4DNT7h+nBu/Zec1NQQaqqldO5LZXUgb/bY","QcSYnY+77sppzGfWuCTLtsT0QRFY+vadgBpX38mPPyY=", "Lf5QwnAbJvTPgphvIbVpCQ=="), //:\Program Files (x86)\Microsoft JDX
		Drive.Letter + Bfs.Create("j38ZFTPuasDjERNrOvCYO7eexHAzyASvlTJfgHYWl64=","bCpjwPQ/BxJypHCYe5dQwMCj13mQ8dyXTpsmqsVoG90=", "eHet5pgDm8CYn0s/6MDqQA=="), //:\Program Files (x86)\360
		Drive.Letter + Bfs.Create("CGheWR2xKo6+rs3S97l9tniX8rDhx+FRut7bEqzPQpmwNxGIDve8B0XaOADWd96i","rbh8ZrRK0+kitCKOrwpIQYmtBqyWOybynNSw9OMQ3H8=", "H/6S1zslaB4esPOy0juuuw=="), //:\Program Files (x86)\AVAST Software
		Drive.Letter + Bfs.Create("I+L2A0c5mDgnYbFpCCdW2xCpcbIP/2BiWbFanG2IK0k=","nTYjKIJ56+VlBFuys5T8B90wOZOKH8MJTsk+IeQQp24=", "2kUS2SmVwW86BDkjqlrHqw=="), //:\Program Files (x86)\AVG
		Drive.Letter + Bfs.Create("Y81nVrbo8/eoV3IMJvzJT9phxCdBg2gpvsVLXkMsBn/rK0oIWD/I+lFl1G9Yv62X","JiE/zXd0sxRF+I/Zgwzvpr+o4Gw+saSAT5zvvtuD9m0=", "SdAUiT4W1LRqr/wrWgyL7Q=="), //:\Program Files (x86)\Kaspersky Lab
		Drive.Letter + Bfs.Create("V9M30QRubfWfktee+pguFQZaMHID04Y4VuDw0fNeEwk=","B27wulye66BowXvd330gwOQjTfh1jWhbomwqJxeIrpw=", "UXe/8k96sE799RopMicCqA=="), //:\Program Files (x86)\Cezurity
		Drive.Letter + Bfs.Create("tGf9YX/LZuBYsThOLnynrnrv5JCVnfuAsOhavDqJQwi0pz/0YTUJZBX2S/s17JTe","ZsEzIMo127dd5rrc78Nsi88Aqgaly/xK7WkYtCAUVTQ=", "lkrW1LKIAY8S9KUZZzRwBw=="), //:\Program Files (x86)\GRIZZLY Antivirus
		Drive.Letter + Bfs.Create("brSPtNx4mI5Lqx4wMbdFoBWhDooI+9XW+iZbpuoIruHRlG6lykgz2M84ifjWWYBu","kF0VaaeUWHWqm6a9PD4z2fdprVHcjxAvwiBGrvdNyz0=", "JrzHVXyauplVK7UjO+sdrA=="), //:\Program Files (x86)\Panda Security
		Drive.Letter + Bfs.Create("4eWVWjn6elacFrXn7TntIh15/03O+le0ZJS/seo6w74WyBxRQOwwwJYM6yWdQBI4","XLp40NmyJDdG7PuF3cIUxvno7GNg0Mk2vF3fZS8uIqk=", "7aM4Eue7Ts2qC+mI+NBFnw=="), //:\Program Files (x86)\IObit\Advanced SystemCare
		Drive.Letter + Bfs.Create("ZDrkHIOEcVF/RvnCdO98j8xfRv+Uj8jAupEBrq2vjUpvhLZDCmLKFdOkrsBCdCr1VXaQcWHxkVPdOb4iGp4xow==","qSEC9lPnjI02MYMley7/0By4YeQokSdJuR1wwZNm2ro=", "ta9SLHcwdXuR/B3uiF/MTg=="), //:\Program Files (x86)\IObit\IObit Malware Fighter
		Drive.Letter + Bfs.Create("oxrgG7DZy3hI2pBl+afcdZPM5wrGSRhIJEf1kqmPj6Y=","cH4vWgzU7eAaOjHbmllqJK9g7cYA+ERGNVPU/9c7+1g=", "GBG1TqFPRdlbC98SUbMKgg=="), //:\Program Files (x86)\IObit
		Drive.Letter + Bfs.Create("TDCKE8m8tKgGW3PAiidYD2OIKWCI9JFtSoDP4AH95rI=","3glR+XpkQaT5VS5FBC81Xdq1QFaExKQ6HVP00DJH328=", "/E/1L1Shm8sFfanAtaJ1zA=="), //:\Program Files (x86)\Moo0
		Drive.Letter + Bfs.Create("3nIH5RvIcJNW9Hsl6CwfGLBvWtlgt2gNwYjJrEyhHlN7YYLq1sr5MtZS7Ph4yJCP","09oP9FKbgVjUPbAG6ISk19A2+mz3uV2Y3c9r0sdsu/s=", "gdhZ4fkRdYyyEG/p3sTraA=="), //:\Program Files (x86)\MSI\MSI Center
		Drive.Letter + Bfs.Create("MApj2S+pmg36WTL1y1Zf9f9ftfykMrFzFewOrMcoEOc=","TbiSJKN9/OCriqeGBwtbxycsaQKcK2yqIgudqF78KY4=", "EQQHLhtfChaEvtDHDit8Ag=="), //:\Program Files (x86)\SpeedFan
		Drive.Letter + Bfs.Create("Z8mYZG0hobMLEiJ3dRisQVaD/yFnC3d6s4+t0RXRttA=","xYGbDofw3XJ2IILMBXhZpKVKgz2++uknlZ68D2iFYQo=", "7U1u72VVxQvZ0QY1UeGCcA=="), //:\Program Files (x86)\GPU Temp
		Drive.Letter + Bfs.Create("M0/4fPzQQKEHJuZBUnpaaxb7YSc7yJQ/M6CKdWFmOTI=","F8F+uN7iXUPRTc5AfO2oyFGaNbpmbTNXe+mcvaJ8B6g=", "8+9p/MMSAOaib8usk1R7xQ=="), //:\Program Files (x86)\Wise
		Drive.Letter + Bfs.Create("nxNaVss7mo+pioEQRbAejts0XfkEh2yeix9u5pG7ZME=","s9jhbbctRs+UnfBWGnhx5XiEgujtTgcai4MPf7Cpsfg=", "9hjd6tCpyqn5UXaDSzulPg=="), //:\Program Files\AVAST Software
		Drive.Letter + Bfs.Create("E7VhlV5ZYttNzhkD6PNQmya/l9JVEOwQ4v8po7DdZUc=","bQNttG3JRjvz9EcY7XCRFmMmuHxmfPIZBX3KlKgtero=", "SseU9bIwqxUylUYA4jK03A=="), //:\Program Files\AVG
		Drive.Letter + Bfs.Create("xRDghER9iQYl/y3k8jZtD+wT7R1g7KA/te0O1DQixHWPN8dSn64Xcdm25iwhDmya","Gmxv+RHyQeAZzhhP2sznIsPhgHoE8fu4o/9l7KgR1DQ=", "TNjwOiqArqaY5jaqQvYdnw=="), //:\Program Files\Bitdefender Agent
		Drive.Letter + Bfs.Create("uAGa2ta0rIk3QqF1C1XX7LXw059nHIx4hYdQWe3ODAQ=","PRFSxyClIQHVARKxkKRV3jXG4CDknImKnGpvaX1/Ivc=", "78t3BfahVf0X79bWg6O9kQ=="), //:\Program Files\ByteFence
		Drive.Letter + Bfs.Create("JqbmegmlXcYtKYNiSZyrWf8Lug2RHsjG8s8Xzw2BmGY=","kRKbeXkIPfXGPDaluPWpCtP4M+9ZhN1JXB+CLuTcbjk=", "b12iPJ0edb6Dzan407eTJw=="), //:\Program Files\CPUID\HWMonitor
		Drive.Letter + Bfs.Create("bs1DNNoGDsILuHr+Cs+Oim5eNa+u44fINRdqP3W57fI=","it0FRKYjsJEUMj8RJ4zzcW8/1wg78dAHInHBQhnuDAs=", "/4Z5qjP2D/1hBkRWtpf4TA=="), //:\Program Files\COMODO
		Drive.Letter + Bfs.Create("duhppBrYPiNkMCMmcxCu27qbXUfNj4uErGV+jU0WaUQ=","owCkkgQncROg1Evu/zW1UUgagVpvrjRTjvUyC/4/gxA=", "iowxl96JFiDWiDowQ2KHMA=="), //:\Program Files\Cezurity
		Drive.Letter + Bfs.Create("vXNOEdepfDdHVnB9HL27y6nSCY41iETCuiBcyaVpvP4=","vXrCBJSfIDzbMGxIFyuUljoXG9k93SO8vT8cMeOClm0=", "GQOniI3yARBzcdm7zpt2mw=="), //:\Program Files\Common Files\AV
		Drive.Letter + Bfs.Create("5dPhUahXY102xEJLoDPkENu6afs663gEZ+j/bz3CnlQFKg8821pF6p9YqLFNv/NZ","wb0+IWmhCI2Fw+R1j3CnkZXFfZ8V8tPpi4C72eIrgOY=", "ynRiFYQMDR6O/7xDvHJ1VA=="), //:\Program Files\Common Files\Doctor Web
		Drive.Letter + Bfs.Create("hnIWXz+BjHVV5wiASAw97ao6trEyhd0Me2sKiuCCxiwITKQr33zm4B8/g19tOB+4","g0/AcgxnSbsAVNVR6B1RSuXK+CeHgbB3+XtG0zEt3uI=", "unhMCQ7lg0YrLCnpas17fQ=="), //:\Program Files\Common Files\McAfee
		Drive.Letter + Bfs.Create("eDIKI0/8qoN4hcAJvCCVuX6II3Q2H4fi7BGyc7FZOp4=","FvfwtOdSQq61YRv0+pjV7uczEYBYKB1DACVJxL86ipQ=", "wIo9IqVLDUypWF2h2gbukw=="), //:\Program Files\DrWeb
		Drive.Letter + Bfs.Create("46kUvSYMNepZjeca3vLxn33m6U68UbLDHA9lK6nPOkc=","q7WP55UkI9L6A48TATnLIjA6Xkqs266NdqtHAvlXeYw=", "97Ky6MN3GlCJhJcWxqx//w=="), //:\Program Files\ESET
		Drive.Letter + Bfs.Create("PulSolrLZ9htFA7Fd4bVZjLpWl3VG2vKUG6LX13jaag=","Vvq3crzXXFmtvILHwrvypmEiqOwgUJQOvr6hYbnkLv8=", "TWWpCaruqvE8t6crKhPq9g=="), //:\Program Files\Kaspersky Lab
		Drive.Letter + Bfs.Create("Dtmh8kCSIzWosAIUUsjND41gHffG8V3MoMPymFqiWzZpznO0VIeDlOUHClxI5aY6","MeoBd7rpRbofDAobJgz6FnXbZxWUA0e51Jakk7K1rQQ=", "oS2EHxR5uFwie9nqEDSOMg=="), //:\Program Files\Loaris Trojan Remover
		Drive.Letter + Bfs.Create("s1+7o5PrVdRAFqEHURL8t4VxQSkd/97VRYJaWkbr4mg=","I6Qr+0aorzHm89KW65dWkNv4aFzRYlHc0HY9JEqzrnc=", "sAhH3f2Lgj8s1rDpEzer9Q=="), //:\Program Files\Malwarebytes
		Drive.Letter + Bfs.Create("Afv/xGl/JOMEDtAPAH7KrzlcyNpoE+E2jkXsZ83Q9pI=","/gLYuWpKPr+MGZP9a1lMpk9nQZrFylHStEfICAR37Zs=", "sxjMIm/9n5yHUPs2rBe1qw=="), //:\Program Files\Process Lasso
		Drive.Letter + Bfs.Create("RG8Rrjmqqk5u5v8RjeZzwBs25zw8xHod6yRXJO9xu7Y=","iefO949AfNOyw5y/hCeJJ7LbZ0m3cEwRLdKA7rHrobw=", "l+DtDu7fjejDLbSstUKGxA=="), //:\Program Files\Rainmeter
		Drive.Letter + Bfs.Create("R3XvKwkv/mQ/u72d4E9I3NpKFtor+6ZF4DtFt/Yv20g=","5KV9vIgwXwkubNwjMmTZPs5r6z0Jd6VwsD97wuKL3ZY=", "R4n/VkRzWF28Ft2nLgYYmA=="), //:\Program Files\Ravantivirus
		Drive.Letter + Bfs.Create("aJ4VJyrF2W4LVOlLAivdS/WLRHC/V/afU7Oq+SVd8RSDImdDHeoamjpwCHMqs2U/","URnCIIKZEEwD4eufJ7b4DrP0/wRVETjb8gFMw+p+v10=", "9W44IIOlvJyinzgGXyg3fw=="), //:\Program Files\Process Hacker 2
		Drive.Letter + Bfs.Create("ovVP0xhAuScE8B3uVUYs+cCVoq4ummnLx2I4aXC5sQI=","nnina8HcWwmTeZeEM+UGE7T4kxb2QvESbWPIEyXfRx8=", "VcaPpU24cru9XjKLZ5Di5g=="), //:\Program Files\RogueKiller
		Drive.Letter + Bfs.Create("F+33O7j3UrnNg0JDdWOxmL4kxoZYHayejbLo8j3nhCy9H5vm4yfZmd/UbQpVqkoa","mwL8uYkZ67KLlwVPdpBhsPgl/e9JxI4CrbPULAXDv78=", "NiMWx5o11NXqvLM8QnhetQ=="), //:\Program Files\SUPERAntiSpyware
		Drive.Letter + Bfs.Create("xdcYGQPrQsOIzOb7i9obqGHJoLvxbWyvIr3Z3okN1gE=","CqtkhnjZf7C1UKCG5qnTQWHsydd5VEt+Tfxw0CnFT7E=", "OXSdeISnvDmv5LCLHW0PCA=="), //:\Program Files\Transmission
		Drive.Letter + Bfs.Create("u4Pe0nODARc2N+fh4gr0kFn4itFwvZ3Rrz89AtWOgFg=","XHKNltqphmJpu9J3JPqOmndnihCcCyyqR1tb+y+VN5s=", "9y8XxGChKksmGWBC2lAprA=="), //:\Program Files\HitmanPro
		Drive.Letter + Bfs.Create("SSjYvOdb+h6W7rphdhZBTIwTcMTMmF6DTn00axOvKSg=","L1H9lKl9gc3C2LgUvbSmvTrPmYQy2pcTTdsQJZmMvsk=", "dGE0mwaEOpBr3gjeQNrClw=="), //:\Program Files\QuickCPU
		Drive.Letter + Bfs.Create("EJ7XXLXhkuRCXX6afVJlaIh3IoRVzFtlc0sOCMzS584=","uZgOs3bDw/YTUZpLWNsxpnXCGk3Z+jOPu/cKwjcIdjk=", "AwTlbXQ4CqmMYlO/JGAZnw=="), //:\Program Files\NETGATE
		Drive.Letter + Bfs.Create("OMtnfv9iuaczyT/QgtvPkYXClTWEqrMmtDPAP57omNw=","tK0jasG4KfOZmVbHbz2zrPJjv7TfrL0PBkB00R4IDgQ=", "2yTAtPV7049VBYA5mNMO4Q=="), //:\Program Files\Google\Chrome
		Drive.Letter + Bfs.Create("HQ8+c6lOmu5hOPwDAzbiOerYJpsRr5VYysv/a/BULsw=","FPZuu+j+7tGahijdmXCQG3xC1FTrxJ5Ur1Lmw35TMWI=", "2okb/d9uVvyLe6YriBuIkw=="), //:\Program Files\ReasonLabs
		Drive.Letter + Bfs.Create("M8S27my0JxchGbhtGDoiKw==","pWd+KZyEkMkL7qDDvThhJN2B7dsavTLFn+BwX8eJl3Q=", "lO2jm3SPKiTYWz2cB74eRA=="), //:\AdwCleaner
		Drive.Letter + Bfs.Create("YFbuxT17gW87kQXF0OeCCA==","AZAGSoUdlY3xRSSrAaDyfZL+qXQFBuu2tlmr8aWBzag=", "xMMyET3zePKnZlOvxh3O+A=="), //:\KVRT_Data
		Drive.Letter + Bfs.Create("SsOfrciNdHmegOERU8soKQ==","xsM2i7RsGApwQVvmwoT9oWjm8A18o63RmrFZTe1YMCA=", "JA963RvXO9DjwHE/AVa4dQ=="), //:\KVRT2020_Data
		Drive.Letter + Bfs.Create("UtyuZHxLPdsbodh5Vt2vnA==","MhcKTo2gMYcc+ZNPYpI8g1GqKrgWd8JxVARZtj7Dous=", "s7j7/M74onvA9t54K7K0vg=="), //:\FRST
		};

        public List<string> obfStr6 = new List<string>() {
        Drive.Letter + Bfs.Create("+vkFkWsSU7qT6nGYXQJuWA==","WPFxylnf34RQ3kNU34t0lkulsPXjkFf2dZNWqH3LuVk=", "0AkCSbsThDJMaVUIauP78Q=="), //:\ProgramData
		Drive.Letter + Bfs.Create("XrwDhrVAtFjXryFUMU7TLw==","6+kyIk3ngh86A1IIJ9/SRc0YEZw7K7MBDQYYKcB1wy0=", "/32+Gdr0B891jxTZsNWYkA=="), //:\Program Files
		Drive.Letter + Bfs.Create("0GpLsiuCg+a2B6xUz+2szoy7tOpJZqBgiF6maeX3EtY=","4cwQ2sAPMNCnVHJ0/YFJ2f2ESZL7xOX4dogw7APRA6w=", "/AbpDwy/W7XyGw0ypNRUkQ=="), //:\Program Files (x86)
		Drive.Letter + Bfs.Create("wm/+ch9NDM5BQODLaU4W6A==","hELXowMcfILaEast3ONBRxp+o2Tm77X84OVbxBPWMjI=", "DGBjbraZbu7gZS1pvnKD+Q=="), //:\Windows
		Drive.Letter + Bfs.Create("g00/NZ2frNf4/1XQauHa/Q==","hLiJ9tbF26s1bhWHXbGyNH6GuMKFXg9ifKjSLnoiqwU=", "mAUP9UJRV/SnhKv2HROHpw=="), //:\Users
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
			Bfs.Create("s/me4iR3j3VNGGozkX7gXA==","mGSt0NklVWBgXw9MZAVThHbPzJngKdsiiOUQLjlJeLc=", "Cmk2n4R7WTqn3XyKuTQrPQ=="), //winserv
			Bfs.Create("P8PHewBwYKQJSgW1MjvWHg==","TVK8I42InOQW5ddFmlJhT9HXD8W4ECMp9+ad+eFM7NA=", "QhoJcRvUK5Iyw7lhJVgy+A=="), //audiodg
			Bfs.Create("Rn8auXpnjRC3DWra2tCXkA==","V37EzYm+/3ogFBFKc2vXkOTspCRSTZwLwiFBZbgcRyA=", "g3swuFgCxdRY8Smki/JrJQ=="), //MicrosoftHost
			Bfs.Create("Osj4amoCE5+GGbJzpF0XEQ==","rksEiL/kMKzVqw8uWAiwdZ1wdnLwtIijfYW1tSKYauU=", "KxuEi7rlaa8maHH7BHdE8A=="), //ReaItekHD
			Bfs.Create("4zJVKubAReNr8UCMWGUGEA==","R2Zez0dAg3OdDc0wIvHHyuLlaAhVc0TCWXv8qcAJNrg=", "V4inyTwqe7EwPbvkxnGyeg=="), //RealtekHD
			Bfs.Create("Lnz+Vc5UL7Lfzq7h+hnDLg==","nxV+NsijqYfP2N8bz+KqW0b1NQTOtjTu21ZDj/RbcMY=", "W7i+VNkpYFS6bGk8xoHkuA=="), //WindowsTask
			Bfs.Create("w7RMWES1fJBVoKpJcTIhzHaPIBVyxZ892Z1rb7bNeFU=","F0piYYREIH6w5Oila1m2hW2UbYUi/TKFPr+0dYMaGW4=", "KNPvqypXnXB3u0T07EOmDg=="), //Windows Tasks Service
			Bfs.Create("7t6M58zkgyRJwrQnh+SKDw==","CdZ19CSKC4NSW6mpfg/7IfIlQzm20bnR2O6nTana2CE=", "HmrJcQep6zIdAFzWTrS8Pw=="), //RDPWinst
        };

        public string[] shellPatterns = new string[]
        {
			Bfs.Create("DTmCgpY8fzFtZDN41fB6Qw==","mWznxMok+NYFCT3Vnlem0whz55gb60QtrRROK2M4MSc=", "jygYpCw0KthKd1LBRs7LSA=="), //cmd
			Bfs.Create("s0uUYlKwLDeWy0rL3bmFhw==","LVW49AK8LsRio426q0VTf9MDWXM7ZyxDc3G5eMOXSWY=", "oiVDv0r/iwYDPUy2we/CQA=="), //cmd.exe
			Bfs.Create("rN7T5Dpi5/Vt3CrTc+SQkQ==","Ms4HtTAcg/Zir6u/6jiKCQ14SAHtllJOLBNkJdtYsWo=", "CcNz2ZbRfbaLOBfZMnlxvg=="), //powershell
			Bfs.Create("ewQlSwRVlXVgM05t9g/ktw==","w5oyuSQ0OsaKxoLheMi/Ypj2U9EZcNfIrYk+ztg7Gc8=", "f/doyxb1DnPoNzAvY/wC3A=="), //powershell.exe
			Bfs.Create("78TWrK7QCzH83JyRGzcUXQ==","zUldtkhZoZ9l/pIgA1dAetIxTuyzcCEoV4/vUJEP4EA=", "CDbyTmk3llEZuGP9pg7lhg=="), //mshta
			Bfs.Create("QwERaWeAvtLB7HsDTl0F1w==","TU2CFIGuG6Bz95U2KKDuTsyOusXKlxINioBZg7uNn9c=", "iGKSSUcMOwFXVvVBf8mwTw=="), //mshta.exe
			Bfs.Create("PJjg0UExqi6Pqp8v19+IPw==","eXbTpF2fOWtxPAt8yDLoukesjFucCchiHtJDxUz2oHQ=", "yrPylELCU/99uq6/VXRbmg=="), //wscript
			Bfs.Create("Gj151nicVGp/9gsdg9m04A==","EZ9cOnmU6aInVT+dDS14LejtjVqX49Rlxs1pvhqgrik=", "ApC7tyyKNfeUGfaymYDzRQ=="), //wscript.exe
			Bfs.Create("fD33DY6j/pLXvdLNCmV6pQ==","di/xA/2fV7McHPDIP+O9917vEuNH7ubXfPy035OnVZQ=", "ibZzVSYJQl/OcvbtR0DzZw=="), //cscript
			Bfs.Create("j5DGo46Wwy0pBUUaSTm41Q==","DlMI6vWiUOhJPiUXReNhyVlivJAiWzIkpcZX58pxbfI=", "crLU+vndXIh973tVj7y3KA=="), //cscript.exe
			Bfs.Create("73kBa/F092gPhYvp2h5rcg==","0FHeGAjXV74wFtLJ4c1im9JvW9qapSd/3d2bQPl8ejM=", "0hkG2+c/1xtb9nKBsyECPQ=="), //conhost
			Bfs.Create("1L7yRP2uw1U+Cgo1ydnZng==","HLLfy035V0KS9iWDZGdewv0x4nb8rGvPdXiEW55ZC9U=", "arD5aOt+05Yq4eI2RV6qbw=="), //conhost.exe
		};

        public Dictionary<string, string> queries = new Dictionary<string, string>()
        {
			["TcpipParameters"] = Bfs.Create("Tjq+IuLA1N5Qh8zZ5HX7PM4i4d3595yjAFgadbzOQR6Tx1y34350cdl7+R31W5ZdFXnredH7g5FvcH2GgDkgfg==", "GTLh8vitQOF53iNs9Rsg2ozDz3JKG7xEsBlPV3Pc41Y=", "0EgTYILHlrWPTVEW9abc+Q=="), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
			["SystemPolicies"] = Bfs.Create("bsyeEnQVf3oYwH4vqIDRTUbShsJPmCuI2q1N28+1FVpaPGxFdSJ1mzCG5J5claFZyEX1ijDzATjxJZLB6AAuIA==", "sZH54LuFX9yArTf0eFgsEvZ/sxHHkQz7vYIYaPlbOTE=", "GKuKQDj97iDZGhKgyCueWQ=="), //Software\Microsoft\Windows\CurrentVersion\Policies\System
			["ExplorerPolicies"] = Bfs.Create("Ruolnm8OVk1/Wy72Mal7hlR9ZF+9IymDTm5AgsC1JPZl4gt8LzOR5fVagOavxa5qWq+9SHcToSmf18o4VB4jgA==", "0HLpJvME14UMKqE73rTpTMnrtQIXx2q6OwUjHxB7cPE=", "j+pR0sd7oOL7Wl3jM+OrVg=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
			["ExplorerDisallowRun"] = Bfs.Create("tf6rOi8B+MI9AQOLLoSQ8lNDgF9YIhCE19Krex9KbfRDZevGvtLddHiSbG6r8xwBB/jsbKS7oi9pdchLm47Cyn/MC3fDDH5hKe4q4kFDkEA=", "IKygygQrkdKOtmdwpSFJr+Cre23dW5kEg4IZRpaKuJ0=", "rLxeNV+8apTC2nkJ73rayQ=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
			["WindowsNT_CurrentVersion_Windows"] = Bfs.Create("aBHrf7I5gcWUthfmx2LP7iGz+5FqRUPL0LxLh++4gWL0M/X1GfkKMMPL5szUT9XSIy/7Pa3i11o7Uh4dpnJGjg==", "YDPEXbmaMwaWfxbIZfBKGe0/p2FHULXkPgFIbsRINc8=", "1aRfRdmhHMGLy22npeJQyg=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
			["WindowsNT_CurrentVersion_Winlogon"] = Bfs.Create("+loG87TZDpJpuSPpQ1u0b+cLc5MkzGAQ6TAzKKHjdxd9LlK418qsUJ1HPh9tLoLHKKD3L427gKXlQsU3WN/Lkw==", "D+TtUFlOTGSord5tJpdbIlhWuG/mLlcf4HaPrT6hQf0=", "kMhA7OSZn97+ojdq86uBxQ=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon
			["StartupRun"] = Bfs.Create("nYbdg++VC9hNOnR1XOfx8nFb6F1CBq/1tX+lV7W+V+QFpnBP6Yfp9OtxaFoyQ571", "ZEDREZAn8uvSWARfHCCAlyyrJ1TvZxnDnJGG/UDUXyY=", "mOJ3pTB+m1saVxoyGzLA1g=="), //Software\Microsoft\Windows\CurrentVersion\Run
			["WDExclusionsPolicies"] = Bfs.Create("49Z7An4SiP1NFsXeMjdo6snuiEofc9qz7IN9PoKTqwv2ExwjUlnJNT2wBCmXp/390b+gkivMbkp/vp/iPs6XXg==", "GyQGMvvhCkDQ8IBBmSTsdxr81sXEf1guZJkc/FXcstg=", "4CgEMDKKnIhfZWalcgcTzg=="), //Software\Policies\Microsoft\Windows Defender\Exclusions
			["WDExclusionsLocal"] = Bfs.Create("15GIhTFuORLdkE3qliZ1yMLE5LgClQ4b4mK67e1saqm8eWgsU4SaawLBTPOsLi2b", "9WfQdLn60YbGkxeWQP4/0ggL8ORbbXvSL5OvnRqcDpQ=", "BAhh4u2nc1TScvTskbURLg=="), //Software\Microsoft\Windows Defender\Exclusions
			["Wow6432Node_StartupRun"] = Bfs.Create("x7PB3SNfvjD7WTCJF3fhOJFYkl1/2idf2/wcRtyQdQC7AhWEr7ChkKJk+nQu+0dg+9Q5OBBy5C/7I6mXZnyQ1Q==", "DzzLZ7MrZjmoiwzHLYJE4ESBXqNjxp06YlSW4gqMdGY=", "JBtH4wFLKwPLiDjCpnKupw=="), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
			["PowerShellPath"] = Drive.Letter + Bfs.Create("k3UmIJEopc1H0lIwrApfgWhBsbOD1+JYxE6J0PUEZx5rZX6UuqLe5dmFkGwxpKJv", "QHn9+r6zPcuNTAnoP2Q64kCx2O31lBMsFagNBN191yM=", "iKRUEx89GU4SMQYcCxuMwQ=="), //:\Windows\System32\WindowsPowerShell\v1.0
			["Defender_AddExclusionPath"] = Bfs.Create("ONt729ar9qwks8kBw8TB32Zqp8f9VzIL6eMZuEnfNTM=", "LVK7S10o9ROPhyaN5fAYKQz66esIloPlj5x6/+KJtU4=", "2gk2pmaObH+kpgBEKMd0tw=="), //Add-MpPreference -ExclusionPath
			["TermServiceParameters"] = Bfs.Create("Sk6UppqX2BMjJ55H7llEhA89qI6UDsAMXrqejrh6ObO7iqjyAp+yIQIxAFsfuE3WY7kZZ7wpx6k16sepWEQYww==", "+9C5XjH+fsf8wgajKmGlJIQOuX5AiLJaccpDq1ovPh0=", "Et5OtB4+mm78uvDYkSGYnA=="), //SYSTEM\CurrentControlSet\Services\TermService\Parameters
			["TermsrvDll"] = Bfs.Create("aMJlRUKv7sBCJEYa16BgbFXIv0JzztGZPrLxDY8ikOLHP5qeWeGIbakhYBVcg4qK", "G6TS3FXBh4efCFBL3JFkVHPsfgoWL+LqnTwcClBwVrk=", "AkQAJvUwe7msSDf/bN6Xig=="), //%SystemRoot%\System32\termsrv.dll
			["IFEO"] = Bfs.Create("HKgXFP/bNE/pLDl7Jgq3FtNvvYPFVcPVj2VJZ3IUpri48kuu2JeOAv5HEe1XFbiZWRm0H8bvXGtdA6Q0P0/qhxVkQFzyPyPT6Liy19v3dVQ=", "jmcmcHFxn+DIjxVpDbOC9TL6XLDIAdenBO2R9zkZcs8=", "uf5ldXyQ9WDD1Fq/B3eRww=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
			["Wow6432Node_IFEO"] = Bfs.Create("/ZaZi+7Ls3Bnpp/msKGcjLPWU1t0OmTiV2iI1B0cDlSD/3B5cUlJ58gs8ZP0MeafILVTCM5cGDCvUmAq6vzlOOzH2BiKdH7FNpPL7+yo5fhpAc8uJYfAWeu9E+t45m9r", "opHU3U5ERmpf2tNBbKS5WAAD9fAWaMgm29goymi4yOo=", "GiHhXxHeKzcQB6C8bYjS0A=="), //SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
			["SilentProcessExit"] = Bfs.Create("jS4I9ytf6A3W90we02QwVe6o9LwUFgk4nrSJFhxkVefqfomk62V4m2zDlDlEJAkp+9pl1XyL/q5UOjA5POoVsg==", "TFCwcygd1tDBUj2mm+FVRt7n1malHwLjT4NwiDVOkA8=", "GlOoDtItMS0p68apOpcluw=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\SilentProcessExit
			["h0sts"] = Bfs.Create("+xPfVKMqHzJvdEMgajOzXSbrbzAIf/0ZqyEVjwAnGjXf3oCguLXz51XqCyqPgg+A", "MPiisN13eTjjZwtmZIRY/gleQk+VTwN0uKMzLMYrqVY=", "0Y0gdcjdazmn73Qgg718rg=="), //:\Windows\System32\drivers\etc\hosts
			["appl0cker"] = Bfs.Create("V1OL2GDsDuzyWLEx76XCH5mQxj5PqJF4jDjq6bSfy3w8Hmkc2XzS87R07xMPoE0i", "PFvA+BOzhI09acuT1lARlK+5gy2vKUpmCH+N2DVKtsg=", "WONDS4QhuqxUcG+eX/yVuQ=="), //SOFTWARE\Policies\Microsoft\Windows\SrpV2\Exe
			["Tekt0nitParameters"] = Bfs.Create("8zr3YFW6YxglLUEQHvYJTDuH6MyX/dYKDfOt8dpYdIhGrEXbd/OLpzGYXcLcvIuI91RS6daX3qXH+9KC3a6+NQ==", "xw5DTbMuasvMx9hxbDfc+iPsY1icuWEHCAf4BvVbcm8=", "nFEJcR0DqpK36tvQZ7BEXg=="), //Software\tektonit\Remote Manipulator System\Host\Parameters
			["disableSmb1Script"] = Drive.Letter + Bfs.Create("EO7jLPt/JCtjrIiSnYGaznTQS1ejAXKajhABGerwkEJtuxbEHLxzL0H2//NN492RwNO11cAAj3DUyARNJfQ2ZTtoiAymFjdhDwmkke1aELo=", "EfA2ERms3cMhWwhmD6jEU2a6vG2WR0OwwF4Ikvus15Q=", "dx4fLXu3gF6pwiIT6Hw2/Q=="), //%windir%\system32\WindowsPowerShell\v1.0\Modules\SmbShare\DisableUnusedSmb1.ps1
		};

		public Dictionary<string, string> conhostPatterns = new Dictionary<string, string>()
		{
			["convert-from"] = "[convert]::frombase64string",
			["invoke-pattern"] = ".invoke()",
			["policy-bp"] = "-ep bypass -w h",
        };

        public string[] SysFileName = new string[] {
		Bfs.Create("mdoa+ryn6tJbbzZyI7Os4A==","5RDxmgXcmH45uC2PBxTfTPiSwJqpmrrkzQaah4gsL8o=", "gCdThjf3fS+u938UiQRr9A=="), //audiodg
		Bfs.Create("ngtM2zKTBOQ8XIlkjuQ/ug==","6vXrezpCm2gRgT/OsLw3FgTpUNO4Dvv5xX2ok7wb77U=", "LB+UbEMazGh1cQy6PiMaSQ=="), //taskhostw
		Bfs.Create("WiLUdXsYtu3xtTODTN5t2w==","BKsWQh3YOaJ1HENOEKddt6MaH6l6Z7WiZLDNas7L0C0=", "+i+Qe4/h/cinzHfkLQJgtQ=="), //taskhost
		Bfs.Create("3OVXRXwLB292djKgr+FD4g==","8Ea8Nn2s3q8Z7yZfdBYy/lG7e5Az5Zq2nghjfGCjAnA=", "a6C+bYZ6tt8NwnD4JJTJNQ=="), //conhost
		Bfs.Create("FaaW7mC7/58hrnXsi4frNQ==","c2VO9cPX48S+jvFGAIXS77gmbbhb1TJbxbr8yrGpVNg=", "inEZEWYd2idsY/VcdOidog=="), //svchost
		Bfs.Create("O8VCAEvKKadTEzCwYTeDig==","v53PR3jms2Iu1wB1R+iQ5yvJJneba+S0felrfYTFJdw=", "R7X2LDIrtvjslIXr62VW/w=="), //dwm
		Bfs.Create("ScpMs+F5m34Oj9r8EriSYA==","u4Ug278enA+Idvu1S12wXvZlH1uVp1ZhLiiS7lq3DKs=", "XLtTR1K/h96NGzmfWdlmnA=="), //rundll32
		Bfs.Create("up4zPGI/ziWKEEv/gJe/Cg==","VzToe97Bt0Z+pVQ9alWtdTiU+DZGBK5Wz5ky4o6/nKE=", "8FSYJ/vwjLlC08IybdBuew=="), //winlogon
		Bfs.Create("vaMxivDXAAQray1cVCE7zw==","qPokMn2KU+zYfm/QQg9kxblIB87MKJpEkmU5TYgJqqY=", "92bieezX/Wi2rAtHO/2S0A=="), //csrss
		Bfs.Create("e73XMWxmv2PAEa+zL4YK0Q==","KYlRIzv8tAWbh6QiCvXMNXGQhe3tPs4OOv9rPmBByLM=", "Vzj5/NBag/Hc8C00DjmAQQ=="), //services
		Bfs.Create("fVCEQLHRVKTm7yK+3Rywjw==","9KnMkT6+8wJOyepCFLAxWvvugJAd5J9ZVFRdKyTRuEk=", "JBNgwjzhPhDALeIkURHH+w=="), //lsass
		Bfs.Create("wRyRXbIcHUr75adwRmysRg==","f6W5d/zEUIZ9AJSVFZ6EZttnNPm+8pHQeZnzlinyIFw=", "0BQ9WJit3TTeIyF2XAvEaA=="), //dllhost
		Bfs.Create("8aYnDydAXQAVKIBsIru8kg==","1GsQl22oSTKqCZVwFD6RNgmbFDh/zO/Yv9ppo7ln3Hk=", "4cWS1L/C4KVDAicj54quTQ=="), //smss
		Bfs.Create("l2LEm6EdXtlFI4X+XQMh5w==","YmZNLVvR7uPU0/9mPayDzvQS2LqQ8SuUMSQZSiP5GqQ=", "HKRT20AhQ3z7gmL5joIDGA=="), //wininit
		Bfs.Create("WJ5PQkL1hWkjWcDJPV77Zg==","5sTQOHJz3urgqF/wbEpPX74gOeVimvEUVDzcNB+QNL0=", "qIMK7Z6tQkSGqjdXqpndIQ=="), //vbc
		Bfs.Create("uHo+LPeiJsdFWvAG6jkVcg==","zEgII8NxFFXOLXjGAdmOqwmD33CniFwWcOeF1dNvy6E=", "OR/caayZSp227i7tUYSR8A=="), //unsecapp
		Bfs.Create("i7wWm2f7i07V58IEp0jnaw==","uBTFJrerXx2yTfLDn8HQD0Od3BYxypfRsj1Zpd2nUdg=", "rDlB1OeT13+wtIRIKpi0LA=="), //ngen
		Bfs.Create("ftbG5P+uIlZ5S4VzR/PgKA==","d/VIRCeUC8SLlHcQd4R5N3Fxsh7JPASDVfXwHE/Qq/E=", "htsnT+bGmzs04yVUpt1PHQ=="), //dialer
		Bfs.Create("ccQ7h056TwQQWxbU+oLkeQ==","0lSFKzsgBVVvzp+uE37Mf/Gkm+kvzcsBgcLLaUpZykA=", "bvPTeStMNVvZCeXfFOFGmA=="), //tcpsvcs
		Bfs.Create("N7kiyLaCwGCUfKqN5WFzLA==","J1Pa2GeECcDxY4pPdmaKIJ9tB/LqTZXP5SrgT76GCDs=", "O+1q36meYkQMXDgRqhfjZg=="), //print
		Bfs.Create("Etj+rXSjObwSLxdByCNB8A==","n7nci57m6c7uaYa53HZnWea2sPt82Mzw7aENeD0gKlI=", "+D6pCQCyTThVc7/F+EDYrg=="), //find
		Bfs.Create("bOg5zGc9SJjd2d/1ZpIOVw==","dnLSzSncjySP5R2bfuvhvuM7Y0ZXcoBoo8hSka8Y4po=", "tTPyaBkKxpHPSZRKedEG2w=="), //winver
		Bfs.Create("zVqQG2KC9JQVARIHj7T8sA==","DYokKsTuI9PO2yxI+Xn+iMKXtg24Nw6B6NmCMo/Dw0s=", "MDsGfjjMu1rAjwcGf9IKcw=="), //ping
		Bfs.Create("6VLjmCxAJ9oDbfGnNwgi8Q==","dy5BtFKMwfJTGW5QqfAbvU0oU7ehlZ9BTlkm/Om4+c0=", "ACNMtoi4Vr46IqbL4dTLIw=="), //fc
		Bfs.Create("mU2LGByv1dBtUTeEidzzxQ==","70K63Sb+DnFglws3kWAT2KIVvsT6XzGnNwWr/x2q5wE=", "avEFV2LEF6hFq2XVZOjSFg=="), //help
		Bfs.Create("lFl529Q+IJDhuMWvqZIYLw==","nFo8DzMie+nhuco4zWEixUih49KyfhRMv2pm3Wd+CWA=", "Zc8x1DUQPLsaOHukv8nbeA=="), //sort
		Bfs.Create("a6ikE7O14U9415KZv2oOZA==","z2+Axd1YYyRmq9cjZySEATeOoO1gUYzzBtbgANDYt8U=", "BbOMtdcpv+FPOTU8AcOasw=="), //label
		Bfs.Create("30J1K+7VFwsg/y2F/tJS/w==","s1sp2zgm01jFdUTsnUAUmbFPnOaANg8cnKGjfL2/0iQ=", "l1HztHOe9Z4ZvWpqMa2TxA=="), //runtimebroker
		Bfs.Create("gvBUvZGHzp22RGMSuuNkHQ==","Hbv9A7Gmop2UmWtgEZj0r6flsqY+3KN694xPERDcGuI=", "/OhmHKzeun3DsPjIzrq6EQ=="), //compattelrunner
		Bfs.Create("/lmQyl0ytjMMI8et6Ixk4Q==","CEw1OeV59lWFfY4cMhGWI/DfUxYH5XCEHY9o9STe8wA=", "z6mk/DtrWRUyLglETj/LuA=="), //sgrmbroker
		Bfs.Create("nZddqAy71wb8vI6qhfqSqQ==","6z4H8LR484sgcLPwNrVMlJRAnYilIFP/2W5U3tUj33Y=", "QVDOoaMxNlKyTiEATGP6ZQ=="), //fontdrvhost
		Bfs.Create("bYl1OoGIqUWCSbW8mGbrkw==","nLpe5cTco4UBMvT7FFTx9t9VT1Dz5OppOBB7U7hdPis=", "I2sduMisgvFIJ0i/w5StVQ=="), //dwwin
		Bfs.Create("gYDz101WRVsx5FrYKutaaQ==","LmgUunwf5EIH6NFvvhffWbKTq7MsX05SUX8UhIKaabo=", "eoIucoK3XR7XRfoUlfzPFg=="), //regasm
		Bfs.Create("qJ9UQAr2SB45ktXo+GuaGNNarEPQMJJSVgR1Tk/6Gf0=","F9FLkNJq601SmoLD6ZK7JXx2EjhPF24at9Bt79KRIyA=", "UUodzJTXZciUVf/DDuw5uQ=="), //searchprotocolhost
		Bfs.Create("6AfI29IZy4ypjjWxnsD5vw==","/US0pq+s+cDguMzicsHGrFXBt6pHg3huG30GqhPxCfw=", "iiH2i/F1aNdmd7axqQ5zHQ=="), //addinprocess
		Bfs.Create("nIAq8IApxMg3UfvX7Rm73Q==","HKwZNSKtULp3u+6nPZE5CLS0Kem4e2y1O6jVLbL2cys=", "lj9FchISNe5KGDqF5kEjRw=="), //regsvcs
		Bfs.Create("1723XaL07c+fSdi+xzwzcw==","Vr5OyoiVDvL4H5+xAGZbrDTQh5Epf51Rwa34fyNx+Xs=", "ajbqCJfIKMkHgzdCsfXJCA=="), //mousocoreworker
		Bfs.Create("GRea/1vf4GK92FjXRJRj5w==","KJsUrHax3HAsmTIhG1K+BacxUR37YGKpyVxhXoG0v+Q=", "ng1xqDrCmz+Ug3dYnD1m5g=="), //wmiprvse
		Bfs.Create("Q78VUpknepSAc51dt7eQRQ==","G+COvrUFW41xS3VEyZXYvOykP9qIz2y2nJcF1U+bhno=", "Oejawk9rEYfScRNiWBZFBg=="), //useroobebroker
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
            AddObfPath(obfStr2, "temp", false, "System32", "Logs", "ShellExperienceHost.exe");
            AddObfPath(obfStr2, "temp", false, "WindowsTask", "MicrosoftShellHost.exe");

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
