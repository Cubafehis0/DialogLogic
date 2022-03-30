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
        private static readonly Stack<Card> cardStack = new Stack<Card>();
        private static readonly Stack<List<Card>> pileStack = new Stack<List<Card>>();
        public static readonly Stack<StatusCounter> statusCounterStack = new Stack<StatusCounter>();
        public static readonly Stack<ChoiceSlot> choiceSlotStack = new Stack<ChoiceSlot>();
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
                List<string> t = name.Split('.').ToList();
                if (t.Count == 2 && t[0].Equals("status"))
                {
                    return PlayerContext.StatusManager.GetStatusValue(t[1]);
                }
                if (t.Count == 2 && t[0].Equals("recorder"))
                {
                    return t[1] switch
                    {
                        "preach_total" =>
                        (from x in CardRecorder.Instance.cardLogs
                         where x.Name == name
                         && x.LogType == CardLogEntryEnum.PlayCard
                         select x).Count(),
                        "preach_thisturn" =>
                        (from x in CardRecorder.Instance.cardLogs
                        where x.Name == name
                        && x.LogType == CardLogEntryEnum.PlayCard
                        && x.Turn==CardGameManager.Instance.turn
                        select x).Count(),
                        _ => throw new SemanticException()
                    };
                }
                return name switch
                {
                    "inner" => PlayerContext.FinalPersonality.Inner,
                    "outside" => PlayerContext.FinalPersonality.Outside,
                    "logic" => PlayerContext.FinalPersonality.Logic,
                    "spritial" => PlayerContext.FinalPersonality.Spritial,
                    "moral" => PlayerContext.FinalPersonality.Moral,
                    "immoral" => PlayerContext.FinalPersonality.Immoral,
                    "roundabout" => PlayerContext.FinalPersonality.Roundabout,
                    "aggressive" => PlayerContext.FinalPersonality.Aggressive,
                    "hand_count" => PlayerContext.Hand.Count,
                    "draw_count" => PlayerContext.DrawPile.Count,
                    "discard_count" => PlayerContext.DiscardPile.Count,
                    _ => throw new SemanticException()
                };
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
