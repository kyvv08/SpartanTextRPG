using System;
using System.Collections.Generic;
using System.Linq;
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
        public int hp { get; private set; } = 100;
        [JsonInclude]
        public int gold { get; private set; } = 1500;
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
                "Lv. {level,2:N}" + "\n" +
                name + "({Class})\n"+
                TextMessages.viewAttack + " : " + attackStat + "\n" +
                TextMessages.viewDefence + " : " + defenceStat + "\n" +
                TextMessages.viewHealth + " : " + hp + "\n" +
                TextMessages.viewGold + " : " + gold + " G\n");
        }
    }
}
