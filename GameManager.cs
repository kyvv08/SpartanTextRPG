using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartanTextRPG
{
    internal class GameManager
    {
        private static GameManager _instance;

        // 2. 외부에서 접근할 수 있도록 프로퍼티
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameManager();
                return _instance;
            }
        }

        // 3. 생성자는 private (외부에서 new 못하게)
        private GameManager()
        {
        }

        // 예시 메서드
        void WrongInput()
        {
            Console.WriteLine("잘못된 입력입니다.(아무키나 눌러 다시 입력)\n");
            Console.ReadKey();
        }
        public void StartGame()
        {
            string name, className;
            if (File.Exists("playerData"))
            {
                //json 파일 읽어오기
                PlayerManager.Instance.SetMainPlayer(new Player());
            }
            else
            {
                int act = 0;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(TextMessages.welcomeMent + TextMessages.enterNameMent);
                    name = Console.ReadLine();
                    Console.WriteLine($"\n입력하신 이름은 {name} 입니다.\n");
                    Console.WriteLine("1. 저장\n2. 취소\n");
                    Console.WriteLine(TextMessages.selectActionMent);
                    act = int.Parse(Console.ReadLine());
                    if (act == 1)
                    {
                        break;
                    }
                    else if (act == 2)
                    {
                        continue;
                    }
                    else
                    {
                        WrongInput();
                    }
                }
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(TextMessages.welcomeMent + TextMessages.enterClassMent);
                    Console.WriteLine($"1. {TextMessages.WARRIOR}\n2. {TextMessages.THIEF}\n3. {TextMessages.MAGICIAN}\n4. {TextMessages.ARCHER}\n");
                    Console.WriteLine(TextMessages.selectActionMent);
                    int selectedClass = int.Parse(Console.ReadLine());
                    Console.WriteLine($"{selectedClass}\n");
                    switch (selectedClass)
                    {
                        case 1:
                            className = TextMessages.WARRIOR;
                            break;
                        case 2:
                            className = TextMessages.THIEF;
                            break;
                        case 3:
                            className = TextMessages.MAGICIAN;
                            break;
                        case 4:
                            className = TextMessages.ARCHER;
                            break;
                        default:
                            WrongInput();
                            continue;
                    }
                    PlayerManager.Instance.SetMainPlayer(new Player(name,className));
                    return;
                }
            }
        }

        public void EnterVillage()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine(TextMessages.enterVillageMent);
                Console.WriteLine($"1. {TextMessages.viewStatusMent}\n2. {TextMessages.viewInvenMent}\n3. {TextMessages.viewShopMent}\n4. {TextMessages.viewDungeonMent}\n5. {TextMessages.viewRestMent}\n6. {TextMessages.viewSaveMent}\n"); ;
                Console.WriteLine(TextMessages.selectActionMent);
                int selectedClass = int.Parse(Console.ReadLine());
                switch (selectedClass)
                {
                    case 1:
                        ViewPlayerStatus();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    default:
                        WrongInput();
                        continue;
                }
            }
        }
        void ViewPlayerStatus()
        {
            Console.Clear ();
            Player p = PlayerManager.Instance.MainPlayer;
            Console.WriteLine($"[{TextMessages.viewStatusMent}]\n\n{TextMessages.viewLevel} {p.level}\n{p.name} ( {p.Class} )\n" +
                $"{TextMessages.viewAttack} : {p.attackStat}\n" +
                $"{TextMessages.viewDefence} : {p.defenceStat}\n" +
                $"{TextMessages.viewHealth} : {p.hp}\n" +
                $"{TextMessages.viewGold} : {p.gold} G\n");
            Console.WriteLine($"0. {TextMessages.viewExitMent}");
            Console.WriteLine(TextMessages.selectActionMent);
            int startLine = Console.CursorTop;
            while (true) {
                Console.SetCursorPosition(0, startLine + 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, startLine + 1);
                int act = int.Parse(Console.ReadLine());
                if (act != 0) { continue; }
                return;
            }
        }
    }
}
