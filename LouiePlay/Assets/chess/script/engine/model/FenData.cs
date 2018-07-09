using System;
using System.Collections.Generic;

public class FenData {
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

    public Qizi this[int x, int y]
    {
        set {
            chess[x, y] = value;
        }
        get
        {
            return chess[x, y];
        }

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
}
