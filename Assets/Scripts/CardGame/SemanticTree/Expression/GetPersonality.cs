using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticTree.Expression
{
    /// <summary>
    /// Expression
    /// </summary>
    public class GetPersonalityNode : IExpression
    {
        private PersonalityType type;

        public GetPersonalityNode(PersonalityType type)
        {
            this.type = type;
        }

        public int Value
        {
            get
            {
                return type switch
                {
                    PersonalityType.Inside => Context.PlayerContext.FinalPersonality.Inner,
                    PersonalityType.Outside => Context.PlayerContext.FinalPersonality.Outside,
                    PersonalityType.Logic => Context.PlayerContext.FinalPersonality.Logic,
                    PersonalityType.Passion => Context.PlayerContext.FinalPersonality.Spritial,
                    PersonalityType.Moral => Context.PlayerContext.FinalPersonality.Moral,
                    PersonalityType.Unethic => Context.PlayerContext.FinalPersonality.Immoral,
                    PersonalityType.Detour => Context.PlayerContext.FinalPersonality.Roundabout,
                    PersonalityType.Strong => Context.PlayerContext.FinalPersonality.Aggressive,
                    _ => 0,
                };
            }
        }
    }
}
