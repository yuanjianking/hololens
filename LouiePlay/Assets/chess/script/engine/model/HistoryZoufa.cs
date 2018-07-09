using System.Collections;
using System.Collections.Generic;

public class HistoryZoufa {
    public List<FenData> history = new List<FenData>();
    public List<FenData> History
    {
        set
        {
            history = value;
        }
        get { return history; }
    }

    public FenData this[int index]
    {
        set { }
        get { return History[index]; }
    }
}
