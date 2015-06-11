function setVisible(targetControl, visible, timeout) {
    if (visible) {
        targetControl.show(timeout);
        return;
    };

    targetControl.hide(timeout);
};