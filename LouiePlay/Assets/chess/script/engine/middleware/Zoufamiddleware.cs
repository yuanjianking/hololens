using System;

//将来扩展查询速度用
public class Zoufamiddleware {
    
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
            result = (new GenerateMoveLine(fen)).GetMoveLine();
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
        return (new GenerateZoufa(fen)).Zoufa();
    }
}
