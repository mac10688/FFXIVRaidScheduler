using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Entities
{
    public class User : IdentityUser
    {
        public Player Player { get; set; }
    }
}
