using System;
using System.Collections.Generic;

namespace PassKeePerLib.Models
{
    public partial class EnterpriseWalletsAdministrators
    {
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
        public int WalletId { get; set; }

        public virtual EnterpriseWallets EnterpriseWallets { get; set; }
        public virtual Workers Workers { get; set; }
    }
}
