using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Users
{ 
    //میتونی پکیح رفرنس رو از اینفرا بدی به دامین که وقتی پکیح رو نصب کردی بخونه
    //**********************************************************************************
    public class User : IdentityUser<int>
    {
        public string? FirstName {  get; set; }  
        public string? LastName { get; set; }

        public int? HotelId { get; set; }


        public Hotels.Hotel Hotel { get; set; }

    }
}
