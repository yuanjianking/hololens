public class DeltaData
{
    //棋盘左上角为0，0
    public PointData target = new PointData();
    public PointData delta = new PointData();
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
