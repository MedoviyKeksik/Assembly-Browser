using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyDataExtractor.Models
{
    public class NamespaceModel
    {
        public string Name { get; set; }

        public List<TypeModel> Types { get; set; } = new();

        public override string ToString()
        {
            return Name;
        }
    }
}
