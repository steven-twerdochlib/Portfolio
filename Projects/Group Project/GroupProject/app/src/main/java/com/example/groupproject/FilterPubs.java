package com.example.groupproject;

import android.app.Activity;
import android.content.Intent;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.view.Window;

public class FilterPubs extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.activity_filter_pubs);
    }

    public void goToListOfPubs(View view){
        Intent goToPubInfoActivity = new Intent(this, ListOfPubs.class);
        startActivity(goToPubInfoActivity);
    }
}
