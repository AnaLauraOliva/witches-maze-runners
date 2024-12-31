namespace Game.Model.MazeNamespace
{
    internal class Cells
    {
        public bool Available{get; private set;}
        public bool Traps {get; private set;}
        private TrapsAndTreasures trapsAndTreasures=new TrapsAndTreasures();
        internal Cells()
        {
            Available = false;
            Traps = false;
        }
        public void PutTrapOrTreasure(int code)
        {
            Traps = true;
            trapsAndTreasures = new TrapsAndTreasures(code);
        }
        public int FallIntoTrap()
        {
            Traps = false;
            return trapsAndTreasures.FallIntoTrap();
        }
        public void  MakeAvailable()
        {
            Available = true;
        }

    }
}