using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SzakdolgozatErtekeloWPF.Models;

namespace SzakdolgozatErtekeloWPF.Services
{
    public class LocalizationService
    {
        public LocalizationModel Current { get; set; }

        public void Load(bool english, bool scientific)
        {
            string file;

            if (english)
                file = "Angol.json";
            else if (scientific)
                file = "MagyarB.json";
            else
                file = "Magyar.json";

            string path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Lokalizacio",
                file);

            string json = File.ReadAllText(path);

            Current = JsonSerializer.Deserialize<LocalizationModel>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
        }
    }
}
