using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TI_Grupo7.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public int is_worker { get; set; }

    public string FirstName { get; set; }

    public string Surname { get; set; }

    public int NIF { get; set; }

    public string Address { get; set; }
}

