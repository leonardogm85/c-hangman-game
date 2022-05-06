namespace HangmanGame
{
    class Program
    {
        static void Main()
        {
            // Inicia o jogo.
            Game game = new Game(3);
            game.Play();
        }
    }
}
