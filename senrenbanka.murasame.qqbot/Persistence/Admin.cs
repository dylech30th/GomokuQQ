using System.Collections.Generic;
using System.IO;
using System.Linq;
using senrenbanka.murasame.qqbot.Resources.Primitive;

namespace senrenbanka.murasame.qqbot.Persistence
{
    public class Administrator
    {
        public string Id { get; set; }
    }

    public class Admin
    {
        private static ISet<Administrator> Administrators = new HashSet<Administrator>
        {
            new Administrator
            {
                Id = Configuration.Me
            } //dc
        };

        public static void AddAdmin(string id)
        {
            if (Administrators.All(admin => admin.Id != id))
            {
                Administrators.Add(new Administrator {Id = id});
            }
        }

        public static ISet<Administrator> GetAdministrators() => Administrators;

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