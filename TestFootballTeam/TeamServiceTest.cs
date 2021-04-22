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
        public void TestTotalGoalsDefaultValuesTeamAndYear()
        {
            //Arrange
            TeamService team = new TeamService();

            //Act
            int result = team.GetTotalGoals(default, default);

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void TestTotalGoalsBacelona2011()
        {
            //Arrange
            TeamService team = new TeamService();
            //Act
            int result = team.GetTotalGoals("Barcelona", 2011);
            //Assert
            Assert.Equal(35, result);
        }
    }
}
