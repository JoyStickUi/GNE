using UnityEngine;

public class EnemyStats : CharacterStats
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;
    public bool isHitted = false;
    public bool isDead = false;

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
        isHitted = true;
        // animatorHandler.PlayTargetAnimation("Damage", true);

        if(currentHealth <= 0){
            currentHealth = 0;
            isDead = true;
            // animatorHandler.PlayTargetAnimation("Dead", false);
        }
    }
}
