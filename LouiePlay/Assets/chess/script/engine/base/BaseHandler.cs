public abstract class BaseHandler {
    protected CanjuLoader canjuloader;
    protected KaijuLoader kaijuloader;
    protected BaseHandler()
    {
        canjuloader = new CanjuLoader();
        kaijuloader = new KaijuLoader();
    }
}
