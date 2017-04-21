<?php
$bingoColumnRange = 15;
$numberOfBingoRows = 5;
$freeSpace = 2;
$bingoRows = [];
function generateRandomArrays($minRange) {
        $array = range($minRange, $minRange + $GLOBALS['bingoColumnRange']);
        shuffle($array);
        return $array;
}

for ($i = 0; $i < $numberOfBingoRows; $i++) {
    $bingoRows[] = generateRandomArrays($i * $bingoColumnRange + 1);
}
?>
<!doctype html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <title>Bingo</title>
<style>
html {
    width: 800px;
    background-color: white;
}
body {
    width: 700px;
    background-color: bisque;
}
table {
    margin-left: 20px;
}
th {
    font-size: 4em;
    display: block;
    padding-left: 35px;
    color: blueviolet;
}
#header {
    display: flex;
    letter-spacing: 48px;
    margin-left: 20px;
}
td {
    display: block;
    height: 75px;
    width: 75px;
    border: 1px solid #000;
    font-size: 36px;
    float: left;
    padding-left: 50px;
    background-color: aliceblue;
}
Button {
    background-position: bottom;
    margin-top: 10px;
    margin-left: 280px;
    font-size: 32px;
}
</style>
</head>
<body>
    <table>
        <thead>
            <tr id="header">
                <th>B</th>
                <th>I</th>
                <th>N</th>
                <th>G</th>
                <th>O</th>
            </tr>
        </thead>
        <tbody>
        <?php
        function createBingoCard($array) {
            for ($j = 0; $j < $GLOBALS['numberOfBingoRows']; $j++) {
                echo "<td>";
                $newArray = array_splice($GLOBALS['bingoRows'][$j], 0, 1);
                foreach ($newArray as $value) {
                    if ($j == $GLOBALS['freeSpace'] && $array == $GLOBALS['freeSpace']) {
                        $value = "Free Space";
                    } 
                    print_r($value);
                }
                echo "</td>";
            }
        }
        for ($k = 0; $k < $numberOfBingoRows; $k++) {
            echo '<tr>';
            createBingoCard($k);
            echo '</tr>';
        }
        ?>
        </tbody>
    </table>
    <a href="<?=substr(__FILE__, strrpos(__FILE__, '/') + 1);?>">
        <button>Refresh</button>
    </a>
</body>

</html>