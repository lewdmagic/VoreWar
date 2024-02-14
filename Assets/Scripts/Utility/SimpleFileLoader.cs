using System.IO;

internal class SimpleFileLoader
{
    private string _extension;
    private string _directory;
    private bool _editor;
    private LoaderType _loaderType;

    private FileLoaderUI _ui;

    public enum LoaderType
    {
        MapEditor,
        PickMap,
        PickSaveForContentSettings
    }

    public SimpleFileLoader(string directory, string extension, FileLoaderUI uI, bool mapEditor, LoaderType loaderType)
    {
        this._loaderType = loaderType;
        this._extension = extension;
        this._directory = directory;
        _editor = mapEditor;
        _ui = uI;
        BuildFiles();
    }

    private void BuildFiles()
    {
        if (Directory.Exists(_directory) == false) Directory.CreateDirectory(_directory);
        string[] files = Directory.GetFiles(_directory);

        foreach (string file in files)
        {
            if (!File.Exists(file)) return;

            if (CompatibleFileExtension(file))
            {
                switch (_loaderType)
                {
                    case LoaderType.MapEditor:
                        _ui.CreateMapLoadButton(file);
                        break;
                    case LoaderType.PickMap:
                        _ui.CreateMapStrategicIntegrateButton(file);
                        break;
                    case LoaderType.PickSaveForContentSettings:
                        _ui.CreateGrabContentSettingsButton(file);
                        break;
                }
            }
        }
    }

    public bool CompatibleFileExtension(string file)
    {
        if (_extension.Length == 0)
        {
            return true;
        }

        if (file.EndsWith("." + _extension))
        {
            return true;
        }

        // Not found, return not compatible
        return false;
    }
}