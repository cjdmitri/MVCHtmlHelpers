using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Di.HtmlHelpers
{
     public class ModelUser
     {

          [DisplayName("Id")]
          [HiddenInput]
          [HtmlInput(TypeInput.hidden, IsHidden = true)]
          public int Id { get; set; }
          [MaxLength(100)]
          [Required]
          [DisplayName("Имя пользователя")]
          public string Name { get; set; }
          [MaxLength(50)]
          [Required]
          [DisplayName("Пароль")]
          [HtmlInput(TypeInput.password)]
          public string Password { get; set; }
          /// <summary>
          /// Временный пароль необходим для восстановления пароля
          /// Отправляется на указанный адрес электронной почты
          /// </summary>
          [DisplayName("Временный пароль")]
          public string? TemporaryPassword { get; set; }
          [MaxLength(100)]
          [Required]
          [DisplayName("Email")]
          [HtmlInput(TypeInput.email)]
          public string Email { get; set; }
          /// <summary>
          /// 'true' если адрес подтвержден
          /// </summary>
          [Required]
          [DisplayName("Email подтверждён")]
          [HtmlInput(TypeInput.checkbox)]
          public bool ItsEmailConfirmed { get; set; }
          /// <summary>
          /// Код, высылаемый при регистрации пользователя, для подтверждения адреса элестронной почты
          /// </summary>
          [MaxLength(30)]
          [DisplayName("Код подтверждения Email")]
          public string? EmailConfirmedKey { get; set; }
          [MaxLength(30)]
          [DisplayName("Роль пользователя")]
          public string? Role { get; set; }
          [MaxLength(70)]
          [Display(Name="Дата регистрации")]
          public DateTime DateRegistered { get; set; }
          [MaxLength(70)]
          public string? Status { get; set; }
          /// <summary>
          /// Является ли пользователь заблокированным на сайте
          /// </summary>
          [DisplayName("Пользователь заблокирован")]
          public bool ItsBlocked { get; set; }

          private string PrivateProperty { get; set; }
     }
}
