using System.Collections.Generic;
using UnityEngine;

public enum team
{
    red,
    blue
}

public class UnitsManager : MonoBehaviour
{
    public Dictionary<team, List<GameObject>> units = new Dictionary<team, List<GameObject>>();

    #region Singleton
    public static UnitsManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    private static UnitsManager _instance;


    private void Start()
    {
        units[team.red] = new List<GameObject>();
        units[team.blue] = new List<GameObject>();
    }
}
