using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform cameraTarget;
    public Transform playerModel;
    public float minimumAngle;
    public float maximumAngle;
    public float mouseSensitivity;
    public bool stickCamera;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        float aimX = Input.GetAxis("Mouse X");
        float aimY = Input.GetAxis("Mouse Y");
        cameraTarget.rotation *= Quaternion.AngleAxis(aimX * mouseSensitivity, Vector3.up);
        cameraTarget.rotation *= Quaternion.AngleAxis(-aimY * mouseSensitivity, Vector3.right);


        var angleX = cameraTarget.localEulerAngles.x;
        if (angleX > 180 && angleX < maximumAngle)
        {
            angleX = maximumAngle;
        }
        else if (angleX < 180 && angleX > minimumAngle)
        {
            angleX = minimumAngle;
        }

        cameraTarget.localEulerAngles = new Vector3(angleX, cameraTarget.localEulerAngles.y, 0);

        if (stickCamera)
        {
            playerModel.rotation = Quaternion.Euler(0, cameraTarget.rotation.eulerAngles.y, 0);
        }
    }
}
