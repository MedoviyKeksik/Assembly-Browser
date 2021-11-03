using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyDataExtractor.Models
{
    public class PropertyModel : MemberModel
    {
        public Type Type { get; set; }

        public MethodModel GetMethod { get; set; }

        public MethodModel SetMethod { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" {Type.Name} {Name} " + "{ " + (GetMethod != null ? "get; " : "") + (SetMethod != null ? "set;" : "") + " }";
        }

        public static PropertyModel CreatePropertyModel(PropertyInfo propertyInfo)
        {
            MethodModel getMethod = null;
            MethodModel setMethod = null;
            if (propertyInfo.CanRead)
            {
                getMethod = MethodModel.CreateMethodModel(propertyInfo.GetMethod);
            }

            if (propertyInfo.CanWrite)
            {
                setMethod = MethodModel.CreateMethodModel(propertyInfo.SetMethod);
            }

            var modifier = ModifiersEnum.Private;
            if (getMethod?.Modifier == ModifiersEnum.Public || setMethod?.Modifier == ModifiersEnum.Public)
                modifier = ModifiersEnum.Public;
            else if (getMethod?.Modifier == ModifiersEnum.ProtectedInternal || setMethod?.Modifier == ModifiersEnum.ProtectedInternal)
                modifier = ModifiersEnum.ProtectedInternal;
            else if (getMethod?.Modifier == ModifiersEnum.Internal || setMethod?.Modifier == ModifiersEnum.Internal)
                modifier = ModifiersEnum.Internal;
            else if (getMethod?.Modifier == ModifiersEnum.Protected || setMethod?.Modifier == ModifiersEnum.Protected)
                modifier = ModifiersEnum.Protected;
            else if (getMethod?.Modifier == ModifiersEnum.PrivateProtected || setMethod?.Modifier == ModifiersEnum.PrivateProtected)
                modifier = ModifiersEnum.PrivateProtected;

            return new PropertyModel
            {
                Name = propertyInfo.Name,
                Type = propertyInfo.PropertyType,
                IsStatic = false,
                GetMethod = getMethod,
                SetMethod = setMethod,
                Modifier = modifier
            };
        }
    }
}
