using UnityEngine;
using UnityEngine.Tilemaps;

public class Droplet : MonoBehaviour
{
    [SerializeField] private Tile _waterTile;
    public Tilemap tilemap;

    private Collider2D _collider;
    
    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!_collider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        var pos = transform.position;

        var colPosition = tilemap.WorldToCell(new Vector3(pos.x, pos.y - .2f));
        tilemap.SetTile(colPosition, _waterTile);
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        var map = col.gameObject.GetComponent<Tilemap>();
        if (map is null) return;

        var pos = transform.position;

        var colPosition = tilemap.WorldToCell(new Vector3(pos.x, pos.y - .2f));
        tilemap.SetTile(colPosition, _waterTile);
        Destroy(gameObject);
    }
}
