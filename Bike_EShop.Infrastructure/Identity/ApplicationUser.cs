using Bike_EShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Infrastructure.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public int CustomerId { get; set; }
        [PersonalData] 
        public Customer Customer { get; set; }
    }
}
