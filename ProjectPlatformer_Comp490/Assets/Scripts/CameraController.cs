using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CameraController : MonoBehaviour
    {
        // movement speed
        public float damping = 1.5f;

        public Vector2
            // character to be not in center of screen
            offset = new Vector2(0f, 0f);

        //  mirror reflection of OFFSET along the y axis
        public bool faceLeft;
        private Transform player;
        private int lastX;
        private Vector3 velocity = Vector3.zero;

        void Start()
        {
            offset = new Vector2(Mathf.Abs(offset.x), offset.y);
            FindPlayer(faceLeft);
        }

        public void FindPlayer(bool playerFaceLeft)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            lastX = Mathf.RoundToInt(player.position.x);
            if (playerFaceLeft)
            {
                transform.position = new Vector3(player.position.x - offset.x, player.position.y + offset.y,
                    transform.position.z);
            }
            else
            {
                transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y,
                    transform.position.z);
            }
        }

        private void LateUpdate()
        {
            if (player)
            {
                int currentX = Mathf.RoundToInt(player.position.x);
                if (currentX > lastX) faceLeft = false;
                else if (currentX < lastX) faceLeft = true;
                lastX = Mathf.RoundToInt(player.position.x);

                Vector3 target;
                if (faceLeft)
                {
                    target = new Vector3(player.position.x - offset.x, player.position.y + offset.y,
                        transform.position.z);
                }
                else
                {
                    target = new Vector3(player.position.x + offset.x, player.position.y + offset.y,
                        transform.position.z);
                }

                // Smoothly move the camera toward the target position using SmoothDamp.
                transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, damping);
            }
        }
    }
}

