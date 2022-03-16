using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SemanticTree.CardSemantics;
using SemanticTree.PileSemantics;
using SemanticTree.PlayerSemantics;

namespace SemanticTree
{
    public interface IExpressionNode
    {
        int Value { get; }
    }

    public interface IConditionNode
    {
        bool Value { get; }
    }

    public interface IEffectNode
    {
        void Execute();
    }



    [Serializable]
    public class SemanticException : Exception
    {
        public SemanticException() { }
        public SemanticException(string message) : base(message) { }
        public SemanticException(string message, Exception inner) : base(message, inner) { }
        protected SemanticException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class SemanticTreeClass
    {
        private static readonly Dictionary<string, int> variableDictionary = new Dictionary<string, int>();

        private enum CompareType
        {
            Greater,
            Less,
            Equal,
            GreaterEqual,
            LessEqual,
            NotEqual
        }

        private enum OperationType
        {
            Add,
            Sub
        }

        private class CompareNode : IConditionNode
        {
            private readonly IExpressionNode left;
            private readonly IExpressionNode right;
            private readonly CompareType compareType;

            public CompareNode(IExpressionNode left, IExpressionNode right, CompareType compareType)
            {
                this.left = left;
                this.right = right;
                this.compareType = compareType;
            }

            public bool Value
            {
                get
                {
                    return compareType switch
                    {
                        CompareType.Greater => left.Value > right.Value,
                        CompareType.Less => left.Value < right.Value,
                        CompareType.Equal => left.Value == right.Value,
                        CompareType.GreaterEqual => left.Value >= right.Value,
                        CompareType.LessEqual => left.Value <= right.Value,
                        CompareType.NotEqual => left.Value != right.Value,
                        _ => false,
                    };
                }
            }
        }

        private class OppositeNode : IExpressionNode
        {
            private readonly IExpressionNode exp;

            public OppositeNode(IExpressionNode exp)
            {
                this.exp = exp;
            }

            public int Value => -exp.Value;
        }

        private class OperationNode : IExpressionNode
        {
            private readonly IExpressionNode left;
            private readonly IExpressionNode right;
            private readonly OperationType operationType;

            public OperationNode(IExpressionNode left, IExpressionNode right, OperationType operationType)
            {
                this.left = left;
                this.right = right;
                this.operationType = operationType;
            }

            public int Value
            {
                get
                {
                    return operationType switch
                    {
                        OperationType.Add => left.Value + right.Value,
                        OperationType.Sub => left.Value - right.Value,
                        _ => 0,
                    };
                }
            }
        }

        private class ConstNode : IExpressionNode
        {
            public ConstNode(int value)
            {
                Value = value;
            }

            public int Value { get; set; }
        }

        private class GetVariableNode : IExpressionNode
        {
            private readonly string name;
            public GetVariableNode(string name)
            {
                this.name = name;
            }
            public int Value
            {
                get
                {
                    return variableDictionary.ContainsKey(name) ? variableDictionary[name] : 0;
                }
            }
        }
        private class SetVariableNode : IEffectNode
        {
            private readonly string name;
            private readonly IExpressionNode exp;

            public SetVariableNode(string name, IExpressionNode exp)
            {
                this.name = name;
                this.exp = exp;
            }

            public void Execute()
            {
                if (variableDictionary.ContainsKey(name))
                {
                    variableDictionary[name] = exp.Value;
                }
                else
                {
                    variableDictionary.Add(name, exp.Value);
                }
            }
        }
        private class EffectSequenceNode : IEffectNode
        {
            private readonly IEffectNode a;
            private readonly IEffectNode b;

            public EffectSequenceNode(IEffectNode a, IEffectNode b)
            {
                this.a = a ?? throw new ArgumentNullException(nameof(a));
                this.b = b ?? throw new ArgumentNullException(nameof(b));
            }

            public void Execute()
            {
                a.Execute();
                b.Execute();
            }
        }
        private class GUISelectHandNode : IEffectNode
        {
            private readonly IConditionNode condition;
            private readonly IEffectNode action;
            private readonly int num;

            public GUISelectHandNode(IConditionNode condition, int num, IEffectNode action)
            {
                this.condition = condition;
                this.num = num;
                this.action = action;
            }

            public void Execute()
            {
                CardGameManager.Instance.OpenHandChoosePanel(condition, num, action);
            }
        }
        private class SetPileContext : IEffectNode
        {
            private readonly IEffectNode effect;
            private readonly List<Card> cards;

            public SetPileContext(IEffectNode effect, List<Card> cards)
            {
                this.effect = effect;
                this.cards = cards;
            }

            public void Execute()
            {
                PileNode.PushCardContext(cards);
                effect.Execute();
                PileNode.PopCardContext();
            }
        }
        private class IfNode : IEffectNode
        {
            private readonly IConditionNode condition;
            private readonly IEffectNode action;

            public IfNode(IConditionNode condition, IEffectNode action)
            {
                this.condition = condition ?? throw new ArgumentNullException(nameof(condition));
                this.action = action ?? throw new ArgumentNullException(nameof(action));
            }

            public void Execute()
            {
                if (condition.Value) action.Execute();
            }
        }

        


        //"测试立刻加逻辑"
        public static IEffectNode TestAddLogicEffectNode
        {
            get
            {
                IEffectNode ret = new ModifyPersonalityNode(new Personality(0, 2, 0, 0), 2);
                return ret;
            }
        }

        //"测试逻辑+2" 
        public static IEffectNode TestAddLogic2Node
        {
            get
            {
                IEffectNode ret = new ModifyPersonalityNode(new Personality(0, 2, 0, 0), null);
                return ret;
            }
        }
        //"测试道德+2" 
        public static IEffectNode TestAddMoral2Node
        {
            get
            {
                IEffectNode ret = new ModifyPersonalityNode(new Personality(0, 0, 2, 0), null);
                return ret;
            }
        }
        //"测试迂回+2"
        public static IEffectNode TestAddDetour2Node
        {
            get
            {
                IEffectNode ret = new ModifyPersonalityNode(new Personality(0, 0, 0, 2), null);
                return ret;
            }
        }

        //"测试选择+2"
        public static IEffectNode TestSelect
        {
            get
            {
                var options = StaticCardLibrary.Instance.cardObjects.FindAll(c => c.info.title.StartsWith("选项"));
                return new SetPileContext(new GUISelectCardNode(1, new ExecuteCurrentCardNode()), options);
            }
        }

        //"测试持续逻辑+1"
        public static IEffectNode TestLgcPlusThreeRound
        {
            get
            {
                return new ModifyPersonalityNode(new Personality(0, 1, 0, 0), 3);
            }
        }

        //"测试抽牌1"
        public static IEffectNode TestDraw
        {
            get
            {
                return new DrawNode(1);
            }
        }

        //"测试向手牌增加1张“说教“卡牌"
        public static IEffectNode TestAddPreach
        {
            get
            {
                return new AddCard2Hand("说教", new ConstNode(1));
            }
        }

        //"测试【丢弃所有】向手牌中增加弃牌数量的【说教】"
        public static IEffectNode TestReconstruction
        {
            get
            {
                IExpressionNode a = new GetPileExpressionNode(new CountCardNode(null), PileType.Hand);
                IEffectNode b = new SetVariableNode("Reconstruct", a);
                IEffectNode c = new DiscardAllHandNode();
                IEffectNode d = new AddCard2Hand("说教", new GetVariableNode("Reconstruct"));
                return new EffectSequenceNode(b, new EffectSequenceNode(c, d));
            }
        }

        //测试随机【激活】1张手牌（无视条件且不消费行动点）
        public static IEffectNode TestRandomActive
        {
            get
            {
                return new GetPileNode(PileType.Hand, new RandomCardNode(1, new ForeachCardNode(null, new ActivateTemporaryNode())));
            }
        }

        //"测试【激活】全部手牌"
        public static IEffectNode TestAllHandActive
        {
            get
            {
                return new GetPileNode(PileType.Hand, new ForeachCardNode(null, new ActivateTemporaryNode()));
            }
        }


        //测试【本回合】【下一次】【对策】不消耗【行动点】
        private static CostModifier freeCardModifier = null;
        public static CostModifier FreeCardModifier
        {
            get
            {
                if (freeCardModifier == null)
                {
                    IExpressionNode modifier = new ConstNode(0);
                    IConditionNode con = new CardCategoryNode(0);
                    freeCardModifier = new CostModifier() { condition = con, exp = modifier };
                }
                return freeCardModifier;
            }
        }

        public static IEffectNode FreeCardOnAdd
        {
            get
            {
                return new AddCostModifierNode(FreeCardModifier, null);
            }
        }

        public static IEffectNode FreeCardOnRemove
        {
            get
            {
                return new RemoveCostModifierNode(FreeCardModifier);
            }
        }

        public static IEffectNode FreeCardOnAfterPlayCard
        {
            get
            {
                IConditionNode con = new CardCategoryNode(0);
                IEffectNode action = new AddStatusNode(Status.FreeCard, -1);
                return new IfNode(con, action);
            }
        }

        public static IEffectNode TestFreeCardNode
        {
            get
            {
                return new AddStatusNode(Status.FreeCard, 1);
            }
        }

        //测试【丢弃：1】
        public static IEffectNode TestDropCard
        {
            get
            {
                return new GUISelectHandNode(null, 1, new DiscardNode());
            }
        }

        //测试从【卡组】中【选择：1】复制加入【手牌】
        public static IEffectNode TestChooseDraw
        {
            get
            {
                return new GetPileNode(PileType.All, new GUISelectCardNode(1, new AddCopy2HandNode()));
            }
        }

        //测试从【手牌】中【选择：1】【激活】
        public static IEffectNode TestChooseActive
        {
            get
            {
                return new GUISelectHandNode(null, 1, new ActivateTemporaryNode());
            }
        }

        //测试从【抽牌堆】中【选择：1】【永久激活】
        public static IEffectNode TestChooseDeckActiveForever
        {
            get
            {
                return new GetPileNode(PileType.DrawDeck, new GUISelectCardNode(1, new ActivatePermanentNode()));
            }
        }

        //测试【本回合】，【说服判定】增加3
        public static IEffectNode TestPersuadPlus
        {
            get
            {
                return new ModifySpeechNode(new SpeechArt(0, 0, 0, 1), 3);
            }
        }

        //测试本回合不能再抽取手牌
        public static IEffectNode TestSetDrawBanNode(bool value)
        {
            return new SetDrawBan(value);
        }

        public static IEffectNode TestSetDrawBan
        {
            get
            {
                return new AddStatusNode(Status.DrawBan, 1);
            }
        }

        //【下一次】说服判定增加打出过的【说教】的数量除以2（取整）的判定值

        //测试每打出一张【说教】牌，增加一点外感
        //有缺陷，不可叠加
        private static IEffectNode outPlusByPreachNode = null;
        public static IEffectNode TestOutPlusByPreachNode
        {
            get
            {
                if (outPlusByPreachNode == null)
                {
                    var c = new CardCategoryNode(0);
                    //new GetStatusCounterValue("OutPlusByPreach")
                    var e = new ModifyPersonalityNode(new Personality(1, 0, 0, 0), null);
                    outPlusByPreachNode = new IfNode(c, e);
                }
                return outPlusByPreachNode;
            }
        }

        //测试【持续】回合开始时，获得一张【说教】牌
        public static IEffectNode TestAddPreachEveryRoundOnTurnStart
        {
            get
            {
                return new AddCard2Hand("说教", new GetStatusCounterValue("addPreachEveryRound"));
            }
        }
        public static IEffectNode TestAddPreachEveryRound
        {
            get
            {
                return new AddStatusNode(Status.AddPreachEveryRound, 1);
            }
        }

        //测试【持续】回合开始时，每持有一点外感便随机揭示x个判定
        public static IEffectNode TestRevealByOutOnTurnStart
        {
            get
            {
                return new RandomRevealNode(null, new GetPersonalityNode(PersonalityType.Outside));
            }
        }
        public static IEffectNode TestRevealByOut
        {
            get
            {
                return new AddStatusNode(Status.RevealByOut, 1);
            }
        }
    }
}
