﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace NetworkSkins.Net
{
    public static class StreetLightUtils
    {
        private const string AmericanSignReplacerWorkshopId = "1523608557";
        public static List<PropInfo> GetAvailableStreetLights()
        {
            var streetLights = new List<PropInfo>();

            var prefabCount = PrefabCollection<PropInfo>.LoadedCount();
            for (uint prefabIndex = 0; prefabIndex < prefabCount; prefabIndex++)
            {
                var prefab = PrefabCollection<PropInfo>.GetLoaded(prefabIndex);
                if (IsStreetLightProp(prefab))
                {
                    streetLights.Add(prefab);
                }
            }

            streetLights.Sort((a, b) => string.Compare(a.GetUncheckedLocalizedTitle(), b.GetUncheckedLocalizedTitle(), StringComparison.Ordinal));

            return streetLights;
        }

        [CanBeNull]
        public static PropInfo GetDefaultStreetLight(NetInfo prefab)
        {
            return NetUtils.GetMatchingLaneProp(prefab, laneProp => IsStreetLightProp(laneProp.m_finalProp))?.m_finalProp;
        }

        public static float GetDefaultRepeatDistance(NetInfo prefab)
        {
            return NetUtils.GetMatchingLaneProp(prefab, laneProp => IsStreetLightProp(laneProp.m_finalProp))?.m_repeatDistance ?? 40f;
        }

        public static bool IsStreetLightProp(PropInfo prefab)
        {
            if (prefab == null) return false;

            if (prefab.m_class.m_service == ItemClass.Service.Road ||
                prefab.m_class.m_subService == ItemClass.SubService.PublicTransportPlane ||
                prefab.name.ToLower().Contains("streetlamp") || prefab.name.ToLower().Contains("streetlight") || prefab.name.ToLower().Contains("lantern"))
            {
                if (prefab.m_effects != null && prefab.m_effects.Length > 0)
                {
                    if (prefab.name.ToLower().Contains("taxiway")) return false;
                    if (prefab.name.ToLower().Contains("runway")) return false;

                    // American highway signs with integrated lights are NOT street lights!
                    if (prefab.name.StartsWith(AmericanSignReplacerWorkshopId)) return false;

                    foreach (var effect in prefab.m_effects)
                    {
                        if (effect.m_effect is LightEffect)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public static void CorrectStreetLightPropAngleAndPosition(NetLaneProps.Prop laneProp, float halfWidth, float lanePosition)
        {
            // Rotate street lights standing on left side of pedestrian paths
            float propX = laneProp.m_position.x + lanePosition;
            if(propX < 0 && halfWidth + propX < 3f)
            {
                laneProp.m_angle = 180;
            }
        }
    }
}
