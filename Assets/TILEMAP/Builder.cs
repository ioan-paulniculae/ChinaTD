using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class Builder : MonoBehaviour
{
    private Tilemap tilemap;

    //tilemap which contains sprites that can't be built on
    public Tilemap buildableTilemap;

    private Ray ray;
    private Vector3Int mousePos;

    public Tile towerSprite;
    public Transform towerPrefab;

    void Awake()
    {
        tilemap = gameObject.GetComponent<Tilemap>();
        if (tilemap == null)
        {
            Debug.Log(gameObject.name + " does not have a Tilemap component");
        }
        
    }

    void Start()
    {

    }

    void Update()
    {
        if( Input.GetKeyDown("mouse 0"))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            mousePos = tilemap.WorldToCell(new Vector3(ray.origin.x, ray.origin.y, 0));


            if( !buildableTilemap.HasTile(mousePos))
            {
                tilemap.SetTile(mousePos, towerSprite);
                Instantiate(towerPrefab, tilemap.GetCellCenterWorld(mousePos), Quaternion.identity);
            }
        }
    }
}