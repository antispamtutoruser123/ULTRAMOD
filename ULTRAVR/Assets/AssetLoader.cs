using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAVR
{
    class AssetLoader
    {
        public static GameObject LeftHandBase;
        public static GameObject RightHandBase;


        public AssetLoader()
        {
            var SkyboxBundle = LoadBundle("vrhands");
            LeftHandBase = LoadAsset<GameObject>(SkyboxBundle, "SteamVR/Prefabs/vr_glove_left_model_slim.prefab");
            RightHandBase = LoadAsset<GameObject>(SkyboxBundle, "SteamVR/Prefabs/vr_glove_right_model_slim.prefab");

        }

        private T LoadAsset<T>(AssetBundle bundle, string prefabName) where T : UnityEngine.Object
        {

            var asset = bundle.LoadAsset<T>($"Assets/{prefabName}");
            if (asset)
                return asset;
            else
            {
                Logs.WriteError($"Failed to load asset {prefabName}");
                return null;
            }

        }

        private static AssetBundle LoadBundle(string assetName)
        {
            Logs.WriteInfo($"LLLL: Loading asset bundle:  {Plugin.gamePath}/UEBSVR/Assets/{assetName}");
            var myLoadedAssetBundle =
                AssetBundle.LoadFromFile(
                    $"{Plugin.gamePath}/ULTRAVR/Assets/{assetName}");
            if (myLoadedAssetBundle == null)
            {
                Logs.WriteError($"Failed to load AssetBundle {assetName}");
                return null;
            }

            return myLoadedAssetBundle;
        }

    }
}
