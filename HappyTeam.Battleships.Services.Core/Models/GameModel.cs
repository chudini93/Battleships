using System.Collections.Generic;

namespace HappyTeam.Battleships.Services.Core.Models
{
    public class GameModel
    {
        public Board Player1Board { get; set; }
        public Board Player2Board { get; set; }

        public IList<Ship> Fleet { get; set; }
    }
}
