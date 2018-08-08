using System;
using System.Collections;

public class ZiliProvider
{
    //棋子子力
    //车
    private const int ZHILI100 = 100;
    //进营兵
    private const int ZHILI80 = 80;
    //马炮
    private const int ZHILI60 = 60;
    //过河兵
    private const int ZHILI40 = 40;
    //仕相
    private const int ZHILI30 = 30;
    //兵将
    private const int ZHILI10 = 10;


    private static Hashtable tabe = new Hashtable();

    static ZiliProvider()
    {
        tabe.Add(Qizi.BLACKCHE, ZHILI100);
        tabe.Add(Qizi.BLACKMA, ZHILI60);
        tabe.Add(Qizi.BLACKPAO, ZHILI60);
        tabe.Add(Qizi.BLACKSHI, ZHILI30);
        tabe.Add(Qizi.BLACKXIANG, ZHILI30);
        tabe.Add(Qizi.BLACKJIANG, ZHILI10);
        tabe.Add(Qizi.BLACKZU, ZHILI10);

        tabe.Add(Qizi.REDCHE, ZHILI100);
        tabe.Add(Qizi.REDMA, ZHILI60);
        tabe.Add(Qizi.REDPAO, ZHILI60);
        tabe.Add(Qizi.REDSHI, ZHILI30);
        tabe.Add(Qizi.REDXIANG, ZHILI30);
        tabe.Add(Qizi.REDSHUAI, ZHILI10);
        tabe.Add(Qizi.REDBING, ZHILI10);
    }
    
    public static int GetZili(Qizi qizi, PointData point)
    {
        int zili = 0;
        zili = (int)tabe[qizi];
        if (qizi == Qizi.BLACKZU)
        {
            if (Utility.IsBlackGuoHe(point))
            {
                zili = ZHILI40;
            }
            if (Utility.IsRedYingZhang(point))
            {
                zili = ZHILI80;
            }
        }
        if (qizi == Qizi.REDBING)
        {
            if (Utility.IsRedGuoHe(point))
            {
                zili = ZHILI40;
            }
            if (Utility.IsBlackYingZhang(point))
            {
                zili = ZHILI80;
            }
        }
        return zili;
    }
}