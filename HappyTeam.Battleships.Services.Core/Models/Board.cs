using System.Collections.Generic;
using System.Linq;
using HappyTeam.Battleships.Services.Core.Enums;
using HappyTeam.Battleships.Services.Core.Extensions;

namespace HappyTeam.Battleships.Services.Core.Models
{
    public class Board : List<GridSpotModel>
    {
        /// <summary>
        /// Block around 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        public void BlockAround(int col, int row)
        {
            // TODO: Refactor into more sophisticated code.
            // Block Top Row.
            BlockCellIfPossible(col - 1, row - 1);
            BlockCellIfPossible(col, row - 1);
            BlockCellIfPossible(col + 1, row - 1);

            // Block Middle Row.
            BlockCellIfPossible(col - 1, row);
            BlockCellIfPossible(col + 1, row);

            // Block Bottom Row.
            BlockCellIfPossible(col - 1, row + 1);
            BlockCellIfPossible(col, row + 1);
            BlockCellIfPossible(col + 1, row + 1);
        }

        /// <summary>
        /// Blocks selected cell if empty and not out of range.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        private void BlockCellIfPossible(int column, int row)
        {
            GridSpotModel emptyCell = this.AllEmptyCells().FirstOrDefault(x => x.Column == column && x.Row == row);
            if (emptyCell == null)
            {
                // Can't be blocked - because not empty.
                return;
            }

            emptyCell.Status = CellStates.Blocked;
        }
    }
}