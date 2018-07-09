using System;

public class AppException : Exception {

    private ErrorMessageType msg;

    public AppException(ErrorMessageType msg)
    {
        this.msg = msg;
    }

    public ErrorMessageType GetMessageType()
    {
        return msg;
    }


    public String GetMsgId()
    {
        return msg.msgid;
    }

    public String GetMsgCode()
    {
        return msg.msgcode;
    }

    public String GetMsg()
    {
        return msg.msg;
    }

    public String GetMsgCat()
    {
        return msg.msgcat;
    }

}
