<?php
//fibonacci sequence number generator with sums for odd and even numbers
$fibonacciArray = [0,1];
$evensArray = [];
$oddsArray = [];
$errors = [];
$evens = 2;
$previousArrayVal = 2;

if (isset($_GET["submit"])) {
    $max = htmlentities($_GET["MaxNumber"]);
    $pattern = '/,/';
    if (preg_match($pattern, $max)) {
        $errors[] = "Use of commas is prohibited";
    }

    while ($fibonacciArray[count($fibonacciArray) - 1] < $max) { 
        $last = $fibonacciArray[count($fibonacciArray) - 1];
        $prev = $fibonacciArray[count($fibonacciArray) - $previousArrayVal];
        $fibonacciSequence = $last + $prev;

        array_push($fibonacciArray,$fibonacciSequence);

        if ($fibonacciSequence % $evens == 0) {
            array_push($evensArray, $fibonacciSequence);
        } elseif ($fibonacciSequence % $evens != 0) {
            array_push($oddsArray, $fibonacciSequence);
        }
    }

    $masterArray = [$fibonacciArray,$evensArray,$oddsArray];

    for ($i=0; $i < count($masterArray); $i++) { 
        if ($masterArray[$i][count($masterArray[$i]) - 1] > $max) {
            array_pop($masterArray[$i]);
        };
    }
}
?>
<!doctype HTML>
<html lang="en">

<head>
    <style>
    li {
        list-style: none;
    }
    body {
        background: white;
    }
    body div {
        opacity: 0.90;
        float: center;
        background: beige;
        margin: auto;
        padding: auto;
        width: 95%;
    }
    form, ul {
        float: center;
        margin: auto;
        font-size: large;
    }    
    form, input, li {
        padding: 5px;
    }
    </style>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Fibonacci Calculator</title>
</head>

<body>
    <div>
        <form action="<?php echo htmlspecialchars($_SERVER["PHP_SELF"]);?>">
        <h2>Fibonacci Sequence Generator</h2>
        <h3>Please do not use commas</h3>
            Maximum Number:
            <input type="text" name="MaxNumber" placeholder="Enter the maximum number" autofocus>
            <br>
            <input type="submit" name="submit" value="submit">
        </form>
        <br>
        <?php
        function display($title,$arrayName) {
            echo "<h4>{$title}:</h4>";
            echo "<ul>";
            
            $arrayName = array_unique($arrayName);

            foreach ($arrayName as $key => $value) {
                echo "<li>$value</li>";
            }
            echo "</ul>";
            echo "<p>Sum of " . strtolower($title) . " numbers: " . array_sum($arrayName) . "</p>";
            echo "<hr>";
        }

        global $masterArray;

        $displayArgArray = ["ALL"=>$masterArray[0],"EVEN"=>$masterArray[1],"ODD"=>$masterArray[2]];

        if (isset($_GET["submit"]) && !$errors) {
            echo "<p>Your Max Number was {$max}</p>";
            foreach ($displayArgArray as $key => $value) {
                display($key,$value);
            }
        } elseif ($errors) {
            foreach ($errors as $key => $value) {
                echo "<p>{$value}</p><br />";
            }
        }
        ?>
    </div>
</body>

</html>
