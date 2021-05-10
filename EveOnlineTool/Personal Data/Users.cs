using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EveOnlineTool.Personal_Data
{
    public class EoiUsers
    {
        [XmlElement]
        public List<User> Users { get; set; }

        public EoiUsers()
        {
            Users = new List<User>();
        }

        internal void Add(User user)
        {
            if (Users.FirstOrDefault(x => x.Name == user.Name) == null)
                Users.Add(user);
        }
    }

    public class User
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public int Role { get; set; }

        public User()
        {

        }

        public User(string name, int role)
        {
            this.Name = name;
            this.Role = role;
        }
    }
}
