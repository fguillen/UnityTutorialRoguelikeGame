using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
  public bool closeWhenEnter = true;
  public bool openWhenEnemiesDeath = true;
  public bool isActive = false;
  public bool doorsOpen = true;

  public GameObject[] doors;
  public List<GameObject> enemies = new List<GameObject>();

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    CheckEnemiesForOpenDoors();
  }

  void CheckEnemiesForOpenDoors()
  {
    if(!doorsOpen && openWhenEnemiesDeath && isActive)
    {
      for (int i = 0; i < enemies.Count; i++)
      {
        if(enemies[i] ==  null)
        {
          enemies.RemoveAt(i);
          i--;
        }

        if(enemies.Count == 0)
        {
          OpenDoors();
        }
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      ActivateRoom();
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      DeactivateRoom();
    }
  }

  private void ActivateRoom()
  {
    isActive = true;
    CameraController.instance.GoToTarget(transform);

    if (closeWhenEnter)
    {
      CloseDoors();
    }
  }

  private void DeactivateRoom()
  {
    isActive = false;
  }

  private void CloseDoors()
  {
    foreach (GameObject door in doors)
    {
      door.SetActive(true);
    }
    doorsOpen = false;
  }

  private void OpenDoors()
  {
    foreach (GameObject door in doors)
    {
      door.SetActive(false);
    }
    doorsOpen = true;
    closeWhenEnter = false;
  }
}
