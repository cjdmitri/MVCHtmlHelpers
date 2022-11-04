using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Di.HtmlHelpers.Extensions
{
     public static class PropertyInfoExtension
     {

          /// <summary>
          /// Проверяет наличие атрибута Required для свойства
          /// </summary>
          /// <param name="property"></param>
          /// <returns></returns>
          public static bool IsRequired(this PropertyInfo property)
          {
               bool result = false;

               object[] attributes = property.GetCustomAttributes(true);
               foreach (object atr in attributes)
               {
                    RequiredAttribute? reqAttribute = atr as RequiredAttribute;
                    if (reqAttribute != null)
                         result = true;
               }

               return result;
          }

          /// <summary>
          /// Возвращает тип поля ввода
          /// </summary>
          /// <param name="property"></param>
          /// <returns></returns>
          public static TypeInput GetHtmlInputType(this PropertyInfo property)
          {
               TypeInput result = TypeInput.text;

               object[] attributes = property.GetCustomAttributes(true);
               foreach (object atr in attributes)
               {
                    HtmlInputAttribute? attribute = atr as HtmlInputAttribute;
                    if (attribute != null)
                         result = attribute.InputType;
               }

               return result;
          }


          /// <summary>
          /// Проверяем атрибуты свойства модели для определения имени.
          /// Если атрибутов нет, то возвращаем название свойства
          /// </summary>
          /// <param name="property"></param>
          /// <returns></returns>
          public static string GetDisplayName(this PropertyInfo property)
          {
               string result = property.Name;

               object[] attributes = property.GetCustomAttributes(true);
               foreach (object atr in attributes)
               {
                    DisplayNameAttribute? displayNameAttribute = atr as DisplayNameAttribute;
                    if (displayNameAttribute != null)
                         result = displayNameAttribute.DisplayName;
                    else
                    {
                         DisplayAttribute? displayAttribute = atr as DisplayAttribute;
                         if (displayAttribute != null)
                              if (displayAttribute.Name != null)
                                   result = displayAttribute.Name;
                    }
               }
               return result;
          }
     }
}
