using BBallService.Data;
using BBallService.Models;
using BBallService.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBallService.Repository.Interfaces
{
    public interface PlayerRepository
    {
        Task<List<Player>> GetPlayers();
        Task<Player> GetPlayer(int id);
        Task<PlayerInfo> GetPlayerInfo(Player player);
        Task PutPlayer(int id, Player player);
        Task PostPlayer(Player player);
        Task DeletePlayer(Player player);
        bool PlayerExists(int id);
        bool TeamExists(int id);
    }
}
