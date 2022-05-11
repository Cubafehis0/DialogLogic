using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 数学类工具
/// ·正态概率分布函数
/// </summary>
public static class MyMath
{
    /// <summary>
    /// 正态分布表
    /// </summary>
    private static readonly double[,] normal_probability = {
//0.0    0.01    0.02    0.03    0.04    0.05    0.06    0.07    0.08    0.09   x
{0.5000, 0.5040, 0.5080, 0.5120, 0.5160, 0.5199, 0.5239, 0.5279, 0.5319, 0.5359 },//0.0
{0.5398, 0.5438, 0.5478, 0.5517, 0.5557, 0.5596, 0.5636, 0.5675, 0.5714, 0.5753 },//0.1
{0.5793, 0.5832, 0.5871, 0.5910, 0.5948, 0.5987, 0.6026, 0.6064, 0.6103, 0.6141 },//0.2
{0.6179, 0.6217, 0.6255, 0.6293, 0.6331, 0.6368, 0.6404, 0.6443, 0.6480, 0.6517 },//0.3
{0.6554, 0.6591, 0.6628, 0.6664, 0.6700, 0.6736, 0.6772, 0.6808, 0.6844, 0.6879 },//0.4
{0.6915, 0.6950, 0.6985, 0.7019, 0.7054, 0.7088, 0.7123, 0.7157, 0.7190, 0.7224 },//0.5
{0.7257, 0.7291, 0.7324, 0.7357, 0.7389, 0.7422, 0.7454, 0.7486, 0.7517, 0.7549 },//0.6
{0.7580, 0.7611, 0.7642, 0.7673, 0.7703, 0.7734, 0.7764, 0.7794, 0.7823, 0.7852 },//0.7
{0.7881, 0.7910, 0.7939, 0.7967, 0.7995, 0.8023, 0.8051, 0.8078, 0.8106, 0.8133 },//0.8
{0.8159, 0.8186, 0.8212, 0.8238, 0.8264, 0.8289, 0.8355, 0.8340, 0.8365, 0.8389 },//0.9
{0.8413, 0.8438, 0.8461, 0.8485, 0.8508, 0.8531, 0.8554, 0.8577, 0.8599, 0.8621 },//1
{0.8643, 0.8665, 0.8686, 0.8708, 0.8729, 0.8749, 0.8770, 0.8790, 0.8810, 0.8830 },//1.1
{0.8849, 0.8869, 0.8888, 0.8907, 0.8925, 0.8944, 0.8962, 0.8980, 0.8997, 0.9015 },//1.2
{0.9032, 0.9049, 0.9066, 0.9082, 0.9099, 0.9115, 0.9131, 0.9147, 0.9162, 0.9177 },//1.3
{0.9192, 0.9207, 0.9222, 0.9236, 0.9251, 0.9265, 0.9279, 0.9292, 0.9306, 0.9319 },//1.4
{0.9332, 0.9345, 0.9357, 0.9370, 0.9382, 0.9394, 0.9406, 0.9418, 0.9430, 0.9441 },//1.5
{0.9452, 0.9463, 0.9474, 0.9484, 0.9495, 0.9505, 0.9515, 0.9525, 0.9535, 0.9535 },//1.6
{0.9554, 0.9564, 0.9573, 0.9582, 0.9591, 0.9599, 0.9608, 0.9616, 0.9625, 0.9633 },//1.7
{0.9641, 0.9648, 0.9656, 0.9664, 0.9672, 0.9678, 0.9686, 0.9693, 0.9700, 0.9706 },//1.8
{0.9713, 0.9719, 0.9726, 0.9732, 0.9738, 0.9744, 0.9750, 0.9756, 0.9762, 0.9767 },//1.9
{0.9772, 0.9778, 0.9783, 0.9788, 0.9793, 0.9798, 0.9803, 0.9808, 0.9812, 0.9817 },//2
{0.9821, 0.9826, 0.9830, 0.9834, 0.9838, 0.9842, 0.9846, 0.9850, 0.9854, 0.9857 },//2.1
{0.9861, 0.9864, 0.9868, 0.9871, 0.9874, 0.9878, 0.9881, 0.9884, 0.9887, 0.9890 },//2.2
{0.9893, 0.9896, 0.9898, 0.9901, 0.9904, 0.9906, 0.9909, 0.9911, 0.9913, 0.9916 },//2.3
{0.9918, 0.9920, 0.9922, 0.9925, 0.9927, 0.9929, 0.9931, 0.9932, 0.9934, 0.9936 },//2.4
{0.9938, 0.9940, 0.9941, 0.9943, 0.9945, 0.9946, 0.9948, 0.9949, 0.9951, 0.9952 },//2.5
{0.9953, 0.9955, 0.9956, 0.9957, 0.9959, 0.9960, 0.9961, 0.9962, 0.9963, 0.9964 },//2.6
{0.9965, 0.9966, 0.9967, 0.9968, 0.9969, 0.9970, 0.9971, 0.9972, 0.9973, 0.9974 },//2.7
{0.9974, 0.9975, 0.9976, 0.9977, 0.9977, 0.9978, 0.9979, 0.9979, 0.9980, 0.9981 },//2.8
{0.9981, 0.9982, 0.9982, 0.9983, 0.9984, 0.9984, 0.9985, 0.9985, 0.9986, 0.9986 },//2.9
{0.9987, 0.9990, 0.9993, 0.9995, 0.9997, 0.9998, 0.9998, 0.9999, 0.9999, 1.0000 }//3
        };

    /// <summary>
    /// 求正态分布区间概率
    /// </summary>
    /// <param name="standred_data_from">区间界限1</param>
    /// <param name="standred_data_to">区间界限2</param>
    /// <param name="mean">正态分布均值，默认为0</param>
    /// <param name="sd">正态分布方差，默认为1</param>
    /// <returns>概率值</returns>
    public static float GetProbability(float standred_data_from, float standred_data_to, float mean = 0.0f, float sd = 1.0f)
    {
        standred_data_from = (standred_data_from - mean) / sd;                          //标准化
        standred_data_to = (standred_data_to - mean) / sd;                              //标准化

        int row = (int)(Mathf.Abs(standred_data_from) * 10f);
        int col = ((int)(Mathf.Abs(standred_data_from) * 100f)) % 10;
        float result_from = row > 30 ? 1f : (float)normal_probability[row, col];        //查表

        row = (int)(Mathf.Abs(standred_data_to) * 10f);
        col = ((int)(Mathf.Abs(standred_data_to) * 100f)) % 10;
        float result_to = row > 30 ? 1f : (float)normal_probability[row, col];          //查表


        if (standred_data_from < 0f) result_from = 1f - result_from;
        if (standred_data_to < 0f) result_to = 1f - result_to;

        return result_to - result_from;

    }

    /// <summary>
    /// 给出曲率半径和角度计算偏移
    /// （通用性不高，有待优化）
    /// </summary>
    /// <param name="angle">与y轴夹角，一四象限为正，二三象限为负</param>
    /// <param name="radius">曲率半径</param>
    /// <returns>偏移</returns>
    public static Vector2 CalcCircleOffset(float angle, float radius)
    {
        Vector2 offset = Vector2.zero;
        offset.x = Mathf.Sin(angle) * radius;
        offset.y = Mathf.Cos(angle) * radius - radius;
        return offset;
    }

    public static void Shuffle<T>(IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int k = Random.Range(0, i + 1);
            (list[k], list[i]) = (list[i], list[k]);
        }
    }

    /// <summary>
    /// 输入分段概率分布函数返回随机值
    /// </summary>
    /// <param name="jp">分段概率分布函数</param>
    /// <returns>随机值</returns>
    public static int GetRandomJudge(float[] jp)
    {
        if (jp == null) return -1;
        float sum = 0;
        foreach (float f in jp) sum += f;
        float a = Random.Range(0,sum);
        float ub = 0;
        for (int i = 0; i < jp.Length; i++)
        {
            ub += jp[i] / sum;
            if (a < ub) return i;
        }
        return jp.Length - 1;
    }
}
