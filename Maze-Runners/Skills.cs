using Maze_Runners;
namespace CharactersAndSkills
{
    class Skills
    {
        public string skillName = "";
        private int damage;
        private int effectiveRange;

        public bool defrost = false;
        public bool immunity = false;
        private int lossOfSpeed;
        private int coolingTime;
        public Skills(int code, string skillType)
        {
            if (skillType == "attack")
            {
                string[] AttackSkills = {"Látigo de Agua","Punzón de Roca"
                        ,"Ascuas","Viento Cortante","Muerte Súbita","Luz de Diosa"};
                int[] Damages = { -15, -20, -25, -30, -100, -50 };
                if (code == 1 || code == 4 || code == 5) effectiveRange = 2;
                else effectiveRange = 1;
                int[] CoolingTime = { 2, 3, 3, 3, 0, 4 };
                skillName = AttackSkills[code];
                damage = Damages[code];
                coolingTime = CoolingTime[code];
            }
            else if (skillType == "defense")
            {
                string[] DefenceSkills = {"Gota de Vida", "Bendición de la Madre Tierra",
                "Danza del Dios del Fuego","Barrera de Aire", "Pacto con el Diablo",
                 "Curación"};
                int[] LossOfSpeed = { -2, -2, -1, -1, -3, -3 };

                lossOfSpeed = LossOfSpeed[code];
                skillName = DefenceSkills[code];
            }
        }
        public bool DefenseSkill(Player currentPlayer)
        {
            if (currentPlayer.speed + lossOfSpeed > 0)
            {
                if (skillName == "Curación" || skillName == "Pacto con el Diablo") currentPlayer.hp = 100;
                else if (skillName == "Gota de Vida" || skillName == "Bendición de la Madre Tierra") defrost = true;
                else immunity = true;
                currentPlayer.speed += lossOfSpeed;
                currentPlayer.defenseCoolingTime += 3;
                return true;
            }
            return false;
        }
        public void AttackSkill(Player[] enemies, Player currentPlayer)
        {
            if (currentPlayer.attackCoolingTime == 0)
            {
                for (global::System.Int32 i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].positionx == currentPlayer.positionx && enemies[i].positiony == currentPlayer.positiony) continue;
                    if (Conditions(enemies[i], currentPlayer))
                    {
                        enemies[i].hp += damage;
                    }
                }
                if (currentPlayer.code==6)currentPlayer.hp-=50;
                currentPlayer.attackCoolingTime = coolingTime;
            }
            
        }
        public bool Conditions(Player enemy, Player currentPlayer)
        {
            return enemy.positionx <= currentPlayer.positionx + effectiveRange
                && enemy.positionx >= currentPlayer.positionx - effectiveRange
                && enemy.positiony <= currentPlayer.positiony + effectiveRange
                && enemy.positiony >= currentPlayer.positiony - effectiveRange
                && noInmunity(enemy);
        }
        public bool noInmunity(Player player)
        {
            if (player.defense.immunity)
            {
                if (damage < 50)
                {
                    player.defense.immunity=false;
                    return false;
                }
                
            }
            return true;
        }
    }
}