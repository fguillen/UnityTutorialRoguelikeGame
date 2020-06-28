using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
  public List<GameObject> enemies = new List<GameObject>();
  public bool openWhenEnemiesDeath = true;
  public RoomController theRoom;

  // Start is called before the first frame update
  void Start()
  {
    theRoom.closeWhenEnter = openWhenEnemiesDeath;
  }

  // Update is called once per frame
  void Update()
  {
    CheckEnemiesForOpenDoors();  
  }

  void CheckEnemiesForOpenDoors()
  {
    if (!theRoom.doorsOpen && openWhenEnemiesDeath && theRoom.isActive)
    {
      for (int i = 0; i < enemies.Count; i++)
      {
        if (enemies[i] == null)
        {
          enemies.RemoveAt(i);
          i--;
        }

        if (enemies.Count == 0)
        {
          theRoom.OpenDoors();
        }
      }
    }
  }
}
