using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAndCloseArea : MonoBehaviour
{
    public GameObject trigger;
    public List<GameObject> border;
    public List<GameObject> enemy;
    public RoomGenerator generator;
    public Tilemap walls;
    public int countOfEnemies;
    private bool isActive = false;
    private bool isEmpty = false;
    private List<Vector3> enemyesPos;
    // Start is called before the first frame update
    private void Start()
    {
        isActive = false;
        isEmpty = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !isActive)
        {
            StartCoroutine(Waiter());
            trigger.GetComponent<Collider2D>().enabled = false;
        }
    }
    private void Update()
    {
        if (isActive)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                foreach (GameObject borders in border)
                    Destroy(borders);
                isEmpty = true;
            }
        }
        if (isEmpty)
            Destroy(trigger);
    }
    private void SideBlock()
    {
        for (float j = (trigger.transform.position.y - trigger.GetComponent<BoxCollider2D>().size.y / 2) - 1;
                j < (trigger.transform.position.y + trigger.GetComponent<BoxCollider2D>().size.y / 2) + 1; j++)
        {
            int x1 = (int)(trigger.transform.position.x - trigger.GetComponent<BoxCollider2D>().size.x / 2) - 1;
            if (walls.GetSprite(new Vector3Int(x1, (int)j, 0)) == null)
                border.Add(Instantiate(Resources.Load("Prefabs/BorderSide") as GameObject, new Vector3Int(x1, (int)j, 0), Quaternion.Euler(0, 0, 0)));
            int x2 = (int)(trigger.transform.position.x + trigger.GetComponent<BoxCollider2D>().size.x / 2);
            if (walls.GetSprite(new Vector3Int(x2, (int)j, 0)) == null)
                border.Add(Instantiate(Resources.Load("Prefabs/BorderSide") as GameObject, new Vector3Int(x2 + 1, (int)j, 0), Quaternion.Euler(0, 0, 0)));
        }
    }
    private void TopBottomBlock()
    {
        for (float j = (trigger.transform.position.x - trigger.GetComponent<BoxCollider2D>().size.x / 2) - 1;
                j < (trigger.transform.position.x + trigger.GetComponent<BoxCollider2D>().size.x / 2) + 1; j++)
        {
            int y1 = (int)(trigger.transform.position.y - trigger.GetComponent<BoxCollider2D>().size.y / 2) - 1;
            if (walls.GetSprite(new Vector3Int((int)j, y1, 0)) == null)
                border.Add(Instantiate(Resources.Load("Prefabs/BorderTopBottom") as GameObject, new Vector3Int((int)j, y1 - 1, 0), Quaternion.Euler(0, 0, 0)));
            int y2 = (int)(trigger.transform.position.y + trigger.GetComponent<BoxCollider2D>().size.y / 2);
            if (walls.GetSprite(new Vector3Int((int)j, y2, 0)) == null)
                border.Add(Instantiate(Resources.Load("Prefabs/BorderTopBottom") as GameObject, new Vector3Int((int)j, y2 + 1, 0), Quaternion.Euler(0, 0, 0)));
        }
    }

    private void CreateEnemies(int countOfEnemyes)
    {
        enemyesPos = new List<Vector3>();
        enemy = new List<GameObject>();
        for (int i = 0; i < countOfEnemyes; i++)
        {
            float x = Random.Range((trigger.transform.position.x - trigger.GetComponent<BoxCollider2D>().size.x / 2) + 1,
                trigger.transform.position.x + trigger.GetComponent<BoxCollider2D>().size.x / 2 - 1);
            float y = Random.Range((trigger.transform.position.y - trigger.GetComponent<BoxCollider2D>().size.y / 2) + 1,
                trigger.transform.position.y + trigger.GetComponent<BoxCollider2D>().size.y / 2 - 1);
            if(!enemyesPos.Contains(new Vector3Int((int)x, (int)y, 0)))
            {
                enemy.Add(Instantiate(Resources.Load("Prefabs/Spawn") as GameObject, new Vector3Int((int)x, (int)y, 0), Quaternion.Euler(0, 0, 0)));
                enemyesPos.Add(enemy[i].transform.position);
                enemy[i].transform.SetParent(trigger.transform);
            }
            else
            {
                while(!enemyesPos.Contains(new Vector3Int((int)x, (int)y, 0)))
                {
                    x = Random.Range((trigger.transform.position.x - trigger.GetComponent<BoxCollider2D>().size.x / 2) + 1,
                        trigger.transform.position.x + trigger.GetComponent<BoxCollider2D>().size.x / 2 - 1);
                    y = Random.Range((trigger.transform.position.y - trigger.GetComponent<BoxCollider2D>().size.y / 2) + 1,
                        trigger.transform.position.y + trigger.GetComponent<BoxCollider2D>().size.y / 2 - 1);
                }
                enemy.Add(Instantiate(Resources.Load("Prefabs/Spawn") as GameObject, new Vector3Int((int)x, (int)y, 0), Quaternion.Euler(0, 0, 0)));
                enemyesPos.Add(enemy[i].transform.position);
                enemy[i].transform.SetParent(trigger.transform);
            }
        }
    }
    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(0.2f);
        SideBlock();
        TopBottomBlock();
        CreateEnemies(countOfEnemies);
        yield return new WaitForSeconds(3);
        isActive = true;
    }
}

