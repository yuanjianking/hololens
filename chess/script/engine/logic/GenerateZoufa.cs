
using System.Collections.Generic;
//走法生成
public class GenerateZoufa : BaseZoufa
{
    private SreachZoufa sreach;
  

    public GenerateZoufa(FenData fen) : base(fen)
    {
        sreach = new SreachZoufa(fen);       
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
