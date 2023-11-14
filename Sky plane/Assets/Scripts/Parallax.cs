using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;//length and start position of our sprite
    public GameObject cam;//camera
    public float parallaxEffect;//variable that says how much parralaxeffect are we gonna apply

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;//get start position
        length = GetComponent<SpriteRenderer>().bounds.size.x;//get length
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if(temp < startpos - length) startpos -= length;
    }
}
