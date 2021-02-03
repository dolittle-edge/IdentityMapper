using System;

namespace RaaLabs.IdentityMapper.Common
{
    interface IConfiguration
    {
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    class NameAttribute : Attribute
    {
        public string Name { get; }
        public NameAttribute(string name)
        {
            Name = name;
        }
    }
}
