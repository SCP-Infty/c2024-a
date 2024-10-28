#include <stdio.h>

void solveHanoi(int layers, int from, int to)
{
    if (layers == 1)
    {
        printf("%d -> %d\n", from, to);
    }
    else
    {
        int newTo = 3 - from - to;
        solveHanoi(layers - 1, from, newTo);
        printf("%d -> %d\n", from, to);
        solveHanoi(layers - 1, newTo, to);
    }
}


int main()
{
    solveHanoi(7, 0, 2);
    return 0;
}