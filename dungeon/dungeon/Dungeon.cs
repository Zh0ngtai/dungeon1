namespace MyGame
{
    public class Dungeon : IDungeon
    {
        public string Name { get; }
        public Monster Monster { get; }

        public Dungeon(string name, Monster monster)
        {
            Name = name;
            Monster = monster;
        }
    }
}
