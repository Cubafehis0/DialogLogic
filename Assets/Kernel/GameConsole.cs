using ModdingAPI;
using System;

namespace Kernel
{
    public class GameConsole : IGameConsole
    {
        public void ActivateCard(string id)
        {
            throw new NotImplementedException();
        }

        public void AddCard(string target, string name, ModdingAPI.PileType pileType)
        {
            throw new NotImplementedException();
        }

        public void AddCopyCard(string target, string id, ModdingAPI.PileType pileType)
        {
            throw new NotImplementedException();
        }

        public void AddHealth(string target, int value)
        {
            throw new NotImplementedException();
        }

        public void AddPressure(string target, int num)
        {
            throw new NotImplementedException();
        }

        public void AddStatus(string target, string name, int value)
        {
            throw new NotImplementedException();
        }

        public void AddStrength(string target, int value)
        {
            throw new NotImplementedException();
        }

        public void Damage(string target, int value)
        {
            throw new NotImplementedException();
        }

        public void Discard(string target, string id)
        {
            throw new NotImplementedException();
        }

        public void Draw(string target, int num)
        {
            throw new NotImplementedException();
        }

        public void ExecuteCard(string target, string id)
        {
            throw new NotImplementedException();
        }

        public void ModifyCost(string target, string id, int duration)
        {
            throw new NotImplementedException();
        }

        public void ModifyPersonality(string target, Personality personality, int duration, ModdingAPI.DMGType type)
        {
            throw new NotImplementedException();
        }

        public void ModifySpeech(string target, SpeechArt type, int duration)
        {
            throw new NotImplementedException();
        }

        public void OpenGUI(string name, params object[] args)
        {
            throw new NotImplementedException();
        }

        public string RegisterCostModifier(Func<bool> condition, Func<int> exp)
        {
            throw new NotImplementedException();
        }

        public string RegisterPersonalityModifier(Personality personality)
        {
            throw new NotImplementedException();
        }

        public void RemoveCondition(string slot, PersonalityType speechType)
        {
            throw new NotImplementedException();
        }

        public void RevealAllCondition(string slot)
        {
            throw new NotImplementedException();
        }

        public void RevealCondition(string slot, PersonalityType type)
        {
            throw new NotImplementedException();
        }

        public void RevealRandomConditiion(string slot)
        {
            throw new NotImplementedException();
        }

        public void RevealRandomCondition(string target, SpeechType speechType, int num)
        {
            throw new NotImplementedException();
        }

        public void RevealRandomCondition(string target, int num)
        {
            throw new NotImplementedException();
        }

        public void SetDrawBan(string target, bool value)
        {
            throw new NotImplementedException();
        }

        public void SetFocus(string target, SpeechType speechType, int duration)
        {
            throw new NotImplementedException();
        }

        public void SetGlobalVar(string name, int value)
        {
            throw new NotImplementedException();
        }
    }
}
