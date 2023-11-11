using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JsonHelper
{
    [Serializable]
    public class ArrayOf<T>
    {
        public T[] Array;
    }
}
