using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra
{
    public class Path
    {
        public List<Node> Nodes { get; set; }
        public float TotalWeight { get; set; }

        public Path()
        {
        }
    }
}
