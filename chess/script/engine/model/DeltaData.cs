public class DeltaData
{
    //棋盘左上角为0，0
    //对象坐标
    public PointData target = new PointData();
    //别腿坐标
    public PointData delta = new PointData();
    //反别腿坐标
    public PointData delta2 = new PointData();

    public DeltaData(int x, int y, int deltax, int deltay)
    {
        target.x = x;
        target.y = y;
        delta.x = deltax;
        delta.y = deltay;
    }


    public DeltaData(int x, int y, int deltax, int deltay, int delta2x, int delta2y)
    {
        target.x = x;
        target.y = y;
        delta.x = deltax;
        delta.y = deltay;
        delta2.x = delta2x;
        delta2.y = delta2y;
    }


    public DeltaData(PointData target, PointData delta)
    {
        this.target = target;
        this.delta = delta;
    }
}
