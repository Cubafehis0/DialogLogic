using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.Condition
{
    public class IsCardName : ComplexCondition
    {
        [XmlElement(ElementName = "name", IsNullable = false)]
        public string Name;
        public override bool Value
        {
            get
            {
                return Context.CardContext.info.Name.Trim().ToLower() == Name.Trim().ToLower();
            }
        }
    }
}
