using System.Collections;

namespace Root.Collections
{
    public sealed class ReverseComparer : IComparer
    {
        private readonly IComparer _innerComparer;

        public ReverseComparer(IComparer innerComparer)
        {
            _innerComparer = innerComparer;
        }

        public int Compare(object x, object y)
        {
            return -1 * _innerComparer.Compare(x, y);
        }
    }
}