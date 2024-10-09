#include<iostream>
#include<cmath>
#include<list>

bool isPrime(int);

std::list<int> getPrime(int upper) {
	std::list<int> primes;

	for (int i = 2; i <= upper; i++) {
		if (isPrime(i)) {
			primes.push_back(i);
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

int main()
{
	std::list<int> primes = getPrime(100);

	for (int i = 6; i <= 100; i += 2)
	{
		std::cout << i;
		for (auto j = primes.begin(); *j <= i / 2; j++)
		{
			if (isPrime(i - *j))
			{
				std::cout << " = " << *j << "+" << i - *j;
				// break;
			}
		}
		std::cout << "\n";
	}
	system("pause");
	return 0;
}