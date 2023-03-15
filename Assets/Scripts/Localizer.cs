﻿using ModdingAPI;

public class Localizer
{
    public static string GetRarityString(int rarity)
    {
        return rarity switch
        {
            0 => "普通",
            1 => "稀有",
            _ => "其他",
        };
    }
    public static string GetCategoryString(PersonalityType personalityType)
    {
        return personalityType switch
        {
            PersonalityType.Logic => "逻辑",
            PersonalityType.Passion => "激情",
            PersonalityType.Moral => "道德",
            PersonalityType.Unethic => "无忌",
            PersonalityType.Detour => "迂回",
            PersonalityType.Strong => "强势",
            _ => "其他",
        };
    }
}

