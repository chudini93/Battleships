namespace HappyTeam.Battleships.Common.Interfaces
{
    public interface IRandomService
    {
        bool GenerateBool();

        int GenerateInt(int max, int min = 0);
    }
}