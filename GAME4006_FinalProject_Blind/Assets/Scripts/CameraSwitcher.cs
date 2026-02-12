using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    public CinemachineFreeLook[] freeLookCamerasArray;

    private void Start()
    {
        ActivateCamera(0); // Start with the first virtual camera
    }

    private void Update()
    {
        for (int i = 0; i < freeLookCamerasArray.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                ActivateCamera(i);
            }
        }
    }

    private void ActivateCamera(int index)
    {
        for (int i = 0; i < freeLookCamerasArray.Length; i++)
        {
            freeLookCamerasArray[i].Priority = (i == index) ? 10 : 0;
        }

        if (index == 0)
        {
            PlayerCamera.Instance.enableMouseLook = true;  // Enable mouse look for blind scanning
            PlayerCamera.Instance.SetBlind(true);
            CrosshairController.Instance.ShowCrosshair(false);
        }
        else
        {
            PlayerCamera.Instance.enableMouseLook = false;  // Disable mouse look for free cursor aiming
            PlayerCamera.Instance.SetBlind(false);
            CrosshairController.Instance.ShowCrosshair(true);
        }
    }
}