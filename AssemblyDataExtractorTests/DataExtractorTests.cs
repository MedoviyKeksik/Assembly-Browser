using AssemblyDataExtractor;
using AssemblyDataExtractor.Models;
using NUnit.Framework;
using System.Linq;

namespace AssemblyDataExtractorTests
{
    public class Tests
    {
        const string Path = @"d:\Projects\SPP\Assembly Browser\SampleAssemblies\bin\Debug\net5.0\SampleAssemblies.dll";
        const string AssemblyName = "SampleAssemblies";
        private IDataExtractor _dataExtractor;

        [SetUp]
        public void Setup()
        {
            _dataExtractor = new DataExtractor();
        }

        [Test]
        public void FindBasicClassA()
        {
            //arrange
            var list = _dataExtractor.GetInformationAboutAssembly(Path);

            //act
            var namespaceModel = list.SingleOrDefault(model => model.Name == AssemblyName);
            var typeModel = namespaceModel?.Types.SingleOrDefault(model => model.Name == "ClassA");

            //assert
            Assert.NotNull(typeModel);
        }

        [Test]
        public void FindExtensionsForClassB()
        {
            //arrange
            var list = _dataExtractor.GetInformationAboutAssembly(
                Path);

            //act
            var namespaceModel = list.SingleOrDefault(model => model.Name == AssemblyName);
            var typeModel = namespaceModel?.Types.SingleOrDefault(model => model.Name == "ClassB");

            //assert
            Assert.NotNull(typeModel);
            Assert.AreEqual(2, typeModel.ExtensionMethods.Count);
        }

        [Test]
        public void FindNestedClassC()
        {
            //arrange
            var list = _dataExtractor.GetInformationAboutAssembly(Path);

            //act
            var namespaceModel = list.SingleOrDefault(model => model.Name == AssemblyName);
            var typeModel = namespaceModel?.Types.SingleOrDefault(model => model.Name.Contains("NestedClassC"));

            //assert
            Assert.NotNull(typeModel);
        }

        [Test]
        public void FindNoNamespaceClass()
        {
            //arrange
            var list = _dataExtractor.GetInformationAboutAssembly(Path);

            //act
            var namespaceModel = list.SingleOrDefault(model => model.Name == "NoNamespace");
            var typeModel = namespaceModel?.Types.SingleOrDefault();

            //assert
            Assert.NotNull(typeModel);
        }

        [Test]
        public void FindSystemClassExtension()
        {
            //arrange
            var list = _dataExtractor.GetInformationAboutAssembly(Path);

            //act
            var namespaceModel = list.SingleOrDefault(model => model.Name == "System");
            var typeModel = namespaceModel?.Types.SingleOrDefault();

            //assert
            Assert.NotNull(typeModel);
            Assert.NotZero(typeModel.ExtensionMethods.Count);
        }

        [Test]
        public void CheckPropertyModel()
        {
            //arrange
            var list = _dataExtractor.GetInformationAboutAssembly(Path);

            //act
            var namespaceModel = list.SingleOrDefault(model => model.Name == AssemblyName);
            var typeModel = namespaceModel?.Types.SingleOrDefault(model => model.Name == "ClassB");
            var propertyModel = typeModel?.Members.SingleOrDefault(model => model is PropertyModel) as PropertyModel;

            //assert
            Assert.NotNull(propertyModel);
            Assert.NotNull(propertyModel.GetMethod);
            Assert.NotNull(propertyModel.SetMethod);
        }
    }
}