using SemanticTree.PlayerSemantics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SemanticTree
{
    namespace CardSemantics
    {
        public class CardNode
        {
            private static readonly Stack<Card> cardStack = new Stack<Card>();
            protected static Card CardContext
            {
                get
                {
                    if (cardStack.Count == 0) throw new SemanticException();
                    return cardStack.Peek();
                }
            }
            public static void PushCardContext(Card card)
            {
                cardStack.Push(card);
            }
            public static void PopCardContext()
            {
                cardStack.Pop();
            }
        }


        public class CardCategoryNode : CardNode, IConditionNode
        {
            private readonly int category;

            public CardCategoryNode(int category)
            {
                this.category = category;
            }

            public bool Value
            {
                get
                {
                    return CardContext.info.category == category;
                }
            }
        }
        public class ExecuteCurrentCardNode : CardNode, IEffectNode
        {
            public void Execute()
            {
                //CardContext.effectNode.Execute();
            }
        }

        public class DiscardNode : CardNode, IEffectNode
        {
            public void Execute()
            {
                PlayerNode.PlayerContext.DiscardCard(CardContext);
            }
        }

        public class ActivateTemporaryNode : CardNode, IEffectNode
        {
            public void Execute()
            {
                CardContext.TemporaryActivate = true;
            }
        }

        public class ActivatePermanentNode : CardNode, IEffectNode
        {
            public void Execute()
            {
                CardContext.PermanentActivate = true;
            }
        }
        public class AddCopy2HandNode : CardNode, IEffectNode
        {
            private int num = 1;
            public void Execute()
            {
                for (int i = 0; i < num && !PlayerNode.PlayerContext.IsHandFull; i++)
                {
                    Card newCard = CardGameManager.Instance.EmptyCard;
                    //有缺陷，这里是原始复制
                    newCard.Construct(CardContext.info);
                    PlayerNode.PlayerContext.Hand.Add(newCard);
                }
            }
        }
    }
}