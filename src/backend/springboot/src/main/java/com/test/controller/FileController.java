package com.test.controller;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.security.cert.CertificateException;
import java.security.cert.X509Certificate;

import javax.net.ssl.HttpsURLConnection;
import javax.net.ssl.SSLContext;
import javax.net.ssl.TrustManager;
import javax.net.ssl.X509TrustManager;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.tomcat.util.json.JSONParser;
import org.json.JSONObject;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.servlet.ModelAndView;

@RestController
public class FileController {
	@RequestMapping(value = "/hello", method = RequestMethod.GET)  
    public String hello(){
		System.out.println("/hello");
        return "Hello!";  
    }
	
	@RequestMapping(value = "/index", method = RequestMethod.GET)  
    public ModelAndView indexPage(){
		System.out.println("/index");
		ModelAndView mav = new ModelAndView();
		mav.setViewName("index.jsp");
        return mav;  
    }
	
	@RequestMapping(value = "/index2", method = RequestMethod.GET)  
    public ModelAndView indexPage2() throws IOException{
		System.out.println("/index2");
		ModelAndView mav = new ModelAndView();
		String questionMeta = getQuestion();
		if(questionMeta!=null) {
			mav.addObject("questionMeta", questionMeta);
		}
		mav.setViewName("index.jsp");
        return mav;  
    }
	
	@RequestMapping(value = "/questionView", method = RequestMethod.GET)  
    public ModelAndView questionView() throws IOException{
		System.out.println("/questionView");
		ModelAndView mav = new ModelAndView();
		
		String questionMeta = getQuestion();
		if(questionMeta!=null) {
			mav.addObject("questionMeta", questionMeta);
		}
		
		//mav.addObject("questionMeta", "{\"question\":\"is this the third question?\",\"question_type\":\"mcq\",\"options\":[\"option1\",\"option2\"]}");
		mav.setViewName("questionView.jsp");
        return mav;  
    }
	
	@RequestMapping(value = "/questionSubmition", method = RequestMethod.GET)  
    public String questionSubmition(HttpServletRequest request,HttpServletResponse response) throws IOException{
		System.out.println("/questionSubmition");
		for(String a:request.getParameterMap().keySet()) {
			System.out.println(a+":"+request.getParameter(a));
			if(a.equals("option")) {
				return "successfully submitted";
			} else if(a.equals("opinion")) {
				return "successfully submitted";
			} else if(a.equals("slideIndex")) {
				return "successfully submitted";
			}
		}
		return "error processing form";
    }
	
	@RequestMapping(value = "/try", method = RequestMethod.GET)  
    public String getQuestion() throws IOException {
		TrustManager[] trustAllCerts = new TrustManager[]{
			    new X509TrustManager() {
					
					@Override
					public X509Certificate[] getAcceptedIssuers() {
						return null;
					}
					
					@Override
					public void checkServerTrusted(X509Certificate[] chain, String authType) throws CertificateException {
						// TODO Auto-generated method stub
						
					}
					
					@Override
					public void checkClientTrusted(X509Certificate[] chain, String authType) throws CertificateException {
						// TODO Auto-generated method stub
						
					}
				}
			};

			// Install the all-trusting trust manager
			try {
			    SSLContext sc = SSLContext.getInstance("SSL");
			    sc.init(null, trustAllCerts, new java.security.SecureRandom());
			    HttpsURLConnection.setDefaultSSLSocketFactory(sc.getSocketFactory());
			} catch (Exception e) {
				return null;
			}

			// Now you can access an https URL without having the certificate in the truststore
			try {
			    URL url = new URL("https://script.google.com/macros/s/AKfycbzI5etjYBSe639KZxkPxDijeF8_tL3XkvfPgJ1zIl53M14y5Q0/exec");
			    HttpURLConnection con = (HttpURLConnection) url.openConnection();
			    
				// optional default is GET
				con.setRequestMethod("GET");

				String USER_AGENT = "Mozilla/5.0";
				//add request header
				con.setRequestProperty("User-Agent", USER_AGENT);

				int responseCode = con.getResponseCode();
				System.out.println("\nSending 'GET' request to URL : " + url);
				System.out.println("Response Code : " + responseCode);

				BufferedReader in = new BufferedReader(
				        new InputStreamReader(con.getInputStream()));
				String inputLine;
				StringBuffer response = new StringBuffer();

				while ((inputLine = in.readLine()) != null) {
					response.append(inputLine);
				}
				in.close();

				//print result
				System.out.println(response.toString());
				
				JSONObject jobj = new JSONObject(response.toString());
				System.out.print(jobj.getString("question"));
				
				return response.toString();
			} catch (MalformedURLException e) {
				return null;
			} catch(Exception e) {
				return null;
			}
	}
	
}
