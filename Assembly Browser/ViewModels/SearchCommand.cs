using System;
using System.Windows.Input;
using Assembly_Browser.ViewModels;

namespace Assembly_Browser.ViewModels
{
    public class SearchCommand : ICommand
    {
        readonly TreeInfoViewModel _assemblyTreeInfoViewModel;

        public SearchCommand(TreeInfoViewModel assemblyTreeInfoViewModel)
        {
            _assemblyTreeInfoViewModel = assemblyTreeInfoViewModel;
        }
        
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _assemblyTreeInfoViewModel.GetInfoAboutAssembly();
        }

        public event EventHandler CanExecuteChanged
        {
         add {}
         remove {}
        }
    }
}