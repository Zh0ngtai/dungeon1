namespace MyGame;

public class EquipmentItem : Item
{
    public EquipmentItem(string name, int atkBonus, int defBonus, int hpBonus, int MpBonus, int price)
        : base(name, atkBonus, defBonus, hpBonus, price)
    {
    }
}
