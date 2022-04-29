using System.ComponentModel.DataAnnotations;

namespace PlottnerBowlingChallenge.Model.DTO
{
    public class SaveRollItem
    {
        [Required]
        [Range(0,long.MaxValue)]
        public long PlayerId { get; set; }

        [Required]
        [Range(0, long.MaxValue)]
        public long GameId { get; set; }

        [Required]
        [Range(1, 10)]
        public int FrameNumber { get; set; }

        [Required]
        [Range(0, 10)]
        public int PinsKnockedDown { get; set; }

        [Required]
        [Range(1, 3)]
        public int Attempt { get; set; }
    }
}