using System;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] private EnemyVisual enemy;
    [SerializeField] private GameObject markedVisual;
    private void Start()
    {
        CrosshairController.Instance.OnMarkedEnemy += HandleMarkedEnemy;
        CrosshairController.Instance.OnUnmarkedEnemy += HandleUnMarkedEnemy;
        Hide();
    }

    private void HandleUnMarkedEnemy(object sender, EventArgs e)
    {
        Hide();
    }

    private void HandleMarkedEnemy(object sender, CrosshairController.MarkedEnemyEventArgs e)
    {
        if (e.markedEnemy == enemy)
        {
            Show();
        }
    
    }

    private void Show()
    {
        Debug.Log("Showing marked visual for " + enemy.name);
        markedVisual.SetActive(true);
    }

    private void Hide()
    {
        markedVisual.SetActive(false);
    }
}