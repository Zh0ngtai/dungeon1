namespace MyGame;

public class Monster
{
    public string Name { get; }
    public int Level { get; }
    public int Atk { get; }
    public int Def { get; }
    public int MaxHp { get; private set; }
    public int Hp { get; private set; }
    public int Gold { get; set; }
    public Monster(string name, int level, int atk, int def, int hp, int gold)
    {
        Name = name;
        Level = level;
        Atk = atk;
        Def = def;
        MaxHp = hp;
        Hp = hp;
        Gold = gold;
    }

    public bool IsAlive
    {
        get { return Hp > 0; }
    }

    public int DealDamage()
    {
        // 몬스터의 공격력을 반환
        return Atk;
    }

    public void TakeDamage(int damage)
    {
        // 몬스터가 데미지를 받는 로직
        Hp = Math.Max(0, Hp - damage);
    }
}
