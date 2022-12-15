using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using AgentOctal.WpfLib;
using AgentOctal.WpfLib.Commands;
using WpfApp1.Light;
using WpfApp1.Model;

namespace WpfApp1;

public class MainWindowVm : ViewModel
{
    public MainWindowVm()
    {
        Fields = new ObservableCollection<SearchFieldInfo>();
        LightItems = new ObservableCollection<object>();
    }

    public static ObservableCollection<object> LightItems { get; set; }
    public static List<object> LightObjects { get; set; }

    public ObservableCollection<SearchFieldInfo> Fields { get; }

    private object _searchType;

    public object SearchType
    {
        get => _searchType;
        set
        {
            _searchType = value;
            Fields.Clear();
            var props = _searchType.GetType();
            foreach (var prop in props.GetProperties())
            {
                var searchField = new SearchFieldInfo(prop.Name,
                    _searchType.GetType().GetProperty(prop.Name)?.GetValue(_searchType, null));
                if (!searchField.Name.Contains("Array"))
                {
                    Fields.Add(searchField);
                }
            }
        }
    }

    public void Efwef()
    {
        SearchType = LightItems.Last();
    }

    private static void GetPropertyValues(object obj)
    {
        var t = obj.GetType();
        Console.WriteLine("Type is: {0}", t.Name);
        var props = t.GetProperties();
        Console.WriteLine("Properties (N = {0}):",
            props.Length);
        foreach (var prop in props)
            if (prop.GetIndexParameters().Length == 0)
                Console.WriteLine("   {0} ({1}): {2}", prop.Name,
                    prop.PropertyType.Name,
                    prop.GetValue(obj));
            else
                Console.WriteLine("   {0} ({1}): <Indexed>", prop.Name,
                    prop.PropertyType.Name);
    }

    private ICommand _searchCommand;

    public ICommand SearchCommand
    {
        get
        {
            return _searchCommand ?? (_searchCommand = new SimpleCommand(obj =>
            {
                WindowManager.ShowMessage(string.Join(", ", Fields.Select(f => $"{f.Name}: {f.Value}")));
            }));
        }
    }
}