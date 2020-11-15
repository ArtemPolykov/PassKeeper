using System;
using System.Collections.Generic;

namespace PassKeePerLib.Models
{
    public partial class BrowsingHistory
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }

        public virtual Accounts Account { get; set; }
        public virtual Users User { get; set; }
    }
}
