using System.Collections.Generic;
using System.Linq;
using HappyTeam.Battleships.Services.Core.Enums;
using HappyTeam.Battleships.Services.Core.Extensions;

namespace HappyTeam.Battleships.Services.Core.Models
{
    public class Board : List<CellInfo>
    {
        /// <summary>
        /// Destroys selected ship from a board and blocks space around.
        /// </summary>
        /// <param name="shipId"></param>
        public void SunkShip(string shipId)
        {
            IList<CellInfo> coords = this.GetShipCoordinates(shipId);
            foreach (CellInfo cellInfo in coords)
            {
                cellInfo.Status = CellStates.Sunk;
            }

            BlockSpaceAround(coords);
        }

        /// <summary>
        /// Block cells around provided coordinates.
        /// </summary>
        /// <param name="targetCells"></param>
        public void BlockSpaceAround(IList<CellInfo> targetCells)
        {
            IList<Coordinate> coordinates = targetCells.Select(x => new Coordinate(x)).ToList();
            BlockSpaceAround(coordinates);
        }

        /// <summary>
        /// Block cells around provided coordinates.
        /// </summary>
        /// <param name="targetCells"></param>
        public void BlockSpaceAround(IList<Coordinate> targetCells)
        {
            int minRow = targetCells.Min(x => x.Row);
            int maxRow = targetCells.Max(x => x.Row);

            int minCol = targetCells.Min(x => x.Column);
            int maxCol = targetCells.Max(x => x.Column);

            for (int y = minRow; y <= maxRow; y++)
            {
                for (int x = minCol; x <= maxCol; x++)
                {
                    BlockAround(x, y);
                }
            }
        }

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
            CellInfo emptyCell = this.AllEmptyCells().FirstOrDefault(x => x.Column == column && x.Row == row);
            if (emptyCell == null)
            {
                // Can't be blocked - because not empty.
                return;
            }

            emptyCell.Status = CellStates.Blocked;
        }
    }
}