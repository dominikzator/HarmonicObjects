using UnityEngine;
using Zenject;

public class ObjectsFactory
{
    private readonly DiContainer diContainer;

    public ObjectsFactory(DiContainer diContainer)
    {
        this.diContainer = diContainer;
    }
    public GameObject CreateGameObjectFromPrefab(Object prefab)
    {
        return diContainer.InstantiatePrefab(prefab);
    }
    public GameObject CreateGameObjectFromPrefab(Object prefab, Transform parent)
    {
        return diContainer.InstantiatePrefab(prefab, parent);
    }

    public GameObject CreateGameObjectFromPrefab(Object prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        return diContainer.InstantiatePrefab(prefab, position, rotation, parent);
    }
}
