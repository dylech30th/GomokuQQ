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

        public static bool HasCustomAttribute<T>(this Type type) where T : Attribute
        {
            return type.GetCustomAttribute<T>() != null;
        }

        public static ConstructorInfo GetNonParameterConstructor(this Type type)
        {
            return type.GetConstructor(new Type[] { });
        }

        public static bool IsInheritFromGeneric(this Type type, Type genericType, Type genericParameterType)
        {
            return type.GetInterfaces().Any(p => p.IsGenericType && p.GetGenericTypeDefinition() == genericType && p.GetGenericArguments().Length == 1 && genericParameterType.IsAssignableFrom(p.GetGenericArguments()[0]));
        }

        public static void CallMethod<T>(this T instance, string method, params object[] parameters)
        {
            instance.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(p => p.Name == method)?.Invoke(instance, parameters);
        }
    }
}