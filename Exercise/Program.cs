namespace Exercise
{
    /*  DFS (Depth First Search 깊이 우선 탐색)
     *  BFS (Breadth First Search 너비 우선 탐색)
     */
    class Graph
    {
        static void Main(String[] args)
        {
            Graph graph = new Graph();
            graph.DFS(3);
        }

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

        bool[] visited = new bool[6];
        // 1) 우선 now부터 방문하고,
        // 2) now와 연결된 정점들을 하나씩 확인해서, [아직 미발견(미방문) 상태라면 ] 방문한다
        public void DFS(int now)
        {
            Console.WriteLine(now);
            visited[now] = true;
            
        /*  C#은 Length로 구하면 2가지 배열 곱한 길이 나옴
            GetLength()가 맞는듯. 단, 0넣어야함
            0 넣는 이유: 첫번째 차원의 길이
            1: 두번째 차원의 길이
            다차원에서는 몇번째 차원의 길이를 가져올지 
            인덱스로 정해줘야 한다
        */

            //배열용 
            for(int next = 0; next< adj.GetLength(0); next++)
            {
                // now에서 next연결된 곳이 아니면 스킵
                if (adj[now, next] == 0)
                    continue;

                // next가 이미 방문한 애라면 스킵
                if (visited[next])
                    continue; 
                
                DFS(next);
            }
        }

        public void DFS2(int now)
        {
            Console.WriteLine(now);
            visited[now]= true; // 1) 우선 now 방문

            foreach(int next in adj2[now])
            {

            }

        }
    }

    internal class Program
    {
        #region 스택/큐
        // 스택: LIFO(후입선출 Last in First out)
        // 큐 : FIFO(선입선출 First in First out)

        public void study()
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(101);
            stack.Push(102);
            stack.Push(103);
            stack.Push(104);
            stack.Push(105);

            int data = stack.Pop(); 
            int data2 = stack.Peek();

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(101);
            queue.Enqueue(102);
            queue.Enqueue(103);
            queue.Enqueue(104);
            queue.Enqueue(105);

            int data3 = queue.Dequeue();
            int data4 = queue.Peek();

            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(101);
            list.AddLast(102);
            list.AddLast(103);

            int value1 = list.First.Value;
            list.RemoveFirst();

            int value2 = list.Last.Value;
            list.RemoveLast();


        }
        #endregion
    }


}
