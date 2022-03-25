
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using SemanticTree.CardEffect;
namespace SemanticTree.CardEffect
{
    /// <summary>
    /// int参数
    /// </summary>
    public class CardCategory : CardNode, ICondition
    {
        [XmlElement(ElementName = "category")]
        public int Category { get; set; }

        public CardCategory()
        {
            this.Category = 0;
        }
        public bool Value
        {
            get
            {
                return Context.CardContext.info.category == Category;
            }
        }

        public override void Construct()
        {

        }

        public override void Execute()
        {
            //有问题
        }
    }
}