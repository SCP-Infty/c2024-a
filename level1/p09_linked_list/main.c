#include<stdio.h>

typedef struct NodeS  // 链表节点
{

    int val;
    struct NodeS* next;

} Node;

typedef struct ChainHeadS  // 表头
{

    Node* next;

} ChainHead;

void prepend(Node*, ChainHead*);
void append(Node*, Node*);
void removeAt(ChainHead*, int);
void reverse(ChainHead*);
Node* next(ChainHead*, int);

void prepend(Node* node, ChainHead* to)  // 将节点添加在链表头部
{
    node->next = to->next;
    to->next = node;
}

void append(Node* node, Node* to)  // 将节点插入另一节点的尾部
{
    node->next = to->next;
    to->next = node;
}

void removeAt(ChainHead* head, int index)
{
    if (index == 0)
    {
        head->next = next(head, 1);
        return;
    }

    Node* front = next(head, index-1);
    front->next = front->next->next;
}

int getLength(ChainHead* head)
{
    int count = 1;
    Node* result = head->next;

    while(result->next != NULL)
    {
        result = result->next;
        count++;
    }

    return count;
}

Node* getTail(ChainHead* head)
{
    Node* result = head->next;

    while(result->next != NULL)
    {
        result = result->next;
    }

    return result;
}

void reverse(ChainHead* head)
{
    if (head->next == NULL)
    {
        return;
    }
    if (head->next->next == NULL)
    {
        return;
    }

    Node* currentNode = head->next;
    Node copy = {currentNode->val, currentNode->next};
    ChainHead rHead = {&copy};

    while (currentNode->next != NULL)
    {
        prepend(&copy, &rHead);
        currentNode = currentNode->next;
        
    }
}

Node* next(ChainHead* head, int step)  // 获取head的第step个节点
{
    Node* result = head->next;

    while (step > 0)
    {
        result = result->next;
        step--;
    }

    return result;
}

int main()
{
    Node testNode = {2, NULL};

    ChainHead h = {&testNode};

    Node n1 = { 123, NULL };
    Node n2 = { 24, NULL };
    Node n3 = { 5, NULL };
    Node n4 = { 888, NULL };

    prepend(&n1, &h);

    append(&n2, next(&h, 0));
    append(&n3, next(&h, 1));
    append(&n4, next(&h, 3));

    for (int i = 0; i < 5; i++)
    {
        printf("%d\n", next(&h, i)->val);
    }

    return 0;
}