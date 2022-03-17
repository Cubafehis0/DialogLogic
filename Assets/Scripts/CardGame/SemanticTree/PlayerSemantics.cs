using SemanticTree.PileSemantics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SemanticTree
{
    namespace PlayerSemantics
    {
        public class PlayerNode
        {
            private static readonly Stack<CardPlayerState> playerContext=new Stack<CardPlayerState>();

            public static CardPlayerState PlayerContext
            {
                get
                {
                    if (playerContext.Count==0) throw new SemanticException();
                    return playerContext.Peek();
                }
            }
            public static void PushPlayerContext(CardPlayerState player)
            {
                playerContext.Push(player);
            }
            public static void PopPlayerContext()
            {
                if (playerContext.Count == 0) throw new SemanticException();
                playerContext.Pop();
            }
        }

        public class GetPersonalityNode : PlayerNode,IExpressionNode
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
                        PersonalityType.Inside => PlayerContext.FinalPersonality.Inner,
                        PersonalityType.Outside => PlayerContext.FinalPersonality.Outside,
                        PersonalityType.Logic => PlayerContext.FinalPersonality.Logic,
                        PersonalityType.Passion => PlayerContext.FinalPersonality.Spritial,
                        PersonalityType.Moral => PlayerContext.FinalPersonality.Moral,
                        PersonalityType.Unethic => PlayerContext.FinalPersonality.Immoral,
                        PersonalityType.Detour => PlayerContext.FinalPersonality.Roundabout,
                        PersonalityType.Strong => PlayerContext.FinalPersonality.Aggressive,
                        _ => 0,
                    };
                }
            }
        }
        public class ModifyPersonalityNode : PlayerNode, IEffectNode
        {
            private Personality modifier;
            private int? timer;

            public ModifyPersonalityNode(Personality modifier, int? timer)
            {
                this.modifier = modifier;
                this.timer = timer;
            }

            public void Execute()
            {
                PlayerContext.AddPersonalityModifer(modifier, timer);
            }
        }


        public class ModifySpeechNode : PlayerNode, IEffectNode
        {
            private SpeechArt modifier;
            private int? timer;

            public ModifySpeechNode(SpeechArt modifier, int? timer)
            {
                this.modifier = modifier;
                this.timer = timer;
            }

            public void Execute()
            {
                PlayerContext.AddSpeechModifer(modifier, timer);
            }
        }

        public class ModifyFocusNode : PlayerNode, IEffectNode
        {
            private SpeechType modifier;
            private int? timer;

            public ModifyFocusNode(SpeechType modifier, int? timer)
            {
                this.modifier = modifier;
                this.timer = timer;
            }

            public void Execute()
            {
                PlayerContext.AddFocusModifer(modifier, timer);
            }
        }

        public class GetPileNode : PlayerNode, IEffectNode
        {
            private int type = 0;
            private IEffectNode effect;


            public GetPileNode(int type, IEffectNode effect)
            {
                this.effect = effect;
                this.type = type;
            }

            public void Execute()
            {
                List<Card> ret = new List<Card>();
                if ((PileType.Hand & type) > 0) ret.AddRange(PlayerContext.Hand);
                if ((PileType.DrawDeck & type) > 0) ret.AddRange(PlayerContext.DrawPile);
                if ((PileType.DiscardDeck & type) > 0) ret.AddRange(PlayerContext.DiscardPile);
                PileNode.PushCardContext(ret);
                effect.Execute();
                PileNode.PopCardContext();
            }
        }

        public class GetPileExpressionNode : PlayerNode, IExpressionNode
        {
            private IExpressionNode effect;
            private int type = 0;

            public GetPileExpressionNode(IExpressionNode effect, int type)
            {
                this.effect = effect;
                this.type = type;
            }

            public int Value
            {
                get
                {
                    List<Card> ret = new List<Card>();
                    if ((PileType.Hand & type) > 0) ret.AddRange(PlayerContext.Hand);
                    if ((PileType.DrawDeck & type) > 0) ret.AddRange(PlayerContext.DrawPile);
                    if ((PileType.DiscardDeck & type) > 0) ret.AddRange(PlayerContext.DiscardPile);
                    PileNode.PushCardContext(ret);
                    int res = effect.Value;
                    PileNode.PopCardContext();
                    return res;
                }
            }
        }
        public class DiscardAllHandNode : PlayerNode, IEffectNode
        {
            public void Execute()
            {
                List<Card> cards = new List<Card>(PlayerContext.Hand);
                foreach (Card card in cards)
                {
                    PlayerContext.DiscardCard(card);
                }
            }
        }



        public class AddStatusNode : PlayerNode, IEffectNode
        {
            private Status status = null;
            private int value = 0;

            public AddStatusNode(Status status, int value)
            {
                this.status = status;
                this.value = value;
            }

            public void Execute()
            {
                PlayerContext.StatusManager.AddStatusCounter(PlayerContext, status, value);
            }
        }


        public class DrawNode : PlayerNode, IEffectNode
        {
            private readonly int num;

            public DrawNode(int num)
            {
                this.num = num;
            }

            public void Execute()
            {
                PlayerContext.Draw((uint)num);
            }
        }

        public class AddCard2Hand : PlayerNode, IEffectNode
        {
            private IExpressionNode num;
            private CardInfo prefab;

            public AddCard2Hand(CardInfo prefab, IExpressionNode num)
            {
                this.num = num;
                this.prefab = prefab;
            }

            public AddCard2Hand(string prefabName, IExpressionNode num)
            {
                this.num = num;
                this.prefab = StaticCardLibrary.Instance.GetCardByName(prefabName);
            }

            public void Execute()
            {
                int b = num.Value;
                for (int i = 0; i < b && !PlayerContext.IsHandFull; i++)
                {
                    Card newCard = CardGameManager.Instance.EmptyCard;
                    newCard.Construct(prefab);
                    PlayerContext.Hand.Add(newCard);
                }
            }
        }

        public class GetStatusCounterValue : PlayerNode, IExpressionNode
        {
            private string name;

            public GetStatusCounterValue(string name)
            {
                this.name = name;
            }

            public int Value
            {
                get => PlayerContext.StatusManager.GetStatusValue(name);
            }
        }

        public class SetDrawBan : PlayerNode, IEffectNode
        {
            private bool value = true;

            public SetDrawBan(bool value)
            {
                this.value = value;
            }

            public void Execute()
            {
                PlayerContext.DrawBan = value;
            }
        }
        public class AddCostModifierNode : PlayerNode, IEffectNode
        {
            private CostModifier modifier = null;
            private int? cd = null;

            public AddCostModifierNode(CostModifier modifier, int? cd)
            {
                this.modifier = modifier;
                this.cd = cd;
            }

            public void Execute()
            {
                PlayerContext.AddCostModifer(modifier, cd);
            }
        }

        public class RemoveCostModifierNode : PlayerNode, IEffectNode
        {
            private CostModifier modifier = null;

            public RemoveCostModifierNode(CostModifier modifier)
            {
                this.modifier = modifier;
            }

            public void Execute()
            {
                PlayerContext.RemoveCostModifer(modifier);
            }
        }

        public class RandomRevealNode : PlayerNode, IEffectNode
        {
            private readonly SpeechType? speechType;
            private readonly IExpressionNode exp;

            public RandomRevealNode(SpeechType? speechType, IExpressionNode exp)
            {
                this.speechType = speechType;
                this.exp = exp;
            }

            public void Execute()
            {
                if (speechType == null) PlayerContext.RandomReveal(exp.Value);
                else PlayerContext.RandomReveal(speechType.Value, exp.Value);
            }
        }
    }
}