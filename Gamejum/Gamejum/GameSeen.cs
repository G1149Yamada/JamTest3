using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamejum
{
    class GameSeen
    {
        Player player;
        MapChip1 map1;
        GamePlay mapDraw;

        public GameSeen()
        {
            player = new Player();
            map1 = new MapChip1();
            mapDraw = new GamePlay();
        }

        public void GameSeen1()
        {
            player = new Player();
            map1 = new MapChip1();
            mapDraw = new GamePlay();
        }
    }
}
