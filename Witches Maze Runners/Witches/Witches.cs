namespace Game.Model.WitchesAndPlayersNamespace
{
    abstract class Witches
    {
        protected enum CoolingTimeCodes
        {
            Attack = 0,
            Defense = 1
        }   
        public string? WitchType {get;protected set;}
        public int AttackRange{get;protected set;}
        public int Speed {get;protected set;}
        protected int[] CoolingTime = new int[2];
        public string? AttackSkill{get; protected set;}
        public string? DefenseSkill{get; protected set;}
        public void ReduceCoolingTime()
        {
            if(CoolingTime[0]>0) CoolingTime[0]--;
            if(CoolingTime[1]>0) CoolingTime[1]--;
        }
        public void retoreSpeedTreasure()=>CoolingTime[(int)CoolingTimeCodes.Defense]=0;
        public bool DefenseCoolingTimeOver()=>CoolingTime[(int)CoolingTimeCodes.Defense]==0;
        public abstract int Attack();
        public abstract (int,bool) Defense();
        public abstract bool IsImmune();
        public abstract bool IsDefrost();
        public abstract void RecoverSpeed();
        public abstract void DefenseUsed();
        public void LossOfSpeed()=>Speed = 1;
        public override string ToString()
        {
            return string.Format($"   Tiempo de espera de ataque: {CoolingTime[(int)CoolingTimeCodes.Attack]}"+
            $"   Tiempo de espera de defensa: {CoolingTime[(int)CoolingTimeCodes.Defense]}");
        }
    }
}