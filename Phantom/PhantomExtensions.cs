using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Phantom
{
    public static class PhantomExtensions
    {
        public static object Get(this object target, string propertyName)
        {
            return PhantomReflection.GetProperty(target, propertyName);
        }

        public static string GetString(this object target, string propertyName)
        {
            return Convert.ToString(Get(target, propertyName));
        }

        public static void Set(this object target, string propertyName, object value)
        {
            PhantomReflection.SetProperty(target, propertyName, value);
        }

        public static object Invoke(this object target, string methodName, params object[] parameters)
        {
            return PhantomReflection.InvokeMethod(target, methodName, parameters);
        }

        public static object Invoke(this object target, string methodName)
        {
            return PhantomReflection.InvokeMethod(target, methodName);
        }

        public static PropertyInfo[] Properties(this object target)
        {
            return PhantomReflection.GetPropertiesInfo(target);
        }

        public static Dictionary<string, object> Values(this object target)
        {
            var values = new Dictionary<string, object>();

            foreach(var property in target.Properties())
            {
                values.Add(property.Name, property.GetValue(target, null));
            }

            return values;
        }

        public static void ImportValues(this object self, object target)
        {
            var targetProperties = target.Values();

            foreach(var property in self.Properties())
            {
                if(targetProperties.ContainsKey(property.Name))
                {
                    self.Set(property.Name, targetProperties[property.Name]);
                }
            }
        }
    }
}