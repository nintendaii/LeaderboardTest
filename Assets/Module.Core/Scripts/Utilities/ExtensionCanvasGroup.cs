namespace Module.Core.Utilities {
    public static class ExtensionCanvasGroup {
        public static void SetActive(this UnityEngine.CanvasGroup canvasGroup, bool isActive) {
            canvasGroup.alpha = isActive ? 1f : 0f;
            canvasGroup.interactable = isActive;
            canvasGroup.blocksRaycasts = isActive;
        }

        public static void SetActive(this UnityEngine.CanvasGroup canvasGroup, bool isActive, bool isInteractable) {
            canvasGroup.SetActive(isActive);
            if (isActive) {
                canvasGroup.interactable = isInteractable;
                canvasGroup.blocksRaycasts = isInteractable;
            }
        }
    }
}