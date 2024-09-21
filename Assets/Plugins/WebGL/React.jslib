mergeInto(LibraryManager.library, {
  Quit: function (userName, score) {
    window.dispatchReactUnityEvent("Quit");
  },
});