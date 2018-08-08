using System.Collections;

//象棋引擎接口类
public class ChineseChessHandler:BaseHandler {

    Zoufamiddleware Zoufamiddleware = new Zoufamiddleware();
    private static ChineseChessHandler chessHandler = null;
    ChineseChessHandler() : base()
    {
        new HistoryZoufaLoader().RemoveAll();
    }

    public static ChineseChessHandler GetNewInstant()
    {
        chessHandler = new ChineseChessHandler();
        return chessHandler;
    }

    public static ChineseChessHandler GetInstant()
    {
        if (chessHandler == null)
        {
            GetNewInstant();
        }
        return chessHandler;
    }

    //检查走法合法性
    public ResultData CheckZoufa(FenData fen)
    {
        ResultData result = Zoufamiddleware.CheckZoufa(fen);
        return result;
    }

    //获取棋子路线
    public ResultData GetMoveLine(FenData fen)
    {
        ResultData result = Zoufamiddleware.GetMoveLine(fen);
        return result;
    }

    //获得最佳走法
    public ResultData GetZoufa(FenData fen)
    {
        ResultData result = new ResultData();
        MoveData move = null;

        //获取历史走法
        move = new HistoryZoufaLoader().GetZoufa(Utility.ConvertQiPanToString(fen));
        if (move != null) goto Complete;

        //查询开局库
        //没完 什么时间看开局库
        if (1 == 1)
        {
            move = kaijuloader.GetKaiju(Utility.ConvertQiPanToString(fen));
            if (move != null) goto Complete;
        }

        //查询残局库
        //没完 什么时间看残局库
        if (2 == 2)
        {
            move = canjuloader.GetCanju(Utility.ConvertQiPanToString(fen));
            if (move != null) goto Complete;
        }

        move = Zoufamiddleware.GetZoufa(fen);
        Complete:
        result.moves.Add(move);
        result.result = true;
        result.caneat = Utility.CanEat(fen,move);
        new HistoryZoufaLoader().AddZoufa(Utility.ConvertQiPanToString(fen), move);
        return result;
    }
    
}
