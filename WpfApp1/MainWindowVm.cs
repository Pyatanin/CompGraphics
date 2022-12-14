using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AgentOctal.WpfLib;
using AgentOctal.WpfLib.Commands;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using OpenTK.Wpf;
using WpfApp1.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using WpfApp1.Light;



namespace WpfApp1;

public class MainWindowVm : ViewModel
{
    public MainWindowVm()
    {
        Fields = new AgentOctal.WpfLib.ObservableCollection<SearchFieldInfo>();
        LightItems = new AgentOctal.WpfLib.ObservableCollection<Type>()
        {
            typeof(Light.DirectedLight),
        };
        LightObjects = new List<object>()
        {
            new DirectedLight()
        };

        SearchType = LightItems.First();
    }

    public static AgentOctal.WpfLib.ObservableCollection<Type> LightItems { get; set; }
    public static List<Object> LightObjects { get; set; }
    public AgentOctal.WpfLib.ObservableCollection<SearchFieldInfo> Fields { get; }


    private Type _searchType;

    public  Type SearchType
    {
        get { return _searchType; }
        set
        {
            _searchType = value;
            Fields.Clear();
            foreach (var prop in _searchType.GetProperties())
            {
                var searchField = new SearchFieldInfo(prop.Name);
                Fields.Add(searchField);
            }
        }
    }
    
    private static void GetPropertyValues(Object obj)
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
        get { return _searchCommand ?? (_searchCommand = new SimpleCommand((obj) =>
        {
            WindowManager.ShowMessage(String.Join(", ", Fields.Select(f => $"{f.Name}: {f.Value}")));
        })); }
    }
}