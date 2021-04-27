using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVisionCone : MonoBehaviour
{
    [SerializeField]
    LayerMask wallLayer;

    float fov; // Rango de vision en grados
    float viewDistance; // Distancia del cono

    //[SerializeField]
    //Mesh mesh; // Encargado de representar el cono de vision
    Vector3 origin;
    float startingAngle; // Angulo desde el que empieza a generarse los triangulos del Mesh en direccion "agujas del reloj"

    int rayCount = 100; // Numero de Rayos usados (incluye el 0)
    float angle;
    float angleIncrese;

    void Start()
    {
        angleIncrese = fov / rayCount; // Calcular y almacenar el angulo que ocupa cada uno de los triangulos del Mesh
    }

    public void SetFov(float fovV)
    {
        fov = fovV;
    }
    public void SetViewDistance(float viewDistanceV)
    {
        viewDistance = viewDistanceV;
    }

    private void Update()
    {
        angle = startingAngle; // Se utiliza para la creacion de los triangulos, recorre el cono asignando triangulos

        // Asignacion de tamaños de los arrays que se van a utilizar para crear el Mesh
        Vector3[] vertices = new Vector3[rayCount + 2];
        //Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex; // Punto exterior utilizado para la creacion de los triangulos del cono de vision

            RaycastHit2D rayCastHit2D = Physics2D.Raycast(origin, AngleToVector(angle), viewDistance, wallLayer);

            if (rayCastHit2D.collider == null) // No hay Collision : Poner el punto "viewDistance" unidades de distancia
                vertex = origin + AngleToVector(angle) * viewDistance;
            else // Collision con pared : Poner el punto en la posicion en la que se ha colisionado
                vertex = rayCastHit2D.point;

            vertices[vertexIndex] = vertex; // Almacenar este punto en el Array que contiene todos los arrays del Mesh "vertices"

            // Configurar los triangulos : Comunicarle al programa el orden de los vertices (unidos con lineas) // Debe ser en la direccion agujas del reloj
            if (i > 0) // No hacer este proceso para el primer Rayo
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            // Actualizar valores para la siguiente vuelta
            vertexIndex++;
            angle -= angleIncrese;
        }

        // Actualizar Mesh con los nuevos valores
        //mesh.vertices = vertices;
        //mesh.uv = uv;
        //mesh.triangles = triangles;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetDirection(Vector3 direction)
    {
        startingAngle = VectorToAngle(direction) + fov / 2;
    }


    Vector3 AngleToVector(float angle) // Devuelve el Vector3 que corresponde al angulo con una magnitud de 1.
    {
        // angle = 0 - 360
        float angleRad = angle * (Mathf.PI / 180f); // Pasar el angulo de grados a radianes
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad)); // Utilizar "Mathf" para saber el coseno y el seno y almacenarlo en el vector
    }

    float VectorToAngle(Vector3 dir) // Devuelve el Vector3 que corresponde al angulo con una magnitud de 1.
    {
        dir = dir.normalized; // Normalizar el vector
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // Convierte el Vector3 a Radianes y luego a grados
        if (n < 0) n += 360; // Si como resultado se obtiene un numero negativo, se le sumara una vuelta
        return n;
    }
}
