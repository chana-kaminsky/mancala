using System;

namespace Mancala
{
    class Program
    {
        static Board board = new Board();
        static readonly int MAX_DEPTH = 4;
        static void Main(string[] args)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("      MANCALA GAME");
            Console.WriteLine("------------------------");

            PlayGame();
            board.PrintScore();
        }

        private static void PlayGame()
        {
            bool showDecision = GetShowDecision();
            int move;

            Console.Write("Do you want to go first? ");
            String input = Console.ReadLine();
            board.PrintBoard();

            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                move = PlayersTurn();
                Console.WriteLine("\n Player's move: " + move);
                board = board.NewBoard(Player.MAX, move);
                board.PrintBoard();
            }

            while (!board.GameOver())
            {
                move = ComputersTurn(showDecision);
                Console.WriteLine("Computer's move: " + move);
                board = board.NewBoard(Player.MIN, move);
                board.PrintBoard();
                if (board.GameOver()) break;

                move = PlayersTurn();
                Console.WriteLine("\n Player's move: " + move);
                board = board.NewBoard(Player.MAX, move);
                board.PrintBoard();
            }
        }

        private static bool GetShowDecision()
        {
            Console.Write("Do you want to see the computer's descision process? ");
            String input = Console.ReadLine();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            return false;
        }

        private static int ComputersTurn(bool showDecision)
        {   
            // Computer is minimizing!
            double lowestValue = 1;
            int bestMove = 0;
            double alpha = -1.0;
            double beta = 1.0;

            for (int pit = 0; pit < Board.NUM_PITS; pit++)
            {
                if (!board.IsPitEmpty(Player.MIN, pit))
                {
                    Board nextPosition = board.NewBoard(Player.MIN, pit);
                    double currentValue = AlphaBeta.Value
                            (nextPosition, MAX_DEPTH, alpha, beta, Player.MAX);

                    if (currentValue < lowestValue)
                    {
                        bestMove = pit;
                        lowestValue = currentValue;
                    }

                    if (showDecision)
                    {
                        Console.WriteLine("pit " + pit + ": " + currentValue);
                    }
                }
            }

            if (showDecision) Console.WriteLine("\n");
            return bestMove;
        }

        private static int PlayersTurn()
        {
            Console.WriteLine("It's your turn. Which pit do you want to start from?");
            int playerMove = UserInput.getInteger("", 0, 5);
            
            while (board.IsPitEmpty(Player.MAX, playerMove))
            {
                Console.WriteLine("Please choose a pit that's not empty.");
                playerMove = UserInput.getInteger("", 0, 5);
            }

            return playerMove;
        }
    }
}
