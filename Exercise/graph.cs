using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise
{
    /* 그래프는 vertex(정점)과 정점사이의 거리(간선-edge)으로 이루어짐
     * 그래프 구현에는 여러가지 방법(인스턴스, 리스트, 배열)이 있다
     *  보통 2~3번 
     *  1. 인스턴스 생성
    */
   
    class Vertex
    {
        public List<Vertex> edges= new List<Vertex>();

        void CreateGraph()
        {
            List<Vertex> v = new List<Vertex>(6)
            {
                new Vertex(),
                new Vertex(), 
                new Vertex(),
                new Vertex(),
                new Vertex(),
                new Vertex(),
            };
            v[0].edges.Add(v[1]);
            v[0].edges.Add(v[3]);
            v[1].edges.Add(v[0]);
            v[1].edges.Add(v[2]);
            v[3].edges.Add(v[4]);
            v[5].edges.Add(v[4]);
        }
    }

    // 2-2. 리스트로 구현(간선 거리 정보까지 표시)
    // : 메모리 소모 적지만, 접근이 느리다
    // 잘 이해 안감-> 왜냐하면 얘도 결국에 생성하는거잖아 new 인스턴스로
    class Edge
    {
        public Edge(int v, int w) { vertex = v; weight = w; }
        public int vertex;
        public int weight;

        List<Edge>[] adjacent = new List<Edge>[6]
        {
            new List<Edge>(){ new Edge(1, 15), new Edge(3, 35) },
            new List<Edge>(){ new Edge(0, 15), new Edge(2, 5), new Edge(3, 10) },
            new List<Edge>(){ },
            new List<Edge>(){ new Edge(4, 5) },
            new List<Edge>(){ },
            new List<Edge>(){ new Edge(4, 5) }
        };
    }

   
    class GraphEx
    {
        // 2-1. 리스트 구현
        List<int>[] adjacent = new List<int>[6]
        {
            new List<int>{ 1, 3,},
            new List<int>{ 0, 2, 3,},
            new List<int>{ },
            new List<int>{ 4 },
            new List<int>{ },
            new List<int>{ 4 }
        };

        // 3. 행렬(2차원 배열)
        //메모리 소모 심하지만, 빠른 접근 가능
        int[,] adjacent2 = new int[6, 6]
        {
            { 0, 1, 0, 1, 0, 0 },
            { 1, 0, 1, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 0 },
        };

        // 아래처럼 간선끼리의 거리 표현도 가능
        // -1: 연결이 끊긴 것을 표현
        int[,] adjacent3 = new int[6, 6]
        {
            {-1, 15, -1, 35, -1, -1 },
            {15, -1, 5, 10, -1, -1 },
            {-1, -1, -1, -1, -1, -1 },
            {-1, -1, -1, -1, 5, -1 },
            {-1, -1, -1, -1, -1, -1 },
            {-1, -1, -1, -1, 5, -1 },
        };


        
    }
}
