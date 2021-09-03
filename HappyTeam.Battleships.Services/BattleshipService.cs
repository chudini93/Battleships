using System.Collections.Generic;
using System.Linq;
using HappyTeam.Battleships.Common.Interfaces;
using HappyTeam.Battleships.Services.Core.Enums;
using HappyTeam.Battleships.Services.Core.Extensions;
using HappyTeam.Battleships.Services.Core.Models;
using HappyTeam.Battleships.Services.Interfaces;

namespace HappyTeam.Battleships.Services
{
    public class BattleshipService : IBattleshipService
    {
        private readonly IRandomService _randomService;
        private readonly IEnumerable<CellStates> _shotableCellStates = new[] { CellStates.Empty, CellStates.Ship };

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BattleshipService(IRandomService randomService)
        {
            _randomService = randomService;
        }

        public AttackOutput Fire(int row, int col, Player otherPlayer)
        {
            return FiredAt(row, col, otherPlayer);
        }

        public AttackOutput FireRandomly(Player otherPlayer)
        {
            CellInfo randomCell = RandomlyChooseShootableCell(otherPlayer);
            if (randomCell == null)
                return AttackOutput.InvalidCell;

            return Fire(randomCell.Row, randomCell.Column, otherPlayer);
        }

        /// <summary>
        /// Receives a hit.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="player">Player that will receive the hit</param>
        public AttackOutput FiredAt(int row, int col, Player player)
        {
            CellInfo cell = player.Board.Get(col, row);

            if (!_shotableCellStates.Contains(cell.Status))
            {
                return AttackOutput.InvalidCell;
            }

            if (cell.Status == CellStates.Empty)
            {
                UpdateCell(cell, CellStates.Miss);
                return AttackOutput.Miss;
            } 
            else if (cell.Status == CellStates.Ship)
            {
                UpdateCell(cell, CellStates.Hit);
                return GetAttackOutputBasedOnShipHealth(player.Board, cell);
            }

            return AttackOutput.Hit;
        }

        /// <summary>
        /// Identifies if Ship is sunk or not.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        private AttackOutput GetAttackOutputBasedOnShipHealth(Board board, CellInfo cell)
        {
            int shipHealth = board.GetShipHealth(cell.ShipId);
            if (shipHealth == 0)
            {
                board.SunkShip(cell.ShipId);
                return AttackOutput.Sunk;
            }
            return AttackOutput.Hit;
        }

        private void UpdateCell(CellInfo cell, CellStates newState)
        {
            cell.Status = newState;
        }

        private CellInfo RandomlyChooseShootableCell(Player otherPlayer)
        {
            IList<CellInfo> uncoveredCells = otherPlayer.Board.AllShootableCells();
            if (!uncoveredCells.Any())
                return null;

            int randomCellIndex = _randomService.GenerateInt(uncoveredCells.Count - 1);

            CellInfo output = uncoveredCells[randomCellIndex];
            return output;
        }
    }
}