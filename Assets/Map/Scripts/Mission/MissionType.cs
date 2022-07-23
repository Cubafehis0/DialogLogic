using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionType
{
    Main,
    Branch,
    Daily,
    Lambda
}

public enum MissionState
{
    Unopened,//未开启
    Opened,//开启未承接
    UnderWay,//承接进行中
    CanDeliver,//已经完成能交付
    Finished, //已经交付
    Expired //过期
}
