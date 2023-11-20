

namespace MyGame;

public class ConsumableItem : Item
{
    public int EffectAmount { get; }

    public ConsumableItem(string name, int effectAmount, int price)
        : base(name, 0, 0, 0, price)
    {
        EffectAmount = effectAmount;
    }

    public void Use(Character character)
    {
        Console.WriteLine($"{character.Name}이(가) {Name}을(를) 사용했습니다.");
        ApplyEffect(character);
    }

    protected virtual void ApplyEffect(Character character)
        // 아이템의 효과를 캐릭터에게 적용
    {
        Console.WriteLine($"{character.Name}에게 {EffectAmount}의 효과를 적용했습니다.");
    }
}
