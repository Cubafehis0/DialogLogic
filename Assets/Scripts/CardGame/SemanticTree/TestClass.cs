﻿using SemanticTree.Condition;
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
            SerializePersonality();
            //SerializeCondition();
        }

        public void Fun()
        {
            Debug.Log("test fun");
        }

        private void SerializePersonality()
        {
            XmlSerializer ser = new XmlSerializer(typeof(Personality));

            var Personality = new Personality(0, 0, 0, 0);

            TextWriter writer = new StreamWriter("Assets/Common/Pers.xml");
            ser.Serialize(writer, Personality);
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
        public static T Deserialize<T>(string s)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using var stream = StreamString2Stream(s);
            return (T)ser.Deserialize(stream);
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
