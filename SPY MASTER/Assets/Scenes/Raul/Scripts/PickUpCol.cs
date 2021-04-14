
using UnityEngine;

public class PickUpCol : MonoBehaviour
{
    public GameObject interactionSymbol;

    PickUpHistory information;
    private void Start()
    {
        information = GetComponent<PickUpHistory>();
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ColBeProduced");
        if (collision.gameObject.layer == 8)
        {
            interactionSymbol.SetActive(true);            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {           
            interactionSymbol.SetActive(false);
            information.Close();
        }
    }

    private void Update()
    {
        if(interactionSymbol.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("GetInformation");
            information.GetInfo();
        }
    }
}
