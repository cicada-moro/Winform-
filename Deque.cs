using System;
using System.Collections;
using System.Collections.Generic;

public class Deque<T> : ICollection<T>
{
    private T[] items;
    private int head; // 指向头部元素的索引  
    private int tail; // 指向尾部元素的下一个位置的索引  
    private int count; // 当前元素数量  


    public bool IsReadOnly => throw new NotImplementedException();

    public int Count { get => count; set => count = value; }

    public Deque(int capacity = 10)
    {
        items = new T[capacity];
    }

    // 扩容  
    private void Resize()
    {
        Array.Resize(ref items, items.Length * 2);
    }

    // 在头部添加元素  
    public void PushFront(T item)
    {
        if (count == items.Length)
        {
            Resize();
        }
        if (head == 0)
        {
            head = items.Length - 1;
        }
        else
        {
            head--;
        }
        items[head] = item;
        count++;
    }

    // 在尾部添加元素  
    public void PushBack(T item)
    {
        if (count == items.Length)
        {
            Resize();
        }
        items[tail] = item;
        tail = (tail + 1) % items.Length;
        count++;
    }

    // 从头部移除元素  
    public T PopFront()
    {
        if (count == 0)
        {
            throw new InvalidOperationException("Deque is empty.");
        }
        T item = items[head];
        head = (head + 1) % items.Length;
        count--;
        return item;
    }

    // 从尾部移除元素  
    public T PopBack()
    {
        if (count == 0)
        {
            throw new InvalidOperationException("Deque is empty.");
        }
        tail--;
        if (tail < 0)
        {
            tail = items.Length - 1;
        }
        T item = items[tail];
        count--;
        return item;
    }

    public T GetFront()
    {
        if (count == 0)
        {
            throw new InvalidOperationException("Deque is empty.");
        }
        return items[head];
    }

    public T GetBack()
    {
        if (count == 0)
        {
            throw new InvalidOperationException("Deque is empty.");
        }
        return items[tail];
    }

    // ... 实现ICollection<T>的其他成员 ...  

    // 示例：测试Deque  


    public void Add(T item)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        // 重置deque的状态  
        head = 0;
        tail = 0;
        count = 0;
    }

    public bool Contains(T item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(T item)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}