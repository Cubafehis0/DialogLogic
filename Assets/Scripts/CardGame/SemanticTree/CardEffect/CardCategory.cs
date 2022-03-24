
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using SemanticTree.CardEffect;
using XmlParser;
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
        public CardCategory(int category)
        {
            this.Category = category;
        }

        public bool Value
        {
            get
            {
                return Context.CardContext.Category == Category;
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