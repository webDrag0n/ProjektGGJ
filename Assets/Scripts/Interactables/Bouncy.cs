using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Interactables
{
    public class Bouncy : MonoBehaviour
    {
        public float time = 2f;
        public float bounciness = 1;
        private Collider2D collide;

        public PhysicsMaterial2D bouncyMaterial; // Assign this in the inspector

        void Start()
        {
            bouncyMaterial.bounciness = 0;

            // Assign the bouncy material to the collider
            collide = GetComponent<Collider2D>();
            collide.sharedMaterial = bouncyMaterial;
        }

        public void EnableBounce()
        {
            IEnumerator wait()
            {
                yield return new WaitForSeconds(time);
                Debug.Log("Debounce");
                bouncyMaterial.bounciness = 0;
                collide.sharedMaterial = bouncyMaterial;
            }
            
            bouncyMaterial.bounciness = bounciness;
            collide.sharedMaterial = bouncyMaterial;
            Debug.Log("bounce");
            StartCoroutine(wait());
        }
    }
}