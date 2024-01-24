using System;
using System.Text.Json;
using System.Threading.Tasks;
using static Rs3TrackerMAUI.Classes.DisplayClasses;

public class FilePickerService
{
    public async Task<Rotation> ImportRotation(string baseDir)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Import Rotation file (JSON)"
            });
            if (result != null)
            {
                string filePath = result.FullPath;
                if (Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    string jsonContent = await File.ReadAllTextAsync(filePath);
                    var jsonData = JsonSerializer.Deserialize<Rotation>(jsonContent);
                    return jsonData;
                }
                return null;
            }
            else
            {
                // The user canceled the operation
                return null;
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            return null;
        }
    }
}
