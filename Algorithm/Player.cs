using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    class Player
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        Random _random = new Random();   
        Map _map;
        public void Initialize(int posY, int posX, int destY, int dextX, Map map)
        {
            PosY = posY;
            PosX = posX;
            _map = map;
        }

        const int MOVE_TICK = 100;
        int _sumTick = 0;
        
        public void Update(int deltaTick)
        {
            _sumTick += deltaTick;
            if(_sumTick >= MOVE_TICK) { 
               _sumTick = 0;

                // 여기에다가 0.1초마다 실행될 로직을 넣어준다
                int randValue = _random.Next(0, 5);
                switch(randValue)
                {
                    case 0:  // 상
                        _map.Tile
                    case 1:  // 하
                    case 2:  // 좌
                    case 3:  // 우

                }
            }
        }
    }
}
