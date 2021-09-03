using System.Collections.Generic;
using HappyTeam.Battleships.Services.Core.Enums;
using HappyTeam.Battleships.Services.Core.Models;

namespace HappyTeam.Battleships.Services.Interfaces
{
    /// <summary>
    /// Provides methods for managing Battleship game.
    /// LINK: https://en.wikipedia.org/wiki/Battleship_(game)
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Indicates if the game has started. If Not <see cref="StartNewGame"/> must be executed.
        /// </summary>
        bool IsStarted { get; }

        bool IsGameOver { get; }

        Players Turn { get; }

        Player Player1 { get; }
        Player Player2 { get; }

        IList<string> Logs { get; }

        /// <summary>
        /// Starts a new game - creates empty boards for players.
        /// </summary>
        /// <param name="version"></param>
        void StartNewGame(GameVersions version);

        /// <summary>
        /// Places ships randomly for provided player.
        /// </summary>
        /// <param name="player"></param>
        void PlaceShipsRandomly(Player player);

        /// <summary>
        /// Fires randomly on <see cref="otherPlayer"/> board.
        /// </summary>
        /// <param name="otherPlayer">Player board to shot at</param>
        void FireRandomly(Player otherPlayer);

        /// <summary>
        /// Fires on <see cref="otherPlayer"/> board at specified coordinates.
        /// </summary>
        /// <param name="row">Row number to fire at</param>
        /// <param name="col">Column number to fire at</param>
        /// <param name="otherPlayer">Player board to shot at</param>
        void Fire(int row, int col, Player otherPlayer);
    }
}