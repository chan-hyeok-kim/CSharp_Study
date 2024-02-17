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
        public Pos(int y, int x) { Y = y; X = x; }
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
        // _points: 실제로 플레이어가 이동하는 지점들
        List<Pos> _points= new List<Pos>();

                               //플레이어 좌표, 생성된 맵
        public void Initialize(int posY, int posX, Map map)
        {
            PosY = posY;
            PosX = posX;
            _map = map;

            AStar();
        }

        struct PQNode : IComparable<PQNode>
        {
            public int F;
            public int G;
            public int Y;
            public int X;

            public int CompareTo(PQNode other)
            {
                if (F == other.F)
                    return 0;
                return F< other.F ? 1 : -1;
            }
        }

        /*  A*(에이스타) 알고리즘:
            시작점만 아는 DFS/BFS와 달리, 시작점과 끝점을 모두 알고 있다. 
            출구에 가까워질수록 경로에 가산점을 주는 방식
         */
        void AStar()
        {
            // U L D R UL DL DR UR  -> 대각선 추가 가능 
            int[] deltaY = new int[] { -1, 0, 1, 0};
            int[] deltaX = new int[] { 0, -1, 0, 1};
            int[] cost = new int[] { 10, 10, 10, 10}; // 상하좌우에 따라 점수 달라질 수 있으므로
            
            //점수 매기기
            // F = G + H
            // F = 최종 점수(작을수록 좋음, 경로에 따라 달라짐)
            // G = 시작점에서 해당 좌표까지 이동하는데 드는 비용 (작을수록 좋음, 경로에 따라 달라짐)
            // H = 목적지에서 얼마나 가까운지 (작을수록 좋음, 고정)

            // (y, x) 이미 방문했는지 여부 (방문 = closed 상태)
            bool[,] closed = new bool[_map.Size, _map.Size];

            // (y, x) 가는 길을 한 번이라도 발견했는지
            // 발견 X => MaxValue
            // 발견 O => F = G + H
            int[,] open = new int[_map.Size, _map.Size];
            for(int y=0; y< _map.Size; y++) 
                for(int x=0; x< _map.Size; x++)
                    open[y,x] = Int32.MaxValue;

            Pos[,] parent = new Pos[_map.Size, _map.Size];

            // 오픈리스트(open)에 있는 정보들 중에서, 가장 좋은 후보를 빠르게 뽑아오기 위한 도구
            PriorityQueue<PQNode> pq = new PriorityQueue<PQNode>();

            // 시작점 발견 (예약 진행)
            open[PosY, PosX] = 10 * (Math.Abs(_map.DestY - PosY) + Math.Abs(_map.DestX - PosX));
            pq.Push(new PQNode() { F = 10 * (Math.Abs(_map.DestY - PosY) +10 * Math.Abs(_map.DestX - PosX)), G = 0, Y=PosY, X=PosX });
            parent[PosY, PosX] = new Pos(PosY, PosX);

            while (pq.Count() > 0)
            {
                // 제일 좋은 후보를 찾는다
                PQNode node = pq.Pop();
                // 동일한 좌표를 여러 경로로 찾아서 더 빠른 경로로 인해서 이미 방문(closed)한 경우 스킵
                if (closed[node.Y, node.X])
                    continue;

                // 방문한다
                closed[node.Y, node.X] = true;
                // 방문한 곳이 목적지라면 종료
                if (node.Y == _map.DestY && node.X == _map.DestX)
                    break;

                // 상하좌우 등 이동할 수 있는 좌표인지 확인해서 예약(open)한다
                for(int i=0; i< deltaY.Length; i++)
                {
                    int nextY = node.Y + deltaY[i];
                    int nextX = node.X + deltaX[i];

                    // 유효 범위 벗어나면 스킵
                    if (nextX < 0 || nextX >= _map.Size || nextY < 0 || nextY >= _map.Size)
                        continue; 
                    if (_map.Tile[nextY, nextX] == Map.TileType.Wall) // 벽 스킵
                        continue;
                    if (closed[nextY, nextX]) // 방문한 곳 스킵
                        continue;

                    // 비용 계산
                    int g = node.G + cost[i];
                    int h = 10 * (Math.Abs(_map.DestY - PosY) + Math.Abs(_map.DestX - PosX));

                    // 다른 경로에서 더 빠른 길 이미 찾았으면 스킵
                    if (open[nextY, nextX] < g + h)
                        continue;

                    // 예약 진행
                    open[nextY, nextX] = g + h;
                    pq.Push(new PQNode() { F = g + h, G = g, Y = nextY, X = nextX});
                    
                    parent[nextY, nextX] = new Pos(node.Y, node.X);


                }
            }
            CalcPathFromParent(parent);

        }

        #region 우수법
        void RightHand()
        {
            //현재 바라보고 있는 방향을 기준으로, 좌표 변화를 나타낸다
            int[] frontY = new int[] { -1, 0, 1, 0 }; // 위, 아래
            int[] frontX = new int[] { 0, -1, 0, 1 }; // 왼쪽, 오른쪽
            int[] rightY = new int[] { 0, -1, 0, 1 };
            int[] rightX = new int[] { 1, 0, -1, 0 };

            _points.Add(new Pos(PosY, PosX));

            while (PosY != _map.DestY || PosX != _map.DestX)
            {
                /*  우수법
                   *1. 현재 바라보는 방향을 기준으로 오른쪽으로 갈 수 있는지 확인
                  */
                if (_map.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Map.TileType.Empty)
                {
                    // 오른쪽 방향으로 90도 회전
                    //3(right)에서 우측 방향 회전하면 2(down)됨 2->1, 1->0
                    //그 로직을 우아하게 표현하면 다음과 같다
                    _dir = (_dir - 1 + 4) % 4; // 양수든 음수든 0~3값 고정하기 위함

                    //앞으로 한 보 전진
                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];

                    _points.Add(new Pos(PosY, PosX));

                }
                // 2. 현재 바라보는 방향으로 전진할 수 있는지 확인
                else if (_map.Tile[PosY + frontY[_dir], PosX + frontX[_dir]] == Map.TileType.Empty)
                {
                    // 앞으로 한 보 전진
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
        #endregion

        #region BFS
        void BFS()
        {
            int[] deltaY = new int[] { -1, 0, 1, 0 };
            int[] deltaX = new int[] { 0, -1, 0, 1 };

            bool[,] found = new bool[_map.Size, _map.Size];
            Pos[,] parent = new Pos[_map.Size, _map.Size];    

            // 큐 안에 좌표 넣었다 뺐다 하며 다음 좌표(next) 찾아갈 예정
            Queue<Pos> q = new Queue<Pos>();    
            q.Enqueue(new Pos(PosY, PosX));
            found[PosY, PosX] = true;
            parent[PosY, PosX] = new Pos(PosY, PosX); 
            
            while (q.Count > 0)
            {
                Pos pos = q.Dequeue();
                int nowY = pos.Y;
                int nowX = pos.X;   

                // 상하좌우 empty 확인

                for(int i = 0; i < 4; i++)
                {
                    int nextY = nowY + deltaY[i];
                    int nextX = nowX + deltaX[i];

                    if (nextX < 0 || nextX >= _map.Size || nextY < 0 || nextY >= _map.Size)
                        continue;

                    //다음 좌표가 벽이면 스킵
                    if (_map.Tile[nextY, nextX] == Map.TileType.Wall)
                        continue;
                    if (found[nextY, nextX])
                        continue;

                    q.Enqueue(new Pos(nextY, nextX));
                    found[nextY, nextX] = true;
                    //지나온 길. 좌표들을 parent에 기록
                    parent[nextY, nextX] = new Pos(nowY, nowX);
                }
            }

            CalcPathFromParent(parent);

        }
        #endregion

        void CalcPathFromParent(Pos[,] parent)
        {
            int y = _map.DestY; //23
            int x = _map.DestX; //23

            while (parent[y, x].Y != y || parent[y, x].X != x)
            {
                _points.Add(new Pos(y, x));
                Pos pos = parent[y, x];

                y = pos.Y;
                x = pos.X;
            }

            _points.Add(new Pos(y, x));
            _points.Reverse();
        }

        

        #region PriorityQueue
        class PriorityQueue<T> where T : IComparable<T>
        {
            List<T> _heap = new List<T>();

            // O(logN)
            public void Push(T data)
            {
                // 힙의 맨 끝에 새로운 데이터 삽입
                _heap.Add(data);

                // 새로 들어온 데이터의 인덱스(마지막에 삽입된 거라서 총 데이터-1)
                int now = _heap.Count - 1;

                // 도장 깨기
                while (now > 0)
                {
                    //next: 지금 싸울 상대, 부모
                    int next = (now - 1) / 2;
                    if (_heap[now].CompareTo(_heap[next]) < 0)
                        break; // 패배

                    // 승리: 두 값을 교체
                    T temp = _heap[now];
                    _heap[now] = _heap[next];
                    _heap[next] = temp;

                    // 도장깨기할 위치를 이동한다
                    now = next;
                }
            }

            // O(logN)
            public T Pop()
            {
                // 반환할 데이터 저장
                T ret = _heap[0];

                // 마지막 데이터를 루트로 이동
                int lastIndex = _heap.Count - 1;
                _heap[0] = _heap[lastIndex];
                _heap.RemoveAt(lastIndex);
                lastIndex--;

                // 역으로 도장깨기
                int now = 0;
                while (true)
                {
                    int left = 2 * now + 1;
                    int right = 2 * now + 2;

                    int next = now;
                    // 왼쪽 값이 현재 값보다 크면, 왼쪽으로 이동
                    if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) < 0)
                        next = left;

                    // 오른쪽 값이 현재 값보다 크면, 왼쪽으로 이동
                    // if문으로만 2번 싸움 붙였기 때문에 셋 중에 최고 승자를 가릴 수 있다
                    if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) < 0)
                        next = right;

                    // 왼쪽/ 오른쪽보다 모두 현재값보다 작으면 종료
                    if (next == now)
                        break;

                    // 두 값을 교체
                    T temp = _heap[now];
                    _heap[now] = _heap[next];
                    _heap[next] = temp;

                    //도장깨기할 위치 이동
                    now = next;
                }




                return ret;
            }

            public int Count()
            {
                return _heap.Count;
            }
        }

        #endregion


        const int MOVE_TICK = 10;
        int _sumTick = 0;
        int _lastIndex = 0;

        public void Update(int deltaTick)
        {
            if (_lastIndex >= _points.Count)
            {
                // 도착지점에 도착하면 맵 다시 생성
                _lastIndex = 0;
                _points.Clear();
                _map.Initialize(_map.Size, this);
                Initialize(1, 1, _map);
            }

            _sumTick += deltaTick;
            if(_sumTick >= MOVE_TICK) { 
               _sumTick = 0;

                PosY = _points[_lastIndex].Y;
                PosX = _points[_lastIndex].X;
                _lastIndex++;

                // 여기에다가 0.1초마다 실행될 로직을 넣어준다
                #region 윗 내용의 상세
               /* int randValue = _random.Next(0, 5);
                switch (randValue)
                {
                    case 0:  // 상
                        if (PosY - 1 >= 0 && _map.Tile[PosY - 1, PosX] == Map.TileType.Empty)
                            PosY = PosY - 1;
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
