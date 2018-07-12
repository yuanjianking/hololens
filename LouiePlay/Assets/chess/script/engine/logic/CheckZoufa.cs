
public class CheckZoufa : BaseZoufa {
    private CheckChess checkChess;
    private GetMoveLine getMoveLine;

    public CheckZoufa(FenData fen) :base(fen)
    {
        checkChess = new CheckChess(fen);
        getMoveLine = new GetMoveLine(fen);
    }

    public ResultData Check()
    {
        ResultData result = new ResultData();
        Utility.CheckQizi(fen);

        Qizi qizi = fen.GetCurrentQizi();
        if (!fen.current.Equals(Utility.GetQiziColor(qizi)))
        {
            throw new AppException(ErrorMessage.AE0001);
        }

        switch (qizi)
        {
            case Qizi.REDSHUAI:
            case Qizi.BLACKJIANG:
                result = checkChess.CheckJiangShuai();
                break;
            case Qizi.REDSHI:
            case Qizi.BLACKSHI:
                result = checkChess.CheckShi();
                break;
            case Qizi.REDXIANG:
            case Qizi.BLACKXIANG:
                result = checkChess.CheckXiang();
                break;
            case Qizi.REDMA:
            case Qizi.BLACKMA:
                result = checkChess.CheckMa();
                break;
            case Qizi.REDCHE:
            case Qizi.BLACKCHE:
                result = checkChess.CheckChe();
                break;
            case Qizi.REDPAO:
            case Qizi.BLACKPAO:
                result = checkChess.CheckPao();
                break;
            case Qizi.REDBING:
            case Qizi.BLACKZU:
                result = checkChess.CheckBingZu();
                break;
                
            case Qizi.KONGZI:
            case Qizi.ZHANWEI:
            default:
                break;
        }
        if(result.result)
        {
            checkChess.CheckJiangjun();
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
            case Qizi.BLACKJIANG:
                result = getMoveLine.GetJiangShuai();
                break;
            case Qizi.REDSHI:
            case Qizi.BLACKSHI:
                result = getMoveLine.GetShi();
                break;
            case Qizi.REDXIANG:
            case Qizi.BLACKXIANG:
                result = getMoveLine.GetXiang();
                break;
            case Qizi.REDMA:
            case Qizi.BLACKMA:
                result = getMoveLine.GetMa();
                break;
            case Qizi.REDCHE:
            case Qizi.BLACKCHE:
                result = getMoveLine.GetChe();
                break;
            case Qizi.REDPAO:
            case Qizi.BLACKPAO:
                result = getMoveLine.GetPao();
                break;
            case Qizi.REDBING:
            case Qizi.BLACKZU:
                result = getMoveLine.GetBingZu();
                break;
                
            case Qizi.KONGZI:
            case Qizi.ZHANWEI:
            default:
                break;
        }
        return result;
    }
}
