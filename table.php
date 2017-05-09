<!doctype html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <title>Multiplication Table</title>
    <style type="text/css">
    table {
      padding: 5%;
    }
    tr:nth-child(even) {
      background-color: pink;
    }
    th,tr,td {
      border-style: solid;
      border-width: 1px;
      width: 5%;
    }
    td {
      text-align: center;
    }
    form, input {
      padding: 1em;
    }
    </style>
</head>

<body>
  <form action="table.php">
  Enter the multiplication factor:<br>
  <input type="text" name="max" placeholder="i.e. 12" autofocus>
  <br>
  <input type="submit" value="Submit">
  </form>
  <table>
  <?php
  if (!isset($_GET["max"])) {
    $_GET["max"] = 12;
  }

  $range = $_GET["max"];
  $range = htmlspecialchars($range);

  // generate first header
  echo '<tr>';
  echo '<th>&nbsp;</th>';
  for ($i = 1; $i <= $range; $i++) {
    echo "<th>$i</th>";
  }
  echo "</tr>";

  // generates all other rows
  for ($j = 1; $j <= $range; $j++) {
    echo '<tr>';
    echo '<th>';
    $multiplier = $j;
    echo $multiplier;
    echo '</th>';
      for ($k = $multiplier; $k <= $multiplier * $range; $k+= $multiplier) {
        echo "<td>$k</td>";
      }
    echo '</tr>';
  };
  ?>
  </table>
</body>

</html>