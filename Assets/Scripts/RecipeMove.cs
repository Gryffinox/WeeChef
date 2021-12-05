using UnityEngine;
using System.Collections;

public class RecipeMove : MonoBehaviour
{
    // The plane the object is currently being dragged on
    private Plane dragPlane;

    // The difference between where the mouse is on the drag plane and 
    // where the origin of the object is on the drag plane
    private Vector3 offset;

    private Camera myMainCamera;

    [SerializeField] GameObject hand;
    private bool shuffle = false;
    private bool canMove = false;

    void Start()
    {
        myMainCamera = Camera.main; // Camera.main is expensive ; cache it here
    }

    private void Update()
    {
        if (shuffle == true)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, hand.transform.position, Time.deltaTime * 20);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "hand")
        {
            shuffle = false;
        }
    }
    void OnMouseDown()
    {
        dragPlane = new Plane(myMainCamera.transform.forward, transform.position);
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        offset = transform.position - camRay.GetPoint(planeDist);
    }

    void OnMouseDrag()
    {
        if (canMove == true)
        {
            Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);

            float planeDist;
            dragPlane.Raycast(camRay, out planeDist);
            transform.position = camRay.GetPoint(planeDist) + offset;
        }
    }

    public void goShuffle()
    {
        shuffle = true;
    }

    public void noShuffle()
    {
        shuffle = false;
    }

    public void yesMove()
    {
        canMove = true;
    }

    public void noMove()
    {
        canMove = false;
    }
}
