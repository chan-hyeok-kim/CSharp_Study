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

            Map map = new Map();
            Player player = new Player();
            map.Initialize(25, player);
            player.Initialize(1, 1, map.Size-2, map.Size-2, player);

            const int WAIT_TICK = 1000 / 30;
            int lastTick = 0;
            while (true)
            {
                int currentTick = Environment.TickCount & Int32.MaxValue;
                if (currentTick - lastTick < WAIT_TICK)
                    continue;
                int deltaTick = currentTick - lastTick;
                lastTick = currentTick; 
                //입력
                //로직
                player.Update(deltaTick); 

                //렌더링

                Console.SetCursorPosition(0, 0);
                map.Render();

              /*  #region 프레임 관리
                Thread.Sleep(1000 / 30); // 30fps로 제한, 초당 30번 렌더링

                #endregion*/
            }
        }
    }
}
