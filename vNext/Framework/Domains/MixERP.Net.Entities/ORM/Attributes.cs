using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetaPoco
{
    // Specify the function name of a poco
    [AttributeUsage(AttributeTargets.Class)]
    public class FunctionNameAttribute : Attribute
    {
        public FunctionNameAttribute(string functionName)
        {
            Value = functionName;
        }
        public string Value { get; private set; }
    }
}
