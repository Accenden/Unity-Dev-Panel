using UnityEngine;
using UnityEngine.EventSystems;
public class DraggableUI : MonoBehaviour , IDragHandler
{
    private RectTransform rectTransform;
    [SerializeField] Canvas canvas;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }


}
