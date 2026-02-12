using System;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public static CrosshairController Instance { get; private set; }

    public event EventHandler<MarkedEnemyEventArgs> OnMarkedEnemy;
    public event EventHandler OnUnmarkedEnemy;
    public class MarkedEnemyEventArgs : EventArgs
    {
        public EnemyVisual markedEnemy;
    }

    public GameObject crosshairUI;
    private RectTransform crosshairRect;

    [Header("Raycast Settings")]
    public float rayDistance = 200f;
    public LayerMask enemyLayer;

    private void Awake()
    {
        Instance = this;
        crosshairRect = crosshairUI.GetComponent<RectTransform>();
        if (crosshairRect == null)
            Debug.LogError("crosshairUI requires a RectTransform component.");
    }

    private void Update()
    {
        if (crosshairUI.activeSelf)
        {
            // Move crosshair to mouse
            crosshairRect.position = Input.mousePosition;

            // Detect enemy when clicking
            if (Input.GetMouseButtonDown(0))
                TryMarkEnemy();
            if(Input.GetMouseButtonDown(1))
                OnUnmarkedEnemy?.Invoke(this, EventArgs.Empty);
        }
    }

    private void TryMarkEnemy()
    {
        Camera cam = PlayerCamera.Instance.mainCam;

        Ray ray = cam.ScreenPointToRay(crosshairRect.position);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, enemyLayer))
        {
            Debug.Log("Hit enemy: " + hit.collider.name);
            EnemyVisual enemy = hit.collider.GetComponent<EnemyVisual>();
            if (enemy != null)
            {
                MarkedEnemy(enemy);
            }
        }
    }

    private void MarkedEnemy(EnemyVisual enemy)
    {
        Debug.Log("invoke marked enemy event for: " + enemy.name);
        OnMarkedEnemy?.Invoke(this, new MarkedEnemyEventArgs { markedEnemy = enemy });
    }

    public void ShowCrosshair(bool show)
    {
        crosshairUI.SetActive(show);

        if (show)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}