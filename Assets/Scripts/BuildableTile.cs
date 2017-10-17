using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableTile : MonoBehaviour {

	public Sprite defaultTileSprite;
	public Sprite buildableTileSprite;
	public Sprite occupiedTileSprite;

	private bool occupied;
	private GameObject building;
	private SpriteRenderer spriteRenderer;

	public void displayBuildMode() {
		if (occupied) {
			spriteRenderer.sprite = occupiedTileSprite;
		} else {
			spriteRenderer.sprite = buildableTileSprite;
		}
	}

	public void hideBuildMode() {
		spriteRenderer.sprite = defaultTileSprite;
	}

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent (typeof(SpriteRenderer)) as SpriteRenderer;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
