using System;
using Models;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using ViewModels.Commands;
using System.Windows.Input;
using System.ComponentModel;

namespace ViewModels
{
    public class MainWindowViewModel
    {
        Person person = new Person();
        public ICommand ShowCommand { get; set; }

        public string Name { get => person.Name; set => person.Name = value; }
        public int Age { get => person.Age; set => person.Age = value; }

        public MainWindowViewModel()
        {
            ShowCommand = new ShowCommand(this);
        }

        public void ReverseName()
        {
            /*  Stack<char> stack = new Stack<char>(Name.ToCharArray());

              Name = "";
              foreach (var c in stack)
                  Name += c; */

            Console.WriteLine("view model command handler triggered");
            Name = "command triggered";
            
        }
        
    }
}
