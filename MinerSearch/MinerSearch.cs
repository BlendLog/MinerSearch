using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using MinerSearch.Properties;
using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MinerSearch
{
    public class MinerSearch
    {
        int[] _PortList = new[]
        {
            1111,
            1112,
            9999,
            14444,
            14433,
            6666,
            16666,
            6633,
            16633,
            4444,
            14444,
            3333,
            13333,
            7777,
            5555,
            9980
        };

        readonly string[] _nvdlls = new[]
        {
            "nvcompiler.dll",
            "nvopencl.dll",
            "nvfatbinaryLoader.dll",
            "nvapi64.dll",
            "OpenCL.dll"
        };

        List<string> obfStr2 = new List<string>() {
Program.drive_letter + Bfs.Create("nqaTn3HAuxm4kAH/2eVXQDV4aSxVljFuOifVTGvwdl0=","Lm5ahjDreV7Ib/whsM4qtL/JPKdKyTFEwA/oeBybBgg=", "mf/WvhS15mNvdEUkekAOmw=="), //:\ProgramData\Microsoft\win.exe
Program.drive_letter + Bfs.Create("HUpNbZoArTAT/SOlujvsnCBi2iOD0AuyfmifTa79y3cUT0wDrBLdxo8l2/3j0XD7","lJ2UeHfLQqYTx1HLCvy+uM982s1f7MVsY6hZauVAYpw=", "DfaGM3m7iccdN4OzcGeJnQ=="), //:\Program Files\Google\Chrome\updater.exe
Program.drive_letter + Bfs.Create("sghABeTCz5fePKYuR7BDFP7AhlDnqDRJu/HsckyjbCBS0ftxi42uyZ4ZfEdQl7mR","aBg7g9KIpfT1uD0hOZCosULC9gyYJP8BnmzhwcQmZvQ=", "UB9/LMfjlHq+YoQT61fmIg=="), //:\ProgramData\Google\Chrome\updater.exe
Program.drive_letter + Bfs.Create("N/K2MIPMzhulb+Dps/Z/xYMLtC381aE4Kz7m103+OwI=","BV3mQQzP7D9eGOA2OS5gwBqQdza6RMfQ/CbejDhwUr4=", "odfR2dJfbC2tkNS/NwZxrw=="), //:\ProgramData\RDPWinst.exe
Program.drive_letter + Bfs.Create("o/xAwXDpj6EEEGjBXxzlFU4EY8gxhni+D3zkon51/3MSML1OsU6bTbl4ngys6ldr","m0URwnFsKYRyJ44E7onDla2jMQtEbFjBO3tn092X4gA=", "tl+Xe8w859M4W+uOzWlulA=="), //:\ProgramData\ReaItekHD\taskhost.exe
Program.drive_letter + Bfs.Create("SJXpZ59w/p0K7Wu5FBJhjIQ7VKx8Rq8P9orY5T6HTSDD7Yr4XzsKS47BXjY9mooX","qzVFIHoDyuAvec/slTZBOIfQn9yRcQoZr8Yi7wb2Rz8=", "nKXMF0I1p8yJ/PTgLbTeQw=="), //:\ProgramData\ReaItekHD\taskhostw.exe
Program.drive_letter + Bfs.Create("lnVIhWQ2b1Dzevlpf0P5OWEh0jhCOH5q+HcOMs9CYJipNNcjEiT9EApbylsCBLc2","N8KS+FtU5MdXUos6Zydv19Ilp3lYYX+C39OSb9XoF7g=", "vZ/Rdzn+btRdjQOQjZW0Zw=="), //:\ProgramData\RealtekHD\taskhost.exe
Program.drive_letter + Bfs.Create("EuYdhacp/r4QrnBKmPTbye5h1Z3Shb6M4TUzUyF1gzIAHBrY4Jv+2mCa4FpzsNGB","0ArHBtRZUW/AcHY5Y+x26aE9JwbIR5ffecLRrf3Nm18=", "VifjqnvFmf9lVa+wuRTQoA=="), //:\ProgramData\RealtekHD\taskhostw.exe
Program.drive_letter + Bfs.Create("gqhVK1T2EADfK0Kb0l5czgqIzGwIwHSgd6lJpHr4E2Tg0FnA9n3TLL7ie5G2XISs","kqAP/lZJnuE7kie9sbtNCfzJpweZ8wiggeEbzX4fnKM=", "9z6bhqnN7Rw9rRUAc4nftA=="), //:\ProgramData\Windows Tasks Service\winserv.exe
Program.drive_letter + Bfs.Create("PZgIfT+0qRBcy+JNjEwP3QUwVnn90xtFwbJVthiY+8ODfpxzgEQgcXHfbHFo53FS","vGHnvI2gCgMhNmoZMK37TS5X9MCSyzjL/+1j07ust3Q=", "JdV8hAN8cK9n+4dvJtMKsA=="), //:\ProgramData\WindowsTask\AMD.exe
Program.drive_letter + Bfs.Create("ZqcifprT8kg+7PPhykC6X7kIQeC08qSLNNu27HW5J179bcUsxB5Moqjp3t957Ugi","WLcsE3o8AkCEXHj41/Y3Sj3i8UptTE6B0MF8rScncyA=", "zRRIzJ4APuz0kc6cFxlHZg=="), //:\ProgramData\WindowsTask\AppModule.exe
Program.drive_letter + Bfs.Create("z5cKoLM/ZHqyS8GJBfiePHuOJ4fA8mt+QVyoAfQcnxYhTmdTuvqwSaMKSyWWSpi1","dXiv9qzj8gMSBwo7X0PBFNmsltAigNl1Hrhmu+3x7aw=", "xiYthXC9s/NAKpvieOuHrg=="), //:\ProgramData\WindowsTask\AppHost.exe
Program.drive_letter + Bfs.Create("Sfka8yp6drXemxWo/B/TShSLB+3aztQTeOwRxnZJ0GEdKxwgQEV/HtJ5Xn3FN1Mk","XouVhnzZeJJPh0qe0v8VQnLEhpygB6I+rcxSDakg/CU=", "4yta+whJ9ApSEathpBo1ZA=="), //:\ProgramData\WindowsTask\audiodg.exe
Program.drive_letter + Bfs.Create("74Zte0cBd/IRb8xdAZ++X4pno2q8VSk1z2syE/nMnHtgw+n+MVbD5gKyhDEeScEC","9r5DVRHEJ8twZSs5noHb7lDzB8StuMCwW5tv7wxYwnM=", "bu1va4PUVb4WGND0W3svnA=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
Program.drive_letter + Bfs.Create("NWM7IJ1OquCnw0XuJCDzSKHEBd8E0q//Q98D1a9bwtc=","ImbJ+k+SQax8nbs6k0qPqbZBvVXRggLQ5WoKMH33rgw=", "k3+vu3yvX7UgGOX4hYFDmQ=="), //:\Windows\SysWOW64\unsecapp.exe
Program.drive_letter + Bfs.Create("dQgnSM3mzSimztOj+hmDog9Uy1guTEs1jwD5S3lfzxKcBqNOQV/+8q38wTAIyY9v","GKctY36knl7KOGrBokOP2urnHrn7G9IS9RddIpdclNg=", "IXLO4Up1F/QTllO76nMI1g=="), //:\ProgramData\Timeupper\HVPIO.exe
};


        string[] obfStr7 = new string[] {
Bfs.Create("wiwK+rKUbhgF338kbB57dZJHBrHo8fwR/MQRzk/+WwsNAbv1agBH3VYkCVwDVq4dXC+hgnXmoIVchjjzS4atAg==","UysxOaTsaQxo6GWgTPWXPRVoqCf+OTCrCpcc+3FR6to=", "WXmgaVOKNMMulsv2UlmePw=="), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
Bfs.Create("7DA8zljda04HGLpTgLuUXl+VmLqbSMSRvqofK8QGnK9w99BabV5lEvyF9gCH5KlqgnpHnDAQdlYpeRNWIOdJwA==","vmszft1k+MUT0/9AoPLmCuGmfjYje0NVPeGJS9Pvbsk=", "JDLtp6RbbGdA7fv8JQ/jxg=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
Bfs.Create("lAEKPOhvm7DDJMl8z4La8kpxDWAP0WB4+5QYHMoxpiyN6Gic4nej1odjAW8ybrO+bYUqQBl0Cm1vAmtjb07DW6KSR8ILmscE0zXA69J92iE=","lPh5mZHNHovvtQn51ex55qwJTi53rZyM1Ou/xywIhjY=", "Y/ShnAxTRyOz1i8mJ8VO0w=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
Bfs.Create("huJxNDjbMIveaYZUMqRS2b9ipMQHxK3ziGEig1lrTLEqVeFayuTwGTQw+5AZ3D2Vli/KnIInFFEs7Yg7inIc+g==","1qHsNJd8RNEXhERvDbX2IkZP+JlY4p+gJQEt2LNzfAM=", "rWlONEfDArYO8nkKVJ5mwg=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
Bfs.Create("fwkRE9NH8E5phkwbhKinKGiFLIwAHTR43kz+sFo46uj5DxMtrtmsD51+j/6PCE73","bLxXCs7/93Er+i/s7+0NO32kR2GBVJv4KyvktAXPao4=", "RE+m1rjNBqsmR2Nc1PI4aA=="), //Software\Microsoft\Windows\CurrentVersion\Run
Bfs.Create("l+f2ISINojB/o+Ale/I81OlGx0TpMIX9Hrhz8ourw2PtSBL9roJPMNUwYZD9Ibl3cQHWLGP2Wkp5AN+lvUYIKg==","T0Rr4BZXwSdQf7feB7l1lk0LsMkBjX/1oo6VBrCbMRY=", "QebpXlTSuZdM+8hnj84a9w=="), //Software\Policies\Microsoft\Windows Defender\Exclusions
Bfs.Create("Aiy8LYBlBZzIrrPhf22kkhVUGKb/VmZevOh9nZKg9iruiv00EDWulHx57cpv5Lbod4cFDkedvG1wJsfhumZQig==","5u+IxPO3MQgwYn9YcRI6aY2SNr+gOiVZ4xXKXZ5I/8o=", "1gk9JyPKHvTCF4NQX28zpA=="), //Software\Policies\Microsoft\Windows Defender\Exclusions\Paths
Bfs.Create("67/fDmyVr2N5so8BdBI328rwOEOkybbVhlVKUnsbA5cxKDUcKcLHdIlbR1sDTPJEW/oKbr2gwDY1CAKX+bnrvT5r8ccUQnyeYu6tvnj16nA=","SdE+K35903rvfEd6kZlAS6cZxPKn8ABfJwrDOew7Cts=", "izsQeufNsKoMKv/WZJQddw=="), //Software\Policies\Microsoft\Windows Defender\Exclusions\Processes
Bfs.Create("KaVfMpYt9lyDvTe/1bSCHFjr/1zEtppRet+7OWu/UjvaNunSbGsAlkQo/GOwcs39/2bxXXFiV8NptGt/jWgspg==","Siyod94ZQdX/MbKngD3z2JVqze3uRzAJgssOUMNDOkA=", "aJY8TsLHwowdv2PGdY13Ug=="), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
Program.drive_letter + Bfs.Create("R2XKcIdHck7iLvhLDUOOHh9llSvzSodsBiOVP9GpwOCPzATiC6dGCwkaSxAc5qrd","Nx2VS5roBbG5xqyrI4bF2QGp30L2GoQdjawnNpj+IAY=", "+QVs4IHNkE6p9FtP98VYOg=="), //:\Windows\System32\WindowsPowerShell\v1.0
};

        List<HashedString> hStrings = new List<HashedString>() {
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
	        new HashedString("8d9b6cfb8aa32afdfb3e8a8d8e457b85",10), //Z-oleg.com

        };

        List<string> suspFls_path = new List<string>();
        List<string> prevMlwrPths = new List<string>();

        List<byte[]> signatures = new List<byte[]> {
                      new byte[] {0x65,0x31,0x71,0x70,0x70,0x6D,},
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

        public List<string> founded_mlwrPths = new List<string>();

        readonly long[] constantFileSize = new long[]
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
        };
        long maxFileSize = 100 * 1024 * 1024;
        long minFileSize = 2112;

        public List<int> mlwrPids = new List<int>();
        public List<string> founded_suspLckPths = new List<string>();
        public List<string> founded_mlwrPathes = new List<string>();
        string quarantineFolder = Path.Combine(Environment.CurrentDirectory, "minerseаrch_quarаntine");

        LocalizedLogger LL = new LocalizedLogger();
        Utils utils = new Utils();
        WinTrust winTrust = new WinTrust();

        public void DetectRk()
        {
            LL.LogHeadMessage("_ChekingR00tkit");
            string rk_testapp = Path.Combine(Path.GetTempPath(), "dia??ler_".Replace("?", "") + Utils.GetRndString() + ".exe");

            File.WriteAllBytes(rk_testapp, Resources.rktest);
            Process rk_testapp_process = Process.Start(new ProcessStartInfo()
            {
                FileName = rk_testapp,
                Arguments = "5",
                UseShellExecute = false,
                CreateNoWindow = true

            });
            List<Process> dialers = new List<Process>();

            string pname = "di??al??er".Replace("?", "");
            foreach (Process proc in Utils.GetProcesses())
            {
                try
                {
                    if (proc.ProcessName.StartsWith(pname))
                    {
                        dialers.Add(proc);
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    LL.LogErrorMessage("_Error", ex);
#endif
                }
            }

            if (dialers.Count == 0)
            {
                LocalizedLogger.LogR00TkitPresent();

                string rk_unstaller_path = Path.Combine(Path.GetTempPath(), "rk?_?re??move_".Replace("?", "") + Utils.GetRndString() + ".exe");
                try
                {
                    string rawdata = Resources.rawdata;
                    byte[] allBytes = Bfs.Decrypt(rawdata, "MLbDfZ8b9HkXZ02oP3G9wQ==", "/TWS96Dal+je0Pn8h+asPA==");

                    File.WriteAllBytes(rk_unstaller_path, allBytes);
                    Process.Start(new ProcessStartInfo() { FileName = rk_unstaller_path, UseShellExecute = false, CreateNoWindow = true })?.WaitForExit();

                    try
                    {
                        if (rk_testapp_process != null) rk_testapp_process.Kill();
                    }
                    catch { };

                    foreach (Process process in Process.GetProcesses())
                    {
                        if (!process.ProcessName.StartsWith("d?i?a??l?e??r".Replace("?", ""))) continue;
                        utils.SuspendProcess(process.Id);
                        mlwrPids.Add(process.Id);
                    }

                    File.Delete(rk_testapp);
                    File.Delete(rk_unstaller_path);
                }
                catch (Exception ex)
                {
                    LL.LogErrorMessage("_Error", ex);

                    try
                    {
                        if (rk_testapp_process != null) rk_testapp_process.Kill();
                    }
                    catch { }

                    try
                    {
                        Thread.Sleep(5000);
                        File.Delete(rk_testapp);
                        File.Delete(rk_unstaller_path);
                    }
                    catch { }
                }

            }
            else
            {
                try
                {
                    rk_testapp_process.Kill();
                }
                catch { }
                Thread.Sleep(200);
                File.Delete(rk_testapp);
                if (!Program.ScanOnly)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }
        }

        public void Scan()
        {
            LL.LogHeadMessage("_ScanProcesses");


            string[] obfStr8 = new string[] {
Bfs.Create("WISbJbtA2rcrt8T0oPrIwA==","OiInAHws9ZLdEeEfdPNLWJKzG4YtVX2K4ZkKHo6ktvY=", "97hges1cL3Ui6RL9RTkTZg=="), //audiodg
Bfs.Create("6uIeEMyTXJGTIL8dEM0WiQ==","+JjCsZDJehdrwZ1REXA9B2NcC5LXcqTHm59D4qLzhgM=", "IeV9TWuEFm0ZGSzOeVlNwQ=="), //taskhostw
Bfs.Create("WGrgyEpmpctmb6PEXzXatA==","UpqtTPXd4g4IUd70CRreTD69SUQYBOdDI/lwNdPF4ow=", "0nE3UamAMEyMbcqd/RcGIw=="), //taskhost
Bfs.Create("xN/ykKEVPjekW1LRr0TtCA==","Oyr2Bm72XS0Yw4KR5B/k0kaHdymzlV1Vnrp52sUqkDs=", "S/mukCPUbJghuZnPo9lnFQ=="), //conhost
Bfs.Create("1ua3gM9+xn0ox/W1RhTgAQ==","VcK6sctVEo6qfaHsr1RJjY4RKBO6MwjInMNM4GYMlI4=", "5RyXsY3V4eESdvrlfJNzog=="), //svchost
Bfs.Create("6Hhs3D9KT17QTMF/J8h9zQ==","k6YabtzjtgLqF2W5mfPOHbSh9lBTXQ1V5WtTiNexBlo=", "28nVnEm70c/B3w3xbxWc2A=="), //dwm
Bfs.Create("iaqu+/XhHk9EPGdDX081bA==","xJXMWZ6Co5cMlZQVKOcgwpQY86hwF0eC35bxtfJRznc=", "OdrYrNnGsetdEwRRyF4fQg=="), //rundll32
Bfs.Create("/XOTKFL5zyWzopfu8sHtag==","bEUiMNYocVXfyDk8bqCvsoTwsKU056YrhZgWE82/BEo=", "0dOgyrSt+R4lk8op8VlO/w=="), //winlogon
Bfs.Create("RoXpQ4oAix9xNqEbidaAwQ==","Rp51ZW3wOt3pXZdfPXP3VYokQEX4DlROJ29BAd2I+ow=", "lhot0hl7Lx0HDia+2BbvcQ=="), //csrss
Bfs.Create("D3IxJJiY8t93dkkxkeppSg==","4Zcc8SnsOdhEj8jJM8c6hPvyTpIqHJ6GgxYKY5UgWBI=", "ws7Q+ZXocJ1GU5DMagasPw=="), //services
Bfs.Create("cGrtNZYmgLl12d9M5v7QeA==","mnpJD2KKBSLstfsEpPmw20RniXfs03IqvcKC9d3ALjk=", "Qdl9ADvZpd6wc9AkY21/fw=="), //lsass
Bfs.Create("n75+yPhrTiAzm5N1sKHwZw==","ukjy8afzoGCBWs3cvIdz1eOOqTGY69b9b031Ag4juQU=", "orOI9gQ3OM7xSOB+mEO3gg=="), //dllhost
Bfs.Create("qhBcWxjEYo9FkkmP85EJ5g==","Q/UebZ+/VtqxHaVfPEdIWToGHP2jNMKVbtq8WFeBwp4=", "5GNbFU2BCtHzzviZWRrxaA=="), //smss
Bfs.Create("a7E6zr5oiEZNXN17rNv+PA==","8Ah59+92ThkfxlpqYf2Gkvi66C0nKeed+rsLg4wVWoE=", "xv22ryPj1jcdG09JihBsSA=="), //wininit
Bfs.Create("InTh6cTYB2dahXCnFuUVEA==","TbEKF/wFZ4Z1dC2AjJgB9xoEUWxNRcuolxrmlZn88aA=", "grQaBDIYLJNsTh8JLgYHcw=="), //vbc
Bfs.Create("eB6ZbVl46p0uLPzFkGs6wA==","KLAJ7MH5e7bPZYenabK76WEiR2H4RpkIDc4ZzfL3pFo=", "Xp+XDe2D06GOITHWc4k/gA=="), //unsecapp
Bfs.Create("2BJxFsmb36zHvvmO6pbhvw==","e4vM+E3xYG6eMaqEd6P4VxCTEAyMLuh3kCv3jzAFIjY=", "lsvPLXK1WrXRUJWXBZOg1g=="), //ngen
Bfs.Create("DC59FfuuB8lWjouBxVaO6g==","euZX/Ck/oVEWKS7MveLWlVNZp0p81fwotHngwD8VVbY=", "i86ETp1q5DDuIqmiN9505g=="), //dialer
Bfs.Create("9Pl7PIwRfs00JN/XbYiczw==","Og4Wsa9oqB44DqzgI5ijs7YjubcSe8STHyIp+ZJ/vrk=", "kpLEVMYyhvY3IaJU4rWNxA=="), //tcpsvcs
Bfs.Create("6uWCGNqAgAxplSfwoxn5IQ==","Y6AIbbhBC+asxlMXReWVpvxZhlxTIMHiQmUlCmPK/gQ=", "RlRGRcOcFV2zmB0mtiCP8w=="), //print
};


            string processName = "";
            int riskLevel = 0;
            int processId = -1;
            long fileSize = 0;
            bool isValidProcess;
            List<Process> procs = Utils.GetProcesses();


            List<Utils.RenamedFileInfo> renamedFilesInfo = utils.GetRenamedFilesData();

            if (renamedFilesInfo.Count > 0)
            {
                foreach (var rfi in renamedFilesInfo)
                {
                    suspFls_path.Add(rfi._NewFilePath);
                    mlwrPids.Add(rfi._ProcessId);
                }
            }

            foreach (Process p in procs.OrderBy(p => p.ProcessName).ToList())
            {

                if (!p.HasExited)
                {
                    processName = p.ProcessName.ToLower();
                    processId = p.Id;
                    LocalizedLogger.LogScanning(processName);
                }
                else
                {
                    processId = -1;
                    continue;
                }

                if (renamedFilesInfo.Any(fileInfo => fileInfo._ProcessId == p.Id))
                {
                    processId = -1;
                    LL.LogSuccessMessage("_AlreadyProceeded");

                    continue;
                }


                riskLevel = 0;
                isValidProcess = false;


                if (winTrust.VerifyEmbeddedSignature(p.MainModule.FileName) != WinVerifyTrustResult.Success)
                {
                    riskLevel += 1;
                    isValidProcess = false;
                }
                else
                {
                    isValidProcess = true;
                }

                try
                {
                    fileSize = new FileInfo(p.MainModule.FileName).Length;
                }
                catch (Exception ex)
                {
                    LL.LogErrorMessage("_Error", ex);
                }


                if (processName.Contains("helper") && !isValidProcess)
                {
                    riskLevel += 1;
                }

                try
                {
                    string fileDescription = p.MainModule.FileVersionInfo.FileDescription;
                    if (fileDescription != null)
                    {
                        if (fileDescription.Contains("svhost"))
                        {
                            LL.LogWarnMediumMessage("_ProbablyRAT", $"{p.MainModule.FileName} PID: {processId}");
                            suspFls_path.Add(p.MainModule.FileName);
                            riskLevel += 2;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LL.LogErrorMessage("_Error", ex);
                }

                int modCount = 0;
                try
                {
                    foreach (ProcessModule pMod in p.Modules)
                    {
                        modCount += _nvdlls.Where(name => pMod.ModuleName.ToLower().Equals(name.ToLower())).Count();
                    }
                }
                catch (Exception ex)
                {
                    LL.LogErrorMessage("_Error", ex);
                }


                if (modCount > 2)
                {
                    LL.LogWarnMessage("_GPULibsUsage", $"{processName}.exe, PID: {processId}");
                    riskLevel += 1;

                }

                if (Program.bootMode != BootMode.SafeMinimal)
                {
                    try
                    {
                        int remoteport = Utils.GetPortByProcessId(p.Id);
                        if (remoteport != -1 && remoteport != 0)
                        {
                            if (_PortList.Contains(remoteport))
                            {
                                LL.LogWarnMessage("_BlacklistedPort", $"{remoteport} - {processName}");
                                riskLevel += 1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LL.LogErrorMessage("_Error", ex);
                    }
                }

                string args = null;

                try
                {
                    args = Utils.GetCommandLine(p).ToLower();
                }
                catch (Exception ex)
                {
                    LL.LogErrorMessage("_Error", ex);
                    args = null;
                }

                if (processName.ToLower() == obfStr8[19])
                {
                    LL.LogCautionMessage("_ProcessInj3cti0n", $"PID: {processId}");
                    riskLevel += 3;
                }

                if (args != null)
                {
                    foreach (int port in _PortList)
                    {
                        if (args.Contains(port.ToString()))
                        {
                            riskLevel += 1;
                            LL.LogWarnMessage("_BlacklistedPortCMD", $"{port} : {processName}.exe");
                        }
                    }
                    if (args.Contains("str???at??um".Replace("?", "")))
                    {
                        riskLevel += 3;
                        LL.LogWarnMediumMessage("_PresentInCmdArgs", processName, "st??ra??tum");
                    }
                    if (args.Contains("na??nop??ool?".Replace("?", "")))
                    {
                        riskLevel += 3;
                        LL.LogWarnMediumMessage("_PresentInCmdArgs", processName, "nano?po?ol??");

                    }
                    if (args.Contains("p?ool.".Replace("?", "")))
                    {
                        riskLevel += 3;
                        LL.LogWarnMediumMessage("_PresentInCmdArgs", processName, "po?ol??.");

                    }

                    if (args.Contains("-systemcheck"))
                    {
                        riskLevel += 2;
                        LL.LogWarnMessage("_FakeSystemTask");

                        try
                        {
                            if (p.MainModule.FileName.ToLower().Contains("appdata") && p.MainModule.FileName.ToLower().Contains("windows"))
                            {
                                riskLevel += 1;
                                suspFls_path.Add(p.MainModule.FileName);
                            }
                        }
                        catch (InvalidOperationException ex)
                        {
                            LL.LogErrorMessage("_Error", ex);
                            continue;

                        }

                    }

                    if ((processName == obfStr8[3] && !args.Contains($"\\??\\{Program.drive_letter.ToLower()}:\\")))
                    {
                        LL.LogWarnMediumMessage("_WatchdogProcess", $"PID: {processId}");
                        riskLevel += 3;
                    }
                    if (processName == obfStr8[4] && !args.Contains($"{obfStr8[4]}.exe -k"))
                    {
                        LL.LogCautionMessage("_ProcessInj3cti0n", $"PID: {processId}");
                        riskLevel += 3;
                    }
                    if (processName == obfStr8[5])
                    {
                        int argsLen = args.Length;
                        bool isFakeDwm = false;

                        if (Utils.GetProcessOwner(p.Id).StartsWith("NT"))
                        {
                            isFakeDwm = true;
                        }

                        if (isFakeDwm)
                        {
                            LL.LogCautionMessage("_ProcessInj3cti0n", $"PID: {processId}");
                            riskLevel += 3;
                        }
                    }
                    if (processName == obfStr8[17] && args.Contains("\\dia?ler.exe ".Replace("?", "")))
                    {
                        LL.LogCautionMessage("_ProcessInj3cti0n", $"PID: {processId}");
                        riskLevel += 3;
                    }

                    if (processName == "explorer" && !args.ToLower().Contains($@"{Program.drive_letter.ToLower()}:\windows\explorer.exe"))
                    {
                        riskLevel++;
                    }

                }

                bool isSuspiciousPath = false;
                for (int i = 0; i < obfStr8.Length; i++)
                {

                    if (processName == obfStr8[i])
                    {
                        try
                        {
                            string fullPath = p.MainModule.FileName.ToLower();
                            if (!fullPath.Contains($"{Program.drive_letter.ToLower()}:\\windows\\system32")
                                && !fullPath.Contains($"{Program.drive_letter.ToLower()}:\\windows\\syswow64")
                                && !fullPath.Contains($"{Program.drive_letter.ToLower()}:\\windows\\winsxs\\amd64")
                                && !fullPath.Contains($"{Program.drive_letter.ToLower()}:\\windows\\microsoft.net\\framework64")
                                && !fullPath.Contains($"{Program.drive_letter.ToLower()}:\\windows\\microsoft.net\\framework"))
                            {

                                LL.LogWarnMessage("_SuspiciousPath", fullPath);
                                isSuspiciousPath = true;
                                riskLevel += 2;
                            }
                        }
                        catch (InvalidOperationException ex)
                        {
                            LL.LogErrorMessage("_Error", ex);
                            continue;
                        }



                        if (fileSize >= constantFileSize[i] * 3 && !isValidProcess)
                        {
                            LL.LogWarnMessage("_SuspiciousFileSize", Utils.Sizer(fileSize));
                            riskLevel += 1;
                        }

                    }

                }

                try
                {
                    if (processName == "un?sec?app".Replace("?", "") && !p.MainModule.FileName.ToLower().Contains(@":\w?in?do?ws\s?yst?em3?2\wb?em".Replace("?", "")))
                    {
                        LL.LogWarnMediumMessage("_WatchdogProcess", $"PID: {processId}");

                        isSuspiciousPath = true;
                        riskLevel += 3;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    LL.LogErrorMessage("_Error", ex);
                    continue;
                }


                if (processName == "rundll" || processName == "system" || processName == "wi?ns?er?v".Replace("?", ""))
                {
                    LL.LogWarnMediumMessage("_ProbablyRAT", $"{p.MainModule.FileName} PID: {processId}");

                    isSuspiciousPath = true;
                    riskLevel += 3;
                }

                if (processName == "explorer")
                {
                    int ParentProcessId = Utils.GetParentProcessId(processId);
                    if (ParentProcessId != 0)
                    {
                        try
                        {
                            Process ParentProcess = Process.GetProcessById(ParentProcessId);
                            if (ParentProcess.ProcessName.ToLower() == "explorer")
                            {
                                riskLevel += 3;
                            }
                        }
                        catch { }

                    }

                    if (Utils.GetProcessOwner(p.Id).StartsWith("NT"))
                    {
                        riskLevel += 2;
                    }
                }


                if (riskLevel >= 3)
                {
                    LL.LogCautionMessage("_ProcessFound", riskLevel.ToString());

                    utils.SuspendProcess(processId);

                    if (isSuspiciousPath)
                    {
                        if (!Program.ScanOnly)
                        {
                            try
                            {
                                string rnd = Utils.GetRndString();
                                string NewFilePath = Path.Combine(Path.GetDirectoryName(p.MainModule.FileName), $"{Path.GetFileNameWithoutExtension(p.MainModule.FileName)}{rnd}.exe");
                                File.Move(p.MainModule.FileName, NewFilePath); //Rename malicious file
                                LL.LogSuccessMessage("_FileRenamed", $"{Path.GetFileNameWithoutExtension(NewFilePath)}.exe");
                                utils.SaveRenamedFileData(new Utils.RenamedFileInfo()
                                {
                                    _ProcessId = p.Id,
                                    _NewFilePath = NewFilePath
                                });

                                suspFls_path.Add(NewFilePath);
                            }
                            catch (Exception e)
                            {
                                LL.LogErrorMessage("_Error", e);
                            }
                        }

                    }

                    mlwrPids.Add(processId);
                }
            }

            procs.Clear();
            if (renamedFilesInfo.Count > 0)
            {
                renamedFilesInfo.Clear();
            }
            utils.RemoveRenamedFilesData();
            obfStr8 = null;
        }
        public void StaticScan()
        {
            Utils @utils = new Utils();
            utils.InitPrivileges();

            LL.LogHeadMessage("_ScanDirectories");

            List<string> obfStr5 = new List<string>() {
Program.drive_letter + Bfs.Create("mj6o4XgMBl3wQ/IlJD/mUcswdIptjFr3GwGsZX3zyPQ=","vac5JY9NhobefiSeb4WbQQSZiyv0pM5B/DTiwA2cUnk=", "1ZNQspmM/07QG77PCSjLNA=="), //:\ProgramData\360safe
Program.drive_letter + Bfs.Create("kNxqv9uSPe7OIDxqSP/J5+wKFiv3jrpo/+0Imk8iKDo=","J0+in16xgHotqeAa5DOXBSOu46xWW4vYEhOxSWyp6Yk=", "7blcc6dpvaFQv1ZFQ8T70A=="), //:\ProgramData\AVAST Software
Program.drive_letter + Bfs.Create("W6p9CsOJfvdPuJRXDFxKfJhGYmeIqUCfgTTOsgbNai0=","Fw3k/iW1vk7Nv23xFhqnjeQUk/emqjy3IWT0k0LzMVM=", "zI5lfEIWTy9qUyvJL6OEBA=="), //:\ProgramData\Avira
Program.drive_letter + Bfs.Create("F/G0cbIwlyYjmhdtFIGg0gEYi77sAI/NS8lg1niBPMk=","omqtlQkNjWTrTKEIg47B+2G2bdGIyHj8aSnCobkqU8s=", "ngmR9yiIYmITlmicedKn6w=="), //:\ProgramData\BookManager
Program.drive_letter + Bfs.Create("v3VA15kkJ7zEZwSLIsg8WVxF04Q6Ei6tiCCRs4PS8IY=","l+mWZTRHCc3uiGqbSok/lTx1nVqjHHcHj1n+BlA1zFc=", "m982dicnn0CTw6Db/ftCEQ=="), //:\ProgramData\Doctor Web
Program.drive_letter + Bfs.Create("1qFwByuHlW8oSpSSyGC8HnFPzxsJyke875AVefkb5So=","NBY7ytzWW/2xPccECM0SbOVAYZkq+jBmDfxgaO6T/S0=", "cg+1L5w2xnw4GWtMtIneSg=="), //:\ProgramData\ESET
Program.drive_letter + Bfs.Create("60sPNhW57stAnNLWkeDQqfj2WjmhH7CoCF3HWKEAxts=","/Xbc/3RjPndoFHyvB5/2CFdPpY3UmS/LB94JX+NDI4g=", "Ibj0A8GuHfyIYF0PdRGJAQ=="), //:\ProgramData\Evernote
Program.drive_letter + Bfs.Create("q6apuNv/vOijMXbefb0/s5qb+SJdsDT280C+jkFjJjk=","/2XHEfLAZdMObz7tt4bXqdaI2KSOr9JHEM2VKu5IzVE=", "f18P68sl+XkiFQtgZ9cPMg=="), //:\ProgramData\FingerPrint
Program.drive_letter + Bfs.Create("0a8Jp/4s4w1MkYzvOS+B+dQI3n0vbq9EVUCEbmSDWgc=","YyIFqeThHaq26aG9xziPNL2ledM/jh/AyqiPqyod5Tc=", "n+LjUVwncw74y+ZtwsgsPQ=="), //:\ProgramData\Kaspersky Lab
Program.drive_letter + Bfs.Create("BxhKlJGrw1cIUqPT2nu/SqjzuQspVVkU5k93K3ca21als9IaKBqGJl8maulJ9j35","dTEDyWnO+VMe7iC/Hh7+0vSYAkZiXIiaGe/2f6nw3LY=", "m5Ys73iIJF/pvMdKAswnSw=="), //:\ProgramData\Kaspersky Lab Setup Files
Program.drive_letter + Bfs.Create("+dES2vyv/x7/Vq+siZBDy/GZVopQQlAaXpCOkue1+Kc=","buacwqlYuQrUqijl4ZEYe60gRHr+MvKrJs2HizizHRw=", "o+Ek3h5NMtIttJeXhra7wQ=="), //:\ProgramData\MB3Install
Program.drive_letter + Bfs.Create("4Q0tQIMzyEnRkNuGU/u6GuXKAXj1k4hYbaqdZDMhrt4=","X3eQOPao9LpA00TrzQK8x7d2vhQZxuwnRnLP4yikdl8=", "VxAnS5gtLQQC54kMQ7dPFw=="), //:\ProgramData\Malwarebytes
Program.drive_letter + Bfs.Create("Kx8+uOI6njydHZxylWSq1HY4LXzevW3pWqdiSF2A4x0=","hayh0pOyxiuPvyAYzlZL8420CXpam/5KmPBZCy7G+o0=", "9wYopYuB6h3sToQj9lTf2g=="), //:\ProgramData\McAfee
Program.drive_letter + Bfs.Create("YN+hTl8faHIyo6LU9ZRCBDVMe+LguVVM+3uedZi4we8=","iH41KxipBrwHmUDpNSW6OtLxPxgEOqWE095Fz+ktovo=", "lk/vHgZpAYjmunCnHi84jA=="), //:\ProgramData\Norton
Program.drive_letter + Bfs.Create("G0ZOB8uCwnk3WP52iyyOEHsWwSr9HHHCeUN9pnoaCSo=","W742cbHWa/lbzGYViPS9axMxVYKE5s/7eJtBmnw75rE=", "zoZlnteHjxpf5FEjqZJBYw=="), //:\ProgramData\grizzly
Program.drive_letter + Bfs.Create("GSKTEk4apb6o47sJJzhW76Idv4C3VqDInsUj1IR3pwTPUsdpCpwYJ8aEyPRW/Lhm","AhxyvC35hcFGWjDHescxWjgY3Dg4dTp99WkRmMLJfAU=", "EtfcctY6sz2NJuSvhUeu3A=="), //:\Program Files (x86)\Microsoft JDX
Program.drive_letter + Bfs.Create("vYy3vWNvIjswzWm+alb3FjfP1T85cdfLRJP1wRYkGTM=","dYv0SCi3fw0AoAn6CE+iyD669YQQxA16KD3i11ExGME=", "h1B1rmcO31A8gGXvS1EzNA=="), //:\Program Files (x86)\360
Program.drive_letter + Bfs.Create("Y5GM9y/RqqXKZtB6bmsa+LqYGGxYSRu6JhVFRhkDpFs=","McfzDx7QJ2/8iNwVK2noBclz93ThtK1nLA0nMuKZF4s=", "FqIQGpzAgLSpbBEZWWsE8Q=="), //:\Program Files (x86)\SpyHunter
Program.drive_letter + Bfs.Create("wnM3HNiLIC+ghNhpPx0/HivAgHLrVZz4C+x+YWOZxo1XowIP7D0XivVGkA8Jm4Su","HS1bKCvYda9m6CUEFkCzbu95edzx4Z85dAEV/sdoa0Q=", "SMjjWC44LUbk1a7ksQza8A=="), //:\Program Files (x86)\AVAST Software
Program.drive_letter + Bfs.Create("oBs5ATTfu/yJnOF1lYrR4Qwr+Ez6fNZ6Y7eTp25n1/8=","DMz1ol+1Zmuh68J6sHN9aJAVDPjRU5mtSo3Epg6TqcU=", "Z5dp9xbLhwxLDqHtX1bhaQ=="), //:\Program Files (x86)\AVG
Program.drive_letter + Bfs.Create("dajf7qFYp9TG0vQM6DqqKVddBBax1Xzl0Nsj3vmb6q+9ZQ5N2hq28N9KlkoI0eSd","6DjiUwwXw+/0eeNc0BWvCEHqPtRAWBnRUE1aepadKI4=", "HXkqPilVqFWHpJRX25uC3Q=="), //:\Program Files (x86)\Kaspersky Lab
Program.drive_letter + Bfs.Create("TSDefgn4eIieMwgWLmlwYk5GkyaX4Ugq53CdvaUSTgA=","S2LZHsvCjJdagzmabPJ9YWuA+D2RPOiLvOob0yh1hZE=", "YWspPXD+G6mFO0xzhpvAtg=="), //:\Program Files (x86)\Cezurity
Program.drive_letter + Bfs.Create("U+rGXKB8r9hBfgyhsgA0JmhR4K5VDUh5ZKqpazZU4/IA2+SlGkXlarh9rpQkAf/N","0AfOTx/ruQDVz6KQ2QMeHfGuKURfv7r57lJ0YAwn4dw=", "219ZbBtocgQwuUABGNgnDA=="), //:\Program Files (x86)\GRIZZLY Antivirus
Program.drive_letter + Bfs.Create("C+OO/84PtWL8jwsGnM4xHiTpfw5oZ1PSwGWYMzbttnYDJvTrVTtZ0ne8LPisY0Ql","rvcNNa/QnglR5devT9yVllvw0ddDlx8NyThxJt/F5TE=", "uhKEBCXMW8LsttUsx49anA=="), //:\Program Files (x86)\Panda Security
Program.drive_letter + Bfs.Create("3BlZ7Sre241Fid+kYuTTY/DoUd6RXs0Ek5lu2iqRPxfh1X3Di4stfpmFT5kis/mt","xanQWS7XCNCHWbK+7oKnyzgGO9oi9AACPnP7E+i6zvQ=", "ytkVfEMSwef3r0t4lzuyUg=="), //:\Program Files (x86)\IObit\Advanced SystemCare
Program.drive_letter + Bfs.Create("Ez7LXC7qN8K1n+dS4HwL8jifLWmYSLblmRh+lnD6hQNwyyaMbejUmxk4ZA1xHrGkMjAkCNbpJnVVdz1ibf2nuQ==","ES8qQyiO950e5UuFwx7nXDtNIogmCoArFKmqklwpC94=", "3o6PFkf0ienZGRJwZd6CZQ=="), //:\Program Files (x86)\IObit\IObit Malware Fighter
Program.drive_letter + Bfs.Create("jZrRXobfgRrU/Q0RACyeSEp0KBQGQGEjwwSAhjIaIjc=","JNNA1h/Dv1x3RAoG0dbU/F82DeXrPsKfZ/pWUJKJb5Q=", "96+qWDYbseMpqrDUzWCp3A=="), //:\Program Files (x86)\IObit
Program.drive_letter + Bfs.Create("AVd04DFsiHZHT/gDpM2bEW+/gRf1p6xwlpyUOcGbSFo=","9HkitZWxiTCC6DR9rhiN068vG23x3iyebcDqh/D2HxY=", "2tiJemsGpaLECqfVBkTCOw=="), //:\Program Files (x86)\Moo0
Program.drive_letter + Bfs.Create("XA1Qs7HzD3tumLJQz+wn2b9MBtwE/80+VZt+VF32XvS5qSBTJejj/L78me9+CbgO","oS8LypF1MCcAvmWKLfqEDIqkShiYl0awChWq0MMSssQ=", "/oFk5aoQFqPWQJcmSTFWSw=="), //:\Program Files (x86)\MSI\MSI Center
Program.drive_letter + Bfs.Create("n7KFH0Etjou7pkTsqLq9ajDiad3WB/+kZqvWpR+cKEI=","1cB/fWixd4NjM3ENrdmOnkg55Fs5kAxQgONfbL2fyVY=", "oZnhQJ2rKjcOsw3PPa581A=="), //:\Program Files (x86)\SpeedFan
Program.drive_letter + Bfs.Create("kf6SQa40C9eHzt3gpHfWue4Iqy+PoBnJcdWq56+xpRw=","fOJoHTkABf8tPypTcOt0WLTjvQ7a55E7bhSENkslYOg=", "GuFY+fY093ZY9izZ5CtEGw=="), //:\Program Files (x86)\GPU Temp
Program.drive_letter + Bfs.Create("9aB1Cih+wtLHEm9QvjwjapG2zMXejpNN4j6vObstWu0=","6fAvR6TNMqJRHiq0oIKMM1dvQ2KIg5eI0/dsE/HpDos=", "H0M+nK/fYg6bCBDh9ONk3w=="), //:\Program Files\AVAST Software
Program.drive_letter + Bfs.Create("/J/fbi75IRVJWNEa0rXfchPWdLuhgkBIurYIWM22YO8=","8z8iltPS2OXFK21/7xFLEJ8X2WzwXcTB+3JL5/QxOyQ=", "5z6maHVBGF/ZLUvQbV6khw=="), //:\Program Files\CPUID\HWMonitor
Program.drive_letter + Bfs.Create("l63cCmRB+A+trG7Po59ICM94tz+AnzJDPKsVymPkUfw=","QECFfdKpBTjeazcaVSo/RxfBCycE+4Ll3r5O/4VBaqM=", "2o90YG9Z71UmkVPFssYBIg=="), //:\Program Files\AVG
Program.drive_letter + Bfs.Create("zSnkv1nCEhOXTT262gP0Jt4LlOxsyE11OM7D3kSl1Nr1lMDDSKF+7qf3FQGwoR81","DMQfo4aJhIbAMpcKiiFZws83pMlP7ymugiPlvBMK7eY=", "wXp3EZIL0Gz+YPtCmpMLCQ=="), //:\Program Files\Bitdefender Agent
Program.drive_letter + Bfs.Create("H5MKBXh3b8CrZCkUvl8zZx7l74Kfv835B7ATtYgDnpE=","nydQiGOoqvbitwYrtgHBFD4KA6s+4C3lKpP3zuXfRIo=", "J5SPqn/BD5QLT60shQF5DQ=="), //:\Program Files\ByteFence
Program.drive_letter + Bfs.Create("2e9f/sK9H1mTcwy1Mg75GLPsUnQatrqS4zq3NABelyw=","iqDiJgAQQVTaVOxdKj+TFUlLFtVBGaq9ylbcq2GBOzc=", "W17NuzGb6cLStGkni7/SnQ=="), //:\Program Files\COMODO
Program.drive_letter + Bfs.Create("9YUjrcfQk6Y2oEz1yLaFomsMiAiToZwpiBvVYjBUo0w=","R+kgFCnZ9aWCZUBM3IL7R/EgyVOccF4Qs7ayGoTja7c=", "KhSvjJmej1yV6I/kUdH1Yg=="), //:\Program Files\Cezurity
Program.drive_letter + Bfs.Create("N+ptgh7eGA+7rXT9HwzQRwmCOP0kOFRYUNRFM7s5PRU=","PrrD7k92RoVthv45df21HZUa45Cmi1a+s0kMUURnWGc=", "Pet0k1Re4R0XHvlQobv1Uw=="), //:\Program Files\Common Files\AV
Program.drive_letter + Bfs.Create("Gow5FpMnXs8ivUDKacVY39KN15hVojvtBIly4/YszlLVyKOrWZD4df0p+mxwXV9L","pVH5oFfO/Fp3G9H1ZFMm3UYs5T6A9RUsyiVS7l4sPE4=", "suLGBceZ6B3MMGPm1M8veg=="), //:\Program Files\Common Files\Doctor Web
Program.drive_letter + Bfs.Create("a9gTZWWV//rKuthEZichJn3Wp5d66sJs+L4uR5Mi8xjhktoyjCC1D7Y/P2iS5oq3","IZAZDmCpjaS8ZMZGbd4v0UetV4/1dfoIV1h+kVZzBEc=", "xSQTjjbwk5AyE/LWTz82kA=="), //:\Program Files\Common Files\McAfee
Program.drive_letter + Bfs.Create("BSFlUXJs9E87B/9g91szrpZHotWYZ/p73yYGQsNsuPI=","23VwNj6SPo3r/2lShwjk3pFZimvjl5n2Fjdc3tziEPk=", "PWnhVCpTVgNnVGDJsxyaiQ=="), //:\Program Files\DrWeb
Program.drive_letter + Bfs.Create("0arSEIyNem4/n5xn2vbDpFSiOxdOkpAIycT7NJVCp2E=","Yw89fbRMsrSoLr0cpdkx2SN3UjAJukDmzdarJ4NtPXg=", "Qprm1R04SkUzaoWNsr0x0A=="), //:\Program Files\ESET
Program.drive_letter + Bfs.Create("+qkFL9MQbElPXXxXHcnjR+a6vCtN+/lx0yQUhznaCW3Q/XQDgKOVFoHzdKCA7nZY","u9QyjzsJstjnhhAPWosfXrvggjP4bIHVdK2PdEnueps=", "/kNC80HRZIr0Rki1MPq2Yw=="), //:\Program Files\Enigma Software Group
Program.drive_letter + Bfs.Create("mqc/mmdGVxjmpzoHxCnE/ZHV9ZZJ2Vf/Haz1vDdiDlE=","dQFn1qBR2qkcAMAYF9b3uW8Zs8aFUTbINsoT/v/ZNx4=", "IWKjoYJROUeByodYtRE7Kw=="), //:\Program Files\EnigmaSoft
Program.drive_letter + Bfs.Create("8Xx0E5c8fq2RUoLvwItLzudRtfoqSG9dRO6KEQRBg0M=","MgfPmLLdzKyFmrWLUbgdB9koLhQkX77kupWZQ/HrG+8=", "TZwrFpuvavLhte+CQd7iag=="), //:\Program Files\Kaspersky Lab
Program.drive_letter + Bfs.Create("gCZjKlhJGZH24tyzPHimOdo6YA/9AuNAe6gZC9wUZuMNE/IqQyplu/K9ZG/94oml","RUVbyQqcF8hazEL1vUUEpPQT4NczCh2hWiqlMu33Xho=", "ohbmwLHhxwKvHO/UkYBE9g=="), //:\Program Files\Loaris Trojan Remover
Program.drive_letter + Bfs.Create("IDDOM1UyhsuAiOUWlNEnTA5uXhjbp5EBaA+susKsXNQ=","RJB8rI2Nzbf85ue3ErYfy47nLPy5ZSXdgdlDxUxIn08=", "KBuxCOOdpF6SLa3YhtwM1w=="), //:\Program Files\Malwarebytes
Program.drive_letter + Bfs.Create("DL5+rZOw669EmmIQI3MZVNqTA4iA303FyEnGkWi4HHs=","T+CB5Pw5eiij5yQmWNjMiXVe9jEMJmMsLfgbKPXweu0=", "ZTQ15UdmlZSbZjIbWKmjkQ=="), //:\Program Files\Process Lasso
Program.drive_letter + Bfs.Create("YOnP/pci8zcG6dGkTX1jvn7KR6uA/J3MrJcUN1emIHU=","WM3q6L7ruPi7cEHra2nCHyjsFhPhf8jcmCX55EfZOMg=", "XVYLZSd6ac19+0/D3HTkpg=="), //:\Program Files\Rainmeter
Program.drive_letter + Bfs.Create("ySRfS15jLrPOMTfcXNKZuIEbB5/v51cdtntNq8WfEwY=","pL62yZ4esDOw0LzlMiWO0mQnGS3MeHuSOI4rtYKzGyg=", "uFGx4n3+zHu5+4+fCYlTNg=="), //:\Program Files\Ravantivirus
Program.drive_letter + Bfs.Create("n1jqRVKvn1u0YjB+W8qcczc0ASkv86h08unsneldMNQ=","YpIp1jiujK0SbllF6fT9UKJ02qNcRvpGaNQwcPBb7Qc=", "iv1glbnrE1jtMjRk8mqvmA=="), //:\Program Files\SpyHunter
Program.drive_letter + Bfs.Create("PxHI8wdlOk+S9YIqt82KNrdJjD0nWLlo/4xneG+9RpTLbMZu6qgo7EMTzZ7wWsSw","QirWs6CFZTmhTnT/G3Age9i3QSDaiVA3T71GcZikKak=", "6ElYQpMPRSsHUXX0ygsW9g=="), //:\Program Files\Process Hacker 2
Program.drive_letter + Bfs.Create("pwS0+xwIEQH/otESX+E0aNif1QL7b0wmHubmDNxjdpw=","JaFm9ZbONHDcvxSHoTGY1GjNMn+AwwL81Lkj6tv45/I=", "8ECvZwgYpK+HTyQYUaYwFQ=="), //:\Program Files\RogueKiller
Program.drive_letter + Bfs.Create("jC76HPYjuKA64GzEW6OJI4KXmiEdFaQnQDzUaERiNCbRzZPnf4dTO2ULOFeL2iwV","3qr0AWqFFzbhPu4Pv/MLVGsUdUZeAigOqcdGs+GwYW8=", "GjN09VvUUaRIpBx3Wvu03w=="), //:\Program Files\SUPERAntiSpyware
Program.drive_letter + Bfs.Create("+1GBFL0dowBD/XJ0si0+YkQw4QivipTZR34A3WjBA2Y=","ouw/OmkZh/tqBUFzKRlg9ZPgKXaO5TwBhGLZ2Xcc+j0=", "pVd/gVxiZ8VJtsK3olEAGw=="), //:\Program Files\HitmanPro
Program.drive_letter + Bfs.Create("nVD/+y+EwT+g1Zqo111NMHpPD3YafS+sd5GuKAH0cmk=","ZIxWoyT3ZJQg7aIpzBodmoXpkRvZhWzRyOSN149Q1sY=", "iV36IcV+t/mRDnfZd/FAxw=="), //:\Program Files\RDP Wrapper
Program.drive_letter + Bfs.Create("ncmKTf1CxoYWG7BtbVVPDfafFwSgd+a47rbiMCR1kvA=","fzzxwYN1Jcra20rLvuan8OfXtQDycKzrd/x/KcXoMvg=", "8YHwqhkG4nKuTc3SArNAYg=="), //:\Program Files\QuickCPU
Program.drive_letter + Bfs.Create("mjbw5/6SU07CdPLxI5geArVQ6/PIzncfKIgwL/xt5Zs=","pBhLdPo7uGnrzWDSTwLzmJMMzkeSr5+BOgysiIXKVHs=", "KSH3KkHDsc5mRG9fGhppLQ=="), //:\Program Files\NETGATE
Program.drive_letter + Bfs.Create("ru9/CfiYFmyzyd60M3HwlNThQG+hh53CsRhILgW+KUk=","SvouRGklCkMqTu2xo/toI2zlqR1j7QWPGEhafABvXkQ=", "1iantUYZBLB9JGTYK1WQ7A=="), //:\Program Files\Google\Chrome
Program.drive_letter + Bfs.Create("iA+dZkg/fGEMFjpqm0i6yg==","t9nKeB92zqSrAfiqPCL/PpV5DM6LOi2y4kIZrq5YrEQ=", "HI/NqpTXEkih73K/pKBp1A=="), //:\AdwCleaner
Program.drive_letter + Bfs.Create("vBpRabp/3uO32hDX3orAuw==","QmZuVe3C1Nirf+8R1jZ2bZjttfriQYK47uT+LL5BD6A=", "vqqhsBAnw0JhFAZCnjqZUg=="), //:\KVRT_Data
Program.drive_letter + Bfs.Create("Oy+1UHu/khgtIkeUcBghmA==","MqOsPtmuy9vh23TokeLb/IwTAx7+zsf3INFwenbrs0A=", "3W15zTrtlQd/edI760XMQw=="), //:\KVRT2020_Data
Program.drive_letter + Bfs.Create("YWkkcGnWoGUGKPUrKy2G5w==","mqZj9zhh5FfJHvJxDeZNml85EaZIr2rZxJNcw2XjThw=", "Y+Nd+PTSh4R2NWZoHrUpGA=="), //:\FRST
};


            if (!Program.WinPEMode)
            {
                obfStr5.Add(Path.Combine(Environment.GetEnvironmentVariable("App~da~ta".Replace("~", "")), "s~ysfi~les".Replace("~", "")));
                obfStr5.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToLower(), "auto?lo?gger".Replace("?", "")));
                obfStr5.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToLower(), "av?_b?l?ock?_?rem?over".Replace("?", "")));
                obfStr5.Add(Path.Combine(@utils.GetDownloadsPath(), "a?utolog?ger".Replace("?", "")));
                obfStr5.Add(Path.Combine(@utils.GetDownloadsPath(), "a?v_b?lock?_re?mov?er".Replace("?", "")));
            }

            ScanDirectories(obfStr5, founded_suspLckPths);

            if (!Program.ScanOnly)
            {
                if (founded_suspLckPths.Count == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }


            List<string> obfStr1 = new List<string>() {
Program.drive_letter + Bfs.Create("cQBPtmnlia5JbghgMFLZxkhKJEhSiEI2t3IAzuRwFeE=","yZmG5iZT01nECq7XG1/Yi9LVMeTtwmF2ESPZ0qV6gX4=", "48fE9re6gjtiOVAUjEcojw=="), //:\ProgramData\Install
Program.drive_letter + Bfs.Create("KW09O4URjMGPVu93Q0Vco6toeA15wthSKQxht8ZM0nI=","mOTBt11bTEjFuv1J5Sw1uCiv99j5CFBWVoREOfpfq7s=", "7r6Kywmdd8x4vMUPKUde9A=="), //:\ProgramData\Microsoft\Check
Program.drive_letter + Bfs.Create("ehHHUvQTvmQj8kQktMQwCTjOAoAAk3Uw2ZgYXsS8z8o=","0+R9BZnfQRHhpSgJb3mVqtvWUo1W4loJXevKtjJ7kNY=", "DKbs30edDc2IfB0kSUVHGw=="), //:\ProgramData\Microsoft\Intel
Program.drive_letter + Bfs.Create("yOKc2A4xZNOVR9Dl6cty0AMOlq7DJYqRInzfZBUvFFomDoSsME1RlPXYLwNentl/yKYYTAk7JA94PU22N9lkkQ==","0oE2xRQp4kUvXXDGJj5oIZFNeDhdO4/emxvMTNb2XBM=", "DIyu8ILFOJR1a7UnDDenYQ=="), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
Program.drive_letter + Bfs.Create("l0+ITHhmHZXekKNCgLs4WD25if4ZSKdYbFYQzrBhylQ=","o7zVyLsjomR9A9spbST2rPZLHkeAc1qpZX9Q67ZkfSc=", "fSw6lQPspxpKCrZWiNeK+g=="), //:\ProgramData\Microsoft\temp
Program.drive_letter + Bfs.Create("eC5OIa9kU5q1Iu8J7uXMAFcvNTHVhlzxkUroCsjpVAM=","x9+Jzgo491WqwGtIp8+Hts74pUrlUHQUHsmYcH4Iryk=", "9qav08svMTplYEOoqVw2/w=="), //:\ProgramData\PuzzleMedia
Program.drive_letter + Bfs.Create("Q4lRPNj6lmcHj2n3DuJvzueOno6++XDXIw7kCDaMMmg=","QOwsHsAlRjn1WfP39kto3OSbuqnUJ8zd89KlfDU4WiI=", "IsORduZwmav6Hlj9s+hyow=="), //:\ProgramData\RealtekHD
Program.drive_letter + Bfs.Create("toBdieDU+Hxjb3TzuQjzCj4a4Ki0RYZ04CFx3D1CsO8=","5R4gw2tj6dEzH8y+k+Iwq5xCQczvcOxSWrM2AD0S48Y=", "EW2N/jVvHC1tcMraeuQClQ=="), //:\ProgramData\ReaItekHD
Program.drive_letter + Bfs.Create("1G8ulyc9KdVb4EXJGVfP1oklIJbvNpzqovElXdf0rks=","Yo/4VZCNlTct6e2SdCV76sFtU373QoyU+Y6H9xULVBM=", "XnIPbPDb8NNzWZqDEzmBtA=="), //:\ProgramData\RobotDemo
Program.drive_letter + Bfs.Create("2SGlDI33s+Xlijc+QEFZDnrcSJkaRWx3V+w19TU8+zY=","YhoyOqcRswnK3Hu+qiXk4qIgb2Kx9f5DEEsymAW44iI=", "QAk+Y15ZnNlkOvF3tDskSQ=="), //:\ProgramData\RunDLL
Program.drive_letter + Bfs.Create("PFjt7KF2nKoycuEsDu8TxSai5EG3z+f2H0XTJabjCBo=","rvJxsNs1+QmcP8JhI9sv4GAVqy2k3QACsfpyXp31HkM=", "yStnfhULf7YSC8pFkK9rtw=="), //:\ProgramData\Setup
Program.drive_letter + Bfs.Create("OquIaio0lQHg19JjbB/+3TaTjcueUOmaRIl/AaVP02g=","GpggbQ92DSqJQEh443F2YXgAS+wYD8kzyYvCTCydgl4=", "KsisyT3tat5vKug+TdLDTw=="), //:\ProgramData\System32
Program.drive_letter + Bfs.Create("8pSih0S4oEnib/Zpim+jFvX/sk87Ck/OqLLCOA7YHzk=","an+7Q2HbgnGDXd3n5w2qXkcciHzJKF9YpRpCPso6PY4=", "ieBb5oAK2SMOZcyW5MIHYA=="), //:\ProgramData\WavePad
Program.drive_letter + Bfs.Create("X6c5yHBPXh+VZzCONpHMHxAibFTBGHs3DpyuhxxUc1ckvVqjUQP5hA9KTVf9PP7z","6558jbXYxzoh6IDRABjW65YTMSwUVGyEmS+zWfvFdsI=", "lp8dfPco6H60xfqqvYdSEw=="), //:\ProgramData\Windows Tasks Service
Program.drive_letter + Bfs.Create("DbWtvSvVEhHm5NV5R+RSpRqRBGlPv1R/RincxMa1pU0=","ByVI99TtgK7UK5EsRd5QuHPi/tPa6Y9QRwkyGKST6bo=", "+TRlYmbxoclp5bOiYOpAew=="), //:\ProgramData\WindowsTask
Program.drive_letter + Bfs.Create("4JYt+F6RPynt5CIWeikEA9fbbbE1B1tOD48Wn/u2G0k=","M+GzSt/esCOR2vJ2kuRM+tktARwOEDx8j7PovUsXaOI=", "pbUTKERwanSekKajK5uW3A=="), //:\ProgramData\Google\Chrome
Program.drive_letter + Bfs.Create("XArU8D+qVicU6uaeUBAjk5R1IWXoQG9ZKp+vtsLCUdU=","IzYxXdPZrF7HS0z/pJYK/BJSilAehqqdJMbzLCGc9e4=", "llmFLlp3MnJc48T7clU9ig=="), //:\Program Files\Transmission
Program.drive_letter + Bfs.Create("WRX6UyasoxFxigggl6LO++gMpCynHx1IJ9ZXI/NLGlM=","ayUiA0zuQ6EkTCDX3CSwghpPVCmTf14JPSMdHvO+dmE=", "TxUsgzGAQK1+PbagLW8ljg=="), //:\Program Files\Google\Libs
Program.drive_letter + Bfs.Create("f19V4xcTkCx2wEqcmSmnIxAIXaQigJNpkA3UcJyco6anx29eNIK9glXi3mGqzRVA","K5z4dGQMrA3LKyeleB9c5HeM0N4gAtOX+mpYmiAEmfs=", "P+IYv2Jxtg6A8/S9871pbA=="), //:\Program Files (x86)\Transmission
Program.drive_letter + Bfs.Create("0Egpy1DZrIf9hI5TXvUL3uazi3tFrqKd5F2pjWXrSYU=","LlUq2a7FjW2VnXi6vLHAGVnbKYjG05tjQ5sg5GoPpW8=", "EEATaWT8QQMQMi/9p2BUHQ=="), //:\Windows\Fonts\Mysql
Program.drive_letter + Bfs.Create("RyemaXXidYon7ceV7LGuikbeZbS/zGmiON9yr7E6aHV1SBT8Ogr4tnSJIzWYrYvS","i7u6fyZN/zDZKIfhb77Gt870M1iUuiO7cDku5GanGxg=", "pL+9kFDIgZe74C+dy2dZNQ=="), //:\Program Files\Internet Explorer\bin
Program.drive_letter + Bfs.Create("aC+QLS1zFEPMKityDM/MFVp39tQtj1AC/heEY5hjUuQ=","cylPUcIAniWrzVCy8T1SvK5+reOGKJ4ysw8RTXvb5KQ=", "O+IOkw7W+1LdJ06RuyEvFg=="), //:\ProgramData\princeton-produce
Program.drive_letter + Bfs.Create("IRQ+otWD53/kWOsOQ+3yhSWZP7TLxMNfVl0EPKdCUSw=","YZxzxZ24TPp8fpnD9PM7li63Q8iha4Up3IXjDOJezJo=", "mJyPKsM3NXEYmVByWbo9DQ=="), //:\ProgramData\Timeupper
Program.drive_letter + Bfs.Create("8sWF+sVJMisfaQJKZIpVVLGk2IHH2bOerVXBAp9zhfQ=","kHDzkXD5wxNqNAx7kXTdGKMZWTIJOsKwJnN7QgCqRF8=", "kaCi2jeG/4jysodRmY0RKg=="), //:\Program Files\RDP Wrapper
};

            ScanDirectories(obfStr1, founded_mlwrPathes);
            if (!Program.ScanOnly)
            {
                if (founded_mlwrPathes.Count == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }

#if !DEBUG
            LL.LogHeadMessage("_ScanFiles");

            string baseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft").ToLower().Replace("x:", $@"{Program.drive_letter}:");
            FindMlwrFiles(baseDirectory);

            if (!Program.ScanOnly)
            {
                if (founded_mlwrPths.Count == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }
#endif



            if (!Program.WinPEMode)
            {
                ScanRegistry();
                if (!Program.no_services)
                {
                    ScanServices();
                }

                switch (Utils.GetBootMode())
                {
                    case BootMode.Normal:
                        LL.LogHeadMessage("_ScanFirewall");
                        ScanFirewall();
                        if (!Program.no_scan_tasks)
                        {
                            LL.LogHeadMessage("_ScanTasks");
                            ScanTaskScheduler();
                        }
                        break;
                    case BootMode.SafeMinimal:
                        LL.LogStatusMessage("_SafeBootHint");
                        break;
                    case BootMode.SafeNetworking:
                        LL.LogHeadMessage("_ScanFirewall");
                        ScanFirewall();
                        LL.LogHeadMessage("_SafeBootNetworkingHint");

                        break;
                    default:
                        break;
                }
            }
            CleanHosts();
        }

        public void Clean()
        {
            if (mlwrPids.Count != 0)
            {
                LL.LogHeadMessage("_Malici0usProcesses");
                if (Program.ScanOnly)
                {
                    foreach (var id in mlwrPids)
                    {
                        using (Process p = Process.GetProcessById(id))
                        {
                            string pname = p.ProcessName;
                            int pid = p.Id;

                            if (!p.HasExited)
                            {
                                LL.LogCautionMessage("_Malici0usProcess", $"{pname} - PID: {pid}");
                            }
                        }
                    }
                    LocalizedLogger.LogScanOnlyMode();

                }
                else
                {
                    LL.LogHeadMessage("_TryCloseProcess");

                    Utils.UnProtect(mlwrPids.ToArray());

                    foreach (var id in mlwrPids)
                    {
                        try
                        {
                            using (Process p = Process.GetProcessById(id))
                            {
                                string pname = p.ProcessName;
                                int pid = p.Id;

                                p.Kill();

                                if (p.HasExited)
                                {
                                    LL.LogSuccessMessage("_ProcessTerminated", $"{pname}, PID: {pid}");
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            LL.LogErrorMessage("_ErrorTerminateProcess", ex);
                            continue;
                        }
                    }
                }
            }

            LL.LogHeadMessage("_RemovingKnownMlwrFiles");

            int deletedFilesCount = 0;

            foreach (string path in obfStr2)
            {
                if (File.Exists(path))
                {
                    if (!Program.ScanOnly)
                    {
                        UnlockFile(path);
                        try
                        {
                            File.SetAttributes(path, FileAttributes.Normal);
                            File.Delete(path);
                            LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");
                            deletedFilesCount++;
                        }
                        catch (Exception)
                        {
                            LL.LogWarnMediumMessage("_ErrorCannotRemove", path);

                            LL.LogMessage("\t[.]", "_TryUnlockDirectory", "", ConsoleColor.White);
                            UnlockDirectory(Path.GetDirectoryName(path));
                            try
                            {
                                LL.LogSuccessMessage("_UnlockSuccess");

                                try
                                {
                                    uint processId = Utils.GetProcessIdByFilePath(path);

                                    if (processId != 0)
                                    {
                                        Process process = Process.GetProcessById((int)processId);
                                        if (!process.HasExited)
                                        {
                                            process.Kill();
                                            LL.LogSuccessMessage("_BlockingProcessClosed", $"PID: {processId}");
                                        }
                                    }
                                }
                                catch (Exception) { }

                                Thread.Sleep(100);
                                File.Delete(path);
                                if (!File.Exists(path))
                                {
                                    LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");
                                    deletedFilesCount++;
                                }

                            }
                            catch (Exception ex)
                            {
                                LL.LogErrorMessage("_ErrorCannotRemove", ex, path, "_File");
                            }
                        }
                    }
                    else
                    {
                        LL.LogCautionMessage("_Malici0usFile", path);
                    }

                }
            }

            if (!Program.ScanOnly)
            {
                if (deletedFilesCount == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }


            if (!Program.ScanOnly)
            {
                CleanFoundedMlwr();
            }

            if (suspFls_path.Count > 0)
            {
                LL.LogHeadMessage("_RemovingMLWRFiles");
                foreach (string path in suspFls_path)
                {
                    if (File.Exists(path))
                    {
                        if (!Program.ScanOnly)
                        {
                            UnlockFile(path);
                            try
                            {
                                File.SetAttributes(path, FileAttributes.Normal);
                                File.Delete(path);
                                LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");

                            }
                            catch (Exception)
                            {
                                LL.LogWarnMediumMessage("_ErrorCannotRemove", path);

                                LL.LogMessage("\t[.]", "_TryUnlockDirectory", "", ConsoleColor.White);

                                UnlockDirectory(Path.GetDirectoryName(path));
                                try
                                {
                                    LL.LogSuccessMessage("_UnlockSuccess");
                                    try
                                    {
                                        uint processId = Utils.GetProcessIdByFilePath(path);

                                        if (processId != 0)
                                        {
                                            Process process = Process.GetProcessById((int)processId);
                                            if (!process.HasExited)
                                            {
                                                process.Kill();
                                                LL.LogSuccessMessage("_BlockingProcessClosed", $"PID: {processId}");

                                            }
                                        }
                                    }
                                    catch (Exception) { }
                                    Thread.Sleep(100);
                                    File.Delete(path);
                                    if (!File.Exists(path))
                                    {
                                        LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");

                                    }

                                }
                                catch (Exception ex)
                                {

                                    LL.LogErrorMessage("_ErrorCannotRemove", ex, path, "_File");
                                }
                            }
                        }
                        else
                        {
                            LL.LogCautionMessage("_Malici0usFile", path);
                        }
                    }
                }
            }

            if (!Program.ScanOnly)
            {
                LL.LogHeadMessage("_CheckingTermService");
                utils.CheckTermService();

            }

            if (founded_mlwrPathes.Count > 0)
            {
                LL.LogHeadMessage("_RemovingMLWRPaths");

                foreach (string str in founded_mlwrPathes)
                {
                    if (!Program.ScanOnly)
                    {
                        UnlockDirectory(str);
                        try
                        {

                            Directory.Delete(str, true);
                            if (!Directory.Exists(str))
                            {
                                LL.LogSuccessMessage("_Directory", str, "_Deleted");
                            }
                        }
                        catch (Exception ex)
                        {

                            LL.LogErrorMessage("_ErrorCannotRemove", ex, $"\"{str}\"", "_Directory");
                        }
                    }
                    else
                    {
                        LL.LogWarnMediumMessage("_MaliciousDir", str);
                    }
                }
            }

            if (founded_suspLckPths.Count > 0)
            {
                UnlockFolders(founded_suspLckPths);
            }

            if (!Program.WinPEMode)
            {
                LL.LogHeadMessage("_CheckUserJohn");

                if (Utils.CheckUserExists("J?ohn".Replace("?", "")))
                {
                    if (!Program.ScanOnly)
                    {
                        try
                        {
                            Utils.DeleteUser("J?ohn".Replace("?", ""));
                            Thread.Sleep(100);
                            if (!Utils.CheckUserExists("J?ohn".Replace("?", "")))
                            {
                                LL.LogSuccessMessage("_Userprofile", "\"J?ohn\"", "_Deleted");
                            }
                            else LL.LogErrorMessage("_ErrorCannotRemove", new Exception(""), $"\"John\"", "_Userprofile");

                        }
                        catch (Exception ex)
                        {
                            LL.LogErrorMessage("_ErrorCannotRemove", ex, $"\"J?ohn\"".Replace("?", ""), "_Userprofile");
                        }
                    }
                    else
                    {
                        LL.LogWarnMediumMessage("_MaliciousProfile", "J?ohn");
                        LocalizedLogger.LogScanOnlyMode();
                    }

                }
                else
                {
                    if (!Program.ScanOnly)
                    {
                        LocalizedLogger.LogNoThreatsFound();
                    }
                }

            }

        }
        void UnlockFolders(List<string> inputList)
        {
            int foldersDeleted = 0;
            foreach (string str in inputList)
            {
                try
                {
                    if (!Program.ScanOnly)
                    {
                        UnlockDirectory(str);
                        if (Utils.IsDirectoryEmpty(str))
                        {
                            Directory.Delete(str, true);
                            if (!Directory.Exists(str))
                            {
                                LL.LogMessage("\t[_]", "_RemovedEmptyDir", $"\"{str}\"", ConsoleColor.White);
                                foldersDeleted++;
                            }
                        }
                    }
                    else
                    {
                        LL.LogWarnMessage("_LockedDir", $"\"{str}\"");
                    }
                }
                catch (Exception ex1)
                {
#if DEBUG
                    Logger.WriteLog($"\t[*] DeleteEmpyFolders exception: {ex1.Message}", ConsoleColor.Gray, false);
#endif
                    try
                    {
                        UnlockDirectory(str);
                        if (Utils.IsDirectoryEmpty(str))
                        {
                            Directory.Delete(str);
                            if (!Directory.Exists(str))
                            {
                                LL.LogMessage("\t[_]", "_RemovedEmptyDir", $"\"{str}\"", ConsoleColor.White);
                                foldersDeleted++;
                            }
                        }
                    }
                    catch (Exception ex2)
                    {
                        LL.LogErrorMessage("_ErrorCannotRemove", ex2, str, "_Directory");

                    }

                }
            }

            if (!Program.ScanOnly)
            {
                if (foldersDeleted == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }

        }
        void ScanDirectories(List<string> constDirsArray, List<string> newList)
        {
            foreach (string dir in constDirsArray)
            {
                if (Directory.Exists(dir))
                {
                    newList.Add(dir);
                }
            }
        }
        void ScanFirewall()
        {
            int firewall_items = 0;
            try
            {
                Type typeFWPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
                dynamic fwPolicy2 = Activator.CreateInstance(typeFWPolicy2);

                INetFwRules rules = fwPolicy2.Rules;

                foreach (string programPath in obfStr2)
                {
                    foreach (INetFwRule rule in rules)
                    {
                        if (rule.ApplicationName != null)
                        {
                            if (rule.ApplicationName.ToLower() == programPath.ToLower())
                            {
                                LL.LogMessage("\t[.]", "_Name", rule.Name, ConsoleColor.White);
                                LL.LogWarnMessage("_Path", rule.ApplicationName);

                                if (!Program.ScanOnly)
                                {
                                    rules.Remove(rule.Name);
                                    firewall_items++;
                                    LL.LogSuccessMessage("_Rule", rule.Name, "_Deleted");
                                }

                                Logger.WriteLog($"------------------------------", ConsoleColor.White);
                            }
                        }

                    }

                }
                if (!Program.ScanOnly && firewall_items == 0)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }

                if (Program.ScanOnly)
                {
                    LocalizedLogger.LogScanOnlyMode();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error get firewall rules: {ex.Message}");
            }
        }
        void FindMlwrFiles(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return;
            }

            try
            {

                var files = Utils.GetFiles(directoryPath, "*.bat");

                foreach (string file in files)
                {
                    if (!Utils.IsAccessibleFile(file))
                    {
                        continue;
                    }

                    if (utils.IsBatchFileBad(file))
                    {
                        LL.LogWarnMessage("_SuspiciousFile", file);

                        founded_mlwrPths.Add(file);
                        foreach (var nearExeFile in Directory.GetFiles(Path.GetDirectoryName(file), "*.exe", SearchOption.TopDirectoryOnly))
                        {
                            founded_mlwrPths.Add(nearExeFile);
                        }
                    }
                    else
                    {
                        LL.LogMessage("[.]", "_File", file, ConsoleColor.White);
                    }
                }

                IEnumerable<string> directories = Directory.EnumerateDirectories(directoryPath);
                foreach (string directory in directories)
                {
                    FindMlwrFiles(directory);
                }


            }
            catch (Exception)
            {
            }


        }
        void CleanHosts()
        {
            LL.LogHeadMessage("_ScanningHosts");

            RegistryKey hostsDir = Registry.LocalMachine.OpenSubKey(obfStr7[0]);
            if (hostsDir != null)
            {
                string hostsPath = hostsDir.GetValue("DataBasePath").ToString();
                if (hostsPath.StartsWith("%"))
                {
                    hostsPath = Utils.ResolveEnvironmentVariables(hostsPath);
                }

                string hostsPath_full = hostsPath + "\\h?os?t?s".Replace("?", "");

                if (Program.WinPEMode)
                {
                    hostsPath_full.Replace("C:", $"{Program.drive_letter}:");
                }

                if (!Program.WinPEMode && !File.Exists(hostsPath_full))
                {
                    LL.LogMessage("\t[?]", "_HostsFileMissing", "", ConsoleColor.Gray);

                    File.Create(hostsPath_full).Close();
                    Thread.Sleep(100);
                    if (File.Exists(hostsPath_full))
                    {
                        LL.LogSuccessMessage("_HostsFileCreated");
                    }
                    return;
                }


                try
                {
                    UnlockFile(hostsPath_full);
                    File.SetAttributes(hostsPath_full, FileAttributes.Normal);
                }
                catch (Exception ex)
                {
                    LL.LogErrorMessage("_ErrorCleanHosts", ex);
                    return;
                }

                try
                {
                    List<string> lines = File.ReadAllLines(hostsPath_full).ToList();
                    int deletedLineCount = 0;

                    for (int i = lines.Count - 1; i >= 0; i--)
                    {
                        string line = lines[i];
                        foreach (HashedString hLine in hStrings)
                        {
                            if (hLine.OriginalLength < line.Length)
                            {
                                string truncatedLine = line.Substring(line.Length - hLine.OriginalLength);
                                if (Utils.StringMD5(truncatedLine).Equals(hLine.Hash))
                                {
                                    if (!Program.ScanOnly)
                                    {
                                        lines.RemoveAt(i);
                                        deletedLineCount++;
                                        break;
                                    }
                                    else
                                    {
                                        LL.LogWarnMessage("_MaliciousEntry", line);
                                    }
                                }
                            }
                        }
                    }


                    if (deletedLineCount > 0)
                    {
                        if (!Program.ScanOnly)
                        {
                            File.WriteAllLines(hostsPath_full, lines);
                            string logMessage = $"Ho?sts file has been recovered. Affected strings {deletedLineCount}".Replace("?", "");
                            Logger.WriteLog(logMessage, Logger.success);
                        }
                        else
                        {
                            LocalizedLogger.LogScanOnlyMode();
                        }

                    }
                    else if (!Program.ScanOnly)
                    {
                        LocalizedLogger.LogNoThreatsFound();
                    }

                }
                catch (Exception e)
                {
                    LL.LogErrorMessage("_ErrorCleanHosts", e);
                }
            }
        }
        void ScanRegistry()
        {
            LL.LogHeadMessage("_ScanRegistry");

            int affected_items = 0;

            #region DisallowRun
            Logger.WriteLog(@"[Reg] Dis?allo?wRun...".Replace("?", ""), ConsoleColor.DarkCyan);
            try
            {
                RegistryKey DisallowRunKey = Registry.CurrentUser.OpenSubKey(obfStr7[1], true);
                if (DisallowRunKey != null)
                {
                    if (DisallowRunKey.GetValueNames().Contains("Dis?allo?wRun".Replace("?", "")))
                    {
                        LL.LogWarnMessage("_SuspiciousRegKey", "D?isa?llo?wRun");

                        if (!Program.ScanOnly)
                        {
                            DisallowRunKey.DeleteValue("Dis?allo?wRun".Replace("?", ""));
                            if (!DisallowRunKey.GetValueNames().Contains("Dis?allo?wRun".Replace("?", "")))
                            {
                                LL.LogSuccessMessage("_RegistryKeyRemoved", "Dis?allo?wRun");
                                affected_items++;
                            }
                        }

                    }

                    if (!Program.ScanOnly)
                    {
                        RegistryKey DisallowRunSub = Registry.CurrentUser.OpenSubKey(obfStr7[2], true);
                        if (DisallowRunSub != null)
                        {
                            DisallowRunKey.DeleteSubKeyTree("Di?sall?owR?un".Replace("?", ""));
                            DisallowRunSub = Registry.CurrentUser.OpenSubKey(obfStr7[2], true);
                            if (DisallowRunSub == null)
                            {
                                LL.LogSuccessMessage("_RegistryKeyRemoved", "Dis?allo?wRun (hive)");
                                affected_items++;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                LL.LogErrorMessage("_ErrorCannotOpen", ex, "HKCU\\...\\Explorer");
            }

            #endregion

            #region Appinit_dlls
            Logger.WriteLog(@"[Reg] AppInitDLL...", ConsoleColor.DarkCyan);
            try
            {
                RegistryKey appinit_key = Registry.LocalMachine.OpenSubKey(obfStr7[3], true);
                if (appinit_key != null)
                {
                    if (!String.IsNullOrEmpty(appinit_key.GetValue("App??In??it_DL?Ls".Replace("?", "")).ToString()))
                    {
                        if (appinit_key.GetValue("Loa??dApp??Init_DLLs".Replace("?", "")).ToString() == "1")
                        {
                            if (!appinit_key.GetValueNames().Contains("RequireSignedApp?Ini?t_D?LLs".Replace("?", "")))
                            {
                                LL.LogWarnMessage("_AppInitNotEmpty");
                                LL.LogCautionMessage("_SignedAppInitNotFound");

                                if (!Program.ScanOnly)
                                {
                                    appinit_key.SetValue("RequireSignedApp?Init?_DLLs".Replace("?", ""), 1, RegistryValueKind.DWord);
                                    if (appinit_key.GetValue("RequireSignedApp?Init?_DLLs".Replace("?", "")).ToString() == "1")
                                    {
                                        LL.LogSuccessMessage("_ValueWasCreated");
                                        affected_items++;
                                    }
                                }
                            }
                            else if (appinit_key.GetValue("RequireSignedApp?Init?_DLLs".Replace("?", "")).ToString() == "0")
                            {
                                LL.LogWarnMessage("_AppInitNotEmpty");
                                LL.LogCautionMessage("_SignedAppInitValue");

                                if (!Program.ScanOnly)
                                {
                                    appinit_key.SetValue("Re?qu?ireSigne?dApp?Init?_DLLs".Replace("?", ""), 1, RegistryValueKind.DWord);
                                    if (appinit_key.GetValue("Requi????reSignedApp?Init?_DLLs".Replace("?", "")).ToString() == "1")
                                    {
                                        LL.LogSuccessMessage("_ValueSetTo1");
                                        affected_items++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LL.LogErrorMessage("_Error", ex);
            }

            #endregion

            #region HKLM
            try
            {
                RegistryKey AutorunKey = Registry.LocalMachine.OpenSubKey(obfStr7[4], true);
                if (AutorunKey != null)
                {
                    Logger.WriteLog(@"[Reg] HKLM Autorun...", ConsoleColor.DarkCyan);
                    List<string> RunKeys = AutorunKey.GetValueNames().ToList();

                    foreach (string value in RunKeys)
                    {
                        string path = Utils.ResolveFilePathFromString(AutorunKey, value);
                        if (path == "")
                            continue;

                        if (File.Exists(path))
                        {
                            winTrust.VerifyEmbeddedSignature(path);
                        }
                        else
                        {
                            LL.LogWarnMessage("_FileIsNotFound", $"{AutorunKey.GetValue(value)} | {value}");
                        }


                        if (AutorunKey.GetValue(value).ToString() == $@"{Program.drive_letter}:\Pro?gra?mDa?ta\Re?aItek?HD\task?host?w.e?x?e".Replace("?", ""))
                        {
                            string valuename = value;
                            if (!Program.ScanOnly)
                            {
                                AutorunKey.DeleteValue(value);
                                LL.LogSuccessMessage("_RegistryKeyRemoved", valuename);

                                affected_items++;
                            }
                            else
                            {
                                LL.LogWarnMediumMessage("_FoundMlwrKey", valuename);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                LL.LogErrorMessage("_ErrorCannotOpen", ex, "HKLM\\...\\run");


            }

            #endregion

            #region HKCU
            Logger.WriteLog(@"[Reg] HKCU Autorun...", ConsoleColor.DarkCyan);
            try
            {
                RegistryKey AutorunKey = Registry.CurrentUser.OpenSubKey(obfStr7[4], true);
                if (AutorunKey != null)
                {

                    List<string> RunKeys = AutorunKey.GetValueNames().ToList();
                    foreach (string value in RunKeys)
                    {
                        string path = Utils.ResolveFilePathFromString(AutorunKey, value);
                        if (path == "")
                            continue;

                        if (File.Exists(path))
                        {
                            winTrust.VerifyEmbeddedSignature(path);
                        }
                        else
                        {
                            LL.LogWarnMessage("_FileIsNotFound", $"{AutorunKey.GetValue(value)} | {value}");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LL.LogErrorMessage("_ErrorCannotOpen", ex, "HKCU\\...\\run");

            }

            try
            {
                Logger.WriteLog(@"[Reg] te!kt!on!it...".Replace("!", ""), ConsoleColor.DarkCyan);

                RegistryKey tektonit = Registry.CurrentUser.OpenSubKey(@"Software", true);
                if (tektonit.GetSubKeyNames().Contains("tek?toni?t".Replace("?", "")))
                {
                    LL.LogWarnMessage("_SuspiciousRegKey", "te?kto?nit");


                    if (!Program.ScanOnly)
                    {
                        tektonit.DeleteSubKeyTree("tek?ton?it".Replace("?", ""));
                        if (!tektonit.GetSubKeyNames().Contains("tek?ton?it".Replace("?", "")))
                        {
                            LL.LogSuccessMessage("_RegistryKeyRemoved", "tek?t?onit");

                            affected_items++;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LL.LogErrorMessage("_ErrorCannotOpen", ex, "HKCU\\...\\t?e??k??ton?i?t");

            }
            #endregion

            #region Applocker

            Logger.WriteLog(@"[Reg] Applocker...", ConsoleColor.DarkCyan);
            string registryPath = @"SOFTWARE\Policies\Microsoft\Windows\SrpV2\Exe";
            List<string> badSubkeys = new List<string>()
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
                "d8ee32c1-472b-41dd-a204-b198cb1ae9b8",
                "ea9fa9c5-2743-44a1-99ed-d9ac26a135e7",
                "ec544bd8-4a5d-4ae7-8c5c-044f4b6d60fb",
                "ec77c5b9-3955-44f4-804b-c678504c16b6",
                "f025c3b3-d9d1-4c09-be3b-bfc05fdbe243",
                "f2be1651-b3c6-477d-a183-8f2946538210",
                "f9729781-9d66-46b8-8553-f0099fd924d3",
                "f9b3908f-4f58-45ec-a9a8-c1b88e9dbe98",
                "d8e659be-d4a5-4cd6-bf96-c92736039685",
                "e8a3f75c-ee02-4c96-958e-7e31352c196c",
                "eedeed7f-e2e7-4181-8050-4a4f90361328",
                "adb6a6f1-9af9-496f-b8d4-ba695911f83a"
            };
            List<string> allSubkeys = utils.GetSubkeys(registryPath);

            if (allSubkeys.Count > 0)
            {
                using (RegistryKey parentKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(registryPath, true))
                {
                    foreach (var subkeyName in allSubkeys)
                    {
                        if (badSubkeys.Contains(subkeyName, StringComparer.OrdinalIgnoreCase))
                        {
                            try
                            {
                                parentKey.DeleteSubKeyTree(subkeyName);
                                if (!Utils.RegistryKeyExists(registryPath))
                                {
                                    LL.LogSuccessMessage("_RegistryKeyRemoved", subkeyName);

                                    affected_items++;
                                }
                            }
                            catch (Exception ex)
                            {
                                LL.LogErrorMessage("_ErrorCannotOpen", ex, subkeyName);

                            }
                        }
                    }
                }
            }

            #endregion

            #region WindowsDefender

            Logger.WriteLog(@"[Reg] Wind~ows De~fe~nder...".Replace("~", ""), ConsoleColor.DarkCyan);
            try
            {
                RegistryKey winDfndr = Registry.LocalMachine.OpenSubKey(obfStr7[5], true);
                if (winDfndr != null)
                {
                    List<string> obfStr3 = new List<string>() {
Program.drive_letter + Bfs.Create("tMa8OQ5hliIzejx7tT2Sm7LKplFLgciEJ1EjbnEJkvQ=","0F7RW+teTJ8ggnAzMkKaAAyZHZ0ITcz68yYqPhLDPG8=", "cO0p8ELEdu2LAfBNTrjqgg=="), //:\Program Files\RDP Wrapper
Program.drive_letter + Bfs.Create("nq7MEzterQipvMVnWAWEfQ==","KJFscx6NrfouJ2obhr/+V1iwPYuPYC416Co51mjJdDI=", "E9OmYeKPVVFNlZ/76VPLrg=="), //:\ProgramData
Program.drive_letter + Bfs.Create("ouP8Nd5rHD1mqQdASCKnjEiVrmpEeI2qgkVDZCrrJEjTU9ukfILDs4Bl6tkelxbk","4vQ95cC1hturas465M6GHD7/jeEyZyHq+a0UaQaDs4w=", "Vk+wfx8+jrL2Ai14SZy+7A=="), //:\ProgramData\ReaItekHD\taskhost.exe
Program.drive_letter + Bfs.Create("yUxWA2lY1k0eeiilu78G4gY1tVOqgDTFw9BbWNybOvK7fsTXJ+dKC9dU4gV5gdHg","XgLrGwDfL/Fg2BCgT+nPTP5oqyiWwdAoZMgmO/iJlug=", "RsPRzddam3X4oYET+ggjXw=="), //:\ProgramData\ReaItekHD\taskhostw.exe
Program.drive_letter + Bfs.Create("4v+G88jXr9sxSBi/3907D/IOAQUEgg4B+Y12yYzxiYC39Hvgv38grounnGd+8SSx","GZJSuL2MGZuR9sH4eiiWXbUM5LFsOZkNPr5B1Yb6FxI=", "qHobsWuT5W/nJvGvZ5O4TA=="), //:\ProgramData\RealtekHD\taskhost.exe
Program.drive_letter + Bfs.Create("payA2QalCaRyea13AS8pPLDekVCOTZsIMb7ebzi4pB/OYMScCDo+5jSCYCnlmBwR","XI17G+Z2JJl/B5DvRZul7bts5ohvHnjkdafxQFqexLA=", "PKu550DZkCDJj0s8r/ikvw=="), //:\ProgramData\ReaItekHD\taskhostw.exe
Program.drive_letter + Bfs.Create("FL6GqDYjm3DScGhTfUJn7QeuHhAiqG58MVQqaaYmY+kCQ9W4I2qijypLr9UDL4S5","mU//Spn/hlYt27ep0FkW69L0qry/sZlDRlWDLbs0fqA=", "Z1C7HvnMd3ZvfBh5BVFvfw=="), //:\ProgramData\Windows Tasks Service\winserv.exe
Program.drive_letter + Bfs.Create("DNuZbEsQ7uRWYJix60zwppJH44crlV4WzU+YTF25LwN0mWTEwOKVUXXNKupd23P0","B1K89IIDMpTjvlU4PE2kYSXSmFDnWg+jslhKxKKtaiU=", "2nlV2rD4RWhCJ/fXNfxqSg=="), //:\ProgramData\WindowsTask\AMD.exe
Program.drive_letter + Bfs.Create("haTh+ZL31n/Jt87BE3SYNxtkXYQ7ZqQ9ZU7IPreS1Obp0giqqsU2x2t7CTDO8eir","TDbcNzt0WLf84x10v0gS0ukRwqnpUvD9yYgCopn654A=", "MEKWuNbudwKk2kbC8GeM0Q=="), //:\ProgramData\WindowsTask\AppModule.exe
Program.drive_letter + Bfs.Create("JCte8IUTNPEUNgi7bMzw/LaVdb7Zi/vp4rO33sm0FwKfJkjFGRoaoEISox/ENbsh","WP8owDTvpMsNmvb9XPh8AvLGgmsWUGtUTAP0p1mgXjM=", "fmFZ5qVv4qQYAE1E5JB6vw=="), //:\ProgramData\WindowsTask\AppHost.exe
Program.drive_letter + Bfs.Create("d15A8T6/Eo9d1HfSuU1okghpYCjr9ycsaEmkG61QtXHGpbdzk8B+HalBtxCSlgwj","USquf/lM9heLgexixXAjaZd+mFPVRzhyrTMjBf7FAUk=", "W6VcUDF5QfjLry/3gjxX3Q=="), //:\ProgramData\WindowsTask\audiodg.exe
Program.drive_letter + Bfs.Create("GVr6pINPeix/xIN7bU4Ttj8qEvWW08EORb2dRCt+OmZY5iKwbwHY8OlRhkVzMA0w","fkQwEJvzcwdZB+IXVTe8rETv6cO8kPuRSdxkyOao48k=", "ntU/KIrOCGe3yiEV6FyKfQ=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
Program.drive_letter + Bfs.Create("4ZFiGR4potKzSWkrc7AGQe7Py8Rf2WTGgooT/rw9sck=","KTIiLtQ+jU8sT2Lz678PyzDFNfJSpY2u7lwDugrhKdM=", "QCKD6x6JZxKFrSLlDEVk7g=="), //:\Windows\System32
Program.drive_letter + Bfs.Create("AYWlGGjvlcZQYJGJtUZDuteCu+9dPJKBuLNwY+JYSlo=","RXuXvGVvKiuqBNHv95KFg5gVhTPgjOj01DGXTyuoX6A=", "fCUW/E9JWG3ZEtEC8+Fz9w=="), //:\Windows\SysWOW64\unsecapp.exe
};



                    foreach (string path in obfStr3)
                    {
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(obfStr7[6], true);

                        if (key != null)
                        {
                            string[] valueNames = key.GetValueNames();

                            foreach (string valueName in valueNames)
                            {
                                try
                                {
                                    if (valueName.ToString().Equals(path, StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (!Program.ScanOnly)
                                        {
                                            key.DeleteValue(valueName);
                                            LL.LogSuccessMessage("_RegistryKeyRemoved", valueName);

                                            affected_items++;
                                        }
                                        else
                                        {
                                            LL.LogWarnMediumMessage("_FoundMlwrKey", valueName);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LL.LogErrorMessage("_ErrorCannotOpen", ex, valueName);
                                }

                            }

                            key.Close();
                        }
                    }

                    obfStr3.Clear();

                    List<string> obfStr4 = new List<string>() {
Program.drive_letter + Bfs.Create("sPTKWoIov6G/mWAHbTbO7Rkn3bWvBi4eHvLP1HaYrAM=","Ii+vR83EB/trcaZz3PNNR8RFskgXzNrYbKanERxV+fA=", "jNcwypylqXgZnTOwOQiFNA=="), //:\ProgramData\RDPWinst.exe
Program.drive_letter + Bfs.Create("YYNFuJsFpkj2QF2iTsKJYV7eJ6l2jKVMds0D/YeqHeA01SIOWSrOOhyNGM8tHVGd","otxyDTm5Sd36AwxnWCvWoMLE6RmaQAMKrKhbROCExw4=", "61BDLfuhzHS4gJzdnUAHcA=="), //:\ProgramData\ReaItekHD\taskhost.exe
Program.drive_letter + Bfs.Create("SZonBcQT10jLBBnsaffn2Uqe0IiX0YdXHWyT+o/42ArDDg65+R04Xg1zRHQmetOe","2Bw8SGeKPRwstejZKqh44dt47abOWH28+8kf2QD8v2k=", "bv5kkR5mHBBq8qMIKn1Svg=="), //:\ProgramData\ReaItekHD\taskhostw.exe
Program.drive_letter + Bfs.Create("6yHBgASFHX46wYya7+AIpKGbB5mQ41ne5ra05EEBXhRegWr9FIm2ZjLbnJu7ddVl","ooxh1WiTzA39ZBuOoaCHCQQBBH31SHOOlzyRGJi4N+E=", "s03obvcGyAfjrPimspDhag=="), //:\ProgramData\RealtekHD\taskhost.exe
Program.drive_letter + Bfs.Create("d5i1kB9LI2dzjzlhQSrgz3S8GgetPYiXjJRKlAKbmOFQ4soTpP9WevdrNsdf/TvX","eEHVQl2hiTNZzF5GgWcc0wybBKf/S7l2m5jKDqo6gcA=", "1HD0bbZnOm9z2+5+E02XcQ=="), //:\ProgramData\RealtekHD\taskhostw.exe
Program.drive_letter + Bfs.Create("DQvbDztmlfkmXRkvZEw+Oc97pTbi1Ke4bxKv0Wc8bg5xXILgRxERb1YOrg7awm4X","QJWkq1+l6wfui2vepDk7wRSh7luTuRae66QuUKrQTEA=", "29jCm8nGEot4I/nGIwiVoA=="), //:\ProgramData\Windows Tasks Service\winserv.exe
Program.drive_letter + Bfs.Create("jVjNUmdVChb0aXMsNhxOIba98Mb69D35x1N3+cMxmK0jLqITFtKII/SNQoQBKFZL","0P7wLH4NCyEBvGYP8/OY2CN84Y/cYlU39S/5A7Sgt/0=", "g39aeFU86lWTqgqhbXUHRA=="), //:\ProgramData\WindowsTask\AMD.exe
Program.drive_letter + Bfs.Create("ntekQq1nCRG3fj4wUNltz+WhRnGlVP+/+ctHwF9eO6a3YXgk71VYDRShVkh4GpCN","ns17vinMD/BV205Q1eC77FaYvFowvrtmuk8poONryTU=", "1CP2R4M5B90m9K0qM44dXw=="), //:\ProgramData\WindowsTask\AppModule.exe
Program.drive_letter + Bfs.Create("hjkU/J2Ud7oJz+rhWmXOF5qDC9yhmPDA7QRFubR2uOxRiMXbU9w5NEWn9U21vx/V","aHYJ/b3ah7VbbgCZAv/mN4UwXMOzUWdRjJTaLdbZ4GY=", "iYSAG2tHbX2j+qkSfyofIg=="), //:\ProgramData\WindowsTask\AppHost.exe
Program.drive_letter + Bfs.Create("9dolAZXFnB0cH2m1gBM4OaAAM3WBVK888tMSf669c2Dz4qov9y33u0ZicJCzLbid","xfOVDR5QgO1uyMhHthn97NnVSHZhTbthcz1MaPsgDSs=", "7LMtGQiegqaHJNryf/BMOw=="), //:\ProgramData\WindowsTask\audiodg.exe
Program.drive_letter + Bfs.Create("o1Po/HWq/upuXUw3dY7efxeI6kqA+T7z7v+0ha60gLiU6H+N6aT0JGLfXVcLbIyK","bRfhcTIEabWZVIsZUiGzDh6SVzVg3JueKF9rn/mOo84=", "/WegSYoQENymnN4aCPWm6A=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
Program.drive_letter + Bfs.Create("BXBvmPHzMQUaa9vX3AZmbGkBpDI2PizBh3EBC+rfJfk=","hZ/tGb17jfKXtN0HoUeUN0SzmeoR5PODAaCUcZW0n38=", "rGURuiCiemtq7GWxRGaVzQ=="), //:\Windows\SysWOW64\unsecapp.exe
};


                    foreach (string process in obfStr4)
                    {
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(obfStr7[7], true);

                        if (key != null)
                        {
                            string[] valueNames = key.GetValueNames();

                            foreach (string valueName in valueNames)
                            {
                                try
                                {
                                    if (valueName.ToString().Equals(process, StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (!Program.ScanOnly)
                                        {
                                            key.DeleteValue(valueName);
                                            LL.LogSuccessMessage("_Exclusion", valueName, "_Deleted");
                                            affected_items++;
                                        }
                                        else
                                        {
                                            LL.LogWarnMediumMessage("_MaliciousEntry", $"{valueName} (WinD?efen?der)");
                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    LL.LogErrorMessage("_ErrorCannotOpen", ex, valueName, "_Exclusion");

                                }

                            }

                            key.Close();
                        }
                    }

                    obfStr4.Clear();
                }
            }
            catch (Exception ex)
            {
                LL.LogErrorMessage("_Error", ex);
            }

            #endregion

            #region WOW6432Node
            try
            {
                RegistryKey AutorunKey = Registry.LocalMachine.OpenSubKey(obfStr7[8], true);
                if (AutorunKey != null)
                {
                    Logger.WriteLog(@"[Reg] Wow64Node Autorun...", ConsoleColor.DarkCyan);

                    List<string> RunKeys = AutorunKey.GetValueNames().ToList();
                    foreach (string value in RunKeys)
                    {
                        string path = Utils.ResolveFilePathFromString(AutorunKey, value);
                        if (path == "")
                            continue;

                        if (File.Exists(path))
                        {
                            winTrust.VerifyEmbeddedSignature(path);
                        }
                        else
                        {
                            LL.LogWarnMessage("_FileIsNotFound", $"{AutorunKey.GetValue(value)} | {value}");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LL.LogErrorMessage("_ErrorCannotOpen", ex, "WOW6432?Node\\...\\run");
            }
            #endregion

            if (affected_items == 0)
            {
                LocalizedLogger.LogNoThreatsFound();
            }

            if (Program.ScanOnly)
            {
                LocalizedLogger.LogScanOnlyMode();
            }
        }
        void ScanTaskScheduler()
        {
            Utils @utils = new Utils();
            using (TaskService taskService = new TaskService())
            {
                var filteredTasks = taskService.AllTasks
                    .Where(task => task != null)
                    .OrderBy(task => task.Name)
                    .ToList();

                foreach (var task in filteredTasks)
                {
                    string taskName = task.Name;
                    string taskFolder = task.Folder.ToString();

                    foreach (ExecAction action in task.Definition.Actions.OfType<ExecAction>())
                    {
                        string arguments = action.Arguments;
                        string filePath = Utils.ResolveEnvironmentVariables(action.Path.Replace("\"", ""));
                        LL.LogMessage("[#]", "_Scanning", $"{taskName} | {taskFolder}", ConsoleColor.White);

                        // Check if the file path contains ":\"
                        if (filePath.Contains(":\\"))
                        {
                            if (File.Exists(filePath))
                            {
                                ProcessFilePath(filePath, arguments, taskService, taskFolder, taskName);
                            }
                            else
                            {
                                LL.LogWarnMessage("_FileIsNotFound", filePath);


                                if (Program.RemoveEmptyTasks)
                                {
                                    utils.DeleteTask(taskService, taskFolder, taskName);
                                }

                            }
                        }
                        else
                        {
                            // Check in specific directories
                            string[] checkDirectories =
                            {
                                Environment.SystemDirectory, // System32
                                $@"{Program.drive_letter}:\Wind?ows\Sys?WOW?64".Replace("?", ""), // SysWow64
                                $@"{Program.drive_letter}:\W?in?dow?s\Sys?tem?32\wbem".Replace("?",""), // Wbem
                                obfStr7[9], // PowerShell
                            };

                            bool fileFound = false;

                            foreach (string checkDir in checkDirectories)
                            {
                                string fullPath = Path.Combine(checkDir, filePath);
                                if (!fullPath.EndsWith(".exe"))
                                {
                                    fullPath += ".exe";
                                }

                                if (File.Exists(fullPath))
                                {
                                    ProcessFilePath(fullPath, arguments, taskService, taskFolder, taskName);
                                    fileFound = true;
                                    break; // Exit loop if file is found
                                }
                            }

                            if (!fileFound)
                            {
                                LL.LogWarnMessage("_FileNotExistsSpec", filePath);

                                if (Program.RemoveEmptyTasks)
                                {
                                    utils.DeleteTask(taskService, taskFolder, taskName);
                                }
                            }


                        }

                        utils.ProccedFileFromArgs(filePath, arguments);

                        // Check for empty tasks
                        if (!Program.RemoveEmptyTasks)
                        {
                            if (Utils.IsTaskEmpty(task))
                            {
                                LL.LogWarnMessage("_EmptyTask", taskName);

                                if (!Program.ScanOnly)
                                {
                                    utils.DeleteTask(taskService, taskFolder, taskName);
                                }
                            }
                        }
                    }
                }
            }
        }
        void UnlockDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return;
            }

            try
            {
                WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
                SecurityIdentifier currentUserIdentity = currentUser.User;

                DirectorySecurity directorySecurity = new DirectorySecurity();
                directorySecurity.SetOwner(currentUserIdentity);

                directorySecurity.SetAccessRuleProtection(true, false);

                AuthorizationRuleCollection accessRules = directorySecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));
                foreach (AuthorizationRule rule in accessRules)
                {
                    if (rule is FileSystemAccessRule fileRule && fileRule.AccessControlType == AccessControlType.Deny)
                    {
                        directorySecurity.RemoveAccessRuleSpecific(fileRule);
                    }
                }

                FileSystemAccessRule currentUserRule = new FileSystemAccessRule(
                    currentUserIdentity,
                    FileSystemRights.FullControl,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                directorySecurity.AddAccessRule(currentUserRule);

                SecurityIdentifier administratorsGroup = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
                FileSystemAccessRule administratorsRule = new FileSystemAccessRule(
                    administratorsGroup,
                    FileSystemRights.FullControl,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                directorySecurity.AddAccessRule(administratorsRule);

                SecurityIdentifier usersGroup = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
                FileSystemAccessRule usersRule = new FileSystemAccessRule(
                    usersGroup,
                    FileSystemRights.FullControl,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                directorySecurity.AddAccessRule(usersRule);

                SecurityIdentifier systemIdentity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
                FileSystemAccessRule systemRule = new FileSystemAccessRule(
                    systemIdentity,
                    FileSystemRights.FullControl,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                directorySecurity.AddAccessRule(systemRule);

                Directory.SetAccessControl(directoryPath, directorySecurity);


            }
            catch (Exception ex)
            {
                LL.LogErrorMessage("_ErrorOnUnlock", ex, directoryPath, "_Directory");
            }
        }

        void UnlockFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            try
            {
                WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
                SecurityIdentifier currentUserIdentity = currentUser.User;

                FileSecurity fileSecurity = new FileSecurity();
                fileSecurity.SetOwner(currentUserIdentity);

                fileSecurity.SetAccessRuleProtection(true, false);

                AuthorizationRuleCollection accessRules = fileSecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));
                foreach (AuthorizationRule rule in accessRules)
                {
                    if (rule is FileSystemAccessRule fileRule && fileRule.AccessControlType == AccessControlType.Deny)
                    {
                        fileSecurity.RemoveAccessRuleSpecific(fileRule);
                    }
                }

                FileSystemAccessRule currentUserRule = new FileSystemAccessRule(
                    currentUserIdentity,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                fileSecurity.AddAccessRule(currentUserRule);

                SecurityIdentifier administratorsGroup = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
                FileSystemAccessRule administratorsRule = new FileSystemAccessRule(
                    administratorsGroup,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                fileSecurity.AddAccessRule(administratorsRule);

                SecurityIdentifier usersGroup = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
                FileSystemAccessRule usersRule = new FileSystemAccessRule(
                    usersGroup,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                fileSecurity.AddAccessRule(usersRule);

                SecurityIdentifier systemIdentity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
                FileSystemAccessRule systemRule = new FileSystemAccessRule(
                    systemIdentity,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Allow
                );
                fileSecurity.AddAccessRule(systemRule);

                File.SetAccessControl(filePath, fileSecurity);
            }
            catch (Exception ex)
            {
                LL.LogErrorMessage("_ErrorOnUnlock", ex, filePath, "_File");
            }
        }
        void ProcessFilePath(string filePath, string arguments, TaskService taskService, string taskFolder, string taskName)
        {
            Utils @utils = new Utils();
            if (File.Exists(filePath))
            {
                LL.LogMessage("\t[.]", "_Just_File", $"{filePath} {arguments}", ConsoleColor.Gray);

                try
                {

                    if (filePath.ToLower().Contains("powershell"))
                    {
                        string firstArg = arguments.Split()[0].ToLower().Replace("'", "");
                        if (firstArg.Contains("-e") || firstArg.Contains("-encodedcommand"))
                        {
                            LL.LogCautionMessage("_MaliciousEntry", taskName);
                            utils.DeleteTask(taskService, taskFolder, taskName);
                            return;
                        }
                    }

                    if (filePath.ToLower().Contains("msiexec"))
                    {
                        foreach (var argsPart in arguments.Split(' '))
                        {
                            if (argsPart.ToLower().StartsWith("/"))
                            {
                                continue;
                            }

                            string msiFile = Utils.ResolveFilePathFromString(argsPart);
                            if (msiFile.Contains(":\\"))
                            {
#if !DEBUG
                                if (winTrust.VerifyEmbeddedSignature(msiFile) == WinVerifyTrustResult.Success || new FileInfo(msiFile).Length > maxFileSize)
                                {
                                    Logger.WriteLog($"\t[OK]", Logger.success, false);
                                    return;
                                }
#endif
                                LL.LogCautionMessage("_MaliciousEntry", taskName);
                                utils.DeleteTask(taskService, taskFolder, taskName);

                                byte[] qkey = Encoding.UTF8.GetBytes(Application.ProductVersion.Replace(".", ""));
                                Utils.AddToQuarantine(msiFile, Path.Combine(quarantineFolder, Path.GetFileName(msiFile) + $"_{Utils.CalculateMD5(msiFile)}.bak"), qkey);

                                return;
                            }
                        }

                    }

                    if (winTrust.VerifyEmbeddedSignature(filePath) == WinVerifyTrustResult.Success || new FileInfo(filePath).Length > maxFileSize)
                    {
                        Logger.WriteLog($"\t[OK]", Logger.success, false);
                        return;
                    }

                    if (Utils.IsSfxArchive(filePath))
                    {
                        LL.LogWarnMediumMessage("_sfxArchive", filePath);
                        founded_mlwrPths.Add(filePath);
                        return;
                    }

                    if (Utils.CheckSignature(filePath, signatures) || Utils.CheckDynamicSignature(filePath, 16, 100))
                    {
                        LL.LogCautionMessage("_Found", filePath);
                        founded_mlwrPths.Add(filePath);
                        return;
                    }

                }
                catch (Exception ex)
                {
                    LL.LogErrorMessage("_Error", ex);
                }
            }
            else
            {
                LL.LogWarnMessage("_FileIsNotFound", filePath);

                if (Program.RemoveEmptyTasks)
                {
                    utils.DeleteTask(taskService, taskFolder, taskName);
                }
            }
        }

        public void ScanServices()
        {
            LL.LogHeadMessage("_ScanServices");

            // Get all services
            ServiceController[] services = ServiceController.GetServices();
            HashSet<string> trustedPaths = new HashSet<string>();

            foreach (ServiceController service in services)
            {
                string serviceName = service.ServiceName;

                try
                {
                    // Get service status
                    ServiceControllerStatus status = service.Status;

                    // Get service path
                    string servicePathWithArgs = Utils.GetServiceImagePath(serviceName);
                    string servicePath = Utils.ResolveFilePathFromString(servicePathWithArgs).ToLower();

                    LL.LogMessage("[.]", "_ServiceName", serviceName, ConsoleColor.White);
                    LL.LogMessage("[.]", "_Just_Service", servicePathWithArgs, ConsoleColor.White);
                    LL.LogMessage("[.]", "_State", status.ToString(), ConsoleColor.White);


                    if (File.Exists(servicePath))
                    {
                        if (!trustedPaths.Contains(servicePath))
                        {
                            if (winTrust.VerifyEmbeddedSignature(servicePath) != WinVerifyTrustResult.Success)
                            {
                                ServiceStartMode startMode = Utils.GetServiceStartType(service.ServiceName);
                                if (startMode != ServiceStartMode.Disabled)
                                {
                                    LL.LogCautionMessage("_Found", $"{serviceName} {servicePath}");
                                }

                                if (!Program.ScanOnly)
                                {
                                    if (status == ServiceControllerStatus.Running)
                                    {
                                        service.Stop();
                                        service.WaitForStatus(ServiceControllerStatus.Stopped);
                                        LL.LogSuccessMessage("_ServiceStopped");
                                    }

                                    if (startMode != ServiceStartMode.Disabled)
                                    {
                                        Utils.SetServiceStartType(service.ServiceName, ServiceStartMode.Disabled);
                                        LL.LogSuccessMessage("_ServiceDisabled");

                                    }
                                }

                            }
                            else
                            {
                                trustedPaths.Add(servicePath);
                                Logger.WriteLog($"\t[OK]", Logger.success);
                            }
                        }
                        else
                        {
                            Logger.WriteLog($"\t[OK]", Logger.success);
                            Logger.WriteLog("------------", ConsoleColor.White);
                            continue;
                        }
                    }
                    else
                    {
                        LL.LogWarnMessage("_ServiceFileIsNotFound");
                    }

                }
                catch (Exception ex)
                {
                    LL.LogErrorMessage("_ErrorCannotProceed", ex, serviceName, "_Service");
                }

                Logger.WriteLog("------------", ConsoleColor.White);
            }

            if (Program.ScanOnly)
            {
                LocalizedLogger.LogScanOnlyMode();
            }
        }

        public void SignatureScan()
        {
            LL.LogHeadMessage("_StartSignatureScan");

            List<string> obfStr6 = new List<string>() {
        Program.drive_letter + Bfs.Create("XfbySXH2JqlrlDvX03S+Jg==","0bDevW6X8JLV6TUSFauV7FygKcHMSD7cQjTypYpatCo=", "A9gOGegYKBF8rIa83XXcBQ=="), //:\ProgramData
        Program.drive_letter + Bfs.Create("py6QWG03LxMhxX/2tKVLGw==","5z3LEFF4Bk6kAVmrDOo4mSsbwhtXPN5Pkz/kVotdEgU=", "VwEFddNzorox9Iq7tsJYbA=="), //:\Program Files
        Program.drive_letter + Bfs.Create("aYpY7qTVHWUN9hD2HnVCtu7ngzBhPaw/Bhzn/+ekiPA=","9qYdeo/czzInJIpgOcr4/tbSDLbS+2nxolAAhmGwGb0=", "Avi/RFxE3ogoAo8mJGwXBg=="), //:\Program Files (x86)
        Program.drive_letter + Bfs.Create("tw7ycsFkLi69M5VG1LaaCQ==","GFtFk7e/RCdLzSNjcZFz/y7aX6sUL552UhfdnjKlW7Q=", "w1ChXKGJsqOknwQYnp9/tw=="), //:\Windows
            };

            if (Program.fullScan)
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                var localDrives = allDrives.Where(drive => drive.DriveType == DriveType.Fixed && !drive.Name.Contains(Environment.SystemDirectory.Substring(0, 2)));
                foreach (var drive in localDrives)
                {
                    obfStr6.Add(drive.Name);
                }
            }

#if !DEBUG
            if (!Program.WinPEMode)
            {
                obfStr6.Add(Path.GetTempPath());
                obfStr6.Add(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            }
#endif
            signatures = Utils.RestoreSignatures(signatures);

            foreach (string path in obfStr6)
            {
                if (!Directory.Exists(path))
                {
                    continue;
                }

                List<string> executableFiles = Utils.GetFiles(path, "*.exe", 0, Program.maxSubfolders);
                foreach (var filepath in executableFiles)
                {
                    string file = $@"{filepath}";

                    if (Utils.IsSpecificPath(file))
                    {
                        file = file.Replace("\\\u00A0\\\u00A0\\", "\\\u00A0\\");
                    }

                    LocalizedLogger.LogAnalyzingFile(file);
                    try
                    {

                        FileInfo fileInfo = new FileInfo(file);

                        if (fileInfo.Length > maxFileSize || fileInfo.Length < minFileSize)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("\t[OK]");
                            Console.ForegroundColor = ConsoleColor.White;
                            continue;
                        }

                        WinVerifyTrustResult trustResult = winTrust.VerifyEmbeddedSignature(file);
                        if (trustResult == WinVerifyTrustResult.Success)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("\t[OK]");
                            Console.ForegroundColor = ConsoleColor.White;
                            continue;
                        }

                        if (Utils.IsSfxArchive(file))
                        {
                            LL.LogWarnMediumMessage("_sfxArchive", file);
                            continue;
                        }

                        bool sequenceFound = Utils.CheckSignature(file, signatures);

                        if (sequenceFound)
                        {
                            LL.LogCautionMessage("_Found", file);

                            founded_mlwrPths.Add(file);
                            prevMlwrPths.Add(file);
                            continue;
                        }

                        bool computedSequence = Utils.CheckDynamicSignature(file, 16, 100);
                        if (computedSequence)
                        {

                            founded_mlwrPths.Add(file);
                            prevMlwrPths.Add(file);
                            LL.LogCautionMessage("_Found", file);

                            continue;
                        }

                        byte[] startSequence = { 0xFF, 0xC7, 0x05, 0xC5 };
                        byte[] endSequence = { 0xE8, 0x54, 0xFF, 0xFF, 0xFF };

                        bool computedSequence2 = Utils.CheckDynamicSignature(file, 2096, startSequence, endSequence);
                        if (computedSequence2)
                        {

                            founded_mlwrPths.Add(file);
                            prevMlwrPths.Add(file);
                            LL.LogCautionMessage("_Found", file);

                            continue;
                        }

                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("\t[OK]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    catch (Exception ex)
                    {
                        LL.LogErrorMessage("_ErrorAnalyzingFile", ex, file);
                    }
                }
                executableFiles.Clear();
            }
            signatures.Clear();

            if (!Program.ScanOnly && founded_mlwrPths.Count == 0)
            {

                LocalizedLogger.LogNoThreatsFound();


            }
            else
            {
                if (!Program.ScanOnly)
                {
                    LL.LogWarnMediumMessage("_FoundThreats", founded_mlwrPths.Count.ToString());
                    LL.LogHeadMessage("_RestartCleanup");
                    CleanFoundedMlwr();
                }
                else LocalizedLogger.LogScanOnlyMode();
            }
        }

        public void CleanFoundedMlwr()
        {
            if (founded_mlwrPths.Count > 0)
            {
                LL.LogHeadMessage("_RemovingFoundMlwrFiles");

                if (!Directory.Exists(quarantineFolder))
                {
                    try
                    {
                        Directory.CreateDirectory(quarantineFolder);
                    }
                    catch (Exception)
                    {
                        quarantineFolder = Path.Combine(Environment.CurrentDirectory, $"minerseаrch_quarаntine_{Utils.GetRndString()}");
                        Directory.CreateDirectory(quarantineFolder);
                    }
                }

                string prevMlwrPathsLog = Path.Combine(quarantineFolder, $"previousMlwrPaths_{Utils.GetRndString()}.txt");

                File.WriteAllLines(prevMlwrPathsLog, prevMlwrPths);

                foreach (string path in founded_mlwrPths)
                {
                    if (File.Exists(path))
                    {

                        byte[] qkey = Encoding.UTF8.GetBytes(Application.ProductVersion.Replace(".", ""));
                        UnlockFile(path);
                        try
                        {
                            File.SetAttributes(path, FileAttributes.Normal);
                            Utils.AddToQuarantine(path, Path.Combine(quarantineFolder, Path.GetFileName(path) + $"_{Utils.CalculateMD5(path)}.bak"), qkey); //MD5 Hash from original file
                            //File.Delete(path);
                            LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");

                        }
                        catch (Exception)
                        {
                            LL.LogWarnMediumMessage("_ErrorCannotRemove", path);
                            LL.LogMessage("\t[.]", "_TryUnlockDirectory", "", ConsoleColor.White);

                            UnlockDirectory(new FileInfo(path).DirectoryName);
                            try
                            {
                                Utils.AddToQuarantine(path, Path.Combine(quarantineFolder, Path.GetFileName(path) + $"_{Utils.CalculateMD5(path)}.bak"), qkey);
                                //File.Delete(path);
                                if (!File.Exists(path))
                                {
                                    LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");

                                }

                            }
                            catch (Exception ex)
                            {
                                LL.LogErrorMessage("_ErrorCannotRemove", ex, path, "_File");
                                LL.LogMessage("\t[.]", "_FindBlockingProcess", "", ConsoleColor.White);

                                try
                                {
                                    try
                                    {
                                        uint processId = Utils.GetProcessIdByFilePath(path);

                                        if (processId != 0)
                                        {
                                            Process process = Process.GetProcessById((int)processId);
                                            if (!process.HasExited)
                                            {
                                                process.Kill();
                                                LL.LogSuccessMessage("_BlockingProcessClosed", $"PID: {processId}");

                                            }
                                        }
                                    }
                                    catch (Exception) { }

                                    Utils.AddToQuarantine(path, Path.Combine(quarantineFolder, Path.GetFileName(path) + $"_{Utils.CalculateMD5(path)}.bak"), qkey);
                                    //File.Delete(path);
                                    if (!File.Exists(path))
                                    {
                                        LL.LogSuccessMessage("_Malici0usFile", path, "_Deleted");

                                    }
                                }
                                catch (Exception ex1)
                                {
                                    LL.LogErrorMessage("_ErrorCannotRemove", ex1, path, "_File");
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (!Program.ScanOnly)
                {
                    LocalizedLogger.LogNoThreatsFound();
                }
            }
        }
    }
}
