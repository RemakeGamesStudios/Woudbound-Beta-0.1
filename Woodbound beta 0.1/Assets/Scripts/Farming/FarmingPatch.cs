using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmingPatch : Sign
{
    [Header("TileMap related")]
    public Tilemap tilemap;
    public TileBase[] tileBases;

    [SerializeField]
    private string statusStr = "plough";
    [SerializeField]
    private int status = 0;
    [SerializeField]
    private bool isFirstTime = true;
    private IEnumerator co;

    [Header("Position of patch")]
    public Vector3Int topLeftCorner;
    public Vector3Int bottomRightCorner;

    

    private void Start()
    {
        co = UpdatePatchStatus();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.E) && playerInRange && status != 1)
        {
            if (isFirstTime)
            {
                Debug.Log("Sowing!");
                StartCoroutine(co);
                isFirstTime = false;
            }
            else if(status >= 2)
            {
                StopCoroutine(co);
                Harvest();
            }
        }
    }

    private IEnumerator UpdatePatchStatus()
    {
        int i = status;
        while (i < 4)
        {
            i = status + 1;
            if(i > 3)
            {
                break;
            }
            //play suitable animation for plough patch, sow seeds, tend, or harvest
            SetTiles(i);
            status = i;
            Debug.Log("growing " + i);
            yield return new WaitForSeconds(3f);
            i++;
        }
    }

    private void Harvest()
    {
        Debug.Log("Harvesting!!");
        SetTiles(0);
        status = 0;
        isFirstTime = true;
        co = UpdatePatchStatus();
    }

    private void SetTiles(int i)
    {
        Vector3Int pos = topLeftCorner;
        for (int k = 0; k < bottomRightCorner.x - topLeftCorner.x + 1; k++)
        {
            for (int j = 0; j < topLeftCorner.y - bottomRightCorner.y + 1; j++)
            {
                tilemap.SetTile(pos, tileBases[i]);
                pos.y--;
            }
            pos.y += topLeftCorner.y - bottomRightCorner.y + 1;
            pos.x++;
        }
    }

}
