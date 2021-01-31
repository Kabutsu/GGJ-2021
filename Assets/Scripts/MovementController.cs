using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float MovementSpeed = 10f;
    public Rigidbody m_Rigidbody;

    void Start()
    {
        
    }

    void Update()
    {
        float vTranslate = Input.GetAxis("Vertical") * MovementSpeed * Time.deltaTime;
        float hTranslate = Input.GetAxis("Horizontal") * MovementSpeed * Time.deltaTime;

        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        transform.Translate(hTranslate, vTranslate, 0, Camera.main.transform);
        Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);

        if (groundPlane.Raycast(cameraRay, out float rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}
