using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;

public struct PlayerSprite : IBufferElementData
{
    public Entity entity;
}

public struct Player : IComponentData
{
    public Entity player;
}

public struct AudioLibrary : IComponentData
{
}

public enum AudioTypes
{
    None,
    Jump,
    Button,
    BGM,
    Star
}

public struct AudioObject : IBufferElementData
{
    public AudioTypes Type;
    public Entity Clip;
}

public struct Level : IBufferElementData
{
    public Entity level;
}

public struct TimeRecord : IBufferElementData
{
    public int level;
    public float time;
}

public struct GameManager : IComponentData
{

    public FixedString64 playerName;
    public int currentLv;
    public int maxLv;

    public bool isOpenLvSelect;
}