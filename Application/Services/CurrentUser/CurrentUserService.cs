using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CurrentUser
{
    public class CurrentUserService : ICurrentUserService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
          
        }

        public int? UserId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user != null)
                {
                    var claim = user.FindFirst(ClaimTypes.NameIdentifier);
                    if (claim != null)
                    {
                        if (int.TryParse(claim.Value,out int userId))
                        {

                            return userId;
                        }


                    }

                }
                return null;
            }


        }


        public int? HotelId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user != null)
                {
                    var claim = user.FindFirst("hotelTd");
                    if (claim != null)
                    {
                        if (int.TryParse(claim.Value, out int hotelId))
                        {

                            return hotelId;
                        }
                    }

                }
                return null;
            }


        }




        public bool IsAdmin
        {
            get
            {

                var user = _httpContextAccessor.HttpContext?.User;
                if (user != null)
                {

                    var roleclaim = user.FindFirst(ClaimTypes.Role);
                    //میشد isinrole هم گزاشت
                    if (roleclaim != null && roleclaim.Value == "admin")
                    {
                        return true;
                    }
                }
                return false;
                
            }


        }

    }
}
