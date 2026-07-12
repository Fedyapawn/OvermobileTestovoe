using UnityEngine;
using UnityEngine.UI;

public class LeaderboardSticky : MonoBehaviour
{
    [Header("—сылки")]
    public RectTransform viewport;
    public RectTransform targetItem; 
    public GameObject stickyItem;   

    private CanvasGroup targetCanvasGroup;
    private RectTransform stickyRect;

    void Start()
    {
        stickyRect = stickyItem.GetComponent<RectTransform>();
        targetCanvasGroup = targetItem.GetComponent<CanvasGroup>();
        if (targetCanvasGroup == null)
            targetCanvasGroup = targetItem.gameObject.AddComponent<CanvasGroup>();
    }

    void Update()
    {
        if (targetItem == null || viewport == null) return;
        Vector3[] viewCorners = new Vector3[4];
        viewport.GetWorldCorners(viewCorners);
        float vTop = viewCorners[1].y;
        float vBottom = viewCorners[0].y;
        Vector3[] targetCorners = new Vector3[4];
        targetItem.GetWorldCorners(targetCorners);
        float tTop = targetCorners[1].y;
        float tBottom = targetCorners[0].y;
        if (tBottom < vBottom)
        {
            ShowSticky(vBottom, new Vector2(0.5f, 0f)); 
        }
        else if (tTop > vTop)
        {
            ShowSticky(vTop, new Vector2(0.5f, 1f)); 
        }
        else
        {
            HideSticky();
        }
    }

    void ShowSticky(float yWorldPos, Vector2 pivot)
    {
        stickyItem.SetActive(true);

        targetCanvasGroup.alpha = 0f;
        targetCanvasGroup.blocksRaycasts = false;
        stickyRect.pivot = pivot;
        Vector3 pos = stickyRect.position;
        pos.y = yWorldPos;
        stickyRect.position = pos;
        stickyRect.sizeDelta = new Vector2(viewport.rect.width, stickyRect.sizeDelta.y);
    }

    void HideSticky()
    {
        if (stickyItem.activeSelf)
        {
            stickyItem.SetActive(false);
            targetCanvasGroup.alpha = 1f;
            targetCanvasGroup.blocksRaycasts = true;
        }
    }
}