using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public partial class GomokuGame
    {
        public static int getPawn(int[,] map, int[] pos, int size = 15)
        {
            if (pos[0] >= 0 && pos[0] < size && pos[1] >= 0 && pos[1] < size)
            {
                return map[pos[0],pos[1]];
            }
            else
            {
                return 2;
            }
        }

        public static void addVector(int[] o, int[] v)
        {
            o[0] += v[0];
            o[1] += v[1];
        }

        public static void subVector(int[] o, int[] v)
        {
            o[0] -= v[0];
            o[1] -= v[1];
        }

        public static int getScoreOfAPoint(int[,] map, int[] pos, int side, int size = 15)
        {
            int result = 1;
            int[] dvector = new int[2];
            int[] startPoint = new int[2];
            int[] endPoint = new int[2];
            int[,] count = new int[3,4]; //第一维：连珠个数； 第二维：活，眠，活跳，眠跳
            bool leap;
            bool ray;
            int length;

            int[,] scores = new int[,]{
                { 4, 2, 3, 1 },
					   { 150, 30, 100, 20 },
					   { 1000, 150, 125, 100 }
            };

            // 连珠情况分析
            for (int direction = 0; direction < 4; direction++)
            {
                // 变量初始化
                leap = false;
                ray = false;
                length = 1;
                switch (direction)
                {
                    case 0:
                        dvector[0] = 1;
                        dvector[1] = 0;
                        break;
                    case 1:
                        dvector[0] = 0;
                        dvector[1] = 1;
                        break;
                    case 2:
                        dvector[0] = 1;
                        dvector[1] = 1;
                        break;
                    case 3:
                        dvector[0] = 1;
                        dvector[1] = -1;
                        break;
                }

                startPoint[0] = pos[0];
                startPoint[1] = pos[1];
                endPoint[0] = pos[0];
                endPoint[1] = pos[1];

                subVector(startPoint, dvector);
                addVector(endPoint, dvector);

                // 判断连几颗
                while (getPawn(map, startPoint) == side)
                {
                    subVector(startPoint, dvector);
                    length++;
                }
                if (getPawn(map, startPoint) == 0)
                {
                    subVector(startPoint, dvector);
                    if (getPawn(map, startPoint) != side)
                    {
                        addVector(startPoint, dvector);
                    }
                    else
                    {
                        leap = true;
                        while (getPawn(map, startPoint) == side)
                        {
                            subVector(startPoint, dvector);
                            length++;
                        }
                    }
                }
                while (getPawn(map, endPoint) == side)
                {
                    addVector(endPoint, dvector);
                    length++;
                }
                if (getPawn(map, endPoint) == 0)
                {
                    addVector(endPoint, dvector);
                    if (getPawn(map, endPoint) != side)
                    {
                        subVector(endPoint, dvector);
                    }
                    else
                    {
                        leap = true;
                        while (getPawn(map, endPoint) == side)
                        {
                            addVector(endPoint, dvector);
                            length++;
                        }
                    }
                }

                //判断状态
                if (length >= 5)
                {
                    return 10000;
                }
                else if (length == 1)
                {
                    continue;
                }
                if (getPawn(map, startPoint) != 0 && getPawn(map, endPoint) != 0) // 两头堵死
                {
                    if (length != 4 || !leap)
                    {
                        continue;
                    }
                }
                else if (getPawn(map, startPoint) != 0 || getPawn(map, endPoint) != 0) // 一头堵死
                {
                    ray = true;
                }

                // 计数
                if (!leap && !ray)
                {
                    count[length - 2,0]++;
                }
                else if (!leap && ray)
                {
                    count[length - 2,1]++;
                }
                else if (leap && !ray)
                {
                    count[length - 2,2]++;
                }
                else if (leap && ray)
                {
                    count[length - 2,3]++;
                }
            }

            // 计算分数

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (count[i,j] != 0)
                    {
                        if (i > 0 && count[i,j] > 2)
                        {
                            count[i,j] = 2;
                        }
                        result *= (int)Math.Pow(scores[i,j], count[i,j]);
                    }
                }
            }

            return result;
        }

        public static int[] getSubScore2(int[,] map, int size=15)
        {
            int[] result = { 0, 0 };
            int[] tmpCount = { 0, 0 };
            int[] pos = { 0, 0 };
            int tempscore;
            for (pos[0] = 0; pos[0] < size; pos[0]++)
            {
                for (pos[1] = 0; pos[1] < size; pos[1]++)
                {
                    if (getPawn(map, pos) == 0)
                    {
                        tempscore = getScoreOfAPoint(map, pos, -1);
                        if (tempscore > result[0])
                        {
                            result[0] = tempscore;
                        }
                        tempscore = getScoreOfAPoint(map, pos, 1);
                        if (tempscore > result[1])
                        {
                            result[1] = tempscore;
                        }
                    }
                }
            }
            return result;
        }

        public static void prtarr(int[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    Console.Write($"{a[i,j]}  ");
                }
                Console.WriteLine();
            }
        }
    }
}
