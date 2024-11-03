using Microlight.MicroEditor;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Microlight.MicroBar {
    // ****************************************************************************************************
    // For MicroBar menu in editor or right clicking in hierarchy
    // ****************************************************************************************************
    internal class MicroBar_EditorMenu : Editor {

        static string GetPrefabsFolder() {
            return MicroEditor_AssetUtility.FindFolderRecursively("Assets", "MicroBar") + "/Prefabs";
        }
        static void InstantiateBar(GameObject bar) {
            bar = Instantiate(bar);   // Instantiate
            bar.name = "HealthBar";   // Change name
            if(Selection.activeGameObject != null) {   // Make child if some object is selected
                bar.transform.SetParent(Selection.activeGameObject.transform, false);
            }
        }

        #region Image Bars
        // BLANK ##################################################
        [MenuItem("GameObject/Microlight/MicroBar/Blank (Image)", false, 10)]
        private static void AddBlankImageBar() {
            GameObject go = MicroEditor_AssetUtility.GetPrefab($"{GetPrefabsFolder()}/ImageBars", "Blank_ImageMicroBar");
            if(go == null) return;
            InstantiateBar(go);
        }
        // SIMPLE ##################################################
        [MenuItem("GameObject/Microlight/MicroBar/Simple (Image)", false, 20)]
        private static void AddSimpleImageBar() {
            GameObject go = MicroEditor_AssetUtility.GetPrefab($"{GetPrefabsFolder()}/ImageBars", "Simple_ImageMicroBar");
            if(go == null) return;
            InstantiateBar(go);
        }
        // DELAYED ##################################################
        [MenuItem("GameObject/Microlight/MicroBar/Delayed (Image)", false, 30)]
        private static void AddDelayedImageBar() {
            GameObject go = MicroEditor_AssetUtility.GetPrefab($"{GetPrefabsFolder()}/ImageBars", "Delayed_ImageMicroBar");
            if(go == null) return;
            InstantiateBar(go);
        }
        // DISAPPEAR ##################################################
        [MenuItem("GameObject/Microlight/MicroBar/Disappear (Image)", false, 40)]
        private static void AddDisappearImageBar() {
            GameObject go = MicroEditor_AssetUtility.GetPrefab($"{GetPrefabsFolder()}/ImageBars", "Disappear_ImageMicroBar");
            if(go == null) return;
            InstantiateBar(go);
        }
        // IMPACT ##################################################
        [MenuItem("GameObject/Microlight/MicroBar/Impact (Image)", false, 50)]
        private static void AddImpactImageBar() {
            GameObject go = MicroEditor_AssetUtility.GetPrefab($"{GetPrefabsFolder()}/ImageBars", "Impact_ImageMicroBar");
            if(go == null) return;
            InstantiateBar(go);
        }
        // PUNCH ##################################################
        [MenuItem("GameObject/Microlight/MicroBar/Punch (Image)", false, 60)]
        private static void AddPunchImageBar() {
            GameObject go = MicroEditor_AssetUtility.GetPrefab($"{GetPrefabsFolder()}/ImageBars", "Punch_ImageMicroBar");
            if(go == null) return;
            InstantiateBar(go);
        }
        // SHAKE ##################################################
        [MenuItem("GameObject/Microlight/MicroBar/Shake (Image)", false, 70)]
        private static void AddShakeImageBar() {
            GameObject go = MicroEditor_AssetUtility.GetPrefab($"{GetPrefabsFolder()}/ImageBars", "Shake_ImageMicroBar");
            if(go == null) return;
            InstantiateBar(go);
        }
        #endregion

        #region Sprite Bars
        // BLANK ##################################################
        [MenuItem("GameObject/Microlight/MicroBar/Blank (Sprite)", false, 11)]
        private static void AddBlankSpriteBar() {
            GameObject go = MicroEditor_AssetUtility.GetPrefab($"{GetPrefabsFolder()}/SpriteBars", "Blank_SpriteMicroBar");
            if(go == null) return;
            InstantiateBar(go);
        }
        // SIMPLE ##################################################
        [MenuItem("GameObject/Microlight/MicroBar/Simple (Sprite)", false, 21)]
        private static void AddSimpleSpriteBar() {
            GameObject go = MicroEditor_AssetUtility.GetPrefab($"{GetPrefabsFolder()}/SpriteBars", "Simple_SpriteMicroBar");
            if(go == null) return;
            InstantiateBar(go);
        }
        // DELAYED ##################################################
        [MenuItem("GameObject/Microlight/MicroBar/Delayed (Sprite)", false, 31)]
        private static void AddDelayedSpriteBar() {
            GameObject go = MicroEditor_AssetUtility.GetPrefab($"{GetPrefabsFolder()}/SpriteBars", "Delayed_SpriteMicroBar");
            if(go == null) return;
            InstantiateBar(go);
        }
        // DISAPPEAR ##################################################
        [MenuItem("GameObject/Microlight/MicroBar/Disappear (Sprite)", false, 41)]
        private static void AddDisappearSpriteBar() {
            GameObject go = MicroEditor_AssetUtility.GetPrefab($"{GetPrefabsFolder()}/SpriteBars", "Disappear_SpriteMicroBar");
            if(go == null) return;
            InstantiateBar(go);
        }
        // IMPACT ##################################################
        [MenuItem("GameObject/Microlight/MicroBar/Impact (Sprite)", false, 51)]
        private static void AddImpactSpriteBar() {
            GameObject go = MicroEditor_AssetUtility.GetPrefab($"{GetPrefabsFolder()}/SpriteBars", "Impact_SpriteMicroBar");
            if(go == null) return;
            InstantiateBar(go);
        }
        // PUNCH ##################################################
        [MenuItem("GameObject/Microlight/MicroBar/Punch (Sprite)", false, 61)]
        private static void AddPunchSpriteBar() {
            GameObject go = MicroEditor_AssetUtility.GetPrefab($"{GetPrefabsFolder()}/SpriteBars", "Punch_SpriteMicroBar");
            if(go == null) return;
            InstantiateBar(go);
        }
        // SHAKE ##################################################
        [MenuItem("GameObject/Microlight/MicroBar/Shake (Sprite)", false, 71)]
        private static void AddShakeSpriteBar() {
            GameObject go = MicroEditor_AssetUtility.GetPrefab($"{GetPrefabsFolder()}/SpriteBars", "Shake_SpriteMicroBar");
            if(go == null) return;
            InstantiateBar(go);
        }
        #endregion

        #region Validators
        [MenuItem("GameObject/Microlight/MicroBar/Blank (Image)", true, 10)]
        [MenuItem("GameObject/Microlight/MicroBar/Simple (Image)", true, 20)]
        [MenuItem("GameObject/Microlight/MicroBar/Delayed (Image)", true, 30)]
        [MenuItem("GameObject/Microlight/MicroBar/Disappear (Image)", true, 40)]
        [MenuItem("GameObject/Microlight/MicroBar/Impact (Image)", true, 50)]
        [MenuItem("GameObject/Microlight/MicroBar/Punch (Image)", true, 60)]
        [MenuItem("GameObject/Microlight/MicroBar/Shake (Image)", true, 70)]
        private static bool AddImageBar_Validate() {
            return Selection.activeGameObject && Selection.activeGameObject.GetComponentInParent<Canvas>();
        }

        [MenuItem("GameObject/Microlight/MicroBar/Blank (Sprite)", true, 11)]
        [MenuItem("GameObject/Microlight/MicroBar/Simple (Sprite)", true, 21)]
        [MenuItem("GameObject/Microlight/MicroBar/Delayed (Sprite)", true, 31)]
        [MenuItem("GameObject/Microlight/MicroBar/Disappear (Sprite)", true, 41)]
        [MenuItem("GameObject/Microlight/MicroBar/Impact (Sprite)", true, 51)]
        [MenuItem("GameObject/Microlight/MicroBar/Punch (Sprite)", true, 61)]
        [MenuItem("GameObject/Microlight/MicroBar/Shake (Sprite)", true, 71)]
        private static bool AddSpriteBar_Validate() {
            if(Selection.activeGameObject == null) return true;
            bool hasCanvas = Selection.activeGameObject.GetComponentInParent<Canvas>() != null;
            return !hasCanvas;
        }
        #endregion
    }
}
#endif