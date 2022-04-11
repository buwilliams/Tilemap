using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public Tile tile;

    public float moveDelay = 0.1f;
    public Tilemap player { get; private set; }
    public Vector3Int position { get; private set; }

    private float moveTime;

    public void Initialize()
    {
        moveTime = Time.time + moveDelay;
    }

    // Start is called before the first frame update
    void Start()
    {
        Tilemap[] tiles = this.GetComponentsInChildren<Tilemap>();
        player = (tiles[0].gameObject.name == "Player") ? tiles[0] : tiles[1];
        player = tiles[0];
        position = new Vector3Int(0, 0, 0);
        player.SetTile(position, tile);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > moveTime)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        Vector3Int previousPosition = position;

        player.SetTile(position, null);

        if (Input.GetKey(KeyCode.A))
            position = position + Vector3Int.left;

        if (Input.GetKey(KeyCode.S))
            position = position + Vector3Int.down;

        if (Input.GetKey(KeyCode.D))
            position = position + Vector3Int.right;

        if (Input.GetKey(KeyCode.W))
            position = position + Vector3Int.up;

        if (OutOfBounds(position))
            position = previousPosition;

        player.SetTile(position, tile);

        moveTime = Time.time + moveDelay;
    }

    private bool OutOfBounds(Vector3Int position)
    {
        if (position.y > 9 || position.y < -10)
            return true;

        if (position.x > 4 || position.x < -5)
            return true;

        return false;
    }
}
