namespace MyGame
{
    public class Item
    {
        public string Name { get; }
        public int AtkBonus { get; }
        public int DefBonus { get; }
        public int HpBonus { get; }
        public int Price { get; }

        public Item(string name, int atkBonus, int defBonus, int hpBonus, int price)
        {
            Name = name;
            AtkBonus = atkBonus;
            DefBonus = defBonus;
            HpBonus = hpBonus;
            Price = price;
        }
    }
}