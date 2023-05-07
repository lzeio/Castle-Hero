using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(StatSystem))]
public class Cannon : MonoBehaviour
{
    private StatSystem characterStats;
    private Vector3 raycastPoint;
    private AudioSource audioSource;
    bool canShoot = true;
    public RaycastHit[] hits;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterStats = GetComponent<StatSystem>();
        characterStats.OnDeath += OnDeath;

    }

    private void OnDeath(GameObject character)
    {
        GameplayManager.Instance.WaveSystem.KilledInAction(character);
        transform.DOScale(0f, 1f).OnComplete(() => Destroy(gameObject));
       
    }

    // Update is called once per frame
    void Update()
    {
        raycastPoint = new Vector3(transform.position.x, transform.position.y + 1.75f, transform.position.z);
        hits = Physics.RaycastAll(raycastPoint, transform.forward, characterStats.characterData.AttackRange);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.gameObject.layer == this.gameObject.layer)
            {
                continue;
            }
            else
            if (hit.transform.gameObject.layer != this.gameObject.layer)
            {
                SpawnCannonBall();
                return;
            }
        }

    }
    private void OnDestroy()
    {
        characterStats.OnDeath -= OnDeath;

        DOTween.KillAll();  
    }
    private void SpawnCannonBall()
    {
        if (canShoot)
        {
            canShoot = false;
            GameObject arrow = Instantiate(characterStats.characterData.Projectile,raycastPoint, Quaternion.identity, this.transform);
            AttackPoint arrowAttack = arrow.GetComponent<AttackPoint>();
            arrowAttack.SetStatsData(characterStats);
            ShootProjectile(arrow);
            DOVirtual.DelayedCall(2.25f, () => canShoot = true).SetUpdate(false);
            audioSource.Play();
        }
    }

    public void ShootProjectile(GameObject projectile)
    {
        projectile.transform.DOMoveZ(projectile.transform.position.z * characterStats.characterData.AttackRange, 10f);
        if (projectile != null)
            DOVirtual.DelayedCall(10f, () => { projectile.transform.DOScale(0, 1f); }).SetUpdate(false);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (characterStats != null)
            Debug.DrawRay(raycastPoint, transform.forward * characterStats.characterData.AttackRange, Color.yellow);
    }
}
