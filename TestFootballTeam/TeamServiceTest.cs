using FootballTeam.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestFootballTeam
{
    public class TeamServiceTest
    {
        [Fact]
        public void TestDefaultTeamAndYear()
        {
            //Arrange
            TeamService team = new TeamService();

            //Act
            int result = team.GetTotalGoals(default, default);

            //Assert
            Assert.Equal(0, result); Assert.Equal(0, result);
        }

        [Fact]
        public void TestBacelona2011()
        {
            //Arrange
            TeamService team = new TeamService();
            //Act
            int result = team.GetTotalGoals("Bacelona", 2011);
            //Assert
            Assert.Equal(35, result);
        }
    }
}
