
public class CheckChess :BaseZoufa{
    private ChangeQiPan change;

    public CheckChess(FenData fen) : base(fen)
    {
        change = new ChangeQiPan(fen);
    }

    #region 判断将军
    public void CheckJiangjun()
    {
        change.Move();
        try
        {
            PointData shuai = Utility.GetShuaiPoint(fen.chess);
            PointData jiang = Utility.GetJiangPoint(fen.chess);
            //将帅在同一列
            if (shuai.x == jiang.x)
            {
                for (int y = jiang.y + 1; y < shuai.y; y++)
                {
                    if (fen.chess[jiang.x, y] != Qizi.KONGZI)
                    {
                        goto JiangShuaiOK;
                    }
                }
                throw new AppException(ErrorMessage.AE0009);
            }
            JiangShuaiOK:
            if (Constant.RED.Equals(fen.current))
            {
                CheJiangJun(shuai);
                MaJiangJun(shuai);
                PaoJiangJun(shuai);
                BingZuJiangJun(shuai);
            }
            else
            {
                CheJiangJun(jiang);
                MaJiangJun(jiang);
                PaoJiangJun(jiang);
                BingZuJiangJun(jiang);
            }
        }
        catch (AppException ae)
        {
            throw ae;
        }
        finally
        {
            change.GoBack();
        }
    }
    private void CheJiangJun(PointData point)
    {
        int len = Constant.CheZouFaDelta.Length;
        for (int i = 0; i < len; i++) {
            PointData target = Constant.CheZouFaDelta[i].target;
            for (int m = 1; m < 10; m++)
            {
                PointData p = point + target * m;
                if (!Utility.IsOnQiPan(p))
                {
                    break;
                }
                else if (fen[p] == Qizi.KONGZI)
                {
                    continue;
                }
                else
                {
                    if ((Constant.RED.Equals(fen.current) && fen[p] == Qizi.BLACKCHE) ||
                        (Constant.BLACK.Equals(fen.current) && fen[p] == Qizi.REDCHE))
                    { 
                        throw new AppException(ErrorMessage.AE0008);
                    }
                    break;
                }
            }
        }
    }

    private void MaJiangJun(PointData point)
    {
        int len = Constant.MaZouFaDelta.Length;
        for (int i = 0; i < len; i++)
        {
            PointData p = point + Constant.MaZouFaDelta[i].target;
            if (Utility.IsOnQiPan(p))
            {
                if ((Constant.RED.Equals(fen.current) && fen[p] == Qizi.BLACKMA) ||
                            (Constant.BLACK.Equals(fen.current) && fen[p] == Qizi.REDMA))
                {
                     PointData delta2 = Constant.MaZouFaDelta[i].delta2;
                    if (fen[point+ delta2] == Qizi.KONGZI)
                    {
                        throw new AppException(ErrorMessage.AE0008);
                    }
                }
            }
        }
    }

    private void PaoJiangJun(PointData point)
    {
        int len = Constant.PaoZouFaDelta.Length;
        for (int i = 0; i < len; i++)
        {
            PointData target = Constant.PaoZouFaDelta[i].target;
            int paotai = 0;
            for (int m = 1; m < 10; m++)
            {
                PointData p = point + target * m;
                if (!Utility.IsOnQiPan(p))
                {
                    break;
                }
                else if (fen[p] == Qizi.KONGZI)
                {
                    continue;
                }
                else
                {
                    if (paotai == 1) { 
                        if ((Constant.RED.Equals(fen.current) && fen[p] == Qizi.BLACKPAO) ||
                        (Constant.BLACK.Equals(fen.current) && fen[p] == Qizi.REDPAO))
                        {
                            throw new AppException(ErrorMessage.AE0008);
                        }
                    }
                    paotai++;
                    if(paotai > 1)
                        break;
                }
            }
        }
    }
    private void BingZuJiangJun(PointData point)
    {
        ResultData result = new ResultData();
        for (int i = 0; i < 3; i++)
        {
            if ((Constant.RED.Equals(fen.current) && fen[point + Constant.BingZouFaDelta[i].target] == Qizi.BLACKZU) ||
                (Constant.BLACK.Equals(fen.current) && fen[point + Constant.ZuZouFaDelta[i].target] == Qizi.REDBING) )

            {
                throw new AppException(ErrorMessage.AE0008);
            }
        }
    }
    #endregion

    #region 判断棋子走法
    public ResultData CheckChe()
    {
        ResultData result = new ResultData();
       //最新走法
        MoveData move = fen[0];
        //没按直线移动
        if ((move.end - move.start) == PointData.Zero)
        {
            throw new AppException(ErrorMessage.AE0005);
        }
        //自己部队碰撞
        else if ((Constant.RED.Equals(fen.current) && Utility.IsRed(fen[move.end])) ||
                    (Constant.BLACK.Equals(fen.current) && Utility.IsBlack(fen[move.end])))
        {
            throw new AppException(ErrorMessage.AE0007);
        }
        else
        {
            PointData offset = move.end - move.start;
            DeltaData delta = Utility.GetCheDelta(offset);
            PointData  p =  move.start + delta.target;
            while (p == move.end )
            {
                if (fen[p] != Qizi.KONGZI)
                {
                    //被阻挡
                    throw new AppException(ErrorMessage.AE0007);
                }
                p = p + delta.target;
            }

            if (fen[move.end] == Qizi.KONGZI)
            {
                //空白移动
                result.result = true;
                result.pgn.fen = fen;
            }
            else if ((Constant.RED.Equals(fen.current) && Utility.IsBlack(fen[move.end])) ||
                (Constant.BLACK.Equals(fen.current) && Utility.IsRed(fen[move.end])))

            {
                result.result = true;
                result.caneat = true;
                result.pgn.fen = fen;
            }
        }
    
        return result;
    }

    public ResultData CheckMa()
    {
        ResultData result = new ResultData();
        //最新走法
        MoveData move = fen[0];
        int len = Constant.MaZouFaDelta.Length;

        for (int i = 0; i < len; i++)
        {
            PointData p = move.start + Constant.MaZouFaDelta[i].target;
            if (p == move.end)
            {
                //马腿
                if (fen[move.start + Constant.MaZouFaDelta[i].delta] != Qizi.KONGZI)
                {
                    throw new AppException(ErrorMessage.AE0007);
                }
                if (fen[move.end] == Qizi.KONGZI)
                {
                    //空白移动
                    result.result = true;
                    result.pgn.fen = fen;
                }
                else if ((Constant.RED.Equals(fen.current) && Utility.IsBlack(fen[move.end])) ||
                      (Constant.BLACK.Equals(fen.current) && Utility.IsRed(fen[move.end])))
                {
                    result.result = true;
                    result.caneat = true;
                    result.pgn.fen = fen;
                }
                else
                {
                    throw new AppException(ErrorMessage.AE0007);
                }
                break;
            }
        }
        if (!result.result) throw new AppException(ErrorMessage.AE0005);
        return result;
    }

    public ResultData CheckPao()
    {
        ResultData result = new ResultData();
        //最新走法
        MoveData move = fen[0];
        //没按直线移动
        if ((move.end - move.start) == PointData.Zero)
        {
            throw new AppException(ErrorMessage.AE0005);
        }
        //自己部队碰撞
        else if ((Constant.RED.Equals(fen.current) && Utility.IsRed(fen[move.end])) ||
                    (Constant.BLACK.Equals(fen.current) && Utility.IsBlack(fen[move.end])))
        {
            throw new AppException(ErrorMessage.AE0007);
        }
        else
        {
            PointData offset = move.end - move.start;
            DeltaData delta = Utility.GetPaoDelta(offset);
            PointData p = move.start + delta.target;
            int paotai = 0;
            while (p == move.end)
            {
                if (fen[p] != Qizi.KONGZI)
                {
                    paotai++;
                }
                p = p + delta.target;
            }

            if (paotai == 0 && fen[move.end] == Qizi.KONGZI)
            {
                //空白移动
                result.result = true;
                result.pgn.fen = fen;
            }
            else if (paotai == 1 && ((Constant.RED.Equals(fen.current) && Utility.IsBlack(fen[move.end])) ||
                (Constant.BLACK.Equals(fen.current) && Utility.IsRed(fen[move.end]))))

            {
                result.result = true;
                result.caneat = true;
                result.pgn.fen = fen;
            }
            else
            {
               throw new AppException(ErrorMessage.AE0007);
            }
        }
        return result;

    }

    public ResultData CheckXiang()
    {
        ResultData result = new ResultData();
        //最新走法
        MoveData move = fen[0];
        int len = Constant.XiangZouFaDelta.Length;

         if ((Constant.RED.Equals(fen.current) && Utility.IsRedGuoHe(move.end)) ||
                    (Constant.BLACK.Equals(fen.current) && Utility.IsBlackGuoHe(move.end)))
        {     
            //相过河了
            throw new AppException(ErrorMessage.AE0006);
        }

        for (int i = 0; i < len; i++)
        {
            PointData p = move.start + Constant.XiangZouFaDelta[i].target;
            if (p == move.end)
            {
                //相眼
                if (fen[move.start + Constant.XiangZouFaDelta[i].delta] != Qizi.KONGZI)
                {
                    throw new AppException(ErrorMessage.AE0007);
                }
                if (fen[move.end] == Qizi.KONGZI)
                {
                    //空白移动
                    result.result = true;
                    result.pgn.fen = fen;
                }
                else if ((Constant.RED.Equals(fen.current) && Utility.IsBlack(fen[move.end])) ||
                     (Constant.BLACK.Equals(fen.current) && Utility.IsRed(fen[move.end])))
                {
                    result.result = true;
                    result.caneat = true;
                    result.pgn.fen = fen;
                }
                else
                {
                    throw new AppException(ErrorMessage.AE0007);
                }
                break;
            }
        }
        if(!result.result) throw new AppException(ErrorMessage.AE0005);
        return result;
    }

    public ResultData CheckShi()
    {
        ResultData result = new ResultData();
       //最新走法
        MoveData move = fen[0];
        int len = Constant.ShiZouFaDelta.Length;
        if ((Constant.RED.Equals(fen.current) && !Utility.IsRedYingZhang(move.end)) ||
                   (Constant.BLACK.Equals(fen.current) && !Utility.IsBlackYingZhang(move.end)))
        {
            //仕出界了
            throw new AppException(ErrorMessage.AE0006);
        }
        for (int i = 0; i < len; i++)
        {
            PointData p = move.start + Constant.ShiZouFaDelta[i].target;
            if (p == move.end)
            {
                if (fen[move.end] == Qizi.KONGZI)
                {
                    //空白移动
                    result.result = true;
                    result.pgn.fen = fen;
                }
                else if ((Constant.RED.Equals(fen.current) && Utility.IsBlack(fen[move.end])) ||
                    (Constant.BLACK.Equals(fen.current) && Utility.IsRed(fen[move.end])))
                {
                    result.result = true;
                    result.caneat = true;
                    result.pgn.fen = fen;
                }
                else
                {
                    throw new AppException(ErrorMessage.AE0007);
                }
                break;
            }
        }
        if (!result.result) throw new AppException(ErrorMessage.AE0005);
        return result;
    }

    public ResultData CheckJiangShuai()
    {
        ResultData result = new ResultData();
      //最新走法
        MoveData move = fen[0];
        int len = Constant.JiangShuaiZouFaDelta.Length;
        if ((Constant.RED.Equals(fen.current) && !Utility.IsRedYingZhang(move.end)) ||
                 (Constant.BLACK.Equals(fen.current) && !Utility.IsBlackYingZhang(move.end)))
        {
             //出界了
            throw new AppException(ErrorMessage.AE0006);
        }
        for (int i = 0; i < len; i++)
        {
            PointData p = move.start + Constant.JiangShuaiZouFaDelta[i].target;
            if (p == move.end)
            {
                if (fen[move.end] == Qizi.KONGZI)
                {
                    //空白移动
                    result.result = true;
                    result.pgn.fen = fen;
                }
                else if ((Constant.RED.Equals(fen.current) && Utility.IsBlack(fen[move.end])) ||
                    (Constant.BLACK.Equals(fen.current) && Utility.IsRed(fen[move.end])))
                {
                    result.result = true;
                    result.caneat = true;
                    result.pgn.fen = fen;
                }
                else
                {
                    throw new AppException(ErrorMessage.AE0007);
                }
                break;
            }
        }
        if (!result.result) throw new AppException(ErrorMessage.AE0005);
        return result;
    }

    public ResultData CheckBingZu()
    {
        ResultData result = new ResultData();
        //最新走法
        MoveData move = fen[0];
        int len = 1;
        if ((Constant.RED.Equals(fen.current) && Utility.IsRedGuoHe(move.end)) ||
                    (Constant.BLACK.Equals(fen.current) && Utility.IsBlackGuoHe(move.end)))
        {
            len = 3;
        }
      
        for (int i = 0; i < len; i++)
        {
            PointData p;
            if (Constant.RED.Equals(fen.current))
            {
                p = move.start + Constant.BingZouFaDelta[i].target;
            }
            else
            {
                p = move.start + Constant.ZuZouFaDelta[i].target;
            }
            if (p == move.end)
            {
                if (fen[move.end] == Qizi.KONGZI)
                {
                    //空白移动
                    result.result = true;
                    result.pgn.fen = fen;
                }
                else if ((Constant.RED.Equals(fen.current) && Utility.IsBlack(fen[move.end])) ||
                  (Constant.BLACK.Equals(fen.current) && Utility.IsRed(fen[move.end])))
                {
                   
                    result.result = true;
                    result.caneat = true;
                    result.pgn.fen = fen;
                }
                else
                {
                    throw new AppException(ErrorMessage.AE0007);
                }
                break;
            }
        }
        return result;
    }
    #endregion

}
