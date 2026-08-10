using Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace AHotel.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine($"{context.Exception},An error occured.");

            //این ایف برای ارور های کاستوممونه
            if (context.Exception is CustomException custom)
            {
                context.Result = new ObjectResult(new
                {
                    Error = "An error while processing your request.",
                    Msg = "Test",
                    Details = custom.Message,

                })
                {
                    StatusCode = 500

                };


            }



            else if (context.Exception is ArgumentException)
            {
                context.Result = new ObjectResult(new
                {
                    Error = "An error while processing your request.",
                    Msg = "Test",
                    Details = "پارامتر اشتباه",

                })
                {
                    StatusCode = 400

                };
            }
           

            else if (context.Exception is ArgumentNullException exception)

            {
                context.Result = new ObjectResult(new
                {
                    Error = "An error while processing your request.",
                    Msg = context.Exception.Message,
                    Details = "نال",

                })
                {
                    StatusCode = 500

                };

            }

            else if (context.Exception is DbUpdateException)

            {
                context.Result = new ObjectResult(new
                {
                    Error = "An error while processing your request.",
                    Msg = context.Exception.Message,
                    Details = "خطایی در دیتابیس رخ داده است",

                })
                {
                    StatusCode = 500

                };

            }

            else
            {

                context.Result = new ObjectResult(new
                {
                    Error = "An error while processing your request.",
                    Msg = context.Exception.Message,
                    Type = context.Exception.GetType(),
                    Details = "خطا",

                })
                {
                    StatusCode = 500

                };

            }

                context.ExceptionHandled = true;


        }
    }
}
