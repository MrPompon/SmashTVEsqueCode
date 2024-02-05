using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test_TilemapFade : MonoBehaviour
{
    public Tilemap tilemap;
    public Color defaultColor;
    public Color fadeColor;
    public float checkForFadeRate = 0.3f;
    public float fadeRange = 3f;
    public AreaObjectTracker areaTracker;

    public List<Vector3Int> prevFadePositions = new List<Vector3Int>();
    private TimerEventer updateTimer;

    private void OnValidate()
    {
        if (tilemap == null)
        {
            tilemap = GetComponentInParent<Tilemap>();
        }
        if (areaTracker == null)
        {
            areaTracker = GetComponent<ColliderAreaTracker>();
        }
    }
    private void Awake()
    {
        updateTimer = new TimerEventer(checkForFadeRate);
        updateTimer.OnTimerReached += DoFadeCheck;
        if (tilemap == null)
        {
            tilemap = GetComponentInParent<Tilemap>();
        }
        if (areaTracker == null)
        {
            areaTracker = GetComponent<ColliderAreaTracker>();
        }
    }
    public void Update()
    {
        updateTimer.DoUpdate(Time.deltaTime, true);
    }
    void DoFadeCheck()
    {
        List<Transform> tracked = areaTracker.GetTrackedTransformsRemoveNulls();
        if (tracked.Count > 0)
        {
            UndoFade(prevFadePositions);
            ApplyFade(tracked[0].position, fadeRange);
        }
    }
    void ApplyFade(Vector3 pos, float fadeRange)
    {
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(position))
            {
                continue;
            }
            Tile tile = tilemap.GetTile(position) as Tile;

            if (tile != null)
            {
                Vector2 tileWorldLocation = tilemap.CellToWorld(position);
                if (Vector2.Distance(pos, tileWorldLocation) < fadeRange)
                {
                    tilemap.SetTileFlags(position, TileFlags.None);
                    tilemap.SetColor(position, fadeColor);
                    //tilemap.SetTileFlags(position, TileFlags.LockColor);
                    prevFadePositions.Add(position);
                }
            }
        }
    }
    void UndoFade(List<Vector3Int> prevs)
    {
        foreach (Vector3Int v3 in prevs)
        {
            tilemap.SetTileFlags(v3, TileFlags.None);
            tilemap.SetColor(v3, defaultColor);
            tilemap.SetTileFlags(v3, TileFlags.LockColor);
        }
        prevFadePositions.Clear();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ApplyFade(collision.transform.position, fadeRange);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UndoFade(prevFadePositions);
        }
    }
}
