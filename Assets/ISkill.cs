using System;

[Serializable]
public class SkillData
{
    public int index;
    public string name;
    public float coolTime;
    public Enums.SkillType skillType;
    public int damage;
    public string description;
}

[Serializable]
public class SkillDataList
{
    public SkillData[] skills;
}

public interface ISkill
{
    void Execute();
    void LoadData(int index);
}
