using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
  public GameObject templateRoom;
  public Color firstRoomColor;
  public Color lastRoomColor;
  public Transform generatorPoint;

  // Start is called before the first frame update
  void Start()
  {
    Instantiate(templateRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = firstRoomColor;
  }

  // Update is called once per frame
  void Update()
  {
    
  }
}
