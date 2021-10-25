using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Ink2Unity
{
    public class FunctionTest : MonoBehaviour
    {
        object TryCoerce<T>(object value)
        {
            if (value == null)
                return null;

            if (value is T)
                return (T)value;

            if (value is float && typeof(T) == typeof(int))
            {
                int intVal = (int)((float)value);
                return intVal;
            }

            if (value is int && typeof(T) == typeof(float))
            {
                float floatVal = (float)(int)value;
                return floatVal;
            }

            if (value is int && typeof(T) == typeof(bool))
            {
                int intVal = (int)value;
                return intVal == 0 ? false : true;
            }

            if (value is bool && typeof(T) == typeof(int))
            {
                bool boolVal = (bool)value;
                return boolVal ? 1 : 0;
            }

            if (typeof(T) == typeof(string))
            {
                return value.ToString();
            }

            return null;
        }
        public TextAsset asset;
        public int choose;
        public bool ctS;
        public bool waitC;
        Ink2Unity ink2Unity;
        // Start is called before the first frame update
        void Start()
        {
            bool a = (bool)TryCoerce<bool>(true);
            ink2Unity = new Ink2Unity(asset);
            choose = -1;
            ctS = false;
            waitC = false;
        }

        // Update is called once per frame
        void Update()
        {
            //是否在等候选择
            if(waitC )
            {
                //choose>0表示已经做出选择
                if(choose>=0)
                {
                    Content c=ink2Unity.SelectChoice(choose);
                    if (c != null)
                        //输出选择后玩家会说的内容
                        Debug.Log(c);
                    waitC = false;
                    choose = -1;
                }
            }
            else if(ctS)
            {
                if (ink2Unity.CanCoutinue)
                {
                    var c = ink2Unity.CurrentContent();
                    Debug.Log(c);
                }
                else
                {
                    var cs = ink2Unity.CurrentChoices();
                    foreach(var c in cs)
                    {
                        Debug.Log(c);
                    }
                    waitC = true;
                }
                ctS = false;
            }
        }
    }

}