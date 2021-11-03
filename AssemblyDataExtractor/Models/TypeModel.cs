using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyDataExtractor.Models
{
    public class TypeModel
    {
        public string Name { get; set; }

        public List<MemberModel> Members { get; set; } = new();

        public List<ExtensionMethodModel> ExtensionMethods { get; set; } = new();

        public override string ToString()
        {
            return Name;
        }
    }
}
