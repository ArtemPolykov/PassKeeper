using System;
using System.Collections.Generic;

namespace PassKeePerLib.Models
{
    public partial class Accounts
    {
        public Accounts()
        {
            BrowsingHistory = new HashSet<BrowsingHistory>();
        }

        public int Id { get; set; }
        public int WalletId { get; set; }
        public string AccountName { get; set; }
        public string AccountaDdress { get; set; }
        public string AccountLogin { get; set; }
        public string AccountPassword { get; set; }

        public virtual Wallets Wallet { get; set; }
        public virtual ICollection<BrowsingHistory> BrowsingHistory { get; set; }
    }
}
