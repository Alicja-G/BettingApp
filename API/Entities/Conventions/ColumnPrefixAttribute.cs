using System;

namespace API.Entities.Conventions
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ColumnPrefixAttribute : System.Attribute
    {   
        public ColumnPrefixAttribute(string prefix)
        {
            this.Prefix = prefix; 
        }
        public string Prefix {get; set;}
    }
}