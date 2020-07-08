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
  public string enemyKind;

  [Header("Wander")]
  public float wanderTime;
  private float wanderCounter;
  public float wanderPauseTime;
  private float wanderPauseCounter;

  [Header("Patrol")]
  public Transform[] patrolPoints;
  public int actualPatrolPoint = 0;

  // Start is called before the first frame update
  void Start()
  {
    if(enemyKind == "wander"){
      wanderPauseCounter = Random.Range(wanderPauseTime * 0.75f, wanderPauseTime * 1.25f);
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
    {
      moveDirection = CalculateMoveDirection();
      theRB.velocity = moveDirection * moveSpeed;
      Flip();

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
    }
    else
    {
      moveDirection = Vector3.zero;
      theRB.velocity = moveDirection * moveSpeed;
    }

    // isMoving?
    if (moveDirection != Vector3.zero)
    {
      anim.SetBool("isMoving", true);
    }
    else
    {
      anim.SetBool("isMoving", false);
    }
  }

  void Flip() {
    if (PlayerController.instance.transform.position.x < transform.position.x)
    {
      transform.localScale = Vector3.one;
    }
    else
    {
      transform.localScale = new Vector3(-1f, 1f, 1f);
    }
  }

  Vector3 CalculateMoveDirection()
  {
    switch (enemyKind)
    {
      case "skeleton":
        return CalculateMoveDirectionForSkeleton();
      case "coward":
        return CalculateMoveDirectionForCoward();
      case "wander":
        return CalculateMoveDirectionForWander();
      case "patrol":
        return CalculateMoveDirectionForPatrol();
      default:
        throw new System.ArgumentException("Not valid enemyKind", "enemyKind");
    }
  }

  Vector3 CalculateMoveDirectionForPatrol()
  {
    Vector3 direction = moveDirection;

    // Chasing
    if (distanceToPlayer() < rangeToChasePlayer)
    {
      direction = PlayerController.instance.transform.position - transform.position;
    }
    else
    {
      Transform patrolPoint = patrolPoints[actualPatrolPoint];

      if(Vector3.Distance(transform.position, patrolPoint.position) < 0.1f) {
        actualPatrolPoint++;

        if(actualPatrolPoint == patrolPoints.Length){
          actualPatrolPoint = 0;
        }
      }

      direction = patrolPoint.position - transform.position;
    }

    direction.Normalize();

    return direction;
  }

  Vector3 CalculateMoveDirectionForWander()
  {
    Vector3 direction = moveDirection;

        // Chasing
    if (distanceToPlayer() < rangeToChasePlayer)
    {
      direction = PlayerController.instance.transform.position - transform.position;
      direction.Normalize();
    }
    else
    {
      if(wanderCounter > 0){
        wanderCounter -= Time.deltaTime;

        if(wanderCounter <= 0){
          wanderPauseCounter = Random.Range(wanderPauseTime * 0.75f, wanderPauseTime * 1.25f);
        }
      }

      if(wanderPauseCounter > 0){
        wanderPauseCounter -= Time.deltaTime;

        if(wanderPauseCounter <= 0){
          direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
          direction.Normalize();

          wanderCounter = Random.Range(wanderTime * 0.75f, wanderTime * 1.25f);
        } else {
          direction = Vector3.zero;
        }
      }
    }

    return direction;
  }

  Vector3 CalculateMoveDirectionForSkeleton()
  {
    // Chasing
    if (distanceToPlayer() < rangeToChasePlayer)
    {
      moveDirection = PlayerController.instance.transform.position - transform.position;
    }
    else
    {
      moveDirection = Vector3.zero;
    }

    moveDirection.Normalize();

    return moveDirection;
  }

  Vector3 CalculateMoveDirectionForCoward()
  {
    // Running away
    if (distanceToPlayer() < rangeToChasePlayer)
    {
      moveDirection = transform.position - PlayerController.instance.transform.position;
    }
    else
    {
      moveDirection = Vector3.zero;
    }

    moveDirection.Normalize();

    return moveDirection;
  }

  private float distanceToPlayer()
  {
    return Vector3.Distance(transform.position, PlayerController.instance.transform.position);
  }

  public void DamageEnemy(int damage)
  {
    AudioManager.instance.playSFX("Enemy Hurt");

    health -= damage;

    Instantiate(bleedingEffect, transform.position, transform.rotation);

    if (health <= 0)
    {
      Death();
    }
  }

  private void Death()
  {
    AudioManager.instance.playSFX("Enemy Death");

    Destroy(gameObject);

    int deathSplatterIndex = Random.Range(0, deathSplatters.Length);
    int rotationIndex = Random.Range(0, 4);

    Instantiate(deathSplatters[deathSplatterIndex], transform.position, Quaternion.Euler(0f, 0f, 90F * rotationIndex));
  }
}
