using System;
using System.Windows.Input;
using Assembly_Browser.ViewModels;
using Microsoft.Win32;

namespace Assembly_Browser.ViewModels
{
    public class OpenFileCommand : ICommand
    {
        readonly TreeInfoViewModel _assemblyTreeInfoViewModel;

        public OpenFileCommand(TreeInfoViewModel assemblyTreeInfoViewModel)
        {
            _assemblyTreeInfoViewModel = assemblyTreeInfoViewModel;
        }
        
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "Assemblies|*.dll;*.exe",
                Title = "Select assembly",
                Multiselect = false
            };

            var isOk = fileDialog.ShowDialog();
            if (isOk != null && isOk.Value)
                _assemblyTreeInfoViewModel.GetInfoAboutAssembly(fileDialog.FileName);
        }

        public event EventHandler CanExecuteChanged
        {
         add {}
         remove {}
        }
    }
}