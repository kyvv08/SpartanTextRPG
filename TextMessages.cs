using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SpartanTextRPG
{
    internal static class TextMessages
    {
        public const string WARRIOR = "전사";
        public const string THIEF = "도적";
        public const string MAGICIAN = "마법사";
        public const string ARCHER= "궁수";

        //----------------게임 시작 시 출력 멘트------------------------
        public const string welcomeMent = "스파르타 던전에 오신 여러분 환영합니다.\n";
        public const string enterNameMent = "원하시는 이름을 설정해주세요.\n";
        public const string enterClassMent = "원하시는 직업을 설정해주세요.\n";

        //----------------마을 진입 시 출력 멘트------------------------
        public const string enterVillageMent = "스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n";
        public const string selectActionMent = "원하시는 행동을 입력해주세요.";

        //----------------메뉴 선택---------------------
        public const string viewStatusMent = "상태 보기";
        public const string viewInvenMent = "인벤토리";
        public const string viewShopMent = "상점";
        public const string viewDungeonMent = "던전입장";
        public const string viewRestMent = "휴식하기";
        public const string viewExitMent = "나가기";
        public const string viewSaveMent = "저장하기";

        public const string viewEquipManageMent = "장착 관리";

        public const string viewItemListMent = "[아이템 목록]";
        public const string viewCurrentCashMent = "[보유 골드]";

        public const string viewBuyItemMent = "아이템 구매";
        public const string viewSellItemMent = "아이템 판매";
        public const string viewSoldItemMent = "구매 완료";

        public const string enterShopMent = "필요한 아이템을 얻을 수 있는 상점입니다.";

        public const string viewEasyDungeonMent = "쉬운 던전";
        public const string viewNormalDungeonMent = "일반 던전";
        public const string viewHardDungeonMent = "어려운 던전";

        public const string dungeonFailed = "던전 실패\n강한 난이도로 인하여 던전 클리어에 실패하였습니다.\n";
        public const string dungeonResult = "[탐험 결과]";
        public const string dungeonSuccess = "던전 클리어\n축하합니다!!";

        //----------------스탯 표기---------------------
        public const string viewLevel = "Lv.";
        public const string viewAttack = "공격력";
        public const string viewDefence = "방어력";
        public const string viewHealth = "체 력";
        public const string viewExp = "경험치";

        public const string viewGold = "Gold";

        public const string itemListFileFath = "items.json";
        public const string playerDataFath = "playerData.json";
    }
}
