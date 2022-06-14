using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticTree
{
    public interface IGameConsole
    {
        IReadonlyPile<Card> GetCards(CardPlayerState player, PileType pileType);
        CardPlayerState GetPlayer(string tag);
        void AddPressure(string target, int num);
        void AddCard(string target, string name, PileType pileType);
        void AddStatus(string target, string name, int value);
        void AddStrength(string target, int value);
        void Discard(string target, string id);
        void Damage(string target, int value);
        void Draw(string target, int num);
        void SetDrawBan(string target, bool value);
        void OpenGUI(string name, params string[] args);
        void SetGlobalVar(string name, int value);
        void ActivateCard(string id);
        void CopyCard(string target, string id, PileType pileType);
        void ExecuteCard(string target, string id);
    }

    public interface IGameQuery
    {
        List<string> GetCards(string tag, PileType pileType);
    }

    public class GameConsole : IGameConsole
    {
        public IReadonlyPile<Card> GetCards(CardPlayerState player, PileType pileType)
        {
            switch (pileType)
            {
                case PileType.Hand:
                    return player.CardController.Hand;
                case PileType.DrawDeck:
                    return player.CardController.Hand;
                case PileType.DiscardDeck:
                    return player.CardController.Hand;
                case PileType.All:
                    return player.CardController.Hand;
            }
            return null;
        }

        public CardPlayerState GetPlayer(string tag)
        {
            return Context.PlayerContext;
        }

        public void Damage(string target, int value)
        {
            GetPlayer(target).Player.PlayerInfo.Health += value;

        }

        public void SetDrawBan(string target, bool value)
        {
            GetPlayer(target).CardController.DrawBan = value;
        }

        public void Draw(string target, int num)
        {
            GetPlayer(target).CardController.Draw(num);
        }

        public void AddPressure(string target, int num)
        {

        }

        public void AddCard(string target, string name, PileType pileType)
        {
            Card newCard = GameManager.Instance.CardLibrary.GetCopyByName(name);
            GetPlayer(target).CardController.AddCard(PileType.Hand, newCard);
        }

        public void AddStatus(string target, string name, int value)
        {
            GetPlayer(target).StatusManager.AddStatusCounter(name, value);
        }

        public void AddStrength(string target, int value)
        {
            GetPlayer(target).Strength += value;
        }

        public void OpenGUI(string name, params string[] args)
        {
            throw new NotImplementedException();
        }

        public void Discard(string target, string id)
        {
            if (id.Trim().ToLower().Equals("all"))
            {
                List<Card> cards = new List<Card>(GetPlayer(target).CardController.Hand);
                foreach (Card card in cards)
                {
                    GetPlayer(target).CardController.DiscardCard(card);
                }
            }
            throw new NotImplementedException();
        }

        public void SetGlobalVar(string name, int value)
        {
            throw new NotImplementedException();
        }

        public void ActivateCard(string id)
        {
            throw new NotImplementedException();
        }

        public void CopyCard(string target, string id, PileType pileType)
        {
            throw new NotImplementedException();
        }

        public void ExecuteCard(string target, string id)
        {
            throw new NotImplementedException();
        }
    }
}
