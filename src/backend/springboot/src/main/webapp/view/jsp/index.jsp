<html>
	<head>
		<title>haha this is some random title</title>
	</head>
	<body>
		<p>
			<% 
				if(request.getAttribute("questionMeta")!=null) {
					out.print(request.getAttribute("questionMeta")); 
				} else {
					out.print("error getting response from server");
				}
			%>
		</p>
	</body>
</html>