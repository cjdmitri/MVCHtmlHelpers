using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Reflection;
using Di.HtmlHelpers.Extensions;
using System.Text.Encodings.Web;
using System.Web;
using System.Diagnostics;

namespace Di.HtmlHelpers
{
     public static class TableHelper
     {
          private static Stopwatch sw = new Stopwatch();
          private static PropertyInfo[] propertyInfos;

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
          /// <param name="actions">Возможные дополнительные действия со строками. При наличии, показывается в первой колонке dropdown. Где Key - URL, value - текст ссылки</param>
          /// <returns></returns>
          public static HtmlString CreateTable<T>(this IHtmlHelper html,
             List<T> listItems,
             string? tableClass = null,
             string idBody = "tableBody",
             bool addHeader = true,
             bool showSearch = true,
             Dictionary<string, string>? replaceStrings = null,
             string[]? excludeFields = null,
             Dictionary<string, string>? actions = null)
          {
               sw.Start();
               //Получаем все свойства модели
               propertyInfos = Common.GetProperties<T>(excludeFields);

               //Устанавливаем класс для таблицы
               string cls = "";
               if (tableClass != null)
                    cls = $"class=\"{tableClass}\"";

               //Контейнер для таблицы
               string result = $"<div style=\"overflow-x:auto\"><table {cls}>";

               //Генерация заголовка и строки поиска
               result += GenerateHeader(addHeader, showSearch, actions);

               //Формируем тело таблицы
               result += $"<tbody id=\"{idBody}\">";
               foreach (var item in listItems)
               {
                    //Всплывающее меню дополнительных действий
                    result += GenerateDropDownMenuItem(item, actions);
                    result += GenerateRow(item, replaceStrings);
               }
               result += "</tbody></table></div>";

               sw.Stop();
               Console.WriteLine($"Генерация таблицы завершена: {sw.ElapsedMilliseconds} msec.");
               return new HtmlString(result);
          }

          /// <summary>
          /// Генерация заголовка таблицы и при необходимости строки поиска для колонок
          /// </summary>
          /// <param name="addHeader"></param>
          /// <param name="showSearch"></param>
          /// <param name="actions"></param>
          /// <returns></returns>
          private static string GenerateHeader(
               bool addHeader = true,
             bool showSearch = true,
             Dictionary<string, string>? actions = null)
          {
               string result = "";
               //Устанавливаем заголовки для колонок
               if (addHeader)
               {
                    if (actions == null)
                         result += "<thead><tr>";
                    else
                         result += "<thead><tr><th></th>";

                    foreach (var propertyInfo in propertyInfos)
                         result += $@"<th>{propertyInfo.GetDisplayName()}</th>";
                    result += "</tr>";

                    //Если необходимо показать строку поиска
                    if (showSearch)
                    {
                         result += "<tr>";
                         if (actions != null)
                              result += $"<th></th>";
                         for (int i = 0; i < propertyInfos.Length; i++)
                         {
                              result += $"<th><input type=\"text\" class=\"form-control form-control-sm tfilterinput\" data-col=\"{(actions == null ? i : i + 1)}\"></th>";
                         }
                         result += "</tr>";
                    }
                    result += "</thead>";
               }

               return result;
          }


          /// <summary>
          /// Генерация строк таблицы
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="item"></param>
          /// <param name="replaceStrings"></param>
          /// <returns></returns>
          private static string GenerateRow<T>(T item,
               Dictionary<string, string>? replaceStrings = null)
          {
               string result = "";
               foreach (var propertyInfo in propertyInfos)
               {
                    object? value = propertyInfo.GetValue(item);
                    //Заменяем значения свойств, значениями из словаря
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

               return result;
          }

          /// <summary>
          /// Генерация всплывающего меню
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="item"></param>
          /// <param name="actions">Словарь ссылок всплывающего списка</param>
          /// <returns></returns>
          private static string GenerateDropDownMenuItem<T>(T item, Dictionary<string, string>? actions = null)
          {
               string result = "";
               if (actions == null)
                    result += "<tr>";
               else
               {
                    result += $"<td><div class=\"dropdown\">" +
                         $"<a class=\"btn btn-outline-secondary btn-sm dropdown-toggle\" href=\"#\" role=\"button\"  data-bs-toggle=\"dropdown\">" +
                         $"<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"16\" height=\"16\" fill=\"currentColor\" class=\"bi bi-sliders\" viewBox=\"0 0 16 16\">\r\n  <path fill-rule=\"evenodd\" d=\"M11.5 2a1.5 1.5 0 1 0 0 3 1.5 1.5 0 0 0 0-3zM9.05 3a2.5 2.5 0 0 1 4.9 0H16v1h-2.05a2.5 2.5 0 0 1-4.9 0H0V3h9.05zM4.5 7a1.5 1.5 0 1 0 0 3 1.5 1.5 0 0 0 0-3zM2.05 8a2.5 2.5 0 0 1 4.9 0H16v1H6.95a2.5 2.5 0 0 1-4.9 0H0V8h2.05zm9.45 4a1.5 1.5 0 1 0 0 3 1.5 1.5 0 0 0 0-3zm-2.45 1a2.5 2.5 0 0 1 4.9 0H16v1h-2.05a2.5 2.5 0 0 1-4.9 0H0v-1h9.05z\"/>\r\n</svg>" +
                         $"</a>" +
                         $"<ul class=\"dropdown-menu\">";
                    foreach (var action in actions)
                    {
                         string actKey = action.Key;
                         //Замена значений в строке URL на значение свойства
                         foreach (var propertyInfo in propertyInfos)
                         {
                              string? propValue = propertyInfo.GetValue(item)?.ToString();
                              string propName = propertyInfo.Name;
                              if (propValue != null)
                                   propValue = HttpUtility.UrlEncode(propValue);

                              actKey = actKey.Replace($"*{propName}*", propValue);
                         }
                         result += $"<li><a class=\"dropdown-item\" href=\"{actKey}\">{action.Value}</a></li>";
                    }
                    result += $"</ul></div>";
               }
               return result;
          }
     }
}
