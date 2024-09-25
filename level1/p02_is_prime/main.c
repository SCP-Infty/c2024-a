#include<stdio.h>
#include<math.h>

int main() {
    unsigned int inputNumber;
    short is_prime;

    while (1) {
        printf("Please input a positive integer: ");
        scanf("%d", &inputNumber);

        if (inputNumber < 3) {
            printf("Invalid input.\n\n");
            continue;
        }

        is_prime = 1;
        for (int i = 2; i <= sqrt(inputNumber); i++) {
            if (inputNumber % i == 0) {
                is_prime = 0;
                break;
            }
        }

        if (is_prime) {
            printf("Yes, it's a prime number!\n\n");
            continue;
        }
        printf("No, it's not a prime number!\n\n");
    }
    return 0;
}