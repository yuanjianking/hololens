
using System.Collections.Generic;

public class GetZoufa : BaseZoufa
{
    private SreachZoufa sreach;
  

    public GetZoufa(FenData fen) : base(fen)
    {
        sreach = new SreachZoufa((FenData)fen.Clone());       
    }

    public MoveData Zoufa()
    {
        MoveData result = null;
        //搜索所有走法
        result = sreach.GetBestZoufa();
        //返回最优走法
        return result;
    }
}
