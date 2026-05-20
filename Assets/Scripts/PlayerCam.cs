using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    public float sensitivityX;
    public float sensitivityY;

    public Transform oritentation;
    public Transform ActualBody;

    float xRotation;
    float yRotation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        // get the mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

        yRotation += mouseX; 
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f); // clamping to only max 60 up and down

        // rotating the camera and body
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        oritentation.rotation = Quaternion.Euler(0, yRotation, 0);
        ActualBody.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
