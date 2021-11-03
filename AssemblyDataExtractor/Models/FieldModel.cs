using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyDataExtractor.Models
{
    public class FieldModel : MemberModel
    {
        public Type Type { get; set; }

        public override string ToString()
        {
            return base.ToString() + $"{Type.Name} {Name}";
        }

        public static FieldModel CreateFieldModel(FieldInfo fieldInfo)
        {
            var modifier = ModifiersEnum.Public;
            {
                if (fieldInfo.IsPrivate) modifier = ModifiersEnum.Private;
                if (fieldInfo.IsPublic) modifier = ModifiersEnum.Public;
                if (fieldInfo.IsFamily) modifier = ModifiersEnum.Protected;
                if (fieldInfo.IsFamilyAndAssembly) modifier = ModifiersEnum.PrivateProtected;
                if (fieldInfo.IsFamilyOrAssembly) modifier = ModifiersEnum.ProtectedInternal;
                if (fieldInfo.IsAssembly) modifier = ModifiersEnum.Internal;
            }

            return new FieldModel
            {
                Name = fieldInfo.Name,
                Type = fieldInfo.FieldType,
                IsStatic = fieldInfo.IsStatic,
                Modifier = modifier
            };
        }
    }
}
