
using UnityEngine;

//走法线路生成
public class GenerateMoveLine : BaseZoufa
{
    public GenerateMoveLine(FenData fen) : base(fen)
    {

    }

    public ResultData GetMoveLine()
    {
        ResultData result = new ResultData();

        Qizi qizi = fen.GetCurrentQizi();
        switch (qizi)
        {
            case Qizi.REDSHUAI:
            case Qizi.BLACKJIANG:
                result = GetJiangShuai();
                break;
            case Qizi.REDSHI:
            case Qizi.BLACKSHI:
                result = GetShi();
                break;
            case Qizi.REDXIANG:
            case Qizi.BLACKXIANG:
                result = GetXiang();
                break;
            case Qizi.REDMA:
            case Qizi.BLACKMA:
                result = GetMa();
                break;
            case Qizi.REDCHE:
            case Qizi.BLACKCHE:
                result = GetChe();
                break;
            case Qizi.REDPAO:
            case Qizi.BLACKPAO:
                result = GetPao();
                break;
            case Qizi.REDBING:
            case Qizi.BLACKZU:
                result = GetBingZu();
                break;

            case Qizi.KONGZI:
            default:
                break;
        }
        return result;
    }

    public ResultData GetChe()
    {
        ResultData result = new ResultData();
        //选中的坐标
        PointData start = fen.selected;
        int len = Constant.CheZouFaDelta.Length;

        for (int i = 0; i < len; i++)
        {
            PointData target = Constant.CheZouFaDelta[i].target;
            for (int m = 1; m < Constant.QIPANBIANJIE; m++)
            {
                PointData p = start + target * m;

                if (!Utility.IsOnQiPan(p))
                {
                    break;
                }
                else if (fen[p] == Qizi.KONGZI)
                {
                    result.moves.Add(new MoveData(start, p));
                }
                else if ((fen.current & (int)fen[p]) == 0x0000)
                {
                    result.moves.Add(new MoveData(start, p));
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        result.result = true;    
        return result;

    }

    public ResultData GetMa()
    {
        ResultData result = new ResultData();
        //选中的坐标
        PointData start = fen.selected;
        int len = Constant.MaZouFaDelta.Length;

        for (int i = 0; i < len; i++)
        {
            PointData target = Constant.MaZouFaDelta[i].target;
            PointData delta = Constant.MaZouFaDelta[i].delta;
            PointData p = start + target;
            if (!Utility.IsOnQiPan(p))
            {
                continue;
            }
            else if (fen[start + delta] != Qizi.KONGZI)
            {
                continue;
            }
            else if (fen[p] == Qizi.KONGZI)
            {
                result.moves.Add(new MoveData(start, p));

            }
            else if ((fen.current & (int)fen[p]) == 0x0000)
            {
                result.moves.Add(new MoveData(start, p));
            }
            else
            {
                continue;
            }
        }
        result.result = true;
        return result;
    }

    public ResultData GetPao()
    {
        ResultData result = new ResultData();
        //选中的坐标
        PointData start = fen.selected;
        int len = Constant.PaoZouFaDelta.Length;

        for (int i = 0; i < len; i++)
        {
            int poatai = 0;
            PointData target = Constant.PaoZouFaDelta[i].target;
            for (int m = 1; m < Constant.QIPANBIANJIE; m++)
            {
                PointData p = start + target * m;

                if (!Utility.IsOnQiPan(p))
                {
                    break;
                }
                else if (fen[p] == Qizi.KONGZI)
                {
                    if(poatai == 0) result.moves.Add(new MoveData(start, p));
                }
                else if ((fen.current & (int)fen[p]) == 0x0000)
                {
                    if (poatai == 1)
                    {
                        result.moves.Add(new MoveData(start, p));
                        break;
                    }
                    poatai++;
                }
                else
                {
                    poatai++;
                    if (poatai > 1)
                        break;

                }
            }
        }
        result.result = true;
        return result;

    }

    public ResultData GetXiang()
    {
        ResultData result = new ResultData();

        //选中的坐标
        PointData start = fen.selected;
        int len = Constant.XiangZouFaDelta.Length;

        for (int i = 0; i < len; i++)
        {
            PointData target = Constant.XiangZouFaDelta[i].target;
            PointData delta = Constant.XiangZouFaDelta[i].delta;
            PointData p = start + target;
            if (!Utility.IsOnQiPan(p))
            {
                continue;
            }
            else if ((Constant.RED == fen.current && Utility.IsRedGuoHe(p)) ||
                     (Constant.BLACK == fen.current && Utility.IsBlackGuoHe(p)))
            {
                continue;
            }
            else if (fen[start + delta] != Qizi.KONGZI)
            {
                continue;
            }
            else if (fen[p] == Qizi.KONGZI)
            {
                result.moves.Add(new MoveData(start, p));
            }
            else if ((fen.current & (int)fen[p]) == 0x0000)
            {
                result.moves.Add(new MoveData(start, p));
            }
            else
            {
                continue;
            }
        }
        result.result = true;
        return result;
    }


    public ResultData GetShi()
    {
        ResultData result = new ResultData();
        //选中的坐标
        PointData start = fen.selected;
        int len = Constant.ShiZouFaDelta.Length;

        for (int i = 0; i < len; i++)
        {
            PointData target = Constant.ShiZouFaDelta[i].target;
            PointData p = start + target;
            if (!Utility.IsOnQiPan(p))
            {
                continue;
            }
            else if ((Constant.RED == fen.current && !Utility.IsRedYingZhang(p)) ||
                     (Constant.BLACK == fen.current && !Utility.IsBlackYingZhang(p)))
            {
                continue;
            }
            else if (fen[p] == Qizi.KONGZI)
            {
                result.moves.Add(new MoveData(start, p));
            }
            else if ((fen.current & (int)fen[p]) == 0x0000)
            {
                result.moves.Add(new MoveData(start, p));
            }
            else
            {
                continue;
            }
        }
        result.result = true;
        return result;
    }

    public ResultData GetJiangShuai()
    {
        ResultData result = new ResultData();
         //选中的坐标
        PointData start = fen.selected;
        int len = Constant.JiangShuaiZouFaDelta.Length;

        for (int i = 0; i < len; i++)
        {
            PointData target = Constant.JiangShuaiZouFaDelta[i].target;
            PointData p = start + target;
            if (!Utility.IsOnQiPan(p))
            {
                continue;
            }
            else if ((Constant.RED == fen.current && !Utility.IsRedYingZhang(p)) ||
                     (Constant.BLACK == fen.current && !Utility.IsBlackYingZhang(p)))
            {
                continue;
            }
            else if (fen[p] == Qizi.KONGZI)
            {
                result.moves.Add(new MoveData(start, p));
            }
            else if ((fen.current & (int)fen[p]) == 0x0000)
            {
                result.moves.Add(new MoveData(start, p));
            }
            else
            {
                continue;
            }
        }
        result.result = true;
        return result;
    }

    public ResultData GetBingZu()
    { 
        ResultData result = new ResultData();
        //选中的坐标
        PointData start = fen.selected;
        int len = 1;
        if ((Constant.RED == fen.current && Utility.IsRedGuoHe(start)) ||
                     (Constant.BLACK == fen.current && Utility.IsBlackGuoHe(start)))
        {
                len = 3;
        }
        for (int i = 0; i < len; i++)
        {
            PointData target;
            if (Constant.RED == fen.current)
            {
                target = Constant.BingZouFaDelta[i].target;
            }
            else
            {
                target = Constant.ZuZouFaDelta[i].target;
            }
            PointData p = start + target;
            if (!Utility.IsOnQiPan(p))
            {
                continue;
            }
            else if (fen[p] == Qizi.KONGZI)
            {
                result.moves.Add(new MoveData(start, p));
            }
            else if ((fen.current & (int)fen[p]) == 0x0000)
            {
                result.moves.Add(new MoveData(start, p));
            }
            else
            {
                continue;
            }
        }
        result.result = true;
        return result;
    }

}
