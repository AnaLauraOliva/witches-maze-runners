namespace Game.Model.WitchesAndPlayersNamespace
{
    class Water : Witches
    {
        public bool Defrost { get; private set; }
        public Water()
        {
            WitchType = "Bruja de Agua";
            Speed = 5;
            AttackSkill = "Látigo de Agua";
            DefenseSkill = "Gota de Vida";
            Defrost=false;
            AttackRange = 3;
        }
        public override int Attack()
        {
            if (CoolingTime[(int)CoolingTimeCodes.Attack] == 0)
            {
                CoolingTime[(int)CoolingTimeCodes.Attack] = 2;
                return 35;
            }
            return 0;
        }
        public override int Defense()
        {
            if (Speed>3&&CoolingTime[(int) CoolingTimeCodes.Defense] == 3)
            {
                Defrost = true;
                CoolingTime[(int) CoolingTimeCodes.Defense] = 3;
                Speed = 3;
            }
            return 0;
        }
        public override bool IsImmune()=> false;
        public override void RecoverSpeed()=> Speed = 5;
        public override void DefenseUsed()=> Defrost = false;
    }
}