using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

  public int width = 50;
  public int height = 50;

  public string seed;
  public bool useRandomSeed;

  [Range(0,100)]
  public int randomFillPercent;

  public GameObject tile_1;

  private int[,] map;

  private float tileSize;

  private Dictionary<string, float> mapBounds;

	void Start () {
		GenerateMap();
    DrawMap();
	}

  void Update() {
    if (Input.GetMouseButtonDown(0)) {
      GenerateMap();
      DrawMap();
    }
  }
	
	void GenerateMap() {
    map = new int[width,height];
    SetMapBounds();
    RandomFillMap();

    for (int i = 0; i < 5; i ++) {
      SmoothMap();
    }
  }

  void SetMapBounds(){
    tileSize = tile_1.GetComponent<Renderer>().bounds.size.x;
    float xVector = (float)width * tileSize * 0.5f;
    float yVector = (float)height * tileSize * 0.5f;

    mapBounds = new Dictionary<string, float>() {
      {"top", yVector},
      {"bottom", yVector * -1f},
      {"right", xVector},
      {"left", xVector * -1f}
    };
  }

  void RandomFillMap() {
    if (useRandomSeed) {
      seed = Time.time.ToString();
    }

    System.Random pseudoRandom = new System.Random(seed.GetHashCode());

    for (int x = 0; x < width; x ++) {
      for (int y = 0; y < height; y ++) {
        if (x == 0 || x == width-1 || y == 0 || y == height -1) {
          map[x,y] = 1;
        }
        else {
          map[x,y] = (pseudoRandom.Next(0,100) < randomFillPercent)? 1: 0;
        }
      }
    }
  }

  void SmoothMap() {
    for (int x = 0; x < width; x ++) {
      for (int y = 0; y < height; y ++) {
        int neighbourWallTiles = GetSurroundingWallCount(x,y);

        if (neighbourWallTiles > 4)
          map[x,y] = 1;
        else if (neighbourWallTiles < 4)
          map[x,y] = 0;

      }
    }
  }

  int GetSurroundingWallCount(int gridX, int gridY) {
    int wallCount = 0;
    for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
      for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
        if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height) {
          if (neighbourX != gridX || neighbourY != gridY) {
            wallCount += map[neighbourX,neighbourY];
          }
        }
        else {
          wallCount ++;
        }
      }
    }

    return wallCount;
  }

  Vector3 TilePosition(int x, int y) {
    float xPos = mapBounds["left"] + (x * tileSize);
    float yPos = mapBounds["top"] - (y * tileSize);
    return new Vector3(xPos, yPos, 0);
  }

  void DrawMap() {
    ClearMap();
    if (map != null) {
      for (int x = 0; x < width; x ++) {
        for (int y = 0; y < height; y ++) {
          DrawTile(x, y);
        }
      }
    }
  }

  void ClearMap() {
    foreach (Transform child in gameObject.transform) {
       GameObject.Destroy(child.gameObject);
     }
  }

  void DrawTile(int x, int y) {
    TilePosition(x, y);
    if (map[x,y] == 1) {
      Instantiate(tile_1, TilePosition(x,y), Quaternion.identity, gameObject.transform);
    }
  }
}
