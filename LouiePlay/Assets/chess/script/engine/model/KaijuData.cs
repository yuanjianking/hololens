using System;
using System.Collections.Generic;

public class KaijuData {
    public List<String> kaiju = new List<String>();
    public List<String> KaiJu
    {
        set
        {
            kaiju = value;
        }
        get { return kaiju; }
    }

    public String this[int index]
    {
        set { }
        get { return KaiJu[index]; }
    }
}
