using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace Qred.Connect
{
    public class TypeNameSerializationBinder : ISerializationBinder
    {
        public TypeNameSerializationBinder()
        {
            var typeNames = new Dictionary<Type, string>
            {
                { typeof(ApplicationRequest),"simple" },
                { typeof(SimpleApplicant),"simple" },
                { typeof(SimpleOrganization),"simple" },
                { typeof(FullApplicationRequest),"full" },
                { typeof(FullApplicant),"full" },
                { typeof(FullOrganization),"full" },
            };
            foreach (var typeName in typeNames)
            {
                Map(typeName.Key, typeName.Value);
            }
        }

        readonly Dictionary<Type, string> typeToName = new Dictionary<Type, string>();

        public void Map(Type type, string name) => this.typeToName.Add(type, name);

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            if (typeToName.TryGetValue(serializedType, out typeName))
            {
                assemblyName = null;
            }
            else
            {
                assemblyName = null;
                typeName = null;
            }
        }

        public Type BindToType(string assemblyName, string typeName) => null;
    }
}
