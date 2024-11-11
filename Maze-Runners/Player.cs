using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using CharactersAndSkills;
namespace Maze_Runners
{
    class Player
    {
        public string name;
        public string piece;
        public Skills attack;
        public Skills defense;
        public bool defenseIsActive;
        public int lowSpeedTimeLeft;
        public int frostTimeLeft;
        public int hp = 100;
        public int temp;
        public int speed;
        public int normalSpeed;
        public int code;
        public int attackCoolingTime;
        public int defenseCoolingTime;
        public int positionx;
        public int positiony;
        public Player(int Code)
        {
            string[] Names = { "Bruja de Agua", "Bruja de Tierra", "Bruja de Fuego"
            ,"Bruja de Aire","Bruja de Oscuridad","Bruja de Luz"};
            int[] Speeds = { 5, 5, 4, 4, 6, 6 };
            int[] Codes = { 2, 3, 4, 5, 6, 7 };
            string[] Pieces = { "💙", "🤎", "🧡", "🩶", "🖤", "🤍" };

            name = Names[Code - 1];
            normalSpeed = Speeds[Code - 1];
            speed = Speeds[Code - 1];
            code = Codes[Code - 1];
            piece = Pieces[Code - 1];
            attack = new Skills(Code - 1, "attack");
            defense = new Skills(Code - 1, "defense");
        }
        public void ActivateAttack(Player[] enemies, Maze currentMaze)
        {
            attack.AttackSkill(enemies, this);
            PlayerDeath(enemies, currentMaze);
        }
        public void ActivateDefense()
        {
            defenseIsActive = defense.DefenseSkill(this);
        }
        public void InitializePlayer(Maze currentMaze)
        {
            currentMaze.maze[0, currentMaze.start] = code;
            positionx = 0;
            positiony = currentMaze.start;
        }
        public bool Turn(Player[] players, Maze currentMaze)
        {
            if (frostTimeLeft == 0)
            {
                for (int i = 0; i < speed; i++)
                {

                    temp = SamePosition(players, currentMaze);
                    ConsoleKey key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.A)
                    { ActivateAttack(players, currentMaze); }
                    else if (key == ConsoleKey.D)
                    { ActivateDefense(); }
                    i = VerifyControl(key, i, currentMaze, players);
                    Console.Clear();
                    currentMaze.PrintMaze(players);
                    PlayerInfo(speed - i - 1);
                    if (positionx == currentMaze.Width - 1) return true;
                }
            }
            ReduceCoolingTime();
            return false;
        }
        private void ReduceCoolingTime()
        {
            bool[] conditions = { frostTimeLeft != 0,
             attackCoolingTime != 0,defenseCoolingTime != 0,
             lowSpeedTimeLeft!=0, lowSpeedTimeLeft==0 && defenseCoolingTime == 0};
            if (conditions[0]) frostTimeLeft--;
            if (conditions[1]) attackCoolingTime--;
            if (conditions[2]) defenseCoolingTime--;
            if (conditions[3]) lowSpeedTimeLeft--;
            if (conditions[4]) speed=normalSpeed;
        }
        public void PlayerInfo(int i)
        {
            System.Console.WriteLine($"Personaje: {name}\nHP: {hp}\n" +
            $"Tiempo de enfriamiento de ataque: {attackCoolingTime}" +
            $" Tiempo de enfriamiento de defensa: {defenseCoolingTime}" +
            $" Movimientos restantes:{i}" +
            $"\nPresiona las flechas para moverte, A para atacar y D para activar la defensa");
        }
        public int SamePosition(Player[] players, Maze currentMaze)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].positionx == positionx && players[i].positiony == positiony && players[i].code != code)
                {
                    currentMaze.maze[players[i].positionx, players[i].positiony] = players[i].code;
                    return players[i].code;
                }
            }
            return 0;
        }
        public int VerifyControl(ConsoleKey key, int i, Maze currentMaze, Player[] players)
        {
            bool[] Conditions = {key == ConsoleKey.RightArrow, key == ConsoleKey.LeftArrow && positionx > 0
            ,key == ConsoleKey.UpArrow, key == ConsoleKey.DownArrow};
            int[] x = { 1, -1, 0, 0 };
            int[] y = { 0, 0, -1, 1 };
            int counter = 0;
            while (true)
            {
                if (counter >= Conditions.Length || Conditions[counter] == true)
                {
                    break;
                }
                counter++;
            }
            if (counter >= Conditions.Length) return i - 1;
            return currentMaze.Directions(Conditions[counter], x[counter], y[counter], this, players) == true ? i : i - 1;
        }
        private void PlayerDeath(Player[] players, Maze currentMaze)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].hp <= 0)
                {
                    players[i].hp = 100;
                    currentMaze.maze[players[i].positionx, players[i].positiony] = 0;
                    players[i].InitializePlayer(currentMaze);
                    players[i].frostTimeLeft = 3;
                    players[i].defenseIsActive = false;
                    players[i].defense.immunity = false;
                    players[i].defense.defrost = false;
                }
            }
        }
    }
}