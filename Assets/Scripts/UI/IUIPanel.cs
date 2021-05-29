public interface ICommonUIPanel
{
    public void Show(params object[] args);
    public void Hide();
}

public interface IPanelWithListeners : ICommonUIPanel
{
    public void AddListeners();
}