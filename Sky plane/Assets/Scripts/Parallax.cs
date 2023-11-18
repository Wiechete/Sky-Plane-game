using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Vector2 length, startpos;//length and start position of our sprite
    public GameObject cam;//camera
    public float parallaxEffect;//variable that says how much parralaxeffect are we gonna apply
    public float parallaxVertical;

    public bool horizontal = true;
    public bool vertical = false;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;//get start position
        if(length == Vector2.zero) length = GetComponent<SpriteRenderer>().bounds.size;//get length
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(horizontal){
            float temp = (cam.transform.position.x * (1 - parallaxEffect));
            float dist = (cam.transform.position.x * parallaxEffect);

            transform.position = new Vector3(startpos.x + dist, transform.position.y, transform.position.z);

            if (temp > startpos.x + length.x) startpos.x += length.x;
            else if (temp < startpos.x - length.x) startpos.x -= length.x;
        }

        if (vertical)
        {
            float temp = (cam.transform.position.y * (1 - parallaxEffect));
            float dist = (cam.transform.position.y * parallaxEffect);

            transform.position = new Vector3(transform.position.x, startpos.y + dist, transform.position.z);

            //if (temp > startpos.y + length.y) startpos.y += length.y;
            //else if (temp < startpos.y - length.y) startpos.y -= length.y;
        }
    }
}
