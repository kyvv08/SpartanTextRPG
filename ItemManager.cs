using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpartanTextRPG
{
    internal class ItemManager
    {
        private static ItemManager _instance;
        public static ItemManager Instance => _instance ??= new ItemManager();
        private ItemManager() { }

        private Dictionary<int, Item> _itemDict = new();

        public void LoadItems(string filePath)
        {
            string json = File.ReadAllText(filePath);
            var items = JsonSerializer.Deserialize<List<Item>>(json);
            foreach (var item in items)
            {
                _itemDict[item.id] = item;
            }
        }
    }
}
