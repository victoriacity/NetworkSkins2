﻿using ICities;
using System;
using System.Linq;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using UnityEngine;

namespace NetworkSkins.Tests
{
    public class NetworkSkinsTests : IUserMod
    {
        public string Name => "NetworkSkinsTests";
        public string Description => "Integration Tests for Network Skins. Enable to run!";

        public void OnEnabled()
        {
            try
            {
                new SkinSerializationTest().TestSerialization();
                TestUtils.LogTest("Tests Successful!");
            }
            catch (Exception e)
            {
                TestUtils.LogTest("Tests Failed!");
                throw e;
            }
        }
    }
}