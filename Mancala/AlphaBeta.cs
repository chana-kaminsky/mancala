namespace Mancala
{
    class AlphaBeta
    {
        public static double Value(Board board, int depth, double alfa, double beta, Player player)
        {
            double value;
            if (depth == 0 || board.GameOver())
            {
                value = board.HeuristicValue();
            }
            else
            {
                Player opponent = player == Player.MAX ? Player.MIN : Player.MAX;

                if (player == Player.MAX)
                {
                    for (int pit = 0; pit < Board.NUM_PITS; pit++)
                    {
                        if (!board.IsPitEmpty(Player.MAX, pit))
                        {
                            Board nextPosition = board.NewBoard(Player.MAX, pit);
                            double thisValue = Value(nextPosition, depth - 1, 
                                                        alfa, beta, opponent);
                            if (thisValue > alfa)
                            {
                                alfa = thisValue;
                            }
                            if (beta <= alfa)
                            {
                                break;
                            }
                        }
                    }
                    value = alfa;
                }
                else  // player == Player.MIN
                {

                    for (int pit = 0; pit < Board.NUM_PITS; pit++)
                    {
                        if(!board.IsPitEmpty(Player.MAX, pit))
                        {
                            Board nextPosition = board.NewBoard(Player.MAX, pit);
                            double thisValue = Value(nextPosition, depth - 1, 
                                                        alfa, beta, opponent);
                            if (thisValue < beta)
                            {
                                beta = thisValue;
                            }
                            if (beta <= alfa)
                            {
                                break;
                            }
                        }
                    }
                    value = beta;
                }
            }
            return value;
        }
    }
}