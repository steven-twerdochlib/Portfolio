package com.example.groupproject;

import android.app.Activity;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.Window;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.PopupWindow;

public class AddToPubCrawl extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.activity_add_to_pub_crawl);
    }

    public void onButtonShowPopupWindowClick(View view) {

        // inflate the layout of the popup window
        LayoutInflater inflater = (LayoutInflater)
                getSystemService(LAYOUT_INFLATER_SERVICE);
        View popupView = inflater.inflate(R.layout.activity_pop_up_time, null);

        // create the popup window
        int width = LinearLayout.LayoutParams.WRAP_CONTENT;
        int height = LinearLayout.LayoutParams.WRAP_CONTENT;
        boolean focusable = true; // lets taps outside the popup also dismiss it
        final PopupWindow popupWindow = new PopupWindow(popupView, width, height, focusable);

        // show the popup window
        // which view you pass in doesn't matter, it is only used for the window tolken
        popupWindow.showAtLocation(view, Gravity.CENTER, 0, 0);

        Button close = (Button) popupView.findViewById(R.id.confirmBtn);
        close.setOnClickListener(new View.OnClickListener() {

            public void onClick(View popupView) {
                popupWindow.dismiss();
            }
        });
    }

    public void goToRandomPubCrawl(View view){
        Intent goToRandomPubCrawlActivity = new Intent(this, random_pub_crawl.class);
        startActivity(goToRandomPubCrawlActivity);
    }

    public void ConfirmPopupWindowClick(View view) {

        // inflate the layout of the popup window
        LayoutInflater inflater = (LayoutInflater)
                getSystemService(LAYOUT_INFLATER_SERVICE);
        View popupView = inflater.inflate(R.layout.activity_confirm_pub_crawl, null);

        // create the popup window
        int width = LinearLayout.LayoutParams.WRAP_CONTENT;
        int height = LinearLayout.LayoutParams.WRAP_CONTENT;
        boolean focusable = true; // lets taps outside the popup also dismiss it
        final PopupWindow popupWindow = new PopupWindow(popupView, width, height, focusable);

        // show the popup window
        // which view you pass in doesn't matter, it is only used for the window tolken
        popupWindow.showAtLocation(view, Gravity.CENTER, 0, 0);

        Button noBtn = (Button) popupView.findViewById(R.id.NoBtn);
        noBtn.setOnClickListener(new View.OnClickListener() {

            public void onClick(View popupView) {
                popupWindow.dismiss();
            }
        });

        Button yesBtn = (Button) popupView.findViewById(R.id.YesBtn);
        yesBtn.setOnClickListener(new View.OnClickListener() {

            public void onClick(View popupView) {
                popupWindow.dismiss();
                Intent goToRandomPubCrawlActivity = new Intent(AddToPubCrawl.this, random_pub_crawl.class);
                startActivity(goToRandomPubCrawlActivity);
            }
        });
    }
}
