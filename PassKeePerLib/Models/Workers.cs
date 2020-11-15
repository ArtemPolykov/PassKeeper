using System;
using System.Collections.Generic;

namespace PassKeePerLib.Models
{
    public partial class Workers
    {
        public Workers()
        {
            EnterpriseWalletsAdministrators = new HashSet<EnterpriseWalletsAdministrators>();
            EnterpriseWalletsApprovedWorkers = new HashSet<EnterpriseWalletsApprovedWorkers>();
        }

        public int UserId { get; set; }
        public int EnterpriseId { get; set; }

        public virtual Enterprises Enterprise { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<EnterpriseWalletsAdministrators> EnterpriseWalletsAdministrators { get; set; }
        public virtual ICollection<EnterpriseWalletsApprovedWorkers> EnterpriseWalletsApprovedWorkers { get; set; }
    }
}
