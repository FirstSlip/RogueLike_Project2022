using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartLocation : MonoBehaviour
{
    private Image black;
    private GameObject player;
    private PlayerController control;
    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Animator>().SetFloat("Vertical", -1);
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        //player.GetComponent<PlayerController>().enabled = false;
        black = GetComponent<Image>();
        black.color = new Color(0, 0, 0, 1);
        //StartCoroutine(DissapearingBlackScreen());
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponentInChildren<WeaponActions>().enabled = false;
        StartCoroutine(CutScene(player.GetComponent<Collider2D>()));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DissapearingBlackScreen()
    {
        for (float i = 1f; i > 0; i-= 0.01f)
        {
            black.color = new Color(0, 0, 0, i);
            yield return new WaitForSeconds(0.01f);
        }
        
        for (float i = 1f; i > 0; i -= 0.01f)
        {
            player.transform.Translate(new Vector3(0, -i / 12));
            yield return new WaitForSeconds(0.03f);
        }
        player.GetComponent<PlayerController>().enabled = true;

    }
    private IEnumerator CutScene(Collider2D collision)
    {
        for (float i = 0; i <= 100; i++)
        {
            black.color = new Color(0, 0, 0, 1 - (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        Vector2 dest = new Vector2(0, -3);
        //if (dest.x > 0)
        //{
        //    collision.GetComponent<Animator>().SetFloat("Horizontal", 1);
        //    collision.transform.localScale = new Vector3(1, 1, 1);
        //}
        //else
        //{
        //    collision.GetComponent<Animator>().SetFloat("Horizontal", -1);
        //    collision.transform.localScale = new Vector3(-1, 1, 1);
        //}
        //for (int i = 0; i < 100; i++)
        //{
        //    collision.transform.Translate(dest / 100);
        //    yield return new WaitForSeconds(0.002f);
        //}
        //collision.GetComponent<Animator>().SetFloat("Horizontal", 0);
        //collision.GetComponent<Collider2D>().isTrigger = true;
        //dest = new Vector2(0, transform.position.y - collision.transform.position.y + 3.8f);
        
        collision.GetComponent<Animator>().SetFloat("Speed", 0.2f);
        for (float i = 0; i <= 100; i++)
        {
            player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (float)i/100);
            player.transform.Translate(dest / 100);
            //if (i >= 50)
            //    collision.GetComponent<SpriteRenderer>().color = new Color(1 - (i - 50) / 50, 1 - (i - 50) / 50, 1 - (i - 50) / 50, 1);
            //if (i >= 90)
            //    collision.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1 - (i - 90) / 10);
            yield return new WaitForSeconds(0.01f);
        }
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponentInChildren<WeaponActions>().enabled = true;

    }
}
