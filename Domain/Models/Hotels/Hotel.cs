using Domain.BaseEntity;
using Domain.Models.HotelGalleries;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Hotels
{
    public class Hotel : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone {  get; set; }
        public string Description { get; set; }
        public int Star {  get; set; }
        public bool State {  get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public ICollection<User> Users {  get; set; }
        public ICollection<HotelGallery> HotelGalleries { get; set; }

    }
}
