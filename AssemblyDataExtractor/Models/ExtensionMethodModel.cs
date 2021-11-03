using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyDataExtractor.Models
{
    public class ExtensionMethodModel
    {
        public Type TargetType { get; set; }

        public ExtensionMethodModel(MethodModel methodModel)
            : base(methodModel.Signature, methodModel.ReturningType, methodModel.IsVirtual, methodModel.IsAbstract)
        {
        }

        public override string ToString()
        {
            return "*ext " + base.ToString();
        }
    }
}
