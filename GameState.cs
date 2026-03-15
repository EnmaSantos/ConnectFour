namespace ConnectFour;

public class GameState
{
    public enum WinState
    {
        No_Winner,
        Player1_Wins,
        Player2_Wins,
        Tie
    }

    public int PlayerTurn { get; private set; } = 1;

    public int CurrentTurn { get; private set; } = 0;

    private int[,] TheBoard { get; set; } = new int[7, 6];

    public void ResetBoard()
    {
        PlayerTurn = 1;
        CurrentTurn = 0;
        TheBoard = new int[7, 6];
    }

    public byte PlayPiece(int column)
    {
        if (CheckForWin() != WinState.No_Winner)
        {
            throw new ArgumentException("Game is over");
        }

        if (TheBoard[column, 0] != 0)
        {
            throw new ArgumentException("Column is full");
        }

        var landingRow = 0;
        for (var i = 5; i >= 0; i--)
        {
            if (TheBoard[column, i] == 0)
            {
                landingRow = i + 1;
                TheBoard[column, i] = PlayerTurn;
                break;
            }
        }

        PlayerTurn = PlayerTurn == 1 ? 2 : 1;
        CurrentTurn++;

        return (byte)landingRow;
    }

    public WinState CheckForWin()
    {
        // Check for vertical wins
        for (var col = 0; col < 7; col++)
        {
            for (var row = 0; row < 3; row++)
            {
                if (TheBoard[col, row] != 0 &&
                    TheBoard[col, row] == TheBoard[col, row + 1] &&
                    TheBoard[col, row] == TheBoard[col, row + 2] &&
                    TheBoard[col, row] == TheBoard[col, row + 3])
                {
                    return TheBoard[col, row] == 1 ? WinState.Player1_Wins : WinState.Player2_Wins;
                }
            }
        }

        // Check for horizontal wins
        for (var col = 0; col < 4; col++)
        {
            for (var row = 0; row < 6; row++)
            {
                if (TheBoard[col, row] != 0 &&
                    TheBoard[col, row] == TheBoard[col + 1, row] &&
                    TheBoard[col, row] == TheBoard[col + 2, row] &&
                    TheBoard[col, row] == TheBoard[col + 3, row])
                {
                    return TheBoard[col, row] == 1 ? WinState.Player1_Wins : WinState.Player2_Wins;
                }
            }
        }

        // Check for diagonal wins (bottom-left to top-right)
        for (var col = 0; col < 4; col++)
        {
            for (var row = 3; row < 6; row++)
            {
                if (TheBoard[col, row] != 0 &&
                    TheBoard[col, row] == TheBoard[col + 1, row - 1] &&
                    TheBoard[col, row] == TheBoard[col + 2, row - 2] &&
                    TheBoard[col, row] == TheBoard[col + 3, row - 3])
                {
                    return TheBoard[col, row] == 1 ? WinState.Player1_Wins : WinState.Player2_Wins;
                }
            }
        }

        // Check for diagonal wins (top-left to bottom-right)
        for (var col = 0; col < 4; col++)
        {
            for (var row = 0; row < 3; row++)
            {
                if (TheBoard[col, row] != 0 &&
                    TheBoard[col, row] == TheBoard[col + 1, row + 1] &&
                    TheBoard[col, row] == TheBoard[col + 2, row + 2] &&
                    TheBoard[col, row] == TheBoard[col + 3, row + 3])
                {
                    return TheBoard[col, row] == 1 ? WinState.Player1_Wins : WinState.Player2_Wins;
                }
            }
        }

        // Check for tie
        if (CurrentTurn == 42)
        {
            return WinState.Tie;
        }

        return WinState.No_Winner;
    }
}