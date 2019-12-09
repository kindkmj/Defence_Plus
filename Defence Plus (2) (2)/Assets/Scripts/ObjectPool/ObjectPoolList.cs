using System;
using System.Collections.Generic;


public class ObjectPoolList<T>
{
    public List<T> objects = new List<T>();

    private int _count;
    private Func<T> _createFunc;

	public ObjectPoolList (int count, Func<T> createFunc)
	{
	    _count = count;
	    _createFunc = createFunc;
	    CreateObject();
	}

    private void CreateObject()
    {
        for (int i = 0; i < _count; i++)
        {
            objects.Add(_createFunc());
        }
    }

    private void ReturnObject(T obj)
    {
        objects.Add(obj);
    }

    private T GetObject()
    {
        if(objects.Count == 0)
            CreateObject();

        T obj = objects[0];
        objects.Remove(obj);

        return obj;
    }
}
