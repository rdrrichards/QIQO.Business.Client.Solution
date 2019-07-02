﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace QIQO.Custom.Controls
{
    public class QIQOContentControl : HeaderedContentControl
    {
        static QIQOContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QIQOContentControl), new FrameworkPropertyMetadata(typeof(QIQOContentControl)));
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            ChangeContent(newContent);
        }

        public void ChangeContent(object new_content)
        {
            if (new_content == null)
            {
                return;
            }

            try
            {
                var header_element = Template.FindName("HeaderContent", this) as ContentPresenter;
                var content_element = Template.FindName("MainContent", this) as ContentPresenter;
                var header_transform = Template.FindName("HeaderTranslateTransform", this) as TranslateTransform;
                var content_transform = Template.FindName("ContentTranslateTransform", this) as TranslateTransform;
                double horz_view_trans_amount = 50;
                content_transform.X = horz_view_trans_amount;
                Content = new_content;

                var sb = new Storyboard();

                var content_anim_x = new DoubleAnimation()
                {
                    From = horz_view_trans_amount,
                    To = 0,
                    Duration = new TimeSpan(0, 0, 0, 1, 500),
                    BeginTime = new TimeSpan(0, 0, 0, 0, 100),
                    EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut }
                };
                Storyboard.SetTarget(content_anim_x, content_element);
                Storyboard.SetTargetProperty(content_anim_x, new PropertyPath("RenderTransform.X"));

                var content_anim_o = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = new TimeSpan(0, 0, 0, 1),
                    //EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut }
                };
                Storyboard.SetTarget(content_anim_o, content_element);
                Storyboard.SetTargetProperty(content_anim_o, new PropertyPath("Opacity"));

                //sb.Children.Add(header_anim_x);
                //sb.Children.Add(header_anim_o);
                sb.Children.Add(content_anim_x);
                sb.Children.Add(content_anim_o);

                sb.Begin(Content as FrameworkElement);

            }
            catch (Exception)
            {
                // throw;
            }

        }
    }
}
