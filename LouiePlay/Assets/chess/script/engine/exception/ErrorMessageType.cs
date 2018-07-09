using System;
using System.Collections;
using System.Collections.Generic;

public class ErrorMessageType
{
    public String msgid;
    public String msgcode;
    public String msg;
    public String msgcat;

    public ErrorMessageType(String msgid, String msgcode, String msg, String msgcat)
    {
        this.msgid = msgid;
        this.msgcode = msgcode;
        this.msg = msg;
        this.msgcat = msgcat;
    }
}
