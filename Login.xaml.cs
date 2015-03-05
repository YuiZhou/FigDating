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
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        #region 登录和注册逻辑
        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            String username, password;
            switch (this.Pivot.SelectedIndex) { 
                case 0:
                    username = this.inUsr.Text.Trim();
                    password = this.inPwd.Password.Trim();
                    if (username.Equals("") || password.Equals("")) {
                    }
                    else if (username.Length != 11) {
                        MessageDialog messageDialog = new MessageDialog("请输入有效的用户名");
                        await messageDialog.ShowAsync();
                    }
                    else if (!login(username, password))
                    {
                        MessageDialog messageDialog = new MessageDialog("密码错误");
                        await messageDialog.ShowAsync();
                    }
                    break;
                case 1:
                    username = this.upUsr.Text;
                    password = this.upPwd.Password;
                    string cfm = this.upCfm.Password;
                    Sign sign = Sign.getSign();
                    if (!password.Equals(cfm))
                    {
                        // 两次输入的密码不一致
                        MessageDialog messageDialog = new MessageDialog("两次输入的密码不一致");
                        await messageDialog.ShowAsync();
                    }
                    else if (username.Length != 11)
                    {
                        MessageDialog messageDialog = new MessageDialog("请输入有效的用户名（学号）");
                        await messageDialog.ShowAsync();
                    }
                    else if (await (sign.signup(username, password)))
                    {
                        // 注册成功，直接登录
                        login(username, password);
                    }
                    else
                    {
                        // 这个账号已经被占用
                        MessageDialog messageDialog = new MessageDialog("注册失败：该用户名已经被占用");
                        await messageDialog.ShowAsync();
                    }

                    break;
            }

        }

        /// <summary>
        /// 登录软件，如果登录成功需要记录用户信息，并且跳转，返回真。否则返回否。
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool login(string username, string password) {
            Sign sign = Sign.getSign();
            if (!sign.signin(username, password)) {
                return false;
            }
            // 记录用户名密码信息
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Windows.Storage.ApplicationDataCompositeValue composite = new Windows.Storage.ApplicationDataCompositeValue();
            composite["username"] = username;
            composite["password"] = password;
            localSettings.Values["loginUsrPwd"] = composite;

            // 跳转页面
            this.Frame.Navigate(typeof(PivotPage));
            return true;
        }

        
        #endregion
    }
}
