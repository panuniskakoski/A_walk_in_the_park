using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girl : MonoBehaviour
{
    public Sprite[] girls;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Sprite girl in girls)
        {
            loadChildObjects(girl);
        }

        // Get a random array number based on girls array size
        int randomInt = Random.Range(0, girls.Length - 1);
        // Select random girl sprite from the sprite array
        this.GetComponent<SpriteRenderer>().sprite = girls[randomInt];  
    }

    void loadChildObjects(Sprite obj)
    {
        Debug.Log(obj.name);
    }
}
