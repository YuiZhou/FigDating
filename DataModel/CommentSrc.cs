using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;

// 此文件定义的数据模型可充当在添加、移除或修改成员时
// 。  所选属性名称与标准项模板中的数据绑定一致。
//
// 应用程序可以使用此模型作为起始点并以它为基础构建，或完全放弃它并
// 替换为适合其需求的其他内容。如果使用此模式，则可提高应用程序
// 响应速度，途径是首次启动应用程序时启动 App.xaml 隐藏代码中的数据加载任务
//。

namespace FigDating.Data
{
    /// <summary>
    /// 泛型项数据模型。
    /// </summary>
    public class CommmentDataItem
    {
        public CommmentDataItem(String user, String group, String comment, String imagePath, String date)
        {
            this.User = user;
            this.Group = group;
            this.ImagePath = imagePath;
            this.Date = date;
            this.Comment = comment;
        }

        public string User { get; private set; }
        public string Group { get; private set; }
        public string ImagePath { get; private set; }
        public string Date { get; private set; }
        public string Comment { get; private set; }


        public override string ToString()
        {
            return this.Comment;
        }
    }

    /// <summary>
    /// 泛型组数据模型。
    /// </summary>
    public class CommentDataGroup
    {
        public CommentDataGroup()
        {
            this.Items = new ObservableCollection<CommmentDataItem>();
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public ObservableCollection<CommmentDataItem> Items { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// 创建包含从静态 json 文件读取内容的组和项的集合。
    /// 
    /// CommentDataSource 通过从项目中包括的静态 json 文件读取的数据进行
    /// 初始化。 这样在设计时和运行时均可提供示例数据。
    /// </summary>
    public sealed class CommentSrc
    {
        private static CommentSrc _CommentDataSource = new CommentSrc();

        private ObservableCollection<CommentDataGroup> _groups = new ObservableCollection<CommentDataGroup>();
        public ObservableCollection<CommentDataGroup> Groups
        {
            get { return this._groups; }
        }

        //public static async Task<IEnumerable<CommentDataGroup>> GetGroupsAsync()
        //{
        //    await _CommentDataSource.GetCommentDataAsync();

        //    return _CommentDataSource.Groups;
        //}
        public static async Task<CommentDataGroup> GetNoteAsync() {
            await _CommentDataSource.GetNotificationDataAsync();
            //await _CommentDataSource.GetCommentDataAsync(uniqueId);
            // 对于小型数据集可接受简单线性搜索
            try
            {
                return _CommentDataSource.Groups[0];
            }
            catch
            {
                return null;
            }
        }
        public static async Task<CommentDataGroup> GetGroupAsync(string uniqueId)
        {
            await _CommentDataSource.GetCommentDataAsync(uniqueId);
            //await _CommentDataSource.GetCommentDataAsync(uniqueId);
            // 对于小型数据集可接受简单线性搜索
            try
            {
                return _CommentDataSource.Groups[0];
            }
            catch
            {
                return null;
            }
            //var matches = _CommentDataSource.Groups.Where((group) => group.UniqueId.Equals(uniqueId));
            //if (matches.Count() == 1) return matches.First();
            //return null;
        }

        public static async Task<CommmentDataItem> GetItemAsync(string appointmentId)
        {
            
            // 对于小型数据集可接受简单线性搜索
            var matches = _CommentDataSource.Groups.SelectMany(group => group.Items).Where((item) => item.User.Equals(appointmentId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }
        private async Task GetNotificationDataAsync() {
            if (this._groups.Count != 0)
                this._groups.Clear();

            //Uri dataUri = new Uri("ms-appx:///DataModel/CommentData.json");

            //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            //string jsonText = await FileIO.ReadTextAsync(file);
            User user = User.getUser();
            string jsonText = await user.getNotification();
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            //JsonArray jsonArray = jsonObject["appointments"].GetArray();

            ////foreach (JsonValue groupValue in jsonArray)
            ////{
            //JsonObject groupObject = jsonArray[0].GetObject();
            CommentDataGroup group = new CommentDataGroup();

            foreach (JsonValue itemValue in jsonObject["notifications"].GetArray())
            {
                //Debug.WriteLine(itemValue.GetString());

                JsonObject itemObject = itemValue.GetObject();
                ////////JsonObject userObject = itemObject["user_id"].GetObject();
                group.Items.Add(new CommmentDataItem("",//userObject["username"].GetString(),        // 姓名
                                                    "",//userObject["college"].GetString() + userObject["grade"].GetString(),        // 学院和年级
                                                    itemObject["message"].GetString(),      // 评论
                                                    "",//userObject["path"].GetString(),    // 图片地址
                                                    itemObject["time"].GetString()      // 发布时间
                                                    ));
            }
            this.Groups.Add(group);
        }
        private async Task GetCommentDataAsync(String appointmentId)
        {
            if (this._groups.Count != 0)
                this._groups.Clear();

            //Uri dataUri = new Uri("ms-appx:///DataModel/CommentData.json");

            //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            //string jsonText = await FileIO.ReadTextAsync(file);
            Appointment appointment = Appointment.getAppointment();
            string jsonText = await appointment.getComment(appointmentId);
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            //JsonArray jsonArray = jsonObject["appointments"].GetArray();

            ////foreach (JsonValue groupValue in jsonArray)
            ////{
            //JsonObject groupObject = jsonArray[0].GetObject();
            CommentDataGroup group = new CommentDataGroup();

            foreach (JsonValue itemValue in jsonObject["comments"].GetArray())
            {
                //Debug.WriteLine(itemValue.GetString());

                JsonObject itemObject = itemValue.GetObject();
                //Debug.WriteLine("############\n\n%s\n\n#########", itemObject["user_id"].GetString());
                //JsonValue userValue = JsonValue.Parse(itemObject["user_id"].GetString());
                JsonObject userObject = itemObject["user_id"].GetObject();
                group.Items.Add(new CommmentDataItem(userObject["username"].GetString(),        // 姓名
                                                    userObject["college"].GetString() + userObject["grade"].GetString(),        // 学院和年级
                                                    itemObject["comment"].GetString(),      // 评论
                                                    userObject["path"].GetString(),    // 图片地址
                                                    itemObject["time"].GetString()      // 发布时间
                                                    ));
            }
            this.Groups.Add(group);
            //}
        }
    }
}