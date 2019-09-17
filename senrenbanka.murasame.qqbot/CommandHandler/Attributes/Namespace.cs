using System;

namespace senrenbanka.murasame.qqbot.CommandHandler.Attributes
{
    public class Namespace : Attribute
    {
        public Namespace(string typeNamespace)
        {
            TypeNamespace = typeNamespace;
        }
        public string TypeNamespace { get; set; }
    }
}