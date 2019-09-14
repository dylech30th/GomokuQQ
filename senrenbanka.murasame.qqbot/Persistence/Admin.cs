using System.Collections.Generic;
using System.IO;
using senrenbanka.murasame.qqbot.Resources.Primitive;

namespace senrenbanka.murasame.qqbot.Persistence
{
    public class Administrator
    {
        public string Id { get; set; }
    }

    public class Admin
    {
        public static ISet<Administrator> Administrators = new HashSet<Administrator>
        {
            new Administrator
            {
                Id = "2653221698"
            } //dc
        };

        private const string Filename = "admins.json";

        public static void SaveAdmins()
        {
            if (!File.Exists(Filename))
            {
                File.Create(Filename);
            }

            File.WriteAllText(Filename, Administrators.ToJson());
        }

        public static void LoadAdmins()
        {
            if (File.Exists(Filename))
            {
                Administrators = File.ReadAllText(Filename).FromJson<ISet<Administrator>>() ?? new HashSet<Administrator> {new Administrator {Id = "2653221698"}};
            }
            else
            {
                Administrators = new HashSet<Administrator> {new Administrator {Id = "2653221698"}};
            }
        }
    }
}