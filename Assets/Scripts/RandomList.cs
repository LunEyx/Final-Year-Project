using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomList 
{
    public static List<int> getRandomIntList(int startNum, int endNum, int size)
    {
        int temp = Random.Range(startNum, endNum+1);
        List<int> randInt = new List<int>();
        for (int i = 0; i < size; i++)
        {
            while (randInt.Contains(temp))
                temp = Random.Range(startNum, endNum+1);
            randInt.Add(temp);
        }
        return randInt;
    }
}
