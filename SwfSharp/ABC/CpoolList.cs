using System.Collections;
using System.Collections.Generic;

namespace SwfSharp.ABC
{
    internal class CpoolList<T> : IList<T>
    {
        private readonly T _zeroItem;
        private readonly List<T> _backingList;

        public CpoolList(T zeroItem, List<T> backingList)
        {
            _zeroItem = zeroItem;
            _backingList = backingList;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ActualListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _backingList.Add(item);
        }

        public void Clear()
        {
            _backingList.Clear();
        }

        public bool Contains(T item)
        {
            return (_zeroItem.Equals(item) || _backingList.Contains(item));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            array[arrayIndex] = _zeroItem;
            _backingList.CopyTo(array, arrayIndex + 1);
        }

        public bool Remove(T item)
        {
            return _backingList.Remove(item);
        }

        public int Count
        {
            get { return _backingList.Count + 1; }
        }

        public bool IsReadOnly
        {
            get { return ((IList<T>) _backingList).IsReadOnly; }
        }
        public int IndexOf(T item)
        {
            if (item.Equals(_zeroItem))
            {
                return 0;
            }
            return _backingList.IndexOf(item) + 1;
        }

        public void Insert(int index, T item)
        {
            _backingList.Insert(index - 1, item);
        }

        public void RemoveAt(int index)
        {
            _backingList.RemoveAt(index - 1);
        }

        public T this[int index]
        {
            get { return index == 0 ? _zeroItem : _backingList[index - 1]; }
            set { _backingList[index - 1] = value; }
        }

        private class ActualListEnumerator : IEnumerator<T>
        {
            private readonly CpoolList<T> _list;
            private int _index;
            private T _current;

            public ActualListEnumerator(CpoolList<T> list)
            {
                _list = list;
            }

            public void Dispose()
            {}

            public bool MoveNext()
            {
                if (_index >= _list.Count)
                {
                    _current = default(T);
                    _index = _list.Count;
                    return false;
                }
                _current = _list[_index];
                ++_index;
                return true;
            }

            public void Reset()
            {
                _index = 0;
                _current = default(T);
            }

            public T Current
            {
                get { return _current; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }
        }
    }
}