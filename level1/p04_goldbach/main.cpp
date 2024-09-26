#include<iostream>
#include<cmath>
#include<cstdlib>

bool isPrime(int);

int main() {
    for (int i = 2; i <= 1000; i++) {
        if (isPrime(i)) {
            std::cout << i << "\n" ;
        }
    }
    system("pause");
    return 0;
}

bool isPrime(int num) {

    for (int factor = 2; factor <= sqrt(num); factor++) {

        if (num % factor == 0) {
            return false;
        }

    }

    return true;

}