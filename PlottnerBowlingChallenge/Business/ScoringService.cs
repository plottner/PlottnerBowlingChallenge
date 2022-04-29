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
    public class ScoringService : IScoringService
    {
        private readonly IFrameRepository _frameRepository;

        public ScoringService(IFrameRepository frameRepository)
        {
            _frameRepository = frameRepository;
        }

        public PlayerScore GetPlayerScore(long playerId, long gameId)
        {
            var playerFrames = _frameRepository.GetFramesByPlayerGame(playerId, gameId);

            if(playerFrames == null)
            {
                throw new InvalidOperationException($"{nameof(_frameRepository.GetFramesByPlayerGame)} should not be null. Empty object should be returned instead.");
            }

            return new PlayerScore
            {
                PlayerId = playerId,
                GameId = gameId,
                Frames = playerFrames,
                TotalScore = CalculateTotalScore(playerFrames)
            };

        }
        private int CalculateTotalScore(List<Frame> frames)
        {
            int totalScore = 0;
            var playerFramesLinkedList = new LinkedList<Frame>(frames.OrderBy(pf => pf.FrameNumber));
            for (var frameLinkeNode = playerFramesLinkedList.First; frameLinkeNode != null; frameLinkeNode = frameLinkeNode.Next)
            {
                totalScore += GetFrameScore(frameLinkeNode);
            }
            return totalScore;
        }

        private int GetFrameScore(LinkedListNode<Frame> frame)
        {
            if (frame?.Value?.Rolls == null)
            {
                throw new ArgumentNullException(nameof(frame));
            }
            int baseScore = frame.Value.Rolls.Sum(r => r.PinsKnockedDown);
            return baseScore + GetBonusEarned(frame);
        }

        private int GetBonusEarned(LinkedListNode<Frame> currentFrame)
        {

            if(currentFrame?.Value?.Rolls == null)
            {
                throw new ArgumentNullException(nameof(currentFrame));
            }
            LinkedListNode<Frame> nextFrame = currentFrame.Next;
            if (nextFrame?.Value?.Rolls == null)
            {
                return 0;
            }

            var bonusCount = GetBonusCount(currentFrame);
            if (bonusCount < 1)
            {
                return 0;
            }

            var rollsToScore = OrderRolls(nextFrame.Value).Take(bonusCount);
            if(bonusCount > 1 && rollsToScore.Count() == 1 && nextFrame.Next?.Value != null)
            {
                rollsToScore.Union(OrderRolls(nextFrame.Next.Value).Take(1));
            }
            return rollsToScore.Sum(r => r.PinsKnockedDown);
        }

        private static int GetBonusCount(LinkedListNode<Frame> currentFrame)
        {
            if (currentFrame?.Value?.Rolls == null)
            {
                throw new ArgumentNullException(nameof(currentFrame));
            }
            var frame = currentFrame.Value;
            int result = 0;
            if(OrderRolls(frame).Sum(r => r.PinsKnockedDown) >= 10)
            {
                result++;
            }
            if (OrderRolls(frame)?.First()?.PinsKnockedDown == 10)
            {
                result++;
            }
            return result;
        }

        private static IOrderedEnumerable<Roll> OrderRolls(Frame frame)
        {
            return frame.Rolls.OrderBy(r => r.Attempt);
        }
    }
}
