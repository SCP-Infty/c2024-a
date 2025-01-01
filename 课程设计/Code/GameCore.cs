using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;



namespace Core
{
    public partial class GomokuGame
    {
        [DllImport("cppCore.dll", CallingConvention = CallingConvention.Cdecl)]
        extern static IntPtr getSubScore1(ref Int32 map, int size = 15);
        [DllImport("cppCore.dll", CallingConvention = CallingConvention.Cdecl)]
        extern static IntPtr getSubScore2(ref Int32 map, int size = 15);

        public void ClearMap()
        {
            map = new int[15, 15];
            newGame = true;
        }

        public void PlacePawn(int x, int y)
        {
            if (blackTurn)
            {
                map[x, y] = 1;  // 黑棋
                blackTurn = false;
            }
            else
            {
                map[x, y] = -1;  // 白棋
                blackTurn = true;
            }
        }

        public int[,] GetMap()
        {
            return map;
        }

        public bool GetPlayerTurn()
        {
            return blackTurn != aiIsBlack;
        }

        public bool GetPlayerColor()
        {
            return !aiIsBlack;
        }

        public int[] AIPlacePawn()
        {
            //int[] bc = BestChoice3(map,false);
            int[] bc = ABTest(3, map);
            PlacePawn(bc[0], bc[1]);
            return new int[] { bc[0], bc[1] };
        }

        public int Judge()
        {
            int[] s = GetScore(map);
            if (s[0]==2147483647)
            {
                return -1;
            }
            else if (s[1]==2147483647)
            {
                return 1;
            }
            return 0;
        }

        private int[] GetScore(int[,] map)
        {
            int[] result = new int[2] { 0, 0 };

            bool[] inf = {false, false};

            List<int[]> strikes = new List<int[]>();

            int GetPawn(int x, int y)
            {
                if(x >= 0 && x < 15 && y >= 0 && y < 15)
                {
                    return map[x, y];
                }

                else
                {
                    return 2;
                }
            }

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 0) 
                    {
                        if (GetPawn(i-1, j)!=0 && GetPawn(i - 1, j) != 2 && GetPawn(i - 1, j) == GetPawn(i + 1, j))
                        {
                            int[] sp = { i-1, j };
                            int[] ep = { i+1, j };
                            int len = 3;
                            while (GetPawn(sp[0]-1, sp[1]) == GetPawn(i-1, j))
                            {
                                sp[0] = sp[0] - 1;
                                len++;
                            }
                            while (GetPawn(ep[0] + 1, ep[1]) == GetPawn(i - 1, j))
                            {
                                ep[0] = ep[0] + 1;
                                len++;
                            }
                            bool ray = false;
                            bool death = false;
                            if (GetPawn(sp[0] - 1, sp[1])!=0||GetPawn(ep[0] + 1, ep[1])!=0)
                            {
                                ray = true;
                            }
                            if (GetPawn(sp[0] - 1, sp[1]) != 0 && GetPawn(ep[0] + 1, ep[1]) != 0)
                            {
                                death = true;
                            }
                            if (!death)
                            {
                                switch (len)
                                {
                                    case 3:
                                        result[(GetPawn(i - 1, j) + 1) / 2] += ray ? 5:30;
                                        break;
                                    case 4:
                                        result[(GetPawn(i - 1, j) + 1) / 2] += ray?500:1000;
                                        break;
                                    case 5:
                                        result[(GetPawn(i - 1, j) + 1) / 2] += 30000;
                                        break;
                                }
                            }
                        }
                        if (GetPawn(i, j-1) != 0 && GetPawn(i, j - 1) != 2 && GetPawn(i, j-1) == GetPawn(i, j+1))
                        {
                            int[] sp = { i, j-1 };
                            int[] ep = { i, j+1 };
                            int len = 3;
                            while (GetPawn(sp[0], sp[1]-1) == GetPawn(i, j-1))
                            {
                                sp[1] = sp[1] - 1;
                                len++;
                            }
                            while (GetPawn(ep[0], ep[1]+1) == GetPawn(i, j-1))
                            {
                                ep[1] = ep[1] + 1;
                                len++;
                            }
                            bool ray = false;
                            bool death = false;
                            if (GetPawn(sp[0], sp[1]-1) != 0 || GetPawn(ep[0], ep[1]+1) != 0)
                            {
                                ray = true;
                            }
                            if (GetPawn(sp[0], sp[1] - 1) != 0 && GetPawn(ep[0], ep[1] + 1) != 0)
                            {
                                death = true;
                            }
                            if (!death)
                            {
                                switch (len)
                                {
                                    case 3:
                                        result[(GetPawn(i, j - 1) + 1) / 2] += ray ? 5 : 30;
                                        break;
                                    case 4:
                                        result[(GetPawn(i, j - 1) + 1) / 2] += ray ? 500 : 1000;
                                        break;
                                    case 5:
                                        result[(GetPawn(i - 1, j) + 1) / 2] += 30000;
                                        break;
                                }
                            }
                        }
                        if (GetPawn(i - 1, j-1) != 0 && GetPawn(i - 1, j - 1) != 2 && GetPawn(i - 1, j-1) == GetPawn(i + 1, j+1))
                        {
                            int[] sp = { i - 1, j-1 };
                            int[] ep = { i + 1, j+1 };
                            int len = 3;
                            while (GetPawn(sp[0] - 1, sp[1]-1) == GetPawn(i - 1, j-1))
                            {
                                sp[0] = sp[0] - 1;
                                sp[1] = sp[1] - 1;
                                len++;
                            }
                            while (GetPawn(ep[0] + 1, ep[1]+1) == GetPawn(i - 1, j-1))
                            {
                                ep[0] = ep[0] + 1;
                                ep[1] = ep[1] + 1;
                                len++;
                            }
                            bool ray = false;
                            bool death = false;
                            if (GetPawn(sp[0] - 1, sp[1]-1) != 0 || GetPawn(ep[0] + 1, ep[1]+1) != 0)
                            {
                                ray = true;
                            }
                            if (GetPawn(sp[0] - 1, sp[1]-1) != 0 && GetPawn(ep[0] + 1, ep[1]+1) != 0)
                            {
                                death = true;
                            }
                            if (!death)
                            {
                                switch (len)
                                {
                                    case 3:
                                        result[(GetPawn(i - 1, j - 1) + 1) / 2] += ray ? 5 : 30;
                                        break;
                                    case 4:
                                        result[(GetPawn(i - 1, j - 1) + 1) / 2] += ray ? 500 : 1000;
                                        break;
                                    case 5:
                                        result[(GetPawn(i - 1, j) + 1) / 2] += 30000;
                                        break;
                                }
                            }
                        }
                        if (GetPawn(i - 1, j + 1) != 0 && GetPawn(i - 1, j + 1) != 2 && GetPawn(i - 1, j + 1) == GetPawn(i + 1, j - 1))
                        {
                            int[] sp = { i - 1, j + 1 };
                            int[] ep = { i + 1, j - 1 };
                            int len = 3;
                            while (GetPawn(sp[0] - 1, sp[1] + 1) == GetPawn(i - 1, j + 1))
                            {
                                sp[0] = sp[0] - 1;
                                sp[1] = sp[1] + 1;
                                len++;
                            }
                            while (GetPawn(ep[0] + 1, ep[1] - 1) == GetPawn(i - 1, j + 1))
                            {
                                ep[0] = ep[0] + 1;
                                ep[1] = ep[1] - 1;
                                len++;
                            }
                            bool ray = false;
                            bool death = false;
                            if (GetPawn(sp[0] - 1, sp[1] + 1) != 0 || GetPawn(ep[0] + 1, ep[1] - 1) != 0)
                            {
                                ray = true;
                            }
                            if (GetPawn(sp[0] - 1, sp[1] + 1) != 0 && GetPawn(ep[0] + 1, ep[1] - 1) != 0)
                            {
                                death = true;
                            }
                            if (!death)
                            {
                                switch (len)
                                {
                                    case 3:
                                        result[(GetPawn(i - 1, j + 1) + 1) / 2] += ray ? 5 : 30;
                                        break;
                                    case 4:
                                        result[(GetPawn(i - 1, j + 1) + 1) / 2] += ray ? 500 : 1000;
                                        break;
                                    case 5:
                                        result[(GetPawn(i - 1, j) + 1) / 2] += 30000;
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int mode = 0; mode < 4; mode++)
                        {
                            int[] startPoint = new int[2] { i, j };
                            int[] endPoint = new int[2] { i, j };
                            int length = 1;
                            bool ray = false;

                            switch (mode)
                            {
                                case 0: //horizontal / axis x
                                    // 对于一个点（i，j），令startPoint和endPoint均等于（i，j）
                                    // 如果startPoint处的棋子等于（i，j）处的棋子，就向x轴负方向移动
                                    while (GetPawn(startPoint[0] - 1, startPoint[1]) == map[i, j])  
                                    {
                                        startPoint[0]--;
                                        length++;
                                    }
                                    // 如果endPoint处的棋子等于（i，j）处的棋子，就向x轴正方向移动
                                    while (GetPawn(endPoint[0] + 1, endPoint[1]) == map[i, j])
                                    {
                                        endPoint[0]++;
                                        length++;
                                    }
                                    // 这样就确定了（i，j）处的棋子连起来多少个了

                                    if (strikes.Contains(new int[4] { startPoint[0], startPoint[1], endPoint[0], endPoint[1] }))
                                    {
                                        break;
                                    }

                                    if (length > 1)
                                    {
                                        if (length >= 5)  // 连五个，分数设置为正无穷，即胜利
                                        {
                                            inf[(map[i, j] + 1) / 2] = true; 
                                            break;
                                        }
                                        if (GetPawn(startPoint[0] - 1, startPoint[1]) != 0 && GetPawn(endPoint[0] + 1, endPoint[1]) != 0) // 两头堵死
                                        {
                                            break;
                                        }
                                        else if (GetPawn(startPoint[0] - 1, startPoint[1]) != 0 || GetPawn(endPoint[0] + 1, endPoint[1]) != 0) // 一头堵死，ray设为true
                                        {
                                            ray = true;
                                        }

                                        switch (length)
                                        {
                                            case 2:
                                                result[(map[i, j] + 1) / 2] += ray ? 10 : 50;
                                                break;
                                            case 3:
                                                result[(map[i, j] + 1) / 2] += ray ? 300 : 1000;
                                                break;
                                            case 4:
                                                result[(map[i, j] + 1) / 2] += ray ? 10000 : 100000;
                                                break;
                                            default:
                                                break;
                                        }

                                        strikes.Add(new int[4] { startPoint[0], startPoint[1], endPoint[0], endPoint[1] });
                                    }
                                    break;

                                case 1: //vertical / axis j
                                    while (GetPawn(startPoint[0], startPoint[1] - 1) == map[i, j])
                                    {
                                        startPoint[1]--;
                                        length++;
                                    }
                                    while (GetPawn(endPoint[0], endPoint[1] + 1) == map[i, j])
                                    {
                                        endPoint[1]++;
                                        length++;
                                    }

                                    if (strikes.Contains(new int[4] { startPoint[0], startPoint[1], endPoint[0], endPoint[1] }))
                                    {
                                        break;
                                    }

                                    if (length > 1)
                                    {
                                        if (length >= 5)
                                        {
                                            inf[(map[i, j] + 1) / 2] = true;
                                            break;
                                        }
                                        if (GetPawn(startPoint[0], startPoint[1] - 1) != 0 && GetPawn(endPoint[0], endPoint[1] + 1) != 0)
                                        {
                                            break;
                                        }
                                        else if (GetPawn(startPoint[0], startPoint[1] - 1) != 0 || GetPawn(endPoint[0], endPoint[1] + 1) != 0)
                                        {
                                            ray = true;
                                        }

                                        switch (length)
                                        {
                                            case 2:
                                                result[(map[i, j] + 1) / 2] += ray ? 10 : 50;
                                                break;
                                            case 3:
                                                result[(map[i, j] + 1) / 2] += ray ? 300 : 1000;
                                                break;
                                            case 4:
                                                result[(map[i, j] + 1) / 2] += ray ? 10000 : 100000;
                                                break;
                                            default:
                                                break;
                                        }

                                        strikes.Add(new int[4] { startPoint[0], startPoint[1], endPoint[0], endPoint[1] });
                                    }
                                    break;

                                case 2: //+i+j
                                    while (GetPawn(startPoint[0] - 1, startPoint[1] - 1) == map[i, j])
                                    {
                                        startPoint[0]--;
                                        startPoint[1]--;
                                        length++;
                                    }
                                    while (GetPawn(endPoint[0] + 1, endPoint[1] + 1) == map[i, j])
                                    {
                                        endPoint[1]++;
                                        endPoint[0]++;
                                        length++;
                                    }

                                    if (strikes.Contains(new int[4] { startPoint[0], startPoint[1], endPoint[0], endPoint[1] }))
                                    {
                                        break;
                                    }

                                    if (length > 1)
                                    {
                                        if (length >= 5)
                                        {
                                            inf[(map[i, j] + 1) / 2] = true;
                                            break;
                                        }
                                        if (GetPawn(startPoint[0] - 1, startPoint[1] - 1) != 0 && GetPawn(endPoint[0] + 1, endPoint[1] + 1) != 0)
                                        {
                                            break;
                                        }
                                        else if (GetPawn(startPoint[0] - 1, startPoint[1] - 1) != 0 || GetPawn(endPoint[0] + 1, endPoint[1] + 1) != 0)
                                        {
                                            ray = true;
                                        }

                                        switch (length)
                                        {
                                            case 2:
                                                result[(map[i, j] + 1) / 2] += ray ? 10 : 50;
                                                break;
                                            case 3:
                                                result[(map[i, j] + 1) / 2] += ray ? 300 : 1000;
                                                break;
                                            case 4:
                                                result[(map[i, j] + 1) / 2] += ray ? 10000 : 100000;
                                                break;
                                            default:
                                                break;
                                        }

                                        strikes.Add(new int[4] { startPoint[0], startPoint[1], endPoint[0], endPoint[1] });
                                    }
                                    break;

                                case 3: //+i-j
                                    while (GetPawn(startPoint[0] - 1, startPoint[1] + 1) == map[i, j])
                                    {
                                        startPoint[0]--;
                                        startPoint[1]++;
                                        length++;
                                    }
                                    while (GetPawn(endPoint[0] + 1, endPoint[1] - 1) == map[i, j])
                                    {
                                        endPoint[0]++;
                                        endPoint[1]--;
                                        length++;
                                    }

                                    if (strikes.Contains(new int[4] { startPoint[0], startPoint[1], endPoint[0], endPoint[1] }))
                                    {
                                        break;
                                    }

                                    if (length > 1)
                                    {
                                        if (length >= 5)
                                        {
                                            inf[(map[i, j] + 1) / 2] = true;
                                            break;
                                        }
                                        if (GetPawn(startPoint[0] - 1, startPoint[1] + 1) != 0 && GetPawn(endPoint[0] + 1, endPoint[1] - 1) != 0)
                                        {
                                            break;
                                        }
                                        else if (GetPawn(startPoint[0] - 1, startPoint[1] + 1) != 0 || GetPawn(endPoint[0] + 1, endPoint[1] - 1) != 0)
                                        {
                                            ray = true;
                                        }

                                        switch (length)
                                        {
                                            case 2:
                                                result[(map[i, j] + 1) / 2] += ray ? 10 : 50;
                                                break;
                                            case 3:
                                                result[(map[i, j] + 1) / 2] += ray ? 300 : 1000;
                                                break;
                                            case 4:
                                                result[(map[i, j] + 1) / 2] += ray ? 10000 : 100000;
                                                break;
                                            default:
                                                break;
                                        }

                                        strikes.Add(new int[4] { startPoint[0], startPoint[1], endPoint[0], endPoint[1] });
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            //int[] score2 = { 0, 0 };

            //unsafe
            //{
            //    int* score2_ = (int*)getSubScore2(ref map[0, 0]);
            //    score2[0] = score2_[0];
            //    score2[1] = score2_[1];
            //    Console.WriteLine($"{result[0]}, {score2[0]}");
            //}

            int[] score2 = GomokuGame.getSubScore2(map);

            result[0] += score2[0] / 5;

            for (int i = 0; i<2; i++)
            {
                if (inf[i])
                {
                    result[i] = 2147483647;
                }
            }

            return result;
        }

        private int[] BestChoice3(int[,] map, bool black=true)
        {
            int[] result = new int[2];
            List<int[]> results = new List<int[]>();
            int[] topLeft = new int[] { -1, -1 };
            int[] bottomRight = new int[] { -1, -1 };
            if (newGame)
            {
                newGame = false;
                result = new int[2] { 7, 7 };
                return result;
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (map[i, j] != 0)
                        {
                            if (topLeft[0] == -1)
                            {
                                topLeft[0] = i - 3 >= 0 ? i - 3 : 0;
                                topLeft[1] = j - 3 >= 0 ? j - 3 : 0;
                                bottomRight[0] = i + 3 <= 14 ? i + 3 : 14;
                                bottomRight[1] = j + 3 <= 14 ? j + 3 : 14;
                            }
                            if ((i - 3 >= 0 ? i - 3 : 0) < topLeft[0])
                            {
                                topLeft[0] = i - 3 >= 0 ? i - 3 : 0;
                            }
                            if ((j - 3 >= 0 ? j - 3 : 0) < topLeft[1])
                            {
                                topLeft[1] = j - 3 >= 0 ? j - 3 : 0;
                            }
                            if ((i + 3 <= 14 ? i + 3 : 14) > bottomRight[0])
                            {
                                bottomRight[0] = i + 3 <= 14 ? i + 3 : 14;
                            }
                            if ((j + 3 <= 14 ? j + 3 : 14) > bottomRight[1])
                            {
                                bottomRight[1] = j + 3 <= 14 ? j + 3 : 14;
                            }
                        }
                    }
                }
                for (int i = topLeft[0]; i <= bottomRight[0]; i++)
                {
                    for (int j = topLeft[1]; j <= bottomRight[1]; j++)
                    {
                        if (map[i, j] == 0)
                        {
                            result = new int[2] { i, j };
                        }
                    }
                }
            }
            int[] tempScore = new int[2];
            int[] newscoreDefend = new int[2] { black ? 2147483647 : 0, black ? 0 : 2147483647 };
            int[] newscoreAttack = new int[2] { black ? 2147483647 : 0, black ? 0 : 2147483647 };
            int[,] mapAssumedCopied = new int[15, 15];
            List<int[]> resultsDefend = new List<int[]>();
            List<int[]> resultsAttack = new List<int[]>();
            for (int i = topLeft[0]; i <= bottomRight[0]; i++)
            {
                for (int j = topLeft[1]; j <= bottomRight[1]; j++)
                {
                    if (map[i, j] != 0)
                    {
                        continue;
                    }
                    Array.Copy(map, mapAssumedCopied, map.Length);
                    mapAssumedCopied[i, j] = black ? 1 : -1;
                    tempScore = GetScore(mapAssumedCopied);
                    //Console.WriteLine("O: {0}, {1}, {2}, {3}", i, j, tempScore[0], tempScore[1]);

                    if (tempScore[black ? 1 : 0] > newscoreAttack[black ? 1 : 0])  // AI分数有所增加
                    {
                        if (tempScore[black ? 1 : 0] == 2147483647)  // 获胜，直接返回获胜的走法
                        {
                            return new int[] { i, j, tempScore[1] - tempScore[0] };
                        }
                        newscoreAttack[0] = tempScore[0];
                        newscoreAttack[1] = tempScore[1];
                        resultsAttack.Clear();
                        resultsAttack.Add(new int[] { i, j, tempScore[1] - tempScore[0] });
                    }
                    else if (tempScore[black ? 1 : 0] == newscoreAttack[black ? 1 : 0])  // AI分数与搜索到的最高的AI分数相等
                    {
                        if (tempScore[black ? 0 : 1] < newscoreDefend[black ? 0 : 1])  // 如果AI分数与之前的相等，而且玩家的分数更小，那么这是更有效的攻击
                        {
                            newscoreAttack[0] = tempScore[0];
                            newscoreAttack[1] = tempScore[1];
                            resultsAttack.Clear();
                            resultsAttack.Add(new int[] { i, j, tempScore[1] - tempScore[0] });

                        }
                        else  // 跟最高AI分数相等，但防守效果没有进步，直接将这个走法加入列表。
                        {
                            resultsAttack.Add(new int[] { i, j, tempScore[1] - tempScore[0] });
                        }
                    }
                    if (tempScore[black ? 0 : 1] < newscoreDefend[black ? 0 : 1])
                    {
                        //Console.WriteLine("Defend: {0}, {1}, {2}, {3}", i, j, tempScore[0], tempScore[1]);
                        newscoreDefend[0] = tempScore[0];
                        newscoreDefend[1] = tempScore[1];
                        resultsDefend.Clear();
                        resultsDefend.Add(new int[] { i, j, tempScore[1] - tempScore[0] });
                    }
                    else if (tempScore[black ? 0 : 1] == newscoreDefend[black ? 0 : 1])
                    {
                        if (tempScore[black ? 1 : 0] > newscoreAttack[black ? 1 : 0])
                        {
                            //Console.WriteLine("Defend: {0}, {1}, {2}, {3}", i, j, tempScore[0], tempScore[1]);
                            newscoreDefend[0] = tempScore[0];
                            newscoreDefend[1] = tempScore[1];
                            resultsDefend.Clear();
                            resultsDefend.Add(new int[] { i, j, tempScore[1] - tempScore[0] });
                        }
                        else
                        {
                            resultsDefend.Add(new int[] { i, j, tempScore[1] - tempScore[0] });
                        }
                    }
                }
            }

            if (newscoreAttack[black ? 1 : 0] == 2147483647)
            {
                //Console.WriteLine("Victory");
                return resultsAttack[new Random().Next(resultsAttack.Count)];
            }

            // "对方有四连珠" 或 "对方有活三，且我方不能做四连珠" 的情况，应当防守。
            if (newscoreAttack[black ? 0 : 1] >= 20000 || (newscoreAttack[black ? 0 : 1] >= 1000 && newscoreAttack[black?1:0] < 200000))
            {
                //Console.WriteLine("Defend");
                 return resultsDefend[new Random().Next(resultsDefend.Count)];
            }

            // 分别计算进攻与防守走法的分数差值
            int ascore = newscoreAttack[1] - newscoreAttack[0], dscore = newscoreDefend[1]-newscoreDefend[0];
            if(!black)
            {
                ascore = -ascore;
                dscore = -dscore;
            }
            //Console.WriteLine($"{ascore}, {dscore}");

            // 进攻优势更大，选择进攻走法
            if ( ascore > dscore )
            {
                //Console.WriteLine("Attack");
                return resultsAttack[new Random().Next(resultsAttack.Count)];
            }
            else
            {
                //Console.WriteLine("Defend");
                return resultsDefend[new Random().Next(resultsDefend.Count)];
            }
        }


        private int[] ABTest(int depth, int[,] map, int a = -2147483648, int b = 2147483647, bool max = true, int tl0=-1, int tl1=-1, int br0=-1, int br1=-1)
        {
            
            if (newGame)
            {
                newGame = false;
                return new int[2] { 7, 7 };
            }
            if (depth == 1)
            {
                int[] bc = BestChoice3(map, max);
                //Console.WriteLine($"{depth}, {bc[0]}, {bc[1]}, {bc[2]}");
                return bc;
            }
            else
            {
                Console.WriteLine($": {depth + 1}->{depth}, {a}, {b}, {max}");
               
                int[] topLeft = new int[] { tl0, tl1 };
                int[] bottomRight = new int[] { br0, br1 };
                int[,] mapAC = new int[15, 15];
                List<int[]> results = new List<int[]>();
                //int score;
                //int[] temp;
                //int[] result = { 0, 0 };
                if (tl0 == -1)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        for (int j = 0; j < 15; j++)
                        {
                            if (map[i, j] != 0)
                            {
                                if (topLeft[0] == -1)
                                {
                                    topLeft[0] = i - 3 >= 0 ? i - 3 : 0;
                                    topLeft[1] = j - 3 >= 0 ? j - 3 : 0;
                                    bottomRight[0] = i + 3 <= 14 ? i + 3 : 14;
                                    bottomRight[1] = j + 3 <= 14 ? j + 3 : 14;
                                }
                                if ((i - 3 >= 0 ? i - 3 : 0) < topLeft[0])
                                {
                                    topLeft[0] = i - 3 >= 0 ? i - 3 : 0;
                                }
                                if ((j - 3 >= 0 ? j - 3 : 0) < topLeft[1])
                                {
                                    topLeft[1] = j - 3 >= 0 ? j - 3 : 0;
                                }
                                if ((i + 3 <= 14 ? i + 3 : 14) > bottomRight[0])
                                {
                                    bottomRight[0] = i + 3 <= 14 ? i + 3 : 14;
                                }
                                if ((j + 3 <= 14 ? j + 3 : 14) > bottomRight[1])
                                {
                                    bottomRight[1] = j + 3 <= 14 ? j + 3 : 14;
                                }
                            }
                        }
                    }
                }
                int score;
                int[] temp;


                //Console.WriteLine($"{depth}, {bc[0]}, {bc[1]}, {bc[2]}");
                int[] bc = BestChoice3(map, max);
                int[] result = { bc[0], bc[1] };



                //estimate
                #region estimate
                Array.Copy(map, mapAC, map.Length);
                mapAC[result[0], result[1]] = max ? 1 : -1;
                temp = ABTest(depth - 1, mapAC, a, b, !max, topLeft[0], topLeft[1], bottomRight[0], bottomRight[1]);
                score = temp[2];

                if (max)
                {

                    if (score > a)
                    {
                        a = score;
                        results.Clear();
                        results.Add(new int[] { result[0], result[1], score });
                    }
                    else if (score == a)
                    {
                        results.Add(new int[] { result[0], result[1], score });
                    }
                    if (a >= b)
                    {
                        return results[new Random().Next(results.Count)];
                    }
                }
                else
                {
                    if (score < b)
                    {
                        b = score;
                        results.Clear();
                        results.Add(new int[] { result[0], result[1], score });
                    }
                    else if (score == b)
                    {
                        results.Add(new int[] { result[0], result[1], score });
                    }
                    if (a >= b)
                    {
                        return results[new Random().Next(results.Count)];
                    }
                }
                if (depth == 2 && results.Count > 0)
                {
                    return results[0];
                }
                else if (depth == 2)
                {
                    return new int[] { -1, -1, max ? -2147483648 : 2147483647 };
                }
                #endregion
                Console.WriteLine(depth);

                for (int i = topLeft[0]; i <= bottomRight[0]; i++)
                {
                    bool cut = false;
                    Parallel.For(topLeft[1], bottomRight[1], (j, pstate) =>
                    {
                        if (map[i, j] == 0)
                        {
                            Array.Copy(map, mapAC, map.Length);
                            mapAC[i, j] = max ? 1 : -1;
                            temp = ABTest(depth - 1, mapAC, a, b, !max);
                            score = temp[2];

                            if (max)
                            {
                                if (score > a)
                                {
                                    a = score;
                                    results.Clear();
                                    results.Add(new int[] { i, j, score });
                                }
                                else if (score == a)
                                {
                                    results.Add(new int[] { i, j, score });
                                }
                                if (a >= b)
                                {
                                    cut = true;
                                    pstate.Stop();
                                }
                            }
                            else
                            {
                                if (score < b)
                                {
                                    b = score;
                                    results.Clear();
                                    results.Add(new int[] { i, j, score });
                                }
                                else if (score == b)
                                {
                                    results.Add(new int[] { i, j, score });
                                }
                                if (a >= b)
                                {
                                    cut = true;
                                    pstate.Stop();
                                }
                            }
                        }
                    });
                    if(cut)
                    {
                        return results[new Random().Next(results.Count)];
                    }
                }
                if (results.Count>0)
                {
                    return results[new Random().Next(results.Count)];
                }
                else
                {
                    return new int[] { -1, -1, max ? -2147483648 : 2147483647 };
                }
            }
        }

        //private int[] ABTest2(int depth, int[,] map, int a = -2147483648, int b = 2147483647, bool max = true)
        //{
        //    if (newGame)
        //    {
        //        newGame = false;
        //        return new int[2] { 7, 7 };
        //    }
        //    if (depth == 0)
        //    {
        //        return BestChoice3(map, false);
        //    }
        //    else
        //    {

        //    }
        //}

        private int[,] map = new int[15,15];
        private bool blackTurn = true;
        private bool aiIsBlack = true;  // AI是黑色
        private bool newGame = true;
    }
}
