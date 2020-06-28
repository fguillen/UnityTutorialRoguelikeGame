using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
  public GameObject templateRoom;
  public Color firstRoomColor;
  public Color lastRoomColor;
  public Transform generatorPoint;
  private enum Direction {up, right, down, left};
  public int numOfRooms;
  private float xOffset = 18;
  private float yOffset = 10;
  public LayerMask roomLayer;
  private GameObject firstRoom;
  private GameObject lastRoom;
  private List<GameObject> rooms = new List<GameObject>();
  private List<GameObject> roomBorders = new List<GameObject>();
  public RoomsPrefabs roomsPrefabs;

  // Centers
  public RoomCenter roomCenterStart;
  public RoomCenter roomCenterEnd;
  public RoomCenter[] roomCenters;


  // Start is called before the first frame update
  void Start()
  {
    GenerateRooms();
    GenerateAllRoomBorders();
    GenerateAllRoomCenters();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKey(KeyCode.R))
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  }

  void GenerateRooms()
  {
    firstRoom = Instantiate(templateRoom, generatorPoint.position, generatorPoint.rotation);
    firstRoom.GetComponent<SpriteRenderer>().color = firstRoomColor;

    for (int i = 0; i < numOfRooms; i++)
    {
      var direction = NewDirection();

      MoveGeneratorPoint(direction);

      while (IsPositionTaken(generatorPoint.position))
      {
        MoveGeneratorPoint(direction);
      }

      GameObject newRoom = GenerateRoom();

      if (i == numOfRooms - 1)
      {
        lastRoom = newRoom;
        lastRoom.GetComponent<SpriteRenderer>().color = lastRoomColor;
      }
      else
      {
        rooms.Add(newRoom);
      }
    }
  }

  bool IsPositionTaken(Vector2 position)
  {
    return (Physics2D.OverlapCircle(position, .2f, roomLayer));
  }

  void GenerateAllRoomBorders()
  {
    roomBorders.Add(GenerateRoomBorders(firstRoom));
    roomBorders.Add(GenerateRoomBorders(lastRoom));

    foreach (var room in rooms)
    {
      roomBorders.Add(GenerateRoomBorders(room));
    }
  }

  void GenerateAllRoomCenters()
  {
    foreach(GameObject roomBorder in roomBorders)
    {
      GenerateRoomCenter(roomBorder);
    }
  }

  void GenerateRoomCenter(GameObject roomBorder)
  {
    RoomCenter roomCenterSelected;

    if(roomBorder == roomBorders[0])
    {
      roomCenterSelected = roomCenterStart;
    }
    else if(roomBorder == roomBorders[roomBorders.Count - 1])
    {
      roomCenterSelected = roomCenterEnd;
    }
    else
    {
      roomCenterSelected = roomCenters[Random.Range(0, roomCenters.Length)];
    }

    RoomCenter newRoomCenter = (RoomCenter)Instantiate(roomCenterSelected, roomBorder.transform.position, roomBorder.transform.rotation);
    newRoomCenter.theRoom = roomBorder.GetComponent<RoomController>();
  }

  GameObject GenerateRoomBorders(GameObject room)
  {
    GameObject result = null;

    bool roomAtU = IsPositionTaken(room.transform.position + new Vector3(0f, yOffset, 0f));
    bool roomAtR = IsPositionTaken(room.transform.position + new Vector3(xOffset, 0f, 0f));
    bool roomAtD = IsPositionTaken(room.transform.position + new Vector3(0f, -yOffset, 0f));
    bool roomAtL = IsPositionTaken(room.transform.position + new Vector3(-xOffset, 0f, 0f));
    // roomR
    if(!roomAtU && roomAtR && !roomAtD && !roomAtL)
    {
      result = Instantiate(roomsPrefabs.roomR, room.transform.position, room.transform.rotation);
    }

    // roomU
    if (roomAtU && !roomAtR && !roomAtD && !roomAtL)
    {
      result = Instantiate(roomsPrefabs.roomU, room.transform.position, room.transform.rotation);
    }

    // roomL
    if (!roomAtU && !roomAtR && !roomAtD && roomAtL)
    {
      result = Instantiate(roomsPrefabs.roomL, room.transform.position, room.transform.rotation);
    }

    // roomD
    if (!roomAtU && !roomAtR && roomAtD && !roomAtL)
    {
      result = Instantiate(roomsPrefabs.roomD, room.transform.position, room.transform.rotation);
    }

    // roomRL
    if (!roomAtU && roomAtR && !roomAtD && roomAtL)
    {
      result = Instantiate(roomsPrefabs.roomRL, room.transform.position, room.transform.rotation);
    }

    // roomUD
    if (roomAtU && !roomAtR && roomAtD && !roomAtL)
    {
      result = Instantiate(roomsPrefabs.roomUD, room.transform.position, room.transform.rotation);
    }

    // roomUR
    if (roomAtU && roomAtR && !roomAtD && !roomAtL)
    {
      result = Instantiate(roomsPrefabs.roomUR, room.transform.position, room.transform.rotation);
    }

    // roomRD
    if (!roomAtU && roomAtR && roomAtD && !roomAtL)
    {
      result = Instantiate(roomsPrefabs.roomRD, room.transform.position, room.transform.rotation);
    }

    // roomDL
    if (!roomAtU && !roomAtR && roomAtD && roomAtL)
    {
      result = Instantiate(roomsPrefabs.roomDL, room.transform.position, room.transform.rotation);
    }

    // roomUL
    if (roomAtU && !roomAtR && !roomAtD && roomAtL)
    {
      result = Instantiate(roomsPrefabs.roomUL, room.transform.position, room.transform.rotation);
    }

    // roomURDL
    if (roomAtU && roomAtR && roomAtD && roomAtL)
    {
      result = Instantiate(roomsPrefabs.roomURDL, room.transform.position, room.transform.rotation);
    }

    // roomURL
    if (roomAtU && roomAtR && !roomAtD && roomAtL)
    {
      result = Instantiate(roomsPrefabs.roomURL, room.transform.position, room.transform.rotation);
    }

    // roomUDL
    if (roomAtU && !roomAtR && roomAtD && roomAtL)
    {
      result = Instantiate(roomsPrefabs.roomUDL, room.transform.position, room.transform.rotation);
    }

    // roomRDL
    if (!roomAtU && roomAtR && roomAtD && roomAtL)
    {
      result = Instantiate(roomsPrefabs.roomRDL, room.transform.position, room.transform.rotation);
    }

    // roomURD
    if (roomAtU && roomAtR && roomAtD && !roomAtL)
    {
      result = Instantiate(roomsPrefabs.roomURD, room.transform.position, room.transform.rotation);
    }

    return result;
}

  GameObject GenerateRoom()
  {
    return Instantiate(templateRoom, generatorPoint.position, generatorPoint.rotation);
  }

  void MoveGeneratorPoint(Direction direction)
  {
    switch (direction)
    {
      case Direction.up:
        generatorPoint.position += new Vector3(0f, yOffset, 0f);
        break;

      case Direction.right:
        generatorPoint.position += new Vector3(xOffset, 0f, 0f);
        break;

      case Direction.down:
        generatorPoint.position += new Vector3(0f, -yOffset, 0f);
        break;

      case Direction.left:
        generatorPoint.position += new Vector3(-xOffset, 0f, 0f);
        break;
    }
  }

  Direction NewDirection() {
    return (Direction)Random.Range(0, 4);
  }
}

[System.Serializable]
public class RoomsPrefabs
{
  public GameObject roomR;
  public GameObject roomU;
  public GameObject roomL;
  public GameObject roomD;
  public GameObject roomRL;
  public GameObject roomUD;
  public GameObject roomUR;
  public GameObject roomRD;
  public GameObject roomDL;
  public GameObject roomUL;
  public GameObject roomURDL;
  public GameObject roomURL;
  public GameObject roomUDL;
  public GameObject roomRDL;
  public GameObject roomURD;
}
