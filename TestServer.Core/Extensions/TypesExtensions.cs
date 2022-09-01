namespace TestServer.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class TypesExtensions
    {
        public static IEnumerable<Type> GetImplementingTypes(this Type type)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(assembly => !assembly.IsDynamic)
                .SelectMany(assembly => assembly.GetTypesImplementing(type));   
        }

        public static IEnumerable<Type> GetTypesWithAttribute<T>()
        {
            return AppDomain.CurrentDomain
                  .GetAssemblies()
                 .Where(assembly => !assembly.IsDynamic)
                 .SelectMany(x => x.GetTypesWithAttribute<T>());
        }            
    }
}
