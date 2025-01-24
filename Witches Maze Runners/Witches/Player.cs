using System.Dynamic;
using Game.Model.WitchesAndPlayersNamespace;
namespace Game.Model
{
    class Player
    {

        public string Name { get; private set; }
        public Witches Witch { get; private set; }
        public int HP { get; private set; }
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
        public bool Attack(List<Player> players, List<string> Narration)
        {
            int damage = Witch.Attack();
            if (damage == 100 && this.HP > 50) this.HP -= 50;
            else if (damage == 100 && this.HP <= 50) return false;
            if (damage > 0)
            {
                Narration.Add(Witch.AttackSkill!);
                for (int i = 0; i < players.Count; i++)
                {
                    if (Conditions(players[i]))
                    {
                        if (players[i].Witch.IsImmune())
                        { 
                            Narration.Add($"{players[i].Name} usó la defensa para evitar el ataque");
                            players[i].Witch.DefenseUsed(); }
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
        internal void ChangeHP(int damage) => HP = damage<0?HP+damage:HP>=90?100:HP+damage;
        internal void TrapsEffects(int time, int index) => EffectsDuration[index] = time == 0 ? EffectsDuration[index] = 0 : EffectsDuration[index] += time;
        public override string ToString()
        {

            return string.Format($"Nombre:{Name}   Tipo de bruja: {Witch.WitchType}   Puntos de Vida: {HP}" +
            $"\nL- Ataque   K- Defensa" +
            $"{Witch.ToString()}");
        }
    }
}