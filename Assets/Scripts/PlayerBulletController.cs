using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
  public Rigidbody2D theRB;
  public float speed = 7.5f;
  public GameObject bulletImpactEffect;
  public GameObject bulletEnemyEffect;
  public int damageToGive;

  // Start is called before the first frame update
  void Start()
  {
    AudioManager.instance.playSFX("Shoot2");  
  }

  // Update is called once per frame
  void Update()
  {
    theRB.velocity = transform.right * speed;
        
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    Destroy(gameObject);

    if (other.tag == "Enemy")
    {
      other.GetComponent<EnemyController>().DamageEnemy(damageToGive);
      Instantiate(bulletEnemyEffect, transform.position, transform.rotation);
    } else
    {
      AudioManager.instance.playSFX("Impact");
      Instantiate(bulletImpactEffect, transform.position, transform.rotation);
    }
  }

  private void OnBecameInvisible()
  {
    Destroy(gameObject);
  }
}
