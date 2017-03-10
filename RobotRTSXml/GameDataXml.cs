// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using RobotsRTS.NetWork;
using System.Collections.Generic;
namespace RobotsRTS.Xml
{
    public class GameData
    {
        /// <summary>
        /// Базовое описание уровней
        /// </summary>
        public IDictionary<int, LevelDescription> Levels;
        /// <summary>
        /// Базовое описнаие строений
        /// </summary>
        public IDictionary<BuildType, BuildDescription> Builds;
        /// <summary>
        /// Базвовое описание турелей
        /// </summary>
        public IDictionary<TurretType, TurretDescription> Turrets;
        /// <summary>
        /// Путь до префаба Платформы турелей
        /// </summary>
        public string TurretPlatformPath;
        /// <summary>
        /// Базовое описание шасси для роботов
        /// </summary>
        public IDictionary<int, RobotChassisDescription> RobotChassis;
        /// <summary>
        /// Базовое описание тел для роботов
        /// </summary>
        public IDictionary<int, RobotBodyDescription> RobotBodys;
        /// <summary>
        /// Базовое описание модулей оправления для роботов
        /// </summary>
        public IDictionary<int, RobotHeadModuleDescription> RobotHeadModules;
        /// <summary>
        /// Базовое описание специальных модулей для роботов
        /// </summary>
        public IDictionary<int, RobotSpecialModuleDescription> RobotSpecialModules;
        /// <summary>
        /// Базовое описание пушек для роботов
        /// </summary>
        public IDictionary<int, RobotWeaponDescription> RobotWeapons;
    }
}
