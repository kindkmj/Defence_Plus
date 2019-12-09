public enum SceneName
{
	Title = 0,
	MainMenu,
	Game,
}

public enum GameDifficulty
{
    None = 0,
    Normal,
    Hard,
}

public enum UnitKind
{
    Knight = 0,
    Archer = 1,
    Hero = 2,
}

public enum MonsterKind
{
    Troll = 0,
}

public enum UnitState
{
    None,
    Idle,
    Walk,
    Attack,
    Die
}

public enum MonsterState
{
    None,
    Walk,
    Attack,
    Die
}

public enum PuzzleState
{
    Normal = 0,
    Pressed = 1,
    Pop = 2,
}

public enum PuzzleManagerState
{
    Waiting = 0,
    Answering,
    BoardIsChanging,
    GameOver,
}

//public enum PuzzleColor
//{
//    Red = 0,
//    Yellow,
//    Green,
//    Blue,
//    Pupple,
//}
