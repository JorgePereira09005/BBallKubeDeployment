using BBallService.Data;
using BBallService.Models;
using BBallService.Models.InputModels;
using BBallService.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BBallService.Repository
{
    public class PlayerRepositoryImpl : PlayerRepository
    {
        private readonly BBallContext _context;
        private readonly IConfiguration _config;

        public PlayerRepositoryImpl(BBallContext bBallContext, IConfiguration config)
        {
            _context = bBallContext;
            _config = config;
        }

        public Task DeletePlayer(Player player)
        {
            _context.Players.Remove(player);
            return _context.SaveChangesAsync();
        }

        public Task<Player> GetPlayer(int id)
        {
            return _context.Players.FirstOrDefaultAsync(p => p.ID == id);
        }

        public Task<List<Player>> GetPlayers()
        {
            return _context.Players
                .Include(p => p.Team)
                .Include(p => p.Representations)
                    .ThenInclude(r => r.Agent)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PlayerInfo> GetPlayerInfo(Player player)
        {
            string url = _config.GetValue<string>(
                "PictureServiceUrl") + "/" + player.FullName;
            PlayerInfo playerInfo;

            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                playerInfo = JsonConvert.DeserializeObject<PlayerInfo>(response.Content.ReadAsStringAsync().Result);
            }

            //if wikiurl isn't set for player object, set it
            if (String.IsNullOrEmpty(player.WikiUrl)) {
                player.WikiUrl = playerInfo.WikiUrl;
                _context.Entry(player).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            
            return playerInfo;
        }

        public Task PostPlayer(Player player)
        {
            _context.Players.Add(player);
            return _context.SaveChangesAsync();
        }

        public Task PutPlayer(int id, Player player)
        {
            _context.Entry(player).State = EntityState.Modified;
            _context.Entry(player).Property(p => p.WikiUrl).IsModified = false;
            return _context.SaveChangesAsync();
        }

        public bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.ID == id);
        }

        public bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.ID == id);
        }
    }
}
