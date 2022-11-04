using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Di.HtmlHelpers
{
     internal class Common
     {

          /// <summary>
          /// Возвращает все публичные свойства модели
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="excludeFields">Какие свойства модели следует исключить</param>
          /// <returns>Массив доступных публичных свойств модели Т</returns>
          internal static PropertyInfo[] GetProperties<T>(string[]? excludeFields = null)
          {
               Type type = typeof(T);
               PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

               if (excludeFields != null)
               {
                    foreach (var exField in excludeFields)
                    {
                         PropertyInfo? pinfo = propertyInfos.FirstOrDefault(x => x.Name == exField);
                         if (pinfo != null)
                              propertyInfos = propertyInfos.Except(new PropertyInfo[] { pinfo }).ToArray();
                    }
               }

               return propertyInfos;
          }
     }
}
