using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIScreen : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    public void Initialize()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        gameObject.SetActive(true);  // warm up - prevent initial hitch
        gameObject.SetActive(false);
        OnInitialize();
    }
    protected virtual void OnInitialize() { }

    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        gameObject.SetActive(true);
        OnShow();
    }
    protected virtual void OnShow() { }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        gameObject.SetActive(false);
    }
}
