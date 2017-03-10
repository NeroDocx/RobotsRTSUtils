// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using RobotsRTS.NetWork;
using System.Collections.Generic;
namespace RobotsRTS.Xml
{
    public static class XmlUtils
    {
        public static bool XMLDataAnalize(DataXml _xml, out GameData _gameXml, out string[] _logMsgs)
        {
            _gameXml = new GameData();
            _logMsgs = new string[0];
            List<string> logList = new List<string>(40);
            Dictionary<int, LevelDescription> levels = new Dictionary<int, LevelDescription>();
            Dictionary<BuildType, BuildDescription> bulids = new Dictionary<BuildType, BuildDescription>();
            Dictionary<TurretType, TurretDescription> turrets = new Dictionary<TurretType, TurretDescription>();
            string turretPlatformPath = string.Empty;
            Dictionary<int, RobotChassisDescription> robotChassis = new Dictionary<int, RobotChassisDescription>();
            Dictionary<int, RobotBodyDescription> robotBodys = new Dictionary<int, RobotBodyDescription>();
            Dictionary<int, RobotHeadModuleDescription> robotHeadModules = new Dictionary<int, RobotHeadModuleDescription>();
            Dictionary<int, RobotSpecialModuleDescription> robotSpecialModules = new Dictionary<int, RobotSpecialModuleDescription>();
            Dictionary<int, RobotWeaponDescription> robotWeapons = new Dictionary<int, RobotWeaponDescription>();

            if (_xml == null)
            {
                logList.Add("XML Analize, DataXml is null");
                _gameXml.Levels = levels;
                _gameXml.Builds = bulids;
                _gameXml.Turrets = turrets;
                _gameXml.TurretPlatformPath = turretPlatformPath;
                _gameXml.RobotChassis = robotChassis;
                _gameXml.RobotBodys = robotBodys;
                _gameXml.RobotHeadModules = robotHeadModules;
                _gameXml.RobotSpecialModules = robotSpecialModules;
                _gameXml.RobotWeapons = robotWeapons;
                _logMsgs = logList.ToArray();
                return false;
            }
            if (ArrayIsNullOrEmpty(_xml.Levels))
            {
                logList.Add("xml.Levels is empty");
            }
            else
            {
                foreach (LevelDescription val in _xml.Levels)
                {
                    if (val == null)
                    {
                        logList.Add("Level is null");
                        continue;
                    }
                    if (string.IsNullOrEmpty(val.Name))
                    {
                        logList.Add(string.Format("Level {0} name is empty", val.Id));
                        continue;
                    }
                    if (string.IsNullOrEmpty(val.Path))
                    {
                        logList.Add(string.Format("Level {0} path is empty", val.Id));
                        continue;
                    }
                    if (levels.ContainsKey(val.Id))
                    {
                        logList.Add(string.Format("Level {0} is dublicate", val.Id));
                        continue;
                    }
                    levels.Add(val.Id, val);
                }
            }
            if (ArrayIsNullOrEmpty(_xml.Builds))
            {
                logList.Add("xml.Builds is empty");
            }
            else
            {
                foreach (BuildDescription val in _xml.Builds)
                {
                    if (val == null)
                    {
                        logList.Add("Build is null");
                        continue;
                    }
                    if (string.IsNullOrEmpty(val.Name))
                    {
                        logList.Add(string.Format("Build {0} name is empty", val.Type));
                        continue;
                    }
                    if (string.IsNullOrEmpty(val.Path))
                    {
                        logList.Add(string.Format("Build {0} path is empty", val.Type));
                        continue;
                    }
                    if (bulids.ContainsKey(val.Type))
                    {
                        logList.Add(string.Format("Build {0} is dublicate", val.Type));
                        continue;
                    }
                    bulids.Add(val.Type, val);
                }
            }

            ////////////////////
            if (ArrayIsNullOrEmpty(_xml.Turrets))
            {
                logList.Add("xml.Turrets is empty");
            }
            else
            {
                foreach (TurretDescription val in _xml.Turrets)
                {
                    if (val == null)
                    {
                        logList.Add("Turret is null");
                        continue;
                    }
                    if (string.IsNullOrEmpty(val.Name))
                    {
                        logList.Add(string.Format("Turret {0} name is empty", val.Type));
                        continue;
                    }
                    if (string.IsNullOrEmpty(val.Path))
                    {
                        logList.Add(string.Format("Turret {0} path is empty", val.Type));
                        continue;
                    }
                    if (turrets.ContainsKey(val.Type))
                    {
                        logList.Add(string.Format("Turret {0} is dublicate", val.Type));
                        continue;
                    }
                    turrets.Add(val.Type, val);
                }
            }
            if (string.IsNullOrEmpty(_xml.TurretPlatformPath))
            {
                logList.Add("turret Platform Path empty");
            }
            else
            {
                turretPlatformPath = _xml.TurretPlatformPath;
            }


            ///////////////////////////////////////////////////
            if (ArrayIsNullOrEmpty(_xml.RobotChassis))
            {
                logList.Add("xml.RobotChassis is empty");
            }
            else
            {
                foreach (RobotChassisDescription val in _xml.RobotChassis)
                {
                    if (val == null)
                    {
                        logList.Add("RobotChassis is null");
                        continue;
                    }
                    if (string.IsNullOrEmpty(val.Name))
                    {
                        logList.Add(string.Format("RobotChassis {0} name is empty", val.Id));
                        continue;
                    }
                    if (val.Health <= 0)
                    {
                        logList.Add(string.Format("RobotChassis {0} Health is Incorrect Health = {1}", val.Id, val.Health));
                        continue;
                    }
                    if (!CheckItemCost(val.Cost))
                    {
                        logList.Add(string.Format("RobotChassis {0} Cost is Incorrect {1}", val.Id, val.Cost == null ? "null" : "zero value"));
                        continue;
                    }
                    if (val.Speed <= 0)
                    {
                        logList.Add(string.Format("RobotChassis {0} Health is Incorrect Speed = {1}", val.Id, val.Speed));
                        continue;
                    }
                    if (val.SpeedRotation <= 0)
                    {
                        logList.Add(string.Format("RobotChassis {0} Health is Incorrect SpeedRotation = {1}", val.Id, val.SpeedRotation));
                        continue;
                    }
                    if (string.IsNullOrEmpty(val.Path))
                    {
                        logList.Add(string.Format("RobotChassis {0} path is empty", val.Id));
                        continue;
                    }
                    if (robotChassis.ContainsKey(val.Id))
                    {
                        logList.Add(string.Format("RobotChassis {0} is dublicate", val.Id));
                        continue;
                    }
                    robotChassis.Add(val.Id, val);
                }
            }

            if (ArrayIsNullOrEmpty(_xml.RobotBodys))
            {
                logList.Add("xml.RobotBodys is empty");
            }
            else
            {
                foreach (RobotBodyDescription val in _xml.RobotBodys)
                {
                    if (val == null)
                    {
                        logList.Add("RobotBody is null");
                        continue;
                    }
                    if (string.IsNullOrEmpty(val.Name))
                    {
                        logList.Add(string.Format("RobotBody {0} name is empty", val.Id));
                        continue;
                    }
                    if (val.Health <= 0)
                    {
                        logList.Add(string.Format("RobotBody {0} Health is Incorrect Health = {1}", val.Id, val.Health));
                        continue;
                    }
                    if (!CheckItemCost(val.Cost))
                    {
                        logList.Add(string.Format("RobotBody {0} Cost is Incorrect {1}", val.Id, val.Cost == null ? "null" : "zero value"));
                        continue;
                    }
                    if (ArrayIsNullOrEmpty(val.WeaponPositions))
                    {
                        logList.Add(string.Format("RobotBody {0} WeaponPositions is empty", val.Id));
                        continue;
                    }
                    foreach (Vector weaponPos in val.WeaponPositions)
                    {
                        if (weaponPos == null)
                        {
                            logList.Add(string.Format("RobotBody {0} constaince WeaponPos is null", val.Id));
                            continue;
                        }
                    }
                    if (string.IsNullOrEmpty(val.Path))
                    {
                        logList.Add(string.Format("RobotBody {0} path is empty", val.Id));
                        continue;
                    }
                    if (robotBodys.ContainsKey(val.Id))
                    {
                        logList.Add(string.Format("RobotBody {0} is dublicate", val.Id));
                        continue;
                    }
                    robotBodys.Add(val.Id, val);
                }
            }

            if (ArrayIsNullOrEmpty(_xml.RobotHeadModules))
            {
                logList.Add("xml.RobotHeadModules is empty");
            }
            else
            {
                foreach (RobotHeadModuleDescription val in _xml.RobotHeadModules)
                {
                    if (val == null)
                    {
                        logList.Add("RobotHeadModule is null");
                        continue;
                    }
                    if (string.IsNullOrEmpty(val.Name))
                    {
                        logList.Add(string.Format("RobotHeadModule {0} name is empty", val.Id));
                        continue;
                    }
                    if (!CheckItemCost(val.Cost))
                    {
                        logList.Add(string.Format("RobotHeadModule {0} Cost is Incorrect {1}", val.Id, val.Cost == null ? "null" : "zero value"));
                        continue;
                    }
                    if (string.IsNullOrEmpty(val.Path))
                    {
                        logList.Add(string.Format("RobotHeadModule {0} path is empty", val.Id));
                        continue;
                    }
                    if (robotHeadModules.ContainsKey(val.Id))
                    {
                        logList.Add(string.Format("RobotHeadModule {0} is dublicate", val.Id));
                        continue;
                    }
                    robotHeadModules.Add(val.Id, val);
                }
            }

            if (ArrayIsNullOrEmpty(_xml.RobotSpecialModules))
            {
                logList.Add("xml.RobotSpecialModules is empty");
            }
            else
            {
                foreach (RobotSpecialModuleDescription val in _xml.RobotSpecialModules)
                {
                    if (val == null)
                    {
                        logList.Add("RobotSpecialModule is null");
                        continue;
                    }
                    if (string.IsNullOrEmpty(val.Name))
                    {
                        logList.Add(string.Format("RobotSpecialModule {0} name is empty", val.Id));
                        continue;
                    }
                    if (!CheckItemCost(val.Cost))
                    {
                        logList.Add(string.Format("RobotSpecialModule {0} Cost is Incorrect {1}", val.Id, val.Cost == null ? "null" : "zero value"));
                        continue;
                    }
                    if (string.IsNullOrEmpty(val.Path))
                    {
                        logList.Add(string.Format("RobotSpecialModule {0} path is empty", val.Id));
                        continue;
                    }
                    if (robotSpecialModules.ContainsKey(val.Id))
                    {
                        logList.Add(string.Format("RobotSpecialModule {0} is dublicate", val.Id));
                        continue;
                    }
                    robotSpecialModules.Add(val.Id, val);
                }
            }

            if (ArrayIsNullOrEmpty(_xml.RobotWeapons))
            {
                logList.Add("xml.RobotWeapons is empty");
            }
            else
            {
                foreach (RobotWeaponDescription val in _xml.RobotWeapons)
                {
                    if (val == null)
                    {
                        logList.Add("RobotWeapon is null");
                        continue;
                    }
                    if (string.IsNullOrEmpty(val.Name))
                    {
                        logList.Add(string.Format("RobotWeapon {0} name is empty", val.Id));
                        continue;
                    }
                    if (!CheckItemCost(val.Cost))
                    {
                        logList.Add(string.Format("RobotWeapon {0} Cost is Incorrect {1}", val.Id, val.Cost == null ? "null" : "zero value"));
                        continue;
                    }
                    if (string.IsNullOrEmpty(val.Path))
                    {
                        logList.Add(string.Format("RobotWeapon {0} path is empty", val.Id));
                        continue;
                    }
                    if (robotWeapons.ContainsKey(val.Id))
                    {
                        logList.Add(string.Format("RobotWeapon {0} is dublicate", val.Id));
                        continue;
                    }
                    robotWeapons.Add(val.Id, val);
                }
            }

            _gameXml.Levels = levels;
            _gameXml.Builds = bulids;
            _gameXml.Turrets = turrets;
            _gameXml.TurretPlatformPath = turretPlatformPath;
            _gameXml.RobotChassis = robotChassis;
            _gameXml.RobotBodys = robotBodys;
            _gameXml.RobotHeadModules = robotHeadModules;
            _gameXml.RobotSpecialModules = robotSpecialModules;
            _gameXml.RobotWeapons = robotWeapons;
            return true;
        }
        private static bool CheckItemCost(ItemCost _cost)
        {
            return _cost != null && (_cost.ResOneCost > 0 || _cost.ResTwoCost > 0 || _cost.ResThreeCost > 0 || _cost.ResFourCost > 0);
        }
        private static bool ArrayIsNullOrEmpty<T>(this T[] _array)
        {
            return ((_array == null) || _array.Length < 1) ? true : false;
        }
    }
}
