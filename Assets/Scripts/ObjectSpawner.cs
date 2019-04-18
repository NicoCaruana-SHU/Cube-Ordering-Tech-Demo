using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    [SerializeField]
    private int numberOfObjectsToSpawn_ = 25;
    [SerializeField]
    private GameObject objectToSpawn_;

    [SerializeField]
    private float minHorizontalPosition_ = -5;
    [SerializeField]
    private float maxHorizontalPosition_ = 5;
    [SerializeField]
    private float minVerticalPosition_ = -5;
    [SerializeField]
    private float maxVerticalPosition_ = 5;
    [SerializeField]
    private float minDepthPosition_ = 0;
    [SerializeField]
    private float maxDepthPosition_ = 0;

    private List<GameObject> objects_;

    void Start()
    {
        objects_ = new List<GameObject>();
    }

    /// <summary>
    /// Create a specified amount of prefab objects within a defined range in the game world.
    /// <para>If the prefab has a ColourGenerator component, sort the collection by colour.</para>
    /// <para>If the prefab has an ObjectMover component, set its destination position after ordering.</para>
    /// </summary>
    public void GenerateObjects()
    {
        if (objects_.Count != 0) // If any gameobjects still exist in the list, destroy them before creating more.
        {
            DestroyObjects();
        }
        for (int i = numberOfObjectsToSpawn_; i > 0; --i)
        {
            objects_.Add(Instantiate(objectToSpawn_, new Vector3(Random.Range(minHorizontalPosition_, maxHorizontalPosition_), Random.Range(minVerticalPosition_, maxVerticalPosition_), Random.Range(minDepthPosition_, maxDepthPosition_)), Quaternion.identity));
        }
        if (objectToSpawn_.GetComponent<ColourGenerator>() != null)
        {
            objects_.Sort(SortByColour);
        }
        if (objectToSpawn_.GetComponent<ObjectMover>() != null)
        {
            SetDestinations();
        }
    }

    /// <summary>
    /// Start the objects moving to their sorted positions if the prefab has an ObjectMover component.
    /// </summary>
    public void OrderObjects()
    {
        if (objectToSpawn_.GetComponent<ObjectMover>() != null)
        {
            foreach (var item in objects_)
            {
                item.GetComponent<ObjectMover>().StartMoving();
            }
        }
    }

    /// <summary>
    /// Destroy all gameobjects controlled by this ObjectSpawner and clear the container.
    /// </summary>
    private void DestroyObjects()
    {
        foreach (var item in objects_)
        {
            Destroy(item);
        }
        objects_.Clear();
    }

    /// <summary>
    /// Assign world space row positions to the objects in the list.
    /// </summary>
    private void SetDestinations()
    {
        float spacing = CalculateSpacing();
        for (int i = 0; i < objects_.Count; ++i)
        {
            objects_[i].GetComponent<ObjectMover>().SetDestination(new Vector3((minHorizontalPosition_ + (i * spacing) + (spacing/2)), 0, 0));
        }
    }

    private float CalculateSpacing()
    {
        return (maxHorizontalPosition_ - minHorizontalPosition_) / (float)objects_.Count; // As I'm using a row lineup here, determining the spacing by taking the total horizontal distance available and dividing it by the number of objects.
    }

    private int SortByColour(GameObject obj1, GameObject obj2)
    {
        return obj1.GetComponent<ColourGenerator>().Hue.CompareTo(obj2.GetComponent<ColourGenerator>().Hue);
    }
}
