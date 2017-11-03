using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class DragonDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;

    public Transform dragonPrefab;
    public Transform range;

    #region IBeginDragHandler implementation

    private void Start()
    {

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        startParent = transform.parent;
        range.gameObject.SetActive(true);
    }

    #endregion

    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        range.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        range.localScale = dragonPrefab.localScale * dragonPrefab.GetComponent<DragonBehaviour>().range * 2;
    }

    #endregion

    #region IEndDragHandler implementation

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPosition;

        Instantiate(dragonPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        Debug.Log(Input.mousePosition);
        GetComponent<Image>().color = Color.white;

        range.gameObject.SetActive(false);
    }

    #endregion
}