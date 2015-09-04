using System;
using System.Collections.Generic;

namespace Bazik.Models
{
    public sealed class MsSqlSession
    {
        public short session_id;
        public DateTime login_time;
        public int request_num;
        public int explicit_tran_num;
        public int locks_num;
        public int waiting_locks_num;
        public String host_name;
        public String program_name;
        public String login_name;
        public String status;
        public int cpu_time;
        public int memory_usage;
        public long reads;
        public long logical_reads;
        public long writes;
        public short transaction_isolation_level;

        public IEnumerable<MsSqlRequest> Requests { get; set; }

        public IEnumerable<MsSqlConnection> Connections { get; set; }
    }
}
