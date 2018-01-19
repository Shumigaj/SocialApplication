using System;
using System.Collections.Generic;
using SocialApplication.Core.Models;

namespace SocialApplication.UnitTests.Content
{
    internal class NewsCollection
    {
        public IList<News> Data { get; }

        public NewsCollection()
        {
            Data = new List<News>
            {
                new News
                {
                    Id = 0,
                    Title = "How your future office will look",
                    Text = "Offices are changing — again. While typing pools of the mid 20th century were eventually replaced by cubicles to create more privacy, the open office made a return in the 1990s, being considered more conducive for the collaborative work in creative environments. Now things are changing again, with new technology, light, sound and a focus on communal spaces that keep workers happy. LinkedIn spoke to an office design expert about what big changes could be coming to your workplace and why coworking spaces will be increasingly common. Join the conversation.",
                    AllowComments = true,
                    CreatedAtUtc = new DateTime(2017, 1, 16),
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            //Cam D. B.
                            Id = 0,
                            Text = "Hopefully we go back to cubicles...",
                            CreatedAtUtc = new DateTime(2017, 1, 16)
                        },
                        new Comment
                        {
                            //Anthony H.
                            Id = 1,
                            Text = "Home office with VR collaboration capability. ",
                            CreatedAtUtc = new DateTime(2017, 1, 17)
                        }
                    }
                },
                new News
                {
                    Id = 1,
                    Title = "IBM may be prepping for massive changes at Global Technology Services group",
                    Text = "IBM has been a company adrift for the last several years with 22 straight quarters of declining revenue. Against that backdrop, The Register published an article yesterday suggesting there could be massive changes afoot for the company’s Global Technology Services group.",
                    AllowComments = true,
                    CreatedAtUtc = new DateTime(2017, 1, 2),
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            //Cliff Jolly 
                            Id = 0,
                            Text = "Theirs no reason to be so mean about it. There just trying to find they're market niche, like everyone else.",
                            CreatedAtUtc = new DateTime(2017, 1, 2)
                        },
                        new Comment
                        {
                            //Fred Farkash
                            Id = 1,
                            Text = "...this a change the company has to make.",
                            CreatedAtUtc = new DateTime(2017, 1, 3)
                        }
                    }
                },
                new News
                {
                    Id = 2,
                    Title = "Is This the BEST 9 Minute Speech ... Ever? ",
                    Text = "We all knew we were watching something special unfold right in front of us. We witnessed a sea change in action. It was eloquence at its best. Oratory for a cause. Here are just a few reasons that nine minute (!) talk was so masterful, so effective. ",
                    AllowComments = true,
                    CreatedAtUtc = new DateTime(2017, 1, 10),
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            //Paul T.
                            Id = 0,
                            Text = "A good analysis, however It was tactical for the audience, taking advantage of the air time.",
                            CreatedAtUtc = new DateTime(2017, 1, 10)
                        }
                    }
                },
                new News
                {
                    Id = 3,
                    Title = "Forget algorithms. The future of AI is hardware!",
                    Text = "The hype about Artificial Intelligence is all about the algorithms. Deep Mind, the Google company that is leading the world in machine learning, recently published an article where it described how AlphaGo Zero managed to become - all by itself and from scratch - a master in Go and beat all previous versions of itself, using an advanced from of reinforcement learning algorithms. But while companies and organizations elbow each other in their quest for top talent in algorithmic design and data science, the real news are coming not from the worlds of bits but from the world of wires, silicon and electronics : hardware is back!",
                    AllowComments = false,
                    CreatedAtUtc = new DateTime(2017, 1, 12)
                }
            };
        }
    }
}
