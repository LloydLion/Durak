using System.Collections.Generic;

namespace DurakGame
{
    public class RankTable
    {
        private readonly List<RankRecord> records = new();


        public IReadOnlyCollection<RankRecord> Records => records;


        public void AddRecord(RankRecord record)
        {
            record.Place = records.Count + 1;
            records.Add(record);
        }
    }
}