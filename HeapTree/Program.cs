using System.ComponentModel.DataAnnotations;

namespace HeapTree
{
    // 힙트리
    // 1 법칙: 부모가 자식보다 크기만 하면 된다
    // - 마지막 레벨을 제외한 모든 레벨에 노드가 꽉 차 있다
    // - 마지막 레벨에 노드가 있을 때는 항상 왼쪽부터 채워야 함

    // 2 법칙: 노드 개수를 알면, 트리 구조는 무조건 확정할 수 있다
    // (1) i번 노드의 왼쪽 자식: [(2*i)+1]번
    // (2) i번 노드의 오른쪽 자식: [(2*i)+2]번
    // (3) i번 노드의 부모: [(i-1)/2]번
   
    // 3. 트리의 높이는 log(N) <-밑이 2인 로그, N은 총 데이터 개수

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
            while(now > 0) 
            {
                //next: 지금 싸울 상대, 부모
                int next = (now - 1) / 2;
                if (_heap[now].CompareTo(_heap[next])<0)
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
            while(true)
            {
                int left = 2 * now + 1;
                int right = 2 * now + 2;

                int next = now;
                // 왼쪽 값이 현재 값보다 크면, 왼쪽으로 이동
                if (left <= lastIndex && _heap[next].CompareTo(_heap[left])<0)
                    next = left;

                // 오른쪽 값이 현재 값보다 크면, 왼쪽으로 이동
                // if문으로만 2번 싸움 붙였기 때문에 셋 중에 최고 승자를 가릴 수 있다
                if (right <= lastIndex && _heap[next].CompareTo(_heap[right])<0)
                    next = right;

                // 왼쪽/ 오른쪽보다 모두 현재값보다 작으면 종료
                if(next == now)
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

    class Knight: IComparable<Knight> 
    {
        public int Id { get; set; }

        public int CompareTo(Knight? other)
        {
            if(Id == other.Id)
                return 0;
            return Id > other.Id ? 1 : -1;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            PriorityQueue<Knight> q=new PriorityQueue<Knight>();
            q.Push(new Knight() { Id = 20 });
            q.Push(new Knight() { Id = 10 });
            q.Push(new Knight() { Id = 30 });
            q.Push(new Knight() { Id = 05 });
            q.Push(new Knight() { Id = 40 });

            while(q.Count() > 0)
            {
                Console.WriteLine(q.Pop().Id);
            }
        }
    }
}
