using System;
using System.Collections.Generic;

namespace PassKeePerLib.Models
{
    public partial class Users
    {
        public Users()
        {
            BrowsingHistory = new HashSet<BrowsingHistory>();
            PersonalWallets = new HashSet<PersonalWallets>();
            PersonalWalletsApprovedUsers = new HashSet<PersonalWalletsApprovedUsers>();
            Workers = new HashSet<Workers>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserlastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }

        public virtual ICollection<BrowsingHistory> BrowsingHistory { get; set; }
        public virtual ICollection<PersonalWallets> PersonalWallets { get; set; }
        public virtual ICollection<PersonalWalletsApprovedUsers> PersonalWalletsApprovedUsers { get; set; }
        public virtual ICollection<Workers> Workers { get; set; }
    }
}
