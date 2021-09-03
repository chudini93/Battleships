using System;
using System.Collections.Generic;
using HappyTeam.Battleships.Services.Core.Enums;
using HappyTeam.Battleships.Services.Core.Extensions;
using HappyTeam.Battleships.Services.Core.Models;
using HappyTeam.Battleships.Services.Interfaces;

namespace HappyTeam.Battleships.Services
{
    public class GameService : IGameService
    {
        private IList<Ship> _fleet;

        private readonly IShipPlacementService _shipPlacementService;
        private readonly IBattleshipService _battleshipService;

        public bool IsStarted { get; private set; }
        public Players Turn { get; private set; }
        public bool IsGameOver { get; private set; }


        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public IList<string> Logs { get; private set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GameService(IShipPlacementService shipPlacementService, IBattleshipService battleshipService)
        {
            _shipPlacementService = shipPlacementService;
            _battleshipService = battleshipService;
        }

        public void StartNewGame(GameVersions version)
        {
            Logs = new List<string>();
            Player1 = new Player(Players.Player1);
            Player2 = new Player(Players.Player2);
            _fleet = version == GameVersions.Milton ? ShipFleet.MiltonBradleyVersion : ShipFleet.HasbroVersion;

            Turn = Players.Player1;
            IsStarted = true;
            IsGameOver = false;
            AddLogEntry("Game has started. Empty board were generated");
        }

        public void PlaceShipsRandomly(Player player)
        {
            _shipPlacementService.PlaceShipsRandomly(player.Board, _fleet);
            AddLogEntry($"{player.Name} ships have been placed");
        }

        public void FireRandomly(Player otherPlayer)
        {
            if (IsGameOver)
                return;

            AttackOutput fireOutput = _battleshipService.FireRandomly(otherPlayer);
            FillLogsBasedOnAttackOutput(fireOutput);
        }

        public void Fire(int row, int col, Player otherPlayer)
        {
            AttackOutput attackOutput = _battleshipService.Fire(row, col, otherPlayer);
            FillLogsBasedOnAttackOutput(attackOutput);
        }

        private void FillLogsBasedOnAttackOutput(AttackOutput fireType)
        {
            switch (fireType)
            {
                case AttackOutput.Miss:
                    AddLogEntry($"{Turn} have missed.");
                    NextPlayerTurn();
                    break;
                case AttackOutput.Hit:
                    AddLogEntry($"{Turn} hit enemy's ship.");
                    break;
                case AttackOutput.Sunk:
                    AddLogEntry($"{Turn} destroyed enemy's ship.");
                    IsGameOver = Player1.Board.IsOutOfShips() || Player2.Board.IsOutOfShips();
                    if (IsGameOver)
                        AddLogEntry($"{Turn} WON THE GAME! Congratulations.");
                    break;
                case AttackOutput.InvalidCell:
                    AddLogEntry($"{Turn} selected invalid cell. Please try again.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Invalid value for type {nameof(AttackOutput)}.");
            }
        }

        /// <summary>
        /// Changes a turn to other player.
        /// </summary>
        private void NextPlayerTurn()
        {
            Turn = Turn == Players.Player1 ? Players.Player2 : Players.Player1;
        }

        private void AddLogEntry(string text)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            Logs.Add($"{time}: {text}");
        }
    }
}
