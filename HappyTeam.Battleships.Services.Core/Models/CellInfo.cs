using HappyTeam.Battleships.Services.Core.Enums;

namespace HappyTeam.Battleships.Services.Core.Models
{
    public class CellInfo
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public char RowLetter
        {
            get
            {
                char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'Q', 'R', 'S', 'T', 'U', 'V', 'W' };

                var output = letters[Row];
                return output;
            }
        }

        public CellStates Status { get; set; } = CellStates.Empty;
        public string ShipId { get; set; }
        public string ShipLabel { get; set; }
    }
}