using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public Slider slider;
    public Text healthText;

    void Start()
    {
        Health.Instance.OnHealthChanged += UpdateSlider;
    }

    void UpdateSlider(int currentHealth, int maxHealth)
    {
        slider.value = (float)currentHealth / maxHealth;
        healthText.text = currentHealth.ToString();
    }

    void OnDestroy()
    {
        Health.Instance.OnHealthChanged -= UpdateSlider;
    }
}
