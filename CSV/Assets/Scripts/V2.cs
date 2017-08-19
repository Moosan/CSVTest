using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V2 {
    public int X { get; set; }
    public int Y { get; set; }
    public V2(float x, float y) {
        X = (int)x;
        Y = (int)y;
    }
    public static V2 operator +(V2 a,V2 b){
        return new V2(a.X+b.X,a.Y+b.Y);
    }

}
