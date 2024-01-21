using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Spinner
{
    public class ChildController : MonoBehaviour
    {

        [SerializeField] private float spinSpeed;
        [SerializeField] private float tempSpeed;
        //[SerializeField] private float multiplySpeed;
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip sfxClip;
        [SerializeField] private bool playAudio;
        private SpinController spinner;
        private bool dragging;
        private bool hasDragged;
        private Vector3 lastMousePos;
        private Vector3 firstMousePos;


        // Start is called before the first frame update
        void Start()
        {
            spinner = FindObjectOfType<SpinController>();
            dragging = false;
            hasDragged = false;
            playAudio = false;
        }

        // Update is called once per frame
        void FixedUpdate()
        {

            if (transform.IsChildOf(spinner.transform))
            {
                if (dragging && !hasDragged)
                {
                    FollowMouse();
                }
                if (!dragging && hasDragged)
                {
                    Spin();
                }

            }

            if (playAudio)
            {
                source.clip = sfxClip;
                source.loop = true;
                source.Play();
            }
            else
            {
                source.Stop();
            }
        }

        private void Spin()
        {
            Vector3 mouseDelta = lastMousePos - firstMousePos;
            //multiplySpeed = mouseDelta.magnitude / Time.deltaTime;
            if (mouseDelta.x > 1f)
            {
                transform.parent.Rotate(0, 0, tempSpeed * Time.deltaTime);
            }
            else if (mouseDelta.x < -1f)
            {
                transform.parent.Rotate(0, 0, -tempSpeed * Time.deltaTime);
            }
            playAudio = true;
        }
        private void FollowMouse()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -0.1f;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
            float angle = Mathf.Atan2(mouseWorldPos.y - transform.parent.position.y, mouseWorldPos.x - transform.parent.position.x) * Mathf.Rad2Deg;
            transform.parent.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            playAudio = false;

        }
        private void OnMouseDrag()
        {
            dragging = true;
            hasDragged = false;
        }
        private void OnMouseDown()
        {
            firstMousePos = Input.mousePosition;
            dragging = false;
            if(hasDragged)
            {
                hasDragged = false;
                tempSpeed = 0f;
                playAudio = false;
            }    
        }
        private void OnMouseUp()
        {
            hasDragged = true;
            dragging = false;
            lastMousePos = Input.mousePosition;
            Vector3 mouseDelta = lastMousePos - firstMousePos;
            if (mouseDelta.x > 1f || mouseDelta.x < -1f)
            {
                tempSpeed = spinSpeed;
                playAudio = true;

            }    
            else
            {
                tempSpeed = 0f;
                playAudio = false;
            }    
        }

    }
}