using BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using log4net;
using System.Reflection;

namespace ViewModels.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void TriggerPropertyChangedEvent(string propertyName)
        {
            ValidatePropertyName(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void ValidatePropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
                throw new Exception("Invalid propery name: " + propertyName);

        }

        protected void SetValue<T>(ref T target, T value, string propertyName)
        {
            target = value;
            TriggerPropertyChangedEvent(propertyName);
        }

        protected void SetValue<T>(object obj, T value, string propertyName)
        {
            obj.GetType().GetProperty(propertyName).SetValue(obj, value);
            TriggerPropertyChangedEvent(propertyName);
        }
        public ToursManager Manager { get; private set; } = Application.GetToursManager();
        public ILog Logger { get; }
        public ViewModelBase(string viewName, string title, ILog logger = null)
        {
            ViewName = viewName;
            Title = title;
            Logger = logger ?? Application.GetLogger();

        }
        public string ViewName { get; protected set; }
        public string Title { get; protected set; }
        public virtual void Reset() { }

        
    }
}
