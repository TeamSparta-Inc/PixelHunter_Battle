using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] Player player;
    [SerializeField] PlayerControler playerControler;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject skillProjectilePrefab;

    [SerializeField] private int poolSize = 10;

    private Queue<GameObject> projectilePool = new Queue<GameObject>();
    private Queue<GameObject> skillProjectilePool = new Queue<GameObject>();

    

    private void Awake()
    {
        instance = this;

        InitializeProjectilePool();
        InitializeSkillProjectilePool();
    }

    void InitializeProjectilePool()
    {
        for (int i = 0; i < poolSize; i++) 
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            projectilePool.Enqueue(projectile);
        }
    }

    public GameObject Getprojectile()
    {
        if (projectilePool.Count > 0)
        {
            GameObject projectile = projectilePool.Dequeue();
            projectile.SetActive(true);
            return projectile;
        }
        else
        {
            GameObject newProjectile = Instantiate(projectilePrefab);
            return newProjectile;
        }
    }

    public void ReturnProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
        projectilePool.Enqueue(projectile);
    }


    void InitializeSkillProjectilePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject skillProjectile = Instantiate(skillProjectilePrefab);
            skillProjectile.SetActive(false);
            skillProjectilePool.Enqueue(skillProjectile);
        }
    }

    public GameObject GetSkillProjectile()
    {
        if (skillProjectilePool.Count > 0)
        {
            GameObject SkillProjectile = skillProjectilePool.Dequeue();
            Debug.Log("가져온다");
            SkillProjectile.SetActive(true);
            return SkillProjectile;
        }
        else
        {
            GameObject newSkillProjectile = Instantiate(skillProjectilePrefab);
            return newSkillProjectile;
        }
    }

    public void ReturnSkillProjectile(GameObject skillProjectile)
    {
        Debug.Log("리턴한다");
        skillProjectile.SetActive(false);
        skillProjectilePool.Enqueue(skillProjectile);
    }
}
