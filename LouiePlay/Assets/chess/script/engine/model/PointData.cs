﻿using System;
using System.Collections.Generic;

public class PointData
{
    //棋盘左上角为0，0
    public int x = 0;
    public int y = 0;
    public int z = 0;

    public static PointData Zero
    {
        get { return new PointData(); }
        private set {}
    }

    public PointData()
    {}

    public PointData(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public PointData(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
    public static PointData operator *(PointData point1, PointData point2)
    {
        PointData p = new PointData();
        p.x = point1.x * point2.x;
        p.y = point1.y * point2.y;
        p.y = point1.z * point2.z;
        return p;
    }

    public static PointData operator *(PointData point1, int scale)
    {
        PointData p = new PointData();
        p.x = point1.x * scale;
        p.y = point1.y * scale;
        p.y = point1.z * scale;
        return p;
    }

    public static PointData operator +(PointData point1, PointData point2)
    {
        PointData p = new PointData();
        p.x = point1.x + point2.x;
        p.y = point1.y + point2.y;
        p.y = point1.z + point2.z;
        return p;
    }


    public static PointData operator -(PointData point1, PointData point2)
    {
        PointData p = new PointData();
        p.x = point1.x - point2.x;
        p.y = point1.y - point2.y;
        p.y = point1.z - point2.z;
        return p;
    }


    public static bool operator ==(PointData point1, PointData point2)
    {
        if (point1.x == point2.x && point1.y == point2.y && point1.z == point2.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public static bool operator !=(PointData point1, PointData point2)
    {
        if (point1.x != point2.x || point1.y != point2.y || point1.z != point2.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
