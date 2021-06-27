using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Durak
{
    class PlayersCollection : IList<Player>
    {
        private List<Player> players;


        public PlayersCollection(int count)
        {
            players = new List<Player>();
            players.AddRange(new Player[count]);
        }


        public Player this[int index] 
        { get => players[NormalizeIndex(index)];
        set { value.Index = NormalizeIndex(index); players[NormalizeIndex(index)] = value; } }

        public int Count => players.Count;

        public bool IsReadOnly => false;

        public void Add(Player item) =>
            throw new System.InvalidOperationException("This collection has fixed size");

        public void Clear() =>
            throw new System.InvalidOperationException("This collection has fixed size");

        public bool Contains(Player item) => players.Contains(item);

        public void CopyTo(Player[] array, int arrayIndex) => players.CopyTo(array, arrayIndex);

        public IEnumerator<Player> GetEnumerator() => ((IEnumerable<Player>)players).GetEnumerator();

        public int IndexOf(Player item)
        {
            if(Contains(item) == false) return -1;
            else return item.Index;
        }

        public void Insert(int index, Player item) =>
            throw new System.InvalidOperationException("This collection has fixed size");

        public bool Remove(Player item) =>
            throw new System.InvalidOperationException("This collection has fixed size");

        public void RemoveAt(int index) =>
            throw new System.InvalidOperationException("This collection has fixed size"); 

        public Player GetPlayer(IPlayerRecord record) 
        {
            if(record is Player player && Contains(player)) return player;
            else throw new ArgumentException(nameof(record), "Can't get player by this record");
        }

        IEnumerator IEnumerable.GetEnumerator() => players.GetEnumerator();

        private int NormalizeIndex(int index)
        {
            var ret = index % Count;
            if(ret < 0) ret += Count;
            return ret;
        }
    }
}