using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
  public Rigidbody2D theRB;
  public float speed = 7.5f;
  public GameObject bulletImpactEffect;

  // Start is called before the first frame update
  void Start()
  {
        
  }

  // Update is called once per frame
  void Update()
  {
    theRB.velocity = transform.right * speed;
        
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    Instantiate(bulletImpactEffect, transform.position, transform.rotation);
    Destroy(gameObject);
  }

  private void OnBecameInvisible()
  {
    Destroy(gameObject);
  }
}
