using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace senrenbanka.murasame.qqbot.Resources.Runtime
{
    public static class Reflection
    {
        public static IEnumerable<Type> GetAllTypesInNamespace(string ns)
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(type => type.Namespace == ns);
        }

        public static IEnumerable<Type> GetAllTypesImplementInterface<TInterface>(this IEnumerable<Type> types)
        {
            return types.Where(type => typeof(TInterface).IsAssignableFrom(type) && type != typeof(TInterface));
        }
    }
}