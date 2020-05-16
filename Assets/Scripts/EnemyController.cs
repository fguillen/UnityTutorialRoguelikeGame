using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public float moveSpeed;
  public Rigidbody2D theRB;
  public Vector3 moveDirection;
  public float rangeToChasePlayer;
  public Animator anim;
  public int health = 150;
  public GameObject[] deathSplatters;
  public GameObject bleedingEffect;
  public bool shouldShoot;
  public Transform firePoint;
  public GameObject bullet;
  public float fireRate;
  private float fireCounter;
  public SpriteRenderer theBody;
  public float rangeShoot;

  // Start is called before the first frame update
  void Start()
  {
        
  }

  // Update is called once per frame
  void Update()
  {
    if (theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
    {
      // Chasing
      if (distanceToPlayer() < rangeToChasePlayer)
      {
        moveDirection = PlayerController.instance.transform.position - transform.position;

        // flip
        if (PlayerController.instance.transform.position.x < transform.position.x)
        {
          transform.localScale = Vector3.one;
        }
        else
        {
          transform.localScale = new Vector3(-1f, 1f, 1f);
        }

      }
      else
      {
        moveDirection = Vector3.zero;
      }

      moveDirection.Normalize();
      theRB.velocity = moveDirection * moveSpeed;

      // Shooting
      if (shouldShoot && (distanceToPlayer() < rangeShoot))
      {
        fireCounter -= Time.deltaTime;
        if (fireCounter <= 0)
        {
          Instantiate(bullet, firePoint.position, firePoint.rotation);
          fireCounter = fireRate;
        }
      }
    } else
    {
      moveDirection = Vector3.zero;
      theRB.velocity = moveDirection * moveSpeed;
    }

    // isMoving?
    if(moveDirection != Vector3.zero)
    {
      anim.SetBool("isMoving", true);
    }
    else
    {
      anim.SetBool("isMoving", false);
    }
  }

  private float distanceToPlayer()
  {
    return Vector3.Distance(transform.position, PlayerController.instance.transform.position);
  }

  public void DamageEnemy(int damage)
  {
    health -= damage;

    Instantiate(bleedingEffect, transform.position, transform.rotation);

    if(health <= 0)
    {
      Destroy(gameObject);

      int deathSplatterIndex = Random.Range(0, deathSplatters.Length);
      int rotationIndex = Random.Range(0, 4);

      Instantiate(deathSplatters[deathSplatterIndex], transform.position, Quaternion.Euler(0f, 0f, 90F * rotationIndex));
    }
  }
}
