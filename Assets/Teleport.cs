using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private GameObject player;
    private Transform destination;
    private GameObject minimap;
    private AudioSource music;
    private float musicVolume;
    // Start is called before the first frame update
    private void Start()
    {
        destination = GameObject.FindGameObjectWithTag("Point").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        minimap = GameObject.FindGameObjectWithTag("MiniMap");
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        musicVolume = music.volume;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Waiter());
        }
    }
    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Animator>().SetFloat("Speed", 0);
        Color color;
        for(float i = 0; i < 100; i++)
        {
            color = new Color(player.GetComponent<SpriteRenderer>().color.r, 
                player.GetComponent<SpriteRenderer>().color.g, 
                player.GetComponent<SpriteRenderer>().color.b, 1-i/100);
            player.GetComponent<SpriteRenderer>().color = color;
            music.volume -= musicVolume/100; 
            yield return new WaitForSeconds(0.01f);
        }
        music.Stop();
        music.volume = musicVolume;
        player.transform.position = new Vector2(destination.position.x, destination.position.y + 1);
        minimap.transform.position = new Vector3(destination.position.x, destination.position.y + 7, -10);
        minimap.GetComponent<Camera>().orthographicSize = 20;
        for (float i = 0; i < 100; i++)
        {
            color = new Color(player.GetComponent<SpriteRenderer>().color.r,
                player.GetComponent<SpriteRenderer>().color.g,
                player.GetComponent<SpriteRenderer>().color.b, i/100);
            player.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.01f);
        }
        player.GetComponent<PlayerController>().enabled = true;
    }
}
