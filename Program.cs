using System.Drawing;

namespace Debil {
    public partial class DebilEngine {
        public static void Main(string[] args) {
            /*
             * Пользователям Windows советую запускать этот шедевр игровой индустрии через Windows Terminal,
             * т.к. он поддерживает эмодзи
             */
            
            int Height = 51, Width = 115;

            //DebilEngine engine = new DebilEngine(Height, Width, new BreadmanStrategy(Height, Width));
            //DebilEngine engine = new DebilEngine(Height, Width, new MazeLike(Height, Width, 30));
            DebilEngine engine = new DebilEngine(Height, Width, new Box(Height, Width));
            //DebilEngine engine = new DebilEngine(Height, Width, new Randomized(Height, Width, 20));
            
            /* 
             * Двигаться на WASD/стрелочки
             * На T(лат.) телепортация в рандомную свободную точку,
             * если есть лишние 500 очков
             * На C просто запускается очистка экрана
             * На K количество хп уменьшается на 1
             * На L увеличивается (макс 3 хп)
             * Нажатие на X переключает на особый режим отрисовки,
             * показывающий глубину распространения волны: 
             *
             * 🟩 -- (0, 10]
             * 🟨 -- (10, 25]
             * 🟧 -- (25, 40]
             * 🟥 -- (40, 55]
             * 🟦 -- (55, 70]
             * 🟪 -- (70, 85]
             * ⬜ -- (85, +∞)
             *
             * F -- включает/отключает noclip
             * PgUp -- делает передвижение врагов чуть быстрее
             * PgDown -- возвращает обычную скорость передвижения врагов
             * 1 -- обычный режим ходьбы
             * 2 -- персонаж оставляет стены в тех местах, где он был
             * 3 -- персонаж убирает стены в тех местах, где он был
             */

            engine.Menu();
        } 
    }
}