
public class CheckZoufa : BaseZoufa {
    
    public CheckZoufa(FenData fen) :base(fen)
    {
    }

    public ResultData Check()
    {
        ResultData result = new ResultData();
        Utility.CheckQizi(fen);

        Qizi qizi = fen.GetCurrentQizi();
        switch (qizi)
        {
            case Qizi.REDSHUAI:
                result = CheckRedShuai();
                break;
            case Qizi.REDSHI:
                result = CheckRedShi();
                break;
            case Qizi.REDXIANG:
                result = CheckRedXiang();
                break;
            case Qizi.REDMA:
                result = CheckRedMa();
                break;
            case Qizi.REDCHE:
                result = CheckRedChe();
                break;
            case Qizi.REDPAO:
                result = CheckRedPao();
                break;
            case Qizi.REDBING:
                result = CheckRedBing();
                break;

            case Qizi.BLACKJIANG:
                result = CheckBlackJiang();
                break;
            case Qizi.BLACKSHI:
                result = CheckBlackShi();
                break;
            case Qizi.BLACKXIANG:
                result = CheckBlackXiang();
                break;
            case Qizi.BLACKMA:
                result = CheckBlackMa();
                break;
            case Qizi.BLACKCHE:
                result = CheckBlackChe();
                break;
            case Qizi.BLACKPAO:
                result = CheckBlackPao();
                break;
            case Qizi.BLACKZU:
                result = CheckBlackZu();
                break;

            case Qizi.KONGZI:
            case Qizi.ZHANWEI:
            default:
                break;
        }
        if(result.result)
        { 
           CheckJiangjun();
        }
        return result;
    }
    private void CheckJiangjun()
    {
        Qizi[,] chess = new ChangeQiPan(fen).Change();
        PointData shuai = Utility.GetShuaiPoint(chess);
        PointData jiang = Utility.GetJiangPoint(chess);
        //将帅在同一列
        if (shuai.x == jiang.x)
        {
            for (int y = jiang.y + 1; y < shuai.y; y++)
            {
                if (chess[jiang.x, y] != Qizi.KONGZI)
                {
                    goto JiangShuaiOK;
                }
            }
            throw new AppException(ErrorMessage.AE0009);
        }
        JiangShuaiOK:
        //将被将军
        if (Constant.BLACK.Equals(fen.current))
        {
            //有没有红车
            for (int x = jiang.x + 1; x < 9; x++)
            {
                if (chess[x, jiang.y] == Qizi.REDCHE)
                {
                    throw new AppException(ErrorMessage.AE0008);
                }
                else if (chess[x, jiang.y] != Qizi.KONGZI)
                {
                    break;
                }
            }
            for (int x = jiang.x - 1; x >= 0; x--)
            {
                if (chess[x, jiang.y] == Qizi.REDCHE)
                {
                    throw new AppException(ErrorMessage.AE0008);
                }
                else if (chess[x, jiang.y] != Qizi.KONGZI)
                {
                    break;
                }
            }
            for (int y = jiang.y + 1; y < 10; y++)
            {
                if (chess[jiang.x, y] == Qizi.REDCHE)
                {
                    throw new AppException(ErrorMessage.AE0008);
                }
                else if (chess[jiang.x, y] != Qizi.KONGZI)
                {
                    break;
                }
            }
            for (int y = jiang.y - 1; y >= 0; y--)
            {
                if (chess[jiang.x, y] == Qizi.REDCHE)
                {
                    throw new AppException(ErrorMessage.AE0008);
                }
                else if (chess[jiang.x, y] != Qizi.KONGZI)
                {
                    break;
                }
            }
            //有没有红马
            int len = Constant.MaZouFaDelta.Length;
            for (int i = 0; i < len; i++)
            {
                PointData p = jiang + Constant.MaZouFaDelta[i].target;
                if (Utility.IsOnQiPan(p))
                {
                    if (chess[p.x, p.y] == Qizi.REDMA)
                    {
                        PointData delta2 = Constant.MaZouFaDelta[i].delta2;
                        if (chess[jiang.x+ delta2.x, jiang.y+ delta2.y] == Qizi.KONGZI)
                        {
                            throw new AppException(ErrorMessage.AE0008);
                        }
                    }
                }
            }
            //有没有红炮
            int paotai = 0;          
            for (int x = jiang.x + 1; x < 9; x++)
            {
                if (chess[x, jiang.y] == Qizi.REDPAO)
                {
                    if(paotai == 1)
                        throw new AppException(ErrorMessage.AE0008);
                }
                else if (chess[x, jiang.y] != Qizi.KONGZI)
                {
                    paotai++;
                    if (paotai > 1)
                        break;
                }
            }
            paotai = 0;
            for (int x = jiang.x - 1; x >= 0; x--)
            {
                if (chess[x, jiang.y] == Qizi.REDPAO)
                {
                    if (paotai == 1)
                        throw new AppException(ErrorMessage.AE0008);
                }
                else if (chess[x, jiang.y] != Qizi.KONGZI)
                {
                    paotai++;
                    if (paotai > 1)
                        break;
                }
            }
            paotai = 0;
            for (int y = jiang.y + 1; y < 10; y++)
            {
                if (chess[jiang.x, y] == Qizi.REDPAO)
                {
                    if (paotai == 1)
                        throw new AppException(ErrorMessage.AE0008);
                }
                else if (chess[jiang.x, y] != Qizi.KONGZI)
                {
                    paotai++;
                    if (paotai > 1)
                        break;
                }
            }
            paotai = 0;
            for (int y = jiang.y - 1; y >= 0; y--)
            {
                if (chess[jiang.x, y] == Qizi.REDPAO)
                {
                    if (paotai == 1)
                        throw new AppException(ErrorMessage.AE0008);
                }
                else if (chess[jiang.x, y] != Qizi.KONGZI)
                {
                    paotai++;
                    if (paotai > 1)
                        break; 
                }
            }
            //有没有兵
            if (chess[jiang.x-1, jiang.y] == Qizi.REDBING || chess[jiang.x + 1, jiang.y] == Qizi.REDBING || chess[jiang.x, jiang.y + 1] == Qizi.REDBING)
            {
                throw new AppException(ErrorMessage.AE0008);
            }
        }
        //帅被将军
        else
        {
            //有没有黑车
            for (int x = shuai.x + 1; x < 9; x++)
            {
                if (chess[x, shuai.y] == Qizi.BLACKCHE)
                {
                    throw new AppException(ErrorMessage.AE0008);
                }
                else if (chess[x, shuai.y] != Qizi.KONGZI)
                {
                    break;
                }
            }
            for (int x = shuai.x - 1; x >= 0; x--)
            {
                if (chess[x, shuai.y] == Qizi.BLACKCHE)
                {
                    throw new AppException(ErrorMessage.AE0008);
                }
                else if (chess[x, shuai.y] != Qizi.KONGZI)
                {
                    break;
                }
            }
            for (int y = shuai.y + 1; y < 10; y++)
            {
                if (chess[shuai.x, y] == Qizi.BLACKCHE)
                {
                    throw new AppException(ErrorMessage.AE0008);
                }
                else if (chess[shuai.x, y] != Qizi.KONGZI)
                {
                    break;
                }
            }
            for (int y = shuai.y - 1; y >= 0; y--)
            {
                if (chess[shuai.x, y] == Qizi.BLACKCHE)
                {
                    throw new AppException(ErrorMessage.AE0008);
                }
                else if (chess[shuai.x, y] != Qizi.KONGZI)
                {
                    break;
                }
            }
            //有没有黑马
            int len = Constant.MaZouFaDelta.Length;
            for (int i = 0; i < len; i++)
            {
                PointData p = shuai + Constant.MaZouFaDelta[i].target;
                if (Utility.IsOnQiPan(p))
                {
                    if (chess[p.x, p.y] == Qizi.BLACKMA)
                    {
                        PointData delta2 = Constant.MaZouFaDelta[i].delta2;
                        if (chess[shuai.x + delta2.x, shuai.y + delta2.y] == Qizi.KONGZI)
                        {
                            throw new AppException(ErrorMessage.AE0008);
                        }
                    }
                }
            }
            //有没有黑炮
            int paotai = 0;
            for (int x = shuai.x + 1; x < 9; x++)
            {
                if (chess[x, shuai.y] == Qizi.BLACKPAO)
                {
                    if (paotai == 1)
                        throw new AppException(ErrorMessage.AE0008);
                }
                else if (chess[x, shuai.y] != Qizi.KONGZI)
                {
                    paotai++;
                    if (paotai > 1)
                        break;
                }
            }
            paotai = 0;
            for (int x = shuai.x - 1; x >= 0; x--)
            {
                if (chess[x, shuai.y] == Qizi.BLACKPAO)
                {
                    if (paotai == 1)
                        throw new AppException(ErrorMessage.AE0008);
                }
                else if (chess[x, shuai.y] != Qizi.KONGZI)
                {
                    paotai++;
                    if(paotai > 1)
                        break;
                }
            }
            paotai = 0;
            for (int y = shuai.y + 1; y < 10; y++)
            {
                if (chess[shuai.x, y] == Qizi.BLACKPAO)
                {
                    if (paotai == 1)
                        throw new AppException(ErrorMessage.AE0008);
                }
                else if (chess[shuai.x, y] != Qizi.KONGZI)
                {
                    paotai++;
                    if (paotai > 1)
                        break;
                }
            }
            paotai = 0;
            for (int y = shuai.y - 1; y >= 0; y--)
            {
                if (chess[shuai.x, y] == Qizi.BLACKPAO)
                {
                    if (paotai == 1)
                        throw new AppException(ErrorMessage.AE0008);
                }
                else if (chess[shuai.x, y] != Qizi.KONGZI)
                {
                    paotai++;
                    if (paotai > 1)
                        break;
                }
            }
            //有没有卒
            if (chess[jiang.x - 1, jiang.y] == Qizi.BLACKZU || chess[jiang.x + 1, jiang.y] == Qizi.BLACKZU || chess[jiang.x, jiang.y - 1] == Qizi.BLACKZU)
            {
                throw new AppException(ErrorMessage.AE0008);
            }
        }
    }


    private ResultData CheckRedChe()
    {
        ResultData result = new ResultData();

        if (Constant.RED.Equals(fen.current))
        {
            //最新走法
            MoveData move = fen[0];

            if ((move.end - move.start) == PointData.Zero)
            {
                throw new AppException(ErrorMessage.AE0005);
            }
            else if (Utility.IsRed(fen[move.end]))
            {
                throw new AppException(ErrorMessage.AE0007);
            }
            else
            {
                PointData start = move.start;
                DeltaData delta = Utility.GetCheDelta(move.end - move.start);
                while (true)
                {
                    start = start + delta.target;
                    if (start != move.end)
                    {
                        if (fen[start] != Qizi.KONGZI)
                        {
                            //被阻挡
                            throw new AppException(ErrorMessage.AE0007);
                        }
                    }
                    else
                    {
                        if (fen[start] == Qizi.KONGZI)
                        {
                            //空白移动
                            result.result = true;
                            result.pgn.fen = fen;
                        }
                        else if (Utility.IsBlack(fen[start]))
                        {
                            //黑子吃掉
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
            }
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData CheckRedMa()
    {
        ResultData result = new ResultData();

        if (Constant.RED.Equals(fen.current))
        {
            //最新走法
            MoveData move = fen[0];
            int len = Constant.MaZouFaDelta.Length;
         
            for (int i = 0; i < len; i++)
            {
               PointData p = move.start + Constant.MaZouFaDelta[i].target;
                if (move.end == p)
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
                    else if (Utility.IsBlack(fen[move.end]))
                    {
                        //黑子吃掉
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData CheckRedPao()
    {
        ResultData result = new ResultData();

        if (Constant.RED.Equals(fen.current))
        {
            //最新走法
            MoveData move = fen[0];

            if ((move.end - move.start) == PointData.Zero)
            {
                throw new AppException(ErrorMessage.AE0005);
            }
            else if (Utility.IsRed(fen[move.end]))
            {
                throw new AppException(ErrorMessage.AE0007);
            }
            if (Utility.IsBlack(fen[move.end]))
            {
                PointData start = move.start;
                DeltaData delta = Utility.GetCheDelta(move.end - move.start);
                int paotai = 0;
                while (true)
                {
                    start = start + delta.target;
                    if (start != move.end)
                    {
                        if (fen[start] != Qizi.KONGZI)
                        {
                            paotai++;
                        }
                    }
                    else
                    {
                        if (paotai == 1)
                        {
                            //黑子吃掉
                            result.result = true;
                            result.caneat = true;
                            result.pgn.fen = fen;
                        }
                        else
                        {
                            //被阻挡
                            throw new AppException(ErrorMessage.AE0007);
                        }
                        break;
                    }
                }          
            }
            else
            {
                PointData start = move.start;
                DeltaData delta = Utility.GetCheDelta(move.end - move.start);
                while (true)
                {
                    start = start + delta.target;
                    if (start != move.end)
                    {
                        if (fen[start] != Qizi.KONGZI)
                        {
                            //被阻挡
                            throw new AppException(ErrorMessage.AE0007);
                        }
                    }
                    else
                    {
                        //空白移动
                        result.result = true;
                        result.pgn.fen = fen;
                        break;
                    }
                }
            }
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;

    }

    private ResultData CheckRedXiang()
    {
        ResultData result = new ResultData();

        if (Constant.RED.Equals(fen.current))
        {
            //最新走法
            MoveData move = fen[0];
            int len = Constant.XiangZouFaDelta.Length;

            if (Utility.IsRedGuoHe(move.end))
            { 
                //相过河了
                throw new AppException(ErrorMessage.AE0006);
            }

            for (int i = 0; i < len; i++)
            {
                PointData p = move.start + Constant.XiangZouFaDelta[i].target;
                if (move.end == p)
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
                    else if (Utility.IsBlack(fen[move.end]))
                    {
                        //黑子吃掉
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }


    private ResultData CheckRedShi()
    {
        ResultData result = new ResultData();

        if (Constant.RED.Equals(fen.current))
        {
            //最新走法
            MoveData move = fen[0];
            int len = Constant.ShiZouFaDelta.Length;
            if (!Utility.IsRedYingZhang(move.end))
            {
                //仕出界了
                throw new AppException(ErrorMessage.AE0006);
            }
            for (int i = 0; i < len; i++)
            {
                PointData p = move.start + Constant.ShiZouFaDelta[i].target;
                if (move.end == p)
                {
                    if (fen[move.end] == Qizi.KONGZI)
                    {
                        //空白移动
                        result.result = true;
                        result.pgn.fen = fen;
                    }
                    else if (Utility.IsBlack(fen[move.end]))
                    {
                        //黑子吃掉
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData CheckRedShuai()
    {
        ResultData result = new ResultData();

        if (Constant.RED.Equals(fen.current))
        {
            //最新走法
            MoveData move = fen[0];
            int len = Constant.JiangShuaiZouFaDelta.Length;
            if (!Utility.IsRedYingZhang(move.end))
            {
                //帅出界了
                throw new AppException(ErrorMessage.AE0006);
            }
            for (int i = 0; i < len; i++)
            {
                PointData p = move.start + Constant.JiangShuaiZouFaDelta[i].target;
                if (move.end == p)
                {
                    if (fen[move.end] == Qizi.KONGZI)
                    {
                        //空白移动
                        result.result = true;
                        result.pgn.fen = fen;
                    }
                    else if (Utility.IsBlack(fen[move.end]))
                    {
                        //黑子吃掉
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData CheckRedBing()
    {
        ResultData result = new ResultData();

        if (Constant.RED.Equals(fen.current))
        {
            //最新走法
            MoveData move = fen[0];
            if (Utility.IsRedGuoHe(move.end))
            {
                int len = Constant.BingZouFaDelta.Length;
                for (int i = 0; i < len; i++)
                {
                    PointData p = move.start + Constant.BingZouFaDelta[i].target;
                    if (move.end == p)
                    {
                        if (fen[move.end] == Qizi.KONGZI)
                        {
                            //空白移动
                            result.result = true;
                            result.pgn.fen = fen;
                        }
                        else if (Utility.IsBlack(fen[move.end]))
                        {
                            //黑子吃掉
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
            }
            else
            {
                PointData p = move.start + Constant.BingZouFaDelta[0].target;
                if (move.end == p)
                {
                    if (fen[move.end] == Qizi.KONGZI)
                    {
                        //空白移动
                        result.result = true;
                        result.pgn.fen = fen;
                    }
                    else if (Utility.IsBlack(fen[move.end]))
                    {
                        //黑子吃掉
                        result.result = true;
                        result.caneat = true;
                        result.pgn.fen = fen;
                    }
                    else
                    {
                        throw new AppException(ErrorMessage.AE0007);
                    }
                }
            }
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData CheckBlackChe()
    {
        ResultData result = new ResultData();

        if (Constant.BLACK.Equals(fen.current))
        {
            //最新走法
            MoveData move = fen[0];

            if ((move.end - move.start) == PointData.Zero)
            {
                throw new AppException(ErrorMessage.AE0005);
            }
            else if (Utility.IsBlack(fen[move.end]))
            {
                throw new AppException(ErrorMessage.AE0007);
            }
            else
            {
                PointData start = move.start;
                DeltaData delta = Utility.GetCheDelta(move.end - move.start);
                while (true)
                {
                    start = start + delta.target;
                    if (start != move.end)
                    {
                        if (fen[start] != Qizi.KONGZI)
                        {
                            //被阻挡
                            throw new AppException(ErrorMessage.AE0007);
                        }
                    }
                    else
                    {
                        if (fen[start] == Qizi.KONGZI)
                        {
                            //空白移动
                            result.result = true;
                            result.pgn.fen = fen;
                        }
                        else if (Utility.IsRed(fen[start]))
                        {
                            //红子吃掉
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
            }
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData CheckBlackMa()
    {
        ResultData result = new ResultData();

        if (Constant.BLACK.Equals(fen.current))
        {
            //最新走法
            MoveData move = fen[0];
            int len = Constant.MaZouFaDelta.Length;

            for (int i = 0; i < len; i++)
            {
                PointData p = move.start + Constant.MaZouFaDelta[i].target;
                if (move.end == p)
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
                    else if (Utility.IsRed(fen[move.end]))
                    {
                        //红子吃掉
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData CheckBlackPao()
    {
        ResultData result = new ResultData();

        if (Constant.BLACK.Equals(fen.current))
        {
            //最新走法
            MoveData move = fen[0];

            if ((move.end - move.start) == PointData.Zero)
            {
                throw new AppException(ErrorMessage.AE0005);
            }
            else if (Utility.IsBlack(fen[move.end]))
            {
                throw new AppException(ErrorMessage.AE0007);
            }
            if (Utility.IsRed(fen[move.end]))
            {
                PointData start = move.start;
                DeltaData delta = Utility.GetCheDelta(move.end - move.start);
                int paotai = 0;
                while (true)
                {
                    start = start + delta.target;
                    if (start != move.end)
                    {
                        if (fen[start] != Qizi.KONGZI)
                        {
                            paotai++;
                        }
                    }
                    else
                    {
                        if (paotai == 1)
                        {
                            //红子吃掉
                            result.result = true;
                            result.caneat = true;
                            result.pgn.fen = fen;
                        }
                        else
                        {
                            //被阻挡
                            throw new AppException(ErrorMessage.AE0007);
                        }
                        break;
                    }
                }
            }
            else
            {
                PointData start = move.start;
                DeltaData delta = Utility.GetCheDelta(move.end - move.start);
                while (true)
                {
                    start = start + delta.target;
                    if (start != move.end)
                    {
                        if (fen[start] != Qizi.KONGZI)
                        {
                            //被阻挡
                            throw new AppException(ErrorMessage.AE0007);
                        }
                    }
                    else
                    {
                        //空白移动
                        result.result = true;
                        result.pgn.fen = fen;
                        break;
                    }
                }
            }
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;

    }

    private ResultData CheckBlackXiang()
    {
        ResultData result = new ResultData();

        if (Constant.BLACK.Equals(fen.current))
        {
            //最新走法
            MoveData move = fen[0];
            int len = Constant.XiangZouFaDelta.Length;

            if (Utility.IsBlackGuoHe(move.end))
            {
                //象过河了
                throw new AppException(ErrorMessage.AE0006);
            }

            for (int i = 0; i < len; i++)
            {
                PointData p = move.start + Constant.XiangZouFaDelta[i].target;
                if (move.end == p)
                {
                    //象眼
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
                    else if (Utility.IsRed(fen[move.end]))
                    {
                        //红子吃掉
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData CheckBlackShi()
    {
        ResultData result = new ResultData();

        if (Constant.BLACK.Equals(fen.current))
        {
            //最新走法
            MoveData move = fen[0];
            int len = Constant.ShiZouFaDelta.Length;
            if (!Utility.IsBlackYingZhang(move.end))
            {
                //仕出界了
                throw new AppException(ErrorMessage.AE0006);
            }
            for (int i = 0; i < len; i++)
            {
                PointData p = move.start + Constant.ShiZouFaDelta[i].target;
                if (move.end == p)
                {
                    if (fen[move.end] == Qizi.KONGZI)
                    {
                        //空白移动
                        result.result = true;
                        result.pgn.fen = fen;
                    }
                    else if (Utility.IsRed(fen[move.end]))
                    {
                        //红子吃掉
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData CheckBlackJiang()
    {
        ResultData result = new ResultData();

        if (Constant.BLACK.Equals(fen.current))
        {
            //最新走法
            MoveData move = fen[0];
            int len = Constant.JiangShuaiZouFaDelta.Length;
            if (!Utility.IsBlackYingZhang(move.end))
            {
                //将出界了
                throw new AppException(ErrorMessage.AE0006);
            }
            for (int i = 0; i < len; i++)
            {
                PointData p = move.start + Constant.JiangShuaiZouFaDelta[i].target;
                if (move.end == p)
                {
                    if (fen[move.end] == Qizi.KONGZI)
                    {
                        //空白移动
                        result.result = true;
                        result.pgn.fen = fen;
                    }
                    else if (Utility.IsRed(fen[move.end]))
                    {
                        //红子吃掉
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData CheckBlackZu()
    {
        ResultData result = new ResultData();

        if (Constant.BLACK.Equals(fen.current))
        {
            //最新走法
            MoveData move = fen[0];
            if (Utility.IsBlackGuoHe(move.end))
            {
                int len = Constant.ZuZouFaDelta.Length;
                for (int i = 0; i < len; i++)
                {
                    PointData p = move.start + Constant.ZuZouFaDelta[i].target;
                    if (move.end == p)
                    {
                        if (fen[move.end] == Qizi.KONGZI)
                        {
                            //空白移动
                            result.result = true;
                            result.pgn.fen = fen;
                        }
                        else if (Utility.IsRed(fen[move.end]))
                        {
                            //红子吃掉
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
            }
            else
            {
                PointData p = move.start + Constant.ZuZouFaDelta[0].target;
                if (move.end == p)
                {
                    if (fen[move.end] == Qizi.KONGZI)
                    {
                        //空白移动
                        result.result = true;
                        result.pgn.fen = fen;
                    }
                    else if (Utility.IsRed(fen[move.end]))
                    {
                        //红子吃掉
                        result.result = true;
                        result.caneat = true;
                        result.pgn.fen = fen;
                    }
                    else
                    {
                        throw new AppException(ErrorMessage.AE0007);
                    }
                }
            }
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }


    public ResultData GetMoveLine()
    {
        ResultData result = new ResultData();

        Qizi qizi = fen.GetCurrentQizi();
        switch (qizi)
        {
            case Qizi.REDSHUAI:
                result = GetRedShuai();
                break;
            case Qizi.REDSHI:
                result = GetRedShi();
                break;
            case Qizi.REDXIANG:
                result = GetRedXiang();
                break;
            case Qizi.REDMA:
                result = GetRedMa();
                break;
            case Qizi.REDCHE:
                result = GetRedChe();
                break;
            case Qizi.REDPAO:
                result = GetRedPao();
                break;
            case Qizi.REDBING:
                result = GetRedBing();
                break;

            case Qizi.BLACKJIANG:
                result = GetBlackJiang();
                break;
            case Qizi.BLACKSHI:
                result = GetBlackShi();
                break;
            case Qizi.BLACKXIANG:
                result = GetBlackXiang();
                break;
            case Qizi.BLACKMA:
                result = GetBlackMa();
                break;
            case Qizi.BLACKCHE:
                result = GetBlackChe();
                break;
            case Qizi.BLACKPAO:
                result = GetBlackPao();
                break;
            case Qizi.BLACKZU:
                result = GetBlackZu();
                break;

            case Qizi.KONGZI:
            case Qizi.ZHANWEI:
            default:
                break;
        }
        if (result.result)
        {
            CheckJiangjun();
        }
        return result;
    }


    private ResultData GetRedChe()
    {
        ResultData result = new ResultData();

        if (Constant.RED.Equals(fen.current))
        {
            //选中的坐标
            PointData start = fen.selected;
            int len = Constant.CheZouFaDelta.Length;

            for (int i = 0; i < len; i++)
            {
                PointData target = Constant.CheZouFaDelta[i].target;
                for(int m = 1; m < 10; m++)
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
                    else if (Utility.IsBlack(fen[p]))
                    {
                        fen[p] = fen[p] + 100;
                        break;
                    }
                    else {
                        break;
                    }
                }
            }
            result.result = true;
            result.pgn.fen = fen;
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;

    }

    private ResultData GetRedMa()
    {
        ResultData result = new ResultData();

        if (Constant.RED.Equals(fen.current))
        {
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
                else if (Utility.IsBlack(fen[p]))
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData GetRedPao()
    {
        ResultData result = new ResultData();

        if (Constant.RED.Equals(fen.current))
        {
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
                    else if (Utility.IsBlack(fen[p]))
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;

    }

    private ResultData GetRedXiang()
    {
        ResultData result = new ResultData();

        if (Constant.RED.Equals(fen.current))
        {
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
                else if (Utility.IsRedGuoHe(p))
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
                else if (Utility.IsBlack(fen[p]))
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }


    private ResultData GetRedShi()
    {
        ResultData result = new ResultData();

        if (Constant.RED.Equals(fen.current))
        {
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
                else if (!Utility.IsRedYingZhang(p))
                {
                    continue;
                }
                else if (fen[p] == Qizi.KONGZI)
                {
                    fen[p] = Qizi.ZHANWEI;
                }
                else if (Utility.IsBlack(fen[p]))
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData GetRedShuai()
    {
        ResultData result = new ResultData();

        if (Constant.RED.Equals(fen.current))
        {
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
                else if (!Utility.IsRedYingZhang(p))
                {
                    continue;
                }
                else if (fen[p] == Qizi.KONGZI)
                {
                    fen[p] = Qizi.ZHANWEI;
                }
                else if (Utility.IsBlack(fen[p]))
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData GetRedBing()
    {
        ResultData result = new ResultData();

        if (Constant.RED.Equals(fen.current))
        {
            //选中的坐标
            PointData start = fen.selected;
            int len = Constant.BingZouFaDelta.Length;
            if (!Utility.IsRedGuoHe(start))
            {
                len = 1;
            }
            for (int i = 0; i < len; i++)
            {
                PointData target = Constant.BingZouFaDelta[i].target;
                PointData p = start + target;
                if (!Utility.IsOnQiPan(p))
                {
                    continue;
                }
                else if (fen[p] == Qizi.KONGZI)
                {
                    fen[p] = Qizi.ZHANWEI;
                }
                else if (Utility.IsBlack(fen[p]))
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData GetBlackChe()
    {
        ResultData result = new ResultData();

        if (Constant.BLACK.Equals(fen.current))
        {
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
                    else if (Utility.IsRed(fen[p]))
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData GetBlackMa()
    {
        ResultData result = new ResultData();

        if (Constant.BLACK.Equals(fen.current))
        {
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
                else if (Utility.IsRed(fen[p]))
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData GetBlackPao()
    {

        ResultData result = new ResultData();

        if (Constant.BLACK.Equals(fen.current))
        {
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
                    else if (Utility.IsRed(fen[p]))
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData GetBlackXiang()
    {
        ResultData result = new ResultData();

        if (Constant.BLACK.Equals(fen.current))
        {
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
                else if (Utility.IsBlackGuoHe(p))
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
                else if (Utility.IsRed(fen[p]))
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData GetBlackShi()
    {
        ResultData result = new ResultData();

        if (Constant.BLACK.Equals(fen.current))
        {
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
                else if (!Utility.IsBlackYingZhang(p))
                {
                    continue;
                }
                else if (fen[p] == Qizi.KONGZI)
                {
                    fen[p] = Qizi.ZHANWEI;
                }
                else if (Utility.IsRed(fen[p]))
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData GetBlackJiang()
    {
        ResultData result = new ResultData();

        if (Constant.BLACK.Equals(fen.current))
        {
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
                else if (!Utility.IsBlackYingZhang(p))
                {
                    continue;
                }
                else if (fen[p] == Qizi.KONGZI)
                {
                    fen[p] = Qizi.ZHANWEI;
                }
                else if (Utility.IsRed(fen[p]))
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }

    private ResultData GetBlackZu()
    {
        ResultData result = new ResultData();

        if (Constant.BLACK.Equals(fen.current))
        {
            //选中的坐标
            PointData start = fen.selected;
            int len = Constant.ZuZouFaDelta.Length;
            if (!Utility.IsBlackGuoHe(start))
            {
                len = 1;
            }
            for (int i = 0; i < len; i++)
            {
                PointData target = Constant.ZuZouFaDelta[i].target;
                PointData p = start + target;
                if (!Utility.IsOnQiPan(p))
                {
                    continue;
                }
                else if (fen[p] == Qizi.KONGZI)
                {
                    fen[p] = Qizi.ZHANWEI;
                }
                else if (Utility.IsRed(fen[p]))
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
        }
        else
        {
            throw new AppException(ErrorMessage.AE0001);
        }
        return result;
    }
}
