using System;
using System.Runtime.Serialization;

namespace ExpressionAnalyser
{ 
    /// <summary>
    /// 表明在表达式解析过程中会产生的异常，有以下类型
    /// 1.多余的操作符 例如：1**2 或者 (1))
    /// 2.多余的操作数，例如 1   2
    /// 3 表达式为空  例如 ()()
    /// 字符串含有不支持此法规则  a^2
    /// </summary>
    [Serializable]
    public class ParseException : Exception
    {
        public int? offset=null;
        public string str=null;
        public ParseException()
        {
        }

        public ParseException(string message,string str,int? offset):base(message)
        {
            this.str = str;
            this.offset = offset;
        }
        public ParseException(string message) : base(message)
        {
        }
        public ParseException(string message,int? offset):base(message)
        {
            this.offset = offset;
        }
        override public string Message
        {
            get
            {
                string res =base.Message;
                if(str!=null) res += "\t对应的字符串为：" + str;
                if (offset != null) res += "\t 对应偏移为：" + offset;
                return res;
            }
        }
        public ParseException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected ParseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}