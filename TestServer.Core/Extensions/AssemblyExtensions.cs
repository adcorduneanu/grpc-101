namespace TestServer.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetTypesWithAttribute<TAttribute>(this Assembly assembly)
        {
            if (assembly.FullName.Contains("domain"))
            {

            }

            return assembly.GetTypes()
                .Where(type => type.GetCustomAttributes(typeof(TAttribute), true).Any());
        }

        public static IEnumerable<Type> GetTypesImplementing(this Assembly assembly, Type type)
        {
            return assembly.GetTypes()
                    .Where(aType => type.IsAssignableFrom(aType) && !aType.IsInterface && !aType.IsAbstract);
        }
    }
}
