using Foundation;
using System;
using UIKit;

namespace Cloudy
{
    public partial class SettingsViewController : UITableViewController
    {
        public event EventHandler SettingsUpdated;

        public SettingsViewController (IntPtr handle) : base (handle)
        {
            SettingsUpdated += OnSettingsUpdated;
        }

        private void OnSettingsUpdated(object sender, EventArgs e)
        {
            SettingsTableView.ReloadData();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            SettingsTableView.Source = new SettingsTableViewSource(SettingsUpdated);
        }

        private void SetupView()
        {
            SettingsTableView.SeparatorInset = UIEdgeInsets.Zero;
        }
    }
}