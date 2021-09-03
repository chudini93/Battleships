using HappyTeam.Battleships.Common;
using HappyTeam.Battleships.Common.Interfaces;
using HappyTeam.Battleships.Services;
using HappyTeam.Battleships.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HappyTeam.Battleships.UI
{
    public static class StartupExtensions
    {
        public static void ConfigureIoCContainer(this IServiceCollection services)
        {
            services.ConfigureServices();
        }

        private static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IShipPlacementService, ShipPlacementService>();
            services.AddScoped<IBattleshipService, BattleshipService>();
            services.AddScoped<IRandomService, RandomService>();
        }
    }
}
