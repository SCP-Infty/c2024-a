#include <stdio.h>
#include <stdlib.h>
#include <windows.h>

int main() {

    int count = 0; // 空格数
    int max = 30; // 边界
    int stop = 50; // ͣ停顿时间
    int rev = 0; // 反转
    while (1) {
        system("cls");

        for (int i = 0; i < count; i++) {
            printf(" ");
        }

        printf("0");

        for (int i = 0; i < max-count; i++) {
            printf(" ");
        }

        printf("|\n");

        Sleep(stop);

        if (rev) {
            count--;
        }
        else {
            count++;
        }

        if (count == max) {
            rev = 1;
        }
        else if(count == 0) {
            rev = 0;
        }

    }
    return 0;
}