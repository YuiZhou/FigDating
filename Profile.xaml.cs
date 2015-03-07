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
using FigDating.Common;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Activation;
// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace FigDating
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Profile : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        CoreApplicationView view = CoreApplication.GetCurrentView();

        public Profile()
        {
            this.InitializeComponent();
            LoadData();
            this.logout.AddHandler(TappedEvent, new TappedEventHandler(Logout), true);
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// 获取与此 <see cref="Page"/> 关联的 <see cref="NavigationHelper"/>。
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// 获取此 <see cref="Page"/> 的视图模型。
        /// 可将其更改为强类型视图模型。
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// 使用在导航过程中传递的内容填充页。  在从以前的会话
        /// 重新创建页时，也会提供任何已保存状态。
        /// </summary>
        /// <param name="sender">
        /// 事件的来源; 通常为 <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">事件数据，其中既提供在最初请求此页时传递给
        /// <see cref="Frame.Navigate(Type, Object)"/> 的导航参数，又提供
        /// 此页在以前会话期间保留的状态的
        /// 字典。 首次访问页面时，该状态将为 null。</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// 保留与此页关联的状态，以防挂起应用程序或
        /// 从导航缓存中放弃此页。值必须符合
        /// <see cref="SuspensionManager.SessionState"/> 的序列化要求。
        /// </summary>
        /// <param name="sender">事件的来源；通常为 <see cref="NavigationHelper"/></param>
        ///<param name="e">提供要使用可序列化状态填充的空字典
        ///的事件数据。</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper 注册

        /// <summary>
        /// 此部分中提供的方法只是用于使
        /// NavigationHelper 可响应页面的导航方法。
        /// <para>
        /// 应将页面特有的逻辑放入用于
        /// <see cref="NavigationHelper.LoadState"/>
        /// 和 <see cref="NavigationHelper.SaveState"/> 的事件处理程序中。
        /// 除了在会话期间保留的页面状态之外
        /// LoadState 方法中还提供导航参数。
        /// </para>
        /// </summary>
        /// <param name="e">提供导航方法数据和
        /// 无法取消导航请求的事件处理程序。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void Logout(object sender, TappedRoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            //Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            localSettings.Values.Remove("loginUsrPwd");
            localSettings.Values.Remove("profile");
            Application.Current.Exit();
        }

        private void Change_Name(object sender, TappedRoutedEventArgs e)
        {
        }

        private void LoadData()
        {
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

        private void uploadThumb(object sender, RoutedEventArgs e)
        {
            FileOpenPicker opener = new FileOpenPicker();
            opener.ViewMode = PickerViewMode.Thumbnail;
            opener.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            opener.FileTypeFilter.Add(".jpg");
            opener.FileTypeFilter.Add(".jpeg");
            opener.FileTypeFilter.Add(".png");
            opener.PickSingleFileAndContinue();
            view.Activated += viewActivated;
            //StorageFile file = await opener.PickSingleFileAsync();
            //if (file != null)
            //{
            //    // We've now got the file. Do something with it.
            //    //var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            //    //var bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
            //    //await bitmapImage.SetSourceAsync(stream);
            //    //bustin.Source = bitmapImage;
            //    //var decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream);
            //}
            //else
            //{
            //    //OutputTextBlock.Text = "The operation may have been cancelled.";
            //}
        }

        private void viewActivated(CoreApplicationView sender, IActivatedEventArgs args1)
        {
            FileOpenPickerContinuationEventArgs args = args1 as FileOpenPickerContinuationEventArgs;

            if (args != null)
            {
                if (args.Files.Count == 0) return;

                view.Activated -= viewActivated;
                StorageFile SelectedImageFile = args.Files[0];

                if (SelectedImageFile != null) {
                    User user = User.getUser();
                    user.uploadPhoto(SelectedImageFile);
                }

            }
        }
    }
}
