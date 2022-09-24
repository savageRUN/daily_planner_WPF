using daily_planner_WPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daily_planner_WPF.Services
{
    class FileIOService
    {
        private readonly string PATH;

        public FileIOService(string path)
        {
            PATH = path;
        }
        public BindingList<PlannerModel> LoadData()
        {
            var fileExsist = File.Exists(PATH);
            if (!fileExsist)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<PlannerModel>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<PlannerModel>>(fileText);
            }
        }

        public void SaveData(Object toDataList)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(toDataList);
                writer.Write(output);
            }
        }
    }
}
