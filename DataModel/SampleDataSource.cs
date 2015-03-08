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
    public class SampleDataItem
    {
        public SampleDataItem(String uniqueId, String userId, String title, String group, String imagePath, String date,
            String hasSee, String hasLoved, String status, String content, String comment)
        {
            this.UniqueId = uniqueId;
            this.UserId = userId;
            this.Title = title;
            this.Group = group;
            this.hasSee = hasSee;
            this.ImagePath = imagePath;
            this.hasLoved = hasLoved;
            this.status = status;
            this.Date = date;
            this.Content = content;
            this.hasComment = comment;
        }

        public string UniqueId { get; private set; }
        public string UserId { get; private set; }
        public string Title { get; private set; }
        public string Group { get; private set; }
        public string hasSee { get; private set; }
        public string ImagePath { get; private set; }
        public string hasLoved { get; private set; }
        public string status { get; private set; }
        public string Date { get; private set; }
        public string Content { get; private set; }
        public string hasComment { get; private set; }


        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// 泛型组数据模型。
    /// </summary>
    public class SampleDataGroup
    {
        public SampleDataGroup(String uniqueId, String title, String subtitle, String imagePath, String description)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.ImagePath = imagePath;
            this.Items = new ObservableCollection<SampleDataItem>();
        }

        public SampleDataGroup()
        {
            this.Items = new ObservableCollection<SampleDataItem>();
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public ObservableCollection<SampleDataItem> Items { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// 创建包含从静态 json 文件读取内容的组和项的集合。
    /// 
    /// SampleDataSource 通过从项目中包括的静态 json 文件读取的数据进行
    /// 初始化。 这样在设计时和运行时均可提供示例数据。
    /// </summary>
    public sealed class SampleDataSource
    {
        private static SampleDataSource _sampleDataSource = new SampleDataSource();

        private ObservableCollection<SampleDataGroup> _groups = new ObservableCollection<SampleDataGroup>();
        public ObservableCollection<SampleDataGroup> Groups
        {
            get { return this._groups; }
        }

        public static async Task<IEnumerable<SampleDataGroup>> GetGroupsAsync()
        {
            await _sampleDataSource.GetSampleDataAsync();

            return _sampleDataSource.Groups;
        }

        public static async Task<SampleDataGroup> GetGroupAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // 对于小型数据集可接受简单线性搜索
            try
            {
                return _sampleDataSource.Groups[0];
            }
            catch
            {
                return null;
            }
            //var matches = _sampleDataSource.Groups.Where((group) => group.UniqueId.Equals(uniqueId));
            //if (matches.Count() == 1) return matches.First();
            //return null;
        }

        public static async Task<SampleDataItem> GetItemAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // 对于小型数据集可接受简单线性搜索
            var matches = _sampleDataSource.Groups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        private async Task GetSampleDataAsync()
        {
            if (this._groups.Count != 0)
                return;

            //Uri dataUri = new Uri("ms-appx:///DataModel/SampleData.json");

            //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            //string jsonText = await FileIO.ReadTextAsync(file);
            Appointment appointment = Appointment.getAppointment();
            string jsonText = await appointment.getNews();
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            //JsonArray jsonArray = jsonObject["appointments"].GetArray();

            ////foreach (JsonValue groupValue in jsonArray)
            ////{
            //JsonObject groupObject = jsonArray[0].GetObject();
            SampleDataGroup group = new SampleDataGroup();

            foreach (JsonValue itemValue in jsonObject["appointments"].GetArray())
            {
                //Debug.WriteLine(itemValue.GetString());

                JsonObject itemObject = itemValue.GetObject();
                //Debug.WriteLine("############\n\n%s\n\n#########", itemObject["user_id"].GetString());
                //JsonValue userValue = JsonValue.Parse(itemObject["user_id"].GetString());
                JsonObject userObject = itemObject["user_id"].GetObject();
                group.Items.Add(new SampleDataItem(itemObject["appointment_id"].GetNumber().ToString(), // id
                                                    userObject["user_id"].GetNumber().ToString(),
                                                    userObject["username"].GetString(),        // 姓名
                                                    userObject["college"].GetString() + userObject["grade"].GetString(),        // 学院和年级
                                                    userObject["path"].GetString(),    // 图片地址
                                                    itemObject["begin"].GetString(), /////////        // 发布时间
                                                    itemObject["view_count"].GetNumber().ToString(),       // 已经看了的人数
                                                    itemObject["like_count"].GetNumber().ToString(),     // 点赞人数
                                                    itemObject["status"].GetString().Equals("waiting")?"等待约会":"约会成功",     // 状态
                                                    "从" + itemObject["begin"].GetString() + "\n到" + itemObject
                                                    ["end"].GetString() + "\n" + itemObject["content"].GetString(),
                                                    itemObject["comment_count"].GetNumber().ToString()
                                                    ));
            }
            this.Groups.Add(group);
            //}
        }
    }
}