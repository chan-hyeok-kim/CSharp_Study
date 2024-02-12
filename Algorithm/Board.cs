using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    class MyList<T>
    {
        const int DEFAULT_SIZE = 1;
        T[] _data=new T[DEFAULT_SIZE];

        public int Count; //실제로 사용중인 데이터 개수
        public int Capacity { get { return _data.Length;  } } //예약된 데이터 개수

        /* O(1) 예외 케이스: 이사 비용은 무시
         * add: 맨 마지막에 1개 추가라서 O(1), 대신에 insert는 O(N)
         */
        public void Add(T item)
        {
            //배열 추가
            //1. 공간이 충분히 남아있는지 확인
            if(Count >= Capacity)
            {
                // 1-1.공간 확보
                T[] newArray=new T[Count*2];
                for(int i = 0; i < Count; i++)
                    newArray[i] = _data[i];
                _data = newArray;
            }
            //2. 확보되면, 공간에다가 데이터를 넣어준다. 
            _data[Count] = item;  
            Count++;
        }

        // O(1)
        public T this[int index] 
        { get { return _data[index]; }
          set { _data[index] = value; }
        }

        // O(N)
        public void RemoveAt(int index)
        {
            for(int i = index; i < Count-1; i++)
                _data[i] = _data[i+1];
            // 101 102 103 104 105
            _data[Count - 1] = default(T);
            // 뭐가 오더라도 기본값 할당

            Count--;
        }

    }
    class Board
    {
        public int[] _data=new int[25]; //배열
        public List<int> _data2=new List<int>();  //동적 배열
        public MyLinkedList<int> _data3=new MyLinkedList<int>();  //연결 리스트
        public void Initialize()
        {
            for(int i=101; i<106; i++)
            {
                _data2.Add(i);
            }
            int temp = _data2[2];

            _data2.RemoveAt(2);



        }

        public void InitializeLinked()
        {
            _data3.AddLast(101);
            _data3.AddLast(102);
            MyLinkedListNode<int> node = _data3.AddLast(103);
            _data3.AddLast(104);
            _data3.AddLast(105);
           
            _data3.Remove(node);
            
        }
    }

    class MyLinkedListNode<T>
    {
        public MyLinkedListNode<T> Next;
        public MyLinkedListNode<T> Prev;
        public T Data;
    }

    class MyLinkedList<T>
    {
        public MyLinkedListNode<T> Head = null; //첫번째
        public MyLinkedListNode<T> Tail = null; //마지막
        public int Count = 0;

        // O(1)
        public MyLinkedListNode<T> AddLast(T data)
        {
            MyLinkedListNode<T> newRoom=new MyLinkedListNode<T>();
            newRoom.Data = data;

            // 만약 아직 방이 아예 없었다면, 새로 추가한 첫번째 방이 곧 Head
            if(Head == null) 
               Head = newRoom;

            // 기존의  [마지막 방]과 [새로 추가되는 방]을 연결
            if (Tail != null)
            {
                Tail.Next = newRoom;
                newRoom.Prev = Tail;
            }

            // [새로 추가되는 방]을 [마지막 방]으로 인정
            Tail=newRoom;
            Count++;
            return newRoom;
        }

        public void Remove(MyLinkedListNode<T> room)
        {
            // [기존의 첫번째 방 다음 방]을 [첫번째 방]으로 인정 
            if (Head == room)
                Head = Head.Next;
            
            // [기존의 마지막 방 이전 방]을 [마지막 방]으로 인정 
            if (Tail == room)
                Tail = Tail.Prev;

            if (room.Prev != null)
                room.Prev.Next = room.Next;

            if (room.Next != null)
                room.Next.Prev = room.Prev;
            
            Count--;
        }
    }
}
