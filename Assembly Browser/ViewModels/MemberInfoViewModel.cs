using AssemblyDataExtractor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_Browser.ViewModels
{
    public class MemberInfoViewModel
    {
        public object MemberInfoModel { get; set; }

        public string MemberInfo { get; set; }

        public List<MemberInfoViewModel> Children { get; set; } = new();

        public MemberInfoViewModel(NamespaceModel namespaceModel)
        {
            MemberInfoModel = namespaceModel;
            MemberInfo = namespaceModel.ToString();

            foreach (var typeModel in namespaceModel.Types)
            {
                Children.Add(new MemberInfoViewModel(typeModel));
            }
        }

        private MemberInfoViewModel(TypeModel typeModel)
        {
            MemberInfoModel = typeModel;
            MemberInfo = typeModel.ToString();

            foreach (var memberModel in typeModel.Members)
            {
                Children.Add(new MemberInfoViewModel(memberModel));
            }

            foreach (var memberModel in typeModel.ExtensionMethods)
            {
                Children.Add(new MemberInfoViewModel(memberModel));
            }
        }

        private MemberInfoViewModel(MemberModel memberModel)
        {
            MemberInfoModel = memberModel;
            MemberInfo = memberModel.ToString();

            if (memberModel is PropertyModel propertyModel)
            {
                if (propertyModel.GetMethod != null)
                    Children.Add(new MemberInfoViewModel(propertyModel.GetMethod));

                if (propertyModel.SetMethod != null)
                    Children.Add(new MemberInfoViewModel(propertyModel.SetMethod));
            }
        }
    }
}
