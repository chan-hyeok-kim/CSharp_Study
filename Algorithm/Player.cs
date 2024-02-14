using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    class Pos
    {
        public Pos(int x, int y) { Y = y; X = x; }
        public int Y;
        public int X;
    }


    class Player
    {
        public int PosY { get; private set; }
        public int PosX { get; private set; }
        Random _random = new Random();   
        Map _map;

        enum Dir
        {
            Up = 0,
            Left = 1,
            Down = 2, 
            Right = 3
        }

        int _dir=(int)Dir.Up;
        List<Pos> _points= new List<Pos>();

        public void Initialize(int posY, int posX, Map map)
        {
            PosY = posY;
            PosX = posX;
            _map = map;

            //현재 바라보고 있는 방향을 기준으로, 좌표 변화를 나타낸다
            int[] frontY = new int[] { -1, 0, 1, 0 }; // 위, 아래
            int[] frontX = new int[] { 0, -1, 0, 1 }; // 왼쪽, 오른쪽
            int[] rightY = new int[] { 0, -1, 0, 1 };
            int[] rightX = new int[] { 1, 0, -1, 0 };

            _points.Add(new Pos(PosY, PosX));

            while (PosY != map.DestY || PosX != map.DestX )
            {
                /*  우수법
                 *  1. 현재 바라보는 방향을 기준으로 오른쪽으로 갈 수 있는지 확인
                  */
                if (_map.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Map.TileType.Empty)
                {
                    // 오른쪽 방향으로 90도 회전
                    //3(right)에서 우측 방향 회전하면 2(down)됨 2->1, 1->0
                    // 그 로직을 우아하게 표현하면 다음과 같다
                    _dir = (_dir - 1 + 4) % 4; // 양수든 음수든 0~3값 고정하기 위함

                    // 앞으로 한 보 전진
                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];

                    _points.Add(new Pos(PosY, PosX));

                }
                // 2. 현재 바라보는 방향으로 전진할 수 있는지 확인
                else if (_map.Tile[PosY + frontY[_dir], PosX + frontX[_dir]] == Map.TileType.Empty)
                {
                    //앞으로 한 보 전진
                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];

                    _points.Add(new Pos(PosY, PosX));
                }
                else
                {
                    // 3. 왼쪽 방향으로 90도 회전. 회전만 실행
                    _dir = (_dir + 1 + 4) % 4;
                                       
                }                   


            }
        }

        const int MOVE_TICK = 10;
        int _sumTick = 0;
        int _lastIndex = 0;

        public void Update(int deltaTick)
        {
            if (_lastIndex >= _points.Count)
                return;

            _sumTick += deltaTick;
            if(_sumTick >= MOVE_TICK) { 
               _sumTick = 0;

                PosY = _points[_lastIndex].Y;
                PosX = _points[_lastIndex].X;
                _lastIndex++;

                // 여기에다가 0.1초마다 실행될 로직을 넣어준다
                #region 윗 내용의 상세
                /* int randValue = _random.Next(0, 5);
                 switch(randValue)
                 {
                     case 0:  // 상
                         if(PosY - 1 >= 0 && _map.Tile[PosY - 1, PosX]==Map.TileType.Empty)
                            PosY = PosY - 1 ;
                         break;
                     case 1:  // 하
                         if (PosY + 1 <= _map.Size && _map.Tile[PosY + 1, PosX] == Map.TileType.Empty)
                             PosY = PosY + 1;
                         break;
                     case 2:  // 좌
                         if (PosX - 1 >= 0 && _map.Tile[PosY, PosX - 1] == Map.TileType.Empty)
                             PosX = PosX - 1;
                         break;
                     case 3:  // 우
                         if (PosX + 1 < _map.Size && _map.Tile[PosY, PosX + 1] == Map.TileType.Empty)
                             PosX = PosX + 1;
                         break;

                 }*/
                #endregion
            }
        }
    }
}
