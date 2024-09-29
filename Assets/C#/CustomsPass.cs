using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomsPass
{
    //模拟关卡数据，0代表普通格子，负数代表数字格子（-10代表数字0），2代表方格，3代表圆圈
    public static int[][][] customPasses = new int[10][][]
    {
        new int[6][] //第一关
        {
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, -3, 0, 0, 0, 0 },
            new int[6] { 2, 0, 0, 0, 0, 3 },
            new int[6] { 0, 0, 0, 0, -3, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
        },

        new int[6][] //第二关
        {
            new int[6] { 0, 0, 0, 0, 0, 3 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, -3, 0, 0, -3, 0 },
            new int[6] { 0, 0, 2, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, -10, 0 },
        },

        new int[6][] //第三关
        {
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, -3, 0, 2, 0, 2 },
            new int[6] { 0, 0, 0, -4, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 3, 0, 0, 0, 0, 0 },
        },

        new int[6][] //第四关
        {
            new int[6] { 0, 0, 0, 0, 0, 3 },
            new int[6] { 0, 0, -10, 0, 0, 0 },
            new int[6] { 0, 2, 0, -4, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 2 },
            new int[6] { 0, 2, 0, 0, -1, 0 },
        },

        new int[6][] //第五关
        {
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 3, -5, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 3, -3, 3, 0 },
            new int[6] { 0, 0, 0, 0, 2, 0 },
            new int[6] { 2, 0, -10, 0, 0, 0 },
        },

        new int[6][] //第六关
        {
            new int[6] { 0, 0, 0, 0, -2, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 2, 3, 0, 0, 0 },
            new int[6] { 0, 0, -7, 0, 0, -2 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 2, 0, -3, 0, 0, 2 },
        },

        new int[6][] //第七关
        {
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 2, 3, 0, 3, 0, 0 },
            new int[6] { -3, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, -6, 0, 0 },
            new int[6] { 0, 0, 0, 2, 3, 0 },
            new int[6] { 0, -2, 2, 0, 0, 0 },
        },

        new int[6][] //第八关
        {
            new int[6] { 0, 0, 0, 0, -10, 0 },
            new int[6] { -3, 0, 3, 0, 0, 0 },
            new int[6] { 0, 0, -6, 3, 0, 0 },
            new int[6] { 0, 3, 0, 0, 0, 0 },
            new int[6] { 0, 0, 2, 2, 0, 2 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
        },

        new int[6][] //第九关
        {
            new int[6] { 3, 0, 3, 0, 0, 2 },
            new int[6] { 0, -6, 0, 2, -3, 0 },
            new int[6] { 0, 3, 0, 0, 0, 2 },
            new int[6] { 0, 0, 0, 3, -4, 0 },
            new int[6] { -2, 0, 0, 0, 2, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
        },

        new int[6][] //第十关
        {
            new int[6] { -3, 3, 0, 0, 2, 0 },
            new int[6] { 3, 2, 0, -8, 0, 0 },
            new int[6] { 0, 0, 0, 0, 0, 0 },
            new int[6] { 0, 0, 0, -4, 0, -3 },
            new int[6] { 0, 3, 0, 0, 2, 3 },
            new int[6] { 0, 0, 0, 0, 3, 0 },
        }
    };
}
