﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //private Vector3Int mousePos0;
    private Vector3Int mousePos;

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;

	public GameplayManager gameplayManager;
	public GameObject infoTextObject;
    public Transform towerPrefab;
    public Transform range;

    public Tile towerSprite;
    public Tile highlightGreen;
    public Tile highlightRed;

    private Tilemap towerMap;
    //private Tilemap highlightMap;
    //tilemap which contains sprites that can't be built on
    private Tilemap buildableTilemap;
	private Button button;

    #region IBeginDragHandler implementation

    private void Start()
    {
        //highlightMap = GameObject.Find("HighlightMap").GetComponent<Tilemap>();
        towerMap = GameObject.Find("TowerMap").GetComponent<Tilemap>();
        buildableTilemap = GameObject.Find("BuildableMap").GetComponent<Tilemap>();
		button = GetComponent<Button> ();
    }

	private void Update() {
		if (gameplayManager.CanAffordTower (towerPrefab.GetComponent<TowerBehaviour> ())) {
			button.interactable = true;
		} else {
			button.interactable = false;
		}

		Text costText = infoTextObject.transform.Find ("Cost").GetComponent<Text>();
		costText.text = "Cost: " + gameplayManager.GetSessionCurrencyCostForTower (towerPrefab.GetComponent<TowerBehaviour> ())
			+ " " + SessionCurrencyManager.sessionCurrencyName;
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
		// Disallow dragging if the player can't afford the tower.
		if (!gameplayManager.CanAffordTower (towerPrefab.GetComponent<TowerBehaviour> ())) {
			return;
		}

        startPosition = transform.position;
        startParent = transform.parent;
        range.gameObject.SetActive(true);
        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        mousePos0 = towerMap.WorldToCell(new Vector3(ray.origin.x, ray.origin.y, 0));
        mousePos1 = mousePos0;*/
    }

    #endregion

    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
	{
		// Disallow dragging if the player can't afford the tower.
		if (!gameplayManager.CanAffordTower (towerPrefab.GetComponent<TowerBehaviour> ())) {
			return;
		}

        transform.position = eventData.position;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        mousePos = towerMap.WorldToCell(new Vector3(ray.origin.x, ray.origin.y, 0));

        range.position = towerMap.GetCellCenterWorld(mousePos);
        range.localScale = towerPrefab.localScale*towerPrefab.GetComponent<TowerBehaviour>().range*2;

        if (!buildableTilemap.HasTile(mousePos) && !towerMap.HasTile(mousePos))
        {
            GetComponent<Image>().color = Color.green;
        }
        else
        {
            GetComponent<Image>().color = Color.red;
        }
        /*
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
        */

    }

    #endregion

    #region IEndDragHandler implementation
    
    public void OnEndDrag(PointerEventData eventData)
    {
		// Disallow dragging if the player can't afford the tower.
		if (!gameplayManager.CanAffordTower (towerPrefab.GetComponent<TowerBehaviour> ())) {
			return;
		}

        transform.position = startPosition;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        mousePos = towerMap.WorldToCell(new Vector3(ray.origin.x, ray.origin.y, 0));

        // Verifica daca se poate construi.
        if (!buildableTilemap.HasTile(mousePos) && !towerMap.HasTile(mousePos))
        {

            towerMap.SetTile(mousePos, towerSprite);
            Transform newTower = Instantiate(towerPrefab, towerMap.GetCellCenterWorld(mousePos), Quaternion.identity);
            GetComponent<Image>().color = Color.white;
			gameplayManager.TowerBuilt (newTower.GetComponent<TowerBehaviour>());
        }
        range.gameObject.SetActive(false);

        /*towerPrefab.GetComponent<SpriteRenderer>().color = Color.red;
        highlightMap.SetTile(mousePos1, null);
        highlightMap.SetTile(mousePos0, null);*/
        //towerPrefab.GetComponent<TowerBehaviour>().range;
    }

    #endregion
}