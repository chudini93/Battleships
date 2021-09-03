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

        /// <summary>
        /// Game state.
        /// </summary>
        private GameModel _game;

        private readonly IShipPlacementService _shipPlacementService;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BattleshipGameService(IShipPlacementService shipPlacementService)
        {
            _shipPlacementService = shipPlacementService;
        }

        /// <summary>
        /// Returns empty boards for two players.
        /// </summary>
        /// <returns></returns>
        public GameModel StartNewGame(GameVersions version)
        {
            var output = new GameModel
            {
                Player1Board = GenerateEmptyBoard(BOARD_WIDTH, BOARD_HEIGHT),
                Player2Board = GenerateEmptyBoard(BOARD_WIDTH, BOARD_HEIGHT),
                Fleet = version == GameVersions.Milton ? ShipFleet.MiltonBradleyVersion : ShipFleet.HasbroVersion
            };

            _game = output;

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
            
            for (int y = 1; y <= width; y++)
            {
                for (int x = 1; x <= height; x++)
                {
                    output.Add(new GridSpotModel
                    {
                        Row = y,
                        Column = x,
                        Status = CellStates.Empty
                    });
                }
            }

            return output;
        }

        /// <summary>
        /// Places battleships on a provided board automatically.
        /// </summary>
        /// <param name="board">Board to place ships to</param>
        public void PlaceShipsRandomly(Board board)
        {
            this._shipPlacementService.PlaceShipsRandomly(board, _game.Fleet);
        }
    }
}
