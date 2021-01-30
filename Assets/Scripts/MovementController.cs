using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float MovementSpeed = 10f;

    void Start()
    {
        
    }

    void Update()
    {
        float vTranslate = Input.GetAxis("Vertical") * MovementSpeed * Time.deltaTime;
        float hTranslate = Input.GetAxis("Horizontal") * MovementSpeed * Time.deltaTime;

        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        transform.position += new Vector3(hTranslate, 0, vTranslate);
        Camera.main.transform.position += new Vector3(hTranslate, 0, vTranslate);

        if (groundPlane.Raycast(cameraRay, out float rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}
