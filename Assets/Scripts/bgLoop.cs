using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgLoop : MonoBehaviour
{
    // List of all the background objects to loop
    public GameObject[] levels;
    private Camera mainCamera;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        // Takes the screen width/hight and plots it on a x and y axis
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        // Goes through all the child objects on a list
        foreach(GameObject obj in levels)
        {
            loadChildObjects(obj);
        }
    }

    // Loads all the child objects
    void loadChildObjects(GameObject obj)
    {
        // Gives the horizontal valua of the boundry box of the sprite
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        int childsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth);
        GameObject clone = Instantiate(obj) as GameObject;
        // Loop all child objects
        for(int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);
            c.name = obj.name + i;
        }
        // Destroy clones no longer needed
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    // Makes sure the bg pattern never ends
    void repositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if(children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x;
            // Detects if the camera is exposing the right edge of the canvas
            if(transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
            }
            // Detects if the camera is exposing the left edge of the canvas
            else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2, firstChild.transform.position.y, firstChild.transform.position.z);
            }
        }
    }

    void LateUpdate()
    {
        foreach(GameObject obj in levels)
        {
            repositionChildObjects(obj);
        }
    }
}
