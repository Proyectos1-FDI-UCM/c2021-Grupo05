using UnityEngine;
//Raúl Saavedra de la Riera
public class PlayerTarget : MonoBehaviour
{
    public bool redTarget, blueTarget, yellowTarget;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TargetIdenty target = collision.gameObject.GetComponent<TargetIdenty>();
        
        if (target != null)
        {
           if(target.redTarget)
            {
                if(!redTarget)
                {
                    redTarget = true;
                    Destroy(collision.gameObject);
                }
            }
           else if(target.blueTarget)
            {
                if (!blueTarget)
                {
                    blueTarget = true;
                    Destroy(collision.gameObject);
                }
            }
           else
            {
                if (!yellowTarget)
                {
                    yellowTarget = true;
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
