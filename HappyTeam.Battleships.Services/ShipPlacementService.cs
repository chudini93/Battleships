using System;
using System.Collections.Generic;
using System.Linq;
using HappyTeam.Battleships.Common.Interfaces;
using HappyTeam.Battleships.Services.Core.Enums;
using HappyTeam.Battleships.Services.Core.Models;
using HappyTeam.Battleships.Services.Core.Extensions;
using IShipPlacementService = HappyTeam.Battleships.Services.Interfaces.IShipPlacementService;

namespace HappyTeam.Battleships.Services
{
    public class ShipPlacementService : IShipPlacementService
    {
        private readonly IRandomService _randomService;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ShipPlacementService(IRandomService randomService)
        {
            _randomService = randomService;
        }

        public void PlaceShipsRandomly(Board board, IList<Ship> ships)
        {
            bool startAgain = false;

            ships = ships
                // Reason: Biggest ships can be placed anywhere.
                .OrderByDescending(x => x.Size)
                .ToList();

            // TODO: Refactor those 3 loops - looks like mess.
            //       Try to minimize number of indentations.
            for (int i = 0; i != ships.Count && !startAgain; ++i)
            {
                Ship ship = ships[i];
                for (int shipNumber = 1; shipNumber <= ship.NumberOfShips; shipNumber++)
                {
                    bool isPlaced = false;

                    int loopCounter = 0;

                    for (; !isPlaced && loopCounter != 1000; ++loopCounter)
                    {
                        GridSpotModel emptyCell = ChooseRandomEmptyCell(board);
                        isPlaced = TryToPlaceShip(board, emptyCell, ship);
                    }

                    if (loopCounter == 1000)
                        startAgain = true;
                }
            }

            if (startAgain)
                PlaceShipsRandomly(board, ships);

            CleanBlockedCells(board);
        }

        private void CleanBlockedCells(Board board)
        {
            IEnumerable<GridSpotModel> blockedCells = board.Where(x => x.Status == CellStates.Blocked).AsEnumerable();

            foreach (GridSpotModel cell in blockedCells) 
                cell.Status = CellStates.Empty;
        }

        private bool TryToPlaceShip(Board board, GridSpotModel cell, Ship ship)
        {
            SearchMethods searchMethods = GenerateRandomSearchMethod();

            bool isPlaced = PlaceShipOnValidSpot(board, cell.Column, cell.Row, ship, searchMethods);

            if (isPlaced)
                return true;

            SearchMethods otherMethods = searchMethods == SearchMethods.Horizontal
                ? SearchMethods.Vertical
                : SearchMethods.Horizontal;

            isPlaced = PlaceShipOnValidSpot(board, cell.Column, cell.Row, ship, otherMethods);

            return isPlaced;
        }

        private SearchMethods GenerateRandomSearchMethod()
        {
            return _randomService.GenerateBool() ? SearchMethods.Horizontal : SearchMethods.Vertical;
        }

        private GridSpotModel ChooseRandomEmptyCell(IList<GridSpotModel> board)
        {
            try
            {
                var emptyCells = board.Where(x => x.Status == CellStates.Empty).ToList();

                int min = 0;
                int max = emptyCells.Count;
                var rand = new Random();

                int emptyCellIndex = rand.Next(min, max);

                GridSpotModel output = emptyCells[emptyCellIndex];
                return output;
            }
            catch
            {
                throw new Exception("Choosing random cell for a ship has failed.");
            }
        }


        /// <summary>
        /// Places ship on randomly found spot and blocks it around on a board in order to prevent collision with other ships.
        /// Returns:
        ///   True - successful placement of a ship,
        ///   False - when there is no way to place the ship on provided cell.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <param name="ship">Ship to place on a board</param>
        /// <param name="searchMethods"></param>
        /// <returns></returns>
        private bool PlaceShipOnValidSpot(Board board, int column, int row, Ship ship, SearchMethods searchMethods)
        {
            try
            {
                IList<Coordinate> validCoordinates = FindCoordinatesForShipPlacement(board, column, row, ship.Size, searchMethods);

                PlaceShip(board, validCoordinates, ship);
                BlockSpaceAround(board, validCoordinates);

                return true;
            }
            catch (Exception)
            {
                // TODO: Log exception.
                return false;
            }
        }

        /// <summary>
        /// Places a ship on a board based on coordinates.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="coordinates">Coordinates to place ship onto</param>
        /// <param name="ship"></param>
        private void PlaceShip(Board board, IList<Coordinate> coordinates, Ship ship)
        {
            int shipSizeRemaining = ship.Size;

            foreach (Coordinate coordinate in coordinates)
            {
                GridSpotModel cell = board.Get(coordinate.Col, coordinate.Row);

                cell.Status = CellStates.Ship;
                cell.ShipIdentifier = ship.Identifier;

                shipSizeRemaining--;
                if (shipSizeRemaining == 0)
                    break;
            }
        }

        /// <summary>
        /// Block cells around provided coordinates.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="targetCells"></param>
        private void BlockSpaceAround(Board board, IList<Coordinate> targetCells)
        {
            int minRow = targetCells.Min(x => x.Row);
            int maxRow = targetCells.Max(x => x.Row);

            int minCol = targetCells.Min(x => x.Col);
            int maxCol = targetCells.Max(x => x.Col);

            for (int y = minRow; y <= maxRow; y++)
            {
                for (int x = minCol; x <= maxCol; x++)
                {
                    board.BlockAround(x, y);
                }
            }
        }

        /// <summary>
        /// Returns coordinates for empty cells onto which ship can be placed.
        /// If there are no enough place on a board <see cref="Exception"/> is thrown.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="shipSize"></param>
        /// <param name="searchMethods"></param>
        /// <returns></returns>
        private IList<Coordinate> FindCoordinatesForShipPlacement(Board board, int col, int row, int shipSize, SearchMethods searchMethods)
        {
            var output = new List<Coordinate>();

            var searchDirection = searchMethods == SearchMethods.Horizontal ? SearchDirections.Left : SearchDirections.Top;

            IList<Coordinate> emptyLeftCells = SearchForEmptyCells(board, row, col, shipSize, searchDirection);
            output.AddRange(emptyLeftCells);

            int shipLengthLeft = shipSize - output.Count;
            if (shipLengthLeft <= 0)
            {
                // No need to search further. There is enough coordinates to place the ship.
                return output;
            }

            if (searchMethods == SearchMethods.Horizontal)
            {
                searchDirection = SearchDirections.Right;
                col = col + 1;
            }
            else
            {
                searchDirection = SearchDirections.Bottom;
                row = row + 1;
            }

            IList<Coordinate> emptyRightCells = SearchForEmptyCells(board, row, col, shipLengthLeft, searchDirection);
            output.AddRange(emptyRightCells);

            bool enoughCoordinatesForShip = output.Count >= shipSize;
            if (!enoughCoordinatesForShip)
                throw new Exception("There are not enough coordinates found to place the ship on the board in this place.");

            return output;
        }

        /// <summary>
        /// Return collection of coordinates with empty cells.
        /// Search is performed in a straight line based on a <see cref="directions"/>
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row">Start row</param>
        /// <param name="col">Start column</param>
        /// <param name="length">Relates to how far board should be searched</param>
        /// <param name="directions">Direction in which search should be performed</param>
        /// <returns></returns>
        private IList<Coordinate> SearchForEmptyCells(Board board, int row, int col, int length, SearchDirections directions)
        {
            var output = new List<Coordinate>();

            for (int i = 1; i <= length; i++)
            {
                if (!board.IsEmpty(row, col))
                    break; // Stop searching after cell is not empty.

                output.Add(new Coordinate(col, row));
                (col, row) = MovePointer(directions, col, row);
            }

            return output;
        }

        /// <summary>
        /// Moves pointer to a column and row based on <see cref="direction"/>
        /// </summary>
        /// <param name="directions>Direction in which pointer should move.</param>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private (int currentColumn, int currentRow) MovePointer(SearchDirections directions, int column, int row)
        {
            switch (directions)
            {
                case SearchDirections.Left:
                    column--;
                    break;
                case SearchDirections.Right:
                    column++;
                    break;
                case SearchDirections.Top:
                    row++;
                    break;
                case SearchDirections.Bottom:
                    row--;
                    break;
            }

            return (column, row);
        }
    }
}
