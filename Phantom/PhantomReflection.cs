using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Phantom
{
    public class PhantomReflection
    {
        public PhantomReflection(object obj)
        {
            Target = obj;
        }

        public object Target { get; private set; }

        public object TargetType { get; private set; }

        public object this[string propertyName]
        {
            get { return GetProperty(Target, propertyName); }
            set { SetProperty(Target, propertyName, value); }
        }

        public object InvokeMethod(string name, params object[] parameters)
        {
            return InvokeMethod(Target, name, parameters);
        }

        public object InvokeMethod(string name)
        {
            return InvokeMethod(name, null);
        }

        public object InvokeMethodIfIsValid(string name, params object[] parameters)
        {
            if(IsMethodValid(name))
            {
                return InvokeMethod(Target, name, parameters);
            }
            else
            {
                return null;
            }
        }

        public object InvokeMethodIfIsValid(string name)
        {
            return InvokeMethodIfIsValid(name, null);
        }

        public bool IsMethodValid(string name)
        {
            return IsMethodValid(Target, name);
        }

        public static object InvokeMethod(object target, string name, params object[] parameters)
        {
            Type targetType = target.GetType();

            Type[] paramTypes = null;

            if(parameters != null)
            {
                paramTypes = parameters.Select(p => p.GetType()).ToArray();
            }
            else
            {
                paramTypes = new Type[] { };
            }

            MethodInfo method = targetType.GetMethod(name, paramTypes);

            if(method == null)
            {
                throw new PhantomException(String.Format("Method '{0}' not found in type '{1}'.", name, targetType));
            }

            return method.Invoke(target, parameters);
        }

        public static void SetProperty(object target, string name, object value)
        {
            GetPropertyInfo(target, name).SetValue(target, value, null);
        }

        public static object GetProperty(object target, string name)
        {
            return GetPropertyInfo(target, name).GetValue(target, null);
        }

        public static bool IsMethodValid(object target, string name)
        {
            MethodInfo method = null;

            try
            {
                method = target.GetType().GetMethod(name);
            }
            catch(AmbiguousMatchException)
            {
                return true;
            }

            return method != null;
        }

        public static PropertyInfo[] GetPropertiesInfo(object target)
        {
            return target.GetType().GetProperties().ToArray();
        }

        public static PropertyInfo GetPropertyInfo(object target, string propertyName)
        {
            var targetType = target.GetType();

            var property = targetType.GetProperty(propertyName);

            if(property == null)
            {
                throw new PhantomException(String.Format("Property '{0}' not found in type '{1}'.", propertyName, targetType));
            }

            return property;
        }
    }
}