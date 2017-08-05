<?php
$bingoValuesArray = [];
$mathArray = [];
$numberOfColumns = 5;
$rangeDiff = 15;
$freeSpaceLocation = 2;

function randomArray($condition,$min,$max) {
	global $bingoValuesArray;
	while (count($bingoValuesArray) < $condition) { 
		$rand = rand($min,$max);
		array_push($bingoValuesArray,$rand);
		$bingoValuesArray = array_unique($bingoValuesArray);
	}
}

for ($j=-1; $j < $numberOfColumns-1; $j++) { 
	$mathArray[] = ($rangeDiff * $j) + $rangeDiff + 1;
}

for ($i=0; $i < $numberOfColumns; $i++) {
	randomArray(($numberOfColumns * $i) + $numberOfColumns,$mathArray[$i],($rangeDiff * $i) + $rangeDiff);
}

$bingoValuesArray = array_chunk($bingoValuesArray,$numberOfColumns);

$bingoValuesArray[$freeSpaceLocation][$freeSpaceLocation]="Free Space";
?>

<!doctype html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Bingo</title>
</head>

<style>
html, body { 
	height: 100%; 
	width: 100%;
}
body div {
	height: 80%;
	width: 80%;
	padding: 5%;
}
table {
	height: 70%;
	width: 80%;
	float: center;
	text-align: center;
}
td, tr {
	width: 5%;
	background: #00FFFF;
	font-size: 20px;
	border: 1px solid black;
}
th {
	background: #F5F5DC;
	color: #00008B;
	border: 1px solid black;
}
td:only-child {
	background: white;
	border: hidden;
}
button {
	font-size: 15px;
}
a {
	color: black;
	text-decoration: none;
}
</style>

<body>
	<div>
	<?php
		echo "
		<table>
			<tr>
				<th>B</th>
				<th>I</th>
				<th>N</th>			
				<th>G</th>			
				<th>O</th>				
			</tr>";
			echo "<tr>";
			for ($i=0; $i < $numberOfColumns; $i++) { 
				for ($j=0; $j < $numberOfColumns; $j++) { 
					echo "<td>" . $bingoValuesArray[$j][$i] . "</td>";
				}
			echo "</tr>";
			}
			echo "<tr><td colspan=5><button><a href=\"" . $_SERVER["PHP_SELF"] . "\">Refresh</a></button></td></tr>";
		echo "</table>";
	?>
	</div>
</body>

</html>
