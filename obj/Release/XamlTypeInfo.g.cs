﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



namespace FigDating
{
    public partial class App : global::Windows.UI.Xaml.Markup.IXamlMetadataProvider
    {
        private global::FigDating.FigDating_XamlTypeInfo.XamlTypeInfoProvider _provider;

        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(global::System.Type type)
        {
            if(_provider == null)
            {
                _provider = new global::FigDating.FigDating_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByType(type);
        }

        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(string fullName)
        {
            if(_provider == null)
            {
                _provider = new global::FigDating.FigDating_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByName(fullName);
        }

        public global::Windows.UI.Xaml.Markup.XmlnsDefinition[] GetXmlnsDefinitions()
        {
            return new global::Windows.UI.Xaml.Markup.XmlnsDefinition[0];
        }
    }
}

namespace FigDating.FigDating_XamlTypeInfo
{
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal partial class XamlTypeInfoProvider
    {
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByType(global::System.Type type)
        {
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByType.TryGetValue(type, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByType(type);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByName(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByName.TryGetValue(typeName, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByName(typeName);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlMember GetMemberByLongName(string longMemberName)
        {
            if (string.IsNullOrEmpty(longMemberName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlMember xamlMember;
            if (_xamlMembers.TryGetValue(longMemberName, out xamlMember))
            {
                return xamlMember;
            }
            xamlMember = CreateXamlMember(longMemberName);
            if (xamlMember != null)
            {
                _xamlMembers.Add(longMemberName, xamlMember);
            }
            return xamlMember;
        }

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByName = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByType = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>
                _xamlMembers = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>();

        string[] _typeNameTable = null;
        global::System.Type[] _typeTable = null;

        private void InitTypeTables()
        {
            _typeNameTable = new string[15];
            _typeNameTable[0] = "FigDating.AddNew";
            _typeNameTable[1] = "Windows.UI.Xaml.Controls.Page";
            _typeNameTable[2] = "Windows.UI.Xaml.Controls.UserControl";
            _typeNameTable[3] = "FigDating.Common.NavigationHelper";
            _typeNameTable[4] = "Windows.UI.Xaml.DependencyObject";
            _typeNameTable[5] = "FigDating.Common.ObservableDictionary";
            _typeNameTable[6] = "Object";
            _typeNameTable[7] = "String";
            _typeNameTable[8] = "FigDating.ItemPage";
            _typeNameTable[9] = "FigDating.Login";
            _typeNameTable[10] = "FigDating.Controls.sp.SplitPanel";
            _typeNameTable[11] = "Windows.UI.Xaml.Controls.Panel";
            _typeNameTable[12] = "FigDating.Controls.PathControl";
            _typeNameTable[13] = "Windows.UI.Xaml.Controls.ContentControl";
            _typeNameTable[14] = "FigDating.PivotPage";

            _typeTable = new global::System.Type[15];
            _typeTable[0] = typeof(global::FigDating.AddNew);
            _typeTable[1] = typeof(global::Windows.UI.Xaml.Controls.Page);
            _typeTable[2] = typeof(global::Windows.UI.Xaml.Controls.UserControl);
            _typeTable[3] = typeof(global::FigDating.Common.NavigationHelper);
            _typeTable[4] = typeof(global::Windows.UI.Xaml.DependencyObject);
            _typeTable[5] = typeof(global::FigDating.Common.ObservableDictionary);
            _typeTable[6] = typeof(global::System.Object);
            _typeTable[7] = typeof(global::System.String);
            _typeTable[8] = typeof(global::FigDating.ItemPage);
            _typeTable[9] = typeof(global::FigDating.Login);
            _typeTable[10] = typeof(global::FigDating.Controls.sp.SplitPanel);
            _typeTable[11] = typeof(global::Windows.UI.Xaml.Controls.Panel);
            _typeTable[12] = typeof(global::FigDating.Controls.PathControl);
            _typeTable[13] = typeof(global::Windows.UI.Xaml.Controls.ContentControl);
            _typeTable[14] = typeof(global::FigDating.PivotPage);
        }

        private int LookupTypeIndexByName(string typeName)
        {
            if (_typeNameTable == null)
            {
                InitTypeTables();
            }
            for (int i=0; i<_typeNameTable.Length; i++)
            {
                if(0 == string.CompareOrdinal(_typeNameTable[i], typeName))
                {
                    return i;
                }
            }
            return -1;
        }

        private int LookupTypeIndexByType(global::System.Type type)
        {
            if (_typeTable == null)
            {
                InitTypeTables();
            }
            for(int i=0; i<_typeTable.Length; i++)
            {
                if(type == _typeTable[i])
                {
                    return i;
                }
            }
            return -1;
        }

        private object Activate_0_AddNew() { return new global::FigDating.AddNew(); }
        private object Activate_5_ObservableDictionary() { return new global::FigDating.Common.ObservableDictionary(); }
        private object Activate_8_ItemPage() { return new global::FigDating.ItemPage(); }
        private object Activate_9_Login() { return new global::FigDating.Login(); }
        private object Activate_10_SplitPanel() { return new global::FigDating.Controls.sp.SplitPanel(); }
        private object Activate_12_PathControl() { return new global::FigDating.Controls.PathControl(); }
        private object Activate_14_PivotPage() { return new global::FigDating.PivotPage(); }
        private void MapAdd_5_ObservableDictionary(object instance, object key, object item)
        {
            var collection = (global::System.Collections.Generic.IDictionary<global::System.String, global::System.Object>)instance;
            var newKey = (global::System.String)key;
            var newItem = (global::System.Object)item;
            collection.Add(newKey, newItem);
        }

        private global::Windows.UI.Xaml.Markup.IXamlType CreateXamlType(int typeIndex)
        {
            global::FigDating.FigDating_XamlTypeInfo.XamlSystemBaseType xamlType = null;
            global::FigDating.FigDating_XamlTypeInfo.XamlUserType userType;
            string typeName = _typeNameTable[typeIndex];
            global::System.Type type = _typeTable[typeIndex];

            switch (typeIndex)
            {

            case 0:   //  FigDating.AddNew
                userType = new global::FigDating.FigDating_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_0_AddNew;
                userType.AddMemberName("NavigationHelper");
                userType.AddMemberName("DefaultViewModel");
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 1:   //  Windows.UI.Xaml.Controls.Page
                xamlType = new global::FigDating.FigDating_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 2:   //  Windows.UI.Xaml.Controls.UserControl
                xamlType = new global::FigDating.FigDating_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 3:   //  FigDating.Common.NavigationHelper
                userType = new global::FigDating.FigDating_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.DependencyObject"));
                userType.SetIsReturnTypeStub();
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 4:   //  Windows.UI.Xaml.DependencyObject
                xamlType = new global::FigDating.FigDating_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 5:   //  FigDating.Common.ObservableDictionary
                userType = new global::FigDating.FigDating_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                userType.DictionaryAdd = MapAdd_5_ObservableDictionary;
                userType.SetIsReturnTypeStub();
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 6:   //  Object
                xamlType = new global::FigDating.FigDating_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 7:   //  String
                xamlType = new global::FigDating.FigDating_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 8:   //  FigDating.ItemPage
                userType = new global::FigDating.FigDating_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_8_ItemPage;
                userType.AddMemberName("NavigationHelper");
                userType.AddMemberName("DefaultViewModel");
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 9:   //  FigDating.Login
                userType = new global::FigDating.FigDating_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_9_Login;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 10:   //  FigDating.Controls.sp.SplitPanel
                userType = new global::FigDating.FigDating_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Panel"));
                userType.Activator = Activate_10_SplitPanel;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 11:   //  Windows.UI.Xaml.Controls.Panel
                xamlType = new global::FigDating.FigDating_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 12:   //  FigDating.Controls.PathControl
                userType = new global::FigDating.FigDating_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.ContentControl"));
                userType.Activator = Activate_12_PathControl;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 13:   //  Windows.UI.Xaml.Controls.ContentControl
                xamlType = new global::FigDating.FigDating_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 14:   //  FigDating.PivotPage
                userType = new global::FigDating.FigDating_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_14_PivotPage;
                userType.AddMemberName("NavigationHelper");
                userType.AddMemberName("DefaultViewModel");
                userType.SetIsLocalType();
                xamlType = userType;
                break;
            }
            return xamlType;
        }


        private object get_0_AddNew_NavigationHelper(object instance)
        {
            var that = (global::FigDating.AddNew)instance;
            return that.NavigationHelper;
        }
        private object get_1_AddNew_DefaultViewModel(object instance)
        {
            var that = (global::FigDating.AddNew)instance;
            return that.DefaultViewModel;
        }
        private object get_2_ItemPage_NavigationHelper(object instance)
        {
            var that = (global::FigDating.ItemPage)instance;
            return that.NavigationHelper;
        }
        private object get_3_ItemPage_DefaultViewModel(object instance)
        {
            var that = (global::FigDating.ItemPage)instance;
            return that.DefaultViewModel;
        }
        private object get_4_PivotPage_NavigationHelper(object instance)
        {
            var that = (global::FigDating.PivotPage)instance;
            return that.NavigationHelper;
        }
        private object get_5_PivotPage_DefaultViewModel(object instance)
        {
            var that = (global::FigDating.PivotPage)instance;
            return that.DefaultViewModel;
        }

        private global::Windows.UI.Xaml.Markup.IXamlMember CreateXamlMember(string longMemberName)
        {
            global::FigDating.FigDating_XamlTypeInfo.XamlMember xamlMember = null;
            global::FigDating.FigDating_XamlTypeInfo.XamlUserType userType;

            switch (longMemberName)
            {
            case "FigDating.AddNew.NavigationHelper":
                userType = (global::FigDating.FigDating_XamlTypeInfo.XamlUserType)GetXamlTypeByName("FigDating.AddNew");
                xamlMember = new global::FigDating.FigDating_XamlTypeInfo.XamlMember(this, "NavigationHelper", "FigDating.Common.NavigationHelper");
                xamlMember.Getter = get_0_AddNew_NavigationHelper;
                xamlMember.SetIsReadOnly();
                break;
            case "FigDating.AddNew.DefaultViewModel":
                userType = (global::FigDating.FigDating_XamlTypeInfo.XamlUserType)GetXamlTypeByName("FigDating.AddNew");
                xamlMember = new global::FigDating.FigDating_XamlTypeInfo.XamlMember(this, "DefaultViewModel", "FigDating.Common.ObservableDictionary");
                xamlMember.Getter = get_1_AddNew_DefaultViewModel;
                xamlMember.SetIsReadOnly();
                break;
            case "FigDating.ItemPage.NavigationHelper":
                userType = (global::FigDating.FigDating_XamlTypeInfo.XamlUserType)GetXamlTypeByName("FigDating.ItemPage");
                xamlMember = new global::FigDating.FigDating_XamlTypeInfo.XamlMember(this, "NavigationHelper", "FigDating.Common.NavigationHelper");
                xamlMember.Getter = get_2_ItemPage_NavigationHelper;
                xamlMember.SetIsReadOnly();
                break;
            case "FigDating.ItemPage.DefaultViewModel":
                userType = (global::FigDating.FigDating_XamlTypeInfo.XamlUserType)GetXamlTypeByName("FigDating.ItemPage");
                xamlMember = new global::FigDating.FigDating_XamlTypeInfo.XamlMember(this, "DefaultViewModel", "FigDating.Common.ObservableDictionary");
                xamlMember.Getter = get_3_ItemPage_DefaultViewModel;
                xamlMember.SetIsReadOnly();
                break;
            case "FigDating.PivotPage.NavigationHelper":
                userType = (global::FigDating.FigDating_XamlTypeInfo.XamlUserType)GetXamlTypeByName("FigDating.PivotPage");
                xamlMember = new global::FigDating.FigDating_XamlTypeInfo.XamlMember(this, "NavigationHelper", "FigDating.Common.NavigationHelper");
                xamlMember.Getter = get_4_PivotPage_NavigationHelper;
                xamlMember.SetIsReadOnly();
                break;
            case "FigDating.PivotPage.DefaultViewModel":
                userType = (global::FigDating.FigDating_XamlTypeInfo.XamlUserType)GetXamlTypeByName("FigDating.PivotPage");
                xamlMember = new global::FigDating.FigDating_XamlTypeInfo.XamlMember(this, "DefaultViewModel", "FigDating.Common.ObservableDictionary");
                xamlMember.Getter = get_5_PivotPage_DefaultViewModel;
                xamlMember.SetIsReadOnly();
                break;
            }
            return xamlMember;
        }
    }

    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlSystemBaseType : global::Windows.UI.Xaml.Markup.IXamlType
    {
        string _fullName;
        global::System.Type _underlyingType;

        public XamlSystemBaseType(string fullName, global::System.Type underlyingType)
        {
            _fullName = fullName;
            _underlyingType = underlyingType;
        }

        public string FullName { get { return _fullName; } }

        public global::System.Type UnderlyingType
        {
            get
            {
                return _underlyingType;
            }
        }

        virtual public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name) { throw new global::System.NotImplementedException(); }
        virtual public bool IsArray { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsCollection { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsConstructible { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsDictionary { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsMarkupExtension { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsBindable { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsReturnTypeStub { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsLocalType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType ItemType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType KeyType { get { throw new global::System.NotImplementedException(); } }
        virtual public object ActivateInstance() { throw new global::System.NotImplementedException(); }
        virtual public void AddToMap(object instance, object key, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void AddToVector(object instance, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void RunInitializer()   { throw new global::System.NotImplementedException(); }
        virtual public object CreateFromString(string input)   { throw new global::System.NotImplementedException(); }
    }
    
    internal delegate object Activator();
    internal delegate void AddToCollection(object instance, object item);
    internal delegate void AddToDictionary(object instance, object key, object item);

    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlUserType : global::FigDating.FigDating_XamlTypeInfo.XamlSystemBaseType
    {
        global::FigDating.FigDating_XamlTypeInfo.XamlTypeInfoProvider _provider;
        global::Windows.UI.Xaml.Markup.IXamlType _baseType;
        bool _isArray;
        bool _isMarkupExtension;
        bool _isBindable;
        bool _isReturnTypeStub;
        bool _isLocalType;

        string _contentPropertyName;
        string _itemTypeName;
        string _keyTypeName;
        global::System.Collections.Generic.Dictionary<string, string> _memberNames;
        global::System.Collections.Generic.Dictionary<string, object> _enumValues;

        public XamlUserType(global::FigDating.FigDating_XamlTypeInfo.XamlTypeInfoProvider provider, string fullName, global::System.Type fullType, global::Windows.UI.Xaml.Markup.IXamlType baseType)
            :base(fullName, fullType)
        {
            _provider = provider;
            _baseType = baseType;
        }

        // --- Interface methods ----

        override public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { return _baseType; } }
        override public bool IsArray { get { return _isArray; } }
        override public bool IsCollection { get { return (CollectionAdd != null); } }
        override public bool IsConstructible { get { return (Activator != null); } }
        override public bool IsDictionary { get { return (DictionaryAdd != null); } }
        override public bool IsMarkupExtension { get { return _isMarkupExtension; } }
        override public bool IsBindable { get { return _isBindable; } }
        override public bool IsReturnTypeStub { get { return _isReturnTypeStub; } }
        override public bool IsLocalType { get { return _isLocalType; } }

        override public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty
        {
            get { return _provider.GetMemberByLongName(_contentPropertyName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType ItemType
        {
            get { return _provider.GetXamlTypeByName(_itemTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType KeyType
        {
            get { return _provider.GetXamlTypeByName(_keyTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name)
        {
            if (_memberNames == null)
            {
                return null;
            }
            string longName;
            if (_memberNames.TryGetValue(name, out longName))
            {
                return _provider.GetMemberByLongName(longName);
            }
            return null;
        }

        override public object ActivateInstance()
        {
            return Activator(); 
        }

        override public void AddToMap(object instance, object key, object item) 
        {
            DictionaryAdd(instance, key, item);
        }

        override public void AddToVector(object instance, object item)
        {
            CollectionAdd(instance, item);
        }

        override public void RunInitializer() 
        {
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(UnderlyingType.TypeHandle);
        }

        override public object CreateFromString(string input)
        {
            if (_enumValues != null)
            {
                int value = 0;

                string[] valueParts = input.Split(',');

                foreach (string valuePart in valueParts) 
                {
                    object partValue;
                    int enumFieldValue = 0;
                    try
                    {
                        if (_enumValues.TryGetValue(valuePart.Trim(), out partValue))
                        {
                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                        }
                        else
                        {
                            try
                            {
                                enumFieldValue = global::System.Convert.ToInt32(valuePart.Trim());
                            }
                            catch( global::System.FormatException )
                            {
                                foreach( string key in _enumValues.Keys )
                                {
                                    if( string.Compare(valuePart.Trim(), key, global::System.StringComparison.OrdinalIgnoreCase) == 0 )
                                    {
                                        if( _enumValues.TryGetValue(key.Trim(), out partValue) )
                                        {
                                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        value |= enumFieldValue; 
                    }
                    catch( global::System.FormatException )
                    {
                        throw new global::System.ArgumentException(input, FullName);
                    }
                }

                return value; 
            }
            throw new global::System.ArgumentException(input, FullName);
        }

        // --- End of Interface methods

        public Activator Activator { get; set; }
        public AddToCollection CollectionAdd { get; set; }
        public AddToDictionary DictionaryAdd { get; set; }

        public void SetContentPropertyName(string contentPropertyName)
        {
            _contentPropertyName = contentPropertyName;
        }

        public void SetIsArray()
        {
            _isArray = true; 
        }

        public void SetIsMarkupExtension()
        {
            _isMarkupExtension = true;
        }

        public void SetIsBindable()
        {
            _isBindable = true;
        }

        public void SetIsReturnTypeStub()
        {
            _isReturnTypeStub = true;
        }

        public void SetIsLocalType()
        {
            _isLocalType = true;
        }

        public void SetItemTypeName(string itemTypeName)
        {
            _itemTypeName = itemTypeName;
        }

        public void SetKeyTypeName(string keyTypeName)
        {
            _keyTypeName = keyTypeName;
        }

        public void AddMemberName(string shortName)
        {
            if(_memberNames == null)
            {
                _memberNames =  new global::System.Collections.Generic.Dictionary<string,string>();
            }
            _memberNames.Add(shortName, FullName + "." + shortName);
        }

        public void AddEnumValue(string name, object value)
        {
            if (_enumValues == null)
            {
                _enumValues = new global::System.Collections.Generic.Dictionary<string, object>();
            }
            _enumValues.Add(name, value);
        }
    }

    internal delegate object Getter(object instance);
    internal delegate void Setter(object instance, object value);

    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlMember : global::Windows.UI.Xaml.Markup.IXamlMember
    {
        global::FigDating.FigDating_XamlTypeInfo.XamlTypeInfoProvider _provider;
        string _name;
        bool _isAttachable;
        bool _isDependencyProperty;
        bool _isReadOnly;

        string _typeName;
        string _targetTypeName;

        public XamlMember(global::FigDating.FigDating_XamlTypeInfo.XamlTypeInfoProvider provider, string name, string typeName)
        {
            _name = name;
            _typeName = typeName;
            _provider = provider;
        }

        public string Name { get { return _name; } }

        public global::Windows.UI.Xaml.Markup.IXamlType Type
        {
            get { return _provider.GetXamlTypeByName(_typeName); }
        }

        public void SetTargetTypeName(string targetTypeName)
        {
            _targetTypeName = targetTypeName;
        }
        public global::Windows.UI.Xaml.Markup.IXamlType TargetType
        {
            get { return _provider.GetXamlTypeByName(_targetTypeName); }
        }

        public void SetIsAttachable() { _isAttachable = true; }
        public bool IsAttachable { get { return _isAttachable; } }

        public void SetIsDependencyProperty() { _isDependencyProperty = true; }
        public bool IsDependencyProperty { get { return _isDependencyProperty; } }

        public void SetIsReadOnly() { _isReadOnly = true; }
        public bool IsReadOnly { get { return _isReadOnly; } }

        public Getter Getter { get; set; }
        public object GetValue(object instance)
        {
            if (Getter != null)
                return Getter(instance);
            else
                throw new global::System.InvalidOperationException("GetValue");
        }

        public Setter Setter { get; set; }
        public void SetValue(object instance, object value)
        {
            if (Setter != null)
                Setter(instance, value);
            else
                throw new global::System.InvalidOperationException("SetValue");
        }
    }
}





