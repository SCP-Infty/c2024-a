#include<cstdlib>
#include<iostream>
#include<cmath>
#include<string>


std::string encrypt(std::string text)
{
	srand((unsigned)time(NULL));

	std::string cipher;

	int digits = 0;
	int p = 0;
	int key;
	int finish = false;
	int start, end;
	char head, tail;

	while (!finish)
	{
		key = rand() % 6 + 1;
		start = digits;
		end = digits + key - 1;
		if (start >= 8*text.size())
		{
			break;
		}
		if (end >= 8*text.size())
		{
			finish = true;
			end = text.size() * 8 - 1;
			key = end - start + 1;
		}
		cipher.push_back(key+32);
		if (start / 8 == end / 8)
		{
			cipher.push_back(((text[start / 8] & (255 >> start % 8)) >> ( 7 - end % 8)) + 39);
		}
		else
		{
			head = (text[start / 8] & (255 >> start % 8)) << end % 8 + 1;
			tail = text[end / 8] >> (8 - end % 8 - 1);
			cipher.push_back(head + tail + 39);
		}
		digits = end+1;
	}
	
	return cipher;
}

std::string decrypt(std::string cipher)
{
	std::string text;
	int digits = 0;
	char c = 0;

	for (int i = 0; i < cipher.size() - 1; i += 2)
		{
		if (digits + cipher[i] - 32 >= 8) 
		{
			c += (cipher[i+1] - 39) >> (digits + cipher[i] - 40);
			text.push_back(c);
			digits += cipher[i] - 40;
			c = (cipher[i + 1] - 39) & (255 >> (8 - digits));
			c <<= 8 - digits;
		}
		else
		{
			c += (cipher[i+1] - 39) << (40 - digits - cipher[i]);
			digits += cipher[i] - 32;
		}
	}
	
	return text;
}

int main()
{
	std::string text = "Top secret: 1 + 1 = 2.";
	std::string cipher = encrypt(text);
	std::cout << "明文：" << text << "\n\n";
	std::cout << "密文：" << cipher << "\n\n";
	std::cout << "解密：" << decrypt(cipher) << "\n";
}
