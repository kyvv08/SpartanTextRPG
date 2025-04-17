using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpartanTextRPG
{
    readonly struct ItemStatus
    {
        public int atk { get; init; }
        public int def { get; init; }
        public int health { get; init; }
    }
    internal class Item : IDescribable
    {
        [JsonInclude]
        public int id { get;private set; }
        [JsonInclude]
        public int type { get; private set; }       //1이 무기, 2가 방어구
        [JsonInclude]
        public string name { get;private set; } = string.Empty;
        [JsonInclude]
        public string description { get;private set; } = string.Empty;
        [JsonInclude] 
        public ItemStatus itemStatus { get;private set; }
        [JsonInclude] 
        public int price { get; private set; }
        [JsonInclude] 
        public bool isSold { get; private set; }
        public Item() { }
        public Item(int id, int type, string name, string description, ItemStatus itemStatus, int price)
        {
            this.id = id;
            this.type = type;
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
            Console.WriteLine($"{PadingKorean(name,18)} | {stat,-10} | {PadingKorean(description, 50)} | {sellPrice,-5}");
        }
        string PadingKorean(string input,int width)
        {
            int len = 0;
            foreach(char c in input){
                len += (c >= 0xAC00 && c <= 0xD7A3) ? 2 : 1;
            }
            int pad = width - len;
            if (pad > 0)
                return input + new string(' ', pad);
            return input;
        }
    }
}
