using System;
using System.Collections.Generic;
using System.Text;

namespace WinHue_Core.Philips_Hue.Exceptions
{
    public class InvalidPropertyException : Exception
    {
        private string propName;

        public InvalidPropertyException(string message, string propertyName) : base(message)
        {
            propName = propertyName;
        }

        public InvalidPropertyException(string message, string propertyName , Exception innerException) : base(message, innerException)
        {
            propName = propertyName;

        }

        public string PropertyName => propName;

    }
}
