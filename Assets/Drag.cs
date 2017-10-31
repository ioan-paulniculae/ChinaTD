using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3Int mousePos;
    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;

    public Transform towerPrefab;
    public Tile towerSprite;
    public Tilemap towerMap;
    //tilemap which contains sprites that can't be built on
    public Tilemap buildableTilemap;

    private int x = 0;
    #region IBeginDragHandler implementation

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        startParent = transform.parent;
        Debug.Log(startPosition);
    }

    #endregion

    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    #endregion

    #region IEndDragHandler implementation

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPosition;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        mousePos = towerMap.WorldToCell(new Vector3(ray.origin.x, ray.origin.y, 0));

        // Verifica daca se poate construi.
        if (!buildableTilemap.HasTile(mousePos) && !towerMap.HasTile(mousePos))
        {
            towerMap.SetTile(mousePos, towerSprite);
            Instantiate(towerPrefab, towerMap.GetCellCenterWorld(mousePos), Quaternion.identity);
        }
        
        Debug.Log(startPosition);
    }

    #endregion
}