using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.MediatR
{
    public class BaseCommandRequest : IPresetModel
    {
        [BindNever]
        public int? UserId {  get; set; }

        //یعنی هیچوقت این پراپرتی ها از سمت کاربر بایند نشن
        [BindNever]
        public bool IsAdmin { get; set; }

        [BindNever]
        public int? HotelId { get; set; }
    }
}
