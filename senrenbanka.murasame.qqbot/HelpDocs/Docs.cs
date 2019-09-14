using System;
using System.Text;
using senrenbanka.murasame.qqbot.Resources.Runtime;

namespace senrenbanka.murasame.qqbot.HelpDocs
{
    public static class Docs
    {
        public static string GetDocsString()
        {
            var types = Reflection.GetAllTypesInNamespace("senrenbanka.murasame.qqbot.HelpDocs").GetAllTypesImplementInterface<IDocsBase>();
            var sb = new StringBuilder();
            foreach (var type in types)
            {
                var instance = type.GetConstructor(new Type[] { })?.Invoke(null);
                sb.Append(instance);
            }
            return sb.ToString();
        }
    }
}