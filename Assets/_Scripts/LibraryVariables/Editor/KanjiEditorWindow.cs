using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class KanjiEditorWindow {

	public ScrObjLibraryVariable kanjiLibrary;
	public Kanji kanjiBase;
	private KanjiValues kv;

	// Selection screen
	Rect selectRect = new Rect();
	Texture2D selectTex;
	Vector2 scrollPos;
	int selKanji = -1;

	// Display screen
	Rect dispRect = new Rect();
	RectOffset dispOffset = new RectOffset();
	Texture2D dispTex;
	Vector2 dispScrollPos;
	int valuePage = 0;
	string[] pageStrings = {"VALUES", "ACTIVATION", "EFFECT"};
	KanjiActivation activation;
	KanjiEffect effect;

	//Creation
	string uuid = "";
	Color repColor = new Color(0,0,0,1f);


	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="entries"></param>
	/// <param name="container"></param>
	public KanjiEditorWindow(ScrObjLibraryVariable entries, Kanji container){
		kanjiLibrary = entries;
		kanjiBase = container;
		LoadLibrary();
	}

	void LoadLibrary() {

		Debug.Log("Loading kanji libraries...");

		kanjiLibrary.GenerateDictionary();

		Debug.Log("Finished loading kanji libraries");

		InitializeWindow();
	}

	public void InitializeWindow() {
		selectTex = new Texture2D(1, 1);
		selectTex.SetPixel(0, 0, new Color(0.8f, 0.8f, 0.8f));
		selectTex.Apply();

		dispTex = new Texture2D(1, 1);
		dispTex.SetPixel(0, 0, new Color(0.1f, 0.6f, 0.6f));
		dispTex.Apply();

		dispOffset.right = 10;

		kanjiBase.ResetValues();
	}


	public void DrawWindow() {

		GUILayout.BeginHorizontal();
		GUILayout.Label("Kanji Editor", EditorStyles.boldLabel);
		if (selKanji != -1) {
			if (GUILayout.Button("Save Kanji")){
				SaveSelectedKanji();
			}
		}
		GUILayout.EndHorizontal();

		GenerateAreas();
		DrawBackgrounds();
		DrawEntryList();
		if (selKanji != -1)
			DrawDisplayWindow();
	}

	void GenerateAreas() {

		selectRect.x = 0;
		selectRect.y = 50;
		selectRect.width = 200;
		selectRect.height = Screen.height - 50;

		dispRect.x = 200;
		dispRect.y = 50;
		dispRect.width = Screen.width - 200;
		dispRect.height = Screen.height - 50;
	}

	void DrawBackgrounds() {
		GUI.DrawTexture(selectRect, selectTex);
		GUI.DrawTexture(dispRect, dispTex);
	}

	void DrawEntryList() {
		GUILayout.BeginArea(selectRect);

		scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(selectRect.width), 
						GUILayout.Height(selectRect.height-110));

		int oldSelected = selKanji;
		selKanji = GUILayout.SelectionGrid(selKanji, kanjiLibrary.GetRepresentations(),1);
		EditorGUILayout.EndScrollView();

		if (oldSelected != selKanji)
			SelectKanji();

		EditorGUIUtility.labelWidth = 110;
		GUILayout.Label("Create new kanji", EditorStyles.boldLabel);
		uuid = EditorGUILayout.TextField("Kanji Name", uuid);

		if (GUILayout.Button("Create new")) {
			InstansiateKanji();
		}
		if (GUILayout.Button("Delete kanji")) {
			DeleteKanji();
		}

		GUILayout.EndArea();
	}

	void DrawDisplayWindow() {
		kv = kanjiBase.values;
		GUILayout.BeginArea(dispRect);
		dispScrollPos = GUILayout.BeginScrollView(dispScrollPos, GUILayout.Width(dispRect.width), 
							GUILayout.Height(dispRect.height-25));

		GUI.skin.textField.margin.right = 20;

		GUILayout.Label("Selected Kanji", EditorStyles.boldLabel);
		EditorGUILayout.SelectableLabel("UUID: " + kanjiBase.uuid);
		kanjiBase.entryName = EditorGUILayout.TextField("Name", kanjiBase.entryName);
		// kanjiBase.repColor = EditorGUILayout.ColorField("List color", kanjiBase.repColor);
		kanjiBase.icon = (Sprite)EditorGUILayout.ObjectField("Kanji Icon", kanjiBase.icon, typeof(Sprite),false);
		// kanjiBase.tintColor = EditorGUILayout.ColorField("Kanji Tint Color", kanjiBase.tintColor);

		GUILayout.Label("Kanji values", EditorStyles.boldLabel);
		valuePage = GUILayout.Toolbar(valuePage, pageStrings);

EditorGUIUtility.labelWidth = 150;

		switch(valuePage)
		{
			case 0: DrawUsagePart(); break;
			case 1: DrawActivationPart(); break;
			case 2: DrawEffectPart(); break;
		}

EditorGUIUtility.labelWidth = 100;

		GUILayout.EndScrollView();
		GUILayout.EndArea();
	}

	void DrawUsagePart() {
		//Usage values
		GUILayout.Label("Usage Values", EditorStyles.boldLabel);
		kv.startCooldownTime = EditorGUILayout.FloatField("Start Cooldown Delay", kv.startCooldownTime);
		kv.maxCharges = EditorGUILayout.IntField("Max Charges", kv.maxCharges);
		kv.delay = EditorGUILayout.FloatField("Usage Delay", kv.delay);
		kv.cooldown = EditorGUILayout.FloatField("Recharge Cooldown", kv.cooldown);

		GUILayout.Space(10);

		//Other values
		GUILayout.Label("Other Values", EditorStyles.boldLabel);
		kanjiBase.moneyValue = EditorGUILayout.IntField("Money Value", kanjiBase.moneyValue);
	}

	void DrawActivationPart() {
		//Activation values
		GUILayout.Label("Activation values", EditorStyles.boldLabel);
		kv.kanjiType = (KanjiValues.KanjiType)EditorGUILayout.EnumPopup("Kanji Type",kv.kanjiType);
		kv.area = EditorGUILayout.FloatField("Activate Area", kv.area);
		kv.holdMin = EditorGUILayout.FloatField("Minimum Hold Duration", kv.holdMin);
		kv.holdMax = EditorGUILayout.FloatField("Maximum Hold Duration", kv.holdMax);

		GUILayout.Space(10);

		// Activation requirements
		GUILayout.Label("Activation Requirements", EditorStyles.boldLabel);
		if (GUILayout.Button("Add Activation Requirement")) {
			kanjiBase.activations.Add(null);
		}

		for (int i = 0; i < kanjiBase.activations.Count; i++) {
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("X", GUILayout.Width(30))){
				kanjiBase.activations.RemoveAt(i);
				i--;
				continue;
			}
			kanjiBase.activations[i] = (KanjiActivation)EditorGUILayout.ObjectField(kanjiBase.activations[i], typeof(KanjiActivation), false);
			GUILayout.EndHorizontal();
		}
	}

	void DrawEffectPart() {
		//Damage
		GUILayout.Label("Damage", EditorStyles.boldLabel);
		kv.damage = EditorGUILayout.IntField("Base Damage", kv.damage);;
		kv.baseDamageScale = EditorGUILayout.FloatField("Additional Damage Scale", kv.baseDamageScale);

		GUILayout.Space(10);

		//Projectile
		GUILayout.Label("Projectile", EditorStyles.boldLabel);
		kv.projectile = (Transform)EditorGUILayout.ObjectField("Projectile Object", kv.projectile, typeof(Transform),false);
		kv.effect = (Transform)EditorGUILayout.ObjectField("Effect Object", kv.effect, typeof(Transform),false);
		kv.projectileSpeed = EditorGUILayout.Vector2Field("Projectile Speed", kv.projectileSpeed);
		kv.projectileLifetime = EditorGUILayout.FloatField("Projectile Lifetime", kv.projectileLifetime);

		GUILayout.Space(10);

		// Effect creators
		GUILayout.Label("Kanji Effects", EditorStyles.boldLabel);
		if (GUILayout.Button("Add Effect")) {
			kanjiBase.effects.Add(null);
		}

		for (int i = 0; i < kanjiBase.effects.Count; i++) {
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("X", GUILayout.Width(30))){
				kanjiBase.effects.RemoveAt(i);
				i--;
			}
			kanjiBase.effects[i] = (KanjiEffect)EditorGUILayout.ObjectField(kanjiBase.effects[i], typeof(KanjiEffect), false);
			GUILayout.EndHorizontal();
		}
	}

	void SelectKanji() {
		GUI.FocusControl(null);
		if (selKanji == -1) {
			// Nothing selected
			kanjiBase.ResetValues();
		}
		else {
			// Something selected
			Kanji ce = (Kanji)kanjiLibrary.GetEntryByIndex(selKanji);
			kanjiBase.CopyValues(ce);
		}
	}

	void SaveSelectedKanji() {
		Kanji ce = (Kanji)kanjiLibrary.GetEntryByIndex(selKanji);
		ce.CopyValues(kanjiBase);
		Undo.RecordObject(ce, "Updated kanji");
		EditorUtility.SetDirty(ce);
	}

	void InstansiateKanji() {
		GUI.FocusControl(null);
		if (kanjiLibrary.ContainsID(uuid)) {
			Debug.Log("uuid already exists!");
			return;
		}
		Kanji c = Editor.CreateInstance<Kanji>();
		c.name = uuid;
		c.uuid = uuid;
		c.entryName = uuid;
		c.repColor = repColor;
		string path = "Assets/LibraryData/Kanji/" + uuid + ".asset";

		AssetDatabase.CreateAsset(c, path);
		kanjiLibrary.InsertEntry(c, 0);
		Undo.RecordObject(kanjiLibrary, "Added kanji");
		EditorUtility.SetDirty(kanjiLibrary);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		uuid = "";
		selKanji = 0;
		SelectKanji();
	}

	void DeleteKanji() {
		GUI.FocusControl(null);
		Kanji c = (Kanji)kanjiLibrary.GetEntryByIndex(selKanji);
		string path = "Assets/LibraryData/Kanji/" + c.uuid + ".asset";

		kanjiLibrary.RemoveEntryByIndex(selKanji);
		Undo.RecordObject(kanjiLibrary, "Deleted kanji");
		EditorUtility.SetDirty(kanjiLibrary);
		bool res = AssetDatabase.MoveAssetToTrash(path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		if (res) {
			Debug.Log("Removed kanji: " + c.uuid);
			selKanji = -1;
		}
	}
}
