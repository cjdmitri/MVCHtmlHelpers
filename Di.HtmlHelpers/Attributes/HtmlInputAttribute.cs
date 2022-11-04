using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Di.HtmlHelpers
{
     public enum TypeInput
     {
          button,
          checkbox,
          color,
          date,
          email,
          file,
          hidden,
          image,
          month,
          number,
          password,
          radio,
          range,
          reset,
          search,
          tel,
          text,
          time,
          url,
          week
     }


     [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
     public class HtmlInputAttribute : Attribute
     {
          public TypeInput InputType;
          public string ClassInput;
          public bool IsHidden;


          public HtmlInputAttribute(TypeInput value)
          {
               this.InputType = value;
               ClassInput = "form-control";
               IsHidden = false;
          }
     }
}
