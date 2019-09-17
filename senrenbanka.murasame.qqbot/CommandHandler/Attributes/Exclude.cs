using System;

namespace senrenbanka.murasame.qqbot.CommandHandler.Attributes
{
    public class Exclude : Attribute
    {
        public string[] Excludes;

        public Exclude(string[] excludes)
        {
            Excludes = excludes;
        }
    }
}