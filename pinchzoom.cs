using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class pinchzoom : MonoBehaviour
{
    public float perspectiveZoomSpeed;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed;        // The rate of change of the orthographic size in orthographic mode.
    public GameObject cube,cylinder;
    Vector3 temp1,initialcubetransform,temp2,initialcylindertransform;
    //public Text text1,text2;
    
    private void Start()
    {
        temp1 = cube.transform.localScale;
        initialcubetransform = temp1;
        temp2 = cylinder.transform.localScale;
        initialcylindertransform = temp2;
    }
    public void loadmainmenu()
    {
        
        SceneManager.LoadScene("mainmenu");
    }
    
    void Update()
    {
        //Debug.Log("in" + initialcubetransform); Debug.Log("tmp" + temp1);
        //Debug.Log("orig" + cube.transform.localScale);
        //  If there are two touches on the device...
        //cube.transform.RotateAround(cube.transform.position, Vector3.up, Time.deltaTime * 90f);
        if (Input.touchCount == 1)
        {
            Touch touchZero = Input.GetTouch(0);
            //text1.text = touchZero.deltaPosition.x.ToString();
            //text2.text = touchZero.deltaPosition.y.ToString();
            if((touchZero.deltaPosition.x>0 && touchZero.deltaPosition.y>0)|| (touchZero.deltaPosition.x < 0 && touchZero.deltaPosition.y < 0))
           cube.transform.RotateAround(cube.transform.position,new Vector3(touchZero.deltaPosition.x, -touchZero.deltaPosition.y,0) ,Time.deltaTime*90f);
           else
           cube.transform.RotateAround(cube.transform.position, new Vector3(-touchZero.deltaPosition.x, touchZero.deltaPosition.y, 0), Time.deltaTime * 90f);

        }
        if (Input.touchCount == 1)
        {
            Touch touchZero = Input.GetTouch(0);
            //text1.text = touchZero.deltaPosition.x.ToString();
            //text2.text = touchZero.deltaPosition.y.ToString();
            if ((touchZero.deltaPosition.x > 0 && touchZero.deltaPosition.y > 0) || (touchZero.deltaPosition.x < 0 && touchZero.deltaPosition.y < 0))
                cylinder.transform.RotateAround(cylinder.transform.position, new Vector3(touchZero.deltaPosition.x, -touchZero.deltaPosition.y, 0), Time.deltaTime * 150f);
            else
                cylinder.transform.RotateAround(cylinder.transform.position, new Vector3(-touchZero.deltaPosition.x, touchZero.deltaPosition.y, 0), Time.deltaTime * 150f);

        }

        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;


          

            // If the camera is orthographic...
            if (this.GetComponent<Camera>().orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                this.GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                //this.GetComponent<Camera>().orthographicSize = Mathf.Max(this.GetComponent<Camera>().orthographicSize, 0.1f);
                
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                this.GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                this.GetComponent<Camera>().fieldOfView = Mathf.Clamp(this.GetComponent<Camera>().fieldOfView, 0.1f, 179.9f);

                if (cube.transform.localScale.x <= 1 && cube.transform.localScale.x > 0)
                {

                    temp1.x -= deltaMagnitudeDiff * perspectiveZoomSpeed * Time.deltaTime;
                    temp1.x= Mathf.Clamp(temp1.x, 0.01f, 1f);
                    temp1.y = temp1.x;
                    temp1.z = temp1.x;
                    cube.transform.localScale = temp1;
                }
                if (cylinder.transform.localScale.x <= 1 && cylinder.transform.localScale.x > 0)
                {

                    temp1.x -= deltaMagnitudeDiff * perspectiveZoomSpeed * Time.deltaTime;
                    temp1.x = Mathf.Clamp(temp1.x, 0.01f, 1f);
                    temp1.y = temp1.x;
                    temp1.z = temp1.x;
                    cylinder.transform.localScale = temp1;
                }

            }
        }
    }
}
