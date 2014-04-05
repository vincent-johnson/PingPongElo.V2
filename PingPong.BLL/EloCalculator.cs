using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using PingPong.Entities;

namespace PingPong.BLL
{
    public static class EloCalculator
    {
        public const double StandardDeviationPointsCount=200;

        public static double GetPlayerEloChange(double playerElo, double opponentElo, bool playerWon, int weight)
        {
            var chart = new Chart();
            double playerExpectedScore = chart.DataManipulator.Statistics.NormalDistribution((playerElo - opponentElo) / StandardDeviationPointsCount);
            int playerActualScore=playerWon?1:0;
            double ratingChange = weight * (playerActualScore-playerExpectedScore);
            return ratingChange;
        }
    }
}
