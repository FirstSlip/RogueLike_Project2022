using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

public class Fireball : MonoBehaviour, ISpell
{
    private GameObject[] minifire = new GameObject[10];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnImpact(RaycastHit2D hitInfo)
    {
        for (int i = 0; i < 10; i++)
        {
            StartCoroutine(AddFireExplode(i, hitInfo));
        }
    }

    private IEnumerator AddFireExplode(int id, RaycastHit2D hit)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        if (hit.point != new Vector2(0, 0))
        {
            minifire[id] = Instantiate(Resources.Load("Prefabs/miniFire") as GameObject, hit.point, Quaternion.Euler(0, 0, 0));
            minifire[id].GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(5f, 20f)));
            yield return new WaitForSeconds(1);
            Destroy(minifire[id]);
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(gameObject);
    }
}
