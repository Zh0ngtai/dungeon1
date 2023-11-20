namespace MyGame
{
    public class Battle
    {
        private Character player;
        private Monster monster;
        private Action onBattleEnd;

        public Battle(Character player, Monster monster)
        {
            this.player = player;
            this.monster = monster;
            this.onBattleEnd = onBattleEnd;

        }
        private void DisplayGameIntroOnBattleEnd()
        {
            onBattleEnd?.Invoke();
        }

        public void StartBattle()
        {
            Console.WriteLine($"전투 시작! {player.Name} vs {monster.Name}");

            while (player.IsAlive && monster.IsAlive)
            {
                DisplayBattleStatus();

                // 플레이어의 턴
                PlayerTurn();

                // 몬스터의 턴
                if (monster.IsAlive)
                {
                    MonsterTurn();
                }
            }

            BattleResult result;
            if (player.IsAlive)
            {
                result = BattleResult.Victory;
            }
            else if (monster.IsAlive)
            {
                result = BattleResult.Defeat;
            }
            else
            {
                result = BattleResult.Escape;
            }

            DisplayBattleResult(result);
        }

        private void DisplayBattleStatus()
        {
            Console.WriteLine("\n-------- 전투 상태 --------");
            Console.WriteLine($"{player.Name} (Lv.{player.Level}) - HP: {player.Hp}/{player.MaxHp}");
            Console.WriteLine($"{monster.Name} (Lv.{monster.Level}) - HP: {monster.Hp}/{monster.MaxHp}");
            Console.WriteLine("--------------------------\n");
        }

        private void PlayerTurn()
        {
            Console.WriteLine($"{player.Name}의 턴");
            Console.WriteLine("1. 일반 공격");
            Console.WriteLine("2. 스킬 사용 (미구현)");
            Console.WriteLine("0. 도망가기");

            int choice = CheckValidInput(0, 2);

            switch (choice)
            {
                case 1:
                    int playerDamage = CalculateDamage(player.Atk, monster.Def);
                    monster.TakeDamage(playerDamage);
                    Console.WriteLine($"{player.Name}이(가) {monster.Name}에게 {playerDamage}의 피해를 입혔습니다!");
                    break;

                case 2:
                    // 스킬 사용 코드 추가
                    Console.WriteLine("미구현");
                    break;

                case 0:
                    Console.WriteLine($"{player.Name}이(가) 도망쳤습니다!");
                    DisplayBattleResult(BattleResult.Escape); // 도망치기 사용 시 메인메뉴로 간다.
                    break;
            }
        }

        private void MonsterTurn()
        {
            Console.WriteLine($"{monster.Name}의 턴");
            int monsterDamage = CalculateDamage(monster.Atk, player.Def);
            player.TakeDamage(monsterDamage);
            Console.WriteLine($"{monster.Name}이(가) {player.Name}에게 {monsterDamage}의 피해를 입혔습니다!");
        }
        public enum BattleResult
        {
            Victory,
            Escape,
            Defeat
        }
        private void DisplayBattleResult(BattleResult result)
        {
            switch (result)
            {
                case BattleResult.Victory:
                    Console.WriteLine("전투에서 승리했습니다!");
                    player.Gold += monster.Gold;
                    Console.WriteLine($"전리품으로 {monster.Gold} G를 획득했습니다.");
                    break;

                case BattleResult.Escape:
                    Console.WriteLine("전투에서 도망쳤습니다...");
                    break;

                case BattleResult.Defeat:
                    Console.WriteLine("전투에서 패배했습니다...");
                    break;
            }


        }


        private int CalculateDamage(int attackerAtk, int defenderDef)
        {
            int damage = attackerAtk - defenderDef;
            return Math.Max(damage, 0); // 데미지가 음수가 되지 않도록 보정
        }


        private int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var result);
                if (parseSuccess && result >= min && result <= max)
                    return result;

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}