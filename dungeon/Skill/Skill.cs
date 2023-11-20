using MyGame;

namespace Rtangame;

public class Skill
{
    public string Name { get; }
    public int Damage { get; }
    public int ManaCost { get; }

    public Skill(string name, int damage, int manaCost)
    {
        Name = name;
        Damage = damage;
        ManaCost = manaCost;
    }

    public void Use(Character caster)
    {
        if (caster != null && caster.HasEnoughMana(ManaCost))
        {
            Console.WriteLine($"{caster.Name}이(가) {Name}을(를) 사용했습니다!");
            caster.ReduceMana(ManaCost);
        }
        else
        {
            Console.WriteLine($"{caster.Name}의 마나가 부족합니다!");
        }
    }
}
