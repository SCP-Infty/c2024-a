#include<iostream>
#include<cmath>
#include<cstdlib>
#include<chrono>

bool isPrime(int);

int main() {
    auto start = std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::system_clock::now().time_since_epoch()).count();

    for (int i = 2; i <= 1000; i++) {
        if (isPrime(i)) {
            std::cout << i << "\n" ;
        }
    }

    std::cout << "total time: " << std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::system_clock::now().time_since_epoch()).count() - start << "ms\n";
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