using AssemblyDataExtractor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyDataExtractor
{
    public class DataExtractor : IDataExtractor
    {
        private Dictionary<string, NamespaceModel> _namespaces;

        private List<ExtensionMethodModel> _extensionMethods;

        private const BindingFlags SearchSettings = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        private const string NoNamespaceName = "NoNamespace";

        public List<NamespaceModel> GetInformationAboutAssembly(string filename)
        {
            _namespaces = new Dictionary<string, NamespaceModel>()
            {
                { NoNamespaceName, new NamespaceModel { Name = NoNamespaceName } }
            };
            _extensionMethods = new List<ExtensionMethodModel>();

            var assembly = Assembly.LoadFile(filename);

            var types = assembly
                .GetTypes()
                .Where(t =>
                    !t.CustomAttributes
                        .Select(ca => ca.AttributeType)
                        .Contains(typeof(CompilerGeneratedAttribute))
                    );

            foreach (var type in types)
            {
                var typeModel = CreateTypeModel(type);
                var typeNamespace = type.Namespace;
                if (typeNamespace != null)
                {
                    _namespaces.TryGetValue(typeNamespace, out var model);
                    if (model == null)
                    {
                        model = new NamespaceModel { Name = typeNamespace };
                        _namespaces.Add(model.Name, model);
                    }
                    model.Types.Add(typeModel);
                }
                else
                {
                    _namespaces[NoNamespaceName].Types.Add(typeModel);
                }
            }

            foreach (var extensionMethod in _extensionMethods)
            {
                _namespaces.TryGetValue(extensionMethod.TargetType.Namespace, out var namespaceModel);
                if (namespaceModel == null)
                {
                    namespaceModel = new NamespaceModel()
                    {
                        Name = extensionMethod.TargetType.Namespace
                    };
                    _namespaces.Add(namespaceModel.Name, namespaceModel);
                }
                var typeModel = namespaceModel.Types.Find(t => t.Name == extensionMethod.TargetType.Name);
                if (typeModel == null)
                {
                    typeModel = new TypeModel()
                    {
                        Name = extensionMethod.TargetType.Name
                    };
                    namespaceModel.Types.Add(typeModel);
                }
                typeModel.ExtensionMethods.Add(extensionMethod);
            }

            return _namespaces.Values.ToList();
        }

        private TypeModel CreateTypeModel(Type type)
        {
            var typeModel = new TypeModel()
            {
                Name = type.Name
            };

            var methods = type.GetMethods(SearchSettings);
            var fields = type.GetFields(SearchSettings);
            var properties = type.GetProperties(SearchSettings);

            foreach (var method in methods)
            {
                var methodModel = MethodModel.CreateMethodModel(method);
                if (methodModel is ExtensionMethodModel extensionMethodModel)
                {
                    _extensionMethods.Add(extensionMethodModel);
                }
                else
                {
                    typeModel.Members.Add(methodModel);
                }
            }

            foreach (var field in fields)
            {
                var fieldModel = FieldModel.CreateFieldModel(field);
                typeModel.Members.Add(fieldModel);
            }

            foreach (var property in properties)
            {
                var propertyModel = PropertyModel.CreatePropertyModel(property);
                typeModel.Members.Add(propertyModel);
            }

            return typeModel;
        }
    }
}
