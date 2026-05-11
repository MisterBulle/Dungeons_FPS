using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector]
    public float playerhealth;

    private float learpTimer;
    public float maxHealth;
    public float Chipspeed;
    public Image frontHealthBar;
    public Image backHealthBar;

    void Start()
    {
        // Load stats from DataManager
        if (DataManager.Instance != null)
        {
            maxHealth = DataManager.Instance.playerStats.maxHealth;
            Chipspeed = DataManager.Instance.playerStats.chipSpeed;
        }

        playerhealth = maxHealth;
    }

    void Update()
    {
        playerhealth = Mathf.Clamp(playerhealth, 0, maxHealth);
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        float fillf = frontHealthBar.fillAmount;
        float fillb = backHealthBar.fillAmount;
        float hFraction = playerhealth / maxHealth;

        //TakeDamage
        if(fillb > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            learpTimer += Time.deltaTime;
            float percentComplete = learpTimer / Chipspeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillb, hFraction, percentComplete);
        }
        //Heal
        if(fillf < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            learpTimer += Time.deltaTime;
            float percentComplete = learpTimer / Chipspeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillf, backHealthBar.fillAmount, percentComplete);
        }

    }

    public void TakeDamage(float damage)
    {
        playerhealth -= damage;
        learpTimer = 0f;
    }

    public void Heal(float healAmount)
    {
        playerhealth += healAmount;
        learpTimer = 0f;
    }
}
