using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
  
    public static AbilitiesManager instance;
    private FireBall fireBall; 
    
    private GameObject activeFireBall;
    private GameObject activeMissile;

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
       
        Debug.Log("FireBall made from abilities manager");
        activeFireBall = Instantiate(fireBallPrefab, spawnPos, Quaternion.identity);

        Vector3 direction = (targetPos.transform.position - spawnPos).normalized;
        Debug.Log("Direction: " + direction);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Debug.Log("Angle: " + angle);
        activeFireBall.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        activeFireBall.GetComponent<Rigidbody2D>().velocity = direction * speed; 
        //DamageManager.DealEnemyAbilityDamage(targetPos, fireBall);
    }


    public IEnumerator UseMissile()
    {
        yield return new WaitForSeconds(3f);
    }

    public IEnumerator UseFrostLance()
    {
        yield return new WaitForSeconds(3f);
    }

    public IEnumerator CastingDelay(float castTime)
    {
        GameManager.instance.LoadCastBar(castTime);
        yield return new WaitForSeconds(castTime);
        Debug.Log("Causing delay whilst casting spell");
    }
}
