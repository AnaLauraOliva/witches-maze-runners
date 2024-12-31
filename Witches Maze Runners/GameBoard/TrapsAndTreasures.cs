namespace Game.Model.MazeNamespace
{
    internal class TrapsAndTreasures
    {
        private int trapCode;
        internal TrapsAndTreasures() { }
        internal TrapsAndTreasures(int Code)
        {
            trapCode = Code;
        }
        public int FallIntoTrap()
        {
            return trapCode;
        }
    }
}