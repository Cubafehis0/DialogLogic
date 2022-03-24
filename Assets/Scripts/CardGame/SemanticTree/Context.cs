using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticTree
{
    public static class Context
    {
        private static readonly Stack<CardPlayerState> playerContext = new Stack<CardPlayerState>();
        private static readonly Stack<Card> cardStack = new Stack<Card>();
        private static readonly Stack<List<Card>> pileStack = new Stack<List<Card>>();
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
    }
}
