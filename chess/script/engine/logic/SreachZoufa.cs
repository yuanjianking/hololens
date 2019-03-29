
using System;
using System.Collections.Generic;

//筛选走法
public class SreachZoufa : BaseZoufa
{
    private CreateZoufa create;
    private EvaluateZoufa evaluate;
    private ChangeQipan change;
    private MoveData best;

    //无穷大
    private const int INFINITY = 65535;

    public SreachZoufa(FenData fen) : base(fen)
    {
        create = new CreateZoufa(fen);
        evaluate = new EvaluateZoufa(fen);
        change = new ChangeQipan(fen);
    }

    public MoveData GetBestZoufa()
    {
        //没完 克服水平线效应
        //没完 迭代加深”(Iterative Deepening)
        //没完 Zobrist校验码
        //没完 检查重复局面
        //没完 置换表
        //  int depth = 1;
        //    AlphaBeta(-INFINITY, INFINITY, depth);
        List<MoveData> list = create.GetZoufaLsit();
        best = list[new Random().Next(list.Count-1)];
        return best;
    }

    private int AlphaBeta(int alpha, int beta, int level)
    {

        int result = 0;
        if (level == 0)
        {
           result = evaluate.Evaluate();
        }
        else
        {
            int L = level - 1;
            List<MoveData> list = create.GetZoufaLsit();
            foreach (MoveData m in list)
            {
                change.AddMove(m);
                int val = -AlphaBeta(-beta, -alpha, L);
                change.GoBack();
                if (val >= beta)
                {
                    best = m;
                    result = beta;
                    break;
                }
                if (val > alpha)
                {
                    best = m;
                    alpha = val;
                }
            }
        }

        return result;
    }
}
