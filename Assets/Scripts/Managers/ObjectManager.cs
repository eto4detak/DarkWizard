using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    public List<Transform> objects = new List<Transform>();

    public void AddItem(Transform adding)
    {
        objects.Add(adding);
    }

    public void RemoveItem(Transform removing)
    {
        objects.Remove(removing);
    }



}
