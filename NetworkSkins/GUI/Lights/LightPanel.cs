﻿using ColossalFramework.UI;
namespace NetworkSkins.GUI
{
    public class LightPanel : ListPanelBase<LightList, PropInfo>
    {
        protected override void RefreshUI(NetInfo netInfo) {

        }

        protected override void OnSearchLostFocus() {
        }

        protected override void OnSearchTextChanged(string text) {
        }

        protected override void OnPanelBuilt() {
            tabStrip.isVisible = false;
            RefreshAfterBuild();
        }
    }
}
