using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyDataExtractor.Models
{
    public class MethodModel : MemberModel
    {
        public string Signature { get; set; }

        public Type ReturningType { get; set; }

        public bool IsVirtual { get; set; }

        public bool IsAbstract { get; set; }

        public override string ToString()
        {
            return (IsVirtual ? "virtual " : "") + (IsAbstract ? "abstract " : "") + base.ToString() + $" {ReturningType.Name} {Name}{Signature}";
        }

        public MethodModel(string signature, Type returningType, bool isVirtual, bool isAbstract)
        {
            Signature = signature;
            ReturningType = returningType;
            IsVirtual = isVirtual;
            IsAbstract = isAbstract;
        }

        private MethodModel()
        {

        }

        public static MethodModel CreateMethodModel(MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters();
            var signature = new StringBuilder();

            foreach (var param in parameters)
            {
                signature.Append($"{param.ParameterType.Name} {param.Name},");
            }

            if (signature.ToString() != "")
            {
                signature.Remove(signature.Length - 1, 1);
            }
            signature.Insert(0, '(');
            signature.Append(')');

            var modifier = ModifiersEnum.Public;
            {
                if (methodInfo.IsPrivate) modifier = ModifiersEnum.Private;
                if (methodInfo.IsPublic) modifier = ModifiersEnum.Public;
                if (methodInfo.IsFamily) modifier = ModifiersEnum.Protected;
                if (methodInfo.IsFamilyAndAssembly) modifier = ModifiersEnum.PrivateProtected;
                if (methodInfo.IsFamilyOrAssembly) modifier = ModifiersEnum.ProtectedInternal;
                if (methodInfo.IsAssembly) modifier = ModifiersEnum.Internal;
            }

            var methodModel = new MethodModel()
            {
                Name = methodInfo.Name,
                ReturningType = methodInfo.ReturnType,
                Signature = signature.ToString(),
                IsAbstract = methodInfo.IsAbstract,
                IsStatic = methodInfo.IsStatic,
                IsVirtual = methodInfo.IsVirtual,
                Modifier = modifier,
            };


            if (!methodInfo.CustomAttributes.Select(ca => ca.AttributeType).Contains(typeof(ExtensionAttribute)))
                return methodModel;

            var extensionMethodModel = new ExtensionMethodModel(methodModel)
            {
                Name = methodModel.Name,
                TargetType = parameters[0].ParameterType
            };

            return extensionMethodModel;
        }
    }
}
