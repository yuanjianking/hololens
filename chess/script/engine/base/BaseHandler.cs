using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHandler {
    protected CanjuData canju;
    protected KaijuData kaiju;
    private ExtentData extent;
    protected BaseHandler()
    {
        (new CanjuLoader()).LoadCanju(out canju);
        (new KaijuLoader()).LoadKaiju(out kaiju);
    }

    public virtual ExtentData Extent
    {
        set;
        get;
    }
}
