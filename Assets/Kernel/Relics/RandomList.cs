using System.Collections.Generic;
using System;

public class RandomList<T> : List<Tuple<int, T>>
{
    public Tuple<int,T> GetRandom()
    {
        int totalWeight = 0;
        for(int i = 0; i < this.Count; i++)
        {
            if (this[i].Item1 > 0)
            {
                totalWeight += this[i].Item1;
            }
        }
        int random = UnityEngine.Random.Range(0, totalWeight);
        int sum = 0;
        for (int i = 0; i < Count; i++)
        {
            if(this[i].Item1 > 0)
            {
                sum += this[i].Item1;
                if (random < sum)
                {
                    return this[i];
                }
            }
        }
        return default;
    }

    public RandomList() : base()
    {

    }

    public RandomList(IEnumerable<Tuple<int, T>> ienum):base(ienum)
    {
        
    }

}
