using System.Collections.Generic;

namespace Durak
{
    class RankTable
    {
        private List<RankRecord> records = new List<RankRecord>();


        public IReadOnlyCollection<RankRecord> Records => records;


        public void AddRecord(RankRecord record)
        {
            record.Place = records.Count + 1;
            records.Add(record);
        }
    }
}