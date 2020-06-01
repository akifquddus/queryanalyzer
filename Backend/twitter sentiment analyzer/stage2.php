<!DOCTYPE html>
<html>
	<head>
		<title>Stage 2 : Twitter Sentiment Analysis</title>
		<link rel="stylesheet" type="text/css" href="css/style.css">
		<script src="js/Chart.js"></script>
		<script src="js/jquery-3.3.1.min.js"></script>
	</head>
	<body class="body">
		<?php
		$myFile = "output.txt";
		$myFile2 = "conslist.txt";
		$myFile3 = "proslist.txt";
		$cons = file($myFile2);
		$pros = file($myFile3);
		$lines = file($myFile);//file in to an array
		$first = (int)$lines[3]; //line 1
		$first1 = (int)$lines[4];
		$first2 = (int)$lines[5];
		$total = $first + $first1 + $first2;
		$p = (int)(($first/$total)*100);
		$neg = (int)(($first1/$total)*100);
		$neu = (int)(($first2/$total)*100);
	//	echo $cons;
		//echo "I like " . $cons[1] ;
		?>
		<!--Header-->
		<header class="header">
			<div class="left">
				<a href="index.html"><img src="images/bird.png" alt="bird" ></a>
			</div>
			
		</header>
		<div align="center">
		<h1>Stage 2: Sentiment Analysis</h1>
		</div>
		<!--Pie Chart-->
		<div align="center">
		<canvas id="mycanvas" width="500" height="500">
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
						color: "yellowgreen",
						highlight: "lightgreen",
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
		<script type="text/javascript">
			function pros(){
				document.getElementById("list1").innerHTML = "<?php echo $pros[1]; ?>";
			}
		</script>
		<script type="text/javascript">
			function cons(){
				document.getElementById("list2").innerHTML = "<?php echo $cons[1]; ?>";
			}
		</script>
		</div>
		<div>
			<button class="button3" onclick="pros()">Pros</button>
			<button class="button4" onclick="cons()">Cons</button>
			<form action="index.html">
			<button class="button5">New Search</button>
			</form>
		</div>
		<p id="list1" class="pros"></p>
		<p id="list2"></p>
	</body>
</html>
