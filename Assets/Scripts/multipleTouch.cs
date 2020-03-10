using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multipleTouch : MonoBehaviour
{
    public GameObject circle;
    public List<touchLocation> touches = new List<touchLocation>();

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while (i < Input.touchCount) {
            Touch t = Input.GetTouch(i);
            if (t.phase == TouchPhase.Began){
                Debug.Log("TOUCH: began");
                touches.Add(new touchLocation(t.fingerId, createCircle(t)));
            } else if (t.phase == TouchPhase.Ended){
                Debug.Log("TOUCH: ended");
                touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
                Destroy(thisTouch.circle);
                touches.RemoveAt(touches.IndexOf(thisTouch));
            }
            ++i;
        }
    }

    Vector2 getTouchPosition(Vector2 touchPosition){
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }

    GameObject createCircle(Touch t){
        GameObject c = Instantiate(circle) as GameObject;
        c.name = "Touch" + t.fingerId;
        c.transform.position = getTouchPosition(t.position);
        return c;
    }
}
