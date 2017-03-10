// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using Utils.UnityImplementation;
namespace RobotsRTS.NetWork
{
    public class Constants
    {
        public const int MaxHeathBuild = 9000;
        public const int MaxProductionFactory = 50;
        public const int MaxCountResource = 9000;
    }
    // Client -> Server
    public class NWGameCommand
    {
        public int IdPlayer;
        public TeamId IdTeam;
        public int IdCommad;
        public GameCommandType GameCommandType;
        public NWUnitCommand[] UnitCommands;
        public NWBuildTask[] BuildTasks;
        public int DEBUG_ID;
    }
    #region GameCommand
    public class NWUnitCommand
    {
        /// <summary>
        /// ID Юнита
        /// </summary>
        public int IdUnit;
        /// <summary>
        /// Тип команды, может быть комбинированный
        /// </summary>
        public UnitCommandType Type;
        /// <summary>
        /// Точка назаначения, цель в виде вектора
        /// </summary>
        public NWUnitTarget Target;
    }
    public class NWUnitTarget
    {
        public TargetType Type;
        public UEITransform ServerTransform;
        public int TargetId;
    }
    #endregion
    //-----------------------------------------------

    // Server -> Client
    public class NWGameStateInfo
    {
        public NWTeamInfo[] Teams;
        public NWUnit[] Units;
        public NWBuild[] Builds;
        public NWTurret[] Turrets;
        public NWBullet[] Bullets;
    }

    public class NWTeamResources
    {
        public int ResOne;
        public int ResTwo;
        public int ResThree;
        public int ResFour;
    }
    public class NWUnit
    {
        public int Id;
        public UEITransform Transform;
        public UEIVector3[] PathForTarget;
        public int Health;
        public int ChassisId;
        public int BodyId;
        public int HeadId;
        public NWWeapon[] Weapons;

        public NWUnit()
        {
            Transform = new UEITransform(UEIVector3.zero);
        }
    }
    public class NWWeapon
    {
        public int Id;
        public float Heating;
    }
    public class NWBuildTask
    {
        public int Id;
        public float progress;
        public int BuildId;
        public BuildTaskType Type;
        #region turret
        public TurretType CreateTurretType;
        public int PlatformId;
        #endregion
    }
    public class NWBuild
    {
        public int Id;
        public BuildType Type;
        public bool IsExist;
        public UEITransform Trasnform;
        public int Health;
        public int CaptureProgress;
        public bool IsProduceResource;
        public NWBuildTask[] BuildTasks;
        public NWTurretPlatform[] TurretPlatforms;
        public NWBuild()
        {
            Trasnform = new UEITransform(UEIVector3.zero);
            BuildTasks = new NWBuildTask[0];
            TurretPlatforms = new NWTurretPlatform[0];
        }
    }
    public class NWTurretPlatform
    {
        public UEIVector3 Position;
        public int TurretId;
        public TurretPlatformState PlatformState;
    }
    public class NWTurret
    {
        public int Id;
        public TurretType Type;
        public UEITransform Transform;
        public int Health;
        public TurretState BuildingState;
        public float BuildingProgress;
        public NWTurret()
        {
            Transform = new UEITransform(UEIVector3.zero);
            BuildingState = TurretState.Ready;
            BuildingProgress = 1.0f;
        }
    }
    public class NWBullet
    {
        public int Id;
        public BulletType Type;
        public BulletStateType State;
        public UEIVector3 CurrentPosition;
    }
    public class NWTeamInfo
    {
        /// <summary>
        /// Id Команды
        /// </summary>
        public TeamId Id { get; set; }
        /// <summary>
        /// спискок ID юнитов закрепленных за командой
        /// </summary>
        public int[] Units { get; set; }
        /// <summary>
        /// спискок ID зданий закрепленных за командой
        /// </summary>
        public int[] Builds { get; set; }
        /// <summary>
        /// спискок ID турелей закрепленных за командой
        /// </summary>
        public int[] Turrets { get; set; }
        /// <summary>
        /// Ресурсы доспутные команде
        /// </summary>
        public NWTeamResources Resources { get; set; }
        /// <summary>
        /// Лимит по количеству Юнитов
        /// </summary>
        public int UnitsLimit { get; set; }
    }

    public class NWRule
    {
        public int Id;
        public uint CoutTeams;
    }
}
