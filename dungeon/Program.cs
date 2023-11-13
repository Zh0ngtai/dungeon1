using System;
using System.Collections.Generic;

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
        player = new Character(playerName, job, 1, 10, 5, 100, 1500);

        // 직업에 따라 다른 아이템 지급
        switch (job)
        {
            case "전사":
                Item sword = new Item("검", 5, 0, 0);
                player.AddItem(sword);
                break;

            case "마법사":
                Item staff = new Item("지팡이", 3, 0, 0);
                player.AddItem(staff);
                break;

            case "레인저":
                Item bow = new Item("활", 4, 0, 0);
                player.AddItem(bow);
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
        Console.ForegroundColor = ConsoleColor.Gray;//<< 다시 원래대로 색깔을 돌려줘야함...
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
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                DisplayMyInfo();
                break;

            case 2:
                DisplayInventory();
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
        Console.WriteLine($"공격력 :{player.Atk}");
        Console.WriteLine($"방어력 : {player.Def}");
        Console.WriteLine($"체력 : {player.Hp}");
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
                Console.WriteLine($"{i + 1}. {item.Name}");
            }

            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, player.Inventory.Count);
            if (input == 0)
            {
                DisplayGameIntro();
            }
            else
            {
                DisplayItemInfo(player.Inventory[input - 1]);
            }
        }
    }

    static void DisplayItemInfo(Item item)
    {
        Console.Clear();

        Console.WriteLine($"아이템 정보 - {item.Name}");
        Console.WriteLine($"공격 보너스: {item.AtkBonus}");
        Console.WriteLine($"방어 보너스: {item.DefBonus}");
        Console.WriteLine($"체력 보너스: {item.HpBonus}");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 0);
        if (input == 0)
        {
            DisplayInventory();
        }
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
}

public class Character
{
    public string Name { get; }
    public string Job { get; }
    public int Level { get; }
    public int Atk { get; }
    public int Def { get; }
    public int Hp { get; }
    public int Gold { get; }

    private List<Item> inventory;

    public Character(string name, string job, int level, int atk, int def, int hp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
        inventory = new List<Item>();
    }

    public void AddItem(Item item)
    {
        inventory.Add(item);
    }

    public List<Item> Inventory
    {
        get { return inventory; }
    }
}

public class Item
{
    public string Name { get; }
    public int AtkBonus { get; }
    public int DefBonus { get; }
    public int HpBonus { get; }

    public Item(string name, int atkBonus, int defBonus, int hpBonus)
    {
        Name = name;
        AtkBonus = atkBonus;
        DefBonus = defBonus;
        HpBonus = hpBonus;
    }
}
