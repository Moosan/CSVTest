using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPVdata {
    private int ID { get; set; }
    private float Latitude { get; set; }
    private float Longitude { get; set; }
    private float ZeroHeight { get; set; }
    private float PWV { get; set; }

    public GPVdata(string[] data) {
        ID = int.Parse(data[0]);
        Latitude = float.Parse(data[1]);
        Longitude = float.Parse(data[2]);
        ZeroHeight = float.Parse(data[3]);
        PWV = float.Parse(data[4]);
    }

    public override string ToString() {
        return ID + " " + Latitude + " " + Longitude + " " + ZeroHeight + " " + PWV;
    }
}