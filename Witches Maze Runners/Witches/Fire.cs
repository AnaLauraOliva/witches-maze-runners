namespace Game.Model.WitchesAndPlayersNamespace
{
    class Fire:Witches
    {
        public bool Defrost{get; private set;}
        public Fire()
        {
            WitchType = "Bruja de Fuego";
            Speed = 4;
            AttackSkill = "Ascuas";
            DefenseSkill = "Danza del Dios del Fuego";
            Defrost=false;
            AttackRange = 2;
        }
        public override int Attack()
        {
            if (CoolingTime[(int)CoolingTimeCodes.Attack]==0)
            {
                CoolingTime[(int)CoolingTimeCodes.Attack]=3;
                return 25;
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
        public override void RecoverSpeed()=> Speed = 4;
        public override void DefenseUsed()=> Defrost = false;
    }
}