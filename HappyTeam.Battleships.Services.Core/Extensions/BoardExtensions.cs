using System;
using System.Collections.Generic;
using System.Linq;
using HappyTeam.Battleships.Services.Core.Enums;
using HappyTeam.Battleships.Services.Core.Models;

namespace HappyTeam.Battleships.Services.Core.Extensions
{
    public static class BoardExtensions
    {
        static Random rnd = new Random();

        /// <summary>
        /// Returns all empty cells from a board.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public static IList<GridSpotModel> AllEmptyCells(this Board board)
        {
            return board.Where(x => x.Status == CellStates.Empty).ToList();
        }

        public static GridSpotModel Get(this IList<GridSpotModel> cells, int column, int row)
        {
            return cells.FirstOrDefault(x => x.Row == row && x.Column == column);
        }

        public static bool CheckIfEmpty(this IList<GridSpotModel> cells, int column, int row)
        {
            return cells.Any(x => x.Status == CellStates.Empty && x.Row == row && x.Column == column);
        }

        public static bool IsEmpty(this Board board, int row, int col)
        {
            return board.Any(x => x.Row == row && x.Column == col && x.Status == CellStates.Empty);
        }
    }
}
