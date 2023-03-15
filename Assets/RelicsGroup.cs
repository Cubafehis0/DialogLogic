using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RelicsGroup : VisualObject<RelicsController>
{
    public OwnedRelicObj prefab;
    public List<OwnedRelicObj> relics;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateVisuals), 0f, 1f);
    }

    public override  void UpdateVisuals()
    {
        var targetRelics = target.GetRelics();
        while (relics.Count < targetRelics.Count)
        {
            relics.Add(Instantiate(prefab, transform));
        }
        for (int i = 0; i < relics.Count; i++)
        {
            if (i < targetRelics.Count)
            {
                relics[i].gameObject.SetActive(true);
                relics[i].target = targetRelics[i];
            }
            else
            {
                relics[i].gameObject.SetActive(false);
            }
        }

    }
}
