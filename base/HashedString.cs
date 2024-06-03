using System.Collections;
using System.Collections.Generic;

namespace DBase
{
    public class HashedString : IEnumerable<char>
    {
        public string Hash { get; }
        public int OriginalLength { get; }

        public HashedString(string hash, int originalLength)
        {
            Hash = hash;
            OriginalLength = originalLength;
        }

        public IEnumerator<char> GetEnumerator()
        {
            foreach (char c in Hash)
            {
                yield return c;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
