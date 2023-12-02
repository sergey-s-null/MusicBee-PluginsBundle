using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using Autofac;
using Module.Settings.Database;
using Module.Settings.Database.Models;

namespace Debug.Settings
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = Container.Create();

            var setting = GetLoadedSettingEntry(container);

            Shutdown();
        }

        private static void AddSettingEntry(IContainer container)
        {
            using var context = container.Resolve<SettingsContext>();

            context.Settings.Add(new SettingEntry
            {
                Area = "area",
                Id = "1",
                Value = new DictionarySettingValue
                {
                    Values =
                    {
                        new KeyedSettingValue
                        {
                            Key = new StringSettingValue { Value = "key" },
                            Value = new DictionarySettingValue
                            {
                                Values =
                                {
                                    new KeyedSettingValue
                                    {
                                        Key = new Int32SettingValue { Value = 33 },
                                        Value = new StringSettingValue { Value = "kavo" }
                                    }
                                }
                            }
                        },
                        new KeyedSettingValue
                        {
                            Key = new Int32SettingValue { Value = 4242 },
                            Value = new ListSettingValue
                            {
                                Values =
                                {
                                    new Int32SettingValue { Value = 5 },
                                    new Int32SettingValue { Value = 6 },
                                    new Int32SettingValue { Value = 7 },
                                }
                            }
                        }
                    }
                }
            });

            context.SaveChanges();
        }

        private static SettingEntry GetLoadedSettingEntry(IContainer container)
        {
            using var context = container.Resolve<SettingsContext>();
            var entry = context.Settings
                .Include(x => x.Value)
                .First(x => x.Area == "area" && x.Id == "1");

            Load(context, entry.Value);

            return entry;
        }

        // todo move to Repository when will be required
        private static void Load(SettingsContext context, SettingValue value)
        {
            switch (value)
            {
                case Int32SettingValue:
                case StringSettingValue:
                    break;
                case KeyedSettingValue keyedValue:
                    LoadKeyedValue(context, keyedValue);
                    break;
                case ListSettingValue listValue:
                    LoadList(context, listValue);
                    break;
                case DictionarySettingValue dictionaryValue:
                    LoadDictionary(context, dictionaryValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        private static void LoadKeyedValue(SettingsContext context, KeyedSettingValue value)
        {
            context.Entry(value)
                .Reference(x => x.Key)
                .Load();
            context.Entry(value)
                .Reference(x => x.Value)
                .Load();

            Load(context, value.Key);
            Load(context, value.Value);
        }

        private static void LoadList(SettingsContext context, ListSettingValue value)
        {
            context.Entry(value)
                .Collection(x => x.Values)
                .Load();

            foreach (var childValue in value.Values)
            {
                Load(context, childValue);
            }
        }

        private static void LoadDictionary(SettingsContext context, DictionarySettingValue value)
        {
            context.Entry(value)
                .Collection(x => x.Values)
                .Load();

            foreach (var childValue in value.Values)
            {
                Load(context, childValue);
            }
        }
    }
}