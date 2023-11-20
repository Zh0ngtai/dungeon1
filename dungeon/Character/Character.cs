using MyGame;
using Rtangame;

namespace MyGame;

public class Character
{
    public string Name { get; }
    public string Job { get; }
    public int Level { get; }
    public int Atk { get; }
    public int Def { get; }
    public int MaxHp { get; private set; }

    public int Hp { get; private set; }
    public int Gold { get; set; }
    public int MaxMana { get; private set; }
    public int Mana { get; private set; }
    public bool IsAlive
    {
        get { return Hp > 0; } // 캐릭터의 생존 여부를 보여주는 코드
    }

    private List<Item> inventory;
    private Item equippedItem;

    public void UnequipItem(Item item)
    {
        // 현재 장착된 아이템과 매개변수로 받은 아이템이 일치하면 해제
        if (equippedItem == item)
        {
            equippedItem = null;
        }
    }
    public void UseConsumableItem(ConsumableItem item)
    {
        if (item != null)
        {
            item.Use(this);
            inventory.Remove(item);
        }
    }
    public void TakeDamage(int damage)
    {
        // 최소 체력이 0보다 작아지지 않도록 조절
        Hp = Math.Max(0, Hp - damage);
    }



    public Character(string name, string job, int level, int atk, int def, int hp, int maxHp, int gold, int maxMana)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        MaxHp = hp;
        Gold = gold;
        inventory = new List<Item>();
        equippedItem = null;
        skills = new List<Skill>();
        MaxMana = maxMana;
        Mana = maxMana; // 캐릭터 생성 시 최대 마나로 초기화
    }
    public void Heal(int amount)
    {
        // 최대 체력을 넘지 않도록 조절
        Hp = Math.Min(MaxHp, Hp + amount);
    }
    public void AddItem(Item item)
    {
        inventory.Add(item);
    }

    public void EquipItem(Item item)
    {
        equippedItem = item;
    }
    public bool HasEnoughMana(int manaCost)
    {
        return Mana >= manaCost;
    }

    public void ReduceMana(int manaCost)
    {
        // 마나 감소
        Mana = Math.Max(0, Mana - manaCost);
    }

    public List<Item> Inventory
    {
        get { return inventory; }
    }

    public Item EquippedItem
    {
        get { return equippedItem; }
    }
    private List<Skill> skills; // 스킬 목록 추가

    public List<Skill> Skills
    {
        get { return skills; }
    }

    // 스킬 추가 메서드
    public void AddSkill(Skill skill)
    {
        skills.Add(skill);
    }
    public void DisplayManaBar()
    {
        const int maxManaBarLength = 20;
        int filledManaBarLength = (int)((double)Mana / MaxMana * maxManaBarLength);
        int emptyManaBarLength = maxManaBarLength - filledManaBarLength;

        Console.Write("마나 : [");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(new string('#', filledManaBarLength));
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(new string('.', emptyManaBarLength));
        Console.WriteLine($"] {Mana}/{MaxMana}");
    }
}
