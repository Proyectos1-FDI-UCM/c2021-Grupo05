
using UnityEngine;
//Raúl Saavedra de la Riera
public class ElectrictyPanelLogic : MonoBehaviour
{
    public GameObject interactionSymbol;
    ElectricityLogic electricity;
    
    private void Start()
    {
        electricity = GetComponentInParent<ElectricityLogic>();
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
        }
    }

    private void Update()
    {
        if (interactionSymbol.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("ElectrictyDown");
            electricity.OffElectricty();
        }
    }
}
