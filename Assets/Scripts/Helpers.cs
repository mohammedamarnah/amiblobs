using System.Collections.Generic;
using UnityEngine;

public class Helpers {
  public static float GenerateRandom(float min, float max) {
    // System.Random rnd = new System.Random();
    // double range = (double) max - (double) min;
    // double sample = rnd.NextDouble();
    // double scaled = (sample * range) + float.MinValue;
    // System.Random rand = new System.Random();
    // Random.InitState((int) System.DateTime.Now.Ticks + Random.Range(1, 10));
    // return min + ((int)rand.NextDouble() )
    return Random.Range(min, max);
  }

  public static int GenerateRandom(int min, int max) {
      // System.Random rand = new System.Random();
      // int val = rand.Next();
      // int res = min + (val % (max - min + 1));
      // while (res == 0) {
      //   val = rand.Next();
      //   res = min + (val % (max - min + 1));
      // }
      return Random.Range(min, max);
  }

  public static int GCD(int a, int b) {
    return b == 0 ? a : GCD(b, a % b);
  }
  
  public static void Rotate<T>(List<T> arr, int d, int n) {
    int gcd = GCD(d, n);
    for (int i = 0; i < gcd; i++) {
      T temp = arr[i];
      int j = i;
      while (true) {
        int k = j + d;
        if (k >= n) {
            k = k - n;
        }
        if (k == i) {
            break;
        }
        arr[j] = arr[k];
        j = k;
      }
      arr[j] = temp;
    }
  }
}