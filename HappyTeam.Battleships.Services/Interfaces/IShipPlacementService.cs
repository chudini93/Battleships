using System.Collections.Generic;
using HappyTeam.Battleships.Services.Core.Models;

namespace HappyTeam.Battleships.Services.Interfaces
{
    public interface IShipPlacementService
    {
        /// <summary>
        /// Places battleships on a provided board automatically.
        /// </summary>
        /// <param name="board">Board to place ships to</param>
        /// <param name="ships"></param>
        void PlaceShipsRandomly(Board board, IList<Ship> ships);
    }
}
