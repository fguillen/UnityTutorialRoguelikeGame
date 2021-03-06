﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  public static PlayerController instance;

  Gamepad gamepad;

  public float currentSpeed;
  public float moveSpeed;
  private Vector2 moveDirection;
  public Rigidbody2D theRB;
  public Animator anim;
  public GameObject bulletToBeFired;
  public Transform firePoint;
  public float timeBetweenShots;
  private float shotCounter;
  public Transform gunArm;
  private Camera theCam;
  public SpriteRenderer theBodySR;
  public string controller = "cursor";

  // dashing
  public float dashTime = .5f;
  private float dashCounter = 0;
  public float dashSpeed = 8f;
  public float dashCoolDownTime = 1f;
  private float dashCoolDownCounter = 0;
  public float dashInvincibleTime = .5f;

  // invincible
  private float invincibleCounter = 0f;

  private bool canMove = true;


  private void Awake()
  {
    gamepad = Gamepad.current;
    instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    theCam = Camera.main;
    moveDirection = Vector2.zero;
    currentSpeed = moveSpeed;
    Time.timeScale = 1f;
  }

  public bool IsDashing()
  {
    return (dashCounter > 0);
  }

  private bool IsDashCoolingDown()
  {
    return (dashCoolDownCounter > 0);
  }

  // Update is called once per frame
  void Update()
  {

    if (!IsDashing()) {
      moveDirection = MoveDirection();
    }

    CheckDashing();
    CheckInvincible();


    theRB.velocity = moveDirection * currentSpeed;



    Vector2 gunDirection = GunDirection();

    // flip Player

    if (gunDirection.x < 0)
    {
      transform.localScale = new Vector3(-1f, 1f, 1f);
      gunArm.localScale = new Vector3(-1f, -1f, 1f);

    } else
    {
      transform.localScale = Vector3.one;
      gunArm.localScale = Vector3.one;
    }




    if (ShootButtonDown())
    {
      Shoot();
    }

    if (ShootButton())
    {
      shotCounter -= Time.deltaTime;

      if(shotCounter <= 0)
      {
        Shoot();
      }
    }


    // rotate gum arm

    float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;

    gunArm.rotation = Quaternion.Euler(0, 0, angle);

    if(moveDirection != Vector2.zero)
    {
      anim.SetBool("isMoving", true);
    } else
    {
      anim.SetBool("isMoving", false);
    }

  }

  private bool ShootButtonDown()
  {
    return (
      Input.GetMouseButtonDown(0) ||
      Input.GetKeyDown(KeyCode.Space) ||
      (gamepad != null && gamepad.rightShoulder.wasPressedThisFrame)
    );
  }

  private bool ShootButton()
  {

    return (
      Input.GetMouseButton(0) ||
      Input.GetKey(KeyCode.Space) ||
      (gamepad != null && gamepad.rightShoulder.isPressed)
    );
  }

  private void Shoot()
  {
    Instantiate(bulletToBeFired, firePoint.position, firePoint.rotation);
    shotCounter = timeBetweenShots;
  }

  private bool DashButtonDown()
  {
    return (
      Input.GetKey(KeyCode.D) ||
      (gamepad != null && gamepad.leftShoulder.wasPressedThisFrame)
    );
  }

  public void CheckDashing()
  {
    if (!IsDashing() && !IsDashCoolingDown())
    {
      if (DashButtonDown())
      {
        AudioManager.instance.playSFX("Player Dash");
        dashCounter = dashTime;
        moveDirection = MoveDirection();
        currentSpeed = dashSpeed;
        anim.SetTrigger("dash");
        MakeInvincible(dashInvincibleTime);
      }
    }

    if (IsDashing())
    {
      dashCounter -= Time.deltaTime;

      if(dashCounter <= 0)
      {
        currentSpeed = moveSpeed;
        dashCoolDownCounter = dashCoolDownTime;
      }
    }

    if (IsDashCoolingDown())
    {
      dashCoolDownCounter -= Time.deltaTime;
    }
  }

  public Vector2 MoveDirection()
  {
    if (!canMove)
    {
      return Vector2.zero;
    }

    Vector2 moveInput = new Vector2();
    moveInput.x = Input.GetAxisRaw("Horizontal");
    moveInput.y = Input.GetAxisRaw("Vertical");
    moveInput.Normalize();

    return moveInput;
  }

  private Vector2 GunDirection()
  {
    if (controller == "mouse")
    {
      return GunDirectionMouseController();
    } else if (controller == "cursor")
    {
      return MoveDirection();
    } else if (controller == "pad")
    {
      return GunDirectionPadController();
    }

    return new Vector2();
  }

  private Vector2 GunDirectionPadController()
  {
    Vector2 move = gamepad.rightStick.ReadValue();

    return move;
  }

  private Vector2 GunDirectionMouseController()
  {
    Vector3 mousePos = Input.mousePosition;
    Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);
    Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
    return offset;
  }

  public bool IsInvincible()
  {
    return(invincibleCounter > 0);
  }

  public void MakeInvincible(float time)
  {
    invincibleCounter = time;
    theBodySR.color = new Color(theBodySR.color.r, theBodySR.color.g, theBodySR.color.b, 0.5f);
  }

  private void RemoveInvicible()
  {
    theBodySR.color = new Color(theBodySR.color.r, theBodySR.color.g, theBodySR.color.b, 1f);
  }

  public void CheckInvincible()
  {
    if(IsInvincible())
    {
      invincibleCounter -= Time.deltaTime;
      if(invincibleCounter <= 0)
      {
        RemoveInvicible();
      }
    }
  }

  public void SetCanMove(bool value)
  {
    canMove = value;
  }

}
