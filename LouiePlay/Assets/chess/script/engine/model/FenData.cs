using System;
using System.Collections.Generic;

public class FenData : ICloneable
{
    public Qizi[,] chess = Constant.CHESS;
    public String current = Constant.RED;
    public int noteatcount = 0;
    public int count = 0;
    public List<MoveData> moves = new List<MoveData>();
    public PointData selected = new  PointData();

    public MoveData this[int index]
    {
        set { }
        get { return moves[index]; }
    }

    public Qizi this[PointData point]
    {
        set
        {
            chess[point.x, point.y] = value;
        }
        get
        {
            return chess[point.x, point.y];
        }

    }
    public Qizi GetCurrentQizi()
    {
        return chess[selected.x, selected.y];
    }

    public object Clone()
    {
        var obj = new FenData();
        obj.chess = (Qizi[,])this.chess.Clone();
        obj.current = this.current;
        obj.noteatcount = this.noteatcount;
        obj.count = this.count;
        obj.moves = new List<MoveData>(this.moves.ToArray());
        obj.selected = (PointData)this.selected.Clone();
        return obj;

    }
}
