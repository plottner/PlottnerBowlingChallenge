using PlottnerBowlingChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlottnerBowlingChallenge.Data.Interface
{
    public interface IFrameRepository
    {
        public Frame SaveFrame(Frame frame);
        public Frame GetFrame(long playerId, long gameId, int frameNumber);
        public List<Frame> GetFramesByPlayerGame(long playerId, long gameId);
    }
}
