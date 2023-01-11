﻿using CoreGraphics;

using Mopups.Pages;

using UIKit;

namespace Mopups.Platforms.iOS
{
    internal class PopupWindow : UIWindow
    {
        public PopupWindow(IntPtr handle) : base(handle)
        {
        }

        public PopupWindow()
        {

        }

        public PopupWindow(UIWindowScene uiWindowScene) : base(uiWindowScene)
        {

        }

        public override UIView HitTest(CGPoint point, UIEvent? uievent)
        {
            var platformHandler = (PopupPageRenderer?)RootViewController;
            var renderer = platformHandler?.Handler;
            var hitTestResult = base.HitTest(point, uievent);

            if (!(platformHandler?.Handler?.VirtualView is PopupPage formsElement))
                return hitTestResult;

            if (formsElement.InputTransparent)
                return null!;

            if ((formsElement.BackgroundInputTransparent || formsElement.CloseWhenBackgroundIsClicked) && renderer?.PlatformView == hitTestResult)
            {
                formsElement.SendBackgroundClick();
                return null!;
            }

            return hitTestResult;
        }
    }
}
