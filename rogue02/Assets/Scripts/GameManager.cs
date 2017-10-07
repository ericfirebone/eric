using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager _instance;
    public static GameManager instance { get { return instance; } }
    public static int food = 100;
    public int level = 1;
    public Transform mapHolder;
    public int rows = 10;
    public int cols = 10;
    public GameObject[] floorArray;
    public GameObject[] outwallArray;
    public GameObject[] wallArray;
    public GameObject[] foodArray;
    public GameObject[] enemyArray;
    public GameObject player;
    public GameObject exit;
    public List<Vector2> positionList=new List<Vector2>();
    public int minWallCount = 2;
    public int maxWallCount = 8;
   
    	// Use this for initialization
	void Awake () {
        initMap();
        _instance = this;
    }
    void Start() {



    }

    // Update is called once per frame
    void Update () {
		
	}
    void initMap()
    {
        for (int x=0;x<cols;x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (x == 0 || y == 0 || x == cols - 1 || y == rows - 1)
                {
                    int randomWallIndex = Random.Range(0, outwallArray.Length);
                    GameObject go = GameObject.Instantiate(outwallArray[randomWallIndex], new Vector2(x, y), Quaternion.identity);
                    go.transform.SetParent(mapHolder);
                }
                else {

                    int randomWallIndex = Random.Range(0, floorArray.Length);
                    GameObject go = GameObject.Instantiate(floorArray[randomWallIndex], new Vector2(x, y), Quaternion.identity);
                    go.transform.SetParent(mapHolder);
                }
            }

         }
        positionList.Clear();
        for (int x=2;x<8;x++)
        {
            for (int y = 2; y < 8; y++)
            {

                positionList.Add(new Vector2(x, y));
            }

        }
        int wallCount = Random.Range(minWallCount, maxWallCount+1);
        createItems(wallCount,wallArray);
        int foodCount = Random.Range(2,2*level+1);
        createItems(foodCount, foodArray);
        int enemyCount = Random.Range(2, 2 * level + 1);
        createItems(enemyCount, enemyArray);
        Vector2 playerPos = new Vector2(1, 1);
        GameObject po= GameObject.Instantiate(player, playerPos, Quaternion.identity);
        Vector2 exitPos = new Vector2(8, 8);
        GameObject eo = GameObject.Instantiate(exit, exitPos, Quaternion.identity);
    }
    /// <summary>
    /// 创建物品和内墙
    /// </summary>
    /// <param name="count">某种物品个数</param>
    /// <param name="prefabs">使用预设数组</param>
    void createItems(int count, GameObject[] prefabs)
    {

        for (int i=0;i<count;i++)//循环生成count个物体
        {
            int randomTypeIndex = Random.Range(0, prefabs.Length);//随机一种物品
            int randomPosition = Random.Range(0, positionList.Count);//随机获取positionlist中的一个位置的索引
            Vector2 pos = positionList[randomPosition];
            GameObject go = GameObject.Instantiate(prefabs[randomTypeIndex], pos,Quaternion.identity);
            positionList.RemoveAt(randomPosition);
            go.transform.SetParent(mapHolder);

        }

    }
    public void addFood(int count)
    {
        food += count;
    }

    public void reduceFood(int count)
    {
        food -= count;
    }
}
