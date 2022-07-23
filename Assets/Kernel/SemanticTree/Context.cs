using ExpressionAnalyser;
using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Recorder;

namespace SemanticTree
{
    public class Context : IVariableAdapter
    {
        private static readonly Stack<CardPlayerState> playerContext = new Stack<CardPlayerState>();
        public static CardPlayerState Target;
        private static readonly Stack<Card> cardStack = new Stack<Card>();
        private static readonly Stack<List<Card>> pileStack = new Stack<List<Card>>();
        public static readonly Stack<StatusCounter> statusCounterStack = new Stack<StatusCounter>();
        public static readonly Stack<ChoiceSlot> choiceSlotStack = new Stack<ChoiceSlot>();
        public static readonly Dictionary<string, int> variableTable = new Dictionary<string, int>();
        public static List<Card> PileContext
        {
            get
            {
                if (pileStack.Count == 0) throw new SemanticException();
                return pileStack.Peek();
            }
        }
        public static void PushPileContext(List<Card> pile)
        {
            pileStack.Push(pile);
        }
        public static void PopPileContext()
        {
            pileStack.Pop();
        }
        public static Card CardContext
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
        public static CardPlayerState PlayerContext
        {
            get
            {
                if (playerContext.Count == 0) throw new SemanticException();
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

        public int this[string name]
        {
            get
            {
                if(variableTable.ContainsKey(name)) return variableTable[name];
                List<string> t = name.Split('.').ToList();
                if (t.Count==2 && t[0].Equals("target"))
                {
                    return Target.GetPlayerProp(t[1]);
                }
                else if (t.Count == 2 && t[0].Equals("card"))
                {

                }
                else if (t.Count == 1)
                {
                    return PlayerContext.GetPlayerProp(t[0]);
                }
                throw new SemanticException();
            }
        }

        public bool Contains(string name)
        {
            try
            {
                var t = this[name];
                return true;
            }
            catch (SemanticException)
            {
                return false;
            }
        }
    }
}
