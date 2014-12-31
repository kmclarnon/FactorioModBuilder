using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfUtils.Bindings
{
    [MarkupExtensionReturnType(typeof(object))]
    public abstract class BaseBinding : MarkupExtension
    {
        // internal binding that we wrap
        private Binding _binding = new Binding();

        #region Binding Properties
        [Browsable(false)]
        public Binding Binding
        {
            get { return _binding; }
            set { _binding = value; }
        }

        [DefaultValue(null)]
        public object AsyncState
        {
            get { return _binding.AsyncState; }
            set { _binding.AsyncState = value; }
        }

        [DefaultValue(false)]
        public bool BindsDirectlyToSource
        {
            get { return _binding.BindsDirectlyToSource; }
            set { _binding.BindsDirectlyToSource = value; }
        }

        [DefaultValue(null)]
        public IValueConverter Converter
        {
            get { return _binding.Converter; }
            set { _binding.Converter = value; }
        }

        [DefaultValue(null)]
        public object TargetNullValue
        {
            get { return _binding.TargetNullValue; }
            set { _binding.TargetNullValue = value; }
        }

        [TypeConverter(typeof(CultureInfoIetfLanguageTagConverter)), DefaultValue(null)]
        public CultureInfo ConverterCulture
        {
            get { return _binding.ConverterCulture; }
            set { _binding.ConverterCulture = value; }
        }

        [DefaultValue(null)]
        public object ConverterParameter
        {
            get { return _binding.ConverterParameter; }
            set { _binding.ConverterParameter = value; }
        }

        [DefaultValue(null)]
        public string ElementName
        {
            get { return _binding.ElementName; }
            set { _binding.ElementName = value; }
        }

        [DefaultValue(null)]
        public object FallbackValue
        {
            get { return _binding.FallbackValue; }
            set { _binding.FallbackValue = value; }
        }

        [DefaultValue(false)]
        public bool IsAsync
        {
            get { return _binding.IsAsync; }
            set { _binding.IsAsync = value; }
        }

        [DefaultValue(BindingMode.Default)]
        public BindingMode Mode
        {
            get { return _binding.Mode; }
            set { _binding.Mode = value; }
        }

        [DefaultValue(false)]
        public bool NotifyOnSourceUpdated
        {
            get { return _binding.NotifyOnSourceUpdated; }
            set { _binding.NotifyOnSourceUpdated = value; }
        }

        [DefaultValue(false)]
        public bool NotifyOnTargetUpdated
        {
            get { return _binding.NotifyOnTargetUpdated; }
            set { _binding.NotifyOnTargetUpdated = value; }
        }

        [DefaultValue(false)]
        public bool NotifyOnValidationError
        {
            get { return _binding.NotifyOnValidationError; }
            set { _binding.NotifyOnValidationError = value; }
        }

        [DefaultValue(null)]
        public PropertyPath Path
        {
            get { return _binding.Path; }
            set { _binding.Path = value; }
        }

        [DefaultValue(null)]
        public RelativeSource RelativeSource
        {
            get { return _binding.RelativeSource; }
            set { _binding.RelativeSource = value; }
        }

        [DefaultValue(null)]
        public object Source
        {
            get { return _binding.Source; }
            set { _binding.Source = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public UpdateSourceExceptionFilterCallback UpdateSourceExceptionFilter
        {
            get { return _binding.UpdateSourceExceptionFilter; }
            set { _binding.UpdateSourceExceptionFilter = value; }
        }

        [DefaultValue(UpdateSourceTrigger.Default)]
        public UpdateSourceTrigger UpdateSourceTrigger
        {
            get { return _binding.UpdateSourceTrigger; }
            set { _binding.UpdateSourceTrigger = value; }
        }

        [DefaultValue(false)]
        public bool ValidatesOnDataErrors
        {
            get { return _binding.ValidatesOnDataErrors; }
            set { _binding.ValidatesOnDataErrors = value; }
        }

        [DefaultValue(false)]
        public bool ValidatesOnExceptions
        {
            get { return _binding.ValidatesOnExceptions; }
            set { _binding.ValidatesOnExceptions = value; }
        }

        [DefaultValue(null)]
        public string XPath
        {
            get { return _binding.XPath; }
            set { _binding.XPath = value; }
        }

        [DefaultValue(null)]
        public Collection<ValidationRule> ValidationRules
        {
            get { return _binding.ValidationRules; }
        }

        [DefaultValue(null)]
        public string StringFormat
        {
            get { return _binding.StringFormat; }
            set { _binding.StringFormat = value; }
        }

        [DefaultValue("")]
        public string BindingGroupName
        {
            get { return _binding.BindingGroupName; }
            set { _binding.BindingGroupName = value; }
        }
        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // create a binding associated with our target
            return _binding.ProvideValue(serviceProvider);
        }

        protected virtual bool TryGetTarget(IServiceProvider provider, out DependencyObject target, out DependencyProperty dp)
        {
            target = null;
            dp = null;
            // ensure that we have a valid provider
            if (provider == null) 
                return false;

            // create a binding for our target
            IProvideValueTarget service = (IProvideValueTarget)provider.GetService(typeof(IProvideValueTarget));
            if (service == null) 
                return false;

            // get our dependency objects and properties
            target = service.TargetObject as DependencyObject;
            dp = service.TargetProperty as DependencyProperty;

            return (target != null) && (dp != null);
        }
    }
}
