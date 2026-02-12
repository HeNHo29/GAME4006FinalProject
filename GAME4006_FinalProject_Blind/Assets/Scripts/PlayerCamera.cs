using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance { get; private set; }

    public Camera mainCam;

    [Header("References")]
    public Transform playerBody;      
    public Transform cameraRoot;    

    [Header("Settings")]
    public float mouseSensitivity = 200f;
    public float verticalClamp = 80f;
    public bool enableMouseLook = true;

    [Header("Layers")]
    public LayerMask blindMask;     // nothing
    public LayerMask normalMask;    // everything

    float xRotation = 0f;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!enableMouseLook) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalClamp, verticalClamp);

        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void SetBlind(bool blind)
    {
        if (blind)
            mainCam.cullingMask = blindMask;
        else
            mainCam.cullingMask = normalMask;
    }

}