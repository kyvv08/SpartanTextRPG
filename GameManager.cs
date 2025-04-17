using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        public void WrongInput()
        {
            Console.WriteLine("잘못된 입력입니다.(아무키나 눌러 다시 입력)\n");
            Console.ReadKey();
        }
        public void StartGame()
        {

            string name, className;
            if (File.Exists("items.json"))
            {
                ItemManager.Instance.LoadItems();
            }
            else
            {
                Console.WriteLine("Fatal Error!!!!!!!!!!!!");
                Console.ReadKey();
            }
            if (File.Exists("playerData.json"))
            {
                //json 파일 읽어오기
                string json = File.ReadAllText(TextMessages.playerDataFath);
                PlayerManager.Instance.SetMainPlayer(JsonSerializer.Deserialize<Player>(json));
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
                    PlayerManager.Instance.SetMainPlayer(new Player(name, className));
                    return;
                }
            }
        }

        public void EnterVillage()
        {
            while (true)
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
                        ViewInventory();
                        break;
                    case 3:
                        EnterShop();
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        SaveData();
                        break;
                    default:
                        WrongInput();
                        continue;
                }
            }
        }
        void ViewPlayerStatus()
        {
            Console.Clear();
            Player p = PlayerManager.Instance.MainPlayer;
            Console.WriteLine(TextMessages.viewStatusMent);
            p.ViewInfo();
            Console.WriteLine($"0. {TextMessages.viewExitMent}");
            Console.WriteLine(TextMessages.selectActionMent);
            int startLine = Console.CursorTop;
            while (true)
            {
                Console.SetCursorPosition(0, startLine);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, startLine);
                if (!int.TryParse(Console.ReadLine(), out int act))
                {
                    continue;
                }
                if (act != 0) { continue; }
                return;
            }
        }
        void PrintInventory(bool isManageMode)
        {
            Player p = PlayerManager.Instance.MainPlayer;
            Console.Clear();
            Console.WriteLine($"{TextMessages.viewInvenMent}\n\n{TextMessages.viewItemListMent}\n");
            p.ViewInventory(isManageMode);
            if (!isManageMode)
            {
                Console.WriteLine($"1. {TextMessages.viewEquipManageMent}");
            }
            Console.WriteLine($"0. {TextMessages.viewExitMent}\n");
            Console.WriteLine(TextMessages.selectActionMent);
        }
        void ViewInventory()
        {
            while (true)
            {
                PrintInventory(false);
                int startLine = Console.CursorTop;
                while (true)
                {
                    Console.SetCursorPosition(0, startLine);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, startLine);
                    if (!int.TryParse(Console.ReadLine(), out int act))
                    {
                        WrongInput();
                        continue;
                    }
                    if (act == 1)
                    {
                        ManageItems();
                        break;
                    }
                    if (act != 0)
                    {
                        WrongInput();
                        continue;
                    }
                    return;
                }
            }
        }

        void ManageItems()
        {
            while (true)
            {
                Player p = PlayerManager.Instance.MainPlayer;
                PrintInventory(true);
                int startLine = Console.CursorTop;
                while (true)
                {
                    Console.SetCursorPosition(0, startLine);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, startLine);
                    if (!int.TryParse(Console.ReadLine(), out int act))
                    {
                        WrongInput();
                        continue;
                    }
                    if (act != 0)
                    {
                        p.ManageEquipment(act);
                        break;
                    }
                    return;
                }
            }
        }

        void PrintShop(bool isBuyMode)
        {
            Console.Clear();

            Console.WriteLine($"{TextMessages.viewShopMent}");
            Console.WriteLine($"\n{TextMessages.viewCurrentCashMent}\n" + PlayerManager.Instance.MainPlayer.gold + " G\n");
            ItemManager.Instance.ShowItems(isBuyMode);

            if (!isBuyMode)
            {
                Console.WriteLine($"1. {TextMessages.viewBuyItemMent}");
            }
            Console.WriteLine($"0. {TextMessages.viewExitMent}\n");
            Console.WriteLine(TextMessages.selectActionMent);
        }

        void EnterShop()
        {
            while (true)
            {
                PrintShop(false);
                int startLine = Console.CursorTop;
                while (true)
                {
                    Console.SetCursorPosition(0, startLine);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, startLine);
                    if (!int.TryParse(Console.ReadLine(), out int act))
                    {
                        WrongInput();
                        continue;
                    }
                    if (act == 1)
                    {
                        BuyItems();
                        break;
                    }
                    if (act != 0) { WrongInput(); continue; }
                    return;
                }
            }
        }
        void BuyItems()
        {
            while (true)
            {
                PrintShop(true);
                int startLine = Console.CursorTop;
                while (true)
                {
                    Console.SetCursorPosition(0, startLine);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, startLine);
                    if (!int.TryParse(Console.ReadLine(), out int act))
                    {
                        WrongInput();
                        continue;
                    }
                    if (act != 0)
                    {
                        ItemManager.Instance.BuyItem(act);
                        break;
                    }
                    return;
                }
            }
        }

        void SaveData()
        {
            string jsonString = JsonSerializer.Serialize(PlayerManager.Instance.MainPlayer, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText("playerData.json", jsonString);
        }
    }
}
