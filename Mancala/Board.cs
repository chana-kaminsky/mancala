using System;

namespace Mancala
{
	public class Board
	{
		int[] playerRow = { 4, 4, 4, 4, 4, 4 };
		int[] computerRow = { 4, 4, 4, 4, 4, 4 };
		double playerMancala;
		double computerMancala;
		public static readonly int NUM_PITS = 6;
		readonly double TOTAL_STONES = 48.0;

		public Board()
		{
			playerRow = new int[]{ 4, 4, 4, 4, 4, 4 };
			computerRow = new int[]{ 4, 4, 4, 4, 4, 4 };
			playerMancala = 0;
			computerMancala = 0;
		}

		public Board(int[] pr, int[] cr, double pm, double cm)
        {
			pr.CopyTo(playerRow, 0);
			cr.CopyTo(computerRow, 0);
			playerMancala = pm;
			computerMancala = cm;
		}

		public void PrintBoard()
		{
            Console.WriteLine("__________________");
            Console.Write(computerMancala + " |");
			foreach (int pit in computerRow)
            {
                Console.Write(pit + "|");
            }

            Console.Write("\n  ------------- \n  ");

			foreach (int pit in playerRow)
			{
				Console.Write("|" + pit);
			}
			Console.WriteLine("| " + playerMancala);
			Console.WriteLine("__________________\n");
		}

		public bool GameOver()
        {
			bool player = true;
			bool computer = true;
            for (int pit = 0; pit < NUM_PITS; pit++)
            {
                if (playerRow[pit] != 0)
                {
					player = false;
                }

				if (computerRow[pit] != 0)
                {
					computer = false;
                }
            }

			return player || computer;
        }

		public Player Won()
        {
			if (!GameOver())
            {
				// throw an error?
                Console.WriteLine("error, game not over");
            }
            else
            {
				for (int pit = 0; pit < NUM_PITS; pit++)
                {
					if (playerRow[pit] != 0)
                    {
						computerMancala += playerRow[pit];
						playerRow[pit] = 0;
                    }
					else if (computerRow[pit] != 0)
                    {
						playerMancala += computerRow[pit];
						computerRow[pit] = 0;
                    }
                }
            }

			double winner = Math.Max(playerMancala, computerMancala);
			return winner == playerMancala ? Player.MAX : Player.MIN;
        }

		public double HeuristicValue()
        {
			if (GameOver())
            {
				return Won() == Player.MAX ? 1.0 : -1.0;
            }
            else
            {
				return ((playerMancala / TOTAL_STONES) - 
						(computerMancala / TOTAL_STONES));
            }
        }

		public Board NewBoard(Player player, int startingPit)
        {
			Board newBoard = new Board(playerRow, computerRow, playerMancala, computerMancala);
			newBoard.MakeMove(player, startingPit);
			return newBoard;
        }

		public void MakeMove(Player player, int startingPit)
		{
			// startingPit must be between 0 and 5
			if (player == Player.MAX)
            {
				PlayerMove(startingPit);
            }
			else
            {
				ComputerMove(startingPit);
            }
        }

		public void PlayerMove(int startingPit)
        {
			int currentPit = startingPit;
			bool mySide = true;
			while (playerRow[startingPit] > 0)
			{
				if (mySide && currentPit == 0)
				{
					mySide = false;
					currentPit--;
				}
				else if (!mySide && currentPit == NUM_PITS - 1)
				{
					mySide = true;
					playerMancala++;
					playerRow[startingPit]--;
					currentPit = NUM_PITS;
					continue;
				}

				if (mySide)
				{
					currentPit--;
					playerRow[currentPit]++;
					
				}
				else
				{
					currentPit++;
					computerRow[currentPit]++;
				}

				playerRow[startingPit]--;
			}
		}

		public void ComputerMove(int startingPit)
        {
			int currentPit = startingPit;
			bool mySide = true;
			while (computerRow[startingPit] > 0)
            {
				if (mySide && currentPit == NUM_PITS - 1)
                {
					mySide = false;
					currentPit++;
                }
				else if (!mySide && currentPit == 0)
                {
					mySide = true;
					computerMancala++;
					computerRow[startingPit]--;
					currentPit = -1;
					continue;
                }

				if (mySide)
                {
					currentPit++;
					computerRow[currentPit]++;
                }
                else
                {
					currentPit--;
					playerRow[currentPit]++;
                }
				
				computerRow[startingPit]--;
            }
		}

		public bool IsPitEmpty(Player player, int pit)
        {
			if (player == Player.MAX)
            {
				return playerRow[pit] == 0 ? true : false;
            }
			else
            {
				return computerRow[pit] == 0 ? true : false;
            }
        }

		public void PrintScore()
        {
			if (Won() == Player.MAX)
			{
				Console.WriteLine("Yay! You won!");
			}
			else
			{
				Console.WriteLine("Game over. The computer won.");
			}

            Console.WriteLine("Your points: " + playerMancala);
            Console.WriteLine("Computer's points " + computerMancala);
		}
	}
}
