using UnityEngine;

[SerializeField]
public class Mission : MissionInfo
{
    [SerializeField]
    private bool finished = false;

    public bool Finished { get => finished; }

    public Mission()
    { }

    public Mission(MissionInfo missionInfo) : base(missionInfo)
    {
        //Debug.LogError(missionInfo.ToString());
    }

    //public void ChangeMisionState()
    //{
    //    if (this.missionState == MissionState.Opened)
    //    {
    //        this.missionState = MissionState.UnderWay;
    //        this.CheckMissionState();
    //    }
    //    if (this.missionState == MissionState.CanDeliver)
    //    {
    //        this.missionState = MissionState.Finished;
    //        this.CheckMissionState();
    //    }
    //}
    //public MissionState CheckMissionState()
    //{
    //    MissionState state = this.missionState;
    //    if (IsDead()) this.missionState = MissionState.Expired;
    //    switch (this.missionState)
    //    {
    //        case MissionState.Unopened:
    //            if (triggerIncident.TrueForAll(x => x.Finished))
    //            {
    //                this.missionState = MissionState.Opened;
    //            }
    //            break;
    //        case MissionState.Opened:
    //            if (this.missionType == MissionType.Main)
    //            {
    //                this.missionState = MissionState.UnderWay;
    //            }
    //            break;
    //        case MissionState.UnderWay:
    //            if (CheckFinshedCondition())
    //            {
    //                this.missionState = MissionState.CanDeliver;
    //            }
    //            break;
    //        case MissionState.CanDeliver:
    //            if (this.missionType == MissionType.Main || this.missionType == MissionType.Lambda)
    //            {
    //                this.missionState = MissionState.Finished;
    //            }
    //            break;
    //        case MissionState.Finished:
    //            break;
    //        case MissionState.Expired:
    //            break;
    //        default:
    //            Debug.LogError("wrong MissionState");
    //            break;
    //    }

    //    if (this.missionState != state)
    //    {
    //    }

    //    return this.missionState;
    //}
    public bool CheckStartCondition()
    {
        return start.Exists(x => x.incident.Finished);
    }

    public bool CheckFinshedCondition()
    {
        //Debug.LogError(end[0].incident.incidentName);
        return end.Exists(x => x.incident.Finished);
    }

    public void MissionFinished()
    {
        finished = true;
    }
    //private bool IsDead()
    //{
    //    return deadIncident.TrueForAll(x => x.Finished);
    //}
}