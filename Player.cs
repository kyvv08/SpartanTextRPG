using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpartanTextRPG
{
    internal class Player:IDescribable
    {
        [JsonInclude]
        public string name { get; private set; } = string.Empty;
        [JsonInclude]
        public string Class { get; private set; } = string.Empty;
        [JsonInclude]
        public int level { get; private set; } = 1;
        [JsonInclude]
        public int attackStat { get; private set; } = 10;
        [JsonInclude]
        public int additionalAttackStat { get; private set; } = 0;
        [JsonInclude]
        public int defenceStat { get; private set; } = 5;
        [JsonInclude]
        public int additionalDefenceStat { get; private set; } = 0;
        [JsonInclude]
        public int currentHp { get; private set; } = 100;
        [JsonInclude]
        public int maxHp { get; private set; } = 100;
        [JsonInclude]
        public int additionalMaxHp { get; private set; } = 0;
        [JsonInclude]
        public int gold { get; private set; } = 1500;
        [JsonInclude]
        public List<int> itemId { get; private set; } = new();
        [JsonInclude]
        public List<int> equipedItem { get; private set; } = new();
        public Player()
        {
        }
        public Player(string Name,string CLASS) { 
            name = Name;
            Class = CLASS;  
        }

        public void ViewInfo()
        {
            string atk = $"{TextMessages.viewAttack} : {attackStat+additionalAttackStat}";
            string def = $"{TextMessages.viewDefence} : {defenceStat+additionalDefenceStat}";
            string hp = $"{TextMessages.viewHealth} : {currentHp}/{maxHp+ additionalMaxHp}";
                if (additionalAttackStat > 0)
                {
                    atk += $" (+{additionalAttackStat})";
                }
                if(additionalDefenceStat > 0)
                {
                    def += $" (+{additionalDefenceStat})";
                }
                if(additionalMaxHp > 0)
                {
                    hp += $" (+{additionalMaxHp})";
                }
            Console.WriteLine(
                $"\nLv. {level,2:00}" + "\n" +
                name + $" ( {Class} )\n"+
                atk + "\n" +
                def + "\n" +
                hp + "\n" +
                TextMessages.viewGold + " : " + gold + " G\n");
        }

        public void ViewInventory(bool isManage)
        {
            if (!isManage)
            {
                foreach (int id in itemId)
                {
                    Item item = ItemManager.Instance.GetItembyId(id);
                    if (item == null)
                    {
                        Console.WriteLine("Fatal Error!!!!");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Write("-");
                        if (equipedItem.Contains(id))
                        {
                            Console.Write("[E]");
                        }
                        item.ViewInfo(false);
                    }
                }
            }
            else
            {
                int i = 1;
                foreach (int id in itemId)
                {
                    Item item = ItemManager.Instance.GetItembyId(id);
                    if (item == null)
                    {
                        Console.WriteLine("Fatal Error!!!!");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Write("- {0} ", i++);
                        if (equipedItem.Contains(id))
                        {
                            Console.Write("[E]");
                        }
                        item.ViewInfo(false);
                    }
                }
            }
            Console.WriteLine();
        }
        public void ManageEquipment(int index)
        {
            if (index > itemId.Count()) {
                GameManager.Instance.WrongInput();
                return;
            }
            --index;
            int equipItemIndex = equipedItem.IndexOf(itemId[index]);
            if (equipItemIndex != -1)
            {
                RemoveItemStat(equipedItem[equipItemIndex]);
                equipedItem.RemoveAt(equipItemIndex);
            }
            else
            {
                Item item = ItemManager.Instance.GetItembyId(itemId[index]);
                if (item == null)
                {
                    Console.WriteLine("Fatal Error!!!!");
                    Console.ReadKey();
                    return;
                }
                foreach (int id in equipedItem)
                {
                    Item temp = ItemManager.Instance.GetItembyId(id);
                    if (temp == null)
                    {
                        Console.WriteLine("Fatal Error!!!!");
                        Console.ReadKey();
                    }
                    else if(temp.type == item.type)
                    {
                        RemoveItemStat(id);
                        equipedItem.Remove(id);
                        break;
                    }
                }
                equipedItem.Add(itemId[index]);
                AddItemStat(itemId[index]);
            }
            return;
        }
        void AddItemStat(int index)
        {
            Item item = ItemManager.Instance.GetItembyId(index);
            if (item == null)
            {
                Console.WriteLine("Fatal Error!!!!");
                Console.ReadKey();
            }
            additionalAttackStat += item.itemStatus.atk;
            additionalDefenceStat += item.itemStatus.def;
            additionalMaxHp += item.itemStatus.health;
        }
        void RemoveItemStat(int index)
        {
            Item item = ItemManager.Instance.GetItembyId(index);
            if (item == null)
            {
                Console.WriteLine("Fatal Error!!!!");
                Console.ReadKey();
            }
            additionalAttackStat -= item.itemStatus.atk;
            additionalDefenceStat -= item.itemStatus.def;
            additionalMaxHp -= item.itemStatus.health;
            //additionalAttackStat = Math.Max(additionalAttackStat - item.itemStatus.atk,0);
            //additionalDefenceStat = Math.Max(additionalDefenceStat - item.itemStatus.def, 0);
            //additionalMaxHp = Math.Max(additionalMaxHp - item.itemStatus.health, 0);
        }
        public void AddItemToInvenTory(int id,int price)
        {
            itemId.Add(id);
            gold -= price;
        }
    }
}
