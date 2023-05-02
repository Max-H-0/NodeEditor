using System;
using System.Collections.Generic;

namespace NodeEditor.Reusable;

public class MultiKeyDictionary<TKey, TValue>
{
    public List<List<TKey>> Keys { get; private set; }
    public List<TValue> Values { get; private set; }


    public void Add(TKey key, TValue value)
    {
        Add(new List<TKey> { key }, value);
    }

    public void Add(List<TKey> keys, TValue value)
    {
        Keys.Add(keys);
        Values.Add(value);
    }


    public void Remove(TKey key)
    {
        for (int i = 0; i < Keys.Count; i++)
        {
            if (Keys[i].Contains(key))
            {
                Keys.RemoveAt(i);
                Values.RemoveAt(i);

                return;
            }
        }

        throw new NullReferenceException($"The key {key} has no entries!");
    }

    public void Remove(TValue value)
    {
        for (int i = 0; i < Keys.Count; i++)
        {
            if (Values[i].Equals(value))
            {
                Keys.RemoveAt(i);
                Values.RemoveAt(i);

                return;
            }
        }

        throw new NullReferenceException($"The value {value} counldnt be found!");
    }


    public TValue GetValue(TKey key)
    {
        for(int i = 0; i < Keys.Count; i++)
        {
            if (Keys[i].Contains(key))
            {
                return Values[i];
            }
        }

        throw new NullReferenceException($"The key { key } has no entries!");
    }
}
