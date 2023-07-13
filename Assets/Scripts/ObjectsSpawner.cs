using UnityEngine;
using Zenject;

public class ObjectsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnObjectPrefab;
    [SerializeField] private int rowCount;
    [SerializeField] private int columnCount;
    [SerializeField] private float offset;

    private ObjectsFactory objectsFactory;

    [Inject] private GridHolder gridHolder;
    [Inject] private readonly DiContainer mainContainer;

    private void Awake()
    {
        objectsFactory = new ObjectsFactory(mainContainer);
        gridHolder.ConstructGrid(rowCount, columnCount);
    }

    private void Start()
    {
        SpawnObjects();
    }
    private void SpawnObjects()
    {
        for (int i = 0; i < rowCount; i++)
        {
            for (int a = 0; a < columnCount; a++)
            {
                Vector3 objectPos = new Vector3(i * offset, 0f , a * offset);
                
                GameObject ob = objectsFactory.CreateGameObjectFromPrefab(spawnObjectPrefab, objectPos, Quaternion.identity, this.transform);
                ob.GetComponent<GridElement>().SetData(i, a);
                ob.gameObject.name = $"{i}:{a}";
                gridHolder.Grid[i,a] = ob;
            }
        }
    }
}
