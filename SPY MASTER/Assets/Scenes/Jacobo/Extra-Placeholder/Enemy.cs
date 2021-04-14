using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    ///<sumary>
    ///Clase gnerica Enemy. Derivar otros tipos partiendo de esta clase.
    ///Definir parametros/fields de clase mas adelante. Actuamente sin implementacion
    ///</sumary>

    protected int maxHp = 3;
    //vision speed,...

    //MaxHP Property   Por ahora deberia ser solo escritura(al ser dañado?)
    public int MaxHp
    {
        get
        {
            return maxHp;
        }
        set
        {
            maxHp--; Debug.Log(maxHp);
        }
        
    }
    
    protected static int enemyCount = 0;
    //Constructor para contar enemigos
    public Enemy()
    {
        enemyCount++;
    }
}
