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
    [SerializeField]
    private bool isFirstTime = true;
    private IEnumerator co;

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
            Vector3Int pos = new Vector3Int(-57, 4, 0);
            SetTiles(pos, i);
            status = i;
            Debug.Log("growing " + i);
            yield return new WaitForSeconds(3f);
            i++;
        }
    }

    private void Harvest()
    {
        Debug.Log("Harvesting!!");
        Vector3Int pos = new Vector3Int(-57, 4, 0);
        SetTiles(pos, 0);
        status = 0;
        isFirstTime = true;
        co = UpdatePatchStatus();
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
