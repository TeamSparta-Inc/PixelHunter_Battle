using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject slashObject;

    ISkill slash;

    private void Start()
    {
        slash = new SlashSkill(slashObject, player.transform);

        slash.LoadData(0);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            slash.Execute();
        }
    }

}
