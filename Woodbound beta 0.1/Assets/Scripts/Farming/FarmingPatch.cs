using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmingPatch : Sign
{
    public Tilemap tilemap;
    public TileBase tileBase;
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            //play suitable animation for plough patch, sow seeds, tend, or harvest
            Vector3Int pos = new Vector3Int(-57, 4, 0);
            for (int i = 0; i < 3; i++)
            {
                tilemap.SetTile(pos, tileBase);
                pos.y++;
            }
            //-57, 4 to 6
        }
    }

}
