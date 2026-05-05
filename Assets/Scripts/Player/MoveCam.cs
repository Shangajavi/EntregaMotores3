using UnityEngine;

public class MoveCam : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;
    void Update()
    {
       transform.position = cameraPosition.position; 
    }
}
