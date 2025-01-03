namespace Game.Model.WitchesAndPlayersNamespace
{
    class Darkness : Witches
    {
        private bool CanAttack;
        public Darkness()
        {
            WitchType = "Bruja de Oscuridad";
            Speed = 6;
            AttackSkill = "Muerte Súbita";
            DefenseSkill = "Pacto con el Diablo";
            AttackRange = 2;
            CanAttack = true;
        }
        public override int Attack()
        {
            if (CanAttack)
            {
                CanAttack = false;
                return 100;
            }
            return 0;
        }
        public override int Defense()
        {
            if (Speed>3&&CoolingTime[(int)CoolingTimeCodes.Defense] == 0)
            {
                CoolingTime[(int)CoolingTimeCodes.Defense] = 3;
                DefenseUsed();
                Speed = 3;
                return 100;
            }
            return 0;
        }
        public override bool IsImmune()=> false;
        public override void RecoverSpeed()=>Speed = 6;
        public override void DefenseUsed()
        {
            CanAttack = true;
        }
    }
}