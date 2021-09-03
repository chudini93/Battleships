namespace HappyTeam.Battleships.Services.Core.Models
{
    public class Coordinate
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Coordinate(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public Coordinate(CellInfo cellInfo)
        {
            Column = cellInfo.Column;
            Row = cellInfo.Row;
        }
    }
}
