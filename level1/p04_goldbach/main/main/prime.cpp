#include<iostream>
#include<cmath>
#include<list>

bool isPrime(int);

std::list<int> getPrime(int upper) {
    std::list<int> primes;

    for (int i = 2; i <= upper; i++) {
        if (isPrime(i)) {
            primes.push_back(upper);
        }
    }
    return primes;
}

bool isPrime(int num) {

    for (int factor = 2; factor <= sqrt(num); factor++) {

        if (num % factor == 0) {
            return false;
        }

    }

    return true;
}