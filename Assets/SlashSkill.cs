using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SlashSkill : ISkill
{
    GameObject skillObject;
    Transform player;

    private SkillData skillData;

    public SlashSkill(GameObject skillObject, Transform player)
    {
        this.skillObject = skillObject;
        this.player = player;
    }

    public void Execute()
    {
        // 스킬 로직 실행
        skillObject.transform.position = player.position;
        skillObject.GetComponent<AttackCollider>().SetDamage(skillData.damage);
        skillObject.SetActive(true);
    }

    public void LoadData(int index) // CSV -> JOSN
    {
        TextAsset jsonData = Resources.Load<TextAsset>("SkillInfo");
        string jsonString = jsonData.text;
        SkillDataList skillList = JsonUtility.FromJson<SkillDataList>(jsonString);
        skillData = Array.Find(skillList.skills, skill => skill.index == index);
    }
}
