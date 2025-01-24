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
        public void FallIntoTrap(Player currentPlayer,List<string> Narration, Gameboard gameboard)
        {
            Traps = false;
            trapsAndTreasures.FallIntoTrap(currentPlayer, Narration, gameboard);
        }
        public void  MakeAvailable()
        {
            Available = true;
        }

    }
}