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

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameManager();
                return _instance;
            }
        }

        private GameManager()
        {
        }

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
                        EnterDungeon();
                        break;
                    case 5:
                        TakeRest();
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

        void EnterDungeon()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(TextMessages.viewDungeonMent);
                Console.WriteLine($"\n1. {Item.PadingKorean(TextMessages.viewEasyDungeonMent,14)} | 방어력 5 이상 권장");
                Console.WriteLine($"2. {Item.PadingKorean(TextMessages.viewNormalDungeonMent, 14)} | 방어력 7 이상 권장");
                Console.WriteLine($"3. {Item.PadingKorean(TextMessages.viewHardDungeonMent, 14)} | 방어력 11 이상 권장");
                Console.WriteLine($"0. {TextMessages.viewExitMent}\n");
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
                    switch (act)
                    {
                        case 0:
                            return;
                        case 1:
                            ChallengeDungeon(1);
                            break;
                        case 2:
                            ChallengeDungeon(2);
                            break;
                        case 3:
                            ChallengeDungeon(3);
                            break;
                        default:
                            WrongInput();
                            break;
                    }
                    break;
                }
            }
        }
        void ChallengeDungeon(int difficulty)
        {
            Console.Clear();
            Random random = new Random();
            Player p = PlayerManager.Instance.MainPlayer;
            int def;
            int reward;
            bool isFailed = false;
            string dif;

            switch (difficulty)
            {
                case 1:     //이지
                    def = 5;
                    reward = 1000;
                    dif = TextMessages.viewEasyDungeonMent;
                    break;
                case 2:     //일반
                    def = 7;
                    reward = 1700;
                    dif = TextMessages.viewNormalDungeonMent;
                    break;
                case 3:     //어려움
                    def = 11;
                    reward = 2500;
                    dif = TextMessages.viewHardDungeonMent;
                    break;
                default:
                    return;
            }
            if (p.GetDefenceStat() < def)
            {
                int chance = random.Next(0, 100);
                if (chance < 40)
                {
                    Console.WriteLine(TextMessages.dungeonFailed);
                    Console.WriteLine(TextMessages.dungeonResult);
                    Console.WriteLine($"{TextMessages.viewHealth} {p.currentHp} -> {p.currentHp / 2}");
                    Console.WriteLine($"{TextMessages.viewGold} {p.gold} G -> {p.gold}");
                    isFailed = true;
                }
            }
            if (!isFailed)
            {
                int reduceHp = random.Next(20, 36) - (p.GetDefenceStat() - def);
                int bonus = random.Next((int)p.GetAttackStat(), (int)(p.GetAttackStat() * 2) + 1);
                reward += (reward * bonus / 100);
                Console.WriteLine(TextMessages.dungeonSuccess);
                Console.WriteLine(dif + "을 클리어 하였습니다.\n");
                Console.WriteLine(TextMessages.dungeonResult);
                Console.WriteLine($"{TextMessages.viewHealth} {p.currentHp} -> {p.currentHp - reduceHp}");
                Console.WriteLine($"{TextMessages.viewGold} {p.gold} G -> {p.gold + reward}\n");
                p.Damaged(reduceHp);
                p.EarnGold(reward);
                p.GainExp(1);
            }
            Console.WriteLine($"0. {TextMessages.viewExitMent}\n");
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
                if (act == 0) return;
            }
        }

        void TakeRest()
        {
            int restGold = 500;
            Player p = PlayerManager.Instance.MainPlayer;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(TextMessages.viewRestMent);
                Console.WriteLine("500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {0} G)\n", p.gold);
                Console.WriteLine($"1. {TextMessages.viewRestMent}\n0. {TextMessages.viewExitMent}\n\n{TextMessages.selectActionMent}");
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
                    if (act == 1)
                    {
                        if (p.gold < restGold)
                        {
                            Console.WriteLine("Gold가 부족합니다.(계속하려면 아무 키 입력)\n");
                            Console.ReadKey();
                        }
                        else
                        {
                            p.UseGold(restGold);
                            p.Heal();
                            Console.WriteLine("휴식을 완료했습니다.(계속하려면 아무 키 입력)\n");
                            Console.ReadKey();
                        }   
                        break;
                    }
                    if (act != 0) { WrongInput(); continue; }
                    return;
                }
            }
        }

        void SaveData()
        {
            string jsonString = JsonSerializer.Serialize(PlayerManager.Instance.MainPlayer, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(TextMessages.playerDataFath, jsonString);

            ItemManager.Instance.SaveData();
        }

    }
}
