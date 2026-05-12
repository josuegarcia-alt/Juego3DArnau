// Coloca este archivo en: Assets/Editor/CreatePrefabs.cs
// Luego en Unity ve a: Tools → Crear Prefabs del Joc

using UnityEngine;
using UnityEditor;
using System.IO;

public class CreatePrefabs : EditorWindow
{
    [MenuItem("Tools/Crear Prefabs del Joc")]
    public static void CrearTots()
    {
        // Asegura que existen las carpetas necesarias
        if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        if (!AssetDatabase.IsValidFolder("Assets/Materials"))
            AssetDatabase.CreateFolder("Assets", "Materials");

        // Asegura que los Tags existen
        CrearTag("Jugador");
        CrearTag("Enemic");
        CrearTag("EnemicEspecial");
        CrearTag("ProjectilJugador");
        CrearTag("ProjectilEnemic");
        CrearTag("ProjectilEnemicEspecial");
        CrearTag("Vida");
        CrearTag("Paret");

        // Crea cada prefab
        CrearNauJugador();
        CrearNauEnemic();
        CrearNauEnemicEspecial();
        CrearProjectilJugador();
        CrearProjectilEnemic();
        CrearProjectilEnemicEspecial();
        CrearObjecVida();
        CrearExplosio();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("✅ Tots els prefabs creats a Assets/Prefabs/");
    }

    // ─────────────────────────────────────────
    // NAU JUGADOR — cubo azul
    // ─────────────────────────────────────────
    static void CrearNauJugador()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = "NauJugador";
        go.transform.localScale = new Vector3(1f, 0.4f, 1f);
        go.tag = "Jugador";

        // Material azul
        Material mat = CrearMaterial("MatJugador", new Color(0.2f, 0.4f, 1f));
        go.GetComponent<Renderer>().sharedMaterial = mat;

        // Rigidbody
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationY
                       | RigidbodyConstraints.FreezeRotationZ
                       | RigidbodyConstraints.FreezePositionZ;

        // BoxCollider ya lo añade CreatePrimitive, solo configuramos
        BoxCollider col = go.GetComponent<BoxCollider>();
        col.isTrigger = false;

        // Script (si existe en el proyecto)
        AfegirScript(go, "NauJugador");

        GuardarPrefab(go, "NauJugador");
    }

    // ─────────────────────────────────────────
    // NAU ENEMIC — cubo rojo
    // ─────────────────────────────────────────
    static void CrearNauEnemic()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = "NauEnemic";
        go.transform.localScale = new Vector3(0.8f, 0.4f, 0.8f);
        go.tag = "Enemic";

        Material mat = CrearMaterial("MatEnemic", new Color(1f, 0.2f, 0.2f));
        go.GetComponent<Renderer>().sharedMaterial = mat;

        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationY
                       | RigidbodyConstraints.FreezeRotationZ
                       | RigidbodyConstraints.FreezePositionZ;

        BoxCollider col = go.GetComponent<BoxCollider>();
        col.isTrigger = true;

        AfegirScript(go, "NauEnemic");

        GuardarPrefab(go, "NauEnemic");
    }

    // ─────────────────────────────────────────
    // NAU ENEMIC ESPECIAL — cubo naranja
    // ─────────────────────────────────────────
    static void CrearNauEnemicEspecial()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = "NauEnemicEspecial";
        go.transform.localScale = new Vector3(1.2f, 0.5f, 1.2f);
        go.tag = "EnemicEspecial";

        Material mat = CrearMaterial("MatEnemicEspecial", new Color(1f, 0.5f, 0f));
        go.GetComponent<Renderer>().sharedMaterial = mat;

        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationY
                       | RigidbodyConstraints.FreezeRotationZ
                       | RigidbodyConstraints.FreezePositionZ;

        BoxCollider col = go.GetComponent<BoxCollider>();
        col.isTrigger = true;

        AfegirScript(go, "NauEnemicEspecial");

        GuardarPrefab(go, "NauEnemicEspecial");
    }

    // ─────────────────────────────────────────
    // PROJECTIL JUGADOR — cubo amarillo pequeño
    // ─────────────────────────────────────────
    static void CrearProjectilJugador()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = "ProjectilJugador";
        go.transform.localScale = new Vector3(0.15f, 0.5f, 0.15f);
        go.tag = "ProjectilJugador";

        Material mat = CrearMaterial("MatProjectilJugador", new Color(1f, 1f, 0.2f));
        go.GetComponent<Renderer>().sharedMaterial = mat;

        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationY
                       | RigidbodyConstraints.FreezeRotationZ
                       | RigidbodyConstraints.FreezePositionZ;

        BoxCollider col = go.GetComponent<BoxCollider>();
        col.isTrigger = true;

        AfegirScript(go, "ProjectilJugador");

        GuardarPrefab(go, "ProjectilJugador");
    }

    // ─────────────────────────────────────────
    // PROJECTIL ENEMIC — cubo rosa
    // ─────────────────────────────────────────
    static void CrearProjectilEnemic()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = "ProjectilEnemic";
        go.transform.localScale = new Vector3(0.15f, 0.5f, 0.15f);
        go.tag = "ProjectilEnemic";

        Material mat = CrearMaterial("MatProjectilEnemic", new Color(1f, 0.4f, 0.8f));
        go.GetComponent<Renderer>().sharedMaterial = mat;

        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationY
                       | RigidbodyConstraints.FreezeRotationZ
                       | RigidbodyConstraints.FreezePositionZ;

        BoxCollider col = go.GetComponent<BoxCollider>();
        col.isTrigger = true;

        AfegirScript(go, "ProjectilEnemic");

        GuardarPrefab(go, "ProjectilEnemic");
    }

    // ─────────────────────────────────────────
    // PROJECTIL ENEMIC ESPECIAL — cubo morado
    // ─────────────────────────────────────────
    static void CrearProjectilEnemicEspecial()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = "ProjectilEnemicEspecial";
        go.transform.localScale = new Vector3(0.2f, 0.6f, 0.2f);
        go.tag = "ProjectilEnemicEspecial";

        Material mat = CrearMaterial("MatProjectilEspecial", new Color(0.6f, 0.2f, 1f));
        go.GetComponent<Renderer>().sharedMaterial = mat;

        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationY
                       | RigidbodyConstraints.FreezeRotationZ
                       | RigidbodyConstraints.FreezePositionZ;

        BoxCollider col = go.GetComponent<BoxCollider>();
        col.isTrigger = true;

        AfegirScript(go, "ProjectilEnemicEspecial");

        GuardarPrefab(go, "ProjectilEnemicEspecial");
    }

    // ─────────────────────────────────────────
    // OBJEC VIDA — cubo verde
    // ─────────────────────────────────────────
    static void CrearObjecVida()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = "ObjecVida";
        go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        go.tag = "Vida";

        Material mat = CrearMaterial("MatVida", new Color(0.2f, 1f, 0.3f));
        go.GetComponent<Renderer>().sharedMaterial = mat;

        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationY
                       | RigidbodyConstraints.FreezeRotationZ
                       | RigidbodyConstraints.FreezePositionZ;

        // Quitamos el BoxCollider y ponemos SphereCollider
        Object.DestroyImmediate(go.GetComponent<BoxCollider>());
        SphereCollider sc = go.AddComponent<SphereCollider>();
        sc.isTrigger = true;
        sc.radius = 0.5f;

        AfegirScript(go, "ObjecVida");

        GuardarPrefab(go, "ObjecVida");
    }

    // ─────────────────────────────────────────
    // EXPLOSIO — sistema de partículas
    // ─────────────────────────────────────────
    static void CrearExplosio()
    {
        GameObject go = new GameObject("Explosio");

        // Particle System
        ParticleSystem ps = go.AddComponent<ParticleSystem>();
        ParticleSystem.MainModule main = ps.main;
        main.duration = 0.5f;
        main.loop = false;
        main.startLifetime = 0.6f;
        main.startSpeed = 5f;
        main.startSize = 0.3f;
        main.startColor = new Color(1f, 0.5f, 0.1f); // naranja
        main.stopAction = ParticleSystemStopAction.Destroy;
        main.maxParticles = 30;

        // Burst: 20 partículas al instante
        ParticleSystem.EmissionModule emission = ps.emission;
        emission.SetBursts(new ParticleSystem.Burst[]
        {
            new ParticleSystem.Burst(0f, 20)
        });
        emission.rateOverTime = 0;

        // Shape: esfera
        ParticleSystem.ShapeModule shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 0.3f;

        AfegirScript(go, "Explosio");

        GuardarPrefab(go, "Explosio");
    }

    // ─────────────────────────────────────────
    // HELPERS
    // ─────────────────────────────────────────

    static Material CrearMaterial(string nom, Color color)
    {
        string path = $"Assets/Materials/{nom}.mat";
        Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);
        if (mat == null)
        {
            mat = new Material(Shader.Find("Standard"));
            mat.color = color;
            AssetDatabase.CreateAsset(mat, path);
        }
        return mat;
    }

    static void AfegirScript(GameObject go, string nomScript)
    {
        // Busca el tipus per nom entre tots els assemblies
        foreach (var assembly in System.AppDomain.CurrentDomain.GetAssemblies())
        {
            System.Type tipus = assembly.GetType(nomScript);
            if (tipus != null && tipus.IsSubclassOf(typeof(MonoBehaviour)))
            {
                go.AddComponent(tipus);
                return;
            }
        }
        Debug.LogWarning($"⚠️ Script '{nomScript}' no trobat. Afegeix-lo manualment al prefab.");
    }

    static void GuardarPrefab(GameObject go, string nom)
    {
        string path = $"Assets/Prefabs/{nom}.prefab";
        PrefabUtility.SaveAsPrefabAsset(go, path);
        Object.DestroyImmediate(go);
        Debug.Log($"  ✓ Prefab creat: {path}");
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