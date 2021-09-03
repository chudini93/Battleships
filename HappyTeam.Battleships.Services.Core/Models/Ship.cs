namespace HappyTeam.Battleships.Services.Core.Models
{
    public class Ship
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Ship(string name, int size, int numberOfShips)
        {
            Name = name;
            Size = size;
            NumberOfShips = numberOfShips;
        }

        /// <summary>
        /// Full name for a ship.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Size of a ship (in cells).
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Number of total ships on the board.
        /// </summary>
        public int NumberOfShips { get; set; }

        public string Label => Name[0].ToString() + Size;

        /// <summary>
        /// Returns ship identifier for single ship.
        /// </summary>
        /// <param name="index">Relates to exact ship of the same type.</param>
        /// <returns></returns>
        public string BuildIdentifier(int index)
        {
            return $"{Label}-{index}";
        }
    }
}