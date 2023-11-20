

using Rtangame;

namespace MyGame;

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
        Console.WriteLine("2. 스킬 사용");
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
                UseSkill();
                break;

            case 0:
                Console.WriteLine($"{player.Name}이(가) 도망쳤습니다!");
                DisplayBattleResult(BattleResult.Escape);
                break;
        }
    }
    private void UseSkill()
    {
        Console.WriteLine("사용할 스킬을 선택하세요:");

        // 플레이어가 보유한 스킬 목록을 표시
        for (int i = 0; i < player.Skills.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {player.Skills[i].Name} (마나 소비: {player.Skills[i].ManaCost})");
        }

        Console.WriteLine("0. 뒤로가기");

        int skillChoice = CheckValidInput(0, player.Skills.Count);

        if (skillChoice == 0)
        {
            return; // 뒤로가기 선택 시 아무것도 하지 않음
        }

        Skill selectedSkill = player.Skills[skillChoice - 1];

        // 스킬 사용
        selectedSkill.Use(player);

        // 몬스터에게 피해 입히기
        int damage = CalculateDamage(selectedSkill.Damage, monster.Def);
        monster.TakeDamage(damage);
        Console.WriteLine($"{player.Name}이(가) {monster.Name}에게 {damage}의 피해를 입혔습니다!");
    }
    private void DisplaySkills()
    {
        Console.WriteLine("사용 가능한 스킬 목록:");
        for (int i = 0; i < player.Skills.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {player.Skills[i].Name} ({player.Skills[i].Damage} 피해)");
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



