using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartanTextRPG
{
    internal class PlayerManager
    {
        private static PlayerManager _instance;
        public static PlayerManager Instance => _instance ??= new PlayerManager();
        private PlayerManager() { }

        // 본인 (Main 플레이어)
        public Player MainPlayer { get; private set; }
        public void SetMainPlayer(Player player)
        {
            MainPlayer = player;
        }
    }
}
