﻿using ColossalFramework.UI;
using NetworkSkins.GUI.Abstraction;
using NetworkSkins.Locale;
using NetworkSkins.TranslationFramework;
using UnityEngine;

namespace NetworkSkins.GUI
{
    public class ButtonBar : PanelBase
    {
        public delegate void TreesButtonClickedEventHandler(UIButton focusedButton, UIButton[] buttons);
        public event TreesButtonClickedEventHandler EventTreesClicked;

        public delegate void LightsButtonClickedEventHandler(UIButton focusedButton, UIButton[] buttons);
        public event LightsButtonClickedEventHandler EventLightsClicked;

        public delegate void SurfacesButtonClickedEventHandler(UIButton focusedButton, UIButton[] buttons);
        public event SurfacesButtonClickedEventHandler EventSurfacesClicked;

        public delegate void PillarsButtonClickedEventHandler(UIButton focusedButton, UIButton[] buttons);
        public event PillarsButtonClickedEventHandler EventPillarsClicked;

        public delegate void CatenaryButtonClickedEventHandler(UIButton focusedButton, UIButton[] buttons);
        public event CatenaryButtonClickedEventHandler EventCatenaryClicked;

        public delegate void ColorButtonClickedEventHandler(UIButton focusedButton, UIButton[] buttons);
        public event ColorButtonClickedEventHandler EventColorClicked;

        public delegate void ExtrasButtonClickedEventHandler(UIButton focusedButton, UIButton[] buttons);
        public event ExtrasButtonClickedEventHandler EventExtrasClicked;

        public delegate void PipetteButtonClickedEventHandler(UIButton focusedButton, UIButton[] buttons);
        public event PipetteButtonClickedEventHandler EventPipetteClicked;

        public delegate void ResetButtonClickedEventHandler(UIButton focusedButton, UIButton[] buttons);
        public event ResetButtonClickedEventHandler EventResetClicked;

        public delegate void TreesButtonVisibilityChangedEventHandler(UIButton focusedButton, UIButton[] buttons, bool visible);
        public event TreesButtonVisibilityChangedEventHandler EventTreesVisibilityChanged;

        public delegate void LightsButtonVisibilityChangedEventHandler(UIButton focusedButton, UIButton[] buttons, bool visible);
        public event LightsButtonVisibilityChangedEventHandler EventLightsVisibilityChanged;

        public delegate void SurfacesButtonVisibilityChangedEventHandler(UIButton focusedButton, UIButton[] buttons, bool visible);
        public event SurfacesButtonVisibilityChangedEventHandler EventSurfacesVisibilityChanged;

        public delegate void PillarsButtonVisibilityChangedEventHandler(UIButton focusedButton, UIButton[] buttons, bool visible);
        public event PillarsButtonVisibilityChangedEventHandler EventPillarsVisibilityChanged;

        public delegate void CatenaryButtonVisibilityChangedEventHandler(UIButton focusedButton, UIButton[] buttons, bool visible);
        public event CatenaryButtonVisibilityChangedEventHandler EventCatenaryVisibilityChanged;

        public delegate void ColorButtonVisibilityChangedEventHandler(UIButton focusedButton, UIButton[] buttons, bool visible);
        public event ColorButtonVisibilityChangedEventHandler EventColorVisibilityChanged;

        public delegate void SettingsButtonVisibilityChangedEventHandler(UIButton focusedButton, UIButton[] buttons, bool visible);
        public event SettingsButtonVisibilityChangedEventHandler EventSettingsVisibilityChanged;
        
        private UIButton treesButton;
        private UIButton lightsButton;
        private UIButton surfacesButton;
        private UIButton pillarsButton;
        private UIButton catenaryButton;
        private UIButton colorButton;
        private UIButton settingsButton;
        private UIButton pipetteButton;
        private UIButton resetButton;

        private UIButton[] buttons;

        public override void OnDestroy() {
            treesButton.eventClicked -= OnTreesButtonClicked;
            lightsButton.eventClicked -= OnLightsButtonClicked;
            surfacesButton.eventClicked -= OnSurfacesButtonClicked;
            pillarsButton.eventClicked -= OnPillarsButtonClicked;
            catenaryButton.eventClicked -= OnCatenaryButtonClicked;
            colorButton.eventClicked -= OnColorButtonClicked;
            settingsButton.eventClicked -= OnSettingsButtonClicked;
            resetButton.eventClicked -= OnResetButtonClicked;
            treesButton.eventVisibilityChanged -= OnTreesButtonVisibilityChanged;
            lightsButton.eventVisibilityChanged -= OnLightsButtonVisibilityChanged;
            surfacesButton.eventVisibilityChanged -= OnSurfacesButtonVisibilityChanged;
            pillarsButton.eventVisibilityChanged -= OnPillarsButtonVisibilityChanged;
            catenaryButton.eventVisibilityChanged -= OnCatenaryButtonVisibilityChanged;
            colorButton.eventVisibilityChanged -= OnColorButtonVisibilityChanged;
            settingsButton.eventVisibilityChanged -= OnSettingsButtonVisibilityChanged;
            base.OnDestroy();
        }

        public override void Build(PanelType panelType, Layout layout) {
            base.Build(panelType, layout);
            CreateButtons();
            UIPanel space = AddUIComponent<UIPanel>();
            space.size = new Vector2(width, 0.1f);
            Refresh();
        }

        protected override void RefreshUI(NetInfo netInfo) {
            treesButton.isVisible = NetworkSkinPanelController.TreesEnabled;
            lightsButton.isVisible = NetworkSkinPanelController.StreetLight.Enabled;
            surfacesButton.isVisible = NetworkSkinPanelController.TerrainSurface.Enabled;
            pillarsButton.isVisible = NetworkSkinPanelController.PillarsEnabled;
            catenaryButton.isVisible = NetworkSkinPanelController.Catenary.Enabled;
            colorButton.isVisible = NetworkSkinPanelController.Color.Enabled;
        }

        private void CreateButtons() {
            Vector2 buttonSize = new Vector2(Layout.Size.x - Layout.Spacing * 2, Layout.Size.x - Layout.Spacing * 2);

            surfacesButton = UIUtil.CreateButton(buttonSize, parentComponent: this, backgroundSprite: Resources.Surface, atlas: Resources.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_SURFACE));
            surfacesButton.eventClicked += OnSurfacesButtonClicked;
            surfacesButton.eventVisibilityChanged += OnSurfacesButtonVisibilityChanged;

            colorButton = UIUtil.CreateButton(buttonSize, parentComponent: this, backgroundSprite: Resources.Color, atlas: Resources.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_COLOR));
            colorButton.eventClicked += OnColorButtonClicked;
            colorButton.eventVisibilityChanged += OnColorButtonVisibilityChanged;

            lightsButton = UIUtil.CreateButton(buttonSize, parentComponent: this, backgroundSprite: Resources.Light, atlas: Resources.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_LIGHTS));
            lightsButton.eventClicked += OnLightsButtonClicked;
            lightsButton.eventVisibilityChanged += OnLightsButtonVisibilityChanged;

            catenaryButton = UIUtil.CreateButton(buttonSize, parentComponent: this, backgroundSprite: Resources.Catenary, atlas: Resources.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_CATENARY));
            catenaryButton.eventClicked += OnCatenaryButtonClicked;
            catenaryButton.eventVisibilityChanged += OnCatenaryButtonVisibilityChanged;

            treesButton = UIUtil.CreateButton(buttonSize, parentComponent: this, backgroundSprite: Resources.Tree, atlas: Resources.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_TREES));
            treesButton.eventClicked += OnTreesButtonClicked;
            treesButton.eventVisibilityChanged += OnTreesButtonVisibilityChanged;

            pillarsButton = UIUtil.CreateButton(buttonSize, parentComponent: this, backgroundSprite: Resources.Pillar, atlas: Resources.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_PILLARS));
            pillarsButton.eventClicked += OnPillarsButtonClicked;
            pillarsButton.eventVisibilityChanged += OnPillarsButtonVisibilityChanged;

            resetButton = UIUtil.CreateButton(buttonSize, parentComponent: this, backgroundSprite: Resources.Undo, atlas: Resources.Atlas, isFocusable: false, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_RESETCURRENT));
            resetButton.eventClicked += OnResetButtonClicked;

            UIPanel panel = AddUIComponent<UIPanel>();
            panel.size = new Vector2(30.0f, 4.0f);
            panel.atlas = Resources.DefaultAtlas;
            panel.backgroundSprite = "WhiteRect";
            panel.color = new Color32(53, 54, 54, 255);

            settingsButton = UIUtil.CreateButton(buttonSize, parentComponent: this, backgroundSprite: Resources.Settings, atlas: Resources.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_SETTINGS));
            settingsButton.eventClicked += OnSettingsButtonClicked;
            settingsButton.eventVisibilityChanged += OnSettingsButtonVisibilityChanged;

            pipetteButton = UIUtil.CreateButton(buttonSize, parentComponent: this, backgroundSprite: Resources.Pipette, atlas: Resources.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_PIPETTE));
            pipetteButton.eventClicked += OnPipetteButtonClicked;

            CreateButtonArray();
        }

        private void CreateButtonArray() {
            buttons = new UIButton[] {
                treesButton,
                lightsButton,
                surfacesButton,
                pillarsButton,
                catenaryButton,
                colorButton,
                settingsButton,
                pipetteButton,
                resetButton
            };
        }

        private void OnTreesButtonVisibilityChanged(UIComponent component, bool value) {
            EventTreesVisibilityChanged?.Invoke(component as UIButton, buttons, value);
        }

        private void OnLightsButtonVisibilityChanged(UIComponent component, bool value) {
            EventLightsVisibilityChanged?.Invoke(component as UIButton, buttons, value);
        }

        private void OnSurfacesButtonVisibilityChanged(UIComponent component, bool value) {
            EventSurfacesVisibilityChanged?.Invoke(component as UIButton, buttons, value);
        }

        private void OnPillarsButtonVisibilityChanged(UIComponent component, bool value) {
            EventPillarsVisibilityChanged?.Invoke(component as UIButton, buttons, value);
        }

        private void OnCatenaryButtonVisibilityChanged(UIComponent component, bool value) {
            EventCatenaryVisibilityChanged?.Invoke(component as UIButton, buttons, value);
        }

        private void OnColorButtonVisibilityChanged(UIComponent component, bool value) {
            EventColorVisibilityChanged?.Invoke(component as UIButton, buttons, value);
        }

        private void OnSettingsButtonVisibilityChanged(UIComponent component, bool value) {
            EventSettingsVisibilityChanged?.Invoke(component as UIButton, buttons, value);
        }

        private void OnTreesButtonClicked(UIComponent component, UIMouseEventParameter eventParam) {
            EventTreesClicked?.Invoke(component as UIButton, buttons);
            treesButton.RefreshTooltip();
        }

        private void OnLightsButtonClicked(UIComponent component, UIMouseEventParameter eventParam) {
            EventLightsClicked?.Invoke(component as UIButton, buttons);
            lightsButton.RefreshTooltip();
        }

        private void OnSurfacesButtonClicked(UIComponent component, UIMouseEventParameter eventParam) {
            EventSurfacesClicked?.Invoke(component as UIButton, buttons);
            surfacesButton.RefreshTooltip();
        }

        private void OnPillarsButtonClicked(UIComponent component, UIMouseEventParameter eventParam) {
            EventPillarsClicked?.Invoke(component as UIButton, buttons);
            pillarsButton.RefreshTooltip();
        }

        private void OnCatenaryButtonClicked(UIComponent component, UIMouseEventParameter eventParam) {
            EventCatenaryClicked?.Invoke(component as UIButton, buttons);
            catenaryButton.RefreshTooltip();
        }

        private void OnColorButtonClicked(UIComponent component, UIMouseEventParameter eventParam) {
            EventColorClicked?.Invoke(component as UIButton, buttons);
            colorButton.RefreshTooltip();
        }

        private void OnSettingsButtonClicked(UIComponent component, UIMouseEventParameter eventParam) {
            EventExtrasClicked?.Invoke(component as UIButton, buttons);
            settingsButton.RefreshTooltip();
        }

        private void OnPipetteButtonClicked(UIComponent component, UIMouseEventParameter eventParam) {
            EventPipetteClicked?.Invoke(component as UIButton, buttons);
            UIView.Find("DefaultTooltip")?.Hide();
        }
        private void OnResetButtonClicked(UIComponent component, UIMouseEventParameter eventParam) {
            EventResetClicked?.Invoke(component as UIButton, buttons);
            resetButton.RefreshTooltip();
        }
    }
}