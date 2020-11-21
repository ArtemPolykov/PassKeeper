using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PassKeePerLib.Models
{
    public partial class Users : IdentityUser<int>
    {
        public Users()
        {
            BrowsingHistory = new HashSet<BrowsingHistory>();
            PersonalWallets = new HashSet<PersonalWallets>();
            PersonalWalletsApprovedUsers = new HashSet<PersonalWalletsApprovedUsers>();
            Workers = new HashSet<Workers>();
        }

        public virtual ICollection<BrowsingHistory> BrowsingHistory { get; set; }
        public virtual ICollection<PersonalWallets> PersonalWallets { get; set; }
        public virtual ICollection<PersonalWalletsApprovedUsers> PersonalWalletsApprovedUsers { get; set; }
        public virtual ICollection<Workers> Workers { get; set; }
    }
}
