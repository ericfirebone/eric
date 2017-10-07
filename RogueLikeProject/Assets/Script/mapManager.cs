using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapManager : MonoBehaviour {

    public GameObject[] outwallArray;
    public GameObject[] floorArray;
    public GameObject[] wallArray;
    public GameObject[] foodArray;
    public GameObject[] enemyArray;
    public GameObject exitPrefab;

    public int cols = 10;
    public int rows = 10;

    private Transform mapHolder;
    private List<Vector2> positionList = new List<Vector2>();

    public int minCountWall = 2;
    public int maxCountWall = 8;

    public GameManager gameManager;
    // Use this for initialization
    void Awake () {
        gameManager = this.GetComponent<GameManager>();
        InitMap();

    }
     void Start()
    {
        
    }
    // Update is called once per frame
    void Update () {
		
	}

    /// <summary>
    /// 初始化地图
    /// </summary>
    private void InitMap() {
        mapHolder = new GameObject("Map").transform;
        for (int x=0;x<cols;x++)
        {
            for (int y=0;y<rows;y++)
            {
                if (x == 0 || y == 0 || x == rows - 1 || y == cols - 1)
                {
                    int index = Random.Range(0, outwallArray.Length);
                   GameObject go=  GameObject.Instantiate(outwallArray[index], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapHolder);
                }
                else
                {

                    int index = Random.Range(0, floorArray.Length);
                    GameObject go = GameObject.Instantiate(floorArray[index], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapHolder);
                }

            }

        }
        //创建障碍物
        positionList.Clear();
        for (int x=2;x<cols-2;x++)
        {
            for (int y=2;y<rows-2;y++)
            {

                positionList.Add(new Vector2(x, y));
            }

        }

        int wallCount = Random.Range(minCountWall, maxCountWall);//障碍物的个数
        for (int i=0;i<wallCount;i++)
        {
            
            Vector2 pos = randomPosition();
            //取得一个随机的障碍物
            // int wallIndex = Random.Range(0, wallArray.Length);
            // GameObject go= GameObject.Instantiate(wallArray[wallIndex],pos,Quaternion.identity) as GameObject;
            GameObject wallPrefab = RandomPrefab(wallArray);
            GameObject go = GameObject.Instantiate(wallPrefab, pos, Quaternion.identity) as GameObject;
            go.transform.SetParent(mapHolder);
        }

        //食物数量
        int foodCount = Random.Range(2,gameManager.level*2+1);

        for (int i=0;i<foodCount;i++)
        {
            Vector2 pos = randomPosition();
            GameObject foodPrefab = RandomPrefab(foodArray);
            GameObject go = GameObject.Instantiate(foodPrefab, pos, Quaternion.identity) as GameObject;
            go.transform.SetParent(mapHolder);
        }

        //敌人生成
        int enemyCount = Random.Range(2, gameManager.level * 2 + 1);
        for (int i=0;i<enemyCount;i++)
        {
            Vector2 pos = randomPosition();
            GameObject enemyPrefab = RandomPrefab(enemyArray);
            GameObject go = GameObject.Instantiate(enemyPrefab, pos, Quaternion.identity) as GameObject;
            go.transform.SetParent(mapHolder);
        }

        //创建出口
        int exitx = cols - 2;
        int exity = rows - 2;
        GameObject exitGo= Instantiate(exitPrefab,new Vector3(exitx, exity, 0),Quaternion.identity) as GameObject;
        exitGo.transform.SetParent(mapHolder);
    }

    private Vector2 randomPosition()
    {
        int positionIndex = Random.Range(0, positionList.Count);
        Vector2 pos = positionList[positionIndex];
        positionList.RemoveAt(positionIndex);
        return pos;
    }

    private GameObject RandomPrefab(GameObject[] prefabs) {
        int index = Random.Range(0, prefabs.Length);
        return prefabs[index];

    }

    /// <summary>
    /// 通过方法创建元素
    /// </summary>
    /// <param name="count">创建个数</param>
    /// <param name="prefabs">所用预设数组</param>
    private void createItems(int count,GameObject[] prefabs)
    {
        /*   for (int i=0;i<count;i++)
           {
               Vector2 pos = randomPosition();//从positionList中取一个位置
               GameObject prefab = RandomPrefab(prefabs);//获取预设数组中的随机一种
               GameObject go = GameObject.Instantiate(prefab, pos, Quaternion.identity) as GameObject;//生成gameObject实例
               go.transform.SetParent(mapHolder);//绑定父物体
           }
        */

     
    }
}
