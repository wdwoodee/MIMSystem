using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;

namespace MIMSystem.Utility
{

    public static class StringEnums
    {
        private static readonly Hashtable StringValues = new Hashtable();

        public enum conType
        {
            [StringValue("消费")]
            xiaofei,
            [StringValue("积分兑换")]
            jifenduihuan,
            [StringValue("加钱购")]
            jiaqiangou,
        }
        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string GetStringValue<T>(T value)
        {
            string output = null;
            var type = value.GetType();
            if (StringValues.ContainsKey(value))
            {
                var stringValueAttribute = StringValues[value] as StringValueAttribute;
                if (stringValueAttribute != null)
                    output = stringValueAttribute.Value;
            }
            else
            {
                var field = type.GetField(value.ToString());
                var stringValue =
                    field.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

                if (stringValue == null)
                {
                    throw new ApplicationException("GetStringValue requires a 'StringValueAttribute' to be present on every enum value.");
                }

                StringValues.Add(value, stringValue[0]);
                output = stringValue[0].Value;
            }

            return output;
        }

        public static string ToStringValue(this Enum value)
        {
            return GetStringValue(value);
        }

        internal class StringValueAttribute : Attribute
        {
            private readonly string value;

            public StringValueAttribute(string value)
            {
                this.value = value;
            }

            public string Value
            {
                get { return value; }
            }
        }
    }
}
