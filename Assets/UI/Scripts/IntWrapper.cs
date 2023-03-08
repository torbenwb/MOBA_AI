using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IntWrapper
{
    [SerializeField] int value;
    [SerializeField] int min;
    [SerializeField] int max;
    [SerializeField] public List<GameObject> listeners;

    public int Value
    {
        get => value;
        set 
        {
            this.value = Mathf.Clamp(value, min, max);
            for(int i = 0; i < listeners.Count; i++) listeners[i].GetComponent<IIntListener>().IntUpdate(this); 
        }
    }
    public int Max => max;
    public int Min => min;

    public static IntWrapper operator+(IntWrapper a, int b)
    {
        a.Value += b;
        return a;
    }

    public static IntWrapper operator-(IntWrapper a, int b)
    {
        a.Value -= b;
        return a;
    }

    public static bool operator == (IntWrapper a, int b) => (a.Value == b);
    public static bool operator != (IntWrapper a, int b) => (a.Value != b);
    public static bool operator > (IntWrapper a, int b) => (a.value > b);
    public static bool operator >= (IntWrapper a, int b) => (a.value >= b);
    public static bool operator < (IntWrapper a, int b) => (a.value < b);
    public static bool operator <= (IntWrapper a, int b) => (a.value <= b);
}
