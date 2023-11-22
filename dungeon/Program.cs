using Rtangame;
using System;
using System.Collections.Generic;


namespace MyGame;
internal class Program
{
    private static Character player;

    static void Main(string[] args)
    {
        DisplayTitleScreen();
        GameDataSetting();
        DisplayGameIntro();
    }

    static void DisplayTitleScreen()
    {
        Console.Clear();
        Console.WriteLine("********************************************************************************************************");
        Console.WriteLine("\r\n _                                    _             __    ______  _                 \r\n| |                                  | |           / _|   | ___ \\| |                \r\n| |      ___   __ _   ___  _ __    __| |     ___  | |_    | |_/ /| |_   __ _  _ __  \r\n| |     / _ \\ / _` | / _ \\| '_ \\  / _` |    / _ \\ |  _|   |    / | __| / _` || '_ \\ \r\n| |____|  __/| (_| ||  __/| | | || (_| |   | (_) || |     | |\\ \\ | |_ | (_| || | | |\r\n\\_____/ \\___| \\__, | \\___||_| |_| \\__,_|    \\___/ |_|     \\_| \\_| \\__| \\__,_||_| |_|\r\n               __/ |                                                                \r\n              |___/                                                                 \r\n");
        Console.WriteLine("********************************************************************************************************");

        Console.WriteLine("\n게임에 오신 것을 환영합니다!\n");
        Console.WriteLine("아무 키나 누르면 시작합니다...");
        Console.ReadKey();
    }

    static void GameDataSetting()
    {
        Console.WriteLine("새로운 모험을 시작합니다!");

        // 이름 입력 받기
        Console.Write("캐릭터의 이름을 입력하세요: ");
        string playerName = Console.ReadLine();

        // 직업 선택
        string job = ChooseClass();

        // 캐릭터 정보 세팅
        player = new Character(playerName, job, 1, 10, 5, 100, 1500, 50, 50); // MaxMana와 초기 Mana 추가

        // 직업에 따라 다른 아이템 지급
        switch (job)
        {
            case "전사":
                EquipmentItem sword = new EquipmentItem("검", 5, 0, 0, 0, 100);
                player.AddItem(sword);
                break;

            case "마법사":
                EquipmentItem staff = new EquipmentItem("지팡이", 3, 0, 0, 10, 100);
                player.AddItem(staff);
                Skill fireballSkill = new Skill("파이어볼", 20, 15); // 스킬에 마나 소비량 추가
                player.AddSkill(fireballSkill);
                break;

            case "레인저":
                EquipmentItem bow = new EquipmentItem("활", 4, 0, 0, 5, 10);
                player.AddItem(bow);
                Skill arrowShotSkill = new Skill("화살 쏘기", 12, 8); // 스킬에 마나 소비량 추가
                player.AddSkill(arrowShotSkill);
                break;
        }
    }

    static string ChooseClass() 
    {
        Console.WriteLine("직업을 선택하세요:");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("1. 전사");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("2. 마법사");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("3. 레인저");
        Console.ForegroundColor = ConsoleColor.Gray;
        int classChoice = CheckValidInput(1, 3);

        switch (classChoice)
        {
            case 1:
                return "전사";

            case 2:
                return "마법사";

            case 3:
                return "레인저";

            default:
                return ""; // 예외 처리를 위한 기본값
        }
    }

    static void DisplayGameIntro()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine("4. 던전 입장");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(1, 4);
        switch (input)
        {
            case 1:
                DisplayMyInfo();
                break;

            case 2:
                DisplayInventory();
                break;

            case 3:
                DisplayShop();
                break;

            case 4:
                DisplayDungeonMenu();
                break;
        }
    }

    static void DisplayMyInfo()
    {
        Console.Clear();
        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 정보를 표시합니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");

        if (player.EquippedItem != null)
        {
            Console.WriteLine($"공격력 : {player.Atk} + ({player.EquippedItem.AtkBonus})");
            Console.WriteLine($"방어력 : {player.Def} + ({player.EquippedItem.DefBonus})");
            DisplayHealthBar(player.Hp, player.EquippedItem.HpBonus);
            DisplayManaBar(player.Mana, player.MaxMana);
        }
        else
        {
            Console.WriteLine($"공격력 : {player.Atk}");
            Console.WriteLine($"방어력 : {player.Def}");
            DisplayHealthBar(player.Hp, 0);
            DisplayManaBar(player.Mana, player.MaxMana);
        }

        Console.WriteLine($"체력 : {player.Hp}/{player.MaxHp}");
        Console.WriteLine($"마나 : {player.Mana}/{player.MaxMana}");
        Console.WriteLine($"Gold : {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
        }
    }

    static void DisplayHealthBar(int currentHp, int bonusHp)
    {
        const int maxHealth = 100; // 최대 체력
        int totalHealth = maxHealth + bonusHp;
        int displayedHealth = currentHp + bonusHp;

        int barLength = 20; // 체력바 길이

        int filledLength = (int)((double)displayedHealth / totalHealth * barLength);
        int emptyLength = barLength - filledLength;

        Console.Write("체력 : [");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(new string('#', filledLength));
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(new string('.', emptyLength));
        Console.WriteLine($"] {displayedHealth}/{totalHealth}");
    }

    static void DisplayManaBar(int currentMana, int maxMana)
    {
        const int maxManaValue = 50; // 최대 마나
        int totalMana = maxMana + maxManaValue;
        int displayedMana = currentMana + maxManaValue;

        int barLength = 20; // 마나바 길이

        int filledLength = (int)((double)displayedMana / totalMana * barLength);
        int emptyLength = barLength - filledLength;

        Console.Write("마나 : [");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(new string('#', filledLength));
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(new string('.', emptyLength));
        Console.WriteLine($"] {displayedMana}/{totalMana}");
    }


    static void DisplayInventory()
    {
        Console.Clear();

        Console.WriteLine("인벤토리");
        Console.WriteLine("아이템 목록을 표시합니다.");
        Console.WriteLine();

        // 인벤토리에 아이템이 있는지 확인 후 표시
        if (player.Inventory.Count == 0)
        {
            Console.WriteLine("인벤토리가 비어있습니다.");
        }
        else
        {
            Console.WriteLine("인벤토리 아이템 목록:");

            for (int i = 0; i < player.Inventory.Count; i++)
            {
                Item item = player.Inventory[i];

                // 장착한 아이템이면 '[E]' 추가
                if (player.EquippedItem == item)
                {
                    Console.WriteLine($"[E] {i + 1}. {item.Name}");
                }
                else
                {
                    Console.WriteLine($"{i + 1}. {item.Name}");
                }
            }

            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, player.Inventory.Count);
            if (input == 0)
            {
                DisplayGameIntro();
            }
            else
            {
                DisplayItemOptions(player.Inventory[input - 1]);
            }
        }
    }

    static void DisplayItemOptions(Item item)
    {
        Console.Clear();

        Console.WriteLine($"아이템 정보 - {item.Name}");

        if (item.AtkBonus > 0)
        {
            Console.WriteLine($"공격 보너스: {item.AtkBonus}");
        }

        if (item.DefBonus > 0)
        {
            Console.WriteLine($"방어 보너스: {item.DefBonus}");
        }

        if (item.HpBonus > 0)
        {
            Console.WriteLine($"체력 보너스: {item.HpBonus}");
        }

        // 아이템의 타입에 따라 다른 옵션 표시
        if (item is EquipmentItem)
        {
            Console.WriteLine("1. 장착하기");
            if (player.EquippedItem == item)
            {
                Console.WriteLine("2. 장착 해제");
            }
        }
        else if (item is ConsumableItem)
        {
            Console.WriteLine("1. 사용하기");
        }
        else
        {
            Console.WriteLine("1. 정보 보기");
        }

        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, item is EquipmentItem ? 2 : 1);
        switch (input)
        {
            case 0:
                DisplayInventory();
                break;
            case 1:
                if (item is EquipmentItem)
                {
                    player.EquipItem(item as EquipmentItem);
                    Console.WriteLine($"{item.Name}을(를) 장착했습니다.");
                }
                else if (item is ConsumableItem)
                {
                    player.UseConsumableItem(item as ConsumableItem);
                }
                else
                {
                    DisplayItemInfo(item);
                }
                Console.WriteLine("아무 키나 누르면 인벤토리로 돌아갑니다...");
                Console.ReadKey();
                DisplayInventory();
                break;
            case 2:
                if (item is EquipmentItem)
                {
                    player.UnequipItem(item as EquipmentItem);
                    Console.WriteLine($"{item.Name}을(를) 장착 해제했습니다.");
                    Console.WriteLine("아무 키나 누르면 인벤토리로 돌아갑니다...");
                    Console.ReadKey();
                    DisplayInventory();
                }
                break;
        }
    }


    static void DisplayItemInfo(Item item)
    {
        Console.Clear();

        Console.WriteLine($"아이템 정보 - {item.Name}");

        if (item is EquipmentItem equipmentItem)
        {
            Console.WriteLine($"공격 보너스: {equipmentItem.AtkBonus}");
            Console.WriteLine($"방어 보너스: {equipmentItem.DefBonus}");
            Console.WriteLine($"체력 보너스: {equipmentItem.HpBonus}");
            Console.WriteLine();
            Console.WriteLine("1. 장착하기");

            // 장착한 아이템이면 해제 옵션 추가
            if (player.EquippedItem == item)
            {
                Console.WriteLine("2. 장착 해제");
            }
        }
        else
        {
            Console.WriteLine("(장비 아이템이 아닙니다.)");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
        }

        int input = CheckValidInput(0, player.EquippedItem == item ? 2 : 1);
        switch (input)
        {
            case 0:
                DisplayInventory();
                break;
            case 1:
                if (item is EquipmentItem)
                {
                    player.EquipItem(item); // 아이템을 장착
                    Console.WriteLine($"{item.Name}을(를) 장착했습니다.");
                }
                else
                {
                    Console.WriteLine("이 아이템은 장비 아이템이 아닙니다.");
                }

                Console.WriteLine("아무 키나 누르면 인벤토리로 돌아갑니다...");
                Console.ReadKey();
                DisplayInventory();
                break;
            case 2:
                if (item is EquipmentItem)
                {
                    player.UnequipItem(item); // 아이템 장착 해제
                    Console.WriteLine($"{item.Name}을(를) 장착 해제했습니다.");
                }
                else
                {
                    Console.WriteLine("이 아이템은 장비 아이템이 아닙니다.");
                }

                Console.WriteLine("아무 키나 누르면 인벤토리로 돌아갑니다...");
                Console.ReadKey();
                DisplayInventory();
                break;
        }
    }
    static void DisplayShop()
    {
        Console.Clear();

        Console.WriteLine("상점");
        Console.WriteLine($"현재 골드: {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("상품 목록:");

        // 상품 목록 표시
        foreach (var item in GetShopItems())
        {
            string status = player.Inventory.Contains(item) ? "구매완료" : $"{item.Name} - 가격: {item.Price} G";
            Console.WriteLine(status);
        }

        Console.WriteLine();
        Console.WriteLine("1. 구매하기");
        Console.WriteLine("2. 판매하기");
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 2);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;

            case 1:
                DisplayBuyMenu(); // 구매 메뉴 표시
                break;

            case 2:
                DisplaySellMenu(); // 판매 메뉴 표시
                break;
        }
    }

    static List<Item> GetShopItems()
    {
        // 상점 아이템 목록
        List<Item> shopItems = new List<Item>
    {
        new EquipmentItem("스파르타 검", 10, 0, 0, 0, 300),
        new EquipmentItem("건강부", 0, 0, 10, 0, 300),
        new EquipmentItem("요정의 활", 8, 2, 0, 10, 300),
        new EquipmentItem("화염의 오브", 3, 0, 0, 15, 300),
        new HealingPotion("힐링 포션", 20, 50),
        new HealingPotion("독약", -20, 50)
    };

        return shopItems;
    }
    static void DisplayBuyMenu()
    {
        Console.Clear();

        Console.WriteLine("구매하기");
        Console.WriteLine($"현재 골드: {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("상품 목록:");

        // 상품 목록 표시
        List<Item> shopItems = GetShopItems();
        for (int i = 0; i < shopItems.Count; i++)
        {
            Item item = shopItems[i];
            Console.WriteLine($"{i + 1}. {item.Name} - 가격: {item.Price} G");
        }

        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, shopItems.Count);
        if (input == 0)
        {
            DisplayShop();
        }
        else
        {
            BuyItem(shopItems[input - 1]);
        }
    }

    static void BuyItem(Item item)
    {
        if (player.Gold >= item.Price)
        {
            // 골드가 충분한 경우
            player.Gold -= item.Price; // 골드 차감
            player.AddItem(item); // 아이템 추가
            Console.WriteLine($"{item.Name}을(를) 구매했습니다.");
        }
        else
        {
            // 골드가 부족한 경우
            Console.WriteLine("골드가 부족하여 구매할 수 없습니다.");
        }

        Console.WriteLine("아무 키나 누르면 상점으로 돌아갑니다...");
        Console.ReadKey();
        DisplayShop();
    }
    static void DisplaySellMenu()
    {
        Console.Clear();

        Console.WriteLine("판매 메뉴");
        Console.WriteLine($"현재 골드: {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("인벤토리 아이템 목록:");

        // 인벤토리에 아이템이 있는지 확인 후 표시
        if (player.Inventory.Count == 0)
        {
            Console.WriteLine("인벤토리가 비어있습니다.");
        }
        else
        {
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                Item item = player.Inventory[i];
                Console.WriteLine($"{i + 1}. {item.Name} - 가격: {item.Price} G");
            }

            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, player.Inventory.Count);
            if (input == 0)
            {
                DisplayGameIntro();
            }
            else
            {
                SellItem(player.Inventory[input - 1]);
            }
        }
    }

    static void SellItem(Item item)
    {
        Console.Clear();

        Console.WriteLine($"아이템 판매 - {item.Name}");
        Console.WriteLine($"판매 가격: {item.Price} G");
        Console.WriteLine();
        Console.WriteLine("1. 판매하기");
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                DisplaySellMenu();
                break;

            case 1:
                SellConfirmed(item);
                break;
        }
    }

    static void SellConfirmed(Item item)
    {
        // 아이템 판매 처리
        player.Gold += item.Price;
        player.Inventory.Remove(item);

        Console.WriteLine($"{item.Name}을(를) 성공적으로 판매했습니다.");
        Console.WriteLine($"현재 골드: {player.Gold} G");
        Console.WriteLine("아무 키나 누르면 나가기...");
        Console.ReadKey();

        // 판매 후 다시 판매 메뉴 표시
        DisplaySellMenu();
    }

    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }

            Console.WriteLine("잘못된 입력입니다.");
        }
    }
    static void DisplayDungeonMenu()
    {
        Console.Clear();

        Console.WriteLine("어느 던전으로 입장하시겠습니까?");
        Console.WriteLine("1. 축축한 공동 (쉬움)");
        Console.WriteLine("2. 고블린 야영지 (보통)");
        Console.WriteLine("3. 오크 군락 (어려움)");
        Console.WriteLine("0. 뒤로 가기");

        int input = CheckValidInput(0, 3);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;

            case 1:
                StartDungeon(new Dungeon("축축한 공동", new Monster("슬라임", 5, 2, 0, 15, 10)));
                break;

            case 2:
                StartDungeon(new Dungeon("고블린 야영지", new Monster("고블린", 10, 5, 15, 30, 30)));
                break;

            case 3:
                StartDungeon(new Dungeon("오크 군락", new Monster("오크", 15, 8, 20, 50, 50 )));
                break;
        }
    }

    static void StartDungeon(Dungeon dungeon)
    {
        Console.Clear();
        Console.WriteLine($"당신은 {dungeon.Name}에 입장했습니다.");

        Battle battle = new Battle(player, dungeon.Monster);
        battle.StartBattle();

        if (player.IsAlive)
        {
            Console.WriteLine($"던전을 클리어했습니다! 획득한 골드: {dungeon.Monster.Gold}");
            player.Gold += dungeon.Monster.Gold;
        }
        else
        {
            Console.WriteLine("던전에서 패배했습니다. 게임 오버!");
            Console.WriteLine("아무 키나 누르면 계속하세요...");
            Console.ReadKey();
            DisplayGameIntro(); // 게임 오버 시 메뉴로 돌아감
        }

        Console.WriteLine("아무 키나 누르면 계속하세요...");
        Console.ReadKey();
        DisplayGameIntro();
    }
}

