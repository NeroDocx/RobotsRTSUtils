// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using Npgsql;
using RobotsRTS.Xml;
using System;
using System.Collections.Generic;
using RobotsRTS.NetWork;
namespace XMLCreator
{
    class DataXmlCreator
    {
        private string adress;
        private string login;
        private string password;
        private string dbName;
        private const string conn_param_base = "Server={0};Port=5432;User Id={1};Password={2};Database={3};";
        private NpgsqlConnection connect;

        private const string SelectLevelsSQL = "SELECT * FROM  levels ORDER BY id";
        private const string SelectBuildsSQL = "SELECT * FROM builds ORDER BY type";
        private const string SelectTurretsSQL = "SELECT * FROM turrets ORDER BY type";
        private const string SelectRobotChassisSQL = "SELECT * FROM robot_chassis ORDER BY id";
        private const string SelectRobotBodysSQL = "SELECT * FROM robot_bodys ORDER BY id";
        private const string SelectRobotWeaponPositionsSQL = "SELECT * FROM weapon_positions WHERE id_body = {0} ORDER BY weapon_positions";
        private const string SelectRobotWeaponsSQL = "SELECT * FROM robot_weapons ORDER BY id";
        private const string SelectRobotHeadsSQL = "SELECT * FROM robot_heads ORDER BY id";
        private const string SelectRobotSpecialModulesSQL = "SELECT * FROM robot_special_modules ORDER BY id";


        private const string SelectTurretPlatformSQL = "SELECT * FROM constants WHERE system_name = \"turret_platform_path\"";
        public DataXmlCreator(string adressBD, string loginBD, string passwordBD, string NameBD)
        {
            adress = adressBD;
            login = loginBD;
            password = passwordBD;
            dbName = NameBD;
        }
        public bool CreateDataXml(out DataXml dataXml)
        {
            dataXml = null;

            if (!CreateConntect())
            {
                Console.WriteLine("Connect to BD fail!!!");
                Console.ReadKey();
                return false;
            }

            #region Levels
            LevelDescription[] levels = null;
            if (!GetLevels(out levels) && CheckLevels(levels))
            {
                Console.WriteLine("Fail load Level!!!");
                return false;
            }
            Console.WriteLine("Load Levels OK...");
            #endregion

            #region Builds
            BuildDescription[] builds = null;
            if (!GetBuilds(out builds) && CheckBuilds(builds))
            {
                Console.WriteLine("Fail load builds!!!");
                return false;
            }
            Console.WriteLine("Load Builds OK...");
            #endregion

            #region Turrets
            TurretDescription[] turrets = null;
            if (!GetTurrets(out turrets) && CheckTurrets(turrets))
            {
                Console.WriteLine("Fail load turrets!!!");
                return false;
            }
            Console.WriteLine("Load Turrets OK...");

            string turretPlatform = "Turrets/TurretPlatform";
            #endregion

            #region Robots
            #region Body
            RobotBodyDescription[] robotBodys = null;
            if (!GetRobotBodys(out robotBodys) && CheckRobotBodys(robotBodys))
            {
                Console.WriteLine("Fail load Bodys!!!");
                return false;
            }
            Console.WriteLine("Load Bodys OK...");
            #endregion

            #region Chassis
            RobotChassisDescription[] robotChassis = null;
            if (!GetRobotChassis(out robotChassis) && CheckRobotChassis(robotChassis))
            {
                Console.WriteLine("Fail load Chassis!!!");
                return false;
            }
            Console.WriteLine("Load Chassis OK...");
            #endregion

            #region Weapons
            RobotWeaponDescription[] robotWeapons = null;
            if (!GetRobotWeapons(out robotWeapons) && CheckRobotWeapons(robotWeapons))
            {
                Console.WriteLine("Fail load Weapons!!!");
                return false;
            }
            Console.WriteLine("Load Weapons OK...");
            #endregion

            #region Heads
            RobotHeadModuleDescription[] robotHeads = null;
            if (!GetRobotHeads(out robotHeads) && CheckRobotHeads(robotHeads))
            {
                Console.WriteLine("Fail load Heads!!!");
                return false;
            }
            Console.WriteLine("Load Heads OK...");
            #endregion

            #region SpecialModules
            RobotSpecialModuleDescription[] robotsModules = new RobotSpecialModuleDescription[0];
            /*RobotSpecialModuleDescription[] robotsModules = null;
            if (!GetRobotSpecialModules(out robotsModules) && CheckRobotSpecialModules(robotsModules))
            {
                Console.WriteLine("Fail load Special Modules!!!");
                return false;
            }*/
            Console.WriteLine("Load Special Modules OK...");
            #endregion
            #endregion

            dataXml = new DataXml(levels, builds, turrets, turretPlatform,
                robotChassis, robotBodys, robotWeapons, robotHeads, robotsModules);

            return true;
        }
        private bool CreateConntect()
        {
            string cnnString = (new NpgsqlConnectionStringBuilder()
            {
                Host = adress,
                UserName = login,
                Password = password,
                Database = dbName
            }).ConnectionString;
            string conn_param = string.Format(conn_param_base, adress, login, password, dbName);
            connect = new NpgsqlConnection(conn_param);
            connect.Open();
            connect.Close();
            return true;
        }
        private bool GetLevels(out LevelDescription[] levels)
        {
            levels = new LevelDescription[0];
            NpgsqlCommand com = new NpgsqlCommand(SelectLevelsSQL, connect);
            connect.Open();
            NpgsqlDataReader reader = com.ExecuteReader();

            List<LevelDescription> descList = new List<LevelDescription>(10);
            int idLevel = 0;
            string nameLevel = string.Empty;
            string pathLevel = string.Empty;
            while (reader.Read())
            {
                idLevel = Int32.Parse(reader["id"].ToString());
                nameLevel = reader["name"].ToString().Trim();
                pathLevel = reader["path"].ToString().Trim();
                descList.Add(new LevelDescription(idLevel, nameLevel, pathLevel));
            }
            connect.Close();
            if (descList.Count > 0)
            {
                levels = descList.ToArray();
                return true;
            }
            return false;
        }
        private bool CheckLevels(LevelDescription[] levels)
        {
            if (!ArrayIsNullOrEmpty(levels))
            {
                Console.WriteLine("Levels is empty!!!");
                return false;
            }
            foreach (LevelDescription val in levels)
            {
                if (val == null)
                {
                    Console.WriteLine("Level is null!!!");
                    return false;
                }
                if (Array.FindIndex(levels, x => x.Id == val.Id) >= 0)
                {
                    Console.WriteLine("Levels id {0} is dublicate!!!", val.Id);
                    return false;
                }
                if (string.IsNullOrEmpty(val.Name))
                {
                    Console.WriteLine("Levels id {0} is conaince empty name!!!", val.Id);
                    return false;
                }
                if (string.IsNullOrEmpty(val.Path))
                {
                    Console.WriteLine("Levels {0} is conaince empty path!!!", val.Id);
                    return false;
                }
            }
            return true;
        }
        private bool GetBuilds(out BuildDescription[] builds)
        {
            builds = new BuildDescription[0];
            NpgsqlCommand com = new NpgsqlCommand(SelectBuildsSQL, connect);
            connect.Open();
            NpgsqlDataReader reader = com.ExecuteReader();

            List<BuildDescription> descList = new List<BuildDescription>(10);
            BuildType idBuild = 0;
            string nameBuild = string.Empty;
            string pathBuild = string.Empty;
            while (reader.Read())
            {
                idBuild = (BuildType)Int32.Parse(reader["type"].ToString());
                nameBuild = reader["name"].ToString().Trim();
                pathBuild = reader["path"].ToString().Trim();
                descList.Add(new BuildDescription(idBuild, nameBuild, pathBuild));
            }
            connect.Close();

            if (descList.Count > 0)
            {
                builds = descList.ToArray();
                return true;
            }
            return false;
        }
        private bool CheckBuilds(BuildDescription[] builds)
        {
            if (!ArrayIsNullOrEmpty(builds))
            {
                Console.WriteLine("Builds is empty!!!");
                return false;
            }
            foreach (BuildDescription val in builds)
            {
                if (val == null)
                {
                    Console.WriteLine("Build is null!!!");
                    return false;
                }
                if (Array.FindIndex(builds, x => x.Type == val.Type) >= 0)
                {
                    Console.WriteLine("Build id {0} is dublicate!!!", val.Type);
                    return false;
                }
                if (string.IsNullOrEmpty(val.Name))
                {
                    Console.WriteLine("Build id {0} is conaince empty name!!!", val.Type);
                    return false;
                }
                if (string.IsNullOrEmpty(val.Path))
                {
                    Console.WriteLine("Build {0} is conaince empty path!!!", val.Type);
                    return false;
                }
            }
            return true;
        }
        private bool GetTurrets(out TurretDescription[] turrets)
        {
            turrets = new TurretDescription[0];
            NpgsqlCommand com = new NpgsqlCommand(SelectTurretsSQL, connect);
            connect.Open();
            NpgsqlDataReader reader = com.ExecuteReader();

            List<TurretDescription> descList = new List<TurretDescription>(10);
            TurretType idTurret = 0;
            string nameTurret = string.Empty;
            int healthTurret = 0;
            int cost1 = 0;
            int cost2 = 0;
            int cost3 = 0;
            int cost4 = 0;
            string pathTurret = string.Empty;
            string pathTurretGhost = string.Empty;
            while (reader.Read())
            {
                idTurret = (TurretType)Int32.Parse(reader["type"].ToString());
                nameTurret = reader["name"].ToString().Trim();
                healthTurret = Int32.Parse(reader["health"].ToString());
                cost1 = Int32.Parse(reader["cost1"].ToString());
                cost2 = Int32.Parse(reader["cost2"].ToString());
                cost3 = Int32.Parse(reader["cost3"].ToString());
                cost4 = Int32.Parse(reader["cost4"].ToString());
                pathTurret = reader["path"].ToString().Trim();
                pathTurretGhost = reader["path_ghost"].ToString().Trim();
                ItemCost costTurret = new ItemCost(cost1, cost2, cost3, cost4);
                descList.Add(new TurretDescription(idTurret, nameTurret, healthTurret, costTurret, pathTurret, pathTurretGhost));
            }
            connect.Close();

            if (descList.Count > 0)
            {
                turrets = descList.ToArray();
                return true;
            }
            return false;
        }
        private bool CheckTurrets(TurretDescription[] turrets)
        {
            if (!ArrayIsNullOrEmpty(turrets))
            {
                Console.WriteLine("Turrets is empty");
                return false;
            }
            foreach (TurretDescription val in turrets)
            {
                if (val == null)
                {
                    Console.WriteLine("Turret is null!!!");
                    return false;
                }
                if (Array.FindIndex(turrets, x => x.Type == val.Type) >= 0)
                {
                    Console.WriteLine("Turret id {0} is dublicate", val.Type);
                    return false;
                }
                if (string.IsNullOrEmpty(val.Name))
                {
                    Console.WriteLine("Turret id {0} is conaince empty name", val.Type);
                    return false;
                }
                if (string.IsNullOrEmpty(val.Path))
                {
                    Console.WriteLine("Turret {0} is conaince empty path", val.Type);
                    return false;
                }
            }
            return true;
        }
        private bool GetRobotChassis(out RobotChassisDescription[] chassis)
        {
            chassis = new RobotChassisDescription[0];
            NpgsqlCommand com = new NpgsqlCommand(SelectRobotChassisSQL, connect);
            connect.Open();
            NpgsqlDataReader reader = com.ExecuteReader();

            List<RobotChassisDescription> descList = new List<RobotChassisDescription>(10);
            int idChassis = 0;
            string nameChassis = string.Empty;
            int healthChassis = 0;
            int cost1 = 0;
            int cost2 = 0;
            int cost3 = 0;
            int cost4 = 0;
            float speedChassis = 0;
            float speedRotationChassis = 0;
            string pathChassis = string.Empty;
            while (reader.Read())
            {
                idChassis = Int32.Parse(reader["id"].ToString());
                nameChassis = reader["name"].ToString().Trim();
                healthChassis = Int32.Parse(reader["health"].ToString());
                cost1 = Int32.Parse(reader["cost1"].ToString());
                cost2 = Int32.Parse(reader["cost2"].ToString());
                cost3 = Int32.Parse(reader["cost3"].ToString());
                cost4 = Int32.Parse(reader["cost4"].ToString());
                speedChassis = float.Parse(reader["speed"].ToString());
                speedRotationChassis = float.Parse(reader["speed_rotation"].ToString());
                pathChassis = reader["path"].ToString().Trim();
                ItemCost costChassis = new ItemCost(cost1, cost2, cost3, cost4);
                descList.Add(new RobotChassisDescription(idChassis, nameChassis, healthChassis, costChassis, speedChassis, speedRotationChassis, pathChassis));
            }
            connect.Close();

            if (descList.Count > 0)
            {
                chassis = descList.ToArray();
                return true;
            }
            return false;
        }
        private bool CheckRobotChassis(RobotChassisDescription[] chassis)
        {
            if (!ArrayIsNullOrEmpty(chassis))
            {
                Console.WriteLine("Chassis is empty");
                return false;
            }
            foreach (RobotChassisDescription val in chassis)
            {
                if (val == null)
                {
                    Console.WriteLine("Chassis is null!!!");
                    return false;
                }
                if (Array.FindIndex(chassis, x => x.Id == val.Id) >= 0)
                {
                    Console.WriteLine("Chassis id {0} is dublicate", val.Id);
                    return false;
                }
                if (string.IsNullOrEmpty(val.Name))
                {
                    Console.WriteLine("Chassis id {0} is conaince empty name", val.Id);
                    return false;
                }
                if (val.Health <= 0)
                {
                    Console.WriteLine("Chassis {0} is conaince Incorect Health {1}", val.Id, val.Health);
                    return false;
                }
                if (!IsValidCost(val.Cost))
                {
                    Console.WriteLine("Chassis {0} is invalid Cost", val.Id);
                    return false;
                }
                if (val.Speed <= 0.0f)
                {
                    Console.WriteLine("Chassis {0} is invalid Speed {1}", val.Id, val.Speed);
                    return false;
                }
                if (val.SpeedRotation <= 0.0f)
                {
                    Console.WriteLine("Chassis {0} is invalid SpeedRotation {1}", val.Id, val.SpeedRotation);
                    return false;
                }
                if (string.IsNullOrEmpty(val.Path))
                {
                    Console.WriteLine("Chassis {0} is conaince empty path", val.Id);
                    return false;
                }
            }
            return true;
        }
        private bool GetRobotBodys(out RobotBodyDescription[] bodys)
        {
            bodys = new RobotBodyDescription[0];
            NpgsqlCommand com = new NpgsqlCommand(SelectRobotBodysSQL, connect);
            connect.Open();
            NpgsqlDataReader reader = com.ExecuteReader();

            List<RobotBodyDescription> descList = new List<RobotBodyDescription>(10);
            int idBody = 0;
            string nameBody = string.Empty;
            int healthBody = 0;
            int cost1 = 0;
            int cost2 = 0;
            int cost3 = 0;
            int cost4 = 0;
            bool special_slot = false;
            string pathBody = string.Empty;
            while (reader.Read())
            {
                idBody = Int32.Parse(reader["id"].ToString());
                nameBody = reader["name"].ToString().Trim();
                healthBody = Int32.Parse(reader["health"].ToString());
                cost1 = Int32.Parse(reader["cost1"].ToString());
                cost2 = Int32.Parse(reader["cost2"].ToString());
                cost3 = Int32.Parse(reader["cost3"].ToString());
                cost4 = Int32.Parse(reader["cost4"].ToString());
                special_slot = bool.Parse(reader["special_slot"].ToString());
                pathBody = reader["path"].ToString().Trim();
                ItemCost costBody = new ItemCost(cost1, cost2, cost3, cost4);

                descList.Add(new RobotBodyDescription(idBody, nameBody, healthBody, costBody, null, special_slot, pathBody));
            }
            connect.Close();

            foreach (RobotBodyDescription body in descList)
            {
                NpgsqlCommand weaponCom = new NpgsqlCommand(string.Format(SelectRobotWeaponPositionsSQL, body.Id), connect);
                connect.Open();
                NpgsqlDataReader weaponReader = weaponCom.ExecuteReader();

                List<Vector> weaponList = new List<Vector>(10);
                float posX = 0;
                float posY = 0;
                float posZ = 0;
                while (weaponReader.Read())
                {
                    posX = float.Parse(weaponReader["pos_x"].ToString());
                    posY = float.Parse(weaponReader["pos_y"].ToString());
                    posZ = float.Parse(weaponReader["pos_z"].ToString());

                    weaponList.Add(new Vector(posX, posY, posZ));
                }
                body.WeaponPositions = weaponList.ToArray();
                connect.Close();
            }

            if (descList.Count > 0)
            {
                bodys = descList.ToArray();
                return true;
            }
            return false;
        }
        private bool CheckRobotBodys(RobotBodyDescription[] bodys)
        {
            if (!ArrayIsNullOrEmpty(bodys))
            {
                Console.WriteLine("Bodys is empty");
                return false;
            }
            foreach (RobotBodyDescription val in bodys)
            {
                if (val == null)
                {
                    Console.WriteLine("Body is null!!!");
                    return false;
                }
                if (Array.FindIndex(bodys, x => x.Id == val.Id) >= 0)
                {
                    Console.WriteLine("Body id {0} is dublicate", val.Id);
                    return false;
                }
                if (string.IsNullOrEmpty(val.Name))
                {
                    Console.WriteLine("Body id {0} is conaince empty name", val.Id);
                    return false;
                }
                if (val.Health <= 0)
                {
                    Console.WriteLine("Body {0} is conaince Incorect Health {1}", val.Id, val.Health);
                    return false;
                }
                if (!IsValidCost(val.Cost))
                {
                    Console.WriteLine("Body {0} is invalid Cost", val.Id);
                    return false;
                }
                if (ArrayIsNullOrEmpty(val.WeaponPositions))
                {
                    Console.WriteLine("Body {0} is invalid WeaponPositions, array is empty", val.Id);
                    return false;
                }
                foreach (Vector pos in val.WeaponPositions)
                {
                    if (pos == null)
                    {
                        Console.WriteLine("Body {0} is invalid WeaponPosition, position is null", val.Id);
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(val.Path))
                {
                    Console.WriteLine("Body {0} is conaince empty path", val.Id);
                    return false;
                }
            }
            return true;
        }
        private bool GetRobotWeapons(out RobotWeaponDescription[] weapons)
        {
            weapons = new RobotWeaponDescription[0];
            NpgsqlCommand com = new NpgsqlCommand(SelectRobotWeaponsSQL, connect);
            connect.Open();
            NpgsqlDataReader reader = com.ExecuteReader();

            List<RobotWeaponDescription> descList = new List<RobotWeaponDescription>(10);
            int idWeapon = 0;
            string nameWeapon = string.Empty;
            int cost1 = 0;
            int cost2 = 0;
            int cost3 = 0;
            int cost4 = 0;
            string pathWeapon = string.Empty;
            while (reader.Read())
            {
                idWeapon = Int32.Parse(reader["id"].ToString());
                nameWeapon = reader["name"].ToString().Trim();
                cost1 = Int32.Parse(reader["cost1"].ToString());
                cost2 = Int32.Parse(reader["cost2"].ToString());
                cost3 = Int32.Parse(reader["cost3"].ToString());
                cost4 = Int32.Parse(reader["cost4"].ToString());
                pathWeapon = reader["path"].ToString().Trim();
                ItemCost costWeapon = new ItemCost(cost1, cost2, cost3, cost4);
                descList.Add(new RobotWeaponDescription(idWeapon, nameWeapon, costWeapon, pathWeapon));
            }
            connect.Close();

            if (descList.Count > 0)
            {
                weapons = descList.ToArray();
                return true;
            }
            return false;
        }
        private bool CheckRobotWeapons(RobotWeaponDescription[] weapons)
        {
            if (!ArrayIsNullOrEmpty(weapons))
            {
                Console.WriteLine("Weapons is empty");
                return false;
            }
            foreach (RobotWeaponDescription val in weapons)
            {
                if (val == null)
                {
                    Console.WriteLine("Weapon is null!!!");
                    return false;
                }
                if (Array.FindIndex(weapons, x => x.Id == val.Id) >= 0)
                {
                    Console.WriteLine("Weapon id {0} is dublicate", val.Id);
                    return false;
                }
                if (string.IsNullOrEmpty(val.Name))
                {
                    Console.WriteLine("Weapon id {0} is conaince empty name", val.Id);
                    return false;
                }
                if (!IsValidCost(val.Cost))
                {
                    Console.WriteLine("Weapon {0} is invalid Cost", val.Id);
                    return false;
                }
                if (string.IsNullOrEmpty(val.Path))
                {
                    Console.WriteLine("Weapon {0} is conaince empty path", val.Id);
                    return false;
                }
            }
            return true;
        }
        private bool GetRobotHeads(out RobotHeadModuleDescription[] heads)
        {
            heads = new RobotHeadModuleDescription[0];
            NpgsqlCommand com = new NpgsqlCommand(SelectRobotHeadsSQL, connect);
            connect.Open();
            NpgsqlDataReader reader = com.ExecuteReader();

            List<RobotHeadModuleDescription> descList = new List<RobotHeadModuleDescription>(10);
            int idHead = 0;
            string nameHead = string.Empty;
            int cost1 = 0;
            int cost2 = 0;
            int cost3 = 0;
            int cost4 = 0;
            string pathHead = string.Empty;
            while (reader.Read())
            {
                idHead = Int32.Parse(reader["id"].ToString());
                nameHead = reader["name"].ToString().Trim();
                cost1 = Int32.Parse(reader["cost1"].ToString());
                cost2 = Int32.Parse(reader["cost2"].ToString());
                cost3 = Int32.Parse(reader["cost3"].ToString());
                cost4 = Int32.Parse(reader["cost4"].ToString());
                pathHead = reader["path"].ToString().Trim();
                ItemCost costHead = new ItemCost(cost1, cost2, cost3, cost4);
                descList.Add(new RobotHeadModuleDescription(idHead, nameHead, costHead, pathHead));
            }
            connect.Close();

            if (descList.Count > 0)
            {
                heads = descList.ToArray();
                return true;
            }
            return false;
        }
        private bool CheckRobotHeads(RobotHeadModuleDescription[] heads)
        {
            if (!ArrayIsNullOrEmpty(heads))
            {
                Console.WriteLine("Heads is empty");
                return false;
            }
            foreach (RobotHeadModuleDescription val in heads)
            {
                if (val == null)
                {
                    Console.WriteLine("Head is null!!!");
                    return false;
                }
                if (Array.FindIndex(heads, x => x.Id == val.Id) >= 0)
                {
                    Console.WriteLine("Head id {0} is dublicate", val.Id);
                    return false;
                }
                if (string.IsNullOrEmpty(val.Name))
                {
                    Console.WriteLine("Head id {0} is conaince empty name", val.Id);
                    return false;
                }
                if (!IsValidCost(val.Cost))
                {
                    Console.WriteLine("Head {0} is invalid Cost", val.Id);
                    return false;
                }
                if (string.IsNullOrEmpty(val.Path))
                {
                    Console.WriteLine("Head {0} is conaince empty path", val.Id);
                    return false;
                }
            }
            return true;
        }
        private bool GetRobotSpecialModules(out RobotSpecialModuleDescription[] modules)
        {
            modules = new RobotSpecialModuleDescription[0];
            NpgsqlCommand com = new NpgsqlCommand(SelectRobotSpecialModulesSQL, connect);
            connect.Open();
            NpgsqlDataReader reader = com.ExecuteReader();

            List<RobotSpecialModuleDescription> descList = new List<RobotSpecialModuleDescription>(10);
            int idModule = 0;
            string nameModule = string.Empty;
            int cost1 = 0;
            int cost2 = 0;
            int cost3 = 0;
            int cost4 = 0;
            string pathModule = string.Empty;
            while (reader.Read())
            {
                idModule = Int32.Parse(reader["id"].ToString());
                nameModule = reader["name"].ToString().Trim();
                cost1 = Int32.Parse(reader["cost1"].ToString());
                cost2 = Int32.Parse(reader["cost2"].ToString());
                cost3 = Int32.Parse(reader["cost3"].ToString());
                cost4 = Int32.Parse(reader["cost4"].ToString());
                pathModule = reader["path"].ToString().Trim();
                ItemCost costModule = new ItemCost(cost1, cost2, cost3, cost4);
                descList.Add(new RobotSpecialModuleDescription(idModule, nameModule, costModule, pathModule));
            }
            connect.Close();

            if (descList.Count > 0)
            {
                modules = descList.ToArray();
                return true;
            }
            return false;
        }
        private bool CheckRobotSpecialModules(RobotSpecialModuleDescription[] modules)
        {
            if (!ArrayIsNullOrEmpty(modules))
            {
                Console.WriteLine("Modules is empty");
                return false;
            }
            foreach (RobotSpecialModuleDescription val in modules)
            {
                if (val == null)
                {
                    Console.WriteLine("Module is null!!!");
                    return false;
                }
                if (Array.FindIndex(modules, x => x.Id == val.Id) >= 0)
                {
                    Console.WriteLine("Module id {0} is dublicate", val.Id);
                    return false;
                }
                if (string.IsNullOrEmpty(val.Name))
                {
                    Console.WriteLine("Module id {0} is conaince empty name", val.Id);
                    return false;
                }
                if (!IsValidCost(val.Cost))
                {
                    Console.WriteLine("Module {0} is invalid Cost", val.Id);
                    return false;
                }
                if (string.IsNullOrEmpty(val.Path))
                {
                    Console.WriteLine("Module {0} is conaince empty path", val.Id);
                    return false;
                }
            }
            return true;
        }
        private bool IsValidCost(ItemCost cost)
        {
            if (cost == null) return false;
            return cost.ResOneCost > 0 || cost.ResTwoCost > 0 || cost.ResThreeCost > 0 || cost.ResFourCost > 0;
        }
        private bool ArrayIsNullOrEmpty<T>(T[] _array)
        {
            return ((_array == null) || _array.Length < 1) ? true : false;
        }
    }
}