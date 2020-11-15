using System;
using System.Collections.Generic;

namespace PassKeePerLib.Models
{
    public partial class PersonalWalletsApprovedUsers
    {
        public int WalletId { get; set; }
        public int UserId { get; set; }

        public virtual Users User { get; set; }
        public virtual Wallets Wallet { get; set; }
    }
}
