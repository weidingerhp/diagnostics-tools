using System;
using System.Collections.Generic;

namespace Bazik.Models
{
    public class MsSqlRequest
    {
        public int request_id;
        public String query_text;
        public String query_plan;
        public byte[] plan_handle;
        public String database_name;
        public DateTime start_time;
        public int total_elapsed_time;
        public String status;
        public String command;
        public Guid connection_id;
        public short session_id;
        public short blocking_session_id;
        public String wait_type;
        public long transaction_id;
        public Single  percent_complete;
        public int cpu_time;
        public long reads;
        public long logical_reads;
        public long writes;
        public short transaction_isolation_level;
        public String user;
        public String host_name;

        public String query_plan_html;

        // required by snapshots
        public DateTime snapshot_time;
    }
}
