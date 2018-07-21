public class KaijuLoader : BaseLoader {
    public void LoadKaiju(out KaijuData kaiju)
    {
        kaiju = new KaijuData();
        //没完 开局加载
        while (true)
            kaiju.KaiJu.Add("", new MoveData());
    }
}
