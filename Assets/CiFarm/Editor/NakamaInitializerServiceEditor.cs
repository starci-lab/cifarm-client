using CiFarm.Scripts.Services.NakamaServices;
using UnityEditor;

namespace CiFarm.Editor
{
    [CustomEditor(typeof(NakamaInitializerService))]
    public class NakamaInitializerServiceEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // Get the target object
            NakamaInitializerService nakamaService = (NakamaInitializerService)target;

            DrawDefaultInspector();

            if (!nakamaService.Uselocal)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Testing Config Editor", EditorStyles.boldLabel);
                nakamaService.testChainKey      = (NakamaInitializerService.SupportChain)EditorGUILayout.EnumPopup("Test Chain Key", nakamaService.testChainKey);
                nakamaService.testAccountNumber = EditorGUILayout.IntField("Test Account Number", nakamaService.testAccountNumber);
            }
        }
    }
}