using AssemblyDataExtractor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyDataExtractor
{
    public interface IDataExtractor
    {
        List<NamespaceModel> GetInformationAboutAssembly(string filename);
    }
}
