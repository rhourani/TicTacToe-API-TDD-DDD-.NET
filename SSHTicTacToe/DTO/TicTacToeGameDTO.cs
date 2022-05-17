namespace SSHTicTacToe.DTO
{
    public class TicTacToeGameDTO : CustomError
    {
        public Guid Id { get; set; }
        public string Board { get; set; }
        public string Status { get; set; }
        public bool UserIsCrosses { get; set; }
    }

    public class TicTacToeGameResponseDTO 
    {
        public Guid Id { get; set; }
        public string Board { get; set; }
        public string Status { get; set; }
        public bool UserIsCrosses { get; set; }
    }

    public class TicTacToeGameCreationDTO
    {
        public string Board { get; set; }
    }

    public class CreateGameResponseDTO
    {
        public string Location { get; set; }
    }

    public class UpdateGameDTO
    {
        public string Board { get; set; }
    }

    public class Winner
    {
        public bool isWin { get; set; }
        public char WinnerName { get; set; }
    }

    public class UpdateGameResponseDTO : CustomError
    {
        public TicTacToeGameDTO TicTacToeGameDTO { get; set; }
    }

    public class CustomError
    {
        public string Message { get; set; }
        public bool isError { get; set; }
    }
}
