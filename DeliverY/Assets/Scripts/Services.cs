using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Services
{
    private static EventManager _eventManager;

    public static EventManager EventManager
    {
        get
        {
            Debug.Assert(_eventManager != null);
            return _eventManager;
        }
        set => _eventManager = value;
    }

    private static List<PlayerControl> _players;

    public static List<PlayerControl> Players
    {
        get
        {
            Debug.Assert(_players != null);
            return _players;
        }
        set => _players = value;
    }

    private static LevelStatus _levelStatus;

    public static LevelStatus LevelStatus
    {
        get
        {
            return _levelStatus;
        }
        set => _levelStatus = value;
    }
}

