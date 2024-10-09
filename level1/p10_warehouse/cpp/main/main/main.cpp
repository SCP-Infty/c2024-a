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
	std::cout << "***添加物品***\n";

	std::string name;
	std::string type;
	int count;

	std::cout << "输入物品名称: ";
	std::cin >> name;

	std::cout << "\n输入物品型号: ";
	std::cin >> type;

	do
	{
		if (std::cin.fail())
		{
			std::cout << "请输入正整数！\n\n";
			std::cin.clear();
		}
		std::cout << "\n输入物品数量: ";
		std::cin >> count;
	} while (std::cin.fail() && count > 0);

	for (auto it = items.begin(); it != items.end(); it++)
	{
		if (it->name == name && it->type == type)
		{
			it->count += count;
			std::cout << "\n添加成功。\n\n";
			return;
		}
	}

	Item newItem = Item(name, type, count);
	items.push_back(newItem);

	std::cout << "\n添加成功。\n\n";
}

void removeItem()
{
	std::cout << "***移除物品***\n";

	std::string name;
	std::string type;
	int count;
	
	std::cout << "输入物品名称: ";
	std::cin >> name;

	std::cout << "\n输入物品型号: ";
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
		std::cout << "未找到物品\n\n";
		return;
	}

	do
	{
		if (std::cin.fail())
		{
			std::cout << "请输入正整数！\n\n";
			std::cin.clear();
		}
		std::cout << "\n输入物品数量: ";
		std::cin >> count;
		if (count > it->count)
		{
			std::cout << "移除的物品数量多余库存数量";
		}
	} while (std::cin.fail() && count > 0);

	it->count -= count;
}

void saveAndQuit()
{

}

int main()
{
	std::cout << "欢迎使用仓库系统！\n帮助：\n    0 - 查看帮助\n    1 - 查看库存\n    2 - 添加物品\n    3 - 移除物品\n    4 - 保存退出\n\n";

	int option;

	while (true)
	{
		std::cout << "-->  ";
		std::cin >> option;

		switch (option)
		{
		case 0:
			std::cout << "帮助：\n    0 - 查看帮助\n    1 - 查看库存\n    2 - 添加物品\n    3 - 移除物品\n    4 - 保存退出\n\n";
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