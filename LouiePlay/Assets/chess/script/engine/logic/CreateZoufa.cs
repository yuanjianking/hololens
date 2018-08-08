
using System;
using System.Collections.Generic;
using UnityEngine;

//生成当前棋局全部走法
public class CreateZoufa : BaseZoufa
{
    private static ILogger log = Debug.unityLogger;

    public CreateZoufa(FenData fen) : base(fen)
    {
       
    }

    public List<MoveData> GetZoufaLsit()
    {
        List<MoveData> result = new List<MoveData>();
        
        Dictionary<int,List<MoveData>> map = new Dictionary<int, List<MoveData>>();
        //x0-8,y0-9(黑方：0-4，红方：5-9)
        for (int x = 0; x <= 8; x++)
        {
            for (int y = 0; y <= 9; y++)
            {
                Qizi  qizi =  fen.chess[x, y];
                if (qizi != Qizi.KONGZI)
                {
                    if (((int)qizi & fen.current) == 0x0000)
                        continue;

                    GetMoveList(ref map, qizi, x, y);
                }
            }
        }

        //排列顺序：车>马>炮>兵>象>仕>将
        for (int i = 10; i <= 70; i += 10)
        {
            if (map.ContainsKey(i))
                result.AddRange(map[i].ToArray());
            if (map.ContainsKey(i + 1))
                result.AddRange(map[i + 1].ToArray());
            if (map.ContainsKey(i + 2))
                result.AddRange(map[i + 2].ToArray());
            if (map.ContainsKey(i + 3))
                result.AddRange(map[i + 3].ToArray());
            if (map.ContainsKey(i + 4))
                result.AddRange(map[i + 4].ToArray());
        }

        return result;
    }

    private void GetMoveList(ref Dictionary<int, List<MoveData>> map, Qizi qizi,int x, int y)
    {
        int key = 0;
        List<MoveData> list = null;
        PointData p = new PointData(x, y);
        switch (qizi)
        {
            case Qizi.REDCHE:
            case Qizi.BLACKCHE:
                GetCheList(out list, p);
                key = 10;
                break;
            case Qizi.REDMA:
            case Qizi.BLACKMA:
                GetMaList(out list, p);
                key = 20;
                break;
            case Qizi.REDPAO:
            case Qizi.BLACKPAO:
                GetPaoList(out list, p);
                key = 30;
                break;
            case Qizi.REDBING:
            case Qizi.BLACKZU:
                GetBingZuList(out list, p);
                key = 40;
                break;
            case Qizi.REDXIANG:
            case Qizi.BLACKXIANG:
                GetXiangList(out list, p);
                key = 50;
                break;
            case Qizi.REDSHI:
            case Qizi.BLACKSHI:
                GetShiList(out list, p);
                key = 60;
                break;
            case Qizi.REDSHUAI:
            case Qizi.BLACKJIANG:
                GetJiangShuaiList(out list, p);
                key = 70;
                break;

            case Qizi.KONGZI:
            default:
                break;
        }
        if(list != null)
        { 
           while(map.ContainsKey(key))
                ++key;
            map.Add(key, list);
        }
    }

    private void GetCheList(out List<MoveData> list, PointData start)
    {
        list = new List<MoveData>();
        int len = Constant.CheZouFaDelta.Length;

        for (int i = 0; i < len; i++)
        {
            PointData target = Constant.CheZouFaDelta[i].target;
            for (int m = 1; m < Constant.QIPANBIANJIE; m++)
            {
                PointData p = start + target * m;

                if (!Utility.IsOnQiPan(p))
                {
                    break;
                }
                else if (fen[p] == Qizi.KONGZI)
                {
                    //添加走法                    
                    AddZouFa(ref list, start, p);
                 }
                else if ((fen.current & (int)fen[p]) == 0x0000)
                {
                    //添加吃子  
                    AddZouFa(ref list, start, p);
                    break;
                }
                else
                {
                    break;
                }
            }
        }
    }
    private void GetMaList(out List<MoveData> list, PointData start)
    {
        list = new List<MoveData>();
        int len = Constant.MaZouFaDelta.Length;

        for (int i = 0; i < len; i++)
        {
            PointData target = Constant.MaZouFaDelta[i].target;
            PointData delta = Constant.MaZouFaDelta[i].delta;
            PointData p = start + target;
            if (!Utility.IsOnQiPan(p))
            {
                continue;
            }
            else if (fen[start + delta] != Qizi.KONGZI)
            {
                continue;
            }
            else if (fen[p] == Qizi.KONGZI)
            {
                //添加走法                    
                AddZouFa(ref list, start, p);
            }
            else if ((fen.current & (int)fen[p]) == 0x0000)
            {
                //添加吃子                    
                AddZouFa(ref list, start, p);
            }
            else
            {
                continue;
            }
        }
    }
    private void GetPaoList(out List<MoveData> list, PointData start)
    {
        list = new List<MoveData>();
        int len = Constant.PaoZouFaDelta.Length;

        for (int i = 0; i < len; i++)
        {
            int poatai = 0;
            PointData target = Constant.PaoZouFaDelta[i].target;
            for (int m = 1; m < Constant.QIPANBIANJIE; m++)
            {
                PointData p = start + target * m;

                if (!Utility.IsOnQiPan(p))
                {
                    break;
                }
                else if (fen[p] == Qizi.KONGZI)                    
                {
                    //添加走法
                    if(poatai == 0)
                        AddZouFa(ref list, start, p);
                }
                else if ((fen.current & (int)fen[p]) == 0x0000)
                {
                    if (poatai == 1)
                    {
                        //添加吃子
                        AddZouFa(ref list, start, p);
                        break;
                    }
                    poatai++;
                }
                else
                {
                    poatai++;
                    if (poatai > 1)
                        break;

                }
            }
        }
    }
    private void GetXiangList(out List<MoveData> list, PointData start)
    {
        list = new List<MoveData>();
        int len = Constant.XiangZouFaDelta.Length;

        for (int i = 0; i < len; i++)
        {
            PointData target = Constant.XiangZouFaDelta[i].target;
            PointData delta = Constant.XiangZouFaDelta[i].delta;
            PointData p = start + target;
            if (!Utility.IsOnQiPan(p))
            {
                continue;
            }
            else if ((Constant.RED == fen.current && Utility.IsRedGuoHe(p)) ||
                     (Constant.BLACK == fen.current && Utility.IsBlackGuoHe(p)))
            {
                continue;
            }
            else if (fen[start + delta] != Qizi.KONGZI)
            {
                continue;
            }
            else if (fen[p] == Qizi.KONGZI)
            {
                //添加走法
                AddZouFa(ref list, start, p);
            }
            else if ((fen.current & (int)fen[p]) == 0x0000)
            {
                //添加吃子
                AddZouFa(ref list, start, p);
            }
            else
            {
                continue;
            }
        }
    }

    private void GetShiList(out List<MoveData> list, PointData start)
    {
        list = new List<MoveData>();
        int len = Constant.ShiZouFaDelta.Length;

        for (int i = 0; i < len; i++)
        {
            PointData target = Constant.ShiZouFaDelta[i].target;
            PointData p = start + target;
            if (!Utility.IsOnQiPan(p))
            {
                continue;
            }
            else if ((Constant.RED == fen.current && !Utility.IsRedYingZhang(p)) ||
                     (Constant.BLACK == fen.current && !Utility.IsBlackYingZhang(p)))
            {
                continue;
            }
            else if (fen[p] == Qizi.KONGZI)
            {
                //添加走法
                AddZouFa(ref list, start, p);
            }
            else if ((fen.current & (int)fen[p]) == 0x0000)
            {
                //添加吃子
                AddZouFa(ref list, start, p);
            }
            else
            {
                continue;
            }
        }
    }

    private void GetJiangShuaiList(out List<MoveData> list, PointData start)
    {
        list = new List<MoveData>();
        int len = Constant.JiangShuaiZouFaDelta.Length;

        for (int i = 0; i < len; i++)
        {
            PointData target = Constant.JiangShuaiZouFaDelta[i].target;
            PointData p = start + target;
            if (!Utility.IsOnQiPan(p))
            {
                continue;
            }
            else if ((Constant.RED == fen.current && !Utility.IsRedYingZhang(p)) ||
                     (Constant.BLACK == fen.current && !Utility.IsBlackYingZhang(p)))
            {
                continue;
            }
            else if (fen[p] == Qizi.KONGZI)
            {
                //添加走法
                AddZouFa(ref list, start, p);
            }
            else if ((fen.current & (int)fen[p]) == 0x0000)
            {
                //添加吃子
                AddZouFa(ref list, start, p);
            }
            else
            {
                continue;
            }
        }
    }

    private void GetBingZuList(out List<MoveData> list, PointData start)
    {
        list = new List<MoveData>();
        int len = 1;
        if ((Constant.RED == fen.current && Utility.IsRedGuoHe(start)) ||
                     (Constant.BLACK == fen.current && Utility.IsBlackGuoHe(start)))
        {
            len = 3;
        }
        for (int i = 0; i < len; i++)
        {
            PointData target;
            if (Constant.RED == fen.current)
            {
                target = Constant.BingZouFaDelta[i].target;
            }
            else
            {
                target = Constant.ZuZouFaDelta[i].target;
            }

            PointData p = start + target;
            if (!Utility.IsOnQiPan(p))
            {
                continue;
            }
            else if (fen[p] == Qizi.KONGZI)
            {
                //添加走法
                AddZouFa(ref list, start, p);
            }
            else if ((fen.current & (int)fen[p]) == 0x0000)
            {
                //添加吃子
                AddZouFa(ref list, start, p);
            }
            else
            {
                continue;
            }
        }
    }

    private void AddZouFa(ref List<MoveData> list, PointData start, PointData end)
    {
        //添加走法                 
        MoveData move = new MoveData(start, end);
        //检查将军
        try
        {
            fen.moves.Insert(0, move);
            new CheckZoufa(fen).CheckJiangjun();
            list.Add(move);
        }
        catch (AppException)
        {
        }
        finally
        {
            fen.moves.Remove(move);
        }

    }
}
