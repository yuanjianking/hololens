using System;

public class Constant {
    //x0-8,y0-9(黑方：0-4，红方：5-9)
    public readonly static Qizi[,] CHESS = {
        { Qizi.BLACKCHE,  Qizi.KONGZI, Qizi.KONGZI,  Qizi.BLACKZU,Qizi.KONGZI,Qizi.KONGZI,Qizi.REDBING,Qizi.KONGZI,Qizi.KONGZI, Qizi.REDCHE},
        { Qizi.BLACKMA,   Qizi.KONGZI, Qizi.BLACKPAO,Qizi.KONGZI, Qizi.KONGZI,Qizi.KONGZI,Qizi.KONGZI, Qizi.REDPAO,Qizi.KONGZI, Qizi.REDMA},
        { Qizi.BLACKXIANG,Qizi.KONGZI, Qizi.KONGZI,  Qizi.BLACKZU,Qizi.KONGZI,Qizi.KONGZI,Qizi.REDBING,Qizi.KONGZI,Qizi.KONGZI, Qizi.REDXIANG},
        { Qizi.BLACKSHI,  Qizi.KONGZI, Qizi.KONGZI,  Qizi.KONGZI, Qizi.KONGZI,Qizi.KONGZI,Qizi.KONGZI, Qizi.KONGZI,Qizi.KONGZI, Qizi.REDSHI},
        { Qizi.BLACKJIANG,Qizi.KONGZI, Qizi.KONGZI,  Qizi.BLACKZU,Qizi.KONGZI,Qizi.KONGZI,Qizi.REDBING,Qizi.KONGZI,Qizi.KONGZI, Qizi.REDSHUAI},
        { Qizi.BLACKSHI,  Qizi.KONGZI, Qizi.KONGZI,  Qizi.KONGZI, Qizi.KONGZI,Qizi.KONGZI,Qizi.KONGZI, Qizi.KONGZI,Qizi.KONGZI, Qizi.REDSHI},
        { Qizi.BLACKXIANG,Qizi.KONGZI, Qizi.KONGZI,  Qizi.BLACKZU,Qizi.KONGZI,Qizi.KONGZI,Qizi.REDBING,Qizi.KONGZI,Qizi.KONGZI, Qizi.REDXIANG},
        { Qizi.BLACKMA,   Qizi.KONGZI, Qizi.BLACKPAO,Qizi.KONGZI, Qizi.KONGZI,Qizi.KONGZI,Qizi.KONGZI, Qizi.REDPAO,Qizi.KONGZI, Qizi.REDMA},
        { Qizi.BLACKCHE,  Qizi.KONGZI, Qizi.KONGZI,  Qizi.BLACKZU,Qizi.KONGZI,Qizi.KONGZI,Qizi.REDBING,Qizi.KONGZI,Qizi.KONGZI, Qizi.REDCHE}
    };

    //车 x固定y轴0-9，y固定x轴0-8 
    public readonly static DeltaData[] CheZouFaDelta = { new DeltaData(0, 1, 0, 1), new DeltaData(0, -1, 0, -1), new DeltaData(1, 0, 1, 0), new DeltaData(-1, 0, -1, 0)};

    //马 x0-8,y0-9
    public readonly static DeltaData[] MaZouFaDelta = { new DeltaData(-2,-1,-1,0,-1,-1), new DeltaData(-1,-2,0,-1,-1,-1), new DeltaData(1, -2,0,-1,1,-1),
                                            new DeltaData(2,-1,1,0,1,-1), new DeltaData(2,1,1,0,1,1), new DeltaData(1, 2,0,1,1,1),
                                            new DeltaData(-1,2,0,1,-1,1), new DeltaData(-2,1,-1,0,-1,1)};

    //炮 x固定y轴0-9，y固定x轴0-8 
    public readonly static DeltaData[] PaoZouFaDelta = CheZouFaDelta;

    //象x0-8，y0-4  相 x0-8,y5-9  
    public readonly static DeltaData[] XiangZouFaDelta = { new DeltaData(-2, -2, -1, -1), new DeltaData(2, -2, 1, -1),
                                             new DeltaData(-2, 2, -1, 1), new DeltaData(2, 2, 1, 1)};

    //士x3-5，y0-2  仕x3-5,y7-9 
    public readonly static DeltaData[] ShiZouFaDelta = { new DeltaData(-1, -1, -1, -1), new DeltaData(1, -1, 1, -1),
                                             new DeltaData(-1, 1, -1, 1), new DeltaData(1, 1, 1, 1)};

    //将x3-5，y0-2 帅x3-5,y7-9 
    public readonly static DeltaData[] JiangShuaiZouFaDelta = { new DeltaData(-1, 0, -1, 0), new DeltaData(0, -1,0, -1),
                                             new DeltaData(0, 1, 0, 1), new DeltaData(1, 0, 1, 0) };

    //卒y>4 可以左右走x0-8
    public readonly static DeltaData[] ZuZouFaDelta = {  new DeltaData(0, 1, 0, 1), new DeltaData(-1, 0,-1, 0),
                                             new DeltaData(1, 0, 1, 0)};

    //兵 y<5可以左右走x0-8
    public readonly static DeltaData[] BingZouFaDelta = {   new DeltaData(0, -1, 0, -1), new DeltaData(-1, 0,-1, 0),
                                             new DeltaData(1, 0, 1, 0) };

    //对弈两方
    public readonly static int RED = 0x0001;
    public readonly static int BLACK= 0x0002;
    //对弈双方MASK
    public readonly static int QIZIGAOLIANG = 0xFF00;

    //棋盘边界线
    public readonly static int QIPANBIANJIE = 10;

}

