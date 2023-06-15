using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRenderer : MonoBehaviour
{
    public void Awake()
    {
        int[,] pathOffsets = new int[,] { { -22, 0, 22, 0 }, { 0, 30, 30, 0 }, { 0, 22, 0, -22 }, { 30, 0, 0, -30 }, { 22, 0, -22, 0 }, { 0, -30, -30, 0 }, { 0, -22, 0, 22 }, { -30, 0, 0, 30 } };
        Mesh mesh = new Mesh();
        int Aposy = PlayerPrefs.GetInt("Aposy");
        int Aposx = PlayerPrefs.GetInt("Aposx");
        int Bposy = PlayerPrefs.GetInt("Bposy");
        int Bposx = PlayerPrefs.GetInt("Bposx");

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(Aposx + pathOffsets[PlayerPrefs.GetInt("APathDir") - 1, 2], Aposy + pathOffsets[PlayerPrefs.GetInt("APathDir") - 1, 3]); //bottom left
        vertices[1] = new Vector3(Aposx + pathOffsets[PlayerPrefs.GetInt("APathDir") - 1, 0], Aposy + pathOffsets[PlayerPrefs.GetInt("APathDir") - 1, 1]); //top left
        vertices[2] = new Vector3(Bposx + pathOffsets[PlayerPrefs.GetInt("BPathDir") - 1, 2], Bposy + pathOffsets[PlayerPrefs.GetInt("BPathDir") - 1, 3]); //top right
        vertices[3] = new Vector3(Bposx + pathOffsets[PlayerPrefs.GetInt("BPathDir") - 1, 0], Bposy + pathOffsets[PlayerPrefs.GetInt("BPathDir") - 1, 1]); //bottom right

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(1, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
