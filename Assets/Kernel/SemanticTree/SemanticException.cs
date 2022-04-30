using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticTree
{

    [Serializable]
    public class SemanticException : Exception
    {
        public SemanticException() { }
        public SemanticException(string message) : base(message) { }
        public SemanticException(string message, Exception inner) : base(message, inner) { }
        protected SemanticException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
