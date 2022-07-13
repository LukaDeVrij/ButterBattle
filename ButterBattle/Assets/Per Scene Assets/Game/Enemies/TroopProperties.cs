using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopProperties : MonoBehaviour
{

    public static int troopSelected = 0;

    public static Dictionary<int, int> costOf = new Dictionary<int, int>()
    {
        {0, 10},
        {1, 10},
        {2, 10},
        {3, 10},
        {4, 10},
        {5, 10},
        {6, 10},
        {7, 10},
        {8, 10},
        {9, 10}
    }; 
    public static Dictionary<int, int> hpOf = new Dictionary<int, int>()
    {
        {0, 30},
        {1, 30},
        {2, 30},
        {3, 30},
        {4, 30},
        {5, 30},
        {6, 30},
        {7, 30},
        {8, 30},
        {9, 30}
    }; 
    public static Dictionary<int, int> speedOf = new Dictionary<int, int>()
    {
        {0, 1000},
        {1, 1000},
        {2, 1000},
        {3, 1000},
        {4, 1000},
        {5, 1000},
        {6, 1000},
        {7, 1000},
        {8, 1000},
        {9, 1000}
    };
    public static Dictionary<int, float> attackRateOf = new Dictionary<int, float>()
    {
        {0, 1},
        {1, 1},
        {2, 1},
        {3, 1},
        {4, 1},
        {5, 1},
        {6, 1},
        {7, 1},
        {8, 1},
        {9, 1}
    }; 
    public static Dictionary<int, int> ADamageMinOf = new Dictionary<int, int>()
    {
        {0, 8},
        {1, 8},
        {2, 8},
        {3, 8},
        {4, 8},
        {5, 8},
        {6, 8},
        {7, 8},
        {8, 8},
        {9, 8}
    };
    public static Dictionary<int, int> ADamageMaxOf = new Dictionary<int, int>()
    {
        {0, 12},
        {1, 12},
        {2, 12},
        {3, 12},
        {4, 12},
        {5, 12},
        {6, 12},
        {7, 12},
        {8, 12},
        {9, 12}
    };
    public static Dictionary<int, int> BDamageMinOf = new Dictionary<int, int>()
    {
        {0, 10},
        {1, 10},
        {2, 10},
        {3, 10},
        {4, 10},
        {5, 10},
        {6, 10},
        {7, 10},
        {8, 10},
        {9, 10}
    };
    public static Dictionary<int, int> BDamageMaxOf = new Dictionary<int, int>()
    {
        {0, 10},
        {1, 10},
        {2, 10},
        {3, 10},
        {4, 10},
        {5, 10},
        {6, 10},
        {7, 10},
        {8, 10},
        {9, 10}
    };
    public static Dictionary<int, string> armorClassOf = new Dictionary<int, string>()
    {
        {0, "armored"},
        {1, "armored"},
        {2, "armored"},
        {3, "armored"},
        {4, "armored"},
        {5, "armored"},
        {6, "armored"},
        {7, "armored"},
        {8, "armored"},
        {9, "armored"}
    };

}
