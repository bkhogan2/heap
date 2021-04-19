public enum HeapType { Max, Min }
    public class Heap<Key> where Key : IComparable {
        private HeapType _type;
        private int _count;
        private Key[] _heap;

        public Heap(HeapType heapType, int initalCapacity = 10) {
            if (initalCapacity <= 0) throw new ArgumentOutOfRangeException("Capacity should be greater than 0.");
            _heap = new Key[initalCapacity + 1];

            _type = heapType;
        }

        public bool Any() {
            return _count != 0;
        }

        public void Push(Key key) {
            if (_count + 1 >= _heap.Length) {
                // Double the length of the array
                var newHeap = new Key[_count * 2 + 1];
                _heap.CopyTo(newHeap, 0);

                _heap = newHeap;
            }

            _heap[++_count] = key;
            Swim(_count);
        }

        public Key Pop() {
            Key max = _heap[1];
            Exchange(1, _count--);
            Sink(1);
            _heap[_count + 1] = default(Key);
            return max;
        }

        public Key Peek() => _heap[1];

        public int Count => _count;

        // Swim to the top (larger than one or both) of its children's
        private void Swim(int k) {
            // parent of node at k is at k / 2;
            while (k > 1 && LessOrMore(k / 2, k)) {
                Exchange(k, k / 2);
                k = k / 2;
            }
        }

        private void Sink(int k) {
            while (2 * k <= _count) {
                int j = 2 * k;
                if (j < _count && LessOrMore(j, j + 1)) j++;

                if (!LessOrMore(k, j)) break;

                Exchange(k, j);
                k = j;
            }
        }

        private bool LessOrMore(int i, int j) {
            if (_type == HeapType.Max) return _heap[i].CompareTo(_heap[j]) < 0;    // Less
            else return _heap[i].CompareTo(_heap[j]) > 0;    // More
        }

        private void Exchange(int i, int j) {
            var temp = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = temp;
        }
    }
