using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
  
    public static AbilitiesManager instance;

    private PlayerController player;
    private FireBall fireBall;
    private ArcaneMissile missile;
    private MageArmor mageArmor;
    
    private GameObject activeFireBall;
    private GameObject activeMissile;

    private bool toxicSplitActive = false;
    private float toxicSplitDamage = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = FindFirstObjectByType<AbilitiesManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
         
    }
    public IEnumerator UseFireball(PlayerController player, EnemyController targetPos, GameObject fireBallPrefab, float speed, float CastTime)
    {
       yield return StartCoroutine(CastingDelay(CastTime));
        Vector3 spawnPos = player.transform.position; 
       
        //Debug.Log("FireBall made from abilities manager");
        activeFireBall = Instantiate(fireBallPrefab, spawnPos, Quaternion.identity);

        Vector3 direction = (targetPos.transform.position - spawnPos).normalized;
        //Debug.Log("Direction: " + direction);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Debug.Log("Angle: " + angle);
        activeFireBall.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        activeFireBall.GetComponent<Rigidbody2D>().velocity = direction * speed; 
        //DamageManager.DealEnemyAbilityDamage(targetPos, fireBall);
    }


    public IEnumerator UseMissile(PlayerController player, EnemyController targetPos, GameObject missilePrefab, float speed, float CastTime)
    {
        yield return StartCoroutine(CastingDelay(CastTime));
        
       
       
    }

    public IEnumerator UseFrostLance()
    {
        yield return new WaitForSeconds(3f);
    }

    //public IEnumerator UseMageArmor()
    //{
    //    player.defenceMultiplier = 0.65f;
    //    player.ManaRegenAmount = 25f;
    //    float originalLastTime = player.lastMageArmorTime;
       
    //    yield return new WaitForSeconds(mageArmor.buffDuration);
       
    //    player.defenceMultiplier = 1f;
    //    player.ManaRegenAmount = 12f;  
    //    player.lastMageArmorTime = originalLastTime;
    //    StartCoroutine(MageArmorCoolDown());
       
    //}

    //private IEnumerator MageArmorCoolDown()
    //{
    //    //lastMageArmorTime = Time.time;
    //    yield return new WaitForSeconds(mageArmor.coolDown);
    //    Debug.Log("Cool down!");
    //}


    public IEnumerator CastingDelay(float castTime)
    {
        GameManager.instance.LoadCastBar(castTime);
        yield return new WaitForSeconds(castTime);
        Debug.Log("Causing delay whilst casting spell");
    }

    

    public IEnumerator UseToxicSplit()
    {
        yield return new WaitForSeconds(2.5f);
        Debug.Log("Toxic Split Cast");
        toxicSplitActive = true;
        GameManager.instance.ApplyToxicSplit();
        StartCoroutine(ToxicSplitDuration());
    }

    private IEnumerator ToxicSplitDuration()
    {
        yield return new WaitForSeconds(20f);
        toxicSplitActive = false;
        toxicSplitDamage = 0;
        Debug.Log("Toxic Split ended");
        
    }

    public void ApplyToxicSplitDamage(float damage)
    {
        if(toxicSplitActive == true)
        {
            toxicSplitDamage += damage * 2;
            Debug.Log("Toxic Split damage: " + toxicSplitDamage);
        }
    }
}
