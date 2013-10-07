namespace ZeldaPlatformer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using (Game game = new Game())
            {
                game.Run();
            }
        }
    }
}