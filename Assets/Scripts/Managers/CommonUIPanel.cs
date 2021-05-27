using UnityEngine;

public class CommonUIPanel : MonoBehaviour, ICommonUIPanel
{
    public virtual void Show(params object[] args)
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
