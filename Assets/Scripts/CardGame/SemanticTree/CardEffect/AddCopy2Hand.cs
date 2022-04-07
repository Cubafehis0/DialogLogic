using ExpressionAnalyser;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace SemanticTree.CardEffects
{
    /// <summary>
    /// card,player环境
    /// string参数，Expression数量
    /// </summary>
    public class AddCopy2HandNode : CardEffect
    {
        [XmlElement(ElementName = "num")]
        public string NumExpression { get; set; }
        private IExpression num = null;
        public override void Execute()
        {

            for (int i = 0; i < num.Value && !Context.PlayerContext.cardManager.IsHandFull; i++)
            {
                //有问题
                Card newCard = CardGameManager.Instance.GetCardCopy(Context.CardContext);
                newCard.GetComponent<Image>().SetNativeSize();
                Context.PlayerContext.cardManager.Hand.Add(newCard);
            }
        }
        public override void Construct()
        {
            num = ExpressionParser.AnalayseExpression(NumExpression);
        }
    }
}
