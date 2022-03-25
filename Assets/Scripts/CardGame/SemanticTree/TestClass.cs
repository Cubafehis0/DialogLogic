using SemanticTree.Condition;
using SemanticTree.PlayerEffect;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace SemanticTree
{
    public class TestClass : MonoBehaviour
    {
        [SerializeField]
        private TextAsset xmlFile;

        private void Start()
        {
            SerializeCondition();
        }

        private void Serialize()
        {
            XmlSerializer ser = new XmlSerializer(typeof(CardInfo));
            CardInfo effect = new CardInfo()
            {
                Name = "english name",
                Title = "Card样例",
                ConditionDesc = "限制样例",
                EffectDesc = "描述样例",
                Meme = "尾巴样例",
                BaseCost = 1,
                category = 0,
                Effects = new EffectList()
                {
                    effects = new List<Effect>()
                    {
                        new ModifyPersonality()
                        {
                            Modifier = new Personality(0, 0, 1, 0),
                        },
                        new ModifyPersonality()
                        {
                            Modifier = new Personality(0, 1, 0, 0),
                        },
                    }
                },

                Personality = new Personality(0, 2, 0, 0),
            };

            TextWriter writer = new StreamWriter("Assets/Common/CardTemplate.xml");
            ser.Serialize(writer, effect);
            writer.Close();
        }

        private void SerializeCondition()
        {
            XmlSerializer ser = new XmlSerializer(typeof(ConditionList));
            ConditionList effect = new AllNode()
            {
                conditions = new List<object>()
                {
                    "r1",
                    "r2",
                    new NoneNode()
                    {
                        conditions =new List<object>()
                        {
                                "r3",
                                "r4",
                            }
                        }
                    }
            };

            TextWriter writer = new StreamWriter("Assets/Common/ConditionTemplate.xml");
            ser.Serialize(writer, effect);
            writer.Close();
        }
        private void Deserialize<T>()
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using var stream = StreamString2Stream(xmlFile.text);
            T res = (T)ser.Deserialize(stream);
            Debug.Log(res);
        }

        public static Stream StreamString2Stream(string s)
        {
            MemoryStream stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

    }
}
