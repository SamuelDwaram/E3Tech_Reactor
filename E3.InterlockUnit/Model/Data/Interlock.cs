using System.Collections.Generic;

namespace E3.InterlockUnit.Model.Data
{
    public class Interlock
    {
        public string DeviceId { get; set; }

        public IList<Property> Targets { get; set; }

        public IList<Dependency> Dependencies { get; set; }

        public bool Result { get; set; }
    }
}
