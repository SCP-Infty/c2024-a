#include<iostream>
#include<list>

class Item
{
public:
	int count;
	std::string name;
	std::string type;

	Item(std::string name, std::string type, int count)
	{
		this->count = count;
		this->name = name;
		this->type = type;
	}
};

std::list<Item> items;

void addItem()
{
	std::cout << "***�����Ʒ***\n";

	std::string name;
	std::string type;
	int count;

	std::cout << "������Ʒ����: ";
	std::cin >> name;

	std::cout << "\n������Ʒ�ͺ�: ";
	std::cin >> type;

	do
	{
		if (std::cin.fail())
		{
			std::cout << "��������������\n\n";
			std::cin.clear();
		}
		std::cout << "\n������Ʒ����: ";
		std::cin >> count;
	} while (std::cin.fail() && count > 0);

	for (auto it = items.begin(); it != items.end(); it++)
	{
		if (it->name == name && it->type == type)
		{
			it->count += count;
			std::cout << "\n��ӳɹ���\n\n";
			return;
		}
	}

	Item newItem = Item(name, type, count);
	items.push_back(newItem);

	std::cout << "\n��ӳɹ���\n\n";
}

void removeItem()
{
	std::cout << "***�Ƴ���Ʒ***\n";

	std::string name;
	std::string type;
	int count;
	
	std::cout << "������Ʒ����: ";
	std::cin >> name;

	std::cout << "\n������Ʒ�ͺ�: ";
	std::cin >> type;

	auto it = items.begin();

	bool exist = false;

	while (it != items.end())
	{
		if (it->name == name && it->type == type)
		{
			exist = true;
			break;
		}
		it++;
	}

	if (!exist)
	{
		std::cout << "δ�ҵ���Ʒ\n\n";
		return;
	}

	do
	{
		if (std::cin.fail())
		{
			std::cout << "��������������\n\n";
			std::cin.clear();
		}
		std::cout << "\n������Ʒ����: ";
		std::cin >> count;
		if (count > it->count)
		{
			std::cout << "�Ƴ�����Ʒ��������������";
		}
	} while (std::cin.fail() && count > 0);

	it->count -= count;
}

void saveAndQuit()
{

}

int main()
{
	std::cout << "��ӭʹ�òֿ�ϵͳ��\n������\n    0 - �鿴����\n    1 - �鿴���\n    2 - �����Ʒ\n    3 - �Ƴ���Ʒ\n    4 - �����˳�\n\n";

	int option;

	while (true)
	{
		std::cout << "-->  ";
		std::cin >> option;

		switch (option)
		{
		case 0:
			std::cout << "������\n    0 - �鿴����\n    1 - �鿴���\n    2 - �����Ʒ\n    3 - �Ƴ���Ʒ\n    4 - �����˳�\n\n";
			break;
		case 1:
			for (auto it = items.begin(); it != items.end(); it++)
			{
				std::cout << it->name << "    " << it->type << "    " << it->count << "\n";
			}
			std::cout << "\n";
			break;
		case 2:
			addItem();
			break;
		case 3:
			removeItem();
			break;
		case 4:
			saveAndQuit();
			break;
		}
	}
}