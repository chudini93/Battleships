namespace HappyTeam.Battleships.Services.Core.Models
{
    public class Coordinate
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Coordinate(int col, int row)
        {
            Col = col;
            Row = row;
        }
    }
}
