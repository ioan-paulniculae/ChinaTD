using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class DragonDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float cooldown = 15;
    public float duration = 10;
    private float cooldownTimer;

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;

    public Transform dragonPrefab;
    public Transform range;
    public Image fillImage;

    #region IBeginDragHandler implementation

    private void Start()
    {
        dragonPrefab.GetComponent<DragonBehaviour>().maxDuration = duration;
        cooldownTimer = 0;
    }

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        fillImage.fillAmount = cooldownTimer / cooldown;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (cooldownTimer > 0)
            return;

        startPosition = transform.position;
        startParent = transform.parent;
        range.gameObject.SetActive(true);
    }

    #endregion

    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
    {
        if (cooldownTimer > 0)
            return;

        transform.position = eventData.position;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        range.position = new Vector3(ray.origin.x, ray.origin.y, 0);
        range.localScale = dragonPrefab.localScale * dragonPrefab.GetComponent<DragonBehaviour>().range * 2;
    }

    #endregion

    #region IEndDragHandler implementation

    public void OnEndDrag(PointerEventData eventData)
    {
        if (cooldownTimer > 0)
            return;

        transform.position = startPosition;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Instantiate(dragonPrefab, new Vector3(ray.origin.x, ray.origin.y, 0), Quaternion.identity);

        GetComponent<Image>().color = Color.white;

        range.gameObject.SetActive(false);

        cooldownTimer = cooldown;
    }

    #endregion
}