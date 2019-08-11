<%@page import="org.json.JSONObject"%>
<%@page import="org.json.JSONArray"%>
<html>
	<head>
		<title>question</title>
		<meta http-equiv="cache-control" content="no-cache">
		<style>
		
			<%@include file="/view/css/main.css"%>
			<link rel="stylesheet" type="text/css" href="/view/css/util.css">
			<link rel="stylesheet" type="text/css" href="/view/css/main.css">
		</style>
	</head>
	<body>
	<div class="limiter">
		<div class="container-login100" style="background-image: url('/view/images/bg-01.jpg')">
		<div class="wrap-login100">
			<form action="http://localhost:8085/questionSubmition" method="get" class="login100-form validate-form">
				<% 
				if(request.getAttribute("questionMeta")!=null) {
					//out.print(request.getAttribute("questionMeta"));
					JSONObject jobj = new JSONObject(request.getAttribute("questionMeta").toString());
					%>
					<div class="wrap-input100 validate-input">
					<span class="login100-form-title p-b-34 p-t-27">
					<%
					out.print(jobj.getString("question"));
					%>
					</span>
					</div>
					
					<%
					if(jobj.getString("question_type")!=null&&jobj.getString("question_type").toString().equals("mcq")) {
						if(jobj.get("options")!=null) {
							JSONArray jarr=new JSONArray(jobj.get("options").toString());
							%>
							<div class="container-login100-form-btn">
							<%
							for(int i=0;i<jarr.length();i++) {
								if(!jarr.get(i).toString().equals(""))
									out.println("<input type='submit' name='option' class='login100-form-btn' value='"+jarr.get(i).toString()+"'/>");
								System.out.println(jarr.get(i));
							}
						}
						%></div><%
					} else if(jobj.getString("question_type")!=null&&jobj.getString("question_type").toString().equals("subjective")) {
						%>
						<div class="wrap-input100 validate-input">
						<textarea name="opinion" rows="4" cols="55"></textarea><br/>
						</div>
						<div class="container-login100-form-btn">
						<input type='submit' class='login100-form-btn'/></div>
					
					<%
					} else if(jobj.getString("question_type")!=null&&jobj.getString("question_type").toString().equals("scale")) {
					%>
						<div class="contact100-form-checkbox">
						<div class="slidecontainer">
						<input type="range" min="1" max="10" value="5" class="slider" id="myRange"><br/>
						</div></div>
						<div class="container-login100-form-btn">
						<input type='submit' class='login100-form-btn'/>
						</div>
						
					<%
							}
						} else {
							out.print("error getting response from server");
						}
					%>
				
			</form>
			</div>
		</div>
	</div>
	</body>
</html>