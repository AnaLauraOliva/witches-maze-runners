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
        public void FallIntoTrap(Player currentPlayer, List<string> Narration, Gameboard gameboard)
        {
            switch (trapCode)
            {
                case 0:
                    Narration.Add($"{currentPlayer.Name} ha caído en una trampa de teletransportación");
                    currentPlayer.UpdatePlayerPosition(gameboard.PutInitialPosition(), 0);
                    break;
                case 1:
                    if (!currentPlayer.Witch.IsDefrost())
                    {
                        currentPlayer.TrapsEffects(3, (int)EffectsDurationCodes.Freeze);
                        Narration.Add($"{currentPlayer.Name} cayó en una trampa de congelamiento");
                    }
                    else { Narration.Add($"{currentPlayer.Name} usó su habilidad para evitar el congelamiento"); }
                    break;
                case 2:
                    currentPlayer.TrapsEffects(3, (int)EffectsDurationCodes.LossOfSpeed);
                    currentPlayer.Witch.LossOfSpeed();
                    Narration.Add($"{currentPlayer.Name} cayó en una trampa de pérdida de velocidad");
                    break;
                case 3:
                    currentPlayer.ChangeHP(-15);
                    Narration.Add($"{currentPlayer.Name} cayó en una trampa de daño, HP actual: {currentPlayer.HP}");
                    break;
                case 4:
                    currentPlayer.ChangeHP(10);
                    Narration.Add($"{currentPlayer.Name} recuperó puntos de vida, HP actual:{currentPlayer.HP}");
                    break;
                case 5:
                    currentPlayer.TrapsEffects(0,(int)EffectsDurationCodes.LossOfSpeed);
                    currentPlayer.Witch.retoreSpeedTreasure();
                    Narration.Add($"{currentPlayer.Name} recuperó todos tus puntos de velocidad");
                    break;
                default:
                    break;
            }
        }
    }
}
public enum EffectsDurationCodes
{
    Freeze = 0,
    LossOfSpeed = 1
}