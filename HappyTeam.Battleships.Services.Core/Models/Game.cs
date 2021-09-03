using System;
using System.Collections.Generic;
using HappyTeam.Battleships.Services.Core.Enums;
using HappyTeam.Battleships.Services.Core.Extensions;

namespace HappyTeam.Battleships.Services.Core.Models
{
    public class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public IList<Ship> Fleet { get; set; }

        public bool IsGameOver => Player1.Board.IsOutOfShips() || Player2.Board.IsOutOfShips();

        public Players Turn { get; set; } = Players.Player1;

        public IList<string> Logs { get; } = new List<string>();

        /// <summary>
        /// Adds entry log.
        /// </summary>
        /// <param name="text"></param>
        public void AddLogEntry(string text)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            Logs.Add($"{time}: {text}");
        }

        /// <summary>
        /// Changes a turn to other player.
        /// </summary>
        public void NextPlayerTurn()
        {
            Turn = Turn == Players.Player1 ? Players.Player2 : Players.Player1;
        }
    }
}
