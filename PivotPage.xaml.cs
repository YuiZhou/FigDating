using FigDating.Common;
using FigDating.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Phone.UI.Input;
using System.Diagnostics;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;

// “透视应用程序”模板在 http://go.microsoft.com/fwlink/?LinkID=391641 上有介绍

namespace FigDating
{
    public class PathData
    {
        public string XAMLContent { get; set; }
    }

    public sealed partial class PivotPage : Page
    {
        
        private const string FirstGroupName = "FirstGroup";
        private const string SecondGroupName = "SecondGroup";

        private readonly NavigationHelper navigationHelper;
        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();
        private readonly ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView("Resources");

        private static  bool srcLoaded = true;
        public static void setsrcLoaded() { srcLoaded = true; }
        
        public PivotPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            login();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            this.ContentPivot.SelectionChanged += pivot_SelectionChanged;
            Profile_Loaded();
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
        /// 使用在导航过程中传递的内容填充页。在从以前的会话
        /// 重新创建页时，也会提供任何已保存状态。
        /// </summary>
        /// <param name="sender">
        /// 事件的来源；通常为 <see cref="NavigationHelper"/>。
        /// </param>
        /// <param name="e">事件数据，其中既提供在最初请求此页时传递给
        /// <see cref="Frame.Navigate(Type, Object)"/> 的导航参数，又提供
        /// 此页在以前会话期间保留的状态的
        /// 的字典。首次访问页面时，该状态将为 null。</param>
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: 创建适用于问题域的合适数据模型以替换示例数据
            if (!srcLoaded) {
                return;
            }
            srcLoaded = false;
            var sampleDataGroup = await SampleDataSource.GetGroupAsync("Group-1");
            if (sampleDataGroup != null)
                this.DefaultViewModel[FirstGroupName] = sampleDataGroup;
            else { 
                // 没有网络连接
                MessageDialog messageDialog = new MessageDialog("没有网络连接");
                await messageDialog.ShowAsync();

            }
        }

        /// <summary>
        /// 保留与此页关联的状态，以防挂起应用程序或
        /// 从导航缓存中放弃此页。值必须符合序列化
        /// <see cref="SuspensionManager.SessionState"/> 的序列化要求。
        /// </summary>
        /// <param name="sender">事件的来源；通常为 <see cref="NavigationHelper"/>。</param>
        ///<param name="e">提供要使用可序列化状态填充的空字典
        ///的事件数据。</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            // TODO: 在此处保存页面的唯一状态。
        }

        private void Notification_ItemClick(object sender, ItemClickEventArgs e) {
            var itemID = ((CommmentDataItem)e.ClickedItem).Id;
            Frame.Navigate(typeof(ItemPage), itemID);
        }
        /// <summary>
        /// 在单击节内的项时调用。
        /// </summary>
        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 导航至相应的目标页，并
            // 通过将所需信息作为导航参数传入来配置新页
            // 判断是否已经expand这个apppointment
            var parentContainer = this.indexList.ContainerFromItem(e.ClickedItem);
            var unlock = FindGrid(parentContainer, "unlockView");
            if (unlock.Visibility != Visibility.Collapsed) { return; }

            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            if (!Frame.Navigate(typeof(ItemPage),itemId))
            {
                throw new Exception(this.resourceLoader.GetString("NavigationFailedExceptionMessage"));
            }
        }

        /// <summary>
        /// 滚动到视图中后，为第二个数据透视项加载内容。
        /// </summary>
        private async void SecondPivot_Loaded(object sender, RoutedEventArgs e)
        {
            var sampleDataGroup = await SampleDataSource.GetGroupAsync("Group-2");
            if (sampleDataGroup != null)
                this.DefaultViewModel[SecondGroupName] = sampleDataGroup;
            else {
                MessageDialog messageDialog = new MessageDialog("没有网络连接");
                await messageDialog.ShowAsync();
            }
        }

        private async void Notification_Loaded(object sender, RoutedEventArgs e)
        {
            var commentDataGroup = await CommentSrc.GetNoteAsync();
            this.DefaultViewModel["Notification"] = commentDataGroup;

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

            // 后退键退出app
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += quitApp;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        #region 软件逻辑
        private void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.applicationBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            switch (this.ContentPivot.SelectedIndex)
            {
                case 0:
                    this.applicationBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    this.title.Text = "新的约会";
                    if (srcLoaded) {
                        srcLoaded = false;
                        NavigationHelper_LoadState(null,null);
                    }
                    break;
                case 1:
                    this.title.Text = "猜你喜欢";
                    break;
                case 2:
                    this.title.Text = "通知";
                    break;
                case 3:
                    Profile_Loaded();
                    this.title.Text = "更多";
                    break;
            }
        }

        private void change_profile(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Profile));

        }

        private async void unlockView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // 判断剩余机会
            User user = User.getUser();
            int chance = user.useChance();
            if (chance < 0)
            {
                MessageDialog messageDialog = new MessageDialog("没有足够的机会查看别人的权限哦");
                await messageDialog.ShowAsync();
                return;
            }
            this.chance.Text = chance.ToString();

            expandListView(sender, e);
            
           // Frame.GoBack();
        }

        private void expandListView(object sender, TappedRoutedEventArgs e)
        {
            var itemContext = (sender as FrameworkElement).DataContext;
            int index = this.indexList.Items.IndexOf(itemContext);

            //Debug.WriteLine("###\n\n\n\n"+index+"\n\n\n##");

            //ItemsControl


            var parentContainer = this.indexList.ContainerFromIndex(index);//.ContainerFromItem(this.indexList.SelectedItem);
            //var unlockView = FindImage(parentContainer, "datingView");
            //var commentView = FindControl<TextBox>(parentContainer, "states");
            var containerView = FindGrid(parentContainer, "containerView");
            containerView.Height = 200;
            var contentView = FindTextBlock(parentContainer, "contentView");
            contentView.Visibility = Visibility.Visible;
            var unlock = FindGrid(parentContainer, "unlockView");
            unlock.Visibility = Visibility.Collapsed;
            var comment = FindGrid(parentContainer, "commentView");
            comment.Visibility = Visibility.Visible;


            //suppose you want to change the visibility
            //unlockView.Visibility = Visibility.Visible;
            //commentView.Visibility = Visibility.Collapsed;

        }

        private void Profile_Loaded()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            //Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            Windows.Storage.ApplicationDataCompositeValue profile =
   (Windows.Storage.ApplicationDataCompositeValue)localSettings.Values["profile"];

            this.username.Text = (string)profile["name"];
            this.usergroup.Text = (string)profile["college"] + (string)profile["grade"];

            string img = profile["image"].ToString();
            setImg(img);

            this.chance.Text = profile["chance"].ToString();

        }

        public void setImg(string img) {
            if (img.Equals("")) { return; }
            BitmapImage bm = new BitmapImage(new Uri(@img, UriKind.RelativeOrAbsolute));
            this.imagePath.Source = bm;
        }

        private  async void login() {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            //Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            Windows.Storage.ApplicationDataCompositeValue UsrPwd =
   (Windows.Storage.ApplicationDataCompositeValue)localSettings.Values["loginUsrPwd"];
            Sign sign = Sign.getSign();
            if (!(await(sign.signin(UsrPwd["username"].ToString(), UsrPwd["password"].ToString()))))
            {
                Login.Logout();
                Frame.Navigate(typeof(Login));
            }
        }
        protected void quitApp(object sender, BackPressedEventArgs e)
        {
            if(!Frame.CanGoBack)
                Application.Current.Exit();
        }

        protected async void new_app(object sender, RoutedEventArgs e)
        {
            int chance = User.GetChance();
            if (chance <= 0)
            {
                MessageDialog messageDialog = new MessageDialog("没有足够的机会发布状态哦");
                await messageDialog.ShowAsync();
                return;
            }
            this.chance.Text = chance.ToString();
            if (!Frame.Navigate(typeof(AddNew)))
            {
                throw new Exception(this.resourceLoader.GetString("NavigationFailedExceptionMessage"));
            }
        }
        #region findControl 系列函数
        public List<Control> AllChildren(DependencyObject parent)
        {
            var _List = new List<Control>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var _Child = VisualTreeHelper.GetChild(parent, i);
                if (_Child is Control)
                    _List.Add(_Child as Control);
                _List.AddRange(AllChildren(_Child));
            }
            return _List;
        }
        private T FindControl<T>(DependencyObject parentContainer, string controlName)
        {
            var childControls = AllChildren(parentContainer);
            var control = childControls.OfType<Control>().Where(x => controlName.Equals(x.Name)).Cast<T>().First();
            return control;
        }
        public List<Image> AllImageChildren(DependencyObject parent)
        {
            var _List = new List<Image>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var _Child = VisualTreeHelper.GetChild(parent, i);
                if (_Child is Image)
                    _List.Add(_Child as Image);
                _List.AddRange(AllImageChildren(_Child));
            }
            return _List;
        }
        private Image FindImage(DependencyObject parentContainer, string controlName)
        {
            var childControls = AllImageChildren(parentContainer);
            var control = childControls.OfType<Image>().Where(x => controlName.Equals(x.Name)).Cast<Image>().First();
            return control;
        }

        public List<Grid> AllGridChildren(DependencyObject parent)
        {
            var _List = new List<Grid>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var _Child = VisualTreeHelper.GetChild(parent, i);
                if (_Child is Grid)
                    _List.Add(_Child as Grid);
                _List.AddRange(AllGridChildren(_Child));
            }
            return _List;
        }
        private Grid FindGrid(DependencyObject parentContainer, string controlName)
        {
            var childControls = AllGridChildren(parentContainer);
            var control = childControls.OfType<Grid>().Where(x => controlName.Equals(x.Name)).Cast<Grid>().First();
            return control;
        }
        public List<TextBlock> AllTextBlockChildren(DependencyObject parent)
        {
            var _List = new List<TextBlock>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var _Child = VisualTreeHelper.GetChild(parent, i);
                if (_Child is TextBlock)
                    _List.Add(_Child as TextBlock);
                _List.AddRange(AllTextBlockChildren(_Child));
            }
            return _List;
        }
        private TextBlock FindTextBlock(DependencyObject parentContainer, string controlName)
        {
            var childControls = AllTextBlockChildren(parentContainer);
            var control = childControls.OfType<TextBlock>().Where(x => controlName.Equals(x.Name)).Cast<TextBlock>().First();
            return control;
        }
        #endregion

        

        
        #endregion


    }
}