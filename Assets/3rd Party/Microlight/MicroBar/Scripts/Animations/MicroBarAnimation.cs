using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Microlight.MicroBar {
    // ****************************************************************************************************
    // Animation which holds commands on how should animation behave
    // Triggered by UpdateAnim definition in the bar update
    // *Class must be public because it needs to be used in editor scripts
    // ****************************************************************************************************
    [System.Serializable]
    public class MicroBarAnimation {
        [SerializeField] UpdateAnim animationType;
        [SerializeField] RenderType renderType;
        [SerializeField] Image targetImage;
        [SerializeField] SpriteRenderer targetSprite;
        [SerializeField] bool notBar;   // If turned on, when animation is skipped, will not fill image/sprite
        [SerializeField] List<AnimCommand> commands;

        MicroBar parentBar;
        GraphicsDefaultValues defaultValues;
        internal GraphicsDefaultValues DefaultValues => defaultValues;
        internal bool NotBar => notBar;

        Sequence sequence;   // Sequence for animations
        public Sequence Sequence => sequence;

        internal bool Initialize(MicroBar bar) {
            if(renderType == RenderType.Image && targetImage == null) {
                return false;
            }
            if(renderType == RenderType.Sprite && targetSprite == null) {
                return false;
            }

            parentBar = bar;
            bar.OnBarUpdate += Update;
            bar.BarDestroyed += Destroy;
            bar.OnDefaultValuesSnapshot += DefaultValuesSnapshot;

            DefaultValuesSnapshot();

            return true;
        }
        // When bar is updated, decide what this animation should do
        void Update(bool skipAnimation, UpdateAnim animationType) {
            // Always kill when bar is updating, because we dont want to have for example active damage animation if heal animation is active
            if(sequence.IsActive()) {
                sequence.Kill();
                sequence = null;
            }
            if(animationType != this.animationType) return;
            if(skipAnimation) {
                if(!notBar) {
                    SilentUpdate();
                }
                return;
            }

            if(renderType == RenderType.Image) {
                AnimationInfo animInfo = new AnimationInfo(commands, targetImage, parentBar, this);
                sequence = AnimBuilder.BuildAnimation(animInfo);
            }
            else {
                AnimationInfo animInfo = new AnimationInfo(commands, targetSprite, parentBar, this);
                sequence = AnimBuilder.BuildAnimation(animInfo);
            }
        }
        internal void DefaultValuesSnapshot() {
            if(renderType == RenderType.Image && targetImage != null) {
                defaultValues = new GraphicsDefaultValues(targetImage);
            }
            else if(renderType == RenderType.Sprite && targetSprite != null) {
                defaultValues = new GraphicsDefaultValues(targetSprite);
            }
        }
        // When animation is skipped, bar can be updated silently
        void SilentUpdate() {
            if(parentBar == null) {
                Debugger.MissingBarReference();
                return;
            }
            if(renderType == RenderType.Image && targetImage != null) {
                targetImage.fillAmount = parentBar.HPPercent;
            }
            else if(renderType == RenderType.Sprite && targetSprite != null) {
                targetSprite.transform.localScale = new Vector3(parentBar.HPPercent, targetSprite.transform.localScale.y, targetSprite.transform.localScale.z);
            }
            Debugger.SilentUpdate();
        }
        // When health bar is being destroyed
        void Destroy() {
            if(sequence.IsActive()) {
                sequence.Kill();
                sequence = null;
            }
        }
    }
}