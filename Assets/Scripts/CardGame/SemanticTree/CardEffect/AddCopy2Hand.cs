using ExpressionAnalyser;
using System.Xml;
using System.Xml.Serialization;

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

            for (int i = 0; i < num.Value && !Context.PlayerContext.IsHandFull; i++)
            {
                Card newCard = CardGameManager.Instance.GetCardCopy(Context.CardContext);
                Context.PlayerContext.Hand.Add(newCard);
            }
        }
        public override void Construct()
        {
            num = ExpressionParser.AnalayseExpression(NumExpression);
        }
    }
}
