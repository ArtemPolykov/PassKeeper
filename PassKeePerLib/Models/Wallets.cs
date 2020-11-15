using System;
using System.Collections.Generic;

namespace PassKeePerLib.Models
{
    public partial class Wallets
    {
        public Wallets()
        {
            Accounts = new HashSet<Accounts>();
            PersonalWalletsApprovedUsers = new HashSet<PersonalWalletsApprovedUsers>();
        }

        public int Id { get; set; }
        public string WalletName { get; set; }

        public virtual EnterpriseWallets EnterpriseWallets { get; set; }
        public virtual PersonalWallets PersonalWallets { get; set; }
        public virtual ICollection<Accounts> Accounts { get; set; }
        public virtual ICollection<PersonalWalletsApprovedUsers> PersonalWalletsApprovedUsers { get; set; }
    }
}
