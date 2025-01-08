using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeUp.Domain.Core.Entities.Users
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
    }
}
