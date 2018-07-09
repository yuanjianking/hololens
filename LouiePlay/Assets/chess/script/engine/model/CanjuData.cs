using System;
using System.Collections.Generic;

public class CanjuData {
    private List<String> chanju= new List<String>();

    public List<String> ChanJu
    {
        set {
            chanju = value;
        }
        get { return chanju; }
    }

    public String this[int index]
    {
        set { }
        get { return ChanJu[index]; }
    }
}
