﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    class Map
    {
        const char CIRCLE = '\u25cf';

        public TileType[,] Tile { get; private set; }
        public int Size { get; private set; }

        Player _player;

        public enum TileType
        {
            Empty,
            Wall,
        }
        public void Initialize(int size, Player player)
        {
            if (size % 2 == 0)
                return;

            _player = player;

            Tile = new TileType[size, size];
            Size = size;

            // Mazes for Programmers
            //GenerateByBinaryTree();
            GenerateBySideWinder();
        }

        void GenerateByBinaryTree()
        {
            //일단 길을 다 막아버리는 작업
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }

            //랜덤으로 우측 혹은 아래로 길을 뚫는 작업
            //binary tree algorithm
            Random rand = new Random();

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (x == Size - 2 && y == Size - 2)
                    {
                        continue;
                    }
                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                    }
                    else
                    {
                        Tile[y + 1, x] = TileType.Empty;
                    }

                }
            }
        }

        void GenerateBySideWinder()
        {
            //일단 길을 다 막아버리는 작업
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }

            //랜덤으로 우측 혹은 아래로 길을 뚫는 작업
            Random rand = new Random();

            for (int y = 0; y < Size; y++)
            {
                int count = 1;
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (x == Size - 2 && y == Size - 2)
                    {
                        continue;
                    }
                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        count++;
                    }
                    else
                    {
                        int randomIndex=rand.Next(0, count);
                        Tile[y + 1, x - randomIndex * 2] = TileType.Empty;
                        count = 1;
                    }

                }
            }
        }

        public void Render()
        {
            ConsoleColor prevColor=Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    /*플레이어 좌표를 갖고 와서, 그 좌표랑 현재 y, x가 일치하면 
                      플레이어 전용 색상으로 표시
                    */
                    if (y == _player.PosY && x == _player.PosX)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else
                        Console.ForegroundColor = GetTileColor(Tile[y, x]);
                    
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor;
        }
        ConsoleColor GetTileColor(TileType type)
        {
            switch(type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Green;
            }
        }
    }
}
