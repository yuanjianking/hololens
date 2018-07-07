using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateZoufa : BaseZoufa
{

    public CreateZoufa(FenData fen) : base(fen)
    {
    }

    public ResultData GetZoufa()
    {
        return new ResultData();
    }
}
