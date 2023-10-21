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

        List<string> obfStr1 = new List<string>() {
Program.drive_letter + Bfs.GetStr(@"⽂⼤⼨⼊⼗⼟⼊⼙⼕⼼⼙⼌⼙⼤⼱⼖⼋⼌⼙⼔⼔", keys[0]), //:\ProgramData\Install
Program.drive_letter + Bfs.GetStr(@"䃩䂏䂃䂡䂼䂴䂡䂲䂾䂗䂲䂧䂲䂏䂞䂺䂰䂡䂼䂠䂼䂵䂧䂏䂐䂻䂶䂰䂸", keys[1]), //:\ProgramData\Microsoft\Check
Program.drive_letter + Bfs.GetStr(@"ឣៅ៉៫៶៾៫៸៴៝៸៭៸ៅ។៰៺៫៶៪៶៿៭ៅ័៷៭៼៵", keys[2]), //:\ProgramData\Microsoft\Intel
Program.drive_letter + Bfs.GetStr(@"䫂䪤䪨䪊䪗䪟䪊䪙䪕䪼䪙䪌䪙䪤䪵䪑䪛䪊䪗䪋䪗䪞䪌䪤䪛䪔䪊䪧䪗䪈䪌䪑䪕䪑䪂䪙䪌䪑䪗䪖䪧䪎䫌䫖䫈䫖䫋䫈䫋䫉䫀䪧䫎䫌", keys[3]), //:\ProgramData\Microsoft\clr_optimization_v4.0.30318_64
Program.drive_letter + Bfs.GetStr(@"㐜㑺㑶㑔㑉㑁㑔㑇㑋㑢㑇㑒㑇㑺㑫㑏㑅㑔㑉㑕㑉㑀㑒㑺㑒㑃㑋㑖", keys[4]), //:\ProgramData\Microsoft\temp
Program.drive_letter + Bfs.GetStr(@"∓≵≹≛≆≎≛≈≄≭≈≝≈≵≹≜≓≓≅≌≤≌≍≀≈", keys[5]), //:\ProgramData\PuzzleMedia
Program.drive_letter + Bfs.GetStr(@"᦭᧋ᧇ᧥᧸᧰᧥᧶᧺᧓᧶᧣᧶᧋ᧅ᧲᧶᧻᧣᧲᧼᧟᧓", keys[6]), //:\ProgramData\RealtekHD
Program.drive_letter + Bfs.GetStr(@"䒯䓉䓅䓧䓺䓲䓧䓴䓸䓑䓴䓡䓴䓉䓇䓰䓴䓜䓡䓰䓾䓝䓑", keys[7]), //:\ProgramData\ReaItekHD
Program.drive_letter + Bfs.GetStr(@"᳁ᲧᲫᲉᲔᲜᲉᲚᲖᲿᲚ᲏ᲚᲧᲩᲔᲙᲔ᲏ᲿᲞᲖᲔ", keys[8]), //:\ProgramData\RobotDemo
Program.drive_letter + Bfs.GetStr(@"₂⃤⃨⃊⃗⃟⃊⃙⃕⃼⃙⃌⃙⃤⃪⃍⃖⃼⃴⃴", keys[9]), //:\ProgramData\RunDLL
Program.drive_letter + Bfs.GetStr(@"⤿⥙⥕⥷⥪⥢⥷⥤⥨⥁⥤⥱⥤⥙⥖⥠⥱⥰⥵", keys[10]), //:\ProgramData\Setup
Program.drive_letter + Bfs.GetStr(@"ⳂⲤⲨⲊⲗⲟⲊⲙⲕⲼⲙⲌⲙⲤⲫⲁⲋⲌⲝⲕⳋⳊ", keys[11]), //:\ProgramData\System32
Program.drive_letter + Bfs.GetStr(@"㈫㉍㉁㉣㉾㉶㉣㉰㉼㉕㉰㉥㉰㉍㉆㉰㉧㉴㉁㉰㉵", keys[12]), //:\ProgramData\WavePad
Program.drive_letter + Bfs.GetStr(@"㦙㧿㧳㧑㧌㧄㧑㧂㧎㧧㧂㧗㧂㧿㧴㧊㧍㧇㧌㧔㧐㦃㧷㧂㧐㧈㧐㦃㧰㧆㧑㧕㧊㧀㧆", keys[13]), //:\ProgramData\Windows Tasks Service
Program.drive_letter + Bfs.GetStr(@"㏍㎫㎧㎅㎘㎐㎅㎖㎚㎳㎖㎃㎖㎫㎠㎞㎙㎓㎘㎀㎄㎣㎖㎄㎜", keys[14]), //:\ProgramData\WindowsTask
Program.drive_letter + Bfs.GetStr(@"㐅㑣㑯㑍㑐㑘㑍㑞㑒㐟㑹㑖㑓㑚㑌㑣㑫㑍㑞㑑㑌㑒㑖㑌㑌㑖㑐㑑", keys[15]), //:\Program Files\Transmission
Program.drive_letter + Bfs.GetStr(@"ਗ਼਼ਰ਒ਏਇ਒ਁ਍ੀਦਉ਌ਅਓ਼ਧਏਏਇ਌ਅ਼ਬਉਂਓ", keys[16]), //:\Program Files\Google\Libs
Program.drive_letter + Bfs.GetStr(@"㵼㴚㴖㴴㴩㴡㴴㴧㴫㵦㴀㴯㴪㴣㴵㵦㵮㴾㵾㵰㵯㴚㴒㴴㴧㴨㴵㴫㴯㴵㴵㴯㴩㴨", keys[17]), //:\Program Files (x86)\Transmission
Program.drive_letter + Bfs.GetStr(@"‰⁖⁝⁣⁤⁮⁥⁽⁹⁖⁌⁥⁤⁾⁹⁖⁇⁳⁹⁻⁦", keys[18]), //:\Windows\Fonts\Mysql
Program.drive_letter + Bfs.GetStr(@"㶼㷚㷖㷴㷩㷡㷴㷧㷫㶦㷀㷯㷪㷣㷵㷚㷏㷨㷲㷣㷴㷨㷣㷲㶦㷃㷾㷶㷪㷩㷴㷣㷴㷚㷤㷯㷨", keys[19]), //:\Program Files\Internet Explorer\bin
Program.drive_letter + Bfs.GetStr(@"㕤㔂㔎㔬㔱㔹㔬㔿㔳㔚㔿㔪㔿㔂㔮㔬㔷㔰㔽㔻㔪㔱㔰㕳㔮㔬㔱㔺㔫㔽㔻", keys[20]), //:\ProgramData\princeton-produce
Program.drive_letter + Bfs.GetStr(@"䒲䓔䓘䓺䓧䓯䓺䓩䓥䓌䓩䓼䓩䓔䓜䓡䓥䓭䓽䓸䓸䓭䓺", keys[21]), //:\ProgramData\Timeupper
};

        List<string> obfStr2 = new List<string>() {
Program.drive_letter + Bfs.GetStr(@"䡚䠼䠰䠒䠏䠇䠒䠁䠍䠤䠁䠔䠁䠼䠭䠉䠃䠒䠏䠓䠏䠆䠔䠼䠗䠉䠎䡎䠅䠘䠅", keys[22]), //:\ProgramData\Microsoft\win.exe
Program.drive_letter + Bfs.GetStr(@"ᔴᕒᕞᕼᕡᕩᕼᕯᕣᔮᕈᕧᕢᕫᕽᕒᕉᕡᕡᕩᕢᕫᕒᕍᕦᕼᕡᕣᕫᕒᕻᕾᕪᕯᕺᕫᕼᔠᕫᕶᕫ", keys[23]), //:\Program Files\Google\Chrome\updater.exe
Program.drive_letter + Bfs.GetStr(@"໼ບຖິຩມິວຫຂວາວບດຂຖຑຯຨີາ໨ຣ຾ຣ", keys[24]), //:\ProgramData\RDPWinst.exe
Program.drive_letter + Bfs.GetStr(@"㿝㾻㾷㾕㾈㾀㾕㾆㾊㾣㾆㾓㾆㾻㾵㾂㾆㾮㾓㾂㾌㾯㾣㾻㾓㾆㾔㾌㾏㾈㾔㾓㿉㾂㾟㾂", keys[25]), //:\ProgramData\ReaItekHD\taskhost.exe
Program.drive_letter + Bfs.GetStr(@"㱯㰉㰅㰧㰺㰲㰧㰴㰸㰑㰴㰡㰴㰉㰇㰰㰴㰜㰡㰰㰾㰝㰑㰉㰡㰴㰦㰾㰽㰺㰦㰡㰢㱻㰰㰭㰰", keys[26]), //:\ProgramData\ReaItekHD\taskhostw.exe
Program.drive_letter + Bfs.GetStr(@"䢪䣌䣀䣢䣿䣷䣢䣱䣽䣔䣱䣤䣱䣌䣂䣵䣱䣼䣤䣵䣻䣘䣔䣌䣤䣱䣣䣻䣸䣿䣣䣤䢾䣵䣨䣵", keys[27]), //:\ProgramData\RealtekHD\taskhost.exe
Program.drive_letter + Bfs.GetStr(@"̵̵̶̬̱̹̬̳̪̻̲̪̻̖̪̭̱̭̪̩ͤ̂̎̿̿̿̂̌̿̂̿̚̚Ͱ̻̦̻", keys[28]), //:\ProgramData\RealtekHD\taskhostw.exe
Program.drive_letter + Bfs.GetStr(@"⬤⭂⭎⭬⭱⭹⭬⭿⭳⭚⭿⭪⭿⭂⭉⭷⭰⭺⭱⭩⭭⬾⭊⭿⭭⭵⭭⬾⭍⭻⭬⭨⭷⭽⭻⭂⭩⭷⭰⭭⭻⭬⭨⬰⭻⭦⭻", keys[29]), //:\ProgramData\Windows Tasks Service\winserv.exe
Program.drive_letter + Bfs.GetStr(@"ዧኁኍኯኲኺኯኼኰኙኼኩኼኁኊኴኳኹኲኪኮ኉ኼኮ኶ኁኜነኙዳኸእኸ", keys[30]), //:\ProgramData\WindowsTask\AMD.exe
Program.drive_letter + Bfs.GetStr(@"ᔔᕲᕾᕜᕁᕉᕜᕏᕃᕪᕏᕚᕏᕲᕹᕇᕀᕊᕁᕙᕝᕺᕏᕝᕅᕲᕯᕞᕞᕣᕁᕊᕛᕂᕋᔀᕋᕖᕋ", keys[31]), //:\ProgramData\WindowsTask\AppModule.exe
Program.drive_letter + Bfs.GetStr(@"̷͑͝Ϳͪ͢Ϳ͉ͬͬ͠͹͚ͬ͑ͤͣͩ͢ͺ;͙ͬ;ͦ͑ͬ͸̣ͩͤͩͪͨ͢͵ͨ", keys[32]), //:\ProgramData\WindowsTask\audiodg.exe
Program.drive_letter + Bfs.GetStr(@"䮩䯏䯃䯡䯼䯴䯡䯲䯾䯗䯲䯧䯲䯏䯄䯺䯽䯷䯼䯤䯠䯇䯲䯠䯸䯏䯞䯺䯰䯡䯼䯠䯼䯵䯧䯛䯼䯠䯧䮽䯶䯫䯶", keys[33]), //:\ProgramData\WindowsTask\MicrosoftHost.exe
Program.drive_letter + Bfs.GetStr(@"ᇆᆠᆫᆕᆒᆘᆓᆋᆏᆠᆯᆅᆏᆫᆳᆫᇊᇈᆠᆉᆒᆏᆙᆟᆝᆌᆌᇒᆙᆄᆙ", keys[34]), //:\Windows\SysWOW64\unsecapp.exe
Program.drive_letter + Bfs.GetStr(@"㴉㵯㵣㵁㵜㵔㵁㵒㵞㵷㵒㵇㵒㵯㵧㵚㵞㵖㵆㵃㵃㵖㵁㵯㵻㵥㵣㵺㵼㴝㵖㵋㵖", keys[35]), //:\ProgramData\Timeupper\HVPIO.exe
};

        List<string> obfStr3 = new List<string>() {
Program.drive_letter + Bfs.GetStr(@"䬦䭀䭌䭮䭳䭻䭮䭽䭱䬼䭚䭵䭰䭹䭯䭀䭎䭘䭌䬼䭋䭮䭽䭬䭬䭹䭮", keys[36]), //:\Program Files\RDP Wrapper
Program.drive_letter + Bfs.GetStr(@"బొె౤౹౱౤౷౻౒౷ౢ౷", keys[37]), //:\ProgramData
Program.drive_letter + Bfs.GetStr(@"䘷䙑䙝䙿䙢䙪䙿䙬䙠䙉䙬䙹䙬䙑䙟䙨䙬䙄䙹䙨䙦䙅䙉䙑䙹䙬䙾䙦䙥䙢䙾䙹䘣䙨䙵䙨", keys[38]), //:\ProgramData\ReaItekHD\taskhost.exe
Program.drive_letter + Bfs.GetStr(@"㐋㑭㑡㑃㑞㑖㑃㑐㑜㑵㑐㑅㑐㑭㑣㑔㑐㑸㑅㑔㑚㑹㑵㑭㑅㑐㑂㑚㑙㑞㑂㑅㑆㐟㑔㑉㑔", keys[39]), //:\ProgramData\ReaItekHD\taskhostw.exe
Program.drive_letter + Bfs.GetStr(@"䁕䀳䀿䀝䀀䀈䀝䀎䀂䀫䀎䀛䀎䀳䀽䀊䀎䀃䀛䀊䀄䀧䀫䀳䀛䀎䀜䀄䀇䀀䀜䀛䁁䀊䀗䀊", keys[40]), //:\ProgramData\RealtekHD\taskhost.exe
Program.drive_letter + Bfs.GetStr(@"ၷထဝဿဢဪဿာဠဉာ္ာထဟဨာင္ဨဦစဉထ္ာှဦဥဢှ္်ၣဨဵဨ", keys[41]), //:\ProgramData\ReaItekHD\taskhostw.exe
Program.drive_letter + Bfs.GetStr(@"㳖㲰㲼㲞㲃㲋㲞㲍㲁㲨㲍㲘㲍㲰㲻㲅㲂㲈㲃㲛㲟㳌㲸㲍㲟㲇㲟㳌㲿㲉㲞㲚㲅㲏㲉㲰㲛㲅㲂㲟㲉㲞㲚㳂㲉㲔㲉", keys[42]), //:\ProgramData\Windows Tasks Service\winserv.exe
Program.drive_letter + Bfs.GetStr(@"ᘟᙹᙵᙗᙊᙂᙗᙄᙈᙡᙄᙑᙄᙹᙲᙌᙋᙁᙊᙒᙖᙱᙄᙖᙎᙹᙤᙨᙡᘋᙀᙝᙀ", keys[43]), //:\ProgramData\WindowsTask\AMD.exe
Program.drive_letter + Bfs.GetStr(@"ಖ೰೼ೞೃೋೞ್ು೨್೘್೰೻೅ೂೈೃ೛೟೸್೟ೇ೰೭೜೜ೡೃೈ೙ೀ೉ಂ೉೔೉", keys[44]), //:\ProgramData\WindowsTask\AppModule.exe
Program.drive_letter + Bfs.GetStr(@"⹴⸒⸞⸼⸡⸩⸼ⸯ⸣⸊ⸯ⸺ⸯ⸒⸙⸧⸠⸪⸡⸹⸽⸚ⸯ⸽⸥⸒ⸯ⸻⸪⸧⸡⸪⸩⹠⸫⸶⸫", keys[45]), //:\ProgramData\WindowsTask\audiodg.exe
Program.drive_letter + Bfs.GetStr(@"ಉ೯ೣು೜೔ು೒ೞ೷೒ೇ೒೯೤೚ೝ೗೜ೄೀ೧೒ೀ೘೯೾೚೐ು೜ೀ೜ೕೇ೻೜ೀೇಝೖೋೖ", keys[46]), //:\ProgramData\WindowsTask\MicrosoftHost.exe
Program.drive_letter + Bfs.GetStr(@"㮩㯏㯄㯺㯽㯷㯼㯤㯠㯏㯀㯪㯠㯧㯶㯾㮠㮡", keys[47]), //:\Windows\System32
Program.drive_letter + Bfs.GetStr(@"䳿䲙䲒䲬䲫䲡䲪䲲䲶䲙䲖䲼䲶䲒䲊䲒䳳䳱䲙䲰䲫䲶䲠䲦䲤䲵䲵䳫䲠䲽䲠", keys[48]), //:\Windows\SysWOW64\unsecapp.exe
};

        List<string> obfStr4 = new List<string>() {
Program.drive_letter + Bfs.GetStr(@"ᮈᯮᯢᯀᯝᯕᯀᯓᯟ᯶ᯓᯆᯓᯮᯠ᯶ᯢᯥᯛᯜᯁᯆᮜᯗᯊᯗ", keys[49]), //:\ProgramData\RDPWinst.exe
Program.drive_letter + Bfs.GetStr(@"㆐ㇶㇺ㇘㇅㇍㇘㇋㇇㇮㇋㇞㇋ㇶㇸ㇏㇋㇣㇞㇏㇁㇢㇮ㇶ㇞㇋㇙㇁㇂㇅㇙㇞ㆄ㇏㇒㇏", keys[50]), //:\ProgramData\ReaItekHD\taskhost.exe
Program.drive_letter + Bfs.GetStr(@"⦜⧺⧶⧔⧉⧁⧔⧇⧋⧢⧇⧒⧇⧺⧴⧃⧇⧯⧒⧃⧍⧮⧢⧺⧒⧇⧕⧍⧎⧉⧕⧒⧑⦈⧃⧞⧃", keys[51]), //:\ProgramData\ReaItekHD\taskhostw.exe
Program.drive_letter + Bfs.GetStr(@"㑲㐔㐘㐺㐧㐯㐺㐩㐥㐌㐩㐼㐩㐔㐚㐭㐩㐤㐼㐭㐣㐀㐌㐔㐼㐩㐻㐣㐠㐧㐻㐼㑦㐭㐰㐭", keys[52]), //:\ProgramData\RealtekHD\taskhost.exe
Program.drive_letter + Bfs.GetStr(@"ಋ೭ೡೃೞೖೃ೐೜೵೐೅೐೭ೣ೔೐ೝ೅೔೚೹೵೭೅೐ೂ೚೙ೞೂ೅ೆಟ೔೉೔", keys[53]), //:\ProgramData\RealtekHD\taskhostw.exe
Program.drive_letter + Bfs.GetStr(@"䏙䎿䎳䎑䎌䎄䎑䎂䎎䎧䎂䎗䎂䎿䎴䎊䎍䎇䎌䎔䎐䏃䎷䎂䎐䎈䎐䏃䎰䎆䎑䎕䎊䎀䎆䎿䎔䎊䎍䎐䎆䎑䎕䏍䎆䎛䎆", keys[54]), //:\ProgramData\Windows Tasks Service\winserv.exe
Program.drive_letter + Bfs.GetStr(@"䛙䚿䚳䚑䚌䚄䚑䚂䚎䚧䚂䚗䚂䚿䚴䚊䚍䚇䚌䚔䚐䚷䚂䚐䚈䚿䚢䚮䚧䛍䚆䚛䚆", keys[55]), //:\ProgramData\WindowsTask\AMD.exe
Program.drive_letter + Bfs.GetStr(@"׺֜֐ֲֲ֧֭֯֡քֳֳִַ֤֮֡֡֜֗֩֯֔֡֫֜ցְְ֍ֵ֤֥֯֬׮ָ֥֥", keys[56]), //:\ProgramData\WindowsTask\AppModule.exe
Program.drive_letter + Bfs.GetStr(@"㽱㼗㼛㼹㼤㼬㼹㼪㼦㼏㼪㼿㼪㼗㼜㼢㼥㼯㼤㼼㼸㼟㼪㼸㼠㼗㼪㼾㼯㼢㼤㼯㼬㽥㼮㼳㼮", keys[57]), //:\ProgramData\WindowsTask\audiodg.exe
Program.drive_letter + Bfs.GetStr(@"࿄ྡྷྮྌྑྙྌྟྒྷྺྟྊྟྡྷྩྗྐྚྑྉྍྪྟྍྕྡྷླྗྜྷྌྑྍྑ྘ྊྶྑྍྊ࿐ྛ྆ྛ", keys[58]), //:\ProgramData\WindowsTask\MicrosoftHost.exe
Program.drive_letter + Bfs.GetStr(@"৕঳সআঁঋঀঘজ঳়খজসঠস৙৛঳চঁজঊঌ঎টটুঊগঊ", keys[59]), //:\Windows\SysWOW64\unsecapp.exe
};

        List<string> obfStr5 = new List<string>() {
Program.drive_letter + Bfs.GetStr(@"᷾ᶘᶔᶶᶫᶣᶶᶥᶩᶀᶥᶰᶥᶘ᷷ᷲᷴᶷᶥᶢᶡ", keys[60]), //:\ProgramData\360safe
Program.drive_letter + Bfs.GetStr(@"๿นตืสยืฤศกฤัฤนคณคถฑ๥ถสรัาฤืภ", keys[61]), //:\ProgramData\AVAST Software
Program.drive_letter + Bfs.GetStr(@"১ঁ঍যল঺য়রঙ়঩়ঁজফ঴য়", keys[62]), //:\ProgramData\Avira
Program.drive_letter + Bfs.GetStr(@"㛳㚕㚙㚻㚦㚮㚻㚨㚤㚍㚨㚽㚨㚕㚋㚦㚦㚢㚄㚨㚧㚨㚮㚬㚻", keys[63]), //:\ProgramData\BookManager
Program.drive_letter + Bfs.GetStr(@"㹶㸐㸜㸾㸣㸫㸾㸭㸡㸈㸭㸸㸭㸐㸈㸣㸯㸸㸣㸾㹬㸛㸩㸮", keys[64]), //:\ProgramData\Doctor Web
Program.drive_letter + Bfs.GetStr(@"⬽⭛⭗⭵⭨⭠⭵⭦⭪⭃⭦⭳⭦⭛⭂⭔⭂⭓", keys[65]), //:\ProgramData\ESET
Program.drive_letter + Bfs.GetStr(@"㪆㫠㫬㫎㫓㫛㫎㫝㫑㫸㫝㫈㫝㫠㫹㫊㫙㫎㫒㫓㫈㫙", keys[66]), //:\ProgramData\Evernote
Program.drive_letter + Bfs.GetStr(@"ǤƂƎƬƱƹƬƿƳƚƿƪƿƂƘƷưƹƻƬƎƬƷưƪ", keys[67]), //:\ProgramData\FingerPrint
Program.drive_letter + Bfs.GetStr(@"ᾣ῅ΈΎῶ῾ΎῸῴ῝Ὸ῭Ὸ῅ῒῸῪῩῼΎῪῲῠᾹ῕ῸΏ", keys[68]), //:\ProgramData\Kaspersky Lab
Program.drive_letter + Bfs.GetStr(@"৬ঊআতহ঱তষ঻঒ষঢষঊঝষথদ঳তথঽয৶চষ঴৶অ঳ঢণদ৶ঐি঺঳থ", keys[69]), //:\ProgramData\Kaspersky Lab Setup Files
Program.drive_letter + Bfs.GetStr(@"ᙤᘂᘎᘬᘱᘹᘬᘿᘳᘚᘿᘪᘿᘂᘓᘜ᙭ᘗᘰᘭᘪᘿᘲᘲ", keys[70]), //:\ProgramData\MB3Install
Program.drive_letter + Bfs.GetStr(@"䲦䳀䳌䳮䳳䳻䳮䳽䳱䳘䳽䳨䳽䳀䳑䳽䳰䳫䳽䳮䳹䳾䳥䳨䳹䳯", keys[71]), //:\ProgramData\Malwarebytes
Program.drive_letter + Bfs.GetStr(@"㬻㭝㭑㭳㭮㭦㭳㭠㭬㭅㭠㭵㭠㭝㭌㭢㭀㭧㭤㭤", keys[72]), //:\ProgramData\McAfee
Program.drive_letter + Bfs.GetStr(@"୫଍ଁଣାଶଣର଼କରଥର଍ଟାଣଥାି", keys[73]), //:\ProgramData\Norton
Program.drive_letter + Bfs.GetStr(@"ݨ܎܂ܠܽܵܠܳܿܖܳܦܳ܎ܵܠܻܨܨܾܫ", keys[74]), //:\ProgramData\grizzly
Program.drive_letter + Bfs.GetStr(@"䝆䜠䜬䜎䜓䜛䜎䜝䜑䝜䜺䜕䜐䜙䜏䝜䝔䜄䝄䝊䝕䜠䜱䜕䜟䜎䜓䜏䜓䜚䜈䝜䜶䜸䜤", keys[75]), //:\Program Files (x86)\Microsoft JDX
Program.drive_letter + Bfs.GetStr(@"֜׺׶ה׉ׁהׇ׋ֆנ׏׊׃וֆ֎מ֞֐֏׺֕֐֖", keys[76]), //:\Program Files (x86)\360
Program.drive_letter + Bfs.GetStr(@"ȆɠɬɎɓɛɎɝɑȜɺɕɐəɏȜȔɄȄȊȕɠɯɌɅɴɉɒɈəɎ", keys[77]), //:\Program Files (x86)\SpyHunter
Program.drive_letter + Bfs.GetStr(@"ᨼᩚᩖᩴᩩᩡᩴᩧᩫᨦᩀᩯᩪᩣ᩵ᨦᨮ᩾ᨾᨰᨯᩚᩇᩐᩇᩕᩒᨦᩕᩩ᩠ᩲᩱᩧᩴᩣ", keys[78]), //:\Program Files (x86)\AVAST Software
Program.drive_letter + Bfs.GetStr(@"㠓㡵㡹㡛㡆㡎㡛㡈㡄㠉㡯㡀㡅㡌㡚㠉㠁㡑㠑㠟㠀㡵㡨㡿㡮", keys[79]), //:\Program Files (x86)\AVG
Program.drive_letter + Bfs.GetStr(@"ᴔᵲᵾᵜᵁᵉᵜᵏᵃᴎᵨᵇᵂᵋᵝᴎᴆᵖᴖᴘᴇᵲᵥᵏᵝᵞᵋᵜᵝᵅᵗᴎᵢᵏᵌ", keys[80]), //:\Program Files (x86)\Kaspersky Lab
Program.drive_letter + Bfs.GetStr(@"⪹⫟⫓⫱⫬⫤⫱⫢⫮⪣⫅⫪⫯⫦⫰⪣⪫⫻⪻⪵⪪⫟⫀⫦⫹⫶⫱⫪⫷⫺", keys[81]), //:\Program Files (x86)\Cezurity
Program.drive_letter + Bfs.GetStr(@"䳲䲔䲘䲺䲧䲯䲺䲩䲥䳨䲎䲡䲤䲭䲻䳨䳠䲰䳰䳾䳡䲔䲏䲚䲁䲒䲒䲄䲑䳨䲉䲦䲼䲡䲾䲡䲺䲽䲻", keys[82]), //:\Program Files (x86)\GRIZZLY Antivirus
Program.drive_letter + Bfs.GetStr(@"㯌㮪㮦㮄㮙㮑㮄㮗㮛㯖㮰㮟㮚㮓㮅㯖㯞㮎㯎㯀㯟㮪㮦㮗㮘㮒㮗㯖㮥㮓㮕㮃㮄㮟㮂㮏", keys[83]), //:\Program Files (x86)\Panda Security
Program.drive_letter + Bfs.GetStr(@"䱁䰧䰫䰉䰔䰜䰉䰚䰖䱛䰽䰒䰗䰞䰈䱛䱓䰃䱃䱍䱒䰧䰲䰴䰙䰒䰏䰧䰺䰟䰍䰚䰕䰘䰞䰟䱛䰨䰂䰈䰏䰞䰖䰸䰚䰉䰞", keys[84]), //:\Program Files (x86)\IObit\Advanced SystemCare
Program.drive_letter + Bfs.GetStr(@"㩶㨐㨜㨾㨣㨫㨾㨭㨡㩬㨊㨥㨠㨩㨿㩬㩤㨴㩴㩺㩥㨐㨅㨃㨮㨥㨸㨐㨅㨃㨮㨥㨸㩬㨁㨭㨠㨻㨭㨾㨩㩬㨊㨥㨫㨤㨸㨩㨾", keys[85]), //:\Program Files (x86)\IObit\IObit Malware Fighter
Program.drive_letter + Bfs.GetStr(@"䷙䶿䶳䶑䶌䶄䶑䶂䶎䷃䶥䶊䶏䶆䶐䷃䷋䶛䷛䷕䷊䶿䶪䶬䶁䶊䶗", keys[86]), //:\Program Files (x86)\IObit
Program.drive_letter + Bfs.GetStr(@"⟨➎➂➠➽➵➠➳➿⟲➔➻➾➷➡⟲⟺➪⟪⟤⟻➎➟➽➽⟢", keys[87]), //:\Program Files (x86)\Moo0
Program.drive_letter + Bfs.GetStr(@"䷒䶴䶸䶚䶇䶏䶚䶉䶅䷈䶮䶁䶄䶍䶛䷈䷀䶐䷐䷞䷁䶴䶻䶘䶍䶍䶌䶮䶉䶆", keys[88]), //:\Program Files (x86)\SpeedFan
Program.drive_letter + Bfs.GetStr(@"⤣⥅⥉⥫⥶⥾⥫⥸⥴⤹⥟⥰⥵⥼⥪⥅⥘⥏⥘⥊⥍⤹⥊⥶⥿⥭⥮⥸⥫⥼", keys[89]), //:\Program Files\AVAST Software
Program.drive_letter + Bfs.GetStr(@"ߠކފި޵޽ި޻޷ߺޜ޳޶޿ީކޛތޝ", keys[90]), //:\Program Files\AVG
Program.drive_letter + Bfs.GetStr(@"䂒䃴䃸䃚䃇䃏䃚䃉䃅䂈䃮䃁䃄䃍䃛䃴䃪䃁䃜䃌䃍䃎䃍䃆䃌䃍䃚䂈䃩䃏䃍䃆䃜", keys[91]), //:\Program Files\Bitdefender Agent
Program.drive_letter + Bfs.GetStr(@"⺪⻌⻀⻢⻿⻷⻢⻱⻽⺰⻖⻹⻼⻵⻣⻌⻒⻩⻤⻵⻖⻵⻾⻳⻵", keys[92]), //:\Program Files\ByteFence
Program.drive_letter + Bfs.GetStr(@"᝹ᜟᜓᜱᜬᜤᜱᜢᜮᝣᜅᜪᜯᜦᜰᜟᜀᜌᜎᜌᜇᜌ", keys[93]), //:\Program Files\COMODO
Program.drive_letter + Bfs.GetStr(@"㼄㽢㽮㽌㽑㽙㽌㽟㽓㼞㽸㽗㽒㽛㽍㽢㽽㽛㽄㽋㽌㽗㽊㽇", keys[94]), //:\Program Files\Cezurity
Program.drive_letter + Bfs.GetStr(@"⚜⛺⛶⛔⛉⛁⛔⛇⛋⚆⛠⛏⛊⛃⛕⛺⛥⛉⛋⛋⛉⛈⚆⛠⛏⛊⛃⛕⛺⛧⛰", keys[95]), //:\Program Files\Common Files\AV
Program.drive_letter + Bfs.GetStr(@"㷉㶯㶣㶁㶜㶔㶁㶒㶞㷓㶵㶚㶟㶖㶀㶯㶰㶜㶞㶞㶜㶝㷓㶵㶚㶟㶖㶀㶯㶷㶜㶐㶇㶜㶁㷓㶤㶖㶑", keys[96]), //:\Program Files\Common Files\Doctor Web
Program.drive_letter + Bfs.GetStr(@"㭚㬼㬰㬒㬏㬇㬒㬁㬍㭀㬦㬉㬌㬅㬓㬼㬣㬏㬍㬍㬏㬎㭀㬦㬉㬌㬅㬓㬼㬭㬃㬡㬆㬅㬅", keys[97]), //:\Program Files\Common Files\McAfee
Program.drive_letter + Bfs.GetStr(@"߀ަުވޕޝވޛޗߚ޼ޓޖޟމަ޾ވޭޟޘ", keys[98]), //:\Program Files\DrWeb
Program.drive_letter + Bfs.GetStr(@"䐩䑏䑃䑡䑼䑴䑡䑲䑾䐳䑕䑺䑿䑶䑠䑏䑖䑀䑖䑇", keys[99]), //:\Program Files\ESET
Program.drive_letter + Bfs.GetStr(@"៺វថឲឯឧឲឡឭ០ឆឩឬឥឳវចឮឩឧឭឡ០នឯឦ឴ិឡឲឥ០ជឲឯ឵ឰ", keys[100]), //:\Program Files\Enigma Software Group
Program.drive_letter + Bfs.GetStr(@"ຘ໾໲໐ໍ໅໐ໃ໏ຂ໤໋໎໇໑໾໧໌໋໅໏ໃ໱ໍໄ໖", keys[101]), //:\Program Files\EnigmaSoft
Program.drive_letter + Bfs.GetStr(@"᪎᫨᫤᫆᫛᫓᫆᫕᫙᪔᫲᫝᫘᫑᫇᫨᫿᫕᫄᫇᫑᫆᫇᫟ᫍ᪔᫸᫕᫖", keys[102]), //:\Program Files\Kaspersky Lab
Program.drive_letter + Bfs.GetStr(@"㎱㏗㏛㏹㏤㏬㏹㏪㏦㎫㏍㏢㏧㏮㏸㏗㏇㏤㏪㏹㏢㏸㎫㏟㏹㏤㏡㏪㏥㎫㏙㏮㏦㏤㏽㏮㏹", keys[103]), //:\Program Files\Loaris Trojan Remover
Program.drive_letter + Bfs.GetStr(@"㵫㴍㴁㴣㴾㴶㴣㴰㴼㵱㴗㴸㴽㴴㴢㴍㴜㴰㴽㴦㴰㴣㴴㴳㴨㴥㴴㴢", keys[104]), //:\Program Files\Malwarebytes
Program.drive_letter + Bfs.GetStr(@"ʃ˥˩ˋ˖˞ˋ˘˔ʙ˿ː˕˜ˊ˥˩ˋ˖˚˜ˊˊʙ˵˘ˊˊ˖", keys[105]), //:\Program Files\Process Lasso
Program.drive_letter + Bfs.GetStr(@"⇝↻↷↕ↈↀ↕ↆ↊⇇↡↎↋ↂ↔↻↵ↆ↎↉↊ↂ↓ↂ↕", keys[106]), //:\Program Files\Rainmeter
Program.drive_letter + Bfs.GetStr(@"㝸㜞㜒㜰㜭㜥㜰㜣㜯㝢㜄㜫㜮㜧㜱㜞㜐㜣㜴㜣㜬㜶㜫㜴㜫㜰㜷㜱", keys[107]), //:\Program Files\Ravantivirus
Program.drive_letter + Bfs.GetStr(@"䳭䲋䲇䲥䲸䲰䲥䲶䲺䳷䲑䲾䲻䲲䲤䲋䲄䲧䲮䲟䲢䲹䲣䲲䲥", keys[108]), //:\Program Files\SpyHunter
Program.drive_letter + Bfs.GetStr(@"̨͔̲̜̜͎̝̲̜̝̝͎̦̜͎̾́̉̏̃̇̂̋̾́̍̋̏̍̅̋͜", keys[109]), //:\Program Files\Process Hacker 2
Program.drive_letter + Bfs.GetStr(@"⩗⨱⨽⨟⨂⨊⨟⨌⨀⩍⨫⨄⨁⨈⨞⨱⨿⨂⨊⨘⨈⨦⨄⨁⨁⨈⨟", keys[110]), //:\Program Files\RogueKiller
Program.drive_letter + Bfs.GetStr(@"䳱䲗䲛䲹䲤䲬䲹䲪䲦䳫䲍䲢䲧䲮䲸䲗䲘䲞䲛䲎䲙䲊䲥䲿䲢䲘䲻䲲䲼䲪䲹䲮", keys[111]), //:\Program Files\SUPERAntiSpyware
Program.drive_letter + Bfs.GetStr(@"⪡⫇⫋⫩⫴⫼⫩⫺⫶⪻⫝⫲⫷⫾⫨⫇⫓⫲⫯⫶⫺⫵⫋⫩⫴", keys[112]), //:\Program Files\HitmanPro
Program.drive_letter + Bfs.GetStr(@"⚒⛴⛸⛚⛇⛏⛚⛉⛅⚈⛮⛁⛄⛍⛛⛴⛺⛬⛸⚈⛿⛚⛉⛘⛘⛍⛚", keys[113]), //:\Program Files\RDP Wrapper
Program.drive_letter + Bfs.GetStr(@"䶜䷺䷧䷂䷑䷥䷊䷃䷇䷈䷃䷔", keys[114]), //:\AdwCleaner
Program.drive_letter + Bfs.GetStr(@"ⴢⵄⵓⵎⵊⵌⵇⵜ⵹⵬⵹", keys[115]), //:\KVRT_Data
Program.drive_letter + Bfs.GetStr(@"଒୴ୣ୾୺୼ଚଘଚଘ୷୬୉ଡ଼୉", keys[116]), //:\KVRT2020_Data
Program.drive_letter + Bfs.GetStr(@"㦅㧣㧹㧭㧬㧫", keys[117]), //:\FRST
};

        List<string> obfStr6 = new List<string>() {
Program.drive_letter + Bfs.GetStr(@"ᴄᵢᵮᵌᵑᵙᵌᵟᵓᵺᵟᵊᵟᵢᵬᵛᵟᵷᵊᵛᵕᵶᵺ", keys[118]), //:\ProgramData\ReaItekHD
Program.drive_letter + Bfs.GetStr(@"ợẅẉẫẶẾẫẸẴẝẸậẸẅẎẰặẽẶẮẪỹẍẸẪẲẪỹẊẼẫắẰẺẼ", keys[119]), //:\ProgramData\Windows Tasks Service
Program.drive_letter + Bfs.GetStr(@"ẓỵỹớỆỎớỈỄửỈờỈỵỾỀệọỆỞỚỽỈỚỂ", keys[120]), //:\ProgramData\WindowsTask
Program.drive_letter + Bfs.GetStr(@"䇣䆅䆉䆫䆶䆾䆫䆸䆴䆝䆸䆭䆸䆅䆊䆠䆪䆭䆼䆴䇪䇫", keys[121]), //:\ProgramData\System32
};

        List<string> obfStr7 = new List<string>() {
Program.drive_letter + Bfs.GetStr(@"䅂䄤䄨䄊䄗䄟䄊䄙䄕䄼䄙䄌䄙", keys[122]), //:\ProgramData
Program.drive_letter + Bfs.GetStr(@"䶁䷧䷫䷉䷔䷜䷉䷚䷖䶛䷽䷒䷗䷞䷈", keys[123]), //:\Program Files
Program.drive_letter + Bfs.GetStr(@"᠑ᡷ᡻ᡙᡄᡌᡙᡊᡆ᠋ᡭᡂᡇᡎᡘ᠋᠃ᡓ᠓᠝᠂", keys[124]), //:\Program Files (x86)
Program.drive_letter + Bfs.GetStr(@"㔴㕒㕙㕧㕠㕪㕡㕹㕽", keys[125]), //:\Windows
};

        string[] obfStr8 = new string[] {
Bfs.GetStr(@"׃׉׃ׄום׌דץעע׵׾פד׿׾פע׿׼׃׵פ׌׃׵עצ׹׳׵ף׌ׄ׳נ׹נ׌׀ױעױ׽׵פ׵עף", keys[126]), //SYSTEM\CurrentControlSet\Services\Tcpip\Parameters
Bfs.GetStr(@"ۣ۪۟۸ۻۭ۾۩ېہۥۯ۾ۣۿۣ۪۸ېۛۥۣۢۨۻۿېۏ۹۾۾۩ۢ۸ۚ۩۾ۿۥۣۢېۣۜ۠ۥۯۥ۩ۿېۉ۴ۼۣ۠۾۩۾", keys[127]), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
Bfs.GetStr(@"⒫⒗⒞⒌⒏⒙⒊⒝⒤⒵⒑⒛⒊⒗⒋⒗⒞⒌⒤⒯⒑⒖⒜⒗⒏⒋⒤Ⓕ⒍⒊⒊⒝⒖⒌⒮⒝⒊⒋⒑⒗⒖⒤⒨⒗⒔⒑⒛⒑⒝⒋⒤Ⓗ⒀⒈⒔⒗⒊⒝⒊⒤Ⓖ⒑⒋⒙⒔⒔⒗⒏⒪⒍⒖", keys[128]), //Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun
Bfs.GetStr(@"ᗉᗕᗜᗎᗍᗛᗈᗟᗆᗗᗳᗹᗨᗵᗩᗵᗼᗮᗆᗍᗳᗴᗾᗵᗭᗩᖺᗔᗎᗆᗙᗯᗨᗨᗿᗴᗮᗌᗿᗨᗩᗳᗵᗴᗆᗍᗳᗴᗾᗵᗭᗩ", keys[129]), //SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows
Bfs.GetStr(@"⸧⹝⸡⹜⸿⸝⸒⸒⸓⸈⹜⸓⸌⸙⸒⹜⸴⸷⸰⸱⸠⹒⹒⹒⸠⸿⸉⸎⸎⸙⸒⸈⸪⸙⸎⸏⸕⸓⸒⸠⸫⸕⸒⸘⸓⸋⸏⹆", keys[130]), //[!] Cannot open HKLM\...\CurrentVersion\Windows:
Bfs.GetStr(@"䮮䮒䮛䮉䮊䮜䮏䮘䮡䮰䮔䮞䮏䮒䮎䮒䮛䮉䮡䮪䮔䮓䮙䮒䮊䮎䮡䮾䮈䮏䮏䮘䮓䮉䮫䮘䮏䮎䮔䮒䮓䮡䮯䮈䮓", keys[131]), //Software\Microsoft\Windows\CurrentVersion\Run
Bfs.GetStr(@"ᾑᾒᾕᾔᾅᾊᾶ᾿ᾭᾮᾸᾫᾼᾅᾉᾶ᾵ᾰᾺᾰᾼᾪᾅᾔᾰᾺᾫᾶᾪᾶ᾿ᾭᾅᾎᾰᾷ᾽ᾶᾮᾪΌᾝᾼ᾿ᾼᾷ᾽ᾼᾫᾅᾜᾡᾺ᾵ᾬᾪᾰᾶᾷᾪ", keys[132]), //HKLM\Software\Policies\Microsoft\Windows Defender\Exclusions
Bfs.GetStr(@"ۃۿ۶ۤۧ۱ۢ۵یۀۿۼ۹۳۹۵ۣی۝۹۳ۢۿۣۿ۶ۤیۇ۹۾۴ۿۣۧڰ۔۵۶۵۾۴۵ۢیەۨ۳ۼۥۣ۹ۿ۾ۣ", keys[133]), //Software\Policies\Microsoft\Windows Defender\Exclusions
Bfs.GetStr(@"ℍℱℸK℩ℿℬ℻ℂℎℱℲℷℽℷ℻ℭℂℓℷℽℬℱℭℱℸKℂ℉ℷℰ℺ℱ℩ℭⅾℚ℻ℸ℻ℰ℺℻ℬℂℛΩℽℲÅℭℷℱℰℭℂℎℿKℶℭ", keys[134]), //Software\Policies\Microsoft\Windows Defender\Exclusions\Paths
Bfs.GetStr(@"▚▦▯▽▾▨▻▬▕▙▦▥■▪■▬►▕▄■▪▻▦►▦▯▽▕▞■▧▭▦▾►◩▍▬▯▬▧▭▬▻▕▌▱▪▥▼►■▦▧►▕▙▻▦▪▬►►▬►", keys[135]), //Software\Policies\Microsoft\Windows Defender\Exclusions\Processes
Bfs.GetStr(@"䶉䷳䶏䷲䶑䶳䶼䶼䶽䶦䷲䶽䶢䶷䶼䷲䶚䶙䶞䶟䶎䷼䷼䷼䶎䶅䶻䶼䶶䶽䶥䶡䷲䶖䶷䶴䶷䶼䶶䶷䶠䶎䶗䶪䶱䶾䶧䶡䶻䶽䶼䶡䷨", keys[136]), //[!] Cannot open HKLM\...\Windows Defender\Exclusions:
Bfs.GetStr(@"‾•‫‹›‬‿
‱›•›⁛⁙⁞ ‣   ‱† ‎‟ „ ​’‱›    ‚„‱‮‘‟‟  ’※ ‟„   ‱‿‘ ", keys[137]), //SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
Bfs.GetStr(@"ƒǫƍƆƸƿƵƾƦƢƍƂƨƢƥƴƼǢǣƍƆƸƿƵƾƦƢƁƾƦƴƣƂƹƴƽƽƍƧǠǿǡ", keys[138]), //C:\Windows\System32\WindowsPowerShell\v1.0
};

        static int[] keys = {
12152,
16595,
6041,
19192,
13350,
8745,
6551,
17557,
7419,
8376,
10501,
11512,
12817,
14755,
13303,
13375,
2656,
15686,
8202,
15750,
13662,
17544,
18528,
5390,
3782,
16359,
15445,
18576,
862,
11038,
4829,
5422,
781,
19347,
4604,
15667,
19228,
3094,
17933,
13361,
16495,
4173,
15596,
5669,
3244,
11854,
3251,
15251,
19653,
7090,
12714,
10662,
13384,
3249,
17379,
18147,
1472,
16203,
4094,
2543,
7620,
3653,
2525,
14025,
15948,
11015,
15036,
478,
8089,
2518,
5726,
19612,
15105,
2897,
1874,
18300,
1446,
572,
6662,
14377,
7470,
10883,
19656,
15350,
19579,
14924,
19939,
10194,
19944,
10521,
2010,
16552,
11920,
5955,
16190,
9894,
15859,
15200,
2042,
17427,
6080,
3746,
6836,
13195,
15697,
697,
8679,
14146,
19671,
878,
10861,
19659,
10907,
9896,
19878,
11544,
2856,
14783,
7486,
7897,
7849,
16857,
16760,
19899,
6187,
13582,
1424,
1676,
9464,
5530,
11900,
19453,
8153,
1680,
8542,
9673,
19922,
8301,
465,
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
        public string WindowsVersion = Registry.LocalMachine.OpenSubKey(Bfs.GetStr(@"≔≈≁≓≐≆≕≂≛≊≮≤≵≨≴≨≡≳≛≐≮≩≣≨≰≴∧≉≓≛≄≲≵≵≢≩≳≑≢≵≴≮≨≩", 8711)).GetValue("Pro?duct?Name".Replace("?", "")).ToString(); //SOFTWARE\Microsoft\Windows NT\CurrentVersion
        string quarantineFolder = Path.Combine(Environment.CurrentDirectory, "minerseаrch_quarаntine");

        public void DetectRk()
        {

            Logger.WriteLog("\t\tChecking ro?otk?it present...".Replace("?", ""), Logger.head, false);
            string rk_testapp = Path.Combine(Path.GetTempPath(), $"dia?ler_{utils.GetRndString()}.exe".Replace("?", ""));

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
                    if (proc.ProcessName.StartsWith("di?a?ler".Replace("?", "")))
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
                Logger.WriteLog("\t[!!!!] Mi?ner's r?o?o?tk?it detected! Trying to remove...".Replace("?", ""), ConsoleColor.White, false);
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
                    Logger.WriteLog($"\t[!] Too much GPU libs usage: {processName}.exe, Process ID: {processId}", Logger.warn);
                    riskLevel += 1;

                }

                try
                {
                    int remoteport = utils.GetPortByProcessId(p.Id);
                    if (remoteport != -1 && remoteport != 0)
                    {
                        if (_PortList.Contains(remoteport))
                        {
                            Logger.WriteLog($"\t[!] Blacklisted port {remoteport} in {processName}", Logger.warn);
                            riskLevel += 1;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Logger.WriteLog($"\t[x] Error read port: {ex.Message}", Logger.warn);
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
                        if (args.Contains(port.ToString()))
                        {
                            riskLevel += 1;
                            Logger.WriteLog($"\t[!] {processName}.exe: Blacklisted port {port} in CMD ARGS", Logger.warn);
                        }
                    }
                    if (args.Contains("str?at?um".Replace("?", "")))
                    {
                        riskLevel += 3;
                        Logger.WriteLog($"\t[!] {processName}.exe: Present \"st?r?a?t?um\" in cmd args.".Replace("?", ""), Logger.warn);
                    }
                    if (args.Contains("na?nop?ool?".Replace("?", "")) || args.Contains("po?ol.".Replace("?", "")))
                    {
                        riskLevel += 3;
                        Logger.WriteLog($"\t[!] {processName}.exe: Present \"po?ol\" in cmd args.".Replace("?", ""), Logger.warn);
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
                        Logger.WriteLog($"\t[!!!] Process in?jec?tion. Process ID: {processId}".Replace("?", ""), Logger.caution);
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
                            Logger.WriteLog($"\t[!] Probably process inje?ction. Process ID: {processId}".Replace("?", ""), Logger.warn);
                            riskLevel += 3;
                        }
                    }
                    if (processName == SysFileName[17] && args.Contains("\\dia?ler.exe ".Replace("?", "")))
                    {
                        Logger.WriteLog($"\t[!!!] Ro?otk?it injection. Process ID: {processId}".Replace("?", ""), Logger.caution);
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
                    if (processName == "un?sec?app".Replace("?", "") && !p.MainModule.FileName.ToLower().Contains(@":\w?in?do?ws\s?yst?em3?2\wb?em".Replace("?", "")))
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


                if (processName == "rundll" || processName == "system" || processName == "wi?ns?er?v".Replace("?", ""))
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
                obfStr5.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToLower(), "aut?olo?gger".Replace("?","")));
                obfStr5.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToLower(), "av?_?b?l?ock_?rem?over".Replace("?", "")));
                obfStr5.Add(Path.Combine(utils.GetDownloadsPath(), "auto?log?ger".Replace("?", "")));
                obfStr5.Add(Path.Combine(utils.GetDownloadsPath(), "a?v_b?lo?ck?_re?mov?er".Replace("?", "")));
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

                int BootMode = winapi.GetSystemMetrics(winapi.SM_CLEANBOOT);

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

            foreach (string path in obfStr7)
            {
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
                Logger.WriteLog("\t\tChecking user Jo?hn...".Replace("?", ""), Logger.head, false);
                if (utils.CheckUserExists("jo?hn".Replace("?", "")))
                {
                    if (Environment.UserName.ToLower() == "jo?hn".Replace("?", ""))
                    {
                        Logger.WriteLog($"\t[#] Current user - jo?hn. Removing is not required".Replace("?", ""), ConsoleColor.Blue);
                    }
                    else
                    {
                        try
                        {
                            utils.DeleteUser("jo?hn".Replace("?", ""));
                            Thread.Sleep(100);
                            if (!utils.CheckUserExists("jo?hn".Replace("?", "")))
                            {
                                Logger.WriteLog("\t[+] Successfull deleted userprofile \"Jo?hn\"".Replace("?", ""), Logger.success);
                            }
                            else
                                Logger.WriteLog("\t[x] Error for remove user profile \"Jo?hn\"".Replace("?", ""), ConsoleColor.Red);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLog($"\t[x] Cannot delete user \"Jo?hn\":\n{ex.Message}".Replace("?", ""), Logger.error);
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

                string hostsPath_full = hostsPath + "\\h?os?t?s".Replace("?", "");

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
                        string logMessage = $"Ho?sts file has been recovered. Affected strings {deletedLineCount}".Replace("?", "");
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
                        Logger.WriteLog("\t[!] Suspicious registry key: Disal?low?Run - restricts the launch of the specified applications".Replace("?", ""), Logger.warn);
                        DisallowRunKey.DeleteValue("DisallowRun");
                        if (!DisallowRunKey.GetValueNames().Contains("Dis?allo?wRun".Replace("?", "")))
                        {
                            Logger.WriteLog("\t[+] Dis?all?owR?un key successfully deleted".Replace("?", ""), Logger.success);
                            affected_items++;
                        }
                    }
                    RegistryKey DisallowRunSub = Registry.CurrentUser.OpenSubKey(obfStr8[2], true);
                    if (DisallowRunSub != null)
                    {
                        DisallowRunKey.DeleteSubKeyTree("Di?sall?owR?un".Replace("?", ""));
                        DisallowRunSub = Registry.CurrentUser.OpenSubKey(obfStr8[2], true);
                        if (DisallowRunSub == null)
                        {
                            Logger.WriteLog("\t[+] D?is?al?low?Ru?n hive successfully deleted".Replace("?", ""), Logger.success);
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
                    if (!String.IsNullOrEmpty(appinit_key.GetValue("App??In??it_DL?Ls".Replace("?", "")).ToString()))
                    {
                        if (appinit_key.GetValue("Loa??dApp??Init_DLLs".Replace("?", "")).ToString() == "1")
                        {
                            if (!appinit_key.GetValueNames().Contains("RequireSignedApp?Ini?t_D?LLs".Replace("?", "")))
                            {
                                Logger.WriteLog("\t[!] A?ppIn??it_DLL?s is not empty".Replace("?", ""), Logger.warn);
                                Logger.WriteLog("\t[!!!] RequireSignedAp?pIn??it_DLLs key is not found".Replace("?", ""), Logger.caution);
                                appinit_key.SetValue("RequireSignedApp?Init?_DLLs".Replace("?", ""), 1, RegistryValueKind.DWord);
                                if (appinit_key.GetValue("RequireSignedApp?Init?_DLLs".Replace("?", "")).ToString() == "1")
                                {
                                    Logger.WriteLog("\t[+] The value was created and set to 1", Logger.success);
                                    affected_items++;
                                }
                            }
                            else if (appinit_key.GetValue("RequireSignedApp?Init?_DLLs".Replace("?", "")).ToString() == "0")
                            {
                                Logger.WriteLog("\t[!] AppInit_DLLs is not empty", Logger.warn);
                                Logger.WriteLog("\t[!!!] RequireS?ign?edApp?Init_DLLs key set is 0".Replace("?", ""), Logger.caution);
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

                        if (AutorunKey.GetValue(value).ToString() == $@"{Program.drive_letter}:\Pro?gra?mDa?ta\Re?aItek?HD\task?host?w.e?x?e".Replace("?", ""))
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
                    if (tektonit.GetSubKeyNames().Contains("tek?toni?t".Replace("?", "")))
                    {
                        Logger.WriteLog("\t[!] Suspicious registry key: tekt?onit".Replace("?", ""), Logger.warn);
                        tektonit.DeleteSubKeyTree("tek?ton?it".Replace("?", ""));
                        if (!tektonit.GetSubKeyNames().Contains("tek?ton?it".Replace("?", "")))
                        {
                            Logger.WriteLog("\t[+] tek?t?onit key successfully deleted".Replace("?", ""), Logger.success);
                            affected_items++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"\t[!] Cannot open HKCU\\...\\t?e?k?t?o?n?i?t: {ex.Message}".Replace("?", ""), Logger.error);
            }
            #endregion

            #region WOW6432Node
            try
            {
                RegistryKey AutorunKey = Registry.LocalMachine.OpenSubKey(obfStr8[11], true);
                if (AutorunKey != null)
                {
                    Logger.WriteLog(@"...\WO?W64?32?Node\...\Run".Replace("?", ""), ConsoleColor.DarkCyan);
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
                Logger.WriteLog($"\t[!] Cannot open WOW6432?Node\\...\\run: {ex.Message}".Replace("?", ""), Logger.error);
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
                        if (taskName.StartsWith("dia?ler".Replace("?", "")))
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
