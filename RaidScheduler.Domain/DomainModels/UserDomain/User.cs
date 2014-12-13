using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels.UserDomain
{
    public class User : IdentityUser
    {
        public string PreferredTimezone { get; set; }
    }
}
