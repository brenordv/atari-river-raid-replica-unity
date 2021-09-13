using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    /// <summary>
    /// Original code from Zenvia.
    /// </summary>
    public class ObjectPool : MonoBehaviour
    {
        public SoundPlayer soundPlayer;
        
        //prefab that the pool will use
        public Enemy poolPrefab;

        //initial number of element
        public int initialNum = 10;

        //collection
        List<Enemy> _pooledObjects;

        //init pool
        private void Awake()
        {
            // if the pool has already been init, don't init again
            if(_pooledObjects == null)
            {
                InitPool();
            }
        }

        //init pool
        public void InitPool()
        {
            //init list
            _pooledObjects = new List<Enemy>();

            // create this initial number of objects
            for (int i = 0; i < initialNum; i++)
            {
                // create a new object
                CreateObj();
            }
        }

        //create a new object
        private Enemy CreateObj()
        {
            // create a new object
            var newObj = Instantiate(poolPrefab);
            newObj.soundPlayer = soundPlayer;
            // set this new object to inactive
            newObj.gameObject.SetActive(false);

            // add it to the list
            _pooledObjects.Add(newObj);

            return newObj;
        }

        // retrieve an object from the pool
        public GameObject GetObj()
        {
            // search our list for an inactive object
            foreach (var t in _pooledObjects)
            {
                // if we find an inactive object
                if(!t.gameObject.activeInHierarchy)
                {
                    //enable it (set it to active)
                    t.gameObject.SetActive(true);

                    // return that object
                    return t.gameObject;
                }
            }

            // increase our pool (create a new object)
            var newObj = CreateObj();

            // enable that new object
            newObj.gameObject.SetActive(true);

            // return that object
            return newObj.gameObject;
        }

        // get all active objects
        public List<Enemy> GetAllActive()
        {
            var activeObjs = new List<Enemy>();

            // search our list for active objects
            foreach (var t in _pooledObjects)
            {
                // if we find an active object
                if (t.gameObject.activeInHierarchy)
                {
                    activeObjs.Add(t);
                }
            }

            return activeObjs;
        }
    }
}
