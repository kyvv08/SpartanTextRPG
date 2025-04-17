using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpartanTextRPG
{
    struct ItemStatus
    {
        public int atk;
        public int def;
        public int health;
    }
    internal class Item : IDescribable
    {
        [JsonInclude]
        public int id { get;private set; }
        [JsonInclude]
        public string name { get;private set; } = string.Empty;
        [JsonInclude]
        public string description { get;private set; } = string.Empty;
        [JsonInclude] 
        public ItemStatus itemStatus { get;private set; }
        [JsonInclude] 
        public int price { get; private set; }
        public bool isSold { get; private set; }
        public Item() { }
        public Item(int id, string name, string description, ItemStatus itemStatus, int price)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.itemStatus = itemStatus;
            this.price = price;
            this.isSold = false;
        }
        public void ViewInfo()
        {
            string stat = string.Empty;
            string sellPrice = string.Empty;
            if (itemStatus.atk != 0)
            {
                stat += $"{TextMessages.viewAttack} +{itemStatus.atk,-2} ";
            }
            if (itemStatus.def != 0)
            {
                stat += $"{TextMessages.viewDefence} +{itemStatus.def,-2} ";
            }
            if(itemStatus.health != 0)
            {
                stat += $"{TextMessages.viewHealth} +{itemStatus.health,-2} ";
            }
            if (isSold)
            {
                sellPrice = $"{TextMessages.viewSoldItemMent}";
            }
            else
            {
                sellPrice = $"{price} G";
            }
            Console.WriteLine($"{name,-10} | {stat} | {description} | {sellPrice,-5}");
        }
    }
}
