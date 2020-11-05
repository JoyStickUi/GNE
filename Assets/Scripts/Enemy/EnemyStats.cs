using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    AnimatorHandler animatorHandler;

    public BossHealthBar bossHealthBar;

    private void Awake(){
        // animatorHandler = GetComponentInChildren<AnimatorHandler>();
    }

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        bossHealthBar.SetMaxHealth(maxHealth);
    }

    private int SetMaxHealthFromHealthLevel(){
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage){
        currentHealth = currentHealth - damage;
        bossHealthBar.SetCurrentHealth(currentHealth);
        // animatorHandler.PlayTargetAnimation("Damage", true);

        if(currentHealth <= 0){
            currentHealth = 0;
            // animatorHandler.PlayTargetAnimation("Dead", false);
        }
    }
}
