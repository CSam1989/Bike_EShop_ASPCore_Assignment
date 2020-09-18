using Bike_EShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Bike_EShop.Domain.Identity
{
    public class ApplicationUser: IdentityUser
    {
        // Geen mooie oplossing, nu zit er een (Identity) dependency in mijn Domain Layer
        // Maar ik zie geen andere oplossing om ApplicationUser & Customer te linken
        // Eerst geprobeerd om Customer aan AspNetUsers te koppelen maar dan kan ik niet aan de Customers info via HttpContext
        [PersonalData] 
        public Customer Customer { get; set; }
    }
}
