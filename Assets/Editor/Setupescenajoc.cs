// Coloca este archivo en: Assets/Editor/SetupEscenaJoc.cs
// Luego en Unity ve a: Tools → Setup EscenaJoc

using UnityEngine;
using UnityEditor;

public class Setupescenajoc : EditorWindow
{
    [MenuItem("Tools/Setup EscenaJoc")]
    public static void Setup()
    {
        CrearParets();
        CollocarNauJugador();
        CrearGeneradorEnemics();
        CrearGameManager();
        ConfigurarCamera();

        Debug.Log("✅ EscenaJoc muntada correctament!");
    }

    // ─────────────────────────────────────────
    // PAREDES INVISIBLES
    // ─────────────────────────────────────────
    static void CrearParets()
    {
        // Asegura que el tag existe
        CrearTag("Paret");

        // Dimensiones del área de juego
        // Cámara ortográfica size=8, aspect ~9:16 → ancho ≈ 9, alto ≈ 16
        float ample = 9f;
        float alt   = 16f;
        float gruix = 1f;

        CrearParet("WallLeft",   new Vector3(-ample / 2f - gruix / 2f, 0, 0),
                                 new Vector3(gruix, alt + gruix * 2f, 1f));

        CrearParet("WallRight",  new Vector3( ample / 2f + gruix / 2f, 0, 0),
                                 new Vector3(gruix, alt + gruix * 2f, 1f));

        CrearParet("WallTop",    new Vector3(0,  alt / 2f + gruix / 2f, 0),
                                 new Vector3(ample + gruix * 2f, gruix, 1f));

        CrearParet("WallBottom", new Vector3(0, -alt / 2f - gruix / 2f, 0),
                                 new Vector3(ample + gruix * 2f, gruix, 1f));

        Debug.Log("  ✓ Paredes creadas (WallLeft, WallRight, WallTop, WallBottom)");
    }

    static void CrearParet(string nom, Vector3 posicio, Vector3 escala)
    {
        // Elimina si ya existe
        GameObject existent = GameObject.Find(nom);
        if (existent != null) Object.DestroyImmediate(existent);

        GameObject paret = new GameObject(nom);
        paret.tag = "Paret";
        paret.transform.position = posicio;

        BoxCollider col = paret.AddComponent<BoxCollider>();
        col.size = escala;
        // No hace falta escalar el transform, ponemos el tamaño directo en el collider
    }

    // ─────────────────────────────────────────
    // NAU JUGADOR
    // ─────────────────────────────────────────
    static void CollocarNauJugador()
    {
        // Elimina si ya existe
        GameObject existent = GameObject.Find("NauJugador");
        if (existent != null)
        {
            Debug.Log("  ⚠️ NauJugador ja existeix a l'escena, no es torna a crear.");
            return;
        }

        // Busca el prefab
        string[] guids = AssetDatabase.FindAssets("NauJugador t:Prefab");
        if (guids.Length == 0)
        {
            Debug.LogWarning("  ⚠️ Prefab 'NauJugador' no trobat a Assets/Prefabs/. " +
                             "Executa primer Tools → Crear Prefabs del Joc.");
            return;
        }

        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

        // Posición: centro-inferior del área de juego
        Vector3 posicio = new Vector3(0f, -6f, 0f);
        GameObject instancia = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        instancia.transform.position = posicio;

        Debug.Log("  ✓ NauJugador col·locat a " + posicio);
    }

    // ─────────────────────────────────────────
    // GENERADOR ENEMICS
    // ─────────────────────────────────────────
    static void CrearGeneradorEnemics()
    {
        GameObject existent = GameObject.Find("GeneradorEnemics");
        if (existent != null)
        {
            Debug.Log("  ⚠️ GeneradorEnemics ja existeix a l'escena.");
            return;
        }

        GameObject go = new GameObject("GeneradorEnemics");
        go.transform.position = Vector3.zero;

        // Añade el script si existe
        AfegirScript(go, "GeneradorEnemics");

        // Asigna prefabs de enemigos automáticamente si los encuentra
        MonoBehaviour script = go.GetComponent<MonoBehaviour>();
        if (script != null)
        {
            AssignarPrefabAlCamp(script, "prefabEnemic",        "NauEnemic");
            AssignarPrefabAlCamp(script, "prefabEnemicEspecial","NauEnemicEspecial");
            // Nombres alternativos comunes
            AssignarPrefabAlCamp(script, "enemicPrefab",        "NauEnemic");
            AssignarPrefabAlCamp(script, "enemicEspecialPrefab","NauEnemicEspecial");
        }

        Debug.Log("  ✓ GeneradorEnemics creat" +
                  (script != null ? " amb script assignat" : " (script no trobat, afegeix-lo manualment)"));
    }

    // ─────────────────────────────────────────
    // GAME MANAGER
    // ─────────────────────────────────────────
    static void CrearGameManager()
    {
        GameObject existent = GameObject.Find("GameManager");
        if (existent != null)
        {
            Debug.Log("  ⚠️ GameManager ja existeix a l'escena.");
            return;
        }

        GameObject go = new GameObject("GameManager");
        go.transform.position = Vector3.zero;

        AfegirScript(go, "GameManager");

        Debug.Log("  ✓ GameManager creat");
    }

    // ─────────────────────────────────────────
    // CÀMERA
    // ─────────────────────────────────────────
    static void ConfigurarCamera()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogWarning("  ⚠️ No s'ha trobat la Main Camera a l'escena.");
            return;
        }

        cam.transform.position   = new Vector3(0f, 0f, -15f);
        cam.transform.rotation   = Quaternion.Euler(0f, 0f, 0f);
        cam.orthographic         = true;
        cam.orthographicSize     = 8f;
        cam.backgroundColor      = Color.black;

        Debug.Log("  ✓ Càmera configurada (ortogràfica, size=8, posició (0,0,-15))");
    }

    // ─────────────────────────────────────────
    // HELPERS
    // ─────────────────────────────────────────

    static void AfegirScript(GameObject go, string nomScript)
    {
        foreach (var assembly in System.AppDomain.CurrentDomain.GetAssemblies())
        {
            System.Type tipus = assembly.GetType(nomScript);
            if (tipus != null && tipus.IsSubclassOf(typeof(MonoBehaviour)))
            {
                go.AddComponent(tipus);
                return;
            }
        }
        Debug.LogWarning($"  ⚠️ Script '{nomScript}' no trobat. Afegeix-lo manualment.");
    }

    static void AssignarPrefabAlCamp(MonoBehaviour script, string nomCamp, string nomPrefab)
    {
        var field = script.GetType().GetField(nomCamp,
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance);

        if (field == null) return;

        string[] guids = AssetDatabase.FindAssets($"{nomPrefab} t:Prefab");
        if (guids.Length == 0) return;

        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (prefab != null)
        {
            field.SetValue(script, prefab);
            EditorUtility.SetDirty(script);
        }
    }

    static void CrearTag(string tag)
    {
        SerializedObject tagManager = new SerializedObject(
            AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        for (int i = 0; i < tagsProp.arraySize; i++)
            if (tagsProp.GetArrayElementAtIndex(i).stringValue == tag) return;

        tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
        tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1).stringValue = tag;
        tagManager.ApplyModifiedProperties();
    }
}