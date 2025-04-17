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
        public int defenceStat { get; private set; } = 5;
        [JsonInclude]
        public int currentHp { get; private set; } = 100;
        [JsonInclude]
        public int maxHp { get; private set; } = 100;
        [JsonInclude]
        public int gold { get; private set; } = 1500;
        [JsonInclude]
        public List<int> itemId { get; } = new();
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
            Console.WriteLine(
                $"\n\nLv. {level,2:00}" + "\n" +
                name + $" ( {Class} )\n"+
                TextMessages.viewAttack + " : " + attackStat + "\n" +
                TextMessages.viewDefence + " : " + defenceStat + "\n" +
                TextMessages.viewHealth + " : " + currentHp + "/"+ maxHp + "\n" +
                TextMessages.viewGold + " : " + gold + " G\n");
        }
        public void AddItemToInventory(int id)
        {
            itemId.Add(id);
        }
        public void ViewInventory(bool isManage = false)
        {
            if (!isManage)
            {
                foreach (int id in itemId)
                {
                    Item item = ItemManager.Instance.GetItembyId(id);
                    if (item != null)
                    {
                        Console.Write("-");
                        if (equipedItem.Contains(id))
                        {
                            Console.Write("[E]");
                        }
                        item.ViewInfo();
                    }
                }
            }
            else
            {
                int i = 1;
                foreach (int id in itemId)
                {
                    Item item = ItemManager.Instance.GetItembyId(id);
                    if (item != null)
                    {
                        Console.Write("- {0}",i++);
                        if (equipedItem.Contains(id))
                        {
                            Console.Write("[E]");
                        }
                        item.ViewInfo();
                    }
                }
            }
        }
        public void ManageEquipment(int index)
        {
            if (index > itemId.Count()) return;
            --index;
            int equipItemIndex = equipedItem.IndexOf(itemId[index]);
            if (equipItemIndex != -1)
            {
                equipedItem.Remove(equipItemIndex);
            }
            else
            {
                Item item = ItemManager.Instance.GetItembyId(itemId[index]);
                if (item == null)
                {
                    Console.WriteLine("Fatal Error!!!!");
                    Console.ReadKey();
                }
                foreach (int id in equipedItem)
                {
                    Item temp = ItemManager.Instance.GetItembyId(id);
                    if (temp == null)
                    {
                        Console.WriteLine("Fatal Error!!!!");
                        Console.ReadKey();
                    }
                    if(temp.type == item.type)
                    {
                        equipedItem.Remove(id);
                        break;
                    }
                }
                equipedItem.Add(itemId[index]);
            }
            UpdateStatus();
            return;
        }
        void UpdateStatus()
        {
            foreach(int id in equipedItem)
            {
                Item item = ItemManager.Instance.GetItembyId(id);
                if (item == null)
                {
                    Console.WriteLine("Fatal Error!!!!");
                    Console.ReadKey();
                }
                attackStat += item.itemStatus.atk;
                defenceStat += item.itemStatus.def;
                maxHp += item.itemStatus.health;
            }
        }
    }
}
