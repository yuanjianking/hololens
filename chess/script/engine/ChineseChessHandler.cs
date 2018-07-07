using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        ResultData result = Zoufamiddleware.GetZoufa(fen);
        result.pgn.extent = Extent;
        return result;
    }
}
