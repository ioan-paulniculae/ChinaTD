using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3Int mousePos0;
    private Vector3Int mousePos1;

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;

    public Transform towerPrefab;
    public Tile towerSprite;
    public Tile highlightGreen;
    public Tile highlightRed;

    private Tilemap towerMap;
    private Tilemap highlightMap;
    //tilemap which contains sprites that can't be built on
    private Tilemap buildableTilemap;

    #region IBeginDragHandler implementation

    private void Start()
    {
        highlightMap = GameObject.Find("HighlightMap").GetComponent<Tilemap>();
        towerMap = GameObject.Find("TowerMap").GetComponent<Tilemap>();
        buildableTilemap = GameObject.Find("BuildableMap").GetComponent<Tilemap>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        startParent = transform.parent;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        mousePos0 = towerMap.WorldToCell(new Vector3(ray.origin.x, ray.origin.y, 0));
        mousePos1 = mousePos0;
    }

    #endregion

    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        mousePos1 = towerMap.WorldToCell(new Vector3(ray.origin.x, ray.origin.y, 0));

        if( mousePos1 != mousePos0 )
        {
            highlightMap.SetTile(mousePos0, null);
            mousePos0 = mousePos1;
        }

        if (!buildableTilemap.HasTile(mousePos1) && !towerMap.HasTile(mousePos1))
        {
            highlightMap.SetTile(mousePos1, highlightGreen);
        } else
        {
            highlightMap.SetTile(mousePos1, highlightRed);
        }
    }

    #endregion

    #region IEndDragHandler implementation
    
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPosition;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        mousePos1 = towerMap.WorldToCell(new Vector3(ray.origin.x, ray.origin.y, 0));

        // Verifica daca se poate construi.
        if (!buildableTilemap.HasTile(mousePos1) && !towerMap.HasTile(mousePos1))
        {
            towerMap.SetTile(mousePos1, towerSprite);
            Instantiate(towerPrefab, towerMap.GetCellCenterWorld(mousePos1), Quaternion.identity);
        }
        highlightMap.SetTile(mousePos1, null);
        highlightMap.SetTile(mousePos0, null);
        //towerPrefab.GetComponent<TowerBehaviour>().range;
    }

    #endregion
}