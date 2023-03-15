using ModdingAPI;
using System;
using UnityEngine;
using System.Reflection;
public partial class GameConsole : Singleton<GameConsole>, IGameConsole
{
    [SerializeField]
    private GameObject player;

    public void AddHealth(string target, int value)
    {
        player.GetComponent<PlayerPacked>().Health += value;
        Debug.Log($"玩家生命增加{value}");
    }

    public void AddMaxHealth(int value)
    {
        player.GetComponent<PlayerPacked>().MaxHealth += value;
        Debug.Log($"玩家最大生命增加{value}");
    }

    public void AddPressure(string target, int value)
    {
        player.GetComponent<PlayerPacked>().Pressure += value;
        Debug.Log($"玩家压力增加{value}");
    }

    public void AddMaxPressure(int value)
    {
        player.GetComponent<PlayerPacked>().MaxPressure += value;
        Debug.Log($"玩家最大生命增加{value}");
    }


    public void AddStatus(string target, string name, int value)
    {
        var status = StaticStatusLibrary.GetByName(name);
        if (status != null)
        {
            player.GetComponent<StatusController>().AddStatusCounter(name, value);
        }
    }

    public void AddStrength(string target, int value)
    {
        throw new NotImplementedException();
    }

    public void Damage(string target, int value)
    {
        player.GetComponent<PlayerPacked>().Health -= value;
    }




    public void ExecuteCard(string target, string id)
    {
        Card card = DynamicCardLibrary.Instance.GetCard(id);
      
        Debug.LogWarning("执行卡牌未实现");
    }


    public void ModifyPersonality(string target, Personality personality, int duration, DMGType type)
    {
        player.GetComponent<PlayerPacked>().Personality += personality;
        Debug.Log("修改人格");
    }

    public void ModifySpeech(string target, SpeechArt type, int duration)
    {
        Debug.LogWarning("修改判定未实现");
        //player.GetComponent<PlayerPacked>().
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

    public void ShufflePile(PileType pileType)
    {
        if (pileType == PileType.DrawDeck)
        {
            player.GetComponent<CardController>().ShuffleDraw();
        }
        else Debug.LogWarning("暂不支持抽牌堆外洗牌");
    }

    public void GainAdditionalTurn()
    {
        Debug.Log("额外回合");
    }

    public void ObtainRelic(string name)
    {
        var relic= StaticRelicLibrary.Instance.GetRelicByName(name);
        player.GetComponent<RelicsController>().AddRelic(relic);
        Debug.Log($"获得遗物{name}");
    }






    public string Execute(string cmd)
    {
        string[] args = cmd.Split(' ');

        if (args.Length > 0)
        {
            Type t = GetType();
            MethodInfo method = t.GetMethod(args[0]);
            if (method != null)
            {
                ParameterInfo[] infos = method.GetParameters();
                if (infos.Length != args.Length - 1)
                {
                    return "参数个数不匹配";
                }


                object[] @params = new object[infos.Length];
                for (int i = 0; i < infos.Length; i++)
                {
                    if (infos[i].ParameterType == typeof(int))
                    {
                        @params[i] = int.Parse(args[i + 1]);
                    }
                    if (infos[i].ParameterType == typeof(string))
                    {
                        @params[i] = args[i + 1];
                    }
                }
                method.Invoke(this, @params);
                return "找到";
            }
        }
        return "失败";
    }

    public static void Print(string s)
    {
        Debug.Log(s);
    }


}


public partial class GameConsole
{
    public void AddCard(string cardName, PileType pileType, PilePositionType pilePositionType)
    {
        player.GetComponent<CardController>().AddNewCard(pileType, cardName, pilePositionType);
        Debug.Log($"加入卡牌{cardName}");
    }
    public void Discard(string target, string id)
    {
        Card card = DynamicCardLibrary.Instance.GetCard(id);
        player.GetComponent<CardController>().DiscardCard(card);
        Debug.Log("丢弃卡牌");
    }

    public void Draw(string target, int num)
    {
        player.GetComponent<CardController>().Draw(num);
        Debug.Log($"抽取{num}张卡牌");
    }

    public void ModifyCost(string target, string id, int duration)
    {
        throw new NotImplementedException();
    }

    public void AddCard(string target, string name, PileType pileType)
    {
        var cardController= player.GetComponent<CardController>();
        var card = StaticCardLibrary.Instance.GetCopyByName(name);
        cardController.AddCard(pileType, card, PilePositionType.Random);
        Debug.Log("加入卡牌");
    }

    public void ActivateCard(string id)
    {
        Card card = DynamicCardLibrary.Instance.GetCard(id);
        card.TemporaryActivate = true;
        Debug.Log($"激活卡牌{card.Name}");
    }

    public void AddCopyCard(string target, string id, PileType pileType)
    {
        var cardController = player.GetComponent<CardController>();
        var card = DynamicCardLibrary.Instance.GetCard(id);
        var newCard = DynamicCardLibrary.Instance.CopyCard(card);
        cardController.AddCard(pileType, newCard, PilePositionType.Random);
        Debug.Log("复制卡牌");
    }
    public void RandomActivateHand()
    {
        var cardController = player.GetComponent<CardController>();
        Debug.LogWarning("随机激活一张手牌未实现");
    }

    public void DiscardAllHand()
    {
        var cardController = player.GetComponent<CardController>();
        cardController.DiscardAll();
        Debug.Log("丢弃所有卡牌");
    }

    public void AddMaxHandNum(int num)
    {
        Debug.LogWarning("增加最大卡牌上限，未实现");
    }

}