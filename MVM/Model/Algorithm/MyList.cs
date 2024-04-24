using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


    public class MyList<T> : IEnumerable<T>
        //מערך דינאמי לא רשימה
    {
        private T[] data;
        private int size = 0;
        private int capacity;
       
        public MyList(int initialCapacity = 8)
        {
            if (initialCapacity < 1) initialCapacity = 1;
            this.capacity = initialCapacity;
            data = new T[initialCapacity];
        }


        // Adding a constructor that takes an IEnumerable<T> and turn it into MyList
        public MyList(IEnumerable<T> collection) : this(collection.Count())
        {
            foreach (T item in collection)
            {
                Add(item);
            }
        }

        public int Size { get { return size; } }
        public bool IsEmpty { get { return size == 0; } }

        public T GetAt(int index) // get value of index
        {
            ThrowIfIndexOutOfRange(index);
            return data[index];
        }


        public int IndexOf(T value) // get index of value
        {
            for (int i = 0; i < size; i++)
            {
                if (data[i].Equals(value))
                {
                    return i;
                }
            }
            return -1;// not found
        }

        public void SetAt(T newElement, int index) //set
        {
            ThrowIfIndexOutOfRange(index);
            data[index] = newElement;
        }

        public void Add(T newElement) // add at the end
        {
            if (size == capacity)
            {
                Resize();
            }
            data[size++] = newElement;
        }

        public void InsertAt(T newElement, int index) // add at index
        {
            if (size == capacity)
            {
                Resize();
            }
            for (int i = size; i > index; i--)
            {
                data[i] = data[i - 1];
            }
            data[index] = newElement;
            size++;
        }

        public void DeleteAt(int index) // delete at index
        {
            for (int i = index; i < size - 1; i++)
            {
                data[i] = data[i + 1];
            }
            size--;
        }
        public void Delete(T value) // delete by value
        {
            for (int i = 0; i < size; i++)
            {
                if (data[i].Equals(value))
                {
                    DeleteAt(i);
                    break;
                }
            }
        }

        public bool Contains(T value) // check if contains
        {
            for (int i = 0; i < size; i++)
            {
                if (data[i].Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        public void Clear()
        {
            size = 0;
            data = new T[capacity]; // Optionally reset capacity to initialCapacity if desired
        }

        private void Resize()
        {
            capacity *= 2;
            T[] newData = new T[capacity];
            for (int i = 0; i < size; i++)
            {
                newData[i] = data[i];
            }
            data = newData;
        }

        public MyList<T> Copy()
        {
            var newList = new MyList<T>()
            {
                data = new T[this.data.Length],
                size = this.size
            };

            Array.Copy(this.data, newList.data, this.size);
            return newList;
        }

        private void ThrowIfIndexOutOfRange(int index)
        {
            if (index > size - 1 || index < 0)
            {
                throw new ArgumentOutOfRangeException(string.Format("The current size of the array is {0}", size));
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < size; i++)
            {
                yield return data[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        // Added method to sort MyList using a specified comparer
        public void Sort(IComparer<T> comparer)
        {
            Array.Sort(data, 0, size, comparer);
        }
    
}