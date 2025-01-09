namespace Game.Model.WitchesAndPlayersNamespace
{
    class Light:Witches
    {
        public Light()
        {
            WitchType = "Bruja de Luz";
            Speed = 6;
            AttackSkill = "Luz de Ángel";
            DefenseSkill = "Curación";
            AttackRange = 3;
        }
        public override int Attack()
        {
            if(CoolingTime[(int)CoolingTimeCodes.Attack] == 0)
            {
                CoolingTime[(int)CoolingTimeCodes.Attack] = 4;
                return 50;
            }
            return 0;
        }
       public override (int,bool) Defense()
        {
            if (Speed>3&&CoolingTime[(int)CoolingTimeCodes.Defense] == 0)
            {
                CoolingTime[(int)CoolingTimeCodes.Defense] = 3;
                Speed = 3;
                return (100,true);
            }
            return (0,false);
        }
        public override bool IsImmune()=> false;
        public override bool IsDefrost()=> false;
        public override void RecoverSpeed()=>Speed = 6;
        public override void DefenseUsed()
        {
        }
    }
}