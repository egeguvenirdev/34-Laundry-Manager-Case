using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MeleeEnemy : EnemyBase
{
    [SerializeField] private float walkRange = 1;
    [SerializeField] private string[] gemNames;
    private Vector3 destination;
    private ObjectPooler objectPooler;

    protected override void MoveTowardsPlayer(Vector3 player)
    {
        if (agent == null) return;

        if (player != null && canMove) // check the player if its dead or what
        {
            destination = player;
            agent.SetDestination(destination);
            //Debug.Log(agent.remainingDistance);
        }

        if (agent.remainingDistance > 0 && agent.remainingDistance < walkRange)
        {
            if (canMove) transform.LookAt(destination);
            if (canMove)
            {
                isRunning = false;
                StartCoroutine(HitRoutine());
            }
        }
        else
        {
            if (canMove && !isRunning)
            {
                StopAllCoroutines();
                isRunning = true;
            }
        }
    }

    private IEnumerator HitRoutine()
    {
        canMove = false;
        yield return new WaitForSeconds(0.833f * 0.54f);
        if (objectPooler == null) objectPooler = ObjectPooler.Instance;
        Fire();
        yield return new WaitForSeconds(0.833f - (0.833f * 0.54f));
        canMove = true;
    }

    public override void TakeDamage(float hitAmount)
    {
        base.TakeDamage(hitAmount);
        PlayParticle("BossHitParticle");
    }

    protected override void Die()
    {
        PlayParticle("HitParticle");
        DropExpDiamond();
        base.Die();
        gameObject.SetActive(false);
    }

    private void PlayParticle(string particleName)
    {
        var particle = ObjectPooler.Instance.GetPooledObjectWithTag(particleName);
        Vector3 particlePos = new Vector3(transform.position.x, 0.25f, transform.position.z);
        particle.transform.position = particlePos;
        particle.transform.rotation = Quaternion.identity;
        particle.SetActive(true);
        particle.GetComponent<ParticleSystem>().Play();
    }

    private void DropExpDiamond()
    {
        GameObject diamond = ObjectPooler.Instance.GetPooledObjectWithTag(gemNames[Random.Range(0, gemNames.Length)]);
        diamond.transform.position = transform.position;
        diamond.transform.rotation = Quaternion.identity;
        diamond.SetActive(true);

        diamond.transform.DOJump(new Vector3(diamond.transform.position.x, 0, diamond.transform.position.z), 2, 1, 0.3f).OnComplete(() =>
        {
            diamond.GetComponent<Collider>().enabled = true;
        });
    }

    private void Fire()
    {
        //
    }

    private void ResEnemy()
    {
        canMove = true;
        isRunning = false;
    }
}