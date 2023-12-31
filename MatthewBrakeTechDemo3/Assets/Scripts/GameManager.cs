using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Enemy References")]
    public GameObject enemyUIPanel;
    public Image enemyIcon;
    public Slider enemyHealthSlider;
    public Slider enemyManaSlider;
    public EnemyController activeEnemy;
    public List<Image> BuffsDebuffs; 
    
   
    public List<EnemyController> enemies;

    [Header("General References")]
    public Slider castBarSlider;
    public GameObject autoAttack;
    public Image fireBallButtonImage; 
    private FireBall fireBall;
    private ArcaneMissile arcaneMissile;
    private FrostLance FrostLance;
    private MageArmor armor;
    public Image mageBuffImage;
    public TextMeshProUGUI armorBuffText;
    public Image armorButtonImage; 
    public TextMeshProUGUI armorCoolDownText;
    
    

    //public Image FireBallEnabled;

    [Header("Player References")]
    public GameObject playerUIPanel;
    public Slider playerHealthSlider;
    public Slider playerManaSlider;
    public PlayerController player;
    public Ability activeAbility;
    public List<Ability> UIAbilities;
    private bool isPlayerDead;
    public bool CastBarFull; 
    
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        enemyUIPanel.SetActive(false);
        autoAttack.SetActive(false);
        isPlayerDead= false;
        mageBuffImage.enabled=false;
        armorButtonImage.enabled=false;
        fireBallButtonImage.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerUI();
        updateEnemyUI();
        UpdateGame();
       
        
    }

    public void Respawn()
    {
        /// reset player and enemy values here 
        Debug.Log("Player has respawned");  ///happens on every frame somehow so continuously respawns 
    }
    //public void CheckPlayerHealth()
    //{
    //    bool isRespawning = player.IsPlayerDead();
    //    if (isRespawning)
    //    {
    //        ///respawn after 3 seconds at full health 
    //        isRespawning = false;
    //        Debug.Log("Respawning!");
    //        Respawn();
           
            
    //    }
    //    else
    //    {
            
    //    }
        
    //}
    public void SetActiveEnemy(EnemyController newEnemy)
    {
        if (newEnemy != null)
        {
            Debug.Log("Setting active enemy: " + newEnemy.name);
            activeEnemy = newEnemy;
            fireBallButtonImage.enabled=false;  
            
        }
        else
        {
            activeEnemy = null;
            enemyIcon.sprite = null;
            enemyHealthSlider.value = float.MinValue;
            fireBallButtonImage.enabled = true;
        }



    }

    void updateEnemyUI()
    {
        // enemyUIPanel.SetActive(true);

        if (activeEnemy != null)
        {
            // Debug.Log("Updating UI for active enemy: " + activeEnemy.name);
            enemyUIPanel.SetActive(true);
            enemyIcon.sprite = activeEnemy.EnemyStats.icon;
            enemyHealthSlider.value = activeEnemy.currentHealth;
            autoAttack.SetActive(true);
        }
        else
        {
            enemyUIPanel.SetActive(false);
            autoAttack.SetActive(false); 
        }
        if(activeEnemy.isEnemyDead)
        {
            activeEnemy = null;
            enemyUIPanel.SetActive(false);
            autoAttack.SetActive(false);

        }

        ///add and remove abilities from player and enemy icons 
        ///for(all abilities in active abilities, add to sprite in abilities panel) 
        

    }
    void UpdatePlayerUI()
    {
        playerHealthSlider.value = player.currentHealth; 
        playerManaSlider.value = player.currentMana;
        


    }

    public void SetActiveCast(Ability ability)
    {
        activeAbility = ability;
        UIAbilities.Add(activeAbility);
        ///or add ability directly into the active ability list rather than assigning it to singular variable 

        //activeAbilityList.Add(activeAbility); ////smth like this? 
    }

    ///function to load cast bar and start particle effects HERE. needs to take casting time as parameter and return to ability function when done 
   public void LoadCastBar(float castTime)
    {
        Debug.Log("Loading Cast Bar");
        castBarSlider.value = 0;
        StartCoroutine(FillCastBar(castTime));
    }
    IEnumerator FillCastBar(float castTime)
    {
        CastBarFull = false;
        float elapsedTime = 0;
        castBarSlider.maxValue = castTime; 
        while (elapsedTime < castTime)
        {
            elapsedTime+= Time.deltaTime;
            castBarSlider.value = elapsedTime;
            yield return null; 

            
            
        }
        
        castBarSlider.value = 0;
    }

    public bool isDead()
    {
        if(player.IsPlayerDead())
        {
            return true;
            
        }
        else
        {
            return false;
        }

    }

    public void UpdateGame() //resetting enemy pos and health when player dies 
    {
        if(isDead())
        {
           foreach(EnemyController enemy in enemies)
            {
                
                
                    enemy.initialiseEnemy();
                
                
               
            }
        }
    }

    public void ActivateMageBuff()
    {
        mageBuffImage.enabled = true;
        armorButtonImage.enabled = false;
        Debug.Log("Enabling MI");
    }
    public void DisableMageBuff()
    {
        mageBuffImage.enabled = false;
        armorButtonImage.enabled = true;
        Debug.Log("Disabling MI"); 
    }

   public void DisplayArmorBuffText(float remainingTime)
    {
        int TimeLeft = Mathf.CeilToInt(remainingTime);
        armorBuffText.text = TimeLeft.ToString(); 
    }
    public void DisplayMageArmorCoolDown(float remainingTime)
    {
        int TimeLeft = Mathf.CeilToInt(remainingTime);
        armorCoolDownText.text = TimeLeft.ToString(); 

    }

    public void ApplyToxicSplit()
    {

    }
   
   
}
