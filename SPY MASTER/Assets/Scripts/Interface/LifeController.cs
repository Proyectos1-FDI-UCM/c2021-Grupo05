using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    [SerializeField]
    int MaxLives = 3;

    public Image Symbol;
    public RectTransform Panel;

    [SerializeField]
    int lives;

    void Start()
    {
        lives = MaxLives;
        StarCount();
        UploadHud();
    }

    private void Update()
    {
        UploadHud();
    }


    public void Damage(int damage)
    {
        lives -= damage;
        UploadHud();
    }

    public void Cure(int livesAdd)
    {
        lives += livesAdd;
        UploadHud();
    }

    public void StarCount()
    {
        for (int i = 0; i < MaxLives; i++)
            Instantiate<Image>(Symbol, Panel);

    }
    public void AddLive(int i)
    {
        Panel.GetChild(i).gameObject.SetActive(true);
    }

    public void LessLive(int i)
    {
        Panel.GetChild(i).gameObject.SetActive(false);
    }



    void UploadHud()
    {
        for (int i = 0; i < Panel.childCount; i++)
            LessLive(i);


        for (int i = 0; i < lives; i++)
            AddLive(i);

    }
}
