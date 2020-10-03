package com.example.groupproject;

import android.content.Intent;
import android.graphics.Color;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.DynamicLayout;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        createLayoutDynamically(10);
    }

    public void goToListOfPubs(View view){
        Intent goToListOfPubsActivity = new Intent(this, ListOfPubs.class);
        startActivity(goToListOfPubsActivity);
    }

    private void createLayoutDynamically(int n) {
        for (int i = 0; i < n; i++) {
            Button myButton = new Button(this);
            myButton.setText("Button: "+i);
            myButton.setId(i);
            final int id_ = myButton.getId();

            LinearLayout layout = (LinearLayout) findViewById(R.id.myDynamicLayout);
            layout.addView(myButton);

            myButton.setOnClickListener(new View.OnClickListener() {
                public void onClick(View view) {
                    Intent goToListOfPubsActivity = new Intent(MainActivity.this, ListOfPubs.class);
                    startActivity(goToListOfPubsActivity);
                }
            });
        }
    }
}
