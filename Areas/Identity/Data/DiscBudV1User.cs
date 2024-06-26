using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DiscBudV1.Models;

namespace DiscBudV1.Areas.Identity.Data;

// Add profile data for application users by adding properties to the DiscBudV1User class
public class DiscBudV1User : IdentityUser
{
    public List<Disc> Discs { get; set; }
}

