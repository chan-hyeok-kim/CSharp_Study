namespace Algorithm
{

    internal class Program
    {

        static void Main(string[] args)
        {
            Board board = new Board();
            //board.Initialize();
            board.InitializeLinked();
            Console.CursorVisible = false;

          
            const char CIRCLE = '\u25cf';
           


            while (true)
            {
               
                //입력
                //로직
                //렌더링

               Console.SetCursorPosition(0, 0);
                for(int i = 0; i < 25; i++)
                {
                    for (int j = 0; j < 25; j++)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue; 
                        Console.Write(CIRCLE);
                    }
                    Console.WriteLine();
                }

                #region 프레임 관리
                Thread.Sleep(1000 / 30); // 30fps로 제한, 초당 30번 렌더링

                #endregion
            }
        }
    }
}
