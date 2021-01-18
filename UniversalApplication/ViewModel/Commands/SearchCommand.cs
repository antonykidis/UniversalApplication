
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniversalApplication.ViewModel.Helpers;//weatherviewmodel

namespace UniversalApplication.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        //Create an instance object of WeatherModels class
        public WeatherViewModel VM { get; set; }
        public event EventHandler CanExecuteChanged;
        //constructor with 1 parameter
        public SearchCommand( WeatherViewModel vm)
        {
            //Assign VM instance to the property
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            string query = parameter as string;

            if (string.IsNullOrWhiteSpace(query))
            {
                return false; //don't execute empty queries
            }
            return true;
        }

        public void Execute(object parameter)
        {
            VM.MakeQuery();
        }
    }
}
