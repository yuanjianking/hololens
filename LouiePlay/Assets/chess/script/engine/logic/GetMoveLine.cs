
public class GetMoveLine : BaseZoufa
{

    public GetMoveLine(FenData fen) : base(fen)
    {

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
            for (int m = 1; m < 10; m++)
            {
                PointData p = start + target * m;

                if (!Utility.IsOnQiPan(p))
                {
                    break;
                }
                else if (fen[p] == Qizi.KONGZI)
                {
                    fen[p] = Qizi.ZHANWEI;
                }
                else if ((Constant.RED.Equals(fen.current) && Utility.IsBlack(fen[p])) ||
                      (Constant.BLACK.Equals(fen.current) && Utility.IsRed(fen[p])))
                {
                    fen[p] = fen[p] + 100;
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        result.result = true;
        result.pgn.fen = fen;
    
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
                fen[p] = Qizi.ZHANWEI;
            }
            else if ((Constant.RED.Equals(fen.current) && Utility.IsBlack(fen[p])) ||
                  (Constant.BLACK.Equals(fen.current) && Utility.IsRed(fen[p])))
            {
                fen[p] = fen[p] + 100;
            }
            else
            {
                continue;
            }
        }
        result.result = true;
        result.pgn.fen = fen;
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
            for (int m = 1; m < 10; m++)
            {
                PointData p = start + target * m;

                if (!Utility.IsOnQiPan(p))
                {
                    break;
                }
                else if (fen[p] == Qizi.KONGZI && poatai == 0)
                {
                    fen[p] = Qizi.ZHANWEI;
                }
                else if ((Constant.RED.Equals(fen.current) && Utility.IsBlack(fen[p])) ||
                      (Constant.BLACK.Equals(fen.current) && Utility.IsRed(fen[p])))
                {
                    if (poatai == 1)
                    {
                        fen[p] = fen[p] + 100;
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
        result.pgn.fen = fen;
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
            else if ((Constant.RED.Equals(fen.current) && Utility.IsRedGuoHe(p)) ||
                     (Constant.BLACK.Equals(fen.current) && Utility.IsBlackGuoHe(p)))
            {
                continue;
            }
            else if (fen[start + delta] != Qizi.KONGZI)
            {
                continue;
            }
            else if (fen[p] == Qizi.KONGZI)
            {
                fen[p] = Qizi.ZHANWEI;
            }
            else if ((Constant.RED.Equals(fen.current) && Utility.IsBlack(fen[p])) ||
                (Constant.BLACK.Equals(fen.current) && Utility.IsRed(fen[p])))
            {
                fen[p] = fen[p] + 100;
            }
            else
            {
                continue;
            }
        }
        result.result = true;
        result.pgn.fen = fen;
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
            else if ((Constant.RED.Equals(fen.current) && !Utility.IsRedYingZhang(p)) ||
                     (Constant.BLACK.Equals(fen.current) && !Utility.IsBlackYingZhang(p)))
            {
                continue;
            }
            else if (fen[p] == Qizi.KONGZI)
            {
                fen[p] = Qizi.ZHANWEI;
            }
            else if ((Constant.RED.Equals(fen.current) && Utility.IsBlack(fen[p])) ||
              (Constant.BLACK.Equals(fen.current) && Utility.IsRed(fen[p])))
            {
                fen[p] = fen[p] + 100;
            }
            else
            {
                continue;
            }
        }
        result.result = true;
        result.pgn.fen = fen;
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
            else if ((Constant.RED.Equals(fen.current) && !Utility.IsRedYingZhang(p)) ||
                     (Constant.BLACK.Equals(fen.current) && !Utility.IsBlackYingZhang(p)))
            {
                continue;
            }
            else if (fen[p] == Qizi.KONGZI)
            {
                fen[p] = Qizi.ZHANWEI;
            }
            else if ((Constant.RED.Equals(fen.current) && Utility.IsBlack(fen[p])) ||
              (Constant.BLACK.Equals(fen.current) && Utility.IsRed(fen[p])))
            {
                fen[p] = fen[p] + 100;
            }
            else
            {
                continue;
            }
        }
        result.result = true;
        result.pgn.fen = fen;
        return result;
    }

    public ResultData GetBingZu()
    {
        ResultData result = new ResultData();
        //选中的坐标
        PointData start = fen.selected;
        int len = 1;
        if ((Constant.RED.Equals(fen.current) && Utility.IsRedGuoHe(start)) ||
                     (Constant.BLACK.Equals(fen.current) && Utility.IsBlackGuoHe(start)))
        {
                len = 3;
        }
        for (int i = 0; i < len; i++)
        {
            PointData target;
            if (Constant.RED.Equals(fen.current))
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
                fen[p] = Qizi.ZHANWEI;
            }
            else if ((Constant.RED.Equals(fen.current) && Utility.IsBlack(fen[p])) ||
                 (Constant.BLACK.Equals(fen.current) && Utility.IsRed(fen[p])))
            {
                fen[p] = fen[p] + 100;
            }
            else
            {
                continue;
            }
        }
        result.result = true;
        result.pgn.fen = fen;
        return result;
    }

}
