// TimedObjectDestructor.cs
// March 2nd, 2020
// Updated by Matheus Vilano
// Removed warnings regarging obsolete Functions

using UnityEngine;

namespace UnityStandardAssets.Utility
{
    public class TimedObjectDestructor : MonoBehaviour
    {
        [SerializeField] private float m_TimeOut = 1.0f;
        [SerializeField] private bool m_DetachChildren = false;
        
        private void Awake()
        {
            Invoke("DestroyNow", m_TimeOut);
        }

        private void DestroyNow()
        {
            if (m_DetachChildren)
            {
                transform.DetachChildren();
            }
            Destroy(gameObject);
        }
    }
}
