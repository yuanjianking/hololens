using System;
using System.Collections.Generic;

public class CanjuData {
    private Dictionary<String, MoveData> canju = new Dictionary<String, MoveData>();
    public Dictionary<String, MoveData> CanJu
    {
        set
        {
            canju = value;
        }
        get { return canju; }
    }

    public MoveData this[string key]
    {
        set { }
        get { return CanJu[key]; }
    }
}
