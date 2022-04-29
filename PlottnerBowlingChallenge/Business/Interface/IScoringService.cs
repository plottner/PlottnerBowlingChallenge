using PlottnerBowlingChallenge.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlottnerBowlingChallenge.Business.Interface
{
    public interface IScoringService
    {
        public PlayerScore GetPlayerScore(long playerId, long gameId);
    }
}
