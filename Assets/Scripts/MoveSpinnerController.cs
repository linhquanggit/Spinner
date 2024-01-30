using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spinner
{
    public class MoveSpinnerController : MonoBehaviour
    {
        private float distance;
        private bool movingParent;
        void OnMouseDrag()
        {
            movingParent = true;
            //get mouse position and convert to world point
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,distance);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // get bound screen value
            float screenAspect = (float)Screen.width / (float)Screen.height;
            float cameraHeight = Camera.main.orthographicSize * 2;
            Bounds bounds = new Bounds(
                Camera.main.transform.position,
                new Vector3(cameraHeight * screenAspect, cameraHeight, 0));

            //Limit the position of the GameObject so that it does not extend beyond the screen
            objPosition.x = Mathf.Clamp(objPosition.x, bounds.min.x, bounds.max.x);
            objPosition.y = Mathf.Clamp(objPosition.y, bounds.min.y, bounds.max.y);

            transform.position = objPosition;
        }

        private void OnMouseDown()
        {
            movingParent = true;
            //get mouse button down (z) value and set to distance
            distance = Camera.main.WorldToScreenPoint(transform.position).z;
        }
        private void OnMouseUp()
        {
            movingParent = false;
        }
        public bool IsMovingParent()
        {
            return movingParent;
        }

    }
}
