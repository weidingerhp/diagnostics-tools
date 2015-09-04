using System;
using System.Collections.Generic;

namespace Bazik.Models
{
    public class MsSqlDatabaseAnalysis
    {
        public class UnusedIndex
        {
            public String status;
            public String table;
            public String index;
            public long reads;
            public long writes;
            public bool is_disabled;
        }

        public class IndexPhysicalStats
        {
            public String table;
            public String index;
            public String index_type_desc;
            public byte index_depth;
            public double avg_fragmentation_in_percent;
            public double avg_fragment_size_in_pages;
            public long page_count;
            public double avg_page_space_used_in_percent;
            public long record_count;
            public double avg_record_size_in_bytes;
            public String alloc_unit_type_desc;
        }

        public String srv;
        public String db;

        public IEnumerable<UnusedIndex> UnusedIndexes { get; set; }

        public DateTime? LastStatsDate { get; set; }
        public IEnumerable<IndexPhysicalStats> IndexesPhysicalStats { get; set; }
    }
}
