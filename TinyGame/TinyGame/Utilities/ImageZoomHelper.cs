using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace TinyGame.Utilities
{
	public static class ImageZoomHelper
	{
		private static Point _center;
		private static double _initialScale;

		public static void OnPinchStarted(object sender, PinchStartedGestureEventArgs e)
		{

			// store the initial rotation angle and scaling
			_initialScale = ((CompositeTransform)((Image)sender).RenderTransform).ScaleX;
			// calculate the center for the zooming
			Point firstTouch = e.GetPosition((Image)sender, 0);
			Point secondTouch = e.GetPosition((Image)sender, 1);
			_center = new Point(firstTouch.X + (secondTouch.X - firstTouch.X) / 2.0,
								firstTouch.Y + (secondTouch.Y - firstTouch.Y) / 2.0);
		}

		public static void OnPinchDelta(object sender, PinchGestureEventArgs e)
		{
			if (_initialScale * e.DistanceRatio > 4 || (_initialScale != 1 && e.DistanceRatio == 1) 
                //|| _initialScale * e.DistanceRatio < 1
                )
			{
				return;
			}
			// if its original size then center it back
            //if (e.DistanceRatio <= 1.08)
            //{
            //    ((CompositeTransform)((Image)sender).RenderTransform).CenterY = 0;
            //    ((CompositeTransform)((Image)sender).RenderTransform).CenterY = 0;
            //    ((CompositeTransform)((Image)sender).RenderTransform).TranslateX = 0;
            //    ((CompositeTransform)((Image)sender).RenderTransform).TranslateY = 0;
            //}
			((CompositeTransform)((Image)sender).RenderTransform).CenterX = _center.X;
			((CompositeTransform)((Image)sender).RenderTransform).CenterY = _center.Y;

			// update the rotation and scaling
			if (((PhoneApplicationFrame)Application.Current.RootVisual).Orientation == PageOrientation.Landscape)
			{
			    // when in landscape we need to zoom faster, if not it looks choppy
			    ((CompositeTransform)((Image)sender).RenderTransform).ScaleX = _initialScale * (1 + (e.DistanceRatio - 1) * 2);
			}
			else
			{
				((CompositeTransform)((Image)sender).RenderTransform).ScaleX = _initialScale * e.DistanceRatio;
			}
			((CompositeTransform)((Image)sender).RenderTransform).ScaleY = ((CompositeTransform)((Image)sender).RenderTransform).ScaleX;
		}

		public static void OnDragDelta(object sender, DragDeltaGestureEventArgs e)
		{
			// if is not touch enabled or the scale is different than 1 then don’t allow moving
			if (((CompositeTransform)((Image)sender).RenderTransform).ScaleX <= 1.1)
			{
				return;
			}
			double centerX = ((CompositeTransform)((Image)sender).RenderTransform).CenterX;
			double centerY = ((CompositeTransform)((Image)sender).RenderTransform).CenterY;
			double translateX = ((CompositeTransform)((Image)sender).RenderTransform).TranslateX;
			double translateY = ((CompositeTransform)((Image)sender).RenderTransform).TranslateY;
			double scale = ((CompositeTransform)((Image)sender).RenderTransform).ScaleX;
			double width = ((Image)sender).ActualWidth;
			double height = ((Image)sender).ActualHeight;

			// verify limits to not allow the image to get out of area
			if (centerX - scale * centerX + translateX + e.HorizontalChange < 0 && centerX + scale * (width - centerX) + translateX + e.HorizontalChange > width)
			{
				((CompositeTransform)((Image)sender).RenderTransform).TranslateX += e.HorizontalChange;
			}
			if (centerY - scale * centerY + translateY + e.VerticalChange < 0 && centerY + scale * (height - centerY) + translateY + e.VerticalChange > height)
			{
				((CompositeTransform)((Image)sender).RenderTransform).TranslateY += e.VerticalChange;
			}
			return;
		}
		public static void OnDoubleTap(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
		{
			if (_initialScale == 0.0)
			{
				_initialScale = ((CompositeTransform)((Image)sender).RenderTransform).ScaleX;
			}
			if (((CompositeTransform)((Image)sender).RenderTransform).ScaleX != 1)
			{
				// 
				((CompositeTransform)((Image)sender).RenderTransform).CenterY = 0;
				((CompositeTransform)((Image)sender).RenderTransform).CenterY = 0;
				((CompositeTransform)((Image)sender).RenderTransform).TranslateX = 0;
				((CompositeTransform)((Image)sender).RenderTransform).TranslateY = 0;

				((CompositeTransform)((Image)sender).RenderTransform).ScaleX = 1;
				((CompositeTransform)((Image)sender).RenderTransform).ScaleY = 1;
			}
			else
			{
				// set the center for the zooming
				var zoomFactor = 2;
				_center = e.GetPosition((Image)sender);
				((CompositeTransform)((Image)sender).RenderTransform).CenterX = _center.X;
				((CompositeTransform)((Image)sender).RenderTransform).CenterY = _center.Y;
				// zoom
				((CompositeTransform)((Image)sender).RenderTransform).ScaleX = _initialScale * zoomFactor;
				((CompositeTransform)((Image)sender).RenderTransform).ScaleY = _initialScale * zoomFactor;

			}
		}

	}
}
