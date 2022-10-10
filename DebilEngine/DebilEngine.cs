using System.Diagnostics;

namespace Debil
{
    public partial class DebilEngine
    {
        public static string WallTexture = "⬛";
        public Level Map;
        public Player Debchick;
        public List<IRenderer> Renderers;
        IRenderer CurrentRenderer;
        int RendererIndex;
        System.Timers.Timer PlayerUpdateTimer;
        System.Timers.Timer EntityMoveTimer;
        System.Timers.Timer EntityUpdateTimer;
        const int PlayerUpdateInterval = 1000;
        const int EntityMoveInterval = 350;
        const int EntityUpdateInterval = 5;
        public DebilEngine(int Height, int Width, Level.ILevelGenerator levelGen, Level.IMobPlacer mobPlacer, Level.IPickupPlacer pickupPlacer)
        {
            Map = new Level(Height, Width, levelGen, mobPlacer, pickupPlacer, this);
            Debchick = new Player(0, 0, "🤡", this);
            Renderers = new List<IRenderer>();
            Renderers.Add(new AreaRender(51, 51));
            CurrentRenderer = Renderers[0];
            RendererIndex = 0;

            PlayerUpdateTimer = new System.Timers.Timer(PlayerUpdateInterval);
            EntityMoveTimer = new System.Timers.Timer(EntityMoveInterval);
            EntityUpdateTimer = new System.Timers.Timer(EntityUpdateInterval);
        }
        public void Initialize()
        {
            Map.Generate();

            PlayerUpdateTimer.Close();
            EntityUpdateTimer.Close();
            EntityMoveTimer.Close();

            PlayerUpdateTimer.Elapsed += Debchick.Update;

            foreach(var mob in Map.Mobs)
            {
                EntityUpdateTimer.Elapsed += mob.Update;
                EntityMoveTimer.Elapsed += mob.Move;
            }

            Console.WriteLine("Mobs");
            
            foreach(var pickup in Map.Pickups)
            {
                EntityUpdateTimer.Elapsed += pickup.CheckCollision;
            }

            Console.WriteLine("Pickups");

            Console.WriteLine("Finally");
        }
        public void Run()
        {
            //Map.TeleportRandom(ref Debchick);
            Debchick.Position = Map.GetRandomPosition();
            Map.UpdateWaveMap(Debchick.Position);
            Debchick.Health = 1;
            Debchick.Score = 0;

            PlayerUpdateTimer.Start();
            EntityUpdateTimer.Start();
            EntityMoveTimer.Start();

            while (Debchick.Health > 0)
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, 0);

                Debchick.Input();
                CurrentRenderer.Draw(Map);

                Thread.Sleep(5);
            }

            PlayerUpdateTimer.Stop();
            EntityUpdateTimer.Stop();
            EntityMoveTimer.Stop();
        }
        public void Menu()
        {
            ConsoleKeyInfo key;
            Process proc;

            Initialize();

            while (true)
            {
                proc = Process.Start("clear");
                proc.Kill(true);
                Console.SetCursorPosition(0, 0);

                Console.WriteLine("\u001b[38;5;202m" +
                                  "▓█████▄ ▓█████  ▄▄▄▄    ██▓ ██▓         ▄████  ▄▄▄       ███▄ ▄███▓▓█████ \n" +
                                  "▒██▀ ██▌▓█   ▀ ▓█████▄ ▓██▒▓██▒        ██▒ ▀█▒▒████▄    ▓██▒▀█▀ ██▒▓█   ▀ \n" +
                                  "░██   █▌▒███   ▒██▒ ▄██▒██▒▒██░       ▒██░▄▄▄░▒██  ▀█▄  ▓██    ▓██░▒███   \n" +
                                  "░▓█▄   ▌▒▓█  ▄ ▒██░█▀  ░██░▒██░       ░▓█  ██▓░██▄▄▄▄██ ▒██    ▒██ ▒▓█  ▄ \n" +
                                  "░▒████▓ ░▒████▒░▓█  ▀█▓░██░░██████▒   ░▒▓███▀▒ ▓█   ▓██▒▒██▒   ░██▒░▒████▒\n" +
                                  " ▒▒▓  ▒ ░░ ▒░ ░░▒▓███▀▒░▓  ░ ▒░▓  ░    ░▒   ▒  ▒▒   ▓▒█░░ ▒░   ░  ░░░ ▒░ ░\n" +
                                  " ░ ▒  ▒  ░ ░  ░▒░▒   ░  ▒ ░░ ░ ▒  ░     ░   ░   ▒   ▒▒ ░░  ░      ░ ░ ░  ░\n" +
                                  " ░ ░  ░    ░    ░    ░  ▒ ░  ░ ░      ░ ░   ░   ░   ▒   ░      ░      ░   \n" +
                                  "   ░       ░  ░ ░       ░      ░  ░         ░       ░  ░       ░      ░  ░\n" +
                                  " ░                   ░                                                    \n" +
                                  "                          Press any key to start\n" +
                                  "                             Press Esc to exit\u001b[0m");

                key = Console.ReadKey(true);

                proc = Process.Start("clear");
                proc.Kill(true);
                Console.SetCursorPosition(0, 0);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }

                Run();

                proc = Process.Start("clear");
                proc.Kill(true);
                Console.SetCursorPosition(0, 0);

                Console.WriteLine("\u001b[38;5;202m" +
                                  "▓██   ██▓ ▒█████   █    ██    ▓█████▄  ██▓▓█████ ▓█████▄ \n" +
                                  "▒██  ██▒▒██▒  ██▒ ██  ▓██▒   ▒██▀ ██▌▓██▒▓█   ▀ ▒██▀ ██▌\n" +
                                  " ▒██ ██░▒██░  ██▒▓██  ▒██░   ░██   █▌▒██▒▒███   ░██   █▌\n" +
                                  " ░ ▐██▓░▒██   ██░▓▓█  ░██░   ░▓█▄   ▌░██░▒▓█  ▄ ░▓█▄   ▌\n" +
                                  " ░ ██▒▓░░ ████▓▒░▒▒█████▓    ░▒████▓ ░██░░▒████▒░▒████▓ \n" +
                                  "  ██▒▒▒ ░ ▒░▒░▒░ ░▒▓▒ ▒ ▒     ▒▒▓  ▒ ░▓  ░░ ▒░ ░ ▒▒▓  ▒ \n" +
                                  "▓██ ░▒░   ░ ▒ ▒░ ░░▒░ ░ ░     ░ ▒  ▒  ▒ ░ ░ ░  ░ ░ ▒  ▒ \n" +
                                  "▒ ▒ ░░  ░ ░ ░ ▒   ░░░ ░ ░     ░ ░  ░  ▒ ░   ░    ░ ░  ░ \n" +
                                  "░ ░         ░ ░     ░           ░     ░     ░  ░   ░    \n" +
                                  "░ ░                           ░                  ░      \u001b[0m");

                Console.ReadKey(true);

                proc = Process.Start("clear");
                proc.Kill(true);
            }
        }
    }
}