using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ErrorMessage
{
    ///<summary>红方移动了黑方棋子</summary>
    public static readonly ErrorMessageType AE0001 = new ErrorMessageType("AE0001", "10", "红方移动了黑方棋子", "E");
    ///<summary>黑方移动了红方棋子</summary>
    public static readonly ErrorMessageType AE0002 = new ErrorMessageType("AE0002", "10", "黑方移动了红方棋子", "E");
    ///<summary>选中的棋子和移动的棋子不是同一个</summary> 
    public static readonly ErrorMessageType AE0003 = new ErrorMessageType("AE0003", "11", "选中的棋子和移动的棋子不是同一个", "E");
    ///<summary>棋子没有移动</summary>
    public static readonly ErrorMessageType AE0004 = new ErrorMessageType("AE0004", "11", "棋子没有移动", "E");
    ///<summary>棋子没按规则移动</summary> 
    public static readonly ErrorMessageType AE0005 = new ErrorMessageType("AE0005", "11", "棋子没按规则移动", "E");
    ///<summary>棋子移动出棋盘</summary>
    public static readonly ErrorMessageType AE0006 = new ErrorMessageType("AE0006", "11", "棋子移动出棋盘", "E");
    ///<summary>棋子被阻挡不能移动</summary>
    public static readonly ErrorMessageType AE0007 = new ErrorMessageType("AE0007", "11", "棋子被阻挡不能移动", "E");
    ///<summary>移动后被将军</summary>
    public static readonly ErrorMessageType AE0008 = new ErrorMessageType("AE0008", "11", "移动后被将军", "E");
    ///<summary>移动后被将帅碰面</summary>
    public static readonly ErrorMessageType AE0009 = new ErrorMessageType("AE0009", "11", "移动后被将帅碰面", "E");

}
