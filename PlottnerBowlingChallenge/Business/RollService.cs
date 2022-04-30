using PlottnerBowlingChallenge.Business.Interface;
using PlottnerBowlingChallenge.Data.Interface;
using PlottnerBowlingChallenge.Model;
using PlottnerBowlingChallenge.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlottnerBowlingChallenge.Business
{
    internal class RollService : IRollService
    {
        private readonly IScoringService _scoringService;
        private readonly IValidator<SaveRollItem> _saveRollItemValidator;
        private readonly IFrameRepository _frameRepository;

        public RollService(IScoringService scoringService,
            IValidator<SaveRollItem> saveRollItemValidator,
            IFrameRepository frameRepository)
        {
            _scoringService = scoringService;
            _saveRollItemValidator = saveRollItemValidator;
            _frameRepository = frameRepository;
        }

        public PlayerScore SaveRoll(SaveRollItem saveRollItem)
        {
            var validationResult = _saveRollItemValidator.Validate(saveRollItem);
            if(validationResult.Count > 0)
            {
                throw new ArgumentException(String.Join(Environment.NewLine, validationResult.Select(vr => $"{vr.MemberNames} - {vr.ErrorMessage}"));
            }

            var frame = _frameRepository.GetFrame(saveRollItem.PlayerId, saveRollItem.GameId, saveRollItem.FrameNumber);
            if (frame == null)
            {
                frame = new Frame
                {
                    PlayerId = saveRollItem.PlayerId,
                    GameId = saveRollItem.GameId,
                    FrameNumber = saveRollItem.FrameNumber,
                    Rolls = new List<Roll>
                    {
                        new Roll
                        {
                            Attempt = saveRollItem.Attempt,
                            PinsKnockedDown = saveRollItem.PinsKnockedDown
                        }
                    }

                };
            }
            else
            {
                var matchingRoll = frame.Rolls.FirstOrDefault(r => r.Attempt == saveRollItem.Attempt);
                if(matchingRoll == null)
                {
                    frame.Rolls.Add(new Roll
                    {
                        Attempt = saveRollItem.Attempt,
                        PinsKnockedDown = saveRollItem.PinsKnockedDown
                    });
                }
                else
                {
                    matchingRoll.PinsKnockedDown = saveRollItem.PinsKnockedDown;
                }
            }
            _frameRepository.SaveFrame(frame);
            return _scoringService.GetPlayerScore(saveRollItem.PlayerId, saveRollItem.GameId);
        }
    }
}
