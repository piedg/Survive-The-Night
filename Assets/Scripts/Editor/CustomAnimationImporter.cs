using UnityEditor;
using UnityEngine;

public class CustomAnimationImporter : AssetPostprocessor
{
    private void OnPreprocessModel()
    {
        if(assetPath.Contains("Animations"))
        {
            ModelImporter importer = assetImporter as ModelImporter;
            importer.animationType = ModelImporterAnimationType.Human;
        }
    }

    private void OnPostprocessModel(GameObject gameObject)
    {
        if(assetPath.Contains("Animations"))
        {
            ModelImporter importer = assetImporter as ModelImporter;
        }

    }
}
