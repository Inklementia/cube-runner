using UnityEngine;

namespace Source.Code.Not_Used
{
    public class RoadMove : MonoBehaviour
    {
        [SerializeField] private float speed = 12f;
    

        // Update is called once per frame
        void Update()
        {
            transform.Translate(0,0,-1 * Time.deltaTime * speed);
        }
    }
}
