public class PrefabGUISystem : ForegoundGUISystem
{
    public override void Open(object msg)
    {
        base.Open(msg);
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        base.Close();
        gameObject.SetActive(false);
    }
}
