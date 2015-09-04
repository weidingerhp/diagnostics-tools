using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bazik.Models
{
    public class MsSqlDashboardModel
    {
        public String ServerName { get; set; }

        public IEnumerable<MsSqlSession> ActiveSesssions { get; set;}

        public IEnumerable<MsSqlRequest> ActiveRequests { get; set; }

        public IEnumerable<MsSqlTransaction> ActiveTransactions { get; set; }

        public IEnumerable<String> DatabaseNames { get; set; }
    }
}
