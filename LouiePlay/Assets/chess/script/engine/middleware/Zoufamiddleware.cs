using System;

public class Zoufamiddleware {

    //多任务执行。待实装。。。。。
    public ResultData CheckZoufa(FenData fen)
    {
        ResultData result = new ResultData();

        try
        {
            result = (new CheckZoufa((FenData)fen.Clone())).Check();
        }
        catch (AppException ae)
        {
            result.errorcode = ae.GetMsgCode();
            result.errormsg = ae.GetMsg();
        }
        catch (Exception e)
        {
            result.errorcode = "99";
            result.errormsg = e.Message;
        }
        finally
        {
            
        }
        return result;
    }

    public ResultData GetMoveLine(FenData fen)
    {

        ResultData result = new ResultData();

        try
        {
            result = (new CheckZoufa((FenData)fen.Clone())).GetMoveLine();
        }
        catch (AppException ae)
        {
            result.errorcode = ae.GetMsgCode();
            result.errormsg = ae.GetMsg();
        }
        catch (Exception e)
        {
            result.errorcode = "99";
            result.errormsg = e.Message;
        }
        finally
        {
            
        }
        return result;
    }


    public MoveData GetZoufa(FenData fen)
    {
        MoveData result = new MoveData();

        return (new GetZoufa(fen)).Zoufa();
    }
}
