using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ManualWindow : EditorWindow
{
    Vector2 scrollPos;
    Texture[] textures=new Texture[15];
    Texture playTex;
    Texture stopTex;
    bool fade1;
    bool fade2;
    bool fade3;
    char checkMark = '\u2713';
    [MenuItem("Window/Camera Utilities/info...", false, 10)]
    public static void ShowWindow()
    {
        var camManager = GetWindow<ManualWindow>("Camera Manager");
        for (int i = 0; i < camManager.textures.Length; i++)
        {
           camManager.textures[i] = AssetDatabase.LoadAssetAtPath<Texture>($"Assets/ResourceImagesManual/{i}.png");
        }
        camManager.playTex = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Textures/play.png");
        camManager.stopTex = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Textures/stop.png");

    }
    private void OnGUI()
    {
        scrollPos=EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height));
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft };
        fade1 = EditorGUILayout.BeginFoldoutHeaderGroup(fade1, "Paso 1: Como empezar");
        maxSize = new Vector2(445, 900);
        minSize = new Vector2(445, 445);
        if (fade1)
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Para arrancar tienes que crear un objeto nuevo y agregarle el \n " +
                "            componente PathEditor como se muestra en la siguiente imagen");
            GUILayout.Box(textures[0]);
            GUILayout.Label("Asi se vera el componente PathEditor una vez añadido ");
            GUILayout.Box(textures[1]);
            GUILayout.Label("los parametros disponibles a modificar son: \n");

            GUILayout.BeginHorizontal();
            GUILayout.Label("Path:", EditorStyles.boldLabel);
            GUILayout.Label("este es el camino a seguir", style);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Smoothness:", EditorStyles.boldLabel);
            GUILayout.Label("controla la cantidad de puntos en el camino",style);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Units:", EditorStyles.boldLabel);
            GUILayout.Label("controla la cantidad de unidades en la que el camino \n" +
                            "sera dibujado en relacion a la posicion del objeto");
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Uniform Scale:", EditorStyles.boldLabel);
            GUILayout.Label("activa o desactiva el escalado uniforme");
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Add:", EditorStyles.boldLabel);
            GUILayout.Label("agrega un punto mas al camino");
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Remove:", EditorStyles.boldLabel);
            GUILayout.Label("remueve un punto al camino");
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Reload:", EditorStyles.boldLabel);
            GUILayout.Label("se usa para actualizar la posicion de los puntos \n"+
                            "del camino y guardar los cambios");
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Delete:", EditorStyles.boldLabel);
            GUILayout.Label("elimina el camino");
            GUILayout.EndHorizontal();


            GUILayout.Label("Para Crear un nuevo camino debes crear un nuevo objeto \n" +
                            "con el menu contextual " + "(Create>ScriptableObjects>Paths) \n" +
                            "como lo muestra la siguiente imagen");
            GUILayout.Box(textures[2]);
            GUILayout.Label("Asi se vera el camino en escena cuando el objeto PathEditor \n" +
                            "este seleccionado, usa los gizmos para modificar el camino\n" +
                            "");
            GUILayout.Box(textures[3]);

            GUILayout.Space(20);
            EditorGUILayout.EndVertical();
        }
       EditorGUILayout.EndFoldoutHeaderGroup();
       fade2 = EditorGUILayout.BeginFoldoutHeaderGroup(fade2, "Paso 2: Crear y configurar un Traveller");
        if (fade2)
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Para crear un traveller lo primero que hay que hacer es \n" +
                            "crear un nuevo objeto con el componente PlayMovement");
            GUILayout.Box(textures[4]);
            EditorGUILayout.EndVertical();
            GUILayout.Label("Asi se ve el componente");
            GUILayout.Box(textures[5]);
            GUILayout.Label("Crea un traveller presionando el boton ''create traveller''");
            GUILayout.Label("Se Creara un nuevo objeto como el siguiente");
            GUILayout.Box(textures[6]);
            GUILayout.Label("Y tendra los siguientes parametros");
            GUILayout.Box(textures[7]);
            GUILayout.BeginHorizontal();
            GUILayout.Label("List Container:", EditorStyles.boldLabel);
            GUILayout.Label("Aqui deberas poner el camino previamente creado ", style);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Speed:", EditorStyles.boldLabel);
            GUILayout.Label("Controla la velocidad con la que el traveller recorrera el camino", style);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("SpeedRotation:", EditorStyles.boldLabel);
            GUILayout.Label("Controla la velocidad con la que el traveller rotara", style);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Threshold:", EditorStyles.boldLabel);
            GUILayout.Label("Controla la distancia en que el traveller cambia su direccion", style);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Loop:", EditorStyles.boldLabel);
            GUILayout.Label("Controla si el traveller repite el camino cuando termina", style);
            GUILayout.EndHorizontal();
            GUILayout.Label("Controls:", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Button(playTex, GUILayout.MaxWidth(20), GUILayout.MaxHeight(20));
            GUILayout.Label("Se utiliza para reproducir una vista previa del recorrido \n" +
                "(tenga en  cuenta que luego debera resetear su posicion)", style);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Button(stopTex, GUILayout.MaxWidth(20), GUILayout.MaxHeight(20));
            GUILayout.Label("Se utiliza para resetear la posicion del recorrido ", style);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Follow Path: ", EditorStyles.boldLabel);
            GUILayout.Label("Si esta desactivado, el traveler seguira la direccion del \n" +
                "recorrido.\n" +
                "por el contrario, seguira una rotacion personalizada ", style);
            GUILayout.EndHorizontal();
            GUILayout.Label("Este es el visor de Rotaciones personalizadas, aqui veras \n" +
                "las rotaciones del recorrido, podras ver el gizmos presionando dicho icono", style);
            GUILayout.Box(textures[8]);
            
            GUILayout.Label("Presiona "+checkMark.ToString() +" para elimiar las rotaciones o presiona + para añadirlas\n" +
                "la linea punteada es la direccion a donde mirara el traveller en determinado\n" +
                " punto del recorrido", style);
            GUILayout.Box(textures[9]);
            
        }
       EditorGUILayout.EndFoldoutHeaderGroup();
        fade3 = EditorGUILayout.BeginFoldoutHeaderGroup(fade3, "Paso 3:Grabar recorrido");
        if (fade3)
        {
            GUILayout.Label("Grabar un recorrido sirve para guardar los comportamientos de un traveller \n" +
                "y ejecutarlos luego en el juego ", style);
            GUILayout.Label("Para grabar un recorrido lo primero que tienes que hacer es ir a windows\n" +
                "y abrir la ventana Record Movement");
            GUILayout.Box(textures[10]);
            GUILayout.Label("Ahora deberas arrastrar todos tus travellers en la ventana y poner un nombre");
            GUILayout.Box(textures[11]);
            GUILayout.Label("Una vez le des al boton Create, se creara una Carpeta llamada Records en\n" +
                " tu carpeta de Scripts, que almacenara la grabacion");
            GUILayout.Box(textures[12]);
            GUILayout.Box(textures[13]);
            GUILayout.Label("Ahora deberas arrastrar tu grabacion al script de PlayMovement creado \n" +
                "previamente");
            GUILayout.Box(textures[14]);
            GUILayout.Label("Debes Arrastrar tantas camaras como Travellers en la grabacion\n" +
               "Para ejecutar el recorrido de manera automatica, deberas dejar marcada la \n" +
               "casilla Auto Play, si lo quieres activar por codigo deberas crear una referencia\n" +
               "de PlayMovement y llamar a su funcion Play() en el Update");
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        GUILayout.Space(20);
        EditorGUILayout.EndScrollView();
    }
}
