using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float CameraMoveLimit = 10f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var camera = Camera.main;

        var height = camera.pixelHeight;
        var width = camera.pixelWidth;

        // mousePosition = �ƹ��b�e���W����m�CEX 1920*1080���۾��j�p���ܡAmousePosition�u�|�^�� (0,0)~(1920,1080);
        var mousePos = Input.mousePosition;


        var Xinternal = (mousePos.x / width) - 0.5f;
        var Yinternal = mousePos.y / height - 0.5f;

        var pos = new Vector2(Xinternal * CameraMoveLimit, Yinternal * CameraMoveLimit);
        camera.transform.position = pos;
    }
}