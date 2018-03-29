/*
Name: TicTacToe
Course: PROG1783-18W-Sec1-IT Support Programming Fundamentals
Professor: Scanlan, H.
Number Student ID: 7679566
Student: Daniel Brazil
Date: March, 20, 2018
*/

using System;
using System.Text;

namespace TicTacToe
{
    
    /// Enumerator that type of itens in board
    
    enum BoardItem
    {
        X, //caracter X
        O, //caracter O
        EMPTY // empty space.
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a object of game
            TicTacToe game = new TicTacToe();
            game.Play();

        }
    }

    
	/// Class of board .
	
	class Board
    {


        
        /// matrix of game.
        
        private BoardItem[,] matrix = new BoardItem[3, 3];

        
        /// Construtor.
        
        public Board()
        {
            // Initializes the matrix positions with EMPTY
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i, j] = BoardItem.EMPTY;
                }
            }
        }

        
        /// Draw the board.
        
        /// <returns>Returns the formatted tray.</returns>
        public string DrawBoard()
        {
            // Uses a StringBuilder to avoid string concatenation.
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Converts a BoardItem to a char to show on screen
                    sb.Append(" ").Append(TakeBoardItemToChar(matrix[i, j]));

                    if (j < 2)
                    {
                        sb.Append(" | ");
                    }
                }
                sb.AppendLine();

                if (i < 2)
                {
                    sb.Append("------------").AppendLine();
                }
            }

            return sb.ToString();
        }

        
        /// Converts a BoardItem to a char.
        
        /// <param name="boardItem">BoardItem to convert.</param>
        /// <returns>return the caracter.</returns>
        private char TakeBoardItemToChar(BoardItem boardItem)
        {
            switch (boardItem)
            {
                case BoardItem.X: return 'X';
                case BoardItem.O: return 'O';
                default: return ' ';
            }
        }

        
        /// Checks if the game is over.
        
        /// <param name="winnerBoardItem">Output parameter, which indicates the winning BoardItem (if any).</param>
        /// <returns>True if the game is over; false otherwise.</returns>
        public bool GameFinished(out BoardItem? winnerBoardItem)
        {
            // Check if there is any 3-item sequence.
            winnerBoardItem = CheckPosition();

            if (winnerBoardItem != null)
            {
                // If there is a sequence, the game is over
                return true;
            }

            //If there is no sequence, check to see if the board can still receive moves.
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matrix[i, j] == BoardItem.EMPTY)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        
        /// Make a move.
        
        /// <param name="row">Line</param>
        /// <param name="col">Column</param>
        /// <param name="boardItem">Item</param>
        public void ExecuteMove(int row, int col, BoardItem boardItem)
        {
            if (matrix[row, col] != BoardItem.EMPTY)
            {
                // If the move has already been made, throw an exception.
                throw new PlayException("This move has already been carried out");
            }

            // Set the item to the position of the board.
            matrix[row, col] = boardItem;
        }

        
        /// Checks if there is a 3-item sequence.
        
        /// <returns>The item that is part of the sequence or null if there is no sequence.</returns>
        private BoardItem? CheckPosition()
        {
            if (matrix[0, 0] != BoardItem.EMPTY && matrix[0, 0] == matrix[0, 1] && matrix[0, 1] == matrix[0, 2])
            {
                return matrix[0, 0];
            }

            if (matrix[1, 0] != BoardItem.EMPTY && matrix[1, 0] == matrix[1, 1] && matrix[1, 1] == matrix[1, 2])
            {
                return matrix[1, 0];
            }

            if (matrix[2, 0] != BoardItem.EMPTY && matrix[2, 0] == matrix[2, 1] && matrix[2, 1] == matrix[2, 2])
            {
                return matrix[2, 0];
            }

            if (matrix[0, 0] != BoardItem.EMPTY && matrix[0, 0] == matrix[1, 0] && matrix[1, 0] == matrix[2, 0])
            {
                return matrix[0, 0];
            }

            if (matrix[0, 1] != BoardItem.EMPTY && matrix[0, 1] == matrix[1, 1] && matrix[1, 1] == matrix[2, 1])
            {
                return matrix[0, 1];
            }

            if (matrix[0, 2] != BoardItem.EMPTY && matrix[0, 2] == matrix[1, 2] && matrix[1, 2] == matrix[2, 2])
            {
                return matrix[0, 2];
            }

            if (matrix[0, 0] != BoardItem.EMPTY && matrix[0, 0] == matrix[1, 1] && matrix[1, 1] == matrix[2, 2])
            {
                return matrix[0, 0];
            }

            if (matrix[0, 2] != BoardItem.EMPTY && matrix[0, 2] == matrix[1, 1] && matrix[1, 1] == matrix[2, 0])
            {
                return matrix[0, 2];
            }

            return null;
        }
    }

    
	/// Class of players
	
	class Players
    {
        
        /// property the name of player.
        
        public string Name { get; private set; }

        
        /// color of player.
        
        public ConsoleColor Color { get; private set; }

        
        /// Type of item ('X' or 'O').
        
        public BoardItem BoardItem { get; private set; }

        
        /// Construtor.
        
        /// <param name="name">Name.</param>
        /// <param name="color">color</param>
        /// <param name="boardItem">Item.</param>
        public Players(string name, ConsoleColor color, BoardItem boardItem)
        {
            this.Name = name;
            this.Color = color;
            this.BoardItem = boardItem;
        }

        
        /// Execute move of player
        
        /// <param name="board">board</param>
        /// <param name="row">Line</param>
        /// <param name="col">column</param>
        public void ExecuteMove(Board board, int row, int col)
        {
            board.ExecuteMove(row, col, BoardItem);
        }
    }

    
	/// Class the game
	
	class TicTacToe
    {
        
        /// board.
        
        private Board board = new Board();

        
        /// set array of players.
        
        private Players[] players = new Players[2];

        
        /// index active player.
        
        private int activePlayerIndex = -1;

        
        /// Method that start the game.
        
        public void Play()
        {
            // change colors
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            //call method that show the welcome 
            Welcome();

            // call method that take name of playes
            TakePlayerNames();

            // Loop excute until the game is finished
            BoardItem? winnerBoardItem;
            while (!board.GameFinished(out winnerBoardItem))
            {
                Console.Clear();
                //call method that show welcome screen
                Welcome();
                //call method that show the board.
                ShowBoard();
                ShowPositions();
                Console.SetCursorPosition(0, 8);

                //take player to play
                Players activePlayer = PlayerToPlay();
                Console.WriteLine("");
                Console.ForegroundColor = activePlayer.Color;
                Console.WriteLine("Player of the time: {0}", activePlayer.Name);

                while (true)
                {
                    // ask about the move.
                    Console.Write("\nEnter the move: ");
                    string play = Console.ReadLine();

                    try
                    {
                        // execute the move
                        RunMove(play, activePlayer);
                        break;
                    }
                    catch (PlayException e)
                    {
                        Console.WriteLine("Error: {0}", e.Message);
                    }
                }
            }

            Console.Clear();

            // call the method that show how the board finished.
            ShowBoard();

            Console.WriteLine("The game is over!\n");

            Players winnerPlayer = null;
            if (winnerBoardItem != null)
            {
                // if winnerBoardItem different of null, someone win
                if (players[0].BoardItem == winnerBoardItem)
                {
                    // player 1 win.
                    winnerPlayer = players[0];
                }
                else
                {
                    // player 2 win.
                    winnerPlayer = players[1];
                }

                Console.WriteLine("The winner is the player {0}! Congratulations!", winnerPlayer.Name);

            }
            else
            {
                // if winnerBoardItem equals null, nobody win.
                Console.WriteLine("There was not a winner this time!");
            }
            Console.ReadLine();
        }

        
        /// Method that read the name of players.
        
        private void TakePlayerNames()
        {
            string player1Name;
            string player2Name;

            while (true)
            {
                // ask name playe1.
                Console.Write("Player Name 1: ");
                player1Name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(player1Name))
                {
                    // if validation is true.
                    Console.WriteLine("Player 1's name is invalid\n");
                }
                else
                {
                    // if name not invalid.
                    break;
                }
            }


            while (true)
            {
                // ask name player2.
                Console.Write("Player Name 2: ");
                player2Name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(player2Name))
                {
                    // if validation is true.
                    Console.WriteLine("Player 2's name is invalid\n");
                }
                else if (player1Name.ToUpper() == player2Name.ToUpper())
                {
                    // if the name of player2 equals playe1
                    Console.WriteLine("Players can not have the same name\n");
                }
                else
                {
                    // if name not invalid.
                    break;
                }
            }

            // create players.
            players[0] = new Players(player1Name, ConsoleColor.Green, BoardItem.X);
            players[1] = new Players(player2Name, ConsoleColor.Red, BoardItem.O);
        }

        
        /// Show the board.
        
        private void ShowBoard()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(board.DrawBoard());
            Console.WriteLine("");
        }

        
        /// Show the welcome screen
        
        private void Welcome()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  Tic-Tac-Toe");
            Console.WriteLine("===============");
            Console.WriteLine("");
        }


        
        /// Show the positions
        
        private void ShowPositions()
        {
            Console.SetCursorPosition(40, 0);
            Console.Write("Top - Left      = 00");
            Console.SetCursorPosition(40, 1);
            Console.Write("Top - Middle    = 01");
            Console.SetCursorPosition(40, 2);
            Console.Write("Top - Right     = 02");

            Console.SetCursorPosition(40, 3);
            Console.Write("");

            Console.SetCursorPosition(40, 4);
            Console.Write("Middle - Left   = 10");
            Console.SetCursorPosition(40, 5);
            Console.Write("Middle - Middle = 11");
            Console.SetCursorPosition(40, 6);
            Console.Write("Middle - Right  = 12");

            Console.SetCursorPosition(40, 7);
            Console.Write("");

            Console.SetCursorPosition(40, 8);
            Console.Write("Bottom - Left   = 20");
            Console.SetCursorPosition(40, 9);
            Console.Write("Bottom - Middle = 21");
            Console.SetCursorPosition(40, 10);
            Console.Write("Bottom - Right  = 22");

        }


        
        /// Switch to the next player.
        
        /// <returns>Next player.</returns>
        private Players PlayerToPlay()
        {
            // Read the next index as if the list were circular.
            activePlayerIndex = (activePlayerIndex + 1) % 2;

            // Returns the player associated with the new index.
            return players[activePlayerIndex];
        }

        
        /// Run the move of player.
        
        /// <param name="move">play.</param>
        /// <param name="player">player</param>
        private void RunMove(string move, Players player)
        {
            int row;
            int col;

            try
            {
                // It makes the parse of the move. If the parse does not work, it throws an exception.
                if (!int.TryParse(move.Substring(0, 1), out row) || !int.TryParse(move.Substring(1, 1), out col))
                {
                    throw new PlayException("The given cast is invalid");
                }

                // Verifies that the row and column are between 0 and 2.
                if (row < 0 || row > 2 || col < 0 || col > 2)
                {
                    throw new PlayException("The indexes used are outside the allowed range");
                }

                // Make the move.
                player.ExecuteMove(board, row, col);
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new PlayException("The given cast is invalid", e);
            }
        }
    }

    
	/// Class of exceptions
	
	public class PlayException : Exception
    {
        
        /// Construtor.
        
        public PlayException() { }

        
        /// Construtor.
        
        /// <param name="message">menssage.</param>
        public PlayException(string message) : base(message) { }

        
        /// Construtor.
        
        /// <param name="message">messager.</param>
        /// <param name="inner">inner message.</param>
        public PlayException(string message, Exception inner) : base(message, inner) { }
    }
}
