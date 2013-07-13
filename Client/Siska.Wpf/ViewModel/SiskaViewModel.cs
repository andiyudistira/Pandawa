namespace Siska.Wpf.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using GalaSoft.MvvmLight;
    using NHibernate.Validator.Engine;
    using Siska.Wpf.Manager;
    using WPFLocalizeExtension.Extensions;

    public class SiskaViewModel : ViewModelBase, IDataErrorInfo, INotifyPropertyChanged
    {
        private readonly ValidatorEngine validation;

        protected IAppSessionManager AppSessionManager { get; private set; }

        protected SiskaViewModel(IAppSessionManager sessionManager)
        {
            validation = NHibernate.Validator.Cfg.Environment.SharedEngineProvider.GetEngine();
            AppSessionManager = sessionManager;
        }

        public string this[string propertyName]
        {
            get
            {
                var rules = GetInvalidRules(propertyName);
                if (rules != null && rules.Count > 0)
                {
                    return rules[0].Message;
                }

                return null;
            }
        }

        public string Error
        {
            get { return string.Empty; }
        }

        public IList<InvalidValue> GetInvalidRules(string propertyName)
        {
            var type = this.GetType();

            return validation.ValidatePropertyValue(type, propertyName, GetPropertyValue(type, propertyName));
        }

        public IList<InvalidValue> GetAllInvalidRules()
        {
            return validation.Validate(this);
        }

        private object GetPropertyValue(Type objectType, string properyName)
        {
            return objectType.GetProperty(properyName).GetValue(this, null);
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected string GetUIString(string key)
        {
            string uiString;
            LocTextExtension locExtension = new LocTextExtension(key);            
            locExtension.ResolveLocalizedValue(out uiString);
            return uiString;
        }
    }
}
