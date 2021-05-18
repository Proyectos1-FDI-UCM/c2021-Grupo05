using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    public int damage;

    Collider2D col;
    
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) Attack();
    }


    public void Attack()
    {
        Invoke("ActiveCol", 0f);
        Invoke("DesactiveCol", 0.1f);
        
    }   
    
   
    void ActiveCol()
    {
        col.enabled = true;
    }
    void DesactiveCol()
    {
        col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
          if(enemy != null && enemy.IsFollowPlayer())
        {
            Debug.Log("EnemyImpacted");
            if (col.enabled) DesactiveCol();
            //falta el método daño a enemigo
        }
    }

}
