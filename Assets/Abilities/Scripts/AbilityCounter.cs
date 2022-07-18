public class AbilityCounter
{
    private AbilityBase ability;
    private int cnt;
    private string owner;

    public AbilityCounter(AbilityBase ability, int cnt,string owner)
    {
        this.ability = ability;
        this.cnt = cnt;
        this.owner = owner;
    }

    public int Count
    {
        get => cnt;
        set
        {
            if (cnt != value)
            {
                int prev = cnt;
                cnt = value;
                ability.OnUpdate(owner, value);
                if (value == 0)
                {
                    ability.OnRemove(owner);
                }
                if (prev != 0 && value == 0)
                {
                    ability.OnAdd(owner);
                }
            }

        }
    }

    public bool NeedDestory() { return cnt <= 0; }
    public AbilityBase Ability { get => ability; }
    public string Owner { get => owner; }

    public void OnRemove() { ability.OnRemove(owner); }
    public void OnAdd() { ability.OnAdd(owner); }
    public void OnTurnStart() { ability.OnTurnStart(owner); }
    public void OnTurnEnd() { ability.OnTurnEnd(owner); }
}
