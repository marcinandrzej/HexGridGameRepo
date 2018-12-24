using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
  
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public GameObject[] CreateGrid(int gridSize, float hexSize, Material material)
    {
        GameObject[] hexGrid = new GameObject[gridSize * gridSize];
        for (int z = 0; z < gridSize; z++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                float offsetx = z % 2 * hexSize;
                hexGrid[x + (gridSize * z)] = CreateHex(hexSize, new Vector3( 0, 0, 0), material);
                hexGrid[x + (gridSize * z)].transform.Translate(new Vector3(((float)x * 2 * hexSize) + offsetx, 0, (z * (-Mathf.Sqrt(3) * 0.75f * hexSize))));
            }
        }
        return hexGrid;
    }

    private GameObject CreateHex(float size, Vector3 position, Material material)
    {
        float w = 2 * size;
        float h = Mathf.Sqrt(3) * size;

        GameObject hex = new GameObject("Hex");
        MeshFilter meshFilter = hex.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();

        // set vertices
        Vector3[] vertices = new Vector3[14];

        vertices[0] = new Vector3(position.x, position.y, position.z);
        vertices[1] = new Vector3(position.x, position.y, (position.z - (0.5f * h)));
        vertices[2] = new Vector3((position.x + (0.5f * w)), position.y, (position.z - (0.25f * h)));
        vertices[3] = new Vector3((position.x + (0.5f * w)), position.y, (position.z + (0.25f * h)));
        vertices[4] = new Vector3(position.x, position.y, (position.z + (0.5f * h)));
        vertices[5] = new Vector3((position.x - (0.5f * w)), position.y, (position.z + (0.25f * h)));
        vertices[6] = new Vector3((position.x - (0.5f * w)), position.y, (position.z - (0.25f * h)));

        vertices[7] = new Vector3(position.x, (position.y - (size * 0.5f)), position.z);
        vertices[8] = new Vector3(position.x, (position.y - (size * 0.5f)), (position.z - (0.5f * h)));
        vertices[9] = new Vector3((position.x + (0.5f * w)), (position.y - (size * 0.5f)), (position.z - (0.25f * h)));
        vertices[10] = new Vector3((position.x + (0.5f * w)), (position.y - (size * 0.5f)), (position.z + (0.25f * h)));
        vertices[11] = new Vector3(position.x, (position.y - (size * 0.5f)), (position.z + (0.5f * h)));
        vertices[12] = new Vector3((position.x - (0.5f * w)), (position.y - (size * 0.5f)), (position.z + (0.25f * h)));
        vertices[13] = new Vector3((position.x - (0.5f * w)), (position.y - (size * 0.5f)), (position.z - (0.25f * h)));

        mesh.vertices = vertices;

        // set normals
        Vector3[] normals = new Vector3[14];

        normals[0] = Vector3.up;
        normals[1] = Vector3.up;
        normals[2] = Vector3.up;
        normals[3] = Vector3.up;
        normals[4] = Vector3.up;
        normals[5] = Vector3.up;
        normals[6] = Vector3.up;

        normals[7] = Vector3.down;
        normals[8] = Vector3.down;
        normals[9] = Vector3.down;
        normals[10] = Vector3.down;
        normals[11] = Vector3.down;
        normals[12] = Vector3.down;
        normals[13] = Vector3.down;

        mesh.normals = normals;

        // set triangles
        int[] triangles = new int[72];

        //Upper face
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        
        triangles[3] = 0;
        triangles[4] = 3;
        triangles[5] = 2;

        triangles[6] = 0;
        triangles[7] = 4;
        triangles[8] = 3;

        triangles[9] = 0;
        triangles[10] = 5;
        triangles[11] = 4;

        triangles[12] = 0;
        triangles[13] = 6;
        triangles[14] = 5;

        triangles[15] = 0;
        triangles[16] = 1;
        triangles[17] = 6;

        //Down face
        triangles[18] = 7;
        triangles[19] = 8;
        triangles[20] = 9;

        triangles[21] = 7;
        triangles[22] = 9;
        triangles[23] = 10;

        triangles[24] = 7;
        triangles[25] = 10;
        triangles[26] = 11;

        triangles[27] = 7;
        triangles[28] = 11;
        triangles[29] = 12;

        triangles[30] = 7;
        triangles[31] = 12;
        triangles[32] = 13;

        triangles[33] = 7;
        triangles[34] = 13;
        triangles[35] = 8;

        //side faces
        triangles[36] = 1;
        triangles[37] = 9;
        triangles[38] = 8;

        triangles[39] = 1;
        triangles[40] = 2;
        triangles[41] = 9;

        triangles[42] = 2;
        triangles[43] = 10;
        triangles[44] = 9;

        triangles[45] = 2;
        triangles[46] = 3;
        triangles[47] = 10;

        triangles[48] = 3;
        triangles[49] = 11;
        triangles[50] = 10;

        triangles[51] = 3;
        triangles[52] = 4;
        triangles[53] = 11;

        triangles[54] = 4;
        triangles[55] = 12;
        triangles[56] = 11;

        triangles[57] = 4;
        triangles[58] = 5;
        triangles[59] = 12;

        triangles[60] = 5;
        triangles[61] = 13;
        triangles[62] = 12;

        triangles[63] = 5;
        triangles[64] = 6;
        triangles[65] = 13;

        triangles[66] = 6;
        triangles[67] = 8;
        triangles[68] = 13;

        triangles[69] = 6;
        triangles[70] = 1;
        triangles[71] = 8;

        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
        hex.AddComponent<MeshRenderer>().material = material;
        hex.AddComponent<MeshCollider>();
        hex.AddComponent<HexActionScript>();
        hex.transform.SetParent(transform);

        return hex;    
    }
}
