package com.example.groupproject;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

public class PubCrawlList extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_pub_crawl_list);
    }

    public void goToRandomPubCrawl(View view){
        Intent goTogoToRandomPubCrawlActivity = new Intent(this, random_pub_crawl.class);
        startActivity(goTogoToRandomPubCrawlActivity);
    }
}
