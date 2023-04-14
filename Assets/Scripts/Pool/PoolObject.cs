using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private Transform poolTransform;
    public Transform PoolTransform
    {
        set 
        {
            poolTransform = value; 
        }
    }

    public void RetunToPool()
    {
        gameObject.transform.SetParent(poolTransform);
        gameObject.SetActive(false);
    }
}