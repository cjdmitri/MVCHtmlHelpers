using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Di.HtmlHelpers.Extensions;

namespace Di.HtmlHelpers
{
     public static class FormHelpers
     {

          public static HtmlString CreateForm<T>(this IHtmlHelper html,
               T model,
               string method,
               string action,
               string? classInput = null,
               string? classGroup = null,
               string[]? excludeFields = null)
          {
               string result = $"<form method=\"{method}\" action=\"{action}\">";
               //Получаем все свойства модели
               PropertyInfo[] propertyInfos = Common.GetProperties<T>(excludeFields);

               string clsInp = "";
               if (classInput != null)
                    clsInp = $"class=\"{classInput}\"";

               string clsGroup = "";
               if (classGroup != null)
                    clsGroup = $"class=\"{classGroup}\"";

               foreach (var pInfo in propertyInfos)
               {
                    result += $"<div {clsGroup}>";
                    string required = "";
                    if (pInfo.IsRequired())
                         required = "required";

                    switch (pInfo.GetHtmlInputType())
                    {
                         case TypeInput.checkbox:
                              bool value = Convert.ToBoolean(pInfo.GetValue(model));
                              string chec = "";
                              if (value)
                                   chec = "checked";
                              result += $"<input type=\"{pInfo.GetHtmlInputType()}\" id=\"{pInfo.Name}\" name=\"{pInfo.Name}\" {chec}>";
                              result += $"<label for=\"{pInfo.Name}\">{pInfo.GetDisplayName()}</label>";
                              break;
                         default:
                              result += $"<label for=\"{pInfo.Name}\">{pInfo.GetDisplayName()}</label>";
                              result += $"<input type=\"{pInfo.GetHtmlInputType()}\" {clsInp} id=\"{pInfo.Name}\" name=\"{pInfo.Name}\" value=\"{pInfo.GetValue(model)}\" {required}>";
                              break;
                    }

                    result += $"</div>";
               }


               result += "Hello in form!";

               result += "</form>";
               return new HtmlString(result);
          }
     }
}
