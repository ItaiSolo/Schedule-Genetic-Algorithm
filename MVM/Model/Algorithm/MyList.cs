using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Dynamic array call MyList that was made t replace the use of ArrayList
public class MyList<T> : IEnumerable<T>
{
    private T[] data;
    private int size = 0;
    private int capacity;
    public int Size { get { return size; } }
    public bool IsEmpty { get { return size == 0; } }

    //init MyList
    public MyList(int initialCapacity = 8)
    {
        if (initialCapacity < 1) initialCapacity = 1;
        capacity = initialCapacity;
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

    // get value of index
    public T GetAt(int index)
    {
        ThrowIfIndexOutOfRange(index);
        return data[index];
    }

    // get index of value
    public int IndexOf(T value)
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
    //set value at index
    public void SetAt(T newElement, int index)
    {
        ThrowIfIndexOutOfRange(index);
        data[index] = newElement;
    }
    // add at the end of the list
    public void Add(T newElement)
    {
        if (size == capacity)
        {
            Resize();
        }
        data[size++] = newElement;
    }

    //add at index
    public void InsertAt(T newElement, int index)
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

    //deletes at index
    public void DeleteAt(int index)
    {
        for (int i = index; i < size - 1; i++)
        {
            data[i] = data[i + 1];
        }
        size--;
    }
    //finds and deletes by value in MyList
    public void Delete(T value)
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

    // check if contains
    public bool Contains(T value)
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

    //Resizes the array to twice its current size when it is full
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

    // method to copy MyList data into a new MyList
    public MyList<T> Copy()
    {
        var newList = new MyList<T>()
        {
            data = new T[data.Length],
            size = this.size
        };

        Array.Copy(data, newList.data, size);
        return newList;
    }

    // Added method to throw an exception if index is out of range
    private void ThrowIfIndexOutOfRange(int index)
    {
        if (index > size - 1 || index < 0)
        {
            throw new ArgumentOutOfRangeException(string.Format("The current size of the array is {0}", size));
        }
    }

    // Added method to get MyList as an IEnumerable
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