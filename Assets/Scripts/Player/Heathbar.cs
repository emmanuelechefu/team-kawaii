using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalHealthBar.fillAmount = (playerHealth.hp /3f) *0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = (playerHealth.hp /3f) *0.6f;
    }
}
