using System.Collections;

public class ChineseChessHandler:BaseHandler {

    Zoufamiddleware Zoufamiddleware = new Zoufamiddleware();

    public override ExtentData Extent
    {
        get
        {
            return Extent;
        }

        set
        {
            Extent = value;
        }
    }

    ChineseChessHandler() : base()
    {
        HistoryZouFaManager.RemoveAll();
    }

    public ResultData CheckZoufa(FenData fen)
    {
        ResultData result = Zoufamiddleware.CheckZoufa(fen);
        result.pgn.extent = Extent;
        return result;
    }

    public ResultData GetMoveLine(FenData fen)
    {
        ResultData result = Zoufamiddleware.GetMoveLine(fen);
        result.pgn.extent = Extent;
        return result;
    }

    public ResultData GetZoufa(FenData fen)
    {
        ResultData result = null;
        MoveData move = null;
      
        //获取历史走法
        move = GetHistoryZouFa(fen);
        if (move != null) goto Complete;

        //查询开局库
        //没完 什么时间看开局库
        if (1 == 1)
        {
            move = GetKaiJuData(fen);
            if (move != null) goto Complete;
        }

        //查询残局库
        //没完 什么时间看残局库
        if (2 == 2)
        {
            move = GetCanJuData(fen);
            if (move != null) goto Complete;
        }

        move = Zoufamiddleware.GetZoufa(fen);
        Complete:
        fen.moves.Insert(0, move);
        result.pgn.fen = fen;
        result.result = true;
        result.caneat = Utility.CanEat(fen);

        result.pgn.extent = Extent;

        HistoryZouFaManager.AddZouFa(result.pgn.fen);
        return result;
    }



    private MoveData GetKaiJuData(FenData fen)
    {
        //查询开局库
        return canju[Utility.ConvertQiPanToString(fen)];
    }

    private MoveData GetCanJuData(FenData fen)
    {
        //查询残局库
        return kaiju[Utility.ConvertQiPanToString(fen)];
    }

    private MoveData GetHistoryZouFa(FenData fen)
    {
        //查询历史走发
        return HistoryZouFaManager.GetZouFa(Utility.ConvertQiPanToString(fen));
    }
}
