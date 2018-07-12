using System;

public class MoveData : ICloneable {
    //棋盘左上角为0，0
    public PointData start =new PointData();
    public PointData end = new PointData();

    public object Clone()
    {
        var obj = new MoveData();
        obj.start = (PointData) this.start.Clone();
        obj.end = (PointData) this.start.Clone();
        return obj;
    }
}
