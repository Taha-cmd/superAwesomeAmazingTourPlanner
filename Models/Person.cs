using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Models
{
    public class Person : INotifyPropertyChanged
    {
        private string name = "John Doe";
        private int age = 24;

        public string Name
        {
            get => name;
            
            set
            {
                name = value;
                TriggerPropertyChangedEvent(nameof(Name));
                Console.WriteLine("model setter worked");
            }
        }
        public int Age
        {
            get => age;

            set
            {
                age = value;
                TriggerPropertyChangedEvent(nameof(Age));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void TriggerPropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
