using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;

sealed class DeserializationBinderEditor : SerializationBinder
{
    public override System.Type BindToType(string assemblyName, string typeName)
    {
        if(string.IsNullOrEmpty(assemblyName) && string.IsNullOrEmpty(typeName))
        {
            return null;
        }       

        System.Type typeToDeserialize = null;       

        assemblyName = Assembly.GetExecutingAssembly().FullName;

        typeToDeserialize = System.Type.GetType(string.Format("{0}, {1}", typeName, assemblyName));       

        return typeToDeserialize;
    }
}
