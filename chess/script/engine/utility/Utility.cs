using System;
using System.Text;
//工具类
public class Utility {

    //
    public static void CheckQizi(FenData fen)
    { 
        //最新走法
        MoveData move = fen.moves[0];
        //当前选择
        PointData point = fen.selected;

        if (point != move.start)
        {
            throw new AppException(ErrorMessage.AE0003);
        }

        if (move.start == move.end)
        {
            throw new AppException(ErrorMessage.AE0004);
        }
        if (!IsOnQiPan(move.end))
        { 
            throw new AppException(ErrorMessage.AE0006);
        }
    }
    

    public static DeltaData GetCheDelta(PointData offset)
    {
        DeltaData delta = null;
        if (offset.x == 0 && offset.y > 0)
        {
            delta = Constant.CheZouFaDelta[0];
        }
        else if (offset.x == 0 && offset.y < 0)
        {
            delta = Constant.CheZouFaDelta[0];
        }
        else if (offset.x > 0 && offset.y == 0)
        {
            delta = Constant.CheZouFaDelta[0];
        }
        else if (offset.x < 0 && offset.y == 0)
        {
            delta = Constant.CheZouFaDelta[0];
        }
        else {
            throw new AppException(ErrorMessage.AE0005);
        }
        return delta;
    }

    public static DeltaData GetPaoDelta(PointData offset)
    {
        DeltaData delta = null;
        if (offset.x == 0 && offset.y > 0)
        {
            delta = Constant.PaoZouFaDelta[0];
        }
        else if (offset.x == 0 && offset.y < 0)
        {
            delta = Constant.PaoZouFaDelta[0];
        }
        else if (offset.x > 0 && offset.y == 0)
        {
            delta = Constant.PaoZouFaDelta[0];
        }
        else if (offset.x < 0 && offset.y == 0)
        {
            delta = Constant.PaoZouFaDelta[0];
        }
        else
        {
            throw new AppException(ErrorMessage.AE0005);
        }
        return delta;
    }

    public static Boolean IsOnQiPan(PointData point)
    {

        if (point.x < 0 || point.x > 8)
        {
            return false;
        }

        if (point.y < 0 || point.y > 9)
        {
            return false;
        }
        return true;
    }

    public static Boolean IsRedGuoHe(PointData point)
    {
        if (point.y < 5)
            return true;
        else
            return false;
    }

    public static Boolean IsBlackGuoHe(PointData point)
    {
        if (point.y > 4)
            return true;
        else
            return false;
    }

    public static Boolean IsRedYingZhang(PointData point)
    {
        if (point.x >= 3 && point.x <=5 && point.y >=  7 && point.y <= 9)
            return true;
        else
            return false;
    }

    public static Boolean IsBlackYingZhang(PointData point)
    {
        if (point.x >= 3 && point.x <= 5 && point.y >= 0 && point.y <= 2)
            return true;
        else
            return false;
    }


    public static PointData GetShuaiPoint(Qizi[,] chess)
    {
        //帅x3 - 5,y7 - 9
        for (int x = 3; x <= 5; x++)
        {
            for (int y = 7; y <= 9; y++)
            {
                if (chess[x, y] == Qizi.REDSHUAI)
                {
                    return new PointData(x,y);
                }
            }
        }
        return new PointData();
    }

    public static PointData GetJiangPoint(Qizi[,] chess)
    {
        //将x3 - 5，y0 - 2
        for (int x = 3; x <= 5; x++)
        {
            for (int y = 0; y <= 2; y++)
            {
                if (chess[x, y] == Qizi.REDSHUAI)
                {
                    return new PointData(x, y);
                }
            }
        }
        return new PointData();
    }

    public static Boolean CanEat(FenData fen,MoveData move)
    {

        if (fen[move.end] == Qizi.KONGZI)
        {
            return false;
        }
        return true;
    }

    public static String ConvertQiPanToString(FenData fen)
    {
        StringBuilder builder = new StringBuilder();
        //x0-8,y0-9(黑方：0-4，红方：5-9)
        for (int x = 0; x <= 8; x++)
        {
            for (int y = 0; y <= 9; y++)
            {
                Qizi qizi = fen.chess[x, y];

                builder.Append(GetQiZiString(qizi));
            }
        }

        return builder.ToString();
    }

    public static String GetQiZiString(Qizi qizi)
    {
        String str = "";
        switch (qizi)
        {
            case Qizi.REDSHUAI:
                str = "K";
                break;
            case Qizi.BLACKJIANG:
                str = "k";
                break;
            case Qizi.REDSHI:
                str = "A";
                break;
            case Qizi.BLACKSHI:
                str = "a";
                break;
            case Qizi.REDXIANG:
                str = "B";
                break;
            case Qizi.BLACKXIANG:
                str = "b";
                break;
            case Qizi.REDMA:
                str = "N";
                break;
            case Qizi.BLACKMA:
                str = "n";
                break;
            case Qizi.REDCHE:
                str = "R";
                break;
            case Qizi.BLACKCHE:
                str = "r";
                break;
            case Qizi.REDPAO:
                str = "C";
                break;
            case Qizi.BLACKPAO:
                str = "c";
                break;
            case Qizi.REDBING:
                str = "p";
                break;
            case Qizi.BLACKZU:
                str = "p";
                break;

            case Qizi.KONGZI:
            default:
                str = "0";
                break;
        }
        return str;
    }
}
