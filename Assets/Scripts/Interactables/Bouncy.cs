using Unity.VisualScripting;
using UnityEngine;

namespace Interactables
{
    public class Bouncy : MonoBehaviour
    {
        public int maxBounces = 3;
        public float bounciness = 1;
        private int remainingBounces;
        private Collider2D collide;

        public PhysicsMaterial2D bouncyMaterial; // Assign this in the inspector

        void Start()
        {
            bouncyMaterial.bounciness = 0;

            // Assign the bouncy material to the collider
            if (bouncyMaterial != null)
            {
                collide = GetComponent<Collider2D>();
                if (collide != null)
                {
                    collide.sharedMaterial = bouncyMaterial;
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if there are remaining bounces
            if (remainingBounces > 0)
            {
                remainingBounces--;
                Debug.Log("bounce");
            }
            else
            {
                bouncyMaterial.bounciness = 0;
                collide.sharedMaterial = bouncyMaterial;
            }
        }

        public void EnableBounce()
        {
            remainingBounces = maxBounces;
            bouncyMaterial.bounciness = bounciness;
            collide.sharedMaterial = bouncyMaterial;
        }
    }
}