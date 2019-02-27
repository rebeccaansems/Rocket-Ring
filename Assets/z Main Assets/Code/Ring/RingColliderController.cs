using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingColliderController : MonoBehaviour
{
    [SerializeField]
    private GameObject ringParticles;

    private void Start()
    {
        ringParticles.SetActive(true);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            ParticleSystem.MainModule particles = ringParticles.GetComponent<ParticleSystem>().main;
            particles.gravityModifier = 2;
            particles.startSpeed = 2;

            StartCoroutine(DestroySystems(particles));
        }
    }

    IEnumerator DestroySystems(ParticleSystem.MainModule particles)
    {
        Destroy(this.GetComponent<Collider>());

        yield return new WaitForSeconds(0.1f);
        particles.loop = false;
    }

}
