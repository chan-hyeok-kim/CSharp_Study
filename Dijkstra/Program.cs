namespace Dijkstra
{
   /*  BFS의 단점
     1. 최단 경로가 아니어도 모든 길을 전부 탐색해보게 됨 
     2. 특정한 경우에만 사용함: 각 경로끼리 이동하는 시간/거리가 같을 때 

     이러한 이유로 다익스트라 사용*/

    class Graph

    {
        int[,] adj = new int[6, 6]
        {
            { -1, 15, -1, 35, -1, -1 },
            { 15, -1, 05, 10, -1, -1 },
            { -1, 05, -1, -1, -1, -1 },
            { 35, 10, -1, -1, 05, -1 },
            { -1, -1, -1, 05, -1, 05 },
            { -1, -1, -1, -1, 05, -1 },
        };

        public void Dijkstra(int start)
        {
            bool[] visited = new bool[6];
            int[] distance = new int[6];
            int[] parent = new int[6];

            Array.Fill(distance, Int32.MaxValue);

            distance[start] = 0;
            parent[start] = start;

            while (true)
            {
                // 제일 좋은 후보를 찾는다 ( 가장 가까이에 있는 )

                // 가장 유력한 후보의 거리와 번호를 저장
                int closest = Int32.MaxValue;
                int now = -1;

                for (int i = 0; i < 6; i++)
                {
                    if (visited[i])
                        continue;
                    // 아직 발견된 적 없거나, 기존 후보보다 멀리 있으면 스킵
                    if (distance[i] == Int32.MaxValue || distance[i] >= closest)
                        continue;

                    // 여기에 도달하면 여태껏 발견한 가장 후보라는 의미. 정보 갱신
                    closest = distance[i];
                    now = i;
                }
                // 다음 후보가 하나도 없다 -> 종료
                if (now == -1)
                    break;

                // 제일 좋은 후보 찾았으니까 방문
                visited[now] = true;
                
                // 방문한 정점과 인접한 정점들을 조사해서 
                // 상황에 따라 발견한 최단거리를 갱신한다
                for (int next = 0; next < 6; next++)
                {
                    // 연결되지 않은 정점 스킵
                    if (adj[now, next] == -1)
                        continue;

                    if (visited[next])
                        continue;

                    // 새로 조사된 정점의 최단거리를 계산
                    int nextDist = distance[now] + adj[now, next];

                    // 만약에 새로 조사된 최단거리가 기존에 발견한 최단거리보다
                    // 짧으면 갱신
                    if (nextDist < distance[next])
                    {
                        distance[next] = nextDist;
                        parent[next] = now;
                    }

                }
            }
        }

    }

    class Program
    {

        static void Main()
        {
            Graph graph = new Graph();
            graph.Dijkstra(0);
        }

    }
}
