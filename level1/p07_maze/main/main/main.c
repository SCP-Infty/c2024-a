#include<stdio.h>
#include<stdlib.h>
#include<time.h>
#include<math.h>


int* map = NULL;
int size;

int sgn(int x)
{
	if (x == 0)
	{
		return 0;
	}
	return abs(x) / x;
}

void generatePath(int start[2], int end[2], int comp)
{
	int deltaX, deltaY;
	int nextJoint[2];
	int joint[2] = {start[0], start[1]};
	int mode = rand() % 2;
	int area = comp * 2;
	area = area <= 3 ? 3 : area;

	for (int i = 0; i < size / comp; i++)
	{
		do
		{
			nextJoint[0] = rand() % size;
			nextJoint[1] = rand() % size;
			/*if (nextJoint[0] < 0)
			{
				nextJoint[0] = 0;
			}
			else if (nextJoint[0] >= size)
			{
				nextJoint[0] = size-1;
			}
			if (nextJoint[1] < 0)
			{
				nextJoint[1] = 0;
			}
			else if (nextJoint[1] >= size)
			{
				nextJoint[1] = size-1;
			}*/
		} while (abs(nextJoint[0] - joint[0]) <= 1 || abs(nextJoint[1] - joint[1]) <= 1);
		deltaX = sgn(nextJoint[0] - joint[0]);
		deltaY = sgn(nextJoint[1] - joint[1]);

		switch (mode)
		{
		case 0:
			for (int i = joint[0]; 1; i += deltaX)
			{
				map[joint[1] * size + i] = 0;
				if (i == nextJoint[0])
				{
					break;
				}
			}
			for (int i = joint[1]; 1; i += deltaY)
			{
				map[i * size + nextJoint[0]] = 0;
				if (i == nextJoint[1])
				{
					break;
				}
			}
			break;
		case 1:
			for (int i = joint[1]; 1; i += deltaY)
			{
				map[i * size + joint[0]] = 0;
				if (i == nextJoint[1])
				{
					break;
				}
			}
			for (int i = joint[0]; 1; i += deltaX)
			{
				map[nextJoint[1] * size + i] = 0;
				if (i == nextJoint[0])
				{
					break;
				}
			}
			break;
		}

		joint[0] = nextJoint[0];
		joint[1] = nextJoint[1];
	}

	nextJoint[0] = end[0];
	nextJoint[1] = end[1];

	deltaX = sgn(nextJoint[0] - joint[0]);
	deltaY = sgn(nextJoint[1] - joint[1]);

	switch (mode)
	{
	case 0:
		for (int i = joint[0]; 1; i += deltaX)
		{
			map[joint[1] * size + i] = 0;
			if (i == nextJoint[0])
			{
				break;
			}
		}
		for (int i = joint[1]; 1; i += deltaY)
		{
			map[i * size + nextJoint[0]] = 0;
			if (i == nextJoint[1])
			{
				break;
			}
		}
		break;
	case 1:
		for (int i = joint[1]; 1; i += deltaY)
		{
			map[i * size + joint[0]] = 0;
			if (i == nextJoint[1])
			{
				break;
			}
		}
		for (int i = joint[0]; 1; i += deltaX)
		{
			map[nextJoint[1] * size + i] = 0;
			if (i == nextJoint[0])
			{
				break;
			}
		}
		break;
	}
}

void displayMap() 
{
	for (int i = 0; i < size * size; i++)
	{
		if (i % size == 0 && i != 0)
		{
			printf("\n");
		}
		if (map[i])
		{
			printf("¡ö");
		}
		else
		{
			printf("¡õ");
		}
	}
}

int main()
{
	int pos[] = {0, 0};
	int mode;

	size = 100;
	map = (int*)calloc(size*size, sizeof(int));

	srand((int)time(NULL));

	for (int i = 0; i < size * size; i++)
	{
		map[i] = 1;
	}

	int start[] = {0, 0};
	int end[] = { size - 1, size - 1 };

	generatePath(start, end, 5);

	for (int i = 0; i < size / 10; i++)
	{
		start[0] = rand() % size;
		start[1] = rand() % size;
		end[0] = rand() % size;
		end[1] = rand() % size;

		printf("%d %d %d %d\n", start[0], start[1], end[0], end[1]);

		// generatePath(start, end, 15);
	}

	displayMap();

	while (1)
	{
		
	}

	free(map);
	map = NULL;

	return 0;
}