using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmingPatch : Sign
{
    public Tilemap tilemap;
    public TileBase[] tileBases;
    [SerializeField]
    private string statusStr = "plough";
    [SerializeField]
    private int status = 0;
    private bool isFirstTime = true, isHarvesting = false;

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.E) && playerInRange && status != 1 && !isHarvesting)
        {
            if (isFirstTime)
            {
                StartCoroutine(UpdatePatchStatus());
                isFirstTime = false;
            }
            else if(status >= 2)
            {
                Harvest();
            }
        }
    }

    private IEnumerator UpdatePatchStatus()
    {
        int i = status;
        while (i < 4 && !isHarvesting)
        {
            i = status + 1;
            //play suitable animation for plough patch, sow seeds, tend, or harvest
            Vector3Int pos = new Vector3Int(-57, 4, 0);
            SetTiles(pos, i);
            status = i;
            if (isHarvesting)
            {
                yield return null;
                break;
            }
            yield return new WaitForSeconds(3f);
            i++;
        }
    }

    private void Harvest()
    {
        isHarvesting = true;
        Debug.Log("Harvesting!!");
        Vector3Int pos = new Vector3Int(-57, 4, 0);
        SetTiles(pos, 0);
        status = 0;
        isFirstTime = true;
        isHarvesting = false;
    }

    private void SetTiles(Vector3Int pos, int i)
    {
        for (int j = 0; j < 3; j++)
        {
            tilemap.SetTile(pos, tileBases[i]);
            pos.y++;
        }
    }

}
