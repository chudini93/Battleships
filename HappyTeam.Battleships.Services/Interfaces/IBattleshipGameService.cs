using HappyTeam.Battleships.Services.Core.Enums;
using HappyTeam.Battleships.Services.Core.Models;

namespace HappyTeam.Battleships.Services.Interfaces
{
    /// <summary>
    /// Provides methods for managing Battleship game.
    /// LINK: https://en.wikipedia.org/wiki/Battleship_(game)
    /// </summary>
    public interface IBattleshipGameService
    {
        /// <summary>
        /// Returns empty boards for two players.
        /// </summary>
        /// <returns></returns>
        GameModel StartNewGame(GameVersions version);

        void PlaceShipsRandomly(Board board);
    }
}