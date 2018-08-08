
using System.Collections;

//棋局评估类
public class EvaluateZoufa : BaseZoufa
{
    public EvaluateZoufa(FenData fen) : base(fen)
    {
      
    }

    public int Evaluate()
    {
        int count = 0;
        //子力
        count = GetZiliEvaluate();
        //局势
        count += GetJuShiEvaluate();
        //趋向
        count += GetQuxiangEvaluate();

        return count;
    }

    private int GetZiliEvaluate()
    {
        int zili = 0;
        //x0-8,y0-9(黑方：0-4，红方：5-9)
        for (int x = 0; x <= 8; x++)
        {
            for (int y = 0; y <= 9; y++)
            {
                Qizi qizi = fen.chess[x, y];
                if (qizi != Qizi.KONGZI)
                {
                    PointData point = new PointData(x, y);
                    if (((int)qizi & fen.current) == 0x0000)
                    {
                        //对手棋子
                        zili -= ZiliProvider.GetZili(qizi, point);
                    }
                    else
                    {
                        //自己棋子
                        zili += ZiliProvider.GetZili(qizi, point);
                    }
                }
            }
        }
        return zili;
    }
    
    private int GetJuShiEvaluate()
    {
        //没完 没有形成杀局，困局。还是双方正常交战中。
        //没完 子有没有被围，被困。
        //杀棋分数调整
        return 0;
    }

    private int GetQuxiangEvaluate()
    {
        //没完  接下来会走向败局，还是胜局，或者胶着。
        //没完 反败为胜，由胜转败。
        
        return 0;
    }
}
