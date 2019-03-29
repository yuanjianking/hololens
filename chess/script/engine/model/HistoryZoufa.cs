using System;
using System.Collections.Generic;

public class HistoryZoufa {
    private Dictionary<String, MoveData> zouf = new Dictionary<String, MoveData>();
    public Dictionary<String, MoveData> ZouFa
    {
        set
        {
            zouf = value;
        }
        get { return zouf; }
    }

    public MoveData this[string key]
    {
        set { }
        get { return ZouFa[key]; }
    }
}
