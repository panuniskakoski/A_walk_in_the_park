using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girl : MonoBehaviour
{
    public GameObject[] girls;
    private GameObject randomGirl;

    // Start is called before the first frame update
    void Start()
    {
        // Get a random array number based on girls array size
        int randomInt = Random.Range(0, girls.Length - 1);
        int randomFlip = Random.Range(0, 2);
        // Select random girl sprite from the sprite array
        randomGirl = girls[randomInt];
        // Spawn a copy of random girl(s) from the array
        Instantiate(randomGirl, this.transform);
        if (randomFlip == 0) this.transform.Rotate(0f, 180f, 0f);
    }
}
