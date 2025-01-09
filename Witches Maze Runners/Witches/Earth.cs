namespace Game.Model.WitchesAndPlayersNamespace
{
    class Earth:Witches
    {
        public bool Immunity{get; private set;}
        public Earth()
        {
            WitchType = "Bruja de Tierra";
            Speed = 5;
            AttackSkill = "Punzón de Roca";
            DefenseSkill = "Bendición de la Madre Tierra";
            Immunity=false;
            AttackRange = 2;
        }
        public override int Attack()
        {
            if(CoolingTime[(int)CoolingTimeCodes.Attack] == 0)
            {
                CoolingTime[(int)CoolingTimeCodes.Attack] = 3;
                return 20;
            }
            return 0;
        }
        public override (int,bool) Defense()
        {
            if (Speed>3&&CoolingTime[(int) CoolingTimeCodes.Defense] == 0)
            {
                Immunity = true;
                CoolingTime[(int) CoolingTimeCodes.Defense] = 3;
                Speed = 3;
                return (0,true);
            }
            return (0,false);
        }
        public override bool IsImmune()=>Immunity;
        public override bool IsDefrost() => false;
        public override void RecoverSpeed()=> Speed = 5;
        public override void DefenseUsed()=> Immunity = false;
    }
}