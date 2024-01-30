using UnityEngine;
using UnityEngine.EventSystems;

namespace Spinner
{
    public class Test : MonoBehaviour
    {

        [SerializeField] private MoveSpinnerController moveSpinner;
        [SerializeField] private bool isMoving;
        [SerializeField] private float stopTime;


        private Vector3 lastInput;
        private float projection;
        private float projectionX;
        private float projectionY;
        private float rotateSpeed;
        private bool isRotating;
        private AudioManager insAudio => AudioManager.Instance;

        void Start()
        {
            lastInput = Input.mousePosition;

        }

        void Update()
        {
            isMoving = moveSpinner.IsMovingParent();
            if (isRotating)
            {
                Vector3 v_deltaInput = lastInput - Input.mousePosition;
                Vector3 v_unitX = new Vector3(1, 0, 0);
                Vector3 v_unitY = new Vector3(0, 1, 0);

                projectionX = Vector3.Dot(v_unitX, v_deltaInput);
                projectionY = Vector3.Dot(v_unitY, v_deltaInput);

                if (Input.mousePosition.y < Screen.height / 2)
                {
                    projectionX = -projectionX;
                }

                if (Input.mousePosition.x > Screen.width / 2)
                {
                    projectionY = -projectionY;
                }

                projection = projectionX + projectionY;
                rotateSpeed = projection / 10f;
                rotateSpeed = (rotateSpeed > 20) ? 20 : (rotateSpeed < -20) ? -20 : rotateSpeed;
                transform.Rotate(Vector3.forward, rotateSpeed);
                lastInput = Input.mousePosition;
            }
            else
            {
                rotateSpeed = Mathf.Lerp(rotateSpeed, 0f, Time.deltaTime / stopTime);
                transform.Rotate(Vector3.forward, rotateSpeed);
                if (isMoving)
                    isRotating = false;
                

            }
            if (Mathf.Abs(rotateSpeed) > 0 && !isRotating)
                insAudio.PlaySpinClip(true, rotateSpeed);
            else
                insAudio.PlaySpinClip(false, rotateSpeed);
            lastInput = Input.mousePosition;
        }

        void OnMouseDown()
        {
            isRotating = true;
            projection = 0.0f;
        }

        void OnMouseUp()
        {
            isRotating = false;
        }
    }
}
