using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showpony
{
    public class Variant
    {
        public string Name { get; private set; }

        public int Weighting { get; private set; }

        public Variant(string name, int weighting)
        {
            this.Name = name;
            this.Weighting = weighting;
        }
    }
}
