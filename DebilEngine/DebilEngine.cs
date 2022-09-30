using System.Diagnostics;

namespace Debil
{
    public partial class DebilEngine
    {
        public Level Map;
        public Player Debchick;
        IRenderer CurrentRenderer;
        List<IRenderer> Renderers;
        int RendererIndex;
        System.Timers.Timer PlayerUpdateTimer;
        System.Timers.Timer EntityMoveTimer;
        System.Timers.Timer EntityUpdateTimer;
        int EntityMoveInterval = 350;
        int EntityUpdateInterval = 5;
        public DebilEngine(int Height, int Width, LevelGenerationStrategy genStrat)
        {
            Debchick = new Player(1, 1, "🤡", this);
            Map = new Level(Height, Width, this, genStrat);
            Map.TeleportRandom(ref Debchick);
            Map.UpdateWaveMap(Debchick.Position);

            CurrentRenderer = new NormalRenderer();
            Renderers = new List<IRenderer>();
            RendererIndex = 0;

            PlayerUpdateTimer = new System.Timers.Timer(1000);
            PlayerUpdateTimer.Elapsed += Debchick.Update;
            EntityMoveTimer = new System.Timers.Timer(EntityMoveInterval);
            EntityUpdateTimer = new System.Timers.Timer(EntityUpdateInterval);

            foreach (var mob in Map.Mobs)
            {
                EntityMoveTimer.Elapsed += mob.Move;
                EntityUpdateTimer.Elapsed += mob.Update;
            }

            foreach (var pickup in Map.Pickups)
            {
                EntityUpdateTimer.Elapsed += pickup.CheckCollision;
            }
        }
        public DebilEngine(int Height, int Width, LevelGenerationStrategy genStrat, List<IRenderer> renderers)
        {
            Debchick = new Player(1, 1, "🤡", this);
            Map = new Level(Height, Width, this, genStrat);
            Map.TeleportRandom(ref Debchick);
            Map.UpdateWaveMap(Debchick.Position);

            CurrentRenderer = renderers[0];
            Renderers = renderers;
            RendererIndex = 0;

            PlayerUpdateTimer = new System.Timers.Timer(1000);
            PlayerUpdateTimer.Elapsed += Debchick.Update;
            EntityMoveTimer = new System.Timers.Timer(EntityMoveInterval);
            EntityUpdateTimer = new System.Timers.Timer(EntityUpdateInterval);

            foreach (var mob in Map.Mobs)
            {
                EntityMoveTimer.Elapsed += mob.Move;
                EntityUpdateTimer.Elapsed += mob.Update;
            }

            foreach (var pickup in Map.Pickups)
            {
                EntityUpdateTimer.Elapsed += pickup.CheckCollision;
            }
        }
        public void Run()
        {

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
                System.Console.WriteLine($"{Map.Engine.RendererIndex + 1} / {Map.Engine.Renderers.Count}".PadRight(10, ' '));

                Thread.Sleep(5);
            }

            PlayerUpdateTimer.Stop();
            EntityUpdateTimer.Stop();
            EntityMoveTimer.Stop();
        }
        public void Menu()
        {
            ConsoleKeyInfo key;

            while (true)
            {
                System.Console.WriteLine("\x1B[1;1H\x1B[2J");
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

                System.Console.WriteLine("\x1B[1;1H\x1B[2J");
                Console.SetCursorPosition(0, 0);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }

                Run();

                System.Console.WriteLine("\x1B[1;1H\x1B[2J");
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

                System.Console.WriteLine("\x1B[1;1H\x1B[2J");
                Console.SetCursorPosition(0, 0);
            }
        }
    }
}