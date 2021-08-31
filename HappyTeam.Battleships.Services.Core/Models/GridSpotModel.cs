using HappyTeam.Battleships.Services.Core.Enums;

namespace HappyTeam.Battleships.Services.Core.Models
{
    public class GridSpotModel
    {
        public char SpotLetter { get; set; }
        public int SpotNumber { get; set; }
        public CellStates Status { get; set; } = CellStates.Empty;
    }
}