using System.Collections.Generic;
using System.Linq;
using HappyTeam.Battleships.Services.Core.Enums;
using HappyTeam.Battleships.Services.Core.Models;

namespace HappyTeam.Battleships.Services.Core.Extensions
{
    public static class BoardExtensions
    {
        /// <summary>
        /// Returns all empty cells from a board.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public static IList<GridSpotModel> AllEmptyCells(this Board board)
        {
            return board.Where(x => x.Status == CellStates.Empty).ToList();
        }

        /// <summary>
        /// Returns a cell based on provided <see cref="column"/> and <see cref="row"/>.
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static GridSpotModel Get(this IList<GridSpotModel> cells, int column, int row)
        {
            return cells.FirstOrDefault(x => x.Row == row && x.Column == column);
        }

        /// <summary>
        /// Returns true if cell on a board is empty.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Board board, int row, int col)
        {
            return board.Any(x => x.Row == row && x.Column == col && x.Status == CellStates.Empty);
        }
    }
}
