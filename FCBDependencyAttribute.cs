using System;

namespace FCB.Extension.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class FCBDependencyAttribute : Attribute
    {
        public Type[] InterfacesRepresentation { get; }

        public FCBDependencyAttribute(params Type[] interfacesRepresentation)
        {
            InterfacesRepresentation = interfacesRepresentation;
        }
    }
}
