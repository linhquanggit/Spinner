using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spinner
{
    public class MoveSpinnerController : MonoBehaviour
    {
        private float distance;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnMouseDrag()
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Lấy ranh giới của màn hình
            float screenAspect = (float)Screen.width / (float)Screen.height;
            float cameraHeight = Camera.main.orthographicSize * 2;
            Bounds bounds = new Bounds(
                Camera.main.transform.position,
                new Vector3(cameraHeight * screenAspect, cameraHeight, 0));

            // Giới hạn vị trí của GameObject để không vượt ra khỏi màn hình
            objPosition.x = Mathf.Clamp(objPosition.x, bounds.min.x, bounds.max.x);
            objPosition.y = Mathf.Clamp(objPosition.y, bounds.min.y, bounds.max.y);

            transform.position = objPosition;
        }




        private void OnMouseDown()
        {
            distance = Camera.main.WorldToScreenPoint(transform.position).z;
        }

    }
}
