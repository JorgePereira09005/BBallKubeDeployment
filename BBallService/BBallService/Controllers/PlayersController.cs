using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BBallService.Data;
using BBallService.Models;
using BBallService.Models.InputModels;
using BBallService.Repository.Interfaces;
using System.Net.Http;

namespace BBallService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly PlayerRepository _playerRepository;

        public PlayersController(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        // GET: api/Players
        [HttpGet("home")]
        public string GetInfo()
        {
            return "BBall Player Service. Have a nice day.";
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _playerRepository.GetPlayers();
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _playerRepository.GetPlayer(id);
            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // GET: api/Players/info/5
        [HttpGet("info/{id}")]
        public async Task<ActionResult<PlayerInfo>> GetPlayerInfo(int id)
        {
            var player = await _playerRepository.GetPlayer(id);
            if (player == null)
            {
                return NotFound();
            }

            try {
                return await _playerRepository.GetPlayerInfo(player);
            } catch (HttpRequestException) {
                return BadRequest();
            }
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (id != player.ID)
            {
                return BadRequest();
            }

            if (!TeamExists(player.TeamID)) return BadRequest();

            try
            {
                await _playerRepository.PutPlayer(id, player);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer([FromBody]PlayerInput player)
        {
            if (!TeamExists(player.TeamID)) return BadRequest();

            Player createdPlayer = player.ConvertToPlayer();
            await _playerRepository.PostPlayer(createdPlayer);

            return CreatedAtAction("GetPlayer", new { id = createdPlayer.ID }, createdPlayer);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _playerRepository.GetPlayer(id);
            if (player == null)
            {
                return NotFound();
            }

            await _playerRepository.DeletePlayer(player);

            return NoContent();
        }

        private bool PlayerExists(int id)
        {
            return _playerRepository.PlayerExists(id);
        }

        private bool TeamExists(int id)
        {
            return _playerRepository.TeamExists(id);
        }
    }
}
