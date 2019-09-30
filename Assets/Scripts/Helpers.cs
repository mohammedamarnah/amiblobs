using System.Collections.Generic;

public class Helpers {
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