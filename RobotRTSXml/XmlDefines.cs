// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using RobotsRTS.NetWork;
using System;
namespace RobotsRTS.Xml
{
    /// <summary>
    /// класс верктор, для сохранения позицй
    /// </summary>
    [Serializable]
    public class Vector
    {
        /// <summary>
        /// координата X
        /// </summary>
        public float X;
        /// <summary>
        /// координата Y
        /// </summary>
        public float Y;
        /// <summary>
        /// координата Z
        /// </summary>
        public float Z;
        /// <summary>
        /// Стандартный конструктор без параметров
        /// </summary>
        public Vector() { }
        /// <summary>
        /// Коструктор с парамерами
        /// </summary>
        /// <param name="_X">координата X</param>
        /// <param name="_Y">координата Y</param>
        /// <param name="_Z">координата Z</param>
        public Vector(float _x, float _y, float _Z)
        {
            X = _x;
            Y = _y;
            Z = _Z;
        }
    }
    /// <summary>
    /// класс для сохранения вращения 
    /// </summary>
    [Serializable]
    public class Quat
    {
        /// <summary>
        /// координата X
        /// </summary>
        public float X;
        /// <summary>
        /// координата Y
        /// </summary>
        public float Y;
        /// <summary>
        /// координата Z
        /// </summary>
        public float Z;
        /// <summary>
        /// координата W
        /// </summary>
        public float W;
        /// <summary>
        /// Стандартный конструктор без параметров
        /// </summary>
        public Quat() { }
        /// <summary>
        /// Коструктор с парамерами
        /// </summary>
        /// <param name="_X">координата X</param>
        /// <param name="_Y">координата Y</param>
        /// <param name="_Z">координата Z</param>
        /// <param name="_W">координата W</param>
        public Quat(float _x, float _y, float _Z, float _W)
        {
            X = _x;
            Y = _y;
            Z = _Z;
            W = _W;
        }
    }
    [Serializable]
    public class ItemCost
    {
        /// <summary>
        /// Стоимость в металле
        /// </summary>
        public int ResOneCost;
        /// <summary>
        /// Стоимость в чипах
        /// </summary>
        public int ResTwoCost;
        /// <summary>
        /// Стоимость в батареях
        /// </summary>
        public int ResThreeCost;
        /// <summary>
        /// Стоимость в плазме
        /// </summary>
        public int ResFourCost;
        /// <summary>
        /// Стандартный конструктор без параметров
        /// </summary>
        public ItemCost() { }
        /// <summary>
        /// Контструктор с параметрами
        /// </summary>
        /// <param name="_resOneCost">Стоимость в металле</param>
        /// <param name="_resTwoCost">Стоимость в чипах</param>
        /// <param name="_resThreeCost">Стоимость в батареях</param>
        /// <param name="_resFourCost">Стоимость в плазме</param>
        public ItemCost(int _resOneCost, int _resTwoCost, int _resThreeCost, int _resFourCost)
        {
            ResOneCost = _resOneCost;
            ResTwoCost = _resTwoCost;
            ResThreeCost = _resThreeCost;
            ResFourCost = _resFourCost;
        }
    }
    [Serializable]
    public class LevelDescription
    {
        /// <summary>
        /// Идентификатор уровня
        /// </summary>
        public int Id;
        /// <summary>
        /// Имя уровня
        /// </summary>
        public string Name;
        /// <summary>
        /// Местоположение детальной информации  о левеле (Префаб, XML, Иконка)
        /// </summary>
        public string Path;
        /// <summary>
        /// Стандартный конструктор без параметров
        /// </summary>
        public LevelDescription() { }
        /// <summary>
        /// Контструктор с параметрами
        /// </summary>
        /// <param name="_id">Идентификатор уровня</param>
        /// <param name="_name">Имя уровня</param>
        /// <param name="_name">Местоположение детальной информации  о левеле</param>
        public LevelDescription(int _id, string _name, string _path)
        {
            Id = _id;
            Name = _name;
            Path = _path;
        }
    }
    [Serializable]
    public class BuildDescription
    {
        /// <summary>
        /// Тип здания
        /// </summary>
        public BuildType Type;
        /// <summary>
        /// Название здания
        /// </summary>
        public string Name;
        /// <summary>
        /// Местоположение детальной информации  о задании (Префаб, Иконка в префабе)
        /// </summary>
        public string Path;
        /// <summary>
        /// Стандартный конструктор без параметров
        /// </summary>
        public BuildDescription() { }
        /// <summary>
        /// Контструктор с параметрами
        /// </summary>
        /// <param name="_id">Тип здания</param>
        /// <param name="_name">Название здания</param>
        /// <param name="_path">Местоположение детальной информации  о задании (Префаб, Иконка в префабе)</param>
        public BuildDescription(BuildType _type, string _name, string _path)
        {
            Type = _type;
            Name = _name;
            Path = _path;
        }
    }
    [Serializable]
    public class TurretDescription
    {
        /// <summary>
        /// Тип турели
        /// </summary>
        public TurretType Type;
        /// <summary>
        /// Название турели
        /// </summary>
        public string Name;
        /// <summary>
        /// количество очков здоровья
        /// </summary>
        public int Health;
        /// <summary>
        /// Стоимость турели
        /// </summary>
        public ItemCost Cost;
        /// <summary>
        /// Местоположение детальной информации  о задании (Префаб, Иконка в префабе)
        /// </summary>
        public string Path;
        /// <summary>
        /// Путь до призрака
        /// </summary>
        public string PathGhost;
        /// <summary>
        /// Стандартный конструктор без параметров
        /// </summary>
        public TurretDescription() { }
        /// <summary>
        /// Контструктор с параметрами
        /// </summary>
        /// <param name="_type">Тип здания</param>
        /// <param name="_name">Название здания</param>
        /// <param name="_resOneCost">Стоимость турели</param>
        /// <param name="_path">Местоположение детальной информации  о задании (Префаб, Иконка в префабе)</param>
        public TurretDescription(TurretType _type, string _name, int _health, ItemCost _сost, string _path, string _pathGhost)
        {
            Type = _type;
            Name = _name;
            Health = _health;
            Cost = _сost;
            Path = _path;
            PathGhost = _pathGhost;
        }
    }
    [Serializable]
    public class RobotChassisDescription
    {
        /// <summary>
        /// Id Шасси
        /// </summary>
        public int Id;
        /// <summary>
        /// Название
        /// </summary>
        public string Name;
        /// <summary>
        /// Количество очков здоровья
        /// </summary>
        public int Health;
        /// <summary>
        /// стоимость
        /// </summary>
        public ItemCost Cost;
        /// <summary>
        /// скорость движения
        /// </summary>
        public float Speed;
        /// <summary>
        /// скорость поворота
        /// </summary>
        public float SpeedRotation;
        /// <summary>
        /// Местоположение детальной информации  о задании (Префаб, Иконка в префабе)
        /// </summary>
        public string Path;
        /// <summary>
        /// Стандартный конструктор без параметров
        /// </summary>
        public RobotChassisDescription() { }
        /// <summary>
        /// Контструктор с параметрами
        /// </summary>
        /// <param name="_id">id</param>
        /// <param name="_name">Название Шасси</param>
        /// <param name="_health">Количество очков здоровья</param>
        /// <param name="_cost">Стоимость</param>
        /// <param name="_Speed">скорость движения</param>
        /// <param name="_SpeedRotation">скорость поворота</param>
        /// <param name="_path">Местоположение детальной информации  о задании (Префаб, Иконка в префабе)</param>
        public RobotChassisDescription(int _id, string _name, int _health, ItemCost _cost, float _Speed, float _SpeedRotation, string _path)
        {
            Id = _id;
            Name = _name;
            Health = _health;
            Cost = _cost;
            Speed = _Speed;
            SpeedRotation = _SpeedRotation;
            Path = _path;
        }
    }
    [Serializable]
    public class RobotBodyDescription
    {
        /// <summary>
        /// Id тела
        /// </summary>
        public int Id;
        /// <summary>
        /// Название 
        /// </summary>
        public string Name;
        /// <summary>
        /// Количество очков здоровья
        /// </summary>
        public int Health;
        /// <summary>
        /// стоимость
        /// </summary>
        public ItemCost Cost;
        /// <summary>
        /// Позтции для установки орудий
        /// </summary>
        public Vector[] WeaponPositions;
        /// <summary>
        /// Можно ли установить спец. снаряжение
        /// </summary>
        public bool CanSpecialSlot;
        /// <summary>
        /// Местоположение детальной информации  о задании (Префаб, Иконка в префабе)
        /// </summary>
        public string Path;
        /// <summary>
        /// Стандартный конструктор без параметров
        /// </summary>
        public RobotBodyDescription() { }
        /// <summary>
        /// Контструктор с параметрами
        /// </summary>
        /// <param name="_type">Id</param>
        /// <param name="_name">Название тела</param>
        /// <param name="_health">Количество очков здоровья</param>
        /// <param name="_cost">Стоимость</param>
        /// <param name="_canSpecialSlot">Можно ли установить спец. снаряжение</param>
        /// <param name="_weaponSlotsCount">количество слотов под орудия</param>
        /// <param name="_path">Местоположение детальной информации  о задании (Префаб, Иконка в префабе)</param>
        public RobotBodyDescription(int _id, string _name, int _health, ItemCost _cost, Vector[] _weaponPositions, bool _canSpecialSlot, string _path)
        {
            Id = _id;
            Name = _name;
            Health = _health;
            Cost = _cost;
            WeaponPositions = _weaponPositions == null ? new Vector[0] : _weaponPositions;
            CanSpecialSlot = _canSpecialSlot;
            Path = _path;
        }
    }
    [Serializable]
    public class RobotWeaponDescription
    {
        /// <summary>
        /// Id Модуля управления
        /// </summary>
        public int Id;
        /// <summary>
        /// название
        /// </summary>
        public string Name;
        /// <summary>
        /// стоимость
        /// </summary>
        public ItemCost Cost;
        /// <summary>
        /// Местоположение детальной информации  о задании (Префаб, Иконка в префабе)
        /// </summary>
        public string Path;
        /// <summary>
        /// Стандартный конструктор без параметров
        /// </summary>
        public RobotWeaponDescription() { }
        /// <summary>
        /// Контструктор с параметрами
        /// </summary>
        /// <param name="_id">Id пушки</param>
        /// <param name="_name">Название пушки</param>
        /// <param name="_cost">стоимость</param>
        /// <param name="_path">Местоположение детальной информации  о пушке (Префаб, Иконка в префабе)</param>
        public RobotWeaponDescription(int _id, string _name, ItemCost _cost, string _path)
        {
            Id = _id;
            Name = _name;
            Cost = _cost;
            Path = _path;
        }
    }
    [Serializable]
    public class RobotHeadModuleDescription
    {
        /// <summary>
        /// Id Модуля управления
        /// </summary>
        public int Id;
        /// <summary>
        /// название
        /// </summary>
        public string Name;
        /// <summary>
        /// стоимость
        /// </summary>
        public ItemCost Cost;
        /// <summary>
        /// Местоположение детальной информации  о задании (Префаб, Иконка в префабе)
        /// </summary>
        public string Path;
        /// <summary>
        /// Стандартный конструктор без параметров
        /// </summary>
        public RobotHeadModuleDescription() { }
        /// <summary>
        /// Контструктор с параметрами
        /// </summary>
        /// <param name="_id">Id Модуля управления</param>
        /// <param name="_name">Название здания</param>
        /// <param name="_cost">стоимость</param>
        /// <param name="_path">Местоположение детальной информации  о задании (Префаб, Иконка в префабе)</param>
        public RobotHeadModuleDescription(int _id, string _name, ItemCost _cost, string _path)
        {
            Id = _id;
            Name = _name;
            Cost = _cost;
            Path = _path;
        }
    }
    [Serializable]
    public class RobotSpecialModuleDescription
    {
        /// <summary>
        /// Id специального модуля
        /// </summary>
        public int Id;
        /// <summary>
        /// название
        /// </summary>
        public string Name;
        /// <summary>
        /// стоимость
        /// </summary>
        public ItemCost Cost;
        /// <summary>
        /// Местоположение детальной информации  о задании (Префаб, Иконка в префабе)
        /// </summary>
        public string Path;
        /// <summary>
        /// Стандартный конструктор без параметров
        /// </summary>
        public RobotSpecialModuleDescription() { }
        /// <summary>
        /// Контструктор с параметрами
        /// </summary>
        /// <param name="_id">Id специального модуля</param>
        /// <param name="_name">название</param>
        /// <param name="_name">стоимость</param>
        /// <param name="_path">Местоположение детальной информации  о задании (Префаб, Иконка в префабе)</param>
        public RobotSpecialModuleDescription(int _id, string _name, ItemCost _cost, string _path)
        {
            Id = _id;
            Name = _name;
            Cost = _cost;
            Path = _path;
        }
    }
    [Serializable]
    public class DataXml
    {
        /// <summary>
        /// Базовое описание уровней
        /// </summary>
        public LevelDescription[] Levels;
        /// <summary>
        /// Базовое описание зданий
        /// </summary>
        public BuildDescription[] Builds;
        /// <summary>
        /// Базовое описание турелей
        /// </summary>
        public TurretDescription[] Turrets;
        /// <summary>
        /// Путь до префаба платформы турелей
        /// </summary>
        public string TurretPlatformPath;
        /// <summary>
        /// Базовое описание шааси роботов
        /// </summary>
        public RobotChassisDescription[] RobotChassis;
        /// <summary>
        /// Базовое описание Тела роботов
        /// </summary>
        public RobotBodyDescription[] RobotBodys;
        /// <summary>
        /// Базовое описание оружия роботов
        /// </summary>
        public RobotWeaponDescription[] RobotWeapons;
        /// <summary>
        /// Базовое описание модулей управления роботов
        /// </summary>
        public RobotHeadModuleDescription[] RobotHeadModules;
        /// <summary>
        /// Базовое описание специальных модулей роботов
        /// </summary>
        public RobotSpecialModuleDescription[] RobotSpecialModules;
        /// <summary>
        /// Стандартный конструктор без параметров
        /// </summary>
        public DataXml() { }
        /// <summary>
        /// Контструктор с параметрами
        /// </summary>
        /// <param name="_levels">список уровней</param>
        public DataXml(LevelDescription[] _levels, BuildDescription[] _builds, TurretDescription[] _turrets, string _turretPlatformPath, RobotChassisDescription[] _robotChassis, RobotBodyDescription[] _robotBodys, RobotWeaponDescription[] _robotWeapons, RobotHeadModuleDescription[] _robotHeadModules, RobotSpecialModuleDescription[] _robotSpecialModules)
        {
            Levels = _levels;
            Builds = _builds;
            Turrets = _turrets;
            TurretPlatformPath = _turretPlatformPath;
            RobotChassis = _robotChassis;
            RobotBodys = _robotBodys;
            RobotWeapons = _robotWeapons;
            RobotHeadModules = _robotHeadModules;
            RobotSpecialModules = _robotSpecialModules;
        }
    }
}
