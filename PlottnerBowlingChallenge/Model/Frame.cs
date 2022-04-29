using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlottnerBowlingChallenge.Model
{
    public class Frame
    {
        public long PlayerId { get; set; }
        public long GameId { get; set; }
        public int FrameNumber { get; set; }
        public List<Roll> Rolls { get; set; } = new();
    }
}
