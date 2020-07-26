using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Notification.Wpf.Utils
{
    internal class VisualTreeHelperExtensions
    {
        private static List<Visual> _ActiveControls = new List<Visual>();

        public static T GetParent<T>(DependencyObject child) where T : DependencyObject => VisualTreeHelper.GetParent(child) is { } parent ? parent as T ?? GetParent<T>(parent) : null;

        public static int GetActiveNotificationCount(Visual element)
        {
            if (element is null)
                throw new ArgumentNullException(nameof(element));

            _ActiveControls.Clear();

            GetControlsList(element, 0);

            var count = _ActiveControls.Count(x => x.GetType().Name.Equals("Notification"));

            return count;
        }

        private static void GetControlsList(Visual control, int level)
        {
            var ChildNumber = VisualTreeHelper.GetChildrenCount(control);

            for (var i = 0; i <= ChildNumber - 1; i++)
            {
                var child = (Visual)VisualTreeHelper.GetChild(control, i);

                _ActiveControls.Add(child);

                if (VisualTreeHelper.GetChildrenCount(child) > 0) 
                    GetControlsList(child, level + 1);
            }
        }

    }
}
