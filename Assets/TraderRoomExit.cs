using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TraderRoomExit : MonoBehaviour
{
    public Image black;
    private AudioSource music;
    private float musicVolume;
    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        musicVolume = music.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().enabled = false;
            collision.GetComponentInChildren<WeaponActions>().enabled = false;
            StartCoroutine(CutScene(collision));
        }
    }

    private IEnumerator CutScene(Collider2D collision)
    {
        Vector2 dest = new Vector2(transform.position.x - collision.transform.position.x, 0);
        if (dest.x > 0)
        {
            collision.GetComponent<Animator>().SetFloat("Horizontal", 1);
            collision.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            collision.GetComponent<Animator>().SetFloat("Horizontal", -1);
            collision.transform.localScale = new Vector3(-1, 1, 1);
        }
        for (int i = 0; i < 100; i++)
        {
            collision.transform.Translate(dest / 100);
            yield return new WaitForSeconds(0.002f);
        }
        collision.GetComponent<Animator>().SetFloat("Horizontal", 0);
        collision.GetComponent<Collider2D>().isTrigger = true;
        dest = new Vector2(0, transform.position.y - collision.transform.position.y + 3.8f);
        collision.GetComponent<Animator>().SetFloat("Vertical", 1);
        for (float i = 0; i <= 100; i++)
        {
            collision.transform.Translate(dest / 100);
            if (i >= 50)
                collision.GetComponent<SpriteRenderer>().color = new Color(1 - (i - 50) / 50, 1 - (i - 50) / 50, 1 - (i - 50) / 50, 1);
            if (i >= 90)
                collision.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1 - (i - 90) / 10);
            yield return new WaitForSeconds(0.01f);
        }
        for (float i = 0; i <= 100; i++)
        {
            black.color = new Color(0, 0, 0, i / 100);
            music.volume -= musicVolume / 100;
            yield return new WaitForSeconds(0.01f);
        }
        music.Stop();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().SavePlayerState();
        SceneManager.LoadScene(4);
    }
}
