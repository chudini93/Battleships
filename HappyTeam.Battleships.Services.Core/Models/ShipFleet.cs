using System.Collections.Generic;

namespace HappyTeam.Battleships.Services.Core.Models
{
    public static class ShipFleet
    {
        /// <summary>
        /// Based on the 1990 Milton Bradley version of the rules.
        /// </summary>
        public static IList<Ship> MiltonBradleyVersion = new List<Ship>
        {
            new ("Carrier", 5, 1),
            new ("Battleship", 4, 1),
            new ("Cruiser", 3, 1),
            new ("Submarine", 3, 2),
            new ("Destroyer", 2, 2)
        };

        /// <summary>
        /// Based on the 2002 Hasbro version of the rules.
        /// </summary>
        public static IList<Ship> HasbroVersion = new List<Ship>
        {
            new ("Carrier", 5, 1),
            new ("Battleship", 4, 1),
            new ("Destroyer", 3, 1),
            new ("Submarine", 3, 2),
            new ("Patrol Boat", 2, 2)
        };
    }
}