using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlottnerBowlingChallenge.Model.DTO
{
    public class PlayerScore
    {
        public long PlayerId { get; set; }
        public long GameId { get; set; }
        public int TotalScore { get; set; }
        public List<Frame> Frames { get; set; } = new();
    }
}
