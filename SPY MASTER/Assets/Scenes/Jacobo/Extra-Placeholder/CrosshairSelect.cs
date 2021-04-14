using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairSelect : MonoBehaviour
{
    /// <summary> 
    /// Cambia el cursor a cursorArrow01 al comenzar.
    ///Escalados ambos a 64x64. 32 muy pequeno
    /// Si el cursor se encuentra sobre este gameObject, cambia a cursorArrow02
    /// </summary>
    
    public Texture2D cursorArrow01, cursorArrow02;
    Vector2 cursorHotspot;
    
    private void Start() 
    {
        cursorHotspot = new Vector2 (cursorArrow01.width / 2, cursorArrow01.height / 2);
        Cursor.SetCursor(cursorArrow01, cursorHotspot, CursorMode.ForceSoftware);
    }

    void OnMouseEnter()
    {
        /*Generalizar esto dentro de la clase padre de enemy en un futuro, para aplicarlo a cada 
        instancia de enemigo directamente, porque este metodo es basura(placeholder en esta escena)
        Cambiar el tinte del cursor en vez de tener 2 Textures2D? //OnGUI se llama multiples veces en cada frame de Update
        */
        Cursor.SetCursor(cursorArrow02, cursorHotspot, CursorMode.ForceSoftware);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(cursorArrow01, cursorHotspot, CursorMode.ForceSoftware);
    }
}
