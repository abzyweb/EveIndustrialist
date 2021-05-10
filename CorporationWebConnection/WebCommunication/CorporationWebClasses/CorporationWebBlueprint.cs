using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporationWebConnection.WebCommunication.CorporationWebClasses
{
    public class CorporationWebBlueprint
    {
        
        public CorporationWebBlueprint(int id, string name, int materialEfficency, int timeEfficency, bool privat)
        {
            this.Id = id.ToString();
            this.Name = name;
            this.ME = materialEfficency.ToString();
            this.TE = timeEfficency.ToString();
            this.Private = privat;
        }

        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public string ME { get; internal set; }
        public string TE { get; internal set; }
        public bool Private { get; internal set; }
    }
}
