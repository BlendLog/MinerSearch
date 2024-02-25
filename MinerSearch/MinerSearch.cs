//#define BETA

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
Program.drive_letter + Bfs.Create("ul05f6cL8bYN05rFo+WAfMnyWxSdRZC5H2yOOwk78F8=","yYJNsNG7YdspD7kYttKj9Ep5v+gRQmE05YlCjo4Ab0c=", "svUudgx8e1ImyePgZhmC0Q=="), //:\ProgramData\Microsoft\win.exe
Program.drive_letter + Bfs.Create("8lJOYOJ1jXTnSCLIf0P8dTyYKWiWki71KVpCw+L6pFXl1y4lGCkx9nQ64gDwVqDO","87ua76Y8ULk4dd2Vvss2YTcLBj1auvDlJ4v+eGgBK20=", "d7ed4sn9m1VwZTrP8Me7qA=="), //:\Program Files\Google\Chrome\updater.exe
Program.drive_letter + Bfs.Create("DnyehNfVry1u3QE95+kNXsaLxFcDnMWzaJT6ABqqbnTmHAL5Pf+r3i1UZazj9SER","Uq4EaLpRcV7NbBDFF4oCK4dadTKJLt0Sp0JGmetZNDw=", "MM1aDUG87HSLro2DQnod1w=="), //:\ProgramData\Google\Chrome\updater.exe
Program.drive_letter + Bfs.Create("dDUlKjP4C1Uh+bLkuxj0ARYw/bdQmoACZNm0zNnQQp0=","VKuYWA8afLCcDJ36RJ8vwvgtZrjJ68a6nWUmpK0Ub84=", "oPaeTEQjipXXhAGfKqK67w=="), //:\ProgramData\RDPWinst.exe
Program.drive_letter + Bfs.Create("IOZgo6xTimZScuEusYqjWFpOqsGASm9LLQvG/psw5gWUEAYHQwFopinl6q8gzRkE","P0pHEvynCcXisH1FUrxV7W2G8HDWxOJPNCy76+HsXQU=", "j4jdr/DwPsUbOFbEu+PePA=="), //:\ProgramData\ReaItekHD\taskhost.exe
Program.drive_letter + Bfs.Create("Fm5dhZj7Py74dapgnJm35i7WfLJ/3iJBx39fofh2cSk5bv7h+U7+nF0PltXEpSfc","4htAOTI/ZIIUckEx+JdATQgptGkJwz0QBdp2ckSGrDc=", "O6OpgoprZWGn+PkGSYcVGA=="), //:\ProgramData\ReaItekHD\taskhostw.exe
Program.drive_letter + Bfs.Create("T3+FNZ7lYvTcVI43aaKdfm9hoS0bLXvBGY0IlktExOZO+GbbqgY51ymGO2LeioAB","GWSxiCNDYdQQY9ZNdXdwnxEVYxdf3IZ+uJW3WNnnmuI=", "C18Jaop3Z3MKT7Lq3Pn0vQ=="), //:\ProgramData\RealtekHD\taskhost.exe
Program.drive_letter + Bfs.Create("2ikOWWQUgc39mK4Y0op50rv8GIO+g19h+WXCKLul+GRhyGreLrnWrc9geUrEKhrI","BfoEPn9cD+4qTFCsrrd0n6phTnWgfw92v4A9kfGCgQY=", "mIGocZoYdadHDX0xSm2Bdg=="), //:\ProgramData\RealtekHD\taskhostw.exe
Program.drive_letter + Bfs.Create("WIxyEpswnyGhFSjPrfNyA6sUYUqxXtXEzhglJkFSy7sSuwunpPzegniogOi3fZ8K","uJj39cWXqJr8BWuYU2+T+erkyedTCNaCE8MZcGNPc+c=", "nN2bSMn/5boCafo7ADncFg=="), //:\ProgramData\Windows Tasks Service\winserv.exe
Program.drive_letter + Bfs.Create("/Ur1OP5WVenKim+agF76JkTmMQOON2hx6tN7hnfhndlDRtMdF2uk19nAOoMOgnbs","nzeHnCz504dbZ5ZMhz9Iw9NKQGDkvXgLc+DgZrjPO80=", "wV4Az/YHcHvxePdApCdZdg=="), //:\ProgramData\WindowsTask\AMD.exe
Program.drive_letter + Bfs.Create("jGriHD4jnk7Gdx35YRdJLgSMCATQWnrJ4W9TxpOwkDl6nFyYzpH/ZuM0FM5UqTF1","NgzVkx/a2l8IsoXqRUh8n38opeu6YNxqZnfGVn2B/I0=", "TuMoAZ9zzAX44CPKnEziMQ=="), //:\ProgramData\WindowsTask\AppModule.exe
Program.drive_letter + Bfs.Create("FXL5HaSnbnltO3+lhG+TdX18C6puzrun/FwhUBfXccGs+ykOvPpoBMVhbAGGHWoU","5syG2Foqm/rZ9jRSQmA/ZBF49NSHCgqMXlh99DHBOMo=", "y2jNOtBLockts/j+TH2KcQ=="), //:\ProgramData\WindowsTask\audiodg.exe
Program.drive_letter + Bfs.Create("NYButwYakBf/iuJax2JnxednzUjsr6qjWccLxJhdRMFnDBGCRjCKteV9Q+w3Mo6I","j1GwZzqAkkk1s/qk8b8s9QvTGpZ2XULm/xdkYqgJyeU=", "NEygBG+qV7R4N+Xe8SKEMA=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
Program.drive_letter + Bfs.Create("Lw0gbN+ePXNia6aQ8POe8PgEIblYqD6vxOFeLktJURI=","W1P0jGm2VPcbI7BZuIN4IjMhrO9Q0dnqp3fuGYNcCsk=", "NhGjsn3fSu6YuTtlqHuc3w=="), //:\Windows\SysWOW64\unsecapp.exe
Program.drive_letter + Bfs.Create("WvNT2ihWs0DmZC3r6w4mg48Khb51yIp6PzQWeGRehkyZKlGCJxd4/FviNpgjcAA7","vqoYiqApb9zSyy8YHX52qye5lMpLVX1uX1Pp65w5KZo=", "ZvFmbTes5IBOyN408WIjkA=="), //:\ProgramData\Timeupper\HVPIO.exe
};


        string[] obfStr7 = new string[] {
Bfs.Create("t7jDg8y1/FrJ8AuPKdlyjhIyuGADez6V+dz3FISxZD3YJd35S4MKWtJ1GdIdpsEnViVCyym4OllszpI1NcLorQ==","izar4DUItn4L1RFK+Nk6oWIuZG+T0jeUtsgt/x5NjHw=", "7jrKkR8Fomu5EPzvXV9kHg=="), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
Bfs.Create("Jsq0OeOVJepGZr1FH/T8LvQqsbuf9+M8A/QeldgPfZlAD2gdZNpT2s85NIdK39tqnF8yl351noSlOiPhrvNVGw==","3GtrQwBMom9PNr/v238qTILpgtlrf6W4ueAgAcci0qo=", "Pkrm2OwcjiFrS5JYj/+ALg=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
Bfs.Create("sBiTG8VDQ8yj7+KBh1nW3PeuXewNF77k+YOpHWx47AeplxBIisssN9Bg+0nNlI2sCfwaVNZx9H4VI3ObrxUN3YX0rsGYac4eeLqHCHuAPRQ=","1Ie3hAmXZPzTk8FAhuS+ekHLrB29MHCYWSvFl4yEtss=", "QJIbEIQxwAF8DUbTYFgpSg=="), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
Bfs.Create("eiPCRGnmkc1I5D2THPWGRrq0rnTlJYkpAI2U+8K3krMCKbmqS/kUZq+kr/Zd9lHEDDumChhfUKuwULMs0xQOkg==","vKMDJkO5PCROHJ3/SCWPZxUqSDak9iEcqi3XMMXJbIA=", "p3UQZNm8Yd0M04qvSV5KCA=="), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
Bfs.Create("pGD7urvw+45SdQ7IlfXYy6VuNxvqhNsOIQtkfnDx0QdXzqaDOhMPDkupJvwZXKfhzoFTdwJ7OHBTgr7QxP/dfg==","94czuFjMy8GD8WtlVZpRDYSEo8lDh/RD1ldtVo0AaFQ=", "Oax4hCvKCBFdLQgzntJXAA=="), //[!] Cannot open HKLM\...\CurrentVersion\Windows:
Bfs.Create("5rTKl9NL4S1IiOZZlFGKe61SdyCIY4+sS+9jpV8ffA79O5LNr0I+iLrhFO0REHgS","TzHGo76SGJaAP3eWx+JP/ljySBhrqGDRpouF32sLX3k=", "qR7gscQ0ilUx1dkI4XrW4Q=="), //Software\Microsoft\Windows\CurrentVersion\Run
Bfs.Create("1HL2TQjQJh1TEMW2paa/ztHBbc22+QCfYo4uOT+hTKByGtCUOHamCTUkhxkeVaCnnqydvuYPtHWZAnY/j9/crA==","b0H+g6El8S4Y4taC8cUuhuJaU0PjeaO83Xqbuftccuk=", "GlQlF3pxlD+v1FWwunHEZg=="), //HKLM\Software\Policies\Microsoft\Windows Defender\Exclusions
Bfs.Create("swgc2YIog233TYJ+xvnA6Zpeb+j0Am4xBr2FMZ85WHVgO6X+NblUwnD50tIvvxjtNwNw1O5TfTpmv6SxJ6ySfg==","tkuHrCP+3rLy1rhEd+QHCZ1DbWjpdVmFqQfSzn/tndI=", "hk2ObyEbvZGPe5dQ7mbGUw=="), //Software\Policies\Microsoft\Windows Defender\Exclusions
Bfs.Create("RiSpPe4DazKuZ+mTRWqxOLY4HRKSUDlchkaiTJU5C/0hjBv9d+PPR2mVFw0ZUVnSQqPbmsJQrP/SqKFHXnlpuA==","IEL9XJH5dHpwffR50VSJua6VBBdxrsbJn7Ebw0gRxFE=", "0/BhOVwrYAp5hTn29IyMew=="), //Software\Policies\Microsoft\Windows Defender\Exclusions\Paths
Bfs.Create("20OFWLU+efGHqKhYN4+rtS1rWt/Z3EsWWVekLhXzaxpiuXPWN9Rm2XJlXKivCQfnMvCRUTtq15A2k6/j6UA0F+n286rBsjgaN17+FEdKAP4=","Ew7A+8V0RZlDl0TaGPgDOse7vDyDuNN4e15dakDgVFw=", "EB0oBfDmeP5OjIqFpV4MjA=="), //Software\Policies\Microsoft\Windows Defender\Exclusions\Processes
Bfs.Create("H24Gt2zPE8/Rl98kr1abqOmVVaxcf6tieHLZFPO2hvvnl0mCS0tirapNtH9xHZ5v83sEwGbZQborOXtK+zPIfw==","qnNs+wGvRDaOAtvP+TS+WNHWp5ehO7pTMFQgz/pDuOw=", "PbNXbRsdy/nXyp2TQr5Jag=="), //[!] Cannot open HKLM\...\Windows Defender\Exclusions:
Bfs.Create("ssL/iOwXqkOFbt5c9fNXPp4A0r8G7amsmbknrCayh9PN4UaRlAzXkpuzAOGb8RAYN3kJvs99KgKsO5xG7fwerw==","ofbHWA8q7bQp3Qa0Fqs1bXBtbckm7riAwbjCc1XAWMA=", "ISFJTlDVQPDX1WEoWLU/ZA=="), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
Bfs.Create("YzJle+32daC0a474LB6OyrGiJ89w4mOjBLQBhlEVOLXdwvBfJEVwW3s5qa/2c4My","Kh8NENCpnhUNEGaffgfRjFp5cjd6ohNX5BV6IVdhzXI=", "O5xxaNvsL6KiXq8oHvzQ3g=="), //:\Windows\System32\WindowsPowerShell\v1.0
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

        List<byte[]> signatures = new List<byte[]> //signatures
                {
                    new byte[] {0x67, 0x33, 0x71, 0x70, 0x70, 0x6D },
                    new byte[] {0x33, 0x6E, 0x6A, 0x6F, 0x66, 0x73, 0x74 },
                    new byte[] {0x6F, 0x6A, 0x64, 0x66, 0x69, 0x62, 0x74, 0x69 },
                    new byte[] {0x75, 0x66, 0x6C, 0x75, 0x70, 0x6F, 0x6A, 0x75 },
                    new byte[] {0x2F, 0x75, 0x69, 0x66, 0x6E, 0x6A, 0x65, 0x62 },
                    new byte[] {0x74, 0x75, 0x73, 0x62, 0x75, 0x76, 0x6E, 0x2C },
                    new byte[] {0x60, 0x73, 0x62, 0x6F, 0x65, 0x70, 0x6E, 0x79, 0x60 },
                    new byte[] {0x46, 0x75, 0x66, 0x73, 0x6F, 0x62, 0x6D, 0x63, 0x6D, 0x76, 0x66 },
                    new byte[] {0x67, 0x6D, 0x7A, 0x71, 0x70, 0x70, 0x6D, 0x2F, 0x70, 0x73, 0x68 },
                    new byte[] {0x6F, 0x62, 0x6F, 0x70, 0x71, 0x70, 0x70, 0x6D, 0x2F, 0x70, 0x73, 0x68 },
                    new byte[] {0x54, 0x69, 0x66, 0x6D, 0x6D, 0x64, 0x70, 0x65, 0x66, 0x47, 0x6A, 0x6D, 0x66 },
                    new byte[] {0x42, 0x6D, 0x68, 0x70, 0x73, 0x6A, 0x75, 0x69, 0x6E, 0x41, 0x79, 0x6E, 0x73, 0x6A, 0x68 },
                    new byte[] {0x45, 0x70, 0x76, 0x63, 0x6D, 0x66, 0x51, 0x76, 0x6D, 0x74, 0x62, 0x73, 0x51, 0x73, 0x66, 0x74, 0x66, 0x6F, 0x75 }
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
            12800 //tcpsvcs
        };
        long maxFileSize = 100 * 1024 * 1024;

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
                    byte[] allBytes = Bfs.Decrypt(rawdata, "fswzACS2imMkaixjaySj6w==", "Hw0NNW2dnnSZ+QGTEOIQFQ==");

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
Bfs.Create("cQDZOGDMHdiaWfYnlYO2AA==","rPr1zRDp77Wo/I/azwoOdMcIIrTVrYivgrOFBZfv0XI=", "+Z0EDPjW91qNwFWBObjBFw=="), //audiodg
Bfs.Create("DgmFFG7dcJcoOrfvYdQhOg==","IuTWxruAZTBR0DYvHrBhFXIRagWCSc6NMeRShVwJznU=", "2mic4FIrj/vXXJpw8SKElw=="), //taskhostw
Bfs.Create("Qe/RM2+PaxSIGNPvccYPtA==","Z37mqv6YBse1e8MqVknvXXH1gqPJi4+1naF5m9K0qWM=", "qQ59Cyr25pEGmPw4GZpHjw=="), //taskhost
Bfs.Create("zvx+7+OZCrMEyobTKn7xmQ==","fMUsopHdO3aqDqjk8c1cwK+ZCTDKDie5LCz0E6w7K2g=", "+aCP8IWblM3r0LWGcZKrZA=="), //conhost
Bfs.Create("4n5ksd9c0kzrWQEU2gQ1Hg==","kNtBZQCVpOGpKtR58NmAv/BgqUKvKIo7I5ylQCSgvTw=", "TJmYn+BdZKzfWn/hErUabA=="), //svchost
Bfs.Create("5HP5UzWO9Cw8HAMCtA/LSw==","4CB1f+6Ja2bV9+ZjKqRrXIdskoEuYz8vAC+dBl8ykX4=", "bnj4OsppwnIGZ4DeV31Iyg=="), //dwm
Bfs.Create("rZdy9yjYHsJtDCPhOP23uA==","Iko1RU2lbFVfdYOi4ESrxOfSHHFnbIZfnbZ9NaJU4kE=", "RB3DNNzuZ0ozNQp+NhfCiA=="), //rundll32
Bfs.Create("m1GNWwYvIe8zJQYS1UVptg==","HbabvjOwKhC3b0JEJD9ODDCmMZo3ZvI9YSTqAOJwkP4=", "S5alE8okZD/liNPhMgFXgg=="), //winlogon
Bfs.Create("gQ0n7+ZqhHPfuPI/IvFfSw==","fZyp8yiDf0h2bQUnSYCuoyY0ARJ/biE04xRj8NgsXRg=", "wRoalcx4PS5GqLnWW4FBwg=="), //csrss
Bfs.Create("psOl/TPwa/TYBUqEL/FxHA==","5ankprg+hjaNZ0MRvsoCRLHrB3FMp2sxhlfXyOKlHI0=", "yVmiuLspnAHlUWRCytpMsA=="), //services
Bfs.Create("7ML3L6IUTV3e9D2yor2yPQ==","h0GQHyEm5bIUjHISDDnzAJJ4AzXheGSNKmLzNNSVBBY=", "PsTjgSVU7QGCnWNVyTOHWg=="), //lsass
Bfs.Create("rTO8T6/F/PNxjW974+MrDg==","zzQczZnvFT0bs5ASDagl4IBcYJP2S5/7MLNcDleagv4=", "kE2/bCr3Qf8xiNd1f6a3DQ=="), //dllhost
Bfs.Create("khCRr6tjljcLBrPEMesw1A==","OS3AUW80TO4fG+RnkR9y2g4nXZC3gx7DqSao94Utymo=", "LayskYWe+SOKKmA6ezgmag=="), //smss
Bfs.Create("bSvW18HMwX9ArXilcHV2KQ==","WTcyhv9XX4O2H6apvaqC/kISjvykbP+bKKt+8DggEEQ=", "toQ86+iTvrciUZnRg30ETA=="), //wininit
Bfs.Create("iCEnEKhoUzfaWkabQPbN1Q==","Sw8fx1C5kHE4aCKc5d29iO/eqVqPftNPUgO89gydAZI=", "VPjwWQkszYa4/xtoA/kHgQ=="), //vbc
Bfs.Create("byVNsTd49maR282ClxHLdg==","b48dP8Jpr5Hyu75aWjr0zLixk91ae6Y+vKGO2Urj8bc=", "rLFRyMVDEU+2yiiFJwHUug=="), //unsecapp
Bfs.Create("YfGSmvRdo+fzi4tehgtk0Q==","UFqrW2L0OYj/9EAwEtmyKH8YRocBiHgFzZFZKKCwHGs=", "JwNQZpH5EfODDSTUiKhTdQ=="), //ngen
Bfs.Create("+tehp4+lQIX58uRofvOXvQ==","HwU073FwuOoBo4yt8qg5VbnkUI7WaIlEYokryFyu4F0=", "X3v8bWEOSphwEPG+iX9VBg=="), //dialer
Bfs.Create("pqtM9P/Kgr6bGweATHPOXQ==","dEJWMxakyqxkKAivao+ewk1/NffIf695auk5AdB49kk=", "PLiTygyxxQUo2b1d5wnnvw=="), //tcpsvcs
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

                    if ((processName == obfStr8[3] && !args.Contains("\\??\\c:\\")))
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


                        if ((Utils.GetWindowsVersion().ToLower().Contains("windows 7") && argsLen > 29) || (Utils.GetWindowsVersion().Contains("8 ") && argsLen > 10) || !Utils.GetWindowsVersion().ToLower().Contains("windows 7") && !Utils.GetWindowsVersion().Contains("8 ") && args.Length > 9)
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

                    if (processName == "explorer" && !args.ToLower().Contains(@"c:\windows\explorer.exe"))
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
                            if (!fullPath.Contains("c:\\windows\\system32")
                                && !fullPath.Contains("c:\\windows\\syswow64")
                                && !fullPath.Contains("c:\\windows\\winsxs\\amd64")
                                && !fullPath.Contains("c:\\windows\\microsoft.net\\framework64")
                                && !fullPath.Contains("c:\\windows\\microsoft.net\\framework"))
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
Program.drive_letter + Bfs.Create("M6rJsUxKtIwZQJVToW8sVB4E/D8Sh9jrTl5i8IgjRqs=","wAcmVfzMBYbFVFXLKE5mmwUj7cxevfjGKbRiK9MIr/M=", "rJt6Z4CujIgEt3OUgpI+yQ=="), //:\ProgramData\360safe
Program.drive_letter + Bfs.Create("1RX2t22+Ct06Zy2nj8dRfQVBFS5ZUumI742iT4x8JMQ=","aIrIoCWBcIPJKtvjG1agTQjRIYQZgV90rmsQUMky8UY=", "5w3svgwpYwtUTZiy5DMq3g=="), //:\ProgramData\AVAST Software
Program.drive_letter + Bfs.Create("gr9kQ82LvU4pCmlO7xU4zRTft0h5RYNfjZLEwi3hh8Y=","3HWAFLpuRIvP9TMi/SKLXshuQd53ImqCBLdcZXbnedE=", "1l/Gu+O3JjEzKyscdup8GQ=="), //:\ProgramData\Avira
Program.drive_letter + Bfs.Create("9JZQT3B1BWwim1dMIMghlrABEiZiKCwPTJ3jG2NmR5E=","oA+76cd0TreGI7D1710PFNghMLNkK3eezrn7x0utanY=", "3s+q7fnKYOlAymjlQERb0w=="), //:\ProgramData\BookManager
Program.drive_letter + Bfs.Create("0hHKMeLnVWaAx8oGmzjV4aYH4hjeG/LFsFKbbZ3AsPQ=","2CRAjWr0GIhEuXFi6oujRnCb+zEE/ng7RaNlzpND6lo=", "ZvSWZoeepnnfEeXEIKEJKA=="), //:\ProgramData\Doctor Web
Program.drive_letter + Bfs.Create("GZ7pnb9AuFy8S7PJ20Q452N9RZYt05qPQVqg9sFQ15k=","XyxUoRWENAzDgsM/xkDRYrm0+vpD/KVJw9PgzazrPVE=", "CgOPwUDQql7B75wvp0KDpQ=="), //:\ProgramData\ESET
Program.drive_letter + Bfs.Create("rEd3i6mHzoP+d3V6W4w+MAEMrBUIcSFtZ5D4QvQ7ivY=","KJLQ7aQkA5Om1xNh8Im3P7byvNwD6HKZ0Wm3ip40OJM=", "bPodLQIkkJGnzsagAsX1Bg=="), //:\ProgramData\Evernote
Program.drive_letter + Bfs.Create("FDKnTqxdFok9A7qkVW4u63vFOsKYhXfAb0gMh9hEp7E=","MEcKwdW/2Ny6VeCvy1D35pkNrhzSFDnYwpCy/76z84o=", "VH4AlpwnSa8J0KLWMiLRcQ=="), //:\ProgramData\FingerPrint
Program.drive_letter + Bfs.Create("AiiIGbUUNHFBoGErralVPmkjj/xyaYaNFyc9qK8uBXE=","H7Q2KCc+z3+jDnm6Jr4GGhYOYBuGSjDbCKBEnKj36uc=", "9+cIovhgTiD4An/C6ElHPw=="), //:\ProgramData\Kaspersky Lab
Program.drive_letter + Bfs.Create("mz7Fpesn60o7et+RThyu/2Bs+ZfSNwbpT58mmL9wyz60JbqVWBWM+bXNMAzQRDAe","f9USsDuPs0ZF93rADaxI8eQXTbjKHiLTP9I0UL4lwHQ=", "15eITxQNJQx1I1WJNCTMxA=="), //:\ProgramData\Kaspersky Lab Setup Files
Program.drive_letter + Bfs.Create("kfaVVtT8Tu+SsQEz++YfySXPj7lLRhPE/PUnD7VkzXo=","2rk8muxGh21C/+CGx/Ln0k9YkySCh+ob68PSRoCQ29A=", "88+WRRfZwI6sLcKvz9joBQ=="), //:\ProgramData\MB3Install
Program.drive_letter + Bfs.Create("QBo1OY86Pk1XqcEqxkkGFQasIedcEvljsgPjvJsrv2U=","zHGUxHZhNQSxHKBtjRTsZKZLPSOO9XUm7BBipZ8R/vo=", "+jeMeX+tvH2DyO9JEtnAqQ=="), //:\ProgramData\Malwarebytes
Program.drive_letter + Bfs.Create("9rcliqlL1AViRUZM5GH5n9QDPabPOoNyfo4ETFtOFU8=","YBej9h8BQ/R//lQCQrHuetmvhwCRBV/c7Pm0h3VPFkc=", "gYuIaRl6CQnIMz0n2W+nLQ=="), //:\ProgramData\McAfee
Program.drive_letter + Bfs.Create("XnGBX7qvRiet8Bw/TbSWtSfzCVuC0fIM+ppDEF5YJ8w=","BvzhOkfoYSBXMucgM8C0afhLkAWayJ1gYNha/6/dfZc=", "nqTWoIn69J3/2mqtMLCe1Q=="), //:\ProgramData\Norton
Program.drive_letter + Bfs.Create("h7O3mBZtkV3EnnXQV3WX4sJ4OWuYnKOKLXRolMGb/S8=","228Amy76EyLtJ6u4TlH6hpSHsXb37VGCKRhOQN/fT8A=", "Xq2Pnn8RZl6ddXEKqt5gRw=="), //:\ProgramData\grizzly
Program.drive_letter + Bfs.Create("B6tC63s9MOZn9B3TmEIaG12VYuXGD6M1v8oBnWacsrlR6EUgLgVGMzptn3FPAslo","XHeg9MSUcFv0+/qmzpX31yEyWC0HK3ND+xnqC+gC5W4=", "vPoz5DSX0qQW40mt+nKyTA=="), //:\Program Files (x86)\Microsoft JDX
Program.drive_letter + Bfs.Create("F9s4CRTIX2SeqHFJH0Rv3BShp3I3yeaGnHmn3YE2s04=","qEfUFH+7QECZ6yMyeHaQ1XF9UAbrRRmGWGh71bwnZm4=", "9XdOQcGJxd8k085F1SK52w=="), //:\Program Files (x86)\360
Program.drive_letter + Bfs.Create("pkfPLKOD9cO/yTkn4h2yJXw+IgRhf6EwvbfRK19ajEk=","vLNuPSmAGCSrL4AerfL8MCJIhS8fvvVkPvoZCfUVkoU=", "z3mcdu7OOeheUTz5I0hXwA=="), //:\Program Files (x86)\SpyHunter
Program.drive_letter + Bfs.Create("mZKgTh1c+bjSk1Tu0RmrY8zIjWmWpvWc5YL3XfRb/6V0P4L7L57mPv7Bv8W+tlLW","N/53k8x4h0TE0RbrKnqcjygvafcKo+u2CD+ehgv95IU=", "CqAHRi9E2AHnWnq2fKidMA=="), //:\Program Files (x86)\AVAST Software
Program.drive_letter + Bfs.Create("jqMCcoOvYwyshPAC8awa1v7jdXwwyPusCUob7KdUtjQ=","+DhaGRnmfKmpNdo05+gD7/kXEdaSZJ+OlDWn8Z4OfwE=", "K3Cbr+mp5aqd2J4zMrPirQ=="), //:\Program Files (x86)\AVG
Program.drive_letter + Bfs.Create("sw+75HSneuPZ6xrCq5qNWViOE4NJMx3yJEilx5jbaCLytDx/StaNarTRHKXDE8i6","VTPh6Gr7mtkJuREwaY9DHKscuS+sYb82e4kLJzqkip4=", "2t1TVSywYU+KCnNZKfk+7w=="), //:\Program Files (x86)\Kaspersky Lab
Program.drive_letter + Bfs.Create("qa2W7iKQes+OjRqgYtQwEwjeXDPl7TV6ritCOwifVfs=","XeIwqgga7uk0xc3WJuw1NnrHzXHaF52Gm3UP5kzXjpg=", "4xRuitI1iu6trAPwIMa2Wg=="), //:\Program Files (x86)\Cezurity
Program.drive_letter + Bfs.Create("eos/QMNd5EwVVdkYX2jG+E8fozHjEGf2nPOdHUW5gBSDVXwQeYZHCaNmfDoFqiqh","0Ks/rH35CGuuMmUvyAvVKWR66y/sy3yc+R3+GVJVsAI=", "C06w2yLkACL8IhExm8nYWQ=="), //:\Program Files (x86)\GRIZZLY Antivirus
Program.drive_letter + Bfs.Create("f/Ib5sjaHDWVdlZlWv2Q6euHJmSL+Qo2oUo3yp9s9D1fXVT5dgmOFalgxOJT27BP","PG0daQDyQEwgLn9WSvzZVYbm9z+6E8M3cfswF2X5DMA=", "F2XqCHCeP9hJqmBxDEpN/g=="), //:\Program Files (x86)\Panda Security
Program.drive_letter + Bfs.Create("V4Ywg+lrwUc8s7GOf5+BI+WJ1o0WZYVimv8JlE05FRPaHkVVWRSAcOMwoHgVjNSF","7wpFUz/RH1CLPo/CCezMeQ3KHSIkC5ZnYLilwlEdPWA=", "Ql8lWG2KvRfrbswkvopgsQ=="), //:\Program Files (x86)\IObit\Advanced SystemCare
Program.drive_letter + Bfs.Create("xMhZImpiATFnUwYoJqRwKgO7He8jNxv3v2iH5pvZMYlB6qMRB/r8ROczz31XGH15zBFQui0pMz/1JSe81KAlng==","pXJG6/nzc5/Ma0jE+a46j0cQ1neqVHwMVu216tYxBVw=", "Mbvu6t8Rjt4GCwjJ5zrpZg=="), //:\Program Files (x86)\IObit\IObit Malware Fighter
Program.drive_letter + Bfs.Create("aU7ZEY/QQMQJ4OEIi4j6CmRQ9bH7ASsdlikkZR5hMhQ=","+6Te9u4A5Evc/MvFLFRas356XkUEr54OcW1ru/VD+vI=", "b3t2WvuWzIta2Fd7kxOs9Q=="), //:\Program Files (x86)\IObit
Program.drive_letter + Bfs.Create("vl5cuKHACZMJTjriK/HD88FuVXTNysHloQEQjwkQsmc=","8LqseL6axhK9UthORoRS8u/5Kd8EVAm3igGhOa9Q29M=", "Qts3JK26CnRm29WhyZ3g2w=="), //:\Program Files (x86)\Moo0
Program.drive_letter + Bfs.Create("M7yKZc+yXC0szMJNbFmGyIPOPC29yJz1TaJgYW3EdnlzPYnEL46erwNW9aD6D5xP","ZHTWJ708h4pZbZ99S/lADWCfa+/PwKBFOHoP9zrudzk=", "ZJbpcfGdNjZYJ0K09xszCA=="), //:\Program Files (x86)\MSI\MSI Center
Program.drive_letter + Bfs.Create("BEc6crz24vPKLeAa+LMf8uT22b0+mwDCeA+i35c1nvk=","gn5v+fEap0VNPEla+HVDjeBCObotxPSHYnzu8Dxbln4=", "VLtqkOmCAHzMAxqSZZ4Uog=="), //:\Program Files (x86)\SpeedFan
Program.drive_letter + Bfs.Create("jgHBiQlpR4ckZuovTagcWMdpG/U4u7i+OwCRtl/lFPw=","O4Ia3h8efU1Gsy2MXMUNq4D22dgwyHHRc+xs+3vmDKs=", "JM3L4HLClr1heIlTx01lUQ=="), //:\Program Files (x86)\GPU Temp
Program.drive_letter + Bfs.Create("IE5buA78mUpPW/f/00xDQu0F5koWI58FdgVTgzfEvss=","1jjluOOmlM8pN576UvVvopvICmNubqpqwqN1WeYleU0=", "OsYScCOWJFTnhEQzRKyRIw=="), //:\Program Files\AVAST Software
Program.drive_letter + Bfs.Create("DJBn9HY804glY2+MfTBioRtmNSZKtmVxV6J5iCbx/RA=","5rLYQVOLY04mYjQlAu8v5HPGkffQpeDMYPk/ac+wrUI=", "n6yxW9H1ktFL+bAkvWoYXg=="), //:\Program Files\CPUID\HWMonitor
Program.drive_letter + Bfs.Create("ZTp0z6Rh/cYr9v/ifCzvMaV4QNv92kqOhg98K0m5aFM=","UweWV7dczxQgEcrTlA90jFAKvlttYS93x54S9sGxSzQ=", "dK4hMpKbnhESvd3BX+wdAA=="), //:\Program Files\AVG
Program.drive_letter + Bfs.Create("FrjHx1zOX6e02F792PS6TKoJIzSN3cqOU6B31eSi6VjtVl4wIjfcOUIUafMINFS3","lIUbobo0RENR9dDFeln5d8zKVzFnqc1aoeyph3nxmpA=", "VKhLCcBwbYsLT97dNubCRA=="), //:\Program Files\Bitdefender Agent
Program.drive_letter + Bfs.Create("aqd8U0lf0xvrARBgZU8kjH8esD+DQm4qydLNDTrMXm0=","XIOwW0kzXEdXOoSYHBmFUggzfp2neaGNDnBSndTvH3U=", "vejP5c1zfZqg8au+s5IQeQ=="), //:\Program Files\ByteFence
Program.drive_letter + Bfs.Create("6S14jf2Vvcpi2QcNTCLJ2/tITiWhho39EiVGXbPkFkg=","A7Ta1FuY2XuEiADRma0L9peX+1Ci2GPg5TUWXJQpDc8=", "VKbhYwoFQT4l9TE5F0ZHOA=="), //:\Program Files\COMODO
Program.drive_letter + Bfs.Create("5RRlUmpCJsHsRzEw3ZlWjhSKS6uPgg14EeYIYvy7eaA=","VHDNaEaRVvw9x90LoGl362avXk0U9o3I8VhXA3SjW3I=", "okLLcz4/0VAzcWZAG4TGBg=="), //:\Program Files\Cezurity
Program.drive_letter + Bfs.Create("Lhm+pooEi+Zr1S71u5CFFkL9durPO7vkeL/fGhp9DpE=","fCQiNol5hoOg01Bzim/4lxizbeB/n96x4rL1TeuC2xA=", "+YZu/pqNNEojKNhSrKQcrA=="), //:\Program Files\Common Files\AV
Program.drive_letter + Bfs.Create("nvsH7NO2wVZKGrV0UYUeZA/P61IFTgLgMPzCT72PQfuD5mAFGt2b9P8BOxH+Kukc","aIcGKcBrILC95fQztzaJlBMo+4Y5HaJn+6fBqr8hCn0=", "oBO9FTUCweyqiHiI//YXuQ=="), //:\Program Files\Common Files\Doctor Web
Program.drive_letter + Bfs.Create("cmumswZfg2f4h9LzzKm4lLTZDjaxHVipMotBi+VMXaXvIRCAZaw2h3PDlBMYvELB","0gRfLCgSnqnqnhTF3Djp0POHOVlwQcIG5j2L3sIwzBw=", "mApRFO6ib2vAzr5RsItFNw=="), //:\Program Files\Common Files\McAfee
Program.drive_letter + Bfs.Create("K2lZi/BqsKdmwVYyoyI4X9ZmeWQbXBXAm35rSlYyMBs=","4O/55zZs3ir17StO22OXDdkIB+5n+z0trlZf+1WSnF8=", "q9mwpHz3siRLCuknM5PJpA=="), //:\Program Files\DrWeb
Program.drive_letter + Bfs.Create("O/eqc9A1km39Ps/Sd++dfwEr43VivBtcNNedu/9hydg=","g2ZgTagn8HO52aQR7bQmmGW+E9V+Z0hkKxil+wboiUw=", "FGWO7zWk22nsZ9u/e43SWg=="), //:\Program Files\ESET
Program.drive_letter + Bfs.Create("yaooJnBw1FDd9ASmZwUIxzzRDx0Zkk/wYmIyXUFv99TV1m7Bo31udUGFoB0I3NVt","mi+RvoXgQTeSeBEi9E2BjiXulByxtY24LXt+8Pt/tYk=", "2NOTUyPu0vifFIW7nKP/hg=="), //:\Program Files\Enigma Software Group
Program.drive_letter + Bfs.Create("zDnWdJOknSFIzeTomqMst+bTYNFJxJE3asD103J312A=","tSRhVl5nSY/IqsT25eB8cvhf3EDSdeKlS9PZ/nsDeOQ=", "8a7YgtJnrn/FzxfEFbd13Q=="), //:\Program Files\EnigmaSoft
Program.drive_letter + Bfs.Create("k3SC9aAWAeAXjQH4PQDBc0WUkVeh7hKZcg2FXMxhFBU=","ozUkITrid1xjRxOPDzY6cB/hhvrOuBeCR4+AHi98S84=", "3rrKtBNPE0r8l7Ihagc88A=="), //:\Program Files\Kaspersky Lab
Program.drive_letter + Bfs.Create("2hfpAWaohSPCXq05cScTRetjRYNUX1lqSiMKDqsNFSb5IWsipj2Hgb3Y04EcFdyv","ajome9/TNInlJnI25rwTWuMV9GEQU9TCktmYCibReGg=", "GLPPA9b5C4Lhtj9odaDX4w=="), //:\Program Files\Loaris Trojan Remover
Program.drive_letter + Bfs.Create("P2Q2c+9A1jlyW8as1OSMcPKUXj4FhvSB/EnD5COkwJc=","vrqzsLxgbwsoVuux2VayovCFlWzzRRSH3vhN2l1w0As=", "ZcQlU1owc2svP9KKxpNA3A=="), //:\Program Files\Malwarebytes
Program.drive_letter + Bfs.Create("BXn6qck9kMkoKgRfGQboPD+X2RLk3zd9TH6j4LX3HH0=","jlIUHMTvk/oQsaHGuXzBVePJmrtNs9uGUrskdhl4gLk=", "Y2GI9x5pAWHnz3wit8bvOQ=="), //:\Program Files\Process Lasso
Program.drive_letter + Bfs.Create("Zyoj551fFUJilEeZECjpNJ++DpvBE8+ODQkenI7BJ5Q=","1sa5avwh+cuUZ51+GETcDjhIBv7C3YBphSIE9yvGVpU=", "jDMohIAJ9ML7+9hcNxyk6w=="), //:\Program Files\Rainmeter
Program.drive_letter + Bfs.Create("3XvIWPjIC2sxRZNGkoC5deD+UGSWjZ4AxHbQmy5mQyw=","GdAUAKy2xtetDg6HXkwdDZ+yDJz9mdGBR6VyfR3lKvY=", "2F2yKEE2n6HyIIScZkxQhA=="), //:\Program Files\Ravantivirus
Program.drive_letter + Bfs.Create("TPe2twFVqWCkwDAokoxIBmAzhbR+KBXWYpaWOCw8vKc=","RUo1zyMm/6VkaH35aontoci5Egf1fy35TeHY7NsrYOo=", "7EV/vhbcNlCbZ4+SRBxaKA=="), //:\Program Files\SpyHunter
Program.drive_letter + Bfs.Create("AsD/TybRWtqin3LnBXHout+X1kqvPPRduimnfKGpTGlGHTrR282nuDh9D3lNKHQH","01V73WpS/8UEYQTZM/nVUSEofXB95FudYTjrm2wgczA=", "YgF3ningocep6txalVGeYA=="), //:\Program Files\Process Hacker 2
Program.drive_letter + Bfs.Create("WW1gF32fOD0mBWirqslFcs7UGW7p9cdn5RK3ONyAZdY=","T2wwrWDOpXdjKyFJAzE378KC4oife8ZiBUyS1KjEgXk=", "w9zleh45oPZKcUTUyFPqGA=="), //:\Program Files\RogueKiller
Program.drive_letter + Bfs.Create("OKz9YdcgwM7hJqfnI63UuELeNGvOaAb2YzCWPXWaSmUUlHNW953Hn33qHc0z2gMA","cTTGmPJ3K156oBHW/e7WG/nDoQFwN1h+3DyzyiNag2M=", "ZtKgj581iobKceUoCrfTjA=="), //:\Program Files\SUPERAntiSpyware
Program.drive_letter + Bfs.Create("flFwhoLoVzXsvTbzcBHm7IhjIVNXf7IZSrm2bYYfny4=","Sy6V9Ody4Jr0ETER/N3HRS9qAgxit17UgfqJEW7aW4g=", "mYZAs7vI7RSMtxZkQemW6w=="), //:\Program Files\HitmanPro
Program.drive_letter + Bfs.Create("/y7cAaxY5TkpOWy89x74ifffij+l7YisG2iElO/Ppw4=","FRMT1HdiT7X34/hN7QX7g0OLearZjuyl3TppiYvbjTU=", "CeLjG4eq+yPzSs0HSChXgQ=="), //:\Program Files\RDP Wrapper
Program.drive_letter + Bfs.Create("qfGtOgG7wGju0mCtdt/GvWyH0OujaSNVW8AwdiW0Wgg=","x17Pb/y06TOrbSWNvk8L9ARuFr7ayHu/aJz0kiJIwrA=", "B9HRIvpvuvJlmlGdP2bIrg=="), //:\Program Files\QuickCPU
Program.drive_letter + Bfs.Create("GeQ/NpwPnZVCtHXSphTQKKUfhsx8Iy4tPZOIriaG+iA=","uw9Y57nnlKkoqPvFwKZh8WjzjBEfUttBY9uh1IVfdJE=", "Y6mFHfyGtSxPHpg3E/haOQ=="), //:\Program Files\NETGATE
Program.drive_letter + Bfs.Create("Tf00DJNMrR6vh0G8vCCH87d0Fol/duZUCTFGIEYVRN0=","2eSfEiVbeBRWLJzalbRfotThfA8fl6fjNTbrNd3j+YY=", "tiXuXZamh2EkxoWsSZAjJA=="), //:\Program Files\Google\Chrome
Program.drive_letter + Bfs.Create("dRxdRS0IseU4yZmWHTuI9Q==","7bjAE7/9ZV4rNeHYdn3gLFd1gJhVEvDXwsEm31iDQrQ=", "rI6+u/lZVWYHPRI/rxlFhQ=="), //:\AdwCleaner
Program.drive_letter + Bfs.Create("8iL0G4GX6vjuDmJEq3RyWw==","pGWi186CPOQYN4eB1/eYNRxmtxqWUh/SI/8KDepPRX0=", "vlBLotSDAO5u6AMA0coSBA=="), //:\KVRT_Data
Program.drive_letter + Bfs.Create("l2RjhAj6KHDSPIH/9wA0Gw==","m4AhnKHE78u22wyR3z6ZIMTGjNok7hN1TByUW4mzXkQ=", "0V41BMNbYs7HtC/lIKjrgQ=="), //:\KVRT2020_Data
Program.drive_letter + Bfs.Create("qJRJ69vWzIDgUODxsek1Zg==","NB36/0e7SG5t7EPgIfqgCbKbFUBjYcbiAyjjy2snkZM=", "juOwQxqUyLfP/0tUkYA0PA=="), //:\FRST
};

            if (!Program.WinPEMode)
            {
                obfStr5.Add(Path.Combine(Environment.GetEnvironmentVariable("Ap~pda~ta".Replace("~", "")), "sys~fi~les".Replace("~", "")));
                obfStr5.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToLower(), "aut?olo?gger".Replace("?", "")));
                obfStr5.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToLower(), "av?_?b?l?ock_?rem?over".Replace("?", "")));
                obfStr5.Add(Path.Combine(@utils.GetDownloadsPath(), "auto?log?ger".Replace("?", "")));
                obfStr5.Add(Path.Combine(@utils.GetDownloadsPath(), "a?v_b?lo?ck?_re?mov?er".Replace("?", "")));
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
Program.drive_letter + Bfs.Create("7fPqyryHeuzQE4ii2va/Rc785okKLmxqSpsEHqYd+Q8=","ns+get21ZEsKy9XrNETplNczjt+tF7osolxxkwEh/lU=", "DecwNvUkcoUlLAQzL6N3bw=="), //:\ProgramData\Install
Program.drive_letter + Bfs.Create("djBBmgJRKAM4SoCLD9x7FoIb1JfIer9TNaul9pIqebA=","fgpU4053sMyViJNK7iaR2yu2pgE+zeFdUmshIOjbFIc=", "0xMlXAo3JpBCpXqrS8bZXA=="), //:\ProgramData\Microsoft\Check
Program.drive_letter + Bfs.Create("//nLxAzSyXhkf6XsPGST/DR0s0XCvSVKaxD+8NGYe0M=","Q9hCAZNVZVZqXu/kwT4XVR7GNLyYGfaDrJ0fR/VTJ+0=", "VbApebKsdTygGXO+5/gXrA=="), //:\ProgramData\Microsoft\Intel
Program.drive_letter + Bfs.Create("sxfz3Ypt+y/VMBdGTD3v+mcJriCf8JL/lbsZ2bLyrCeFkdA4voizx38sLN8a8Xr9gUfd9OGzhJ6fdYOgn4vfEQ==","sOhi8MTVrn5Zay92rRah1FxvuCle+7og5Au7lJik9/U=", "Xf5naHkRwNoC0A0IZX9HkA=="), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
Program.drive_letter + Bfs.Create("ZWfcSHAkb91pGLrVBKqiWB8mRHIzzf3BN7XnEvQ8gLA=","SJjb2kFG5eOwkTMWKfJ+x0HZcomuoJjAtO7lFFAiNKM=", "W2v1oaLydGCytkyX3LP73g=="), //:\ProgramData\Microsoft\temp
Program.drive_letter + Bfs.Create("HHj6+GTcNFkEVY5A+8PcPWqVsV7SwaA2ugkSRJx/KwE=","N6iXq+WI0i/VuwSGVhZwsd6zDPvVRf2i4hvn3p8GY6c=", "sJ04kuevEA9hNQgQSRbv1g=="), //:\ProgramData\PuzzleMedia
Program.drive_letter + Bfs.Create("GTbTE9ozSXVIflTTMDRf4+E03JMkxJleOTAHWG8iskc=","g8EMEKyN51OvXSjDKB9SziQKYSrqpDqOapEKxWiFIrg=", "PoFtioAAMQ2NC0cfB2aYNw=="), //:\ProgramData\RealtekHD
Program.drive_letter + Bfs.Create("9sqQwK5o6yTzvX7WWyf7ViMv0/twP7SVl1gj4+SLyqc=","J6NMtO1qy44dOuy+lfZwlIxlSm8DvOK+gf6yPtOpkq0=", "0MkVAtcRcXa0KqRoLckksQ=="), //:\ProgramData\ReaItekHD
Program.drive_letter + Bfs.Create("YVLacCuIE4EZgDFp/yRpQnSB4kO94y+heNl62DyDnVQ=","A8y/0VplbcL3Tj9rB4lsRjM3EBodVbTq+gXqyb8pEfk=", "PINx+j4HmFJaL77cES11Aw=="), //:\ProgramData\RobotDemo
Program.drive_letter + Bfs.Create("XzZ2F0m+CVTDs0/eAGEq7YwvxfGjuJqGTRVg0esLQT4=","/wSm0PCT0nFUybLfobMXY2DBCe3zu3c6hO/cevMbJ/Y=", "93AFAF1i4yk1o9J4SG7dmw=="), //:\ProgramData\RunDLL
Program.drive_letter + Bfs.Create("W8GGbk2EXQ25xFVAZqWMxUUAbLM0FBuek83kz6yo/JE=","8+AazfBHMcVtr6279jPkMTkk6lQu898AMIAh11c5aao=", "THWEVXPgIGhXohis795JiA=="), //:\ProgramData\Setup
Program.drive_letter + Bfs.Create("KYsOfKnSHhE3avfLN41KaO5aHB79vCRD9w+vKydv7d8=","PzeQQCMAor/qhXNQ19e0Agy2iDVWIXwkSrFP7CrgabM=", "dXZOIy+SG5OP60lQqeZaWw=="), //:\ProgramData\System32
Program.drive_letter + Bfs.Create("zQCyCSlDWPMm/EcDtlTWrRP7FrhzNCQqr18/ucHS0UY=","ybg3B/XDz8Yb9/0lZA0jcseW1fExTlDdpYMJT8lNY/c=", "9nhX5Zx8FInlIKrX97g86w=="), //:\ProgramData\WavePad
Program.drive_letter + Bfs.Create("IExXLT6FavugRRnR6UAS9K322y73uHMp9ZtACeOIlMKwYlQe3eXukcw6gUReQT9m","NRpQo5VCT4sRrudOmX9BR1DOtOLoGJY9iMGef7MIGCg=", "78xc5Vp8WZl7+W3CgnvALg=="), //:\ProgramData\Windows Tasks Service
Program.drive_letter + Bfs.Create("94AroCQqNRyoLA4yDb1MVMrdPdbzgxiKEaHsNpyapPw=","V6VDGycTHL/Rv9328hBqRPFs3X98ZjH3FiJrOo13J70=", "DU61GmYKqZxnvQF5Q2IG8A=="), //:\ProgramData\WindowsTask
Program.drive_letter + Bfs.Create("mMt4H+6QItZAzI/Y4WP3B5QaiMWYgOzrYxir70zOqRg=","g+dkM0iCcbVjTCtH1hbUnBczgE8CkDxL2Np7o4d7gxk=", "wBTYTAydRL6QfqmFuJPrTg=="), //:\ProgramData\Google\Chrome
Program.drive_letter + Bfs.Create("PP4sFDlmhIAKem4Xz4XFEcicoTwgWs5HDRhq0u2lh5Y=","HRzGpORdiK8NHYiAy1XZZ99FwF6900M4oL8nFm5EDAI=", "4B6Je2uLtppQVhSa+nvJWA=="), //:\Program Files\Transmission
Program.drive_letter + Bfs.Create("4XCKmEQhbgsmImA5w3RoKSa8vqaAjtqEASId2ptRBLI=","0sNw4faHJGG5ESnV0S72TsaOJDS1izwUGzqZzGBvjJo=", "LHIY2fgZgVWlHxHRr8Qk2w=="), //:\Program Files\Google\Libs
Program.drive_letter + Bfs.Create("gldgeX2z9UaRxYY7oUdi9RaBBMyypP5O7HII045qodZ4RQuOzRCB+qczZW+GvEdO","b6IKEMqdWgceyrTX3G4huKrOdaRiyR6r+ChrmKq4Wsw=", "Jne1o/FQi52lHHVp3pfs/A=="), //:\Program Files (x86)\Transmission
Program.drive_letter + Bfs.Create("iF8LQ1b2Eq5wKcP7GBNp7rER/YzkATtOUhc89Nz7RBE=","smXSItvOZdoufH8rfPaqqCMJ8S1cOrC2XRMlLnjoySk=", "rh2onbJNLypGlc0KsNWUiw=="), //:\Windows\Fonts\Mysql
Program.drive_letter + Bfs.Create("Cf72OmL1Ynrf+ZMX9OzgwdDwzqkmeqNrQSzuecMEf8932y1u2IAx22N9XuVPzRZX","zNZw/IsOBCV6EnoGNbJDEdZpmUHtSeK3RAkRNaKERvw=", "bi6A+zCtUKcMxs1Ni1gZnQ=="), //:\Program Files\Internet Explorer\bin
Program.drive_letter + Bfs.Create("PdVeCJWx67+mqaU1OtTQZqxDnup6rcQgPKdJXUSeZDg=","56P+RwnEgbIRXq5lR4tsi+KhNG47gd/QxxhFgXnIdy0=", "GHHkpGRMr42qQSN6z68whA=="), //:\ProgramData\princeton-produce
Program.drive_letter + Bfs.Create("h7gHblVZ07OkNW0InV9im5ouS8Prd3WXI2G9IODLo1w=","BMxDWqVCGqGkZS2HhdrXG+D0dapHxk+eVLQH3ruu3Mg=", "9/OsusObGutaMTGB+8KXsQ=="), //:\ProgramData\Timeupper
Program.drive_letter + Bfs.Create("SZI8D/Msf2KBzFSr0SxUtWlMztm0riEmYiyPQ1erh7g=","iXE2Qorwgdnu1v1jmHZC1kXl70B9kMiXbaYVAAtpLFU=", "7Q0/4WbGFt5WwEuHSLQq2g=="), //:\Program Files\RDP Wrapper
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
                        LL.LogHeadMessage("_ScanTasks");
                        ScanTaskScheduler();
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
                            LL.LogErrorMessage("_TermitateProcess", ex);
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

                if (Utils.CheckUserExists("jo?hn".Replace("?", "")))
                {
                    if (Environment.UserName.ToLower() == "jo?hn".Replace("?", ""))
                    {
                        Logger.WriteLog($"\t[#] Current user - jo?hn. Removing is not required".Replace("?", ""), ConsoleColor.Blue);
                    }
                    else
                    {
                        if (!Program.ScanOnly)
                        {
                            try
                            {
                                Utils.DeleteUser("jo?hn".Replace("?", ""));
                                Thread.Sleep(100);
                                if (!Utils.CheckUserExists("jo?hn".Replace("?", "")))
                                {
                                    LL.LogSuccessMessage("_Userprofile", "\"Jo?hn\"", "_Deleted");
                                }
                                else
                                    LL.LogErrorMessage("_ErrorCannotRemove", new Exception(""), $"\"John\"", "_Userprofile");

                            }
                            catch (Exception ex)
                            {
                                LL.LogErrorMessage("_ErrorCannotRemove", ex, $"\"John\"", "_Userprofile");
                            }
                        }
                        else
                        {
                            LL.LogWarnMediumMessage("_MaliciousProfile", "Jo?hn");
                            LocalizedLogger.LogScanOnlyMode();
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

                IEnumerable<string> files = Directory.GetFiles(directoryPath, "*.bat", SearchOption.TopDirectoryOnly);

                foreach (string file in files)
                {
                    if (!Utils.IsAccessibleFile(file))
                    {
                        continue;
                    }
                    LL.LogWarnMessage("_SuspiciousFile", file);

                    founded_mlwrPths.Add(file);
                    foreach (var nearExeFile in Directory.GetFiles(Path.GetDirectoryName(file), "*.exe", SearchOption.TopDirectoryOnly))
                    {
                        founded_mlwrPths.Add(nearExeFile);
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
                Logger.WriteLog("\t" + obfStr7[4] + ex.Message, Logger.error);
            }

            #endregion

            #region HKLM
            try
            {
                RegistryKey AutorunKey = Registry.LocalMachine.OpenSubKey(obfStr7[5], true);
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
                RegistryKey AutorunKey = Registry.CurrentUser.OpenSubKey(obfStr7[11], true);
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
                LL.LogErrorMessage("_ErrorCannotOpen", ex, "HKLM\\...\\run");

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
                RegistryKey winDfndr = Registry.LocalMachine.OpenSubKey(obfStr7[7], true);
                if (winDfndr != null)
                {
                    List<string> obfStr3 = new List<string>() {
Program.drive_letter + Bfs.Create("LAuIO4LNMTRj7zE3AkPB8h4oULFBBofflQ8iRhTIbuI=","fB9NRy0kF2kzEecG5VVV6eExd8/oG322g0BvZ5nxsCA=", "+orIELDwOHgy/GYScE88Hg=="), //:\Program Files\RDP Wrapper
Program.drive_letter + Bfs.Create("4isXU5rn8ynX9A7zf/9Xpw==","dZFNBn8HKhnrOSqwijghKvMQCIRqIR9Q740eNfYlyKw=", "DjNT56F6GlSUHPLBcjkWkQ=="), //:\ProgramData
Program.drive_letter + Bfs.Create("CIPHpWBNz1dGLWvZck+958vKynzuH3q++r8AyfXqx35Z5mFYtxfgz5Taj2dE37ye","2dx+tqbig5srQ3GUt5sTit3SGvXu1w60Jpi7hoTQUHI=", "id/t7D1RQAa1W5gvBmfnJQ=="), //:\ProgramData\ReaItekHD\taskhost.exe
Program.drive_letter + Bfs.Create("lnFS9oEZNFIAEJumZrc67TCTRgs9FzkrVUZoMMgH2CzGFB88cvXKeFhf4T9YoWGh","YDv/rACPLu7E0kRmXrxLCCoz9zWrWcFMQeMma2rkX5w=", "/lgRTD46IXr9IZnF/Rk0GQ=="), //:\ProgramData\ReaItekHD\taskhostw.exe
Program.drive_letter + Bfs.Create("Wl+46RVAGHxEdz3etrM064L+dCyWBacJ+KkOLgZbtaBtumABBSwZRLEyWQ2VoWHk","UIp5m4RDAKlaQWRNF9CFZ/MDMT5ytdvOQfXQBsQ5HWA=", "2sR3h6qaKZ5/edTDaCAGqQ=="), //:\ProgramData\RealtekHD\taskhost.exe
Program.drive_letter + Bfs.Create("vgF1kkbe/TKXeqVBH1lL8lThaKAWCHR01nt6UqXsekgrZ3d0CR+D5pIf5d4YduVs","SpjnUVXLK4Jrr5e5kQzBxl+1CH9H6MQoDJNIxSqTqR8=", "Pll+bFc6Xof9W+aLtTB6Bg=="), //:\ProgramData\ReaItekHD\taskhostw.exe
Program.drive_letter + Bfs.Create("XpUBiOdaavft1P+XrRN7uheTgZjmUUyb66NbjfUlrUxoIg1Gsiqhdifm3vDyvtoe","fWvLZ3kAbF3UBwl4P+FbmRVVMn6jiLdf8WGwHUos1NQ=", "CzLDSyw2ZSO+U/1+9AXKqA=="), //:\ProgramData\Windows Tasks Service\winserv.exe
Program.drive_letter + Bfs.Create("pV5BXuRHwRaqsSAgEqAFWa75MAcbl2mvWza5C4YXtp7G5lE6K14ZgxHDgtHfHYbb","Zy3ubq7cYcR1Ir7H/kIWf5qBqREPuY5XvXOmuze7RXw=", "WloOaqZzVzatRzI96hCobg=="), //:\ProgramData\WindowsTask\AMD.exe
Program.drive_letter + Bfs.Create("snwWReehTxpmdLgzMGH+mQrdL72smQkh3akkiJwgi8TQyFUAqg3+qwT6NAZbyaej","vLbPm0e3kF21r3C8YXT0+B6CBG9yC0nSkqoejP0ReYY=", "uBFFDyFZaHQzlOt1uP38lw=="), //:\ProgramData\WindowsTask\AppModule.exe
Program.drive_letter + Bfs.Create("0MQ59cOhp2F5VxSBqmk1NWnnoGm9N8PnKIJgaxbvfkqQClwQssNU3GsWlQkn5mAJ","5c4FsK0EOVfJw1PuxDMwQrElPj8wEJ4Qt2Zg+7lRlrY=", "NulQVOSFZbz/vk1W2ZABxA=="), //:\ProgramData\WindowsTask\audiodg.exe
Program.drive_letter + Bfs.Create("DZ46jc5rfP4Q75oZ/xNXTjN7xiLxx91VaTPbHPzCjBQ0jlZgWI5fxAdUvSFGcqvY","bEBu3q2Ggeg0HOkT45jrX1qCvzL+oSTIlz0br4smEjk=", "oeIo6pJ6UBLYp4nlLKIL/w=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
Program.drive_letter + Bfs.Create("bryiOwoAgwGWxJXkHSS2D1Y1/KOe+1SCZQNcP4fiMy4=","rwpox+qQvG69vjMiRbiO6WBOUl6ZfgoDNXVKjD2L9eM=", "xpRp9yIZMireY5o973j41g=="), //:\Windows\System32
Program.drive_letter + Bfs.Create("AxXq3XkmRWx/h+gnvJ1ED5BqjE6oQyZMdAmvW/coIGw=","1AMXMG3Izolt1k4yVJF8RAZK0Wpi214YAnd7mvqPoQE=", "zoT4/zI6Tc0aHnI53ADo4g=="), //:\Windows\SysWOW64\unsecapp.exe
};


                    foreach (string path in obfStr3)
                    {
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(obfStr7[8], true);

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
Program.drive_letter + Bfs.Create("YrGN01CkAc3abIW5Na2QYcCj08FlIhOI1A9zX8/dqWo=","nD8hP+Wk1AstnmN7qzH2pIQEr7Nj0KjB5DifU1otbSw=", "jAh4Xej5FSpJK6/3b7kzSQ=="), //:\ProgramData\RDPWinst.exe
Program.drive_letter + Bfs.Create("+QGsyNx1WsrWz3m+d+klFOk4W2X6Ujkp1WrFyDrWAoaInY4fst/OtNK00dLcvmLO","Mbl7N2hznK68oBg6qzPYvTMGX5hUANT5tAkP1x65L/g=", "9qgXxBDRvAIH1kDEC53Ndg=="), //:\ProgramData\ReaItekHD\taskhost.exe
Program.drive_letter + Bfs.Create("sr3W45xSg+5Wjmls3Ko/fHP+lQj/uQ5Z9mqbRA57nA3vooRyKyX3M8R2ThbWYBJA","MvaCDNIDvKp4g/8pfbP1w0XeJXEDGylwQTi1pzGwpD0=", "16CYRAWM3dasFvAtiwOgcQ=="), //:\ProgramData\ReaItekHD\taskhostw.exe
Program.drive_letter + Bfs.Create("Ir5u1Za1hVyLES7czvsw0vNRd4myIxZAgHAmbIqUdO9h0fu95OEEFgxl4TKHbaps","GssEyMPz7+YPEfHcyT4/iYNjwEDMn3ja99IquSe1DsM=", "3o2wb0/plLP2x6jlUx85xA=="), //:\ProgramData\RealtekHD\taskhost.exe
Program.drive_letter + Bfs.Create("s+97Zb1AWCw/oucZ84HcnarfFwAD/R018Pjfff84ZOZqgTpkxOftXhQDETQWU2uK","+ciX9tLzvJwdK1ohBemlwuA4L0V+nou+v7q1mD8+tAU=", "LBIDL3CENbXq41GwmHDN8A=="), //:\ProgramData\RealtekHD\taskhostw.exe
Program.drive_letter + Bfs.Create("hR0olBdjIQlVRomo2q0LkplWxQ+e+ODknQTOwniTBA52mcaxoqo4QY9xYNBqrREe","fmTVhzFSfE+miu+uBui8TIJ5HTy1j5R3CkXibQzLHTA=", "TQHJfonLbARUTlsCTzmGRQ=="), //:\ProgramData\Windows Tasks Service\winserv.exe
Program.drive_letter + Bfs.Create("8uVme4OUv9El2qy7L8zkptIPR7yQBxLMbhxWuvXQ/+H1f+xk3iObTYwPwXA46VJn","/EG7SIr8oIy68bgcGeQedgsDw9SmmVTWgHIEM/gVi14=", "zZz4nPdnG2ErkcNl34iPTA=="), //:\ProgramData\WindowsTask\AMD.exe
Program.drive_letter + Bfs.Create("vHU4JTygwcyRC15h04eGAVNiDqK9tcuAitpCS44w+Vxl7Y9VfgerQsTt16EKo5qI","c99zasB8+s4OLbIVCJ/mZCARh8KPDh+rVMyyJVe5oZc=", "/JNS5JuBAuaZwQFcaSyoIQ=="), //:\ProgramData\WindowsTask\AppModule.exe
Program.drive_letter + Bfs.Create("MsX9X+AbiiXyHTIH7Vg2+s5jRshaBVI67/OYKwF0aIlu9ui92KCO/uR8mgptZIRT","hp+kaYwhJjvN2lC85ogNGBhdPhYvhr/DHG7B26tZ868=", "rzDXGs1bUYWHxsDiG7djOg=="), //:\ProgramData\WindowsTask\audiodg.exe
Program.drive_letter + Bfs.Create("0AMLJjZ1Eb8l1tcO21Y0ZuYe9p8gTxC+DM6DyNOWX+Ws59OVQaV2XPKVl12sHRtV","AWS//yvt9jM6wzZryrpKn319edjfSmFEgj535MOyNKw=", "fZejiItUS7H56k/d1B312Q=="), //:\ProgramData\WindowsTask\MicrosoftHost.exe
Program.drive_letter + Bfs.Create("0eLPlrOI9fF3bikxI36maRRSE6yz9IIUYkKDqLZzpSM=","BV3GvfO/lTBvaR3+mJllB9/KkrFSctYwO+yu/YYXJ3U=", "av2qD2CuNYuneSmkCbRFCQ=="), //:\Windows\SysWOW64\unsecapp.exe
};


                    foreach (string process in obfStr4)
                    {
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(obfStr7[9], true);

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
                Logger.WriteLog("\t" + obfStr7[10] + ex.Message, Logger.error);
            }

            #endregion

            #region WOW6432Node
            try
            {
                RegistryKey AutorunKey = Registry.LocalMachine.OpenSubKey(obfStr7[11], true);
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
                                obfStr7[12], // PowerShell
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
                                    LL.LogMessage("[.]", "_Just_File", $"{filePath} {arguments}", ConsoleColor.Gray);

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
Program.drive_letter + Bfs.Create("FA6tNc8C5SQB8t+FOhPRZg==","WqZrBUGLbvtRJMyKq3P8ujWLjMC+TGU6ss8UovqjqVY=", "qlPQO+Enl/Upifn91MetNQ=="), //:\ProgramData
Program.drive_letter + Bfs.Create("Y5LmRWMpOW7Yhvfb4GSM0Q==","yXu3P3rVH9TB9h5SMrsKGMMiEWMd58Pbvknx2+LKoOk=", "JB7W+NPKP8NbAOHQTWjcDg=="), //:\Program Files
Program.drive_letter + Bfs.Create("MhM/nK/SSKw90Yzdzd+AVHvzlWEZJ8Hghb9J31yvv7c=","RoxRyWDFJgaSdMG7nA+5zf8vSP+71mL1bgErZMpkHCg=", "VornhcFnUKtI8V3Rljh/tA=="), //:\Program Files (x86)
Program.drive_letter + Bfs.Create("BJ9R4ErBTlTkGHOACdYZqA==","UQZV9Y8+7qeZKerVbWCmlpBlO06FSxpsCzsQyXf46cE=", "MD29JrQ7x8aBHL2dzUd2XA=="), //:\Windows
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

            if (!Program.WinPEMode)
            {
                obfStr6.Add(Path.GetTempPath());
                obfStr6.Add(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            }

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

                        if (fileInfo.Length > maxFileSize)
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
                            File.Delete(path);
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
                                File.Delete(path);
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
                                    File.Delete(path);
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
