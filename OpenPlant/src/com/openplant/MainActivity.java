package com.openplant;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpResponseException;
import org.ksoap2.transport.HttpTransportSE;
import org.xmlpull.v1.XmlPullParserException;

import android.os.AsyncTask;
import android.os.Bundle;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.view.Menu;

public class MainActivity extends Activity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		
		CouchTestTask task = new CouchTestTask();
		task.execute();
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}
	
	private class CouchTestTask extends AsyncTask<Void, Void, List<String>> {
		
		private static final String action = "GetFirstData";
		
		@Override
		protected List<String> doInBackground(Void... params) {
			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
			SoapObject request = new SoapObject("http://tempuri.org/", action);
			envelope.bodyOut = request;
			HttpTransportSE transport = new HttpTransportSE("http://10.145.128.97:8734/MongoService/Service/?wsdl");
			
			try {
				transport.call("http://tempuri.org/IService/" + action, envelope);
			} catch (HttpResponseException e) {
				e.printStackTrace();
			} catch (IOException e) {
				e.printStackTrace();
			} catch (XmlPullParserException e) {
				e.printStackTrace();
			}
			
			SoapObject results = (SoapObject)envelope.bodyIn;
			
			List<String> data = new ArrayList<String>();
			
			data.add(results.getAttributeAsString(0));
			
			return data;
		}
		
		@Override
		protected void onPostExecute(List<String> result) {
			StringBuilder sb = new StringBuilder();
			
			for(String s: result)
				sb.append(s + "\n");
			
			new AlertDialog.Builder(MainActivity.this)
				.setTitle("Results")
				.setMessage(sb.toString())
				.setNeutralButton(android.R.string.ok, new DialogInterface.OnClickListener() {
					@Override
					public void onClick(DialogInterface dialog, int which) {
						// null
					}
				})
				.show();
		}
	}
}
