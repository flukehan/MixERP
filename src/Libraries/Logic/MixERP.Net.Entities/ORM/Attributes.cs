using System;

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
