using HappyTeam.Battleships.Services.Core.Enums;
using HappyTeam.Battleships.Services.Core.Models;

namespace HappyTeam.Battleships.Services.Interfaces
{
    public interface IBattleshipService
    {
        /// <summary>
        /// Attacks <see cref="otherPlayer"/> based on provided coordinates.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="otherPlayer"></param>
        AttackOutput Fire(int row, int col, Player otherPlayer);

        /// <summary>
        /// Automatically generates coordinates and attacks <see cref="otherPlayer"/>.
        /// </summary>
        /// <param name="otherPlayer"></param>
        AttackOutput FireRandomly(Player otherPlayer);
    }
}