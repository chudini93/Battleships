using HappyTeam.Battleships.Services.Core.Enums;

namespace HappyTeam.Battleships.Services.Core.Models
{
    public class Player
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

        public Players Name { get; set; }

        public Board Board { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="player"></param>
        public Player(Players player)
        {
            Name = player;
            Board = GenerateEmptyBoard(BOARD_WIDTH, BOARD_HEIGHT);
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

            for (int y = 1; y <= width; y++)
            {
                for (int x = 1; x <= height; x++)
                {
                    output.Add(new CellInfo
                    {
                        Row = y,
                        Column = x,
                        Status = CellStates.Empty
                    });
                }
            }

            return output;
        }
    }
}