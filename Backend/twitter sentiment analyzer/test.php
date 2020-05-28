


<!DOCTYPE html>
<html>
	<head>
		<title>Stage 1: Sentiment Analysis</title>
		<link rel="stylesheet" type="text/css" href="css/style.css">
		<script src="js/Chart.js"></script>
		<script src="js/jquery-3.3.1.min.js"></script>
		<scriptsrc="//cdnjs.cloudflare.com/ajax/libs/numeral.js/1.4.5/numeral.min.js"> </script>
	</head>
	<body class="body">
		<?php
		$myFile = "output.txt";
		
		$lines = file($myFile);//file in to an array
		$first = (int)$lines[0]; //line 1
		$first1 = (int)$lines[1];
		$first2 = (int)$lines[2];
		$total = $first + $first1 + $first2;
		$p = (int)(($first/$total)*100);
		$neg = (int)(($first1/$total)*100);
		$neu = (int)(($first2/$total)*100);

	//	echo $first1;
	
		?>
		<!--Header-->
		<header class="header">
			<div class="left">
				<a href="index.html"><img src="images/bird.png" alt="bird" ></a>
			</div>
			
		</header>
		<div align="center">
		<h1>Stage 1: Sentiment Analysis</h1>
		</div>
		<!--Pie Chart-->
		<div align="center">
		<canvas id="mycanvas" width="500" height="500" chart-legend="true" chart-options="tooltipTemplate:  <%=label%>: <%= numeral(value).format('($00[.]00)') %> - <%= numeral(circumference / 6.283).format('(0[.][00]%)') %>">
		<script>
			$(document).ready(function(){
				var ctx = $("#mycanvas").get(0).getContext("2d");

				//pie chart data
				//sum of values = 360
				var data = [
					{
						value: <?php echo $p; ?>,
						color: "cornflowerblue",
						highlight: "lightskyblue",
						label: "Positive"
					},
					{
						value: <?php echo $neg; ?>,
						color: "lightgreen",
						highlight: "yellowgreen",
						label: "Negative"
					},
					{
						value: <?php echo $neu; ?>,
						color: "orange",
						highlight: "darkorange",
						label: "Neutral"
					}
				];

				//draw
				var piechart = new Chart(ctx).Pie(data);
			});
		</script>
		</div>
		<!--End Pie Chart-->
		<div>
			<form action="stage2.php">
			<button class="button2">Stage 2</button>
			
			</form>
		</div>
	</body>
</html>

