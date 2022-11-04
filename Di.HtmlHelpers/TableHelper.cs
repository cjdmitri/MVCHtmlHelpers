using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Di.HtmlHelpers
{
     public static class TableHelper
     {
          /// <summary>
          /// Создание гибкой, настраиваемой таблицы
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="html"></param>
          /// <param name="listItems">Список данных</param>
          /// <param name="tableClass">CSS класс таблицы</param>
          /// <param name="idBody">Идентификатор тела таблицы</param>
          /// <param name="addHeader">Необходимо ли отображать строку заголовков столбцов</param>
          /// <param name="showSearch">Показывать строку поиска</param>
          /// <param name="replaceStrings">Какие строковые значения в ячейках данных следует заменить</param>
          /// <param name="excludeFields">Какие свойства модели следует исключить</param>
          /// <returns></returns>
          public static HtmlString CreateTable<T>(this IHtmlHelper html, 
             List<T> listItems,
             string? tableClass = null,
             string? idBody = null,
             bool addHeader = true,
             bool showSearch = true,
             Dictionary<string, string>? replaceStrings = null,
             string[]? excludeFields = null)
          {
               //Получаем все свойства модели
               Type type = typeof(T);
               PropertyInfo[] propertyInfos = Common.GetProperties<T>(excludeFields);

               //Устанавливаем класс для таблицы
               string cls = "";
               if (tableClass != null)
                    cls = $"class=\"{tableClass}\"";

               //Контейнер для таблицы
               string result = $"<div style=\"overflow-x:auto\"><table {cls}>";

               //Устанавливаем заголовки для колонок
               if (addHeader)
               {
                    result += "<thead><tr>";
                    foreach (var propertyInfo in propertyInfos)
                         result += $@"<th>{Common.GetPropertyName(propertyInfo)}</th>";
                    result += "</tr>";

                    //Если необходимо показать строку поиска
                    if (showSearch)
                    {
                         result += "<tr>";
                         foreach (var propertyInfo in propertyInfos)
                              result += $"<th><input type=\"text\" class=\"form-control form-control-sm\"></th>";
                         result += "</tr>";
                    }
                    result += "</thead>";
               }

               //Формируем тело таблицы
               if (idBody == null)
                    result += "<tbody>";
               else
                    result += $"<tbody id=\"{idBody}\">";
               foreach (var item in listItems)
               {
                    result += "<tr>";
                    foreach (var propertyInfo in propertyInfos)
                    {
                         object? value = propertyInfo.GetValue(item);
                         //
                         if (replaceStrings != null)
                         {
                              foreach (var replItem in replaceStrings)
                              {
                                   if (value != null)
                                   {
                                        if (replItem.Key == Convert.ToString(value))
                                        {
                                             value = replItem.Value;
                                             break;
                                        }
                                   }
                              }
                         }
                         result += $@"<td>{value}</td>";

                    }
                    result += "</tr>";
               }

               result += "</tbody></table></div>";
               return new HtmlString(result);
          }
     }
}
