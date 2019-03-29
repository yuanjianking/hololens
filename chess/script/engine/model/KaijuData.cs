using System;
using System.Collections;
using System.Collections.Generic;

public class KaijuData {
    private Dictionary<String,MoveData> kaiju = new Dictionary<String, MoveData>();
    public Dictionary<String, MoveData> KaiJu
    {
        set
        {
            kaiju = value;
        }
        get { return kaiju; }
    }

    public MoveData this[string key]
    {
        set { }
        get { return KaiJu[key]; }
    }
}
