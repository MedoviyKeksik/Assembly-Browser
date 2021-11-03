using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyDataExtractor.Models
{
    public enum ModifiersEnum
    {
        Private,
        Protected,
        Public,
        Internal,
        ProtectedInternal,
        PrivateProtected
    }
    public class MemberModel
    {
        public string Name { get; set; }

        public ModifiersEnum Modifier { get; set; }

        public bool IsStatic { get; set; }

        public override string ToString()
        {
            return (IsStatic ? "static " : "") + $"{Modifier.ToString()}";
        }
    }
}
