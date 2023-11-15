using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public float backgroundSpeed;
    public PlaneControllerV2 backgroundMovementSpeed;
    public Renderer backgroundRenderer;
    // Update is called once per frame
    void Update()
    {
        backgroundRenderer.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0f);
        transform.position += new Vector3(backgroundMovementSpeed.currentSpeed * Time.deltaTime, 0, 0);
    }
}
