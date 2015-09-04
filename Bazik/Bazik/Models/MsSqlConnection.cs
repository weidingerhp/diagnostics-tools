using System;

namespace Bazik.Models
{
    public class MsSqlConnection
    {
        public int session_id;
        public String text;
        public DateTime connect_time;
        public String protocol_type;
        public int num_reads;
        public int num_writes;
        public String client_net_address;
        public Guid connection_id;
        public int most_recent_session_id;
    }
}
