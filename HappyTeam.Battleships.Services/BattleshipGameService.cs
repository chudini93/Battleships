using HappyTeam.Battleships.Services.Core.Enums;
using HappyTeam.Battleships.Services.Core.Models;
using HappyTeam.Battleships.Services.Interfaces;

namespace HappyTeam.Battleships.Services
{
    public class BattleshipGameService : IBattleshipGameService
    {
        /// <summary>
        /// Width of the board.
        /// NOTE: Currently width >= 10 is supported.
        /// </summary>
        private const int BOARD_WIDTH = 10;

        /// <summary>
        /// Height of the board.
        /// NOTE: Currently height >= 10 is supported.
        /// </summary>
        private const int BOARD_HEIGHT = 10;

        public GameModel StartNewGame()
        {
            var output = new GameModel
            {
                Player1Board = GenerateEmptyBoard(BOARD_WIDTH, BOARD_HEIGHT),
                Player2Board = GenerateEmptyBoard(BOARD_WIDTH, BOARD_HEIGHT)
            };

            return output;
        }

        /// <summary>
        /// Generates empty board based on the <see cref="width"/> and <see cref="height"/>.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Board GenerateEmptyBoard(int width, int height)
        {
            var output = new Board();

            // TODO: Rewrite it to something smarter without the need for manual placement of chars inside char.
            char[] xRowNames = new [] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'Q', 'R', 'S', 'T', 'U', 'V', 'W'};

            for (int y = 1; y <= height ; y++)
            {
                for (int x = 1; x <= width; x++)
                {
                    output.Add(new GridSpotModel
                    {
                        SpotLetter = xRowNames[x-1],
                        SpotNumber = y,
                        Status = CellStates.Empty
                    });
                }
            }

            return output;
        }
    }
}
