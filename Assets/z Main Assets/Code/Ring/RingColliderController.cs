using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingColliderController : MonoBehaviour
{
    [SerializeField]
    private GameObject ringParticles, mainObject;

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

            Score();

            StartCoroutine(DestroySystems(particles));
        }
    }

    private void Score()
    {
        GameController.instance.Player.GetComponent<PlayerMovement>().SpeedBoost();
    }

    IEnumerator DestroySystems(ParticleSystem.MainModule particles)
    {
        Destroy(this.GetComponent<Collider>());

        yield return new WaitForSeconds(0.1f);
        particles.loop = false;

        yield return new WaitForSeconds(1);
        Destroy(mainObject);
    }

}
