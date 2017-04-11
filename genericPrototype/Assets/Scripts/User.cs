using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class User : MonoBehaviour {

    public string heroName;
    public string email;

    void Start() {
        // Set this before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://stepcrusade.firebaseio.com/");

        // Get the root reference location of the database.
        DatabaseReference rootRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public User() {
    }

    public User(string heroName, string email) {
        this.heroName = heroName;
        this.email = email;
    }
    

}
