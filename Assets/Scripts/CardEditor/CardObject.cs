using ModdingAPI;
using UnityEngine;
using UnityEngine.UI;

namespace CardEditor
{
    public class CardObject : MonoBehaviour
    {
        [SerializeField] private Text titleText;
        [SerializeField] private Text cdtDescText;
        [SerializeField] private Text eftDescText;
        [SerializeField] private Text memeText;
        [SerializeField] private Text rarety;
        public CardInfo CardInfo
        {
            set
            {
                if (titleText) titleText.text = value.Title;
                if (cdtDescText) cdtDescText.text = value.ConditionDesc;
                if (eftDescText) eftDescText.text = value.EffectDesc;
                if (memeText) memeText.text = value.Meme;
                if (rarety) rarety.text = "ÔÝÎ´Ìí¼Ó";
            }
        }

    }
}