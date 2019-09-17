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
        private class AdminEqualityComparer : IEqualityComparer<Administrator>
        {
            public bool Equals(Administrator x, Administrator y)
            {
                return x?.Id == y?.Id;
            }

            public int GetHashCode(Administrator obj)
            {
                return obj.GetHashCode();
            }
        }

        private static ISet<Administrator> _administrators = new HashSet<Administrator>(new AdminEqualityComparer())
        {
            new Administrator
            {
                Id = Configuration.Me
            } //dc
        };

        public static void AddAdmin(string id)
        {
            if (_administrators.All(admin => admin.Id != id))
            {
                _administrators.Add(new Administrator {Id = id});
                SaveAdmins();
            }
        }

        public static ISet<Administrator> GetAdministrators() => _administrators;

        private const string Filename = "admins.json";

        public static void SaveAdmins()
        {
            if (!File.Exists(Filename))
            {
                File.Create(Filename);
            }

            File.WriteAllText(Filename, _administrators.ToJson());
        }

        public static void LoadAdmins()
        {
            if (File.Exists(Filename))
            {
                _administrators = File.ReadAllText(Filename).FromJson<ISet<Administrator>>() ?? new HashSet<Administrator> {new Administrator {Id = Configuration.Me}};
            }
            else
            {
                _administrators = new HashSet<Administrator> {new Administrator {Id = Configuration.Me}};
            }
        }
    }
}