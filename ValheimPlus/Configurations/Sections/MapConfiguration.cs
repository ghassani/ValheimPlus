﻿namespace ValheimPlus.Configurations.Sections
{
    public class MapConfiguration : BaseConfig<MapConfiguration>
    {
        public bool shareMapProgression { get; internal set; } = false;
        public float exploreRadius { get; internal set; } = 100;
        public bool preventPlayerFromTurningOffPublicPosition { get; internal set; } = false;
        //public bool useImprovedPinEditorUI { get; internal set; } = false;
        public bool shareablePins { get; internal set; } = false;
    }
}
