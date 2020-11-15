using System;
using System.Collections.Generic;

namespace PassKeePerLib.Models
{
    public partial class Enterprises
    {
        public Enterprises()
        {
            EnterpriseWallets = new HashSet<EnterpriseWallets>();
            Workers = new HashSet<Workers>();
        }

        public int Id { get; set; }
        public string EnterpriseName { get; set; }
        public string EnterpriseAddress { get; set; }
        public string EnterpriseEmail { get; set; }

        public virtual ICollection<EnterpriseWallets> EnterpriseWallets { get; set; }
        public virtual ICollection<Workers> Workers { get; set; }
    }
}
