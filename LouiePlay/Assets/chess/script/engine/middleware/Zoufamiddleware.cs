using System;

public class Zoufamiddleware {

    //多任务执行。待实装。。。。。
    public ResultData CheckZoufa(FenData fen)
    {
        ResultData result = new ResultData();

        try
        {
            result = (new CheckZoufa(fen)).Check();
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
            result = (new CheckZoufa(fen)).GetMoveLine();
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


    public ResultData GetZoufa(FenData fen)
    {
        ResultData result = new ResultData();

        try
        {
            result = (new CreateZoufa(fen)).GetZoufa();
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
}
