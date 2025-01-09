using System.Dynamic;
using Game.Model.WitchesAndPlayersNamespace;
namespace Game.Model
{
    class Player
    {
        private enum EffectsDurationCodes
        {
            Freeze = 0,
            LossOfSpeed = 1
        }
        public string Name { get; private set; }
        public Witches Witch { get; private set; }
        private int HP;
        private (int, int) position;
        private int[] EffectsDuration = new int[2];
        public Player(string name, Witches witch)
        {
            Name = name;
            Witch = witch;
            HP = 100;

        }
        public bool Effects() => EffectsDuration[(int)EffectsDurationCodes.Freeze] == 0;
        public void ReduceEffects()
        {
            if (EffectsDuration[0] > 0) EffectsDuration[0] -= 1;
            if (EffectsDuration[1] > 0) EffectsDuration[1] -= 1;
            Witch.ReduceCoolingTime();
            if (Witch.DefenseCoolingTimeOver() &&
            EffectsDuration[(int)EffectsDurationCodes.LossOfSpeed] == 0)
                Witch.RecoverSpeed();
        }
        public bool Attack(List<Player> players)
        {
            int damage = Witch.Attack();
            if (damage == 100 && this.HP > 50) this.HP -= 50;
            else if (damage == 100 && this.HP <= 50) return false;
            if (damage > 0)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    if (Conditions(players[i]))
                    {
                        if (players[i].Witch.IsImmune()) players[i].Witch.DefenseUsed();
                        else
                        {
                            players[i].HP -= damage;
                        }
                    }
                }

                return true;
            }
            return false;
        }
        public bool Defense()
        {
            (int, bool) defense = Witch.Defense();
            if (defense.Item1 != 0) HP = defense.Item1;
            return defense.Item2;
        }
        private bool Conditions(Player other)
        {
            return other.Witch.WitchType != this.Witch.WitchType
            && other.position.Item1 >= this.position.Item1 - Witch.AttackRange
            && other.position.Item2 <= this.position.Item2 + Witch.AttackRange
            && other.position.Item1 <= this.position.Item1 + Witch.AttackRange
            && other.position.Item2 >= this.position.Item2 - Witch.AttackRange;
        }
        public void UpdatePlayerPosition(int Row, int Col)
        {
            position.Item1 = Row;
            position.Item2 = Col;
        }
        public (int, int) GetPlayerPosition() => position;
        public bool PlayerDeath(List<string> Narration)
        {
            if (HP <= 0)
            {
                Narration.Add($"{Name} murió :)");
                EffectsDuration[(int)EffectsDurationCodes.Freeze] = 2;
                HP = 100;
                return true;
            }
            return false;
        }
        public bool ConvertToEffect(int Effect, List<string> Narration)
        {
            switch (Effect)
            {
                case 0:
                    return false;
                case 1:
                    if (!Witch.IsDefrost())
                    {
                        EffectsDuration[(int)EffectsDurationCodes.Freeze] = 3;
                        Narration.Add($"{Name} cayó en una trampa de congelamiento");
                    }
                    else{Narration.Add($"{Name} usó su habilidad para evitar el congelamiento");}
                    break;
                case 2:
                    EffectsDuration[(int)EffectsDurationCodes.LossOfSpeed] = 3;
                    Witch.LossOfSpeed();
                    Narration.Add($"{Name} cayó en una trampa de pérdida de velocidad");
                    break;
                case 3:
                    HP -= 15;
                    Narration.Add($"{Name} cayó en una trampa de daño");
                    break;
                case 4:
                    HP = HP > 90 ? 100 : HP + 10;
                    Narration.Add($"{Name} recuperó puntos de vida, HP actual:{HP}");
                    break;
                case 5:
                    EffectsDuration[(int)EffectsDurationCodes.LossOfSpeed] = 0;
                    Witch.retoreSpeedTreasure();
                    Narration.Add($"{Name} recuperó todos tus puntos de velocidad");
                    break;
            }
            return true;
        }
        public override string ToString()
        {

            return string.Format($"Nombre:{Name}   Tipo de bruja: {Witch.WitchType}   Puntos de Vida: {HP}" +
            $"\nL- Ataque   K- Defensa" +
            $"{Witch.ToString()}");
        }
    }
}