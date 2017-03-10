// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;
namespace RobotsRTS.NetWork
{
    public enum TeamId
    {
        Red,
        Green,
        Blue,
        Yelow,


        /// <summary>
        /// Команда за котрую нельяз играть. 
        /// Она не строит юнитов и турелей
        /// </summary>
        Grey,
        /// <summary>
        /// Это команда для наблюдателей
        /// </summary>
        Spectator
    }
    [Flags]
    public enum UnitCommandType
    {
        None = 1 << 0,                  // Юнит стоит
        Move = 1 << 1,                  // Юнит двигается к цели
        RotateBody = 1 << 2,            // Тело юнита вращается
        Atack = 1 << 3,                 // Юнит атакует
        Capture = 1 << 4,               // Юнит захватывает
        SelfDestruction = 1 << 5,       // Приказ на самоуничтожение
        Locked = 1 << 6                 // Управление этим юнитом забокировано
    }
    public enum GameCommandType
    {
        WaitStartValues,                // Клиент готов начать игру и ждет стартовых условий 
        GameReady,                      // Клиент завершил подготовку и полностью готов к игре
        UnitCommand,                    // Команда для юнита
        AddBuildTask,                   // Запрос на создание нового обьекта(турель или робот)
        RemoveBuildTask,                // Запрос на удаление обьекта(турель или робот) из очереди на строительство
        DEBUG_DESTROY_TURRET,           // Запрос на уничтожение турели
        DEBUG_DESTROY_BUILD,            // Запрос на уничтожение здания
        DEBUG_DAMAGE_TURRET,            // Запрос на урон по здоровью турели
        DEBUG_DAMAGE_BUILD              // Запрос на урон по здоровью зданию
    }
    public enum BuildType
    {
        Base,
        FactoryOne,
        FactoryTwo,
        FactotyThree,
        FactoryFour
    }
    public enum BuildTaskType
    {
        Robot,
        Turret
    }
    public enum TurretType
    {
        None,
        LightTurret,
        HeavyTurret,
        LaserTurret,
        RocketTurret

    }
    public enum TurretState
    {
        Bulding,
        Ready
    }
    public enum TurretPlatformState
    {
        Turret,
        Building,
        Empty
    }
    public enum BulletType
    {
        PermanentLaser,
        MomentLaser,
        Bullet,
        Cloud
    }
    public enum BulletStateType
    {
        Move,               // пуля движестя
        Hit,                // пуля попала

        Destruction         // пуля сомоуничтожилась
    }
    public enum TargetType
    {
        Unit,
        Build,
        Turret,
        Ground
    }
    public enum RezCode
    {
        Ok,
        InvalidCommmand,
        GameCommandIsNull,
        UnitCommandsIsNull,
        UnitCommandIsIcorrectArrayLength,
        UnitCommandUnitsForCommandIsNotBelongsForTargetTeam,
        UnitCommandIsNull,
        UnitCommandIsNotCorrectButAccepted,
        SendCommandForNonexistentUnit,
        BuildCommandIsIcorrectArrayLength,
        BuildCommandItemIsNull,
        BuildCommandBuildForCommandIsNotFound,
        BuildCommandBuildForCommandIsNotBelongsForTargetTeam,
        BuildCommandTaskForCommandIsNotFound,
        BuildCommandBuildForCommandIsNotContainsTurretPlatforms,
        BuildCommandBuildForCommandIsNotContainsTargetTurretPlatform,
        BuildCommandTargetTurretPlatformIsContainTurret,
        BuildCommandTargetTurretCantBuy,
        BuildCommandCantCreateTurret,
        BuildCommandCantCompliteCommand
    }
}