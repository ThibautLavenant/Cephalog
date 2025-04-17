using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Cephalog.Models;

namespace Cephalog.BusinessLogic
{
    public class BusinessService
    {
        public static BusinessService Instance { get; } = new BusinessService();

        private BusinessService()
        {
            // Initialize the business service
        }

        public int CeilToMinuteDivision(TimeSpan timespan, int minuteDivision)
        {
            return Convert.ToInt32(Math.Ceiling(timespan.TotalMinutes / minuteDivision));
        }

        public void StoreData(MainWindowViewModel viewModel)
        {
            try
            {
                var storagePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                storagePath = Path.Combine(storagePath, $"Cephalog");
                if (!Directory.Exists(storagePath))
                {
                    Directory.CreateDirectory(storagePath);
                }
                storagePath = Path.Combine(storagePath, $"Cephalog_{viewModel.CurrentDateTime:yy-MM-dd}.data");
                var opt = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };
                File.WriteAllText(storagePath, JsonSerializer.Serialize(viewModel.TodayTasks, opt));
            }
            catch (Exception)
            {
                return;
            }
        }

        public List<TimedTask> GetData(DateTime day)
        {
            try
            {
                var storagePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                storagePath = Path.Combine(storagePath, $"Cephalog");
                storagePath = Path.Combine(storagePath, $"Cephalog_{day:yy-MM-dd}.data");
                var storedValue = File.ReadAllText(storagePath);
                return JsonSerializer.Deserialize<List<TimedTask>>(storedValue) ?? [];
            }
            catch (Exception)
            {
                return new List<TimedTask>();
            }
        }

        public void StoreCientList(List<string> clientList)
        {
            try
            {
                var storagePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                storagePath = Path.Combine(storagePath, $"Cephalog");
                if (!Directory.Exists(storagePath))
                {
                    Directory.CreateDirectory(storagePath);
                }
                storagePath = Path.Combine(storagePath, $"clientList.data");
                var opt = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };
                File.WriteAllText(storagePath, JsonSerializer.Serialize(clientList, opt));
            }
            catch (Exception)
            {
                return;
            }
        }

        public List<string> GetCientList()
        {
            try
            {
                var storagePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                storagePath = Path.Combine(storagePath, $"Cephalog");
                storagePath = Path.Combine(storagePath, $"clientList.data");
                var storedValue = File.ReadAllText(storagePath);
                return JsonSerializer.Deserialize<List<string>>(storedValue) ?? [];
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        public void StoreCategoryList(List<string> categoryList)
        {
            try
            {
                var storagePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                storagePath = Path.Combine(storagePath, $"Cephalog");
                if (!Directory.Exists(storagePath))
                {
                    Directory.CreateDirectory(storagePath);
                }
                storagePath = Path.Combine(storagePath, $"categoryList.data");
                var opt = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };
                File.WriteAllText(storagePath, JsonSerializer.Serialize(categoryList, opt));
            }
            catch (Exception)
            {
                return;
            }
        }

        public List<string> GetCategoryList()
        {
            try
            {
                var storagePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                storagePath = Path.Combine(storagePath, $"Cephalog");
                storagePath = Path.Combine(storagePath, $"categoryList.data");
                var storedValue = File.ReadAllText(storagePath);
                return JsonSerializer.Deserialize<List<string>>(storedValue) ?? [];
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
    }
}
