namespace Game.Model.WitchesAndPlayersNamespace
{
    class Air:Witches
    {
        public bool Immunity{get; private set;}
        public Air()
        {
            WitchType = "Bruja de Aire";
            Speed = 4;
            AttackSkill = "Viento Cortante";
            DefenseSkill = "Barrera de Aire";
            Immunity=false;
            AttackRange = 3;
        }
        public override int Attack()
        {
            if(CoolingTime[(int)CoolingTimeCodes.Attack]==0)
            {
                CoolingTime[(int)CoolingTimeCodes.Attack]=3;
                return 30;
            }
            return 0;
        }
        public override int Defense()
        {
            if (Speed>3&&CoolingTime[(int) CoolingTimeCodes.Defense] == 3)
            {
                Immunity = true;
                CoolingTime[(int) CoolingTimeCodes.Defense] = 3;
                Speed = 3;
            }
            return 0;
        }
        public override void RecoverSpeed()=> Speed = 4;
        public override bool IsImmune() => Immunity;
        public override void DefenseUsed()=> Immunity = false;
    }
}