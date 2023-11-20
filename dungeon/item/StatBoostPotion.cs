

namespace MyGame;

public class StatBoostPotion : ConsumableItem
{
    public StatBoostPotion(string name, int effectAmount, int price)
        : base(name, effectAmount, price)
    {
    }

    protected override void ApplyEffect(Character character)
    {
        // 스탯을 올리는 효과를 캐릭터에게 적용
        Console.WriteLine($"{character.Name}에게 {EffectAmount}의 스탯 효과를 적용했습니다.");
    }
}
