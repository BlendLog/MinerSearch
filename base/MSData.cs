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
			new HashedString("e00662fd56d5e0788bde888b0f2cac70",7), //avg.com
			new HashedString("f3226bd720850e4b8115efc39c2b0fe9",9), //avira.com
			new HashedString("60d2f4fe0275d790764f40abc6734499",9), //baidu.com
			new HashedString("348ccdb280b0c9205f73931c35380b3a",15), //biblprog.org.ua
			new HashedString("1fd952adcdbaade15b584f7e8c7de1e0",15), //bitdefender.com
			new HashedString("5c6cfe5d644fb02b0e1a6ac13172ae6e",8), //bkav.com
			new HashedString("eb401ae50e38bdf97bf98eb67b7f9764",14), //blackberry.com
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
			new HashedString("683ca3c4043fb12d3bb49c2470a087ea",26), //download.windowsupdate.com
			new HashedString("683ca3c4043fb12d3bb49c2470a087ea",26), //download.windowsupdate.com
			new HashedString("84b419681661cc59155b795e0ca7edf9",20), //download-software.ru
			new HashedString("a65eb4af101a55b3e844dc9ccc42f2ff",11), //dpbolvw.net
			new HashedString("1e0daaee7cb5f7fe6b9ff65f28008e0a",9), //drweb.com
			new HashedString("98d3a8a27234fa519e04907d7ace9ff1",8), //drweb.ru
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
			new HashedString("b8d20b5201f66f17af21dc966c1e15f8",13), //free.drweb.ru
			new HashedString("9bfeda9d06879971756e549d5edb6acd",20), //free-software.com.ua
			new HashedString("867692a785fd911f6ee022bc146bf28c",12), //f-secure.com
			new HashedString("c46cfad9e681cd63c8559ca9ba0c87ce",17), //gdatasoftware.com
			new HashedString("99cd2175108d157588c04758296d1cfc",10), //github.com
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
			new HashedString("13805dd1b3a52b30ab43114c184dc266",13), //nnm-club.name
			new HashedString("4e42a4a95cf99a3d088efba6f84068c4",10), //norton.com
			new HashedString("84eac61e5ebc87c23550d11bce7cab5d",17), //novirusthanks.org
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
			new HashedString("ec532f0313071cb7d33bf21781ec751f",10), //sophos.com
			new HashedString("5641840b2116c66124c1b59a15f32189",15), //spamfighter.com
			new HashedString("39cf9beb22c318b315fad9d0d5caa105",13), //spec-komp.com
			new HashedString("e56f530f736bcb360515f71ab7b0a391",14), //spyware-ru.com
			new HashedString("861cd2c94ae7af5a4534abc999d9169f",13), //stopzilla.com
			new HashedString("90711c695c197049eb736afec84e9ff4",20), //superantispyware.com
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
			new HashedString("2e903514bf9d2c7ca3e714d28730f91e",17), //windowsupdate.com
			new HashedString("6c1e4b893bda58da0e9ef2d6d85ac34f",18), //wustat.windows.com
			new HashedString("5fa4d0d3dc665c270e1d8f4f36742398",12), //www.avast.ru
			new HashedString("80b73c20690f51646fecf5bedd00f14e",12), //www.avast.ua
			new HashedString("de6446136e6394b2b9d335cd3488c191",25), //www.besplatnoprogrammy.ru
			new HashedString("ddf153fb8a8aefd506b182cb8ede597c",24), //www.bleepingcomputer.com
			new HashedString("54cc7b8155fe3c550153cb8f70214343",12), //www.comss.ru
			new HashedString("1a01fc7cc8de2fa07c52183572f06ac8",15), //www.dropbox.com
			new HashedString("82ccc585a90ff5da773ed6321e1335d4",13), //www.drweb.com
			new HashedString("91c394760272fc16c952bdba553d3ea6",12), //www.drweb.ua
			new HashedString("2f814f460634c256b37b3b827abbf81d",16), //www.esetnod32.ru
			new HashedString("176fb162f5608954f82fbf82f6239860",15), //www.greatis.com
			new HashedString("205081240db0af1eae2b071aadb85bbc",17), //www.hitmanpro.com
			new HashedString("a2c665f4f9d1b72b6cf88bf0ec3de52a",17), //www.kaspersky.com
			new HashedString("3277391ae8c21f703aedfa065382025e",14), //www.nashnet.ua
			new HashedString("5a6822824a14727fd67a75ca9bcc0058",18), //www.softportal.com
			new HashedString("f360d4a971574eca32732b1f2b55f437",11), //xcitium.com
			new HashedString("05dfd988ff6658197a53a559d03d48d5",7), //yadi.su
			new HashedString("2b001a98c1a66626944954ee5522718b",10), //Zillya.com
			new HashedString("686f4ba84015e8950f4aed794934ed11",10), //zillya.com
			new HashedString("34c51c2dd1fa286e2665ed157dec0601",9), //zillya.ua
			new HashedString("8d9b6cfb8aa32afdfb3e8a8d8e457b85",10), //Z-oleg.com
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
		Bfs.Create("UVy10RKitq9fp1X62bXKnw==","vTH1P7kQvElpBlFWhB56RRHcHp6jFM4ggBG0Qdd1ij4=", "a85ReNcDu1lm1k/KrVBkgw=="), //ads
		Bfs.Create("FUm6HYi8hMYTnaPiVr0TDQ==","b+mYbV5628QdQz6kfR41PFVhUZ9AaYlQkJJ/5oc+plo=", "GnEcNYEdFJVOKdx56/Cuew=="), //msn
		Bfs.Create("B5dVqAd7LBKe/1y0EENCwg==","kz4ve4WOt2zL5WRl9M2rb6/4josYK9GnX9DmAhEYGvw=", "tKhe6L76dOm1wlbqP+GCRw=="), //ztd.
		Bfs.Create("QzMPt54PJ91P0Z9ny1pGFg==","DI21XR5ALdpb322lwLYVpScDY1rZ0FMTeV4YthvFZkk=", "BlRkaL681dfcgQ4Jvk8p3g=="), //aria
		Bfs.Create("UZzyNkaizMyktKzgHueQdQ==","7HCRIuuyQF9l+W/wytmvZCnZYo6SkqsjPuwDkuzG7GM=", "DJZtw0BkL25LpOY8wDyD5w=="), //blob
		Bfs.Create("nw7T56KyNgCHhMgaV138YA==","OrspgBYXsfCiBwSCmwrF55WoNK8L2hF+bl2xbsmhfQ8=", "z8uGhwIu1n6sET1ZhTeqvA=="), //llnw
		Bfs.Create("qTgTLMTOjoiHzO9tijmR5w==","ye/n/zBh0BUaGYKcKxyCxG//v2d6iHZHxtI6e7X7GS0=", "FRkojpPSV33Zr65+42mNwQ=="), //ipv6
		Bfs.Create("MMF5vl+h8EWzvZCnZaHFyA==","Xtq++61M/2y7tY/nZWM6yOOWN5bUKqawhf/tvkYUxco=", "Hq/yRP6TjO0Bdx8Lj5dJYg=="), //.sts.
		Bfs.Create("EkDoWqbE7spmxh3AZcZhxA==","qFfBP2UT5dYt0v8xGBHcsdjluTQE1Rb5epzoQHNjsy0=", "MBygxu55e9jrb3Ul8/0+FA=="), //.dds.
		Bfs.Create("tG2TjKOkmIEywlKlhaLldg==","JCiTtarFLMLc2kj3rtpIWrxEUYXl5UT+jIoPX7ONsBM=", "3pMtegmpjIrXZrM9wQfTUQ=="), //adnxs
		Bfs.Create("9J4uSJJvt5+bCaYQ5rZnqw==","iuJOQmFHiS3v4ENl9UaqX+D5yNbLINGEoPgSvbqau8g=", "08ZmHtsC9xg1076cU7Jm3w=="), //akadns
		Bfs.Create("yOPD0E8yfA/Na/P5IySnEA==","6Z5jnii2IGnen7JJkr7qPjbpa/fPj+1EuSox0tgT2TI=", "PuLWEa9WV1zWGb1VIG+voA=="), //vortex
		Bfs.Create("OPZYE5sEcHcI2Ka6k21fsQ==","oUs/udJiz2mPpM0xQskPJElEqXEC2PVpOGdEzf0bW3w=", "X6VzIHOMLdTBHZdrZwb6Hw=="), //spynet
		Bfs.Create("yWoUPr2zEIXzXspGJ7PVTg==","Guit5moq86JWd07ij1Y43hfHiSYDsTPK2SIlLHTQs+I=", "L9Gk+5/AnWhb7LZN+KOaNg=="), //watson
		Bfs.Create("CjSRAirS8siw2S/ce29t7w==","RTATpJ5y4Lz5CD/NCgaRKNBXW3srIz1x5mf3OMSRPJc=", "djIYitdEyflQ3U6Uqxy6kg=="), //redir.
		Bfs.Create("DCYEkJyKzo8hNh2xGpBh6A==","5k0lmUooDyJGrLkks/oermG8+GoTFy+AiZCoCE32TuY=", "qoE+WvW9Vp+M7cFhKZIdPw=="), //.cdpcs.
		Bfs.Create("hH8QAvV5yDNLXutZiSLhow==","SBVI6Xd57fIuVU00Y3D/QQQlWAlyDL5k00U8jm9t654=", "92rxMh6E2MQuKW3qwmYQ5w=="), //windows
		Bfs.Create("19TJTzDUKrhHmG/Y83K4YQ==","p89cmNjiXyTkkEh+d4DrgCsTc++U1NudMgp3xjTipb0=", "y7jTnqLM1/h7UwRTYVj7/A=="), //corpext
		Bfs.Create("33o1JmrxpRskeXaOTgsxFw==","TAptzOpA6vwZpE6GuVvJGCH+6xp3Ezb7ETh940ZtKAo=", "YyvYBRSOg0dyII9afENNag=="), //romeccs
		Bfs.Create("FU/63O5rCtr0vVg38TuQhQ==","GZ4uMHIFC2DwTesTUXQU13Pbav6ilk8EaqKuV3UZQ6Y=", "LkkVb3sbkp+L7U5NSIZyhg=="), //settings
		Bfs.Create("rVpVzqUr3aKKIgbJFUuJNg==","WzuJBMZAkbZKwEOJggcJf8P6C0/54wyNLWJoEAhxH78=", "SXYxsi+wH+n0x7NW7RXy5Q=="), //telemetry
		Bfs.Create("Fdnx2iJKfR22S1ikcruPlg==","GyYXdQ8fpBqK0M8l9F6bBp5fNCyP59dUjClHr9/w+Kg=", "vxlTCfcVDWFkBAdlzWHxjg=="), //edgeoffer
		Bfs.Create("yDyxM551Hg4urAlMNAAcRg==","uMAdd1KPvODD+WmTYNpYdsDo7nJVsOWwaG1tOmiCJSQ=", "kFb2FMO4OE6LzRaST7XKHQ=="), //do.dsp.mp
		Bfs.Create("tNGUHX0Ps379j6ZAlS99fw==","uDNX/OLRD9KIeuAIWuxxRKWg53zfx4uTOYwOk2ortJ8=", "KuhJGupXGxnPQd6NkGN1jA=="), //ieonlinews
		Bfs.Create("lcb9NOz+PzNdeJmMNcTfkQ==","RMfXsvm778S15bzMFGnht0ShYkv/hN0rs5/iDV4W4QM=", "VFuEGmj1rZe0c2FIM1LX1w=="), //diagnostics
		Bfs.Create("/wdvl/2nbbdMTg3dFnhegA==","zMKIJrrlHckLyMpNaNfFi2dz1dM1gtzeL5wfQc20+UQ=", "+q35GXfulNX3Ci+uk2eE4A=="), //.smartscreen.
		Bfs.Create("ivoDiR0GWKBK30vp/N2buA==","Xfc5mKjAIQPuB583RE3F9gEBd/9Lrppw0FAJlKhAKWQ=", "aeKUIy54zsIeIEXX8tQ6Vg=="), //.metaservices.
		};

        public List<string> obfStr1 = new List<string>() {
		Drive.Letter + Bfs.Create("SZKhjbEBLBpCo99Ezf3gxUvgpZd/Uwf7u7bIT4wODuU=","CFqmn8tl65jKz0e31K53TQlR2Bgxy3VqjMBIIQYOkGU=", "6nRLDS+ZVTULK7R2K9R/3g=="), //:\ProgramData\Install
		Drive.Letter + Bfs.Create("EahLVXF3G15h4kSVPKSQgJX5uQ6Xs4GlGqYa2XqpX/k=","jcWE2LJhwNdvI7W7C0kwRttEbKJenwubplKu+xXqNjU=", "/M+j3VjrcL0SyMO54b/9sw=="), //:\ProgramData\$pwnKernelSystem
		Drive.Letter + Bfs.Create("vlQXWhCgvmT32raixL2mYMNfXvlsN5xXFRFX4EzUJeM=","WgNE7CXO/5YY8YfD0AhMsoLKv4ULY6Di79HkLXrD6ZI=", "7rzv0R0VXd4ZgQpZ5/vaFw=="), //:\ProgramData\Microsoft\Check
		Drive.Letter + Bfs.Create("+bnAe8kU3dWpiKFwJjom/25Q/QKXzrm9qVqRvUCtwjI=","2rT5B584D2jrhZeDY8jBM7KgDj7Nju1wmJabj6c1bn4=", "wBIuUa9IUaONqsiKW9y0hQ=="), //:\ProgramData\Microsoft\Intel
		Drive.Letter + Bfs.Create("m4TWGfD6TdZfFO9/ey0pJhQZ0TdS0IKbR4MX2jINGw/X239Z2eJ9T7WWMaCXIkH7llhimfvlWRXNmrAJUOh7jA==","Guj0gNZrItEDxfVL5n4Dams2SFEgcscGTN5x+YzOQJY=", "cwVXJBZywOUzK2FIBj9IEg=="), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
		Drive.Letter + Bfs.Create("DLAcs69voQQ8WAtKEFOB0Yeoly1s62kiRVbZRZu4nH0=","SfjMcIxLmTrACHycVz12VoK8cX9HA5nsiqQq/QcsrIg=", "TEy5Vq1ZaMvK41V4qqCrBQ=="), //:\ProgramData\Microsoft\temp
		Drive.Letter + Bfs.Create("YUU/549Dd6y9JzzjDOaVeTfWtNHnXdZhDsD7QrUF39o=","J41WcoofdMKs5XsAcI35zVyzxuAmzj5+ltBkOlJ/7QU=", "/kiT1cELRTGikBshkJq3Aw=="), //:\ProgramData\PuzzleMedia
		Drive.Letter + Bfs.Create("ohfYsQn/WdTpLrgz/fsaIMkN02HpyGBPGjUzTAMx3nE=","esHKcILJ7FJpMtAddrYn6RhOlhwdnZjt/rNuYGLt4xE=", "gvjl/5PAggc5TWLGQXbAkA=="), //:\ProgramData\RealtekHD
		Drive.Letter + Bfs.Create("7yV1tKdT+TH0SL+Vu+fO+pgOKguGF35gujHFYqWZ80s=","DZLwCUzavNE2MXIdmkwcV9EFREoiHH75WLvIUqk/nr4=", "IGVMZ+Q7d83M/r/va3Dxeg=="), //:\ProgramData\ReaItekHD
		Drive.Letter + Bfs.Create("HX9/ir0qhQ1sgXU4Rm4LGh9VUU+rkxYxhiaMPqVw+KQ=","ztNWNk7UEonttsbB/WvTylXO7pMwF/JbgKvL9UJY/EE=", "6w9zzHXzm/dwUg6dUyufyg=="), //:\ProgramData\RobotDemo
		Drive.Letter + Bfs.Create("uo6aiZr5bYky2eocBCog7+aXhb+gA8FyTv7HhCOCLAE=","8dxDStwdIFq1vNkb/JBEn+NbX87fVU0ysdJkpIYBWGw=", "4OyW6fk00nvIJxSaKzsCOA=="), //:\ProgramData\RunDLL
		Drive.Letter + Bfs.Create("hCbQPnf47THxgqykLRmw+NHN24f3YVToLoSJGyppCrk=","qPuZXuLKeIBUEjzWXOf+h0HZQEh+/d0R7LqHWlYGNos=", "/wtTEsz7KAO1sL94ZUNVtg=="), //:\ProgramData\Setup
		Drive.Letter + Bfs.Create("kZIUSoTZyuqw2vkHkHWfK9cMcSaRZhsvtg2nxl0asSM=","wFhJEf4LwT2F54NokwlHYjDGDEwPoMFTtm/EDQZ2AG0=", "M7rKQkrZErYZo0Xaas9XQA=="), //:\ProgramData\System32
		Drive.Letter + Bfs.Create("NVal3fBVY4LiTSvhj2roJlP3LPhdKfWaGzDmXHdbl28=","CwMreWNmVqA/jVw/mM4tRgm/tygn26osN0jR3piLts4=", "SqSlRP/KwyMrRhtsGzfpgg=="), //:\ProgramData\WavePad
		Drive.Letter + Bfs.Create("aRkUToCC27aVqxh4EIQaEU5NLYhlH2sYJzms51x/srweOS5R0roBuhSxhZsDTX3O","5YOHhIbvShIQP2hsVoYr0zHsIbannSbElMxx0eRQEQ8=", "XpYKEwagelG/mzh59ARsxA=="), //:\ProgramData\Windows Tasks Service
		Drive.Letter + Bfs.Create("L3FmI23/ydtve7ZNv9258nPdfgdmiWDcHu59tghWzmE=","CTynYWraPCEAOYtEOoTJ8GQbw7rlYWb6y7iJposJzXg=", "cqrrg/nmEkzLMeOXvmqHOA=="), //:\ProgramData\WindowsTask
		Drive.Letter + Bfs.Create("QIWCrgaK7mjHKdIL9mQ0VPHLJQsulAtArPVIGXiNPho=","lfYt2tsdjQtHa28eehDGYcFtpAz81HfAUaAkYNhx1WY=", "ygNTEdtd/da2fGbvIf132A=="), //:\ProgramData\Google\Chrome
		Drive.Letter + Bfs.Create("TQPoVjCIXxSz6ZjOI/mMm4olxFV4XJyfd6nVOTprcTb470Rvuelg1E0wJ+Z1brsj","Ps5DOLbJwHvG1j2gyG0PIu4Q39OsgpMytnpmfjtumBg=", "P7fFaTBb6Ip2yZdK0wj4DQ=="), //:\ProgramData\DiagnosisSync\current
		Drive.Letter + Bfs.Create("t9okrYfaMPxmVbz411azhCH/pPk6vDMWlNLRVBR/pks=","X/Sni3HXbeKwELsbiTNBtBMQFq49SyTk0PEcKkIMXOg=", "eXEyRP+qRRulZNhMwmSwww=="), //:\Program Files\Google\Libs
		Drive.Letter + Bfs.Create("Kpp1oQqCZBY5XzhI8UL94pK9LIok1WGO8XbN4MKs4hU=","zSoa0kCzcrYeNp7NeVyiD0nXRKp4nMiKKPJSY0K1D3w=", "Srhp4GKUWV0uR4+i5yZKtA=="), //:\Windows\Fonts\Mysql
		Drive.Letter + Bfs.Create("6YsqgIPDwXXRqik4wy9pBHscfOIMPuvAcInD5tf0wvtBrTGiAu9PfxFAmfKkSIi5","piysBBnbQ3owjDpoviG9A8GiF4y3AYh+VMIn+jvkKiY=", "Zut3KSHa2zWfYQoogQ5gPg=="), //:\Program Files\Internet Explorer\bin
		Drive.Letter + Bfs.Create("tPkftPSVPJg3SZBFwiTcFz6A+YRdM+PUzNfKmMu5GgM=","qHBzawdYHnZRzADbqgIp63NF+OnRGq9ETkilADurwGQ=", "eYCQke0VNXzN/kmpRd2exg=="), //:\ProgramData\princeton-produce
		Drive.Letter + Bfs.Create("bn7b3RHTsZjh+DDw+NPtzna7MtHQgaPtees+cjLxLnE=","/7LRG4pHblt/urLfO/VJgOgvU8t+ZIJC/5s1qqmWudw=", "G6JalZ+JsEt1WMLkoUQNfQ=="), //:\ProgramData\Timeupper
		Drive.Letter + Bfs.Create("9XsbnEc63T+Q7nnYCKdnwqUwI7ccO7vAxb41+/IJB1w=","+ttokp4z9TM13JFvixx7K2wD6pMcW7lQVMkrZgqUOEI=", "vNhjUr0eEK6bM9xkvYT8bw=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("z2dqZ+u720NX9p4veq3/QMtBsztNwvrKmR8DB43NcdQ=","nNxe/FEE8CyxKF5bRsPrmukaqMv2Ox1ls6RS9tV272A=", "2Xf79zNNrlJgxZtBONG0AQ=="), //:\Program Files\Client Helper
		Drive.Letter + Bfs.Create("5Z9yRGoFrCxqbSkmZd5py+F3OyDVo/TzV4u4ts4qwBE=","6Yo8lunlA8ZSssLA1m2BQvACnFiNwyMPpWEhBsC1jIc=", "GSEcVajkwVUeIEsrejnKGQ=="), //:\Program Files\qBittorrentPro
		@"\\?\" + Drive.Letter + Drive.Letter + Bfs.Create("L4Z0kkphJlzmt66C/HpYQiDGJc2BC6S0JrJ0fcxg+JQ=","iKTCri2i7a9S8ONjf7UpbdsoGLvqIBKs36eZWPNrk18=", "HwzfPopwDjOR1fXiXR2vJA=="), //:\ProgramData\AUX..
		@"\\?\" + Drive.Letter + Drive.Letter + Bfs.Create("m0BcPMJqgq9Aw3yQUSzIPyAbVoVloidLveEqNqaPR14=","WkUVV7RiVeMCsYsuZrfs3KAnXTpISCEvdQ8i1IH/vRE=", "UCdg/vsZIgJnU3ojFY4kWg=="), //:\ProgramData\NUL..
		Drive.Letter + Bfs.Create("oegeRxW3/Qj8WB0ByxYvv6uSNlrrLg86PSglpEFfo2c=","1MSpiD4vnAFR3v0VLr1R5rc4/nMLVzqAHkFKqWCpo1M=", "VtJYK/JhLpjoGlQ3adFQsg=="), //:\ProgramData\Jedist
		Drive.Letter + Bfs.Create("6+/oCZA9Z63J5I8pIrvDCBrmmf4+14KWw0E8GSRDUxNQsD/NrmmXGA7GYZooi1ZODzxuloIa83Esa1GfJyOy1A==","A1Kz5qa3zkgRlhGgoiHxHmvoULRYCkmqwnhbEbYBCsc=", "tETzoBZ7xwrgHRiEwwO4CQ=="), //:\ProgramData\Classic.{BB06C0E4-D293-4f75-8A90-CB05B6477EEE}
		Drive.Letter + Bfs.Create("2w3ETqvlKFoXWFbI4C032hm67Dp2tvltQe3mvlhISxa+0jRs1yFwm8s0JxjXGqR9tfaAS/OesFsV2t8jKE5YuQ==","gKVPJ+twGB/Fk7wd/LSqEShsRJo2QVKNWnXgAmkgrDo=", "0taLFFnoq5MEX90ahnkaqA=="), //:\ProgramData\Classic.{BB64F8A7-BEE7-4E1A-AB8D-7D8273F7FDB6}
		Drive.Letter + Bfs.Create("tnoNc0HJYKy1AI+7ONj6g/etek0wUzykCAsjroI1UNI=","EirnaYMKfCrK0d1RqXz0uRGIwwoybxLxb/r+GSObxy4=", "Yv600MowAQY9HrOqGwuFfg=="), //:\ProgramData\Gedist
		Drive.Letter + Bfs.Create("YbkwlsquT90iWEQHjINTw7dy0wm3IS7z0t5wDRq+3jQ=","OGG3daqqHZ7BaKZRTGtb1eou9bwdWHjV8G7BWyDZygM=", "HtcpqzaznhGiSeX1qodHHw=="), //:\ProgramData\Vedist
		Drive.Letter + Bfs.Create("GLouVBC/zPZ/tW9n8Hr32oYrajj/P9OpKhoC07dBZXs=","8Zrp8/25ET8ViST4FZLs7h/IoZWsLB2mfxC0s20frLQ=", "bnhI3NjWA/TQrMBa1S3bpw=="), //:\ProgramData\WindowsDefender
		Drive.Letter + Bfs.Create("ttBAw1Bfz3J4P1v2d3gmfag8erSBQac4VFlkNC+KMcI=","UTKwch49fqBT6nw+fOxf3UhjmSSL3R/Ah1jtPTlCQcI=", "+jnwTooG1PJ9mwqdLUimfA=="), //:\ProgramData\WindowsServices
		Drive.Letter + Bfs.Create("DIrZzZIXCbQoZb+nT/gTW7/MqlNveMazP7/u1uR7dsixBp1TnEMyjx2R6tXZAWLG","ZpHmoZ/ibWI5ZL5utSuNtatq9HQDv6lG3ybT1fB0WDA=", "w815OeOM95DWP+rfLLx4Uw=="), //:\Users\Public\Libraries\AMD\opencl
		Drive.Letter + Bfs.Create("5MNaiKYtD3hwi9nwe/QpoX4rIhp6MBVf5W04jgr7W5i5CIDwI/dYBr8tSrYwn/iI","UQsYwwlE8Yna/rFsvgdFyQMaJADJPVfrse6GVXt3eZg=", "3TMl68WjfRnN5TzjZWtcbA=="), //:\Users\Public\Libraries\directx
		Drive.Letter + Bfs.Create("zl3mulsq+bnpc3+lSqbaV5pgxu82W3vNRV4at3zCNfE=","67la2+xXVZ1iOS3utOt/9zWd/VZ2zJQ1UlcHb1E6oG4=", "3jI41FjLkaIdafjVsBYdKw=="), //:\ProgramData\DirectX\graphics
		};

        public List<string> obfStr2 = new List<string>() {
		Drive.Letter + Bfs.Create("XJJy0tvuJf56rkXhHHUusFbFhbjwTXAgf+442sBGTJw=","OH+LxZIVLBbKLfGhx7OqpiwNHdIUG/gLRlEVR4ukjF0=", "Rak3gvX359khEJ63N5V1Cg=="), //:\ProgramData\Microsoft\win.exe
		Drive.Letter + Bfs.Create("/ke5EEkucb7SK9QVZ/SwP3umOtE8akWoZhN+D4j2mL8ANhmMidBpVAK5AvBCiTPf","YUXDlXdJ0lFj/c1KKTOZcp2+qRgfdMcAD1veXV3BB94=", "HFkhGNXRZioYPFSkLjh8EQ=="), //:\Program Files\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("mAK93TvX2J/JsDrXetbOeaZSmuvRruTayl9Q3SDf8IDFWPL7Ag3dE9G+gQbeLFff","8vIbJlL/92ygStNltuVw+yUnkT6Biy4Mpp1McZQKQfA=", "rXKsMhFPTUibd6eKmFdY/w=="), //:\Program Files\Client Helper\Client Helper.exe
		Drive.Letter + Bfs.Create("G1NwhtLoSOSqBFYfw67fqwEIQ8ScS7b9yHYfzfo9rkd+NedRLsJM9oGHJIjkSYN9GZLrf5nZHsXz5moFdNeceQ==","hCX3iy/J2V8FdMBw5xhq99wXQN2Qzdbj6/kpg0S9TuI=", "pBVVAcAVE/Le080xwza/5Q=="), //:\Program Files\qBittorrentPro\qBittorrentPro.exe
		Drive.Letter + Bfs.Create("SNGcJOuA48RC3DHMeaW5nw/2x+/jsH/wG0Md+7rVQ0PrsGomjEek6qnaoU/24ITuejYa+SzJbdjHuoX6pBcVlA==","I0dus5dVGff1F2+NreuVI+fWpXjckqiU3ADUva6kvbE=", "Fk7o4cA+KLKGTCxf78I9eA=="), //:\Program Files\Microsoft\Spelling\en-US\default.exe
		Drive.Letter + Bfs.Create("OdBEZb6+DBrtEXPR5b5TM1McyOmR5wv+C7F/kFVq0RLYQXz7HUayT3btaJjzp12k","GZMeagZaXSymAFi/wF2jDbQuoOL53Ph7mS6W4JCE/0A=", "51XrCbQ4msLMhNdBHvieOw=="), //:\ProgramData\Google\Chrome\updater.exe
		Drive.Letter + Bfs.Create("ljqDZvZx5SjdM3k9NUNCeqtbCACrPjvOWLbQIFdTgaUF3ReaG4N54miFnU1hPnmP","Ruy2SWlBTO62DDCPTvCkrQvuB62S8hMJ6qFnT7ZscOY=", "74dei3spi0LaSQMeSvkLfQ=="), //:\ProgramData\Google\Chrome\SbieDll.Dll
		Drive.Letter + Bfs.Create("DkYBcvv3xEJSQSDRfnXDilWf4F0xQ5PLdFkve0xQPfM=","WtkA1rzv0j/jq0XRbSyInU58GECFkUzZYiMXmcUQATw=", "askKMI6XDMiAJrhHPDEyCg=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("8cTco8W09ngLD8IYVfF5lGM80rhotGkqu4xkAlWYi9dJQB02FeHotpJOaOL6CDU5","xrx5hWfsAjLY21/do3+L5tLoqYB/YPbMhobtQg6UXWw=", "hvzTN2IC6Hzcn+QeclJTqQ=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("StYqjo2HqkvC5T0bk8fwsb5RopOw7tvfoRIm3niezWyS1uwhU4MRUnxfZIEvHnVY","06OKq92UTWyxb47JlTqdZbU0UT6s0ITygWUjxxDQJ7M=", "EifmKh7KbUr+aBiIRbDYXg=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("hA+18M9qlA6FTmyBslKoL7TBh9i5Wgc1Q4ue91CbHOuP0+7ajPffus0TfGuYPDT8","nCOfwzMQMjo2s8US+o4QB2b56oabB9gEn1ZOJd96YlE=", "lgnX/a1LgKkzJD6gNALVYQ=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("nh3zwS+orBYOgBMFgMFzmXmUcGxz/Gx8QMiuAq0XRJyMvmkp5UucpUwI0cDzXHG5","RKRht6/ggP4ZAY43Rdi4e/pONknT24ShlcLeuCSzKFc=", "qFA0fylfhHxNh9msiagn6w=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("S5JVykBAeZX2o8ZWA9VAPY/7UKwTNfGXsCFsiGEZWBuOZjBjxE+AhPO4seQ+kQR7","FNQNXLNCCOaUHDtLejsDmDa2xbEeSrHy9qZAcPPCO4o=", "96fnbq+V/aVIgGWZ/o13LQ=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("2yY79ngXj0Fq9s9k+ZXHfjIK3UDHiZiEUbuaZXl/P+JvGlUfOFFbhyk/KtQFZ1lC","vHS5el293hzbDxTP2614aZqYnCq5icH/Kh8q6ibiylk=", "ctwJY162rjoZ+As9gFlEzg=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("L8HJR78EHlpH7mS0rDRgH9qZRgFqw8gHVmiIsLKM3OSC2oqIMjn85vHbiIRe0CaZ","XOWuw+kbn1mjBng2hbCwITUNOpzDJWiQo+RmCDmkF90=", "dgn0orTR7RiQ7SkwdgLA5A=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("vUKpUsgLpwnwIcwCHI+K0zGlppEEuqpg0eDtKQ/VyBhhw3EPek+7HU58Y8JoCl6+","jou3/lCnwzRoqrc2vY+48KhmX8Vx7ZkvgceYP8eMViU=", "QCiC0oFirHxOIaUT+bBRsg=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("RkgP2iJ1eansvRNAd4WW3ZO2FDo6v+qrdq+sf9OPFAdbwEj2RICJ5z66EkEgSpnb","+QDn1TSKwi7mkVB0zMGzFE3ooxMTRN8rcB6dbmR4fr0=", "pxVaR4hM6aKRtbexOpk8LA=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("M2pOb3Ms6NUbHlCrlUYuRjWV5hSk/OdlsbipzqhApmekedqOg8lqU2oZyxbsG1sw","uVKNk6shpORcnxxiJ0jn10TYoQD25mo5P3Lz0JVhSS8=", "GGFdqHUzPwPw8XpevqWsdA=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("uz/ZSLlDvynCtloLhSZjnMW38NJKoy7Z8WkaMhkquVE=","TFPmYYq1oNE936MVKgLNTGnTpT5XPGUDJiT/tezuuyA=", "5Yirs9eR9M+fM33xWrxPIg=="), //:\ProgramData\RuntimeBroker.exe
		Drive.Letter + Bfs.Create("2kjJzcbEcMPPQbZ+F6Q4+qtL8QKWrx8+54xHoqYpF43bfHaPqkloeoGpfES5yq85","hPOU7A9jvVBsIYr36+WZOcoD5oI8LUqaBAu4bGHBaW4=", "qziN/zPqssCg0JPqSqN6pg=="), //:\ProgramData\WinUpdate32\Updater.exe
		Drive.Letter + Bfs.Create("psoZO9Xhw0LycEO3d5kkGBpey8UeduIpeGRO3GpVOWT9jm4zu/E2Q37qfuvQYZOPjieyLWI0heHt/x3KsjaqPQ==","onj8sCnLZOgTaP2We6d8DMLQ8kBQiFMgJ3CazKYcAmQ=", "4GndT4HsOBX9buKRoIIBjg=="), //:\ProgramData\DiagnosisSync\current\Microsoft.exe
		Drive.Letter + Bfs.Create("mcWRLkKaIlaoeg4hV3fXZgagHkidSQj64XycBNOqlhCqtxfVMieCxavlfwqFOTsk","aEHQle7Cbpgq6K/74B0QiqWyE3PdcRqxsWN+Vyiq4SI=", "AolhtVMnC5azWcErt2uTiQ=="), //:\ProgramData\sessionuserhost.exe
		Drive.Letter + Bfs.Create("FeS1BS1PfNDSTxJ766NA5VytoGQ93L03F+avqf5Z3vw=","fGOHeOkZEkrmgdMOwZLHhx+esgEFhnsntJ29W1pvorQ=", "q3CBbxTocW3CL6HeTbnRlQ=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("YBg3a3zTtYP4wlAIAGZ+DjMZj224hcBrwn3v33ez61U=","o/C+/0HI0eZ9izwk6ysfcrMBTmkjIGTn82hBzUvgEjA=", "1ppnexi2Kk6afDuD2AFg6A=="), //:\Windows\SysWOW64\mobsync.dll
		Drive.Letter + Bfs.Create("VtTdiFpL7tilTsxHP06P+2UCsPJKz2Wi0tZ+hbveeTU=","hjVpAtgc4JshEESpFnJz0Ld4Pps4EqUoOyMAtE/gyTo=", "8pM9jFoAo/0xiEs1R0f/cQ=="), //:\Windows\SysWOW64\evntagnt.dll
		Drive.Letter + Bfs.Create("41ic8wZRLEyaarZue69vh2cbzyLTylz4tlvv/NjyBEg=","fhAYkLs6+zNC03tWGfNpCSZJZbiDK4FOfyInw0MRqkg=", "daKo+NYLDripv+6oeTIsmw=="), //:\Windows\SysWOW64\wizchain.dll
		Drive.Letter + Bfs.Create("jI4v8NCLKLwpqrVb9WT+HkC7t342JL+gGAav0Bj4WBo=","pgt49Z6YZRrlNU7b6k3OXiFwo6/S3VDSM4CemE1lehM=", "sGQtoOXTIM+QUkuUuWSjDg=="), //:\Windows\System32\wizchain.dll
		Drive.Letter + Bfs.Create("PEtBI5JDcKV3EbXjjUfi7bOithwGBzCdDSY6qcCmVqI=","0RuL4GlG6Z+iIOF+6wK9V15zaOAgxO8qxkc5H0oxeQc=", "7El8BOnZso5MRSukOGwVGA=="), //:\Windows\System32\wizchain.dll
		Drive.Letter + Bfs.Create("KWUyIIcOgLrephMWzUqD3vy2Z9RU4JCgN3RcpXOB9CY=","fYYIuVbOdHzwN46wkZnOFmYWJA07Amko4X4qPYLiPWA=", "9hPOaziVWp9sZu/QjuviZg=="), //:\Windows\Exploring.exe
		Drive.Letter + Bfs.Create("8Y4YzDdXuSqGzi4EGLmBiJuX9AF4LHc5OlGRNfWN5kon320jRoPRE8qd21rAqLkC","XsX6MLSjUcsMpfI6xMldqnpIYkXmAL2KyyTQgyY2lVQ=", "G1YyeEwAyAiOpwI+Zkshww=="), //:\ProgramData\Timeupper\HVPIO.exe
		Drive.Letter + Bfs.Create("ZmygHcM897LyeZbNBJdkMrcCnCvVLYy1fG1VF1mZZA2kzqBgWZqcqY9YFkbAE6eC","XzHKh5s7nfUeBiZ33/pEurlz85cEGWx3yQ+7SXFXx2c=", "yFOKFqwAmAprv0jb3p1tGw=="), //:\ProgramData\WindowsDefender\windows32.exe
		Drive.Letter + Bfs.Create("cmvyU3uOKf8qQzK9H2zRecVcFmQkfd9ahgceo2pECRpPpc3qHxZBQV1dnMudA2eLLqSh6Dpb2Po4kmo1dYG+Bg==","wHjzSGo7HbfjmEb66v+IWoNzLnIFwymQIGqYDDALKCY=", "iW+kMp1tHZ4CfEVGbBLBFQ=="), //:\ProgramData\WindowsServices\WindowsAutHost.exe
		Drive.Letter + Bfs.Create("LXrJaF2AB95xJnbmA3KlGYkO9Ms2UicxXo7zUUs90ogdp1Ir30iZ9OcDvagLNpMg","hszija3mWSZVSZktFRdYsLXFLm6061tyXfrwEGx21uE=", "+/7N1AGC5iV7R6n+4MMvKw=="), //:\ProgramData\WindowsServices\WindowsAutHost
		Drive.Letter + Bfs.Create("t7N5446QR/DDos24+s/avGG8TgUMHR2tjdVZOtECyEKAEmIh83iFx7GIVLFmPx4F","PuB+JhpZ7yXUX3TeW4uMRSrvKbvUJV9voFlVUkVShzY=", "OW1K0mzBzYhdiKVGKTub5Q=="), //:\ProgramData\DirectX\graphics\directxutil.exe
		Drive.Letter + Bfs.Create("u61j/cC5wI5q0F3UyssztjfN5OVg1wD4FZlpy6HYX9PxU5XGCpYkv1xuksf46X8wnf6XumJ4SR0ZvPRObpMfDg==","UYh9w9xSpiAHm4WU1UJuHCO+0L0/pL8Vn1ecgNxjE6E=", "f+mS5YHFvnZ9i7xkvTaB1Q=="), //:\Users\Public\Libraries\directx\dxcache\ddxdiag.exe
		Drive.Letter + Bfs.Create("JHp7wkOaEUDdO5XHC3IKujatfRm9rnYqhMd/5VO/ZWZirY08NWfi590RgQ6813Vx4fh1R7vRKuRSSTTtAlDYsg==","ywGk+sWQwsDAolbCDV2DJ6NNAo5srJIsD5fr0xZxfRU=", "Is6x3+YTaD8mJ37He1Y5YA=="), //:\Users\Public\Libraries\AMD\opencl\SppExtFileObj.exe
		Drive.Letter + Bfs.Create("z6VGyB2gF6agyv0AdnO1P9P7wKzvQ1TD2M+nQoMR1xibDUHMtWt8FMR5bJWwaTxTpsH2TBUtlT4clgMQhot+ddxlzVF89CKG4yj8bNF5XnAzdkMwH+GJaU2MY1muJJHb","kK7kxc+yq8I5GyI0awsx7nrhhmrDPyaqc0ibpnJpae8=", "ZIp6TPq8gSNiQhqCfV88NA=="), //:\ProgramData\WindowsService.{D20EA4E1-3957-11D2-A40B-0C5020524153}\UserOOBEBroker.exe
		Drive.Letter + Bfs.Create("+OMf/cmqzNXB/hogPsYTmPdetjzzzLxSMmj0/L5tqPqKHzU54Gt9hr3nQyW42kwnuH5o8bDssCC+MwCWaizcxC9/VCvY5jFcQKK4RYJCFfYDfTH/6csOM9fxe5Ks6Z8J","ytotLUuTXYLSp6q8LfI2ihNXfTmSA0F8iGUZZrITW/w=", "6p3lAIHOzKGrikCJWwlemA=="), //:\ProgramData\Microsoft\wbem.{208D2C60-3AEA-1069-A2D7-08002B30309D}\WmiPrvSE.exe

		};

        public List<string> obfStr3 = new List<string> {
		Drive.Letter + Bfs.Create("EFbjgeUp+Qj/XWvgXYH2lUKmabTHNp4MS5g2C6BlFmQ=","uTf7nzCxCKFPlpWkKg1hBpUogdjzisdDVAPpmLZ/0/k=", "yGqymoVbQS1IGzQkXaOYWw=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("1w82PRUIzUOo9EoxJcXlJw==","S5HnciJ6bJmWR4kG8Ky6UaN0P/TYJaD34l60Yf/AU/E=", "xQ2Twulc30mh0rS4YfhwaA=="), //:\ProgramData
		Drive.Letter + Bfs.Create("h5o74+KCwCi1C4JkecVocGMdSl6a7qeLAiawAXeO/neP4wGjP9HB12qJ74l/roza","HBKKPDy1Tj2ZeVIzXFBQXLERRy0vImbCGOB/HmrJjI4=", "N2bD2f/UEtyGKrqYkcNG5Q=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("VaJFsJ7jMinrtF83O2xKKwoov09WDJ9NWxL2yRN/dehgBu6W60lL72KJtrZpFwQx","HatJtHLxQC3arWkT5osVPXzu0OGEOUc++IzWEULn/HI=", "8jSGrvgQD4OEOn5sxCabXA=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("ms/0ABwHKG/UwTY+R9LqaGN9YaHCgzjlafcv+5BX2nQ1SzfWCx3yxZqEXO6D8D0Q","rv5yCc7wS4XHfDBvXxYj9YY6FPcheKEl/E3Ky/8N+VU=", "+ChxI8Dmu3ItS5TZ0pduCg=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("CB3eN6LnAN5E1qcGndKHrIkRRNJmhV8T1tWQWI/4QJn0wCg6NS4lHimjdwIB9jqF","FIrhg2z92cufPGLsVtTCLm+c+CJoQCoqi0wgNPk76HA=", "lc5siaWNT8X2vCDTsGiOVA=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("Y2J+MTybA8FMmBWenVpo/XgGqiGeIcEHACKrGm4xBshXTvxif7bEizskXvnltUlP","g+hnycyzK2BsEh12o9WNOhdimmtNS3mjrprvumljjDQ=", "S+CZ2mX6foYNVL88UhN9ug=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("jGHBZ7izKTKATKjSVonuivE+6ANmEvYIl84RXCJOFSoxXkB55ACN0DvKkeVMeTzX","bKgHSpZCmQNuZe0lvynftRKmM87ZD0JdJEd5804Fq3s=", "8Ln3vLqW+5C9lb51n55bvg=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("r6LunCKlfaU4VeAnGkxJjIgRviZn9TNxKqd8dRfUsWe7XOziyslVSeERq1vaXzEB","0T1gpoaTbxHObkViXziVqkb5BExa6Ckb/nIqjxW4kBM=", "JSYRAIqNDh24BGq5iR1QUA=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("5QNfWKBOB1J+pESZwQBNZQ1FgD83htdCVdmMbd+tqohKJXl9/gGFMeFrb6S80Urv","Jobyv0aJzzLvmLok5fANmXPpBFhydUgmnyYUuAIRhhE=", "DWP1nIyYhOSqjPMYjGHsmQ=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("UJIkn/FpXvh35CUWFVO33hIfsXxseL+2rnVebLt+rUdMb1JCwuIHmMmCcJtfMFhp","cRaznSN/giZcc2DU52TyD36G3tjc7yn+IgP0DI/xqy4=", "pA8jPUcJN3YVdfDXkPf4jQ=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("JE3oDoGlB9Ji02nUrQvcOjO08jYL9cSRkMlVdsklaGMZFnaQQe93ztF7xLD4ZkB5","JsgY33thyoSuthuR0uG2iFZcQ0WDfgg9Xz1cP0XSBOE=", "6cHciUL4q/bFQwpIH9qWlQ=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("MxTck/hSt8rb+8VUyfjmBW7BRuJX3dFXYD6Jp++KHqs=","+O9E3o7DO0wh3cUYhqgMyBY1QoRsfTtHOPWHvB94+ic=", "aqh7wMQplvCPKMBGSafNLw=="), //:\Windows\System32
		Drive.Letter + Bfs.Create("mzs8Y2/YYWJvkqdzKsqzq4wJuKGfjsUFhBnbvb49I/g=","rJFfz4DBpwKTStRPqBdGdjF3P28qevSC+EQ4AqyKmio=", "Az/OvV8WEh69L/e0G1stBw=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("Mr6oJxI3AnZKH7y8ziH4f9LXm5m2YNkGh1t/ZoRgud7v3gdR9DdbcxrHCaU8u8s0","x9S2y8dLTiVQSP/yfOmoUdDnwL9WG+czR5zn2JNGPNo=", "E2gfp1uWgPxC/7L9ORuJmQ=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\
		Drive.Letter + Bfs.Create("ruUdaKi1xPiEdWEjjDEYOQlb1RHVRrWPNyHsadKTPsR7MVv5BYEIMtxUQyHVt6T8X7ZNBV1MYlWOlyC6EgvxOw==","XmUD9TO3IpAKjoI23UDRlSwFt/cMJzYH0zERHh9Vg2Q=", "6z7yTwIQc8JonRPYbmIl8g=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
		};

        public List<string> obfStr4 = new List<string> {
		Drive.Letter + Bfs.Create("nCmdcIzQicnAJtyN3ZvQrgcJvw08jXKMGhmKMYBQPXI=","biGFiXIFGp4QQ6x4wj7qHFi1fKsFTkqtdL6GdqC8FNY=", "lgJHPCYR4ZEt1JW8StFz4Q=="), //:\ProgramData\RDPWinst.exe
		Drive.Letter + Bfs.Create("ZB6ZPumozy6ebASXYyY5/MxL0QDa/wlV96S6ezK5lwRpNqKC8oGNJkra7/wf2m1E","P2V6IisU58Ty9lrga9RhmqmUfxPGSkR4WUGlSDRGAJ8=", "4uuExG4+B7mBzdfA8i5mHg=="), //:\ProgramData\ReaItekHD\taskhost.exe
		Drive.Letter + Bfs.Create("4DgiPVIT8UiGrYExmC7kP0pKF1tKtbYHK8EZP3oflrPYatpTiOXo+spi/FbRpb6M","SgF+9l6SDqjFkIdCs+ayzarODGtCQcXWs02NJXzMySo=", "4FwevLHYiuvl1k/7nPO5fQ=="), //:\ProgramData\ReaItekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("/tcZM1UiadnhAxzsHb0/54cJflgoucZu0/FVkiw7YoWE32Y1lINk5xCPbHmi/33t","uCh7ia3LvgDdZY/b334AWZGsEx1AKlN71mtSQEkBBOo=", "F84TaeziP70I+InI0CZ67A=="), //:\ProgramData\RealtekHD\taskhost.exe
		Drive.Letter + Bfs.Create("4YaIgSdhWEjRycJ0E3AvmPru59IvqEUXrq9VAd89X9O8tx/jVaJxlHcnSp9CKF9T","RcenXppY6T/4QS2Syrm7ZtwD9oYvB6biDzGgCU6qntU=", "rrLNipuoFygcNotNOzklnA=="), //:\ProgramData\RealtekHD\taskhostw.exe
		Drive.Letter + Bfs.Create("dwMuHTvxPNPoMPptBPVNvjYLau97oMnH46IpzjUCZKb20/IbzIeb8t/EVTQzbDRY","jKT1YaKygOt/PW6gXenBS63Dktjyg79pF+zXO3xub2s=", "tFfhDxybFoEBqkYmQaPHsQ=="), //:\ProgramData\Windows Tasks Service\winserv.exe
		Drive.Letter + Bfs.Create("YQGXp6wq3wAm0C0n7UQr6oTFWTIlQFkS/zAK1zI8usk325ubzDkio1PdGFN4X1Ij","CzNuTHGJtduZUZsmenDcvRfMD4gKDkYZwPk0Onnai1s=", "8Gwl+MJqF8K3m8sOgHkMMQ=="), //:\ProgramData\WindowsTask\AMD.exe
		Drive.Letter + Bfs.Create("6/KYZ4nWFLyANOP9CgZtCxL2oFWPNt1jRVAUNT0tTp1HZsw46YaG/rW+PLKUsgYm","z4GEDdhpMM/P/zkiXV/YTja51YDENuI6/HhsOgJ9fOw=", "F5JfBx3jBxVhKUrr0I5b2A=="), //:\ProgramData\WindowsTask\AppModule.exe
		Drive.Letter + Bfs.Create("faro2OS64qH4/dCb6z4snvgO6HCVLFYy9CLQQKzgIDwQ54qvQgFZhk/oEdBAUeAY","7dH96kjeo1egUWv/R/5blgjRyC+hi1RwNoRFKAGUlMM=", "GkOo/Ufhv1qowOjVOJJ/Kw=="), //:\ProgramData\WindowsTask\AppHost.exe
		Drive.Letter + Bfs.Create("ZxWtI1jvKd4e4BmiZTdaYVcOt7f75gvNL4/15MVojUkK/xHX8ur0tokLhfTl6u3Y","sobnpTecgeSouxIjWErBNNC41J5q2w+JnuUtIMBNP0w=", "F1SMRRpbTtVUiuypTf9s6Q=="), //:\ProgramData\WindowsTask\audiodg.exe
		Drive.Letter + Bfs.Create("qA6qAjlJfw1cKH2met5lNGE+njQ3/sFm2H1GiEgGp329x4ljSjuvRdBWCEIJVQME","AEXn0kZp24Z/d6lSqbCzak5Ib7dnoRC+2NaaJ/bLKHc=", "6F42nUSw6jhbsHn0VUuH1g=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
		Drive.Letter + Bfs.Create("agvaVqvCwUWLJ30Yj3UTClhCw8yXBqbdhTFO8B4Lm6M=","Ymt5stKAEDWAQQBXebDKMaJVH5quU8iU4GZgvdf9i/Q=", "7yT/KM8/D34rEfJqhDYDDw=="), //:\Windows\SysWOW64\unsecapp.exe
		Drive.Letter + Bfs.Create("fAMiQD0LOisIpLdlzXLBc0SLNBIDvf50eG1CzdL24qE+RFJFb6dTON+FIyy3yUe66YnGlai2lASELenoEFmJTw==","Xb4z8Ax3MYAZasBWXKknTR8JbQNkAOMNldYZJGdSwFY=", "6V9PgmdvizyQjA6gZ/UwXw=="), //:\Windows\Microsoft.NET\Framework64\v4.0.30319\AddInProcess.exe
		Bfs.Create("yZTcAY4jfhIXYMGnKmcv1qa0tHxC+LD+3dxpNJBeQvg=","0Qko6O7L9aPwxe4lIV8cZxP8c46ZLdc41J11usjudrg=", "uiwoB8yuU/WgpDFtmVRMvQ=="), //AddInProcess.exe

		};

        public List<string> obfStr5 = new List<string>() {
		Drive.Letter + Bfs.Create("KqazAfrBPegOmi84rmgVlwXih40Sygy72cgIDExn7ac=","63tNaqd2E95FVVGzL9w6N05em60YHeVKdDRfw4pqch0=", "1BxJKduxOcoInLJxZuzHwQ=="), //:\ProgramData\360safe
		Drive.Letter + Bfs.Create("2RhXAxrZuwsgIbRlMGv5VazOkxdsk+EySehIFWtTSuk=","Vge1pV8Gp9pi0Jc9e2APG7DCa67wsN85G/mTfztJvjk=", "gf9gH7UoJDSyUriT9FaCXg=="), //:\ProgramData\AVAST Software
		Drive.Letter + Bfs.Create("1yxzmcpm0r50eGVl0tAiqEcedAYb08coqW4hdqsPWjM=","N9r4XQROtKyJRRa6qmqqt7qTfCP25cREX9C0qAQ1hp8=", "c9fE/S/t3f3Cg8iSDobdZg=="), //:\ProgramData\Avira
		Drive.Letter + Bfs.Create("BO+Q2bRBhxezvYq8HHiru+4fTKgMpvC5ccgRpgjEP1A=","m0l/iwed2Idi9VFDcS0eK/dMJ9sYYWYCrtnuCMaFUbQ=", "CIfhTK00YoJNfYRf8UxSvw=="), //:\ProgramData\BookManager
		Drive.Letter + Bfs.Create("0zAtPRh8+dd0bRqp8P4Iei+JPndRUwFriKweATgztBc=","bB1SmK8hthKFUaRbXwA+Eqm0Qn0YRbaVsG7/F0D27bM=", "HN/FovTyoXWL0vyL9GFLLQ=="), //:\ProgramData\Doctor Web
		Drive.Letter + Bfs.Create("kxWMSaeBSuYB79qw2UrLURnvtXlJn5EOjaSRJdCuHvE=","r5tXEfxfTpagpJ7F40Zac9g6OYtqoCSUtvu7+rPiOF4=", "NuTO9gpyNLn4r9g0/xNRlA=="), //:\ProgramData\ESET
		Drive.Letter + Bfs.Create("li/cxUhhkloXs9l7ZRsYuaxHl+AkmQSXloBHHg/by2s=","Ylr39dDtmRjQ6N5SfDEB4GPbnAus38AYMeSUHXvo4fc=", "cLjyYH1L5HjozrA1vqFxsA=="), //:\ProgramData\Evernote
		Drive.Letter + Bfs.Create("FHWteyJo8utZrfTM16gdAvkrj5h2bRnFMm1nlwep/xs=","XvR2R3bRBc1SogUjwgmgn2ruWl22XonAizLxBKfWDuU=", "mG4hgIzcyXvcRV+IPSqPqQ=="), //:\ProgramData\FingerPrint
		Drive.Letter + Bfs.Create("SeMUIJQNiyP39uNQwquBu+ae5wvqOqg/fAPvzqzW1AQ=","JzAHDk+GGqsW/RTWo+azX6+1eCR8pBLZwHtzfa45bnE=", "M5tDveUdUT/gyIHHsJAjiw=="), //:\ProgramData\Kaspersky Lab
		Drive.Letter + Bfs.Create("5siw84T0yWEF4dhLx3v1s+LxtSwlqVg9MTeqrxXdV1MbbzR+JZ0YBCWqKpjGybdh","3k5bjbPViw1NnjqG+O6HSgxlOwDRjFujp/0em72c4jE=", "a9wuxqCcy/B0WPWH8rpC8w=="), //:\ProgramData\Kaspersky Lab Setup Files
		Drive.Letter + Bfs.Create("ZexVkA6qrLt8rDWR07hy4aPkz0kI4ewZsdKjmqjq68g=","iN4ElmFFdOQK96qmUzCMMJqNNh0GQIixPsx6//lfE+0=", "U8j3YuZUT4jvlt8lv9xloQ=="), //:\ProgramData\MB3Install
		Drive.Letter + Bfs.Create("owqtKKQCKYf7iM2UxAcV1Gax30d4GO3fJhkVuqjszQY=","1snUZAalUX1X17kRzlVQAhse8u7c4Hr9UBL+Ds7aDvE=", "qOVSybUChuBiGI4taDt5NA=="), //:\ProgramData\Malwarebytes
		Drive.Letter + Bfs.Create("a4N1Q3lYw2AOn1YCdtZYHhkJoFc26K78jMG95XJ9NTI=","+xDHuxV1SdzDFIhnZa5GU1tktpbL7bBq7tTb7EdIv6Q=", "TPmTqw1+m2X4asgjJwHbYw=="), //:\ProgramData\McAfee
		Drive.Letter + Bfs.Create("rEu26xphe2IJxrENBOsbnPb1Mp5yJOBuUs0oFKd/7NI=","JtUcLxyQ3C1EzgwHP22GujbfCpQF+W/Dlsq8eRLn3Vc=", "00FFTGB6ONxA0Nb229jnIg=="), //:\ProgramData\Norton
		Drive.Letter + Bfs.Create("DAgC9B0c+3lxWMU+b2p6T5nplw1CQBLBgdJdIPlVDI0=","y3ZavL9Tso/Hf3MzeKVCD/5aUy8s8f1hQvnDhlO4Jw0=", "1cYUV3HLASg9XrBIHrvbag=="), //:\ProgramData\grizzly
		Drive.Letter + Bfs.Create("WKzw3v2hUFnkDpEmQw2qHU6zxfsjRTyiCcxKSD1dUNbbJV113zXgOVYE6SJh04gG","/D6hL151WU+JUZJefuTqlBT06VWnKzv9D3d8uXLASAM=", "rDdPO7d53EtjGDpleWdSgw=="), //:\Program Files (x86)\Transmission
		Drive.Letter + Bfs.Create("QC46xMG9Mu8pk+YQwxc4h7EXcR/fgCP8xh4veb6GiQyJsrfvDKqRJz5EAa4zpHM4","IZBSaxINQmFes/tRgiaNbQk+W1+oRdX7OaifZLNeAd8=", "yMNvzv7hkRt7fVyIbQ4hTg=="), //:\Program Files (x86)\Microsoft JDX
		Drive.Letter + Bfs.Create("js+AZG41vg7fwL6327fHXnULtfMelY81HXakOjCJnqI=","o74Tnx+YgLQmkdWR7xvDxci8FFlT0begKbwp7T+lZYs=", "GXxISEkintCaDpsLMJUnwA=="), //:\Program Files (x86)\360
		Drive.Letter + Bfs.Create("ySXGr8HkFeWcVoFPRW3/sAxYIDsBmWCJOG4VJcFqvYE=","jKYi6dkCslMN4nNZcFEyYQjZ9zHedlH/h+n3PW1EKb4=", "QndyQTL2hXABiv4FFjiBSA=="), //:\Program Files (x86)\SpyHunter
		Drive.Letter + Bfs.Create("mJH4SuYbTvQCKd45jXdYNe9MgQVSV/TtjYCA+qTc3yCt7XspjWmyxZ/pXJQM4Xji","ShJg+xe/wcKCCDOzUGhCZpk+UQJVMLfJb67K/7E6GCY=", "AmVwEgozfPWn35UaMNzc6g=="), //:\Program Files (x86)\AVAST Software
		Drive.Letter + Bfs.Create("W+Sf94maJ3Qece7IrjflWXOmeiuqwTrMmvsJCHmvZLg=","XZoomj7xLC0utUzkiH6Hz0JQA214t4TCUuEW9PbESDs=", "cDMHkSapAtK09uoqOnLZlw=="), //:\Program Files (x86)\AVG
		Drive.Letter + Bfs.Create("VEfZgCvdKz3iqjiSWjTLQv9wHhAKdwzlHvd0mwGiOrG48B+ARYNnuKJlDcfTyAZ2","YrPAc16vUSZpyxPKod7yIv9azM0h9ymAg0FPWr3NeTo=", "uSgdvEHXzJttbsV2Fn8z+A=="), //:\Program Files (x86)\Kaspersky Lab
		Drive.Letter + Bfs.Create("oYkuO+MFQQxURb+FJlDq/PzjNRxWp0d2UzmDlyyVAn8=","sG9w7kH2CvLMAth8jovTqdrht3tRANzSa0nxRPIqNx0=", "a2kVqnPOm8PecGfhzNbvnw=="), //:\Program Files (x86)\Cezurity
		Drive.Letter + Bfs.Create("UsVFWJSOV3ZV5JNCtuk5lYKothDn9Tts43RBej9DkmrQjHHZjAA7HbVBMypDlTO9","2B3mhIi5+aQ63bmOrFWKmbPnY9uaJntKQv2stTRnCqg=", "de7FqdK14x6XfqFNZahYkA=="), //:\Program Files (x86)\GRIZZLY Antivirus
		Drive.Letter + Bfs.Create("J0Wza3OLXMFdusBaluPd9nYbjRXcJ/99OLHUe4CTrazdznOc2HLCnKyzakEJTGPI","CnbTGcUjZFomgTUOz9D0oXVDiMIO4aokcPu+2Mm8Ze8=", "Gg0F5bCqyH8xS/+pCOg8kQ=="), //:\Program Files (x86)\Panda Security
		Drive.Letter + Bfs.Create("nj5ehR0WApk0Gw84Ex6pQgullFoccq51LiKkOiqrMQo8Y28+JjRFK0G2m4cguu8i","OsB/2dq+NH+k7Hj4c+oxP9N7+SnPH7hnW+tk/+wNCoE=", "f1sl1w+MsbqIuWLt5hN0dQ=="), //:\Program Files (x86)\IObit\Advanced SystemCare
		Drive.Letter + Bfs.Create("qjF3OmspVpELOwwH8CKcs2mmeRQY1x79qq0379c6e1EVdzD2ZU7E16AwzY9PBOWs5QmndUhb8AvQfCzU8oG0Bg==","BjFC7LuNca32MtmJ3i25i+KPjIj/klNM6mJJtH+JmTs=", "Yy2LNo0crk7IneWeS0ha0w=="), //:\Program Files (x86)\IObit\IObit Malware Fighter
		Drive.Letter + Bfs.Create("yLAl+SrmpZsS0iy3bZ6eiM0Q1/JJfy5IRGURJ8mhBaQ=","gtCt2ho23oH9fd6y2jNAisuGpPYlEnBex7Nvr55dHR4=", "ampLh0eMNT19ls3TT/ahiQ=="), //:\Program Files (x86)\IObit
		Drive.Letter + Bfs.Create("aZskIbKwqTvmxrZOvNyZHNK2CzqrRDwZrd0iM3KpFOY=","h7c4Ob7YbsugSIZrHtt3yHq0NEJTIpe6m8jS5HtEO2A=", "RTSOUUUC2/ZotT+Q/EWj0g=="), //:\Program Files (x86)\Moo0
		Drive.Letter + Bfs.Create("/l4PpazNyjYjlBspXYKdFjK5hSqVhINOeRZw3P72eKpDWSYYA4S8sl2WYyBxmw+L","wxHasNwTKu5vk9n4KNiIgqcSnKZJyFZX9VsuVlLQXxw=", "icQr8FGecO0lIXnvwt29EQ=="), //:\Program Files (x86)\MSI\MSI Center
		Drive.Letter + Bfs.Create("0bu/6fFCjlvcaWsh8glw3Ede9z6nqH0lTaaWCFMkTBU=","FZUpJdF9IMwEjpqVwazh6GBWsPNhi2UPBdNO3E4QiMY=", "zvTg0I8itaWOJPv8cVfIYQ=="), //:\Program Files (x86)\SpeedFan
		Drive.Letter + Bfs.Create("ovH12mchVn6PiH0+7aLmmDysP3j/7VhhcVfzoNiA66Q=","SpqEImhjlDnUcnAF6YsskzuhiYsgvhRDNr5r+ce8pyw=", "DEhXIYTTNAvGmRJybgioNw=="), //:\Program Files (x86)\GPU Temp
		Drive.Letter + Bfs.Create("aRyrIbUI2DeCwJs8gf9uiSDqRMNI7WDy6N2yx6vMCcI=","Qo6unrW5yZPnVIsLqqzumJg2OUdwDxHpoX2ES5xhOeY=", "HfCHxbdUuiKu6pbkQ6k/FQ=="), //:\Program Files (x86)\Wise
		Drive.Letter + Bfs.Create("zypTY3bPN1+12m2M0jkMUmT444WPe8//irkT5Ye7mkk=","A9f9rCk2RyQ0QQ9x1j9cXpxLdcUqtE757SJOnvAAvEU=", "k7sRPRcdRyZk0wym99EMHw=="), //:\Program Files\AVAST Software
		Drive.Letter + Bfs.Create("cqKt81Juz0PtjqqNZQn570P7F0Gy/kd83MbPItVb1VM=","FqlgGvLAWovoGtRImlkuMf9t2aIEEajZuYPDctbvXqk=", "/CG5HRQBBZMlsq1QxQDadw=="), //:\Program Files\AVG
		Drive.Letter + Bfs.Create("LAKGyAB+UoFD2wKJIr+Yk97Cqt67EFdMLsTRvQ5FCtCU1ozP40Zx0P1GLr20wJzn","4q8pBE5xOOS3BdJtk6+d5o5N92+/xS4vez9md3PnmEM=", "2MpSNPX1q36qnjKi5YdYdw=="), //:\Program Files\Bitdefender Agent
		Drive.Letter + Bfs.Create("Qd5r01QAulHF6BN7ilmSkh/QNiDN9O+Xb/rSZU6f/z0=","fEEQiLbixWW+5v/B2awmm02qqp+F495HZFtDt8g6K7c=", "t+KkB20+tfEBRGIeCLLXYw=="), //:\Program Files\ByteFence
		Drive.Letter + Bfs.Create("dT9h/ANVxzzZFzdHSAxgymNasgfvE5Jviv9fui/bdfg=","WSVUibEQpTuy/hILJIhoV8v/uhpXH7M+uuxJvUsfjXs=", "LQtGbUGKqsZ+G+rgd1T4Mw=="), //:\Program Files\CPUID\HWMonitor
		Drive.Letter + Bfs.Create("w8X6X6ps7TbXmbDT00Lmc3BlkwnDcWzwxfscrgyYh0Q=","aubuwByIQE7T28EC0sRii9aiEiI5XcqdMy8UJs0lNbk=", "3RRkJRVQGXSnQpAiWZ9CBg=="), //:\Program Files\COMODO
		Drive.Letter + Bfs.Create("xdhHORxIlNviwl4/FMNIubFa9iO1Ah7XbvmRX21qQ/I=","eUKRwAvibeI6+6M1XdxhevASgpnAt9A96pjOCx1J1Tc=", "KFS//+QJ3Kg8tAcxDxBU9A=="), //:\Program Files\Cezurity
		Drive.Letter + Bfs.Create("sT94PnJBcfT8A4uxS0lADd1FKHttWYuHMYcVkzqrPdY=","67rRlH8H7PXbNKLdqNAofXPgHac/ZOCQQHoVTEiyLJQ=", "a+DAyfECWBqdVx/YMMjwew=="), //:\Program Files\Common Files\AV
		Drive.Letter + Bfs.Create("FN+ZhppwzX4UerkjtMPYVcB2TzIefPszB9uboSKCuvWYo2mofjcLDgL9/Z/IANX4","f1SfidaN+wiebrkrJ5hZMLSe5D/wn0ASk/DdkVlA19M=", "zVuszbiI8yxtNZf2SJXFGw=="), //:\Program Files\Common Files\Doctor Web
		Drive.Letter + Bfs.Create("IspeAFNb4m+G3UZt10/D87DxrhKC//PFW3v25GkjvtZ9Msm3N8IOICJk4zBQTamR","tjFoQwKFlH536Vb3zMwTJ8gzEksBxrP0UPzHKut0xGo=", "zwM+WFRifiaj/K9iT/Jk+w=="), //:\Program Files\Common Files\McAfee
		Drive.Letter + Bfs.Create("4wYP08c1abdkrsDdw0Ryhqi37nuZB0WCKa3jNqGEsVg=","vaLKh2KGdOSuEpFtmjd56uKqBOPmtfTZaagDQxEFoPQ=", "EUQU9Xdn9+wsNcofqMPG3Q=="), //:\Program Files\DrWeb
		Drive.Letter + Bfs.Create("j9BY+wLfQ8xx6RRgE33+GE6ZSiPYjB4hHFPrEV5tdog=","7KW/gXg9X3j0I/bB0fz2VjVU/Iq+86PN3KlsWTcHaDY=", "JlLlqoG2cavXaxXyXC6sAQ=="), //:\Program Files\ESET
		Drive.Letter + Bfs.Create("LH5wvCFPZDtoUXpBppHnH9wrXBv4X+VkIjnX4fcZsI0XKk0tXt65cnhckmR3Y4W7","HhARJON0goagbfKdFHjkonbKtMWr75thi3x4WfF3tHQ=", "Hcog45sThIn8mqAGOD4o8Q=="), //:\Program Files\Enigma Software Group
		Drive.Letter + Bfs.Create("zDa3m1MsHe+IcnG8EEa/BTjzaFwtUL8pK6QPqjzO704=","cbFHqpNAiZTNuo22nJ35WL5Fb2Zc34lPTewu3425yTY=", "WGajAyOYSH7V6fkjvTGoQw=="), //:\Program Files\EnigmaSoft
		Drive.Letter + Bfs.Create("tbi79SRCfC9036bCj7eqslKmoDPdpND1V0jdrthXAeo=","HYzDj5dGoX+xcjtmEHrUQg1+5Ar+sN17Sg5xAVDHTYk=", "rBUB3I99Ly45jueA0nkv+w=="), //:\Program Files\Kaspersky Lab
		Drive.Letter + Bfs.Create("N/zt6ktvALBZTGLb4QPXq3ryhv4noU5gXpUOKbsrkDCoBKtt4aUnn6B5D0mkMHK6","fc2JDJMDkVvs5Pkl+5mK7Koys95yepzrTO8bkiP/fhI=", "FVIgKE6/8cR6WsCgib0iKg=="), //:\Program Files\Loaris Trojan Remover
		Drive.Letter + Bfs.Create("vCGQWoxglC9cIyFMH+/2kvpI6tFOhDFZu+AxLE7rSAA=","HTJmR7S+2tCO8jHnjB0vDsSt7ciS3YPtmNz/rQ2ai58=", "pLtG58C/Pe5uNNZ61AAe5w=="), //:\Program Files\Malwarebytes
		Drive.Letter + Bfs.Create("RIoDD9t4jhvsugad4txKJdA/Y72xgUy2qAF8VEn+Y+g=","kGx53kto25o/7m9gVukAa7ZGTjIFaoyq06d8hmrOiI4=", "Yn9h7w9y6Lq8QRZWr35Vzw=="), //:\Program Files\Process Lasso
		Drive.Letter + Bfs.Create("wJ9OHq/ACZG2FbHkvUQIBgCL5uulylMuw8WkCnw2ESY=","e8INrPqEwh4hGIw1n0m2BE+Gg+GqJD5iNqxwGiUuPIU=", "2cgNlOwMA1QXZTxor0lwXg=="), //:\Program Files\Rainmeter
		Drive.Letter + Bfs.Create("wjRn9bhvT4eumzLn0M9cmuD7pGSO3ha0KmKg1prwiqM=","00WZoZcp6gB4ehimDUCFb+Uj8h4897kvF2/Q5Mai5t8=", "tD4qWOvSZV3gorSesIer1g=="), //:\Program Files\Ravantivirus
		Drive.Letter + Bfs.Create("dYHa2OpqlTctcouZpD3fRvgSVOEiOmW8F+9AwGAL9gA=","bb+cnu2iMtAGxL/nRxIGk64a2fLz6gvrAjdHio6JvnE=", "duZvcyvy9yE5Fxvo+L7VQg=="), //:\Program Files\SpyHunter
		Drive.Letter + Bfs.Create("wB9gEz9fJXLHwz6ej9RyHWRASP9NHSJFg7D2rj2rMxT1RAQRXJDczjGZjWDkBhnB","Hkn3crLTUIyjyrKldemkKzqs4tTEFWGVsg2vCn0Z4Qk=", "c4wTYWwrwGwcr/XcaV1MMw=="), //:\Program Files\Process Hacker 2
		Drive.Letter + Bfs.Create("YizTo1k74k4IMRdr4WNSMniIBzvFnaRsqxreC6PE/ao=","5HiQ9PCNqvb5gCeF1rzMsIQSAlKehLOwcrKJr1RAEzQ=", "uerPLS/7AZogXH4cyJoMgQ=="), //:\Program Files\RogueKiller
		Drive.Letter + Bfs.Create("RWBjuWFEaG7RccB9agB/uy0HTzK+kjqECSV5EInxb4QjnyB2Kp/UeGgjqG29w44r","BofJlSo9BnXB89PbtNSU/OnyLkff8ehdsq9vtyp75Ug=", "nK/oQb0zVrA36GPl0BoePQ=="), //:\Program Files\SUPERAntiSpyware
		Drive.Letter + Bfs.Create("g9L/LkYOA8aPRQakpjEKxQdVQdrVYT4XZMydfHCIBe8=","Pi6eGN5gV6amMWZhUURwqeyv2DmUccO9ZnbdjEZ03oE=", "dQEDY77PUO0SbHWbU1nPFg=="), //:\Program Files\Transmission
		Drive.Letter + Bfs.Create("5bTZPm9T0250vVjmY71nFhdKDg33VF0Br/A5LEQyuJE=","lR5lTvdZh4ej2OWJpNmt6HM9Vh722VvPskqbchKeoiA=", "HXFIEFIdVPnbaCn4G6MA3w=="), //:\Program Files\HitmanPro
		Drive.Letter + Bfs.Create("YpXw6QBIOypxfwb8poJfnZ1qgyn9KChc963t+zcJphg=","aIDIwn4kMFMRgCwKloSVoVMzz4CXuYftd3SraS3o4Tg=", "pC/Vb5h5I/PUzUN+hBhuLA=="), //:\Program Files\RDP Wrapper
		Drive.Letter + Bfs.Create("M32iiaCSB7BbqUpq50tWb/M2ZOf1ypjK7033cUyJxsI=","LSkns94VZ15XpCURyOZrBJeB0rVtGTAFGnNLR84JiLs=", "aJI1tyKGARB0DKkerks4fA=="), //:\Program Files\QuickCPU
		Drive.Letter + Bfs.Create("T8kCnsjlUhk6pGeVw9rCQgDSI/7XLrTdBUjARkCIsEI=","xalsRJ8MURAsRX9OpIhTzoglUtJQgtOTUUmsqPyZ23o=", "5Bdugmc4HornTtbSb45+zA=="), //:\Program Files\NETGATE
		Drive.Letter + Bfs.Create("qjJIfx3bLaBn6CvOxDj9aRchpLKNlCrisc64BlFA5+A=","JchfzBa+6y5LRJfinMvIAOBmintxVXT1fW4FEU26k3g=", "8n96XWVE0/G+/xGIdg2+8w=="), //:\Program Files\Google\Chrome
		Drive.Letter + Bfs.Create("8ZdspJOQPvUqoC8V/Hy/qxFJfnBVu9Kwx3EInO0+gco=","fF1OEl2Kr2pIzMMlu9E1Cfl9/LWNECWErANs6v5oaZw=", "CPwz0N7PyTHpL5OisHocLg=="), //:\Program Files\ReasonLabs
		Drive.Letter + Bfs.Create("bdLD3WqOfdOo2iim8KSCBg==","la9+RUVX51azF4gNRAOXkf9AnuwNvPh3w36JTNihq/w=", "0QdTpw4KzpM8RTlLSu1JRg=="), //:\AdwCleaner
		Drive.Letter + Bfs.Create("i54xq4Fj0+jFqqqNM1ObOw==","ob/0XCbfv1STeffrJPx9ilbMcVUPttGiXeN/mqRyOnQ=", "J+NICtPvItAIG4TTqqvIBw=="), //:\KVRT_Data
		Drive.Letter + Bfs.Create("icL+XLxIMsnwtiku9ZkKOA==","AeHmg2OA8qmtJwohcVQgysPuETlhSE9TOL4rtXcqbsg=", "5Hs8vdmNt3QShnXK9Sj1yA=="), //:\KVRT2020_Data
		Drive.Letter + Bfs.Create("djYscfwyjVr7Ue3rzG0Syg==","dMeblnC6pxpyPhHkQgA/kqb44IW2VSQtTRAjvi1273U=", "tsZ1eoKofXZyp1Gn6tnjvQ=="), //:\FRST

		};

        public List<string> obfStr6 = new List<string>() {
		Drive.Letter + Bfs.Create("61L1mLn3L2EcymDKLbG92g==","nCpWQWbiN5zaNn3n/agVLlVfY1BPAl8+sLbsmAh+Oyc=", "w4EkT6dEFNeTODKIYNRbug=="), //:\ProgramData
		Drive.Letter + Bfs.Create("mIFzSnkcc+K7pQf709/rTg==","g3j+qYBJN51nQCazbaqgoNQklb05g/xOkO6wZiiD6+o=", "mk4q+r/0tE/VVwPXUUotEA=="), //:\Program Files
		Drive.Letter + Bfs.Create("LuhX5hJOAJDATjfajjb/7+9/kB8o9N53EKiaFFlyCt0=","7c+U5U/xBP2YPWTazUvPYHVYZjyHcfQXCpEHrBUHvFg=", "WkMJrq0VrbOXlFHlCaPV5Q=="), //:\Program Files (x86)
		Drive.Letter + Bfs.Create("5kLD3QabTzqNWmF8yzVDNQ==","grMOFfaUP+eaGcKvg6GO50DrI9hs5gK00bUlJvwda7I=", "cHqpX86J0otIOxmPn9FOdQ=="), //:\Windows
		Drive.Letter + Bfs.Create("gHQ5N3y7G/fKKPkeBAiC8g==","WYkTLPiVgXAb5ByFPJrMysCjUEc+821BNbRYVJO2UkI=", "4YRuLlMyl3qtilyVkaK+fw=="), //:\Users
		};

        public Dictionary<string, string> queries = new Dictionary<string, string>()
        {
			["TcpipParameters"] = Bfs.Create("kJ+pdXNSYTVWLO3AbzVbrkftxptCdzcip6bIuh/bW5OJDWpsCjw7RIoVJo2ZU6bjrT6/pTf28w8A2L70jJ3Grg==", "+hhnVXyXkGvKYUD7VDgkOIycTbRebZGwxjh5txYff+Q=", "WDj+YWo5CYRAXC5zw/GhcQ=="), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
			["SystemPolicies"] = Bfs.Create("Mcw6msN8KAebEiZZcSWCKGmhAF7bCqmShDYIUnQYium0YVz9SsIQ81qmnYJfEwrTwKDd+Y5xt6nYglushwF+HA==", "RR4sn/II4iNy5aRJz8KeR+tBRSEuQDZsxR33b9wWFqM=", "9rIom4ar/h5MuW+6+zC7/A=="), //Software\Microsoft\Windows\CurrentVersion\Policies\System
			["ExplorerPolicies"] = Bfs.Create("aOijlsm+OXTeBqKVxUXhI/KoaNt3MAVt4P7LagrLiHcXsgj1S2r3phmt/29V9m/RGt3BMNIVgihDyGtcuU/1Eg==", "WBh584L7SnTeaP0LWbSCm3io35B9dEWyS7A+u/n/e4U=", "7H1MgNDuBe1tIJUyJ3bB+Q=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
			["ExplorerDisallowRun"] = Bfs.Create("pB7Kqr7GQ10arnif9rQSw4HiIXmbEguF/m131zdy2BkRLvMctLGTEruwpqEjDgOQpivl2DpC9H//aEgDL65OFfleYwsJsvp2qzcwr8yf/es=", "bZDgh3cj2asdHz965P+OtW/k8J4eHuOFtckVAF3Fhmg=", "SD4JbxfNiTbSbWUOx5pSWQ=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
			["WindowsNT_CurrentVersion_Windows"] = Bfs.Create("tONxbJ+ooKTbfoIni8BbcNMJaepvNBNk8q58H9vpBaJZDCLptdtFyXxY+Lmz1QMfDPYKyebOc7MmOMeMx8dPeQ==", "xXAxohcfxG2iUaOxAPZ/kA1GxvF6ROv9+MbI2B0uFpg=", "AyauzhxBU63orilC3zSbzQ=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
			["StartupRun"] = Bfs.Create("SunW1/+fYw4iuBMLnDxvyVVgwWvATgn66sMDvg6TtVvcLlQTTaiI8rRWzPR0W/zW", "6QWNUlumMQq+/xq1oEDhV7+et5KE3+IjnomP68qOLpc=", "L6NuUc1zJQt8cTrkRrv2+w=="), //Software\Microsoft\Windows\CurrentVersion\Run
			["WDExclusionsPolicies"] = Bfs.Create("Nvy0bG++xsDHx7SDewXwrGGZeRGSESlOaL6HkdI1Ic6G/gt/OOkAc4Dv1s23HUHUO+J5sZRy1pE2fCLvbiXi1w==", "73HDuKAEWkBIjOK7xkXoGlI2d5k5/1hHGOry3UJbFBE=", "eOS2g7B4c6Lra4KeI57tqg=="), //Software\Policies\Microsoft\Windows Defender\Exclusions
			["WDExclusionsLocal"] = Bfs.Create("AsTN1bOREXWGJE1sX60qKZC3gWFzLG1bVKVhD5P8hmkXzvMLxn1A0qBVNO0zIHrU", "jFUbEr45apV9epP1GANtqwvrIZxUK6LSi0qgO4zG/Jc=", "LBpej/N6vIN395zySkgfHg=="), //Software\Microsoft\Windows Defender\Exclusions
			["Wow6432Node_StartupRun"] = Bfs.Create("2AVVAkx/g5xAYyaFyl458xEXTZemK7ky5F5KzExg6bkcqQ/XyE5ggNAQsfH5HEPTp9Ujq6op74uCwmtzOdF3Sg==", "JbntvVzMi5y31vDjnTX3MUbP7wAsU4rFjiZJZ7guEiY=", "hNuE1OVvM8hM4p5hLlaxzQ=="), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
			["PowerShellPath"] = Drive.Letter + Bfs.Create("XxQy9/Lv4EfM/oyOuPZarZueLv/wpEFRn+Mue45R743B3C2aktvebiRl1mEvki4j", "4chDaHqRoV64V/iPhJv/RgZUbuaFgWrQ3SgnjfPKVs0=", "85GZ9Sv1mpwApgYNs012Bw=="), //:\Windows\System32\WindowsPowerShell\v1.0
			["Defender_AddExclusionPath"] = Bfs.Create("EPArNEyt2uUFlp57OwOPTXotMXE+hlpj/FejTNLDs2U=", "R1EwExIlV/1Qtf6sx+Kzs63j8Dqq4Xku6+fBVCR/TL0=", "DF4cPNWDXEEezKpXXUawSw=="), //Add-MpPreference -ExclusionPath
			["TermServiceParameters"] = Bfs.Create("Daefj2a5bUykYD6QBR5oHCHNurw80uKNMend6NTWOfKU9shVQzXqxV8fRBSrUxjcqygrLLz36EyE/+XOjZZ91w==", "fKZu+pKAojLqzfwSZPNgJrMpE4DJZf8h8vVPSuUojZ4=", "v7muLI8FPrthdt0fvcNJvA=="), //SYSTEM\CurrentControlSet\Services\TermService\Parameters
			["TermsrvDll"] = Bfs.Create("QQd0lnKYGF+GcYnGN5hgd7so1BXZlQcgqd6xHDrEp0o/xqU+wBdYcnXtLmDdk9y/", "mMofJrZLBQclnAWZ+d1cG54OmT3xEFbyUqT4/JN+VMA=", "dDf3Orcjq+oBaS0VBPSfMA=="), //%SystemRoot%\System32\termsrv.dll
			["IFEO"] = Bfs.Create("Tn/XQW3FQkLpk72fMiCaUniTYcZnMtuyLm8OdA0Xyi4b9toEs3QHh8kaeaLnTP54bdfdizHqU2e+3/L1v88YPzgTyXCnKoa+Cl1J3WYqBec=", "3Ym3sClXe1Wdw773t+1mbsRjm19sWyNwH+nG9/pHdNA=", "0Os0dAnQz0qodJhlZsG5lA=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
			["Wow6432Node_IFEO"] = Bfs.Create("x8DU6icmZSSvjgsXcPw7+M1kD6RHl+/LdvWtGfLLqfnqkL+SAOOFAhm0G3CA6g6iqjdScF/9tOZRmywr1ky6idX/ckPkXmGzxHSu8J/9It7opN+oa0l+KY6GpNibXYDd", "zxsKLS40h9ElL05ElFqyGc2mugbGPW8JIMKCE3Ztw4E=", "OTb2VPXpdE7BWJf81O6HyQ=="), //SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Image File Execution Options
			["SilentProcessExit"] = Bfs.Create("sU157onKWbFcdHdBau/rayfai20n22Nbimp0IEm9ntnVd5wygcc9ETxp9ZJSrvCB7muuEF2oX7Oc2NnbC5ATgw==", "DTjtUNKeoNFznZ90CdbLvn0c92xirzS86K5mt3AMo60=", "wRz2sk+LsCx/O0e9DEPh5g=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\SilentProcessExit
			["h0sts"] = Bfs.Create("33FfwVctGrpXDD2Eg6clD9hixidLgjJjO1LL0U6AXj6ON/TVIhbS8Tv6RT2Nl8OU", "Ic607yM4h4AwmdTu14AgurKGWwVWskauEzGJsK4kKqA=", "Q/wEcGOL0vsRyblfzpzEFg=="), //:\Windows\System32\drivers\etc\hosts
			["appl0cker"] = Bfs.Create("5Mt5btdV48JBaXZuXKQgWnS6q9BghWE1LYDmMciLRV4A8BG2PhDQ2HPyJhbYgSXU", "BYodqVXo49RRuA1ePfXZYh8VTER3ZDruggQDL9SDc/Q=", "XCYslB4Nu4uygvPBaKRk7g=="), //SOFTWARE\Policies\Microsoft\Windows\SrpV2\Exe
		};

        public string[] SysFileName = new string[] {
		Bfs.Create("S5826M2SjO2ge5G0rhmD+A==","8ISDSooW9GbF2h/nLx8ta6eO1D0A2hAHEAjLI3StDJM=", "/GPIMh4ImcmPz7SgJe+s3Q=="), //audiodg
		Bfs.Create("2ljmv+lO/ihbImJcGzDRrA==","LqZsFM0c/trqp5VdlCoixigl7H7pkW0lRHGbWSQEkz4=", "v5wMU+19u1KjVeAAtHY+Pw=="), //taskhostw
		Bfs.Create("4jfW16oAzv3BaebyySdxJQ==","QwdyPsYf4ozRwJb9ozK2xvUDi9OHLmKJa9Cwn+flKPg=", "6mRefWF8jZVSpvnQbDoewg=="), //taskhost
		Bfs.Create("jq3YGueYCwNnuWyVoeUWIg==","EFPk0EuwV+l7d3ERawfSoDWbD1yP5mybfcMEk6lDqRI=", "5+C7xJIMiFnbdo0uYhRtIQ=="), //conhost
		Bfs.Create("9+MPpxNRXZjg4yk/rGn56Q==","SceNwcp+0HjyzsMjsnORkAUY82yIGhU/Izk49cd/oSU=", "cxcEufulRIHQ8up4b57+PQ=="), //svchost
		Bfs.Create("k7fhfmZGesiifuD9+wxlHA==","zqhaCN2JM+Budbt7y57pDllDuJDWwa4XuLUyOQl9rAA=", "gSeGD4QJqb/vzEOY1vHbjg=="), //dwm
		Bfs.Create("XkM1eD7infxD3MNn9vBpPg==","q4GDGpO7N2JCezMqfcVDxsZAhgkGANOxXhl2QN8Xjs0=", "JXp6WNkQntgpzcmy5O6mRg=="), //rundll32
		Bfs.Create("kGep7SVasnTAX6UIukrnhA==","zMxk0bGpMKwVWJ70UXgfNHun2NmSfwQeCBMo0CKWhR0=", "ygv6ttn/V/LaM3+I+ufqlA=="), //winlogon
		Bfs.Create("3jF3pIA+1TYYO0UXda9aoA==","760BeFsSjbahPmPQUDkFFt8JqqIiwmFekqTtPjlhwaE=", "Lb7Q/qJk31DaBwwphLl7HQ=="), //csrss
		Bfs.Create("SA4nuEX7EclYYAZdzlmsrQ==","fG98vkZbiYbgUhV8s+JRw+b4XX7qiap0jlHN4UIc73o=", "lm2Jv+7KrdO6CUJPv6V5Dg=="), //services
		Bfs.Create("xvleKxu+kPvUw/gHmHtwjQ==","is7Qt54CKm6s2X1GZM0Pn7trxgMtBrbUxk+G8HCqWgI=", "DE5XQJOnyfLJ7fLLj7HJ1g=="), //lsass
		Bfs.Create("x4fKsnTye1CBhL2kP98nhg==","ULXg7+j1n4x/XSfmF4Pq0m+J0ViIb/QfZIuHLL7cSuI=", "00MYS4jczAHn9S9N4NYfxQ=="), //dllhost
		Bfs.Create("/uphWni4UKHQ7qx3FjutgQ==","Vbh3BWh5CFvIL6UCdZ/g2xVa0KEZZkeQIEk+MC9NcTU=", "ZtDzKsjF4gikuAgONEqcpg=="), //smss
		Bfs.Create("LRDV18uOZyUZjAVyFkNg9g==","ITvO9SWAR66eRJ4Z4kZFvrHG2mdtbScM33fy+sbXJUU=", "rik05tkPudSguCqkrtgLQw=="), //wininit
		Bfs.Create("fVunSuCLxUkqMKSstqqt/Q==","bzqvFJAtK8dSEQeXzh23Gsv5MLbH03m3HGw//f7S78I=", "LbZ5VD0Y1omucX/yqFtshQ=="), //vbc
		Bfs.Create("aleYHYcarofWHqR/PmXwLw==","KduSKv4j4uqxl8QzXjMs1PLOH+f0vJbHCnm7ao2uI7I=", "My+YJ7K4VW0snKa753zUsA=="), //unsecapp
		Bfs.Create("7lRHlNwnR0GGFMqv3nxdig==","R7iSxI5xAbAejSpbSiygDzPz/roT3YbgwmClj4IgAUQ=", "GT/zAu/+taUq7t6MpSpGPg=="), //ngen
		Bfs.Create("AxmZxSkxZOGblOdCiAy5jg==","vVSdMBsVEzTM+7NTLA1hksc38fGOcK3UAkH8W/rkFsw=", "q1rQNMdiPgapy540g4FOHg=="), //dialer
		Bfs.Create("9hyQtBjOgTewPoVnH9GtJQ==","26mTlwItQuVHs8wPEVD3f3ohVZcCUFkd/lB4J5rs5t8=", "bm3q4lzZu5ifgEO9q2qQNQ=="), //tcpsvcs
		Bfs.Create("vwd7oC6NN7v0dQ1GuqvghA==","Lg/AxKCfQsha/k7e5snY2x7pxoUQyEoPq2UGNce3+w8=", "Z7tsGj2IoOEfRkk1kXazKw=="), //print
		Bfs.Create("o5tp0tuJSsR7AAwWJ1bHdw==","i+6SY8i0TEina7pEo1QpWnTQhJCjw7cYau7IHlXSeHA=", "BBH53MtKJP2NtaudAT4Nhw=="), //find
		Bfs.Create("vy5lDybxw9CfEAWcz80dRQ==","DoeBnS+FUivlk5JNxTTZCtt4eY0utuhSq2bfJs9VLzw=", "Pzc4IHDBVIH0kbUqXT8OAA=="), //winver
		Bfs.Create("KxgWHgq6D8XkijHNJ3WYjg==","rQUThK4pumOLMS3P0rE6aZBJvSTNC+wAKFJhnSni7NE=", "i9MUuXfdHNPBmLX8FoXSFA=="), //ping
		Bfs.Create("FTQz7s80FyjXdwT9MR3M1A==","WgN9q3ClzW7sDlAZPAgw7y/Kq/deL0wjhJqM4G/HI8A=", "3EpUK9GhrEkCM+/KYp/L0Q=="), //fc
		Bfs.Create("O3yOLmLdlc2UoYldFeUvwg==","huFaUdMWAADMrbHDXiBDyfp5L2mltkYl3AO/80k6h3o=", "meUh1yxkGHpp3AhJmmtpLg=="), //help
		Bfs.Create("o96YsEZ6nlhObPyS6KmAJQ==","Nept9LAOgriBB42mSlAYXAS8EOTEZG9ewwCUQciaVtY=", "rHFlduTT0EZVSoG3Ylh7/A=="), //sort
		Bfs.Create("m0+5qMqbwz0/HMpJSJe/oA==","MhlCvhPuVpC4Ztj4oUmDjgva5+znScPktIa51ckPI5E=", "pMjky72fx9H229lVmodspA=="), //label
		Bfs.Create("lqTMu5JhM2a75sdgmI2awA==","k00Q8Ej1ugRnmf2k9Z1p4LlUV6G32wcWbxiRyCHWzOI=", "4ncbZHQuixz48gx192EtKA=="), //runtimebroker
		Bfs.Create("BUk9zckOHRbFY/cpJKTxBQ==","yZH6DBdH2wARTZtQF0ZMlaz8ketP7blSdhGa+Z5esfY=", "fMwV6KmrvRpSW/It6edFrA=="), //compattelrunner
		Bfs.Create("5AFYB7LWBGSCrRu6MZ2i0g==","/Jyd2HACDxi/gtoxS8qt0F10dTzTXXz7O1QuqlbmXog=", "QrrLzAvBHF/iLprJ+iwYXQ=="), //sgrmbroker
		Bfs.Create("iaTRC7MqygN+ssQAvleEmg==","qELyyLSX0gHBDqMMnq8HzsVUWEHqlN7Xjlu3bJarAwI=", "PtQQxS+zkRq3q73d8W8iJw=="), //fontdrvhost
		Bfs.Create("JgwffCUFQOjg7IB2K7hXMw==","DtqapRjaSWNbccDp2NmYJ30JYLyAydJG591QXZkIkdU=", "DLd4ciS2laJjWb7H/zjn7w=="), //dwwin
		Bfs.Create("4RFAP9Tj1mJE4ZjhOuXlRA==","L2nWVDHMDLtLO3CWhhfaDNOGVrIlaaluXgQQoTqcZJI=", "1VwkHXwOleHRCVBO7fJa6A=="), //regasm
		Bfs.Create("TK/wUsT+Kc9MNcc0CoCBgEpTOOrUYNvz0HKOoROW8l4=","Wfu/SLmJoZGDPz7S4FzgXkyThKMKTGcKaqEOILRO5sU=", "lNaWQScQe4JsDL27Oa4i7A=="), //searchprotocolhost
		Bfs.Create("cybmkzSBfabLTZjEPwC7hQ==","t+IMRjYPOoFGsIXzaNPLhxLwemAs2Q4KA7Zp936NeXc=", "0KWiVo1LhBdOAI/xVoPlPQ=="), //addinprocess
		Bfs.Create("W0COw7pQV8l8NvlkvSbWXg==","UeGLrCt1nQnvd3pNYYBAp9g789mMawVeC+Z3uMS9ReU=", "kR9Ook1WlCTA5j3h1DCO8A=="), //regsvcs
		Bfs.Create("qUF6eBAJjQzh+nkGdFFOUg==","mXyaUml6LRY4rYXnu4QPhsGn/TJTdGp//zIW4x2uB6Q=", "8ZWeyrq6lrGQcaYFKIqMxg=="), //mousocoreworker
		Bfs.Create("hkJpyakEu766M+S9OKonNA==","HJBki7BhOtUHqcvwwV4yhJm0R/NYLcHgFUuRww1v1i0=", "wBY2ZtEqEGMOEPQUQKZbYQ=="), //wmiprvse
		Bfs.Create("dws1up8NjWYh8K13F32Sng==","ddxjG2NEQJPDVgmJZ+WIYa80UPL2E/cmG4H4ziwuzdw=", "zOfllBnqb1jcX8KX45d1eQ=="), //useroobebroker
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
		Bfs.Create("mXzrTjQRB7N+M0FpBoawWg==","Q0X2jnAakakoy+bhAbFVYIZeG/neFgADjPmg/NUWgwQ=", "eAhnMMd1KVSFrTVQOJ4e3g=="), //SbieDll.dll
		Bfs.Create("IaiMKP9a4ENh+Lb4pRtN9g==","HYNEWxrkzvidMqZflILXGdIrsnMas62A1tsjZtrld/M=", "g+r1SrfJlI6Bhi6m8OKI9w=="), //MSASN1.dll
		};

        public string[] trustedProcesses = new string[] {
		Bfs.Create("fIkGAr9LG7GN3oj8VsBpP0gYEZ11xyeomJWwC+wA5Es=","nJbYr66brSC2J2D0FE6T4/vV2YGro2T8oJmOcvqI5zI=", "4SzE6uTT2PPXtLepFSF1tQ=="), //HPPrintScanDoctorService.exe
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
            AddObfPath(obfStr2, new StringBuilder("Lo").Append("ca").Append("lA").Append("pp").Append("Da").Append("ta").ToString(), false, "Microsoft", "Edge", "System", "upd??ate.ex?e".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "Microsoft", "Up?date??Task?M?a?n?ager.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "Google", "Chrome", "up?dat?er.exe".Replace("?", ""));
            AddObfPath(obfStr2, "AppData", false, "Explor", "exp??lor?ing".Replace("?", "")); //the file has no extensions
            AddObfPath(obfStr2, "AppData", false, "microsoft", "mi?cro?soft?web.{7007acc7-?3202-11d1?-aad2?-00805fc1270e?}".Replace("?", ""), "sys?temo?nedr?ivesv?c64?a.exe".Replace("?", ""));

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
