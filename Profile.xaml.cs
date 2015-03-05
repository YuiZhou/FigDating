using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace FigDating
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Profile : Page
    {
        public Profile()
        {
            this.InitializeComponent();
            LoadData();
            this.logout.AddHandler(TappedEvent, new TappedEventHandler(Logout), true);
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Logout(object sender, TappedRoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            //Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            localSettings.Values.Remove("loginUsrPwd");
            Application.Current.Exit();
        }

        private void Change_Name(object sender, TappedRoutedEventArgs e)
        {
        }

        private void LoadData() {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            //Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            Windows.Storage.ApplicationDataCompositeValue profile =
   (Windows.Storage.ApplicationDataCompositeValue)localSettings.Values["profile"];

            this.name.Text = (string)profile["name"];
            this.gender.Text = (string)profile["gender"];
            this.age.Text = (string)profile["birth_year"];
            this.group.Text = (string)profile["college"];
            this.grade.Text = (string)profile["grade"];
            this.id.Text = (string)profile["id"];

        }
    }
}
