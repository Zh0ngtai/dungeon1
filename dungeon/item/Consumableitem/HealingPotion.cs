

namespace MyGame;

public class HealingPotion : ConsumableItem
{
    public HealingPotion(string name, int effectAmount, int price)
        : base(name, effectAmount, price)
    {
    }
    protected override void ApplyEffect(Character character)
    {
        // 체력을 회복하는 효과를 캐릭터에게 적용
        Console.WriteLine($"{character.Name}에게 {EffectAmount}의 체력을 회복했습니다.");
        character.Heal(EffectAmount);
    }
}
