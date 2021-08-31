using HappyTeam.Battleships.Services.Core.Models;

namespace HappyTeam.Battleships.Services.Interfaces
{
    public interface ISimulatorService
    {
        /// <summary>
        /// Places battleships on a provided board automatically.
        /// </summary>
        /// <param name="board">Board to place ships to</param>
        void SimulateShipsPlacement(Board board);

        /// <summary>
        /// Simulates a game between two players.
        /// </summary>
        /// <param name="game"></param>
        void SimulateGame(GameModel game);
    }
}
