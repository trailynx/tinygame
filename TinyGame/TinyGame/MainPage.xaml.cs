using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using TinyGame.Utilities;

namespace TinyGame
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnPinchStarted(object sender, PinchStartedGestureEventArgs e)
        {
            ImageZoomHelper.OnPinchStarted(sender, e);
        }

        private void OnPinchDelta(object sender, PinchGestureEventArgs e)
        {
            ImageZoomHelper.OnPinchDelta(sender, e);
        }

        private void OnDragDelta(object sender, DragDeltaGestureEventArgs e)
        {
            if (((CompositeTransform)((Image)sender).RenderTransform).ScaleX <= 1.1)
            {
                return;
            }
            ImageZoomHelper.OnDragDelta(sender, e);
        }


        private void OnDoubleTap(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            ImageZoomHelper.OnDoubleTap(sender, e);
        }

        private void OnPinchCompleted(object sender, PinchGestureEventArgs e)
        {
            MessageBox.Show(String.Format("Height={0}, \nWidth={1}", Math.Round(((CompositeTransform)((Image)sender).RenderTransform).ScaleX * ((Image)sender).ActualWidth), Math.Round(((CompositeTransform)((Image)sender).RenderTransform).ScaleY * ((Image)sender).ActualHeight)));
            //reset image
            ImageZoomHelper.OnDoubleTap(sender, e);
        }
    }
}