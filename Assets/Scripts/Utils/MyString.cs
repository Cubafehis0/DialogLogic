using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utils
{
    public static class MyString
    {
        public static List<string> SplitString(string str)
        {
            if (str == null)
            {
                return new List<string>();
            }
            return new List<string>(str.Split(';', '；')).FindAll(e => !string.IsNullOrEmpty(e)).ToList();
        }

        public static List<int> SplitIntString(string str, int intCnt)
        {
            List<int> retval = new List<int>();
            for (int i = 0; i < intCnt; i++)
                retval.Add(0);
            int cnt = 0;
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Trim();
                string[] nums = str.Split(new char[] { ';', '；' });
                foreach (string num in nums)
                {
                    if (cnt == intCnt)
                        break;
                    int.TryParse(num, out int t);
                    retval[cnt++] = t;
                }
            }
            return retval;
        }
    }
}
