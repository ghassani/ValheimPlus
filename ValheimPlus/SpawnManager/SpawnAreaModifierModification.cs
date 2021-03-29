using System;
using ValheimPlus.Utility;

namespace ValheimPlus
{
    [Serializable]
    public class SpawnAreaModifierModification : IZPackageable
    {
        public float? SpawnTimer;
        public bool? OnGroundOnly;
        public int? MaxTotal;
        public int? MaxNear;
        public float? FarRadius;
        public float? SpawnRadius;
        public float? TriggerDistance;
        public float? SpawnIntervalSec;
        public float? LevelupChance;
        public float? NearRadius;

        public SpawnAreaModifierModification()
        {

        }
        public void Apply(SpawnArea area)
        {
            if ( SpawnTimer != null )       area.m_spawnTimer       = SpawnTimer.Value;
            if ( OnGroundOnly != null )     area.m_onGroundOnly     = OnGroundOnly.Value;
            if ( MaxTotal != null )         area.m_maxTotal         = MaxTotal.Value;
            if ( FarRadius != null )        area.m_farRadius        = FarRadius.Value;
            if ( SpawnRadius != null )      area.m_spawnRadius      = SpawnRadius.Value;
            if ( TriggerDistance != null )  area.m_triggerDistance  = TriggerDistance.Value;
            if ( SpawnIntervalSec != null ) area.m_spawnIntervalSec = SpawnIntervalSec.Value;
            if ( LevelupChance != null )    area.m_levelupChance    = LevelupChance.Value;
            if ( NearRadius != null )       area.m_nearRadius       = NearRadius.Value;
        }

        /// <summary>
        /// Serialize the object into a ZPackage
        /// </summary>
        /// <param name="package"></param>
        public void Serialize(ZPackage package)
        {
            package.WriteNullable(SpawnTimer);
            package.WriteNullable(OnGroundOnly);
            package.WriteNullable(MaxTotal);
            package.WriteNullable(MaxNear);
            package.WriteNullable(FarRadius);
            package.WriteNullable(SpawnRadius);
            package.WriteNullable(TriggerDistance);
            package.WriteNullable(SpawnIntervalSec);
            package.WriteNullable(LevelupChance);
            package.WriteNullable(NearRadius);
        }
        
        /// <summary>
        /// Unserialize from a ZPackage into this object
        /// </summary>
        /// <param name="package"></param>
        public void Unserialize(ZPackage package)
        {            
            SpawnTimer          = package.ReadNullableSingle();
            OnGroundOnly        = package.ReadNullableBool();
            MaxTotal            = package.ReadNullableInt();
            MaxNear             = package.ReadNullableInt();
            FarRadius           = package.ReadNullableSingle();
            SpawnRadius         = package.ReadNullableSingle();
            TriggerDistance     = package.ReadNullableSingle();
            SpawnIntervalSec    = package.ReadNullableSingle();
            LevelupChance       = package.ReadNullableSingle();
            NearRadius          = package.ReadNullableSingle();
        }
    }
}
