using System;

namespace Bazik.Models
{
    public class MsSqlTransaction
    {
        public short session_id;
        public bool is_user_transaction;
        public String host_name;
        public String program_name;
        public String login_name;
        public long transaction_id;
        public DateTime? database_transaction_begin_time;
        public int database_transaction_state;
        public int database_transaction_log_record_count;
        public long database_transaction_log_bytes_used;
        public long database_transaction_log_bytes_reserved;
        public decimal database_transaction_begin_lsn;
        public decimal database_transaction_last_lsn;
    }
}
