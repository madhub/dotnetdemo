using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class HelperExtensions
    {
        
         public static Type? FindType(this string name)
        {
          Type? result = null;
          var nonDynamicAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic);
          try{
              result = nonDynamicAssemblies.SelectMany(a => a.GetExportedTypes()). FirstOrDefault(t => t.Name == name);
             }catch{
              result = nonDynamicAssemblies.SelectMany(a => a.GetTypes()).FirstOrDefault(t => t.Name == name);
             }
          return result;
        }


        public static bool ImplementsInterface(this Type? type, Type? @interface)
        {
          bool result = false;
          if (type == null || @interface == null)
            return result;
          var interfaces = type.GetInterfaces();
          if (@interface.IsGenericTypeDefinition)
          {
              foreach (var item in interfaces)
              {
                   if (item.IsConstructedGenericType && item.GetGenericTypeDefinition() == @interface)
                   {
                                result = true;
                   }
                    else
                   {
                        foreach (var item in interfaces)
                        {
                            if (item == @interface)
                                result = true;
                        }
                    }
                }
               return result;
           }
        }


            public static bool IsDerivingFrom(this Type type, Type searchType)
            {
                if (type == null) throw new NullReferenceException();
                    return type.BaseType != null && (type.BaseType == searchType || type.BaseType.IsDerivingFrom(searchType));
            }
        
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Extension method")]
            public static Type GetItemType<T>(this IEnumerable<T> enumerable) => typeof(T);
        
            public static Type? GetItemType(this object enumerable) => enumerable == null ? null : (enumerable.GetType().GetInterface(typeof(IEnumerable<>).Name)?.GetGenericArguments()[0]);
        
            public static bool IsDerivingFromGenericType(this Type type, Type searchGenericType)
            {
              if (type == null) throw new ArgumentNullException(nameof(type));

              if (searchGenericType == null) throw new ArgumentNullException(nameof(searchGenericType));
                      return type != typeof(object) && (type.IsGenericType && searchGenericType.GetGenericTypeDefinition() == searchGenericType ||IsDerivingFromGenericType(type.BaseType, searchGenericType));
            }


            public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
            {
                foreach (T element in source)
                {
                    action(element);
                }
            }

        public static long ToUnixTimeStamp(this DateTime dateTimeUtc)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0);
            TimeSpan timeSpan = dateTimeUtc - epoch;
            return Convert.ToInt64(timeSpan.TotalSeconds);
        }
        public static string UserFriendlyFullMessage(this Exception exception)
        {
            StringBuilder exceptionBuilder = new StringBuilder();
            while (exception != null)
            {
                exceptionBuilder.AppendLine(exception.Message);
                exception = exception.InnerException;
            }
            return exceptionBuilder.ToString();
        }
        public static string ToUserFriendlyString(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }

        public static SecureString ToSecureString(this string s)
        {
            if (s == null)
                return null;

            SecureString psw = new SecureString();
            foreach (char c in s.ToCharArray())
            {
                psw.AppendChar(c);
            }
            return psw;
        }
        public static bool IsBetween(this int theNumber, int lower, int higher)
        {
            return (theNumber >= lower) && (theNumber <= higher);
        }
        public static string TrimPrefix(this string str, string prefix)
        {
            if (!String.IsNullOrEmpty(str) && !String.IsNullOrEmpty(prefix) && str.StartsWith(prefix))
            {
                return str.Substring(prefix.Length);
            }
            return str;
        }

        public static string TrimSuffix(this string str, string suffix)
        {
            if (!String.IsNullOrEmpty(str) && !String.IsNullOrEmpty(suffix) && str.EndsWith(suffix))
            {
                return str.Remove(str.Length - suffix.Length);
            }
            return str;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            if (list == null)
                return true;

            if (list is ICollection<T>)
                return ((ICollection<T>)list).Count == 0;
            else
                return !list.Any();
        }

    }
}
