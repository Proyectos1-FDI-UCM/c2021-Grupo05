using UnityEngine;
using UnityEngine.UI;

public class HudCountController : MonoBehaviour
{
    [SerializeField]
    int MaxCount = 3;

    public Image Symbol;
    public RectTransform Panel;

    public int currentCount;

    void Start()
    {
        currentCount = MaxCount;
        StarCount();
        UploadHud();
    }

    private void Update()
    {
        UploadHud();
    }

    public void HudLess(int loss)
    {
        currentCount -= loss;
        UploadHud();
    }

    public void HudAdd(int add)
    {
        currentCount += add;
        UploadHud();
    }

    public void StarCount()
    {
        for (int i = 0; i < MaxCount; i++)
            Instantiate<Image>(Symbol, Panel);

    }
    public void AddPoint(int i)
    {
        Panel.GetChild(i).gameObject.SetActive(true);
    }

    public void RemovePoint(int i)
    {
        Panel.GetChild(i).gameObject.SetActive(false);
    }

    public void UploadHud()
    {
        for (int i = 0; i < Panel.childCount; i++)
            RemovePoint(i);


        for (int i = 0; i < currentCount; i++)
            AddPoint(i);

    }
}