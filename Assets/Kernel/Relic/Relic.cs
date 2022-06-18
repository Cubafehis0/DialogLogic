using ModdingAPI;
using System;

[Serializable]
public class Relic
{
    public RelicInfo relicInfo = null;

    public static PersonalityType GetOppositeCategory(PersonalityType type)
    {
        return type switch
        {
            PersonalityType.Logic => PersonalityType.Passion,
            PersonalityType.Passion => PersonalityType.Logic,
            PersonalityType.Moral => PersonalityType.Unethic,
            PersonalityType.Unethic => PersonalityType.Moral,
            PersonalityType.Detour => PersonalityType.Strong,
            PersonalityType.Strong => PersonalityType.Detour,
            _ => PersonalityType.Inside,
        };
    }

    public Relic() { }

    public Relic(RelicInfo relicInfo) : this()
    {
        this.relicInfo = relicInfo;
    }

    public static string GetRarityString(int rarity)
    {
        return rarity switch
        {
            0 => "��ͨ",
            1 => "ϡ��",
            _ => "����",
        };
    }

    public static string GetCategoryString(PersonalityType personalityType)
    {
        return personalityType switch
        {
            PersonalityType.Logic => "�߼�",
            PersonalityType.Passion => "����",
            PersonalityType.Moral => "����",
            PersonalityType.Unethic => "�޼�",
            PersonalityType.Detour => "�ػ�",
            PersonalityType.Strong => "ǿ��",
            _ => "����",
        };
    }
}