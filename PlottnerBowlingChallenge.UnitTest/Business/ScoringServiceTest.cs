using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using PlottnerBowlingChallenge.Model;
using Moq;
using PlottnerBowlingChallenge.Data.Interface;
using PlottnerBowlingChallenge.Business;

namespace PlottnerBowlingChallenge.UnitTest.Business
{
    public class ScoringServiceTest
    {
        public class GetPlayerScore
        {
            #region Setup and Helpers
            long _playerId = 16;
            long _gameId = 32;
            public List<Frame> GetValidTestFrames() => new()
            {
                new()
                {
                    PlayerId = _playerId,
                    GameId = _gameId,
                    FrameNumber = 1,
                    Rolls = new()
                    {
                        new()
                        {
                            Attempt = 1,
                            PinsKnockedDown = 4
                        },
                        new()
                        {
                            Attempt = 2,
                            PinsKnockedDown = 3
                        }

                    }
                },
                new()
                {
                    PlayerId = _playerId,
                    GameId = _gameId,
                    FrameNumber = 2,
                    Rolls = new()
                    {
                        new()
                        {
                            Attempt = 1,
                            PinsKnockedDown = 7
                        },
                        new()
                        {
                            Attempt = 2,
                            PinsKnockedDown = 3
                        }

                    }
                },
                new()
                {
                    PlayerId = _playerId,
                    GameId = _gameId,
                    FrameNumber = 3,
                    Rolls = new()
                    {
                        new()
                        {
                            Attempt = 1,
                            PinsKnockedDown = 5
                        },
                        new()
                        {
                            Attempt = 2,
                            PinsKnockedDown = 2
                        }

                    }
                },
                new()
                {
                    PlayerId = _playerId,
                    GameId = _gameId,
                    FrameNumber = 4,
                    Rolls = new()
                    {
                        new()
                        {
                            Attempt = 1,
                            PinsKnockedDown = 8
                        },
                        new()
                        {
                            Attempt = 2,
                            PinsKnockedDown = 1
                        }

                    }
                },
                new()
                {
                    PlayerId = _playerId,
                    GameId = _gameId,
                    FrameNumber = 5,
                    Rolls = new()
                    {
                        new()
                        {
                            Attempt = 1,
                            PinsKnockedDown = 4
                        },
                        new()
                        {
                            Attempt = 2,
                            PinsKnockedDown = 6
                        }

                    }
                },
                new()
                {
                    PlayerId = _playerId,
                    GameId = _gameId,
                    FrameNumber = 6,
                    Rolls = new()
                    {
                        new()
                        {
                            Attempt = 1,
                            PinsKnockedDown = 2
                        },
                        new()
                        {
                            Attempt = 2,
                            PinsKnockedDown = 4
                        }

                    }
                },
                new()
                {
                    PlayerId = _playerId,
                    GameId = _gameId,
                    FrameNumber = 7,
                    Rolls = new()
                    {
                        new()
                        {
                            Attempt = 1,
                            PinsKnockedDown = 8
                        },
                        new()
                        {
                            Attempt = 2,
                            PinsKnockedDown = 0
                        }

                    }
                },
                new()
                {
                    PlayerId = _playerId,
                    GameId = _gameId,
                    FrameNumber = 8,
                    Rolls = new()
                    {
                        new()
                        {
                            Attempt = 1,
                            PinsKnockedDown = 8
                        },
                        new()
                        {
                            Attempt = 2,
                            PinsKnockedDown = 0
                        }

                    }
                },
                new()
                {
                    PlayerId = _playerId,
                    GameId = _gameId,
                    FrameNumber = 9,
                    Rolls = new()
                    {
                        new()
                        {
                            Attempt = 1,
                            PinsKnockedDown = 8
                        },
                        new()
                        {
                            Attempt = 2,
                            PinsKnockedDown = 2
                        }

                    }
                },
                new()
                {
                    PlayerId = _playerId,
                    GameId = _gameId,
                    FrameNumber = 10,
                    Rolls = new()
                    {
                        new()
                        {
                            Attempt = 1,
                            PinsKnockedDown = 10
                        },
                        new()
                        {
                            Attempt = 2,
                            PinsKnockedDown = 1
                        },
                        new()
                        {
                            Attempt = 3,
                            PinsKnockedDown = 7
                        }

                    }
                }
            };
            public Mock<IFrameRepository> GetValidTestFrameRepository(long? playerId = null, long? gameId = null )
            {
                playerId = playerId ?? _playerId;
                gameId = gameId ?? _gameId;
                var result = new Mock<IFrameRepository>();
                result.Setup(fr => fr.GetFramesByPlayerGame(playerId.Value, gameId.Value))
                    .Returns(GetValidTestFrames());
                return result;
            }
            #endregion
            [Fact]
            public void ReturnCorrectScore_WhenRollsAreFound()
            {
                var frameRepository = GetValidTestFrameRepository();
                var scoreService = new ScoringService(frameRepository.Object);
                var result = scoreService.GetPlayerScore(_playerId, _gameId);
                Assert.Equal(110, result.TotalScore);
                Assert.Equal(_playerId, result.PlayerId);
                Assert.Equal(_gameId, result.GameId);
                Assert.Equal(10, result.Frames.Count);

            }
        }

    }
}
