using System;
using System.Collections.Generic;

namespace PassKeePerLib.Models
{
    public partial class EnterpriseWallets
    {
        public EnterpriseWallets()
        {
            EnterpriseWalletsAdministrators = new HashSet<EnterpriseWalletsAdministrators>();
            EnterpriseWalletsApprovedWorkers = new HashSet<EnterpriseWalletsApprovedWorkers>();
        }

        public int WalletId { get; set; }
        public int EnterpriseId { get; set; }

        public virtual Enterprises Enterprise { get; set; }
        public virtual Wallets Wallet { get; set; }
        public virtual ICollection<EnterpriseWalletsAdministrators> EnterpriseWalletsAdministrators { get; set; }
        public virtual ICollection<EnterpriseWalletsApprovedWorkers> EnterpriseWalletsApprovedWorkers { get; set; }
    }
}
