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
    Unopened,//δ����
    Opened,//����δ�н�
    UnderWay,//�нӽ�����
    CanDeliver,//�Ѿ�����ܽ���
    Finished, //�Ѿ�����
    Expired //����
}
