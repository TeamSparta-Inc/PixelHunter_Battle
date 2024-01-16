using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_SkillManager : MonoBehaviour
{
    public GameObject player;
    public GameObject bottom;

    public GameObject[] prefabs;

    public MonsterManager monsterManger;

    void Update()
    {
        // 도사근접 (기본, 궁극스킬)
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Instantiate(prefabs[0], player.transform.position, prefabs[0].transform.rotation);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Instantiate(prefabs[1], bottom.transform.position, prefabs[1].transform.rotation);

        // 닌자근접 (기본, 궁극스킬)
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Instantiate(prefabs[2], player.transform.position, prefabs[2].transform.rotation);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            Instantiate(prefabs[3], bottom.transform.position, prefabs[3].transform.rotation);

        // 페르시안근접 (기본, 궁극스킬)
        if (Input.GetKeyDown(KeyCode.Alpha5))
            Instantiate(prefabs[4], player.transform.position, prefabs[4].transform.rotation);
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            for(int i = 0; i < monsterManger.activeMonsters.Count; i++)
            {
                if (monsterManger.activeMonsters[i].activeSelf)
                {
                    prefabs[5].GetComponent<Test_Destroy>().target = monsterManger.activeMonsters[i];
                    Instantiate(prefabs[5], monsterManger.activeMonsters[i].transform.position, prefabs[5].transform.rotation);
                }
            }
        }

        // 스파르타근접 (기본, 궁극스킬)
        if (Input.GetKeyDown(KeyCode.Alpha7))
            Instantiate(prefabs[6], player.transform.position, prefabs[6].transform.rotation);
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            for (int i = 0; i < monsterManger.activeMonsters.Count; i++)
            {
                if (monsterManger.activeMonsters[i].activeSelf)
                {
                    prefabs[7].GetComponent<Test_Destroy>().target = monsterManger.activeMonsters[i];
                    Instantiate(prefabs[7], monsterManger.activeMonsters[i].transform.position, prefabs[7].transform.rotation);
                }
            }
        }

        //삼국지근접 (기본, 궁극스킬)
        if (Input.GetKeyDown(KeyCode.Alpha9))
            Instantiate(prefabs[8], player.transform.position, prefabs[8].transform.rotation);
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Vector3 vec3 = player.transform.localScale;
            if (Mathf.Approximately(vec3.x, 1f))
            {
                Instantiate(prefabs[9], bottom.transform.position, prefabs[9].transform.rotation);
                prefabs[9].transform.rotation = Quaternion.Euler(0f, -90, 20f);
            }
            else
            {
                Instantiate(prefabs[9], bottom.transform.position, prefabs[9].transform.rotation);
                prefabs[9].transform.rotation = Quaternion.Euler(0f, 90, 20f);
            }
        }
    }

}

