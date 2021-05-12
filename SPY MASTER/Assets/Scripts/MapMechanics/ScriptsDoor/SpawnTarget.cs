
using UnityEngine;
//Raúl Saavedra de la Riera
public class SpawnTarget : MonoBehaviour
{   
    [SerializeField]
    GameObject targetPref;
    [SerializeField]
    GameObject targetVisual;
    [SerializeField]
    float targetDistance;

    private void Start()
    {
        Instantiate<GameObject>(targetVisual, 
            new Vector3(transform.position.x, transform.position.y + targetDistance, transform.position.x), 
            targetPref.transform.rotation, 
            this.transform);
    }

    private void OnDestroy()
    {
        Instantiate<GameObject>(targetPref, transform.position, targetPref.transform.rotation);
    }
}
