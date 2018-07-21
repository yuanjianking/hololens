
using System.Collections.Generic;

public class SreachZoufa : BaseZoufa
{
    private CreateZoufa create;
    private EvaluateZoufa evaluate;
    private ChangeQiPan change;
    private MoveData best;

    //无穷大
    private const int INFINITY = 65535;

    public SreachZoufa(FenData fen) : base(fen)
    {
        create = new CreateZoufa(fen);
        evaluate = new EvaluateZoufa(fen);
        change = new ChangeQiPan(fen);
    }

    public MoveData GetBestZoufa()
    {
        //没完 克服水平线效应
        //没完 迭代加深”(Iterative Deepening)
        //没完 Zobrist校验码
        //没完 检查重复局面
        //没完 置换表
        int depth = 5;
        AlphaBeta(-INFINITY, INFINITY, depth);
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
            List<MoveData> list = create.GetZoufaLsit();
            foreach (MoveData m in list)
            {
                change.AddMove(m);
                int val = -AlphaBeta(-beta, -alpha, --level);
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
