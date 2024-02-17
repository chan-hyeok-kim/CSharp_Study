namespace BFS
{
    class Graph
    {
        int[,] adj = new int[6, 6]
            {
                { 0, 1, 0, 1, 0, 0 },
                { 1, 0, 1, 1, 0, 0 },
                { 0, 1, 0, 0, 0, 0 },
                { 1, 1, 0, 0, 1, 0 },
                { 0, 0, 0, 1, 0, 1 },
                { 0, 0, 0, 0, 1, 0 },
             };

        List<int>[] adj2 = new List<int>[]
        {
                new List<int>(){ 1, 3 },
                new List<int>(){ 0, 2, 3 },
                new List<int>(){ 1 },
                new List<int>(){ 0, 1, 4 },
                new List<int>(){ 3, 5 },
                new List<int>(){ 4 },
        };

        public void BFS(int start)
        {
            bool[] found = new bool[6];
            int[] parent = new int[6];
            int[] distance = new int[6];

            Queue<int> q = new Queue<int>(); // 순서대로 예약하기
            q.Enqueue(start);
            found[start] = true;
            parent[start] = start;
            distance[start] = 0;

            while (q.Count > 0)
            {
                int now = q.Dequeue();
                Console.WriteLine(now);

                // now랑 연결된 next찾아야 하는데 
                for (int next = 0; next < 6; next++)
                {                       // 연결될 수 있는 길의 수
                    if (adj[now, next] == 0) //인접하지 않았으면 스킵
                        continue;
                    if (found[next])  //방문한 애라면 스킵
                        continue;
                    q.Enqueue(next);
                    found[next] = true;
                    parent[next] = now; //next의 부모는 now
                    distance[next] = distance[now] + 1; 
                                 //next의 거리는 now로부터 1증가
                }
            }
        }
    }

    class Program()
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            graph.BFS(0);
        }
    }
        
    
}
