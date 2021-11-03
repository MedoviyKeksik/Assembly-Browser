using AssemblyDataExtractor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_Browser.ViewModels
{
    public class TreeInfoViewModel : INotifyPropertyChanged
    {
        private IDataExtractor _dataExtractor;

        public SearchCommand SearchCommand { get; }

        public string AssemblyFilename { get; set; }

        public ObservableCollection<MemberInfoViewModel> MemberInfoViewModels { get; set; } = new();

        public TreeInfoViewModel()
        {
            _dataExtractor = new DataExtractor();

            SearchCommand = new SearchCommand(this);
        }

        public void GetInfoAboutAssembly()
        {
            var list = _dataExtractor.GetInformationAboutAssembly(AssemblyFilename);

            foreach (var namespaceModel in list)
            {
                MemberInfoViewModels.Add(new MemberInfoViewModel(namespaceModel));
            }

            OnPropertyChanged(nameof(MemberInfoViewModels));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
