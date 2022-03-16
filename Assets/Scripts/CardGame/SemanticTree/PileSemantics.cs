using System.Collections;
using UnityEngine;
using SemanticTree.CardSemantics;
using System;
using System.Collections.Generic;

namespace SemanticTree
{
    namespace PileSemantics
    {
        public class PileNode
        {
            private static readonly Stack<List<Card>> pileStack = new Stack<List<Card>>();
            protected static List<Card> PileContext
            {
                get
                {
                    if (pileStack.Count == 0) throw new SemanticException();
                    return pileStack.Peek();
                }
            }
            public static void PushCardContext(List<Card> pile)
            {
                pileStack.Push(pile);
            }
            public static void PopCardContext()
            {
                pileStack.Pop();
            }
        }
        public class CountCardNode : PileNode,IExpressionNode
        {
            private readonly IConditionNode condition;

            public CountCardNode(IConditionNode condition)
            {
                this.condition = condition;
            }

            public int Value
            {
                get
                {
                    int ret = 0;
                    foreach (Card card in PileContext)
                    {
                        CardNode.PushCardContext(card);
                        if (condition?.Value ?? true) ret++;
                        CardNode.PopCardContext();
                    }
                    return ret;
                }
            }
        }
        public class AnyCardNode : PileNode, IConditionNode
        {
            private readonly IConditionNode condition = null;

            public AnyCardNode(IConditionNode condition)
            {
                this.condition = condition ?? throw new ArgumentNullException(nameof(condition));
            }

            public bool Value
            {
                get
                {
                    foreach (Card card in PileContext)
                    {
                        CardNode.PushCardContext(card);
                        if (condition.Value) return true;
                        CardNode.PopCardContext();
                    }
                    return false;
                }
            }
        }
        public class AllCardNode : PileNode, IConditionNode
        {
            private readonly IConditionNode condition;

            public AllCardNode(IConditionNode condition)
            {
                this.condition = condition ?? throw new System.ArgumentNullException(nameof(condition));
            }

            public bool Value
            {
                get
                {
                    foreach (Card card in PileContext)
                    {
                        CardNode.PushCardContext(card);
                        if (!condition.Value) return false;
                        CardNode.PopCardContext();
                    }
                    return true;
                }
            }
        }
        public class GUISelectCardNode : PileNode, IEffectNode
        {
            private IEffectNode action;
            private int num;

            public GUISelectCardNode(int num, IEffectNode action)
            {
                this.num = num;
                this.action = action;
            }

            public void Execute()
            {
                CardGameManager.Instance.OpenCardChoosePanel(PileContext, num, action);
            }
        }

        public class RandomCardNode : PileNode, IEffectNode
        {
            private int num;
            private IEffectNode action;

            public RandomCardNode(int num, IEffectNode action)
            {
                this.num = num;
                this.action = action;
            }

            public void Execute()
            {
                List<Card> tmp = new List<Card>(PileContext);
                MyMath.Shuffle(tmp);
                PushCardContext(tmp.GetRange(0, num));
                action.Execute();
                PopCardContext();
            }
        }
        public class SelectCardNode : PileNode, IEffectNode
        {
            private readonly IConditionNode condition;
            private readonly int num;
            private readonly IEffectNode action;

            public SelectCardNode(IConditionNode condition, int num, IEffectNode action)
            {
                this.condition = condition;
                this.num = num;
                this.action = action;
            }

            public void Execute()
            {
                List<Card> res = new List<Card>();
                foreach (Card card in PileContext)
                {
                    CardNode.PushCardContext(card);
                    if (condition.Value) res.Add(card);
                    CardNode.PopCardContext();
                }
                CardGameManager.Instance.OpenPileChoosePanel(res, num, action);
                //禁用输入
            }
        }
        public class ForeachCardNode : PileNode, IEffectNode
        {
            private IConditionNode condition = null;
            private IEffectNode action = null;

            public ForeachCardNode(IConditionNode condition, IEffectNode action)
            {
                this.condition = condition;
                this.action = action;
            }

            public void Execute()
            {
                foreach (Card card in PileContext)
                {
                    CardNode.PushCardContext(card);
                    if (condition?.Value ?? true) action?.Execute();
                    CardNode.PopCardContext();
                }
            }
        }
    }
}