using System.Diagnostics;

namespace Debil {
    public partial class DebilEngine {
        public Level map;
        public Player player;
        System.Timers.Timer PlayerUpdateTimer;
        System.Timers.Timer EntityMoveTimer;
        System.Timers.Timer EntityUpdateTimer;
        int EntityMoveInterval = 350;
        int EntityUpdateInterval = 5;
        bool DoDebugDraw;
        public DebilEngine(int Height, int Width, LevelGenerationStrategy genStrat) {
            DoDebugDraw = false;

            player = new Player(1,1, "🤡", this);
            map = new Level(Height, Width, this, genStrat);

            map.TeleportRandom(ref player);
            map.UpdateWaveMap(player.Position);

            PlayerUpdateTimer = new System.Timers.Timer(1000);
            PlayerUpdateTimer.Elapsed += player.Update;

            EntityMoveTimer = new System.Timers.Timer(EntityMoveInterval);
            EntityUpdateTimer = new System.Timers.Timer(EntityUpdateInterval);

            foreach(var mob in map.Mobs) {
                EntityMoveTimer.Elapsed += mob.Move;
                EntityUpdateTimer.Elapsed += mob.Update;
            }

            foreach(var pickup in map.Pickups) {
                EntityUpdateTimer.Elapsed += pickup.CheckCollision;
            }
        }
        public void Draw() {
            string stats = "\u001b[37;1mHealth:\u001b[0m " + string.Join("", Enumerable.Repeat("❤️", player.Health).ToArray())
             + "  \u001b[37;1mScore: " + player.Score + "\u001b[0m";

            Console.WriteLine(new string(' ', stats.Length) + '\r' + stats);

            string[,] frame = new string[map.Height, map.Width];

            for(int i = 0; i < map.Height; i++) {
                for(int j = 0; j < map.Width; j++) {
                    frame[i, j] = map[i, j].Texture;
                }
            }

            frame[player.Position.y, player.Position.x] = player.Texture;

            /* if(player.Position.y > 0 && map[player.Position.y - 1, player.Position.x].IsSolid) {
                frame[player.Position.y - 1, player.Position.x] = "🟪";
            }

            if(player.Position.y < map.Height - 1 && map[player.Position.y + 1, player.Position.x].IsSolid) {
                frame[player.Position.y + 1, player.Position.x] = "🟪";
            }

            if(player.Position.x > 0 && map[player.Position.y, player.Position.x - 1].IsSolid) {
                frame[player.Position.y, player.Position.x - 1] = "🟪";
            }

            if(player.Position.x < map.Width - 1 && map[player.Position.y, player.Position.x + 1].IsSolid) {
                frame[player.Position.y, player.Position.x + 1] = "🟪";
            } */

            foreach(var mob in map.Mobs) {
                frame[mob.Position.y, mob.Position.x] = mob.Texture;
            }

            foreach(var pickup in map.Pickups) {
                frame[pickup.Position.y, pickup.Position.x] = pickup.Texture;
            }

            

            for (int i = 0; i < map.Height; i++) {
                for (int j = 0; j < map.Width; j++) {
                    Console.Write(frame[i,j]);
                }

                Console.WriteLine();
            }

            System.Console.WriteLine(map.WaveMap[player.Position.y, player.Position.x].ToString().PadRight(3, ' '));
        }

        static List<string> WaveDistances = "🟩 🟨 🟧 🟥 🟦 🟪 ⬜".Split(' ').ToList();
        public void DebugDraw() {
            string stats = "\u001b[37;1mHealth:\u001b[0m " + string.Join("", Enumerable.Repeat("❤️", player.Health).ToArray())
             + "  \u001b[37;1mScore: " + player.Score + "\u001b[0m";

            Console.WriteLine(new string(' ', stats.Length) + '\r' + stats);

            string[,] frame = new string[map.Height, map.Width];

            for(int i = 0; i < map.Height; i++) {
                for(int j = 0; j < map.Width; j++) {
                    if(map[i, j].IsSolid) {
                        frame[i, j] = "⬛";
                    } else {
                        if(map.WaveMap[i, j] == 0) {
                            frame[i, j] = "  ";
                        } else if(map.WaveMap[i, j] <= 10) {
                            frame[i, j] = WaveDistances[0];
                        }  else if(map.WaveMap[i, j] <= 25){
                            frame[i, j] = WaveDistances[1];
                        } else if(map.WaveMap[i, j] <= 40) {
                            frame[i, j] = WaveDistances[2];
                        } else if(map.WaveMap[i, j] <= 55) {
                            frame[i, j] = WaveDistances[3];
                        } else if(map.WaveMap[i, j] <= 70) {
                            frame[i, j] = WaveDistances[4];
                        } else if(map.WaveMap[i, j] <= 85) {
                            frame[i, j] = WaveDistances[5];
                        } else {
                            frame[i, j] = WaveDistances[6];
                        }
                    }
                }
            }

            frame[player.Position.y, player.Position.x] = "🟪";

            foreach(var mob in map.Mobs) {
                

                switch(mob.GetType().Name) {
                    case "RandomDurachock":
                        frame[mob.Position.y, mob.Position.x] = "🟢";
                        break;
                    case "PathFindingDebchick":
                        frame[mob.Position.y, mob.Position.x] = "🔴";
                        break;
                    default:
                        frame[mob.Position.y, mob.Position.x] = "? ";
                        break;
                }
            }

            foreach(var pickup in map.Pickups) {
                frame[pickup.Position.y, pickup.Position.x] = "🍔";
            }

            for (int i = 0; i < map.Height; i++) {
                for (int j = 0; j < map.Width; j++) {
                    Console.Write(frame[i,j]);
                }

                Console.WriteLine();
            }

            System.Console.WriteLine(map.WaveMap[player.Position.y, player.Position.x].ToString().PadRight(3, ' '));
        }
        public void Run() {
            
            player.Position = map.GetRandomPosition();
            map.UpdateWaveMap(player.Position);
            player.Health = 1;
            player.Score = 0;

            PlayerUpdateTimer.Start();
            EntityUpdateTimer.Start();
            EntityMoveTimer.Start();
            
            while(player.Health > 0) {
                Console.CursorVisible = false;

                Console.SetCursorPosition(0, 0);
                
                player.Input();

                if(DoDebugDraw) {
                    this.DebugDraw();
                } else {
                    this.Draw();
                }

                Thread.Sleep(5);
            }

            PlayerUpdateTimer.Stop();
            EntityUpdateTimer.Stop();
            EntityMoveTimer.Stop();
        }
        public void Menu() {
            ConsoleKeyInfo key;
            Process proc;

            while(true) {
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

                if(key.Key == ConsoleKey.Escape) {
                    return;
                }

                Run();

                proc = Process.Start("clear");
                proc.Kill(true);
                
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