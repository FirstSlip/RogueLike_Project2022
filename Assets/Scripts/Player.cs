using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public Rigidbody2D player;
    private Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Rigidbody2D>();
        Camera.main.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        velocity = move.normalized * speed;
        Camera.main.transform.position = CameraMove();
    }

    void FixedUpdate()
    {
        player.MovePosition(player.position + velocity * Time.fixedDeltaTime);
    }
    private Vector3 CameraMove() => new Vector3(player.position.x, player.position.y, -10);
}
