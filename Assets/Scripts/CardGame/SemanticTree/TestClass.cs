using SemanticTree.PlayerEffect;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace SemanticTree
{
    public class TestClass : MonoBehaviour
    {
        private void Start()
        {
            XmlSerializer ser = new XmlSerializer(typeof(EffectList));
            EffectList effect = new ModifyPersonality()
            {
                Modifier = new Personality(0, 1, 2, 3),
                Timer = 2
            };
            effect.Add(new AddCard2Hand()
            {
                NumExpression = "1",
                CardName = "说教"
            });
            TextWriter writer = new StreamWriter("test.xml");
            ser.Serialize(writer, effect);
            writer.Close();
        }
    }
}
