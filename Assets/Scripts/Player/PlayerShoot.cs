using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private int bullets = 2;
    [SerializeField]private GameObject camera;
    [SerializeField]private int damage = 10;
    private void Start()
    {
        InputManager.Instance.OnShootInitiated += Shoot;
    }

    private void Shoot()
    {
        RaycastHit hit;
        if(bullets > 0)
        {
            bullets--;
            Debug.DrawLine(camera.transform.position, camera.transform.position + camera.transform.forward * 100f,
                Color.red);
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 100f))
            {
                Debug.LogWarning("BANG!");
                IDamagable target = hit.collider.GetComponentInParent<IDamagable>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
        }
        else
        {
            Debug.LogWarning("THERE AREN'T ANY MORE BULLETS!");
        }
    }

    private void OnDisable()
        {
            InputManager.Instance.OnShootInitiated -= Shoot;
        }
    
}
