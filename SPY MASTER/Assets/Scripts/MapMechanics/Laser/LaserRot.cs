using UnityEngine;
//Raúl Saavedra de la Riera
public class LaserRot : MonoBehaviour
{
    [SerializeField]
    float anglesRotation = 0f;
    [SerializeField]
    float speed = 0f;

    float realRot, iniPos, actualRot;
    bool mov;
    // Start is called before the first frame update
    void Start()
    {
        realRot = Mathf.Abs(anglesRotation + transform.eulerAngles.z)%360;
        iniPos = transform.eulerAngles.z;
        mov = true;
    }

    // Update is called once per frame
    void Update()
    {

        actualRot = transform.eulerAngles.z;
        float target;
        if (mov) target = realRot;
        else target = iniPos;

        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, target , speed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, angle);

        if (Mathf.Abs(target - transform.eulerAngles.z) < 0.1f) mov = !mov;
    }
}
