namespace SSHTicTacToe
{
    public class TicTacToeGame
    {
        public Guid Id { get; set; }
        public string Board { get; set; }
        public GameStatues Status { get; set; }
    }

    public enum GameStatues
    {
        RUNNING = 10,
        X_WON = 20,
        O_WON = 30,
        DRAW = 40,
    }
}