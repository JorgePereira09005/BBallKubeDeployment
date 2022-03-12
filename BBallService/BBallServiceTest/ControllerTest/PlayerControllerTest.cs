using BBallService.Controllers;
using BBallService.Data;
using BBallService.Models;
using BBallService.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace BBallServiceTest
{
    public class PlayerControllerTest
    {
        [Fact]
        public async void GetPlayers()
        {
            //given
            var mockRepository = new Mock<PlayerRepository>();
            mockRepository.Setup(repo => repo.GetPlayers())
                .ReturnsAsync(GetTestPlayers());

            var controller = new PlayersController(mockRepository.Object);

            //when
            var result = await controller.GetPlayers();

            //then
            Assert.Equal(GetTestPlayers().ToString(), result.Value.ToString());
        }

        private List<Player> GetTestPlayers()
        {
            var players = new List<Player>();
            players.Add(new Player()
            {
                ID = 1,
                LastName = "LastName1",
                FirstName = "FirstName1",
                Salary = 12346.77M,
                SigningDate = DateTime.Today,
                Number = 15,
                IsAllStar = false,
                TeamID = 1
            });
            players.Add(new Player()
            {
                ID = 2,
                LastName = "Jojojojo",
                FirstName = "Jajajajaja",
                Salary = 567890,
                SigningDate = DateTime.Today,
                Number = 17,
                IsAllStar = true,
                TeamID = 2
            });
            return players;
        }
    }
}
