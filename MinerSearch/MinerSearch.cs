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
using System.Threading;
using System.Windows.Forms;

namespace MinerSearch
{
    public class MinerSearch
    {
        int[] _PortList = new[]
        {
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

        List<string> obfStr1 = new List<string>() {
Program.drive_letter + Bfs.GetStr(@"ẝỻỷổỈỀổỆỊợỆồỆỻỮỉỔồỆịị", keys[0]), //:\ProgramData\Install
Program.drive_letter + Bfs.GetStr(@"ⰒⱴⱸⱚⱇⱏⱚⱉⱅⱬⱉⱜⱉⱴⱥⱁⱋⱚⱇⱛⱇⱎⱜⱴⱫⱀⱍⱋⱃ", keys[1]), //:\ProgramData\Microsoft\Check
Program.drive_letter + Bfs.GetStr(@"䇐䆶䆺䆘䆅䆍䆘䆋䆇䆮䆋䆞䆋䆶䆧䆃䆉䆘䆅䆙䆅䆌䆞䆶䆣䆄䆞䆏䆆", keys[2]), //:\ProgramData\Microsoft\Intel
Program.drive_letter + Bfs.GetStr(@"ዌኪኦኄኙኑኄኗኛኲኗኂኗኪኻኟንኄኙኅኙነኂኪንኚኄኩኙኆኂኟኛኟኌኗኂኟኙኘኩኀዂዘ዆ዘዅ዆ዅ዇ዎኩዀዂ", keys[3]), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
Program.drive_letter + Bfs.GetStr(@"᠃ᡥᡩᡋᡖᡞᡋᡘᡔ᡽ᡘᡍᡘᡥᡴᡐᡚᡋᡖᡊᡖᡟᡍᡥᡍᡜᡔᡉ", keys[4]), //:\ProgramData\Microsoft\temp
Program.drive_letter + Bfs.GetStr(@"➡⟇⟋⟩⟴⟼⟩⟺⟶⟟⟺⟯⟺⟇⟋⟮⟡⟡⟷⟾⟖⟾⟿⟲⟺", keys[5]), //:\ProgramData\PuzzleMedia
Program.drive_letter + Bfs.GetStr(@"㳟㲹㲵㲗㲊㲂㲗㲄㲈㲡㲄㲑㲄㲹㲷㲀㲄㲉㲑㲀㲎㲭㲡", keys[6]), //:\ProgramData\RealtekHD
Program.drive_letter + Bfs.GetStr(@"আৠ৬ৎ৓৛ৎঢ়৑৸ঢ়ৈঢ়ৠ৮৙ঢ়৵ৈ৙ৗ৴৸", keys[7]), //:\ProgramData\ReaItekHD
Program.drive_letter + Bfs.GetStr(@"ቂሤረሊሗሟሊሙሕሼሙሌሙሤሪሗሚሗሌሼምሕሗ", keys[8]), //:\ProgramData\RobotDemo
Program.drive_letter + Bfs.GetStr(@"⺫⻍⻁⻣⻾⻶⻣⻰⻼⻕⻰⻥⻰⻍⻃⻤⻿⻕⻝⻝", keys[9]), //:\ProgramData\RunDLL
Program.drive_letter + Bfs.GetStr(@"ㅊㄬㄠ㄂ㄟㄗ㄂ㄑㄝㄴㄑ㄄ㄑㄬㄣㄕ㄄ㄅ㄀", keys[10]), //:\ProgramData\Setup
Program.drive_letter + Bfs.GetStr(@"䜾䝘䝔䝶䝫䝣䝶䝥䝩䝀䝥䝰䝥䝘䝗䝽䝷䝰䝡䝩䜷䜶", keys[11]), //:\ProgramData\System32
Program.drive_letter + Bfs.GetStr(@"ڞ۸۴ۖۋۃۖۅۉ۠ۅېۅ۸۳ۅےہ۴ۅۀ", keys[12]), //:\ProgramData\WavePad
Program.drive_letter + Bfs.GetStr(@"㔜㕺㕶㕔㕉㕁㕔㕇㕋㕢㕇㕒㕇㕺㕱㕏㕈㕂㕉㕑㕕㔆㕲㕇㕕㕍㕕㔆㕵㕃㕔㕐㕏㕅㕃", keys[13]), //:\ProgramData\Windows Tasks Service
Program.drive_letter + Bfs.GetStr(@"䶴䷒䷞䷼䷡䷩䷼䷯䷣䷊䷯䷺䷯䷒䷙䷧䷠䷪䷡䷹䷽䷚䷯䷽䷥", keys[14]), //:\ProgramData\WindowsTask
Program.drive_letter + Bfs.GetStr(@"⤚⥼⥰⥒⥏⥇⥒⥁⥍⤀⥦⥉⥌⥅⥓⥼⥴⥒⥁⥎⥓⥍⥉⥓⥓⥉⥏⥎", keys[15]), //:\Program Files\Transmission
Program.drive_letter + Bfs.GetStr(@"ᖴᗒᗞᗼᗡᗩᗼᗯᗣᖮᗈᗧᗢᗫᗽᗒᗉᗡᗡᗩᗢᗫᗒᗂᗧᗬᗽ", keys[16]), //:\Program Files\Google\Libs
Program.drive_letter + Bfs.GetStr(@"ᨅᩣᩯᩍᩐᩘᩍᩞᩒ᨟᩹ᩖᩓᩚᩌ᨟ᨗᩇᨇᨉᨖᩣᩫᩍᩞᩑᩌᩒᩖᩌᩌᩖᩐᩑ", keys[17]), //:\Program Files (x86)\Transmission
Program.drive_letter + Bfs.GetStr(@"ᢝ᣻ᣰᣎᣉᣃᣈᣐᣔ᣻ᣡᣈᣉᣓᣔ᣻ᣪᣞᣔᣖᣋ", keys[18]), //:\Windows\Fonts\Mysql
Program.drive_letter + Bfs.GetStr(@"៩តឃឡូ឴ឡឲើ៳ផឺឿាហតរួឧាឡួាឧ៳ពឫឣឿូឡាឡតឱឺួ", keys[19]), //:\Program Files\Internet Explorer\bin
Program.drive_letter + Bfs.GetStr(@"୪ଌ଀ଢିଷଢ଱ଽଔ଱ତ଱ଌଠଢହାଳଵତିା୽ଠଢି଴ଥଳଵ", keys[20]), //:\ProgramData\princeton-produce
Program.drive_letter + Bfs.GetStr(@"៲បមឺឧឯឺឩឥឌឩូឩបវឡឥឭួីីឭឺ", keys[21]), //:\ProgramData\Timeupper
};

        List<string> obfStr2 = new List<string>() {
Program.drive_letter + Bfs.GetStr(@"㔰㕖㕚㕸㕥㕭㕸㕫㕧㕎㕫㕾㕫㕖㕇㕣㕩㕸㕥㕹㕥㕬㕾㕖㕽㕣㕤㔤㕯㕲㕯", keys[22]), //:\ProgramData\Microsoft\win.exe
Program.drive_letter + Bfs.GetStr(@"⬙⭿⭳⭑⭌⭄⭑⭂⭎⬃⭥⭊⭏⭆⭐⭿⭤⭌⭌⭄⭏⭆⭿⭠⭋⭑⭌⭎⭆⭿⭖⭓⭇⭂⭗⭆⭑⬍⭆⭛⭆", keys[23]), //:\Program Files\Google\Chrome\updater.exe
Program.drive_letter + Bfs.GetStr(@"䇂䆤䆨䆊䆗䆟䆊䆙䆕䆼䆙䆌䆙䆤䆪䆼䆨䆯䆑䆖䆋䆌䇖䆝䆀䆝", keys[24]), //:\ProgramData\RDPWinst.exe
Program.drive_letter + Bfs.GetStr(@"ᓒᒴᒸᒚᒇᒏᒚᒉᒅᒬᒉᒜᒉᒴᒺᒍᒉᒡᒜᒍᒃᒠᒬᒴᒜᒉᒛᒃᒀᒇᒛᒜᓆᒍᒐᒍ", keys[25]), //:\ProgramData\ReaItekHD\taskhost.exe
Program.drive_letter + Bfs.GetStr(@"᥃ᤥᤩᤋᤖᤞᤋᤘᤔ᤽ᤘᤍᤘᤥᤫᤜᤘᤰᤍᤜᤒᤱ᤽ᤥᤍᤘᤊᤒᤑᤖᤊᤍᤎᥗᤜᤁᤜ", keys[26]), //:\ProgramData\ReaItekHD\taskhostw.exe
Program.drive_letter + Bfs.GetStr(@"ᱮᰈᰄᰦ᰻ᰳᰦᰵ᰹ᰐᰵᰠᰵᰈᰆᰱᰵ᰸ᰠᰱ᰿ᰜᰐᰈᰠᰵᰧ᰿᰼᰻ᰧᰠᱺᰱᰬᰱ", keys[27]), //:\ProgramData\RealtekHD\taskhost.exe
Program.drive_letter + Bfs.GetStr(@"᫜᪺᪶᪔᪉᪁᪔᪇᪋᪢᪇᪒᪇᪺᪴᪃᪇᪊᪒᪃᪍᪮᪢᪺᪒᪇᪕᪍᪎᪉᪕᪒᪑᫈᪃᪞᪃", keys[28]), //:\ProgramData\RealtekHD\taskhostw.exe
Program.drive_letter + Bfs.GetStr(@"߂ޤިފޗޟފޙޕ޼ޙތޙޤޯޑޖޜޗޏދߘެޙދޓދߘޫޝފގޑޛޝޤޏޑޖދޝފގߖޝހޝ", keys[29]), //:\ProgramData\Windows Tasks Service\winserv.exe
Program.drive_letter + Bfs.GetStr(@"᫝᪷᪻᪕᪈᪀᪕᪆᪊᪣᪆᪓᪆᪻᪰᪎᪉᪃᪈᪐᪔᪳᪆᪔᪌᪻᪦᪪᪣᫉᪂᪟᪂", keys[30]), //:\ProgramData\WindowsTask\AMD.exe
Program.drive_letter + Bfs.GetStr(@"㸽㹛㹗㹵㹨㹠㹵㹦㹪㹃㹦㹳㹦㹛㹐㹮㹩㹣㹨㹰㹴㹓㹦㹴㹬㹛㹆㹷㹷㹊㹨㹣㹲㹫㹢㸩㹢㹿㹢", keys[31]), //:\ProgramData\WindowsTask\AppModule.exe
Program.drive_letter + Bfs.GetStr(@"ࣾ࢘࢔ࢶࢫࢣࢶࢥࢩࢀࢥࢰࢥ࢘࢓ࢭࢪࢠࢫࢳࢷ࢐ࢥࢷࢯ࢘ࢥࢱࢠࢭࢫࢠࢣ࣪ࢡࢼࢡ", keys[32]), //:\ProgramData\WindowsTask\audiodg.exe
Program.drive_letter + Bfs.GetStr(@"㴯㵉㵅㵧㵺㵲㵧㵴㵸㵑㵴㵡㵴㵉㵂㵼㵻㵱㵺㵢㵦㵁㵴㵦㵾㵉㵘㵼㵶㵧㵺㵦㵺㵳㵡㵝㵺㵦㵡㴻㵰㵭㵰", keys[33]), //:\ProgramData\WindowsTask\MicrosoftHost.exe
Program.drive_letter + Bfs.GetStr(@"⠟⡹⡲⡌⡋⡁⡊⡒⡖⡹⡶⡜⡖⡲⡪⡲⠓⠑⡹⡐⡋⡖⡀⡆⡄⡕⡕⠋⡀⡝⡀", keys[34]), //:\Windows\SysWOW64\unsecapp.exe
Program.drive_letter + Bfs.GetStr(@"ӇҡҭҏҒҚҏҜҐҹҜ҉ҜҡҩҔҐҘ҈ҍҍҘҏҡҵҫҭҴҲӓҘ҅Ҙ", keys[35]), //:\ProgramData\Timeupper\HVPIO.exe
};

        List<string> obfStr3 = new List<string>() {
Program.drive_letter + Bfs.GetStr(@"䶱䷗䷛䷹䷤䷬䷹䷪䷦䶫䷍䷢䷧䷮䷸䷗䷙䷏䷛䶫䷜䷹䷪䷻䷻䷮䷹", keys[36]), //:\Program Files\RDP Wrapper
Program.drive_letter + Bfs.GetStr(@"⼉⽯⽣⽁⽜⽔⽁⽒⽞⽷⽒⽇⽒", keys[37]), //:\ProgramData
Program.drive_letter + Bfs.GetStr(@"̯͉ͧͅͺͲͧʹ͸͑ʹ͡ʹ͉͇Ͱʹ͜͡Ͱ;͉͑͝͡ʹͦ;ͽͺ̻ͦ͡ͰͭͰ", keys[38]), //:\ProgramData\ReaItekHD\taskhost.exe
Program.drive_letter + Bfs.GetStr(@"ᓺᒜᒐᒲᒯᒧᒲᒡᒭᒄᒡᒴᒡᒜᒒᒥᒡᒉᒴᒥᒫᒈᒄᒜᒴᒡᒳᒫᒨᒯᒳᒴᒷᓮᒥᒸᒥ", keys[39]), //:\ProgramData\ReaItekHD\taskhostw.exe
Program.drive_letter + Bfs.GetStr(@"⁥ ‏‭‰‸‭‾′‛‾‫‾ ‍›‾″‫›‴‗‛ ‫‾‬‴‷‰‬‫ⁱ›‧›", keys[40]), //:\ProgramData\RealtekHD\taskhost.exe
Program.drive_letter + Bfs.GetStr(@"䒹䓟䓓䓱䓬䓤䓱䓢䓮䓇䓢䓷䓢䓟䓑䓦䓢䓊䓷䓦䓨䓋䓇䓟䓷䓢䓰䓨䓫䓬䓰䓷䓴䒭䓦䓻䓦", keys[41]), //:\ProgramData\ReaItekHD\taskhostw.exe
Program.drive_letter + Bfs.GetStr(@"ḂṤṨṊṗṟṊṙṕṼṙṌṙṤṯṑṖṜṗṏṋḘṬṙṋṓṋḘṫṝṊṎṑṛṝṤṏṑṖṋṝṊṎḖṝṀṝ", keys[42]), //:\ProgramData\Windows Tasks Service\winserv.exe
Program.drive_letter + Bfs.GetStr(@"ᯭᮋᮇᮥ᮸᮰ᮥ᮶ᮺᮓ᮶ᮣ᮶ᮋᮀᮾ᮹᮳᮸ᮠᮤᮃ᮶ᮤᮼᮋᮖᮚᮓ᯹᮲ᮯ᮲", keys[43]), //:\ProgramData\WindowsTask\AMD.exe
Program.drive_letter + Bfs.GetStr(@"㗕㖳㖿㖝㖀㖈㖝㖎㖂㖫㖎㖛㖎㖳㖸㖆㖁㖋㖀㖘㖜㖻㖎㖜㖄㖳㖮㖟㖟㖢㖀㖋㖚㖃㖊㗁㖊㖗㖊", keys[44]), //:\ProgramData\WindowsTask\AppModule.exe
Program.drive_letter + Bfs.GetStr(@"⡚⠼⠰⠒⠏⠇⠒⠁⠍⠤⠁⠔⠁⠼⠷⠉⠎⠄⠏⠗⠓⠴⠁⠓⠋⠼⠁⠕⠄⠉⠏⠄⠇⡎⠅⠘⠅", keys[45]), //:\ProgramData\WindowsTask\audiodg.exe
Program.drive_letter + Bfs.GetStr(@"ČŪŦńřőńŗśŲŗłŗŪšşŘŒřŁŅŢŗŅŝŪŻşŕńřŅřŐłžřŅłĘœŎœ", keys[46]), //:\ProgramData\WindowsTask\MicrosoftHost.exe
Program.drive_letter + Bfs.GetStr(@"⮗⯱⯺⯄⯃⯉⯂⯚⯞⯱⯾⯔⯞⯙⯈⯀⮞⮟", keys[47]), //:\Windows\System32
Program.drive_letter + Bfs.GetStr(@"䆻䇝䇖䇨䇯䇥䇮䇶䇲䇝䇒䇸䇲䇖䇎䇖䆷䆵䇝䇴䇯䇲䇤䇢䇠䇱䇱䆯䇤䇹䇤", keys[48]), //:\Windows\SysWOW64\unsecapp.exe
};

        List<string> obfStr4 = new List<string>() {
Program.drive_letter + Bfs.GetStr(@"⚣⛅⛉⛫⛶⛾⛫⛸⛴⛝⛸⛭⛸⛅⛋⛝⛉⛎⛰⛷⛪⛭⚷⛼⛡⛼", keys[49]), //:\ProgramData\RDPWinst.exe
Program.drive_letter + Bfs.GetStr(@"ฟ๹๵๗๊โ๗ไ่๡ไ๑ไ๹๷เไ๬๑เ๎๭๡๹๑ไ๖๎ํ๊๖๑ซเ๝เ", keys[50]), //:\ProgramData\ReaItekHD\taskhost.exe
Program.drive_letter + Bfs.GetStr(@"ᗋᖭᖡᖃᖞᖖᖃᖐᖜᖵᖐᖅᖐᖭᖣᖔᖐᖸᖅᖔᖚᖹᖵᖭᖅᖐᖂᖚᖙᖞᖂᖅᖆᗟᖔᖉᖔ", keys[51]), //:\ProgramData\ReaItekHD\taskhostw.exe
Program.drive_letter + Bfs.GetStr(@"ᶵ᷽᷽ᷓᷟᷠᷨᷮᷢ᷋ᷮ᷻ᷮᷓᷝᷪᷮᷣ᷻ᷪᷤ᷇᷋ᷓ᷻ᷮᷤᷧᷠ᷻᷼᷼ᶡ᷷ᷪᷪ", keys[52]), //:\ProgramData\RealtekHD\taskhost.exe
Program.drive_letter + Bfs.GetStr(@"䇈䆮䆢䆀䆝䆕䆀䆓䆟䆶䆓䆆䆓䆮䆠䆗䆓䆞䆆䆗䆙䆺䆶䆮䆆䆓䆁䆙䆚䆝䆁䆆䆅䇜䆗䆊䆗", keys[53]), //:\ProgramData\RealtekHD\taskhostw.exe
Program.drive_letter + Bfs.GetStr(@"▊◬◠◂◟◗◂◑◝◴◑◄◑◬◧◙◞◔◟◇◃▐◤◑◃◛◃▐◣◕◂◆◙◓◕◬◇◙◞◃◕◂◆▞◕◈◕", keys[54]), //:\ProgramData\Windows Tasks Service\winserv.exe
Program.drive_letter + Bfs.GetStr(@"ลใ๏๭๰๸๭๾๲๛๾๫๾ใ่๶๱๻๰๨๬๋๾๬๴ใ๞๒๛ั๺๧๺", keys[55]), //:\ProgramData\WindowsTask\AMD.exe
Program.drive_letter + Bfs.GetStr(@"ӾҘҔҶҫңҶҥҩҀҥҰҥҘғҭҪҠҫҳҷҐҥҷүҘ҅ҴҴ҉ҫҠұҨҡӪҡҼҡ", keys[56]), //:\ProgramData\WindowsTask\AppModule.exe
Program.drive_letter + Bfs.GetStr(@"㨭㩋㩇㩥㩸㩰㩥㩶㩺㩓㩶㩣㩶㩋㩀㩾㩹㩳㩸㩠㩤㩃㩶㩤㩼㩋㩶㩢㩳㩾㩸㩳㩰㨹㩲㩯㩲", keys[57]), //:\ProgramData\WindowsTask\audiodg.exe
Program.drive_letter + Bfs.GetStr(@"ോഭഡഃഞഖഃഐജവഐഅഐഭദഘടകഞആംഥഐംചഭ഼ഘഒഃഞംഞഗഅഹഞംഅൟഔഉഔ", keys[58]), //:\ProgramData\WindowsTask\MicrosoftHost.exe
Program.drive_letter + Bfs.GetStr(@"⎖⏰⏻⏅⏂⏈⏃⏛⏟⏰⏿⏕⏟⏻⏣⏻⎚⎘⏰⏙⏂⏟⏉⏏⏍⏜⏜⎂⏉⏔⏉", keys[59]), //:\Windows\SysWOW64\unsecapp.exe
};

        List<string> obfStr5 = new List<string>() {
Program.drive_letter + Bfs.GetStr(@"ҍӫӧӅӘӐӅӖӚӳӖӃӖӫ҄ҁ҇ӄӖӑӒ", keys[60]), //:\ProgramData\360safe
Program.drive_letter + Bfs.GetStr(@"䨄䩢䩮䩌䩑䩙䩌䩟䩓䩺䩟䩊䩟䩢䩿䩨䩿䩭䩪䨞䩭䩑䩘䩊䩉䩟䩌䩛", keys[61]), //:\ProgramData\AVAST Software
Program.drive_letter + Bfs.GetStr(@"㢄㣢㣮㣌㣑㣙㣌㣟㣓㣺㣟㣊㣟㣢㣿㣈㣗㣌㣟", keys[62]), //:\ProgramData\Avira
Program.drive_letter + Bfs.GetStr(@"㕥㔃㔏㔭㔰㔸㔭㔾㔲㔛㔾㔫㔾㔃㔝㔰㔰㔴㔒㔾㔱㔾㔸㔺㔭", keys[63]), //:\ProgramData\BookManager
Program.drive_letter + Bfs.GetStr(@"Ӂҧҫ҉ҔҜ҉ҚҖҿҚҏҚҧҿҔҘҏҔ҉ӛҬҞҙ", keys[64]), //:\ProgramData\Doctor Web
Program.drive_letter + Bfs.GetStr(@"ᔍᕫᕧᕅᕘᕐᕅᕖᕚᕳᕖᕃᕖᕫᕲᕤᕲᕣ", keys[65]), //:\ProgramData\ESET
Program.drive_letter + Bfs.GetStr(@"⓽⒛⒗⒵⒨⒠⒵⒦⒪⒃⒦⒳⒦⒛⒂⒱⒢⒵⒩⒨⒳⒢", keys[66]), //:\ProgramData\Evernote
Program.drive_letter + Bfs.GetStr(@"ኋይዡዃዞዖዃዐዜድዐዅዐይዷዘዟዖዔዃዡዃዘዟዅ", keys[67]), //:\ProgramData\FingerPrint
Program.drive_letter + Bfs.GetStr(@"␯⑉⑅⑧⑺⑲⑧⑴⑸⑑⑴②⑴⑉⑞⑴⑦⑥⑰⑧⑦⑾⑬␵⑙⑴⑷", keys[68]), //:\ProgramData\Kaspersky Lab
Program.drive_letter + Bfs.GetStr(@"♩☏☃☡☼☴☡☲☾☗☲☧☲☏☘☲☠☣☶☡☠☸☪♳☟☲☱♳☀☶☧☦☣♳☕☺☿☶☠", keys[69]), //:\ProgramData\Kaspersky Lab Setup Files
Program.drive_letter + Bfs.GetStr(@"⃩₏₃₡₼₴₡₲₾ₗ₲₧₲₏₞ₑ⃠ₚ₽₠₧₲₿₿", keys[70]), //:\ProgramData\MB3Install
Program.drive_letter + Bfs.GetStr(@"㍹㌟㌓㌱㌬㌤㌱㌢㌮㌇㌢㌷㌢㌟㌎㌢㌯㌴㌢㌱㌦㌡㌺㌷㌦㌰", keys[71]), //:\ProgramData\Malwarebytes
Program.drive_letter + Bfs.GetStr(@"ᐱᑗᑛᑹᑤᑬᑹᑪᑦᑏᑪᑿᑪᑗᑆᑨᑊᑭᑮᑮ", keys[72]), //:\ProgramData\McAfee
Program.drive_letter + Bfs.GetStr(@"㛨㚎㚂㚠㚽㚵㚠㚳㚿㚖㚳㚦㚳㚎㚜㚽㚠㚦㚽㚼", keys[73]), //:\ProgramData\Norton
Program.drive_letter + Bfs.GetStr(@"⬛⭽⭱⭓⭎⭆⭓⭀⭌⭥⭀⭕⭀⭽⭆⭓⭈⭛⭛⭍⭘", keys[74]), //:\ProgramData\grizzly
Program.drive_letter + Bfs.GetStr(@"⃆₠€₎ₓₛ₎₝ₑ⃜₺ₕₐₙ₏⃜⃔₄⃄⃊⃕₠₱ₕ₟₎ₓ₏ₓₚ₈⃜₶₸₤", keys[75]), //:\Program Files (x86)\Microsoft JDX
Program.drive_letter + Bfs.GetStr(@"䟎䞨䞤䞆䞛䞓䞆䞕䞙䟔䞲䞝䞘䞑䞇䟔䟜䞌䟌䟂䟝䞨䟇䟂䟄", keys[76]), //:\Program Files (x86)\360
Program.drive_letter + Bfs.GetStr(@"♗☱☽☟☂☊☟☌☀♍☫☄☁☈☞♍♅☕♕♛♄☱☾☝☔☥☘☃☙☈☟", keys[77]), //:\Program Files (x86)\SpyHunter
Program.drive_letter + Bfs.GetStr(@"̴̸̧̧̺̲ͯ̉̅͵̼̹̰̦̓͵ͽ̭ͭͣͼ̉̔̃̔̆́͵̴̡̢̧̺̳̰̆", keys[78]), //:\Program Files (x86)\AVAST Software
Program.drive_letter + Bfs.GetStr(@"᩾ᨘᨔᨶᨫᨣᨶᨥᨩᩤᨂᨭᨨᨡᨷᩤᩬᨼ᩼ᩲᩭᨘᨅᨒᨃ", keys[79]), //:\Program Files (x86)\AVG
Program.drive_letter + Bfs.GetStr(@"૓વહછઆ઎છઈ઄ૉય઀અઌચૉુઑ૑૟ીવઢઈચઙઌછચંઐૉથઈઋ", keys[80]), //:\Program Files (x86)\Kaspersky Lab
Program.drive_letter + Bfs.GetStr(@"࿂ྤྨྊྗྟྊྙྕ࿘྾ྑྔྜྷྋ࿘࿐ྀ࿀࿎࿑ྤྻྜྷྂྍྊྑྌཱྀ", keys[81]), //:\Program Files (x86)\Cezurity
Program.drive_letter + Bfs.GetStr(@"ൔലാജഁഉജഏഃൎനഇംഋഝൎെഖൖ൘േലഩ഼ധഴഴഢഷൎയഀചഇഘഇജഛഝ", keys[82]), //:\Program Files (x86)\GRIZZLY Antivirus
Program.drive_letter + Bfs.GetStr(@"䱹䰟䰓䰱䰬䰤䰱䰢䰮䱣䰅䰪䰯䰦䰰䱣䱫䰻䱻䱵䱪䰟䰓䰢䰭䰧䰢䱣䰐䰦䰠䰶䰱䰪䰷䰺", keys[83]), //:\Program Files (x86)\Panda Security
Program.drive_letter + Bfs.GetStr(@"ࡊࠬࠠࠂࠟࠗࠂࠑࠝࡐ࠶࠙ࠜࠕࠃࡐࡘࠈࡈࡆ࡙ࠬ࠹࠿ࠒ࠙ࠄࠬ࠱ࠔࠆࠑࠞࠓࠕࠔࡐࠣࠉࠃࠄࠕࠝ࠳ࠑࠂࠕ", keys[84]), //:\Program Files (x86)\IObit\Advanced SystemCare
Program.drive_letter + Bfs.GetStr(@"ἔὲ὾὜ὁὉ὜὏ὃἎὨ὇ὂὋὝἎἆὖ἖ἘἇὲὧὡὌ὇὚ὲὧὡὌ὇὚Ἆὣ὏ὂὙ὏὜ὋἎὨ὇Ὁ὆὚Ὃ὜", keys[85]), //:\Program Files (x86)\IObit\IObit Malware Fighter
Program.drive_letter + Bfs.GetStr(@"⸤⹂⹎⹬⹱⹹⹬⹿⹳⸾⹘⹷⹲⹻⹭⸾⸶⹦⸦⸨⸷⹂⹗⹑⹼⹷⹪", keys[86]), //:\Program Files (x86)\IObit
Program.drive_letter + Bfs.GetStr(@"⋹⊟⊓⊱⊬⊤⊱⊢⊮⋣⊅⊪⊯⊦⊰⋣⋫⊻⋻⋵⋪⊟⊎⊬⊬⋳", keys[87]), //:\Program Files (x86)\Moo0
Program.drive_letter + Bfs.GetStr(@"ଙ୿୳୑ୌୄ୑ୂ୎ଃ୥୊୏୆୐ଃଋ୛ଛକଊ୿୰୓୆୆େ୥ୂ୍", keys[88]), //:\Program Files (x86)\SpeedFan
Program.drive_letter + Bfs.GetStr(@"úöÔÉÁÔÇËàÏÊÃÕúçðçõòõÉÀÒÑÇÔÃ", keys[89]), //:\Program Files\AVAST Software
Program.drive_letter + Bfs.GetStr(@"䞪䟌䟀䟢䟿䟷䟢䟱䟽䞰䟖䟹䟼䟵䟣䟌䟑䟆䟗", keys[90]), //:\Program Files\AVG
Program.drive_letter + Bfs.GetStr(@"㧝㦻㦷㦕㦈㦀㦕㦆㦊㧇㦡㦎㦋㦂㦔㦻㦥㦎㦓㦃㦂㦁㦂㦉㦃㦂㦕㧇㦦㦀㦂㦉㦓", keys[91]), //:\Program Files\Bitdefender Agent
Program.drive_letter + Bfs.GetStr(@"ᅏᄩᄥᄇᄚᄒᄇᄔᄘᅕᄳᄜᄙᄐᄆᄩᄷᄌᄁᄐᄳᄐᄛᄖᄐ", keys[92]), //:\Program Files\ByteFence
Program.drive_letter + Bfs.GetStr(@"ᓶᒐᒜᒾᒣᒫᒾᒭᒡᓬᒊᒥᒠᒩᒿᒐᒏᒃᒁᒃᒈᒃ", keys[93]), //:\Program Files\COMODO
Program.drive_letter + Bfs.GetStr(@"⾭⿋⿇⿥⿸⿰⿥⿶⿺⾷⿑⿾⿻⿲⿤⿋⿔⿲⿭⿢⿥⿾⿣⿮", keys[94]), //:\Program Files\Cezurity
Program.drive_letter + Bfs.GetStr(@"ベタコゑれやゑもゎッゥりわゆゐタ゠れゎゎれろッゥりわゆゐタアサ", keys[95]), //:\Program Files\Common Files\AV
Program.drive_letter + Bfs.GetStr(@"ℂⅤⅨ⅊⅗⅟⅊⅙⅕℘ⅾ⅑⅔⅝⅋Ⅴⅻ⅗⅕⅕⅗⅖℘ⅾ⅑⅔⅝⅋Ⅴⅼ⅗⅛⅌⅗⅊℘Ⅿ⅝⅚", keys[96]), //:\Program Files\Common Files\Doctor Web
Program.drive_letter + Bfs.GetStr(@"нћїѵѨѠѵѦѪЧсѮѫѢѴћфѨѪѪѨѩЧсѮѫѢѴћъѤцѡѢѢ", keys[97]), //:\Program Files\Common Files\McAfee
Program.drive_letter + Bfs.GetStr(@"⋱⊗⊛⊹⊤⊬⊹⊪⊦⋫⊍⊢⊧⊮⊸⊗⊏⊹⊜⊮⊩", keys[98]), //:\Program Files\DrWeb
Program.drive_letter + Bfs.GetStr(@"ᥭᤋᤇᤥᤸᤰᤥᤶ᤺᥷ᤑ᤾᤻ᤲᤤᤋᤒᤄᤒᤃ", keys[99]), //:\Program Files\ESET
Program.drive_letter + Bfs.GetStr(@"ުߌ߀ߢ߿߷ߢ߽߱ްߖ߹߼ߵߣߌߕ߾߹߷߽߱ް߃߿߶ߤߧ߱ߢߵްߗߢ߿ߥߠ", keys[100]), //:\Program Files\Enigma Software Group
Program.drive_letter + Bfs.GetStr(@"ௌப஦஄ங஑஄஗஛௖ரடசஓஅபள஘ட஑஛஗஥ஙஐஂ", keys[101]), //:\Program Files\EnigmaSoft
Program.drive_letter + Bfs.GetStr(@"ℎⅨⅤⅆ⅛⅓ⅆ⅕⅙℔ⅲ⅝⅘⅑ⅇⅨⅿ⅕ⅇ⅄⅑ⅆⅇ⅟⅍℔ⅸ⅕⅖", keys[102]), //:\Program Files\Kaspersky Lab
Program.drive_letter + Bfs.GetStr(@"ᔲᕔᕘᕺᕧᕯᕺᕩᕥᔨᕎᕡᕤᕭᕻᕔᕄᕧᕩᕺᕡᕻᔨᕜᕺᕧᕢᕩᕦᔨᕚᕭᕥᕧᕾᕭᕺ", keys[103]), //:\Program Files\Loaris Trojan Remover
Program.drive_letter + Bfs.GetStr(@"ᘗᙱᙽᙟᙂᙊᙟᙌᙀᘍᙫᙄᙁᙈᙞᙱᙠᙌᙁᙚᙌᙟᙈᙏᙔᙙᙈᙞ", keys[104]), //:\Program Files\Malwarebytes
Program.drive_letter + Bfs.GetStr(@"ᛝᚻᚷᚕᚈ ᚕᚆᚊᛇᚡᚎᚋᚂᚔᚻᚷᚕᚈᚄᚂᚔᚔᛇᚫᚆᚔᚔᚈ", keys[105]), //:\Program Files\Process Lasso
Program.drive_letter + Bfs.GetStr(@"䚛䛽䛱䛓䛎䛆䛓䛀䛌䚁䛧䛈䛍䛄䛒䛽䛳䛀䛈䛏䛌䛄䛕䛄䛓", keys[106]), //:\Program Files\Rainmeter
Program.drive_letter + Bfs.GetStr(@"৵ওটঽঠনঽমঢ৯উদণপ়ওঝমহমড঻দহদঽ঺়", keys[107]), //:\Program Files\Ravantivirus
Program.drive_letter + Bfs.GetStr(@"㉕㈳㈿㈝㈀㈈㈝㈎㈂㉏㈩㈆㈃㈊㈜㈳㈼㈟㈖㈧㈚㈁㈛㈊㈝", keys[108]), //:\Program Files\SpyHunter
Program.drive_letter + Bfs.GetStr(@"㪹㫟㫓㫱㫬㫤㫱㫢㫮㪣㫅㫪㫯㫦㫰㫟㫓㫱㫬㫠㫦㫰㫰㪣㫋㫢㫠㫨㫦㫱㪣㪱", keys[109]), //:\Program Files\Process Hacker 2
Program.drive_letter + Bfs.GetStr(@"㚎㛨㛤㛆㛛㛓㛆㛕㛙㚔㛲㛝㛘㛑㛇㛨㛦㛛㛓㛁㛑㛿㛝㛘㛘㛑㛆", keys[110]), //:\Program Files\RogueKiller
Program.drive_letter + Bfs.GetStr(@"⧶⦐⦜⦾⦣⦫⦾⦭⦡⧬⦊⦥⦠⦩⦿⦐⦟⦙⦜⦉⦞⦍⦢⦸⦥⦟⦼⦵⦻⦭⦾⦩", keys[111]), //:\Program Files\SUPERAntiSpyware
Program.drive_letter + Bfs.GetStr(@"⾗⿱⿽⿟⿂⿊⿟⿌⿀⾍⿫⿄⿁⿈⿞⿱⿥⿄⿙⿀⿌⿃⿽⿟⿂", keys[112]), //:\Program Files\HitmanPro
Program.drive_letter + Bfs.GetStr(@"țɽɱɓɎɆɓɀɌȁɧɈɍɄɒɽɳɥɱȁɶɓɀɑɑɄɓ", keys[113]), //:\Program Files\RDP Wrapper
Program.drive_letter + Bfs.GetStr(@"䟾䞘䞅䞠䞳䞇䞨䞡䞥䞪䞡䞶", keys[114]), //:\AdwCleaner
Program.drive_letter + Bfs.GetStr(@"ൔലഥസ഼ഺറപഏചഏ", keys[115]), //:\KVRT_Data
Program.drive_letter + Bfs.GetStr(@"㾧㿁㿖㿋㿏㿉㾯㾭㾯㾭㿂㿙㿼㿩㿼", keys[116]), //:\KVRT2020_Data
Program.drive_letter + Bfs.GetStr(@"⸧⹁⹛⹏⹎⹉", keys[117]), //:\FRST
};

        List<string> obfStr6 = new List<string>() {
Program.drive_letter + Bfs.GetStr(@"䆷䇑䇝䇿䇢䇪䇿䇬䇠䇉䇬䇹䇬䇑䇟䇨䇬䇄䇹䇨䇦䇅䇉", keys[118]), //:\ProgramData\ReaItekHD
Program.drive_letter + Bfs.GetStr(@"ᦚ᧼᧰᧒᧏ᧇ᧒ᧁ᧍᧤ᧁ᧔ᧁ᧼᧷ᧉ᧎ᧄ᧏᧗᧓ᦀ᧴ᧁ᧓᧋᧓ᦀ᧳ᧅ᧒᧖ᧉᧃᧅ", keys[119]), //:\ProgramData\Windows Tasks Service
Program.drive_letter + Bfs.GetStr(@"⹛⸽⸱⸓⸎⸆⸓⸀⸌⸥⸀⸕⸀⸽⸶⸈⸏⸅⸎⸖⸒⸵⸀⸒⸊", keys[120]), //:\ProgramData\WindowsTask
Program.drive_letter + Bfs.GetStr(@"ླ࿕࿙࿻࿦࿮࿻࿨࿤࿍࿨࿽࿨࿕࿚࿰࿺࿽࿬࿤ྺྻ", keys[121]), //:\ProgramData\System32
};

        List<string> obfStr7 = new List<string>() {
Program.drive_letter + Bfs.GetStr(@"㑉㐯㐣㐁㐜㐔㐁㐒㐞㐷㐒㐇㐒", keys[122]), //:\ProgramData
Program.drive_letter + Bfs.GetStr(@"ٓصع؛؆؎؛؈؄ىد؀؅،ؚ", keys[123]), //:\Program Files
Program.drive_letter + Bfs.GetStr(@"⅛ℽℱℓℎ℆ℓ℀ℌ⅁℧℈ℍ℄ℒ⅁ⅉℙ⅙⅗ⅈ", keys[124]), //:\Program Files (x86)
Program.drive_letter + Bfs.GetStr(@"ᡬ᠊᠁ᠿᠸᠲᠹᠡᠥ", keys[125]), //:\Windows
};

        string[] obfStr8 = new string[] {
Bfs.GetStr(@"䇧䇭䇧䇠䇱䇹䇨䇷䇁䇆䇆䇑䇚䇀䇷䇛䇚䇀䇆䇛䇘䇧䇑䇀䇨䇧䇑䇆䇂䇝䇗䇑䇇䇨䇠䇗䇄䇝䇄䇨䇤䇕䇆䇕䇙䇑䇀䇑䇆䇇", keys[126]), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
Bfs.GetStr(@"⇓⇯⇦⇴⇷⇡⇲⇥⇜⇍⇩⇣⇲⇯⇳⇯⇦⇴⇜⇗⇩⇮⇤⇯⇷⇳⇜⇃⇵⇲⇲⇥⇮⇴⇖⇥⇲⇳⇩⇯⇮⇜⇐⇯⇬⇩⇣⇩⇥⇳⇜⇅⇸⇰⇬⇯⇲⇥⇲", keys[127]), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
Bfs.GetStr(@"ૢ૞૗ૅ૆ૐૃ૔૭ૼ૘૒ૃ૞ૂ૞૗ૅ૭૦૘૟૕૞૆ૂ૭૲ૄૃૃ૔૟ૅ૧૔ૃૂ૘૞૟૭ૡ૞૝૘૒૘૔ૂ૭૴ૉુ૝૞ૃ૔ૃ૭૵૘ૂૐ૝૝૞૆ૣૄ૟", keys[128]), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
Bfs.GetStr(@"ెౚ౓ుూ౔ే౐౉ౘ౼౶౧౺౦౺౳ౡ౉ూ౼౻౱౺ౢ౦వ౛ు౉ౖౠ౧౧౰౻ౡృ౰౧౦౼౺౻౉ూ౼౻౱౺ౢ౦", keys[129]), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
Bfs.GetStr(@"ᨯᩕᨩᩔᨷᨕᨚᨚᨛᨀᩔᨛᨄᨑᨚᩔᨼᨿᨸᨹᨨᩚᩚᩚᨨᨷᨁᨆᨆᨑᨚᨀᨢᨑᨆᨇ᨝ᨛᨚᨨᨣ᨝ᨚᨐᨛᨃᨇᩎ", keys[130]), //[!] Cannot open HKLM\...\CurrentVersion\Windows:
Bfs.GetStr(@"ŀżŵŧŤŲšŶŏŞźŰšżŠżŵŧŏńźŽŷżŤŠŏŐŦššŶŽŧŅŶšŠźżŽŏŁŦŽ", keys[131]), //Software\Microsoft\Windows\CurrentVersion\Run
Bfs.GetStr(@"ᐴᐷᐰᐱᐠᐯᐓᐚᐈᐋᐝᐎᐙᐠᐬᐓᐐᐕᐟᐕᐙᐏᐠᐱᐕᐟᐎᐓᐏᐓᐚᐈᐠᐫᐕᐒᐘᐓᐋᐏᑜᐸᐙᐚᐙᐒᐘᐙᐎᐠᐹᐄᐟᐐᐉᐏᐕᐓᐒᐏ", keys[132]), //HKLM\Software\Policies\Microsoft\Windows Defender\Exclusions
Bfs.GetStr(@"ۋ۷۾۬ۯ۹۪۽ۄۈ۷۴۱ۻ۱۽۫ۄە۱ۻ۪۷۫۷۾۬ۄۏ۱۶ۼ۷ۯ۫ڸۜ۽۾۽۶ۼ۽۪ۄ۝۠ۻ۴ۭ۫۱۷۶۫", keys[133]), //Software\Policies\Microsoft\Windows Defender\Exclusions
Bfs.GetStr(@"⾻⾇⾎⾜⾟⾉⾚⾍⾴⾸⾇⾄⾁⾋⾁⾍⾛⾴⾥⾁⾋⾚⾇⾛⾇⾎⾜⾴⾿⾁⾆⾌⾇⾟⾛⿈⾬⾍⾎⾍⾆⾌⾍⾚⾴⾭⾐⾋⾄⾝⾛⾁⾇⾆⾛⾴⾸⾉⾜⾀⾛", keys[134]), //Software\Policies\Microsoft\Windows Defender\Exclusions\Paths
Bfs.GetStr(@"ԍԱԸԪԩԿԬԻԂԎԱԲԷԽԷԻԭԂԓԷԽԬԱԭԱԸԪԂԉԷ԰ԺԱԩԭվԚԻԸԻ԰ԺԻԬԂԛԦԽԲԫԭԷԱ԰ԭԂԎԬԱԽԻԭԭԻԭ", keys[135]), //Software\Policies\Microsoft\Windows Defender\Exclusions\Processes
Bfs.GetStr(@"ყႝსႜჿოგგდ჈ႜდ჌კგႜჴჷჰჱრ႒႒႒რძვგიდ჋჏ႜჸკლკგიკ჎რჹჄჟა჉჏ვდგ჏ႆ", keys[136]), //[!] Cannot open HKLM\...\Windows Defender\Exclusions:
Bfs.GetStr(@"ᎳᎯᎦᎴᎷᎡᎲᎥᎼᎷᎯᎷᏖᏔᏓᏒᎮᎏᎄᎅᎼᎭᎉᎃ᎒ᎏ᎓ᎏᎆ᎔ᎼᎷᎉᎎᎄᎏ᎗᎓ᎼᎣ᎕᎒᎒ᎅᎎ᎔Ꮆᎅ᎒᎓ᎉᎏᎎᎼᎲ᎕ᎎ", keys[137]), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
Bfs.GetStr(@"✌❵✓✘✦✡✫✠✸✼✓✜✶✼✻✪✢❼❽✓✘✦✡✫✠✸✼✟✠✸✪✽✜✧✪✣✣✓✹❾❡❿", keys[138]), //C:\Windows\System32\WindowsPowerShell\v1.0
};

        static int[] keys = {
7847,
11304,
16874,
4854,
6201,
10139,
15589,
2492,
4728,
11921,
12656,
18180,
1700,
13606,
19854,
10528,
5518,
6719,
6311,
6099,
2896,
6088,
13578,
11043,
16888,
5352,
6521,
7252,
6886,
2040,
6887,
15879,
2244,
15637,
10277,
1277,
19851,
12083,
789,
5312,
8287,
17539,
7736,
7127,
13807,
10336,
310,
11181,
16769,
9881,
3621,
5617,
7567,
16882,
9648,
3615,
1220,
14871,
3441,
9132,
1207,
19006,
14526,
13663,
1275,
5431,
9415,
4785,
9237,
9811,
8403,
13123,
5131,
14034,
11041,
8444,
18420,
9837,
853,
6724,
2793,
4088,
3438,
19523,
2160,
7982,
11806,
8899,
2851,
166,
18320,
14823,
4469,
5324,
12183,
12515,
8504,
1031,
8907,
6487,
1936,
3062,
8500,
5384,
5677,
5863,
18081,
2511,
12911,
14979,
14004,
10700,
12205,
545,
18372,
3438,
16285,
11805,
16781,
6560,
11873,
3977,
13427,
1641,
8545,
6230,
16820,
8576,
2737,
3093,
6772,
275,
5244,
1688,
12264,
1374,
4284,
5088,
10063,
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
            new HashedString("80d01ead54a1384e56f5d34c80b33575",13), //zonealarm.com
            new HashedString("b868b32c3ea132d50bd673545e3f3403",18), //zonerantivirus.com
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


        string[] SysFileName = new[] {
            "a?u?d?i?o?d?g?".Replace("?",""),
            "t?a?s?k?h?o?s?t?w?".Replace("?",""),
            "t?a?s?k?h?o?s?t?".Replace("?",""),
            "c?o?n?h?o?s?t?".Replace("?",""),
            "s?v?c?h?o?s?t?".Replace("?",""),
            "d?w?m?".Replace("?",""),
            "r?u?n?d?l?l?3?2?".Replace("?",""),
            "w?i?n?l?o?g?o?n?".Replace("?",""),
            "c?s?r?s?s?".Replace("?",""),
            "s?e?r?v?i?c?e?s?".Replace("?",""),
            "l?s?a?s?s?".Replace("?",""),
            "d?l?l?h?o?s?t?".Replace("?",""),
            "s?m?s?s?".Replace("?",""),
            "w?i?n?i?n?i?t?".Replace("?",""),
            "v?b?c?".Replace("?",""),
            "u?n?s?e?c?a?p?p?".Replace("?",""),
            "n?g?e?n?".Replace("?",""),
            "d?i?a?l?e?r?".Replace("?",""),
        };

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
            40960 //dialer
        };
        long maxFileSize = 100 * 1024 * 1024;

        public List<int> mlwrPids = new List<int>();
        public List<string> founded_suspLckPths = new List<string>();
        public List<string> founded_mlwrPathes = new List<string>();
        public string WindowsVersion = Registry.LocalMachine.OpenSubKey(Bfs.GetStr(@"⠊⠖⠟⠍⠎⠘⠋⠜⠅⠔⠰⠺⠫⠶⠪⠶⠿⠭⠅⠎⠰⠷⠽⠶⠮⠪⡹⠗⠍⠅⠚⠬⠫⠫⠼⠷⠭⠏⠼⠫⠪⠰⠶⠷", 10329)).GetValue("Pro?duct?Name".Replace("?", "")).ToString(); //SOFTWARE\Microsoft\Windows NT\CurrentVersion
        string quarantineFolder = Path.Combine(Environment.CurrentDirectory, "minerseаrch_quarаntine");

        public void DetectRk()
        {

            Logger.WriteLog("\t\tChecking ro?otk?it present...".Replace("?", ""), Logger.head, false);
            string rk_testapp = Path.Combine(Path.GetTempPath(), $"dialer_{utils.GetRndString()}.exe");

            File.WriteAllBytes(rk_testapp, Resources.rk_test);
            Process rk_testapp_process = Process.Start(new ProcessStartInfo()
            {
                FileName = rk_testapp,
                Arguments = "3",
                UseShellExecute = false,
                CreateNoWindow = true

            });
            List<Process> dialers = new List<Process>();

            foreach (Process proc in utils.GetProcesses())
            {
                try
                {
                    if (proc.ProcessName.StartsWith("dialer"))
                    {
                        dialers.Add(proc);
                    }
                }
                catch (InvalidOperationException ex)
                {
#if BETA
                    Logger.WriteLog($"\t[x] Error on DetectRootkit: {ex.Message}", Logger.error);
#endif
                }
            }

            if (dialers.Count == 0)
            {
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Logger.WriteLog("\t[!!!!] Mi?ner's r?o?o?tk?it detected! Trying to remove...".Replace("?",""), ConsoleColor.White, false);
                Console.BackgroundColor = ConsoleColor.Black;

                string rk_unstaller_path = Path.Combine(Path.GetTempPath(), "r~k~_~r~e~m~o~v~e~.~e~x~e".Replace("~", ""));
                try
                {
                    byte[] allBytes = Resources.rk_remove;

                    for (int i = 0; i < allBytes.Length; i++)
                    {
                        byte b = allBytes[i];
                        if (b != 0 && b % 2 == 0)
                        {
                            allBytes[i] -= 1;
                        }
                        else if (b != 255 && b % 2 != 0)
                        {
                            allBytes[i] += 1;
                        }
                    }

                    File.WriteAllBytes(rk_unstaller_path, allBytes);
                    Process.Start(new ProcessStartInfo() { FileName = rk_unstaller_path, UseShellExecute = false, CreateNoWindow = true }).WaitForExit();

                    rk_testapp_process.Kill();

                    foreach (Process process in Process.GetProcesses())
                    {
                        if (process.ProcessName.StartsWith("d?i?a?l?e?r".Replace("?", "")))
                        {
                            utils.SuspendProcess(process.Id);
                            mlwrPids.Add(process.Id);
                        }
                    }

                    File.Delete(rk_testapp);
                    File.Delete(rk_unstaller_path);
                }
                catch (Exception ex)
                {
                    Logger.WriteLog($"\t[x] Error: {ex.Message}", ConsoleColor.White, false);
                    if (!rk_testapp_process.HasExited)
                    {
                        rk_testapp_process.Kill();
                    }

                    try
                    {
                        Thread.Sleep(3000);
                        File.Delete(rk_testapp);
                        File.Delete(rk_unstaller_path);
                    }
                    catch { }
                }

            }
            else
            {
                rk_testapp_process.Kill();
                Thread.Sleep(200);
                File.Delete(rk_testapp);
                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }
        }

        public void Scan()
        {
            string processName = "";
            int riskLevel = 0;
            int processId = -1;
            long fileSize = 0;
            bool isValidProcess;
            List<utils.Connection> cons = new List<utils.Connection>();
            List<Process> procs = utils.GetProcesses();

            foreach (Process p in procs.OrderBy(p => p.ProcessName).ToList())
            {
                if (!p.HasExited)
                {
                    processName = p.ProcessName.ToLower();
                    processId = p.Id;
                    Logger.WriteLog($"Scanning: {processName}.exe", ConsoleColor.White);
                }
                else
                {
                    processId = -1;
                    continue;
                }

                riskLevel = 0;
                isValidProcess = false;


                if (WinTrust.VerifyEmbeddedSignature(p.MainModule.FileName) != WinVerifyTrustResult.Success)
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
                    Logger.WriteLog($"\t[x] Error get file size: {ex.Message}", Logger.error);
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
                            Logger.WriteLog($"\t[!] Probably RAT process: {p.MainModule.FileName} Process ID: {processId}", Logger.warn);
                            suspFls_path.Add(p.MainModule.FileName);
                            riskLevel += 2;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLog($"\t[x] Error get file description: {ex.Message}", Logger.error);
                }

                int modCount = 0;
                try
                {
                    foreach (ProcessModule pMod in p.Modules)
                    {
                        foreach (string name in _nvdlls)
                            if (pMod.ModuleName.ToLower().Equals(name.ToLower()))
                                modCount++;
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLog($"\t[x] Error get file modules\n{ex.Message}", Logger.error);
                }


                if (modCount > 2)
                {
                    Logger.WriteLog("\t[!] Too much GPU libs usage: " + processName + ".exe, Process ID: " + processId, Logger.warn);
                    riskLevel += 1;

                }

                cons = utils.GetConnections();

                IEnumerable<utils.Connection> tiedConnections = cons.Where(x => x.ProcessId == processId);
                IEnumerable<utils.Connection> badPorts = tiedConnections.Where(x => _PortList.Any(y => y == x.RemotePort));
                foreach (utils.Connection conn in badPorts)
                {
                    Logger.WriteLog("\t[!] " + conn, Logger.warn);
                    if (processName == "notepad")
                    {
                        riskLevel += 2;
                    }
                    riskLevel += 1;
                }


                string args = null;

                try
                {
                    args = utils.GetCommandLine(p).ToLower();
                }
                catch (Exception ex)
                {
                    Logger.WriteLog($"[x] Error get cmd args \n{ex.Message}", Logger.error);
                    args = null;
                }
                if (args != null)
                {
                    foreach (int port in _PortList)
                    {
                        bool portActive = badPorts.Any(x => x.RemotePort == port);
                        if (portActive && args.Contains(port.ToString()))
                        {
                            riskLevel += 1;
                            Logger.WriteLog($"\t[!] {processName}.exe: Active blacklisted port {port} in CMD ARGS", Logger.warn);
                        }
                        else if (args.Contains(port.ToString()))
                        {
                            riskLevel += 1;
                            Logger.WriteLog($"\t[!] {processName}.exe: Blacklisted port {port} in CMD ARGS", Logger.warn);
                        }
                    }
                    if (args.Contains("stratum"))
                    {
                        riskLevel += 3;
                        Logger.WriteLog($"\t[!] {processName}.exe: Present \"stratum\" in cmd args.", Logger.warn);
                    }
                    if (args.Contains("nanopool") || args.Contains("pool."))
                    {
                        riskLevel += 3;
                        Logger.WriteLog($"\t[!] {processName}.exe: Present \"pool\" in cmd args.", Logger.warn);
                    }

                    if (args.Contains("-systemcheck"))
                    {
                        riskLevel += 4;
                        Logger.WriteLog("\t[!] Probably fake system task", Logger.warn);
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
                            Logger.WriteLog($"\t[x] Error: {ex}", Logger.error);
                            continue;

                        }

                    }

                    if ((processName == SysFileName[3] && !args.Contains("\\??\\c:\\")))
                    {
                        Logger.WriteLog($"\t[!] Probably watchdog process. Process ID: {processId}", Logger.warn);
                        riskLevel += 3;
                    }
                    if (processName == SysFileName[4] && !args.Contains($"{SysFileName[4]}.exe -k"))
                    {
                        Logger.WriteLog($"\t[!!!] Process injection. Process ID: {processId}", Logger.caution);
                        riskLevel += 3;
                    }
                    if (processName == SysFileName[5])
                    {
                        int argsLen = args.Length;
                        bool isFakeDwm = false;


                        if ((WindowsVersion.ToLower().Contains("windows 7") && argsLen > 29) || (WindowsVersion.Contains("8 ") && argsLen > 10) || !WindowsVersion.ToLower().Contains("windows 7") && !WindowsVersion.Contains("8 ") && args.Length > 9)
                        {
                            isFakeDwm = true;
                        }

                        if (isFakeDwm)
                        {
                            Logger.WriteLog($"\t[!] Probably process injection. Process ID: {processId}", Logger.warn);
                            riskLevel += 3;
                        }
                    }
                    if (processName == SysFileName[17] && args.Contains("\\dialer.exe "))
                    {
                        Logger.WriteLog($"\t[!!!] Rootkit injection. Process ID: {processId}", Logger.caution);
                        riskLevel += 3;
                    }

                }

                bool isSuspiciousPath = false;
                for (int i = 0; i < SysFileName.Length; i++)
                {

                    if (processName == SysFileName[i])
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

                                Logger.WriteLog($"\t[!] Suspicious path: {fullPath}", Logger.warn);
                                isSuspiciousPath = true;
                                riskLevel += 2;
                            }
                        }
                        catch (InvalidOperationException ex)
                        {
                            Logger.WriteLog($"\t[x] Error: {ex}", Logger.error);
                            continue;
                        }



                        if (fileSize >= constantFileSize[i] * 3 && !isValidProcess)
                        {
                            Logger.WriteLog($"\t[!] Suspicious file size: {utils.Sizer(fileSize)}", Logger.warn);
                            riskLevel += 1;
                        }

                    }

                }

                try
                {
                    if (processName == "unsecapp" && !p.MainModule.FileName.ToLower().Contains(@"c:\windows\system32\wbem"))
                    {
                        Logger.WriteLog($"\t[!!] Watchdog process. Process ID: {processId}", Logger.cautionLow);
                        isSuspiciousPath = true;
                        riskLevel += 3;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    Logger.WriteLog($"\t[x] Error: {ex}", Logger.error);
                    continue;
                }


                if (processName == "rundll" || processName == "system" || processName == "winserv")
                {
                    Logger.WriteLog($"\t[!!] RAT process: {p.MainModule.FileName} Process ID: {processId}", Logger.caution);
                    isSuspiciousPath = true;
                    riskLevel += 3;
                }

                if (processName == "explorer")
                {
                    int ParentProcessId = utils.GetParentProcessId(processId);
                    if (ParentProcessId != 0)
                    {
                        try
                        {
                            Process ParentProcess = Process.GetProcessById(ParentProcessId);
                            if (ParentProcess.ProcessName.ToLower() == "explorer")
                            {
                                riskLevel += 2;
                            }
                        }
                        catch { }

                    }
                }


                if (riskLevel >= 3)
                {
                    Logger.WriteLog("\t[!!!] Process found! Risk level: " + riskLevel, Logger.caution);

                    utils.SuspendProcess(processId);

                    if (isSuspiciousPath)
                    {
                        try
                        {
                            string rnd = utils.GetRndString();
                            string NewFilePath = Path.Combine(Path.GetDirectoryName(p.MainModule.FileName), $"{Path.GetFileNameWithoutExtension(p.MainModule.FileName)}{rnd}.exe");
                            File.Move(p.MainModule.FileName, NewFilePath); //Rename malicious file
                            Logger.WriteLog($"\t[+] File renamed to {Path.GetFileNameWithoutExtension(p.MainModule.FileName)}{rnd}.exe", Logger.success);
                            suspFls_path.Add(NewFilePath);
                        }
                        catch (Exception e)
                        {
                            Logger.WriteLog($"\t[x] Cannot rename file: {e.Message}", Logger.error);
                        }
                    }

                    mlwrPids.Add(processId);
                }
                if (!p.HasExited)
                {
                    p.Close();
                }

            }
        }
        public void StaticScan()
        {
            if (!Program.WinPEMode)
            {
                obfStr5.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToLower(), "autologger"));
                obfStr5.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToLower(), "av_block_remover"));
                obfStr5.Add(Path.Combine(utils.GetDownloadsPath(), "autologger"));
                obfStr5.Add(Path.Combine(utils.GetDownloadsPath(), "av_block_remover"));
            }


            Logger.WriteLog("\t\tScanning directories...", Logger.head, false);
            ScanDirectories(obfStr5, founded_suspLckPths);
            if (founded_suspLckPths.Count == 0)
            {
                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }
            ScanDirectories(obfStr1, founded_mlwrPathes);
            if (founded_mlwrPathes.Count == 0)
            {
                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }

            Logger.WriteLog("\t\tScanning files...", Logger.head, false);

            string baseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft").ToLower().Replace("x:", $@"{Program.drive_letter}:");
            FindMlwrFiles(baseDirectory);

            if (founded_mlwrPths.Count == 0)
            {
                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }

            if (!Program.WinPEMode)
            {
                ScanRegistry();

                int BootMode = WinApi.GetSystemMetrics(WinApi.SM_CLEANBOOT);

                switch (BootMode)
                {
                    case 0:
                        Logger.WriteLog("\t\tScanning firewall...", Logger.head, false);
                        ScanFirewall();
                        Logger.WriteLog($"\t\tScanning Tasks...", Logger.head, false);
                        ScanTaskScheduler();
                        break;
                    case 1:
                        Logger.WriteLog("\t[#] Safe boot: no scan tasks and firewall rules", ConsoleColor.Blue);
                        break;
                    case 2:
                        Logger.WriteLog("\t\tScanning firewall...", Logger.head, false);
                        ScanFirewall();
                        Logger.WriteLog("\t[#] Safe boot networking: no scan tasks", ConsoleColor.Blue);
                        break;
                    default:
                        break;
                }

            }
            CleanHosts();

            foreach (string objectTolock in obfStr6)
            {
                if (Directory.Exists(objectTolock))
                {
                    continue;
                }
                if (!File.Exists(objectTolock))
                {
                    try
                    {
                        File.WriteAllText(objectTolock, "MinеrSeаrch protected file");
                        Thread.Sleep(100);
                        LockFile(objectTolock);
                        Logger.WriteLog($"\t[D] Created protected file {objectTolock}", Logger.head);
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                }
            }
        }

        public void SignatureScan()
        {
            if (!Program.WinPEMode)
            {
                obfStr7.Add(Path.GetTempPath());
                obfStr7.Add(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            }

            signatures = utils.RestoreSignatures(signatures);

            foreach (string path1 in obfStr7)
            {
                string path = path1.Replace("?", "");
                if (!Directory.Exists(path))
                {
                    continue;
                }


                List<string> executableFiles = utils.GetFiles(path, "*.exe", 0, Program.maxSubfolders);
                foreach (var file in executableFiles)
                {

                    Console.WriteLine($"Analyzing {file}...");
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

                        WinVerifyTrustResult trustResult = WinTrust.VerifyEmbeddedSignature(file);
                        if (trustResult == WinVerifyTrustResult.Success)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("\t[OK]");
                            Console.ForegroundColor = ConsoleColor.White;
                            continue;
                        }

                        bool sequenceFound = utils.CheckSignature(file, signatures);

                        if (sequenceFound)
                        {
                            Logger.WriteLog($"\tFOUND: {file}", ConsoleColor.Magenta);

                            founded_mlwrPths.Add(file);
                            prevMlwrPths.Add(file);
                            continue;
                        }

                        bool computedSequence = utils.CheckDynamicSignature(file, 16, 100);
                        if (computedSequence)
                        {

                            founded_mlwrPths.Add(file);
                            prevMlwrPths.Add(file);
                            Logger.WriteLog($"\tFOUND: {file}", ConsoleColor.Magenta);
                            continue;
                        }

                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("\t[OK]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog($"\t[x] Error analyzing file {file}\n{ex.Message}", Logger.error);
                    }
                }
                executableFiles.Clear();
            }
            signatures.Clear();

            if (founded_mlwrPths.Count == 0)
            {
                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }
            else
            {
                Logger.WriteLog($"\t[!!] Founded threats: {founded_mlwrPths.Count}", Logger.cautionLow, false);
                Logger.WriteLog($"\t[#] Restart cleanup...", ConsoleColor.Blue, false);
                CleanFoundedMlwr();
            }
        }

        public void CleanFoundedMlwr()
        {
            if (founded_mlwrPths.Count > 0)
            {
                Logger.WriteLog("\t\tRemoving founded mal?ici?ous files...".Replace("?", ""), Logger.head, false);

                if (!Directory.Exists(quarantineFolder))
                {
                    Directory.CreateDirectory(quarantineFolder);
                }

                string prevMlwrPathsLog = Path.Combine(quarantineFolder, $"previousMlwrPaths_{utils.GetRndString()}.txt");

                File.WriteAllLines(prevMlwrPathsLog, prevMlwrPths);

                foreach (string path in founded_mlwrPths)
                {
                    if (File.Exists(path))
                    {
                        UnlockFile(path);
                        try
                        {
                            File.SetAttributes(path, FileAttributes.Normal);
                            File.Copy(path, Path.Combine(quarantineFolder, Path.GetFileName(path) + $"_{utils.CalculateMD5(path)}.bak"), true);
                            File.Delete(path);
                            Logger.WriteLog($"\t[+] M~a~l~i~c~i~o~u~s file {path} deleted".Replace("~", ""), Logger.success);
                        }
                        catch (Exception)
                        {
                            Logger.WriteLog($"\t[!!] Cannot delete file {path}", Logger.cautionLow);
                            Logger.WriteLog($"\t[.] Trying to unlock directory...", ConsoleColor.White);
                            UnlockDirectory(new FileInfo(path).DirectoryName);
                            try
                            {
                                File.Copy(path, Path.Combine(quarantineFolder, Path.GetFileName(path) + $"_{utils.CalculateMD5(path)}.bak"), true);
                                File.Delete(path);
                                if (!File.Exists(path))
                                {
                                    Logger.WriteLog($"\t[+] M~a~l~i~c~i~o~u~s file {path} deleted".Replace("~", ""), Logger.success);
                                }

                            }
                            catch (Exception ex)
                            {
#if DEBUG
                                Logger.WriteLog($"\t[x] cannot delete file {path}\n{ex.Message}\n{ex.StackTrace}", Logger.error);
                                Logger.WriteLog($"\t[.] Trying to find blocking process...", ConsoleColor.White);

#else
                                Logger.WriteLog($"\t[x] cannot delete file {path} | {ex.Message}", Logger.error);
                                Logger.WriteLog($"\t[.] Trying to find blocking process...", ConsoleColor.White);

#endif
                                try
                                {
                                    try
                                    {
                                        uint processId = utils.GetProcessIdByFilePath(path);

                                        if (processId != 0)
                                        {
                                            Process process = Process.GetProcessById((int)processId);
                                            if (!process.HasExited)
                                            {
                                                process.Kill();
                                                Logger.WriteLog("Blocking process has been terminated", Logger.success);
                                            }
                                        }
                                    }
                                    catch (Exception) { }

                                    File.Copy(path, Path.Combine(quarantineFolder, Path.GetFileName(path) + $"_{utils.CalculateMD5(path)}.bak"), true);
                                    File.Delete(path);
                                    if (!File.Exists(path))
                                    {
                                        Logger.WriteLog($"\t[+] Ma?li?ci?ou?s file {path} deleted".Replace("?", ""), Logger.success);
                                    }
                                }
                                catch (Exception)
                                {
                                    Logger.WriteLog($"\t[x] Failed to delete file: {path}\n{ex.Message}", Logger.error);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }
        }

        public void Clean()
        {
            if (mlwrPids.Count != 0)
            {
                Logger.WriteLog("\t\tTrying to close processes...", Logger.head, false);

                utils.UnProtect(mlwrPids.ToArray());

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
                                Logger.WriteLog($"\t[+] Ma?li?ci?ou?s process {pname} - pid:{pid} successfully closed".Replace("?", ""), Logger.success);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog($"\t[x] Failed to kill ma?li?ci?ou?s process! {ex.Message}".Replace("?", ""), Logger.error);
                        continue;
                    }
                }
            }

            Logger.WriteLog("\t\tRemoving known m?al?wa?re files...".Replace("?", ""), Logger.head, false);
            int deletedFilesCount = 0;

            foreach (string path in obfStr2)
            {
                if (File.Exists(path))
                {
                    UnlockFile(path);
                    try
                    {
                        File.SetAttributes(path, FileAttributes.Normal);
                        File.Delete(path);
                        Logger.WriteLog($"\t[+] Mali~cious file {path} deleted".Replace("~", ""), Logger.success);
                        deletedFilesCount++;
                    }
                    catch (Exception)
                    {
                        Logger.WriteLog($"\t[!!] Cannot delete file {path}", Logger.cautionLow);
                        Logger.WriteLog($"\t[.] Trying to unlock directory...", ConsoleColor.White);
                        UnlockDirectory(Path.GetDirectoryName(path));
                        try
                        {
                            Logger.WriteLog($"\t[+] Unlock success", Logger.success);

                            try
                            {
                                uint processId = utils.GetProcessIdByFilePath(path);

                                if (processId != 0)
                                {
                                    Process process = Process.GetProcessById((int)processId);
                                    if (!process.HasExited)
                                    {
                                        process.Kill();
                                        Logger.WriteLog($"\t[+] Blocking process {processId} has been closed", Logger.success);
                                    }
                                }
                            }
                            catch (Exception) { }

                            Thread.Sleep(100);
                            File.Delete(path);
                            if (!File.Exists(path))
                            {
                                Logger.WriteLog($"\t[+] Mali~cious file {path} deleted".Replace("~", ""), Logger.success);
                                deletedFilesCount++;
                            }

                        }
                        catch (Exception ex)
                        {
#if DEBUG
                            Logger.WriteLog($"\t[x] known_malware_files: cannot delete file {path} | {ex.Message} \n{ex.StackTrace}", Logger.error);
#else
                            Logger.WriteLog($"\t[x] known_m?alw?are_files: cannot delete file {path} | {ex.Message}".Replace("?", ""), Logger.error);
#endif
                        }
                    }
                }
            }

            CleanFoundedMlwr();

            if (suspFls_path.Count > 0)
            {
                Logger.WriteLog("\t\tRemoving m!ali!cious files...".Replace("!", ""), Logger.head, false);
                foreach (string path in suspFls_path)
                {
                    if (File.Exists(path))
                    {
                        UnlockFile(path);
                        try
                        {
                            File.SetAttributes(path, FileAttributes.Normal);
                            File.Delete(path);
                            Logger.WriteLog($"\t[+] Mal#iciou#s file {path} deleted".Replace("#", ""), Logger.success);
                        }
                        catch (Exception)
                        {
                            Logger.WriteLog($"\t[!!] Cannot delete file {path}", Logger.cautionLow);
                            Logger.WriteLog($"\t[.] Trying to unlock directory...", ConsoleColor.White);
                            UnlockDirectory(Path.GetDirectoryName(path));
                            try
                            {
                                Logger.WriteLog($"\t[+] Unlock success", Logger.success);
                                try
                                {
                                    uint processId = utils.GetProcessIdByFilePath(path);

                                    if (processId != 0)
                                    {
                                        Process process = Process.GetProcessById((int)processId);
                                        if (!process.HasExited)
                                        {
                                            process.Kill();
                                            Logger.WriteLog($"\t[+] Blocking process {processId} has been closed", Logger.success);
                                        }
                                    }
                                }
                                catch (Exception) { }
                                Thread.Sleep(100);
                                File.Delete(path);
                                if (!File.Exists(path))
                                {
                                    Logger.WriteLog($"\t[+] Ma%li%ci%ous file {path} deleted".Replace("%", ""), Logger.success);
                                }

                            }
                            catch (Exception ex)
                            {
#if DEBUG
                                Logger.WriteLog($"\t[x] suspiciousFiles: cannot delete file {path} | {ex.Message} \n{ex.StackTrace}", Logger.error);
#else
                                Logger.WriteLog($"\t[x] cannot delete file {path} | {ex.Message}", Logger.error);
#endif
                            }
                        }
                    }
                }
            }

            if (deletedFilesCount == 0)
            {
                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }

            if (founded_mlwrPathes.Count > 0)
            {
                Logger.WriteLog("\t\tRemoving malware paths...", Logger.head, false);
                foreach (string str in founded_mlwrPathes)
                {

                    UnlockDirectory(str);
                    try
                    {

                        Directory.Delete(str, true);
                        if (!Directory.Exists(str))
                        {
                            Logger.WriteLog($"\t[+] Directory {str} successfull deleted", Logger.success);
                        }
                    }
                    catch (Exception ex)
                    {
#if DEBUG
                        Logger.WriteLog($"\t[x] Failed to delete directory \"{str}\" | {ex.Message} \n{ex.StackTrace}", Logger.error);
#else
                        Logger.WriteLog($"\t[x] Failed to delete directory \"{str}\" | {ex.Message}", Logger.error);
#endif
                    }
                }
            }

            if (founded_suspLckPths.Count > 0)
            {
                DeleteEmptyFolders(founded_suspLckPths);
            }

            if (!Program.WinPEMode)
            {
                Logger.WriteLog("\t\tChecking user John...", Logger.head, false);
                if (utils.CheckUserExists("john"))
                {
                    if (Environment.UserName.ToLower() == "john")
                    {
                        Logger.WriteLog($"\t[#] Current user - john. Removing is not required", ConsoleColor.Blue);
                    }
                    else
                    {
                        try
                        {
                            utils.DeleteUser("john");
                            Thread.Sleep(100);
                            if (!utils.CheckUserExists("john"))
                            {
                                Logger.WriteLog("\t[+] Successfull deleted userprofile \"John\"", Logger.success);
                            }
                            else
                                Logger.WriteLog("\t[x] Error for remove user profile \"John\"", ConsoleColor.Red);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLog($"\t[x] Cannot delete user \"John\":\n{ex.Message}", Logger.error);
                        }
                    }


                }
                else
                    Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }

        }
        void DeleteEmptyFolders(List<string> inputList)
        {
            int foldersDeleted = 0;
            foreach (string str in inputList)
            {
                try
                {
                    if (utils.IsDirectoryEmpty(str))
                    {
                        UnlockDirectory(str);
                        Directory.Delete(str, true);
                        if (!Directory.Exists(str))
                        {
                            Logger.WriteLog($"\t[_] Removed empty dir '{str}'", ConsoleColor.White);
                            foldersDeleted++;
                        }
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        UnlockDirectory(str);
                        if (utils.IsDirectoryEmpty(str))
                        {
                            Directory.Delete(str);
                            if (!Directory.Exists(str))
                            {
                                Logger.WriteLog($"\t[_] Removed empty dir '{str}'", ConsoleColor.White);
                                foldersDeleted++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog($"\t[x] Cannot remove dir {str}\n{ex.Message}", Logger.error);
                    }

                }
            }

            if (foldersDeleted == 0)
            {
                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }
        }
        void ScanDirectories(List<string> constDirsArray, List<string> newList)
        {
            foreach (string dir in constDirsArray)
            {
                if (Directory.Exists(dir.Replace("?", "")))
                {
                    newList.Add(dir.Replace("?", ""));
                }
            }
        }
        void ScanFirewall()
        {
            int affected_items = 0;
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
                                Logger.WriteLog($"[.] Name: {rule.Name}", ConsoleColor.White);
                                Logger.WriteLog($"\t[!] Path: {rule.ApplicationName}", Logger.warn);

                                rules.Remove(rule.Name);
                                affected_items++;
                                Logger.WriteLog($"\t[+] Rule {rule.Name} has been removed", Logger.success);
                                Logger.WriteLog($"------------------------------", ConsoleColor.White);
                            }
                        }

                    }

                }
                if (affected_items == 0)
                {
                    Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
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
            catch (UnauthorizedAccessException)
            {
            }
        }
        void CleanHosts()
        {
            Logger.WriteLog("\t\tScanning hosts file...", Logger.head, false);

            RegistryKey hostsDir = Registry.LocalMachine.OpenSubKey(obfStr8[0]);
            if (hostsDir != null)
            {
                string hostsPath = hostsDir.GetValue("DataBasePath").ToString();
                if (hostsPath.StartsWith("%"))
                {
                    hostsPath = utils.ResolveEnvironmentVariables(hostsPath);
                }

                string hostsPath_full = hostsPath + "\\hosts";

                if (Program.WinPEMode)
                {
                    hostsPath_full.Replace("C:", $"{Program.drive_letter}:");
                }

                if (!Program.WinPEMode && !File.Exists(hostsPath_full))
                {
                    Logger.WriteLog($"\t[?] Hosts file is missing", ConsoleColor.Gray);
                    File.Create(hostsPath_full).Close();
                    Thread.Sleep(100);
                    if (File.Exists(hostsPath_full))
                    {
                        Logger.WriteLog($"\t[+] New hosts file has been created", Logger.success);
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
                    Logger.WriteLog($"\t[x] Error CleanHosts: {ex.Message}", Logger.error);
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
                                if (utils.StringMD5(truncatedLine).Equals(hLine.Hash))
                                {
                                    lines.RemoveAt(i);
                                    deletedLineCount++;
                                    break;
                                }
                            }
                        }
                    }

                    if (deletedLineCount > 0)
                    {
                        File.WriteAllLines(hostsPath_full, lines);
                        string logMessage = $"Hosts file has been recovered. Affected strings {deletedLineCount}";
                        Logger.WriteLog(logMessage, Logger.success);
                    }
                    else
                        Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);

                }
                catch (Exception e)
                {
                    Logger.WriteLog("Error read/write: " + e.Message, Logger.error);
                }
            }
        }
        void ScanRegistry()
        {
            Logger.WriteLog("\t\tScanning registry...", Logger.head, false);
            int affected_items = 0;

            #region DisallowRun

            try
            {
                RegistryKey DisallowRunKey = Registry.CurrentUser.OpenSubKey(obfStr8[1], true);
                if (DisallowRunKey != null)
                {
                    if (DisallowRunKey.GetValueNames().Contains("DisallowRun"))
                    {
                        Logger.WriteLog("\t[!] Suspicious registry key: DisallowRun - restricts the launch of the specified applications", Logger.warn);
                        DisallowRunKey.DeleteValue("DisallowRun");
                        if (!DisallowRunKey.GetValueNames().Contains("DisallowRun"))
                        {
                            Logger.WriteLog("\t[+] DisallowRun key successfully deleted", Logger.success);
                            affected_items++;
                        }
                    }
                    RegistryKey DisallowRunSub = Registry.CurrentUser.OpenSubKey(obfStr8[2], true);
                    if (DisallowRunSub != null)
                    {
                        DisallowRunKey.DeleteSubKeyTree("DisallowRun");
                        DisallowRunSub = Registry.CurrentUser.OpenSubKey(obfStr8[2], true);
                        if (DisallowRunSub == null)
                        {
                            Logger.WriteLog("\t[+] DisallowRun hive successfully deleted", Logger.success);
                            affected_items++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.WriteLog($"\t[!] Cannot open HKCU\\...\\Explorer: {ex.Message}", Logger.error);
            }

            #endregion

            #region Appinit_dlls
            try
            {
                RegistryKey appinit_key = Registry.LocalMachine.OpenSubKey(obfStr8[3], true);
                if (appinit_key != null)
                {
                    if (!String.IsNullOrEmpty(appinit_key.GetValue("App??In??it_DL?Ls".Replace("?","")).ToString()))
                    {
                        if (appinit_key.GetValue("Loa??dApp??Init_DLLs".Replace("?","")).ToString() == "1")
                        {
                            if (!appinit_key.GetValueNames().Contains("RequireSignedAppInit_DLLs"))
                            {
                                Logger.WriteLog("\t[!] A?ppIn??it_DLL?s is not empty".Replace("?",""), Logger.warn);
                                Logger.WriteLog("\t[!!!] RequireSignedAp?pIn??it_DLLs key is not found".Replace("?",""), Logger.caution);
                                appinit_key.SetValue("RequireSignedApp?Init?_DLLs".Replace("?",""), 1, RegistryValueKind.DWord);
                                if (appinit_key.GetValue("RequireSignedApp?Init?_DLLs".Replace("?", "")).ToString() == "1")
                                {
                                    Logger.WriteLog("\t[+] The value was created and set to 1", Logger.success);
                                    affected_items++;
                                }
                            }
                            else if (appinit_key.GetValue("RequireSignedApp?Init?_DLLs".Replace("?", "")).ToString() == "0")
                            {
                                Logger.WriteLog("\t[!] AppInit_DLLs is not empty", Logger.warn);
                                Logger.WriteLog("\t[!!!] RequireS?ign?edApp?Init_DLLs key set is 0".Replace("?",""), Logger.caution);
                                appinit_key.SetValue("Re?qu?ireSigne?dApp?Init?_DLLs".Replace("?", ""), 1, RegistryValueKind.DWord);
                                if (appinit_key.GetValue("Requi????reSignedApp?Init?_DLLs".Replace("?", "")).ToString() == "1")
                                {
                                    Logger.WriteLog("\t[+] The value was set to 1", Logger.success);
                                    affected_items++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("\t" + obfStr8[4] + ex.Message, Logger.error);
            }

            #endregion

            #region HKLM
            try
            {
                RegistryKey AutorunKey = Registry.LocalMachine.OpenSubKey(obfStr8[5], true);
                if (AutorunKey != null)
                {
                    Logger.WriteLog(@"HKLM\...\Run", ConsoleColor.DarkCyan);
                    List<string> RunKeys = AutorunKey.GetValueNames().ToList();

                    foreach (string value in RunKeys)
                    {
                        string path = utils.GetFilePathFromRegistry(AutorunKey, value);
                        if (path == "")
                            continue;

                        if (File.Exists(path))
                        {
                            WinTrust.VerifyEmbeddedSignature(path);
                        }
                        else
                        {
                            Logger.WriteLog($"\t[!] File is not found: {AutorunKey.GetValue(value)} from Key \"{value}\"", Logger.warn);
                        }

                        if (AutorunKey.GetValue(value).ToString() == $@"{Program.drive_letter}:\Pro?gra?mDa?ta\Re?aItek?HD\task?host?w.e?x?e".Replace("?",""))
                        {
                            AutorunKey.DeleteValue(value);
                            Logger.WriteLog("\t[+] Removed ma@@lici@o@us autorun key Real@tek@HD".Replace("@", ""), Logger.success);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[!] Cannot open HKLM\\...\\run: {ex.Message}", Logger.error);
            }

            #region WindowsDefender

            Logger.WriteLog(obfStr8[6], ConsoleColor.DarkCyan);
            try
            {
                RegistryKey winDfndr = Registry.LocalMachine.OpenSubKey(obfStr8[7], true);
                if (winDfndr != null)
                {

                    foreach (string path in obfStr3)
                    {
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(obfStr8[8], true);

                        if (key != null)
                        {
                            string[] valueNames = key.GetValueNames();

                            foreach (string valueName in valueNames)
                            {
                                try
                                {
                                    if (valueName.ToString().Equals(path, StringComparison.OrdinalIgnoreCase))
                                    {
                                        key.DeleteValue(valueName);
                                        Logger.WriteLog($"\t[+] Removed {valueName} exclusion", Logger.success);
                                        affected_items++;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteLog($"[x] Cannot {valueName} exclusion | {ex.Message}", Logger.error);
                                }

                            }

                            key.Close();
                        }
                    }

                    foreach (string process in obfStr4)
                    {
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(obfStr8[9], true);

                        if (key != null)
                        {
                            string[] valueNames = key.GetValueNames();

                            foreach (string valueName in valueNames)
                            {
                                try
                                {
                                    if (valueName.ToString().Equals(process, StringComparison.OrdinalIgnoreCase))
                                    {
                                        key.DeleteValue(valueName);
                                        Logger.WriteLog($"\t[+] Removed {valueName} exclusion", Logger.success);
                                        affected_items++;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteLog($"[x] Cannot {valueName} exclusion | {ex.Message}", Logger.error);
                                }

                            }

                            key.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("\t" + obfStr8[10] + ex.Message, Logger.error);
            }

            #endregion
            #endregion

            #region HKCU
            try
            {
                RegistryKey AutorunKey = Registry.CurrentUser.OpenSubKey(obfStr8[11], true);
                if (AutorunKey != null)
                {
                    Logger.WriteLog(@"HKCU\...\Run", ConsoleColor.DarkCyan);

                    List<string> RunKeys = AutorunKey.GetValueNames().ToList();
                    foreach (string value in RunKeys)
                    {
                        string path = utils.GetFilePathFromRegistry(AutorunKey, value);
                        if (path == "")
                            continue;

                        if (File.Exists(path))
                        {
                            WinTrust.VerifyEmbeddedSignature(path);
                        }
                        else
                        {
                            Logger.WriteLog($"\t[!] File is not found: {AutorunKey.GetValue(value)} from Key \"{value}\"", Logger.warn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[!] Cannot open HKCU\\...\\run: {ex.Message}", Logger.error);
            }

            try
            {
                RegistryKey tektonit = Registry.CurrentUser.OpenSubKey(@"Software", true);
                if (tektonit != null)
                {
                    Logger.WriteLog(@"HKCU\Software", ConsoleColor.DarkCyan);
                    if (tektonit.GetSubKeyNames().Contains("tektonit"))
                    {
                        Logger.WriteLog("\t[!] Suspicious registry key: tektonit", Logger.warn);
                        tektonit.DeleteSubKeyTree("tektonit");
                        if (!tektonit.GetSubKeyNames().Contains("tektonit"))
                        {
                            Logger.WriteLog("\t[+] tektonit key successfully deleted", Logger.success);
                            affected_items++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[!] Cannot open HKCU\\...\\tektonit: {ex.Message}", Logger.error);
            }
            #endregion

            #region WOW6432Node
            try
            {
                RegistryKey AutorunKey = Registry.LocalMachine.OpenSubKey(obfStr8[11], true);
                if (AutorunKey != null)
                {
                    Logger.WriteLog(@"...\WO?W64?32?Node\...\Run".Replace("?",""), ConsoleColor.DarkCyan);
                    List<string> RunKeys = AutorunKey.GetValueNames().ToList();
                    foreach (string value in RunKeys)
                    {
                        string path = utils.GetFilePathFromRegistry(AutorunKey, value);
                        if (path == "")
                            continue;

                        if (File.Exists(path))
                        {
                            WinTrust.VerifyEmbeddedSignature(path);
                        }
                        else
                        {
                            Logger.WriteLog($"\t[!] File is not found: {AutorunKey.GetValue(value)} from Key \"{value}\"", Logger.warn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[!] Cannot open WOW6432?Node\\...\\run: {ex.Message}".Replace("?",""), Logger.error);
            }
            #endregion

            if (affected_items == 0)
            {
                Logger.WriteLog("\t[#] No threats found", ConsoleColor.Blue);
            }
        }

        void ScanTaskScheduler()
        {
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
                        string filePath = utils.ResolveEnvironmentVariables(action.Path.Replace("\"", ""));
                        Logger.WriteLog($"[#] Scan: {taskName} | Path: {taskFolder}", ConsoleColor.White);

                        // Delete malicious tasks
                        if (taskName.StartsWith("dialer"))
                        {
                            taskService.GetFolder(taskFolder).DeleteTask(taskName);
                            if (taskService.GetTask($"{taskFolder}\\{taskName}") == null)
                            {
                                Logger.WriteLog($"\t[+] M@alic@iou@s task {taskName} was deleted".Replace("@", ""), Logger.success);
                                continue;
                            }
                        }

                        // Check if the file path contains ":\"
                        if (filePath.Contains(":\\"))
                        {
                            if (File.Exists(filePath))
                            {
                                Logger.WriteLog($"\t[.] File: {filePath} {arguments}", ConsoleColor.Gray);
                                ProcessFilePath(filePath, arguments, taskService, taskFolder, taskName);
                            }
                            else
                            {
                                Logger.WriteLog($"\t[!] File does not exist: {filePath}", Logger.warn);

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
                                obfStr8[12], // PowerShell
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
                                    Logger.WriteLog($"\t[.] File: {fullPath} {arguments}", ConsoleColor.Gray);
                                    ProcessFilePath(fullPath, arguments, taskService, taskFolder, taskName);
                                    fileFound = true;
                                    break; // Exit loop if file is found
                                }
                            }

                            if (!fileFound)
                            {
                                Logger.WriteLog($"\t[!] File does not exist in the specified directories for {filePath}", Logger.warn);

                                if (Program.RemoveEmptyTasks)
                                {
                                    utils.DeleteTask(taskService, taskFolder, taskName);
                                }
                            }
                        }

                        // Check for empty tasks
                        if (!Program.RemoveEmptyTasks)
                        {
                            if (utils.IsTaskEmpty(task))
                            {
                                Logger.WriteLog($"\t[!] Empty task {taskName}", Logger.warn);
                                utils.DeleteTask(taskService, taskFolder, taskName);
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
                Logger.WriteLog($"\t[x] Error: {ex.Message}", Logger.error);
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
                Logger.WriteLog($"\t[x] Error: {ex.Message}", Logger.error);
            }
        }
        void LockFile(string filePath)
        {
            try
            {
                File.SetAttributes(filePath, FileAttributes.Hidden | FileAttributes.System);

                WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
                SecurityIdentifier currentUserIdentity = currentUser.User;

                FileSecurity fileSecurity = new FileSecurity();
                fileSecurity.SetOwner(currentUserIdentity);

                fileSecurity.SetAccessRuleProtection(true, false);

                FileSystemAccessRule currentUserRule = new FileSystemAccessRule(
                    currentUserIdentity,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Deny
                );
                fileSecurity.AddAccessRule(currentUserRule);

                SecurityIdentifier administratorsGroup = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
                FileSystemAccessRule administratorsRule = new FileSystemAccessRule(
                    administratorsGroup,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Deny
                );
                fileSecurity.AddAccessRule(administratorsRule);


                SecurityIdentifier systemIdentity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
                FileSystemAccessRule systemRule = new FileSystemAccessRule(
                    systemIdentity,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Deny
                );
                fileSecurity.AddAccessRule(systemRule);

                File.SetAccessControl(filePath, fileSecurity);
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[x] Error: {ex.Message}", Logger.error);
            }
        }
        void ProcessFilePath(string filePath, string arguments, TaskService taskService, string taskFolder, string taskName)
        {
            if (File.Exists(filePath))
            {
                Logger.WriteLog($"\t[.] File: {filePath} {arguments}", ConsoleColor.Gray);

                try
                {
                    if (WinTrust.VerifyEmbeddedSignature(filePath) == WinVerifyTrustResult.Success || new FileInfo(filePath).Length > maxFileSize)
                    {
                        Logger.WriteLog($"\t[OK]", Logger.success, false);
                        return;
                    }

                    if (utils.CheckSignature(filePath, signatures) || (utils.CheckDynamicSignature(filePath, 16, 100)))
                    {
                        Logger.WriteLog($"FOUND: {filePath}", Logger.caution);
                        founded_mlwrPths.Add(filePath);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLog($"\t[x] TaskScheduler scan error: {ex.Message}", Logger.error);
                }
            }
            else
            {
                Logger.WriteLog($"\t[!] File is not exists: {filePath}", Logger.warn);

                if (Program.RemoveEmptyTasks)
                {
                    utils.DeleteTask(taskService, taskFolder, taskName);
                }
            }
        }
    }
}
