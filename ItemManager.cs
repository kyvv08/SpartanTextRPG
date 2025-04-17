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

        private Dictionary<int, Item> itemDict = new();

        public void LoadItems()
        {
            string json = File.ReadAllText(TextMessages.itemListFileFath);
            var items = JsonSerializer.Deserialize<List<Item>>(json);
            foreach (var item in items)
            {
                itemDict[item.id] = item;
            }
        }
        public Item? GetItembyId(int id)
        {
            return itemDict.TryGetValue(id, out var item) ? item : null;
        }
    }
}
