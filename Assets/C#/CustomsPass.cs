using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class functionInfo
{
    // Ax + By + C = 0
    public float A;
    public float B;
    public float C;
}


public class CustomsPass
{
    //模拟关卡数据，0代表普通格子，2代表方格，3代表圆圈，
    //数字按照特定编码 10 代表数字类型1，数字大小0， -30 代表数字类型3 数字大小 -3
    //其中,数字类型 1代表白色 2代表黑色 3代表绿色 4代表红色
    public static int[][][] customPassesStage1 = new int[10][][] //关卡主题一
    {
        new int[6][] //第一关
        {
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 13, 0, 0, 0, 0 },
            new int[6] { 2, 0, 0, 0, 0, 3 },
            new int[6] { 0, 0, 0, 0, 13, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
        },

        new int[6][] //第二关
        {
            new int[6] { 0, 0, 0, 0, 0, 3 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 13, 0, 0, 13, 0 },
            new int[6] { 0, 0, 2, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, -10, 0 },
        },

        new int[6][] //第三关
        {
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 13, 0, 2, 0, 2 },
            new int[6] { 0, 0, 0, 14, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 3, 0, 0, 0, 0, 0 },
        },

        new int[6][] //第四关
        {
            new int[6] { 0, 0, 0, 0, 0, 3 },
            new int[6] { 0, 0, 10, 0, 0, 0 },
            new int[6] { 0, 2, 0, 14, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 2 },
            new int[6] { 0, 2, 0, 0, 11, 0 },
        },

        new int[6][] //第五关
        {
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 3, 15, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 3, 13, 3, 0 },
            new int[6] { 0, 0, 0, 0, 2, 0 },
            new int[6] { 2, 0, 10, 0, 0, 0 },
        },

        new int[6][] //第六关
        {
            new int[6] { 0, 0, 0, 0, 12, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 2, 3, 0, 0, 0 },
            new int[6] { 0, 0, 17, 0, 0, 12 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 2, 0, 13, 0, 0, 2 },
        },

        new int[6][] //第七关
        {
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 2, 3, 0, 3, 0, 0 },
            new int[6] { 13, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 16, 0, 0 },
            new int[6] { 0, 0, 0, 2, 3, 0 },
            new int[6] { 0, 12, 2, 0, 0, 0 },
        },

        new int[6][] //第八关
        {
            new int[6] { 0, 0, 0, 0, 10, 0 },
            new int[6] { 13, 0, 3, 0, 0, 0 },
            new int[6] { 0, 0, 16, 3, 0, 0 },
            new int[6] { 0, 3, 0, 0, 0, 0 },
            new int[6] { 0, 0, 2, 2, 0, 2 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
        },

        new int[6][] //第九关
        {
            new int[6] { 3, 0, 3, 0, 0, 2 },
            new int[6] { 0, 16, 0, 2, 13, 0 },
            new int[6] { 0, 3, 0, 0, 0, 2 },
            new int[6] { 0, 0, 0, 3, 14, 0 },
            new int[6] { 12, 0, 0, 0, 2, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
        },

        new int[6][] //第十关
        {
            new int[6] { 13, 3, 0, 0, 2, 0 },
            new int[6] { 3, 2, 0, 18, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 14, 0, 13 },
            new int[6] { 0, 3, 0, 0, 2, 3 },
            new int[6] { 0, 0, 0, 0, 3, 0 },
        }
    }; //关卡主题一
    public static int[][][] customPassesStage2 = new int[10][][] //关卡主题二
    {
        new int[6][] //第一关
        {
            new int[6] { 0, 0, 0, 2, 0, 0 },
            new int[6] { 0, 0, 13, 0, 0, 0 },
            new int[6] { 0, 0, 0, 2, 0, 0 },
            new int[6] { 0, 0, 0, 0, 25, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 3, 0, 0 },
        },

        new int[6][] //第二关
        {
            new int[6] { 0, 0, 0, 2, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 12 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 11, 0, 22, 0, 0 },
            new int[6] { 0, 0, 0, 3, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
        },

        new int[6][] //第三关
        {
            new int[6] { 0, 0, 0, 0, 0, 3 },
            new int[6] { 0, 0, 0, 0, 0, 13 },
            new int[6] { 0, 0, 23, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 21 },
            new int[6] { 2, 0, 13, 0, 3, 0 },
        },

        new int[6][] //第四关
        {
            new int[6] { 2, 0, 0, 0, 0, 0 },
            new int[6] { 0, 23, 0, 0, 0, 22 },
            new int[6] { 2, 0, 0, 21, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 13, 0, 3, 0, 0, 13 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
        },

        new int[6][] //第五关
        {
            new int[6] { 0, 0, 0, 0, 0, 13 },
            new int[6] { 0, 23, 0, 0, 0, 0 },
            new int[6] { 2, 0, 0, 3, 0, 0 },
            new int[6] { 0, 0, 0, 25, 41, 0 },
            new int[6] { 0, 0, 0, 3, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
        },

        new int[6][] //第六关
        {
            new int[6] { 0, 0, 0, 0, 0, 13 },
            new int[6] { 0, 0, 0, 16, 0, 0 },
            new int[6] { 0, 0, 0, 0, 41, 3 },
            new int[6] { 21, 0, 0, 25, 27, 0 },
            new int[6] { 0, 0, 0, 0, 2, 42 },
            new int[6] { 2, 0, 0, 0, 0, 0 },
        },

        new int[6][] //第七关
        {
            new int[6] { 21, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 20, 0 },
            new int[6] { 23, 0, 0, 0, 3, 0 },
            new int[6] { 0, 13, 0, 31, 0, 0 },
            new int[6] { 0, 31, 2, 0, 0, 21 },
        },

        new int[6][] //第八关
        {
            new int[6] { 0, 0, 0, 0, 12, 2 },
            new int[6] { 0, 0, -32, 0, 31, 0 },
            new int[6] { 0, 0, 25, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 3, 21, -41, 0, 0, 21 },
        },

        new int[6][] //第九关
        {
            new int[6] { 0, 0, -31, 0, 0, 12 },
            new int[6] { 0, 12, 31, 0, 0, 0 },
            new int[6] { 0, 0, 41, 2, 0, 0 },
            new int[6] { 0, 3, 0, 22, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 22, 42, 0, 0, 0 },
        },

        new int[6][] //第十关
        {
            new int[6] { 12, 0, 0, 0, 0, 13 },
            new int[6] { 0, 0, 31, 23, 0, 0 },
            new int[6] { -31, 0, 2, 0, 0, 0 },
            new int[6] { 0, 0, 0, 41, 15, 0 },
            new int[6] { 0, 24, 0, 0, 3, 0 },
            new int[6] { 0, -42, 0, 0, 0, 0 },
        }
    }; //关卡主题二
    public static int[][][] customPassesStage3 = new int[10][][] //关卡主题三
    {
        new int[6][] //第一关
        {
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 13, 0, 0, 0, 0 },
            new int[6] { 2, 0, 0, 0, 0, 3 },
            new int[6] { 0, 0, 0, 0, 0, 23 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
        },
        new int[6][] //第二关
        {
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 11, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 13, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 3, 0, 0, 0, 2 },
        },

        new int[6][] //第三关
        {
            new int[6] { 2, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 21, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 13, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 3, 0, 0, 0, 0 },
        },

        new int[6][] //第四关
        {
            new int[6] { 2, 0, 0, 0, 0, 20 },
            new int[6] { 0, 0, 0, 23, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 3, 0, 0, 0, 0, 0 },
            new int[6] { 0, 17, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
        },

        new int[6][] //第五关
        {
            new int[6] { 2, 0, 0, 21, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 15, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 3, 0, 0, 0, 0 },
            new int[6] { 0, 0, 21, 0, 0, 2 },
        },

        new int[6][] //第六关
        {
            new int[6] { 2, 0, 0, 0, 0, 2 },
            new int[6] { 0, 0, 21, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 13 },
            new int[6] { 0, 12, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 3, 0, 0, 3 },
        },

        new int[6][] //第七关
        {
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 2, 0, 0, 14, 0 },
            new int[6] { 0, 0, 0, 0, 0, 3 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 22, 0, 0, 0 },
        },

        new int[6][] //第八关
        {
            new int[6] { 2, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 13, 0, 0, 3, 0 },
            new int[6] { 0, 0, 0, 0, 23, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
        },

        new int[6][] //第九关
        {
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 3, 0, 0, 0, 0 },
            new int[6] { 0, 0, 2, 16, 0, 0 },
            new int[6] { 0, 0, 0, 2, 0, 22 },
            new int[6] { 0, 0, 0, 0, 3, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
        },

        new int[6][] //第十关
        {
            new int[6] {3, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 16, 0, 0 },
            new int[6] { 0, 0, 0, 0, 2, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 14, 0 },
        },
    }; //关卡主题三

    public static functionInfo[] functionInfos =
    {
        new functionInfo {A = 1, B = 0, C = -2.5f },
        new functionInfo {A = 1, B = 0, C = -2.5f },
        new functionInfo {A = 0, B = 1, C = -2.5f },
        new functionInfo {A = 0, B = 1, C = -1.5f },
        new functionInfo {A = -1, B = 1, C = 0 },
        new functionInfo {A = 1, B = 0, C = -3.5f},
        new functionInfo {A = -1, B = 1, C = 2f },
    };
}
