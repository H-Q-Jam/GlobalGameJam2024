#if UNITY_EDITOR
    using UnityEditor;
    using UnityEditor.UIElements;
    using UnityEngine.UIElements;

    [CustomEditor(typeof(MotManager))]
    public class MotManager_Editor: Editor
    {
        public override VisualElement CreateInspectorGUI() {
            var inspector = new VisualElement();
            
            var equipe1 = new PropertyField(
                serializedObject.FindProperty("equipe1")
            );
            var equipe2 = new PropertyField(
                serializedObject.FindProperty("equipe2")
            );
            
            var mots = new PropertyField(
                serializedObject.FindProperty("mots")
            );
            var lettres = new PropertyField(
                serializedObject.FindProperty("lettres")
            );
            var zones = new PropertyField(
                serializedObject.FindProperty("zones")
            );

            inspector.Add(equipe1);
            inspector.Add(equipe2);
            inspector.Add(mots);
            inspector.Add(lettres);
            inspector.Add(zones);
            
            mots.SetEnabled(false);
            lettres.SetEnabled(false);
            zones.SetEnabled(false);
            
            return inspector;
        }
    }
#endif
