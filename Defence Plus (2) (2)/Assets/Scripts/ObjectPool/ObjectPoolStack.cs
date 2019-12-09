using System;
using System.Collections.Generic;


public class ObjectPoolStack<T>
{
    public Stack<T> objects = new Stack<T>();

    public Func<T> createFunc;
    public int createAmount;

    public ObjectPoolStack(int createAmount, Func<T> createFunc)
    {
        this.createAmount = createAmount;
        this.createFunc = createFunc;
        CreateObjects();
    }

    private void CreateObjects()
    {
        for (int i = 0; i < createAmount; i++)
        {
            objects.Push(createFunc());
        }
    }

    public void ReturnObject(T obj)
    {
        objects.Push(obj);
    }

    public T GetObject()
    {
        if (objects.Count == 0)
            CreateObjects();

        return objects.Pop();
    }
}
